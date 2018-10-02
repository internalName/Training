// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncMethodBuilderCore
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  internal struct AsyncMethodBuilderCore
  {
    internal IAsyncStateMachine m_stateMachine;
    internal Action m_defaultContextAction;

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
      if (stateMachine == null)
        throw new ArgumentNullException(nameof (stateMachine));
      if (this.m_stateMachine != null)
        throw new InvalidOperationException(Environment.GetResourceString("AsyncMethodBuilder_InstanceNotInitialized"));
      this.m_stateMachine = stateMachine;
    }

    [SecuritySafeCritical]
    internal Action GetCompletionAction(Task taskForTracing, ref AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize)
    {
      Debugger.NotifyOfCrossThreadDependency();
      ExecutionContext context = ExecutionContext.FastCapture();
      AsyncMethodBuilderCore.MoveNextRunner moveNextRunner;
      Action continuation;
      if (context != null && context.IsPreAllocatedDefault)
      {
        Action defaultContextAction = this.m_defaultContextAction;
        if (defaultContextAction != null)
          return defaultContextAction;
        moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(context, this.m_stateMachine);
        continuation = new Action(moveNextRunner.Run);
        this.m_defaultContextAction = taskForTracing == null ? continuation : (continuation = this.OutputAsyncCausalityEvents(taskForTracing, continuation));
      }
      else
      {
        moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(context, this.m_stateMachine);
        continuation = new Action(moveNextRunner.Run);
        if (taskForTracing != null)
          continuation = this.OutputAsyncCausalityEvents(taskForTracing, continuation);
      }
      if (this.m_stateMachine == null)
        runnerToInitialize = moveNextRunner;
      return continuation;
    }

    private Action OutputAsyncCausalityEvents(Task innerTask, Action continuation)
    {
      return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, (Action) (() =>
      {
        AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, innerTask.Id, CausalitySynchronousWork.Execution);
        continuation();
        AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
      }), innerTask);
    }

    internal void PostBoxInitialization(IAsyncStateMachine stateMachine, AsyncMethodBuilderCore.MoveNextRunner runner, Task builtTask)
    {
      if (builtTask != null)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, builtTask.Id, "Async: " + stateMachine.GetType().Name, 0UL);
        if (Task.s_asyncDebuggingEnabled)
          Task.AddToActiveTasks(builtTask);
      }
      this.m_stateMachine = stateMachine;
      this.m_stateMachine.SetStateMachine(this.m_stateMachine);
      runner.m_stateMachine = this.m_stateMachine;
    }

    internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
    {
      ExceptionDispatchInfo exceptionDispatchInfo1 = ExceptionDispatchInfo.Capture(exception);
      if (targetContext != null)
      {
        try
        {
          SynchronizationContext synchronizationContext = targetContext;
          ExceptionDispatchInfo exceptionDispatchInfo2 = exceptionDispatchInfo1;
          synchronizationContext.Post((SendOrPostCallback) (state => ((ExceptionDispatchInfo) state).Throw()), (object) exceptionDispatchInfo2);
          return;
        }
        catch (Exception ex)
        {
          exceptionDispatchInfo1 = ExceptionDispatchInfo.Capture((Exception) new AggregateException(new Exception[2]
          {
            exception,
            ex
          }));
        }
      }
      if (WindowsRuntimeMarshal.ReportUnhandledError(exceptionDispatchInfo1.SourceException))
        return;
      ThreadPool.QueueUserWorkItem((WaitCallback) (state => ((ExceptionDispatchInfo) state).Throw()), (object) exceptionDispatchInfo1);
    }

    internal static Action CreateContinuationWrapper(Action continuation, Action invokeAction, Task innerTask = null)
    {
      return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
    }

    internal static Action TryGetStateMachineForDebugger(Action action)
    {
      object target = action.Target;
      AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = target as AsyncMethodBuilderCore.MoveNextRunner;
      if (moveNextRunner != null)
        return new Action(moveNextRunner.m_stateMachine.MoveNext);
      AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = target as AsyncMethodBuilderCore.ContinuationWrapper;
      if (continuationWrapper != null)
        return AsyncMethodBuilderCore.TryGetStateMachineForDebugger(continuationWrapper.m_continuation);
      return action;
    }

    internal static Task TryGetContinuationTask(Action action)
    {
      if (action != null)
      {
        AsyncMethodBuilderCore.ContinuationWrapper target = action.Target as AsyncMethodBuilderCore.ContinuationWrapper;
        if (target != null)
          return target.m_innerTask;
      }
      return (Task) null;
    }

    internal sealed class MoveNextRunner
    {
      private readonly ExecutionContext m_context;
      internal IAsyncStateMachine m_stateMachine;
      [SecurityCritical]
      private static ContextCallback s_invokeMoveNext;

      [SecurityCritical]
      internal MoveNextRunner(ExecutionContext context, IAsyncStateMachine stateMachine)
      {
        this.m_context = context;
        this.m_stateMachine = stateMachine;
      }

      [SecuritySafeCritical]
      internal void Run()
      {
        if (this.m_context != null)
        {
          try
          {
            ContextCallback callback = AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext;
            if (callback == null)
              AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext = callback = new ContextCallback(AsyncMethodBuilderCore.MoveNextRunner.InvokeMoveNext);
            ExecutionContext.Run(this.m_context, callback, (object) this.m_stateMachine, true);
          }
          finally
          {
            this.m_context.Dispose();
          }
        }
        else
          this.m_stateMachine.MoveNext();
      }

      [SecurityCritical]
      private static void InvokeMoveNext(object stateMachine)
      {
        ((IAsyncStateMachine) stateMachine).MoveNext();
      }
    }

    private class ContinuationWrapper
    {
      internal readonly Action m_continuation;
      private readonly Action m_invokeAction;
      internal readonly Task m_innerTask;

      internal ContinuationWrapper(Action continuation, Action invokeAction, Task innerTask)
      {
        if (innerTask == null)
          innerTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
        this.m_continuation = continuation;
        this.m_innerTask = innerTask;
        this.m_invokeAction = invokeAction;
      }

      internal void Invoke()
      {
        this.m_invokeAction();
      }
    }
  }
}
