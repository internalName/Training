// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.YieldAwaitable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет контекст для ожидания при переключении асинхронно на целевую среду.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct YieldAwaitable
  {
    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> объектом для данного экземпляра класса.
    /// </summary>
    /// <returns>
    ///   Объект, который используется для отслеживания завершения асинхронной операции.
    /// </returns>
    [__DynamicallyInvokable]
    public YieldAwaitable.YieldAwaiter GetAwaiter()
    {
      return new YieldAwaitable.YieldAwaiter();
    }

    /// <summary>
    ///   Предоставляет контекст типа awaiter для переключения на целевую среду.
    /// </summary>
    [__DynamicallyInvokable]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
    {
      private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);
      private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);

      /// <summary>
      ///   Возвращает значение, указывающее, является ли yield не является обязательным.
      /// </summary>
      /// <returns>
      ///   Всегда <see langword="false" />, указывающая, что результат всегда является обязательным для <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" />.
      /// </returns>
      [__DynamicallyInvokable]
      public bool IsCompleted
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      /// <summary>Задает продолжения для вызова.</summary>
      /// <param name="continuation">
      ///   Действия для асинхронного вызова.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Свойство <paramref name="continuation" /> имеет значение <see langword="null" />.
      /// </exception>
      [SecuritySafeCritical]
      [__DynamicallyInvokable]
      public void OnCompleted(Action continuation)
      {
        YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
      }

      /// <summary>
      ///   Сообщения <paramref name="continuation" /> в текущем контексте.
      /// </summary>
      /// <param name="continuation">
      ///   Действия для асинхронного вызова.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Аргумент <paramref name="continuation" /> имеет значение <see langword="null" />.
      /// </exception>
      [SecurityCritical]
      [__DynamicallyInvokable]
      public void UnsafeOnCompleted(Action continuation)
      {
        YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
      }

      [SecurityCritical]
      private static void QueueContinuation(Action continuation, bool flowContext)
      {
        if (continuation == null)
          throw new ArgumentNullException(nameof (continuation));
        if (TplEtwProvider.Log.IsEnabled())
          continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
        SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
        if (currentNoFlow != null && currentNoFlow.GetType() != typeof (SynchronizationContext))
        {
          currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, (object) continuation);
        }
        else
        {
          TaskScheduler current = TaskScheduler.Current;
          if (current == TaskScheduler.Default)
          {
            if (flowContext)
              ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, (object) continuation);
            else
              ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, (object) continuation);
          }
          else
            Task.Factory.StartNew(continuation, new CancellationToken(), TaskCreationOptions.PreferFairness, current);
        }
      }

      private static Action OutputCorrelationEtwEvent(Action continuation)
      {
        int continuationId = Task.NewId();
        Task internalCurrent = Task.InternalCurrent;
        TplEtwProvider.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, continuationId);
        return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, (Action) (() =>
        {
          TplEtwProvider log = TplEtwProvider.Log;
          log.TaskWaitContinuationStarted(continuationId);
          Guid oldActivityThatWillContinue = new Guid();
          if (log.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out oldActivityThatWillContinue);
          continuation();
          if (log.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
          log.TaskWaitContinuationComplete(continuationId);
        }), (Task) null);
      }

      private static void RunAction(object state)
      {
        ((Action) state)();
      }

      /// <summary>Завершает операцию await.</summary>
      [__DynamicallyInvokable]
      public void GetResult()
      {
      }
    }
  }
}
