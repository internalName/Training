// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="QName" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapQName : ISoapXsd
  {
    private string _name;
    private string _namespace;
    private string _key;

    /// <summary>
    ///   Возвращает язык определения схемы XML (XSD) текущего типа SOAP.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.String" /> указывает XSD текущего типа SOAP.
    /// </returns>
    public static string XsdType
    {
      get
      {
        return "QName";
      }
    }

    /// <summary>
    ///   Возвращает язык определения схемы XML (XSD) текущего типа SOAP.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> указывающее, XSD текущего типа SOAP.
    /// </returns>
    public string GetXsdType()
    {
      return SoapQName.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" />.
    /// </summary>
    public SoapQName()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> локальную часть имени класса.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий локальную часть имени.
    /// </param>
    public SoapQName(string value)
    {
      this._name = value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> псевдоним пространства имен и локальной части имени класса.
    /// </summary>
    /// <param name="key">
    ///   Объект <see cref="T:System.String" /> содержащий псевдоним пространства имен полного имени.
    /// </param>
    /// <param name="name">
    ///   Объект <see cref="T:System.String" /> содержащий локальную часть имени.
    /// </param>
    public SoapQName(string key, string name)
    {
      this._name = name;
      this._key = key;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> класс пространством имен псевдоним, локальную часть полного имени и пространства имен, на который ссылается псевдоним.
    /// </summary>
    /// <param name="key">
    ///   Объект <see cref="T:System.String" /> содержащий псевдоним пространства имен полного имени.
    /// </param>
    /// <param name="name">
    ///   Объект <see cref="T:System.String" /> содержащий локальную часть имени.
    /// </param>
    /// <param name="namespaceValue">
    ///   Объект <see cref="T:System.String" /> содержащий пространство имен, на который ссылается <paramref name="key" />.
    /// </param>
    public SoapQName(string key, string name, string namespaceValue)
    {
      this._name = name;
      this._namespace = namespaceValue;
      this._key = key;
    }

    /// <summary>Возвращает или задает часть полного имени.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий часть полного имени.
    /// </returns>
    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает пространство имен, на который ссылается <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий пространство имен, на который ссылается <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.
    /// </returns>
    public string Namespace
    {
      get
      {
        return this._namespace;
      }
      set
      {
        this._namespace = value;
      }
    }

    /// <summary>
    ///   Получает или задает псевдоним пространства имен полного имени.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий псевдоним пространства имен полного имени.
    /// </returns>
    public string Key
    {
      get
      {
        return this._key;
      }
      set
      {
        this._key = value;
      }
    }

    /// <summary>
    ///   Возвращает полное имя как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.String" /> в формате « <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" /> ».
    ///    Если <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> не указан, этот метод возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />.
    /// </returns>
    public override string ToString()
    {
      if (this._key == null || this._key.Length == 0)
        return this._name;
      return this._key + ":" + this._name;
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapQName Parse(string value)
    {
      if (value == null)
        return new SoapQName();
      string key = "";
      string name = value;
      int length = value.IndexOf(':');
      if (length > 0)
      {
        key = value.Substring(0, length);
        name = value.Substring(length + 1);
      }
      return new SoapQName(key, name);
    }
  }
}
