// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerQueueTimer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Diagnostics.Tracing;
using System.Security;

namespace System.Threading
{
  internal sealed class TimerQueueTimer
  {
    internal TimerQueueTimer m_next;
    internal TimerQueueTimer m_prev;
    internal int m_startTicks;
    internal uint m_dueTime;
    internal uint m_period;
    private readonly TimerCallback m_timerCallback;
    private readonly object m_state;
    private readonly ExecutionContext m_executionContext;
    private int m_callbacksRunning;
    private volatile bool m_canceled;
    private volatile WaitHandle m_notifyWhenNoCallbacksRunning;
    [SecurityCritical]
    private static ContextCallback s_callCallbackInContext;

    [SecurityCritical]
    internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
    {
      this.m_timerCallback = timerCallback;
      this.m_state = state;
      this.m_dueTime = uint.MaxValue;
      this.m_period = uint.MaxValue;
      if (!ExecutionContext.IsFlowSuppressed())
        this.m_executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
      if (dueTime == uint.MaxValue)
        return;
      this.Change(dueTime, period);
    }

    internal bool Change(uint dueTime, uint period)
    {
      bool flag;
      lock (TimerQueue.Instance)
      {
        if (this.m_canceled)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
        try
        {
        }
        finally
        {
          this.m_period = period;
          if (dueTime == uint.MaxValue)
          {
            TimerQueue.Instance.DeleteTimer(this);
            flag = true;
          }
          else
          {
            if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
              FrameworkEventSource.Log.ThreadTransferSendObj((object) this, 1, string.Empty, true);
            flag = TimerQueue.Instance.UpdateTimer(this, dueTime, period);
          }
        }
      }
      return flag;
    }

    public void Close()
    {
      lock (TimerQueue.Instance)
      {
        try
        {
        }
        finally
        {
          if (!this.m_canceled)
          {
            this.m_canceled = true;
            TimerQueue.Instance.DeleteTimer(this);
          }
        }
      }
    }

    public bool Close(WaitHandle toSignal)
    {
      bool flag1 = false;
      bool flag2;
      lock (TimerQueue.Instance)
      {
        try
        {
        }
        finally
        {
          if (this.m_canceled)
          {
            flag2 = false;
          }
          else
          {
            this.m_canceled = true;
            this.m_notifyWhenNoCallbacksRunning = toSignal;
            TimerQueue.Instance.DeleteTimer(this);
            if (this.m_callbacksRunning == 0)
              flag1 = true;
            flag2 = true;
          }
        }
      }
      if (flag1)
        this.SignalNoCallbacksRunning();
      return flag2;
    }

    internal void Fire()
    {
      bool flag1 = false;
      lock (TimerQueue.Instance)
      {
        try
        {
        }
        finally
        {
          flag1 = this.m_canceled;
          if (!flag1)
            ++this.m_callbacksRunning;
        }
      }
      if (flag1)
        return;
      this.CallCallback();
      bool flag2 = false;
      lock (TimerQueue.Instance)
      {
        try
        {
        }
        finally
        {
          --this.m_callbacksRunning;
          if (this.m_canceled && this.m_callbacksRunning == 0 && this.m_notifyWhenNoCallbacksRunning != null)
            flag2 = true;
        }
      }
      if (!flag2)
        return;
      this.SignalNoCallbacksRunning();
    }

    [SecuritySafeCritical]
    internal void SignalNoCallbacksRunning()
    {
      Win32Native.SetEvent(this.m_notifyWhenNoCallbacksRunning.SafeWaitHandle);
    }

    [SecuritySafeCritical]
    internal void CallCallback()
    {
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferReceiveObj((object) this, 1, string.Empty);
      if (this.m_executionContext == null)
      {
        this.m_timerCallback(this.m_state);
      }
      else
      {
        using (ExecutionContext executionContext = this.m_executionContext.IsPreAllocatedDefault ? this.m_executionContext : this.m_executionContext.CreateCopy())
        {
          ContextCallback callback = TimerQueueTimer.s_callCallbackInContext;
          if (callback == null)
            TimerQueueTimer.s_callCallbackInContext = callback = new ContextCallback(TimerQueueTimer.CallCallbackInContext);
          ExecutionContext.Run(executionContext, callback, (object) this, true);
        }
      }
    }

    [SecurityCritical]
    private static void CallCallbackInContext(object state)
    {
      TimerQueueTimer timerQueueTimer = (TimerQueueTimer) state;
      timerQueueTimer.m_timerCallback(timerQueueTimer.m_state);
    }
  }
}
