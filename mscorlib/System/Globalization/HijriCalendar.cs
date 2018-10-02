// Decompiled with JetBrains decompiler
// Type: System.Globalization.HijriCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
  /// <summary>Представляет исламский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class HijriCalendar : Calendar
  {
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public static readonly int HijriEra = 1;
    internal static readonly int[] HijriMonthDays = new int[13]
    {
      0,
      30,
      59,
      89,
      118,
      148,
      177,
      207,
      236,
      266,
      295,
      325,
      355
    };
    internal static readonly DateTime calendarMinValue = new DateTime(622, 7, 18);
    internal static readonly DateTime calendarMaxValue = DateTime.MaxValue;
    private int m_HijriAdvance = int.MinValue;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MinAdvancedHijri = -2;
    internal const int MaxAdvancedHijri = 2;
    private const string InternationalRegKey = "Control Panel\\International";
    private const string HijriAdvanceRegKeyEntry = "AddHijriDate";
    internal const int MaxCalendarYear = 9666;
    internal const int MaxCalendarMonth = 4;
    internal const int MaxCalendarDay = 3;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;

    /// <summary>
    ///   Получает минимальное значение даты и времени, поддерживаемое этим календарем.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.HijriCalendar" /> тип, который эквивалентен первый момент, 18 июля 622 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return HijriCalendar.calendarMinValue;
      }
    }

    /// <summary>
    ///   Возвращает последнюю дату и время, поддерживаемые этим календарем.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.HijriCalendar" /> тип, который эквивалентен последний момент 31 декабря 9999 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return HijriCalendar.calendarMaxValue;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />.
    /// </returns>
    [ComVisible(false)]
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunarCalendar;
      }
    }

    internal override int ID
    {
      get
      {
        return 6;
      }
    }

    /// <summary>
    ///   Возвращает число дней в году, предшествующий год, который задается параметром <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" /> свойство.
    /// </summary>
    /// <returns>
    ///   Число дней в году, который предшествует года в заданном по <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" />.
    /// </returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 354;
      }
    }

    private long GetAbsoluteDateHijri(int y, int m, int d)
    {
      return this.DaysUpToHijriYear(y) + (long) HijriCalendar.HijriMonthDays[m - 1] + (long) d - 1L - (long) this.HijriAdjustment;
    }

    private long DaysUpToHijriYear(int HijriYear)
    {
      int num1 = (HijriYear - 1) / 30 * 30;
      int year = HijriYear - num1 - 1;
      long num2 = (long) num1 * 10631L / 30L + 227013L;
      for (; year > 0; --year)
        num2 += (long) (354 + (this.IsLeapYear(year, 0) ? 1 : 0));
      return num2;
    }

    /// <summary>
    ///   Возвращает или задает количество дней, чтобы добавить или вычесть из календаря компенсировать различия в начале и конце рамазана и разницу в датах между странах и регионах.
    /// </summary>
    /// <returns>
    ///   Целое число от -2 до 2, представляющее число дней для добавления или вычитания из календаря.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство задано недопустимое значение.
    /// </exception>
    public int HijriAdjustment
    {
      [SecuritySafeCritical] get
      {
        if (this.m_HijriAdvance == int.MinValue)
          this.m_HijriAdvance = HijriCalendar.GetAdvanceHijriDate();
        return this.m_HijriAdvance;
      }
      set
      {
        switch (value)
        {
          case 0:
          case 1:
          case 2:
            this.VerifyWritable();
            this.m_HijriAdvance = value;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (HijriAdjustment), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) -2, (object) 2));
        }
      }
    }

    [SecurityCritical]
    private static int GetAdvanceHijriDate()
    {
      int num1 = 0;
      RegistryKey registryKey;
      try
      {
        registryKey = Registry.CurrentUser.InternalOpenSubKey("Control Panel\\International", false);
      }
      catch (ObjectDisposedException ex)
      {
        return 0;
      }
      catch (ArgumentException ex)
      {
        return 0;
      }
      if (registryKey != null)
      {
        try
        {
          object obj = registryKey.InternalGetValue("AddHijriDate", (object) null, false, false);
          if (obj == null)
            return 0;
          string strA = obj.ToString();
          if (string.Compare(strA, 0, "AddHijriDate", 0, "AddHijriDate".Length, StringComparison.OrdinalIgnoreCase) == 0)
          {
            if (strA.Length == "AddHijriDate".Length)
            {
              num1 = -1;
            }
            else
            {
              string str = strA.Substring("AddHijriDate".Length);
              try
              {
                int num2 = int.Parse(str.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
                if (num2 >= -2)
                {
                  if (num2 <= 2)
                    num1 = num2;
                }
              }
              catch (ArgumentException ex)
              {
              }
              catch (FormatException ex)
              {
              }
              catch (OverflowException ex)
              {
              }
            }
          }
        }
        finally
        {
          registryKey.Close();
        }
      }
      return num1;
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < HijriCalendar.calendarMinValue.Ticks || ticks > HijriCalendar.calendarMaxValue.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) HijriCalendar.calendarMinValue, (object) HijriCalendar.calendarMaxValue));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != HijriCalendar.HijriEra)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      HijriCalendar.CheckEraRange(era);
      if (year < 1 || year > 9666)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9666));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      if (year == 9666 && month > 4)
        throw new ArgumentOutOfRangeException(nameof (month), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 4));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      HijriCalendar.CheckTicksRange(ticks);
      long num1 = ticks / 864000000000L + 1L + (long) this.HijriAdjustment;
      int num2 = (int) ((num1 - 227013L) * 30L / 10631L) + 1;
      long hijriYear = this.DaysUpToHijriYear(num2);
      long daysInYear = (long) this.GetDaysInYear(num2, 0);
      if (num1 < hijriYear)
      {
        hijriYear -= daysInYear;
        --num2;
      }
      else if (num1 == hijriYear)
      {
        --num2;
        hijriYear -= (long) this.GetDaysInYear(num2, 0);
      }
      else if (num1 > hijriYear + daysInYear)
      {
        hijriYear += daysInYear;
        ++num2;
      }
      if (part == 0)
        return num2;
      int num3 = 1;
      long num4 = num1 - hijriYear;
      if (part == 1)
        return (int) num4;
      while (num3 <= 12 && num4 > (long) HijriCalendar.HijriMonthDays[num3 - 1])
        ++num3;
      int num5 = num3 - 1;
      if (part == 2)
        return num5;
      int num6 = (int) (num4 - (long) HijriCalendar.HijriMonthDays[num5 - 1]);
      if (part == 3)
        return num6;
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число месяцев из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> Добавление месяцев.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанное число месяцев в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="months" /> — меньше -120000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="months" /> больше 120000.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int d = this.GetDatePart(time.Ticks, 3);
      int num1 = datePart2 - 1 + months;
      int num2;
      int num3;
      if (num1 >= 0)
      {
        num2 = num1 % 12 + 1;
        num3 = datePart1 + num1 / 12;
      }
      else
      {
        num2 = 12 + (num1 + 1) % 12;
        num3 = datePart1 + (num1 - 11) / 12;
      }
      int daysInMonth = this.GetDaysInMonth(num3, num2);
      if (d > daysInMonth)
        d = daysInMonth;
      long ticks = this.GetAbsoluteDateHijri(num3, num2, d) * 864000000000L + time.Ticks % 864000000000L;
      Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число лет из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> Добавление лет.
    /// </param>
    /// <param name="years">Число лет для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число лет к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>
    ///   Возвращает день месяца в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 30, обозначающее день месяца в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    /// <summary>
    ///   Возвращает день недели в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DayOfWeek" /> значение, представляющее день недели в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>
    ///   Возвращает день года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 355, обозначающее день года в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 1);
    }

    /// <summary>
    ///   Возвращает число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      HijriCalendar.CheckYearMonthRange(year, month, era);
      if (month == 12)
        return !this.IsLeapYear(year, 0) ? 29 : 30;
      return month % 2 != 1 ? 29 : 30;
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Число дней в указанном году указанной эры.
    ///    Число дней равно 354 месяцам для обычного года или 355 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return !this.IsLeapYear(year, 0) ? 354 : 355;
    }

    /// <summary>
    ///   Возвращает значение эры в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetEra(DateTime time)
    {
      HijriCalendar.CheckTicksRange(time.Ticks);
      return HijriCalendar.HijriEra;
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.HijriCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.HijriCalendar" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ HijriCalendar.HijriEra };
      }
    }

    /// <summary>
    ///   Возвращает месяц в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 12, представляющее месяц в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число месяцев в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return 12;
    }

    /// <summary>
    ///   Возвращает значение года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>Определяет, является ли указанный день високосным.</summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 30, представляющее день.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      if (this.IsLeapYear(year, era) && month == 12)
        return day == 30;
      return false;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see cref="F:System.Globalization.Calendar.CurrentEra" /> или <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.
    /// </param>
    /// <returns>
    ///   Всегда равно 0 из-за <see cref="T:System.Globalization.HijriCalendar" /> тип не поддерживает понятие високосного года.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> меньше календарный год хиджры 1 или больше года 9666.
    /// 
    ///   -или-
    /// 
    ///   Свойству <paramref name="era" /> задано значение, отличное от <see cref="F:System.Globalization.Calendar.CurrentEra" /> или <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.
    /// </exception>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Этот метод всегда возвращает <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      HijriCalendar.CheckYearMonthRange(year, month, era);
      return false;
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
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return (year * 11 + 14) % 30 < 11;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеющим значение указанной даты, времени и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 30, представляющее день.
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
    ///   <see cref="T:System.DateTime" /> Имеет значение указанной даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше нуля или больше 23.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="minute" /> меньше нуля или больше 59.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="second" /> меньше нуля или больше 59.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="millisecond" /> меньше нуля или больше 999.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      long absoluteDateHijri = this.GetAbsoluteDateHijri(year, month, day);
      if (absoluteDateHijri >= 0L)
        return new DateTime(absoluteDateHijri * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот календарь доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в наборе операций меньше 100 или больше, чем 9666.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9666)
          throw new ArgumentOutOfRangeException(nameof (value), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9666));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.HijriCalendar.TwoDigitYearMax" /> Свойства для определения века.
    /// </summary>
    /// <param name="year">
    ///   Двузначное или четырехзначное целое число, представляющее год для преобразования.
    /// </param>
    /// <returns>
    ///   Целое число, содержащее четырехразрядное представление <paramref name="year" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 9666)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9666));
      return year;
    }
  }
}
