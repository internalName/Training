// Decompiled with JetBrains decompiler
// Type: System.Utf8String
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
  internal struct Utf8String
  {
    [SecurityCritical]
    private unsafe void* m_pStringHeap;
    private int m_StringHeapByteLength;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe bool EqualsCaseSensitive(void* szLhs, void* szRhs, int cSz);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe bool EqualsCaseInsensitive(void* szLhs, void* szRhs, int cSz);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe uint HashCaseInsensitive(void* sz, int cSz);

    [SecurityCritical]
    private static unsafe int GetUtf8StringByteLength(void* pUtf8String)
    {
      int num = 0;
      for (byte* numPtr = (byte*) pUtf8String; *numPtr != (byte) 0; ++numPtr)
        ++num;
      return num;
    }

    [SecurityCritical]
    internal unsafe Utf8String(void* pStringHeap)
    {
      this.m_pStringHeap = pStringHeap;
      if ((IntPtr) pStringHeap != IntPtr.Zero)
        this.m_StringHeapByteLength = Utf8String.GetUtf8StringByteLength(pStringHeap);
      else
        this.m_StringHeapByteLength = 0;
    }

    [SecurityCritical]
    internal unsafe Utf8String(void* pUtf8String, int cUtf8String)
    {
      this.m_pStringHeap = pUtf8String;
      this.m_StringHeapByteLength = cUtf8String;
    }

    [SecuritySafeCritical]
    internal unsafe bool Equals(Utf8String s)
    {
      if ((IntPtr) this.m_pStringHeap == IntPtr.Zero)
        return s.m_StringHeapByteLength == 0;
      if (s.m_StringHeapByteLength == this.m_StringHeapByteLength && this.m_StringHeapByteLength != 0)
        return Utf8String.EqualsCaseSensitive(s.m_pStringHeap, this.m_pStringHeap, this.m_StringHeapByteLength);
      return false;
    }

    [SecuritySafeCritical]
    internal unsafe bool EqualsCaseInsensitive(Utf8String s)
    {
      if ((IntPtr) this.m_pStringHeap == IntPtr.Zero)
        return s.m_StringHeapByteLength == 0;
      if (s.m_StringHeapByteLength == this.m_StringHeapByteLength && this.m_StringHeapByteLength != 0)
        return Utf8String.EqualsCaseInsensitive(s.m_pStringHeap, this.m_pStringHeap, this.m_StringHeapByteLength);
      return false;
    }

    [SecuritySafeCritical]
    internal unsafe uint HashCaseInsensitive()
    {
      return Utf8String.HashCaseInsensitive(this.m_pStringHeap, this.m_StringHeapByteLength);
    }

    [SecuritySafeCritical]
    public override unsafe string ToString()
    {
      byte* bytes = stackalloc byte[this.m_StringHeapByteLength];
      byte* pStringHeap = (byte*) this.m_pStringHeap;
      for (int index = 0; index < this.m_StringHeapByteLength; ++index)
      {
        bytes[index] = *pStringHeap;
        ++pStringHeap;
      }
      if (this.m_StringHeapByteLength == 0)
        return "";
      int charCount = Encoding.UTF8.GetCharCount(bytes, this.m_StringHeapByteLength);
      char* chars = stackalloc char[charCount];
      Encoding.UTF8.GetChars(bytes, this.m_StringHeapByteLength, chars, charCount);
      return new string(chars, 0, charCount);
    }
  }
}
