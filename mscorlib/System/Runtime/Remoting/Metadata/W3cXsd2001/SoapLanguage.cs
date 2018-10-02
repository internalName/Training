// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Упаковывает XML <see langword="language" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapLanguage : ISoapXsd
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
        return "language";
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
      return SoapLanguage.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" />.
    /// </summary>
    public SoapLanguage()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> класса со значением идентификатора языка <see langword="language" /> атрибут.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий значение идентификатора языка <see langword="language" /> атрибута.
    /// </param>
    public SoapLanguage(string value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает или задает идентификатор языка <see langword="language" /> атрибута.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий идентификатор языка <see langword="language" /> атрибута.
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> объект, который получен из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" />.
    /// </returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapLanguage Parse(string value)
    {
      return new SoapLanguage(value);
    }
  }
}
