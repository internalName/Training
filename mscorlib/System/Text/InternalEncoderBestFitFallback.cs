// Decompiled with JetBrains decompiler
// Type: System.Text.InternalEncoderBestFitFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  [Serializable]
  internal class InternalEncoderBestFitFallback : EncoderFallback
  {
    internal Encoding encoding;
    internal char[] arrayBestFit;

    internal InternalEncoderBestFitFallback(Encoding encoding)
    {
      this.encoding = encoding;
      this.bIsMicrosoftBestFitFallback = true;
    }

    public override EncoderFallbackBuffer CreateFallbackBuffer()
    {
      return (EncoderFallbackBuffer) new InternalEncoderBestFitFallbackBuffer(this);
    }

    public override int MaxCharCount
    {
      get
      {
        return 1;
      }
    }

    public override bool Equals(object value)
    {
      InternalEncoderBestFitFallback encoderBestFitFallback = value as InternalEncoderBestFitFallback;
      if (encoderBestFitFallback != null)
        return this.encoding.CodePage == encoderBestFitFallback.encoding.CodePage;
      return false;
    }

    public override int GetHashCode()
    {
      return this.encoding.CodePage;
    }
  }
}
