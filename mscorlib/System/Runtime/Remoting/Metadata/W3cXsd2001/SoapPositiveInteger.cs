﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="positiveInteger" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapPositiveInteger : ISoapXsd
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
        return "positiveInteger";
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
      return SoapPositiveInteger.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" />.
    /// </summary>
    public SoapPositiveInteger()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> класса <see cref="T:System.Decimal" /> значение.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Decimal" /> значение для инициализации текущего экземпляра.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Значение <paramref name="value" /> меньше 1.
    /// </exception>
    public SoapPositiveInteger(Decimal value)
    {
      this._value = Decimal.Truncate(value);
      if (this._value < Decimal.One)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:positiveInteger", (object) value));
    }

    /// <summary>
    ///   Возвращает или задает числовое значение текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Decimal" /> указывающий числовое значение текущего экземпляра.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Значение параметра <paramref name="value" /> меньше 1.
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
        if (this._value < Decimal.One)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:positiveInteger", (object) value));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" />.
    /// </returns>
    public override string ToString()
    {
      return this._value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapPositiveInteger Parse(string value)
    {
      return new SoapPositiveInteger(Decimal.Parse(value, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture));
    }
  }
}
