// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptographicUnexpectedOperationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Исключение возникает при выполнении непредвиденной операции во время криптографической операции.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class CryptographicUnexpectedOperationException : CryptographicException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> стандартными свойствами.
    /// </summary>
    public CryptographicUnexpectedOperationException()
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public CryptographicUnexpectedOperationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> класса с указанным сообщением об ошибке в указанном формате.
    /// </summary>
    /// <param name="format">
    ///   Формат, используемый для вывода сообщения об ошибке.
    /// </param>
    /// <param name="insert">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public CryptographicUnexpectedOperationException(string format, string insert)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, (object) insert))
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public CryptographicUnexpectedOperationException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
