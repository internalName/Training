// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FormatterConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Представляет базовую реализацию <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> интерфейса, который использует <see cref="T:System.Convert" /> класса и <see cref="T:System.IConvertible" /> интерфейса.
  /// </summary>
  [ComVisible(true)]
  public class FormatterConverter : IFormatterConverter
  {
    /// <summary>
    ///   Преобразует значение заданного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Куда <paramref name="value" /> преобразуется.
    /// </param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public object Convert(object value, Type type)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение заданного <see cref="T:System.TypeCode" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <param name="typeCode">
    ///   <see cref="T:System.TypeCode" /> Куда <paramref name="value" /> преобразуется.
    /// </param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />, или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public object Convert(object value, TypeCode typeCode)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ChangeType(value, typeCode, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.Boolean" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool ToBoolean(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToBoolean(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>Преобразует значение в символ Юникода.</summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public char ToChar(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToChar(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.SByte" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    public sbyte ToSByte(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToSByte(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public byte ToByte(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToByte(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public short ToInt16(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToInt16(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    public ushort ToUInt16(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToUInt16(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public int ToInt32(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    public uint ToUInt32(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToUInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public long ToInt64(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    public ulong ToUInt64(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToUInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public float ToSingle(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToSingle(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public double ToDouble(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Decimal ToDecimal(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToDecimal(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public DateTime ToDateTime(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный объект в <see cref="T:System.String" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" /> или <see langword="null" /> Если <paramref name="type" /> параметр <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public string ToString(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
