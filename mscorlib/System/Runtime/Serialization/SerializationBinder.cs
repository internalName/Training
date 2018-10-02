// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationBinder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Позволяет пользователям управлять загрузкой классов и выбирать класс для загрузки.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class SerializationBinder
  {
    /// <summary>
    ///   При переопределении в производном классе управляет привязкой сериализованного объекта к типу.
    /// </summary>
    /// <param name="serializedType">
    ///   Тип объекта, экземпляр которого создается модулем форматирования.
    /// </param>
    /// <param name="assemblyName">
    ///   Указывает имя <see cref="T:System.Reflection.Assembly" /> сериализованного объекта.
    /// </param>
    /// <param name="typeName">
    ///   Указывает имя <see cref="T:System.Type" /> сериализованного объекта.
    /// </param>
    public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
      assemblyName = (string) null;
      typeName = (string) null;
    }

    /// <summary>
    ///   При переопределении в производном классе управляет привязкой сериализованного объекта к типу.
    /// </summary>
    /// <param name="assemblyName">
    ///   Указывает имя <see cref="T:System.Reflection.Assembly" /> сериализованного объекта.
    /// </param>
    /// <param name="typeName">
    ///   Указывает имя <see cref="T:System.Type" /> сериализованного объекта.
    /// </param>
    /// <returns>
    ///   Тип объекта, экземпляр которого создается модулем форматирования.
    /// </returns>
    public abstract Type BindToType(string assemblyName, string typeName);
  }
}
