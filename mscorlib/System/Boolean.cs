// Decompiled with JetBrains decompiler
// Type: System.Boolean
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
  /// <summary>
  ///   Представляет логическое значение (<see langword="true" /> или <see langword="false" />).
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
  {
    /// <summary>
    ///   Представляет логическое значение <see langword="true" /> в виде строки.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly string TrueString = nameof (True);
    /// <summary>
    ///   Представляет логическое значение <see langword="false" /> в виде строки.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly string FalseString = nameof (False);
    private bool m_value;
    internal const int True = 1;
    internal const int False = 0;
    internal const string TrueLiteral = "True";
    internal const string FalseLiteral = "False";

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Boolean" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return !this ? 0 : 1;
    }

    /// <summary>
    ///   Преобразовывает значение этого экземпляра в эквивалентное ему строковое представление ("True" или "False").
    /// </summary>
    /// <returns>
    ///   "True" (значение свойства <see cref="F:System.Boolean.TrueString" />), если значение этого экземпляра равно <see langword="true" />, или "False" (значение свойства <see cref="F:System.Boolean.FalseString" />), если значение этого экземпляра равно <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return !this ? "False" : "True";
    }

    /// <summary>
    ///   Преобразовывает значение этого экземпляра в эквивалентное ему строковое представление ("True" или "False").
    /// </summary>
    /// <param name="provider">
    ///   (Зарезервирован.) Объект <see cref="T:System.IFormatProvider" />.
    /// </param>
    /// <returns>
    ///   <see cref="F:System.Boolean.TrueString" />, если значением данного экземпляра является <see langword="true" />, или <see cref="F:System.Boolean.FalseString" />, если его значением является <see langword="false" />.
    /// </returns>
    public string ToString(IFormatProvider provider)
    {
      return !this ? "False" : "True";
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="obj" /> имеет тип <see cref="T:System.Boolean" /> и содержит то же значение, что и данный экземпляр; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is bool))
        return false;
      return this == (bool) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли данный экземпляр заданному объекту <see cref="T:System.Boolean" />.
    /// </summary>
    /// <param name="obj">
    ///   Значение типа <see cref="T:System.Boolean" /> для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(bool obj)
    {
      return this == obj;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным объектом и возвращает целое число, которое показывает их связь друг с другом.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или <see langword="null" />.
    /// </param>
    /// <returns>
    /// Целое число со знаком, показывающее относительный порядок данного экземпляра и <paramref name="obj" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр содержит значение <see langword="false" />, а параметр <paramref name="obj" /> — значение <see langword="true" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр и параметр <paramref name="obj" /> равны (оба равны значению <see langword="true" /> или <see langword="false" />).
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр содержит значение <see langword="true" />, а параметр <paramref name="obj" /> — значение <see langword="false" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> не является объектом <see cref="T:System.Boolean" />.
    /// </exception>
    public int CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      if (!(obj is bool))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeBoolean"));
      if (this == (bool) obj)
        return 0;
      return !this ? -1 : 1;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным объектом <see cref="T:System.Boolean" /> и возвращает целое число, которое показывает их связь друг с другом.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Boolean" />, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    /// Целое число со знаком, представляющее результат сравнения значений этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр содержит значение <see langword="false" />, а параметр <paramref name="value" /> — значение <see langword="true" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр и параметр <paramref name="value" /> равны (оба равны значению <see langword="true" /> или <see langword="false" />).
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр содержит значение <see langword="true" />, а параметр <paramref name="value" /> — значение <see langword="false" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(bool value)
    {
      if (this == value)
        return 0;
      return !this ? -1 : 1;
    }

    /// <summary>
    ///   Преобразует заданное строковое представление логического значения в эквивалентное значение <see cref="T:System.Boolean" />.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое значение.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> эквивалентно <see cref="F:System.Boolean.TrueString" />. Значение <see langword="false" />, если <paramref name="value" /> эквивалентно <see cref="F:System.Boolean.FalseString" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="value" /> не эквивалентно ни <see cref="F:System.Boolean.TrueString" />, ни <see cref="F:System.Boolean.FalseString" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool Parse(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      bool result = false;
      if (!bool.TryParse(value, out result))
        throw new FormatException(Environment.GetResourceString("Format_BadBoolean"));
      return result;
    }

    /// <summary>
    ///   Пытается преобразовать заданное строковое представление логического значения в его эквивалент типа <see cref="T:System.Boolean" />.
    ///    Возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая преобразуемое значение.
    /// </param>
    /// <param name="result">
    ///   Если после возврата из этого метода преобразование выполнено успешно, содержит <see langword="true" />, если значение параметра <paramref name="value" /> равно <see cref="F:System.Boolean.TrueString" /> или <see langword="false" />, если значение параметра <paramref name="value" /> равно <see cref="F:System.Boolean.FalseString" />.
    ///    Если преобразование завершилось неудачей, содержит <see langword="false" />.
    ///    Преобразование завершается неудачей, если значение параметра <paramref name="value" /> равно <see langword="null" /> или не равно значению в поле <see cref="F:System.Boolean.TrueString" /> или <see cref="F:System.Boolean.FalseString" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string value, out bool result)
    {
      result = false;
      if (value == null)
        return false;
      if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = true;
        return true;
      }
      if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = false;
        return true;
      }
      value = bool.TrimWhiteSpaceAndNull(value);
      if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = true;
        return true;
      }
      if (!"False".Equals(value, StringComparison.OrdinalIgnoreCase))
        return false;
      result = false;
      return true;
    }

    private static string TrimWhiteSpaceAndNull(string value)
    {
      int startIndex = 0;
      int index = value.Length - 1;
      char minValue = char.MinValue;
      while (startIndex < value.Length && (char.IsWhiteSpace(value[startIndex]) || (int) value[startIndex] == (int) minValue))
        ++startIndex;
      while (index >= startIndex && (char.IsWhiteSpace(value[index]) || (int) value[index] == (int) minValue))
        --index;
      return value.Substring(startIndex, index - startIndex + 1);
    }

    /// <summary>
    ///   Возвращает код типа для типа значения <see cref="T:System.Boolean" />.
    /// </summary>
    /// <returns>
    ///   Перечислимая константа типа <see cref="F:System.TypeCode.Boolean" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Boolean;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Boolean), (object) "Char"));
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
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Boolean), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
