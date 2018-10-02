// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="anyURI" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapAnyUri : ISoapXsd
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
        return "anyURI";
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
      return SoapAnyUri.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" />.
    /// </summary>
    public SoapAnyUri()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> с заданным универсальным кодом ресурса.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.String" /> содержащий URI.
    /// </param>
    public SoapAnyUri(string value)
    {
      this._value = value;
    }

    /// <summary>Возвращает или задает URI.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий URI.
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />.
    /// </returns>
    public override string ToString()
    {
      return this._value;
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapAnyUri Parse(string value)
    {
      return new SoapAnyUri(value);
    }
  }
}
