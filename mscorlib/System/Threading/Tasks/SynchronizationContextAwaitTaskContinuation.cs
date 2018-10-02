// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.SynchronizationContextAwaitTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
  {
    private static readonly SendOrPostCallback s_postCallback = (SendOrPostCallback) (state => ((Action) state)());
    [SecurityCritical]
    private static ContextCallback s_postActionCallback;
    private readonly SynchronizationContext m_syncContext;

    [SecurityCritical]
    internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
      : base(action, flowExecutionContext, ref stackMark)
    {
      this.m_syncContext = context;
    }

    [SecuritySafeCritical]
    internal override sealed void Run(Task task, bool canInlineContinuationTask)
    {
      if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
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
        this.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), (object) this, ref Task.t_currentTask);
      }
    }

    [SecurityCritical]
    private static void PostAction(object state)
    {
      SynchronizationContextAwaitTaskContinuation taskContinuation = (SynchronizationContextAwaitTaskContinuation) state;
      if (TplEtwProvider.Log.TasksSetActivityIds && taskContinuation.m_continuationId != 0)
        taskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, (object) SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(taskContinuation.m_continuationId, taskContinuation.m_action));
      else
        taskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, (object) taskContinuation.m_action);
    }

    private static Action GetActionLogDelegate(int continuationId, Action action)
    {
      return (Action) (() =>
      {
        Guid oldActivityThatWillContinue;
        EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out oldActivityThatWillContinue);
        try
        {
          action();
        }
        finally
        {
          EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
        }
      });
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ContextCallback GetPostActionCallback()
    {
      ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
      if (contextCallback == null)
        SynchronizationContextAwaitTaskContinuation.s_postActionCallback = contextCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction);
      return contextCallback;
    }
  }
}
