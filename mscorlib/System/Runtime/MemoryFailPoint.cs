// Decompiled with JetBrains decompiler
// Type: System.Runtime.MemoryFailPoint
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime
{
  /// <summary>
  ///   Проверяет наличие достаточного количество ресурсов памяти перед выполнением операции.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
  {
    private static readonly ulong TopOfMemory;
    private static long hiddenLastKnownFreeAddressSpace;
    private static long hiddenLastTimeCheckingAddressSpace;
    private const int CheckThreshold = 10000;
    private const int LowMemoryFudgeFactor = 16777216;
    private const int MemoryCheckGranularity = 16;
    private static readonly ulong GCSegmentSize;
    private ulong _reservedMemory;
    private bool _mustSubtractReservation;

    private static long LastKnownFreeAddressSpace
    {
      get
      {
        return Volatile.Read(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace);
      }
      set
      {
        Volatile.Write(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, value);
      }
    }

    private static long AddToLastKnownFreeAddressSpace(long addend)
    {
      return Interlocked.Add(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, addend);
    }

    private static long LastTimeCheckingAddressSpace
    {
      get
      {
        return Volatile.Read(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace);
      }
      set
      {
        Volatile.Write(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace, value);
      }
    }

    [SecuritySafeCritical]
    static MemoryFailPoint()
    {
      MemoryFailPoint.GetMemorySettings(out MemoryFailPoint.GCSegmentSize, out MemoryFailPoint.TopOfMemory);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.MemoryFailPoint" />, указывающий объем памяти, необходимый для успешного выполнения.
    /// </summary>
    /// <param name="sizeInMegabytes">
    ///   Необходимый объем памяти в мегабайтах.
    ///    Это значение должно быть положительным.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Заданный объем памяти является отрицательным.
    /// </exception>
    /// <exception cref="T:System.InsufficientMemoryException">
    ///   Не хватает памяти, чтобы начать выполнение кода, защищенного с помощью шлюза.
    /// </exception>
    [SecurityCritical]
    public unsafe MemoryFailPoint(int sizeInMegabytes)
    {
      if (sizeInMegabytes <= 0)
        throw new ArgumentOutOfRangeException(nameof (sizeInMegabytes), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      ulong num1 = (ulong) sizeInMegabytes << 20;
      this._reservedMemory = num1;
      ulong size = (ulong) (Math.Ceiling((double) num1 / (double) MemoryFailPoint.GCSegmentSize) * (double) MemoryFailPoint.GCSegmentSize);
      if (size >= MemoryFailPoint.TopOfMemory)
        throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_TooBig"));
      ulong num2 = (ulong) (Math.Ceiling((double) sizeInMegabytes / 16.0) * 16.0) << 20;
      ulong availPageFile = 0;
      ulong totalAddressSpaceFree = 0;
      for (int index = 0; index < 3; ++index)
      {
        MemoryFailPoint.CheckForAvailableMemory(out availPageFile, out totalAddressSpaceFree);
        ulong pointReservedMemory = SharedStatics.MemoryFailPointReservedMemory;
        ulong num3 = size + pointReservedMemory;
        bool flag1 = num3 < size || num3 < pointReservedMemory;
        bool flag2 = availPageFile < num2 + pointReservedMemory + 16777216UL | flag1;
        bool flag3 = totalAddressSpaceFree < num3 | flag1;
        long tickCount = (long) Environment.TickCount;
        if (tickCount > MemoryFailPoint.LastTimeCheckingAddressSpace + 10000L || tickCount < MemoryFailPoint.LastTimeCheckingAddressSpace || MemoryFailPoint.LastKnownFreeAddressSpace < (long) size)
          MemoryFailPoint.CheckForFreeAddressSpace(size, false);
        bool flag4 = (ulong) MemoryFailPoint.LastKnownFreeAddressSpace < size;
        if (flag2 || flag3 || flag4)
        {
          switch (index)
          {
            case 0:
              GC.Collect();
              break;
            case 1:
              if (flag2)
              {
                RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                }
                finally
                {
                  void* address = Win32Native.VirtualAlloc((void*) null, new UIntPtr(size), 4096, 4);
                  if ((IntPtr) address != IntPtr.Zero && !Win32Native.VirtualFree(address, UIntPtr.Zero, 32768))
                    __Error.WinIOError();
                }
                break;
              }
              break;
            case 2:
              if (flag2 | flag3)
                throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint"));
              if (flag4)
                throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
              break;
          }
        }
        else
          break;
      }
      MemoryFailPoint.AddToLastKnownFreeAddressSpace(-(long) num1);
      if (MemoryFailPoint.LastKnownFreeAddressSpace < 0L)
        MemoryFailPoint.CheckForFreeAddressSpace(size, true);
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        SharedStatics.AddMemoryFailPointReservation((long) num1);
        this._mustSubtractReservation = true;
      }
    }

    [SecurityCritical]
    private static void CheckForAvailableMemory(out ulong availPageFile, out ulong totalAddressSpaceFree)
    {
      Win32Native.MEMORYSTATUSEX buffer = new Win32Native.MEMORYSTATUSEX();
      if (!Win32Native.GlobalMemoryStatusEx(ref buffer))
        __Error.WinIOError();
      availPageFile = buffer.availPageFile;
      totalAddressSpaceFree = buffer.availVirtual;
    }

    [SecurityCritical]
    private static unsafe bool CheckForFreeAddressSpace(ulong size, bool shouldThrow)
    {
      ulong num = MemoryFailPoint.MemFreeAfterAddress((void*) null, size);
      MemoryFailPoint.LastKnownFreeAddressSpace = (long) num;
      MemoryFailPoint.LastTimeCheckingAddressSpace = (long) Environment.TickCount;
      if (num < size & shouldThrow)
        throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
      return num >= size;
    }

    [SecurityCritical]
    private static unsafe ulong MemFreeAfterAddress(void* address, ulong size)
    {
      if (size >= MemoryFailPoint.TopOfMemory)
        return 0;
      ulong val1 = 0;
      Win32Native.MEMORY_BASIC_INFORMATION buffer = new Win32Native.MEMORY_BASIC_INFORMATION();
      UIntPtr sizeOfBuffer = (UIntPtr) ((ulong) Marshal.SizeOf<Win32Native.MEMORY_BASIC_INFORMATION>(buffer));
      ulong uint64;
      for (; (ulong) address + size < MemoryFailPoint.TopOfMemory; address = (void*) ((ulong) address + uint64))
      {
        if (Win32Native.VirtualQuery(address, ref buffer, sizeOfBuffer) == UIntPtr.Zero)
          __Error.WinIOError();
        uint64 = buffer.RegionSize.ToUInt64();
        if (buffer.State == 65536U)
        {
          if (uint64 >= size)
            return uint64;
          val1 = Math.Max(val1, uint64);
        }
      }
      return val1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetMemorySettings(out ulong maxGCSegmentSize, out ulong topOfMemory);

    /// <summary>
    ///   Обеспечивает освобождение ресурсов и выполнение других завершающих операций, когда сборщик мусора восстанавливает объект <see cref="T:System.Runtime.MemoryFailPoint" />.
    /// </summary>
    [SecuritySafeCritical]
    ~MemoryFailPoint()
    {
      this.Dispose(false);
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Runtime.MemoryFailPoint" />.
    /// </summary>
    [SecuritySafeCritical]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void Dispose(bool disposing)
    {
      if (!this._mustSubtractReservation)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        SharedStatics.AddMemoryFailPointReservation(-(long) this._reservedMemory);
        this._mustSubtractReservation = false;
      }
    }
  }
}
