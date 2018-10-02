// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Упаковывает XML <see langword="token" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapToken : ISoapXsd
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
        return "token";
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
      return SoapToken.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" />.
    /// </summary>
    public SoapToken()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> класса с XML- <see langword="token" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий XML <see langword="token" />.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// Один из следующих вариантов:
    /// 
    ///     <paramref name="value" /> содержит недопустимые символы (0xD или 0x9).
    /// 
    ///     <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />. Length - 1] содержит пробел.
    /// 
    ///     <paramref name="value" /> содержит пробелы.
    ///   </exception>
    public SoapToken(string value)
    {
      this._value = this.Validate(value);
    }

    /// <summary>
    ///   Возвращает или задает XML <see langword="token" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий XML <see langword="token" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Одно из следующих: <paramref name="value" /> содержит недопустимые символы (0xD или 0x9).
    /// 
    ///   <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />. Length - 1] содержит пробел.
    /// 
    ///   <paramref name="value" /> содержит пробелы.
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />.
    /// </returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapToken Parse(string value)
    {
      return new SoapToken(value);
    }

    private string Validate(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      char[] anyOf = new char[2]{ '\r', '\t' };
      if (value.LastIndexOfAny(anyOf) > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])))
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      if (value.IndexOf("  ") > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      return value;
    }
  }
}
