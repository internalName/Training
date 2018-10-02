// Decompiled with JetBrains decompiler
// Type: System.TimeZone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
  /// <summary>Представляет часовой пояс.</summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class TimeZone
  {
    private static volatile TimeZone currentTimeZone;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (TimeZone.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, obj, (object) null);
        }
        return TimeZone.s_InternalSyncObject;
      }
    }

    /// <summary>Возвращает часовой пояс текущего компьютера.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.TimeZone" /> , представляющий текущего часового пояса.
    /// </returns>
    public static TimeZone CurrentTimeZone
    {
      get
      {
        TimeZone currentTimeZone = TimeZone.currentTimeZone;
        if (currentTimeZone == null)
        {
          lock (TimeZone.InternalSyncObject)
          {
            if (TimeZone.currentTimeZone == null)
              TimeZone.currentTimeZone = (TimeZone) new CurrentSystemTimeZone();
            currentTimeZone = TimeZone.currentTimeZone;
          }
        }
        return currentTimeZone;
      }
    }

    internal static void ResetTimeZone()
    {
      if (TimeZone.currentTimeZone == null)
        return;
      lock (TimeZone.InternalSyncObject)
        TimeZone.currentTimeZone = (TimeZone) null;
    }

    /// <summary>Возвращает имя зимнего времени часового пояса.</summary>
    /// <returns>Стандартное название часового пояса.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка присвоить этому свойству значение <see langword="null" />.
    /// </exception>
    public abstract string StandardName { get; }

    /// <summary>Возвращает имя летнего времени часового пояса.</summary>
    /// <returns>Имя зоны летнего времени.</returns>
    public abstract string DaylightName { get; }

    /// <summary>
    ///   Возвращает смещение для заданного локального времени по Гринвичу (UTC).
    /// </summary>
    /// <param name="time">Значение даты и времени.</param>
    /// <returns>
    ///   Смещение по Гринвичу (UTC) от <paramref name="time" />.
    /// </returns>
    public abstract TimeSpan GetUtcOffset(DateTime time);

    /// <summary>
    ///   Возвращает указанное время по Гринвичу (UTC), соответствующий.
    /// </summary>
    /// <param name="time">Дата и время.</param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" /> объект, значение которого равно по Гринвичу (UTC), соответствующий <paramref name="time" />.
    /// </returns>
    public virtual DateTime ToUniversalTime(DateTime time)
    {
      if (time.Kind == DateTimeKind.Utc)
        return time;
      long ticks = time.Ticks - this.GetUtcOffset(time).Ticks;
      if (ticks > 3155378975999999999L)
        return new DateTime(3155378975999999999L, DateTimeKind.Utc);
      if (ticks < 0L)
        return new DateTime(0L, DateTimeKind.Utc);
      return new DateTime(ticks, DateTimeKind.Utc);
    }

    /// <summary>
    ///   Возвращает местное время, соответствующее заданной дате и времени.
    /// </summary>
    /// <param name="time">Время по Гринвичу (UTC).</param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" /> объект, значение которого является местное время, соответствующее <paramref name="time" />.
    /// </returns>
    public virtual DateTime ToLocalTime(DateTime time)
    {
      if (time.Kind == DateTimeKind.Local)
        return time;
      bool isAmbiguousLocalDst = false;
      long fromUniversalTime = ((CurrentSystemTimeZone) TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref isAmbiguousLocalDst);
      return new DateTime(time.Ticks + fromUniversalTime, DateTimeKind.Local, isAmbiguousLocalDst);
    }

    /// <summary>
    ///   Возвращает период летнего времени для определенного года.
    /// </summary>
    /// <param name="year">
    ///   Год, к которому применяется период летнего времени.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.DaylightTime" /> , содержащий дату начала и окончания для летнего времени в <paramref name="year" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// </exception>
    public abstract DaylightTime GetDaylightChanges(int year);

    /// <summary>
    ///   Возвращает значение, указывающее, является ли указанной даты и времени в течение летнего времени.
    /// </summary>
    /// <param name="time">Дата и время.</param>
    /// <returns>
    ///   <see langword="true" />Если <paramref name="time" /> в к летнему времени период, в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsDaylightSavingTime(DateTime time)
    {
      return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли указанной даты и времени в течение периода летнего времени.
    /// </summary>
    /// <param name="time">Дата и время.</param>
    /// <param name="daylightTimes">Период летнего времени.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="time" /> в <paramref name="daylightTimes" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="daylightTimes" /> имеет значение <see langword="null" />.
    /// </exception>
    public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
    {
      return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
    }

    internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
    {
      if (daylightTimes == null || time.Kind == DateTimeKind.Utc)
        return TimeSpan.Zero;
      DateTime dateTime1 = daylightTimes.Start + daylightTimes.Delta;
      DateTime end = daylightTimes.End;
      DateTime dateTime2;
      DateTime dateTime3;
      if (daylightTimes.Delta.Ticks > 0L)
      {
        dateTime2 = end - daylightTimes.Delta;
        dateTime3 = end;
      }
      else
      {
        dateTime2 = dateTime1;
        dateTime3 = dateTime1 - daylightTimes.Delta;
      }
      bool flag = false;
      if (dateTime1 > end)
      {
        if (time >= dateTime1 || time < end)
          flag = true;
      }
      else if (time >= dateTime1 && time < end)
        flag = true;
      if (flag && time >= dateTime2 && time < dateTime3)
        flag = time.IsAmbiguousDaylightSavingTime();
      if (flag)
        return daylightTimes.Delta;
      return TimeSpan.Zero;
    }
  }
}
