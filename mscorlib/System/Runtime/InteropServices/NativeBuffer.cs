// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.NativeBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  internal class NativeBuffer : IDisposable
  {
    [SecurityCritical]
    private static readonly SafeHandle s_emptyHandle = (SafeHandle) new NativeBuffer.EmptySafeHandle();
    private static readonly SafeHeapHandleCache s_handleCache = new SafeHeapHandleCache(64UL, 2048UL, 0);
    [SecurityCritical]
    private SafeHeapHandle _handle;
    private ulong _capacity;

    [SecuritySafeCritical]
    static NativeBuffer()
    {
    }

    public NativeBuffer(ulong initialMinCapacity = 0)
    {
      this.EnsureByteCapacity(initialMinCapacity);
    }

    protected unsafe void* VoidPointer
    {
      [SecurityCritical, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        if (this._handle != null)
          return this._handle.DangerousGetHandle().ToPointer();
        return (void*) null;
      }
    }

    protected unsafe byte* BytePointer
    {
      [SecurityCritical] get
      {
        return (byte*) this.VoidPointer;
      }
    }

    [SecuritySafeCritical]
    public SafeHandle GetHandle()
    {
      return (SafeHandle) this._handle ?? NativeBuffer.s_emptyHandle;
    }

    public ulong ByteCapacity
    {
      get
      {
        return this._capacity;
      }
    }

    [SecuritySafeCritical]
    public void EnsureByteCapacity(ulong minCapacity)
    {
      if (this._capacity >= minCapacity)
        return;
      this.Resize(minCapacity);
      this._capacity = minCapacity;
    }

    public unsafe byte this[ulong index]
    {
      [SecuritySafeCritical] get
      {
        if (index >= this._capacity)
          throw new ArgumentOutOfRangeException();
        return this.BytePointer[index];
      }
      [SecuritySafeCritical] set
      {
        if (index >= this._capacity)
          throw new ArgumentOutOfRangeException();
        this.BytePointer[index] = value;
      }
    }

    [SecuritySafeCritical]
    private void Resize(ulong byteLength)
    {
      if (byteLength == 0UL)
        this.ReleaseHandle();
      else if (this._handle == null)
        this._handle = NativeBuffer.s_handleCache.Acquire(byteLength);
      else
        this._handle.Resize(byteLength);
    }

    [SecuritySafeCritical]
    private void ReleaseHandle()
    {
      if (this._handle == null)
        return;
      NativeBuffer.s_handleCache.Release(this._handle);
      this._capacity = 0UL;
      this._handle = (SafeHeapHandle) null;
    }

    [SecuritySafeCritical]
    public virtual void Free()
    {
      this.ReleaseHandle();
    }

    [SecuritySafeCritical]
    public void Dispose()
    {
      this.Free();
    }

    [SecurityCritical]
    private sealed class EmptySafeHandle : SafeHandle
    {
      public EmptySafeHandle()
        : base(IntPtr.Zero, true)
      {
      }

      public override bool IsInvalid
      {
        [SecurityCritical] get
        {
          return true;
        }
      }

      [SecurityCritical]
      protected override bool ReleaseHandle()
      {
        return true;
      }
    }
  }
}
