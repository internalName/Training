// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.AwaitTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.Threading.Tasks
{
  internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
  {
    private readonly ExecutionContext m_capturedContext;
    protected readonly Action m_action;
    protected int m_continuationId;
    [SecurityCritical]
    private static ContextCallback s_invokeActionCallback;

    [SecurityCritical]
    internal AwaitTaskContinuation(Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
    {
      this.m_action = action;
      if (!flowExecutionContext)
        return;
      this.m_capturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    [SecurityCritical]
    internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
    {
      this.m_action = action;
      if (!flowExecutionContext)
        return;
      this.m_capturedContext = ExecutionContext.FastCapture();
    }

    protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
    {
      return new Task((Delegate) action, state, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
      {
        CapturedContext = this.m_capturedContext
      };
    }

    [SecuritySafeCritical]
    internal override void Run(Task task, bool canInlineContinuationTask)
    {
      if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
      {
        this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), (object) this.m_action, ref Task.t_currentTask);
      }
      else
      {
        TplEtwProvider log = TplEtwProvider.Log;
        if (log.IsEnabled())
        {
          this.m_continuationId = Task.NewId();
          log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
        }
        ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) this, false);
      }
    }

    internal static bool IsValidLocationForInlining
    {
      get
      {
        SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
        if (currentNoFlow != null && currentNoFlow.GetType() != typeof (SynchronizationContext))
          return false;
        TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
        if (internalCurrent != null)
          return internalCurrent == TaskScheduler.Default;
        return true;
      }
    }

    [SecurityCritical]
    private void ExecuteWorkItemHelper()
    {
      TplEtwProvider log = TplEtwProvider.Log;
      Guid oldActivityThatWillContinue = Guid.Empty;
      if (log.TasksSetActivityIds && this.m_continuationId != 0)
        EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.m_continuationId), out oldActivityThatWillContinue);
      try
      {
        if (this.m_capturedContext == null)
        {
          this.m_action();
        }
        else
        {
          try
          {
            ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), (object) this.m_action, true);
          }
          finally
          {
            this.m_capturedContext.Dispose();
          }
        }
      }
      finally
      {
        if (log.TasksSetActivityIds && this.m_continuationId != 0)
          EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
      }
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.ExecuteWorkItem()
    {
      if (this.m_capturedContext == null && !TplEtwProvider.Log.IsEnabled())
        this.m_action();
      else
        this.ExecuteWorkItemHelper();
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
    {
    }

    [SecurityCritical]
    private static void InvokeAction(object state)
    {
      ((Action) state)();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static ContextCallback GetInvokeActionCallback()
    {
      ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
      if (contextCallback == null)
        AwaitTaskContinuation.s_invokeActionCallback = contextCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction);
      return contextCallback;
    }

    [SecurityCritical]
    protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
    {
      Task task = currentTask;
      try
      {
        if (task != null)
          currentTask = (Task) null;
        if (this.m_capturedContext == null)
          callback(state);
        else
          ExecutionContext.Run(this.m_capturedContext, callback, state, true);
      }
      catch (Exception ex)
      {
        AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
      }
      finally
      {
        if (task != null)
          currentTask = task;
        if (this.m_capturedContext != null)
          this.m_capturedContext.Dispose();
      }
    }

    [SecurityCritical]
    internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
    {
      if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
      {
        AwaitTaskContinuation.UnsafeScheduleAction(action, currentTask);
      }
      else
      {
        Task task = currentTask;
        try
        {
          if (task != null)
            currentTask = (Task) null;
          action();
        }
        catch (Exception ex)
        {
          AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
        }
        finally
        {
          if (task != null)
            currentTask = task;
        }
      }
    }

    [SecurityCritical]
    internal static void UnsafeScheduleAction(Action action, Task task)
    {
      AwaitTaskContinuation taskContinuation = new AwaitTaskContinuation(action, false);
      TplEtwProvider log = TplEtwProvider.Log;
      if (log.IsEnabled() && task != null)
      {
        taskContinuation.m_continuationId = Task.NewId();
        log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, taskContinuation.m_continuationId);
      }
      ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) taskContinuation, false);
    }

    protected static void ThrowAsyncIfNecessary(Exception exc)
    {
      if (exc is ThreadAbortException || exc is AppDomainUnloadedException || WindowsRuntimeMarshal.ReportUnhandledError(exc))
        return;
      ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exc);
      ThreadPool.QueueUserWorkItem((WaitCallback) (s => ((ExceptionDispatchInfo) s).Throw()), (object) exceptionDispatchInfo);
    }

    internal override Delegate[] GetDelegateContinuationsForDebugger()
    {
      return new Delegate[1]
      {
        (Delegate) AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action)
      };
    }
  }
}
