// Decompiled with JetBrains decompiler
// Type: System.Convert
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Преобразует значение одного базового типа данных к другому базовому типу данных.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Convert
  {
    internal static readonly RuntimeType[] ConvertTypes = new RuntimeType[19]
    {
      (RuntimeType) typeof (Empty),
      (RuntimeType) typeof (object),
      (RuntimeType) typeof (System.DBNull),
      (RuntimeType) typeof (bool),
      (RuntimeType) typeof (char),
      (RuntimeType) typeof (sbyte),
      (RuntimeType) typeof (byte),
      (RuntimeType) typeof (short),
      (RuntimeType) typeof (ushort),
      (RuntimeType) typeof (int),
      (RuntimeType) typeof (uint),
      (RuntimeType) typeof (long),
      (RuntimeType) typeof (ulong),
      (RuntimeType) typeof (float),
      (RuntimeType) typeof (double),
      (RuntimeType) typeof (Decimal),
      (RuntimeType) typeof (DateTime),
      (RuntimeType) typeof (object),
      (RuntimeType) typeof (string)
    };
    private static readonly RuntimeType EnumType = (RuntimeType) typeof (Enum);
    internal static readonly char[] base64Table = new char[65]
    {
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z',
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z',
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      '+',
      '/',
      '='
    };
    /// <summary>
    ///   Константа, представляющая не содержащий данных столбец базы данных, то есть значение NULL базы данных.
    /// </summary>
    public static readonly object DBNull = (object) System.DBNull.Value;
    private const int base64LineBreakPosition = 76;

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для заданного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.TypeCode" /> для <paramref name="value" /> или <see cref="F:System.TypeCode.Empty" />, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TypeCode GetTypeCode(object value)
    {
      if (value == null)
        return TypeCode.Empty;
      IConvertible convertible = value as IConvertible;
      if (convertible != null)
        return convertible.GetTypeCode();
      return TypeCode.Object;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли заданный объект тип <see cref="T:System.DBNull" />.
    /// </summary>
    /// <param name="value">Объект.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="value" /> имеет тип <see cref="T:System.DBNull" />; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsDBNull(object value)
    {
      if (value == System.DBNull.Value)
        return true;
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
        return false;
      return convertible.GetTypeCode() == TypeCode.DBNull;
    }

    /// <summary>
    ///   Возвращает объект указанного типа, чье значение эквивалентно заданному объекту.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="typeCode">Тип возвращаемого объекта.</param>
    /// <returns>
    ///   Объект, базовый тип которого равен <paramref name="typeCode" />, а значение эквивалентно <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если <paramref name="value" /> равняется <see langword="null" />, а <paramref name="typeCode" /> равняется <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" /> или <see cref="F:System.TypeCode.Object" />.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равен <see langword="null" /> и <paramref name="typeCode" /> указывает тип значения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом, распознаваемым типом <paramref name="typeCode" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое не входит в диапазон типа <paramref name="typeCode" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="typeCode" /> недопустим.
    /// </exception>
    public static object ChangeType(object value, TypeCode typeCode)
    {
      return Convert.ChangeType(value, typeCode, (IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает объект указанного типа, чье значение эквивалентно заданному объекту.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="typeCode">Тип возвращаемого объекта.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Объект, базовый тип которого равен <paramref name="typeCode" />, а значение эквивалентно <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если <paramref name="value" /> равняется <see langword="null" />, а <paramref name="typeCode" /> равняется <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" /> или <see cref="F:System.TypeCode.Object" />.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> равен <see langword="null" /> и <paramref name="typeCode" /> указывает тип значения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом для типа <paramref name="typeCode" />, распознаваемым <paramref name="provider" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое не входит в диапазон типа <paramref name="typeCode" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="typeCode" /> недопустим.
    /// </exception>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
    {
      if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
        return (object) null;
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
      switch (typeCode)
      {
        case TypeCode.Empty:
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
        case TypeCode.Object:
          return value;
        case TypeCode.DBNull:
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
        case TypeCode.Boolean:
          return (object) convertible.ToBoolean(provider);
        case TypeCode.Char:
          return (object) convertible.ToChar(provider);
        case TypeCode.SByte:
          return (object) convertible.ToSByte(provider);
        case TypeCode.Byte:
          return (object) convertible.ToByte(provider);
        case TypeCode.Int16:
          return (object) convertible.ToInt16(provider);
        case TypeCode.UInt16:
          return (object) convertible.ToUInt16(provider);
        case TypeCode.Int32:
          return (object) convertible.ToInt32(provider);
        case TypeCode.UInt32:
          return (object) convertible.ToUInt32(provider);
        case TypeCode.Int64:
          return (object) convertible.ToInt64(provider);
        case TypeCode.UInt64:
          return (object) convertible.ToUInt64(provider);
        case TypeCode.Single:
          return (object) convertible.ToSingle(provider);
        case TypeCode.Double:
          return (object) convertible.ToDouble(provider);
        case TypeCode.Decimal:
          return (object) convertible.ToDecimal(provider);
        case TypeCode.DateTime:
          return (object) convertible.ToDateTime(provider);
        case TypeCode.String:
          return (object) convertible.ToString(provider);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_UnknownTypeCode"));
      }
    }

    internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException(nameof (targetType));
      RuntimeType runtimeType = targetType as RuntimeType;
      if (runtimeType != (RuntimeType) null)
      {
        if (value.GetType() == targetType)
          return (object) value;
        if (runtimeType == Convert.ConvertTypes[3])
          return (object) value.ToBoolean(provider);
        if (runtimeType == Convert.ConvertTypes[4])
          return (object) value.ToChar(provider);
        if (runtimeType == Convert.ConvertTypes[5])
          return (object) value.ToSByte(provider);
        if (runtimeType == Convert.ConvertTypes[6])
          return (object) value.ToByte(provider);
        if (runtimeType == Convert.ConvertTypes[7])
          return (object) value.ToInt16(provider);
        if (runtimeType == Convert.ConvertTypes[8])
          return (object) value.ToUInt16(provider);
        if (runtimeType == Convert.ConvertTypes[9])
          return (object) value.ToInt32(provider);
        if (runtimeType == Convert.ConvertTypes[10])
          return (object) value.ToUInt32(provider);
        if (runtimeType == Convert.ConvertTypes[11])
          return (object) value.ToInt64(provider);
        if (runtimeType == Convert.ConvertTypes[12])
          return (object) value.ToUInt64(provider);
        if (runtimeType == Convert.ConvertTypes[13])
          return (object) value.ToSingle(provider);
        if (runtimeType == Convert.ConvertTypes[14])
          return (object) value.ToDouble(provider);
        if (runtimeType == Convert.ConvertTypes[15])
          return (object) value.ToDecimal(provider);
        if (runtimeType == Convert.ConvertTypes[16])
          return (object) value.ToDateTime(provider);
        if (runtimeType == Convert.ConvertTypes[18])
          return (object) value.ToString(provider);
        if (runtimeType == Convert.ConvertTypes[1])
          return (object) value;
        if (runtimeType == Convert.EnumType)
          return (object) (Enum) value;
        if (runtimeType == Convert.ConvertTypes[2])
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
        if (runtimeType == Convert.ConvertTypes[0])
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
      }
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) value.GetType().FullName, (object) targetType.FullName));
    }

    /// <summary>
    ///   Возвращает объект указанного типа, значение которого эквивалентно заданному объекту.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="conversionType">Тип возвращаемого объекта.</param>
    /// <returns>
    ///   Объект, тип которого равен <paramref name="conversionType" />, а значение эквивалентно <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если <paramref name="value" /> равняется <see langword="null" />, а <paramref name="conversionType" /> не является типом значения.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" /> — <see langword="null" />, а <paramref name="conversionType" /> — это тип значения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом, распознаваемым типом <paramref name="conversionType" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое не входит в диапазон <paramref name="conversionType" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="conversionType" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, Type conversionType)
    {
      return Convert.ChangeType(value, conversionType, (IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает объект указанного типа, чье значение эквивалентно заданному объекту.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="conversionType">Тип возвращаемого объекта.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Объект, тип которого равен <paramref name="conversionType" />, а значение эквивалентно <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" />, если <see cref="T:System.Type" /> параметра <paramref name="value" /> равен <paramref name="conversionType" />.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если <paramref name="value" /> равняется <see langword="null" />, а <paramref name="conversionType" /> не является типом значения.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" /> — <see langword="null" />, а <paramref name="conversionType" /> — это тип значения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом для типа <paramref name="conversionType" />, распознаваемым поставщиком <paramref name="provider" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое не входит в диапазон <paramref name="conversionType" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="conversionType" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
    {
      if (conversionType == (Type) null)
        throw new ArgumentNullException(nameof (conversionType));
      if (value == null)
      {
        if (conversionType.IsValueType)
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCastNullToValueType"));
        return (object) null;
      }
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
      {
        if (value.GetType() == conversionType)
          return value;
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
      }
      RuntimeType runtimeType = conversionType as RuntimeType;
      if (runtimeType == Convert.ConvertTypes[3])
        return (object) convertible.ToBoolean(provider);
      if (runtimeType == Convert.ConvertTypes[4])
        return (object) convertible.ToChar(provider);
      if (runtimeType == Convert.ConvertTypes[5])
        return (object) convertible.ToSByte(provider);
      if (runtimeType == Convert.ConvertTypes[6])
        return (object) convertible.ToByte(provider);
      if (runtimeType == Convert.ConvertTypes[7])
        return (object) convertible.ToInt16(provider);
      if (runtimeType == Convert.ConvertTypes[8])
        return (object) convertible.ToUInt16(provider);
      if (runtimeType == Convert.ConvertTypes[9])
        return (object) convertible.ToInt32(provider);
      if (runtimeType == Convert.ConvertTypes[10])
        return (object) convertible.ToUInt32(provider);
      if (runtimeType == Convert.ConvertTypes[11])
        return (object) convertible.ToInt64(provider);
      if (runtimeType == Convert.ConvertTypes[12])
        return (object) convertible.ToUInt64(provider);
      if (runtimeType == Convert.ConvertTypes[13])
        return (object) convertible.ToSingle(provider);
      if (runtimeType == Convert.ConvertTypes[14])
        return (object) convertible.ToDouble(provider);
      if (runtimeType == Convert.ConvertTypes[15])
        return (object) convertible.ToDecimal(provider);
      if (runtimeType == Convert.ConvertTypes[16])
        return (object) convertible.ToDateTime(provider);
      if (runtimeType == Convert.ConvertTypes[18])
        return (object) convertible.ToString(provider);
      if (runtimeType == Convert.ConvertTypes[1])
        return value;
      return convertible.ToType(conversionType, provider);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> или <see langword="false" />, отражающее значение, возвращаемое методом <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> при вызове для базового типа параметра <paramref name="value" />.
    ///    Если значением параметра <paramref name="value" /> является <see langword="null" />, метод возвращает <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> является строкой, которая не равна <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование <paramref name="value" /> в <see cref="T:System.Boolean" /> не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool ToBoolean(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToBoolean((IFormatProvider) null);
      return false;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное логическое значение, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> или <see langword="false" />, отражающее значение, возвращаемое методом <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> при вызове для базового типа параметра <paramref name="value" />.
    ///    Если значением параметра <paramref name="value" /> является <see langword="null" />, метод возвращает <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> является строкой, которая не равна <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование <paramref name="value" /> в <see cref="T:System.Boolean" /> не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool ToBoolean(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToBoolean(provider);
      return false;
    }

    /// <summary>
    ///   Возвращает заданное логическое значение; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Возвращаемое логическое значение.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(bool value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(sbyte value)
    {
      return (uint) value > 0U;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static bool ToBoolean(char value)
    {
      return ((IConvertible) value).ToBoolean((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(byte value)
    {
      return value > (byte) 0;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(short value)
    {
      return (uint) value > 0U;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(ushort value)
    {
      return value > (ushort) 0;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(int value)
    {
      return (uint) value > 0U;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(uint value)
    {
      return value > 0U;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(long value)
    {
      return (ulong) value > 0UL;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(ulong value)
    {
      return value > 0UL;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление логического значения в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая значение <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> имеет значение <see cref="F:System.Boolean.TrueString" />, или <see langword="false" />, если параметр <paramref name="value" /> имеет значение <see cref="F:System.Boolean.FalseString" /> либо <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="value" /> не равен <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool ToBoolean(string value)
    {
      if (value == null)
        return false;
      return bool.Parse(value);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление логического значения в эквивалентное логическое значение, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая значение <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> имеет значение <see cref="F:System.Boolean.TrueString" />, или <see langword="false" />, если параметр <paramref name="value" /> имеет значение <see cref="F:System.Boolean.FalseString" /> либо <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="value" /> не равен <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool ToBoolean(string value, IFormatProvider provider)
    {
      if (value == null)
        return false;
      return bool.Parse(value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(float value)
    {
      return (double) value != 0.0;
    }

    /// <summary>
    ///   Преобразует значение заданного числа двойной точности с плавающей запятой в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(double value)
    {
      return value != 0.0;
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное логическое значение.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> не равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool ToBoolean(Decimal value)
    {
      return value != Decimal.Zero;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static bool ToBoolean(DateTime value)
    {
      return ((IConvertible) value).ToBoolean((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в знак Юникода.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению, или <see cref="F:System.Char.MinValue" />, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <paramref name="value" /> является пустой строкой.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование <paramref name="value" /> в <see cref="T:System.Char" /> не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" /> или больше <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToChar((IFormatProvider) null);
      return char.MinValue;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентный знак Юникода, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен <paramref name="value" />, или <see cref="F:System.Char.MinValue" />, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <paramref name="value" /> является пустой строкой.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование <paramref name="value" /> в <see cref="T:System.Char" /> не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" /> или больше <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToChar(provider);
      return char.MinValue;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static char ToChar(bool value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>
    ///   Возвращает заданное значение символа Юникода; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Возвращаемый знак Юникода.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    public static char ToChar(char value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(sbyte value)
    {
      if (value < (sbyte) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа без знака в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static char ToChar(byte value)
    {
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(short value)
    {
      if (value < (short) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного целого числа без знака в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(ushort value)
    {
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" /> или больше <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(int value)
    {
      if (value < 0 || value > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного целого числа без знака в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(uint value)
    {
      if (value > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Char.MinValue" /> или больше <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(long value)
    {
      if (value < 0L || value > (long) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Знак Юникода, который эквивалентен значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Char.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(ulong value)
    {
      if (value > (ulong) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>
    ///   Преобразует первый знак указанной строки в знак Юникода.
    /// </summary>
    /// <param name="value">Строка длиной в 1 знак.</param>
    /// <returns>
    ///   Знак Юникода, эквивалентный первому и единственному знаку в <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="value" /> не равна 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(string value)
    {
      return Convert.ToChar(value, (IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует первый знак заданной строки в знак Юникода, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка длиной в 1 знак или <see langword="null" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Знак Юникода, эквивалентный первому и единственному знаку в <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="value" /> не равна 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(string value, IFormatProvider provider)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (value.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
      return value[0];
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static char ToChar(float value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static char ToChar(double value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static char ToChar(Decimal value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static char ToChar(DateTime value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToSByte((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 8-разрядное знаковое целое число, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToSByte(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(bool value)
    {
      return !value ? (sbyte) 0 : (sbyte) 1;
    }

    /// <summary>
    ///   Возвращает заданное 8-битовое целое число со знаком; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 8-разрядное целое число со знаком.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(sbyte value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(char value)
    {
      if (value > '\x007F')
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(byte value)
    {
      if (value > (byte) 127)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа со знаком в эквивалентное 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(short value)
    {
      if (value < (short) sbyte.MinValue || value > (short) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(ushort value)
    {
      if (value > (ushort) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(int value)
    {
      if (value < (int) sbyte.MinValue || value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(uint value)
    {
      if (value > (uint) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(long value)
    {
      if (value < (long) sbyte.MinValue || value > (long) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(ulong value)
    {
      if (value > (ulong) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(float value)
    {
      return Convert.ToSByte((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(double value)
    {
      return Convert.ToSByte(Convert.ToInt32(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.SByte.MaxValue" /> или меньше <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(Decimal value)
    {
      return Decimal.ToSByte(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число со знаком, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если значение равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value)
    {
      if (value == null)
        return 0;
      return sbyte.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 8-битовое целое число со знаком, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следуют цифры (0-9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value, IFormatProvider provider)
    {
      return sbyte.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static sbyte ToSByte(DateTime value)
    {
      return ((IConvertible) value).ToSByte((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом свойства для значения <see cref="T:System.Byte" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование из <paramref name="value" /> в тип <see cref="T:System.Byte" /> не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToByte((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 8-разрядное целое число без знака, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является форматом свойства для значения <see cref="T:System.Byte" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование из <paramref name="value" /> в тип <see cref="T:System.Byte" /> не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToByte(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static byte ToByte(bool value)
    {
      return !value ? (byte) 0 : (byte) 1;
    }

    /// <summary>
    ///   Возвращает заданное 8-битовое целое число без знака; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 8-разрядное целое число без знака.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static byte ToByte(byte value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(char value)
    {
      if (value > 'ÿ')
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(sbyte value)
    {
      if (value < (sbyte) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(short value)
    {
      if (value < (short) 0 || value > (short) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного целого числа без знака в эквивалентное 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(ushort value)
    {
      if (value > (ushort) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(int value)
    {
      if (value < 0 || value > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(uint value)
    {
      if (value > (uint) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(long value)
    {
      if (value < 0L || value > (long) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(ulong value)
    {
      if (value > (ulong) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Byte.MaxValue" /> или меньше <see cref="F:System.Byte.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(float value)
    {
      return Convert.ToByte((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Byte.MaxValue" /> или меньше <see cref="F:System.Byte.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(double value)
    {
      return Convert.ToByte(Convert.ToInt32(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 8-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Byte.MaxValue" /> или меньше <see cref="F:System.Byte.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(Decimal value)
    {
      return Decimal.ToByte(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(string value)
    {
      if (value == null)
        return 0;
      return byte.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 8-разрядное целое число без знака, учитывая сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return byte.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static byte ToByte(DateTime value)
    {
      return ((IConvertible) value).ToByte((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Int16" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt16((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 16-битовое целое число со знаком, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Int16" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt16(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static short ToInt16(bool value)
    {
      return !value ? (short) 0 : (short) 1;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(char value)
    {
      if (value > '翿')
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа со знаком в эквивалентное 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(sbyte value)
    {
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа без знака в эквивалентное 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static short ToInt16(byte value)
    {
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(ushort value)
    {
      if (value > (ushort) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число со знаком, эквивалентное <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int16.MaxValue" /> или меньше <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(int value)
    {
      if (value < (int) short.MinValue || value > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(uint value)
    {
      if (value > (uint) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Возвращает заданное 16-битовое целое число со знаком; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 16-разрядное целое число со знаком.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static short ToInt16(short value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int16.MaxValue" /> или меньше <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(long value)
    {
      if (value < (long) short.MinValue || value > (long) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(ulong value)
    {
      if (value > (ulong) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int16.MaxValue" /> или меньше <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(float value)
    {
      return Convert.ToInt16((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int16.MaxValue" /> или меньше <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(double value)
    {
      return Convert.ToInt16(Convert.ToInt32(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int16.MaxValue" /> или меньше <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(Decimal value)
    {
      return Decimal.ToInt16(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(string value)
    {
      if (value == null)
        return 0;
      return short.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 16-битовое целое число со знаком, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return short.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static short ToInt16(DateTime value)
    {
      return ((IConvertible) value).ToInt16((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt16((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 16-битовое целое число без знака, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt16(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(bool value)
    {
      return !value ? (ushort) 0 : (ushort) 1;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, эквивалентное <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(char value)
    {
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(sbyte value)
    {
      if (value < (sbyte) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа без знака в эквивалентное 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(byte value)
    {
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(short value)
    {
      if (value < (short) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(int value)
    {
      if (value < 0 || value > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Возвращает заданное 16-битовое целое число без знака; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 16-разрядное целое число без знака.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(ushort value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного целого числа без знака в эквивалентное 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(uint value)
    {
      if (value > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(long value)
    {
      if (value < 0L || value > (long) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(ulong value)
    {
      if (value > (ulong) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(float value)
    {
      return Convert.ToUInt16((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(double value)
    {
      return Convert.ToUInt16(Convert.ToInt32(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 16-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(Decimal value)
    {
      return Decimal.ToUInt16(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value)
    {
      if (value == null)
        return 0;
      return ushort.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 16-битовое целое число без знака, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return ushort.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static ushort ToUInt16(DateTime value)
    {
      return ((IConvertible) value).ToUInt16((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt32((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 32-битовое целое число со знаком, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt32(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ToInt32(bool value)
    {
      return !value ? 0 : 1;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ToInt32(char value)
    {
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа со знаком в эквивалентное 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(sbyte value)
    {
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ToInt32(byte value)
    {
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного целого числа со знаком в эквивалентное 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ToInt32(short value)
    {
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(ushort value)
    {
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(uint value)
    {
      if (value > (uint) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>
    ///   Возвращает заданное 32-битовое целое число со знаком; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 32-разрядное целое число со знаком.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ToInt32(int value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа со знаком в эквивалентное 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int32.MaxValue" /> или меньше <see cref="F:System.Int32.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(long value)
    {
      if (value < (long) int.MinValue || value > (long) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(ulong value)
    {
      if (value > (ulong) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int32.MaxValue" /> или меньше <see cref="F:System.Int32.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(float value)
    {
      return Convert.ToInt32((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int32.MaxValue" /> или меньше <see cref="F:System.Int32.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(double value)
    {
      if (value >= 0.0)
      {
        if (value < 2147483647.5)
        {
          int num1 = (int) value;
          double num2 = value - (double) num1;
          if (num2 > 0.5 || num2 == 0.5 && (num1 & 1) != 0)
            ++num1;
          return num1;
        }
      }
      else if (value >= -2147483648.5)
      {
        int num1 = (int) value;
        double num2 = value - (double) num1;
        if (num2 < -0.5 || num2 == -0.5 && (num1 & 1) != 0)
          --num1;
        return num1;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int32.MaxValue" /> или меньше <see cref="F:System.Int32.MinValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ToInt32(Decimal value)
    {
      return Decimal.FCallToInt32(value);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(string value)
    {
      if (value == null)
        return 0;
      return int.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 32-битовое целое число со знаком, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return int.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static int ToInt32(DateTime value)
    {
      return ((IConvertible) value).ToInt32((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль (0), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt32.MinValue" /> или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt32((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 32-битовое целое число без знака, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Параметр <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt32.MinValue" /> или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt32(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(bool value)
    {
      return !value ? 0U : 1U;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(char value)
    {
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(sbyte value)
    {
      if (value < (sbyte) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(byte value)
    {
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(short value)
    {
      if (value < (short) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(ushort value)
    {
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(int value)
    {
      if (value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>
    ///   Возвращает заданное 32-битовое целое число без знака; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 32-разрядное целое число без знака.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(uint value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(long value)
    {
      if (value < 0L || value > (long) uint.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(ulong value)
    {
      if (value > (ulong) uint.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(float value)
    {
      return Convert.ToUInt32((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(double value)
    {
      if (value < -0.5 || value >= 4294967295.5)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      uint num1 = (uint) value;
      double num2 = value - (double) num1;
      if (num2 > 0.5 || num2 == 0.5 && ((int) num1 & 1) != 0)
        ++num1;
      return num1;
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 32-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(Decimal value)
    {
      return Decimal.ToUInt32(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt32.MinValue" /> или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value)
    {
      if (value == null)
        return 0;
      return uint.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 32-битовое целое число без знака, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt32.MinValue" /> или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return uint.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static uint ToUInt32(DateTime value)
    {
      return ((IConvertible) value).ToUInt32((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt64((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 64-битовое целое число со знаком, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt64(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(bool value)
    {
      return value ? 1L : 0L;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(char value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа со знаком в эквивалентное 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(sbyte value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(byte value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(short value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(ushort value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(int value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(uint value)
    {
      return (long) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" /> больше значения <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(ulong value)
    {
      if (value > (ulong) long.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
      return (long) value;
    }

    /// <summary>
    ///   Возвращает заданное 64-битовое целое число со знаком; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">64-разрядное целое число со знаком.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static long ToInt64(long value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int64.MaxValue" /> или меньше <see cref="F:System.Int64.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(float value)
    {
      return Convert.ToInt64((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int64.MaxValue" /> или меньше <see cref="F:System.Int64.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(double value)
    {
      return checked ((long) Math.Round(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа со знаком.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Int64.MaxValue" /> или меньше <see cref="F:System.Int64.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(Decimal value)
    {
      return Decimal.ToInt64(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(string value)
    {
      if (value == null)
        return 0;
      return long.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 64-битовое целое число со знаком, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return long.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static long ToInt64(DateTime value)
    {
      return ((IConvertible) value).ToInt64((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt64.MinValue" /> или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt64((IFormatProvider) null);
      return 0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное 64-битовое целое число без знака, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt64.MinValue" /> или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt64(provider);
      return 0;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(bool value)
    {
      return !value ? 0UL : 1UL;
    }

    /// <summary>
    ///   Преобразует значение заданного символа Юникода в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(char value)
    {
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(sbyte value)
    {
      if (value < (sbyte) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(byte value)
    {
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(short value)
    {
      if (value < (short) 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(ushort value)
    {
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(int value)
    {
      if (value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного целого числа без знака в эквивалентное 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(uint value)
    {
      return (ulong) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Значение параметра <paramref name="value" /> меньше нуля.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(long value)
    {
      if (value < 0L)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>
    ///   Возвращает заданное 64-битовое целое число без знака; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое 64-разрядное целое число без знака.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(ulong value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(float value)
    {
      return Convert.ToUInt64((double) value);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(double value)
    {
      return checked ((ulong) Math.Round(value));
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Значение <paramref name="value" />, округленное до ближайшего 64-разрядного целого числа без знака.
    ///    Если <paramref name="value" /> имеет среднее значение между двумя целыми числами, будет возвращено четное число; так, значение 4,5 преобразуется в 4, а 5,5 — в 6.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> меньше нуля или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(Decimal value)
    {
      return Decimal.ToUInt64(Decimal.Round(value, 0));
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не состоит из необязательного знака, за которым следует последовательность цифр (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt64.MinValue" /> или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value)
    {
      if (value == null)
        return 0;
      return ulong.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное 64-битовое целое число без знака, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является последовательностью из необязательного знака, за которым следуют цифры (0–9).
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число, которое меньше <see cref="F:System.UInt64.MinValue" /> или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return ulong.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static ulong ToUInt64(DateTime value)
    {
      return ((IConvertible) value).ToUInt64((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float ToSingle(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToSingle((IFormatProvider) null);
      return 0.0f;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в число с плавающей запятой одиночной точности, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> представляет число, которое меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float ToSingle(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToSingle(provider);
      return 0.0f;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное знаковое целое число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(sbyte value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(byte value)
    {
      return (float) value;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static float ToSingle(char value)
    {
      return ((IConvertible) value).ToSingle((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(short value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(ushort value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(int value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(uint value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(long value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(ulong value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Возвращает заданное число с плавающей запятой одиночной точности; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(float value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// 
    ///   Значение <paramref name="value" /> округляется до ближайшего числа.
    ///    Например, при округлении до второго знака после десятичной запятой значение 2,345 преобразуется в 2,34, а значение 2,355 — в 2,36.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(double value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, которое эквивалентно значению <paramref name="value" />.
    /// 
    ///   Значение <paramref name="value" /> округляется до ближайшего числа.
    ///    Например, при округлении до второго знака после десятичной запятой значение 2,345 преобразуется в 2,34, а значение 2,355 — в 2,36.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(Decimal value)
    {
      return (float) value;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float ToSingle(string value)
    {
      if (value == null)
        return 0.0f;
      return float.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное число с плавающей запятой одиночной точности, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Single.MinValue" /> или больше <see cref="F:System.Single.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static float ToSingle(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0.0f;
      return float.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное число с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static float ToSingle(bool value)
    {
      return value ? 1f : 0.0f;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static float ToSingle(DateTime value)
    {
      return ((IConvertible) value).ToSingle((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Double" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double ToDouble(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDouble((IFormatProvider) null);
      return 0.0;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в число с плавающей запятой двойной точности, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное значению <paramref name="value" />, или нуль, если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Double" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double ToDouble(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDouble(provider);
      return 0.0;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного знакового целого числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число со знаком, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(sbyte value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого числа без знака в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(byte value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного знакового целого числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(short value)
    {
      return (double) value;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static double ToDouble(char value)
    {
      return ((IConvertible) value).ToDouble((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного целого числа без знака в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(ushort value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного знакового целого числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(int value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного целого числа без знака в эквивалентное число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(uint value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного знакового целого числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(long value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(ulong value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одинарной точности в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(float value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Возвращает заданное число с плавающей запятой двойной точности; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">
    ///   Возвращаемое число с плавающей запятой двойной точности.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(double value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(Decimal value)
    {
      return (double) value;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double ToDouble(string value)
    {
      if (value == null)
        return 0.0;
      return double.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное число с плавающей запятой двойной точности, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой двойной точности, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Double.MinValue" /> или больше <see cref="F:System.Double.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double ToDouble(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0.0;
      return double.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное число с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static double ToDouble(bool value)
    {
      return value ? 1.0 : 0.0;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static double ToDouble(DateTime value)
    {
      return ((IConvertible) value).ToDouble((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Десятичное число, эквивалентное <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> равняется <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Decimal" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDecimal((IFormatProvider) null);
      return Decimal.Zero;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное десятичное число, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Десятичное число, эквивалентное <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> равняется <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> имеет неправильный формат для типа <see cref="T:System.Decimal" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDecimal(provider);
      return Decimal.Zero;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного знакового целого числа в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(sbyte value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 8-разрядного целого число без знака в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(byte value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static Decimal ToDecimal(char value)
    {
      return ((IConvertible) value).ToDecimal((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного знакового целого числа в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(short value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 16-разрядного целого числа без знака в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(ushort value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного знакового целого числа в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(int value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 32-разрядного целого числа без знака в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(uint value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного знакового целого числа в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(long value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного 64-разрядного целого числа без знака в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(ulong value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Decimal.MaxValue" /> или меньше <see cref="F:System.Decimal.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(float value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Десятичное число, которое эквивалентно значению <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> больше <see cref="F:System.Decimal.MaxValue" /> или меньше <see cref="F:System.Decimal.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(double value)
    {
      return (Decimal) value;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <returns>
    ///   Десятичное число, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> равняется <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(string value)
    {
      if (value == null)
        return Decimal.Zero;
      return Decimal.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное десятичное число, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Десятичное число, эквивалентное числу <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> равняется <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является числом в допустимом формате.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Decimal.MinValue" /> или больше <see cref="F:System.Decimal.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(string value, IFormatProvider provider)
    {
      if (value == null)
        return Decimal.Zero;
      return Decimal.Parse(value, NumberStyles.Number, provider);
    }

    /// <summary>
    ///   Возвращает заданное десятичное число; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Десятичное число.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(Decimal value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует заданное логическое значение в эквивалентное десятичное число.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Число 1, если <paramref name="value" /> имеет значение <see langword="true" />; в противном случае — 0.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(bool value)
    {
      return (Decimal) (value ? 1 : 0);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static Decimal ToDecimal(DateTime value)
    {
      return ((IConvertible) value).ToDecimal((IFormatProvider) null);
    }

    /// <summary>
    ///   Возвращает заданный объект <see cref="T:System.DateTime" />; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Значение даты и времени.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    public static DateTime ToDateTime(DateTime value)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует значение указанного объекта в объект <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Дата и время, эквивалентные значению <paramref name="value" />, или дата и время, эквивалентные значению <see cref="F:System.DateTime.MinValue" />, если значение <paramref name="value" /> равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является допустимым значением даты и времени.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDateTime((IFormatProvider) null);
      return DateTime.MinValue;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в объект <see cref="T:System.DateTime" />, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Объект, реализующий интерфейс <see cref="T:System.IConvertible" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Дата и время, эквивалентные значению <paramref name="value" />, или дата и время, эквивалентные значению <see cref="F:System.DateTime.MinValue" />, если значение <paramref name="value" /> равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не является допустимым значением даты и времени.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="value" /> не реализует интерфейс <see cref="T:System.IConvertible" />.
    /// 
    ///   -или-
    /// 
    ///   Преобразование не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDateTime(provider);
      return DateTime.MinValue;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление даты и времени в эквивалентное значение даты и времени.
    /// </summary>
    /// <param name="value">
    ///   Строковое представление даты и времени.
    /// </param>
    /// <returns>
    ///   Дата и время, эквивалентные значению <paramref name="value" />, или дата и время, эквивалентные значению <see cref="F:System.DateTime.MinValue" />, если значение <paramref name="value" /> равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> является неправильно отформатированной строкой даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(string value)
    {
      if (value == null)
        return new DateTime(0L);
      return DateTime.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует заданное строковое представление числа в эквивалентное значение даты и времени, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая дату и время, которые нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Дата и время, эквивалентные значению <paramref name="value" />, или дата и время, эквивалентные значению <see cref="F:System.DateTime.MinValue" />, если значение <paramref name="value" /> равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> является неправильно отформатированной строкой даты и времени.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(string value, IFormatProvider provider)
    {
      if (value == null)
        return new DateTime(0L);
      return DateTime.Parse(value, provider);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(sbyte value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(byte value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(short value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(ushort value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(int value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(uint value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(long value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(ulong value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(bool value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(char value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(float value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">
    ///   Значение с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(double value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.InvalidCastException" />.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>
    ///   Данное преобразование не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Данное преобразование не поддерживается.
    /// </exception>
    public static DateTime ToDateTime(Decimal value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Объект, содержащий значение для преобразования, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Строковое представление имеет значение <paramref name="value" /> или <see cref="F:System.String.Empty" />, если значение параметра <paramref name="value" /> равно <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(object value)
    {
      return Convert.ToString(value, (IFormatProvider) null);
    }

    /// <summary>
    ///   Преобразует значение указанного объекта в эквивалентное строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="value">
    ///   Объект, содержащий значение для преобразования, или <see langword="null" />.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="value" /> или <see cref="F:System.String.Empty" />, если <paramref name="value" /> представляет собой объект, значение которого равно <see langword="null" />.
    ///    Если значением параметра <paramref name="value" /> является <see langword="null" />, метод возвращает <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(object value, IFormatProvider provider)
    {
      IConvertible convertible = value as IConvertible;
      if (convertible != null)
        return convertible.ToString(provider);
      IFormattable formattable = value as IFormattable;
      if (formattable != null)
        return formattable.ToString((string) null, provider);
      if (value != null)
        return value.ToString();
      return string.Empty;
    }

    /// <summary>
    ///   Преобразует указанное логическое значение в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(bool value)
    {
      return value.ToString();
    }

    /// <summary>
    ///   Преобразует указанное логическое значение в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое необходимо преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Экземпляр объекта.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(bool value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного знака Юникода в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(char value)
    {
      return char.ToString(value);
    }

    /// <summary>
    ///   Преобразует значение заданного знака Юникода в эквивалентное строковое представление, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(char value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(sbyte value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа со знаком в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(sbyte value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(byte value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(byte value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(short value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(short value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ushort value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа без знака в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число без знака для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ushort value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(int value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(int value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(uint value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа без знака в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число без знака для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(uint value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(long value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(long value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ulong value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа без знака в эквивалентное строковое представление, учитывая указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число без знака для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ulong value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(float value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой одиночной точности в эквивалентное строковое представление, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой одиночной точности, которое нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(float value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(double value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного числа с плавающей запятой двойной точности в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Число с плавающей запятой двойной точности, которое нужно преобразовать.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(double value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(Decimal value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного десятичного числа в эквивалентное строковое представление, используя указанные сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="value">Десятичное число для преобразования.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(Decimal value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта <see cref="T:System.DateTime" /> в эквивалентное строковое представление.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(DateTime value)
    {
      return value.ToString();
    }

    /// <summary>
    ///   Преобразует значение заданного объекта <see cref="T:System.DateTime" /> в эквивалентное строковое представление с использованием указанных сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="value">
    ///   Значение даты и времени для преобразования.
    /// </param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Строковое представление параметра <paramref name="value" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(DateTime value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>
    ///   Возвращает заданное строковое представление; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Возвращаемая строка.</param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    public static string ToString(string value)
    {
      return value;
    }

    /// <summary>
    ///   Возвращает заданное строковое представление; фактическое преобразование не производится.
    /// </summary>
    /// <param name="value">Возвращаемая строка.</param>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   <paramref name="value" /> возвращается без изменений.
    /// </returns>
    public static string ToString(string value, IFormatProvider provider)
    {
      return value;
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 8-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Параметру <paramref name="value" />, представляющему десятичное число без знака, предшествует знак минус.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Byte.MinValue" /> или больше <see cref="F:System.Byte.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte ToByte(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int num = ParseNumbers.StringToInt(value, fromBase, 4608);
      if (num < 0 || num > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) num;
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 8-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число со знаком, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равняется 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.SByte.MinValue" /> или больше <see cref="F:System.SByte.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int num = ParseNumbers.StringToInt(value, fromBase, 5120);
      if (fromBase != 10 && num <= (int) byte.MaxValue || num >= (int) sbyte.MinValue && num <= (int) sbyte.MaxValue)
        return (sbyte) num;
      throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 16-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   16-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int16.MinValue" /> или больше <see cref="F:System.Int16.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short ToInt16(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int num = ParseNumbers.StringToInt(value, fromBase, 6144);
      if (fromBase != 10 && num <= (int) ushort.MaxValue || num >= (int) short.MinValue && num <= (int) short.MaxValue)
        return (short) num;
      throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 16-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt16.MinValue" /> или больше <see cref="F:System.UInt16.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int num = ParseNumbers.StringToInt(value, fromBase, 4608);
      if (num < 0 || num > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) num;
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 32-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int32.MinValue" /> или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToInt32(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.StringToInt(value, fromBase, 4096);
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 32-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равняется 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt32.MinValue" /> или больше <see cref="F:System.UInt32.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return (uint) ParseNumbers.StringToInt(value, fromBase, 4608);
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 64-битовое целое число со знаком.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значению <paramref name="value" />, представляющему недесятичное число со знаком, предшествует отрицательный знак.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.Int64.MinValue" /> или больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long ToInt64(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.StringToLong(value, fromBase, 4096);
    }

    /// <summary>
    ///   Преобразует строковое представление числа с указанным основанием системы счисления в эквивалентное ему 64-битовое целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое число.
    /// </param>
    /// <param name="fromBase">
    ///   Основание системы счисления, используемой для представления числа, заданного в параметре <paramref name="value" />, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, которое эквивалентно значению параметра <paramref name="value" />, или 0 (нуль), если <paramref name="value" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fromBase" /> не равно 2, 8, 10 или 16.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="value" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> содержит символ, который не является допустимой цифрой в основании, заданном параметром <paramref name="fromBase" />.
    ///    Сообщение об исключении указывает, что отсутствуют цифры для преобразования, если первый символ в <paramref name="value" /> недопустим; в противном случае сообщение указывает, что <paramref name="value" /> содержит недопустимые конечные символы.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Значение <paramref name="value" />, представляющее недесятичное число без знака, использовано со знаком минус.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> представляет число меньше <see cref="F:System.UInt64.MinValue" /> или больше <see cref="F:System.UInt64.MaxValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return (ulong) ParseNumbers.StringToLong(value, fromBase, 4608);
    }

    /// <summary>
    ///   Преобразует значение заданного 8-битового целого числа без знака в эквивалентное строковое представление в указанной системе счисления.
    /// </summary>
    /// <param name="value">
    ///   8-разрядное целое число без знака для преобразования.
    /// </param>
    /// <param name="toBase">
    ///   Основание системы счисления возвращаемого значения, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="value" /> в системе счисления с основанием <paramref name="toBase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toBase" /> не равно 2, 8, 10 или 16.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(byte value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString((int) value, toBase, -1, ' ', 64);
    }

    /// <summary>
    ///   Преобразует значение заданного 16-битового целого числа со знаком в эквивалентное строковое представление в указанной системе счисления.
    /// </summary>
    /// <param name="value">
    ///   16-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="toBase">
    ///   Основание системы счисления возвращаемого значения, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="value" /> в системе счисления с основанием <paramref name="toBase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toBase" /> не равно 2, 8, 10 или 16.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(short value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString((int) value, toBase, -1, ' ', 128);
    }

    /// <summary>
    ///   Преобразует значение заданного 32-битового целого числа со знаком в эквивалентное строковое представление в указанной системе счисления.
    /// </summary>
    /// <param name="value">
    ///   32-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="toBase">
    ///   Основание системы счисления возвращаемого значения, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="value" /> в системе счисления с основанием <paramref name="toBase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toBase" /> не равно 2, 8, 10 или 16.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(int value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
    }

    /// <summary>
    ///   Преобразует значение заданного 64-битового целого числа со знаком в эквивалентное строковое представление в указанной системе счисления.
    /// </summary>
    /// <param name="value">
    ///   64-разрядное целое число со знаком для преобразования.
    /// </param>
    /// <param name="toBase">
    ///   Основание системы счисления возвращаемого значения, равное 2, 8, 10 или 16.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="value" /> в системе счисления с основанием <paramref name="toBase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toBase" /> не равно 2, 8, 10 или 16.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(long value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
    }

    /// <summary>
    ///   Преобразует массив 8-разрядных целых чисел без знака в эквивалентное строковое представление, состоящее из цифр в кодировке Base64.
    /// </summary>
    /// <param name="inArray">
    ///   Массив 8-битовых целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Строковое представление содержимого массива <paramref name="inArray" /> в кодировке Base64.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inArray" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ToBase64String(byte[] inArray)
    {
      if (inArray == null)
        throw new ArgumentNullException(nameof (inArray));
      return Convert.ToBase64String(inArray, 0, inArray.Length, Base64FormattingOptions.None);
    }

    /// <summary>
    ///   Преобразует массив 8-разрядных целых чисел без знака в эквивалентное строковое представление, состоящее из цифр в кодировке Base64.
    ///    Параметр указывает, следует ли вставлять в возвращаемое значение разрывы строки.
    /// </summary>
    /// <param name="inArray">
    ///   Массив 8-битовых целых чисел без знака.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> для вставки разрыва строки после каждых 76 знаков или <see cref="F:System.Base64FormattingOptions.None" />, чтобы разрывы строки не вставлялись.
    /// </param>
    /// <returns>
    ///   Строковое представление элементов массива <paramref name="inArray" /> в кодировке Base64.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является допустимым значением <see cref="T:System.Base64FormattingOptions" />.
    /// </exception>
    [ComVisible(false)]
    public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException(nameof (inArray));
      return Convert.ToBase64String(inArray, 0, inArray.Length, options);
    }

    /// <summary>
    ///   Преобразует подмножество массива 8-разрядных целых чисел без знака в эквивалентное строковое представление, состоящее из цифр в кодировке Base64.
    ///    В параметрах задается подмножество как смещение во входном массиве и количество преобразуемых элементов этого массива.
    /// </summary>
    /// <param name="inArray">
    ///   Массив 8-битовых целых чисел без знака.
    /// </param>
    /// <param name="offset">
    ///   Смещение в массиве <paramref name="inArray" />.
    /// </param>
    /// <param name="length">
    ///   Число преобразуемых элементов <paramref name="inArray" />.
    /// </param>
    /// <returns>
    ///   Строковое представление <paramref name="length" /> элементов массива <paramref name="inArray" /> в кодировке Base64, начиная с позиции <paramref name="offset" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений <paramref name="offset" /> и <paramref name="length" /> превышает длину массива <paramref name="inArray" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ToBase64String(byte[] inArray, int offset, int length)
    {
      return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
    }

    /// <summary>
    ///   Преобразует подмножество массива 8-разрядных целых чисел без знака в эквивалентное строковое представление, состоящее из цифр в кодировке Base64.
    ///    В параметрах задаются подмножество как смещение во входном массиве и количество преобразуемых элементов этого массива, а также значение, указывающее, следует ли вставлять в выходной массив разрывы строки.
    /// </summary>
    /// <param name="inArray">
    ///   Массив 8-битовых целых чисел без знака.
    /// </param>
    /// <param name="offset">
    ///   Смещение в массиве <paramref name="inArray" />.
    /// </param>
    /// <param name="length">
    ///   Число преобразуемых элементов <paramref name="inArray" />.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> для вставки разрыва строки после каждых 76 знаков или <see cref="F:System.Base64FormattingOptions.None" />, чтобы разрывы строки не вставлялись.
    /// </param>
    /// <returns>
    ///   Строковое представление <paramref name="length" /> элементов массива <paramref name="inArray" /> в кодировке Base64, начиная с позиции <paramref name="offset" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений <paramref name="offset" /> и <paramref name="length" /> превышает длину массива <paramref name="inArray" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является допустимым значением <see cref="T:System.Base64FormattingOptions" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static unsafe string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException(nameof (inArray));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      switch (options)
      {
        case Base64FormattingOptions.None:
        case Base64FormattingOptions.InsertLineBreaks:
          int length1 = inArray.Length;
          if (offset > length1 - length)
            throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
          if (length1 == 0)
            return string.Empty;
          bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
          string str1 = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks));
          string str2 = str1;
          char* outChars = (char*) str2;
          if ((IntPtr) outChars != IntPtr.Zero)
            outChars += RuntimeHelpers.OffsetToStringData;
          fixed (byte* inData = inArray)
          {
            Convert.ConvertToBase64Array(outChars, inData, offset, length, insertLineBreaks);
            return str1;
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      }
    }

    /// <summary>
    ///   Преобразует подмножество массива 8-разрядных целых чисел без знака в эквивалентное подмножество массива знаков Юникода, состоящее из цифр в кодировке Base64.
    ///    Подмножества задаются с помощью параметров как смещение во входном и выходном массивах и количеством преобразуемых элементов входного массива.
    /// </summary>
    /// <param name="inArray">
    ///   Входной массив 8-битовых целых чисел без знака.
    /// </param>
    /// <param name="offsetIn">
    ///   Позиция в массиве <paramref name="inArray" />.
    /// </param>
    /// <param name="length">
    ///   Число преобразуемых элементов <paramref name="inArray" />.
    /// </param>
    /// <param name="outArray">Выходной массив знаков Юникода.</param>
    /// <param name="offsetOut">
    ///   Позиция в массиве <paramref name="outArray" />.
    /// </param>
    /// <returns>
    ///   32-битовое целое число со знаком, представляющее число байтов в массиве <paramref name="outArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inArray" /> или <paramref name="outArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offsetIn" />, <paramref name="offsetOut" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений <paramref name="offsetIn" /> и <paramref name="length" /> превышает длину массива <paramref name="inArray" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offsetOut" /> плюс количество возвращаемых элементов превышает длину массива <paramref name="outArray" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
    {
      return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
    }

    /// <summary>
    ///   Преобразует подмножество массива 8-разрядных целых чисел без знака в эквивалентное подмножество массива знаков Юникода, состоящее из цифр в кодировке Base64.
    ///    В параметрах задаются подмножества как смещение во входном и выходном массивах и количество преобразуемых элементов входного массива, а также значение, указывающее, следует ли вставлять в выходной массив разрывы строки.
    /// </summary>
    /// <param name="inArray">
    ///   Входной массив 8-битовых целых чисел без знака.
    /// </param>
    /// <param name="offsetIn">
    ///   Позиция в массиве <paramref name="inArray" />.
    /// </param>
    /// <param name="length">
    ///   Число преобразуемых элементов <paramref name="inArray" />.
    /// </param>
    /// <param name="outArray">Выходной массив знаков Юникода.</param>
    /// <param name="offsetOut">
    ///   Позиция в массиве <paramref name="outArray" />.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> для вставки разрыва строки после каждых 76 знаков или <see cref="F:System.Base64FormattingOptions.None" />, чтобы разрывы строки не вставлялись.
    /// </param>
    /// <returns>
    ///   32-битовое целое число со знаком, представляющее число байтов в массиве <paramref name="outArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inArray" /> или <paramref name="outArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offsetIn" />, <paramref name="offsetOut" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений <paramref name="offsetIn" /> и <paramref name="length" /> превышает длину массива <paramref name="inArray" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offsetOut" /> плюс количество возвращаемых элементов превышает длину массива <paramref name="outArray" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является допустимым значением <see cref="T:System.Base64FormattingOptions" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static unsafe int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException(nameof (inArray));
      if (outArray == null)
        throw new ArgumentNullException(nameof (outArray));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offsetIn < 0)
        throw new ArgumentOutOfRangeException(nameof (offsetIn), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (offsetOut < 0)
        throw new ArgumentOutOfRangeException(nameof (offsetOut), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      switch (options)
      {
        case Base64FormattingOptions.None:
        case Base64FormattingOptions.InsertLineBreaks:
          int length1 = inArray.Length;
          if (offsetIn > length1 - length)
            throw new ArgumentOutOfRangeException(nameof (offsetIn), Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
          if (length1 == 0)
            return 0;
          bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
          int length2 = outArray.Length;
          int validateOutputLength = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
          if (offsetOut > length2 - validateOutputLength)
            throw new ArgumentOutOfRangeException(nameof (offsetOut), Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
          int base64Array;
          fixed (char* outChars = &outArray[offsetOut])
            fixed (byte* inData = inArray)
              base64Array = Convert.ConvertToBase64Array(outChars, inData, offsetIn, length, insertLineBreaks);
          return base64Array;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      }
    }

    [SecurityCritical]
    private static unsafe int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
    {
      int num1 = length % 3;
      int num2 = offset + (length - num1);
      int index1 = 0;
      int num3 = 0;
      fixed (char* chPtr1 = Convert.base64Table)
      {
        int index2 = offset;
        while (index2 < num2)
        {
          if (insertLineBreaks)
          {
            if (num3 == 76)
            {
              char* chPtr2 = outChars;
              int num4 = index1;
              int num5 = num4 + 1;
              IntPtr num6 = (IntPtr) num4 * 2;
              *(short*) ((IntPtr) chPtr2 + num6) = (short) 13;
              char* chPtr3 = outChars;
              int num7 = num5;
              index1 = num7 + 1;
              IntPtr num8 = (IntPtr) num7 * 2;
              *(short*) ((IntPtr) chPtr3 + num8) = (short) 10;
              num3 = 0;
            }
            num3 += 4;
          }
          outChars[index1] = chPtr1[((int) inData[index2] & 252) >> 2];
          outChars[index1 + 1] = chPtr1[((int) inData[index2] & 3) << 4 | ((int) inData[index2 + 1] & 240) >> 4];
          outChars[index1 + 2] = chPtr1[((int) inData[index2 + 1] & 15) << 2 | ((int) inData[index2 + 2] & 192) >> 6];
          outChars[index1 + 3] = chPtr1[(int) inData[index2 + 2] & 63];
          index1 += 4;
          index2 += 3;
        }
        int index3 = num2;
        if (insertLineBreaks && num1 != 0 && num3 == 76)
        {
          char* chPtr2 = outChars;
          int num4 = index1;
          int num5 = num4 + 1;
          IntPtr num6 = (IntPtr) num4 * 2;
          *(short*) ((IntPtr) chPtr2 + num6) = (short) 13;
          char* chPtr3 = outChars;
          int num7 = num5;
          index1 = num7 + 1;
          IntPtr num8 = (IntPtr) num7 * 2;
          *(short*) ((IntPtr) chPtr3 + num8) = (short) 10;
        }
        switch (num1)
        {
          case 1:
            outChars[index1] = chPtr1[((int) inData[index3] & 252) >> 2];
            outChars[index1 + 1] = chPtr1[((int) inData[index3] & 3) << 4];
            outChars[index1 + 2] = chPtr1[64];
            outChars[index1 + 3] = chPtr1[64];
            index1 += 4;
            break;
          case 2:
            outChars[index1] = chPtr1[((int) inData[index3] & 252) >> 2];
            outChars[index1 + 1] = chPtr1[((int) inData[index3] & 3) << 4 | ((int) inData[index3 + 1] & 240) >> 4];
            outChars[index1 + 2] = chPtr1[((int) inData[index3 + 1] & 15) << 2];
            outChars[index1 + 3] = chPtr1[64];
            index1 += 4;
            break;
        }
      }
      return index1;
    }

    private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
    {
      long num1 = (long) inputLength / 3L * 4L + (inputLength % 3 != 0 ? 4L : 0L);
      if (num1 == 0L)
        return 0;
      if (insertLineBreaks)
      {
        long num2 = num1 / 76L;
        if (num1 % 76L == 0L)
          --num2;
        num1 += num2 * 2L;
      }
      if (num1 > (long) int.MaxValue)
        throw new OutOfMemoryException();
      return (int) num1;
    }

    /// <summary>
    ///   Преобразует заданную строку, представляющую двоичные данные в виде цифр в кодировке Base64, в эквивалентный массив 8-разрядных целых чисел без знака.
    /// </summary>
    /// <param name="s">Преобразуемая строка.</param>
    /// <returns>
    ///   Массив 8-разрядных целых чисел без знака, эквивалентный <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="s" />, не учитывая символы пробелов, не равна нулю и не кратна 4.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый формат <paramref name="s" />.
    ///   <paramref name="s" /> содержит символ в кодировке, отличной от Base 64, больше двух символов заполнения или символ, не являющийся пробелом, среди символов заполнения.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] FromBase64String(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      string str = s;
      char* inputPtr = (char*) str;
      if ((IntPtr) inputPtr != IntPtr.Zero)
        inputPtr += RuntimeHelpers.OffsetToStringData;
      return Convert.FromBase64CharPtr(inputPtr, s.Length);
    }

    /// <summary>
    ///   Преобразует подмножество массива знаков Юникода, представляющих двоичные данные в виде цифр в кодировке Base64, в эквивалентный массив 8-разрядных целых чисел без знака.
    ///    Параметры задают подмножество входного массива и количество преобразуемых элементов.
    /// </summary>
    /// <param name="inArray">Массив знаков Юникода.</param>
    /// <param name="offset">
    ///   Позиция в массиве <paramref name="inArray" />.
    /// </param>
    /// <param name="length">
    ///   Число преобразуемых элементов массива <paramref name="inArray" />.
    /// </param>
    /// <returns>
    ///   Массив 8-битовых целых чисел без знака, эквивалентный <paramref name="length" /> элементам с позиции <paramref name="offset" /> в массиве <paramref name="inArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="offset" /> или <paramref name="length" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" /> плюс <paramref name="length" /> указывает на позицию за пределами <paramref name="inArray" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="inArray" />, не учитывая символы пробелов, не равна нулю и не кратна 4.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый формат <paramref name="inArray" />.
    ///   <paramref name="inArray" /> содержит символ в кодировке, отличной от Base 64, больше двух символов заполнения или символ, не являющийся пробелом, среди символов заполнения.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] FromBase64CharArray(char[] inArray, int offset, int length)
    {
      if (inArray == null)
        throw new ArgumentNullException(nameof (inArray));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (offset > inArray.Length - length)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      fixed (char* chPtr = inArray)
        return Convert.FromBase64CharPtr(chPtr + offset, length);
    }

    [SecurityCritical]
    private static unsafe byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
    {
      for (; inputLength > 0; --inputLength)
      {
        switch (inputPtr[inputLength - 1])
        {
          case '\t':
          case '\n':
          case '\r':
          case ' ':
            continue;
          default:
            goto label_4;
        }
      }
label_4:
      int resultLength = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
      byte[] numArray = new byte[resultLength];
      fixed (byte* startDestPtr = numArray)
        Convert.FromBase64_Decode(inputPtr, inputLength, startDestPtr, resultLength);
      return numArray;
    }

    [SecurityCritical]
    private static unsafe int FromBase64_Decode(char* startInputPtr, int inputLength, byte* startDestPtr, int destLength)
    {
      char* chPtr1 = startInputPtr;
      byte* numPtr1 = startDestPtr;
      char* chPtr2 = chPtr1 + inputLength;
      byte* numPtr2 = numPtr1 + destLength;
      uint num1 = (uint) byte.MaxValue;
      while (chPtr1 < chPtr2)
      {
        uint num2 = (uint) *chPtr1;
        chPtr1 += 2;
        uint num3;
        if (num2 - 65U <= 25U)
          num3 = num2 - 65U;
        else if (num2 - 97U <= 25U)
          num3 = num2 - 71U;
        else if (num2 - 48U <= 9U)
        {
          num3 = num2 - 4294967292U;
        }
        else
        {
          switch (num2)
          {
            case 9:
            case 10:
            case 13:
            case 32:
              continue;
            case 43:
              num3 = 62U;
              break;
            case 47:
              num3 = 63U;
              break;
            case 61:
              if (chPtr1 == chPtr2)
              {
                uint num4 = num1 << 6;
                if (((int) num4 & int.MinValue) == 0)
                  throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
                if ((int) (numPtr2 - numPtr1) < 2)
                  return -1;
                byte* numPtr3 = numPtr1;
                int num5 = 1;
                byte* numPtr4 = numPtr3 + num5;
                int num6 = (int) (byte) (num4 >> 16);
                *numPtr3 = (byte) num6;
                byte* numPtr5 = numPtr4;
                int num7 = 1;
                numPtr1 = numPtr5 + num7;
                int num8 = (int) (byte) (num4 >> 8);
                *numPtr5 = (byte) num8;
                num1 = (uint) byte.MaxValue;
                goto label_32;
              }
              else
              {
                while ((UIntPtr) chPtr1 < (UIntPtr) chPtr2 - new UIntPtr(2))
                {
                  switch (*chPtr1)
                  {
                    case '\t':
                    case '\n':
                    case '\r':
                    case ' ':
                      chPtr1 += 2;
                      continue;
                    default:
                      goto label_25;
                  }
                }
label_25:
                if ((IntPtr) chPtr1 != (IntPtr) chPtr2 - 2 || *chPtr1 != '=')
                  throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
                uint num4 = num1 << 12;
                if (((int) num4 & int.MinValue) == 0)
                  throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
                if ((int) (numPtr2 - numPtr1) < 1)
                  return -1;
                *numPtr1++ = (byte) (num4 >> 16);
                num1 = (uint) byte.MaxValue;
                goto label_32;
              }
            default:
              throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
          }
        }
        num1 = num1 << 6 | num3;
        if (((int) num1 & int.MinValue) != 0)
        {
          if ((int) (numPtr2 - numPtr1) < 3)
            return -1;
          *numPtr1 = (byte) (num1 >> 16);
          numPtr1[1] = (byte) (num1 >> 8);
          numPtr1[2] = (byte) num1;
          numPtr1 += 3;
          num1 = (uint) byte.MaxValue;
        }
      }
label_32:
      if (num1 != (uint) byte.MaxValue)
        throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
      return (int) (numPtr1 - startDestPtr);
    }

    [SecurityCritical]
    private static unsafe int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
    {
      char* chPtr = inputPtr + inputLength;
      int num1 = inputLength;
      int num2 = 0;
      while (inputPtr < chPtr)
      {
        uint num3 = (uint) *inputPtr;
        inputPtr += 2;
        if (num3 <= 32U)
          --num1;
        else if (num3 == 61U)
        {
          --num1;
          ++num2;
        }
      }
      switch (num2)
      {
        case 0:
          return num1 / 4 * 3 + num2;
        case 1:
          num2 = 2;
          goto case 0;
        case 2:
          num2 = 1;
          goto case 0;
        default:
          throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
      }
    }
  }
}
