// Decompiled with JetBrains decompiler
// Type: System.TimeSpan
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет интервал времени.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable
  {
    /// <summary>
    ///   Представляет нулевое значение <see cref="T:System.TimeSpan" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly TimeSpan Zero = new TimeSpan(0L);
    /// <summary>
    ///   Представляет максимальное значение <see cref="T:System.TimeSpan" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);
    /// <summary>
    ///   Представляет минимальное значение <see cref="T:System.TimeSpan" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);
    /// <summary>
    ///   Возвращает количество тактов в 1 миллисекунде.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const long TicksPerMillisecond = 10000;
    private const double MillisecondsPerTick = 0.0001;
    /// <summary>Возвращает количество тактов в 1 секунде.</summary>
    [__DynamicallyInvokable]
    public const long TicksPerSecond = 10000000;
    private const double SecondsPerTick = 1E-07;
    /// <summary>
    ///   Возвращает количество тактов в 1 минуте.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const long TicksPerMinute = 600000000;
    private const double MinutesPerTick = 1.66666666666667E-09;
    /// <summary>
    ///   Представляет количество тактов в 1 часе.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const long TicksPerHour = 36000000000;
    private const double HoursPerTick = 2.77777777777778E-11;
    /// <summary>
    ///   Возвращает количество тактов в 1 сутках.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const long TicksPerDay = 864000000000;
    private const double DaysPerTick = 1.15740740740741E-12;
    private const int MillisPerSecond = 1000;
    private const int MillisPerMinute = 60000;
    private const int MillisPerHour = 3600000;
    private const int MillisPerDay = 86400000;
    internal const long MaxSeconds = 922337203685;
    internal const long MinSeconds = -922337203685;
    internal const long MaxMilliSeconds = 922337203685477;
    internal const long MinMilliSeconds = -922337203685477;
    internal const long TicksPerTenthSecond = 1000000;
    internal long _ticks;
    private static volatile bool _legacyConfigChecked;
    private static volatile bool _legacyMode;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.TimeSpan" /> заданным числом тактов.
    /// </summary>
    /// <param name="ticks">
    ///   Интервал времени, выраженный в единицах измерения, равных 100 нс.
    /// </param>
    [__DynamicallyInvokable]
    public TimeSpan(long ticks)
    {
      this._ticks = ticks;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.TimeSpan" /> заданным количеством часов, минут и секунд.
    /// </summary>
    /// <param name="hours">Количество часов.</param>
    /// <param name="minutes">Количество минут.</param>
    /// <param name="seconds">Количество секунд.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметры указывают значение <see cref="T:System.TimeSpan" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan(int hours, int minutes, int seconds)
    {
      this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.TimeSpan" /> заданным количеством дней, часов, минут и секунд.
    /// </summary>
    /// <param name="days">Количество дней.</param>
    /// <param name="hours">Количество часов.</param>
    /// <param name="minutes">Количество минут.</param>
    /// <param name="seconds">Количество секунд.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметры указывают значение <see cref="T:System.TimeSpan" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan(int days, int hours, int minutes, int seconds)
    {
      this = new TimeSpan(days, hours, minutes, seconds, 0);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.TimeSpan" /> заданным количеством дней, часов, минут, секунд и миллисекунд.
    /// </summary>
    /// <param name="days">Количество дней.</param>
    /// <param name="hours">Количество часов.</param>
    /// <param name="minutes">Количество минут.</param>
    /// <param name="seconds">Количество секунд.</param>
    /// <param name="milliseconds">Количество миллисекунд.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметры указывают значение <see cref="T:System.TimeSpan" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
    {
      long num = ((long) days * 3600L * 24L + (long) hours * 3600L + (long) minutes * 60L + (long) seconds) * 1000L + (long) milliseconds;
      if (num > 922337203685477L || num < -922337203685477L)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      this._ticks = num * 10000L;
    }

    /// <summary>
    ///   Возвращает количество тактов, представляющее значение текущей структуры <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>Количество тактов, содержащихся в этом экземпляре.</returns>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this._ticks;
      }
    }

    /// <summary>
    ///   Возвращает компонент дней периода времени, представленного текущей структурой <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Компонент дня данного экземпляра.
    ///    Возвращаемое значение может быть положительным или отрицательным.
    /// </returns>
    [__DynamicallyInvokable]
    public int Days
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 864000000000L);
      }
    }

    /// <summary>
    ///   Возвращает компонент часов периода времени, представленного текущей структурой <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Компонент текущей структуры <see cref="T:System.TimeSpan" />, представляющий количество часов.
    ///    Возвращаемое значение лежит в диапазоне от -23 до 23.
    /// </returns>
    [__DynamicallyInvokable]
    public int Hours
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 36000000000L % 24L);
      }
    }

    /// <summary>
    ///   Возвращает компонент миллисекунд периода времени, представленного текущей структурой <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Компонент текущей структуры <see cref="T:System.TimeSpan" />, представляющий количество миллисекунд.
    ///    Возвращаемое значение лежит в диапазоне от -999 до 999.
    /// </returns>
    [__DynamicallyInvokable]
    public int Milliseconds
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 10000L % 1000L);
      }
    }

    /// <summary>
    ///   Возвращает компонент минут периода времени, представленного текущей структурой <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Компонент текущей структуры <see cref="T:System.TimeSpan" />, представляющий количество минут.
    ///    Возвращаемое значение лежит в диапазоне от -59 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Minutes
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 600000000L % 60L);
      }
    }

    /// <summary>
    ///   Возвращает компонент секунд периода времени, представленного текущей структурой <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Компонент текущей структуры <see cref="T:System.TimeSpan" />, представляющий количество секунд.
    ///    Возвращаемое значение лежит в диапазоне от -59 до 59.
    /// </returns>
    [__DynamicallyInvokable]
    public int Seconds
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 10000000L % 60L);
      }
    }

    /// <summary>
    ///   Возвращает значение текущей структуры <see cref="T:System.TimeSpan" />, выраженное как целое и дробное количество дней.
    /// </summary>
    /// <returns>
    ///   Общее количество дней в периоде, представленном этим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public double TotalDays
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1.15740740740741E-12;
      }
    }

    /// <summary>
    ///   Получает значение текущей структуры <see cref="T:System.TimeSpan" />, выраженное как целое и дробное количество часов.
    /// </summary>
    /// <returns>
    ///   Общее количество часов в периоде, представленном этим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public double TotalHours
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 2.77777777777778E-11;
      }
    }

    /// <summary>
    ///   Получает значение текущей структуры <see cref="T:System.TimeSpan" />, выраженное как целое и дробное количество миллисекунд.
    /// </summary>
    /// <returns>
    ///   Общее количество миллисекунд в периоде, представленном этим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public double TotalMilliseconds
    {
      [__DynamicallyInvokable] get
      {
        double num = (double) this._ticks * 0.0001;
        if (num > 922337203685477.0)
          return 922337203685477.0;
        if (num < -922337203685477.0)
          return -922337203685477.0;
        return num;
      }
    }

    /// <summary>
    ///   Возвращает значение текущей структуры <see cref="T:System.TimeSpan" />, выраженное как целое и дробное количество минут.
    /// </summary>
    /// <returns>
    ///   Общее количество минут в периоде, представленном этим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public double TotalMinutes
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1.66666666666667E-09;
      }
    }

    /// <summary>
    ///   Возвращает значение текущей структуры <see cref="T:System.TimeSpan" />, выраженное как целое и дробное количество секунд.
    /// </summary>
    /// <returns>
    ///   Общее количество секунд в периоде, представленном этим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public double TotalSeconds
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1E-07;
      }
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.TimeSpan" />, значение которого равно сумме указанного объекта <see cref="T:System.TimeSpan" /> и данного экземпляра.
    /// </summary>
    /// <param name="ts">Добавляемый интервал времени.</param>
    /// <returns>
    ///   Новый объект, представляющий сумму значений данного экземпляра и параметра <paramref name="ts" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Итоговое значение <see cref="T:System.TimeSpan" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan Add(TimeSpan ts)
    {
      long ticks = this._ticks + ts._ticks;
      if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != ticks >> 63)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan(ticks);
    }

    /// <summary>
    ///   Сравнивает два значения <see cref="T:System.TimeSpan" /> и возвращает целое значение, которое показывает, является ли первое значение короче, равно или длиннее второго значения.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    /// Одно из следующих значений.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         -1
    /// 
    ///         <paramref name="t1" />короче, чем <paramref name="t2" />.
    /// 
    ///         0
    /// 
    ///         <paramref name="t1" /> равно <paramref name="t2" />.
    /// 
    ///         1
    /// 
    ///         <paramref name="t1" />длиннее, чем <paramref name="t2" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Compare(TimeSpan t1, TimeSpan t2)
    {
      if (t1._ticks > t2._ticks)
        return 1;
      return t1._ticks < t2._ticks ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным объектом и возвращает целое число, которое показывает, как соотносится данный экземпляр с указанным объектом: короче него, равен ему или длиннее указанного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Одно из следующих значений.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         -1
    /// 
    ///         Этот экземпляр меньше, чем <paramref name="value" />.
    /// 
    ///         0
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         1
    /// 
    ///         Этот экземпляр длиннее, чем <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.TimeSpan" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is TimeSpan))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeTimeSpan"));
      long ticks = ((TimeSpan) value)._ticks;
      if (this._ticks > ticks)
        return 1;
      return this._ticks < ticks ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным объектом <see cref="T:System.TimeSpan" /> и возвращает целое число, которое показывает, как соотносится данный экземпляр с объектом <see cref="T:System.TimeSpan" />: короче него, равен ему или длиннее указанного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         Отрицательное целое число
    /// 
    ///         Этот экземпляр меньше, чем <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Положительное целое число
    /// 
    ///         Этот экземпляр длиннее, чем <paramref name="value" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(TimeSpan value)
    {
      long ticks = value._ticks;
      if (this._ticks > ticks)
        return 1;
      return this._ticks < ticks ? -1 : 0;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий заданное количество дней, округленное до ближайшей миллисекунды.
    /// </summary>
    /// <param name="value">
    ///   Количество дней, округленное до ближайшей миллисекунды.
    /// </param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.PositiveInfinity" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan FromDays(double value)
    {
      return TimeSpan.Interval(value, 86400000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.TimeSpan" />, значением которого является абсолютное значение текущего объекта <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <returns>
    ///   Новый объект, значением которого является абсолютное значение текущего объекта <see cref="T:System.TimeSpan" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Этот экземпляр имеет значение <see cref="F:System.TimeSpan.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan Duration()
    {
      if (this.Ticks == TimeSpan.MinValue.Ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_Duration"));
      return new TimeSpan(this._ticks >= 0L ? this._ticks : -this._ticks);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> является объектом <see cref="T:System.TimeSpan" /> и представляет тот же интервал времени, что и текущий объект <see cref="T:System.TimeSpan" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (value is TimeSpan)
        return this._ticks == ((TimeSpan) value)._ticks;
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли данный экземпляр заданному объекту <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="obj" /> представляет то т же интервал времени, что и данный экземпляр; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(TimeSpan obj)
    {
      return this._ticks == obj._ticks;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равны ли два заданных экземпляра <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения параметров <paramref name="t1" /> и <paramref name="t2" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool Equals(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks == t2._ticks;
    }

    /// <summary>Возвращает хэш-код для данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this._ticks ^ (int) (this._ticks >> 32);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий указанное количество часов, округленное до ближайшей миллисекунды.
    /// </summary>
    /// <param name="value">
    ///   Количество часов, округленное до ближайшей миллисекунды.
    /// </param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.PositiveInfinity" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan FromHours(double value)
    {
      return TimeSpan.Interval(value, 3600000);
    }

    private static TimeSpan Interval(double value, int scale)
    {
      if (double.IsNaN(value))
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotBeNaN"));
      double num = value * (double) scale + (value >= 0.0 ? 0.5 : -0.5);
      if (num > 922337203685477.0 || num < -922337203685477.0)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan((long) num * 10000L);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий указанное количество миллисекунд.
    /// </summary>
    /// <param name="value">Количество миллисекунд.</param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.PositiveInfinity" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan FromMilliseconds(double value)
    {
      return TimeSpan.Interval(value, 1);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий указанное количество минут, округленное до ближайшей миллисекунды.
    /// </summary>
    /// <param name="value">
    ///   Количество минут, округленное до ближайшей миллисекунды.
    /// </param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.PositiveInfinity" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan FromMinutes(double value)
    {
      return TimeSpan.Interval(value, 60000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.TimeSpan" />, значение которого равно значению данного экземпляра с противоположным знаком.
    /// </summary>
    /// <returns>
    ///   Новый объект, числовое значение которого совпадает со значением данного экземпляра, но с противоположным знаком.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Инвертированное значение этого экземпляра не может быть представлено <see cref="T:System.TimeSpan" />, т. е. этот экземпляр имеет значение <see cref="F:System.TimeSpan.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan Negate()
    {
      if (this.Ticks == TimeSpan.MinValue.Ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return new TimeSpan(-this._ticks);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий указанное количество секунд, округленное до ближайшей миллисекунды.
    /// </summary>
    /// <param name="value">
    ///   Количество секунд, округленное до ближайшей миллисекунды.
    /// </param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.PositiveInfinity" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan FromSeconds(double value)
    {
      return TimeSpan.Interval(value, 1000);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.TimeSpan" />, значение которого равно разности между указанным объектом <see cref="T:System.TimeSpan" /> и данным экземпляром.
    /// </summary>
    /// <param name="ts">Интервал времени, который будет вычтен.</param>
    /// <returns>
    ///   Новый интервал времени, значение которого является результатом вычитания значения параметра <paramref name="ts" /> из данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(TimeSpan ts)
    {
      long ticks = this._ticks - ts._ticks;
      if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != ticks >> 63)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan(ticks);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" />, представляющий заданное время в тактах.
    /// </summary>
    /// <param name="value">
    ///   Количество тактов, представляющее время.
    /// </param>
    /// <returns>
    ///   Объект, представляющий объект <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TimeSpan FromTicks(long value)
    {
      return new TimeSpan(value);
    }

    internal static long TimeToTicks(int hour, int minute, int second)
    {
      long num = (long) hour * 3600L + (long) minute * 60L + (long) second;
      if (num > 922337203685L || num < -922337203685L)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return num * 10000000L;
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет недопустимый формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Как минимум один из компонентов дней, часов, минут или секунд выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan Parse(string s)
    {
      return TimeSpanParse.Parse(s, (IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="input" />, в виде, заданном параметром <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="input" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Хотя бы один из компонентов дней, часов, минут или секунд в <paramref name="input" /> выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan Parse(string input, IFormatProvider formatProvider)
    {
      return TimeSpanParse.Parse(input, formatProvider);
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные формат и сведения о форматировании, связанные с языком и региональными параметрами.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата, которая определяет требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="input" />, в виде, заданном параметрами <paramref name="format" /> и <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="input" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Хотя бы один из компонентов дней, часов, минут или секунд в <paramref name="input" /> выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанный массив строк форматирования и сведения о форматировании, связанные с языком и региональными параметрами.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formats">
    ///   Массив стандартных или пользовательских строк формата, которые определяют требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="input" />, в виде, заданном параметрами <paramref name="formats" /> и <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="input" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Хотя бы один из компонентов дней, часов, минут или секунд в <paramref name="input" /> выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
    {
      return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные формат, сведения о форматировании, связанные с языком и региональными параметрами, и стили.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата, которая определяет требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <param name="styles">
    ///   Побитовое сочетание значений перечисления, определяющее элементы стиля, которые могут присутствовать в параметре <paramref name="input" />.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="input" />, в виде, заданном параметрами <paramref name="format" />, <paramref name="formatProvider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="styles" /> имеет недопустимое значение <see cref="T:System.Globalization.TimeSpanStyles" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="input" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Хотя бы один из компонентов дней, часов, минут или секунд в <paramref name="input" /> выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.ValidateStyles(styles, nameof (styles));
      return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
    }

    /// <summary>
    ///   Преобразует строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные форматы, сведения о форматировании, связанные с языком и региональными параметрами, и стили.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formats">
    ///   Массив стандартных или пользовательских строк формата, которые определяют требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <param name="styles">
    ///   Побитовое сочетание значений перечисления, определяющее элементы стиля, которые могут присутствовать в параметре input.
    /// </param>
    /// <returns>
    ///   Интервал времени, соответствующий параметру <paramref name="input" />, в виде, заданном параметрами <paramref name="formats" />, <paramref name="formatProvider" /> и <paramref name="styles" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="styles" /> имеет недопустимое значение <see cref="T:System.Globalization.TimeSpanStyles" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="input" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="input" /> представляет число, которое меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Хотя бы один из компонентов дней, часов, минут или секунд в <paramref name="input" /> выходит за пределы допустимого диапазона.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.ValidateStyles(styles, nameof (styles));
      return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
    }

    /// <summary>
    ///   Преобразовывает строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" /> и возвращает значение, позволяющее определить успешность преобразования.
    /// </summary>
    /// <param name="s">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="s" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    ///    Эта операция возвращает значение <see langword="false" />, если параметр <paramref name="s" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />, либо его формат недопустим, либо он представляет интервал времени, меньший <see cref="F:System.TimeSpan.MinValue" /> или больший <see cref="F:System.TimeSpan.MaxValue" />, либо минимум один из его компонентов — количество дней, часов, минут или секунд — находится вне допустимого диапазона.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out TimeSpan result)
    {
      return TimeSpanParse.TryParse(s, (IFormatProvider) null, out result);
    }

    /// <summary>
    ///   Преобразовывает заданное строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанную информацию о форматировании, связанную с языком и региональными параметрами, и возвращает значение, которое показывает успешность преобразования.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="input" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    ///    Эта операция возвращает значение <see langword="false" />, если параметр <paramref name="input" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />, либо его формат недопустим, либо он представляет интервал времени, меньший <see cref="F:System.TimeSpan.MinValue" /> или больший <see cref="F:System.TimeSpan.MaxValue" />, либо минимум один из его компонентов — количество дней, часов, минут или секунд — находится вне допустимого диапазона.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParse(input, formatProvider, out result);
    }

    /// <summary>
    ///   Преобразовывает заданное строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанный формат и информацию о форматировании, связанную с языком и региональными параметрами, и возвращает значение, которое показывает успешность преобразования.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата, которая определяет требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="input" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
    }

    /// <summary>
    ///   Преобразовывает заданное строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные форматы и информацию о форматировании, связанную с языком и региональными параметрами, и возвращает значение, которое показывает успешность преобразования.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formats">
    ///   Массив стандартных или пользовательских строк формата, которые определяют допустимые форматы параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="input" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
    }

    /// <summary>
    ///   Преобразовывает заданное строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанный формат, информацию о форматировании, связанную с языком и региональными параметрами, и стили, и возвращает значение, которое показывает успешность преобразования.
    ///    Формат строкового представления должен полностью соответствовать заданному формату.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата, которая определяет требуемый формат параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <param name="styles">
    ///   Одно или несколько перечислимых значений, указывающих стиль параметра <paramref name="input" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="input" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.ValidateStyles(styles, nameof (styles));
      return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
    }

    /// <summary>
    ///   Преобразовывает заданное строковое представление интервала времени в его эквивалент <see cref="T:System.TimeSpan" />, используя указанные форматы, информацию о форматировании, связанную с языком и региональными параметрами, и стили, и возвращает значение, которое показывает успешность преобразования.
    ///    Формат строкового представления должен полностью соответствовать одному из заданных форматов.
    /// </summary>
    /// <param name="input">
    ///   Строка, которая указывает преобразуемый интервал времени.
    /// </param>
    /// <param name="formats">
    ///   Массив стандартных или пользовательских строк формата, которые определяют допустимые форматы параметра <paramref name="input" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="styles">
    ///   Одно или несколько перечислимых значений, указывающих стиль параметра <paramref name="input" />.
    /// </param>
    /// <param name="result">
    ///   После возврата из этого метода содержит объект, представляющий интервал времени, заданный в параметре <paramref name="input" />, или значение <see cref="F:System.TimeSpan.Zero" />, если преобразование завершилось неудачей.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.ValidateStyles(styles, nameof (styles));
      return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.TimeSpan" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.TimeSpan" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return TimeSpanFormat.Format(this, (string) null, (IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.TimeSpan" /> в эквивалентное ему строковое представление с использованием заданного формата.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата <see cref="T:System.TimeSpan" />.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.TimeSpan" /> в формате, заданном параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="format" /> не распознается или не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return TimeSpanFormat.Format(this, format, (IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует числовое значение текущего объекта <see cref="T:System.TimeSpan" /> в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка формата <see cref="T:System.TimeSpan" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление текущего значения <see cref="T:System.TimeSpan" /> в виде, заданном параметрами <paramref name="format" /> и <paramref name="formatProvider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="format" /> не распознается или не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider formatProvider)
    {
      if (TimeSpan.LegacyMode)
        return TimeSpanFormat.Format(this, (string) null, (IFormatProvider) null);
      return TimeSpanFormat.Format(this, format, formatProvider);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeSpan" /> со значением, равным значению данного экземпляра с противоположным знаком.
    /// </summary>
    /// <param name="t">
    ///   Интервал времени, у которого будет изменен знак.
    /// </param>
    /// <returns>
    ///   Объект, числовое значение которого совпадает со значением данного экземпляра, но с противоположным знаком.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Инвертированное значение этого экземпляра не может быть представлено <see cref="T:System.TimeSpan" />, т. е. этот экземпляр имеет значение <see cref="F:System.TimeSpan.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(TimeSpan t)
    {
      if (t._ticks == TimeSpan.MinValue._ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return new TimeSpan(-t._ticks);
    }

    /// <summary>
    ///   Вычитает указанный объект <see cref="T:System.TimeSpan" /> из другого указанного объекта <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">Уменьшаемое.</param>
    /// <param name="t2">Вычитаемое.</param>
    /// <returns>
    ///   Объект, значение которого является разностью значений <paramref name="t1" /> и <paramref name="t2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
    {
      return t1.Subtract(t2);
    }

    /// <summary>
    ///   Возвращает указанный экземпляр <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t">Возвращаемый интервал времени.</param>
    /// <returns>
    ///   Интервал времени, который задается параметром <paramref name="t" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TimeSpan operator +(TimeSpan t)
    {
      return t;
    }

    /// <summary>
    ///   Складывает два указанных экземпляра <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">Первый из добавляемых интервалов времени.</param>
    /// <param name="t2">Второй из добавляемых интервалов времени.</param>
    /// <returns>
    ///   Объект, значение которого является суммой значений <paramref name="t1" /> и <paramref name="t2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Итоговое значение <see cref="T:System.TimeSpan" /> меньше <see cref="F:System.TimeSpan.MinValue" /> или больше <see cref="F:System.TimeSpan.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
    {
      return t1.Add(t2);
    }

    /// <summary>
    ///   Указывает, равны ли два экземпляра <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения параметров <paramref name="t1" /> и <paramref name="t2" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks == t2._ticks;
    }

    /// <summary>
    ///   Показывает, являются ли два экземпляра <see cref="T:System.TimeSpan" /> неравными.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения <paramref name="t1" /> и <paramref name="t2" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks != t2._ticks;
    }

    /// <summary>
    ///   Показывает, является ли заданное значение <see cref="T:System.TimeSpan" /> меньшим, чем другое заданное значение <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> меньше <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks < t2._ticks;
    }

    /// <summary>
    ///   Показывает, является ли заданное значение <see cref="T:System.TimeSpan" /> меньшим или равным другому заданному значению <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> меньше или равно <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator <=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks <= t2._ticks;
    }

    /// <summary>
    ///   Показывает, является ли заданное значение <see cref="T:System.TimeSpan" /> большим, чем другое заданное значение <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> больше <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks > t2._ticks;
    }

    /// <summary>
    ///   Показывает, является ли заданное значение <see cref="T:System.TimeSpan" /> большим или равным другому заданному значению <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="t1">
    ///   Первый из сравниваемых интервалов времени.
    /// </param>
    /// <param name="t2">
    ///   Второй из сравниваемых интервалов времени.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="t1" /> больше или равно <paramref name="t2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks >= t2._ticks;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool LegacyFormatMode();

    [SecuritySafeCritical]
    private static bool GetLegacyFormatMode()
    {
      if (TimeSpan.LegacyFormatMode())
        return true;
      return CompatibilitySwitches.IsNetFx40TimeSpanLegacyFormatMode;
    }

    private static bool LegacyMode
    {
      get
      {
        if (!TimeSpan._legacyConfigChecked)
        {
          TimeSpan._legacyMode = TimeSpan.GetLegacyFormatMode();
          TimeSpan._legacyConfigChecked = true;
        }
        return TimeSpan._legacyMode;
      }
    }
  }
}
