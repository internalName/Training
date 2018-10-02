// Decompiled with JetBrains decompiler
// Type: System.SafeTypeNameParserHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  [SecurityCritical]
  internal class SafeTypeNameParserHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _ReleaseTypeNameParser(IntPtr pTypeNameParser);

    public SafeTypeNameParserHandle()
      : base(true)
    {
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeTypeNameParserHandle._ReleaseTypeNameParser(this.handle);
      this.handle = IntPtr.Zero;
      return true;
    }
  }
}
