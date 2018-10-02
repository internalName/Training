// Decompiled with JetBrains decompiler
// Type: System.Text.ISO2022Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class ISO2022Encoding : DBCSCodePageEncoding
  {
    private static int[] tableBaseCodePages = new int[12]
    {
      932,
      932,
      932,
      0,
      0,
      949,
      936,
      0,
      0,
      0,
      0,
      0
    };
    private static ushort[] HalfToFullWidthKanaTable = new ushort[63]
    {
      (ushort) 41379,
      (ushort) 41430,
      (ushort) 41431,
      (ushort) 41378,
      (ushort) 41382,
      (ushort) 42482,
      (ushort) 42401,
      (ushort) 42403,
      (ushort) 42405,
      (ushort) 42407,
      (ushort) 42409,
      (ushort) 42467,
      (ushort) 42469,
      (ushort) 42471,
      (ushort) 42435,
      (ushort) 41404,
      (ushort) 42402,
      (ushort) 42404,
      (ushort) 42406,
      (ushort) 42408,
      (ushort) 42410,
      (ushort) 42411,
      (ushort) 42413,
      (ushort) 42415,
      (ushort) 42417,
      (ushort) 42419,
      (ushort) 42421,
      (ushort) 42423,
      (ushort) 42425,
      (ushort) 42427,
      (ushort) 42429,
      (ushort) 42431,
      (ushort) 42433,
      (ushort) 42436,
      (ushort) 42438,
      (ushort) 42440,
      (ushort) 42442,
      (ushort) 42443,
      (ushort) 42444,
      (ushort) 42445,
      (ushort) 42446,
      (ushort) 42447,
      (ushort) 42450,
      (ushort) 42453,
      (ushort) 42456,
      (ushort) 42459,
      (ushort) 42462,
      (ushort) 42463,
      (ushort) 42464,
      (ushort) 42465,
      (ushort) 42466,
      (ushort) 42468,
      (ushort) 42470,
      (ushort) 42472,
      (ushort) 42473,
      (ushort) 42474,
      (ushort) 42475,
      (ushort) 42476,
      (ushort) 42477,
      (ushort) 42479,
      (ushort) 42483,
      (ushort) 41387,
      (ushort) 41388
    };
    private const byte SHIFT_OUT = 14;
    private const byte SHIFT_IN = 15;
    private const byte ESCAPE = 27;
    private const byte LEADBYTE_HALFWIDTH = 16;

    [SecurityCritical]
    internal ISO2022Encoding(int codePage)
      : base(codePage, ISO2022Encoding.tableBaseCodePages[codePage % 10])
    {
      this.m_bUseMlangTypeForSerialization = true;
    }

    [SecurityCritical]
    internal ISO2022Encoding(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
    }

    [SecurityCritical]
    protected override unsafe string GetMemorySectionName()
    {
      int num = this.bFlagDataTable ? this.dataTableCodePage : this.CodePage;
      string format;
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
        case 50222:
          format = "CodePage_{0}_{1}_{2}_{3}_{4}_ISO2022JP";
          break;
        case 50225:
          format = "CodePage_{0}_{1}_{2}_{3}_{4}_ISO2022KR";
          break;
        case 52936:
          format = "CodePage_{0}_{1}_{2}_{3}_{4}_HZ";
          break;
        default:
          format = "CodePage_{0}_{1}_{2}_{3}_{4}";
          break;
      }
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) num, (object) this.pCodePage->VersionMajor, (object) this.pCodePage->VersionMinor, (object) this.pCodePage->VersionRevision, (object) this.pCodePage->VersionBuild);
    }

    protected override bool CleanUpBytes(ref int bytes)
    {
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
        case 50222:
          if (bytes >= 256)
          {
            if (bytes >= 64064 && bytes <= 64587)
            {
              if (bytes >= 64064 && bytes <= 64091)
              {
                if (bytes <= 64073)
                  bytes -= 2897;
                else if (bytes >= 64074 && bytes <= 64083)
                  bytes -= 29430;
                else if (bytes >= 64084 && bytes <= 64087)
                  bytes -= 2907;
                else if (bytes == 64088)
                  bytes = 34698;
                else if (bytes == 64089)
                  bytes = 34690;
                else if (bytes == 64090)
                  bytes = 34692;
                else if (bytes == 64091)
                  bytes = 34714;
              }
              else if (bytes >= 64092 && bytes <= 64587)
              {
                byte num = (byte) bytes;
                if (num < (byte) 92)
                  bytes -= 3423;
                else if (num >= (byte) 128 && num <= (byte) 155)
                  bytes -= 3357;
                else
                  bytes -= 3356;
              }
            }
            byte num1 = (byte) (bytes >> 8);
            byte num2 = (byte) bytes;
            byte num3 = (byte) (((int) (byte) ((int) num1 - (num1 > (byte) 159 ? 177 : 113)) << 1) + 1);
            byte num4;
            if (num2 > (byte) 158)
            {
              num4 = (byte) ((uint) num2 - 126U);
              ++num3;
            }
            else
            {
              if (num2 > (byte) 126)
                --num2;
              num4 = (byte) ((uint) num2 - 31U);
            }
            bytes = (int) num3 << 8 | (int) num4;
            break;
          }
          if (bytes >= 161 && bytes <= 223)
            bytes += 3968;
          if (bytes >= 129 && (bytes <= 159 || bytes >= 224 && bytes <= 252))
            return false;
          break;
        case 50225:
          if (bytes >= 128 && bytes <= (int) byte.MaxValue || bytes >= 256 && ((bytes & (int) byte.MaxValue) < 161 || (bytes & (int) byte.MaxValue) == (int) byte.MaxValue || ((bytes & 65280) < 41216 || (bytes & 65280) == 65280)))
            return false;
          bytes &= 32639;
          break;
        case 52936:
          if (bytes >= 129 && bytes <= 254)
            return false;
          break;
      }
      return true;
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
    {
      return this.GetBytes(chars, count, (byte*) null, 0, baseEncoder);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      ISO2022Encoding.ISO2022Encoder encoder = (ISO2022Encoding.ISO2022Encoder) baseEncoder;
      int num = 0;
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
        case 50222:
          num = this.GetBytesCP5022xJP(chars, charCount, bytes, byteCount, encoder);
          break;
        case 50225:
          num = this.GetBytesCP50225KR(chars, charCount, bytes, byteCount, encoder);
          break;
        case 52936:
          num = this.GetBytesCP52936(chars, charCount, bytes, byteCount, encoder);
          break;
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      return this.GetChars(bytes, count, (char*) null, 0, baseDecoder);
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      ISO2022Encoding.ISO2022Decoder decoder = (ISO2022Encoding.ISO2022Decoder) baseDecoder;
      int num = 0;
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
        case 50222:
          num = this.GetCharsCP5022xJP(bytes, byteCount, chars, charCount, decoder);
          break;
        case 50225:
          num = this.GetCharsCP50225KR(bytes, byteCount, chars, charCount, decoder);
          break;
        case 52936:
          num = this.GetCharsCP52936(bytes, byteCount, chars, charCount, decoder);
          break;
      }
      return num;
    }

    [SecurityCritical]
    private unsafe int GetBytesCP5022xJP(char* chars, int charCount, byte* bytes, int byteCount, ISO2022Encoding.ISO2022Encoder encoder)
    {
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) encoder, bytes, byteCount, chars, charCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      ISO2022Encoding.ISO2022Modes isO2022Modes2 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      if (encoder != null)
      {
        char charLeftOver = encoder.charLeftOver;
        isO2022Modes1 = encoder.currentMode;
        isO2022Modes2 = encoder.shiftInOutMode;
        if (charLeftOver > char.MinValue)
          encodingByteBuffer.Fallback(charLeftOver);
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        ushort num1 = this.mapUnicodeToBytes[nextChar];
        byte b1;
        byte num2;
        while (true)
        {
          b1 = (byte) ((uint) num1 >> 8);
          num2 = (byte) ((uint) num1 & (uint) byte.MaxValue);
          switch (b1)
          {
            case 0:
              goto label_23;
            case 16:
              if (this.CodePage == 50220)
              {
                if (num2 >= (byte) 33 && (int) num2 < 33 + ISO2022Encoding.HalfToFullWidthKanaTable.Length)
                {
                  num1 = (ushort) ((uint) ISO2022Encoding.HalfToFullWidthKanaTable[(int) num2 - 33] & 32639U);
                  continue;
                }
                goto label_7;
              }
              else
                goto label_9;
            default:
              goto label_16;
          }
        }
label_7:
        encodingByteBuffer.Fallback(nextChar);
        continue;
label_9:
        if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana)
        {
          if (this.CodePage == 50222)
          {
            if (encodingByteBuffer.AddByte((byte) 14))
            {
              isO2022Modes2 = isO2022Modes1;
              isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana;
            }
            else
              break;
          }
          else if (encodingByteBuffer.AddByte((byte) 27, (byte) 40, (byte) 73))
            isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana;
          else
            break;
        }
        if (encodingByteBuffer.AddByte((byte) ((uint) num2 & (uint) sbyte.MaxValue)))
          continue;
        break;
