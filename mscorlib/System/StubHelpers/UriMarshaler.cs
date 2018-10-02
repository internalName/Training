// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.UriMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class UriMarshaler
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetRawUriFromNative(IntPtr pUri);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe IntPtr CreateNativeUriInstanceHelper(char* rawUri, int strLen);

    [SecurityCritical]
    internal static unsafe IntPtr CreateNativeUriInstance(string rawUri)
    {
      string str = rawUri;
      char* rawUri1 = (char*) str;
      if ((IntPtr) rawUri1 != IntPtr.Zero)
        rawUri1 += RuntimeHelpers.OffsetToStringData;
      return UriMarshaler.CreateNativeUriInstanceHelper(rawUri1, rawUri.Length);
    }
  }
}
