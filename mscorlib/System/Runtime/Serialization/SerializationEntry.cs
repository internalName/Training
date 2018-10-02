// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Содержит значение, <see cref="T:System.Type" />, и имя сериализованного объекта.
  /// </summary>
  [ComVisible(true)]
  public struct SerializationEntry
  {
    private Type m_type;
    private object m_value;
    private string m_name;

    /// <summary>Возвращает значение, содержащееся в объекте.</summary>
    /// <returns>Значение, содержащееся в объекте.</returns>
    public object Value
    {
      get
      {
        return this.m_value;
      }
    }

    /// <summary>Возвращает имя объекта.</summary>
    /// <returns>Имя объекта.</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> объекта.
    /// </summary>
    /// <returns>
    ///   Ключ сущности <see cref="T:System.Type" /> объекта.
    /// </returns>
    public Type ObjectType
    {
      get
      {
        return this.m_type;
      }
    }

    internal SerializationEntry(string entryName, object entryValue, Type entryType)
    {
      this.m_value = entryValue;
      this.m_name = entryName;
      this.m_type = entryType;
    }
  }
}
