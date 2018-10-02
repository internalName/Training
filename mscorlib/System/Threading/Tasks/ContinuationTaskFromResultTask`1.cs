// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ContinuationTaskFromResultTask`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal sealed class ContinuationTaskFromResultTask<TAntecedentResult> : Task
  {
    private Task<TAntecedentResult> m_antecedent;

    public ContinuationTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
      : base(action, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, internalOptions, (TaskScheduler) null)
    {
      this.m_antecedent = antecedent;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal override void InnerInvoke()
    {
      Task<TAntecedentResult> antecedent = this.m_antecedent;
      this.m_antecedent = (Task<TAntecedentResult>) null;
      antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
      Action<Task<TAntecedentResult>> action1 = this.m_action as Action<Task<TAntecedentResult>>;
      if (action1 != null)
      {
        action1(antecedent);
      }
      else
      {
        Action<Task<TAntecedentResult>, object> action2 = this.m_action as Action<Task<TAntecedentResult>, object>;
        if (action2 == null)
          return;
        action2(antecedent, this.m_stateObject);
      }
    }
  }
}
