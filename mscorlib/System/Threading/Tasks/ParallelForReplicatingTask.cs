// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelForReplicatingTask
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
  internal class ParallelForReplicatingTask : Task
  {
    private int m_replicationDownCount;

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal ParallelForReplicatingTask(ParallelOptions parallelOptions, Action action, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
      : base((Delegate) action, (object) null, Task.InternalCurrent, new CancellationToken(), creationOptions, internalOptions | InternalTaskOptions.SelfReplicating, (TaskScheduler) null)
    {
      this.m_replicationDownCount = parallelOptions.EffectiveMaxConcurrencyLevel;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal override bool ShouldReplicate()
    {
      if (this.m_replicationDownCount == -1)
        return true;
      if (this.m_replicationDownCount <= 0)
        return false;
      --this.m_replicationDownCount;
      return true;
    }

    internal override Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
    {
      return (Task) new ParallelForReplicaTask(taskReplicaDelegate, stateObject, parentTask, taskScheduler, creationOptionsForReplica, internalOptionsForReplica);
    }
  }
}
