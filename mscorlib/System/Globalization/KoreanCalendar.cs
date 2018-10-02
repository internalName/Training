// Decompiled with JetBrains decompiler
// Type: System.Globalization.KoreanCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>Представляет корейский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class KoreanCalendar : Calendar
  {
    internal static EraInfo[] koreanEraInfo = new EraInfo[1]
    {
      new EraInfo(1, 1, 1, 1, -2333, 2334, 12332)
    };
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public const int KoreanEra = 1;
    internal GregorianCalendarHelper helper;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 4362;

    /// <summary>
    ///   Получает самые ранние дату и время, поддерживаемые классом <see cref="T:System.Globalization.KoreanCalendar" />.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.KoreanCalendar" /> класс, который эквивалентен первый момент 1 января 0001 года нашей эры в григорианском календаре.
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
    ///   Получает самые последние дату и время, поддерживаемые классом <see cref="T:System.Globalization.KoreanCalendar" />.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.KoreanCalendar" /> класс, который эквивалентен последний момент 31 декабря 9999 года нашей эры в григорианском календаре.
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.KoreanCalendar" />.
    /// </summary>
    /// <exception cref="T:System.TypeInitializationException">
    ///   Не удалось инициализировать <see cref="T:System.Globalization.KoreanCalendar" /> объект из-за отсутствия язык и региональные параметры.
    /// </exception>
    public KoreanCalendar()
    {
      try
      {
        CultureInfo cultureInfo = new CultureInfo("ko-KR");
      }
      catch (ArgumentException ex)
      {
        throw new TypeInitializationException(this.GetType().FullName, (Exception) ex);
      }
      this.helper = new GregorianCalendarHelper((Calendar) this, KoreanCalendar.koreanEraInfo);
    }

    internal override int ID
    {
      get
      {
        return 5;
      }
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
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="months" /> — меньше -120000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="months" /> больше 120000.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      return this.helper.AddMonths(time, months);
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
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="years" /> или <paramref name="time" /> выходит за пределы диапазона.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.helper.AddYears(time, years);
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
    public override int GetDaysInMonth(int year, int month, int era)
    {
      return this.helper.GetDaysInMonth(year, month, era);
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      return this.helper.GetDaysInYear(year, era);
    }

    /// <summary>
    ///   Возвращает день месяца в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 31, представляющее день месяца в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.helper.GetDayOfMonth(time);
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
      return this.helper.GetDayOfWeek(time);
    }

    /// <summary>
    ///   Возвращает день года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 366, обозначающее день года в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfYear(DateTime time)
    {
      return this.helper.GetDayOfYear(time);
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число месяцев в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      return this.helper.GetMonthsInYear(year, era);
    }

    /// <summary>
    ///   Возвращает неделю года, к которой относится дата в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <param name="rule">
    ///   Один из <see cref="T:System.Globalization.CalendarWeekRule" /> значений, определяющее календарную неделю.
    /// </param>
    /// <param name="firstDayOfWeek">
    ///   Один из <see cref="T:System.DayOfWeek" /> значений, представляющее первый день недели.
    /// </param>
    /// <returns>
    ///   На основе 1 целое число, представляющее неделю года, к которой относится дата в <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> или <paramref name="firstDayOfWeek" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="rule" /> не является допустимым значением <see cref="T:System.Globalization.CalendarWeekRule" />.
    /// </exception>
    [ComVisible(false)]
    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
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
      return this.helper.GetEra(time);
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
      return this.helper.GetMonth(time);
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
      return this.helper.GetYear(time);
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
      return this.helper.IsLeapDay(year, month, day, era);
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
      return this.helper.IsLeapYear(year, era);
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">Эра.</param>
    /// <returns>
    ///   Возвращаемое значение всегда равно 0 из-за <see cref="T:System.Globalization.KoreanCalendar" /> класс не поддерживает понятие високосного года.
    /// </returns>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      return this.helper.GetLeapMonth(year, era);
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
      return this.helper.IsLeapMonth(year, month, era);
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
      return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.KoreanCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.KoreanCalendar" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return this.helper.Eras;
      }
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
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 4362);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.helper.MaxYear)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) this.helper.MaxYear));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.KoreanCalendar.TwoDigitYearMax" /> Свойства для определения века.
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
      return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
    }
  }
}
