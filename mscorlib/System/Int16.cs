// Decompiled with JetBrains decompiler
// Type: System.Int16
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>Представляет 16-разрядное целое число со знаком.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Int16 : IComparable, IFormattable, IConvertible, IComparable<short>, IEquatable<short>
  {
    internal short m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Int16" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const short MaxValue = 32767;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Int16" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const short MinValue = -32768;

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным объектом и возвращает целое число, которое показывает, является ли значение данного экземпляра меньше, больше или равно значению объекта.
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
    ///   Параметр <paramref name="value" /> не является классом <see cref="T:System.Int16" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (value is short)
        return (int) this - (int) (short) value;
      throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt16"));
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным 16-битовым знаковым целым числом и возвращает целое число, которое показывает, является ли значение данного экземпляра меньше, больше или равным значению заданного 16-битового знакового целого числа.
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
    public int CompareTo(short value)
    {
      return (int) this - (int) value;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Int16" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is short))
        return false;
      return (int) this == (int) (short) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли этот экземпляр заданному значению типа <see cref="T:System.Int16" />.
    /// </summary>
    /// <param name="obj">
    ///   Значение типа <see cref="T:System.Int16" /> для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(short obj)
    {
      return (int) this == (int) obj;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) (ushort) this | (int) this << 16;
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения этого экземпляра, содержащее знак минус, если значение отрицательное, и последовательность цифр в диапазоне от 0 до 9 без нулей в начале.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="provider">
    ///   Объект <see cref="T:System.IFormatProvider" />, предоставляющий сведения о форматировании, связанные с определенным языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="provider" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное строковое представление с использованием указанного формата.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="format" />.
    /// </returns>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return this.ToString(format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметрами <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return this.ToString(format, NumberFormatInfo.GetInstance(provider));
    }

    [SecuritySafeCritical]
    private string ToString(string format, NumberFormatInfo info)
    {
      if (this < (short) 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
        return Number.FormatUInt32((uint) this & (uint) ushort.MaxValue, format, info);
      return Number.FormatInt32((int) this, format, info);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int16.MinValue" /> или больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short Parse(string s)
    {
      return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате в эквивалентное ему 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
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
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int16.MinValue" /> или больше значения <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные цифры.
    /// </exception>
    [__DynamicallyInvokable]
    public static short Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return short.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате, соответствующем языку и региональным параметрам, в эквивалентное ему 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="provider">
    ///   Интерфейс <see cref="T:System.IFormatProvider" /> предоставляет сведения о форматировании параметра <paramref name="s" /> для соответствующего языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int16.MinValue" /> или больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short Parse(string s, IFormatProvider provider)
    {
      return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем языку и региональным параметрам, в эквивалентное ему 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Интерфейс <see cref="T:System.IFormatProvider" /> предоставляет сведения о форматировании параметра <paramref name="s" /> для соответствующего языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
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
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Int16.MinValue" /> или больше значения <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные цифры.
    /// </exception>
    [__DynamicallyInvokable]
    public static short Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static short Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      int int32;
      try
      {
        int32 = Number.ParseInt32(s, style, info);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"), (Exception) ex);
      }
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (int32 < 0 || int32 > (int) ushort.MaxValue)
          throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
        return (short) int32;
      }
      if (int32 < (int) short.MinValue || int32 > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) int32;
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 16-битовое целое число со знаком.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 16-битное целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в правильном формате или представляет число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out short result)
    {
      return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем языку и региональным параметрам, в эквивалентное ему 16-битовое целое число со знаком.
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
    ///   При возвращении этим методом содержит 16-битное целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в формате, совместимом с <paramref name="style" /> или представляет собой число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out short result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out short result)
    {
      result = (short) 0;
      int result1;
      if (!Number.TryParseInt32(s, style, info, out result1))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (result1 < 0 || result1 > (int) ushort.MaxValue)
          return false;
        result = (short) result1;
        return true;
      }
      if (result1 < (int) short.MinValue || result1 > (int) short.MaxValue)
        return false;
      result = (short) result1;
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Int16" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Int16" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Int16;
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
      return this;
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
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Int16), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
