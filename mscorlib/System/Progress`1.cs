// Decompiled with JetBrains decompiler
// Type: System.Progress`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System
{
  /// <summary>
  ///   Предоставляет <see cref="T:System.IProgress`1" /> , инициирующий обратные вызовы для каждого зарегистрирована значение хода выполнения.
  /// </summary>
  /// <typeparam name="T">
  ///   Указывает тип значение отчета о ходе выполнения.
  /// </typeparam>
  [__DynamicallyInvokable]
  public class Progress<T> : IProgress<T>
  {
    private readonly SynchronizationContext m_synchronizationContext;
    private readonly Action<T> m_handler;
    private readonly SendOrPostCallback m_invokeHandlers;

    /// <summary>
    ///   Инициализирует <see cref="T:System.Progress`1" /> объекта.
    /// </summary>
    [__DynamicallyInvokable]
    public Progress()
    {
      this.m_synchronizationContext = SynchronizationContext.CurrentNoFlow ?? ProgressStatics.DefaultContext;
      this.m_invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
    }

    /// <summary>
    ///   Инициализирует <see cref="T:System.Progress`1" /> объект с указанным обратного вызова.
    /// </summary>
    /// <param name="handler">
    ///   Обработчик для вызова каждого представлена значение хода выполнения.
    ///    Этот обработчик будет вызываться в дополнение к каких-либо делегатов, зарегистрированные с <see cref="E:System.Progress`1.ProgressChanged" /> события.
    ///    В зависимости от <see cref="T:System.Threading.SynchronizationContext" /> захвачено экземпляр <see cref="T:System.Progress`1" /> во время создания, возможно, что этот экземпляр обработчика может вызываться параллельно с самого.
    /// </param>
    [__DynamicallyInvokable]
    public Progress(Action<T> handler)
      : this()
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      this.m_handler = handler;
    }

    /// <summary>
    ///   Вызывается для каждого заявленного значения хода выполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public event EventHandler<T> ProgressChanged;

    /// <summary>Сообщает об изменении хода выполнения.</summary>
    /// <param name="value">Значение обновленные хода выполнения.</param>
    [__DynamicallyInvokable]
    protected virtual void OnReport(T value)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.m_handler == null && this.ProgressChanged == null)
        return;
      this.m_synchronizationContext.Post(this.m_invokeHandlers, (object) value);
    }

    [__DynamicallyInvokable]
    void IProgress<T>.Report(T value)
    {
      this.OnReport(value);
    }

    private void InvokeHandlers(object state)
    {
      T e = (T) state;
      Action<T> handler = this.m_handler;
      // ISSUE: reference to a compiler-generated field
      EventHandler<T> progressChanged = this.ProgressChanged;
      if (handler != null)
        handler(e);
      if (progressChanged == null)
        return;
      progressChanged((object) this, e);
    }
  }
}
