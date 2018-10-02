// Decompiled with JetBrains decompiler
// Type: System.CurrentSystemTimeZone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
  [Serializable]
  internal class CurrentSystemTimeZone : TimeZone
  {
    private Hashtable m_CachedDaylightChanges = new Hashtable();
    private const long TicksPerMillisecond = 10000;
    private const long TicksPerSecond = 10000000;
    private const long TicksPerMinute = 600000000;
    private long m_ticksOffset;
    private string m_standardName;
    private string m_daylightName;
    private static object s_InternalSyncObject;

    [SecuritySafeCritical]
    internal CurrentSystemTimeZone()
    {
      this.m_ticksOffset = (long) CurrentSystemTimeZone.nativeGetTimeZoneMinuteOffset() * 600000000L;
      this.m_standardName = (string) null;
      this.m_daylightName = (string) null;
    }

    public override string StandardName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_standardName == null)
          this.m_standardName = CurrentSystemTimeZone.nativeGetStandardName();
        return this.m_standardName;
      }
    }

    public override string DaylightName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_daylightName == null)
        {
          this.m_daylightName = CurrentSystemTimeZone.nativeGetDaylightName();
          if (this.m_daylightName == null)
            this.m_daylightName = this.StandardName;
        }
        return this.m_daylightName;
      }
    }

    internal long GetUtcOffsetFromUniversalTime(DateTime time, ref bool isAmbiguousLocalDst)
    {
      TimeSpan timeSpan = new TimeSpan(this.m_ticksOffset);
      DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
      isAmbiguousLocalDst = false;
      if (daylightChanges != null)
      {
        TimeSpan delta = daylightChanges.Delta;
        if (delta.Ticks != 0L)
        {
          DateTime dateTime1 = daylightChanges.Start - timeSpan;
          DateTime dateTime2 = daylightChanges.End - timeSpan - daylightChanges.Delta;
          delta = daylightChanges.Delta;
          DateTime dateTime3;
          DateTime dateTime4;
          if (delta.Ticks > 0L)
          {
            dateTime3 = dateTime2 - daylightChanges.Delta;
            dateTime4 = dateTime2;
          }
          else
          {
            dateTime3 = dateTime1;
            dateTime4 = dateTime1 - daylightChanges.Delta;
          }
          if (!(dateTime1 > dateTime2) ? time >= dateTime1 && time < dateTime2 : time < dateTime2 || time >= dateTime1)
          {
            timeSpan += daylightChanges.Delta;
            if (time >= dateTime3 && time < dateTime4)
              isAmbiguousLocalDst = true;
          }
          return timeSpan.Ticks;
        }
      }
      return timeSpan.Ticks;
    }

    public override DateTime ToLocalTime(DateTime time)
    {
      if (time.Kind == DateTimeKind.Local)
        return time;
      bool isAmbiguousLocalDst = false;
      long fromUniversalTime = this.GetUtcOffsetFromUniversalTime(time, ref isAmbiguousLocalDst);
      long ticks = time.Ticks + fromUniversalTime;
      if (ticks > 3155378975999999999L)
        return new DateTime(3155378975999999999L, DateTimeKind.Local);
      if (ticks < 0L)
        return new DateTime(0L, DateTimeKind.Local);
      return new DateTime(ticks, DateTimeKind.Local, isAmbiguousLocalDst);
    }

    private static object InternalSyncObject
    {
      get
      {
        if (CurrentSystemTimeZone.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref CurrentSystemTimeZone.s_InternalSyncObject, obj, (object) null);
        }
        return CurrentSystemTimeZone.s_InternalSyncObject;
      }
    }

    [SecuritySafeCritical]
    public override DaylightTime GetDaylightChanges(int year)
    {
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 9999));
      object key = (object) year;
      if (!this.m_CachedDaylightChanges.Contains(key))
      {
        lock (CurrentSystemTimeZone.InternalSyncObject)
        {
          if (!this.m_CachedDaylightChanges.Contains(key))
          {
            short[] daylightChanges = CurrentSystemTimeZone.nativeGetDaylightChanges(year);
            if (daylightChanges == null)
            {
              this.m_CachedDaylightChanges.Add(key, (object) new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero));
            }
            else
            {
              DaylightTime daylightTime = new DaylightTime(CurrentSystemTimeZone.GetDayOfWeek(year, (uint) daylightChanges[0] > 0U, (int) daylightChanges[1], (int) daylightChanges[2], (int) daylightChanges[3], (int) daylightChanges[4], (int) daylightChanges[5], (int) daylightChanges[6], (int) daylightChanges[7]), CurrentSystemTimeZone.GetDayOfWeek(year, (uint) daylightChanges[8] > 0U, (int) daylightChanges[9], (int) daylightChanges[10], (int) daylightChanges[11], (int) daylightChanges[12], (int) daylightChanges[13], (int) daylightChanges[14], (int) daylightChanges[15]), new TimeSpan((long) daylightChanges[16] * 600000000L));
              this.m_CachedDaylightChanges.Add(key, (object) daylightTime);
            }
          }
        }
      }
      return (DaylightTime) this.m_CachedDaylightChanges[key];
    }

    public override TimeSpan GetUtcOffset(DateTime time)
    {
      if (time.Kind == DateTimeKind.Utc)
        return TimeSpan.Zero;
      return new TimeSpan(TimeZone.CalculateUtcOffset(time, this.GetDaylightChanges(time.Year)).Ticks + this.m_ticksOffset);
    }

    private static DateTime GetDayOfWeek(int year, bool fixedDate, int month, int targetDayOfWeek, int numberOfSunday, int hour, int minute, int second, int millisecond)
    {
      DateTime dateTime;
      if (fixedDate)
      {
        int num = DateTime.DaysInMonth(year, month);
        dateTime = new DateTime(year, month, num < numberOfSunday ? num : numberOfSunday, hour, minute, second, millisecond, DateTimeKind.Local);
      }
      else if (numberOfSunday <= 4)
      {
        dateTime = new DateTime(year, month, 1, hour, minute, second, millisecond, DateTimeKind.Local);
        int dayOfWeek = (int) dateTime.DayOfWeek;
        int num1 = targetDayOfWeek - dayOfWeek;
        if (num1 < 0)
          num1 += 7;
        int num2 = num1 + 7 * (numberOfSunday - 1);
        if (num2 > 0)
          dateTime = dateTime.AddDays((double) num2);
      }
      else
      {
        Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
        dateTime = new DateTime(year, month, defaultInstance.GetDaysInMonth(year, month), hour, minute, second, millisecond, DateTimeKind.Local);
        int num = (int) (dateTime.DayOfWeek - targetDayOfWeek);
        if (num < 0)
          num += 7;
        if (num > 0)
          dateTime = dateTime.AddDays((double) -num);
      }
      return dateTime;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeGetTimeZoneMinuteOffset();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetDaylightName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetStandardName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern short[] nativeGetDaylightChanges(int year);
  }
}
