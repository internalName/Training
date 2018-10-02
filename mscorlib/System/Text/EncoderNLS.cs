// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderNLS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class EncoderNLS : Encoder, ISerializable
  {
    internal char charLeftOver;
    protected Encoding m_encoding;
    [NonSerialized]
    protected bool m_mustFlush;
    [NonSerialized]
    internal bool m_throwOnOverflow;
    [NonSerialized]
    internal int m_charsUsed;

    internal EncoderNLS(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), (object) this.GetType()));
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.SerializeEncoder(info);
      info.AddValue("encoding", (object) this.m_encoding);
      info.AddValue("charLeftOver", this.charLeftOver);
      info.SetType(typeof (Encoding.DefaultEncoder));
    }

    internal EncoderNLS(Encoding encoding)
    {
      this.m_encoding = encoding;
      this.m_fallback = this.m_encoding.EncoderFallback;
      this.Reset();
    }

    internal EncoderNLS()
    {
      this.m_encoding = (Encoding) null;
      this.Reset();
    }

    public override void Reset()
    {
      this.charLeftOver = char.MinValue;
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    [SecuritySafeCritical]
    public override unsafe int GetByteCount(char[] chars, int index, int count, bool flush)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length == 0)
        chars = new char[1];
      int byteCount;
      fixed (char* chPtr = chars)
        byteCount = this.GetByteCount(chPtr + index, count, flush);
      return byteCount;
    }

    [SecurityCritical]
    public override unsafe int GetByteCount(char* chars, int count, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = true;
      return this.m_encoding.GetByteCount(chars, count, this);
    }

    [SecuritySafeCritical]
    public override unsafe int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
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
        chars = new char[1];
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (char* chPtr = chars)
        fixed (byte* numPtr = bytes)
          return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, flush);
    }

    [SecurityCritical]
    public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = true;
      return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
    }

    [SecuritySafeCritical]
    public override unsafe void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length == 0)
        chars = new char[1];
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (char* chPtr = chars)
        fixed (byte* numPtr = bytes)
          this.Convert(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
    }

    [SecurityCritical]
    public override unsafe void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_mustFlush = flush;
      this.m_throwOnOverflow = false;
      this.m_charsUsed = 0;
      bytesUsed = this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
      charsUsed = this.m_charsUsed;
      completed = charsUsed == charCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
    }

    public Encoding Encoding
    {
      get
      {
        return this.m_encoding;
      }
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
        return this.charLeftOver > char.MinValue;
      }
    }

    internal void ClearMustFlush()
    {
      this.m_mustFlush = false;
    }
  }
}