label_16:
        if (this.CodePage == 50222 && isO2022Modes1 == ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana)
        {
          if (encodingByteBuffer.AddByte((byte) 15))
            isO2022Modes1 = isO2022Modes2;
          else
            break;
        }
        if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeJIS0208)
        {
          if (encodingByteBuffer.AddByte((byte) 27, (byte) 36, (byte) 66))
            isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeJIS0208;
          else
            break;
        }
        if (encodingByteBuffer.AddByte(b1, num2))
          continue;
        break;
label_23:
        if (num1 != (ushort) 0 || nextChar == char.MinValue)
        {
          if (this.CodePage == 50222 && isO2022Modes1 == ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana)
          {
            if (encodingByteBuffer.AddByte((byte) 15))
              isO2022Modes1 = isO2022Modes2;
            else
              break;
          }
          if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeASCII)
          {
            if (encodingByteBuffer.AddByte((byte) 27, (byte) 40, (byte) 66))
              isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
            else
              break;
          }
          if (!encodingByteBuffer.AddByte(num2))
            break;
        }
        else
          encodingByteBuffer.Fallback(nextChar);
      }
      if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeASCII && (encoder == null || encoder.MustFlush))
      {
        if (this.CodePage == 50222 && isO2022Modes1 == ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana)
        {
          if (encodingByteBuffer.AddByte((byte) 15))
          {
            isO2022Modes1 = isO2022Modes2;
          }
          else
          {
            int nextChar1 = (int) encodingByteBuffer.GetNextChar();
          }
        }
        if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeASCII && (this.CodePage != 50222 || isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana))
        {
          if (encodingByteBuffer.AddByte((byte) 27, (byte) 40, (byte) 66))
          {
            isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
          }
          else
          {
            int nextChar2 = (int) encodingByteBuffer.GetNextChar();
          }
        }
      }
      if ((IntPtr) bytes != IntPtr.Zero && encoder != null)
      {
        encoder.currentMode = isO2022Modes1;
        encoder.shiftInOutMode = isO2022Modes2;
        if (!encodingByteBuffer.fallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    private unsafe int GetBytesCP50225KR(char* chars, int charCount, byte* bytes, int byteCount, ISO2022Encoding.ISO2022Encoder encoder)
    {
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) encoder, bytes, byteCount, chars, charCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      ISO2022Encoding.ISO2022Modes isO2022Modes2 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      if (encoder != null)
      {
        char charLeftOver = encoder.charLeftOver;
        isO2022Modes1 = encoder.currentMode;
        isO2022Modes2 = encoder.shiftInOutMode;
        if (charLeftOver > char.MinValue)
          encodingByteBuffer.Fallback(charLeftOver);
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        ushort num1 = this.mapUnicodeToBytes[nextChar];
        byte b1 = (byte) ((uint) num1 >> 8);
        byte num2 = (byte) ((uint) num1 & (uint) byte.MaxValue);
        if (b1 != (byte) 0)
        {
          if (isO2022Modes2 != ISO2022Encoding.ISO2022Modes.ModeKR)
          {
            if (encodingByteBuffer.AddByte((byte) 27, (byte) 36, (byte) 41, (byte) 67))
              isO2022Modes2 = ISO2022Encoding.ISO2022Modes.ModeKR;
            else
              break;
          }
          if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeKR)
          {
            if (encodingByteBuffer.AddByte((byte) 14))
              isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeKR;
            else
              break;
          }
          if (!encodingByteBuffer.AddByte(b1, num2))
            break;
        }
        else if (num1 != (ushort) 0 || nextChar == char.MinValue)
        {
          if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeASCII)
          {
            if (encodingByteBuffer.AddByte((byte) 15))
              isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
            else
              break;
          }
          if (!encodingByteBuffer.AddByte(num2))
            break;
        }
        else
          encodingByteBuffer.Fallback(nextChar);
      }
      if (isO2022Modes1 != ISO2022Encoding.ISO2022Modes.ModeASCII && (encoder == null || encoder.MustFlush))
      {
        if (encodingByteBuffer.AddByte((byte) 15))
        {
          isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
        }
        else
        {
          int nextChar1 = (int) encodingByteBuffer.GetNextChar();
        }
      }
      if ((IntPtr) bytes != IntPtr.Zero && encoder != null)
      {
        if (!encodingByteBuffer.fallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.currentMode = isO2022Modes1;
        encoder.shiftInOutMode = !encoder.MustFlush || encoder.charLeftOver != char.MinValue ? isO2022Modes2 : ISO2022Encoding.ISO2022Modes.ModeASCII;
        encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    private unsafe int GetBytesCP52936(char* chars, int charCount, byte* bytes, int byteCount, ISO2022Encoding.ISO2022Encoder encoder)
    {
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) encoder, bytes, byteCount, chars, charCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
      if (encoder != null)
      {
        char charLeftOver = encoder.charLeftOver;
        isO2022Modes = encoder.currentMode;
        if (charLeftOver > char.MinValue)
          encodingByteBuffer.Fallback(charLeftOver);
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        ushort num1 = this.mapUnicodeToBytes[nextChar];
        if (num1 == (ushort) 0 && nextChar != char.MinValue)
        {
          encodingByteBuffer.Fallback(nextChar);
        }
        else
        {
          byte num2 = (byte) ((uint) num1 >> 8);
          byte b1 = (byte) ((uint) num1 & (uint) byte.MaxValue);
          if (num2 != (byte) 0 && (num2 < (byte) 161 || num2 > (byte) 247 || (b1 < (byte) 161 || b1 > (byte) 254)) || num2 == (byte) 0 && b1 > (byte) 128 && b1 != byte.MaxValue)
            encodingByteBuffer.Fallback(nextChar);
          else if (num2 != (byte) 0)
          {
            if (isO2022Modes != ISO2022Encoding.ISO2022Modes.ModeHZ)
            {
              if (encodingByteBuffer.AddByte((byte) 126, (byte) 123, 2))
                isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeHZ;
              else
                break;
            }
            if (!encodingByteBuffer.AddByte((byte) ((uint) num2 & (uint) sbyte.MaxValue), (byte) ((uint) b1 & (uint) sbyte.MaxValue)))
              break;
          }
          else
          {
            if (isO2022Modes != ISO2022Encoding.ISO2022Modes.ModeASCII)
            {
              if (encodingByteBuffer.AddByte((byte) 126, (byte) 125, b1 == (byte) 126 ? 2 : 1))
                isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
              else
                break;
            }
            if (b1 == (byte) 126 && !encodingByteBuffer.AddByte((byte) 126, 1) || !encodingByteBuffer.AddByte(b1))
              break;
          }
        }
      }
      if (isO2022Modes != ISO2022Encoding.ISO2022Modes.ModeASCII && (encoder == null || encoder.MustFlush))
      {
        if (encodingByteBuffer.AddByte((byte) 126, (byte) 125))
        {
          isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
        }
        else
        {
          int nextChar1 = (int) encodingByteBuffer.GetNextChar();
        }
      }
      if (encoder != null && (IntPtr) bytes != IntPtr.Zero)
      {
        encoder.currentMode = isO2022Modes;
        if (!encodingByteBuffer.fallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    private unsafe int GetCharsCP5022xJP(byte* bytes, int byteCount, char* chars, int charCount, ISO2022Encoding.ISO2022Decoder decoder)
    {
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) decoder, chars, charCount, bytes, byteCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      ISO2022Encoding.ISO2022Modes isO2022Modes2 = ISO2022Encoding.ISO2022Modes.ModeASCII;
      byte[] bytes1 = new byte[4];
      int count = 0;
      if (decoder != null)
      {
        isO2022Modes1 = decoder.currentMode;
        isO2022Modes2 = decoder.shiftInOutMode;
        count = decoder.bytesLeftOverCount;
        for (int index = 0; index < count; ++index)
          bytes1[index] = decoder.bytesLeftOver[index];
      }
      while (encodingCharBuffer.MoreData || count > 0)
      {
        byte fallbackByte;
        if (count > 0)
        {
          if (bytes1[0] == (byte) 27)
          {
            if (!encodingCharBuffer.MoreData)
            {
              if (decoder != null && !decoder.MustFlush)
                break;
            }
            else
            {
              bytes1[count++] = encodingCharBuffer.GetNextByte();
              ISO2022Encoding.ISO2022Modes isO2022Modes3 = this.CheckEscapeSequenceJP(bytes1, count);
              switch (isO2022Modes3)
              {
                case ISO2022Encoding.ISO2022Modes.ModeInvalidEscape:
                  break;
                case ISO2022Encoding.ISO2022Modes.ModeIncompleteEscape:
                  continue;
                default:
                  count = 0;
                  isO2022Modes1 = isO2022Modes2 = isO2022Modes3;
                  continue;
              }
            }
          }
          fallbackByte = this.DecrementEscapeBytes(ref bytes1, ref count);
        }
        else
        {
          fallbackByte = encodingCharBuffer.GetNextByte();
          if (fallbackByte == (byte) 27)
          {
            if (count == 0)
            {
              bytes1[0] = fallbackByte;
              count = 1;
              continue;
            }
            encodingCharBuffer.AdjustBytes(-1);
          }
        }
        if (fallbackByte == (byte) 14)
        {
          isO2022Modes2 = isO2022Modes1;
          isO2022Modes1 = ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana;
        }
        else if (fallbackByte == (byte) 15)
        {
          isO2022Modes1 = isO2022Modes2;
        }
        else
        {
          ushort index = (ushort) fallbackByte;
          bool flag = false;
          if (isO2022Modes1 == ISO2022Encoding.ISO2022Modes.ModeJIS0208)
          {
            if (count > 0)
            {
              if (bytes1[0] != (byte) 27)
              {
                index = (ushort) ((uint) (ushort) ((uint) index << 8) | (uint) this.DecrementEscapeBytes(ref bytes1, ref count));
                flag = true;
              }
            }
            else if (encodingCharBuffer.MoreData)
            {
              index = (ushort) ((uint) (ushort) ((uint) index << 8) | (uint) encodingCharBuffer.GetNextByte());
              flag = true;
            }
            else
            {
              if (decoder == null || decoder.MustFlush)
              {
                encodingCharBuffer.Fallback(fallbackByte);
                break;
              }
              if ((IntPtr) chars != IntPtr.Zero)
              {
                bytes1[0] = fallbackByte;
                count = 1;
                break;
              }
              break;
            }
            if (flag && ((int) index & 65280) == 10752)
              index = (ushort) ((uint) (ushort) ((uint) index & (uint) byte.MaxValue) | 4096U);
          }
          else if (index >= (ushort) 161 && index <= (ushort) 223)
            index = (ushort) ((uint) (ushort) ((uint) index | 4096U) & 65407U);
          else if (isO2022Modes1 == ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana)
            index |= (ushort) 4096;
          char ch = this.mapBytesToUnicode[index];
          if (ch == char.MinValue && index != (ushort) 0)
          {
            if (flag)
            {
              if (!encodingCharBuffer.Fallback((byte) ((uint) index >> 8), (byte) index))
                break;
            }
            else if (!encodingCharBuffer.Fallback(fallbackByte))
              break;
          }
          else if (!encodingCharBuffer.AddChar(ch, flag ? 2 : 1))
            break;
        }
      }
      if ((IntPtr) chars != IntPtr.Zero && decoder != null)
      {
        if (!decoder.MustFlush || count != 0)
        {
          decoder.currentMode = isO2022Modes1;
          decoder.shiftInOutMode = isO2022Modes2;
          decoder.bytesLeftOverCount = count;
          decoder.bytesLeftOver = bytes1;
        }
        else
        {
          decoder.currentMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
          decoder.shiftInOutMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
          decoder.bytesLeftOverCount = 0;
        }
        decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    private ISO2022Encoding.ISO2022Modes CheckEscapeSequenceJP(byte[] bytes, int escapeCount)
    {
      if (bytes[0] != (byte) 27)
        return ISO2022Encoding.ISO2022Modes.ModeInvalidEscape;
      if (escapeCount < 3)
        return ISO2022Encoding.ISO2022Modes.ModeIncompleteEscape;
      if (bytes[1] == (byte) 40)
      {
        if (bytes[2] == (byte) 66 || bytes[2] == (byte) 72 || bytes[2] == (byte) 74)
          return ISO2022Encoding.ISO2022Modes.ModeASCII;
        if (bytes[2] == (byte) 73)
          return ISO2022Encoding.ISO2022Modes.ModeHalfwidthKatakana;
      }
      else if (bytes[1] == (byte) 36)
      {
        if (bytes[2] == (byte) 64 || bytes[2] == (byte) 66)
          return ISO2022Encoding.ISO2022Modes.ModeJIS0208;
        if (escapeCount < 4)
          return ISO2022Encoding.ISO2022Modes.ModeIncompleteEscape;
        if (bytes[2] == (byte) 40 && bytes[3] == (byte) 68)
          return ISO2022Encoding.ISO2022Modes.ModeJIS0208;
      }
      else if (bytes[1] == (byte) 38 && bytes[2] == (byte) 64)
        return ISO2022Encoding.ISO2022Modes.ModeNOOP;
      return ISO2022Encoding.ISO2022Modes.ModeInvalidEscape;
    }

    private byte DecrementEscapeBytes(ref byte[] bytes, ref int count)
    {
      --count;
      byte num = bytes[0];
      for (int index = 0; index < count; ++index)
        bytes[index] = bytes[index + 1];
      bytes[count] = (byte) 0;
      return num;
    }

    [SecurityCritical]
    private unsafe int GetCharsCP50225KR(byte* bytes, int byteCount, char* chars, int charCount, ISO2022Encoding.ISO2022Decoder decoder)
    {
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) decoder, chars, charCount, bytes, byteCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
      byte[] bytes1 = new byte[4];
      int count = 0;
      if (decoder != null)
      {
        isO2022Modes = decoder.currentMode;
        count = decoder.bytesLeftOverCount;
        for (int index = 0; index < count; ++index)
          bytes1[index] = decoder.bytesLeftOver[index];
      }
      while (encodingCharBuffer.MoreData || count > 0)
      {
        byte fallbackByte;
        if (count > 0)
        {
          if (bytes1[0] == (byte) 27)
          {
            if (!encodingCharBuffer.MoreData)
            {
              if (decoder != null && !decoder.MustFlush)
                break;
            }
            else
            {
              bytes1[count++] = encodingCharBuffer.GetNextByte();
              switch (this.CheckEscapeSequenceKR(bytes1, count))
              {
                case ISO2022Encoding.ISO2022Modes.ModeInvalidEscape:
                  break;
                case ISO2022Encoding.ISO2022Modes.ModeIncompleteEscape:
                  continue;
                default:
                  count = 0;
                  continue;
              }
            }
          }
          fallbackByte = this.DecrementEscapeBytes(ref bytes1, ref count);
        }
        else
        {
          fallbackByte = encodingCharBuffer.GetNextByte();
          if (fallbackByte == (byte) 27)
          {
            if (count == 0)
            {
              bytes1[0] = fallbackByte;
              count = 1;
              continue;
            }
            encodingCharBuffer.AdjustBytes(-1);
          }
        }
        if (fallbackByte == (byte) 14)
          isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeKR;
        else if (fallbackByte == (byte) 15)
        {
          isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
        }
        else
        {
          ushort index = (ushort) fallbackByte;
          bool flag = false;
          if (isO2022Modes == ISO2022Encoding.ISO2022Modes.ModeKR && fallbackByte != (byte) 32 && (fallbackByte != (byte) 9 && fallbackByte != (byte) 10))
          {
            if (count > 0)
            {
              if (bytes1[0] != (byte) 27)
              {
                index = (ushort) ((uint) (ushort) ((uint) index << 8) | (uint) this.DecrementEscapeBytes(ref bytes1, ref count));
                flag = true;
              }
            }
            else if (encodingCharBuffer.MoreData)
            {
              index = (ushort) ((uint) (ushort) ((uint) index << 8) | (uint) encodingCharBuffer.GetNextByte());
              flag = true;
            }
            else
            {
              if (decoder == null || decoder.MustFlush)
              {
                encodingCharBuffer.Fallback(fallbackByte);
                break;
              }
              if ((IntPtr) chars != IntPtr.Zero)
              {
                bytes1[0] = fallbackByte;
                count = 1;
                break;
              }
              break;
            }
          }
          char ch = this.mapBytesToUnicode[index];
          if (ch == char.MinValue && index != (ushort) 0)
          {
            if (flag)
            {
              if (!encodingCharBuffer.Fallback((byte) ((uint) index >> 8), (byte) index))
                break;
            }
            else if (!encodingCharBuffer.Fallback(fallbackByte))
              break;
          }
          else if (!encodingCharBuffer.AddChar(ch, flag ? 2 : 1))
            break;
        }
      }
      if ((IntPtr) chars != IntPtr.Zero && decoder != null)
      {
        if (!decoder.MustFlush || count != 0)
        {
          decoder.currentMode = isO2022Modes;
          decoder.bytesLeftOverCount = count;
          decoder.bytesLeftOver = bytes1;
        }
        else
        {
          decoder.currentMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
          decoder.shiftInOutMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
          decoder.bytesLeftOverCount = 0;
        }
        decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    private ISO2022Encoding.ISO2022Modes CheckEscapeSequenceKR(byte[] bytes, int escapeCount)
    {
      if (bytes[0] != (byte) 27)
        return ISO2022Encoding.ISO2022Modes.ModeInvalidEscape;
      if (escapeCount < 4)
        return ISO2022Encoding.ISO2022Modes.ModeIncompleteEscape;
      return bytes[1] == (byte) 36 && bytes[2] == (byte) 41 && bytes[3] == (byte) 67 ? ISO2022Encoding.ISO2022Modes.ModeKR : ISO2022Encoding.ISO2022Modes.ModeInvalidEscape;
    }

    [SecurityCritical]
    private unsafe int GetCharsCP52936(byte* bytes, int byteCount, char* chars, int charCount, ISO2022Encoding.ISO2022Decoder decoder)
    {
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) decoder, chars, charCount, bytes, byteCount);
      ISO2022Encoding.ISO2022Modes isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
      int num = -1;
      bool flag = false;
      if (decoder != null)
      {
        isO2022Modes = decoder.currentMode;
        if (decoder.bytesLeftOverCount != 0)
          num = (int) decoder.bytesLeftOver[0];
      }
      while (encodingCharBuffer.MoreData || num >= 0)
      {
        byte fallbackByte;
        if (num >= 0)
        {
          fallbackByte = (byte) num;
          num = -1;
        }
        else
          fallbackByte = encodingCharBuffer.GetNextByte();
        if (fallbackByte == (byte) 126)
        {
          if (!encodingCharBuffer.MoreData)
          {
            if (decoder == null || decoder.MustFlush)
            {
              encodingCharBuffer.Fallback(fallbackByte);
              break;
            }
            decoder?.ClearMustFlush();
            if ((IntPtr) chars != IntPtr.Zero)
            {
              decoder.bytesLeftOverCount = 1;
              decoder.bytesLeftOver[0] = (byte) 126;
              flag = true;
              break;
            }
            break;
          }
          byte nextByte = encodingCharBuffer.GetNextByte();
          if (nextByte == (byte) 126 && isO2022Modes == ISO2022Encoding.ISO2022Modes.ModeASCII)
          {
            if (encodingCharBuffer.AddChar((char) nextByte, 2))
              continue;
            break;
          }
          switch (nextByte)
          {
            case 10:
              continue;
            case 123:
              isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeHZ;
              continue;
            case 125:
              isO2022Modes = ISO2022Encoding.ISO2022Modes.ModeASCII;
              continue;
            default:
              encodingCharBuffer.AdjustBytes(-1);
              fallbackByte = (byte) 126;
              break;
          }
        }
        if (isO2022Modes != ISO2022Encoding.ISO2022Modes.ModeASCII && fallbackByte >= (byte) 32)
        {
          if (!encodingCharBuffer.MoreData)
          {
            if (decoder == null || decoder.MustFlush)
            {
              encodingCharBuffer.Fallback(fallbackByte);
              break;
            }
            decoder?.ClearMustFlush();
            if ((IntPtr) chars != IntPtr.Zero)
            {
              decoder.bytesLeftOverCount = 1;
              decoder.bytesLeftOver[0] = fallbackByte;
              flag = true;
              break;
            }
            break;
          }
          byte nextByte = encodingCharBuffer.GetNextByte();
          ushort index = (ushort) ((uint) fallbackByte << 8 | (uint) nextByte);
          char ch;
          if (fallbackByte == (byte) 32 && nextByte != (byte) 0)
          {
            ch = (char) nextByte;
          }
          else
          {
            if ((fallbackByte < (byte) 33 || fallbackByte > (byte) 119 || (nextByte < (byte) 33 || nextByte > (byte) 126)) && (fallbackByte < (byte) 161 || fallbackByte > (byte) 247 || (nextByte < (byte) 161 || nextByte > (byte) 254)))
            {
              if (nextByte == (byte) 32 && (byte) 33 <= fallbackByte && fallbackByte <= (byte) 125)
              {
                index = (ushort) 8481;
              }
              else
              {
                if (encodingCharBuffer.Fallback((byte) ((uint) index >> 8), (byte) index))
                  continue;
                break;
              }
            }
            index |= (ushort) 32896;
            ch = this.mapBytesToUnicode[index];
          }
          if (ch == char.MinValue && index != (ushort) 0)
          {
            if (!encodingCharBuffer.Fallback((byte) ((uint) index >> 8), (byte) index))
              break;
          }
          else if (!encodingCharBuffer.AddChar(ch, 2))
            break;
        }
        else
        {
          char ch = this.mapBytesToUnicode[fallbackByte];
          if ((ch == char.MinValue || ch == char.MinValue) && fallbackByte != (byte) 0)
          {
            if (!encodingCharBuffer.Fallback(fallbackByte))
              break;
          }
          else if (!encodingCharBuffer.AddChar(ch))
            break;
        }
      }
      if ((IntPtr) chars != IntPtr.Zero && decoder != null)
      {
        if (!flag)
          decoder.bytesLeftOverCount = 0;
        decoder.currentMode = !decoder.MustFlush || decoder.bytesLeftOverCount != 0 ? isO2022Modes : ISO2022Encoding.ISO2022Modes.ModeASCII;
        decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      int num2 = 2;
      int num3 = 0;
      int num4 = 0;
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
          num2 = 5;
          num4 = 3;
          break;
        case 50222:
          num2 = 5;
          num4 = 4;
          break;
        case 50225:
          num2 = 3;
          num3 = 4;
          num4 = 1;
          break;
        case 52936:
          num2 = 4;
          num4 = 2;
          break;
      }
      long num5 = num1 * (long) num2 + (long) (num3 + num4);
      if (num5 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num5;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int num1 = 1;
      int num2 = 1;
      switch (this.CodePage)
      {
        case 50220:
        case 50221:
        case 50222:
        case 50225:
          num1 = 1;
          num2 = 3;
          break;
        case 52936:
          num1 = 1;
          num2 = 1;
          break;
      }
      long num3 = (long) byteCount * (long) num1 + (long) num2;
      if (this.DecoderFallback.MaxCharCount > 1)
        num3 *= (long) this.DecoderFallback.MaxCharCount;
      if (num3 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num3;
    }

    public override Encoder GetEncoder()
    {
      return (Encoder) new ISO2022Encoding.ISO2022Encoder((EncodingNLS) this);
    }

    public override Decoder GetDecoder()
    {
      return (Decoder) new ISO2022Encoding.ISO2022Decoder((EncodingNLS) this);
    }

    internal enum ISO2022Modes
    {
      ModeNOOP = -3,
      ModeInvalidEscape = -2,
      ModeIncompleteEscape = -1,
      ModeHalfwidthKatakana = 0,
      ModeJIS0208 = 1,
      ModeKR = 5,
      ModeHZ = 6,
      ModeGB2312 = 7,
      ModeCNS11643_1 = 9,
      ModeCNS11643_2 = 10, // 0x0000000A
      ModeASCII = 11, // 0x0000000B
    }

    [Serializable]
    internal class ISO2022Encoder : EncoderNLS
    {
      internal ISO2022Encoding.ISO2022Modes currentMode;
      internal ISO2022Encoding.ISO2022Modes shiftInOutMode;

      internal ISO2022Encoder(EncodingNLS encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.currentMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
        this.shiftInOutMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
        this.charLeftOver = char.MinValue;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          if (this.charLeftOver == char.MinValue)
            return this.currentMode != ISO2022Encoding.ISO2022Modes.ModeASCII;
          return true;
        }
      }
    }

    [Serializable]
    internal class ISO2022Decoder : DecoderNLS
    {
      internal byte[] bytesLeftOver;
      internal int bytesLeftOverCount;
      internal ISO2022Encoding.ISO2022Modes currentMode;
      internal ISO2022Encoding.ISO2022Modes shiftInOutMode;

      internal ISO2022Decoder(EncodingNLS encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.bytesLeftOverCount = 0;
        this.bytesLeftOver = new byte[4];
        this.currentMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
        this.shiftInOutMode = ISO2022Encoding.ISO2022Modes.ModeASCII;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          if (this.bytesLeftOverCount == 0)
            return this.currentMode != ISO2022Encoding.ISO2022Modes.ModeASCII;
          return true;
        }
      }
    }
  }
}
