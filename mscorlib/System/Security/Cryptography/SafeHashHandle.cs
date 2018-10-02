// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeHashHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeHashHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    private SafeHashHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeHashHandle InvalidHandle
    {
      get
      {
        return new SafeHashHandle();
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void FreeHash(IntPtr pHashContext);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeHashHandle.FreeHash(this.handle);
      return true;
    }
  }
}
