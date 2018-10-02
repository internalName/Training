// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="gMonth" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapMonth : ISoapXsd
  {
    private static string[] formats = new string[2]
    {
      "--MM--",
      "--MM--zzz"
    };
    private DateTime _value = DateTime.MinValue;

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
        return "gMonth";
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
      return SoapMonth.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" />.
    /// </summary>
    public SoapMonth()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> класса с заданными <see cref="T:System.DateTime" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.DateTime" /> объект для инициализации текущего экземпляра.
    /// </param>
    public SoapMonth(DateTime value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает или задает дату и время текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Объект, содержащий дату и время текущего экземпляра.
    /// </returns>
    public DateTime Value
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> в формате «--MM--».
    /// </returns>
    public override string ToString()
    {
      return this._value.ToString("--MM--", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> не содержит дату и время, соответствующие каким-либо распознаваемым шаблонам формата.
    /// </exception>
    public static SoapMonth Parse(string value)
    {
      return new SoapMonth(DateTime.ParseExact(value, SoapMonth.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None));
    }
  }
}
