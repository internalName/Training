// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.SessionMask
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal struct SessionMask
  {
    private uint m_mask;
    internal const int SHIFT_SESSION_TO_KEYWORD = 44;
    internal const uint MASK = 15;
    internal const uint MAX = 4;

    public SessionMask(SessionMask m)
    {
      this.m_mask = m.m_mask;
    }

    public SessionMask(uint mask = 0)
    {
      this.m_mask = mask & 15U;
    }

    public bool IsEqualOrSupersetOf(SessionMask m)
    {
      return ((int) this.m_mask | (int) m.m_mask) == (int) this.m_mask;
    }

    public static SessionMask All
    {
      get
      {
        return new SessionMask(15U);
      }
    }

    public static SessionMask FromId(int perEventSourceSessionId)
    {
      return new SessionMask((uint) (1 << perEventSourceSessionId));
    }

    public ulong ToEventKeywords()
    {
      return (ulong) this.m_mask << 44;
    }

    public static SessionMask FromEventKeywords(ulong m)
    {
      return new SessionMask((uint) (m >> 44));
    }

    public bool this[int perEventSourceSessionId]
    {
      get
      {
        return ((ulong) this.m_mask & (ulong) (1 << perEventSourceSessionId)) > 0UL;
      }
      set
      {
        if (value)
          this.m_mask |= (uint) (1 << perEventSourceSessionId);
        else
          this.m_mask &= (uint) ~(1 << perEventSourceSessionId);
      }
    }

    public static SessionMask operator |(SessionMask m1, SessionMask m2)
    {
      return new SessionMask(m1.m_mask | m2.m_mask);
    }

    public static SessionMask operator &(SessionMask m1, SessionMask m2)
    {
      return new SessionMask(m1.m_mask & m2.m_mask);
    }

    public static SessionMask operator ^(SessionMask m1, SessionMask m2)
    {
      return new SessionMask(m1.m_mask ^ m2.m_mask);
    }

    public static SessionMask operator ~(SessionMask m)
    {
      return new SessionMask((uint) (15 & ~(int) m.m_mask));
    }

    public static explicit operator ulong(SessionMask m)
    {
      return (ulong) m.m_mask;
    }

    public static explicit operator uint(SessionMask m)
    {
      return m.m_mask;
    }
  }
}
