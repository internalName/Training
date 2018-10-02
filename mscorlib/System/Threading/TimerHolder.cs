// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal sealed class TimerHolder
  {
    internal TimerQueueTimer m_timer;

    public TimerHolder(TimerQueueTimer timer)
    {
      this.m_timer = timer;
    }

    ~TimerHolder()
    {
      if (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload())
        return;
      this.m_timer.Close();
    }

    public void Close()
    {
      this.m_timer.Close();
      GC.SuppressFinalize((object) this);
    }

    public bool Close(WaitHandle notifyObject)
    {
      bool flag = this.m_timer.Close(notifyObject);
      GC.SuppressFinalize((object) this);
      return flag;
    }
  }
}
