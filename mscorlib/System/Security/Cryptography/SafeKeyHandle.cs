// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeKeyHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeKeyHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    private SafeKeyHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeKeyHandle InvalidHandle
    {
      get
      {
        return new SafeKeyHandle();
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void FreeKey(IntPtr pKeyCotext);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeKeyHandle.FreeKey(this.handle);
      return true;
    }
  }
}
