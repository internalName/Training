// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.AnsiCharMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class AnsiCharMarshaler
  {
    [SecurityCritical]
    internal static unsafe byte[] DoAnsiConversion(string str, bool fBestFit, bool fThrowOnUnmappableChar, out int cbLength)
    {
      byte[] numArray = new byte[(str.Length + 1) * Marshal.SystemMaxDBCSCharSize];
      fixed (byte* pbNativeBuffer = numArray)
        cbLength = str.ConvertToAnsi(pbNativeBuffer, numArray.Length, fBestFit, fThrowOnUnmappableChar);
      return numArray;
    }

    [SecurityCritical]
    internal static unsafe byte ConvertToNative(char managedChar, bool fBestFit, bool fThrowOnUnmappableChar)
    {
      int cbNativeBuffer = 2 * Marshal.SystemMaxDBCSCharSize;
      byte* pbNativeBuffer = stackalloc byte[cbNativeBuffer];
      managedChar.ToString().ConvertToAnsi(pbNativeBuffer, cbNativeBuffer, fBestFit, fThrowOnUnmappableChar);
      return pbNativeBuffer[0];
    }

    internal static char ConvertToManaged(byte nativeChar)
    {
      return Encoding.Default.GetString(new byte[1]
      {
        nativeChar
      })[0];
    }
  }
}
