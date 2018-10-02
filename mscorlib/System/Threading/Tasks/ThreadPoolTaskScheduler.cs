// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ThreadPoolTaskScheduler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class ThreadPoolTaskScheduler : TaskScheduler
  {
    private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);

    internal ThreadPoolTaskScheduler()
    {
      int id = this.Id;
    }

    private static void LongRunningThreadWork(object obj)
    {
      (obj as Task).ExecuteEntry(false);
    }

    [SecurityCritical]
    protected internal override void QueueTask(Task task)
    {
      if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
      {
        new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
        {
          IsBackground = true
        }.Start((object) task);
      }
      else
      {
        bool forceGlobal = (uint) (task.Options & TaskCreationOptions.PreferFairness) > 0U;
        ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) task, forceGlobal);
      }
    }

    [SecurityCritical]
    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
      if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem((IThreadPoolWorkItem) task))
        return false;
      try
      {
        return task.ExecuteEntry(false);
      }
      finally
      {
        if (taskWasPreviouslyQueued)
          this.NotifyWorkItemProgress();
      }
    }

    [SecurityCritical]
    protected internal override bool TryDequeue(Task task)
    {
      return ThreadPool.TryPopCustomWorkItem((IThreadPoolWorkItem) task);
    }

    [SecurityCritical]
    protected override IEnumerable<Task> GetScheduledTasks()
    {
      return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
    }

    private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
    {
      foreach (IThreadPoolWorkItem tpwItem in tpwItems)
      {
        if (tpwItem is Task)
          yield return (Task) tpwItem;
      }
    }

    internal override void NotifyWorkItemProgress()
    {
      ThreadPool.NotifyWorkItemProgress();
    }

    internal override bool RequiresAtomicStartTransition
    {
      get
      {
        return false;
      }
    }
  }
}
