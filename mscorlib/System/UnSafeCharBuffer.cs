// Decompiled with JetBrains decompiler
// Type: System.UnSafeCharBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  internal struct UnSafeCharBuffer
  {
    [SecurityCritical]
    private unsafe char* m_buffer;
    private int m_totalSize;
    private int m_length;

    [SecurityCritical]
    public unsafe UnSafeCharBuffer(char* buffer, int bufferSize)
    {
      this.m_buffer = buffer;
      this.m_totalSize = bufferSize;
      this.m_length = 0;
    }

    [SecuritySafeCritical]
    public unsafe void AppendString(string stringToAppend)
    {
      if (string.IsNullOrEmpty(stringToAppend))
        return;
      if (this.m_totalSize - this.m_length < stringToAppend.Length)
        throw new IndexOutOfRangeException();
      string str = stringToAppend;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      Buffer.Memcpy((byte*) (this.m_buffer + this.m_length), (byte*) chPtr, stringToAppend.Length * 2);
      str = (string) null;
      this.m_length += stringToAppend.Length;
    }

    public int Length
    {
      get
      {
        return this.m_length;
      }
    }
  }
}
