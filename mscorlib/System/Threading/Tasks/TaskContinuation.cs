// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading.Tasks
{
  internal abstract class TaskContinuation
  {
    internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

    [SecuritySafeCritical]
    protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
    {
      if (needsProtection)
      {
        if (!task.MarkStarted())
          return;
      }
      else
        task.m_stateFlags |= 65536;
      try
      {
        if (task.m_taskScheduler.TryRunInline(task, false))
          return;
        task.m_taskScheduler.InternalQueueTask(task);
      }
      catch (Exception ex)
      {
        if (ex is ThreadAbortException && (task.m_stateFlags & 134217728) != 0)
          return;
        TaskSchedulerException schedulerException = new TaskSchedulerException(ex);
        task.AddException((object) schedulerException);
        task.Finish(false);
      }
    }

    internal abstract Delegate[] GetDelegateContinuationsForDebugger();
  }
}
