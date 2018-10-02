// Decompiled with JetBrains decompiler
// Type: System.Globalization.JulianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>Представляет юлианский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class JulianCalendar : Calendar
  {
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public static readonly int JulianEra = 1;
    private static readonly int[] DaysToMonth365 = new int[13]
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
    private static readonly int[] DaysToMonth366 = new int[13]
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
    internal int MaxYear = 9999;
    private const int DatePartYear = 0;
    private const int DatePartDayOfYear = 1;
    private const int DatePartMonth = 2;
    private const int DatePartDay = 3;
    private const int JulianDaysPerYear = 365;
    private const int JulianDaysPer4Years = 1461;

    /// <summary>
    ///   Получает самые ранние дату и время, поддерживаемые классом <see cref="T:System.Globalization.JulianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.JulianCalendar" /> класс, который эквивалентен первый момент 1 января 0001 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>
    ///   Получает самые последние дату и время, поддерживаемые классом <see cref="T:System.Globalization.JulianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.JulianCalendar" /> класс, который эквивалентен последний момент 31 декабря 9999 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return DateTime.MaxValue;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.
    /// </returns>
    [ComVisible(false)]
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.SolarCalendar;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.JulianCalendar" />.
    /// </summary>
    public JulianCalendar()
    {
      this.twoDigitYearMax = 2029;
    }

    internal override int ID
    {
      get
      {
        return 13;
      }
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != JulianCalendar.JulianEra)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal void CheckYearEraRange(int year, int era)
    {
      JulianCalendar.CheckEraRange(era);
      if (year <= 0 || year > this.MaxYear)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) this.MaxYear));
    }

    internal static void CheckMonthRange(int month)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    internal static void CheckDayRange(int year, int month, int day)
    {
      if (year == 1 && month == 1 && day < 3)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int num = numArray[month] - numArray[month - 1];
      if (day < 1 || day > num)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) num));
    }

    internal static int GetDatePart(long ticks, int part)
    {
      int num1 = (int) ((ticks + 1728000000000L) / 864000000000L);
      int num2 = num1 / 1461;
      int num3 = num1 - num2 * 1461;
      int num4 = num3 / 365;
      if (num4 == 4)
        num4 = 3;
      if (part == 0)
        return num2 * 4 + num4 + 1;
      int num5 = num3 - num4 * 365;
      if (part == 1)
        return num5 + 1;
      int[] numArray = num4 == 3 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int index = num5 >> 6;
      while (num5 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num5 - numArray[index - 1] + 1;
    }

    internal static long DateToTicks(int year, int month, int day)
    {
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int num = year - 1;
      return (long) (num * 365 + num / 4 + numArray[month - 1] + day - 1 - 2) * 864000000000L;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число месяцев из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются месяцы.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанное число месяцев в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
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
      int datePart1 = JulianCalendar.GetDatePart(time.Ticks, 0);
      int datePart2 = JulianCalendar.GetDatePart(time.Ticks, 2);
      int day = JulianCalendar.GetDatePart(time.Ticks, 3);
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
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? JulianCalendar.DaysToMonth365 : JulianCalendar.DaysToMonth366;
      int num2 = numArray[month] - numArray[month - 1];
      if (day > num2)
        day = num2;
      long ticks = JulianCalendar.DateToTicks(year, month, day) + time.Ticks % 864000000000L;
      Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число лет из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются годы.
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
    ///   Целое число от 1 до 31, представляющее день месяца в <paramref name="time" />.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 3);
    }

    /// <summary>
    ///   Возвращает день недели в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DayOfWeek" /> значение, представляющее день недели в <paramref name="time" />.
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
    ///   Целое число от 1 до 366, обозначающее день года в <paramref name="time" />.
    /// </returns>
    public override int GetDayOfYear(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 1);
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
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      return numArray[month] - numArray[month - 1];
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      return !this.IsLeapYear(year, era) ? 365 : 366;
    }

    /// <summary>
    ///   Возвращает значение эры в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в <paramref name="time" />.
    /// </returns>
    public override int GetEra(DateTime time)
    {
      return JulianCalendar.JulianEra;
    }

    /// <summary>
    ///   Возвращает месяц в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 12, представляющее месяц в <paramref name="time" />.
    /// </returns>
    public override int GetMonth(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 2);
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.JulianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.JulianCalendar" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ JulianCalendar.JulianEra };
      }
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число месяцев в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      this.CheckYearEraRange(year, era);
      return 12;
    }

    /// <summary>
    ///   Возвращает значение года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в <paramref name="time" />.
    /// </returns>
    public override int GetYear(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 0);
    }

    /// <summary>
    ///   Определяет, является ли указанная дата указанной эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      JulianCalendar.CheckMonthRange(month);
      if (this.IsLeapYear(year, era))
      {
        JulianCalendar.CheckDayRange(year, month, day);
        if (month == 2)
          return day == 29;
        return false;
      }
      JulianCalendar.CheckDayRange(year, month, day);
      return false;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Положительное целое число, указывающее високосный месяц в указанном году указанной эры.
    ///    Кроме того, этот метод возвращает нуль, если календарь не поддерживает високосные месяцы или <paramref name="year" /> и <paramref name="era" /> не указан високосный год.
    /// </returns>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      this.CheckYearEraRange(year, era);
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
    ///   Этот метод всегда возвращает <see langword="false" />, если не переопределено в производном классе.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
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
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      this.CheckYearEraRange(year, era);
      return year % 4 == 0;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеет значение указанной даты и времени в заданной эре.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
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
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Имеет значение указанной даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится вне диапазона, поддерживаемого календарем.
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
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
      JulianCalendar.CheckDayRange(year, month, day);
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 999));
      if (hour >= 0 && hour < 24 && (minute >= 0 && minute < 60) && (second >= 0 && second < 60))
        return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, указанное в наборе операций — до 99.
    /// 
    ///   -или-
    /// 
    ///   Значение, указанное в наборе операций больше, чем <see langword="MaxSupportedDateTime.Year" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В наборе операций текущим экземпляром доступно только для чтения.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.MaxYear)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) this.MaxYear));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.JulianCalendar.TwoDigitYearMax" /> Свойства для определения века.
    /// </summary>
    /// <param name="year">
    ///   Двузначное или четырехзначное целое число, представляющее год для преобразования.
    /// </param>
    /// <returns>
    ///   Целое число, содержащее четырехразрядное представление <paramref name="year" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year > this.MaxYear)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) this.MaxYear));
      return base.ToFourDigitYear(year);
    }
  }
}
