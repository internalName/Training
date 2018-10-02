// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.SafeCertContextHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  [SecurityCritical]
  internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeCertContextHandle()
      : base(true)
    {
    }

    internal SafeCertContextHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeCertContextHandle InvalidHandle
    {
      get
      {
        return new SafeCertContextHandle(IntPtr.Zero);
      }
    }

    internal IntPtr pCertContext
    {
      get
      {
        if (this.handle == IntPtr.Zero)
          return IntPtr.Zero;
        return Marshal.ReadIntPtr(this.handle);
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _FreePCertContext(IntPtr pCert);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeCertContextHandle._FreePCertContext(this.handle);
      return true;
    }
  }
}
