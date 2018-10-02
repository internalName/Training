// Decompiled with JetBrains decompiler
// Type: System.SByte
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>Представляет 8-разрядное целое число со знаком.</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct SByte : IComparable, IFormattable, IConvertible, IComparable<sbyte>, IEquatable<sbyte>
  {
    private sbyte m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.SByte" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const sbyte MaxValue = 127;
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.SByte" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const sbyte MinValue = -128;

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом и возвращает значение, указывающее, как соотносятся значения этих объектов.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="obj" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="obj" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="obj" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="obj" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="obj" /> не является классом <see cref="T:System.SByte" />.
    /// </exception>
    public int CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      if (!(obj is sbyte))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSByte"));
      return (int) this - (int) (sbyte) obj;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным 8-битовым целым числом со знаком и возвращает значение, указывающее, как соотносятся их значения.
    /// </summary>
    /// <param name="value">
    ///   8-битовое целое число со знаком для сравнения.
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
    public int CompareTo(sbyte value)
    {
      return (int) this - (int) value;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.SByte" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is sbyte))
        return false;
      return (int) this == (int) (sbyte) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли этот экземпляр заданному значению типа <see cref="T:System.SByte" />.
    /// </summary>
    /// <param name="obj">
    ///   Значение типа <see cref="T:System.SByte" /> для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(sbyte obj)
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
      return (int) this ^ (int) this << 8;
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
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.CurrentInfo);
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
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.GetInstance(provider));
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
    ///   <paramref name="format" /> недопустим.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return this.ToString(format, NumberFormatInfo.CurrentInfo);
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
    ///   <paramref name="format" /> недопустим.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return this.ToString(format, NumberFormatInfo.GetInstance(provider));
    }

    [SecuritySafeCritical]
    private string ToString(string format, NumberFormatInfo info)
    {
      if (this < (sbyte) 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
        return Number.FormatUInt32((uint) this & (uint) byte.MaxValue, format, info);
      return Number.FormatInt32((int) this, format, info);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в эквивалентное ему 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    ///    Данная строка интерпретируется с использованием стиля <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <returns>
    ///   8-битовое целое число со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.SByte.MinValue" /> или больше значения <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Parse(string s)
    {
      return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате в эквивалентное ему 8-битовое целое число со знаком.
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
    ///   8-битовое целое число со знаком, эквивалентное числу, указанному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представлен в формате, совместимом с <paramref name="style" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.SByte.MinValue" /> или больше значения <see cref="F:System.SByte.MaxValue" />.
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
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///   Преобразует строковое представление числа в указанном формате, соответствующем языку и региональным параметрам, в эквивалентное ему 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    ///    Данная строка интерпретируется с использованием стиля <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    ///    Если значение параметра <paramref name="provider" /> равно <see langword="null" />, используются текущий язык и региональные параметры потока.
    /// </param>
    /// <returns>
    ///   8-битовое целое число со знаком, эквивалентное числу, указанному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="s" /> представляет число, которое меньше значения <see cref="F:System.SByte.MinValue" /> или больше значения <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Parse(string s, IFormatProvider provider)
    {
      return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>
    ///   Преобразует строковое представление числа в формате, соответствующем определенному стилю, языку и региональным параметрам, в эквивалентное ему 8-битовое целое число со знаком.
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
    /// <returns>
    ///   8-битовое значение типа byte со знаком, эквивалентное числу, заданному в параметре <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="style" /> не является значением <see cref="T:System.Globalization.NumberStyles" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="style" /> не является сочетанием <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> и <see cref="F:System.Globalization.NumberStyles.HexNumber" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="s" /> не представлен в формате, совместимом с <paramref name="style" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="s" /> представляет число, которое меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="s" /> содержит ненулевые дробные разряды.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static sbyte Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      int int32;
      try
      {
        int32 = Number.ParseInt32(s, style, info);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), (Exception) ex);
      }
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (int32 < 0 || int32 > (int) byte.MaxValue)
          throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
        return (sbyte) int32;
      }
      if (int32 < (int) sbyte.MinValue || int32 > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) int32;
    }

    /// <summary>
    ///   Предпринимает попытку преобразования строкового представления числа в его эквивалент типа <see cref="T:System.SByte" /> и возвращает значение, позволяющее определить, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">Строка, содержащая преобразуемое число.</param>
    /// <param name="result">
    ///   По возвращении из этого метода содержит 8-битовое целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или ноль, если оно завершилось неудачей.
    ///    Преобразование завершается неудачей, если параметр <paramref name="s" /> равен <see langword="null" />, или <see cref="F:System.String.Empty" /> не в правильном формате или представляет число меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    ///    Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте <paramref name="result" />, будет перезаписано.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out sbyte result)
    {
      return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>
    ///   Предпринимает попытку преобразования числа в формате, который определяется заданным стилем и языком и региональными параметрами, в эквивалент типа <see cref="T:System.SByte" /> и возвращает значение, определяющее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, представляющая преобразуемое число.
    /// </param>
    /// <param name="style">
    ///   Побитовая комбинация значений перечисления, которая показывает разрешенный формат параметра <paramref name="s" />.
    ///    Обычно указывается значение <see cref="F:System.Globalization.NumberStyles.Integer" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, который предоставляет сведения о форматировании параметра <paramref name="s" /> в зависимости от языка и региональных параметров.
    /// </param>
    /// <param name="result">
    ///   При возвращении этим методом содержит 8-битное целочисленное значение со знаком, эквивалентное числу, содержащемуся в параметре <paramref name="s" />, если преобразование выполнено успешно, или нуль, если оно завершилось неудачей.
    ///    Преобразование завершается сбоем, если параметр <paramref name="s" /> равен <see langword="null" /> или <see cref="F:System.String.Empty" />, не находится в формате, совместимом с <paramref name="style" /> или представляет собой число меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
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
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out sbyte result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out sbyte result)
    {
      result = (sbyte) 0;
      int result1;
      if (!Number.TryParseInt32(s, style, info, out result1))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (result1 < 0 || result1 > (int) byte.MaxValue)
          return false;
        result = (sbyte) result1;
        return true;
      }
      if (result1 < (int) sbyte.MinValue || result1 > (int) sbyte.MaxValue)
        return false;
      result = (sbyte) result1;
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.SByte" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.SByte" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.SByte;
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
      return this;
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
      return (int) this;
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
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (SByte), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
