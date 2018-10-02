// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EtwSession
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal class EtwSession
  {
    private static List<WeakReference<EtwSession>> s_etwSessions = new List<WeakReference<EtwSession>>();
    public readonly int m_etwSessionId;
    public ActivityFilter m_activityFilter;
    private const int s_thrSessionCount = 16;

    public static EtwSession GetEtwSession(int etwSessionId, bool bCreateIfNeeded = false)
    {
      if (etwSessionId < 0)
        return (EtwSession) null;
      foreach (WeakReference<EtwSession> etwSession in EtwSession.s_etwSessions)
      {
        EtwSession target;
        if (etwSession.TryGetTarget(out target) && target.m_etwSessionId == etwSessionId)
          return target;
      }
      if (!bCreateIfNeeded)
        return (EtwSession) null;
      if (EtwSession.s_etwSessions == null)
        EtwSession.s_etwSessions = new List<WeakReference<EtwSession>>();
      EtwSession target1 = new EtwSession(etwSessionId);
      EtwSession.s_etwSessions.Add(new WeakReference<EtwSession>(target1));
      if (EtwSession.s_etwSessions.Count > 16)
        EtwSession.TrimGlobalList();
      return target1;
    }

    public static void RemoveEtwSession(EtwSession etwSession)
    {
      if (EtwSession.s_etwSessions == null || etwSession == null)
        return;
      EtwSession.s_etwSessions.RemoveAll((Predicate<WeakReference<EtwSession>>) (wrEtwSession =>
      {
        EtwSession target;
        if (wrEtwSession.TryGetTarget(out target))
          return target.m_etwSessionId == etwSession.m_etwSessionId;
        return false;
      }));
      if (EtwSession.s_etwSessions.Count <= 16)
        return;
      EtwSession.TrimGlobalList();
    }

    private static void TrimGlobalList()
    {
      if (EtwSession.s_etwSessions == null)
        return;
      EtwSession target;
      EtwSession.s_etwSessions.RemoveAll((Predicate<WeakReference<EtwSession>>) (wrEtwSession => !wrEtwSession.TryGetTarget(out target)));
    }

    private EtwSession(int etwSessionId)
    {
      this.m_etwSessionId = etwSessionId;
    }
  }
}
