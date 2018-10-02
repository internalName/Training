// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventCommand
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Описывает команду (<see cref="P:System.Diagnostics.Tracing.EventCommandEventArgs.Command" /> Свойства), передается <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> обратного вызова.
  /// </summary>
  [__DynamicallyInvokable]
  public enum EventCommand
  {
    [__DynamicallyInvokable] Disable = -3,
    [__DynamicallyInvokable] Enable = -2,
    [__DynamicallyInvokable] SendManifest = -1,
    [__DynamicallyInvokable] Update = 0,
  }
}
