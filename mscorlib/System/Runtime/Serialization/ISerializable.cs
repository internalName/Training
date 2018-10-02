// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISerializable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Позволяет объекту управлять его собственной сериализации и десериализации.
  /// </summary>
  [ComVisible(true)]
  public interface ISerializable
  {
    /// <summary>
    ///   Заполняет объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для сериализации целевого объекта.
    /// </summary>
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
    void GetObjectData(SerializationInfo info, StreamingContext context);
  }
}
