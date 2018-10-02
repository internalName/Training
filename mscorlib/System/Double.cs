// Decompiled with JetBrains decompiler
// Type: System.Double
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет число двойной точности с плавающей запятой.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
  {
    internal static double NegativeZero = BitConverter.Int64BitsToDouble(long.MinValue);
    internal double m_value;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Double" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double MinValue = -1.79769313486232E+308;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Double" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double MaxValue = 1.79769313486232E+308;
    /// <summary>
    ///   Представляет наименьшее положительное значение <see cref="T:System.Double" /> больше нуля.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double Epsilon = 4.94065645841247E-324;
    /// <summary>
    ///   Представляет минус бесконечность.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double NegativeInfinity = -1.0 / 0.0;
    /// <summary>
    ///   Представляет плюс бесконечность.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double PositiveInfinity = 1.0 / 0.0;
    /// <summary>
    ///   Представляет значение, не являющееся числом (<see langword="NaN" />).
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const double NaN = 0.0 / 0.0;

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равно ли данное число плюс или минус бесконечности.
    /// </summary>
    /// <param name="d">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="d" /> равен значению <see cref="F:System.Double.PositiveInfinity" /> или <see cref="F:System.Double.NegativeInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsInfinity(double d)
    {
      return (*(long*) &d & long.MaxValue) == 9218868437227405312L;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равно ли данное число плюс бесконечности.
    /// </summary>
    /// <param name="d">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="d" /> равно значению <see cref="F:System.Double.PositiveInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool IsPositiveInfinity(double d)
    {
      return d == double.PositiveInfinity;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равно ли данное число минус бесконечности.
    /// </summary>
    /// <param name="d">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="d" /> равно значению <see cref="F:System.Double.NegativeInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool IsNegativeInfinity(double d)
    {
      return d == double.NegativeInfinity;
    }

    [SecuritySafeCritical]
    internal static unsafe bool IsNegative(double d)
    {
      return (*(long*) &d & long.MinValue) == long.MinValue;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, что указанное значение не является числом (<see cref="F:System.Double.NaN" />).
    /// </summary>
    /// <param name="d">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="d" /> равно значению <see cref="F:System.Double.NaN" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsNaN(double d)
    {
      return (ulong) (*(long*) &d & long.MaxValue) > 9218868437227405312UL;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным объектом и возвращает целое число, которое показывает, является ли значение данного экземпляра меньше, больше или равно значению заданного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения или значение <see langword="null" />.
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
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр не является числом (<see cref="F:System.Double.NaN" />) и <paramref name="value" /> является числом.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр и <paramref name="value" /> оба <see langword="Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, или<see cref="F:System.Double.NegativeInfinity" />
    /// 
    ///         Положительное целое число
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр является числом и <paramref name="value" /> не является числом (<see cref="F:System.Double.NaN" />).
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Double" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is double))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDouble"));
      double d = (double) value;
      if (this < d)
        return -1;
      if (this > d)
        return 1;
      if (this == d)
        return 0;
      if (!double.IsNaN(this))
        return 1;
      return !double.IsNaN(d) ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным числом двойной точности с плавающей запятой и возвращает целое число, которое показывает, является ли значение данного экземпляра меньше, больше или равно значению заданного числа двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   Число двойной точности с плавающей запятой для сравнения.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр не является числом (<see cref="F:System.Double.NaN" />) и <paramref name="value" /> является числом.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         -или-
    /// 
    ///         Оба этого экземпляра и <paramref name="value" /> не число (<see cref="F:System.Double.NaN" />), <see cref="F:System.Double.PositiveInfinity" />, или <see cref="F:System.Double.NegativeInfinity" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр является числом и <paramref name="value" /> не является числом (<see cref="F:System.Double.NaN" />).
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(double value)
    {
      if (this < value)
        return -1;
      if (this > value)
        return 1;
      if (this == value)
        return 0;
      if (!double.IsNaN(this))
        return 1;
      return !double.IsNaN(value) ? -1 : 0;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Double" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is double))
        return false;
      double d = (double) obj;
      if (d == this)
        return true;
      if (double.IsNaN(d))
        return double.IsNaN(this);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равны ли два заданных значения <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator ==(double left, double right)
    {
      return left == right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, не равны ли два заданных значения <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator !=(double left, double right)
    {
      return left != right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Double" /> меньше другого заданного значения <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> меньше значения <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <(double left, double right)
    {
      return left < right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Double" /> больше другого заданного значения <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="left" /> больше <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >(double left, double right)
    {
      return left > right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Double" /> меньше или равно другому заданному значению <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> меньше или равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <=(double left, double right)
    {
      return left <= right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Double" /> больше или равно другому заданному значению <see cref="T:System.Double" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> больше или равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >=(double left, double right)
    {
      return left >= right;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Double" /> одно и то же значение.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Double" />, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="obj" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(double obj)
    {
      if (obj == this)
        return true;
      if (double.IsNaN(obj))
        return double.IsNaN(this);
      return false;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      double num1 = this;
      if (num1 == 0.0)
        return 0;
      long num2 = *(long*) &num1;
      return (int) num2 ^ (int) (num2 >> 32);
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>Строковое представление значения этого экземпляра.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatDouble(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное строковое представление с использованием указанного формата.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
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
      return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
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
      return Number.FormatDouble(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно числовому значению или символу, заданному параметром <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представляет число в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Parse(string s)
    {
      return double.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа указанного стиля в эквивалентное ему число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут присутствовать в параметре <paramref name="s" />.
    ///    Типичным задаваемым значением является сочетание <see cref="F:System.Globalization.NumberStyles.Float" /> и <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно числовому значению или символу, заданному параметром <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представляет число в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число, которое меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> включает значение <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа, выраженное в заданном формате, связанном с языком и региональными параметрами, в эквивалентное ему число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно числовому значению или символу, заданному параметром <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представляет число в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Parse(string s, IFormatProvider provider)
    {
      return double.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа указанного стиля, выраженное в формате, соответствующем определенному языку и региональным параметрам, в эквивалентное ему число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут присутствовать в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Float" /> в сочетании со значением <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно числовому значению или символу, заданному параметром <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="s" /> не представляет числовое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> является значением <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static double Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      return Number.ParseDouble(s, style, info);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему число двойной точности с плавающей запятой.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="result">
    ///   При возврате этот метод содержит число двойной точности с плавающей запятой, эквивалентное параметру <paramref name="s" />, если преобразование завершилось успешно, или нуль, если оно завершилось неудачно.
    ///    Преобразование завершается неудачно, если значение параметра <paramref name="s" /> равно <see langword="null" /> или <see cref="F:System.String.Empty" />, не является числом допустимого формата или представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out double result)
    {
      return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа указанного стиля, выраженное в формате, соответствующем определенному языку и региональным параметрам, в эквивалентное ему число двойной точности с плавающей запятой.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений <see cref="T:System.Globalization.NumberStyles" />, определяющее разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Float" /> в сочетании со значением <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <param name="provider">
    ///   Интерфейс <see cref="T:System.IFormatProvider" /> предоставляет сведения о форматировании параметра <paramref name="s" /> для соответствующего языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   При возврате этот метод содержит число двойной точности с плавающей запятой, эквивалентное числовому значению или символу, содержащемуся в параметре <paramref name="s" />, если преобразование завершилось успешно, или нуль, если оно завершилось неудачно.
    ///    Преобразование завершается неудачно, если параметр <paramref name="s" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />, не является значением в формате, совместимом с параметром <paramref name="style" />, представляет число меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />, либо если <paramref name="style" /> не является допустимой комбинацией перечисленных констант <see cref="T:System.Globalization.NumberStyles" />.
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
    ///   <paramref name="style" /> включает значение <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out double result)
    {
      if (s == null)
      {
        result = 0.0;
        return false;
      }
      if (!Number.TryParseDouble(s, style, info, out result))
      {
        string str = s.Trim();
        if (str.Equals(info.PositiveInfinitySymbol))
          result = double.PositiveInfinity;
        else if (str.Equals(info.NegativeInfinitySymbol))
        {
          result = double.NegativeInfinity;
        }
        else
        {
          if (!str.Equals(info.NaNSymbol))
            return false;
          result = double.NaN;
        }
      }
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Double" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Double" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Double;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Double), (object) "Char"));
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
      return this;
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Double), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
