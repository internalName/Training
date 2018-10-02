// Decompiled with JetBrains decompiler
// Type: System.Text.Latin1Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class Latin1Encoding : EncodingNLS, ISerializable
  {
    private static readonly char[] arrayCharBestFit = new char[604]
    {
      'Ā',
      'A',
      'ā',
      'a',
      'Ă',
      'A',
      'ă',
      'a',
      'Ą',
      'A',
      'ą',
      'a',
      'Ć',
      'C',
      'ć',
      'c',
      'Ĉ',
      'C',
      'ĉ',
      'c',
      'Ċ',
      'C',
      'ċ',
      'c',
      'Č',
      'C',
      'č',
      'c',
      'Ď',
      'D',
      'ď',
      'd',
      'Đ',
      'D',
      'đ',
      'd',
      'Ē',
      'E',
      'ē',
      'e',
      'Ĕ',
      'E',
      'ĕ',
      'e',
      'Ė',
      'E',
      'ė',
      'e',
      'Ę',
      'E',
      'ę',
      'e',
      'Ě',
      'E',
      'ě',
      'e',
      'Ĝ',
      'G',
      'ĝ',
      'g',
      'Ğ',
      'G',
      'ğ',
      'g',
      'Ġ',
      'G',
      'ġ',
      'g',
      'Ģ',
      'G',
      'ģ',
      'g',
      'Ĥ',
      'H',
      'ĥ',
      'h',
      'Ħ',
      'H',
      'ħ',
      'h',
      'Ĩ',
      'I',
      'ĩ',
      'i',
      'Ī',
      'I',
      'ī',
      'i',
      'Ĭ',
      'I',
      'ĭ',
      'i',
      'Į',
      'I',
      'į',
      'i',
      'İ',
      'I',
      'ı',
      'i',
      'Ĵ',
      'J',
      'ĵ',
      'j',
      'Ķ',
      'K',
      'ķ',
      'k',
      'Ĺ',
      'L',
      'ĺ',
      'l',
      'Ļ',
      'L',
      'ļ',
      'l',
      'Ľ',
      'L',
      'ľ',
      'l',
      'Ł',
      'L',
      'ł',
      'l',
      'Ń',
      'N',
      'ń',
      'n',
      'Ņ',
      'N',
      'ņ',
      'n',
      'Ň',
      'N',
      'ň',
      'n',
      'Ō',
      'O',
      'ō',
      'o',
      'Ŏ',
      'O',
      'ŏ',
      'o',
      'Ő',
      'O',
      'ő',
      'o',
      'Œ',
      'O',
      'œ',
      'o',
      'Ŕ',
      'R',
      'ŕ',
      'r',
      'Ŗ',
      'R',
      'ŗ',
      'r',
      'Ř',
      'R',
      'ř',
      'r',
      'Ś',
      'S',
      'ś',
      's',
      'Ŝ',
      'S',
      'ŝ',
      's',
      'Ş',
      'S',
      'ş',
      's',
      'Š',
      'S',
      'š',
      's',
      'Ţ',
      'T',
      'ţ',
      't',
      'Ť',
      'T',
      'ť',
      't',
      'Ŧ',
      'T',
      'ŧ',
      't',
      'Ũ',
      'U',
      'ũ',
      'u',
      'Ū',
      'U',
      'ū',
      'u',
      'Ŭ',
      'U',
      'ŭ',
      'u',
      'Ů',
      'U',
      'ů',
      'u',
      'Ű',
      'U',
      'ű',
      'u',
      'Ų',
      'U',
      'ų',
      'u',
      'Ŵ',
      'W',
      'ŵ',
      'w',
      'Ŷ',
      'Y',
      'ŷ',
      'y',
      'Ÿ',
      'Y',
      'Ź',
      'Z',
      'ź',
      'z',
      'Ż',
      'Z',
      'ż',
      'z',
      'Ž',
      'Z',
      'ž',
      'z',
      'ƀ',
      'b',
      'Ɖ',
      'D',
      'Ƒ',
      'F',
      'ƒ',
      'f',
      'Ɨ',
      'I',
      'ƚ',
      'l',
      'Ɵ',
      'O',
      'Ơ',
      'O',
      'ơ',
      'o',
      'ƫ',
      't',
      'Ʈ',
      'T',
      'Ư',
      'U',
      'ư',
      'u',
      'ƶ',
      'z',
      'Ǎ',
      'A',
      'ǎ',
      'a',
      'Ǐ',
      'I',
      'ǐ',
      'i',
      'Ǒ',
      'O',
      'ǒ',
      'o',
      'Ǔ',
      'U',
      'ǔ',
      'u',
      'Ǖ',
      'U',
      'ǖ',
      'u',
      'Ǘ',
      'U',
      'ǘ',
      'u',
      'Ǚ',
      'U',
      'ǚ',
      'u',
      'Ǜ',
      'U',
      'ǜ',
      'u',
      'Ǟ',
      'A',
      'ǟ',
      'a',
      'Ǥ',
      'G',
      'ǥ',
      'g',
      'Ǧ',
      'G',
      'ǧ',
      'g',
      'Ǩ',
      'K',
      'ǩ',
      'k',
      'Ǫ',
      'O',
      'ǫ',
      'o',
      'Ǭ',
      'O',
      'ǭ',
      'o',
      'ǰ',
      'j',
      'ɡ',
      'g',
      'ʹ',
      '\'',
      'ʺ',
      '"',
      'ʼ',
      '\'',
      '˄',
      '^',
      'ˆ',
      '^',
      'ˈ',
      '\'',
      'ˉ',
      '?',
      'ˊ',
      '?',
      'ˋ',
      '`',
      'ˍ',
      '_',
      '˚',
      '?',
      '˜',
      '~',
      '̀',
      '`',
      '̂',
      '^',
      '̃',
      '~',
      '̎',
      '"',
      '̱',
      '_',
      '̲',
      '_',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      ' ',
      '‐',
      '-',
      '‑',
      '-',
      '–',
      '-',
      '—',
      '-',
      '‘',
      '\'',
      '’',
      '\'',
      '‚',
      ',',
      '“',
      '"',
      '”',
      '"',
      '„',
      '"',
      '†',
      '?',
      '‡',
      '?',
      '•',
      '.',
      '…',
      '.',
      '‰',
      '?',
      '′',
      '\'',
      '‵',
      '`',
      '‹',
      '<',
      '›',
      '>',
      '™',
      'T',
      '！',
      '!',
      '＂',
      '"',
      '＃',
      '#',
      '＄',
      '$',
      '％',
      '%',
      '＆',
      '&',
      '＇',
      '\'',
      '（',
      '(',
      '）',
      ')',
      '＊',
      '*',
      '＋',
      '+',
      '，',
      ',',
      '－',
      '-',
      '．',
      '.',
      '／',
      '/',
      '０',
      '0',
      '１',
      '1',
      '２',
      '2',
      '３',
      '3',
      '４',
      '4',
      '５',
      '5',
      '６',
      '6',
      '７',
      '7',
      '８',
      '8',
      '９',
      '9',
      '：',
      ':',
      '；',
      ';',
      '＜',
      '<',
      '＝',
      '=',
      '＞',
      '>',
      '？',
      '?',
      '＠',
      '@',
      'Ａ',
      'A',
      'Ｂ',
      'B',
      'Ｃ',
      'C',
      'Ｄ',
      'D',
      'Ｅ',
      'E',
      'Ｆ',
      'F',
      'Ｇ',
      'G',
      'Ｈ',
      'H',
      'Ｉ',
      'I',
      'Ｊ',
      'J',
      'Ｋ',
      'K',
      'Ｌ',
      'L',
      'Ｍ',
      'M',
      'Ｎ',
      'N',
      'Ｏ',
      'O',
      'Ｐ',
      'P',
      'Ｑ',
      'Q',
      'Ｒ',
      'R',
      'Ｓ',
      'S',
      'Ｔ',
      'T',
      'Ｕ',
      'U',
      'Ｖ',
      'V',
      'Ｗ',
      'W',
      'Ｘ',
      'X',
      'Ｙ',
      'Y',
      'Ｚ',
      'Z',
      '［',
      '[',
      '＼',
      '\\',
      '］',
      ']',
      '＾',
      '^',
      '＿',
      '_',
      '｀',
      '`',
      'ａ',
      'a',
      'ｂ',
      'b',
      'ｃ',
      'c',
      'ｄ',
      'd',
      'ｅ',
      'e',
      'ｆ',
      'f',
      'ｇ',
      'g',
      'ｈ',
      'h',
      'ｉ',
      'i',
      'ｊ',
      'j',
      'ｋ',
      'k',
      'ｌ',
      'l',
      'ｍ',
      'm',
      'ｎ',
      'n',
      'ｏ',
      'o',
      'ｐ',
      'p',
      'ｑ',
      'q',
      'ｒ',
      'r',
      'ｓ',
      's',
      'ｔ',
      't',
      'ｕ',
      'u',
      'ｖ',
      'v',
      'ｗ',
      'w',
      'ｘ',
      'x',
      'ｙ',
      'y',
      'ｚ',
      'z',
      '｛',
      '{',
      '｜',
      '|',
      '｝',
      '}',
      '～',
      '~'
    };

    public Latin1Encoding()
      : base(28591)
    {
    }

    internal Latin1Encoding(SerializationInfo info, StreamingContext context)
      : base(28591)
    {
      this.DeserializeEncoding(info, context);
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.SerializeEncoding(info, context);
      info.AddValue("CodePageEncoding+maxCharSize", 1);
      info.AddValue("CodePageEncoding+m_codePage", this.CodePage);
      info.AddValue("CodePageEncoding+dataItem", (object) null);
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int charCount, EncoderNLS encoder)
    {
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
          ++charCount;
        return charCount;
      }
      int num = 0;
      char* charEnd = chars + charCount;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
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
        if (ch2 > 'ÿ')
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, false);
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
        char ch2 = replacementFallback.DefaultString[0];
        if (ch2 <= 'ÿ')
        {
          if (ch1 > char.MinValue)
          {
            if (byteCount == 0)
              this.ThrowBytesOverflow(encoder, true);
            *bytes++ = (byte) ch2;
            --byteCount;
          }
          if (byteCount < charCount)
          {
            this.ThrowBytesOverflow(encoder, byteCount < 1);
            charEnd = chars + byteCount;
          }
          while (chars < charEnd)
          {
            char ch3 = *chars++;
            *bytes++ = ch3 <= 'ÿ' ? (byte) ch3 : (byte) ch2;
          }
          if (encoder != null)
          {
            encoder.charLeftOver = char.MinValue;
            encoder.m_charsUsed = (int) (chars - chPtr);
          }
          return (int) (bytes - numPtr1);
        }
      }
      byte* numPtr2 = bytes + byteCount;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      if (ch1 > char.MinValue)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
        if ((long) encoderFallbackBuffer.Remaining > numPtr2 - bytes)
          this.ThrowBytesOverflow(encoder, true);
      }
      char ch4;
      while ((ch4 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch4 == char.MinValue)
        {
          ch4 = *chars;
          chars += 2;
        }
        if (ch4 > 'ÿ')
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch4, ref chars);
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
          *bytes = (byte) ch4;
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
      return count;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      if (charCount < byteCount)
      {
        this.ThrowCharsOverflow(decoder, charCount < 1);
        byteCount = charCount;
      }
      for (byte* numPtr = bytes + byteCount; bytes < numPtr; ++bytes)
      {
        *chars = (char) *bytes;
        chars += 2;
      }
      if (decoder != null)
        decoder.m_bytesUsed = byteCount;
      return byteCount;
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

    public override bool IsAlwaysNormalized(NormalizationForm form)
    {
      return form == NormalizationForm.FormC;
    }

    internal override char[] GetBestFitUnicodeToBytesData()
    {
      return Latin1Encoding.arrayCharBestFit;
    }
  }
}
