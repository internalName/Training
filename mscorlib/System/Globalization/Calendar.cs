// Decompiled with JetBrains decompiler
// Type: System.Globalization.Calendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>
  ///   Представляет время в виде раздельных значений, например недель, месяцев и лет.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Calendar : ICloneable
  {
    internal int m_currentEraValue = -1;
    internal int twoDigitYearMax = -1;
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
    internal const int CAL_GREGORIAN = 1;
    internal const int CAL_GREGORIAN_US = 2;
    internal const int CAL_JAPAN = 3;
    internal const int CAL_TAIWAN = 4;
    internal const int CAL_KOREA = 5;
    internal const int CAL_HIJRI = 6;
    internal const int CAL_THAI = 7;
    internal const int CAL_HEBREW = 8;
    internal const int CAL_GREGORIAN_ME_FRENCH = 9;
    internal const int CAL_GREGORIAN_ARABIC = 10;
    internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;
    internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;
    internal const int CAL_JULIAN = 13;
    internal const int CAL_JAPANESELUNISOLAR = 14;
    internal const int CAL_CHINESELUNISOLAR = 15;
    internal const int CAL_SAKA = 16;
    internal const int CAL_LUNAR_ETO_CHN = 17;
    internal const int CAL_LUNAR_ETO_KOR = 18;
    internal const int CAL_LUNAR_ETO_ROKUYOU = 19;
    internal const int CAL_KOREANLUNISOLAR = 20;
    internal const int CAL_TAIWANLUNISOLAR = 21;
    internal const int CAL_PERSIAN = 22;
    internal const int CAL_UMALQURA = 23;
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly;
    /// <summary>Представляет текущую эру для текущего календаря.</summary>
    [__DynamicallyInvokable]
    public const int CurrentEra = 0;

    /// <summary>
    ///   Возвращает минимальное значение даты и времени, поддерживаемое данным <see cref="T:System.Globalization.Calendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Самая ранняя дата и время, поддерживаемые этим календарем.
    ///    Значение по умолчанию — <see cref="F:System.DateTime.MinValue" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual DateTime MinSupportedDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>
    ///   Возвращает последнюю дату и время, поддерживаемые этим <see cref="T:System.Globalization.Calendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Последние дата и время, поддерживаемые этим календарем.
    ///    Значение по умолчанию — <see cref="F:System.DateTime.MaxValue" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual DateTime MaxSupportedDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.MaxValue;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.Calendar" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Calendar()
    {
    }

    internal virtual int ID
    {
      get
      {
        return -1;
      }
    }

    internal virtual int BaseCalendarID
    {
      get
      {
        return this.ID;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Globalization.CalendarAlgorithmType" />.
    /// </returns>
    [ComVisible(false)]
    public virtual CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.Unknown;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это <see cref="T:System.Globalization.Calendar" /> объект доступен только для чтения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот <see cref="T:System.Globalization.Calendar" /> объект только для чтения; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего <see cref="T:System.Globalization.Calendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Object" /> является копией текущего <see cref="T:System.Globalization.Calendar" /> объекта.
    /// </returns>
    [ComVisible(false)]
    public virtual object Clone()
    {
      object obj = this.MemberwiseClone();
      ((Calendar) obj).SetReadOnlyState(false);
      return obj;
    }

    /// <summary>
    ///   Возвращает только для чтения версию указанного <see cref="T:System.Globalization.Calendar" /> объекта.
    /// </summary>
    /// <param name="calendar">
    ///   Объект <see cref="T:System.Globalization.Calendar" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Globalization.Calendar" /> Объекта, заданного параметром <paramref name="calendar" /> параметр, если <paramref name="calendar" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Только для чтения копией по <see cref="T:System.Globalization.Calendar" /> объекта, заданного параметром <paramref name="calendar" />, если <paramref name="calendar" /> не только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="calendar" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    public static Calendar ReadOnly(Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException(nameof (calendar));
      if (calendar.IsReadOnly)
        return calendar;
      Calendar calendar1 = (Calendar) calendar.MemberwiseClone();
      calendar1.SetReadOnlyState(true);
      return calendar1;
    }

    internal void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal void SetReadOnlyState(bool readOnly)
    {
      this.m_isReadOnly = readOnly;
    }

    internal virtual int CurrentEraValue
    {
      get
      {
        if (this.m_currentEraValue == -1)
          this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
        return this.m_currentEraValue;
      }
    }

    internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
    {
      if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Argument_ResultCalendarRange"), (object) minValue, (object) maxValue));
    }

    internal DateTime Add(DateTime time, double value, int scale)
    {
      double num1 = value * (double) scale + (value >= 0.0 ? 0.5 : -0.5);
      if (num1 <= -315537897600000.0 || num1 >= 315537897600000.0)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
      long num2 = (long) num1;
      long ticks = time.Ticks + num2 * 10000L;
      Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> именно указанного числа миллисекунд из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> Для добавления в миллисекундах.
    /// </param>
    /// <param name="milliseconds">
    ///   Количество миллисекунд для добавления.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанного числа миллисекунд к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="milliseconds" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
    {
      return this.Add(time, milliseconds, 1);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> указанного число дней из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому добавляются дни.
    /// </param>
    /// <param name="days">Число дней для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанного количества дней в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="days" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddDays(DateTime time, int days)
    {
      return this.Add(time, (double) days, 86400000);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> именно указанное число часов из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому добавляются часы.
    /// </param>
    /// <param name="hours">Количество часов для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число часов к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="hours" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddHours(DateTime time, int hours)
    {
      return this.Add(time, (double) hours, 3600000);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> именно указанное число минут из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому добавляются минуты.
    /// </param>
    /// <param name="minutes">Число минут для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число минут к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="minutes" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddMinutes(DateTime time, int minutes)
    {
      return this.Add(time, (double) minutes, 60000);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает <see cref="T:System.DateTime" /> это заданное число месяцев из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются месяцы.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанное число месяцев в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="months" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract DateTime AddMonths(DateTime time, int months);

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> именно указанное число секунд из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому добавляются секунды.
    /// </param>
    /// <param name="seconds">Число секунд для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число секунд к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="seconds" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddSeconds(DateTime time, int seconds)
    {
      return this.Add(time, (double) seconds, 1000);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> указанное число недель из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому добавляются недели.
    /// </param>
    /// <param name="weeks">Число недель.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанного количества недель в указанный <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="weeks" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddWeeks(DateTime time, int weeks)
    {
      return this.AddDays(time, weeks * 7);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает <see cref="T:System.DateTime" /> это заданное число лет из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются годы.
    /// </param>
    /// <param name="years">Число лет для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число лет к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами поддерживаемого диапазона значений данного календаря.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="years" /> находится за пределами поддерживаемого диапазона <see cref="T:System.DateTime" /> возвращаемое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract DateTime AddYears(DateTime time, int years);

    /// <summary>
    ///   При переопределении в производном классе возвращает день месяца в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Положительное целое число, представляющее день месяца в <paramref name="time" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int GetDayOfMonth(DateTime time);

    /// <summary>
    ///   При переопределении в производном классе возвращает день недели в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DayOfWeek" /> значение, представляющее день недели в <paramref name="time" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract DayOfWeek GetDayOfWeek(DateTime time);

    /// <summary>
    ///   При переопределении в производном классе возвращает день года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Положительное целое число, представляющее день года в <paramref name="time" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int GetDayOfYear(DateTime time);

    /// <summary>
    ///   Возвращает число дней в указанный месяц и год текущей эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetDaysInMonth(int year, int month)
    {
      return this.GetDaysInMonth(year, month, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает число дней в указанном месяце, года и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
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
    [__DynamicallyInvokable]
    public abstract int GetDaysInMonth(int year, int month, int era);

    /// <summary>Возвращает число дней в указанном году текущей эры.</summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <returns>Число дней в указанном году текущей эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetDaysInYear(int year)
    {
      return this.GetDaysInYear(year, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает число дней в указанном году указанной эры.
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
    [__DynamicallyInvokable]
    public abstract int GetDaysInYear(int year, int era);

    /// <summary>
    ///   При переопределении в производном классе, возвращает значение эры в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int GetEra(DateTime time);

    /// <summary>
    ///   При переопределении в производном классе возвращает список эр в текущем календаре.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эр в текущем календаре.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int[] Eras { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение часов в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 0 до 23, представляющее час в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int GetHour(DateTime time)
    {
      return (int) (time.Ticks / 36000000000L % 24L);
    }

    /// <summary>
    ///   Возвращает значение миллисекунд в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Значение двойной точности с плавающей запятой от 0 до 999, представляющее миллисекунды в <paramref name="time" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual double GetMilliseconds(DateTime time)
    {
      return (double) (time.Ticks / 10000L % 1000L);
    }

    /// <summary>
    ///   Возвращает значение минут в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 0 до 59, представляющее минуты в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int GetMinute(DateTime time)
    {
      return (int) (time.Ticks / 600000000L % 60L);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает месяц в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Положительное целое число, представляющее месяц в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int GetMonth(DateTime time);

    /// <summary>
    ///   Возвращает число месяцев в указанном году текущей эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <returns>Число месяцев в указанном году текущей эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetMonthsInYear(int year)
    {
      return this.GetMonthsInYear(year, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает число месяцев в указанном году указанной эры.
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
    [__DynamicallyInvokable]
    public abstract int GetMonthsInYear(int year, int era);

    /// <summary>
    ///   Возвращает значение секунд в указанном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 0 до 59, представляющее секунды в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int GetSecond(DateTime time)
    {
      return (int) (time.Ticks / 10000000L % 60L);
    }

    internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
    {
      int num1 = this.GetDayOfYear(time) - 1;
      int num2 = (int) (this.GetDayOfWeek(time) - num1 % 7 - firstDayOfWeek + 14) % 7;
      return (num1 + num2) / 7 + 1;
    }

    private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
    {
      int num1 = this.GetDayOfYear(time) - 1;
      int num2 = (int) (this.GetDayOfWeek(time) - num1 % 7);
      int num3 = (firstDayOfWeek - num2 + 14) % 7;
      if (num3 != 0 && num3 >= fullDays)
        num3 -= 7;
      int num4 = num1 - num3;
      if (num4 >= 0)
        return num4 / 7 + 1;
      if (time <= this.MinSupportedDateTime.AddDays((double) num1))
        return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
      return this.GetWeekOfYearFullDays(time.AddDays((double) -(num1 + 1)), firstDayOfWeek, fullDays);
    }

    private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
    {
      int num1 = (int) (this.GetDayOfWeek(this.MinSupportedDateTime) - (this.GetDayOfYear(this.MinSupportedDateTime) - 1) % 7);
      int num2 = (firstDayOfWeek + 7 - num1) % 7;
      if (num2 == 0 || num2 >= minimumDaysInFirstWeek)
        return 1;
      int num3 = this.DaysInYearBeforeMinSupportedYear - 1;
      int num4 = num1 - 1 - num3 % 7;
      int num5 = (firstDayOfWeek - num4 + 14) % 7;
      int num6 = num3 - num5;
      if (num5 >= minimumDaysInFirstWeek)
        num6 += 7;
      return num6 / 7 + 1;
    }

    /// <summary>
    ///   Возвращает число дней в году, предшествующий год, который задается параметром <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> свойство.
    /// </summary>
    /// <returns>
    ///   Число дней в году, который предшествует года в заданном по <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" />.
    /// </returns>
    protected virtual int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 365;
      }
    }

    /// <summary>
    ///   Возвращает неделю года, к которой относится дата в заданном <see cref="T:System.DateTime" /> значение.
    /// </summary>
    /// <param name="time">Значение даты и времени.</param>
    /// <param name="rule">
    ///   Значение перечисления, определяющее календарную неделю.
    /// </param>
    /// <param name="firstDayOfWeek">
    ///   Значение перечисления, представляющее первый день недели.
    /// </param>
    /// <returns>
    ///   Положительное целое число, представляющее неделю года, к которой относится дата в <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> более ранняя, чем <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> или более поздней, чем <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="firstDayOfWeek" /> не является допустимым значением <see cref="T:System.DayOfWeek" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="rule" /> не является допустимым значением <see cref="T:System.Globalization.CalendarWeekRule" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      switch (firstDayOfWeek)
      {
        case DayOfWeek.Sunday:
        case DayOfWeek.Monday:
        case DayOfWeek.Tuesday:
        case DayOfWeek.Wednesday:
        case DayOfWeek.Thursday:
        case DayOfWeek.Friday:
        case DayOfWeek.Saturday:
          switch (rule)
          {
            case CalendarWeekRule.FirstDay:
              return this.GetFirstDayWeekOfYear(time, (int) firstDayOfWeek);
            case CalendarWeekRule.FirstFullWeek:
              return this.GetWeekOfYearFullDays(time, (int) firstDayOfWeek, 7);
            case CalendarWeekRule.FirstFourDayWeek:
              return this.GetWeekOfYearFullDays(time, (int) firstDayOfWeek, 4);
            default:
              throw new ArgumentOutOfRangeException(nameof (rule), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) CalendarWeekRule.FirstDay, (object) CalendarWeekRule.FirstFourDayWeek));
          }
        default:
          throw new ArgumentOutOfRangeException(nameof (firstDayOfWeek), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает год в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в <paramref name="time" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int GetYear(DateTime time);

    /// <summary>
    ///   Определяет, является ли указанная дата текущей эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Положительное целое число, представляющее день.
    /// </param>
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
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapDay(int year, int month, int day)
    {
      return this.IsLeapDay(year, month, day, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе определяет, является ли указанная дата указанной эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Положительное целое число, представляющее день.
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
    [__DynamicallyInvokable]
    public abstract bool IsLeapDay(int year, int month, int day, int era);

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный месяц — високосный; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapMonth(int year, int month)
    {
      return this.IsLeapMonth(year, month, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see langword="true" /> Если указанный месяц — високосный; в противном случае — <see langword="false" />.
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
    [__DynamicallyInvokable]
    public abstract bool IsLeapMonth(int year, int month, int era);

    /// <summary>Вычисляет високосный месяц для заданных года.</summary>
    /// <param name="year">Год.</param>
    /// <returns>
    ///   Положительное целое число, указывающее високосный месяц в указанном году.
    /// 
    ///   -или-
    /// 
    ///   Нуль, если этот календарь не поддерживает високосные месяцы, или если <paramref name="year" /> параметр представляет високосным годом.
    /// </returns>
    [ComVisible(false)]
    public virtual int GetLeapMonth(int year)
    {
      return this.GetLeapMonth(year, 0);
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">Эра.</param>
    /// <returns>
    ///   Положительное целое число, указывающее високосный месяц в указанном году указанной эры.
    /// 
    ///   -или-
    /// 
    ///   Нуль, если этот календарь не поддерживает високосные месяцы, или если <paramref name="year" /> и <paramref name="era" /> параметров не указан високосный год.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int GetLeapMonth(int year, int era)
    {
      if (!this.IsLeapYear(year, era))
        return 0;
      int monthsInYear = this.GetMonthsInYear(year, era);
      for (int month = 1; month <= monthsInYear; ++month)
      {
        if (this.IsLeapMonth(year, month, era))
          return month;
      }
      return 0;
    }

    /// <summary>
    ///   Определяет, является ли указанный год текущей эры високосным годом.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный год — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapYear(int year)
    {
      return this.IsLeapYear(year, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе определяет, является ли указанный год указанной эры високосным годом.
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
    [__DynamicallyInvokable]
    public abstract bool IsLeapYear(int year, int era);

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеет значение указанной даты и времени в текущей эре.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Положительное целое число, представляющее день.
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
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает <see cref="T:System.DateTime" /> имеет значение указанной даты и времени в заданной эре.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Положительное целое число, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Положительное целое число, представляющее день.
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
    [__DynamicallyInvokable]
    public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

    internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
    {
      result = DateTime.MinValue;
      try
      {
        result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
        return true;
      }
      catch (ArgumentException ex)
      {
        return false;
      }
    }

    internal virtual bool IsValidYear(int year, int era)
    {
      if (year >= this.GetYear(this.MinSupportedDateTime))
        return year <= this.GetYear(this.MaxSupportedDateTime);
      return false;
    }

    internal virtual bool IsValidMonth(int year, int month, int era)
    {
      if (this.IsValidYear(year, era) && month >= 1)
        return month <= this.GetMonthsInYear(year, era);
      return false;
    }

    internal virtual bool IsValidDay(int year, int month, int day, int era)
    {
      if (this.IsValidMonth(year, month, era) && day >= 1)
        return day <= this.GetDaysInMonth(year, month, era);
      return false;
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Globalization.Calendar" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int TwoDigitYearMax
    {
      [__DynamicallyInvokable] get
      {
        return this.twoDigitYearMax;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> Свойства для определения века.
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
    [__DynamicallyInvokable]
    public virtual int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return (this.TwoDigitYearMax / 100 - (year > this.TwoDigitYearMax % 100 ? 1 : 0)) * 100 + year;
      return year;
    }

    internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
    {
      if (hour < 0 || hour >= 24 || (minute < 0 || minute >= 60) || (second < 0 || second >= 60))
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 999));
      return TimeSpan.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
    }

    [SecuritySafeCritical]
    internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
    {
      int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
      if (num < 0)
        num = defaultYearValue;
      return num;
    }
  }
}
