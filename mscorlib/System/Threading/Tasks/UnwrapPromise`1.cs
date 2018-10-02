// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.UnwrapPromise`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
  {
    private const byte STATE_WAITING_ON_OUTER_TASK = 0;
    private const byte STATE_WAITING_ON_INNER_TASK = 1;
    private const byte STATE_DONE = 2;
    private byte _state;
    private readonly bool _lookForOce;

    public UnwrapPromise(Task outerTask, bool lookForOce)
      : base((object) null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
    {
      this._lookForOce = lookForOce;
      this._state = (byte) 0;
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.Unwrap", 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) this);
      if (outerTask.IsCompleted)
        this.ProcessCompletedOuterTask(outerTask);
      else
        outerTask.AddCompletionAction((ITaskCompletionAction) this);
    }

    public void Invoke(Task completingTask)
    {
      StackGuard currentStackGuard = Task.CurrentStackGuard;
      if (currentStackGuard.TryBeginInliningScope())
      {
        try
        {
          this.InvokeCore(completingTask);
        }
        finally
        {
          currentStackGuard.EndInliningScope();
        }
      }
      else
        this.InvokeCoreAsync(completingTask);
    }

    private void InvokeCore(Task completingTask)
    {
      switch (this._state)
      {
        case 0:
          this.ProcessCompletedOuterTask(completingTask);
          break;
        case 1:
          this.TrySetFromTask(completingTask, false);
          this._state = (byte) 2;
          break;
      }
    }

    [SecuritySafeCritical]
    private void InvokeCoreAsync(Task completingTask)
    {
      ThreadPool.UnsafeQueueUserWorkItem((WaitCallback) (state =>
      {
        Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>) state;
        tuple.Item1.InvokeCore(tuple.Item2);
      }), (object) Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
    }

    private void ProcessCompletedOuterTask(Task task)
    {
      this._state = (byte) 1;
      switch (task.Status)
      {
        case TaskStatus.RanToCompletion:
          Task<Task<TResult>> task1 = task as Task<Task<TResult>>;
          this.ProcessInnerTask(task1 != null ? (Task) task1.Result : ((Task<Task>) task).Result);
          break;
        case TaskStatus.Canceled:
        case TaskStatus.Faulted:
          this.TrySetFromTask(task, this._lookForOce);
          break;
      }
    }

    private bool TrySetFromTask(Task task, bool lookForOce)
    {
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
      bool flag = false;
      switch (task.Status)
      {
        case TaskStatus.RanToCompletion:
          Task<TResult> task1 = task as Task<TResult>;
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          flag = this.TrySetResult(task1 != null ? task1.Result : default (TResult));
          break;
        case TaskStatus.Canceled:
          flag = this.TrySetCanceled(task.CancellationToken, (object) task.GetCancellationExceptionDispatchInfo());
          break;
        case TaskStatus.Faulted:
          ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
          ExceptionDispatchInfo exceptionDispatchInfo;
          OperationCanceledException sourceException;
          flag = !lookForOce || exceptionDispatchInfos.Count <= 0 || ((exceptionDispatchInfo = exceptionDispatchInfos[0]) == null || (sourceException = exceptionDispatchInfo.SourceException as OperationCanceledException) == null) ? this.TrySetException((object) exceptionDispatchInfos) : this.TrySetCanceled(sourceException.CancellationToken, (object) exceptionDispatchInfo);
          break;
      }
      return flag;
    }

    private void ProcessInnerTask(Task task)
    {
      if (task == null)
      {
        this.TrySetCanceled(new CancellationToken());
        this._state = (byte) 2;
      }
      else if (task.IsCompleted)
      {
        this.TrySetFromTask(task, false);
        this._state = (byte) 2;
      }
      else
        task.AddCompletionAction((ITaskCompletionAction) this);
    }
  }
}
