// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="time" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapTime : ISoapXsd
  {
    private static string[] formats = new string[22]
    {
      "HH:mm:ss.fffffffzzz",
      "HH:mm:ss.ffff",
      "HH:mm:ss.ffffzzz",
      "HH:mm:ss.fff",
      "HH:mm:ss.fffzzz",
      "HH:mm:ss.ff",
      "HH:mm:ss.ffzzz",
      "HH:mm:ss.f",
      "HH:mm:ss.fzzz",
      "HH:mm:ss",
      "HH:mm:sszzz",
      "HH:mm:ss.fffff",
      "HH:mm:ss.fffffzzz",
      "HH:mm:ss.ffffff",
      "HH:mm:ss.ffffffzzz",
      "HH:mm:ss.fffffff",
      "HH:mm:ss.ffffffff",
      "HH:mm:ss.ffffffffzzz",
      "HH:mm:ss.fffffffff",
      "HH:mm:ss.fffffffffzzz",
      "HH:mm:ss.fffffffff",
      "HH:mm:ss.fffffffffzzz"
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
        return "time";
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
      return SoapTime.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" />.
    /// </summary>
    public SoapTime()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> класса с заданными <see cref="T:System.DateTime" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.DateTime" /> объект для инициализации текущего экземпляра.
    /// </param>
    public SoapTime(DateTime value)
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
        this._value = new DateTime(1, 1, 1, value.Hour, value.Minute, value.Second, value.Millisecond);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> в формате «HH:mm:ss.fffzzz».
    /// </returns>
    public override string ToString()
    {
      return this._value.ToString("HH:mm:ss.fffffffzzz", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> не содержит дату и время, соответствующие каким-либо распознаваемым шаблонам формата.
    /// </exception>
    public static SoapTime Parse(string value)
    {
      string s = value;
      if (value.EndsWith("Z", StringComparison.Ordinal))
        s = value.Substring(0, value.Length - 1) + "-00:00";
      return new SoapTime(DateTime.ParseExact(s, SoapTime.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None));
    }
  }
}
