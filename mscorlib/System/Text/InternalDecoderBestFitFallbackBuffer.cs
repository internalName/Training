// Decompiled with JetBrains decompiler
// Type: System.Text.InternalDecoderBestFitFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Text
{
  internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
  {
    internal int iCount = -1;
    internal char cBestFit;
    internal int iSize;
    private InternalDecoderBestFitFallback oFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, obj, (object) null);
        }
        return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
      }
    }

    public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
    {
      this.oFallback = fallback;
      if (this.oFallback.arrayBestFit != null)
        return;
      lock (InternalDecoderBestFitFallbackBuffer.InternalSyncObject)
      {
        if (this.oFallback.arrayBestFit != null)
          return;
        this.oFallback.arrayBestFit = fallback.encoding.GetBestFitBytesToUnicodeData();
      }
    }

    public override bool Fallback(byte[] bytesUnknown, int index)
    {
      this.cBestFit = this.TryBestFit(bytesUnknown);
      if (this.cBestFit == char.MinValue)
        this.cBestFit = this.oFallback.cReplacement;
      this.iCount = this.iSize = 1;
      return true;
    }

    public override char GetNextChar()
    {
      --this.iCount;
      if (this.iCount < 0)
        return char.MinValue;
      if (this.iCount != int.MaxValue)
        return this.cBestFit;
      this.iCount = -1;
      return char.MinValue;
    }

    public override bool MovePrevious()
    {
      if (this.iCount >= 0)
        ++this.iCount;
      if (this.iCount >= 0)
        return this.iCount <= this.iSize;
      return false;
    }

    public override int Remaining
    {
      get
      {
        if (this.iCount <= 0)
          return 0;
        return this.iCount;
      }
    }

    [SecuritySafeCritical]
    public override unsafe void Reset()
    {
      this.iCount = -1;
      this.byteStart = (byte*) null;
    }

    [SecurityCritical]
    internal override unsafe int InternalFallback(byte[] bytes, byte* pBytes)
    {
      return 1;
    }

    private char TryBestFit(byte[] bytesCheck)
    {
      int num1 = 0;
      int num2 = this.oFallback.arrayBestFit.Length;
      if (num2 == 0 || bytesCheck.Length == 0 || bytesCheck.Length > 2)
        return char.MinValue;
      char ch1 = bytesCheck.Length != 1 ? (char) (((uint) bytesCheck[0] << 8) + (uint) bytesCheck[1]) : (char) bytesCheck[0];
      if ((int) ch1 < (int) this.oFallback.arrayBestFit[0] || (int) ch1 > (int) this.oFallback.arrayBestFit[num2 - 2])
        return char.MinValue;
      int num3;
      while ((num3 = num2 - num1) > 6)
      {
        int index = num3 / 2 + num1 & 65534;
        char ch2 = this.oFallback.arrayBestFit[index];
        if ((int) ch2 == (int) ch1)
          return this.oFallback.arrayBestFit[index + 1];
        if ((int) ch2 < (int) ch1)
          num1 = index;
        else
          num2 = index;
      }
      int index1 = num1;
      while (index1 < num2)
      {
        if ((int) this.oFallback.arrayBestFit[index1] == (int) ch1)
          return this.oFallback.arrayBestFit[index1 + 1];
        index1 += 2;
      }
      return char.MinValue;
    }
  }
}
