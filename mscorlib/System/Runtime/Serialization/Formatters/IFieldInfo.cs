// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.IFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Разрешает доступ к имена полей и типы полей объектов, поддерживающих <see cref="T:System.Runtime.Serialization.ISerializable" /> интерфейса.
  /// </summary>
  [ComVisible(true)]
  public interface IFieldInfo
  {
    /// <summary>
    ///   Возвращает или задает имена полей сериализованных объектов.
    /// </summary>
    /// <returns>Имена полей сериализованных объектов.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    string[] FieldNames { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>
    ///   Возвращает или задает типы полей сериализованных объектов.
    /// </summary>
    /// <returns>Типы полей сериализованных объектов.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    Type[] FieldTypes { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
