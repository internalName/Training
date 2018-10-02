// Decompiled with JetBrains decompiler
// Type: System.Int32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет 32-разрядное целое число со знаком.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
  {
    internal int m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Int32" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const int MaxValue = 2147483647;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Int32" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const int MinValue = -2147483648;

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом и возвращает значение, указывающее, как соотносятся значения этих объектов.
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
    ///   Параметр <paramref name="value" /> не является классом <see cref="T:System.Int32" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is int))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"));
      int num = (int) value;
      if (this < num)
        return -1;
      return this > num ? 1 : 0;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным 32-битовым целым числом со знаком и возвращает значение, указывающее, как соотносятся их значения.
    /// </summary>
    /// <param name="value">Целое число для сравнения.</param>
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
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(int value)
    {
      if (this < value)
        return -1;
      return this > value ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Int32" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is int))
        return false;
      return this == (int) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли этот экземпляр заданному значению типа <see cref="T:System.Int32" />.
    /// </summary>
    /// <param name="obj">
    ///   Значение типа <see cref="T:System.Int32" /> для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(int obj)
    {
      return this == obj;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this;
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, состоящее из знака минус, если число отрицательное, и последовательности цифр в диапазоне от 0 до 9 с ненулевой первой цифрой.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatInt32(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное строковое представление с использованием указанного формата.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка числового формата.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> является недопустимым или не поддерживается.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatInt32(this, format, NumberFormatInfo.CurrentInfo);
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
      return Number.FormatInt32(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка числового формата.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> является недопустимым или не поддерживается.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatInt32(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int32.MinValue" /> или больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Parse(string s)
    {
      return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате в эквивалентное ему 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> не является сочетанием значений <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представлен в формате, совместимом с <paramref name="style" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int32.MinValue" /> или больше значения <see cref="F:System.Int32.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате, соответствующем языку и региональным параметрам, в эквивалентное ему 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> находится в неправильном формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int32.MinValue" /> или больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Parse(string s, IFormatProvider provider)
    {
      return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем языку и региональным параметрам, в эквивалентное ему 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> не является сочетанием значений <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представлен в формате, совместимом с <paramref name="style" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int32.MinValue" /> или больше значения <see cref="F:System.Int32.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 32-битовое целое число со знаком.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 32-разрядное целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось сбоем.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в правильном формате или представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out int result)
    {
      return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем языку и региональным параметрам, в эквивалентное ему 32-битовое целое число со знаком.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая преобразуемое число.
    ///    Строка интерпретируется с использованием стиля, указанного в <paramref name="style" />.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 32-разрядное целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось сбоем.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в формате, совместимом с <paramref name="style" /> или представляет собой число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
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
    ///   <paramref name="style" /> не является сочетанием значений <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Int32" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Int32" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Int32;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this);
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
      return this;
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
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Int32), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
