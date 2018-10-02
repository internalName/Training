// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeLsaReturnBufferHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafeLsaReturnBufferHandle : SafeBuffer
  {
    private SafeLsaReturnBufferHandle()
      : base(true)
    {
    }

    internal SafeLsaReturnBufferHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeLsaReturnBufferHandle InvalidHandle
    {
      get
      {
        return new SafeLsaReturnBufferHandle(IntPtr.Zero);
      }
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.LsaFreeReturnBuffer(this.handle) >= 0;
    }
  }
}
