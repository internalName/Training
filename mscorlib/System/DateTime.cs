// Decompiled with JetBrains decompiler
// Type: System.DateTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет текущее время, обычно выраженное как дата и время суток.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct DateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
  {
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
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.DateTime" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.DateTime" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);
    private const long TicksPerMillisecond = 10000;
    private const long TicksPerSecond = 10000000;
    private const long TicksPerMinute = 600000000;
    private const long TicksPerHour = 36000000000;
    private const long TicksPerDay = 864000000000;
    private const int MillisPerSecond = 1000;
    private const int MillisPerMinute = 60000;
    private const int MillisPerHour = 3600000;
    private const int MillisPerDay = 86400000;
    private const int DaysPerYear = 365;
    private const int DaysPer4Years = 1461;
    private const int DaysPer100Years = 36524;
    private const int DaysPer400Years = 146097;
    private const int DaysTo1601 = 584388;
    private const int DaysTo1899 = 693593;
    internal const int DaysTo1970 = 719162;
    private const int DaysTo10000 = 3652059;
    internal const long MinTicks = 0;
    internal const long MaxTicks = 3155378975999999999;
    private const long MaxMillis = 315537897600000;
    private const long FileTimeOffset = 504911232000000000;
    private const long DoubleDateOffset = 599264352000000000;
    private const long OADateMinAsTicks = 31241376000000000;
    private const double OADateMinAsDouble = -657435.0;
    private const double OADateMaxAsDouble = 2958466.0;
    private const int DatePartYear = 0;
    private const int DatePartDayOfYear = 1;
    private const int DatePartMonth = 2;
    private const int DatePartDay = 3;
    private const ulong TicksMask = 4611686018427387903;
    private const ulong FlagsMask = 13835058055282163712;
    private const ulong LocalMask = 9223372036854775808;
    private const long TicksCeiling = 4611686018427387904;
    private const ulong KindUnspecified = 0;
    private const ulong KindUtc = 4611686018427387904;
    private const ulong KindLocal = 9223372036854775808;
    private const ulong KindLocalAmbiguousDst = 13835058055282163712;
    private const int KindShift = 62;
    private const string TicksField = "ticks";
    private const string DateDataField = "dateData";
    private ulong dateData;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданным числом тактов.
    /// </summary>
    /// <param name="ticks">
    ///   Дата и время, представленные в виде нескольких 100-наносекундных интервалов, завершившихся с момента 00:00:00.000 1 января 0001 г. по григорианскому календарю.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="ticks" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(long ticks)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException(nameof (ticks), Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      this.dateData = (ulong) ticks;
    }

    private DateTime(ulong dateData)
    {
      this.dateData = dateData;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданным числом тактов и временем UTC или местным временем.
    /// </summary>
    /// <param name="ticks">
    ///   Дата и время, представленные в виде нескольких 100-наносекундных интервалов, завершившихся с момента 00:00:00.000 1 января 0001 г. по григорианскому календарю.
    /// </param>
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее, какое время задает параметр <paramref name="ticks" />: локальное, универсальное глобальное (UTC) или ни то, ни другое.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="ticks" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="kind" /> не является одним из значений <see cref="T:System.DateTimeKind" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(long ticks, DateTimeKind kind)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException(nameof (ticks), Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      switch (kind)
      {
        case DateTimeKind.Unspecified:
        case DateTimeKind.Utc:
        case DateTimeKind.Local:
          this.dateData = (ulong) (ticks | (long) kind << 62);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), nameof (kind));
      }
    }

    internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException(nameof (ticks), Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      this.dateData = (ulong) (ticks | (isAmbiguousDst ? -4611686018427387904L : long.MinValue));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданными значениями года, месяца и дня.
    /// </summary>
    /// <param name="year">Год (от 1 до 9999).</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day)
    {
      this.dateData = (ulong) DateTime.DateToTicks(year, month, day);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданным годом, месяцем и днем для указанного календаря.
    /// </summary>
    /// <param name="year">
    ///   Год (от 1 до количества лет в <paramref name="calendar" />).
    /// </param>
    /// <param name="month">
    ///   Месяц (от 1 до количества месяцев в <paramref name="calendar" />).
    /// </param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="calendar">
    ///   Календарь, используемый для анализа параметров <paramref name="year" />, <paramref name="month" /> и <paramref name="day" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="calendar" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> не входит в диапазон, поддерживаемый <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> меньше 1 или больше числа месяцев в <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// </exception>
    public DateTime(int year, int month, int day, Calendar calendar)
    {
      this = new DateTime(year, month, day, 0, 0, 0, calendar);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданным годом, месяцем, днем, часом, минутой и секундой.
    /// </summary>
    /// <param name="year">Год (от 1 до 9999).</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> имеет значение меньше 0 или больше 59.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second)
    {
      this.dateData = (ulong) (DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданными значениями года, месяца, дня, часа, минуты и секунды, а также временем UTC или местным временем.
    /// </summary>
    /// <param name="year">Год (от 1 до 9999).</param>
    /// <param name="month">Месяц (от 1 до 12).</param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее, что задают параметры <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" /> и <paramref name="second" />: локальное время, универсальное глобальное время (UTC), или ни то, ни другое.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> имеет значение меньше 0 или больше 59.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="kind" /> не является одним из значений <see cref="T:System.DateTimeKind" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
    {
      switch (kind)
      {
        case DateTimeKind.Unspecified:
        case DateTimeKind.Utc:
        case DateTimeKind.Local:
          this.dateData = (ulong) (DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) | (long) kind << 62);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), nameof (kind));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> указанным годом, месяцем, днем, часом, минутой и секундой для заданного календаря.
    /// </summary>
    /// <param name="year">
    ///   Год (от 1 до количества лет в <paramref name="calendar" />).
    /// </param>
    /// <param name="month">
    ///   Месяц (от 1 до количества месяцев в <paramref name="calendar" />).
    /// </param>
    /// <param name="day">
    ///   День (от 1 до количества дней в <paramref name="month" />).
    /// </param>
    /// <param name="hour">Часы (от 0 до 23).</param>
    /// <param name="minute">Минуты (от 0 до 59).</param>
    /// <param name="second">Секунды (от 0 до 59).</param>
    /// <param name="calendar">
    ///   Календарь, используемый для анализа параметров <paramref name="year" />, <paramref name="month" /> и <paramref name="day" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="calendar" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> не входит в диапазон, поддерживаемый <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> меньше 1 или больше числа месяцев в <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> имеет значение меньше 0 или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> имеет значение меньше 0 или больше 59.
    /// </exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException(nameof (calendar));
      this.dateData = (ulong) calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданным годом, месяцем, днем, часом, минутой, секундой и миллисекундой.
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
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
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
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) num;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданными значениями года, месяца, дня, часа, минуты, секунды и миллисекунды, а также временем UTC или местным временем.
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
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее, что задают параметры <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" /> и <paramref name="millisecond" />: локальное время, универсальное глобальное время (UTC), или ни то, ни другое.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
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
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="kind" /> не является одним из значений <see cref="T:System.DateTimeKind" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
    {
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      switch (kind)
      {
        case DateTimeKind.Unspecified:
        case DateTimeKind.Utc:
        case DateTimeKind.Local:
          long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
          if (num < 0L || num > 3155378975999999999L)
            throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
          this.dateData = (ulong) (num | (long) kind << 62);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), nameof (kind));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> указанным годом, месяцем, днем, часом, минутой, секундой и миллисекундой для заданного календаря.
    /// </summary>
    /// <param name="year">
    ///   Год (от 1 до количества лет в <paramref name="calendar" />).
    /// </param>
    /// <param name="month">
    ///   Месяц (от 1 до количества месяцев в <paramref name="calendar" />).
    /// </param>
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
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="calendar" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> не входит в диапазон, поддерживаемый <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> меньше 1 или больше числа месяцев в <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
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
    /// </exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException(nameof (calendar));
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) num;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.DateTime" /> заданными значениями года, месяца, дня, часа, минуты, секунды и миллисекунды, а также временем UTC или местным временем для заданного календаря.
    /// </summary>
    /// <param name="year">
    ///   Год (от 1 до количества лет в <paramref name="calendar" />).
    /// </param>
    /// <param name="month">
    ///   Месяц (от 1 до количества месяцев в <paramref name="calendar" />).
    /// </param>
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
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее, что задают параметры <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" /> и <paramref name="millisecond" />: локальное время, универсальное глобальное время (UTC), или ни то, ни другое.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="calendar" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> не входит в диапазон, поддерживаемый <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> меньше 1 или больше числа месяцев в <paramref name="calendar" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше, чем число дней в <paramref name="month" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
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
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="kind" /> не является одним из значений <see cref="T:System.DateTimeKind" />.
    /// </exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
    {
      if (calendar == null)
        throw new ArgumentNullException(nameof (calendar));
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException(nameof (millisecond), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      switch (kind)
      {
        case DateTimeKind.Unspecified:
        case DateTimeKind.Utc:
        case DateTimeKind.Local:
          long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks + (long) millisecond * 10000L;
          if (num < 0L || num > 3155378975999999999L)
            throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
          this.dateData = (ulong) (num | (long) kind << 62);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), nameof (kind));
      }
    }

    private DateTime(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      bool flag1 = false;
      bool flag2 = false;
      long num1 = 0;
      ulong num2 = 0;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "ticks"))
        {
          if (name == nameof (dateData))
          {
            num2 = Convert.ToUInt64(enumerator.Value, (IFormatProvider) CultureInfo.InvariantCulture);
            flag2 = true;
          }
        }
        else
        {
          num1 = Convert.ToInt64(enumerator.Value, (IFormatProvider) CultureInfo.InvariantCulture);
          flag1 = true;
        }
      }
      if (flag2)
      {
        this.dateData = num2;
      }
      else
      {
        if (!flag1)
          throw new SerializationException(Environment.GetResourceString("Serialization_MissingDateTimeData"));
        this.dateData = (ulong) num1;
      }
      long internalTicks = this.InternalTicks;
      if (internalTicks < 0L || internalTicks > 3155378975999999999L)
        throw new SerializationException(Environment.GetResourceString("Serialization_DateTimeTicksOutOfRange"));
    }

    internal long InternalTicks
    {
      get
      {
        return (long) this.dateData & 4611686018427387903L;
      }
    }

    private ulong InternalKind
    {
      get
      {
        return this.dateData & 13835058055282163712UL;
      }
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий значение заданного объекта <see cref="T:System.TimeSpan" /> к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Положительный или отрицательный интервал времени.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и интервала времени, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime Add(TimeSpan value)
    {
      return this.AddTicks(value._ticks);
    }

    private DateTime Add(double value, int scale)
    {
      long num = (long) (value * (double) scale + (value >= 0.0 ? 0.5 : -0.5));
      if (num <= -315537897600000L || num >= 315537897600000L)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
      return this.AddTicks(num * 10000L);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число дней к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число полных и неполных дней.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества дней, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddDays(double value)
    {
      return this.Add(value, 86400000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число часов к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число полных и неполных часов.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества часов, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddHours(double value)
    {
      return this.Add(value, 3600000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число миллисекунд к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число полных и неполных миллисекунд.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    ///    Заметьте, что это значение округляется до ближайшего целого числа.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества миллисекунд, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddMilliseconds(double value)
    {
      return this.Add(value, 1);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число минут к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число полных и неполных минут.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества минут, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddMinutes(double value)
    {
      return this.Add(value, 60000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число месяцев к значению данного экземпляра.
    /// </summary>
    /// <param name="months">
    ///   Число месяцев.
    ///    Параметр <paramref name="months" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значением которого является сумма даты и времени, представленных этим экземпляром, и значения, представленного параметром <paramref name="months" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="months" /> имеет значение меньше –120 000 или больше 120 000.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddMonths(int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadMonths"));
      int year1;
      int month;
      int day;
      this.GetDatePart(out year1, out month, out day);
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
      if (year2 < 1 || year2 > 9999)
        throw new ArgumentOutOfRangeException(nameof (months), Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      int num2 = DateTime.DaysInMonth(year2, month);
      if (day > num2)
        day = num2;
      return new DateTime((ulong) (DateTime.DateToTicks(year2, month, day) + this.InternalTicks % 864000000000L) | this.InternalKind);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число секунд к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число полных и неполных секунд.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества секунд, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddSeconds(double value)
    {
      return this.Add(value, 1000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число тактов к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число 100-наносекундных тактов.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и времени, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddTicks(long value)
    {
      long internalTicks = this.InternalTicks;
      if (value > 3155378975999999999L - internalTicks || value < -internalTicks)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks + value) | this.InternalKind);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.DateTime" />, добавляющий заданное число лет к значению данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Число лет.
    ///    Параметр <paramref name="value" /> может быть положительным или отрицательным.
    /// </param>
    /// <returns>
    ///   Объект, значение которого равно сумме даты и времени, представленных текущим экземпляром, и количества лет, представленного параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="value" /> или итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime AddYears(int value)
    {
      if (value < -10000 || value > 10000)
        throw new ArgumentOutOfRangeException("years", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadYears"));
      return this.AddMonths(value * 12);
    }

    /// <summary>
    ///   Сравнивает два экземпляра объекта <see cref="T:System.DateTime" /> и возвращает целое число, которое показывает, предшествует ли первый экземпляр второму, совпадает или расположен позже.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    /// Число со знаком, обозначающее относительные значения параметров <paramref name="t1" /> и <paramref name="t2" />.
    /// 
    ///         Тип значения
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Момент, указанный в параметре <paramref name="t1" />, наступает раньше, чем момент, указанный в параметре <paramref name="t2" />.
    /// 
    ///         Нуль
    /// 
    ///         Параметр <paramref name="t1" /> совпадает с параметром <paramref name="t2" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Момент, указанный в параметре <paramref name="t1" />, наступает позже, чем момент, указанный в параметре <paramref name="t2" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Compare(DateTime t1, DateTime t2)
    {
      long internalTicks1 = t1.InternalTicks;
      long internalTicks2 = t2.InternalTicks;
      if (internalTicks1 > internalTicks2)
        return 1;
      return internalTicks1 < internalTicks2 ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает значение данного экземпляра с заданным объектом, содержащим указанное значение <see cref="T:System.DateTime" />, и возвращает целочисленное значение, указывающее, когда наступает момент, заданный в данном экземпляре: раньше, позже или одновременно с моментом, заданным значением <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">
    ///   Сравниваемый упакованный объект или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр раньше <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр равен <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр позже <paramref name="value" />, или параметр <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.DateTime" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is DateTime))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTime"));
      long internalTicks1 = ((DateTime) value).InternalTicks;
      long internalTicks2 = this.InternalTicks;
      if (internalTicks2 > internalTicks1)
        return 1;
      return internalTicks2 < internalTicks1 ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает значение данного экземпляра с заданным значением <see cref="T:System.DateTime" /> и возвращает целочисленное значение, указывающее, когда наступает момент, заданный в данном экземпляре: раньше, позже или одновременно с моментом, заданным значением <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Число со знаком, представляющее результат сравнения значений этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр раньше <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр равен <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр позже <paramref name="value" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(DateTime value)
    {
      long internalTicks1 = value.InternalTicks;
      long internalTicks2 = this.InternalTicks;
      if (internalTicks2 > internalTicks1)
        return 1;
      return internalTicks2 < internalTicks1 ? -1 : 0;
    }

    private static long DateToTicks(int year, int month, int day)
    {
      if (year >= 1 && year <= 9999 && (month >= 1 && month <= 12))
      {
        int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
        if (day >= 1 && day <= numArray[month] - numArray[month - 1])
        {
          int num = year - 1;
          return (long) (num * 365 + num / 4 - num / 100 + num / 400 + numArray[month - 1] + day - 1) * 864000000000L;
        }
      }
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    private static long TimeToTicks(int hour, int minute, int second)
    {
      if (hour >= 0 && hour < 24 && (minute >= 0 && minute < 60) && (second >= 0 && second < 60))
        return TimeSpan.TimeToTicks(hour, minute, second);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
    }

    /// <summary>
    ///   Возвращает число дней в указанном месяце указанного года.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">Месяц (число в диапазоне от 1 до 12).</param>
    /// <returns>
    ///   Число дней в <paramref name="month" /> для заданного <paramref name="year" />.
    /// 
    ///   Например, если параметр <paramref name="month" /> равен 2 для февраля, возвращаемым значением является 28 или 29 в зависимости от того, является ли <paramref name="year" /> високосным годом.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="month" /> меньше 1 или больше 12.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="year" /> меньше 1 или больше 9999.
    /// </exception>
    [__DynamicallyInvokable]
    public static int DaysInMonth(int year, int month)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      return numArray[month] - numArray[month - 1];
    }

    internal static long DoubleDateToTicks(double value)
    {
      if (value >= 2958466.0 || value <= -657435.0)
        throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
      long num1 = (long) (value * 86400000.0 + (value >= 0.0 ? 0.5 : -0.5));
      if (num1 < 0L)
        num1 -= num1 % 86400000L * 2L;
      long num2 = num1 + 59926435200000L;
      if (num2 < 0L || num2 >= 315537897600000L)
        throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateScale"));
      return num2 * 10000L;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool LegacyParseMode();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool EnableAmPmParseAdjustment();

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> является экземпляром типа <see cref="T:System.DateTime" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (value is DateTime)
        return this.InternalTicks == ((DateTime) value).InternalTicks;
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равно ли значение данного экземпляра значению указанного экземпляра <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> равен значению этого экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(DateTime value)
    {
      return this.InternalTicks == value.InternalTicks;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, содержат ли два экземпляра <see cref="T:System.DateTime" /> одно и то же значение даты и времени.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если два значения равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool Equals(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks == t2.InternalTicks;
    }

    /// <summary>
    ///   Десериализует 64-разрядное значение и воссоздает исходный сериализованный объект <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="dateData">
    ///   64-разрядное целое число со знаком, кодирующее свойство <see cref="P:System.DateTime.Kind" /> в 2-разрядное поле и свойство <see cref="P:System.DateTime.Ticks" /> в 62-разрядное поле.
    /// </param>
    /// <returns>
    ///   Объект, который эквивалентен объекту <see cref="T:System.DateTime" />, сериализованному методом <see cref="M:System.DateTime.ToBinary" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="dateData" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime FromBinary(long dateData)
    {
      if ((dateData & long.MinValue) == 0L)
        return DateTime.FromBinaryRaw(dateData);
      long ticks1 = dateData & 4611686018427387903L;
      if (ticks1 > 4611685154427387904L)
        ticks1 -= 4611686018427387904L;
      bool isAmbiguousLocalDst = false;
      long ticks2;
      if (ticks1 < 0L)
        ticks2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      else if (ticks1 > 3155378975999999999L)
      {
        ticks2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      }
      else
      {
        DateTime time = new DateTime(ticks1, DateTimeKind.Utc);
        bool isDaylightSavings = false;
        ticks2 = TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out isDaylightSavings, out isAmbiguousLocalDst).Ticks;
      }
      long ticks3 = ticks1 + ticks2;
      if (ticks3 < 0L)
        ticks3 += 864000000000L;
      if (ticks3 < 0L || ticks3 > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), nameof (dateData));
      return new DateTime(ticks3, DateTimeKind.Local, isAmbiguousLocalDst);
    }

    internal static DateTime FromBinaryRaw(long dateData)
    {
      long num = dateData & 4611686018427387903L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), nameof (dateData));
      return new DateTime((ulong) dateData);
    }

    /// <summary>
    ///   Преобразует заданное время файла Windows в его эквивалент по местному времени.
    /// </summary>
    /// <param name="fileTime">
    ///   Временная характеристика файла Windows, выраженная в тактах.
    /// </param>
    /// <returns>
    ///   Объект, представляющий местное время, эквивалентное дате и времени, которые определяются параметром <paramref name="fileTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="fileTime" /> меньше 0 или представляет время больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime FromFileTime(long fileTime)
    {
      return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
    }

    /// <summary>
    ///   Преобразует заданное время файла Windows в его UTC-эквивалент.
    /// </summary>
    /// <param name="fileTime">
    ///   Временная характеристика файла Windows, выраженная в тактах.
    /// </param>
    /// <returns>
    ///   Объект, представляющий время в формате UTC, эквивалентное дате и времени, которые определяются параметром <paramref name="fileTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="fileTime" /> меньше 0 или представляет время больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime FromFileTimeUtc(long fileTime)
    {
      if (fileTime < 0L || fileTime > 2650467743999999999L)
        throw new ArgumentOutOfRangeException(nameof (fileTime), Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
      return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" />, эквивалентный заданному значению даты OLE-автоматизации.
    /// </summary>
    /// <param name="d">Значение даты OLE-автоматизации.</param>
    /// <returns>
    ///   Объект, представляющий дату и время, которые совпадают с датой и временем, определяемыми параметром <paramref name="d" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Дата не является допустимым значением даты OLE-автоматизации.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime FromOADate(double d)
    {
      return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("ticks", this.InternalTicks);
      info.AddValue("dateData", this.dateData);
    }

    /// <summary>
    ///   Указывает, попадает ли данный экземпляр объекта <see cref="T:System.DateTime" /> в диапазон летнего времени для текущего часового пояса.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если значение свойства <see cref="P:System.DateTime.Kind" /> — <see cref="F:System.DateTimeKind.Local" /> или <see cref="F:System.DateTimeKind.Unspecified" /> и значение этого экземпляра <see cref="T:System.DateTime" /> находится в диапазоне летнего времени для локального часового пояса; значение <see langword="false" />, если <see cref="P:System.DateTime.Kind" /> — <see cref="F:System.DateTimeKind.Utc" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime()
    {
      if (this.Kind == DateTimeKind.Utc)
        return false;
      return TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.DateTime" />, имеющий то же количество тактов, что и заданный объект <see cref="T:System.DateTime" />, но предназначенный для использования с местным временем, со стандартом UTC, либо ни с тем, ни с другим, как определено значением <see cref="T:System.DateTimeKind" />.
    /// </summary>
    /// <param name="value">Дата и время.</param>
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее, что представляет новый объект: локальное время, универсальное глобальное время (UTC), или ни то, ни другое.
    /// </param>
    /// <returns>
    ///   Новый объект, имеющий то же количество тактов, что и объект, представленный параметром <paramref name="value" />, и значение <see cref="T:System.DateTimeKind" />, заданное параметром <paramref name="kind" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
    {
      return new DateTime(value.InternalTicks, kind);
    }

    /// <summary>
    ///   Сериализует текущий объект <see cref="T:System.DateTime" /> в 64-разрядное двоичное значение, которое может использоваться в дальнейшем для воссоздания объекта <see cref="T:System.DateTime" />.
    /// </summary>
    /// <returns>
    ///   64-разрядное целое число со знаком, кодирующее свойства <see cref="P:System.DateTime.Kind" /> и <see cref="P:System.DateTime.Ticks" />.
    /// </returns>
    [__DynamicallyInvokable]
    public long ToBinary()
    {
      if (this.Kind != DateTimeKind.Local)
        return (long) this.dateData;
      long num = this.Ticks - TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      if (num < 0L)
        num = 4611686018427387904L + num;
      return num | long.MinValue;
    }

    internal long ToBinaryRaw()
    {
      return (long) this.dateData;
    }

    /// <summary>Возвращает компоненту даты этого экземпляра.</summary>
    /// <returns>
    ///   Новый объект с такой же датой, как этот экземпляр и значением времени, равным полуночи (00:00:00).
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime Date
    {
      [__DynamicallyInvokable] get
      {
        long internalTicks = this.InternalTicks;
        return new DateTime((ulong) (internalTicks - internalTicks % 864000000000L) | this.InternalKind);
      }
    }

    private int GetDatePart(int part)
    {
      int num1 = (int) (this.InternalTicks / 864000000000L);
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
      int[] numArray = num8 == 3 && (num6 != 24 || num4 == 3) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      int index = num9 >> 6;
      while (num9 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num9 - numArray[index - 1] + 1;
    }

    internal void GetDatePart(out int year, out int month, out int day)
    {
      int num1 = (int) (this.InternalTicks / 864000000000L);
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
      year = num2 * 400 + num4 * 100 + num6 * 4 + num8 + 1;
      int num9 = num7 - num8 * 365;
      int[] numArray = num8 == 3 && (num6 != 24 || num4 == 3) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      int index = (num9 >> 5) + 1;
      while (num9 >= numArray[index])
        ++index;
      month = index;
      day = num9 - numArray[index - 1] + 1;
    }

    /// <summary>
    ///   Возвращает день месяца, представленный этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий день, выраженный как значение от 1 до 31.
    /// </returns>
    [__DynamicallyInvokable]
    public int Day
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(3);
      }
    }

    /// <summary>
    ///   Возвращает день недели, представленный этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Перечислимая константа, которая указывает на день недели для этого значения <see cref="T:System.DateTime" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DayOfWeek DayOfWeek
    {
      [__DynamicallyInvokable] get
      {
        return (DayOfWeek) ((this.InternalTicks / 864000000000L + 1L) % 7L);
      }
    }

    /// <summary>
    ///   Возвращает день года, представленный этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий день года, выраженный как значение от 1 до 366.
    /// </returns>
    [__DynamicallyInvokable]
    public int DayOfYear
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(1);
      }
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      long internalTicks = this.InternalTicks;
      return (int) internalTicks ^ (int) (internalTicks >> 32);
    }

    /// <summary>
    ///   Возвращает компонент часа даты, представленной этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий час, выраженный как значение от 0 до 23.
    /// </returns>
    [__DynamicallyInvokable]
    public int Hour
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 36000000000L % 24L);
      }
    }

    internal bool IsAmbiguousDaylightSavingTime()
    {
      return this.InternalKind == 13835058055282163712UL;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, на каком времени основано время, представленное этим экземпляром: местном, UTC или ни на том, ни на другом.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющее значение текущего времени.
    ///    Значение по умолчанию — <see cref="F:System.DateTimeKind.Unspecified" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTimeKind Kind
    {
      [__DynamicallyInvokable] get
      {
        switch (this.InternalKind)
        {
          case 0:
            return DateTimeKind.Unspecified;
          case 4611686018427387904:
            return DateTimeKind.Utc;
          default:
            return DateTimeKind.Local;
        }
      }
    }

    /// <summary>
    ///   Возвращает компонент миллисекунд для даты, представленной в данном экземпляре.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий миллисекунды, выраженный как значение от 0 до 999.
    /// </returns>
    [__DynamicallyInvokable]
    public int Millisecond
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 10000L % 1000L);
      }
    }

    /// <summary>
    ///   Возвращает компонент минуты даты, представленной этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий минуту, выраженный как значение от 0 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Minute
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 600000000L % 60L);
      }
    }

    /// <summary>
    ///   Возвращает компонент месяца даты, представленной этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий месяц, выраженный как значение от 1 до 12.
    /// </returns>
    [__DynamicallyInvokable]
    public int Month
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(2);
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" />, которому присвоены текущие дата и время данного компьютера, выраженные как местное время.
    /// </summary>
    /// <returns>
    ///   Объект, значение которого является текущим значением местной даты и времени.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTime Now
    {
      [__DynamicallyInvokable] get
      {
        DateTime utcNow = DateTime.UtcNow;
        bool isAmbiguousLocalDst = false;
        long ticks1 = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out isAmbiguousLocalDst).Ticks;
        long ticks2 = utcNow.Ticks + ticks1;
        if (ticks2 > 3155378975999999999L)
          return new DateTime(3155378975999999999L, DateTimeKind.Local);
        if (ticks2 < 0L)
          return new DateTime(0L, DateTimeKind.Local);
        return new DateTime(ticks2, DateTimeKind.Local, isAmbiguousLocalDst);
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.DateTime" />, которому присвоены текущие дата и время данного компьютера, выраженные в формате UTC.
    /// </summary>
    /// <returns>
    ///   Объект, значение которого является текущим значением даты и времени в формате UTC.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTime UtcNow
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return new DateTime((ulong) (DateTime.GetSystemTimeAsFileTime() + 504911232000000000L | 4611686018427387904L));
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern long GetSystemTimeAsFileTime();

    /// <summary>
    ///   Возвращает компонент секунды даты, представленной этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Компонент, представляющий секунды, выраженный как значение от 0 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Second
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 10000000L % 60L);
      }
    }

    /// <summary>
    ///   Возвращает число тактов, которое представляет дату и время этого экземпляра.
    /// </summary>
    /// <returns>
    ///   Число тактов, которое представляет дату и время этого экземпляра.
    ///    Это значение находится в диапазоне от <see langword="DateTime.MinValue.Ticks" /> до <see langword="DateTime.MaxValue.Ticks" />.
    /// </returns>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalTicks;
      }
    }

    /// <summary>Возвращает время дня для этого экземпляра.</summary>
    /// <returns>
    ///   Интервал времени, представляющий часть дня, прошедшую с полуночи.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan TimeOfDay
    {
      [__DynamicallyInvokable] get
      {
        return new TimeSpan(this.InternalTicks % 864000000000L);
      }
    }

    /// <summary>Возвращает текущую дату.</summary>
    /// <returns>
    ///   Объект, которому присвоена сегодняшняя дата, с компонентом времени, равным 00:00:00.
    /// </returns>
    [__DynamicallyInvokable]
    public static DateTime Today
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.Now.Date;
      }
    }

    /// <summary>
    ///   Возвращает компонент года даты, представленной этим экземпляром.
    /// </summary>
    /// <returns>Значение года в диапазоне от 1 до 9999.</returns>
    [__DynamicallyInvokable]
    public int Year
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(0);
      }
    }

    /// <summary>
    ///   Возвращает сведения о том, является ли указанный год високосным.
    /// </summary>
    /// <param name="year">4-значный номер года.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="year" /> является високосным годом; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="year" /> имеет значение меньше 1 или больше 9999.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsLeapYear(int year)
    {
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_Year"));
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
    }

    /// <summary>
    ///   Преобразует строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, содержащимся в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не содержит допустимое строковое представление даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s)
    {
      return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
    }

    /// <summary>
    ///   Преобразует строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, которые содержатся в параметре <paramref name="s" />, определяемом параметром <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не содержит допустимое строковое представление даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s, IFormatProvider provider)
    {
      return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
    }

    /// <summary>
    ///   Преобразует строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя указанные сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, определяющая элементы стиля, которые могут присутствовать в параметре <paramref name="s" /> для того, чтобы операция анализа прошла успешно. Эта комбинация определяет способ интерпретации анализируемых данных с учетом текущего часового пояса или текущей даты.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, которые содержатся в параметре <paramref name="s" />, определяемом параметрами <paramref name="provider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не содержит допустимое строковое представление даты и времени.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="styles" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" />.
    ///    Например, значения <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
    {
      DateTimeFormatInfo.ValidateStyles(styles, nameof (styles));
      return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Описатель формата, задающий требуемый формат <paramref name="s" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, которые содержатся в параметре <paramref name="s" />, определяемом параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> или <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="s" /> или <paramref name="format" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> не содержит дату и время, соответствующие шаблону, заданному в <paramref name="format" />.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="s" /> не совпадают.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider)
    {
      return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя заданный формат, указанные сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    ///    Формат строкового представления должен полностью соответствовать заданному формату. В противном случае возникает исключение.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Описатель формата, задающий требуемый формат <paramref name="s" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая предоставляет дополнительную информацию о параметре <paramref name="s" />, об элементах стиля, которые могут присутствовать в параметре <paramref name="s" />, или о преобразовании из параметра <paramref name="s" /> в значение <see cref="T:System.DateTime" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, которые содержатся в параметре <paramref name="s" />, определяемом параметрами <paramref name="format" />, <paramref name="provider" /> и <paramref name="style" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> или <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="s" /> или <paramref name="format" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> не содержит дату и время, соответствующие шаблону, заданному в <paramref name="format" />.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="s" /> не совпадают.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" />.
    ///    Например, значения <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
    {
      DateTimeFormatInfo.ValidateStyles(style, nameof (style));
      return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя заданный массив форматов, указанные сведения о форматировании, связанные с языком и региональными параметрами, и стиль форматирования.
    ///    Формат строкового представления должен полностью соответствовать по крайней мере одному из заданных форматов. В противном случае возникает исключение.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formats">
    ///   Массив разрешенных форматов <paramref name="s" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <returns>
    ///   Объект, эквивалентный дате и времени, которые содержатся в параметре <paramref name="s" />, определяемом параметрами <paramref name="formats" />, <paramref name="provider" /> и <paramref name="style" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> или <paramref name="formats" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="s" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="formats" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> не содержит дату и время, соответствующие элементу <paramref name="formats" />.
    /// 
    ///   -или-
    /// 
    ///   Компонент часов и обозначение AM/PM в <paramref name="s" /> не совпадают.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" />.
    ///    Например, значения <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
    {
      DateTimeFormatInfo.ValidateStyles(style, nameof (style));
      return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
    }

    /// <summary>
    ///   Вычитает из этого экземпляра указанную дату и время.
    /// </summary>
    /// <param name="value">Вычитаемые дата и время.</param>
    /// <returns>
    ///   Интервал времени, равный разнице между датой и временем, представленными этим экземпляром, и датой и временем, представленными параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результат меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(DateTime value)
    {
      return new TimeSpan(this.InternalTicks - value.InternalTicks);
    }

    /// <summary>
    ///   Вычитает из этого экземпляра указанную длительность.
    /// </summary>
    /// <param name="value">Вычитаемый интервал времени.</param>
    /// <returns>
    ///   Объект, равный разнице между датой и временем, представленными этим экземпляром, и интервалом времени, представленным параметром <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результат меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DateTime Subtract(TimeSpan value)
    {
      long internalTicks = this.InternalTicks;
      long ticks = value._ticks;
      if (internalTicks - 0L < ticks || internalTicks - 3155378975999999999L > ticks)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks - ticks) | this.InternalKind);
    }

    private static double TicksToOADate(long value)
    {
      if (value == 0L)
        return 0.0;
      if (value < 864000000000L)
        value += 599264352000000000L;
      if (value < 31241376000000000L)
        throw new OverflowException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
      long num1 = (value - 599264352000000000L) / 10000L;
      if (num1 < 0L)
      {
        long num2 = num1 % 86400000L;
        if (num2 != 0L)
          num1 -= (86400000L + num2) * 2L;
      }
      return (double) num1 / 86400000.0;
    }

    /// <summary>
    ///   Преобразует числовое значение этого экземпляра в эквивалентное ему значение даты OLE-автоматизации.
    /// </summary>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, содержащее значение даты OLE-автоматизации, эквивалентное значению этого экземпляра.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение этого экземпляра невозможно представить в виде даты автоматизации OLE.
    /// </exception>
    [__DynamicallyInvokable]
    public double ToOADate()
    {
      return DateTime.TicksToOADate(this.InternalTicks);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> во временную характеристику файла Windows.
    /// </summary>
    /// <returns>
    ///   Значение текущего объекта <see cref="T:System.DateTime" />, представленное в виде временной характеристики файла Windows.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Получившееся время файла представит дату и время до 0:00 1 января 1601 года нашей эры в формате UTC.
    /// </exception>
    [__DynamicallyInvokable]
    public long ToFileTime()
    {
      return this.ToUniversalTime().ToFileTimeUtc();
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> во временную характеристику файла Windows.
    /// </summary>
    /// <returns>
    ///   Значение текущего объекта <see cref="T:System.DateTime" />, представленное в виде временной характеристики файла Windows.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Получившееся время файла представит дату и время до 0:00 1 января 1601 года нашей эры в формате UTC.
    /// </exception>
    [__DynamicallyInvokable]
    public long ToFileTimeUtc()
    {
      long num = (((long) this.InternalKind & long.MinValue) != 0L ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
      if (num < 0L)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
      return num;
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в местное время.
    /// </summary>
    /// <returns>
    ///   Объект, свойство <see cref="P:System.DateTime.Kind" /> которого имеет значение <see cref="F:System.DateTimeKind.Local" />, а значение является либо местным временем, эквивалентным значению текущего объекта <see cref="T:System.DateTime" />, либо значением <see cref="F:System.DateTime.MaxValue" />, если преобразованное значение слишком велико для представления объектом <see cref="T:System.DateTime" />, либо значением <see cref="F:System.DateTime.MinValue" />, если преобразованное значение слишком мало для представления объектом <see cref="T:System.DateTime" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime ToLocalTime()
    {
      return this.ToLocalTime(false);
    }

    internal DateTime ToLocalTime(bool throwOnOverflow)
    {
      if (this.Kind == DateTimeKind.Local)
        return this;
      bool isDaylightSavings = false;
      bool isAmbiguousLocalDst = false;
      long ticks = this.Ticks + TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out isDaylightSavings, out isAmbiguousLocalDst).Ticks;
      if (ticks > 3155378975999999999L)
      {
        if (throwOnOverflow)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
        return new DateTime(3155378975999999999L, DateTimeKind.Local);
      }
      if (ticks >= 0L)
        return new DateTime(ticks, DateTimeKind.Local, isAmbiguousLocalDst);
      if (throwOnOverflow)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      return new DateTime(0L, DateTimeKind.Local);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему длинное строковое представление даты.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая длинное строковое представление даты текущего объекта <see cref="T:System.DateTime" />.
    /// </returns>
    public string ToLongDateString()
    {
      return DateTimeFormat.Format(this, "D", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему длинное строковое представление времени.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая длинное строковое представление времени текущего объекта <see cref="T:System.DateTime" />.
    /// </returns>
    public string ToLongTimeString()
    {
      return DateTimeFormat.Format(this, "T", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему короткое строковое представление даты.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая короткое строковое представление даты текущего объекта <see cref="T:System.DateTime" />.
    /// </returns>
    public string ToShortDateString()
    {
      return DateTimeFormat.Format(this, "d", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему короткое строковое представление времени.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая короткое строковое представление времени текущего объекта <see cref="T:System.DateTime" />.
    /// </returns>
    public string ToShortTimeString()
    {
      return DateTimeFormat.Format(this, "t", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное строковое представление с помощью соглашений о форматировании для текущего языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, принятым для текущего языка и региональных параметров.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return DateTimeFormat.Format(this, (string) null, DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное строковое представление с использованием указанного формата и соглашений о форматировании, принятых для текущего языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Строка стандартного или пользовательского формата даты и времени (см. примечания).
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTime" />, заданное параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="format" /> равна 1, и он не является одним из символов описателя формата, определенного для <see cref="T:System.Globalization.DateTimeFormatInfo" />.
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
      return DateTimeFormat.Format(this, format, DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему строковое представление с использованием указанных сведений о форматировании, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTime" />, заданное параметром <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, который используется у <paramref name="provider" />.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return DateTimeFormat.Format(this, (string) null, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях формата для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Строка стандартного или пользовательского формата даты и времени.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.DateTime" />, заданное параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="format" /> равна 1, и он не является одним из символов описателя формата, определенного для <see cref="T:System.Globalization.DateTimeFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="format" /> не содержит допустимого шаблона пользовательского формата.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Дата и время находятся за пределами диапазона дат, поддерживаемого календарем, который используется у <paramref name="provider" />.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return DateTimeFormat.Format(this, format, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.DateTime" /> во время UTC.
    /// </summary>
    /// <returns>
    ///   Объект, свойство <see cref="P:System.DateTime.Kind" /> которого имеет значение <see cref="F:System.DateTimeKind.Utc" />, а значение является либо временем в формате UTC, эквивалентным значению текущего объекта <see cref="T:System.DateTime" />, либо значением <see cref="F:System.DateTime.MaxValue" />, если преобразованное значение слишком велико для представления объектом <see cref="T:System.DateTime" />, либо значением <see cref="F:System.DateTime.MinValue" />, если преобразованное значение слишком мало для представления объектом <see cref="T:System.DateTime" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DateTime ToUniversalTime()
    {
      return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    /// <summary>
    ///   Преобразовывает указанное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" /> и возвращает значение, позволяющее определить успешность преобразования.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит значение <see cref="T:System.DateTime" />, эквивалентное дате и времени, заданным в параметре <paramref name="s" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTime.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если значение параметра <paramref name="s" /> равно <see langword="null" /> или пустой строке ("") или не содержит допустимого строкового представления даты и времени.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out DateTime result)
    {
      return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя указанную информацию о форматировании, связанную с языком и региональными параметрами, и возвращает значение, которое показывает успешность преобразования.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Побитовая комбинация значений перечисления, которая определяет, как интерпретировать проанализированную дату по отношению к текущему часовому поясу или текущей дате.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит значение <see cref="T:System.DateTime" />, эквивалентное дате и времени, заданным в параметре <paramref name="s" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTime.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если значение параметра <paramref name="s" /> равно <see langword="null" /> или пустой строке ("") или не содержит допустимого строкового представления даты и времени.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="styles" /> не является допустимым значением <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" /> (например, и <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" />, и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" /> ).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="provider" /> является нейтральным языком и региональными параметрами и не может использоваться в операции анализа.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(styles, nameof (styles));
      return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя заданный формат, указанные сведения о форматировании, связанные с языком и региональными параметрами, а также стиль.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    ///    Метод возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="format">
    ///   Необходимый формат <paramref name="s" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация одного или нескольких значений перечисления, которые указывают на разрешенный формат параметра <paramref name="s" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит значение <see cref="T:System.DateTime" />, эквивалентное дате и времени, заданным в параметре <paramref name="s" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTime.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если значением параметра <paramref name="s" /> или <paramref name="format" /> является <see langword="null" /> либо пустая строка или не является дата и время, соответствующие шаблону, заданному в параметре <paramref name="format" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="styles" /> не является допустимым значением <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" /> (например, и <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" />, и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" /> ).
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(style, nameof (style));
      return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в его эквивалент <see cref="T:System.DateTime" />, используя заданный массив форматов, указанные сведения о форматировании, связанные с языком и региональными параметрами, и стиль форматирования.
    ///    Формат представления строки должен полностью соответствовать хотя бы одному заданному формату.
    ///    Метод возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="formats">
    ///   Массив разрешенных форматов <paramref name="s" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.DateTimeStyles.None" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит значение <see cref="T:System.DateTime" />, эквивалентное дате и времени, заданным в параметре <paramref name="s" />, если преобразование прошло успешно, или значение <see cref="F:System.DateTime.MinValue" />, если преобразование завершилось неудачей.
    ///    Преобразование завершается неудачей, если параметр <paramref name="s" /> или <paramref name="formats" /> имеет значение <see langword="null" />, либо если параметр <paramref name="s" /> или элемент <paramref name="formats" /> является пустой строкой, либо если формат параметра <paramref name="s" /> не в точности соответствует заданному хотя бы одним из шаблонов формата в <paramref name="formats" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="styles" /> не является допустимым значением <see cref="T:System.Globalization.DateTimeStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="styles" /> содержит недопустимое сочетание значений <see cref="T:System.Globalization.DateTimeStyles" /> (например, и <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" />, и <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" /> ).
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(style, nameof (style));
      return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
    }

    /// <summary>
    ///   Прибавляет указанный временной интервал к заданной дате и времени, возвращая новую дату и время.
    /// </summary>
    /// <param name="d">Добавляемое значение даты и времени.</param>
    /// <param name="t">Добавляемый интервал времени.</param>
    /// <returns>
    ///   Объект, значение которого является суммой значений <paramref name="d" /> и <paramref name="t" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime operator +(DateTime d, TimeSpan t)
    {
      long internalTicks = d.InternalTicks;
      long ticks = t._ticks;
      if (ticks > 3155378975999999999L - internalTicks || ticks < -internalTicks)
        throw new ArgumentOutOfRangeException(nameof (t), Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks + ticks) | d.InternalKind);
    }

    /// <summary>
    ///   Вычитает заданный временной интервал из указанной даты и времени и возвращает новую дату и время.
    /// </summary>
    /// <param name="d">
    ///   Значение даты и времени, из которого производится вычитание.
    /// </param>
    /// <param name="t">Вычитаемый интервал времени.</param>
    /// <returns>
    ///   Объект, значение которого является разностью значений <paramref name="d" /> и <paramref name="t" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговое значение <see cref="T:System.DateTime" /> меньше <see cref="F:System.DateTime.MinValue" /> или больше <see cref="F:System.DateTime.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime operator -(DateTime d, TimeSpan t)
    {
      long internalTicks = d.InternalTicks;
      long ticks = t._ticks;
      if (internalTicks - 0L < ticks || internalTicks - 3155378975999999999L > ticks)
        throw new ArgumentOutOfRangeException(nameof (t), Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks - ticks) | d.InternalKind);
    }

    /// <summary>
    ///   Вычитает указанную дату и время из другой указанной даты и времени и возвращает временной интервал.
    /// </summary>
    /// <param name="d1">
    ///   Значение даты и времени, из которого вычитается интервал (уменьшаемое).
    /// </param>
    /// <param name="d2">
    ///   Значение даты и времени, которое вычитается (вычитаемое).
    /// </param>
    /// <returns>
    ///   Временной интервал между значениями <paramref name="d1" /> и <paramref name="d2" />, то есть <paramref name="d1" /> минус <paramref name="d2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(DateTime d1, DateTime d2)
    {
      return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
    }

    /// <summary>
    ///   Определяет, равны ли два заданных экземпляра класса <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="d1">Первый из сравниваемых объектов.</param>
    /// <param name="d2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметры <paramref name="d1" /> и <paramref name="d2" /> представляют одинаковую дату и время; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(DateTime d1, DateTime d2)
    {
      return d1.InternalTicks == d2.InternalTicks;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных экземпляра класса <see cref="T:System.DateTime" /> неравными.
    /// </summary>
    /// <param name="d1">Первый из сравниваемых объектов.</param>
    /// <param name="d2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметры <paramref name="d1" /> и <paramref name="d2" /> не представляют одинаковую дату и время; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(DateTime d1, DateTime d2)
    {
      return d1.InternalTicks != d2.InternalTicks;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного объекта <see cref="T:System.DateTime" /> более ранним относительно другого заданного объекта <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> раньше значения <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks < t2.InternalTicks;
    }

    /// <summary>
    ///   Определяет, представляет ли заданный объект <see cref="T:System.DateTime" /> дату и время, которые совпадают со значением другого заданного объекта <see cref="T:System.DateTime" /> или являются более ранними относительно него.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="t1" /> происходит одновременно или раньше <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <=(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks <= t2.InternalTicks;
    }

    /// <summary>
    ///   Определяет, является ли значение одного заданного объекта <see cref="T:System.DateTime" /> более поздним относительно другого заданного объекта <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> позже значения <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks > t2.InternalTicks;
    }

    /// <summary>
    ///   Определяет, представляет ли заданный объект <see cref="T:System.DateTime" /> дату и время, которые совпадают со значением другого заданного объекта <see cref="T:System.DateTime" /> или являются более поздними относительно него.
    /// </summary>
    /// <param name="t1">Первый из сравниваемых объектов.</param>
    /// <param name="t2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="t1" /> происходит одновременно или позже <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >=(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks >= t2.InternalTicks;
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра во все строковые представления, поддерживаемые стандартным форматом даты и времени.
    /// </summary>
    /// <returns>
    ///   Массив строк, каждый элемент которого является представлением значения этого экземпляра, отформатированным с использованием одного из стандартных спецификаторов формата даты и времени.
    /// </returns>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats()
    {
      return this.GetDateTimeFormats((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра во все строковые представления, поддерживаемые стандартными спецификаторами формата даты и времени и указанными сведениями о форматировании, связанными с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, который представляет связанную с языком и региональными параметрами информацию о форматировании этого экземпляра.
    /// </param>
    /// <returns>
    ///   Массив строк, каждый элемент которого является представлением значения этого экземпляра, отформатированным с использованием одного из стандартных спецификаторов формата даты и времени.
    /// </returns>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(IFormatProvider provider)
    {
      return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра во все строковые представления, поддерживаемые указанным стандартным спецификатором формата даты и времени.
    /// </summary>
    /// <param name="format">
    ///   Стандартная строка формата даты и времени (см. примечания).
    /// </param>
    /// <returns>
    ///   Массив строк, каждый элемент которого является представлением значения этого экземпляра, отформатированным с использованием стандартного спецификатора формата даты и времени <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым символом стандартного спецификатора формата даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(char format)
    {
      return this.GetDateTimeFormats(format, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра во все строковые представления, поддерживаемые указанным стандартным спецификатором формата даты и времени и сведениями о форматировании, связанными с языком и региональными параметрами.
    /// </summary>
    /// <param name="format">
    ///   Строка формата даты и времени (см. примечания).
    /// </param>
    /// <param name="provider">
    ///   Объект, который представляет связанную с языком и региональными параметрами информацию о форматировании этого экземпляра.
    /// </param>
    /// <returns>
    ///   Массив строк, каждый элемент которого является представлением значения этого экземпляра, отформатированным с использованием одного из стандартных спецификаторов формата даты и времени.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым символом стандартного спецификатора формата даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(char format, IFormatProvider provider)
    {
      return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.DateTime" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.DateTime" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.DateTime;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Boolean"));
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Char"));
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "SByte"));
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Byte"));
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Int16"));
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "UInt16"));
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Int32"));
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "UInt32"));
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Int64"));
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "UInt64"));
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Single"));
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Double"));
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (DateTime), (object) "Decimal"));
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
    {
      result = DateTime.MinValue;
      if (year < 1 || year > 9999 || (month < 1 || month > 12))
        return false;
      int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      if (day < 1 || day > numArray[month] - numArray[month - 1] || (hour < 0 || hour >= 24) || (minute < 0 || minute >= 60 || (second < 0 || second >= 60)) || (millisecond < 0 || millisecond >= 1000))
        return false;
      long ticks = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
      if (ticks < 0L || ticks > 3155378975999999999L)
        return false;
      result = new DateTime(ticks, DateTimeKind.Unspecified);
      return true;
    }
  }
}
