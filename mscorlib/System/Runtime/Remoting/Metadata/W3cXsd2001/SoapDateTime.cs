// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDateTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Предоставляет статические методы для сериализации и десериализации <see cref="T:System.DateTime" /> строку, форматируется как XSD <see langword="dateTime" />.
  /// </summary>
  [ComVisible(true)]
  public sealed class SoapDateTime
  {
    private static string[] formats = new string[22]
    {
      "yyyy-MM-dd'T'HH:mm:ss.fffffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.ffff",
      "yyyy-MM-dd'T'HH:mm:ss.ffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.fff",
      "yyyy-MM-dd'T'HH:mm:ss.fffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.ff",
      "yyyy-MM-dd'T'HH:mm:ss.ffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.f",
      "yyyy-MM-dd'T'HH:mm:ss.fzzz",
      "yyyy-MM-dd'T'HH:mm:ss",
      "yyyy-MM-dd'T'HH:mm:sszzz",
      "yyyy-MM-dd'T'HH:mm:ss.fffff",
      "yyyy-MM-dd'T'HH:mm:ss.fffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.ffffff",
      "yyyy-MM-dd'T'HH:mm:ss.ffffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.fffffff",
      "yyyy-MM-dd'T'HH:mm:ss.ffffffff",
      "yyyy-MM-dd'T'HH:mm:ss.ffffffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.fffffffff",
      "yyyy-MM-dd'T'HH:mm:ss.fffffffffzzz",
      "yyyy-MM-dd'T'HH:mm:ss.ffffffffff",
      "yyyy-MM-dd'T'HH:mm:ss.ffffffffffzzz"
    };

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
        return "dateTime";
      }
    }

    /// <summary>
    ///   Возвращает указанный <see cref="T:System.DateTime" /> объекта в виде <see cref="T:System.String" />.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.DateTime" /> Преобразуемый объект.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.String" /> представление <paramref name="value" /> в формате «гггг мм dd'T'HH:mm:ss.fffffffzzz».
    /// </returns>
    public static string ToString(DateTime value)
    {
      return value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.DateTime" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DateTime" /> получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// Один из следующих вариантов:
    /// 
    ///     Параметр <paramref name="value" /> равен пустой строке.
    /// 
    ///     <paramref name="value" /> — <see langword="null" /> ссылки.
    /// 
    ///     <paramref name="value" /> не содержит дату и время, соответствующие каким-либо распознаваемым шаблонам формата.
    ///   </exception>
    public static DateTime Parse(string value)
    {
      DateTime dateTime;
      try
      {
        if (value == null)
        {
          dateTime = DateTime.MinValue;
        }
        else
        {
          string s = value;
          if (value.EndsWith("Z", StringComparison.Ordinal))
            s = value.Substring(0, value.Length - 1) + "-00:00";
          dateTime = DateTime.ParseExact(s, SoapDateTime.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:dateTime", (object) value));
      }
      return dateTime;
    }
  }
}
