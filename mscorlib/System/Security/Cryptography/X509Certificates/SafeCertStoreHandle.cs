// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.SafeCertStoreHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Security.Cryptography.X509Certificates
{
  [SecurityCritical]
  internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeCertStoreHandle()
      : base(true)
    {
    }

    internal SafeCertStoreHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeCertStoreHandle InvalidHandle
    {
      get
      {
        return new SafeCertStoreHandle(IntPtr.Zero);
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _FreeCertStoreContext(IntPtr hCertStore);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeCertStoreHandle._FreeCertStoreContext(this.handle);
      return true;
    }
  }
}
