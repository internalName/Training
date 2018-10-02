// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventKeywords
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Определяет стандартные ключевые слова, которые применяются к событиям.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventKeywords : long
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] All = -1,
    MicrosoftTelemetry = 562949953421312, // 0x0002000000000000
    [__DynamicallyInvokable] WdiContext = MicrosoftTelemetry, // 0x0002000000000000
    [__DynamicallyInvokable] WdiDiagnostic = 1125899906842624, // 0x0004000000000000
    [__DynamicallyInvokable] Sqm = 2251799813685248, // 0x0008000000000000
    [__DynamicallyInvokable] AuditFailure = 4503599627370496, // 0x0010000000000000
    [__DynamicallyInvokable] AuditSuccess = 9007199254740992, // 0x0020000000000000
    [__DynamicallyInvokable] CorrelationHint = AuditFailure, // 0x0010000000000000
    [__DynamicallyInvokable] EventLogClassic = 36028797018963968, // 0x0080000000000000
  }
}
