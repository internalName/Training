// Decompiled with JetBrains decompiler
// Type: System.Globalization.GregorianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>Представляет григорианский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class GregorianCalendar : Calendar
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
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public const int ADEra = 1;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MaxYear = 9999;
    internal GregorianCalendarTypes m_type;
    private static volatile Calendar s_defaultInstance;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 2029;

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_type < GregorianCalendarTypes.Localized || this.m_type > GregorianCalendarTypes.TransliteratedFrench)
        throw new SerializationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_MemberOutOfRange"), (object) "type", (object) nameof (GregorianCalendar)));
    }

    /// <summary>
    ///   Возвращает минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.GregorianCalendar" /> тип.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.GregorianCalendar" /> тип, который первый момент 1 января 0001 года нашей эры и эквивалентен <see cref="F:System.DateTime.MinValue" />.
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
    ///   Возвращает последнюю дату и время, поддерживаемые <see cref="T:System.Globalization.GregorianCalendar" /> тип.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.GregorianCalendar" /> тип, который является самый последний момент 31 декабря 9999 года нашей эры и эквивалентен <see cref="F:System.DateTime.MaxValue" />.
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

    internal static Calendar GetDefaultInstance()
    {
      if (GregorianCalendar.s_defaultInstance == null)
        GregorianCalendar.s_defaultInstance = (Calendar) new GregorianCalendar();
      return GregorianCalendar.s_defaultInstance;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Globalization.GregorianCalendar" /> класса по умолчанию <see cref="T:System.Globalization.GregorianCalendarTypes" /> значение.
    /// </summary>
    public GregorianCalendar()
      : this(GregorianCalendarTypes.Localized)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Globalization.GregorianCalendar" /> класса с помощью указанного <see cref="T:System.Globalization.GregorianCalendarTypes" /> значения.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Globalization.GregorianCalendarTypes" /> Указывает, какую языковую версию календаря следует создать.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="type" /> не является членом <see cref="T:System.Globalization.GregorianCalendarTypes" /> перечисления.
    /// </exception>
    public GregorianCalendar(GregorianCalendarTypes type)
    {
      if (type < GregorianCalendarTypes.Localized || type > GregorianCalendarTypes.TransliteratedFrench)
        throw new ArgumentOutOfRangeException(nameof (type), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) GregorianCalendarTypes.Localized, (object) GregorianCalendarTypes.TransliteratedFrench));
      this.m_type = type;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Globalization.GregorianCalendarTypes" /> значение, определяющее языковую версию текущего <see cref="T:System.Globalization.GregorianCalendar" />.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Globalization.GregorianCalendarTypes" /> значение, определяющее языковую версию текущего <see cref="T:System.Globalization.GregorianCalendar" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, указанное в наборе операций не является членом <see cref="T:System.Globalization.GregorianCalendarTypes" /> перечисления.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В наборе операций текущим экземпляром доступно только для чтения.
    /// </exception>
    public virtual GregorianCalendarTypes CalendarType
    {
      get
      {
        return this.m_type;
      }
      set
      {
        this.VerifyWritable();
        switch (value)
        {
          case GregorianCalendarTypes.Localized:
          case GregorianCalendarTypes.USEnglish:
          case GregorianCalendarTypes.MiddleEastFrench:
          case GregorianCalendarTypes.Arabic:
          case GregorianCalendarTypes.TransliteratedEnglish:
          case GregorianCalendarTypes.TransliteratedFrench:
            this.m_type = value;
            break;
          default:
            throw new ArgumentOutOfRangeException("m_type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
    }

    internal override int ID
    {
      get
      {
        return (int) this.m_type;
      }
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
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
      int[] numArray = num8 == 3 && (num6 != 24 || num4 == 3) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
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
        int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
        if (day >= 1 && day <= numArray[month] - numArray[month - 1])
        {
          int num = year - 1;
          return (long) (num * 365 + num / 4 - num / 100 + num / 400 + numArray[month - 1] + day - 1);
        }
      }
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    internal virtual long DateToTicks(int year, int month, int day)
    {
      return GregorianCalendar.GetAbsoluteDate(year, month, day) * 864000000000L;
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
      int year1;
      int month;
      int day;
      time.GetDatePart(out year1, out month, out day);
      int num1 = month - 1 + months;
      int year2;
      if (num1 >= 0)
      {
        month = num1 % 12 + 1;
        year2 = year1 + num1 / 12;
      }
      else
      {
        month = 12 + (num1 + 1) % 12;
        year2 = year1 + (num1 - 11) / 12;
      }
      int[] numArray = year2 % 4 != 0 || year2 % 100 == 0 && year2 % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
      int num2 = numArray[month] - numArray[month - 1];
      if (day > num2)
        day = num2;
      long ticks = this.DateToTicks(year2, month, day) + time.Ticks % 864000000000L;
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
      return time.Day;
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
      return time.DayOfYear;
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
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 9999));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
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
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      return year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? 365 : 366;
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
      return 1;
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.GregorianCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.GregorianCalendar" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ 1 };
      }
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
      return time.Month;
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
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year >= 1 && year <= 9999)
        return 12;
      throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
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
      return time.Year;
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
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 12));
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 9999));
      if (day < 1 || day > this.GetDaysInMonth(year, month))
        throw new ArgumentOutOfRangeException(nameof (day), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) this.GetDaysInMonth(year, month)));
      return this.IsLeapYear(year) && month == 2 && day == 29;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Укажите либо <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> или <see langword="GregorianCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   Всегда равно 0 поскольку григорианский календарь не распознает месяцев високосным.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> меньше, чем год по григорианскому календарю 1 или больше года 9999.
    /// 
    ///   -или-
    /// 
    ///   Свойству <paramref name="era" /> задано значение, отличное от <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> или <see langword="GregorianCalendar.Eras[Calendar.CurrentEra]" />.
    /// </exception>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
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
    public override bool IsLeapMonth(int year, int month, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 12));
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
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
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
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
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
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      if (era == 0 || era == 1)
        return new DateTime(year, month, day, hour, minute, second, millisecond);
      throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal override bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
    {
      if (era == 0 || era == 1)
        return DateTime.TryCreate(year, month, day, hour, minute, second, millisecond, out result);
      result = DateTime.MinValue;
      return false;
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
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2029);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9999)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9999));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.GregorianCalendar.TwoDigitYearMax" /> Свойства для определения века.
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
      if (year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      return base.ToFourDigitYear(year);
    }
  }
}
