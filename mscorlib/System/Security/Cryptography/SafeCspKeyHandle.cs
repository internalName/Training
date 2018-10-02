// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeCspKeyHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeCspKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal SafeCspKeyHandle()
      : base(true)
    {
    }

    [DllImport("advapi32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptDestroyKey(IntPtr hKey);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return SafeCspKeyHandle.CryptDestroyKey(this.handle);
    }
  }
}
