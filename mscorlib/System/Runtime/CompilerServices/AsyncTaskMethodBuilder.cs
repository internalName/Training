// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Представляет конструктор для асинхронных методов, возвращающих задачу.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncTaskMethodBuilder
  {
    private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;
    private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder" />.
    /// </summary>
    /// <returns>Новый экземпляр построителя.</returns>
    [__DynamicallyInvokable]
    public static AsyncTaskMethodBuilder Create()
    {
      return new AsyncTaskMethodBuilder();
    }

    /// <summary>
    ///   Запускает построитель с связанного конечного автомата.
    /// </summary>
    /// <param name="stateMachine">
    ///   Экземпляр конечного автомата, передаваемый по ссылке.
    /// </param>
    /// <typeparam name="TStateMachine">
    ///   Тип конечного автомата.
    /// </typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stateMachine" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [__DynamicallyInvokable]
    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
    {
      if ((object) stateMachine == null)
        throw new ArgumentNullException(nameof (stateMachine));
      ExecutionContextSwitcher ecsw = new ExecutionContextSwitcher();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        ExecutionContext.EstablishCopyOnWriteScope(ref ecsw);
        stateMachine.MoveNext();
      }
      finally
      {
        ecsw.Undo();
      }
    }

    /// <summary>
    ///   Связывает построитель с указанного конечного автомата.
    /// </summary>
    /// <param name="stateMachine">
    ///   Экземпляр конечного автомата для связи с помощью построителя.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stateMachine" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Конечный автомат было установлено ранее.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
      this.m_builder.SetStateMachine(stateMachine);
    }

    /// <summary>
    ///   Планирует конечного автомата, чтобы перейти к следующему действию по завершении заданного объекта типа awaiter.
    /// </summary>
    /// <param name="awaiter">Типа awaiter.</param>
    /// <param name="stateMachine">Конечный автомат.</param>
    /// <typeparam name="TAwaiter">Тип объекта типа awaiter.</typeparam>
    /// <typeparam name="TStateMachine">
    ///   Тип конечного автомата.
    /// </typeparam>
    [__DynamicallyInvokable]
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
    }

    /// <summary>
    ///   Планирует конечного автомата, чтобы перейти к следующему действию по завершении заданного объекта типа awaiter.
    ///    Этот метод может вызываться из частично доверенного кода.
    /// </summary>
    /// <param name="awaiter">Типа awaiter.</param>
    /// <param name="stateMachine">Конечный автомат.</param>
    /// <typeparam name="TAwaiter">Тип объекта типа awaiter.</typeparam>
    /// <typeparam name="TStateMachine">
    ///   Тип конечного автомата.
    /// </typeparam>
    [__DynamicallyInvokable]
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
    }

    /// <summary>Возвращает задачу для этот построитель.</summary>
    /// <returns>Задача для этого построитель.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Построитель отчетов не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public Task Task
    {
      [__DynamicallyInvokable] get
      {
        return (Task) this.m_builder.Task;
      }
    }

    /// <summary>Помечает задача успешно завершена.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача уже завершена.
    /// 
    ///   -или-
    /// 
    ///   Построитель отчетов не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetResult()
    {
      this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
    }

    /// <summary>
    ///   Помечает задачу как сбой и привязывает указанное исключение в задачу.
    /// </summary>
    /// <param name="exception">Исключение для привязки к задаче.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="exception" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача уже завершена.
    /// 
    ///   -или-
    /// 
    ///   Построитель отчетов не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      this.m_builder.SetException(exception);
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      this.m_builder.SetNotificationForWaitCompletion(enabled);
    }

    private object ObjectIdForDebugger
    {
      get
      {
        return (object) this.Task;
      }
    }
  }
}
