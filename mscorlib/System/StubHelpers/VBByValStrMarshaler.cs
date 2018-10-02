// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.VBByValStrMarshaler
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
  internal static class VBByValStrMarshaler
  {
    [SecurityCritical]
    internal static unsafe IntPtr ConvertToNative(string strManaged, bool fBestFit, bool fThrowOnUnmappableChar, ref int cch)
    {
      if (strManaged == null)
        return IntPtr.Zero;
      cch = strManaged.Length;
      System.StubHelpers.StubHelpers.CheckStringLength(cch);
      byte* numPtr1 = (byte*) (void*) Marshal.AllocCoTaskMem(4 + (cch + 1) * Marshal.SystemMaxDBCSCharSize);
      int* numPtr2 = (int*) numPtr1;
      byte* pDest = numPtr1 + 4;
      if (cch == 0)
      {
        *pDest = (byte) 0;
        *numPtr2 = 0;
      }
      else
      {
        int cbLength;
        byte[] src = AnsiCharMarshaler.DoAnsiConversion(strManaged, fBestFit, fThrowOnUnmappableChar, out cbLength);
        Buffer.Memcpy(pDest, 0, src, 0, cbLength);
        pDest[cbLength] = (byte) 0;
        *numPtr2 = cbLength;
      }
      return new IntPtr((void*) pDest);
    }

    [SecurityCritical]
    internal static unsafe string ConvertToManaged(IntPtr pNative, int cch)
    {
      if (IntPtr.Zero == pNative)
        return (string) null;
      return new string((sbyte*) (void*) pNative, 0, cch);
    }

    [SecurityCritical]
    internal static void ClearNative(IntPtr pNative)
    {
      if (!(IntPtr.Zero != pNative))
        return;
      Win32Native.CoTaskMemFree((IntPtr) ((long) pNative - 4L));
    }
  }
}
