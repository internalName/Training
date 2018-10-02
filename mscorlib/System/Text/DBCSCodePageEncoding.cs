// Decompiled with JetBrains decompiler
// Type: System.Text.DBCSCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  [Serializable]
  internal class DBCSCodePageEncoding : BaseCodePageEncoding, ISerializable
  {
    [SecurityCritical]
    [NonSerialized]
    protected unsafe char* mapBytesToUnicode = (char*) null;
    [SecurityCritical]
    [NonSerialized]
    protected unsafe ushort* mapUnicodeToBytes = (ushort*) null;
    [SecurityCritical]
    [NonSerialized]
    protected unsafe int* mapCodePageCached = (int*) null;
    [NonSerialized]
    protected const char UNKNOWN_CHAR_FLAG = '\0';
    [NonSerialized]
    protected const char UNICODE_REPLACEMENT_CHAR = '�';
    [NonSerialized]
    protected const char LEAD_BYTE_CHAR = '\xFFFE';
    [NonSerialized]
    private ushort bytesUnknown;
    [NonSerialized]
    private int byteCountUnknown;
    [NonSerialized]
    protected char charUnknown;
    private static object s_InternalSyncObject;

    [SecurityCritical]
    public DBCSCodePageEncoding(int codePage)
      : this(codePage, codePage)
    {
    }

    [SecurityCritical]
    internal unsafe DBCSCodePageEncoding(int codePage, int dataCodePage)
      : base(codePage, dataCodePage)
    {
    }

    [SecurityCritical]
    internal unsafe DBCSCodePageEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentNullException("this");
    }

    [SecurityCritical]
    protected override unsafe void LoadManagedCodePage()
    {
      if (this.pCodePage->ByteCount != (short) 2)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.CodePage));
      this.bytesUnknown = this.pCodePage->ByteReplace;
      this.charUnknown = this.pCodePage->UnicodeReplace;
      if (this.DecoderFallback.IsMicrosoftBestFitFallback)
        ((InternalDecoderBestFitFallback) this.DecoderFallback).cReplacement = this.charUnknown;
      this.byteCountUnknown = 1;
      if (this.bytesUnknown > (ushort) byte.MaxValue)
        ++this.byteCountUnknown;
      byte* sharedMemory = this.GetSharedMemory(262148 + this.iExtraBytes);
      this.mapBytesToUnicode = (char*) sharedMemory;
      this.mapUnicodeToBytes = (ushort*) (sharedMemory + 131072);
      this.mapCodePageCached = (int*) (sharedMemory + 262144 + this.iExtraBytes);
      if (*this.mapCodePageCached != 0)
      {
        if (*this.mapCodePageCached != this.dataTableCodePage && this.bFlagDataTable || *this.mapCodePageCached != this.CodePage && !this.bFlagDataTable)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else
      {
        char* chPtr = (char*) &this.pCodePage->FirstDataWord;
        int num = 0;
        while (num < 65536)
        {
          char index = *chPtr;
          chPtr += 2;
          if (index == '\x0001')
          {
            num = (int) *chPtr;
            chPtr += 2;
          }
          else if (index < ' ' && index > char.MinValue)
          {
            num += (int) index;
          }
          else
          {
            int bytes;
            switch (index)
            {
              case '�':
                ++num;
                continue;
              case '\xFFFE':
                bytes = num;
                break;
              case char.MaxValue:
                bytes = num;
                index = (char) num;
                break;
              default:
                bytes = num;
                break;
            }
            if (this.CleanUpBytes(ref bytes))
            {
              if (index != '\xFFFE')
                this.mapUnicodeToBytes[index] = (ushort) bytes;
              this.mapBytesToUnicode[bytes] = index;
            }
            ++num;
          }
        }
        this.CleanUpEndBytes(this.mapBytesToUnicode);
        if (!this.bFlagDataTable)
          return;
        *this.mapCodePageCached = this.dataTableCodePage;
      }
    }

    protected virtual bool CleanUpBytes(ref int bytes)
    {
      return true;
    }

    [SecurityCritical]
    protected virtual unsafe void CleanUpEndBytes(char* chars)
    {
    }

    private static object InternalSyncObject
    {
      get
      {
        if (DBCSCodePageEncoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref DBCSCodePageEncoding.s_InternalSyncObject, obj, (object) null);
        }
        return DBCSCodePageEncoding.s_InternalSyncObject;
      }
    }

    [SecurityCritical]
    protected override unsafe void ReadBestFitTable()
    {
      lock (DBCSCodePageEncoding.InternalSyncObject)
      {
        if (this.arrayUnicodeBestFit != null)
          return;
        char* chPtr1 = (char*) &this.pCodePage->FirstDataWord;
        int num1 = 0;
        while (num1 < 65536)
        {
          char ch = *chPtr1;
          chPtr1 += 2;
          if (ch == '\x0001')
          {
            num1 = (int) *chPtr1;
            chPtr1 += 2;
          }
          else if (ch < ' ' && ch > char.MinValue)
            num1 += (int) ch;
          else
            ++num1;
        }
        char* chPtr2 = chPtr1;
        int num2 = 0;
        int num3 = (int) *chPtr1;
        char* chPtr3 = (char*) ((IntPtr) chPtr1 + 2);
        while (num3 < 65536)
        {
          char ch = *chPtr3;
          chPtr3 += 2;
          if (ch == '\x0001')
          {
            num3 = (int) *chPtr3;
            chPtr3 += 2;
          }
          else if (ch < ' ' && ch > char.MinValue)
          {
            num3 += (int) ch;
          }
          else
          {
            if (ch != '�')
            {
              int bytes = num3;
              if (this.CleanUpBytes(ref bytes) && (int) this.mapBytesToUnicode[bytes] != (int) ch)
                ++num2;
            }
            ++num3;
          }
        }
        char[] chArray1 = new char[num2 * 2];
        int num4 = 0;
        char* chPtr4 = chPtr2;
        int num5 = (int) *chPtr4;
        char* chPtr5 = (char*) ((IntPtr) chPtr4 + 2);
        bool flag = false;
        while (num5 < 65536)
        {
          char ch = *chPtr5;
          chPtr5 += 2;
          if (ch == '\x0001')
          {
            num5 = (int) *chPtr5;
            chPtr5 += 2;
          }
          else if (ch < ' ' && ch > char.MinValue)
          {
            num5 += (int) ch;
          }
          else
          {
            if (ch != '�')
            {
              int bytes = num5;
              if (this.CleanUpBytes(ref bytes) && (int) this.mapBytesToUnicode[bytes] != (int) ch)
              {
                if (bytes != num5)
                  flag = true;
                char[] chArray2 = chArray1;
                int index1 = num4;
                int num6 = index1 + 1;
                int num7 = (int) (ushort) bytes;
                chArray2[index1] = (char) num7;
                char[] chArray3 = chArray1;
                int index2 = num6;
                num4 = index2 + 1;
                int num8 = (int) ch;
                chArray3[index2] = (char) num8;
              }
            }
            ++num5;
          }
        }
        if (flag)
        {
          int index1 = 0;
          while (index1 < chArray1.Length - 2)
          {
            int index2 = index1;
            char ch1 = chArray1[index1];
            int index3 = index1 + 2;
            while (index3 < chArray1.Length)
            {
              if ((int) ch1 > (int) chArray1[index3])
              {
                ch1 = chArray1[index3];
                index2 = index3;
              }
              index3 += 2;
            }
            if (index2 != index1)
            {
              char ch2 = chArray1[index2];
              chArray1[index2] = chArray1[index1];
              chArray1[index1] = ch2;
              char ch3 = chArray1[index2 + 1];
              chArray1[index2 + 1] = chArray1[index1 + 1];
              chArray1[index1 + 1] = ch3;
            }
            index1 += 2;
          }
        }
        this.arrayBytesBestFit = chArray1;
        char* chPtr6 = chPtr5;
        char* chPtr7 = chPtr5;
        int num9 = 2;
        char* chPtr8 = (char*) ((IntPtr) chPtr7 + num9);
        int num10 = (int) *chPtr7;
        int num11 = 0;
        while (num10 < 65536)
        {
          char ch = *chPtr8;
          chPtr8 += 2;
          if (ch == '\x0001')
          {
            num10 = (int) *chPtr8;
            chPtr8 += 2;
          }
          else if (ch < ' ' && ch > char.MinValue)
          {
            num10 += (int) ch;
          }
          else
          {
            if (ch > char.MinValue)
              ++num11;
            ++num10;
          }
        }
        char[] chArray4 = new char[num11 * 2];
        char* chPtr9 = chPtr6;
        int num12 = 2;
        char* chPtr10 = (char*) ((IntPtr) chPtr9 + num12);
        int num13 = (int) *chPtr9;
        int num14 = 0;
        while (num13 < 65536)
        {
          char ch = *chPtr10;
          chPtr10 += 2;
          if (ch == '\x0001')
          {
            num13 = (int) *chPtr10;
            chPtr10 += 2;
          }
          else if (ch < ' ' && ch > char.MinValue)
          {
            num13 += (int) ch;
          }
          else
          {
            if (ch > char.MinValue)
            {
              int bytes = (int) ch;
              if (this.CleanUpBytes(ref bytes))
              {
                char[] chArray2 = chArray4;
                int index1 = num14;
                int num6 = index1 + 1;
                int num7 = (int) (ushort) num13;
                chArray2[index1] = (char) num7;
                char[] chArray3 = chArray4;
                int index2 = num6;
                num14 = index2 + 1;
                int num8 = (int) this.mapBytesToUnicode[bytes];
                chArray3[index2] = (char) num8;
              }
            }
            ++num13;
          }
        }
        this.arrayUnicodeBestFit = chArray4;
      }
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if (encoder.InternalHasFallbackBuffer && encoder.FallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      int num1 = 0;
      char* charEnd = chars + count;
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
        ushort num2 = this.mapUnicodeToBytes[ch2];
        if (num2 == (ushort) 0 && ch2 != char.MinValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - count, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
        {
          ++num1;
          if (num2 >= (ushort) 256)
            ++num1;
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* charEnd = chars + charCount;
      char* chPtr = chars;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      if (encoder != null)
      {
        char charLeftOver = encoder.charLeftOver;
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        if (encoder.m_throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
        if (charLeftOver > char.MinValue)
          encoderFallbackBuffer.InternalFallback(charLeftOver, ref chars);
      }
      char ch;
      while ((ch = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch == char.MinValue)
        {
          ch = *chars;
          chars += 2;
        }
        ushort num = this.mapUnicodeToBytes[ch];
        if (num == (ushort) 0 && ch != char.MinValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch, ref chars);
        }
        else
        {
          if (num >= (ushort) 256)
          {
            if (bytes + 1 >= numPtr2)
            {
              if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
                chars -= 2;
              else
                encoderFallbackBuffer.MovePrevious();
              this.ThrowBytesOverflow(encoder, chars == chPtr);
              break;
            }
            *bytes = (byte) ((uint) num >> 8);
            ++bytes;
          }
          else if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            else
              encoderFallbackBuffer.MovePrevious();
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
          *bytes = (byte) ((uint) num & (uint) byte.MaxValue);
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
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      this.CheckMemorySection();
      DBCSCodePageEncoding.DBCSDecoder dbcsDecoder = (DBCSCodePageEncoding.DBCSDecoder) baseDecoder;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte* numPtr = bytes + count;
      int num1 = count;
      if (dbcsDecoder != null && dbcsDecoder.bLeftOver > (byte) 0)
      {
        if (count == 0)
        {
          if (!dbcsDecoder.MustFlush)
            return 0;
          DecoderFallbackBuffer fallbackBuffer = dbcsDecoder.FallbackBuffer;
          fallbackBuffer.InternalInitialize(bytes, (char*) null);
          byte[] bytes1 = new byte[1]
          {
            dbcsDecoder.bLeftOver
          };
          return fallbackBuffer.InternalFallback(bytes1, bytes);
        }
        int index = (int) dbcsDecoder.bLeftOver << 8 | (int) *bytes;
        ++bytes;
        if (this.mapBytesToUnicode[index] == char.MinValue && index != 0)
        {
          int num2 = num1 - 1;
          decoderFallbackBuffer = dbcsDecoder.FallbackBuffer;
          decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          byte[] bytes1 = new byte[2]
          {
            (byte) (index >> 8),
            (byte) index
          };
          num1 = num2 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      while (bytes < numPtr)
      {
        int index = (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if (ch == '\xFFFE')
        {
          --num1;
          if (bytes < numPtr)
          {
            index = index << 8 | (int) *bytes;
            ++bytes;
            ch = this.mapBytesToUnicode[index];
          }
          else if (dbcsDecoder == null || dbcsDecoder.MustFlush)
          {
            ++num1;
            ch = char.MinValue;
          }
          else
            break;
        }
        if (ch == char.MinValue && index != 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = dbcsDecoder != null ? dbcsDecoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          int num2 = num1 - 1;
          byte[] bytes1;
          if (index < 256)
            bytes1 = new byte[1]{ (byte) index };
          else
            bytes1 = new byte[2]
            {
              (byte) (index >> 8),
              (byte) index
            };
          num1 = num2 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      this.CheckMemorySection();
      DBCSCodePageEncoding.DBCSDecoder dbcsDecoder = (DBCSCodePageEncoding.DBCSDecoder) baseDecoder;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      char* chPtr = chars;
      char* charEnd = chars + charCount;
      bool flag = false;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      if (dbcsDecoder != null && dbcsDecoder.bLeftOver > (byte) 0)
      {
        if (byteCount == 0)
        {
          if (!dbcsDecoder.MustFlush)
            return 0;
          DecoderFallbackBuffer fallbackBuffer = dbcsDecoder.FallbackBuffer;
          fallbackBuffer.InternalInitialize(bytes, charEnd);
          byte[] bytes1 = new byte[1]
          {
            dbcsDecoder.bLeftOver
          };
          if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
          dbcsDecoder.bLeftOver = (byte) 0;
          return (int) (chars - chPtr);
        }
        int index = (int) dbcsDecoder.bLeftOver << 8 | (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if (ch == char.MinValue && index != 0)
        {
          decoderFallbackBuffer = dbcsDecoder.FallbackBuffer;
          decoderFallbackBuffer.InternalInitialize(numPtr2 - byteCount, charEnd);
          byte[] bytes1 = new byte[2]
          {
            (byte) (index >> 8),
            (byte) index
          };
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
        }
        else
        {
          if (chars >= charEnd)
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
          *chars++ = ch;
        }
      }
      while (bytes < numPtr2)
      {
        int index = (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if (ch == '\xFFFE')
        {
          if (bytes < numPtr2)
          {
            index = index << 8 | (int) *bytes;
            ++bytes;
            ch = this.mapBytesToUnicode[index];
          }
          else if (dbcsDecoder == null || dbcsDecoder.MustFlush)
          {
            ch = char.MinValue;
          }
          else
          {
            flag = true;
            dbcsDecoder.bLeftOver = (byte) index;
            break;
          }
        }
        if (ch == char.MinValue && index != 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = dbcsDecoder != null ? dbcsDecoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr2 - byteCount, charEnd);
          }
          byte[] bytes1;
          if (index < 256)
            bytes1 = new byte[1]{ (byte) index };
          else
            bytes1 = new byte[2]
            {
              (byte) (index >> 8),
              (byte) index
            };
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            bytes -= bytes1.Length;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, bytes == numPtr1);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            if (index >= 256)
              --bytes;
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, bytes == numPtr1);
            break;
          }
          *chars++ = ch;
        }
      }
      if (dbcsDecoder != null)
      {
        if (!flag)
          dbcsDecoder.bLeftOver = (byte) 0;
        dbcsDecoder.m_bytesUsed = (int) (bytes - numPtr1);
      }
      return (int) (chars - chPtr);
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 * 2L;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    public override Decoder GetDecoder()
    {
      return (Decoder) new DBCSCodePageEncoding.DBCSDecoder(this);
    }

    [Serializable]
    internal class DBCSDecoder : DecoderNLS
    {
      internal byte bLeftOver;

      public DBCSDecoder(DBCSCodePageEncoding encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.bLeftOver = (byte) 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          return this.bLeftOver > (byte) 0;
        }
      }
    }
  }
}
