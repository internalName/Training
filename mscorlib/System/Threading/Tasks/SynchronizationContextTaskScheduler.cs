// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.SynchronizationContextTaskScheduler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
  {
    private static SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextTaskScheduler.PostCallback);
    private SynchronizationContext m_synchronizationContext;

    internal SynchronizationContextTaskScheduler()
    {
      SynchronizationContext current = SynchronizationContext.Current;
      if (current == null)
        throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_FromCurrentSynchronizationContext_NoCurrent"));
      this.m_synchronizationContext = current;
    }

    [SecurityCritical]
    protected internal override void QueueTask(Task task)
    {
      this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, (object) task);
    }

    [SecurityCritical]
    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
      if (SynchronizationContext.Current == this.m_synchronizationContext)
        return this.TryExecuteTask(task);
      return false;
    }

    [SecurityCritical]
    protected override IEnumerable<Task> GetScheduledTasks()
    {
      return (IEnumerable<Task>) null;
    }

    public override int MaximumConcurrencyLevel
    {
      get
      {
        return 1;
      }
    }

    private static void PostCallback(object obj)
    {
      ((Task) obj).ExecuteEntry(true);
    }
  }
}
