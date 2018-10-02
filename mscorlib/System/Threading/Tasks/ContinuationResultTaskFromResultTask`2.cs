// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ContinuationResultTaskFromResultTask`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal sealed class ContinuationResultTaskFromResultTask<TAntecedentResult, TResult> : Task<TResult>
  {
    private Task<TAntecedentResult> m_antecedent;

    public ContinuationResultTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
      : base(function, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, internalOptions, (TaskScheduler) null)
    {
      this.m_antecedent = antecedent;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal override void InnerInvoke()
    {
      Task<TAntecedentResult> antecedent = this.m_antecedent;
      this.m_antecedent = (Task<TAntecedentResult>) null;
      antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
      Func<Task<TAntecedentResult>, TResult> action1 = this.m_action as Func<Task<TAntecedentResult>, TResult>;
      if (action1 != null)
      {
        this.m_result = action1(antecedent);
      }
      else
      {
        Func<Task<TAntecedentResult>, object, TResult> action2 = this.m_action as Func<Task<TAntecedentResult>, object, TResult>;
        if (action2 == null)
          return;
        this.m_result = action2(antecedent, this.m_stateObject);
      }
    }
  }
}
