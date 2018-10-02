// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.UTF8Marshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class UTF8Marshaler
  {
    private const int MAX_UTF8_CHAR_SIZE = 3;

    [SecurityCritical]
    internal static unsafe IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
    {
      if (strManaged == null)
        return IntPtr.Zero;
      System.StubHelpers.StubHelpers.CheckStringLength(strManaged.Length);
      byte* pbNativeBuffer = (byte*) (void*) pNativeBuffer;
      int cbNativeBuffer1;
      if ((IntPtr) pbNativeBuffer != IntPtr.Zero)
      {
        int cbNativeBuffer2 = (strManaged.Length + 1) * 3;
        cbNativeBuffer1 = strManaged.GetBytesFromEncoding(pbNativeBuffer, cbNativeBuffer2, Encoding.UTF8);
      }
      else
      {
        cbNativeBuffer1 = Encoding.UTF8.GetByteCount(strManaged);
        pbNativeBuffer = (byte*) (void*) Marshal.AllocCoTaskMem(cbNativeBuffer1 + 1);
        strManaged.GetBytesFromEncoding(pbNativeBuffer, cbNativeBuffer1, Encoding.UTF8);
      }
      pbNativeBuffer[cbNativeBuffer1] = (byte) 0;
      return (IntPtr) ((void*) pbNativeBuffer);
    }

    [SecurityCritical]
    internal static unsafe string ConvertToManaged(IntPtr cstr)
    {
      if (IntPtr.Zero == cstr)
        return (string) null;
      int byteLength = System.StubHelpers.StubHelpers.strlen((sbyte*) (void*) cstr);
      return string.CreateStringFromEncoding((byte*) (void*) cstr, byteLength, Encoding.UTF8);
    }

    [SecurityCritical]
    internal static void ClearNative(IntPtr pNative)
    {
      if (!(pNative != IntPtr.Zero))
        return;
      Win32Native.CoTaskMemFree(pNative);
    }
  }
}
