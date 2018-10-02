// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerQueue
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  internal class TimerQueue
  {
    private static TimerQueue s_queue = new TimerQueue();
    [SecurityCritical]
    private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;
    private bool m_isAppDomainTimerScheduled;
    private int m_currentAppDomainTimerStartTicks;
    private uint m_currentAppDomainTimerDuration;
    private TimerQueueTimer m_timers;
    private volatile int m_pauseTicks;
    private static WaitCallback s_fireQueuedTimerCompletion;

    public static TimerQueue Instance
    {
      get
      {
        return TimerQueue.s_queue;
      }
    }

    private TimerQueue()
    {
    }

    private static int TickCount
    {
      [SecuritySafeCritical] get
      {
        if (!Environment.IsWindows8OrAbove)
          return Environment.TickCount;
        ulong UnbiasedTime;
        if (!Win32Native.QueryUnbiasedInterruptTime(out UnbiasedTime))
          throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
        return (int) (uint) (UnbiasedTime / 10000UL);
      }
    }

    [SecuritySafeCritical]
    private bool EnsureAppDomainTimerFiresBy(uint requestedDuration)
    {
      uint dueTime = Math.Min(requestedDuration, 268435455U);
      if (this.m_isAppDomainTimerScheduled)
      {
        uint num1 = (uint) (TimerQueue.TickCount - this.m_currentAppDomainTimerStartTicks);
        if (num1 >= this.m_currentAppDomainTimerDuration)
          return true;
        uint num2 = this.m_currentAppDomainTimerDuration - num1;
        if (dueTime >= num2)
          return true;
      }
      if (this.m_pauseTicks != 0)
        return true;
      if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
      {
        this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(dueTime);
        if (this.m_appDomainTimer.IsInvalid)
          return false;
        this.m_isAppDomainTimerScheduled = true;
        this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
        this.m_currentAppDomainTimerDuration = dueTime;
        return true;
      }
      if (!TimerQueue.ChangeAppDomainTimer(this.m_appDomainTimer, dueTime))
        return false;
      this.m_isAppDomainTimerScheduled = true;
      this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
      this.m_currentAppDomainTimerDuration = dueTime;
      return true;
    }

    [SecuritySafeCritical]
    internal static void AppDomainTimerCallback()
    {
      TimerQueue.Instance.FireNextTimers();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern TimerQueue.AppDomainTimerSafeHandle CreateAppDomainTimer(uint dueTime);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool ChangeAppDomainTimer(TimerQueue.AppDomainTimerSafeHandle handle, uint dueTime);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool DeleteAppDomainTimer(IntPtr handle);

    [SecurityCritical]
    internal void Pause()
    {
      lock (this)
      {
        if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
          return;
        this.m_appDomainTimer.Dispose();
        this.m_appDomainTimer = (TimerQueue.AppDomainTimerSafeHandle) null;
        this.m_isAppDomainTimerScheduled = false;
        this.m_pauseTicks = TimerQueue.TickCount;
      }
    }

    [SecurityCritical]
    internal void Resume()
    {
      lock (this)
      {
        try
        {
        }
        finally
        {
          int pauseTicks = this.m_pauseTicks;
          this.m_pauseTicks = 0;
          int tickCount = TimerQueue.TickCount;
          int num1 = tickCount - pauseTicks;
          bool flag = false;
          uint requestedDuration = uint.MaxValue;
          for (TimerQueueTimer timerQueueTimer = this.m_timers; timerQueueTimer != null; timerQueueTimer = timerQueueTimer.m_next)
          {
            uint num2 = timerQueueTimer.m_startTicks > pauseTicks ? (uint) (tickCount - timerQueueTimer.m_startTicks) : (uint) (pauseTicks - timerQueueTimer.m_startTicks);
            timerQueueTimer.m_dueTime = timerQueueTimer.m_dueTime > num2 ? timerQueueTimer.m_dueTime - num2 : 0U;
            timerQueueTimer.m_startTicks = tickCount;
            if (timerQueueTimer.m_dueTime < requestedDuration)
            {
              flag = true;
              requestedDuration = timerQueueTimer.m_dueTime;
            }
          }
          if (flag)
            this.EnsureAppDomainTimerFiresBy(requestedDuration);
        }
      }
    }

    private void FireNextTimers()
    {
      TimerQueueTimer timerQueueTimer = (TimerQueueTimer) null;
      lock (this)
      {
        try
        {
        }
        finally
        {
          this.m_isAppDomainTimerScheduled = false;
          bool flag = false;
          uint requestedDuration = uint.MaxValue;
          int tickCount = TimerQueue.TickCount;
          TimerQueueTimer timer = this.m_timers;
          while (timer != null)
          {
            uint num1 = (uint) (tickCount - timer.m_startTicks);
            if (num1 >= timer.m_dueTime)
            {
              TimerQueueTimer next = timer.m_next;
              if (timer.m_period != uint.MaxValue)
              {
                timer.m_startTicks = tickCount;
                timer.m_dueTime = timer.m_period;
                if (timer.m_dueTime < requestedDuration)
                {
                  flag = true;
                  requestedDuration = timer.m_dueTime;
                }
              }
              else
                this.DeleteTimer(timer);
              if (timerQueueTimer == null)
                timerQueueTimer = timer;
              else
                TimerQueue.QueueTimerCompletion(timer);
              timer = next;
            }
            else
            {
              uint num2 = timer.m_dueTime - num1;
              if (num2 < requestedDuration)
              {
                flag = true;
                requestedDuration = num2;
              }
              timer = timer.m_next;
            }
          }
          if (flag)
            this.EnsureAppDomainTimerFiresBy(requestedDuration);
        }
      }
      timerQueueTimer?.Fire();
    }

    [SecuritySafeCritical]
    private static void QueueTimerCompletion(TimerQueueTimer timer)
    {
      WaitCallback callBack = TimerQueue.s_fireQueuedTimerCompletion;
      if (callBack == null)
        TimerQueue.s_fireQueuedTimerCompletion = callBack = new WaitCallback(TimerQueue.FireQueuedTimerCompletion);
      ThreadPool.UnsafeQueueUserWorkItem(callBack, (object) timer);
    }

    private static void FireQueuedTimerCompletion(object state)
    {
      ((TimerQueueTimer) state).Fire();
    }

    public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
    {
      if (timer.m_dueTime == uint.MaxValue)
      {
        timer.m_next = this.m_timers;
        timer.m_prev = (TimerQueueTimer) null;
        if (timer.m_next != null)
          timer.m_next.m_prev = timer;
        this.m_timers = timer;
      }
      timer.m_dueTime = dueTime;
      timer.m_period = period == 0U ? uint.MaxValue : period;
      timer.m_startTicks = TimerQueue.TickCount;
      return this.EnsureAppDomainTimerFiresBy(dueTime);
    }

    public void DeleteTimer(TimerQueueTimer timer)
    {
      if (timer.m_dueTime == uint.MaxValue)
        return;
      if (timer.m_next != null)
        timer.m_next.m_prev = timer.m_prev;
      if (timer.m_prev != null)
        timer.m_prev.m_next = timer.m_next;
      if (this.m_timers == timer)
        this.m_timers = timer.m_next;
      timer.m_dueTime = uint.MaxValue;
      timer.m_period = uint.MaxValue;
      timer.m_startTicks = 0;
      timer.m_prev = (TimerQueueTimer) null;
      timer.m_next = (TimerQueueTimer) null;
    }

    [SecurityCritical]
    private class AppDomainTimerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      public AppDomainTimerSafeHandle()
        : base(true)
      {
      }

      [SecurityCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      protected override bool ReleaseHandle()
      {
        return TimerQueue.DeleteAppDomainTimer(this.handle);
      }
    }
  }
}
