// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TaskAwaiter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет объект, который ожидает завершения асинхронной задачи.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
  {
    private readonly Task m_task;

    internal TaskAwaiter(Task task)
    {
      this.m_task = task;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, завершена ли асинхронная задача.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если задача была завершена; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> Объект не был правильно инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task.IsCompleted;
      }
    }

    /// <summary>
    ///   Задает действие, выполняемое при <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> объект прекращает ожидание завершения асинхронной задачи.
    /// </summary>
    /// <param name="continuation">
    ///   Действие, выполняемое при завершении операции ожидания.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="continuation" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> Объект не был правильно инициализирован.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void OnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
    }

    /// <summary>
    ///   Планирует продолжение действия для асинхронной задачи, который связан с этой типа awaiter.
    /// </summary>
    /// <param name="continuation">
    ///   Действие, вызываемый после завершения операции await.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="continuation" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Типа awaiter не был правильно инициализирован.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void UnsafeOnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
    }

    /// <summary>Прекращает ожидание завершения асинхронной задачи.</summary>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> Объект не был правильно инициализирован.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача отменена.
    /// </exception>
    /// <exception cref="T:System.Exception">
    ///   Задача завершилась в <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> состояние.
    /// </exception>
    [__DynamicallyInvokable]
    public void GetResult()
    {
      TaskAwaiter.ValidateEnd(this.m_task);
    }

    internal static void ValidateEnd(Task task)
    {
      if (!task.IsWaitNotificationEnabledOrNotRanToCompletion)
        return;
      TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
    }

    private static void HandleNonSuccessAndDebuggerNotification(Task task)
    {
      if (!task.IsCompleted)
        task.InternalWait(-1, new CancellationToken());
      task.NotifyDebuggerOfWaitCompletionIfNecessary();
      if (task.IsRanToCompletion)
        return;
      TaskAwaiter.ThrowForNonSuccess(task);
    }

    private static void ThrowForNonSuccess(Task task)
    {
      switch (task.Status)
      {
        case TaskStatus.Canceled:
          task.GetCancellationExceptionDispatchInfo()?.Throw();
          throw new TaskCanceledException(task);
        case TaskStatus.Faulted:
          ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
          if (exceptionDispatchInfos.Count <= 0)
            throw task.Exception;
          exceptionDispatchInfos[0].Throw();
          break;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
    {
      if (continuation == null)
        throw new ArgumentNullException(nameof (continuation));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (TplEtwProvider.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
        continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
      task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext, ref stackMark);
    }

    private static Action OutputWaitEtwEvents(Task task, Action continuation)
    {
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(task);
      TplEtwProvider etwLog = TplEtwProvider.Log;
      if (etwLog.IsEnabled())
      {
        Task internalCurrent = Task.InternalCurrent;
        Task continuationTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
        etwLog.TaskWaitBegin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, task.Id, TplEtwProvider.TaskWaitBehavior.Asynchronous, continuationTask != null ? continuationTask.Id : 0, Thread.GetDomainID());
      }
      return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, (Action) (() =>
      {
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(task.Id);
        Guid oldActivityThatWillContinue = new Guid();
        bool flag = etwLog.IsEnabled();
        if (flag)
        {
          Task internalCurrent = Task.InternalCurrent;
          etwLog.TaskWaitEnd(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, task.Id);
          if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions) 1024) != TaskCreationOptions.None)
            EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(task.Id), out oldActivityThatWillContinue);
        }
        continuation();
        if (!flag)
          return;
        etwLog.TaskWaitContinuationComplete(task.Id);
        if (!etwLog.TasksSetActivityIds || (task.Options & (TaskCreationOptions) 1024) == TaskCreationOptions.None)
          return;
        EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
      }), (Task) null);
    }
  }
}
