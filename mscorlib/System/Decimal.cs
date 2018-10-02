// Decompiled with JetBrains decompiler
// Type: System.Decimal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>Представляет десятичное число.</summary>
  [ComVisible(true)]
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Decimal : IFormattable, IComparable, IConvertible, IDeserializationCallback, IComparable<Decimal>, IEquatable<Decimal>
  {
    private static uint[] Powers10 = new uint[10]
    {
      1U,
      10U,
      100U,
      1000U,
      10000U,
      100000U,
      1000000U,
      10000000U,
      100000000U,
      1000000000U
    };
    /// <summary>Представляет число нуль (0).</summary>
    [__DynamicallyInvokable]
    public const Decimal Zero = new Decimal(0);
    /// <summary>Представляет число один (1).</summary>
    [__DynamicallyInvokable]
    public const Decimal One = new Decimal(1);
    /// <summary>Представляет число минус один (-1).</summary>
    [__DynamicallyInvokable]
    public const Decimal MinusOne = new Decimal(-1);
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Decimal" />.
    ///    В этом поле содержится константа, и оно доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public const Decimal MaxValue = new Decimal(-1, -1, -1, false, (byte) 0);
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Decimal" />.
    ///    В этом поле содержится константа, и оно доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public const Decimal MinValue = new Decimal(-1, -1, -1, true, (byte) 0);
    private const Decimal NearNegativeZero = new Decimal(1, 0, 0, true, (byte) 27);
    private const Decimal NearPositiveZero = new Decimal(1, 0, 0, false, (byte) 27);
    private const int SignMask = -2147483648;
    private const byte DECIMAL_NEG = 128;
    private const byte DECIMAL_ADD = 0;
    private const int ScaleMask = 16711680;
    private const int ScaleShift = 16;
    private const int MaxInt32Scale = 9;
    private int flags;
    private int hi;
    private int lo;
    private int mid;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение указанного 32-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    [__DynamicallyInvokable]
    public Decimal(int value)
    {
      int num = value;
      if (num >= 0)
      {
        this.flags = 0;
      }
      else
      {
        this.flags = int.MinValue;
        num = -num;
      }
      this.lo = num;
      this.mid = 0;
      this.hi = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение указанного 32-разрядного целого числа без знака.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Decimal(uint value)
    {
      this.flags = 0;
      this.lo = (int) value;
      this.mid = 0;
      this.hi = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение указанного 64-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    [__DynamicallyInvokable]
    public Decimal(long value)
    {
      long num = value;
      if (num >= 0L)
      {
        this.flags = 0;
      }
      else
      {
        this.flags = int.MinValue;
        num = -num;
      }
      this.lo = (int) num;
      this.mid = (int) (num >> 32);
      this.hi = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение указанного 64-разрядного целого числа без знака.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Decimal(ulong value)
    {
      this.flags = 0;
      this.lo = (int) value;
      this.mid = (int) (value >> 32);
      this.hi = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение заданного числа одиночной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше <see cref="F:System.Decimal.MaxValue" /> или меньше <see cref="F:System.Decimal.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равняется <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" /> или <see cref="F:System.Single.NegativeInfinity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Decimal(float value);

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" />, используя значение заданного числа двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое необходимо представить в формате <see cref="T:System.Decimal" />.
    /// </param>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше <see cref="F:System.Decimal.MaxValue" /> или меньше <see cref="F:System.Decimal.MinValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равняется <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" /> или <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Decimal(double value);

    internal Decimal(Currency value)
    {
      Decimal num = Currency.ToDecimal(value);
      this.lo = num.lo;
      this.mid = num.mid;
      this.hi = num.hi;
      this.flags = num.flags;
    }

    /// <summary>
    ///   Преобразует заданное значение <see cref="T:System.Decimal" /> в эквивалентное значение денежного типа OLE-автоматизации, которое содержится в 64-разрядном целом числе со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   64-разрядное целое число со знаком, в котором содержится значение денежного типа OLE-автоматизации, эквивалентное значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToOACurrency(Decimal value)
    {
      return new Currency(value).ToOACurrency();
    }

    /// <summary>
    ///   Преобразует заданное 64-разрядное целое число со знаком, соответствующее значению денежного типа OLE-автоматизации, в эквивалентное значение типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="cy">
    ///   Значение денежного типа OLE-автоматизации.
    /// </param>
    /// <returns>
    ///   Объект типа <see cref="T:System.Decimal" />, содержащий эквивалент значения <paramref name="cy" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal FromOACurrency(long cy)
    {
      return Currency.ToDecimal(Currency.FromOACurrency(cy));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" /> с представленным в двоичном виде десятичным значением, содержащимся в указанном массиве.
    /// </summary>
    /// <param name="bits">
    ///   Массив 32-разрядных целых чисел со знаком, содержащий представление десятичного значения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bits" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="bits" /> не равна 4.
    /// 
    ///   -или-
    /// 
    ///   Недопустимое представление десятичного значения в <paramref name="bits" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Decimal(int[] bits)
    {
      this.lo = 0;
      this.mid = 0;
      this.hi = 0;
      this.flags = 0;
      this.SetBits(bits);
    }

    private void SetBits(int[] bits)
    {
      if (bits == null)
        throw new ArgumentNullException(nameof (bits));
      if (bits.Length == 4)
      {
        int bit = bits[3];
        if ((bit & 2130771967) == 0 && (bit & 16711680) <= 1835008)
        {
          this.lo = bits[0];
          this.mid = bits[1];
          this.hi = bits[2];
          this.flags = bit;
          return;
        }
      }
      throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Decimal" /> на основе параметров, задающих составные части экземпляра.
    /// </summary>
    /// <param name="lo">
    ///   Младшие 32 разряда 96-разрядного целого числа.
    /// </param>
    /// <param name="mid">
    ///   Средние 32 разряда 96-разрядного целого числа.
    /// </param>
    /// <param name="hi">
    ///   Старшие 32 разряда 96-разрядного целого числа.
    /// </param>
    /// <param name="isNegative">
    ///   Значение <see langword="true" /> для обозначения отрицательного числа; значение <see langword="false" /> для обозначения положительного числа.
    /// </param>
    /// <param name="scale">
    ///   Степень числа 10 в диапазоне от 0 до 28.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="scale" /> больше 28.
    /// </exception>
    [__DynamicallyInvokable]
    public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
    {
      if (scale > (byte) 28)
        throw new ArgumentOutOfRangeException(nameof (scale), Environment.GetResourceString("ArgumentOutOfRange_DecimalScale"));
      this.lo = lo;
      this.mid = mid;
      this.hi = hi;
      this.flags = (int) scale << 16;
      if (!isNegative)
        return;
      this.flags |= int.MinValue;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      try
      {
        this.SetBits(Decimal.GetBits(this));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), (Exception) ex);
      }
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        this.SetBits(Decimal.GetBits(this));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), (Exception) ex);
      }
    }

    private Decimal(int lo, int mid, int hi, int flags)
    {
      if ((flags & 2130771967) != 0 || (flags & 16711680) > 1835008)
        throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
      this.lo = lo;
      this.mid = mid;
      this.hi = hi;
      this.flags = flags;
    }

    internal static Decimal Abs(Decimal d)
    {
      return new Decimal(d.lo, d.mid, d.hi, d.flags & int.MaxValue);
    }

    /// <summary>
    ///   Суммирует два заданных значения типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое из складываемых значений.</param>
    /// <param name="d2">Второе из складываемых значений.</param>
    /// <returns>
    ///   Сумма <paramref name="d1" /> и <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Сумма <paramref name="d1" /> и <paramref name="d2" /> меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Add(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 0);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallAddSub(ref Decimal d1, ref Decimal d2, byte bSign);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallAddSubOverflowed(ref Decimal d1, ref Decimal d2, byte bSign, ref bool overflowed);

    /// <summary>
    ///   Возвращает наименьшее целое число, которое больше или равно заданному десятичному числу.
    /// </summary>
    /// <param name="d">Десятичное число.</param>
    /// <returns>
    ///   Наименьшее целое число, которое больше или равно значению параметра <paramref name="d" />.
    ///    Обратите внимание, что данный метод возвращает не целочисленное значение, а значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Ceiling(Decimal d)
    {
      return -Decimal.Floor(-d);
    }

    /// <summary>
    ///   Сравнивает два заданных значения типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    /// Число со знаком, обозначающее относительные значения параметров <paramref name="d1" /> и <paramref name="d2" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Значение <paramref name="d1" /> меньше <paramref name="d2" />.
    /// 
    ///         Нуль
    /// 
    ///         Значения параметров <paramref name="d1" /> и <paramref name="d2" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="d1" /> больше значения <paramref name="d2" />.
    ///       </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Compare(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int FCallCompare(ref Decimal d1, ref Decimal d2);

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом и возвращает сравнение значений этих объектов.
    /// </summary>
    /// <param name="value">
    ///   Объект, который следует сравнить с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Decimal" />.
    /// </exception>
    [SecuritySafeCritical]
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is Decimal))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"));
      Decimal d2 = (Decimal) value;
      return Decimal.FCallCompare(ref this, ref d2);
    }

    /// <summary>
    ///   Сравнивает этот экземпляр с указанным объектом <see cref="T:System.Decimal" /> и возвращает сравнение значений этих объектов.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    ///       </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int CompareTo(Decimal value)
    {
      return Decimal.FCallCompare(ref this, ref value);
    }

    /// <summary>
    ///   Выполняет деление двух заданных значений типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Делимое.</param>
    /// <param name="d2">Делитель.</param>
    /// <returns>
    ///   Результат деления <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   <paramref name="d2" /> равен нулю.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение (то есть частное) меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Divide(Decimal d1, Decimal d2)
    {
      Decimal.FCallDivide(ref d1, ref d2);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallDivide(ref Decimal d1, ref Decimal d2);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallDivideOverflowed(ref Decimal d1, ref Decimal d2, ref bool overflowed);

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Object" /> одно и то же значение и тип.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> принадлежит к типу <see cref="T:System.Decimal" /> и эквивалентен данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (!(value is Decimal))
        return false;
      Decimal d2 = (Decimal) value;
      return Decimal.FCallCompare(ref this, ref d2) == 0;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Decimal" /> одно и то же значение.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="value" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(Decimal value)
    {
      return Decimal.FCallCompare(ref this, ref value) == 0;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public override extern int GetHashCode();

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли два заданных экземпляра <see cref="T:System.Decimal" /> одно и то же значение.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="d1" /> и <paramref name="d2" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Equals(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) == 0;
    }

    /// <summary>
    ///   Округляет заданное число типа <see cref="T:System.Decimal" /> до ближайшего целого в направлении минус бесконечности.
    /// </summary>
    /// <param name="d">Значение для округления.</param>
    /// <returns>
    ///   Если параметр <paramref name="d" /> имеет дробную часть — следующее целое число <see cref="T:System.Decimal" /> в направлении минус бесконечности меньше <paramref name="d" />.
    /// 
    ///   -или-
    /// 
    ///   Если у параметра <paramref name="d" /> нет дробной части, значение параметра <paramref name="d" /> возвращается без изменений.
    ///    Обратите внимание, что этот метод возвращает целочисленное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Floor(Decimal d)
    {
      Decimal.FCallFloor(ref d);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallFloor(ref Decimal d);

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения данного экземпляра.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatDecimal(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное строковое представление с использованием указанного формата.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка числового формата (см. примечания).
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="provider" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatDecimal(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Строка числового формата (см. примечания).
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <returns>
    ///   Эквивалентно числу, содержащемуся в свойстве <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Decimal.MinValue" /> или больше значения <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s)
    {
      return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным стилем в его эквивалент в формате <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений <see cref="T:System.Globalization.NumberStyles" />, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Number" />.
    /// </param>
    /// <returns>
    ///   Число типа <see cref="T:System.Decimal" />, равное числу, содержащемуся в параметре <paramref name="s" />, который задается параметром <paramref name="style" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> является значением <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент <see cref="T:System.Decimal" />, используя заданные сведения о формате для языка и региональных параметров.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <param name="provider">
    ///   Интерфейс <see cref="T:System.IFormatProvider" />, который предоставляет сведения об анализе параметра <paramref name="s" /> для соответствующего языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число типа <see cref="T:System.Decimal" />, равное числу, содержащемуся в параметре <paramref name="s" />, который задается параметром <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, IFormatProvider provider)
    {
      return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент <see cref="T:System.Decimal" />, используя заданный стиль и формат для языка и региональных параметров.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений <see cref="T:System.Globalization.NumberStyles" />, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Number" />.
    /// </param>
    /// <param name="provider">
    ///   Объект <see cref="T:System.IFormatProvider" />, который предоставляет сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число типа <see cref="T:System.Decimal" />, равное числу, содержащемуся в параметре <paramref name="s" />, который задается параметрами <paramref name="style" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Decimal.MinValue" /> или больше значения <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> является значением <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент типа <see cref="T:System.Decimal" />.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <param name="result">
    ///   По возвращении из этого метода содержит число типа <see cref="T:System.Decimal" />, эквивалентное числовому значению, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если значение параметра <paramref name="s" /> равно <see langword="null" /> или <see cref="F:System.String.Empty" />, не является числом допустимого формата или представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out Decimal result)
    {
      return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент <see cref="T:System.Decimal" />, используя заданный стиль и формат для языка и региональных параметров.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Преобразовываемое строковое представление числа.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Number" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения об анализе параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   По возвращении из этого метода содержит число типа <see cref="T:System.Decimal" />, эквивалентное числовому значению, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в формате, совместимом с <paramref name="style" /> или представляет собой число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> является значением <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Decimal result)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    /// <summary>
    ///   Преобразует значение заданного экземпляра <see cref="T:System.Decimal" /> в эквивалентное ему двоичное представление.
    /// </summary>
    /// <param name="d">Преобразуемое значение.</param>
    /// <returns>
    ///   Массив 32-разрядных целых чисел со знаком, состоящий из четырех элементов, в которых содержится двоичное представление параметра <paramref name="d" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static int[] GetBits(Decimal d)
    {
      return new int[4]{ d.lo, d.mid, d.hi, d.flags };
    }

    internal static void GetBytes(Decimal d, byte[] buffer)
    {
      buffer[0] = (byte) d.lo;
      buffer[1] = (byte) (d.lo >> 8);
      buffer[2] = (byte) (d.lo >> 16);
      buffer[3] = (byte) (d.lo >> 24);
      buffer[4] = (byte) d.mid;
      buffer[5] = (byte) (d.mid >> 8);
      buffer[6] = (byte) (d.mid >> 16);
      buffer[7] = (byte) (d.mid >> 24);
      buffer[8] = (byte) d.hi;
      buffer[9] = (byte) (d.hi >> 8);
      buffer[10] = (byte) (d.hi >> 16);
      buffer[11] = (byte) (d.hi >> 24);
      buffer[12] = (byte) d.flags;
      buffer[13] = (byte) (d.flags >> 8);
      buffer[14] = (byte) (d.flags >> 16);
      buffer[15] = (byte) (d.flags >> 24);
    }

    internal static Decimal ToDecimal(byte[] buffer)
    {
      return new Decimal((int) buffer[0] | (int) buffer[1] << 8 | (int) buffer[2] << 16 | (int) buffer[3] << 24, (int) buffer[4] | (int) buffer[5] << 8 | (int) buffer[6] << 16 | (int) buffer[7] << 24, (int) buffer[8] | (int) buffer[9] << 8 | (int) buffer[10] << 16 | (int) buffer[11] << 24, (int) buffer[12] | (int) buffer[13] << 8 | (int) buffer[14] << 16 | (int) buffer[15] << 24);
    }

    private static void InternalAddUInt32RawUnchecked(ref Decimal value, uint i)
    {
      uint lo = (uint) value.lo;
      uint num1 = lo + i;
      value.lo = (int) num1;
      if (num1 >= lo && num1 >= i)
        return;
      uint mid = (uint) value.mid;
      uint num2 = mid + 1U;
      value.mid = (int) num2;
      if (num2 >= mid && num2 >= 1U)
        return;
      ++value.hi;
    }

    private static uint InternalDivRemUInt32(ref Decimal value, uint divisor)
    {
      uint num1 = 0;
      if (value.hi != 0)
      {
        ulong hi = (ulong) (uint) value.hi;
        value.hi = (int) (uint) (hi / (ulong) divisor);
        num1 = (uint) (hi % (ulong) divisor);
      }
      if (value.mid != 0 || num1 != 0U)
      {
        ulong num2 = (ulong) num1 << 32 | (ulong) (uint) value.mid;
        value.mid = (int) (uint) (num2 / (ulong) divisor);
        num1 = (uint) (num2 % (ulong) divisor);
      }
      if (value.lo != 0 || num1 != 0U)
      {
        ulong num2 = (ulong) num1 << 32 | (ulong) (uint) value.lo;
        value.lo = (int) (uint) (num2 / (ulong) divisor);
        num1 = (uint) (num2 % (ulong) divisor);
      }
      return num1;
    }

    private static void InternalRoundFromZero(ref Decimal d, int decimalCount)
    {
      int num1 = ((d.flags & 16711680) >> 16) - decimalCount;
      if (num1 <= 0)
        return;
      uint divisor;
      uint num2;
      do
      {
        int index = num1 > 9 ? 9 : num1;
        divisor = Decimal.Powers10[index];
        num2 = Decimal.InternalDivRemUInt32(ref d, divisor);
        num1 -= index;
      }
      while (num1 > 0);
      if (num2 >= divisor >> 1)
        Decimal.InternalAddUInt32RawUnchecked(ref d, 1U);
      d.flags = decimalCount << 16 & 16711680 | d.flags & int.MinValue;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static Decimal Max(Decimal d1, Decimal d2)
    {
      if (Decimal.FCallCompare(ref d1, ref d2) < 0)
        return d2;
      return d1;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static Decimal Min(Decimal d1, Decimal d2)
    {
      if (Decimal.FCallCompare(ref d1, ref d2) >= 0)
        return d2;
      return d1;
    }

    /// <summary>
    ///   Вычисляет остаток от деления двух значений типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Делимое.</param>
    /// <param name="d2">Делитель.</param>
    /// <returns>
    ///   Остаток от деления <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   <paramref name="d2" /> равен нулю.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Remainder(Decimal d1, Decimal d2)
    {
      d2.flags = d2.flags & int.MaxValue | d1.flags & int.MinValue;
      if (Decimal.Abs(d1) < Decimal.Abs(d2))
        return d1;
      d1 -= d2;
      if (d1 == Decimal.Zero)
        d1.flags = d1.flags & int.MaxValue | d2.flags & int.MinValue;
      Decimal num1 = Decimal.Truncate(d1 / d2) * d2;
      Decimal num2 = d1 - num1;
      if ((d1.flags & int.MinValue) != (num2.flags & int.MinValue))
      {
        if (new Decimal(1, 0, 0, true, (byte) 27) <= num2 && num2 <= new Decimal(1, 0, 0, false, (byte) 27))
          num2.flags = num2.flags & int.MaxValue | d1.flags & int.MinValue;
        else
          num2 += d2;
      }
      return num2;
    }

    /// <summary>
    ///   Умножает два заданных значения <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Множимое.</param>
    /// <param name="d2">Множитель.</param>
    /// <returns>
    ///   Результат умножения <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Multiply(Decimal d1, Decimal d2)
    {
      Decimal.FCallMultiply(ref d1, ref d2);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallMultiply(ref Decimal d1, ref Decimal d2);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallMultiplyOverflowed(ref Decimal d1, ref Decimal d2, ref bool overflowed);

    /// <summary>
    ///   Возвращает результат умножения заданного значения <see cref="T:System.Decimal" /> на минус единицу.
    /// </summary>
    /// <param name="d">Инвертируемое значение.</param>
    /// <returns>
    ///   Десятичное число со значением <paramref name="d" />, но с противоположным знаком.
    /// 
    ///   -или-
    /// 
    ///   Нуль, если значение параметра <paramref name="d" /> равно нулю.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Negate(Decimal d)
    {
      return new Decimal(d.lo, d.mid, d.hi, d.flags ^ int.MinValue);
    }

    /// <summary>Округляет десятичное значение до ближайшего целого.</summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="d" />.
    ///    Если <paramref name="d" /> находится на равном расстоянии от двух целых чисел (четного и нечетного), возвращается четное число.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона значения <see cref="T:System.Decimal" />.
    /// </exception>
    public static Decimal Round(Decimal d)
    {
      return Decimal.Round(d, 0);
    }

    /// <summary>
    ///   Округляет значение <see cref="T:System.Decimal" /> до указанного числа десятичных знаков.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="decimals">
    ///   Значение от 0 до 28, задающее число десятичных знаков, до которого необходимо округлить значение.
    /// </param>
    /// <returns>
    ///   Десятичное число, эквивалентное значению параметра <paramref name="d" />, округленное до количества десятичных знаков, заданного в параметре <paramref name="decimals" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="decimals" /> не в диапазоне от 0 до 28.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals)
    {
      Decimal.FCallRound(ref d, decimals);
      return d;
    }

    /// <summary>
    ///   Округляет десятичное значение до ближайшего целого.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя другими числами.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="d" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="d" />.
    ///    Если <paramref name="d" /> находится на равном расстоянии от двух чисел (четного и нечетного), возвращаемое число определяется по значению параметра <paramref name="mode" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" /> не является значением <see cref="T:System.MidpointRounding" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона объекта <see cref="T:System.Decimal" />.
    /// </exception>
    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      return Decimal.Round(d, 0, mode);
    }

    /// <summary>
    ///   Округляет десятичное значение с указанной точностью.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя другими числами.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="decimals">
    ///   Количество значащих десятичных знаков дробной части числа (точность) возвращаемого значения.
    /// </param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="d" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Число, ближайшее к параметру <paramref name="d" />, при точности, равной значению параметра <paramref name="decimals" />.
    ///    Если <paramref name="d" /> находится на равном расстоянии от двух чисел (четного и нечетного), возвращаемое число определяется по значению параметра <paramref name="mode" />.
    ///    Если точность <paramref name="d" /> меньше, чем <paramref name="decimals" />, то <paramref name="d" /> возвращается без изменений.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="decimals" /> имеет значение меньше 0 или больше 28.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" /> не является значением <see cref="T:System.MidpointRounding" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона объекта <see cref="T:System.Decimal" />.
    /// </exception>
    [SecuritySafeCritical]
    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      if (decimals < 0 || decimals > 28)
        throw new ArgumentOutOfRangeException(nameof (decimals), Environment.GetResourceString("ArgumentOutOfRange_DecimalRound"));
      if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) mode, (object) "MidpointRounding"), nameof (mode));
      if (mode == MidpointRounding.ToEven)
        Decimal.FCallRound(ref d, decimals);
      else
        Decimal.InternalRoundFromZero(ref d, decimals);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallRound(ref Decimal d, int decimals);

    /// <summary>
    ///   Вычитает одно указанное значение типа <see cref="T:System.Decimal" /> из другого.
    /// </summary>
    /// <param name="d1">Уменьшаемое.</param>
    /// <param name="d2">Вычитаемое.</param>
    /// <returns>
    ///   Результат вычитания <paramref name="d2" /> из <paramref name="d1" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Subtract(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 128);
      return d1;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   8-разрядное целое число без знака, эквивалентное <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(Decimal value)
    {
      uint uint32;
      try
      {
        uint32 = Decimal.ToUInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), (Exception) ex);
      }
      if (uint32 < 0U || uint32 > (uint) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) uint32;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 8-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   8-разрядное целое число со знаком, эквивалентное значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(Decimal value)
    {
      int int32;
      try
      {
        int32 = Decimal.ToInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), (Exception) ex);
      }
      if (int32 < (int) sbyte.MinValue || int32 > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) int32;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(Decimal value)
    {
      int int32;
      try
      {
        int32 = Decimal.ToInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"), (Exception) ex);
      }
      if (int32 < (int) short.MinValue || int32 > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) int32;
    }

    [SecuritySafeCritical]
    internal static Currency ToCurrency(Decimal d)
    {
      Currency result = new Currency();
      Decimal.FCallToCurrency(ref result, d);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallToCurrency(ref Currency result, Decimal d);

    /// <summary>
    ///   Преобразует значение заданного типа <see cref="T:System.Decimal" /> в эквивалентное число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное значению <paramref name="d" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double ToDouble(Decimal d);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int FCallToInt32(Decimal d);

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное значению <paramref name="d" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="d" /> меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ToInt32(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0 && d.mid == 0)
      {
        int lo = d.lo;
        if (d.flags >= 0)
        {
          if (lo >= 0)
            return lo;
        }
        else
        {
          int num = -lo;
          if (num <= 0)
            return num;
        }
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   64-разрядное целое число со знаком, эквивалентное значению <paramref name="d" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="d" /> меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static long ToInt64(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0)
      {
        long num1 = (long) d.lo & (long) uint.MaxValue | (long) d.mid << 32;
        if (d.flags >= 0)
        {
          if (num1 >= 0L)
            return num1;
        }
        else
        {
          long num2 = -num1;
          if (num2 <= 0L)
            return num2;
        }
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   16-разрядное целое число без знака, эквивалентное значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.UInt16.MaxValue" /> или меньше <see cref="F:System.UInt16.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(Decimal value)
    {
      uint uint32;
      try
      {
        uint32 = Decimal.ToUInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), (Exception) ex);
      }
      if (uint32 < 0U || uint32 > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) uint32;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   32-разрядное целое число без знака, эквивалентное значению <paramref name="d" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="d" /> является отрицательным или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0 && d.mid == 0)
      {
        uint lo = (uint) d.lo;
        if (d.flags >= 0 || lo == 0U)
          return lo;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   64-разрядное целое число без знака, эквивалентное значению <paramref name="d" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="d" /> является отрицательным или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0)
      {
        ulong num = (ulong) (uint) d.lo | (ulong) (uint) d.mid << 32;
        if (d.flags >= 0 || num == 0UL)
          return num;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
    }

    /// <summary>
    ///   Преобразует значение заданного объекта типа <see cref="T:System.Decimal" /> в эквивалентное число одиночной точности с плавающей запятой.
    /// </summary>
    /// <param name="d">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Число одиночной точности с плавающей запятой, эквивалентное значению <paramref name="d" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float ToSingle(Decimal d);

    /// <summary>
    ///   Возвращает цифры целой части заданного значения типа <see cref="T:System.Decimal" />; все цифры дробной части удаляются.
    /// </summary>
    /// <param name="d">Десятичное число для усечения.</param>
    /// <returns>
    ///   Результат округления <paramref name="d" /> в сторону нуля до ближайшего целого числа.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Truncate(Decimal d)
    {
      Decimal.FCallTruncate(ref d);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallTruncate(ref Decimal d);

    /// <summary>
    ///   Определяет неявное преобразование 8-разрядного целого числа в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>Преобразованное 8-битное целое число без знака.</returns>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(byte value)
    {
      return new Decimal((int) value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 8-битового целого числа со знаком в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>Преобразованное 8-битное целое число со знаком.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(sbyte value)
    {
      return new Decimal((int) value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 16-разрядного целого числа со знаком в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>Преобразованное 16-битное целое число со знаком.</returns>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(short value)
    {
      return new Decimal((int) value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 16-разрядного целого числа без знака в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>Преобразованное 16-битное целое число без знака.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(ushort value)
    {
      return new Decimal((int) value);
    }

    /// <summary>
    ///   Определяет явное преобразование символа Юникода в <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>Преобразованный символ Юникода.</returns>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(char value)
    {
      return new Decimal((int) value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 32-разрядного целого числа со знаком в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>Преобразованное 32-битное целое число со знаком.</returns>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(int value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 32-разрядного целого числа без знака в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>Преобразованное 32-битное целое число без знака.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(uint value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 64-разрядного целого числа со знаком в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>Преобразованное 64-битное целое число со знаком.</returns>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(long value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет неявное преобразование 64-разрядного целого числа без знака в значение <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>Преобразованное 64-битное целое число без знака.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(ulong value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет явное преобразование числа одиночной точности с плавающей запятой в <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Преобразованное число одиночной точности с плавающей запятой.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равняется <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" /> или <see cref="F:System.Single.NegativeInfinity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator Decimal(float value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет явное преобразование числа двойной точности с плавающей запятой в <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Преобразованное число двойной точности с плавающей запятой.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равняется <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" /> или <see cref="F:System.Double.NegativeInfinity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator Decimal(double value)
    {
      return new Decimal(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 8-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое представляет преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator byte(Decimal value)
    {
      return Decimal.ToByte(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 8-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   8-разрядное целое число со знаком, которое представляет преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator sbyte(Decimal value)
    {
      return Decimal.ToSByte(value);
    }

    /// <summary>
    ///   Определяет явное преобразование объекта <see cref="T:System.Decimal" /> в символ Юникода.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   Символ Юникода, представляющий преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" /> или больше <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator char(Decimal value)
    {
      try
      {
        return (char) Decimal.ToUInt16(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"), (Exception) ex);
      }
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   16-разрядное целое число со знаком, которое представляет преобразованное значение <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> является менее <see cref="F:System.Int16.MinValue" /> или больше, чем <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator short(Decimal value)
    {
      return Decimal.ToInt16(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое представляет преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.UInt16.MaxValue" /> или меньше <see cref="F:System.UInt16.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator ushort(Decimal value)
    {
      return Decimal.ToUInt16(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   32-разрядное целое число со знаком, которое представляет преобразованное значение <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator int(Decimal value)
    {
      return Decimal.ToInt32(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое представляет преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> является отрицательным или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator uint(Decimal value)
    {
      return Decimal.ToUInt32(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   64-разрядное целое число со знаком, которое представляет преобразованное значение <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static explicit operator long(Decimal value)
    {
      return Decimal.ToInt64(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое представляет преобразованное значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> является отрицательным или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator ulong(Decimal value)
    {
      return Decimal.ToUInt64(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в число одиночной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   Значение одиночной точности с плавающей запятой, которое представляет преобразованное значение <see cref="T:System.Decimal" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static explicit operator float(Decimal value)
    {
      return Decimal.ToSingle(value);
    }

    /// <summary>
    ///   Определяет явное преобразование <see cref="T:System.Decimal" /> в число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">Преобразуемое значение.</param>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, которое представляет преобразованное число <see cref="T:System.Decimal" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static explicit operator double(Decimal value)
    {
      return Decimal.ToDouble(value);
    }

    /// <summary>
    ///   Возвращает значение операнда <see cref="T:System.Decimal" /> (знак операнда при этом не меняется).
    /// </summary>
    /// <param name="d">Возвращаемый операнд.</param>
    /// <returns>
    ///   Значение операнда <paramref name="d" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal operator +(Decimal d)
    {
      return d;
    }

    /// <summary>
    ///   Делает отрицательным значение заданного операнда типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d">Инвертируемое значение.</param>
    /// <returns>
    ///   Результат умножения параметра <paramref name="d" /> на минус единицу (-1).
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal operator -(Decimal d)
    {
      return Decimal.Negate(d);
    }

    /// <summary>
    ///   Увеличивает операнд <see cref="T:System.Decimal" /> на 1.
    /// </summary>
    /// <param name="d">Увеличиваемое значение.</param>
    /// <returns>
    ///   Значение параметра <paramref name="d" />, увеличенное на 1.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal operator ++(Decimal d)
    {
      return Decimal.Add(d, Decimal.One);
    }

    /// <summary>
    ///   Уменьшает операнд типа <see cref="T:System.Decimal" /> на единицу.
    /// </summary>
    /// <param name="d">Уменьшаемое значение.</param>
    /// <returns>
    ///   Значение параметра <paramref name="d" />, уменьшенное на 1.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal operator --(Decimal d)
    {
      return Decimal.Subtract(d, Decimal.One);
    }

    /// <summary>
    ///   Суммирует два заданных значения типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое из складываемых значений.</param>
    /// <param name="d2">Второе из складываемых значений.</param>
    /// <returns>
    ///   Результат сложения <paramref name="d1" /> и <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator +(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 0);
      return d1;
    }

    /// <summary>
    ///   Находит разность двух заданных значений типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Уменьшаемое.</param>
    /// <param name="d2">Вычитаемое.</param>
    /// <returns>
    ///   Результат вычитания <paramref name="d2" /> из <paramref name="d1" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator -(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 128);
      return d1;
    }

    /// <summary>
    ///   Умножает два заданных значения <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое значение для перемножения.</param>
    /// <param name="d2">Второе значение для перемножения.</param>
    /// <returns>
    ///   Результат умножения <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator *(Decimal d1, Decimal d2)
    {
      Decimal.FCallMultiply(ref d1, ref d2);
      return d1;
    }

    /// <summary>
    ///   Выполняет деление двух заданных значений типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Делимое.</param>
    /// <param name="d2">Делитель.</param>
    /// <returns>
    ///   Результат деления <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   <paramref name="d2" /> равен нулю.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator /(Decimal d1, Decimal d2)
    {
      Decimal.FCallDivide(ref d1, ref d2);
      return d1;
    }

    /// <summary>
    ///   Возвращает остаток от деления двух заданных значений <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Делимое.</param>
    /// <param name="d2">Делитель.</param>
    /// <returns>
    ///   Остаток от деления <paramref name="d1" /> на <paramref name="d2" />.
    /// </returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   Свойство <paramref name="d2" /> имеет значение <see langword="zero" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Возвращаемое значение меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal operator %(Decimal d1, Decimal d2)
    {
      return Decimal.Remainder(d1, d2);
    }

    /// <summary>
    ///   Возвращает значение, определяющее, равны ли два значения <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="d1" /> и <paramref name="d2" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator ==(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) == 0;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, различаются ли значения двух объектов <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="d1" /> и <paramref name="d2" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator !=(Decimal d1, Decimal d2)
    {
      return (uint) Decimal.FCallCompare(ref d1, ref d2) > 0U;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, действительно ли заданное значение типа <see cref="T:System.Decimal" /> меньше другого заданного значения типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="d1" /> меньше значения <paramref name="d2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator <(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) < 0;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, действительно ли заданное значение типа <see cref="T:System.Decimal" /> меньше или равно другому заданному значению типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="d1" /> меньше или равно значению <paramref name="d2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator <=(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) <= 0;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, действительно ли заданное значение типа <see cref="T:System.Decimal" /> больше другого заданного значения типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="d1" /> больше <paramref name="d2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator >(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) > 0;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, действительно ли заданное значение типа <see cref="T:System.Decimal" /> больше или равно другому заданному значению типа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="d1">Первое сравниваемое значение.</param>
    /// <param name="d2">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="d1" /> больше или равно значению <paramref name="d2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator >=(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) >= 0;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Decimal" />.
    /// </summary>
    /// <returns>
    ///   Перечислимая константа типа <see cref="F:System.TypeCode.Decimal" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Decimal;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Decimal), (object) "Char"));
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Decimal), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
