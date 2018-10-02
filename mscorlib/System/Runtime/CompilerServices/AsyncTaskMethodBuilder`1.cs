// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1
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
  ///   Представляет конструктор для асинхронных методов, которая возвращает задачу и предоставляет параметр для результата.
  /// </summary>
  /// <typeparam name="TResult">Результат выполнения задачи.</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncTaskMethodBuilder<TResult>
  {
    internal static readonly System.Threading.Tasks.Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default (TResult));
    private AsyncMethodBuilderCore m_coreState;
    private System.Threading.Tasks.Task<TResult> m_task;

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1" />.
    /// </summary>
    /// <returns>Новый экземпляр построителя.</returns>
    [__DynamicallyInvokable]
    public static AsyncTaskMethodBuilder<TResult> Create()
    {
      return new AsyncTaskMethodBuilder<TResult>();
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
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? (System.Threading.Tasks.Task) this.Task : (System.Threading.Tasks.Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          System.Threading.Tasks.Task<TResult> task = this.Task;
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (System.Threading.Tasks.Task) task);
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
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? (System.Threading.Tasks.Task) this.Task : (System.Threading.Tasks.Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          System.Threading.Tasks.Task<TResult> task = this.Task;
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (System.Threading.Tasks.Task) task);
        }
        awaiter.UnsafeOnCompleted(completionAction);
      }
      catch (Exception ex)
      {
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) null);
      }
    }

    /// <summary>Возвращает задачу для этот построитель.</summary>
    /// <returns>Задача для этого построитель.</returns>
    [__DynamicallyInvokable]
    public System.Threading.Tasks.Task<TResult> Task
    {
      [__DynamicallyInvokable] get
      {
        System.Threading.Tasks.Task<TResult> task = this.m_task;
        if (task == null)
          this.m_task = task = new System.Threading.Tasks.Task<TResult>();
        return task;
      }
    }

    /// <summary>Помечает задача успешно завершена.</summary>
    /// <param name="result">Результат выполнения задачи.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача уже завершена.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetResult(TResult result)
    {
      System.Threading.Tasks.Task<TResult> task = this.m_task;
      if (task == null)
      {
        this.m_task = this.GetTaskForResult(result);
      }
      else
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
        if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
          System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
        if (!task.TrySetResult(result))
          throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
      }
    }

    internal void SetResult(System.Threading.Tasks.Task<TResult> completedTask)
    {
      if (this.m_task == null)
        this.m_task = completedTask;
      else
        this.SetResult(default (TResult));
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
    /// </exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      System.Threading.Tasks.Task<TResult> task = this.m_task ?? this.Task;
      OperationCanceledException canceledException = exception as OperationCanceledException;
      if (!(canceledException != null ? task.TrySetCanceled(canceledException.CancellationToken, (object) canceledException) : task.TrySetException((object) exception)))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      this.Task.SetNotificationForWaitCompletion(enabled);
    }

    private object ObjectIdForDebugger
    {
      get
      {
        return (object) this.Task;
      }
    }

    [SecuritySafeCritical]
    private System.Threading.Tasks.Task<TResult> GetTaskForResult(TResult result)
    {
      if ((object) default (TResult) != null)
      {
        if (typeof (TResult) == typeof (bool))
          return JitHelpers.UnsafeCast<System.Threading.Tasks.Task<TResult>>((bool) (object) result ? (object) AsyncTaskCache.TrueTask : (object) AsyncTaskCache.FalseTask);
        if (typeof (TResult) == typeof (int))
        {
          int num = (int) (object) result;
          if (num < 9 && num >= -1)
            return JitHelpers.UnsafeCast<System.Threading.Tasks.Task<TResult>>((object) AsyncTaskCache.Int32Tasks[num - -1]);
        }
        else if (typeof (TResult) == typeof (uint) && (uint) (object) result == 0U || typeof (TResult) == typeof (byte) && (byte) (object) result == (byte) 0 || (typeof (TResult) == typeof (sbyte) && (sbyte) (object) result == (sbyte) 0 || typeof (TResult) == typeof (char) && (char) (object) result == char.MinValue) || (typeof (TResult) == typeof (Decimal) && Decimal.Zero == (Decimal) (object) result || typeof (TResult) == typeof (long) && (long) (object) result == 0L || (typeof (TResult) == typeof (ulong) && (ulong) (object) result == 0UL || typeof (TResult) == typeof (short) && (short) (object) result == (short) 0)) || (typeof (TResult) == typeof (ushort) && (ushort) (object) result == (ushort) 0 || typeof (TResult) == typeof (IntPtr) && IntPtr.Zero == (IntPtr) (object) result || typeof (TResult) == typeof (UIntPtr) && UIntPtr.Zero == (UIntPtr) (object) result))
          return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
      }
      else if ((object) result == null)
        return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
      return new System.Threading.Tasks.Task<TResult>(result);
    }
  }
}
