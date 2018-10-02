// Decompiled with JetBrains decompiler
// Type: System.DateTimeOffset
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет момент времени, который обычно выражается в виде даты и времени суток, относительно времени в формате UTC.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct DateTimeOffset : IComparable, IFormattable, ISerializable, IDeserializationCallback, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
  {
    /// <summary>
    ///   Представляет самой ранней возможной <see cref="T:System.DateTimeOffset" /> значение.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.DateTimeOffset" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <see cref="F:System.DateTime.MaxValue" />выходит за пределы текущей или заданной календарь культуры по умолчанию.
    /// </exception>
    [__DynamicallyInvokable]
    public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);
    internal const long MaxOffset = 504000000000;
    internal const long MinOffset = -504000000000;
    private const long UnixEpochTicks = 621355968000000000;
    private const long UnixEpochSeconds = 62135596800;
    private const long UnixEpochMilliseconds = 62135596800000;
    private DateTime m_dateTime;
    private short m_offsetMinutes;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" /> с использованием заданного количества тактов и смещения.
    /// </summary>
    /// <param name="ticks">
    ///   Дата и время, представленные в виде числа 100-наносекундных интервалов, прошедших с 00:00 1 января 0001 г.
    /// </param>
    /// <param name="offset">
    ///   Смещение по времени от времени в формате UTC.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> не следует указывать в целых минутах.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <see cref="P:System.DateTimeOffset.UtcDateTime" /> является более ранним, чем <see cref="F:System.DateTimeOffset.MinValue" />, или более поздним, чем <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="ticks" /> меньше <see langword="DateTimeOffset.MinValue.Ticks" /> или больше <see langword="DateTimeOffset.MaxValue.Ticks" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="Offset" /> меньше -14 часов или больше 14 часов.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(long ticks, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(ticks), offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" /> с использованием заданного значения <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="dateTime">Дата и время.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время в формате UTC, которые получаются в результате применения смещения, более раннего чем <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Дата и время в формате UTC, которые получаются в результате применения смещения, более позднего чем <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(DateTime dateTime)
    {
      TimeSpan offset = dateTime.Kind == DateTimeKind.Utc ? new TimeSpan(0L) : TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" /> с использованием заданного значения <see cref="T:System.DateTime" /> и смещения.
    /// </summary>
    /// <param name="dateTime">Дата и время.</param>
    /// <param name="offset">
    ///   Смещение по времени от времени в формате UTC.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dateTime.Kind" /> равно <see cref="F:System.DateTimeKind.Utc" />, а <paramref name="offset" /> не равно нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="dateTime.Kind" /> равно <see cref="F:System.DateTimeKind.Local" />, а <paramref name="offset" /> не равно смещению местного часового пояса в системе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" /> не указано в целых минутах.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> меньше -14 часов или больше 14 часов.
    /// 
    ///   -или-
    /// 
    ///   Значение <see cref="P:System.DateTimeOffset.UtcDateTime" /> меньше <see cref="F:System.DateTimeOffset.MinValue" /> или больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(DateTime dateTime, TimeSpan offset)
    {
      if (dateTime.Kind == DateTimeKind.Local)
      {
        if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
          throw new ArgumentException(Environment.GetResourceString("Argument_OffsetLocalMismatch"), nameof (offset));
      }
      else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetUtcMismatch"), nameof (offset));
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" />, используя указанный год, месяц, день, час, минуту, секунду и смещение.
    /// </summary>
    /// <param name="year">Год (от 1 до 9999).</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <param name="offset">
    ///   Смещение по времени от времени в формате UTC.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="offset" /> не представляет целое количество минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="year" /> меньше единицы или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="month" /> меньше единицы или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="day" /> меньше единицы или больше чем число дней в параметре <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше нуля или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="offset" /> меньше –14 часов или больше 14 часов.
    /// 
    ///   -или-
    /// 
    ///   Момент времени, заданный в свойстве <see cref="P:System.DateTimeOffset.UtcDateTime" />, наступает раньше <see cref="F:System.DateTimeOffset.MinValue" /> или позже <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" />, используя указанные год, месяц, день, час, минуту, секунду, миллисекунду и смещение.
    /// </summary>
    /// <param name="year">Год (от 1 до 9999).</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <param name="millisecond">Миллисекунды (от 0 до 999).</param>
    /// <param name="offset">
    ///   Смещение по времени от времени в формате UTC.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="offset" /> не представляет целое количество минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="year" /> меньше единицы или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="month" /> меньше единицы или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="day" /> меньше единицы или больше чем число дней в параметре <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше нуля или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="millisecond" /> имеет значение меньше 0 или больше 999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="offset" /> меньше –14 или больше 14.
    /// 
    ///   -или-
    /// 
    ///   Момент времени, заданный в свойстве <see cref="P:System.DateTimeOffset.UtcDateTime" />, наступает раньше <see cref="F:System.DateTimeOffset.MinValue" /> или позже <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTimeOffset" />, используя указанные год, месяц, день, час, минуту, секунду, миллисекунду и смещение для заданного календаря.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <param name="millisecond">Миллисекунды (от 0 до 999).</param>
    /// <param name="calendar">
    ///   Календарь, используемый для анализа параметров <paramref name="year" />, <paramref name="month" /> и <paramref name="day" />.
    /// </param>
    /// <param name="offset">
    ///   Смещение по времени от времени в формате UTC.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="offset" /> не представляет целое количество минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="calendar" /> не может иметь значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="year" /> меньше заданного для параметра <paramref name="calendar" /> значения <see langword="MinSupportedDateTime.Year" /> или больше чем <see langword="MaxSupportedDateTime.Year" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="month" /> меньше или больше чем число месяцев, заданное в параметре <paramref name="year" /> параметра <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="day" /> меньше единицы или больше чем число дней в параметре <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше нуля или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="millisecond" /> имеет значение меньше 0 или больше 999.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="offset" /> меньше –14 часов или больше 14 часов.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="year" />, <paramref name="month" /> и <paramref name="day" /> не удается представить как значения типа даты и времени.
    /// 
    ///   -или-
    /// 
    ///   Момент времени, заданный в свойстве <see cref="P:System.DateTimeOffset.UtcDateTime" />, наступает раньше <see cref="F:System.DateTimeOffset.MinValue" /> или позже <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTimeOffset" />, для которого в качестве значения установлены текущие дата и время на текущем компьютере, а в качестве смещения — смещение местного времени от времени в формате UTC.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.DateTimeOffset" />, дата и время которого соответствуют текущему местному времени, а смещение равно смещению местного часового пояса от времени в формате UTC.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTimeOffset Now
    {
      [__DynamicallyInvokable] get
      {
        return new DateTimeOffset(DateTime.Now);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTimeOffset" /> объекта, Дата и время задаются текущей даты в формате UTC (UTC) и время и смещение которого равно <see cref="F:System.TimeSpan.Zero" />.
    /// </summary>
    /// <returns>
    ///   Объект, Дата и время является текущей по Гринвичу (UTC), а смещение равно <see cref="F:System.TimeSpan.Zero" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTimeOffset UtcNow
    {
      [__DynamicallyInvokable] get
      {
        return new DateTimeOffset(DateTime.UtcNow);
      }
    }

    /// <summary>
    ///   Получает значение <see cref="T:System.DateTime" />, представляющее дату и время текущего объекта <see cref="T:System.DateTimeOffset" />.
    /// </summary>
    /// <returns>
    ///   Дата и время текущего объекта <see cref="T:System.DateTimeOffset" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime DateTime
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> значение, представляющее дату в формате UTC (UTC) и время текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Дата в формате UTC (UTC) и время текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime UtcDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.SpecifyKind(this.m_dateTime, DateTimeKind.Utc);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> значение, представляющее локальные Дата и время текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Локальные дата и время текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime LocalDateTime
    {
      [__DynamicallyInvokable] get
      {
        return this.UtcDateTime.ToLocalTime();
      }
    }

    /// <summary>
    ///   Преобразует значение текущего <see cref="T:System.DateTimeOffset" /> объекта даты и времени, заданного параметром значения смещения.
    /// </summary>
    /// <param name="offset">
    ///   Смещение для преобразования <see cref="T:System.DateTimeOffset" /> значение.
    /// </param>
    /// <returns>
    ///   Объект, который совпадает с исходным <see cref="T:System.DateTimeOffset" /> объекта (то есть их <see cref="M:System.DateTimeOffset.ToUniversalTime" /> методы возвращают идентичных моментов времени), но которого <see cref="P:System.DateTimeOffset.Offset" /> свойству <paramref name="offset" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTimeOffset" /> объект имеет <see cref="P:System.DateTimeOffset.DateTime" /> значение более ранней, чем <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговый <see cref="T:System.DateTimeOffset" /> объект имеет <see cref="P:System.DateTimeOffset.DateTime" /> значение позже, чем <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" />— меньше-14 часов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" />больше 14 часов.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset ToOffset(TimeSpan offset)
    {
      return new DateTimeOffset((this.m_dateTime + offset).Ticks, offset);
    }

    private DateTime ClockDateTime
    {
      get
      {
        return new DateTime((this.m_dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> значение, представляющее компонент даты текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.DateTime" /> значение, представляющее компонент даты текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime Date
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Date;
      }
    }

    /// <summary>
    ///   Возвращает день месяца, представленный текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент дня текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженный как значение от 1 до 31.
    /// </returns>
    [__DynamicallyInvokable]
    public int Day
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Day;
      }
    }

    /// <summary>
    ///   Возвращает день недели, представленный текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее день недели текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public DayOfWeek DayOfWeek
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.DayOfWeek;
      }
    }

    /// <summary>
    ///   Возвращает день года, представленный текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   День года текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженный как значение от 1 до 366.
    /// </returns>
    [__DynamicallyInvokable]
    public int DayOfYear
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.DayOfYear;
      }
    }

    /// <summary>
    ///   Возвращает компонент часа времени, представленных текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент часов текущего <see cref="T:System.DateTimeOffset" /> объекта.
    ///    В этом свойстве используются 24-часовые часы; значение может меняться в диапазоне от 0 до 23.
    /// </returns>
    [__DynamicallyInvokable]
    public int Hour
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Hour;
      }
    }

    /// <summary>
    ///   Возвращает компонент миллисекунд времени, представленных текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент миллисекунд текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженное как целое число от 0 до 999.
    /// </returns>
    [__DynamicallyInvokable]
    public int Millisecond
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Millisecond;
      }
    }

    /// <summary>
    ///   Возвращает компонент минут для времени, представленного текущей <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент минут текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженное как целое число от 0 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Minute
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Minute;
      }
    }

    /// <summary>
    ///   Возвращает компонент месяца даты, представленной текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент месяца текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженное как целое число от 1 до 12.
    /// </returns>
    [__DynamicallyInvokable]
    public int Month
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Month;
      }
    }

    /// <summary>Возвращает смещение времени в формате UTC.</summary>
    /// <returns>
    ///   Разница между текущим <see cref="T:System.DateTimeOffset" /> значение времени и по Гринвичу (UTC) объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan Offset
    {
      [__DynamicallyInvokable] get
      {
        return new TimeSpan(0, (int) this.m_offsetMinutes, 0);
      }
    }

    /// <summary>
    ///   Возвращает компонент секунд по показаниям часов, представленный текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Вторым компонентом <see cref="T:System.DateTimeOffset" /> объекта, выраженный в виде целочисленного значения в диапазоне от 0 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Second
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Second;
      }
    }

    /// <summary>
    ///   Возвращает количество тактов, представляющее дату и время текущего <see cref="T:System.DateTimeOffset" /> объекта в формате времени.
    /// </summary>
    /// <returns>
    ///   Количество тактов в <see cref="T:System.DateTimeOffset" /> время объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Ticks;
      }
    }

    /// <summary>
    ///   Возвращает количество тактов, представляющее дату и время текущего <see cref="T:System.DateTimeOffset" /> объекта в формате UTC.
    /// </summary>
    /// <returns>
    ///   Количество тактов в <see cref="T:System.DateTimeOffset" /> объекта по Гринвичу (UTC).
    /// </returns>
    [__DynamicallyInvokable]
    public long UtcTicks
    {
      [__DynamicallyInvokable] get
      {
        return this.UtcDateTime.Ticks;
      }
    }

    /// <summary>
    ///   Возвращает время дня для текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>Время, истекшее с полуночи на текущую дату.</returns>
    [__DynamicallyInvokable]
    public TimeSpan TimeOfDay
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.TimeOfDay;
      }
    }

    /// <summary>
    ///   Возвращает компонент года даты, представленной текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Компонент года текущего объекта <see cref="T:System.DateTimeOffset" /> объекта, выраженный в виде целочисленного значения в диапазоне от 0 до 9999.
    /// </returns>
    [__DynamicallyInvokable]
    public int Year
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Year;
      }
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий указанный интервал времени к значению этого экземпляра.
    /// </summary>
    /// <param name="timeSpan">
    ///   Объект <see cref="T:System.TimeSpan" />, представляющий положительный или отрицательный интервал времени.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и интервала времени, представленного параметром <paramref name="timeSpan" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset Add(TimeSpan timeSpan)
    {
      return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.DateTimeOffset" /> объекта, который добавляет указанное число полных и неполных дней к значению данного экземпляра.
    /// </summary>
    /// <param name="days">
    ///   Число полных и неполных дней.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим <see cref="T:System.DateTimeOffset" /> объекта и количество дней, представленного параметром <paramref name="days" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddDays(double days)
    {
      return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий заданное количество полных и неполных часов к значению этого экземпляра.
    /// </summary>
    /// <param name="hours">
    ///   Число полных и неполных часов.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и количества часов, представленного параметром <paramref name="hours" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddHours(double hours)
    {
      return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий заданное число миллисекунд к значению данного экземпляра.
    /// </summary>
    /// <param name="milliseconds">
    ///   Число полных и неполных миллисекунд.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и количества полных миллисекунд, представленных параметром <paramref name="milliseconds" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMilliseconds(double milliseconds)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий заданное количество полных и неполных минут к значению данного экземпляра.
    /// </summary>
    /// <param name="minutes">
    ///   Число полных и неполных минут.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и количества минут, представленного параметром <paramref name="minutes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMinutes(double minutes)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.DateTimeOffset" /> объекта, добавляющий заданное число месяцев к значению данного экземпляра.
    /// </summary>
    /// <param name="months">
    ///   Число полных месяцев.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим <see cref="T:System.DateTimeOffset" /> объекта и номер месяца, представленный <paramref name="months" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMonths(int months)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий заданное количество полных и неполных секунд к значению данного экземпляра.
    /// </summary>
    /// <param name="seconds">
    ///   Число полных и неполных секунд.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и количества секунд, представленного параметром <paramref name="seconds" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddSeconds(double seconds)
    {
      return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTimeOffset" />, добавляющий заданное число тактов к значению этого экземпляра.
    /// </summary>
    /// <param name="ticks">
    ///   Число 100-наносекундных тактов.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим объектом <see cref="T:System.DateTimeOffset" />, и количества тактов, представленного параметром <paramref name="ticks" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddTicks(long ticks)
    {
      return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.DateTimeOffset" /> объекта, добавляющий заданное число лет к значению данного экземпляра.
    /// </summary>
    /// <param name="years">
    ///   Число лет.
    ///    Это число может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим <see cref="T:System.DateTimeOffset" /> объекта и количества лет, представленного параметром <paramref name="years" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddYears(int years)
    {
      return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.DateTimeOffset" /> объектов и указывает, является ли первый более ранней, чем второй, равен ему или более поздней версии, чем второй.
    /// </summary>
    /// <param name="first">Первый из сравниваемых объектов.</param>
    /// <param name="second">Второй из сравниваемых объектов.</param>
    /// <returns>
    /// Целое число со знаком, указывающее ли значение <paramref name="first" /> параметр раньше, позже, или же время, что значение <paramref name="second" /> параметра, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Момент, указанный в параметре <paramref name="first" />, наступает раньше, чем момент, указанный в параметре <paramref name="second" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="first" /> равно <paramref name="second" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Момент, указанный в параметре <paramref name="first" />, наступает позже, чем момент, указанный в параметре <paramref name="second" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Compare(DateTimeOffset first, DateTimeOffset second)
    {
      return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
    }

    [__DynamicallyInvokable]
    int IComparable.CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      if (!(obj is DateTimeOffset))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTimeOffset"));
      DateTime utcDateTime1 = ((DateTimeOffset) obj).UtcDateTime;
      DateTime utcDateTime2 = this.UtcDateTime;
      if (utcDateTime2 > utcDateTime1)
        return 1;
      return utcDateTime2 < utcDateTime1 ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.DateTimeOffset" /> объекта с заданным <see cref="T:System.DateTimeOffset" /> объекта и указывает, является ли текущий объект более ранней, чем же образом или более поздней версии, чем второй <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="other">
    ///   Объект, сравниваемый с текущим объектом <see cref="T:System.DateTimeOffset" />.
    /// </param>
    /// <returns>
    /// Целое число со знаком, показывающее связь между текущим <see cref="T:System.DateTimeOffset" /> объекта и <paramref name="other" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Текущий <see cref="T:System.DateTimeOffset" /> объекта более ранняя, чем <paramref name="other" />.
    /// 
    ///         Нуль
    /// 
    ///         Текущий <see cref="T:System.DateTimeOffset" /> объекта совпадает с именем <paramref name="other" />.
    /// 
    ///         Больше нуля.
    /// 
    ///         Текущий <see cref="T:System.DateTimeOffset" /> объект является более поздней, чем <paramref name="other" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(DateTimeOffset other)
    {
      DateTime utcDateTime1 = other.UtcDateTime;
      DateTime utcDateTime2 = this.UtcDateTime;
      if (utcDateTime2 > utcDateTime1)
        return 1;
      return utcDateTime2 < utcDateTime1 ? -1 : 0;
    }

    /// <summary>
    ///   Определяет, является ли <see cref="T:System.DateTimeOffset" /> объект представляет тот же момент времени, что указанный объект.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> параметр <see cref="T:System.DateTimeOffset" /> объекта и представляет тот же момент времени, что и текущий <see cref="T:System.DateTimeOffset" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is DateTimeOffset)
        return this.UtcDateTime.Equals(((DateTimeOffset) obj).UtcDateTime);
      return false;
    }

    /// <summary>
    ///   Определяет ли текущий <see cref="T:System.DateTimeOffset" /> объект представляет тот же момент времени, что и указанный <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если оба <see cref="T:System.DateTimeOffset" /> объектов с одинаковым <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(DateTimeOffset other)
    {
      return this.UtcDateTime.Equals(other.UtcDateTime);
    }

    /// <summary>
    ///   Определяет ли текущий <see cref="T:System.DateTimeOffset" /> объект представляет то же время и имеет же смещения указанного <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="other">
    ///   Объект, сравниваемый с текущим <see cref="T:System.DateTimeOffset" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.DateTimeOffset" /> объекта и <paramref name="other" /> иметь то же значение даты и времени и тот же <see cref="P:System.DateTimeOffset.Offset" /> значение; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool EqualsExact(DateTimeOffset other)
    {
      if (this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset)
        return this.ClockDateTime.Kind == other.ClockDateTime.Kind;
      return false;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных <see cref="T:System.DateTimeOffset" /> объектов представляют тот же момент времени.
    /// </summary>
    /// <param name="first">Первый из сравниваемых объектов.</param>
    /// <param name="second">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если два <see cref="T:System.DateTimeOffset" /> объектов с одинаковым <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool Equals(DateTimeOffset first, DateTimeOffset second)
    {
      return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
    }

    /// <summary>
    ///   Преобразует заданное время файла Windows в его эквивалент по местному времени.
    /// </summary>
    /// <param name="fileTime">
    ///   Время файла Windows, выраженное в тактах.
    /// </param>
    /// <returns>
    ///   Объект, представляющий дату и время <paramref name="fileTime" /> с смещение равно смещению местного времени.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="filetime" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="filetime" /> больше значения <see langword="DateTimeOffset.MaxValue.Ticks" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromFileTime(long fileTime)
    {
      return new DateTimeOffset(DateTime.FromFileTime(fileTime));
    }

    /// <summary>
    ///   Преобразует время Unix, выраженное как количество секунд, истекших с 1970-01-01T00: 00 00z для <see cref="T:System.DateTimeOffset" /> значение.
    /// </summary>
    /// <param name="seconds">
    ///   Время Unix, выраженное как количество секунд, истекших с 1970-01-01T00:00:00Z (1 января 1970 года, 00:00 UTC).
    ///    Для времени Unix до этой даты значение отрицательное.
    /// </param>
    /// <returns>
    ///   Значение даты и времени, представляющий один и тот же момент времени в качестве времени Unix.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="seconds" />— меньше, чем-62,135,596,800.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="seconds" />больше, чем 253,402,300,799.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
    {
      if (seconds < -62135596800L || seconds > 253402300799L)
        throw new ArgumentOutOfRangeException(nameof (seconds), string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -62135596800L, (object) 253402300799L));
      return new DateTimeOffset(seconds * 10000000L + 621355968000000000L, TimeSpan.Zero);
    }

    /// <summary>
    ///   Преобразует время Unix, выраженное как количество миллисекунд, истекших с 1970-01-01T00: 00 00z для <see cref="T:System.DateTimeOffset" /> значение.
    /// </summary>
    /// <param name="milliseconds">
    ///   Время Unix, выраженное как количество миллисекунд, истекших с 1970-01-01T00:00:00Z (1 января 1970 года, 00:00 UTC).
    ///    Для времени Unix до этой даты значение отрицательное.
    /// </param>
    /// <returns>
    ///   Значение даты и времени, представляющий один и тот же момент времени в качестве времени Unix.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="milliseconds" />— меньше, чем-62,135,596,800,000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="milliseconds" />больше, чем 253,402,300,799,999.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
    {
      if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
        throw new ArgumentOutOfRangeException(nameof (milliseconds), string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -62135596800000L, (object) 253402300799999L));
      return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        this.m_offsetMinutes = DateTimeOffset.ValidateOffset(this.Offset);
        this.m_dateTime = DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("DateTime", this.m_dateTime);
      info.AddValue("OffsetMinutes", this.m_offsetMinutes);
    }

    private DateTimeOffset(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_dateTime = (DateTime) info.GetValue(nameof (DateTime), typeof (DateTime));
      this.m_offsetMinutes = (short) info.GetValue("OffsetMinutes", typeof (short));
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.UtcDateTime.GetHashCode();
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты, времени и смещения в его эквивалент <see cref="T:System.DateTimeOffset" />.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше –14 часов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> содержит строковое представление смещения без даты или времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input)
    {
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out offset).Ticks, offset);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании значения <paramref name="input" />, связанные с языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />, в соответствии со значением параметра <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше -14 часов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> содержит строковое представление смещения без даты или времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
    {
      return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя указанную информацию о форматировании, связанную с языком и региональными параметрами, а также заданный стиль форматирования.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании значения <paramref name="input" />, связанные с языком и региональными параметрами.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="input" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />, в соответствии со значениями параметров <paramref name="formatProvider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше –14 часов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> не является допустимым значением <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> содержит неподдерживаемое значение <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит значения <see cref="T:System.Globalization.DateTimeStyles" />, которые нельзя использовать вместе.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> содержит строковое представление смещения без даты или времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Описатель формата, задающий ожидаемый формат <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />, в соответствии со значениями параметров <paramref name="format" /> и <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше –14 часов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="format" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="input" /> не соответствуют друг другу.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя заданный формат, указанные сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Описатель формата, задающий ожидаемый формат <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="input" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />, в соответствии со значениями параметров <paramref name="format" />, <paramref name="formatProvider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше –14 часов.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит неподдерживаемое значение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит значения <see cref="T:System.Globalization.DateTimeStyles" />, которые нельзя использовать вместе.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="format" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="input" /> не соответствуют друг другу.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя заданные форматы, сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formats">
    ///   Массив спецификаторов формата, в котором заданы требуемые форматы <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="input" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="input" />, в соответствии со значениями параметров <paramref name="formats" />, <paramref name="formatProvider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Смещение больше 14 часов или меньше –14 часов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> содержит неподдерживаемое значение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит значения <see cref="T:System.Globalization.DateTimeStyles" />, которые нельзя использовать вместе.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="input" /> не содержит допустимое строковое представление даты и времени.
    /// 
    ///   -или-
    /// 
    ///   В <paramref name="formats" /> нет элементов, содержащих допустимые спецификаторы формата.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="input" /> не соответствуют друг другу.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>
    ///   Вычитает значение <see cref="T:System.DateTimeOffset" />, представляющее определенную дату и время, из текущего объекта <see cref="T:System.DateTimeOffset" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, представляющий вычитаемое значение.
    /// </param>
    /// <returns>
    ///   Объект, задающий интервал между двумя объектами <see cref="T:System.DateTimeOffset" />.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(DateTimeOffset value)
    {
      return this.UtcDateTime.Subtract(value.UtcDateTime);
    }

    /// <summary>
    ///   Вычитает указанный интервал времени из текущего объекта <see cref="T:System.DateTimeOffset" />.
    /// </summary>
    /// <param name="value">Вычитаемый интервал времени.</param>
    /// <returns>
    ///   Объект, значение которого равно дате и времени, представленным текущим объектом <see cref="T:System.DateTimeOffset" />, за вычетом интервала времени, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTimeOffset Subtract(TimeSpan value)
    {
      return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTimeOffset" /> во временную характеристику файла Windows.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.DateTimeOffset" /> объекта, выраженный в виде временной характеристики файла Windows.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результирующая временная представляет дату и время до полуночи 1 января 1601 года нашей эры Всемирного координированного времени (UTC).
    /// </exception>
    [__DynamicallyInvokable]
    public long ToFileTime()
    {
      return this.UtcDateTime.ToFileTime();
    }

    /// <summary>
    ///   Возвращает количество секунд, истекших с 1970-01-01T00: 00 00z.
    /// </summary>
    /// <returns>Число секунд, истекших с 1970-01-01T00: 00 00z.</returns>
    [__DynamicallyInvokable]
    public long ToUnixTimeSeconds()
    {
      return this.UtcDateTime.Ticks / 10000000L - 62135596800L;
    }

    /// <summary>
    ///   Возвращает число миллисекунд, истекших с 1970-01-01T00:00:00.000Z.
    /// </summary>
    /// <returns>
    ///   Число миллисекунд, истекших с 1970-01-01T00:00:00.000Z.
    /// </returns>
    [__DynamicallyInvokable]
    public long ToUnixTimeMilliseconds()
    {
      return this.UtcDateTime.Ticks / 10000L - 62135596800000L;
    }

    /// <summary>
    ///   Преобразует текущий <see cref="T:System.DateTimeOffset" /> объект <see cref="T:System.DateTimeOffset" /> , представляющий местное время.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий дату и время текущего <see cref="T:System.DateTimeOffset" /> преобразовать объект в местное время.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTimeOffset ToLocalTime()
    {
      return this.ToLocalTime(false);
    }

    internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
    {
      return new DateTimeOffset(this.UtcDateTime.ToLocalTime(throwOnOverflow));
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTimeOffset" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление <see cref="T:System.DateTimeOffset" /> объект, который включает смещение в конце строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, принятым для текущего языка и региональных параметров.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return DateTimeFormat.Format(this.ClockDateTime, (string) null, DateTimeFormatInfo.CurrentInfo, this.Offset);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTimeOffset" /> в эквивалентное ему строковое представление с использованием заданного формата.
    /// </summary>
    /// <param name="format">Строка формата.</param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTimeOffset" />, заданное параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="format" /> равна 1, и он не является одним из стандартных символов описателя формата, определенных для <see cref="T:System.Globalization.DateTimeFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="format" /> не содержит допустимого шаблона пользовательского формата.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, принятым для текущего языка и региональных параметров.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.CurrentInfo, this.Offset);
    }

    /// <summary>
    ///   Преобразует значение текущего <see cref="T:System.DateTimeOffset" /> объекта в эквивалентное строковое представление с использованием указанного языком и региональными параметрами сведения о форматировании.
    /// </summary>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего <see cref="T:System.DateTimeOffset" /> объекта в соответствии с <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, который используется у <paramref name="formatProvider" />.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider formatProvider)
    {
      return DateTimeFormat.Format(this.ClockDateTime, (string) null, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTimeOffset" /> в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях формата для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">Строка формата.</param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTimeOffset" />, заданное параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="format" /> равна 1, и он не является одним из стандартных символов описателя формата, определенных для <see cref="T:System.Globalization.DateTimeFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="format" /> не содержит допустимого шаблона пользовательского формата.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, который используется у <paramref name="formatProvider" />.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider formatProvider)
    {
      return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
    }

    /// <summary>
    ///   Преобразует текущий <see cref="T:System.DateTimeOffset" /> объект <see cref="T:System.DateTimeOffset" /> значение, представляющее по Гринвичу (UTC).
    /// </summary>
    /// <returns>
    ///   Объект, представляющий дату и время текущего <see cref="T:System.DateTimeOffset" /> преобразовать объект в формате UTC.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTimeOffset ToUniversalTime()
    {
      return new DateTimeOffset(this.UtcDateTime);
    }

    /// <summary>
    ///   Предпринимает попытку преобразования указанного строкового представления даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" /> и возвращает значение, позволяющее определить успешность преобразования.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит эквивалент <see cref="T:System.DateTimeOffset" /> для даты и времени, заданных в параметре <paramref name="input" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTimeOffset.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если значение параметра <paramref name="input" /> равно <see langword="null" /> или в нем не содержится допустимое строковое представление даты и времени.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out DateTimeOffset result)
    {
      DateTime result1;
      TimeSpan offset;
      bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result1, out offset);
      result = new DateTimeOffset(result1.Ticks, offset);
      return flag;
    }

    /// <summary>
    ///   Предпринимает попытку преобразования указанного строкового представления даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" /> и возвращает значение, позволяющее определить успешность преобразования.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит значение <see cref="T:System.DateTimeOffset" />, эквивалентное дате и времени, заданным в параметре <paramref name="input" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTimeOffset.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если значение параметра <paramref name="input" /> равно <see langword="null" /> или в нем не содержится допустимое строковое представление даты и времени.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="styles" /> включает неопределенное значение <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   Объект <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит взаимоисключающие значения <see cref="T:System.Globalization.DateTimeStyles" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      DateTime result1;
      TimeSpan offset;
      bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset);
      result = new DateTimeOffset(result1.Ticks, offset);
      return flag;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя заданный формат, указанные сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Описатель формата, задающий требуемый формат <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовое сочетание значений перечисления, которое показывает разрешенный формат ввода.
    ///    Обычно указывается значение <see langword="None" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит эквивалент <see cref="T:System.DateTimeOffset" /> для даты и времени, заданных в параметре <paramref name="input" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTimeOffset.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачно, если параметр <paramref name="input" /> имеет значение <see langword="null" /> или не содержит допустимое строчное представление даты и времени в требуемом формате, заданном параметрами <paramref name="format" /> и <paramref name="provider" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="styles" /> включает неопределенное значение <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   Объект <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит взаимоисключающие значения <see cref="T:System.Globalization.DateTimeStyles" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      DateTime result1;
      TimeSpan offset;
      bool exact = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset);
      result = new DateTimeOffset(result1.Ticks, offset);
      return exact;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTimeOffset" />, используя заданный массив форматов, указанные сведения о форматировании, связанные с языком и региональными параметрами, и стиль форматирования.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formats">
    ///   Массив, в котором задаются ожидаемые форматы <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="input" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовое сочетание значений перечисления, которое показывает разрешенный формат ввода.
    ///    Обычно указывается значение <see langword="None" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит эквивалент <see cref="T:System.DateTimeOffset" /> для даты и времени, заданных в параметре <paramref name="input" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTimeOffset.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачно, если параметр <paramref name="format" /> не содержит допустимое строчное представление даты и времени или же даты и времени в требуемом формате, заданном параметром <paramref name="formats" />, или если параметр <see langword="null" /> имеет значение <paramref name="input" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="styles" /> включает неопределенное значение <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   Объект <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="styles" /> содержит взаимоисключающие значения <see cref="T:System.Globalization.DateTimeStyles" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, nameof (styles));
      DateTime result1;
      TimeSpan offset;
      bool exactMultiple = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset);
      result = new DateTimeOffset(result1.Ticks, offset);
      return exactMultiple;
    }

    private static short ValidateOffset(TimeSpan offset)
    {
      long ticks = offset.Ticks;
      if (ticks % 600000000L != 0L)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetPrecision"), nameof (offset));
      if (ticks < -504000000000L || ticks > 504000000000L)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("Argument_OffsetOutOfRange"));
      return (short) (offset.Ticks / 600000000L);
    }

    private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
    {
      long ticks = dateTime.Ticks - offset.Ticks;
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("Argument_UTCOutOfRange"));
      return new DateTime(ticks, DateTimeKind.Unspecified);
    }

    private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
    {
      if ((style & ~(DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetInvalidDateTimeStyles"), parameterName);
      style &= ~DateTimeStyles.RoundtripKind;
      style &= ~DateTimeStyles.AssumeLocal;
      return style;
    }

    /// <summary>
    ///   Определяет неявное преобразование из <see cref="T:System.DateTime" /> объект <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="dateTime">Преобразуемый объект.</param>
    /// <returns>Преобразованный объект.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время в формате UTC, которые получаются в результате применения смещения, более раннего чем <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Дата и время в формате UTC, которые получаются в результате применения смещения, более позднего чем <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static implicit operator DateTimeOffset(DateTime dateTime)
    {
      return new DateTimeOffset(dateTime);
    }

    /// <summary>
    ///   Добавляет указанный интервал времени в объект <see cref="T:System.DateTimeOffset" />, имеющий заданную дату и время, и образует объект <see cref="T:System.DateTimeOffset" /> с новыми значениями даты и времени.
    /// </summary>
    /// <param name="dateTimeOffset">
    ///   Объект, к которому необходимо добавить интервал времени.
    /// </param>
    /// <param name="timeSpan">Добавляемый интервал времени.</param>
    /// <returns>
    ///   Объект, значение которого является суммой значений <paramref name="dateTimeTz" /> и <paramref name="timeSpan" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
    }

    /// <summary>
    ///   Вычитает заданный временной интервал из указанной даты и времени и выдает новую дату и время.
    /// </summary>
    /// <param name="dateTimeOffset">
    ///   Объект даты и времени, из которого вычитается интервал.
    /// </param>
    /// <param name="timeSpan">Вычитаемый интервал времени.</param>
    /// <returns>
    ///   Объект, равный значению <paramref name="dateTimeOffset" /> за вычетом значения <paramref name="timeSpan" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTimeOffset" /> меньше <see cref="F:System.DateTimeOffset.MinValue" /> или больше <see cref="F:System.DateTimeOffset.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
    }

    /// <summary>
    ///   Вычитает объект <see cref="T:System.DateTimeOffset" /> из другого объекта и выдает интервал времени.
    /// </summary>
    /// <param name="left">Уменьшаемое.</param>
    /// <param name="right">Вычитаемое.</param>
    /// <returns>
    ///   Объект, представляющий разность между <paramref name="left" /> и <paramref name="right" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime - right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных <see cref="T:System.DateTimeOffset" /> объектов представляют тот же момент времени.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если оба <see cref="T:System.DateTimeOffset" /> объектов с одинаковым <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime == right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных <see cref="T:System.DateTimeOffset" /> объекты ссылаются на разные моменты времени.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> и <paramref name="right" /> не с одинаковым <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime != right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного <see cref="T:System.DateTimeOffset" /> объекта указано меньше секунды <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="left" /> более ранняя, чем <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime < right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного <see cref="T:System.DateTimeOffset" /> объекта указано меньше секунды <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="left" /> более ранняя, чем <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime <= right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного <see cref="T:System.DateTimeOffset" /> объекта больше (или более поздней, чем) указан второй <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />Если <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="left" /> новее, чем <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="right" />; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime > right.UtcDateTime;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного <see cref="T:System.DateTimeOffset" /> объекта больше или равно значению другого указанного <see cref="T:System.DateTimeOffset" /> объекта.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="left" /> является одновременно или позже, чем <see cref="P:System.DateTimeOffset.UtcDateTime" /> значение <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime >= right.UtcDateTime;
    }
  }
}
