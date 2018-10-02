// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.UnobservedTaskExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Предоставляет данные для события, вызванные ошибками <see cref="T:System.Threading.Tasks.Task" />происходит непредвиденное исключение.
  /// </summary>
  [__DynamicallyInvokable]
  public class UnobservedTaskExceptionEventArgs : EventArgs
  {
    private AggregateException m_exception;
    internal bool m_observed;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> класса непредвиденное исключение.
    /// </summary>
    /// <param name="exception">
    ///   Исключение, которое прошло без наблюдения.
    /// </param>
    [__DynamicallyInvokable]
    public UnobservedTaskExceptionEventArgs(AggregateException exception)
    {
      this.m_exception = exception;
    }

    /// <summary>
    ///   Метки <see cref="P:System.Threading.Tasks.UnobservedTaskExceptionEventArgs.Exception" /> «соблюдения», таким образом предотвращая его запускать политику роста исключений, который по умолчанию завершает процесс.
    /// </summary>
    [__DynamicallyInvokable]
    public void SetObserved()
    {
      this.m_observed = true;
    }

    /// <summary>
    ///   Получает ли это исключение будет помечена как «наблюдаемых».
    /// </summary>
    /// <returns>
    ///   значение true, если это исключение будет помечена как «окончание»; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Observed
    {
      [__DynamicallyInvokable] get
      {
        return this.m_observed;
      }
    }

    /// <summary>Непредвиденное исключение.</summary>
    /// <returns>Непредвиденное исключение.</returns>
    [__DynamicallyInvokable]
    public AggregateException Exception
    {
      [__DynamicallyInvokable] get
      {
        return this.m_exception;
      }
    }
  }
}
