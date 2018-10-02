// Decompiled with JetBrains decompiler
// Type: System.Mda
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  internal static class Mda
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ReportStreamWriterBufferedDataLost(string text);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsStreamWriterBufferedDataLostEnabled();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsStreamWriterBufferedDataLostCaptureAllocatedCallStack();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void MemberInfoCacheCreation();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DateTimeInvalidLocalFormat();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsInvalidGCHandleCookieProbeEnabled();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void FireInvalidGCHandleCookieProbe(IntPtr cookie);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ReportErrorSafeHandleRelease(Exception ex);

    internal static class StreamWriterBufferedDataLost
    {
      private static volatile int _enabledState;
      private static volatile int _captureAllocatedCallStackState;

      internal static bool Enabled
      {
        [SecuritySafeCritical] get
        {
          if (Mda.StreamWriterBufferedDataLost._enabledState == 0)
            Mda.StreamWriterBufferedDataLost._enabledState = !Mda.IsStreamWriterBufferedDataLostEnabled() ? 2 : 1;
          return Mda.StreamWriterBufferedDataLost._enabledState == 1;
        }
      }

      internal static bool CaptureAllocatedCallStack
      {
        [SecuritySafeCritical] get
        {
          if (Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 0)
            Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = !Mda.IsStreamWriterBufferedDataLostCaptureAllocatedCallStack() ? 2 : 1;
          return Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 1;
        }
      }

      [SecuritySafeCritical]
      internal static void ReportError(string text)
      {
        Mda.ReportStreamWriterBufferedDataLost(text);
      }
    }
  }
}
