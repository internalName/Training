// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptographicException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Исключение, которое возникает в случае ошибки при выполнении криптографической операции.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class CryptographicException : SystemException
  {
    private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicException" /> стандартными свойствами.
    /// </summary>
    public CryptographicException()
      : base(Environment.GetResourceString("Arg_CryptographyException"))
    {
      this.SetErrorCode(-2146233296);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public CryptographicException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233296);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.CryptographicException" /> класса с указанным сообщением об ошибке в указанном формате.
    /// </summary>
    /// <param name="format">
    ///   Формат, используемый для вывода сообщения об ошибке.
    /// </param>
    /// <param name="insert">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public CryptographicException(string format, string insert)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, (object) insert))
    {
      this.SetErrorCode(-2146233296);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public CryptographicException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233296);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.CryptographicException" /> с заданным <see langword="HRESULT" /> код ошибки.
    /// </summary>
    /// <param name="hr">
    ///   <see langword="HRESULT" /> Код ошибки.
    /// </param>
    [SecuritySafeCritical]
    public CryptographicException(int hr)
      : this(Win32Native.GetMessage(hr))
    {
      if (((long) hr & 2147483648L) != 2147483648L)
        hr = hr & (int) ushort.MaxValue | -2147024896;
      this.SetErrorCode(hr);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptographicException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected CryptographicException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    private static void ThrowCryptographicException(int hr)
    {
      throw new CryptographicException(hr);
    }
  }
}
