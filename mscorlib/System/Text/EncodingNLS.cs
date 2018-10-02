// Decompiled with JetBrains decompiler
// Type: System.Text.EncodingNLS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
  [ComVisible(true)]
  [Serializable]
  internal abstract class EncodingNLS : Encoding
  {
    protected EncodingNLS(int codePage)
      : base(codePage)
    {
    }

    [SecuritySafeCritical]
    public override unsafe int GetByteCount(char[] chars, int index, int count)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length == 0)
        return 0;
      fixed (char* chPtr = chars)
        return this.GetByteCount(chPtr + index, count, (EncoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe int GetByteCount(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      string str = s;
      char* chars = (char*) str;
      if ((IntPtr) chars != IntPtr.Zero)
        chars += RuntimeHelpers.OffsetToStringData;
      return this.GetByteCount(chars, s.Length, (EncoderNLS) null);
    }

    [SecurityCritical]
    public override unsafe int GetByteCount(char* chars, int count)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetByteCount(chars, count, (EncoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (s == null || bytes == null)
        throw new ArgumentNullException(s == null ? nameof (s) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (s.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (s), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException(nameof (byteIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      string str = s;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      fixed (byte* numPtr = bytes)
        return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException(nameof (byteIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (chars.Length == 0)
        return 0;
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (char* chPtr = chars)
        fixed (byte* numPtr = bytes)
          return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    [SecurityCritical]
    public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetBytes(chars, charCount, bytes, byteCount, (EncoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe int GetCharCount(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return 0;
      fixed (byte* numPtr = bytes)
        return this.GetCharCount(numPtr + index, count, (DecoderNLS) null);
    }

    [SecurityCritical]
    public override unsafe int GetCharCount(byte* bytes, int count)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetCharCount(bytes, count, (DecoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (charIndex < 0 || charIndex > chars.Length)
        throw new ArgumentOutOfRangeException(nameof (charIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (bytes.Length == 0)
        return 0;
      int charCount = chars.Length - charIndex;
      if (chars.Length == 0)
        chars = new char[1];
      fixed (byte* numPtr = bytes)
        fixed (char* chPtr = chars)
          return this.GetChars(numPtr + byteIndex, byteCount, chPtr + charIndex, charCount, (DecoderNLS) null);
    }

    [SecurityCritical]
    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetChars(bytes, byteCount, chars, charCount, (DecoderNLS) null);
    }

    [SecuritySafeCritical]
    public override unsafe string GetString(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return string.Empty;
      fixed (byte* numPtr = bytes)
        return string.CreateStringFromEncoding(numPtr + index, count, (Encoding) this);
    }

    public override Decoder GetDecoder()
    {
      return (Decoder) new DecoderNLS((Encoding) this);
    }

    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }
  }
}
