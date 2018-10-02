// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISerializationSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Реализует селектор суррогата сериализации, который позволяет одному объекту выполнять сериализацию и десериализацию другого.
  /// </summary>
  [ComVisible(true)]
  public interface ISerializationSurrogate
  {
    /// <summary>
    ///   Заполняет предоставленный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для сериализации объекта.
    /// </summary>
    /// <param name="obj">Объект для сериализации.</param>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> для заполнения данными.
    /// </param>
    /// <param name="context">
    ///   Конечный объект (см. <see cref="T:System.Runtime.Serialization.StreamingContext" />) для этой сериализации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

    /// <summary>
    ///   Заносит в объект, используя сведения в <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="obj">Объект для заполнения.</param>
    /// <param name="info">Сведения для заполнения объекта.</param>
    /// <param name="context">
    ///   Источник, из которого десериализуется объект.
    /// </param>
    /// <param name="selector">
    ///   Селектор суррогата, с которой начинается поиск совместимый суррогат.
    /// </param>
    /// <returns>Заполняет десериализованный объект.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
  }
}
