// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventDispatcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal class EventDispatcher
  {
    internal readonly EventListener m_Listener;
    internal bool[] m_EventEnabled;
    internal bool m_activityFilteringEnabled;
    internal EventDispatcher m_Next;

    internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
    {
      this.m_Next = next;
      this.m_EventEnabled = eventEnabled;
      this.m_Listener = listener;
    }
  }
}
