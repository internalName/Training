// Decompiled with JetBrains decompiler
// Type: System.SizedReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
  internal class SizedReference : IDisposable
  {
    internal volatile IntPtr _handle;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr CreateSizedRef(object o);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FreeSizedRef(IntPtr h);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object GetTargetOfSizedRef(IntPtr h);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern long GetApproximateSizeOfSizedRef(IntPtr h);

    [SecuritySafeCritical]
    private void Free()
    {
      IntPtr handle = this._handle;
      if (!(handle != IntPtr.Zero) || !(Interlocked.CompareExchange(ref this._handle, IntPtr.Zero, handle) == handle))
        return;
      SizedReference.FreeSizedRef(handle);
    }

    [SecuritySafeCritical]
    public SizedReference(object target)
    {
      IntPtr zero = IntPtr.Zero;
      this._handle = SizedReference.CreateSizedRef(target);
    }

    ~SizedReference()
    {
      this.Free();
    }

    public object Target
    {
      [SecuritySafeCritical] get
      {
        IntPtr handle = this._handle;
        if (handle == IntPtr.Zero)
          return (object) null;
        object targetOfSizedRef = SizedReference.GetTargetOfSizedRef(handle);
        if (!(this._handle == IntPtr.Zero))
          return targetOfSizedRef;
        return (object) null;
      }
    }

    public long ApproximateSize
    {
      [SecuritySafeCritical] get
      {
        IntPtr handle = this._handle;
        if (handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        long approximateSizeOfSizedRef = SizedReference.GetApproximateSizeOfSizedRef(handle);
        if (this._handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        return approximateSizeOfSizedRef;
      }
    }

    public void Dispose()
    {
      this.Free();
      GC.SuppressFinalize((object) this);
    }
  }
}
