// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ActivityFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Tracing
{
  internal sealed class ActivityFilter : IDisposable
  {
    private ConcurrentDictionary<Guid, int> m_activeActivities;
    private ConcurrentDictionary<Guid, Tuple<Guid, int>> m_rootActiveActivities;
    private Guid m_providerGuid;
    private int m_eventId;
    private int m_samplingFreq;
    private int m_curSampleCount;
    private int m_perEventSourceSessionId;
    private const int MaxActivityTrackCount = 100000;
    private ActivityFilter m_next;
    private Action<Guid> m_myActivityDelegate;

    public static void DisableFilter(ref ActivityFilter filterList, EventSource source)
    {
      if (filterList == null)
        return;
      ActivityFilter activityFilter1 = filterList;
      ActivityFilter next = activityFilter1.m_next;
      while (next != null)
      {
        if (next.m_providerGuid == source.Guid)
        {
          if (next.m_eventId >= 0 && next.m_eventId < source.m_eventData.Length)
            --source.m_eventData[next.m_eventId].TriggersActivityTracking;
          activityFilter1.m_next = next.m_next;
          next.Dispose();
          next = activityFilter1.m_next;
        }
        else
        {
          activityFilter1 = next;
          next = activityFilter1.m_next;
        }
      }
      if (filterList.m_providerGuid == source.Guid)
      {
        if (filterList.m_eventId >= 0 && filterList.m_eventId < source.m_eventData.Length)
          --source.m_eventData[filterList.m_eventId].TriggersActivityTracking;
        ActivityFilter activityFilter2 = filterList;
        filterList = activityFilter2.m_next;
        activityFilter2.Dispose();
      }
      if (filterList == null)
        return;
      ActivityFilter.EnsureActivityCleanupDelegate(filterList);
    }

    public static void UpdateFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, string startEvents)
    {
      ActivityFilter.DisableFilter(ref filterList, source);
      if (string.IsNullOrEmpty(startEvents))
        return;
      string str1 = startEvents;
      char[] chArray = new char[1]{ ' ' };
      foreach (string str2 in str1.Split(chArray))
      {
        int result1 = 1;
        int result2 = -1;
        int length = str2.IndexOf(':');
        if (length < 0)
        {
          source.ReportOutOfBandMessage("ERROR: Invalid ActivitySamplingStartEvent specification: " + str2, false);
        }
        else
        {
          string s = str2.Substring(length + 1);
          if (!int.TryParse(s, out result1))
          {
            source.ReportOutOfBandMessage("ERROR: Invalid sampling frequency specification: " + s, false);
          }
          else
          {
            string str3 = str2.Substring(0, length);
            if (!int.TryParse(str3, out result2))
            {
              result2 = -1;
              for (int index = 0; index < source.m_eventData.Length; ++index)
              {
                EventSource.EventMetadata[] eventData = source.m_eventData;
                if (eventData[index].Name != null && eventData[index].Name.Length == str3.Length && string.Compare(eventData[index].Name, str3, StringComparison.OrdinalIgnoreCase) == 0)
                {
                  result2 = eventData[index].Descriptor.EventId;
                  break;
                }
              }
            }
            if (result2 < 0 || result2 >= source.m_eventData.Length)
              source.ReportOutOfBandMessage("ERROR: Invalid eventId specification: " + str3, false);
            else
              ActivityFilter.EnableFilter(ref filterList, source, perEventSourceSessionId, result2, result1);
          }
        }
      }
    }

    public static ActivityFilter GetFilter(ActivityFilter filterList, EventSource source)
    {
      for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
      {
        if (activityFilter.m_providerGuid == source.Guid && activityFilter.m_samplingFreq != -1)
          return activityFilter;
      }
      return (ActivityFilter) null;
    }

    [SecurityCritical]
    public static unsafe bool PassesActivityFilter(ActivityFilter filterList, Guid* childActivityID, bool triggeringEvent, EventSource source, int eventId)
    {
      bool flag = false;
      if (triggeringEvent)
      {
        for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
        {
          if (eventId == activityFilter.m_eventId && source.Guid == activityFilter.m_providerGuid)
          {
            int curSampleCount;
            int num1;
            do
            {
              curSampleCount = activityFilter.m_curSampleCount;
              num1 = curSampleCount > 1 ? curSampleCount - 1 : activityFilter.m_samplingFreq;
            }
            while (Interlocked.CompareExchange(ref activityFilter.m_curSampleCount, num1, curSampleCount) != curSampleCount);
            if (curSampleCount <= 1)
            {
              Guid threadActivityId = EventSource.InternalCurrentThreadActivityId;
              Tuple<Guid, int> tuple;
              if (!activityFilter.m_rootActiveActivities.TryGetValue(threadActivityId, out tuple))
              {
                flag = true;
                activityFilter.m_activeActivities[threadActivityId] = Environment.TickCount;
                activityFilter.m_rootActiveActivities[threadActivityId] = Tuple.Create<Guid, int>(source.Guid, eventId);
                break;
              }
              break;
            }
            Guid threadActivityId1 = EventSource.InternalCurrentThreadActivityId;
            Tuple<Guid, int> tuple1;
            if (activityFilter.m_rootActiveActivities.TryGetValue(threadActivityId1, out tuple1) && tuple1.Item1 == source.Guid && tuple1.Item2 == eventId)
            {
              int num2;
              activityFilter.m_activeActivities.TryRemove(threadActivityId1, out num2);
              break;
            }
            break;
          }
        }
      }
      ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
      if (activeActivities != null)
      {
        if (!flag)
          flag = !activeActivities.IsEmpty && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
        if (flag && (IntPtr) childActivityID != IntPtr.Zero && source.m_eventData[eventId].Descriptor.Opcode == (byte) 9)
          ActivityFilter.FlowActivityIfNeeded(filterList, (Guid*) null, childActivityID);
      }
      return flag;
    }

    [SecuritySafeCritical]
    public static bool IsCurrentActivityActive(ActivityFilter filterList)
    {
      ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
      return activeActivities != null && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
    }

    [SecurityCritical]
    public static unsafe void FlowActivityIfNeeded(ActivityFilter filterList, Guid* currentActivityId, Guid* childActivityID)
    {
      ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
      if ((IntPtr) currentActivityId != IntPtr.Zero && !activeActivities.ContainsKey(*currentActivityId))
        return;
      if (activeActivities.Count > 100000)
      {
        ActivityFilter.TrimActiveActivityStore(activeActivities);
        activeActivities[EventSource.InternalCurrentThreadActivityId] = Environment.TickCount;
      }
      activeActivities[*childActivityID] = Environment.TickCount;
    }

    public static void UpdateKwdTriggers(ActivityFilter activityFilter, Guid sourceGuid, EventSource source, EventKeywords sessKeywords)
    {
      for (ActivityFilter activityFilter1 = activityFilter; activityFilter1 != null; activityFilter1 = activityFilter1.m_next)
      {
        if (sourceGuid == activityFilter1.m_providerGuid && (source.m_eventData[activityFilter1.m_eventId].TriggersActivityTracking > (byte) 0 || source.m_eventData[activityFilter1.m_eventId].Descriptor.Opcode == (byte) 9))
          source.m_keywordTriggers |= (long) ((EventKeywords) source.m_eventData[activityFilter1.m_eventId].Descriptor.Keywords & sessKeywords);
      }
    }

    public IEnumerable<Tuple<int, int>> GetFilterAsTuple(Guid sourceGuid)
    {
      ActivityFilter af;
      for (af = this; af != null; af = af.m_next)
      {
        if (af.m_providerGuid == sourceGuid)
          yield return Tuple.Create<int, int>(af.m_eventId, af.m_samplingFreq);
      }
      af = (ActivityFilter) null;
    }

    public void Dispose()
    {
      if (this.m_myActivityDelegate == null)
        return;
      EventSource.s_activityDying -= this.m_myActivityDelegate;
      this.m_myActivityDelegate = (Action<Guid>) null;
    }

    private ActivityFilter(EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq, ActivityFilter existingFilter = null)
    {
      this.m_providerGuid = source.Guid;
      this.m_perEventSourceSessionId = perEventSourceSessionId;
      this.m_eventId = eventId;
      this.m_samplingFreq = samplingFreq;
      this.m_next = existingFilter;
      ConcurrentDictionary<Guid, int> activeActivities;
      if (existingFilter == null || (activeActivities = ActivityFilter.GetActiveActivities(existingFilter)) == null)
      {
        this.m_activeActivities = new ConcurrentDictionary<Guid, int>();
        this.m_rootActiveActivities = new ConcurrentDictionary<Guid, Tuple<Guid, int>>();
        this.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(this);
        EventSource.s_activityDying += this.m_myActivityDelegate;
      }
      else
      {
        this.m_activeActivities = activeActivities;
        this.m_rootActiveActivities = existingFilter.m_rootActiveActivities;
      }
    }

    private static void EnsureActivityCleanupDelegate(ActivityFilter filterList)
    {
      if (filterList == null)
        return;
      for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
      {
        if (activityFilter.m_myActivityDelegate != null)
          return;
      }
      filterList.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(filterList);
      EventSource.s_activityDying += filterList.m_myActivityDelegate;
    }

    private static Action<Guid> GetActivityDyingDelegate(ActivityFilter filterList)
    {
      return (Action<Guid>) (oldActivity =>
      {
        int num;
        filterList.m_activeActivities.TryRemove(oldActivity, out num);
        Tuple<Guid, int> tuple;
        filterList.m_rootActiveActivities.TryRemove(oldActivity, out tuple);
      });
    }

    private static bool EnableFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq)
    {
      filterList = new ActivityFilter(source, perEventSourceSessionId, eventId, samplingFreq, filterList);
      if (0 <= eventId && eventId < source.m_eventData.Length)
        ++source.m_eventData[eventId].TriggersActivityTracking;
      return true;
    }

    private static void TrimActiveActivityStore(ConcurrentDictionary<Guid, int> activities)
    {
      if (activities.Count <= 100000)
        return;
      KeyValuePair<Guid, int>[] array = activities.ToArray();
      int tickNow = Environment.TickCount;
      Array.Sort<KeyValuePair<Guid, int>>(array, (Comparison<KeyValuePair<Guid, int>>) ((x, y) => (int.MaxValue & tickNow - y.Value) - (int.MaxValue & tickNow - x.Value)));
      for (int index = 0; index < array.Length / 2; ++index)
      {
        int num;
        activities.TryRemove(array[index].Key, out num);
      }
    }

    private static ConcurrentDictionary<Guid, int> GetActiveActivities(ActivityFilter filterList)
    {
      for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
      {
        if (activityFilter.m_activeActivities != null)
          return activityFilter.m_activeActivities;
      }
      return (ConcurrentDictionary<Guid, int>) null;
    }
  }
}
