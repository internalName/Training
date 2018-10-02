// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IFormatterConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Обеспечивает связь между экземпляром <see cref="T:System.Runtime.Serialization.SerializationInfo" /> и предоставленным форматером классом, наиболее подходящим для анализа данных внутри <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
  /// </summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface IFormatterConverter
  {
    /// <summary>
    ///   Преобразует значение заданного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Куда <paramref name="value" /> должен быть преобразован.
    /// </param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    object Convert(object value, Type type);

    /// <summary>
    ///   Преобразует значение заданного <see cref="T:System.TypeCode" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <param name="typeCode">
    ///   <see cref="T:System.TypeCode" /> Куда <paramref name="value" /> должен быть преобразован.
    /// </param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    object Convert(object value, TypeCode typeCode);

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.Boolean" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    bool ToBoolean(object value);

    /// <summary>Преобразует значение в символ Юникода.</summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    char ToChar(object value);

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.SByte" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    sbyte ToSByte(object value);

    /// <summary>
    ///   Преобразует значение в 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    byte ToByte(object value);

    /// <summary>
    ///   Преобразует значение в 16-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    short ToInt16(object value);

    /// <summary>
    ///   Преобразует значение в 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    ushort ToUInt16(object value);

    /// <summary>
    ///   Преобразует значение в 32-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    int ToInt32(object value);

    /// <summary>
    ///   Преобразует значение в 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    uint ToUInt32(object value);

    /// <summary>
    ///   Преобразует значение в 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    long ToInt64(object value);

    /// <summary>
    ///   Преобразует значение в 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    ulong ToUInt64(object value);

    /// <summary>
    ///   Преобразует значение с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    float ToSingle(object value);

    /// <summary>
    ///   Преобразует значение с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    double ToDouble(object value);

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    Decimal ToDecimal(object value);

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    DateTime ToDateTime(object value);

    /// <summary>
    ///   Преобразует значение в <see cref="T:System.String" />.
    /// </summary>
    /// <param name="value">Преобразуемый объект.</param>
    /// <returns>
    ///   Преобразованный <paramref name="value" />.
    /// </returns>
    string ToString(object value);
  }
}
