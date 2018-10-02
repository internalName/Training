// Decompiled with JetBrains decompiler
// Type: System.Threading._ThreadPoolWaitOrTimerCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal class _ThreadPoolWaitOrTimerCallback
  {
    [SecurityCritical]
    private static ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);
    [SecurityCritical]
    private static ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
    private WaitOrTimerCallback _waitOrTimerCallback;
    private ExecutionContext _executionContext;
    private object _state;

    [SecuritySafeCritical]
    static _ThreadPoolWaitOrTimerCallback()
    {
    }

    [SecurityCritical]
    internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool compressStack, ref StackCrawlMark stackMark)
    {
      this._waitOrTimerCallback = waitOrTimerCallback;
      this._state = state;
      if (!compressStack || ExecutionContext.IsFlowSuppressed())
        return;
      this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    [SecurityCritical]
    private static void WaitOrTimerCallback_Context_t(object state)
    {
      _ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
    }

    [SecurityCritical]
    private static void WaitOrTimerCallback_Context_f(object state)
    {
      _ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
    }

    private static void WaitOrTimerCallback_Context(object state, bool timedOut)
    {
      _ThreadPoolWaitOrTimerCallback waitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback) state;
      waitOrTimerCallback._waitOrTimerCallback(waitOrTimerCallback._state, timedOut);
    }

    [SecurityCritical]
    internal static void PerformWaitOrTimerCallback(object state, bool timedOut)
    {
      _ThreadPoolWaitOrTimerCallback waitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback) state;
      if (waitOrTimerCallback._executionContext == null)
      {
        waitOrTimerCallback._waitOrTimerCallback(waitOrTimerCallback._state, timedOut);
      }
      else
      {
        using (ExecutionContext copy = waitOrTimerCallback._executionContext.CreateCopy())
        {
          if (timedOut)
            ExecutionContext.Run(copy, _ThreadPoolWaitOrTimerCallback._ccbt, (object) waitOrTimerCallback, true);
          else
            ExecutionContext.Run(copy, _ThreadPoolWaitOrTimerCallback._ccbf, (object) waitOrTimerCallback, true);
        }
      }
    }
  }
}
