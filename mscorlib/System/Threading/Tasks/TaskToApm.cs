// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskToApm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;

namespace System.Threading.Tasks
{
  internal static class TaskToApm
  {
    public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
    {
      IAsyncResult asyncResult;
      if (task.IsCompleted)
      {
        asyncResult = (IAsyncResult) new TaskToApm.TaskWrapperAsyncResult(task, state, true);
        if (callback != null)
          callback(asyncResult);
      }
      else
      {
        asyncResult = task.AsyncState == state ? (IAsyncResult) task : (IAsyncResult) new TaskToApm.TaskWrapperAsyncResult(task, state, false);
        if (callback != null)
          TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
      }
      return asyncResult;
    }

    public static void End(IAsyncResult asyncResult)
    {
      TaskToApm.TaskWrapperAsyncResult wrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
      Task task = wrapperAsyncResult == null ? asyncResult as Task : wrapperAsyncResult.Task;
      if (task == null)
        __Error.WrongAsyncResult();
      task.GetAwaiter().GetResult();
    }

    public static TResult End<TResult>(IAsyncResult asyncResult)
    {
      TaskToApm.TaskWrapperAsyncResult wrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
      Task<TResult> task = wrapperAsyncResult == null ? asyncResult as Task<TResult> : wrapperAsyncResult.Task as Task<TResult>;
      if (task == null)
        __Error.WrongAsyncResult();
      return task.GetAwaiter().GetResult();
    }

    private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
    {
      antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted((Action) (() => callback(asyncResult)));
    }

    private sealed class TaskWrapperAsyncResult : IAsyncResult
    {
      internal readonly Task Task;
      private readonly object m_state;
      private readonly bool m_completedSynchronously;

      internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
      {
        this.Task = task;
        this.m_state = state;
        this.m_completedSynchronously = completedSynchronously;
      }

      object IAsyncResult.AsyncState
      {
        get
        {
          return this.m_state;
        }
      }

      bool IAsyncResult.CompletedSynchronously
      {
        get
        {
          return this.m_completedSynchronously;
        }
      }

      bool IAsyncResult.IsCompleted
      {
        get
        {
          return this.Task.IsCompleted;
        }
      }

      WaitHandle IAsyncResult.AsyncWaitHandle
      {
        get
        {
          return ((IAsyncResult) this.Task).AsyncWaitHandle;
        }
      }
    }
  }
}
