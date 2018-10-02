// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeHeapHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  [SecurityCritical]
  internal sealed class SafeHeapHandle : SafeBuffer
  {
    public SafeHeapHandle(ulong byteLength)
      : base(true)
    {
      this.Resize(byteLength);
    }

    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        return this.handle == IntPtr.Zero;
      }
    }

    public void Resize(ulong byteLength)
    {
      if (this.IsClosed)
        throw new ObjectDisposedException(nameof (SafeHeapHandle));
      ulong num1 = 0;
      if (this.handle == IntPtr.Zero)
      {
        this.handle = Marshal.AllocHGlobal((IntPtr) ((long) byteLength));
      }
      else
      {
        num1 = this.ByteLength;
        this.handle = Marshal.ReAllocHGlobal(this.handle, (IntPtr) ((long) byteLength));
      }
      if (this.handle == IntPtr.Zero)
        throw new OutOfMemoryException();
      if (byteLength > num1)
      {
        ulong num2 = byteLength - num1;
        if (num2 > (ulong) long.MaxValue)
        {
          GC.AddMemoryPressure(long.MaxValue);
          GC.AddMemoryPressure((long) num2 - long.MaxValue);
        }
        else
          GC.AddMemoryPressure((long) num2);
      }
      else
        this.RemoveMemoryPressure(num1 - byteLength);
      this.Initialize(byteLength);
    }

    private void RemoveMemoryPressure(ulong removedBytes)
    {
      if (removedBytes == 0UL)
        return;
      if (removedBytes > (ulong) long.MaxValue)
      {
        GC.RemoveMemoryPressure(long.MaxValue);
        GC.RemoveMemoryPressure((long) removedBytes - long.MaxValue);
      }
      else
        GC.RemoveMemoryPressure((long) removedBytes);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      IntPtr handle = this.handle;
      this.handle = IntPtr.Zero;
      if (handle != IntPtr.Zero)
      {
        this.RemoveMemoryPressure(this.ByteLength);
        Marshal.FreeHGlobal(handle);
      }
      return true;
    }
  }
}
