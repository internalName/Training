// Decompiled with JetBrains decompiler
// Type: System.Globalization.PersianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>Представляет персидский календарь.</summary>
  [Serializable]
  public class PersianCalendar : Calendar
  {
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public static readonly int PersianEra = 1;
    internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;
    internal static int[] DaysToMonth = new int[13]
    {
      0,
      31,
      62,
      93,
      124,
      155,
      186,
      216,
      246,
      276,
      306,
      336,
      366
    };
    internal static DateTime minDate = new DateTime(622, 3, 22);
    internal static DateTime maxDate = DateTime.MaxValue;
    private const int ApproximateHalfYear = 180;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MonthsPerYear = 12;
    internal const int MaxCalendarYear = 9378;
    internal const int MaxCalendarMonth = 10;
    internal const int MaxCalendarDay = 13;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;

    /// <summary>
    ///   Получает самые ранние дату и время, поддерживаемые классом <see cref="T:System.Globalization.PersianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Самые ранние дата и время, поддерживаемые классом <see cref="T:System.Globalization.PersianCalendar" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return PersianCalendar.minDate;
      }
    }

    /// <summary>
    ///   Получает самые последние дату и время, поддерживаемые классом <see cref="T:System.Globalization.PersianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Самые последние дата и время, поддерживаемые классом <see cref="T:System.Globalization.PersianCalendar" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return PersianCalendar.maxDate;
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли текущий календарь солнечным, лунным или солнечно-лунным.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.
    /// </returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.SolarCalendar;
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 1;
      }
    }

    internal override int ID
    {
      get
      {
        return 22;
      }
    }

    private long GetAbsoluteDatePersian(int year, int month, int day)
    {
      if (year < 1 || year > 9378 || (month < 1 || month > 12))
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
      int num1 = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
      int num2 = (int) (365.242189 * (double) (year - 1));
      return CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long) num2 + 180L) + (long) num1;
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) PersianCalendar.minDate, (object) PersianCalendar.maxDate));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != PersianCalendar.PersianEra)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      PersianCalendar.CheckEraRange(era);
      if (year < 1 || year > 9378)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9378));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378 && month > 10)
        throw new ArgumentOutOfRangeException(nameof (month), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 10));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    private static int MonthFromOrdinalDay(int ordinalDay)
    {
      int index = 0;
      while (ordinalDay > PersianCalendar.DaysToMonth[index])
        ++index;
      return index;
    }

    private static int DaysInPreviousMonths(int month)
    {
      --month;
      return PersianCalendar.DaysToMonth[month];
    }

    internal int GetDatePart(long ticks, int part)
    {
      PersianCalendar.CheckTicksRange(ticks);
      long numberOfDays = ticks / 864000000000L + 1L;
      int year = (int) Math.Floor((double) (CalendricalCalculationsHelper.PersianNewYearOnOrBefore(numberOfDays) - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
      if (part == 0)
        return year;
      int ordinalDay = (int) (numberOfDays - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 1)));
      if (part == 1)
        return ordinalDay;
      int month = PersianCalendar.MonthFromOrdinalDay(ordinalDay);
      if (part == 2)
        return month;
      int num = ordinalDay - PersianCalendar.DaysInPreviousMonths(month);
      if (part == 3)
        return num;
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" />, который смещен от указанного объекта <see cref="T:System.DateTime" /> на заданное число месяцев.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются месяцы.
    /// </param>
    /// <param name="months">
    ///   Положительное или отрицательное число месяцев для добавления.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" />, представляющий дату, полученную путем добавления числа месяцев, указанных в параметре <paramref name="months" />, к указанной дате в параметре <paramref name="time" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="months" /> имеет значение меньше –120 000 или больше 120 000.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
      int num = datePart2 - 1 + months;
      int month;
      int year;
      if (num >= 0)
      {
        month = num % 12 + 1;
        year = datePart1 + num / 12;
      }
      else
      {
        month = 12 + (num + 1) % 12;
        year = datePart1 + (num - 11) / 12;
      }
      int daysInMonth = this.GetDaysInMonth(year, month);
      if (day > daysInMonth)
        day = daysInMonth;
      long ticks = this.GetAbsoluteDatePersian(year, month, day) * 864000000000L + time.Ticks % 864000000000L;
      Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" />, который смещен от указанного объекта <see cref="T:System.DateTime" /> на заданное число лет.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются годы.
    /// </param>
    /// <param name="years">
    ///   Положительное или отрицательное число лет для добавления.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" />, полученный в результате добавления указанного числа лет к указанному объекту <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="years" /> меньше параметр больше или равно 10 000.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>
    ///   Возвращает день месяца в заданном объекте <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 31, обозначающее день месяца в заданном объекте <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> Параметр представляет дату меньше <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    /// <summary>
    ///   Возвращает день недели из заданного объекта <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.DayOfWeek" />, представляющее день недели в заданном объекте <see cref="T:System.DateTime" />.
    /// </returns>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>
    ///   Возвращает день года в заданном объекте <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 366, обозначающее день года в заданном объекте <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> Параметр представляет дату меньше <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 1);
    }

    /// <summary>
    ///   Возвращает число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="month">
    ///   Целое число, представляющее код, в диапазоне от 1 до 12, если параметр <paramref name="year" /> не равен 9378, или от 1 до 10, если параметр <paramref name="year" /> равен 9378.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      PersianCalendar.CheckYearMonthRange(year, month, era);
      if (month == 10 && year == 9378)
        return 13;
      int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
      if (month == 12 && !this.IsLeapYear(year))
        --num;
      return num;
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Число дней в указанном году указанной эры.
    ///    Возвращенное число дней равно 365 дням для обычного года или 366 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378)
        return PersianCalendar.DaysToMonth[9] + 13;
      return !this.IsLeapYear(year, 0) ? 365 : 366;
    }

    /// <summary>
    ///   Возвращает значение эры в заданном объекте <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> Параметр представляет дату меньше <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetEra(DateTime time)
    {
      PersianCalendar.CheckTicksRange(time.Ticks);
      return PersianCalendar.PersianEra;
    }

    /// <summary>
    ///   Получает список эр в объекте <see cref="T:System.Globalization.PersianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в объекте <see cref="T:System.Globalization.PersianCalendar" />.
    ///    Этот массив состоит из одного элемента, значение которого равно <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ PersianCalendar.PersianEra };
      }
    }

    /// <summary>
    ///   Возвращает значение месяца в заданном объекте <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 12, обозначающее месяц в заданном объекте <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> Параметр представляет дату меньше <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Возвращает 10, если параметр <paramref name="year" /> равен 9378; в противном случае всегда возвращает 12.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      return year == 9378 ? 10 : 12;
    }

    /// <summary>
    ///   Возвращает значение года в заданном объекте <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 9378, обозначающее год в заданном объекте <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> Параметр представляет дату меньше <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>Определяет, является ли указанный день високосным.</summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="month">
    ///   Целое число, представляющее код, в диапазоне от 1 до 12, если параметр <paramref name="year" /> не равен 9378, или от 1 до 10, если параметр <paramref name="year" /> равен 9378.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
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

    /// <summary>
    ///   Возвращает високосный месяц для заданных года и эры.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год для преобразования.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>Возвращаемое значение всегда равно 0.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetLeapMonth(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="month">
    ///   Целое число, представляющее код, в диапазоне от 1 до 12, если параметр <paramref name="year" /> не равен 9378, или от 1 до 10, если параметр <paramref name="year" /> равен 9378.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Всегда возвращает <see langword="false" />, потому что класс <see cref="T:System.Globalization.PersianCalendar" /> не поддерживает понятие високосного года.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      PersianCalendar.CheckYearMonthRange(year, month, era);
      return false;
    }

    /// <summary>
    ///   Определяет, является ли указанный год указанной эры високосным годом.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный год — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378)
        return false;
      return this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" /> с заданными значениями даты и времени в заданной эре.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год.
    /// </param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
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
    /// <param name="era">
    ///   Целое число от 0 до 1, представляющее эру.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.DateTime" /> с заданными значениями даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
      if (absoluteDatePersian >= 0L)
        return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
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
    ///   Значение в наборе операций меньше 100 или больше 9378.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9378)
          throw new ArgumentOutOfRangeException(nameof (value), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9378));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначное представление года.
    /// </summary>
    /// <param name="year">
    ///   Целое число от 1 до 9378, представляющее год для преобразования.
    /// </param>
    /// <returns>
    ///   Целое число, содержащее четырехразрядное представление <paramref name="year" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> значение меньше 0 или больше 9378.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 9378)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9378));
      return year;
    }
  }
}
