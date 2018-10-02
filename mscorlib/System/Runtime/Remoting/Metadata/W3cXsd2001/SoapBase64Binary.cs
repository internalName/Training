// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="base64Binary" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapBase64Binary : ISoapXsd
  {
    private byte[] _value;

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
        return "base64Binary";
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
      return SoapBase64Binary.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" />.
    /// </summary>
    public SoapBase64Binary()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> класса двоичное представление 64-разрядного числа.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Byte" /> массив, содержащий 64-разрядное число.
    /// </param>
    public SoapBase64Binary(byte[] value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает или задает двоичное представление 64-разрядного числа.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Byte" /> массив, содержащий двоичное представление 64-разрядного числа.
    /// </returns>
    public byte[] Value
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />.
    /// </returns>
    public override string ToString()
    {
      if (this._value == null)
        return (string) null;
      return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// Один из следующих вариантов:
    /// 
    ///     Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// 
    ///     Длина <paramref name="value" /> меньше 4.
    /// 
    ///     Длина <paramref name="value" /> не кратно 4.
    ///   </exception>
    public static SoapBase64Binary Parse(string value)
    {
      if (value != null)
      {
        if (value.Length != 0)
        {
          byte[] numArray;
          try
          {
            numArray = Convert.FromBase64String(SoapType.FilterBin64(value));
          }
          catch (Exception ex)
          {
            throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "base64Binary", (object) value));
          }
          return new SoapBase64Binary(numArray);
        }
      }
      return new SoapBase64Binary(new byte[0]);
    }
  }
}
