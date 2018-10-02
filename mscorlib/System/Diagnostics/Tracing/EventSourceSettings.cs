// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceSettings
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Задает параметры конфигурации для источника события.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventSourceSettings
  {
    [__DynamicallyInvokable] Default = 0,
    [__DynamicallyInvokable] ThrowOnEventWriteErrors = 1,
    [__DynamicallyInvokable] EtwManifestEventFormat = 4,
    [__DynamicallyInvokable] EtwSelfDescribingEventFormat = 8,
  }
}
