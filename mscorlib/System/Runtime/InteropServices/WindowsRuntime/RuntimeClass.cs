// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.RuntimeClass
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal abstract class RuntimeClass : __ComObject
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetRedirectedGetHashCodeMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern int RedirectGetHashCode(IntPtr pMD);

    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      IntPtr redirectedGetHashCodeMd = this.GetRedirectedGetHashCodeMD();
      if (redirectedGetHashCodeMd == IntPtr.Zero)
        return base.GetHashCode();
      return this.RedirectGetHashCode(redirectedGetHashCodeMd);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetRedirectedToStringMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern string RedirectToString(IntPtr pMD);

    [SecuritySafeCritical]
    public override string ToString()
    {
      IStringable stringable = this as IStringable;
      if (stringable != null)
        return stringable.ToString();
      IntPtr redirectedToStringMd = this.GetRedirectedToStringMD();
      if (redirectedToStringMd == IntPtr.Zero)
        return base.ToString();
      return this.RedirectToString(redirectedToStringMd);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetRedirectedEqualsMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool RedirectEquals(object obj, IntPtr pMD);

    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      IntPtr redirectedEqualsMd = this.GetRedirectedEqualsMD();
      if (redirectedEqualsMd == IntPtr.Zero)
        return base.Equals(obj);
      return this.RedirectEquals(obj, redirectedEqualsMd);
    }
  }
}
