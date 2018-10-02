// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceCreatedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.Diagnostics.Tracing.EventListener.EventSourceCreated" />.
  /// </summary>
  public class EventSourceCreatedEventArgs : EventArgs
  {
    /// <summary>
    ///   Получение источника события, который присоединяется к прослушивателю.
    /// </summary>
    /// <returns>
    ///   Источник события, который присоединяется к прослушивателю.
    /// </returns>
    public EventSource EventSource { get; internal set; }
  }
}
