// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Упаковывает XML <see langword="IDREFS" /> атрибута.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapIdref : ISoapXsd
  {
    private string _value;

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
        return "IDREF";
      }
    }

    /// <summary>
    ///   Возвращает язык определения схемы XML (XSD) текущего типа SOAP.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.String" /> указывает XSD текущего типа SOAP.
    /// </returns>
    public string GetXsdType()
    {
      return SoapIdref.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" />.
    /// </summary>
    public SoapIdref()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> класса с XML- <see langword="IDREF" /> атрибута.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий XML <see langword="IDREF" /> атрибута.
    /// </param>
    public SoapIdref(string value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает или задает XML <see langword="IDREF" /> атрибута.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий XML <see langword="IDREF" /> атрибута.
    /// </returns>
    public string Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.
    /// </returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.String" /> получен из <paramref name="value" />.
    /// </returns>
    public static SoapIdref Parse(string value)
    {
      return new SoapIdref(value);
    }
  }
}
