// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventTags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Задает отслеживание событий запуска и остановки действия.
  ///    Следует использовать только младшие 24 бита.
  ///    Дополнительные сведения см. в разделах <see cref="T:System.Diagnostics.Tracing.EventSourceOptions" /> и <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" />.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventTags
  {
    [__DynamicallyInvokable] None = 0,
  }
}
