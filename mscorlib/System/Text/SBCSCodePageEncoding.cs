// Decompiled with JetBrains decompiler
// Type: System.Text.SBCSCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  [Serializable]
  internal class SBCSCodePageEncoding : BaseCodePageEncoding, ISerializable
  {
    [SecurityCritical]
    [NonSerialized]
    private unsafe char* mapBytesToUnicode = (char*) null;
    [SecurityCritical]
    [NonSerialized]
    private unsafe byte* mapUnicodeToBytes = (byte*) null;
    [SecurityCritical]
    [NonSerialized]
    private unsafe int* mapCodePageCached = (int*) null;
    private const char UNKNOWN_CHAR = '�';
    [NonSerialized]
    private byte byteUnknown;
    [NonSerialized]
    private char charUnknown;
    private static object s_InternalSyncObject;

    [SecurityCritical]
    public SBCSCodePageEncoding(int codePage)
      : this(codePage, codePage)
    {
    }

    [SecurityCritical]
    internal unsafe SBCSCodePageEncoding(int codePage, int dataCodePage)
      : base(codePage, dataCodePage)
    {
    }

    [SecurityCritical]
    internal unsafe SBCSCodePageEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentNullException("this");
    }

    [SecurityCritical]
    protected override unsafe void LoadManagedCodePage()
    {
      if (this.pCodePage->ByteCount != (short) 1)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.CodePage));
      this.byteUnknown = (byte) this.pCodePage->ByteReplace;
      this.charUnknown = this.pCodePage->UnicodeReplace;
      byte* sharedMemory = this.GetSharedMemory(66052 + this.iExtraBytes);
      this.mapBytesToUnicode = (char*) sharedMemory;
      this.mapUnicodeToBytes = sharedMemory + 512;
      this.mapCodePageCached = (int*) (sharedMemory + 512 + 65536 + this.iExtraBytes);
      if (*this.mapCodePageCached != 0)
      {
        if (*this.mapCodePageCached != this.dataTableCodePage)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else
      {
        char* chPtr = (char*) &this.pCodePage->FirstDataWord;
        for (int index = 0; index < 256; ++index)
        {
          if (chPtr[index] != char.MinValue || index == 0)
          {
            this.mapBytesToUnicode[index] = chPtr[index];
            if (chPtr[index] != '�')
              this.mapUnicodeToBytes[(int) chPtr[index]] = (byte) index;
          }
          else
            this.mapBytesToUnicode[index] = '�';
        }
        *this.mapCodePageCached = this.dataTableCodePage;
      }
    }

    private static object InternalSyncObject
    {
      get
      {
        if (SBCSCodePageEncoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref SBCSCodePageEncoding.s_InternalSyncObject, obj, (object) null);
        }
        return SBCSCodePageEncoding.s_InternalSyncObject;
      }
    }

    [SecurityCritical]
    protected override unsafe void ReadBestFitTable()
    {
      lock (SBCSCodePageEncoding.InternalSyncObject)
      {
        if (this.arrayUnicodeBestFit != null)
          return;
        byte* numPtr1 = (byte*) ((IntPtr) &this.pCodePage->FirstDataWord + 512);
        char[] chArray1 = new char[256];
        for (int index = 0; index < 256; ++index)
          chArray1[index] = this.mapBytesToUnicode[index];
        byte* numPtr2;
        ushort num1;
        for (; (num1 = *(ushort*) numPtr1) != (ushort) 0; numPtr1 = numPtr2 + 2)
        {
          numPtr2 = numPtr1 + 2;
          chArray1[(int) num1] = (char) *(ushort*) numPtr2;
        }
        this.arrayBytesBestFit = chArray1;
        byte* numPtr3 = numPtr1 + 2;
        byte* numPtr4 = numPtr3;
        int num2 = 0;
        int num3 = (int) *(ushort*) numPtr3;
        byte* numPtr5 = numPtr3 + 2;
        while (num3 < 65536)
        {
          byte num4 = *numPtr5;
          ++numPtr5;
          if (num4 == (byte) 1)
          {
            num3 = (int) *(ushort*) numPtr5;
            numPtr5 += 2;
          }
          else if (num4 < (byte) 32 && num4 > (byte) 0 && num4 != (byte) 30)
          {
            num3 += (int) num4;
          }
          else
          {
            if (num4 > (byte) 0)
              ++num2;
            ++num3;
          }
        }
        char[] chArray2 = new char[num2 * 2];
        byte* numPtr6 = numPtr4;
        int num5 = (int) *(ushort*) numPtr6;
        byte* numPtr7 = numPtr6 + 2;
        int num6 = 0;
        while (num5 < 65536)
        {
          byte index1 = *numPtr7;
          ++numPtr7;
          if (index1 == (byte) 1)
          {
            num5 = (int) *(ushort*) numPtr7;
            numPtr7 += 2;
          }
          else if (index1 < (byte) 32 && index1 > (byte) 0 && index1 != (byte) 30)
          {
            num5 += (int) index1;
          }
          else
          {
            if (index1 == (byte) 30)
            {
              index1 = *numPtr7;
              ++numPtr7;
            }
            if (index1 > (byte) 0)
            {
              char[] chArray3 = chArray2;
              int index2 = num6;
              int num4 = index2 + 1;
              int num7 = (int) (ushort) num5;
              chArray3[index2] = (char) num7;
              char[] chArray4 = chArray2;
              int index3 = num4;
              num6 = index3 + 1;
              int num8 = (int) this.mapBytesToUnicode[index1];
              chArray4[index3] = (char) num8;
            }
            ++num5;
          }
        }
        this.arrayUnicodeBestFit = chArray2;
      }
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        if (ch1 > char.MinValue)
          ++count;
        return count;
      }
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      int num = 0;
      char* charEnd = chars + count;
      if (ch1 > char.MinValue)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch2;
      while ((ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch2 == char.MinValue)
        {
          ch2 = *chars;
          chars += 2;
        }
        if (this.mapUnicodeToBytes[(int) ch2] == (byte) 0 && ch2 != char.MinValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - count, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
          ++num;
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      char* chPtr = chars;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        byte num1 = this.mapUnicodeToBytes[(int) replacementFallback.DefaultString[0]];
        if (num1 != (byte) 0)
        {
          if (ch1 > char.MinValue)
          {
            if (byteCount == 0)
              this.ThrowBytesOverflow(encoder, true);
            *bytes++ = num1;
            --byteCount;
          }
          if (byteCount < charCount)
          {
            this.ThrowBytesOverflow(encoder, byteCount < 1);
            charEnd = chars + byteCount;
          }
          while (chars < charEnd)
          {
            char ch2 = *chars;
            chars += 2;
            byte num2 = this.mapUnicodeToBytes[(int) ch2];
            *bytes = num2 != (byte) 0 || ch2 == char.MinValue ? num2 : num1;
            ++bytes;
          }
          if (encoder != null)
          {
            encoder.charLeftOver = char.MinValue;
            encoder.m_charsUsed = (int) (chars - chPtr);
          }
          return (int) (bytes - numPtr1);
        }
      }
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      byte* numPtr2 = bytes + byteCount;
      if (ch1 > char.MinValue)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
        if ((long) encoderFallbackBuffer.Remaining > numPtr2 - bytes)
          this.ThrowBytesOverflow(encoder, true);
      }
      char ch3;
      while ((ch3 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch3 == char.MinValue)
        {
          ch3 = *chars;
          chars += 2;
        }
        byte num = this.mapUnicodeToBytes[(int) ch3];
        if (num == (byte) 0 && ch3 != char.MinValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch3, ref chars);
          if ((long) encoderFallbackBuffer.Remaining > numPtr2 - bytes)
          {
            chars -= 2;
            encoderFallbackBuffer.InternalReset();
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
        }
        else
        {
          if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
          *bytes = num;
          ++bytes;
        }
      }
      if (encoder != null)
      {
        if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = (int) (chars - chPtr);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      this.CheckMemorySection();
      DecoderReplacementFallback replacementFallback;
      bool microsoftBestFitFallback;
      if (decoder == null)
      {
        replacementFallback = this.DecoderFallback as DecoderReplacementFallback;
        microsoftBestFitFallback = this.DecoderFallback.IsMicrosoftBestFitFallback;
      }
      else
      {
        replacementFallback = decoder.Fallback as DecoderReplacementFallback;
        microsoftBestFitFallback = decoder.Fallback.IsMicrosoftBestFitFallback;
      }
      if (microsoftBestFitFallback || replacementFallback != null && replacementFallback.MaxCharCount == 1)
        return count;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      int num = count;
      byte[] bytes1 = new byte[1];
      byte* numPtr = bytes + count;
      while (bytes < numPtr)
      {
        char ch = this.mapBytesToUnicode[*bytes];
        ++bytes;
        if (ch == '�')
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          bytes1[0] = *(bytes - 1);
          num = num - 1 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      this.CheckMemorySection();
      byte* numPtr1 = bytes + byteCount;
      byte* numPtr2 = bytes;
      char* chPtr = chars;
      DecoderReplacementFallback replacementFallback;
      bool microsoftBestFitFallback;
      if (decoder == null)
      {
        replacementFallback = this.DecoderFallback as DecoderReplacementFallback;
        microsoftBestFitFallback = this.DecoderFallback.IsMicrosoftBestFitFallback;
      }
      else
      {
        replacementFallback = decoder.Fallback as DecoderReplacementFallback;
        microsoftBestFitFallback = decoder.Fallback.IsMicrosoftBestFitFallback;
      }
      if (microsoftBestFitFallback || replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch1 = replacementFallback != null ? replacementFallback.DefaultString[0] : '?';
        if (charCount < byteCount)
        {
          this.ThrowCharsOverflow(decoder, charCount < 1);
          numPtr1 = bytes + charCount;
        }
        while (bytes < numPtr1)
        {
          char ch2;
          if (microsoftBestFitFallback)
          {
            if (this.arrayBytesBestFit == null)
              this.ReadBestFitTable();
            ch2 = this.arrayBytesBestFit[(int) *bytes];
          }
          else
            ch2 = this.mapBytesToUnicode[*bytes];
          ++bytes;
          *chars = ch2 != '�' ? ch2 : ch1;
          chars += 2;
        }
        if (decoder != null)
          decoder.m_bytesUsed = (int) (bytes - numPtr2);
        return (int) (chars - chPtr);
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte[] bytes1 = new byte[1];
      char* charEnd = chars + charCount;
      while (bytes < numPtr1)
      {
        char ch = this.mapBytesToUnicode[*bytes];
        ++bytes;
        if (ch == '�')
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr1 - byteCount, charEnd);
          }
          bytes1[0] = *(bytes - 1);
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow(decoder, bytes == numPtr2);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            this.ThrowCharsOverflow(decoder, bytes == numPtr2);
            break;
          }
          *chars = ch;
          chars += 2;
        }
      }
      if (decoder != null)
        decoder.m_bytesUsed = (int) (bytes - numPtr2);
      return (int) (chars - chPtr);
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num *= (long) this.EncoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    public override bool IsSingleByte
    {
      get
      {
        return true;
      }
    }

    [ComVisible(false)]
    public override bool IsAlwaysNormalized(NormalizationForm form)
    {
      if (form == NormalizationForm.FormC)
      {
        switch (this.CodePage)
        {
          case 37:
          case 437:
          case 500:
          case 720:
          case 737:
          case 775:
          case 850:
          case 852:
          case 855:
          case 858:
          case 860:
          case 861:
          case 862:
          case 863:
          case 865:
          case 866:
          case 869:
          case 870:
          case 1026:
          case 1047:
          case 1140:
          case 1141:
          case 1142:
          case 1143:
          case 1144:
          case 1145:
          case 1146:
          case 1147:
          case 1148:
          case 1149:
          case 1250:
          case 1251:
          case 1252:
          case 1254:
          case 1256:
          case 10007:
          case 10017:
          case 10029:
          case 20273:
          case 20277:
          case 20278:
          case 20280:
          case 20284:
          case 20285:
          case 20297:
          case 20866:
          case 20871:
          case 20880:
          case 20924:
          case 21025:
          case 21866:
          case 28591:
          case 28592:
          case 28594:
          case 28595:
          case 28599:
          case 28603:
          case 28605:
            return true;
        }
      }
      return false;
    }
  }
}
