// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="negativeInteger" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNegativeInteger : ISoapXsd
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
        return "negativeInteger";
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
      return SoapNegativeInteger.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" />.
    /// </summary>
    public SoapNegativeInteger()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> класса <see cref="T:System.Decimal" /> значение.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Decimal" /> значение для инициализации текущего экземпляра.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> больше -1.
    /// </exception>
    public SoapNegativeInteger(Decimal value)
    {
      this._value = Decimal.Truncate(value);
      if (value > Decimal.MinusOne)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:negativeInteger", (object) value));
    }

    /// <summary>
    ///   Возвращает или задает числовое значение текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Decimal" /> Указывает числовое значение текущего экземпляра.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> больше -1.
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
        if (this._value > Decimal.MinusOne)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:negativeInteger", (object) value));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see langword="Value" />.
    /// </returns>
    public override string ToString()
    {
      return this._value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapNegativeInteger Parse(string value)
    {
      return new SoapNegativeInteger(Decimal.Parse(value, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture));
    }
  }
}
