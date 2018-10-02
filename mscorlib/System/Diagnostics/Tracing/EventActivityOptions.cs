// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventActivityOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Задает отслеживание событий запуска и остановки действия.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventActivityOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Disable = 2,
    [__DynamicallyInvokable] Recursive = 4,
    [__DynamicallyInvokable] Detachable = 8,
  }
}
