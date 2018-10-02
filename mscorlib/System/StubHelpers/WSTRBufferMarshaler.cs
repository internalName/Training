// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.WSTRBufferMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class WSTRBufferMarshaler
  {
    internal static IntPtr ConvertToNative(string strManaged)
    {
      return IntPtr.Zero;
    }

    internal static string ConvertToManaged(IntPtr bstr)
    {
      return (string) null;
    }

    internal static void ClearNative(IntPtr pNative)
    {
    }
  }
}
