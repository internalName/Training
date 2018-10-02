// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeLocalAllocHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafeLocalAllocHandle : SafeBuffer
  {
    private SafeLocalAllocHandle()
      : base(true)
    {
    }

    internal SafeLocalAllocHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    internal static SafeLocalAllocHandle InvalidHandle
    {
      get
      {
        return new SafeLocalAllocHandle(IntPtr.Zero);
      }
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.LocalFree(this.handle) == IntPtr.Zero;
    }
  }
}
