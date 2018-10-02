// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncVoidMethodBuilder
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
  ///   Представляет конструктор для асинхронных методов, которые не возвращают никакое значение.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncVoidMethodBuilder
  {
    private SynchronizationContext m_synchronizationContext;
    private AsyncMethodBuilderCore m_coreState;
    private Task m_task;

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Runtime.CompilerServices.AsyncVoidMethodBuilder" />.
    /// </summary>
    /// <returns>Новый экземпляр построителя.</returns>
    [__DynamicallyInvokable]
    public static AsyncVoidMethodBuilder Create()
    {
      SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
      currentNoFlow?.OperationStarted();
      return new AsyncVoidMethodBuilder()
      {
        m_synchronizationContext = currentNoFlow
      };
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
      this.m_coreState.SetStateMachine(stateMachine);
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
      try
      {
        AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize = (AsyncMethodBuilderCore.MoveNextRunner) null;
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : (Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (Task) null);
        }
        awaiter.OnCompleted(completionAction);
      }
      catch (Exception ex)
      {
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) null);
      }
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
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      try
      {
        AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize = (AsyncMethodBuilderCore.MoveNextRunner) null;
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : (Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (Task) null);
        }
        awaiter.UnsafeOnCompleted(completionAction);
      }
      catch (Exception ex)
      {
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) null);
      }
    }

    /// <summary>
    ///   Помечает построитель метода как успешного завершения.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Построитель отчетов не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetResult()
    {
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Completed);
      if (this.m_synchronizationContext == null)
        return;
      this.NotifySynchronizationContextOfCompletion();
    }

    /// <summary>Привязывает исключение построитель метода.</summary>
    /// <param name="exception">Исключение для привязки.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="exception" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Построитель отчетов не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Error);
      if (this.m_synchronizationContext != null)
      {
        try
        {
          AsyncMethodBuilderCore.ThrowAsync(exception, this.m_synchronizationContext);
        }
        finally
        {
          this.NotifySynchronizationContextOfCompletion();
        }
      }
      else
        AsyncMethodBuilderCore.ThrowAsync(exception, (SynchronizationContext) null);
    }

    private void NotifySynchronizationContextOfCompletion()
    {
      try
      {
        this.m_synchronizationContext.OperationCompleted();
      }
      catch (Exception ex)
      {
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) null);
      }
    }

    private Task Task
    {
      get
      {
        if (this.m_task == null)
          this.m_task = new Task();
        return this.m_task;
      }
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
