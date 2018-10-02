// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationCallbackInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal class CancellationCallbackInfo
  {
    internal readonly Action<object> Callback;
    internal readonly object StateForCallback;
    internal readonly SynchronizationContext TargetSyncContext;
    internal readonly ExecutionContext TargetExecutionContext;
    internal readonly CancellationTokenSource CancellationTokenSource;
    [SecurityCritical]
    private static ContextCallback s_executionContextCallback;

    internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
    {
      this.Callback = callback;
      this.StateForCallback = stateForCallback;
      this.TargetSyncContext = targetSyncContext;
      this.TargetExecutionContext = targetExecutionContext;
      this.CancellationTokenSource = cancellationTokenSource;
    }

    [SecuritySafeCritical]
    internal void ExecuteCallback()
    {
      if (this.TargetExecutionContext != null)
      {
        ContextCallback callback = CancellationCallbackInfo.s_executionContextCallback;
        if (callback == null)
          CancellationCallbackInfo.s_executionContextCallback = callback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback);
        ExecutionContext.Run(this.TargetExecutionContext, callback, (object) this);
      }
      else
        CancellationCallbackInfo.ExecutionContextCallback((object) this);
    }

    [SecurityCritical]
    private static void ExecutionContextCallback(object obj)
    {
      CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
      cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
    }
  }
}
