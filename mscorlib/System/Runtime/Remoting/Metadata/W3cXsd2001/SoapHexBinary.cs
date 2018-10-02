// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Создает оболочку для XSD <see langword="hexBinary" /> типа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapHexBinary : ISoapXsd
  {
    private StringBuilder sb = new StringBuilder(100);
    private byte[] _value;

    /// <summary>
    ///   Возвращает язык определения схемы XML (XSD) текущего типа SOAP.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> указывающее, XSD текущего типа SOAP.
    /// </returns>
    public static string XsdType
    {
      get
      {
        return "hexBinary";
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
      return SoapHexBinary.XsdType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" />.
    /// </summary>
    public SoapHexBinary()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" />.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Byte" /> массив, содержащий шестнадцатеричное число.
    /// </param>
    public SoapHexBinary(byte[] value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает или задает шестнадцатеричное представление числа.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Byte" /> массив, содержащий шестнадцатеричное представление числа.
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
    ///   Возвращает <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> полученный из <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" />.
    /// </returns>
    public override string ToString()
    {
      this.sb.Length = 0;
      for (int index = 0; index < this._value.Length; ++index)
      {
        string str = this._value[index].ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
        if (str.Length == 1)
          this.sb.Append('0');
        this.sb.Append(str);
      }
      return this.sb.ToString();
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see langword="String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    public static SoapHexBinary Parse(string value)
    {
      return new SoapHexBinary(SoapHexBinary.ToByteArray(SoapType.FilterBin64(value)));
    }

    private static byte[] ToByteArray(string value)
    {
      char[] charArray = value.ToCharArray();
      if (charArray.Length % 2 != 0)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:hexBinary", (object) value));
      byte[] numArray = new byte[charArray.Length / 2];
      for (int index = 0; index < charArray.Length / 2; ++index)
        numArray[index] = (byte) ((uint) SoapHexBinary.ToByte(charArray[index * 2], value) * 16U + (uint) SoapHexBinary.ToByte(charArray[index * 2 + 1], value));
      return numArray;
    }

    private static byte ToByte(char c, string value)
    {
      c.ToString();
      try
      {
        return byte.Parse(c.ToString(), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (Exception ex)
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:hexBinary", (object) value));
      }
    }
  }
}
