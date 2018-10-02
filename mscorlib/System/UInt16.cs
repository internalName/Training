// Decompiled with JetBrains decompiler
// Type: System.UInt16
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>Представляет 16-битовое целое число без знака.</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct UInt16 : IComparable, IFormattable, IConvertible, IComparable<ushort>, IEquatable<ushort>
  {
    private ushort m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.UInt16" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const ushort MaxValue = 65535;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.UInt16" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const ushort MinValue = 0;

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
    ///   <paramref name="value" /> не является объектом <see cref="T:System.UInt16" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (value is ushort)
        return (int) this - (int) (ushort) value;
      throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt16"));
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным 16-битовым целым числом без знака и возвращает значение, указывающее, как соотносятся их значения.
    /// </summary>
    /// <param name="value">Целое число без знака для сравнения.</param>
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
    public int CompareTo(ushort value)
    {
      return (int) this - (int) value;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.UInt16" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is ushort))
        return false;
      return (int) this == (int) (ushort) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли этот экземпляр заданному значению типа <see cref="T:System.UInt16" />.
    /// </summary>
    /// <param name="obj">
    ///   16-битовое целое число без знака для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(ushort obj)
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
      return (int) this;
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, состоящее из последовательности цифр от 0 до 9 без знака и нулей в начале.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatUInt32((uint) this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, состоящее из последовательности цифр от 0 до 9 без знака и нулей в начале.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatUInt32((uint) this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с использованием указанного формата.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимый параметр <paramref name="format" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatUInt32((uint) this, format, NumberFormatInfo.CurrentInfo);
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
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatUInt32((uint) this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 16-битовое целое число без знака.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, эквивалентное числу, содержащемуся в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.UInt16.MinValue" /> или больше значения <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Parse(string s)
    {
      return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате в эквивалентное ему 16-битовое целое число без знака.
    /// 
    ///   Этот метод несовместим с CLS.
    ///    CLS-совместимая альтернатива — <see cref="M:System.Int32.Parse(System.String,System.Globalization.NumberStyles)" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    ///    Строка интерпретируется с использованием стиля, указанного параметром <paramref name="style" />.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, которое показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   16-битовое целое число без знака, эквивалентное числу, заданному параметром <paramref name="s" />.
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
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.UInt16.MinValue" /> или больше значения <see cref="F:System.UInt16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 16-разрядное целое число без знака в указанном формате, соответствующем языку и региональным параметрам.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-битовое целое число без знака, эквивалентное числу, заданному параметром <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.UInt16.MinValue" /> или больше значения <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Parse(string s, IFormatProvider provider)
    {
      return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем стилю, языку и региональным параметрам, в эквивалентное ему 16-битовое целое число без знака.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    ///    Строка интерпретируется с использованием стиля, указанного параметром <paramref name="style" />.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут присутствовать в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-битовое целое число без знака, эквивалентное числу, заданному параметром <paramref name="s" />.
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
    ///   Параметр <paramref name="s" /> представляет число, которое меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static ushort Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      uint uint32;
      try
      {
        uint32 = Number.ParseUInt32(s, style, info);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), (Exception) ex);
      }
      if (uint32 > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) uint32;
    }

    /// <summary>
    ///   Предпринимает попытку преобразования строкового представления числа в эквивалентное ему 16-битовое целое число без знака.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 16-разрядное целочисленное значение без знака, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось сбоем.
    ///    Преобразование завершается ошибкой, если параметр <paramref name="s" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" /> (неверный формат).
    ///    или представляет число, которое меньше значения <see cref="F:System.UInt16.MinValue" /> или больше значения <see cref="F:System.UInt16.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out ushort result)
    {
      return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Предпринимает попытку преобразовать строковое представление числа в формате, соответствующем стилю, языку и региональным параметрам, в эквивалентное 16-битовое целое число без знака.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    ///    Строка интерпретируется с использованием стиля, указанного параметром <paramref name="style" />.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 16-разрядное целочисленное значение без знака, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось сбоем.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в формате, совместимом с <paramref name="style" /> или представляет собой число меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
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
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out ushort result)
    {
      result = (ushort) 0;
      uint result1;
      if (!Number.TryParseUInt32(s, style, info, out result1) || result1 > (uint) ushort.MaxValue)
        return false;
      result = (ushort) result1;
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.UInt16" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.UInt16" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.UInt16;
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
      return this;
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
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (UInt16), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
