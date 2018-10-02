// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskSchedulerAwaitTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
  {
    private readonly TaskScheduler m_scheduler;

    [SecurityCritical]
    internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
      : base(action, flowExecutionContext, ref stackMark)
    {
      this.m_scheduler = scheduler;
    }

    internal override sealed void Run(Task ignored, bool canInlineContinuationTask)
    {
      if (this.m_scheduler == TaskScheduler.Default)
      {
        base.Run(ignored, canInlineContinuationTask);
      }
      else
      {
        bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
        Task task = this.CreateTask((Action<object>) (state =>
        {
          try
          {
            ((Action) state)();
          }
          catch (Exception ex)
          {
            AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
          }
        }), (object) this.m_action, this.m_scheduler);
        if (flag)
        {
          TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
        }
        else
        {
          try
          {
            task.ScheduleAndStart(false);
          }
          catch (TaskSchedulerException ex)
          {
          }
        }
      }
    }
  }
}
