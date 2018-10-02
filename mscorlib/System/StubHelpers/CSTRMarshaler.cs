// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.CSTRMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class CSTRMarshaler
  {
    [SecurityCritical]
    internal static unsafe IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
    {
      if (strManaged == null)
        return IntPtr.Zero;
      System.StubHelpers.StubHelpers.CheckStringLength(strManaged.Length);
      byte* numPtr = (byte*) (void*) pNativeBuffer;
      int cbLength;
      if ((IntPtr) numPtr != IntPtr.Zero || Marshal.SystemMaxDBCSCharSize == 1)
      {
        int num = (strManaged.Length + 1) * Marshal.SystemMaxDBCSCharSize;
        if ((IntPtr) numPtr == IntPtr.Zero)
          numPtr = (byte*) (void*) Marshal.AllocCoTaskMem(num + 1);
        cbLength = strManaged.ConvertToAnsi(numPtr, num + 1, (uint) (flags & (int) byte.MaxValue) > 0U, (uint) (flags >> 8) > 0U);
      }
      else
      {
        byte[] src = AnsiCharMarshaler.DoAnsiConversion(strManaged, (uint) (flags & (int) byte.MaxValue) > 0U, (uint) (flags >> 8) > 0U, out cbLength);
        numPtr = (byte*) (void*) Marshal.AllocCoTaskMem(cbLength + 2);
        Buffer.Memcpy(numPtr, 0, src, 0, cbLength);
      }
      numPtr[cbLength] = (byte) 0;
      numPtr[cbLength + 1] = (byte) 0;
      return (IntPtr) ((void*) numPtr);
    }

    [SecurityCritical]
    internal static unsafe string ConvertToManaged(IntPtr cstr)
    {
      if (IntPtr.Zero == cstr)
        return (string) null;
      return new string((sbyte*) (void*) cstr);
    }

    [SecurityCritical]
    internal static void ClearNative(IntPtr pNative)
    {
      Win32Native.CoTaskMemFree(pNative);
    }
  }
}
