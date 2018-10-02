// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCLatencyMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>
  ///   Настраивает время вмешательства сборщика мусора в работу приложения.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCLatencyMode
  {
    [__DynamicallyInvokable] Batch,
    [__DynamicallyInvokable] Interactive,
    [__DynamicallyInvokable] LowLatency,
    [__DynamicallyInvokable] SustainedLowLatency,
    NoGCRegion,
  }
}
