// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.BSTRMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class BSTRMarshaler
  {
    [SecurityCritical]
    internal static unsafe IntPtr ConvertToNative(string strManaged, IntPtr pNativeBuffer)
    {
      if (strManaged == null)
        return IntPtr.Zero;
      System.StubHelpers.StubHelpers.CheckStringLength(strManaged.Length);
      byte data;
      bool trailByte = strManaged.TryGetTrailByte(out data);
      uint len = (uint) (strManaged.Length * 2);
      if (trailByte)
        ++len;
      byte* dest;
      if (pNativeBuffer != IntPtr.Zero)
      {
        *(int*) pNativeBuffer.ToPointer() = (int) len;
        dest = (byte*) ((IntPtr) pNativeBuffer.ToPointer() + 4);
      }
      else
        dest = (byte*) Win32Native.SysAllocStringByteLen((byte[]) null, len).ToPointer();
      string str = strManaged;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      Buffer.Memcpy(dest, (byte*) chPtr, (strManaged.Length + 1) * 2);
      str = (string) null;
      if (trailByte)
        dest[len - 1U] = data;
      return (IntPtr) ((void*) dest);
    }

    [SecurityCritical]
    internal static unsafe string ConvertToManaged(IntPtr bstr)
    {
      if (IntPtr.Zero == bstr)
        return (string) null;
      uint length = Win32Native.SysStringByteLen(bstr);
      System.StubHelpers.StubHelpers.CheckStringLength(length);
      string str = length != 1U ? new string((char*) (void*) bstr, 0, (int) (length / 2U)) : string.FastAllocateString(0);
      if (((int) length & 1) == 1)
        str.SetTrailByte(*(byte*) ((IntPtr) bstr.ToPointer() + (IntPtr) (length - 1U)));
      return str;
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
