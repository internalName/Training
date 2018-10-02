// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Упаковывает XML <see langword="normalizedString" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNormalizedString : ISoapXsd
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
        return "normalizedString";
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
      return SoapNormalizedString.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" />.
    /// </summary>
    public SoapNormalizedString()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> класса нормализованную строку.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий нормализованную строку.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> содержит недопустимые символы (0xD, 0xA или 0x9).
    /// </exception>
    public SoapNormalizedString(string value)
    {
      this._value = this.Validate(value);
    }

    /// <summary>Получает или устанавливает нормализованную строку.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий нормализованную строку.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> содержит недопустимые символы (0xD, 0xA или 0x9).
    /// </exception>
    public string Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = this.Validate(value);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> в формате «&lt;! [ CDATA [» + <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> + «]] &gt;».
    /// </returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> содержит недопустимые символы (0xD, 0xA или 0x9).
    /// </exception>
    public static SoapNormalizedString Parse(string value)
    {
      return new SoapNormalizedString(value);
    }

    private string Validate(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      char[] anyOf = new char[3]{ '\r', '\n', '\t' };
      if (value.LastIndexOfAny(anyOf) > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:normalizedString", (object) value));
      return value;
    }
  }
}
