// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.StandardTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class StandardTaskContinuation : TaskContinuation
  {
    internal readonly Task m_task;
    internal readonly TaskContinuationOptions m_options;
    private readonly TaskScheduler m_taskScheduler;

    internal StandardTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
    {
      this.m_task = task;
      this.m_options = options;
      this.m_taskScheduler = scheduler;
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.m_task.Id, "Task.ContinueWith: " + ((Delegate) task.m_action).Method.Name, 0UL);
      if (!Task.s_asyncDebuggingEnabled)
        return;
      Task.AddToActiveTasks(this.m_task);
    }

    internal override void Run(Task completedTask, bool bCanInlineContinuationTask)
    {
      TaskContinuationOptions options = this.m_options;
      bool flag = completedTask.IsRanToCompletion ? (options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None : (completedTask.IsCanceled ? (options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None : (options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None);
      Task task = this.m_task;
      if (flag)
      {
        if (!task.IsCanceled && AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, task.Id, CausalityRelation.AssignDelegate);
        task.m_taskScheduler = this.m_taskScheduler;
        if (bCanInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
        {
          TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
        }
        else
        {
          try
          {
            task.ScheduleAndStart(true);
          }
          catch (TaskSchedulerException ex)
          {
          }
        }
      }
      else
        task.InternalCancel(false);
    }

    internal override Delegate[] GetDelegateContinuationsForDebugger()
    {
      if (this.m_task.m_action == null)
        return this.m_task.GetDelegateContinuationsForDebugger();
      return new Delegate[1]
      {
        this.m_task.m_action as Delegate
      };
    }
  }
}
