// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderNLS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class DecoderNLS : Decoder, ISerializable
  {
    protected Encoding m_encoding;
    [NonSerialized]
    protected bool m_mustFlush;
    [NonSerialized]
    internal bool m_throwOnOverflow;
    [NonSerialized]
    internal int m_bytesUsed;

    internal DecoderNLS(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), (object) this.GetType()));
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.SerializeDecoder(info);
      info.AddValue("encoding", (object) this.m_encoding);
      info.SetType(typeof (Encoding.DefaultDecoder));
    }

    internal DecoderNLS(Encoding encoding)
    {
      this.m_encoding = encoding;
      this.m_fallback = this.m_encoding.DecoderFallback;
      this.Reset();
    }

    internal DecoderNLS()
    {
      this.m_encoding = (Encoding) null;
      this.Reset();
    }

    public override void Reset()
    {
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    public override int GetCharCount(byte[] bytes, int index, int count)
    {
      return this.GetCharCount(bytes, index, count, false);
    }

    [SecuritySafeCritical]
    public override unsafe int GetCharCount(byte[] bytes, int index, int count, bool flush)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (byte* numPtr = bytes)
        return this.GetCharCount(numPtr + index, count, flush);
    }

    [SecurityCritical]
    public override unsafe int GetCharCount(byte* bytes, int count, bool flush)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = true;
      return this.m_encoding.GetCharCount(bytes, count, this);
    }

    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
    }

    [SecuritySafeCritical]
    public override unsafe int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
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
        bytes = new byte[1];
      int charCount = chars.Length - charIndex;
      if (chars.Length == 0)
        chars = new char[1];
      fixed (byte* numPtr = bytes)
        fixed (char* chPtr = chars)
          return this.GetChars(numPtr + byteIndex, byteCount, chPtr + charIndex, charCount, flush);
    }

    [SecurityCritical]
    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = true;
      return this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
    }

    [SecuritySafeCritical]
    public override unsafe void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        bytes = new byte[1];
      if (chars.Length == 0)
        chars = new char[1];
      fixed (byte* numPtr = bytes)
        fixed (char* chPtr = chars)
          this.Convert(numPtr + byteIndex, byteCount, chPtr + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
    }

    [SecurityCritical]
    public override unsafe void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = false;
      this.m_bytesUsed = 0;
      charsUsed = this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
      bytesUsed = this.m_bytesUsed;
      completed = bytesUsed == byteCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
    }

    public bool MustFlush
    {
      get
      {
        return this.m_mustFlush;
      }
    }

    internal virtual bool HasState
    {
      get
      {
        return false;
      }
    }

    internal void ClearMustFlush()
    {
      this.m_mustFlush = false;
    }
  }
}
