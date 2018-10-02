// Decompiled with JetBrains decompiler
// Type: System.Globalization.EastAsianLunisolarCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Представляет календарь, который делит время на месяцы, дни, года и эры и даты, основанные на циклы Солнца и луны.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class EastAsianLunisolarCalendar : Calendar
  {
    internal static readonly int[] DaysToMonth365 = new int[12]
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
      334
    };
    internal static readonly int[] DaysToMonth366 = new int[12]
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
      335
    };
    internal const int LeapMonth = 0;
    internal const int Jan1Month = 1;
    internal const int Jan1Date = 2;
    internal const int nDaysPerMonth = 3;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MaxCalendarMonth = 13;
    internal const int MaxCalendarDay = 30;
    private const int DEFAULT_GREGORIAN_TWO_DIGIT_YEAR_MAX = 2029;

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.
    /// </returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunisolarCalendar;
      }
    }

    /// <summary>
    ///   Рассчитывает год в шестидесятилетнем (60-год), соответствующее указанной дате.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" /> для чтения.
    /// </param>
    /// <returns>
    ///   Число от 1 до 60 в шестидесятилетнем, соответствующий <paramref name="date" /> параметр.
    /// </returns>
    public virtual int GetSexagenaryYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return (year - 4) % 60 + 1;
    }

    /// <summary>
    ///   Вычисляет Небесных корень указанного года в шестидесятилетнем (60-год).
    /// </summary>
    /// <param name="sexagenaryYear">
    ///   Целое число от 1 до 60, представляющее год в шестидесятилетнем.
    /// </param>
    /// <returns>Число от 1 до 10.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="sexagenaryYear" /> значение меньше 1 или больше 60.
    /// </exception>
    public int GetCelestialStem(int sexagenaryYear)
    {
      if (sexagenaryYear < 1 || sexagenaryYear > 60)
        throw new ArgumentOutOfRangeException(nameof (sexagenaryYear), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 60));
      return (sexagenaryYear - 1) % 10 + 1;
    }

    /// <summary>
    ///   Расчет земной ветви указанного года в шестидесятилетнем (60-год).
    /// </summary>
    /// <param name="sexagenaryYear">
    ///   Целое число от 1 до 60, представляющее год в шестидесятилетнем.
    /// </param>
    /// <returns>Целое число от 1 до 12.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="sexagenaryYear" /> значение меньше 1 или больше 60.
    /// </exception>
    public int GetTerrestrialBranch(int sexagenaryYear)
    {
      if (sexagenaryYear < 1 || sexagenaryYear > 60)
        throw new ArgumentOutOfRangeException(nameof (sexagenaryYear), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 60));
      return (sexagenaryYear - 1) % 12 + 1;
    }

    internal abstract int GetYearInfo(int LunarYear, int Index);

    internal abstract int GetYear(int year, DateTime time);

    internal abstract int GetGregorianYear(int year, int era);

    internal abstract int MinCalendarYear { get; }

    internal abstract int MaxCalendarYear { get; }

    internal abstract EraInfo[] CalEraInfo { get; }

    internal abstract DateTime MinDate { get; }

    internal abstract DateTime MaxDate { get; }

    internal int MinEraCalendarYear(int era)
    {
      EraInfo[] calEraInfo = this.CalEraInfo;
      if (calEraInfo == null)
        return this.MinCalendarYear;
      if (era == 0)
        era = this.CurrentEraValue;
      if (era == this.GetEra(this.MinDate))
        return this.GetYear(this.MinCalendarYear, this.MinDate);
      for (int index = 0; index < calEraInfo.Length; ++index)
      {
        if (era == calEraInfo[index].era)
          return calEraInfo[index].minEraYear;
      }
      throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal int MaxEraCalendarYear(int era)
    {
      EraInfo[] calEraInfo = this.CalEraInfo;
      if (calEraInfo == null)
        return this.MaxCalendarYear;
      if (era == 0)
        era = this.CurrentEraValue;
      if (era == this.GetEra(this.MaxDate))
        return this.GetYear(this.MaxCalendarYear, this.MaxDate);
      for (int index = 0; index < calEraInfo.Length; ++index)
      {
        if (era == calEraInfo[index].era)
          return calEraInfo[index].maxEraYear;
      }
      throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal EastAsianLunisolarCalendar()
    {
    }

    internal void CheckTicksRange(long ticks)
    {
      if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) this.MinSupportedDateTime, (object) this.MaxSupportedDateTime));
    }

    internal void CheckEraRange(int era)
    {
      if (era == 0)
        era = this.CurrentEraValue;
      if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal int CheckYearRange(int year, int era)
    {
      this.CheckEraRange(era);
      year = this.GetGregorianYear(year, era);
      if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) this.MinEraCalendarYear(era), (object) this.MaxEraCalendarYear(era)));
      return year;
    }

    internal int CheckYearMonthRange(int year, int month, int era)
    {
      year = this.CheckYearRange(year, era);
      if (month == 13 && this.GetYearInfo(year, 0) == 0)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      if (month < 1 || month > 13)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      return year;
    }

    internal int InternalGetDaysInMonth(int year, int month)
    {
      int num = 32768 >> month - 1;
      return (this.GetYearInfo(year, 3) & num) != 0 ? 30 : 29;
    }

    /// <summary>
    ///   Вычисляет число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12 в обычном году или от 1 до 13 в високосном году, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      return this.InternalGetDaysInMonth(year, month);
    }

    private static int GregorianIsLeapYear(int y)
    {
      return y % 4 == 0 && (y % 100 != 0 || y % 400 == 0) ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеющим значение указанной даты, времени и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
    /// </param>
    /// <param name="hour">
    ///   Целое число от 0 до 23, представляющее час.
    /// </param>
    /// <param name="minute">
    ///   Целое число от 0 до 59, представляющее минуту.
    /// </param>
    /// <param name="second">
    ///   Целое число от 0 до 59, представляющее секунду.
    /// </param>
    /// <param name="millisecond">
    ///   Целое число от 0 до 999, представляющее миллисекунду.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" /> имеющим значение указанной даты, времени и эры.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int daysInMonth = this.InternalGetDaysInMonth(year, month);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), Environment.GetResourceString("ArgumentOutOfRange_Day", (object) daysInMonth, (object) month));
      int nSolarYear = 0;
      int nSolarMonth = 0;
      int nSolarDay = 0;
      if (this.LunarToGregorian(year, month, day, ref nSolarYear, ref nSolarMonth, ref nSolarDay))
        return new DateTime(nSolarYear, nSolarMonth, nSolarDay, hour, minute, second, millisecond);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    internal void GregorianToLunar(int nSYear, int nSMonth, int nSDate, ref int nLYear, ref int nLMonth, ref int nLDate)
    {
      int num1 = (EastAsianLunisolarCalendar.GregorianIsLeapYear(nSYear) == 1 ? EastAsianLunisolarCalendar.DaysToMonth366[nSMonth - 1] : EastAsianLunisolarCalendar.DaysToMonth365[nSMonth - 1]) + nSDate;
      nLYear = nSYear;
      int yearInfo1;
      int yearInfo2;
      if (nLYear == this.MaxCalendarYear + 1)
      {
        --nLYear;
        num1 += EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1 ? 366 : 365;
        yearInfo1 = this.GetYearInfo(nLYear, 1);
        yearInfo2 = this.GetYearInfo(nLYear, 2);
      }
      else
      {
        yearInfo1 = this.GetYearInfo(nLYear, 1);
        yearInfo2 = this.GetYearInfo(nLYear, 2);
        if (nSMonth < yearInfo1 || nSMonth == yearInfo1 && nSDate < yearInfo2)
        {
          --nLYear;
          num1 += EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1 ? 366 : 365;
          yearInfo1 = this.GetYearInfo(nLYear, 1);
          yearInfo2 = this.GetYearInfo(nLYear, 2);
        }
      }
      int num2 = num1 - EastAsianLunisolarCalendar.DaysToMonth365[yearInfo1 - 1] - (yearInfo2 - 1);
      int num3 = 32768;
      int yearInfo3 = this.GetYearInfo(nLYear, 3);
      int num4 = (yearInfo3 & num3) != 0 ? 30 : 29;
      nLMonth = 1;
      for (; num2 > num4; num4 = (yearInfo3 & num3) != 0 ? 30 : 29)
      {
        num2 -= num4;
        ++nLMonth;
        num3 >>= 1;
      }
      nLDate = num2;
    }

    internal bool LunarToGregorian(int nLYear, int nLMonth, int nLDate, ref int nSolarYear, ref int nSolarMonth, ref int nSolarDay)
    {
      if (nLDate < 1 || nLDate > 30)
        return false;
      int num1 = nLDate - 1;
      for (int month = 1; month < nLMonth; ++month)
        num1 += this.InternalGetDaysInMonth(nLYear, month);
      int yearInfo1 = this.GetYearInfo(nLYear, 1);
      int yearInfo2 = this.GetYearInfo(nLYear, 2);
      int num2 = EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear);
      int[] numArray = num2 == 1 ? EastAsianLunisolarCalendar.DaysToMonth366 : EastAsianLunisolarCalendar.DaysToMonth365;
      nSolarDay = yearInfo2;
      if (yearInfo1 > 1)
        nSolarDay += numArray[yearInfo1 - 1];
      nSolarDay += num1;
      if (nSolarDay > num2 + 365)
      {
        nSolarYear = nLYear + 1;
        nSolarDay -= num2 + 365;
      }
      else
        nSolarYear = nLYear;
      nSolarMonth = 1;
      while (nSolarMonth < 12 && numArray[nSolarMonth] < nSolarDay)
        ++nSolarMonth;
      nSolarDay -= numArray[nSolarMonth - 1];
      return true;
    }

    internal DateTime LunarToTime(DateTime time, int year, int month, int day)
    {
      int nSolarYear = 0;
      int nSolarMonth = 0;
      int nSolarDay = 0;
      this.LunarToGregorian(year, month, day, ref nSolarYear, ref nSolarMonth, ref nSolarDay);
      return GregorianCalendar.GetDefaultInstance().ToDateTime(nSolarYear, nSolarMonth, nSolarDay, time.Hour, time.Minute, time.Second, time.Millisecond);
    }

    internal void TimeToLunar(DateTime time, ref int year, ref int month, ref int day)
    {
      Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
      this.GregorianToLunar(defaultInstance.GetYear(time), defaultInstance.GetMonth(time), defaultInstance.GetDayOfMonth(time), ref year, ref month, ref day);
    }

    /// <summary>
    ///   Вычисляет дату на указанное число месяцев от указанной даты.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому необходимо добавить <paramref name="months" />.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   Новый <see cref="T:System.DateTime" /> полученный в результате добавления указанного количества месяцев, <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результат находится вне поддерживаемого диапазона <see cref="T:System.DateTime" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="months" /> меньше -120000 или больше 120000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="time" /> является менее <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) -120000, (object) 120000));
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month1 = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month1, ref day);
      int num1 = month1 + months;
      int month2;
      if (num1 > 0)
      {
        for (int index = this.InternalIsLeapYear(year) ? 13 : 12; num1 - index > 0; index = this.InternalIsLeapYear(year) ? 13 : 12)
        {
          num1 -= index;
          ++year;
        }
        month2 = num1;
      }
      else
      {
        while (num1 <= 0)
        {
          int num2 = this.InternalIsLeapYear(year - 1) ? 13 : 12;
          num1 += num2;
          --year;
        }
        month2 = num1;
      }
      int daysInMonth = this.InternalGetDaysInMonth(year, month2);
      if (day > daysInMonth)
        day = daysInMonth;
      DateTime time1 = this.LunarToTime(time, year, month2, day);
      Calendar.CheckAddResult(time1.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return time1;
    }

    /// <summary>
    ///   Вычисляет дату на указанное число лет от указанной даты.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому необходимо добавить <paramref name="years" />.
    /// </param>
    /// <param name="years">Число лет для добавления.</param>
    /// <returns>
    ///   Новый <see cref="T:System.DateTime" /> полученный в результате добавления указанного количества лет <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результат находится вне поддерживаемого диапазона <see cref="T:System.DateTime" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> является менее <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      this.CheckTicksRange(time.Ticks);
      int year1 = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year1, ref month, ref day);
      int year2 = year1 + years;
      if (month == 13 && !this.InternalIsLeapYear(year2))
      {
        month = 12;
        day = this.InternalGetDaysInMonth(year2, month);
      }
      int daysInMonth = this.InternalGetDaysInMonth(year2, month);
      if (day > daysInMonth)
        day = daysInMonth;
      DateTime time1 = this.LunarToTime(time, year2, month, day);
      Calendar.CheckAddResult(time1.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return time1;
    }

    /// <summary>Рассчитывает день года из заданной даты.</summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 354 в обычном году или от 1 до 384 в високосном году, представляющее день года в заданном <paramref name="time" /> параметр.
    /// </returns>
    public override int GetDayOfYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month1 = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month1, ref day);
      for (int month2 = 1; month2 < month1; ++month2)
        day += this.InternalGetDaysInMonth(year, month2);
      return day;
    }

    /// <summary>Вычисляет число месяца указанной даты.</summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 31, представляющее день месяца в заданном <paramref name="time" /> параметр.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return day;
    }

    /// <summary>
    ///   Вычисляет количество дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      int num1 = 0;
      int num2 = this.InternalIsLeapYear(year) ? 13 : 12;
      while (num2 != 0)
        num1 += this.InternalGetDaysInMonth(year, num2--);
      return num1;
    }

    /// <summary>Возвращает месяц заданной даты.</summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 13, представляющее месяц в заданном <paramref name="time" /> параметр.
    /// </returns>
    public override int GetMonth(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return month;
    }

    /// <summary>Возвращает год заданной даты.</summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return this.GetYear(year, time);
    }

    /// <summary>Рассчитывает день недели из заданной даты.</summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Один из <see cref="T:System.DayOfWeek" /> значений, представляющее день недели, заданный в <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> является менее <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.
    /// </exception>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>
    ///   Вычисляет число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Число месяцев в указанном году указанной эры.
    ///    Возвращает значение 12 месяцев в обычном году или 13 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      return !this.InternalIsLeapYear(year) ? 12 : 13;
    }

    /// <summary>
    ///   Определяет, является ли указанная дата указанной эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int daysInMonth = this.InternalGetDaysInMonth(year, month);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), Environment.GetResourceString("ArgumentOutOfRange_Day", (object) daysInMonth, (object) month));
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo != 0)
        return month == yearInfo + 1;
      return false;
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="month" /> параметр — високосный; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo != 0)
        return month == yearInfo + 1;
      return false;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Положительное целое число от 1 до 13, указывающее високосный месяц в указанном году указанной эры.
    /// 
    ///    -или-
    /// 
    ///   Нуль, если этот календарь не поддерживает високосные месяцы, или если <paramref name="year" /> и <paramref name="era" /> параметров не указан високосный год.
    /// </returns>
    public override int GetLeapMonth(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo > 0)
        return yearInfo + 1;
      return 0;
    }

    internal bool InternalIsLeapYear(int year)
    {
      return (uint) this.GetYearInfo(year, 0) > 0U;
    }

    /// <summary>
    ///   Определяет, является ли указанный год указанной эры високосным годом.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный год — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      return this.InternalIsLeapYear(year);
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Globalization.EastAsianLunisolarCalendar" />  доступно только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в наборе операций меньше 99 или больше наибольшего поддерживаемого года в текущем календаре.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.MaxCalendarYear)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 99, (object) this.MaxCalendarYear));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>Преобразует указанный год в четырехзначный год.</summary>
    /// <param name="year">
    ///   Двузначное или четырехзначное целое число, представляющее год для преобразования.
    /// </param>
    /// <returns>
    ///   Целое число, содержащая четырехзначное представление <paramref name="year" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      year = base.ToFourDigitYear(year);
      this.CheckYearRange(year, 0);
      return year;
    }
  }
}
