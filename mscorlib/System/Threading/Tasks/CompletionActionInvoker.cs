// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.CompletionActionInvoker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
  {
    private readonly ITaskCompletionAction m_action;
    private readonly Task m_completingTask;

    internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
    {
      this.m_action = action;
      this.m_completingTask = completingTask;
    }

    [SecurityCritical]
    public void ExecuteWorkItem()
    {
      this.m_action.Invoke(this.m_completingTask);
    }

    [SecurityCritical]
    public void MarkAborted(ThreadAbortException tae)
    {
    }
  }
}
