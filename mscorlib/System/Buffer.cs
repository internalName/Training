// Decompiled with JetBrains decompiler
// Type: System.Buffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>Манипулирует массивами простых типов.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Buffer
  {
    /// <summary>
    ///   Копирует указанное число байтов из исходного массива, начиная с определенного смещения, в конечный массив, начиная с определенного смещения.
    /// </summary>
    /// <param name="src">Исходный буфер.</param>
    /// <param name="srcOffset">
    ///   Смещение байтов (начиная с нуля) в <paramref name="src" />.
    /// </param>
    /// <param name="dst">Буфер назначения.</param>
    /// <param name="dstOffset">
    ///   Смещение байтов (начиная с нуля) в <paramref name="dst" />.
    /// </param>
    /// <param name="count">Число байт для копирования.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="src" /> или <paramref name="dst" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="src" /> или <paramref name="dst" /> —не массив примитивов.
    /// 
    ///   -или-
    /// 
    ///   Число байтов в <paramref name="src" /> меньше суммы значений <paramref name="srcOffset" /> и <paramref name="count" />.
    /// 
    ///   -или-
    /// 
    ///   Число байтов в <paramref name="dst" /> меньше суммы значений <paramref name="dstOffset" /> и <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="srcOffset" />, <paramref name="dstOffset" /> или <paramref name="count" /> меньше 0.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalBlockCopy(Array src, int srcOffsetBytes, Array dst, int dstOffsetBytes, int byteCount);

    [SecurityCritical]
    internal static unsafe int IndexOfByte(byte* src, byte value, int index, int count)
    {
      byte* numPtr;
      for (numPtr = src + index; ((int) numPtr & 3) != 0; ++numPtr)
      {
        if (count == 0)
          return -1;
        if ((int) *numPtr == (int) value)
          return (int) (numPtr - src);
        --count;
      }
      uint num1 = ((uint) value << 8) + (uint) value;
      uint num2 = (num1 << 16) + num1;
      while (count > 3)
      {
        uint num3 = *(uint*) numPtr ^ num2;
        uint num4 = 2130640639U + num3;
        if (((num3 ^ uint.MaxValue ^ num4) & 2164326656U) != 0U)
        {
          int num5 = (int) (numPtr - src);
          if ((int) *numPtr == (int) value)
            return num5;
          if ((int) numPtr[1] == (int) value)
            return num5 + 1;
          if ((int) numPtr[2] == (int) value)
            return num5 + 2;
          if ((int) numPtr[3] == (int) value)
            return num5 + 3;
        }
        count -= 4;
        numPtr += 4;
      }
      while (count > 0)
      {
        if ((int) *numPtr == (int) value)
          return (int) (numPtr - src);
        --count;
        ++numPtr;
      }
      return -1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsPrimitiveTypeArray(Array array);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern byte _GetByte(Array array, int index);

    /// <summary>
    ///   Извлекает байт из указанного места в указанном массиве.
    /// </summary>
    /// <param name="array">Массив.</param>
    /// <param name="index">Расположение в массиве.</param>
    /// <returns>
    ///   Возвращает <paramref name="index" /> байтов в массиве.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="array" />не является простым.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" />является отрицательным или превышать длину <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="array" />не должен превышать 2 гигабайта (ГБ).
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static byte GetByte(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), nameof (array));
      if (index < 0 || index >= Buffer._ByteLength(array))
        throw new ArgumentOutOfRangeException(nameof (index));
      return Buffer._GetByte(array, index);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _SetByte(Array array, int index, byte value);

    /// <summary>
    ///   Присваивает указанное значение байту в определенном месте в указанном массиве.
    /// </summary>
    /// <param name="array">Массив.</param>
    /// <param name="index">Расположение в массиве.</param>
    /// <param name="value">Присваиваемое значение.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="array" />не является простым.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" />является отрицательным или превышать длину <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="array" />не должен превышать 2 гигабайта (ГБ).
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetByte(Array array, int index, byte value)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), nameof (array));
      if (index < 0 || index >= Buffer._ByteLength(array))
        throw new ArgumentOutOfRangeException(nameof (index));
      Buffer._SetByte(array, index, value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _ByteLength(Array array);

    /// <summary>Возвращает число байтов в указанном массиве.</summary>
    /// <param name="array">Массив.</param>
    /// <returns>Число байтов в массиве.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="array" />не является простым.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="array" />не должен превышать 2 гигабайта (ГБ).
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ByteLength(Array array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), nameof (array));
      return Buffer._ByteLength(array);
    }

    [SecurityCritical]
    internal static unsafe void ZeroMemory(byte* src, long len)
    {
      while (len-- > 0L)
        src[len] = (byte) 0;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memcpy(byte[] dest, int destIndex, byte* src, int srcIndex, int len)
    {
      if (len == 0)
        return;
      fixed (byte* numPtr = dest)
        Buffer.Memcpy(numPtr + destIndex, src + srcIndex, len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
    {
      if (len == 0)
        return;
      fixed (byte* numPtr = src)
        Buffer.Memcpy(pDest + destIndex, numPtr + srcIndex, len);
    }

    [FriendAccessAllowed]
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe void Memcpy(byte* dest, byte* src, int len)
    {
      Buffer.Memmove(dest, src, (uint) len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memmove(byte* dest, byte* src, uint len)
    {
      if ((uint) dest - (uint) src >= len && (uint) src - (uint) dest >= len)
      {
        byte* numPtr1 = src + len;
        byte* numPtr2 = dest + len;
        if (len > 16U)
        {
          if (len > 64U)
            goto label_15;
label_3:
          *(Buffer.Block16*) dest = *(Buffer.Block16*) src;
          if (len > 32U)
          {
            *(Buffer.Block16*) (dest + 16) = *(Buffer.Block16*) (src + 16);
            if (len > 48U)
              *(Buffer.Block16*) (dest + 32) = *(Buffer.Block16*) (src + 32);
          }
          *(Buffer.Block16*) (numPtr2 - 16) = *(Buffer.Block16*) (numPtr1 - 16);
          return;
label_15:
          if (len <= 2048U)
          {
            uint num = len >> 6;
            do
            {
              *(Buffer.Block64*) dest = *(Buffer.Block64*) src;
              dest += 64;
              src += 64;
              --num;
            }
            while (num != 0U);
            len %= 64U;
            if (len <= 16U)
            {
              *(Buffer.Block16*) (numPtr2 - 16) = *(Buffer.Block16*) (numPtr1 - 16);
              return;
            }
            goto label_3;
          }
        }
        else
        {
          if (((int) len & 24) != 0)
          {
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (numPtr2 - 8) = *(int*) (numPtr1 - 8);
            *(int*) (numPtr2 - 4) = *(int*) (numPtr1 - 4);
            return;
          }
          if (((int) len & 4) != 0)
          {
            *(int*) dest = *(int*) src;
            *(int*) (numPtr2 - 4) = *(int*) (numPtr1 - 4);
            return;
          }
          if (len == 0U)
            return;
          *dest = *src;
          if (((int) len & 2) == 0)
            return;
          *(short*) (numPtr2 - 2) = *(short*) (numPtr1 - 2);
          return;
        }
      }
      Buffer._Memmove(dest, src, len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static unsafe void _Memmove(byte* dest, byte* src, uint len)
    {
      Buffer.__Memmove(dest, src, len);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void __Memmove(byte* dest, byte* src, uint len);

    /// <summary>
    ///   Копирует число байтов, указанное как длинное целое значение, из одного адреса в памяти в другой.
    /// 
    ///   Этот интерфейс API CLS-несовместим.
    /// </summary>
    /// <param name="source">Адрес байтов для копирования.</param>
    /// <param name="destination">Целевой адрес.</param>
    /// <param name="destinationSizeInBytes">
    ///   Число доступных байтов в конечном блоке памяти.
    /// </param>
    /// <param name="sourceBytesToCopy">
    ///   Число байт для копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="sourceBytesToCopy" /> больше значения <paramref name="destinationSizeInBytes" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
    {
      if (sourceBytesToCopy > destinationSizeInBytes)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
      Buffer.Memmove((byte*) destination, (byte*) source, checked ((uint) sourceBytesToCopy));
    }

    /// <summary>
    ///   Копирует число байтов, указанное как длинное целое значение без знака, из одного адреса в памяти в другой.
    /// 
    ///   Этот интерфейс API CLS-несовместим.
    /// </summary>
    /// <param name="source">Адрес байтов для копирования.</param>
    /// <param name="destination">Целевой адрес.</param>
    /// <param name="destinationSizeInBytes">
    ///   Число доступных байтов в конечном блоке памяти.
    /// </param>
    /// <param name="sourceBytesToCopy">
    ///   Число байт для копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="sourceBytesToCopy" /> больше значения <paramref name="destinationSizeInBytes" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void MemoryCopy(void* source, void* destination, ulong destinationSizeInBytes, ulong sourceBytesToCopy)
    {
      if (sourceBytesToCopy > destinationSizeInBytes)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
      Buffer.Memmove((byte*) destination, (byte*) source, checked ((uint) sourceBytesToCopy));
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    private struct Block16
    {
    }

    [StructLayout(LayoutKind.Sequential, Size = 64)]
    private struct Block64
    {
    }
  }
}
