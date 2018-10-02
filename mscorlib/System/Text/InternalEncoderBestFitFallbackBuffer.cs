// Decompiled with JetBrains decompiler
// Type: System.Text.InternalEncoderBestFitFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Text
{
  internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
  {
    private int iCount = -1;
    private char cBestFit;
    private InternalEncoderBestFitFallback oFallback;
    private int iSize;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, (object) null);
        }
        return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
      }
    }

    public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
    {
      this.oFallback = fallback;
      if (this.oFallback.arrayBestFit != null)
        return;
      lock (InternalEncoderBestFitFallbackBuffer.InternalSyncObject)
      {
        if (this.oFallback.arrayBestFit != null)
          return;
        this.oFallback.arrayBestFit = fallback.encoding.GetBestFitUnicodeToBytesData();
      }
    }

    public override bool Fallback(char charUnknown, int index)
    {
      this.iCount = this.iSize = 1;
      this.cBestFit = this.TryBestFit(charUnknown);
      if (this.cBestFit == char.MinValue)
        this.cBestFit = '?';
      return true;
    }

    public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException(nameof (charUnknownHigh), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      this.cBestFit = '?';
      this.iCount = this.iSize = 2;
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
      this.charStart = (char*) null;
      this.bFallingBack = false;
    }

    private char TryBestFit(char cUnknown)
    {
      int num1 = 0;
      int num2 = this.oFallback.arrayBestFit.Length;
      int num3;
      while ((num3 = num2 - num1) > 6)
      {
        int index = num3 / 2 + num1 & 65534;
        char ch = this.oFallback.arrayBestFit[index];
        if ((int) ch == (int) cUnknown)
          return this.oFallback.arrayBestFit[index + 1];
        if ((int) ch < (int) cUnknown)
          num1 = index;
        else
          num2 = index;
      }
      int index1 = num1;
      while (index1 < num2)
      {
        if ((int) this.oFallback.arrayBestFit[index1] == (int) cUnknown)
          return this.oFallback.arrayBestFit[index1 + 1];
        index1 += 2;
      }
      return char.MinValue;
    }
  }
}
