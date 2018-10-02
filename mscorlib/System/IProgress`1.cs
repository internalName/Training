// Decompiled with JetBrains decompiler
// Type: System.IProgress`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>Определяет поставщика обновлений хода выполнения.</summary>
  /// <typeparam name="T">
  ///   Тип значения хода выполнения обновления.
  /// </typeparam>
  [__DynamicallyInvokable]
  public interface IProgress<in T>
  {
    /// <summary>Отчеты обновления хода выполнения.</summary>
    /// <param name="value">Значение обновленные хода выполнения.</param>
    [__DynamicallyInvokable]
    void Report(T value);
  }
}
