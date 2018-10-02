// Decompiled with JetBrains decompiler
// Type: System.Security.XmlSyntaxException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>
  ///   Исключение, которое выдается при синтаксической ошибки синтаксического разбора XML.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class XmlSyntaxException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.XmlSyntaxException" /> стандартными свойствами.
    /// </summary>
    public XmlSyntaxException()
      : base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.XmlSyntaxException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public XmlSyntaxException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.XmlSyntaxException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public XmlSyntaxException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.XmlSyntaxException" /> класса с номером строки, в которой обнаружено исключение.
    /// </summary>
    /// <param name="lineNumber">
    ///   Номер строки XML-поток, где была обнаружена ошибка синтаксиса XML.
    /// </param>
    public XmlSyntaxException(int lineNumber)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), (object) lineNumber))
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.XmlSyntaxException" /> с указанным сообщением об ошибке и номер строки, в которой обнаружено исключение.
    /// </summary>
    /// <param name="lineNumber">
    ///   Номер строки XML-поток, где была обнаружена ошибка синтаксиса XML.
    /// </param>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public XmlSyntaxException(int lineNumber, string message)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), (object) lineNumber, (object) message))
    {
      this.SetErrorCode(-2146233320);
    }

    internal XmlSyntaxException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
