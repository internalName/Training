// Decompiled with JetBrains decompiler
// Type: System.IObservable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>Определяет поставщика push-уведомлений.</summary>
  /// <typeparam name="T">
  ///   Объект, предоставляющий сведения об уведомлениях.
  /// </typeparam>
  [__DynamicallyInvokable]
  public interface IObservable<out T>
  {
    /// <summary>
    ///   Уведомляет поставщика о том, что наблюдатель должен получать уведомления.
    /// </summary>
    /// <param name="observer">
    ///   Объект, который должен получать уведомления.
    /// </param>
    /// <returns>
    ///   Ссылка на интерфейс, позволяющий наблюдателям прекратить получение уведомлений до того, как поставщик завершит их отправку.
    /// </returns>
    [__DynamicallyInvokable]
    IDisposable Subscribe(IObserver<T> observer);
  }
}
