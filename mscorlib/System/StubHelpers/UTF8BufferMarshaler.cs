// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.UTF8BufferMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class UTF8BufferMarshaler
  {
    [SecurityCritical]
    internal static unsafe IntPtr ConvertToNative(StringBuilder sb, IntPtr pNativeBuffer, int flags)
    {
      if (sb == null)
        return IntPtr.Zero;
      string s = sb.ToString();
      int byteCount = Encoding.UTF8.GetByteCount(s);
      byte* pbNativeBuffer = (byte*) (void*) pNativeBuffer;
      int bytesFromEncoding = s.GetBytesFromEncoding(pbNativeBuffer, byteCount, Encoding.UTF8);
      pbNativeBuffer[bytesFromEncoding] = (byte) 0;
      return (IntPtr) ((void*) pbNativeBuffer);
    }

    [SecurityCritical]
    internal static unsafe void ConvertToManaged(StringBuilder sb, IntPtr pNative)
    {
      int num = System.StubHelpers.StubHelpers.strlen((sbyte*) (void*) pNative);
      int charCount = Encoding.UTF8.GetCharCount((byte*) (void*) pNative, num);
      char[] chArray = new char[charCount + 1];
      chArray[charCount] = char.MinValue;
      fixed (char* chPtr = chArray)
      {
        int chars = Encoding.UTF8.GetChars((byte*) (void*) pNative, num, chPtr, charCount);
        sb.ReplaceBufferInternal(chPtr, chars);
      }
    }
  }
}
