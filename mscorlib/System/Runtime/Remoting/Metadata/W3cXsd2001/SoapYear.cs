// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="gYear" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapYear : ISoapXsd
  {
    private static string[] formats = new string[6]
    {
      "yyyy",
      "'+'yyyy",
      "'-'yyyy",
      "yyyyzzz",
      "'+'yyyyzzz",
      "'-'yyyyzzz"
    };
    private DateTime _value = DateTime.MinValue;
    private int _sign;

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
        return "gYear";
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
      return SoapYear.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" />.
    /// </summary>
    public SoapYear()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> класса с заданными <see cref="T:System.DateTime" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.DateTime" /> объект для инициализации текущего экземпляра.
    /// </param>
    public SoapYear(DateTime value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> класса с заданными <see cref="T:System.DateTime" /> объекта и целое число, указывающее ли <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> положительным или отрицательным значением.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.DateTime" /> объект для инициализации текущего экземпляра.
    /// </param>
    /// <param name="sign">
    ///   Целое число, указывающее, является ли <paramref name="value" /> является положительным.
    /// </param>
    public SoapYear(DateTime value, int sign)
    {
      this._value = value;
      this._sign = sign;
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
    ///   Возвращает или задает дату и время текущего экземпляра является положительным или отрицательным.
    /// </summary>
    /// <returns>
    ///   Целое число, указывающее, является ли <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> положительным или отрицательным.
    /// </returns>
    public int Sign
    {
      get
      {
        return this._sign;
      }
      set
      {
        this._sign = value;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> в формате «гггг» или «-гггг» Если <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Sign" /> является отрицательным.
    /// </returns>
    public override string ToString()
    {
      if (this._sign < 0)
        return this._value.ToString("'-'yyyy", (IFormatProvider) CultureInfo.InvariantCulture);
      return this._value.ToString("yyyy", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> не содержит дату и время, соответствующие каким-либо распознаваемым шаблонам формата.
    /// </exception>
    public static SoapYear Parse(string value)
    {
      int sign = 0;
      if (value[0] == '-')
        sign = -1;
      return new SoapYear(DateTime.ParseExact(value, SoapYear.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
    }
  }
}
