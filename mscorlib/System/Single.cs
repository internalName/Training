// Decompiled with JetBrains decompiler
// Type: System.Single
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
  ///   Представляет число с плавающей запятой одиночной точности.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
  {
    internal float m_value;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Single" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float MinValue = -3.402823E+38f;
    /// <summary>
    ///   Представляет наименьшее положительное значение <see cref="T:System.Single" /> больше нуля.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float Epsilon = 1.401298E-45f;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Single" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float MaxValue = 3.402823E+38f;
    /// <summary>
    ///   Представляет плюс бесконечность.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float PositiveInfinity = 1.0f / 0.0f;
    /// <summary>
    ///   Представляет минус бесконечность.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float NegativeInfinity = -1.0f / 0.0f;
    /// <summary>
    ///   Представляет нечисловое значение (<see langword="NaN" />).
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const float NaN = 0.0f / 0.0f;

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равно ли данное число плюс или минус бесконечности.
    /// </summary>
    /// <param name="f">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="f" /> равен значению <see cref="F:System.Single.PositiveInfinity" /> или <see cref="F:System.Single.NegativeInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsInfinity(float f)
    {
      return (*(int*) &f & int.MaxValue) == 2139095040;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равно ли данное число плюс бесконечности.
    /// </summary>
    /// <param name="f">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="f" /> равно значению <see cref="F:System.Single.PositiveInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsPositiveInfinity(float f)
    {
      return *(int*) &f == 2139095040;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равно ли данное число минус бесконечности.
    /// </summary>
    /// <param name="f">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="f" /> равно значению <see cref="F:System.Single.NegativeInfinity" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsNegativeInfinity(float f)
    {
      return *(int*) &f == -8388608;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, что указанное значение не является числом (<see cref="F:System.Single.NaN" />).
    /// </summary>
    /// <param name="f">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="f" /> относится к нечисловому типу (<see cref="F:System.Single.NaN" />); в противном случае — значение <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsNaN(float f)
    {
      return (*(int*) &f & int.MaxValue) > 2139095040;
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
    ///         Этот экземпляр не является числом (<see cref="F:System.Single.NaN" />) и <paramref name="value" /> является числом.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр и значение не является числом (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, или <see cref="F:System.Single.NegativeInfinity" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр является числом и <paramref name="value" /> не является числом (<see cref="F:System.Single.NaN" />).
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Single" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is float))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSingle"));
      float f = (float) value;
      if ((double) this < (double) f)
        return -1;
      if ((double) this > (double) f)
        return 1;
      if ((double) this == (double) f)
        return 0;
      if (!float.IsNaN(this))
        return 1;
      return !float.IsNaN(f) ? -1 : 0;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным числом с плавающей запятой одиночной точности и возвращает целое число, которое показывает, является ли значение данного экземпляра меньше, больше или равным значению заданного числа с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   Сравниваемое число с плавающей запятой одиночной точности.
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
    ///         Этот экземпляр не является числом (<see cref="F:System.Single.NaN" />) и <paramref name="value" /> является числом.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         -или-
    /// 
    ///         Оба этого экземпляра и <paramref name="value" /> не число (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, или <see cref="F:System.Single.NegativeInfinity" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Этот экземпляр является числом и <paramref name="value" /> не является числом (<see cref="F:System.Single.NaN" />).
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(float value)
    {
      if ((double) this < (double) value)
        return -1;
      if ((double) this > (double) value)
        return 1;
      if ((double) this == (double) value)
        return 0;
      if (!float.IsNaN(this))
        return 1;
      return !float.IsNaN(value) ? -1 : 0;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равны ли два заданных значения <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator ==(float left, float right)
    {
      return (double) left == (double) right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, не равны ли два заданных значения <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator !=(float left, float right)
    {
      return (double) left != (double) right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Single" /> меньше другого заданного значения <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> меньше значения <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <(float left, float right)
    {
      return (double) left < (double) right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Single" /> больше другого заданного значения <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="left" /> больше <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >(float left, float right)
    {
      return (double) left > (double) right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Single" /> меньше или равно другому заданному значению <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> меньше или равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <=(float left, float right)
    {
      return (double) left <= (double) right;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, действительно ли заданное значение <see cref="T:System.Single" /> больше или равно другому заданному значению <see cref="T:System.Single" />.
    /// </summary>
    /// <param name="left">Первое сравниваемое значение.</param>
    /// <param name="right">Второе сравниваемое значение.</param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="left" /> больше или равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >=(float left, float right)
    {
      return (double) left >= (double) right;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Single" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is float))
        return false;
      float f = (float) obj;
      if ((double) f == (double) this)
        return true;
      if (float.IsNaN(f))
        return float.IsNaN(this);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Single" /> одно и то же значение.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="obj" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(float obj)
    {
      if ((double) obj == (double) this)
        return true;
      if (float.IsNaN(obj))
        return float.IsNaN(this);
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
      float num = this;
      if ((double) num == 0.0)
        return 0;
      return *(int*) &num;
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>Строковое представление значения этого экземпляра.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatSingle(this, (string) null, NumberFormatInfo.CurrentInfo);
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
      return Number.FormatSingle(this, (string) null, NumberFormatInfo.GetInstance(provider));
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
      return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
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
      return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числовому значению или символу, указанному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представляет число в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float Parse(string s)
    {
      return float.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном стиле в эквивалентное ему число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Float" /> в сочетании со значением <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числовому значению или символу, указанному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число, которое меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> включает значение <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return float.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа, записанное в формате, соответствующем определенному языку и региональным параметрам, в эквивалентное ему число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числовому значению или символу, указанному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представляет число в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float Parse(string s, IFormatProvider provider)
    {
      return float.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном стиле и с использованием формата, соответствующего данному языку и региональным параметрам, в эквивалентное ему число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Float" /> в сочетании со значением <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числовому значению или символу, указанному в параметре <paramref name="s" />.
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
    ///   Параметр <paramref name="s" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return float.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static float Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      return Number.ParseSingle(s, style, info);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему число с плавающей запятой одиночной точности.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <param name="result">
    ///   Когда этот метод возвращает значение, оно содержит эквивалент числового значения или символа, содержащегося в параметре <paramref name="s" />, представленный в виде числа с плавающей запятой одиночной точности, если преобразование прошло успешно, или нуль, если произошел сбой преобразования.
    ///    Преобразование завершается сбоем, если значение параметра <paramref name="s" /> равно <see langword="null" /> или <see cref="F:System.String.Empty" />, не является числом допустимого формата или представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out float result)
    {
      return float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном стиле и с использованием формата, соответствующего данному языку и региональным параметрам, в эквивалентное ему число с плавающей запятой одиночной точности.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Float" /> в сочетании со значением <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   Когда этот метод возвращает значение, оно содержит эквивалент числового значения или символа, содержащегося в параметре <paramref name="s" />, представленный в виде числа с плавающей запятой одиночной точности, если преобразование прошло успешно, или нуль, если произошел сбой преобразования.
    ///    Преобразование завершается неудачно, если параметр <paramref name="s" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />, не является значением в формате, совместимом с параметром <paramref name="style" />, представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />, либо если <paramref name="style" /> не является допустимой комбинацией перечисленных констант <see cref="T:System.Globalization.NumberStyles" />.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
    {
      if (s == null)
      {
        result = 0.0f;
        return false;
      }
      if (!Number.TryParseSingle(s, style, info, out result))
      {
        string str = s.Trim();
        if (str.Equals(info.PositiveInfinitySymbol))
          result = float.PositiveInfinity;
        else if (str.Equals(info.NegativeInfinitySymbol))
        {
          result = float.NegativeInfinity;
        }
        else
        {
          if (!str.Equals(info.NaNSymbol))
            return false;
          result = float.NaN;
        }
      }
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Single" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Single" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Single;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Single), (object) "Char"));
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
      return this;
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Single), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
