// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.AnsiBSTRMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class AnsiBSTRMarshaler
  {
    [SecurityCritical]
    internal static IntPtr ConvertToNative(int flags, string strManaged)
    {
      if (strManaged == null)
        return IntPtr.Zero;
      int length = strManaged.Length;
      System.StubHelpers.StubHelpers.CheckStringLength(length);
      byte[] str = (byte[]) null;
      int cbLength = 0;
      if (length > 0)
        str = AnsiCharMarshaler.DoAnsiConversion(strManaged, (uint) (flags & (int) byte.MaxValue) > 0U, (uint) (flags >> 8) > 0U, out cbLength);
      return Win32Native.SysAllocStringByteLen(str, (uint) cbLength);
    }

    [SecurityCritical]
    internal static unsafe string ConvertToManaged(IntPtr bstr)
    {
      if (IntPtr.Zero == bstr)
        return (string) null;
      return new string((sbyte*) (void*) bstr);
    }

    [SecurityCritical]
    internal static void ClearNative(IntPtr pNative)
    {
      if (!(IntPtr.Zero != pNative))
        return;
      Win32Native.SysFreeString(pNative);
    }
  }
}
