// Decompiled with JetBrains decompiler
// Type: System.DBNull
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет несуществующее значение.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class DBNull : ISerializable, IConvertible
  {
    /// <summary>
    ///   Представляет единственный экземпляр <see cref="T:System.DBNull" /> класса.
    /// </summary>
    public static readonly DBNull Value = new DBNull();

    private DBNull()
    {
    }

    private DBNull(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DBNullSerial"));
    }

    /// <summary>
    ///   Реализует <see cref="T:System.Runtime.Serialization.ISerializable" /> интерфейс и возвращает данные, необходимые для сериализации <see cref="T:System.DBNull" /> объекта.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект, содержащий сведения, необходимые для сериализации <see cref="T:System.DBNull" /> объекта.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" /> объект, содержащий источник и назначение сериализованного потока, связанного с <see cref="T:System.DBNull" /> объекта.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      UnitySerializationHolder.GetUnitySerializationInfo(info, 2, (string) null, (RuntimeAssembly) null);
    }

    /// <summary>
    ///   Возвращает пустую строку (<see cref="F:System.String.Empty" />).
    /// </summary>
    /// <returns>
    ///   Пустая строка (<see cref="F:System.String.Empty" />).
    /// </returns>
    public override string ToString()
    {
      return string.Empty;
    }

    /// <summary>
    ///   Возвращает пустую строку, используя указанный <see cref="T:System.IFormatProvider" />.
    /// </summary>
    /// <param name="provider">
    ///   <see cref="T:System.IFormatProvider" /> Для использования формат возвращаемого значения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Для получения сведений о форматировании из текущих локальных настроек операционной системы.
    /// </param>
    /// <returns>
    ///   Пустая строка (<see cref="F:System.String.Empty" />).
    /// </returns>
    public string ToString(IFormatProvider provider)
    {
      return string.Empty;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> значение <see cref="T:System.DBNull" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.TypeCode" /> Значение <see cref="T:System.DBNull" />, который является <see cref="F:System.TypeCode.DBNull" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.DBNull;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
