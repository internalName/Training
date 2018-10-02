// Decompiled with JetBrains decompiler
// Type: System.IObserver`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Предоставляет механизм для получения push-уведомлений.
  /// </summary>
  /// <typeparam name="T">
  ///   Объект, предоставляющий сведения об уведомлениях.
  /// </typeparam>
  [__DynamicallyInvokable]
  public interface IObserver<in T>
  {
    /// <summary>Предоставляет наблюдателю новые данные.</summary>
    /// <param name="value">Текущие сведения об уведомлениях.</param>
    [__DynamicallyInvokable]
    void OnNext(T value);

    /// <summary>
    ///   Уведомляет наблюдателя о том, что у поставщика возникла ошибка.
    /// </summary>
    /// <param name="error">
    ///   Объект, который предоставляет дополнительную информацию об ошибке.
    /// </param>
    [__DynamicallyInvokable]
    void OnError(Exception error);

    /// <summary>
    ///   Уведомляет наблюдателя о том, что поставщик завершил отправку push-уведомлений.
    /// </summary>
    [__DynamicallyInvokable]
    void OnCompleted();
  }
}
