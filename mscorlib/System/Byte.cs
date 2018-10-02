// Decompiled with JetBrains decompiler
// Type: System.Byte
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>Представляет 8-битовое целое число без знака.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Byte : IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>
  {
    private byte m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Byte" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const byte MaxValue = 255;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Byte" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const byte MinValue = 0;

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом и возвращает значение, указывающее, как соотносятся значения этих объектов.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Целое число со знаком, показывающее относительный порядок данного экземпляра и <paramref name="value" />.
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
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Byte" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is byte))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeByte"));
      return (int) this - (int) (byte) value;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным 8-битовым целым числом без знака и возвращает значение, указывающее, как соотносятся их значения.
    /// </summary>
    /// <param name="value">
    ///   8-битовое целое число без знака для сравнения.
    /// </param>
    /// <returns>
    /// Целое число со знаком, показывающее относительный порядок данного экземпляра и <paramref name="value" />.
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
    public int CompareTo(byte value)
    {
      return (int) this - (int) value;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Byte" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is byte))
        return false;
      return (int) this == (int) (byte) obj;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Byte" /> одно и то же значение.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="obj" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(byte obj)
    {
      return (int) this == (int) obj;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Byte" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this;
    }

    /// <summary>
    ///   Преобразует строковое представление числа в его эквивалент типа <see cref="T:System.Byte" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая преобразуемое число.
    ///    Данная строка интерпретируется с использованием стиля <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   Байтовое значение, эквивалентное числу, которое содержится в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> находится в неправильном формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Byte.MinValue" /> или больше значения <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte Parse(string s)
    {
      return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным стилем в его эквивалент в формате <see cref="T:System.Byte" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая преобразуемое число.
    ///    Строка интерпретируется с использованием стиля, указанного в <paramref name="style" />.
    /// </param>
    /// <param name="style">
    ///   Побитовое сочетание значений перечисления, обозначающих элементы стиля, которые могут быть представлены в параметре <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   Байтовое значение, эквивалентное числу, которое содержится в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> находится в неправильном формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Byte.MinValue" /> или больше значения <see cref="F:System.Byte.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> не является сочетанием значений <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате, связанном с определенным языком и региональными параметрами, в его эквивалент типа <see cref="T:System.Byte" />.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая преобразуемое число.
    ///    Данная строка интерпретируется с использованием стиля <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения об анализе параметра <paramref name="s" /> для определенного языка и региональных параметров.
    ///    Если значение параметра <paramref name="provider" /> равно <see langword="null" />, используются текущий язык и региональные параметры потока.
    /// </param>
    /// <returns>
    ///   Байтовое значение, эквивалентное числу, которое содержится в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> находится в неправильном формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Byte.MinValue" /> или больше значения <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte Parse(string s, IFormatProvider provider)
    {
      return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном стиле и формате, связанном с определенным языком и региональными параметрами, в его эквивалент типа <see cref="T:System.Byte" />.
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
    ///   Объект, который предоставляет сведения о формате параметра <paramref name="s" /> для определенного языка и региональных параметров.
    ///    Если значение параметра <paramref name="provider" /> равно <see langword="null" />, используются текущий язык и региональные параметры потока.
    /// </param>
    /// <returns>
    ///   Байтовое значение, эквивалентное числу, которое содержится в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> находится в неправильном формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.Byte.MinValue" /> или больше значения <see cref="F:System.Byte.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> не является сочетанием значений <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static byte Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      int int32;
      try
      {
        int32 = Number.ParseInt32(s, style, info);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), (Exception) ex);
      }
      if (int32 < 0 || int32 > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) int32;
    }

    /// <summary>
    ///   Предпринимает попытку преобразования строкового представления числа в его эквивалент типа <see cref="T:System.Byte" /> и возвращает значение, позволяющее определить, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая преобразуемое число.
    ///    Данная строка интерпретируется с использованием стиля <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="result">
    ///   При возврате этого метода содержит значение <see cref="T:System.Byte" />, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или ноль, если оно завершилось неудачей.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out byte result)
    {
      return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном стиле и формате, связанном с определенным языком и региональными параметрами, в его эквивалент типа <see cref="T:System.Byte" />.
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
    ///    Если значение параметра <paramref name="provider" /> равно <see langword="null" />, используются текущий язык и региональные параметры потока.
    /// </param>
    /// <param name="result">
    ///   По возврате из этого метода содержит 8-битовое целочисленное значение без знака, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или ноль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в правильном формате или представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out byte result)
    {
      result = (byte) 0;
      int result1;
      if (!Number.TryParseInt32(s, style, info, out result1) || result1 < 0 || result1 > (int) byte.MaxValue)
        return false;
      result = (byte) result1;
      return true;
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.Byte" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значения данного объекта, состоящее из последовательности цифр в диапазоне от 0 до 9 без начальных нулей.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует значение текущего объекта <see cref="T:System.Byte" /> в эквивалентное ему строковое представление с использованием заданного формата.
    /// </summary>
    /// <param name="format">Строка числового формата.</param>
    /// <returns>
    ///   Строковое представление текущего объекта <see cref="T:System.Byte" />, отформатированное, как указано в параметре <paramref name="format" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> содержит неподдерживаемый спецификатор.
    ///    Спецификаторы поддерживаемого формата перечислены в разделе "Примечания".
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatInt32((int) this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует числовое значение текущего объекта <see cref="T:System.Byte" /> в эквивалентное ему строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного объекта в формате, заданном в параметре <paramref name="provider" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует числовое значение текущего объекта <see cref="T:System.Byte" /> в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Стандартная или пользовательская строка числового формата.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление текущего объекта <see cref="T:System.Byte" />, отформатированное, как указано в параметрах <paramref name="format" /> и <paramref name="provider" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> содержит неподдерживаемый спецификатор.
    ///    Спецификаторы поддерживаемого формата перечислены в разделе "Примечания".
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatInt32((int) this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Byte" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Byte" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Byte;
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
      return this;
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
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Byte), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
