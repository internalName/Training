// Decompiled with JetBrains decompiler
// Type: System.Globalization.GregorianCalendarHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Globalization
{
  [Serializable]
  internal class GregorianCalendarHelper
  {
    internal static readonly int[] DaysToMonth365 = new int[13]
    {
      0,
      31,
      59,
      90,
      120,
      151,
      181,
      212,
      243,
      273,
      304,
      334,
      365
    };
    internal static readonly int[] DaysToMonth366 = new int[13]
    {
      0,
      31,
      60,
      91,
      121,
      152,
      182,
      213,
      244,
      274,
      305,
      335,
      366
    };
    [OptionalField(VersionAdded = 1)]
    internal int m_maxYear = 9999;
    internal const long TicksPerMillisecond = 10000;
    internal const long TicksPerSecond = 10000000;
    internal const long TicksPerMinute = 600000000;
    internal const long TicksPerHour = 36000000000;
    internal const long TicksPerDay = 864000000000;
    internal const int MillisPerSecond = 1000;
    internal const int MillisPerMinute = 60000;
    internal const int MillisPerHour = 3600000;
    internal const int MillisPerDay = 86400000;
    internal const int DaysPerYear = 365;
    internal const int DaysPer4Years = 1461;
    internal const int DaysPer100Years = 36524;
    internal const int DaysPer400Years = 146097;
    internal const int DaysTo10000 = 3652059;
    internal const long MaxMillis = 315537897600000;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    [OptionalField(VersionAdded = 1)]
    internal int m_minYear;
    internal Calendar m_Cal;
    [OptionalField(VersionAdded = 1)]
    internal EraInfo[] m_EraInfo;
    [OptionalField(VersionAdded = 1)]
    internal int[] m_eras;
    [OptionalField(VersionAdded = 1)]
    internal DateTime m_minDate;

    internal int MaxYear
    {
      get
      {
        return this.m_maxYear;
      }
    }

    internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
    {
      this.m_Cal = cal;
      this.m_EraInfo = eraInfo;
      this.m_minDate = this.m_Cal.MinSupportedDateTime;
      this.m_maxYear = this.m_EraInfo[0].maxEraYear;
      this.m_minYear = this.m_EraInfo[0].minEraYear;
    }

    internal int GetGregorianYear(int year, int era)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (era == 0)
        era = this.m_Cal.CurrentEraValue;
      for (int index = 0; index < this.m_EraInfo.Length; ++index)
      {
        if (era == this.m_EraInfo[index].era)
        {
          if (year < this.m_EraInfo[index].minEraYear || year > this.m_EraInfo[index].maxEraYear)
            throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) this.m_EraInfo[index].minEraYear, (object) this.m_EraInfo[index].maxEraYear));
          return this.m_EraInfo[index].yearOffset + year;
        }
      }
      throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal bool IsValidYear(int year, int era)
    {
      if (year < 0)
        return false;
      if (era == 0)
        era = this.m_Cal.CurrentEraValue;
      for (int index = 0; index < this.m_EraInfo.Length; ++index)
      {
        if (era == this.m_EraInfo[index].era)
          return year >= this.m_EraInfo[index].minEraYear && year <= this.m_EraInfo[index].maxEraYear;
      }
      return false;
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      this.CheckTicksRange(ticks);
      int num1 = (int) (ticks / 864000000000L);
      int num2 = num1 / 146097;
      int num3 = num1 - num2 * 146097;
      int num4 = num3 / 36524;
      if (num4 == 4)
        num4 = 3;
      int num5 = num3 - num4 * 36524;
      int num6 = num5 / 1461;
      int num7 = num5 - num6 * 1461;
      int num8 = num7 / 365;
      if (num8 == 4)
        num8 = 3;
      if (part == 0)
        return num2 * 400 + num4 * 100 + num6 * 4 + num8 + 1;
      int num9 = num7 - num8 * 365;
      if (part == 1)
        return num9 + 1;
      int[] numArray = num8 == 3 && (num6 != 24 || num4 == 3) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365;
      int index = num9 >> 6;
      while (num9 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num9 - numArray[index - 1] + 1;
    }

    internal static long GetAbsoluteDate(int year, int month, int day)
    {
      if (year >= 1 && year <= 9999 && (month >= 1 && month <= 12))
      {
        int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendarHelper.DaysToMonth365 : GregorianCalendarHelper.DaysToMonth366;
        if (day >= 1 && day <= numArray[month] - numArray[month - 1])
        {
          int num = year - 1;
          return (long) (num * 365 + num / 4 - num / 100 + num / 400 + numArray[month - 1] + day - 1);
        }
      }
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    internal static long DateToTicks(int year, int month, int day)
    {
      return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
    }

    internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
    {
      if (hour < 0 || hour >= 24 || (minute < 0 || minute >= 60) || (second < 0 || second >= 60))
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 999));
      return TimeSpan.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
    }

    internal void CheckTicksRange(long ticks)
    {
      if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) this.m_Cal.MinSupportedDateTime, (object) this.m_Cal.MaxSupportedDateTime));
    }

    public DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      this.CheckTicksRange(time.Ticks);
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
      int num1 = datePart2 - 1 + months;
      int month;
      int year;
      if (num1 >= 0)
      {
        month = num1 % 12 + 1;
        year = datePart1 + num1 / 12;
      }
      else
      {
        month = 12 + (num1 + 1) % 12;
        year = datePart1 + (num1 - 11) / 12;
      }
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendarHelper.DaysToMonth365 : GregorianCalendarHelper.DaysToMonth366;
      int num2 = numArray[month] - numArray[month - 1];
      if (day > num2)
        day = num2;
      long ticks = GregorianCalendarHelper.DateToTicks(year, month, day) + time.Ticks % 864000000000L;
      Calendar.CheckAddResult(ticks, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    public DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    public int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    public DayOfWeek GetDayOfWeek(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      return (DayOfWeek) ((time.Ticks / 864000000000L + 1L) % 7L);
    }

    public int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 1);
    }

    public int GetDaysInMonth(int year, int month, int era)
    {
      year = this.GetGregorianYear(year, era);
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendarHelper.DaysToMonth365 : GregorianCalendarHelper.DaysToMonth366;
      return numArray[month] - numArray[month - 1];
    }

    public int GetDaysInYear(int year, int era)
    {
      year = this.GetGregorianYear(year, era);
      return year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? 365 : 366;
    }

    public int GetEra(DateTime time)
    {
      long ticks = time.Ticks;
      for (int index = 0; index < this.m_EraInfo.Length; ++index)
      {
        if (ticks >= this.m_EraInfo[index].ticks)
          return this.m_EraInfo[index].era;
      }
      throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Era"));
    }

    public int[] Eras
    {
      get
      {
        if (this.m_eras == null)
        {
          this.m_eras = new int[this.m_EraInfo.Length];
          for (int index = 0; index < this.m_EraInfo.Length; ++index)
            this.m_eras[index] = this.m_EraInfo[index].era;
        }
        return (int[]) this.m_eras.Clone();
      }
    }

    public int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    public int GetMonthsInYear(int year, int era)
    {
      year = this.GetGregorianYear(year, era);
      return 12;
    }

    public int GetYear(DateTime time)
    {
      long ticks = time.Ticks;
      int datePart = this.GetDatePart(ticks, 0);
      for (int index = 0; index < this.m_EraInfo.Length; ++index)
      {
        if (ticks >= this.m_EraInfo[index].ticks)
          return datePart - this.m_EraInfo[index].yearOffset;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_NoEra"));
    }

    public int GetYear(int year, DateTime time)
    {
      long ticks = time.Ticks;
      for (int index = 0; index < this.m_EraInfo.Length; ++index)
      {
        if (ticks >= this.m_EraInfo[index].ticks)
          return year - this.m_EraInfo[index].yearOffset;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_NoEra"));
    }

    public bool IsLeapDay(int year, int month, int day, int era)
    {
      if (day < 1 || day > this.GetDaysInMonth(year, month, era))
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) this.GetDaysInMonth(year, month, era)));
      return this.IsLeapYear(year, era) && month == 2 && day == 29;
    }

    public int GetLeapMonth(int year, int era)
    {
      year = this.GetGregorianYear(year, era);
      return 0;
    }

    public bool IsLeapMonth(int year, int month, int era)
    {
      year = this.GetGregorianYear(year, era);
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 12));
      return false;
    }

    public bool IsLeapYear(int year, int era)
    {
      year = this.GetGregorianYear(year, era);
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
    }

    public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      year = this.GetGregorianYear(year, era);
      long ticks = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
      this.CheckTicksRange(ticks);
      return new DateTime(ticks);
    }

    public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      this.CheckTicksRange(time.Ticks);
      return GregorianCalendar.GetDefaultInstance().GetWeekOfYear(time, rule, firstDayOfWeek);
    }

    public int ToFourDigitYear(int year, int twoDigitYearMax)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (year < 100)
      {
        int num = year % 100;
        return (twoDigitYearMax / 100 - (num > twoDigitYearMax % 100 ? 1 : 0)) * 100 + num;
      }
      if (year < this.m_minYear || year > this.m_maxYear)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) this.m_minYear, (object) this.m_maxYear));
      return year;
    }
  }
}
