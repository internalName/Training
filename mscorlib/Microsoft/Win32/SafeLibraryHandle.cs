// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeLibraryHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32
{
  [SecurityCritical]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal SafeLibraryHandle()
      : base(true)
    {
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return UnsafeNativeMethods.FreeLibrary(this.handle);
    }
  }
}
