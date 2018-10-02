// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="nonNegativeInteger" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNonNegativeInteger : ISoapXsd
  {
    private Decimal _value;

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
        return "nonNegativeInteger";
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
      return SoapNonNegativeInteger.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" />.
    /// </summary>
    public SoapNonNegativeInteger()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> класса <see cref="T:System.Decimal" /> значение.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Decimal" /> значение для инициализации текущего экземпляра.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Значение параметра <paramref name="value" /> меньше 0.
    /// </exception>
    public SoapNonNegativeInteger(Decimal value)
    {
      this._value = Decimal.Truncate(value);
      if (this._value < Decimal.Zero)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:nonNegativeInteger", (object) value));
    }

    /// <summary>
    ///   Возвращает или задает числовое значение текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Decimal" /> Указывает числовое значение текущего экземпляра.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Значение параметра <paramref name="value" /> меньше 0.
    /// </exception>
    public Decimal Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = Decimal.Truncate(value);
        if (this._value < Decimal.Zero)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:nonNegativeInteger", (object) value));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" />.
    /// </returns>
    public override string ToString()
    {
      return this._value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.String" />  Для преобразования.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapNonNegativeInteger Parse(string value)
    {
      return new SoapNonNegativeInteger(Decimal.Parse(value, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture));
    }
  }
}
