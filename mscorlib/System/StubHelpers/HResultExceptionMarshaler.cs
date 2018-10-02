// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.HResultExceptionMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class HResultExceptionMarshaler
  {
    internal static int ConvertToNative(Exception ex)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      if (ex == null)
        return 0;
      return ex._HResult;
    }

    [SecuritySafeCritical]
    internal static Exception ConvertToManaged(int hr)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      Exception exception = (Exception) null;
      if (hr < 0)
        exception = System.StubHelpers.StubHelpers.InternalGetCOMHRExceptionObject(hr, IntPtr.Zero, (object) null, true);
      return exception;
    }
  }
}
