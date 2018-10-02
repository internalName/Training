// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.COMException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Исключение, которое выдается при возвращении неизвестного значения HRESULT после вызова метода COM.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class COMException : ExternalException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.COMException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public COMException()
      : base(Environment.GetResourceString("Arg_COMException"))
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.COMException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public COMException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.COMException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public COMException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.COMException" /> класса с указанным сообщением и кодом ошибки.
    /// </summary>
    /// <param name="message">
    ///   Произошла сообщение, указывающее причину исключения.
    /// </param>
    /// <param name="errorCode">
    ///   Код ошибки (HRESULT) значение связанной с этим исключением.
    /// </param>
    [__DynamicallyInvokable]
    public COMException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }

    [SecuritySafeCritical]
    internal COMException(int hresult)
      : base(Win32Native.GetMessage(hresult))
    {
      this.SetErrorCode(hresult);
    }

    internal COMException(string message, int hresult, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(hresult);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.COMException" /> класс данные сериализации.
    /// </summary>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Объект, предоставляющий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected COMException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Преобразует содержание исключения в строку.</summary>
    /// <returns>
    ///   Строка, содержащая <see cref="P:System.Exception.HResult" />, <see cref="P:System.Exception.Message" />, <see cref="P:System.Exception.InnerException" />, и <see cref="P:System.Exception.StackTrace" /> Свойства исключения.
    /// </returns>
    public override string ToString()
    {
      string message = this.Message;
      string str = this.GetType().ToString() + " (0x" + this.HResult.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture) + ")";
      if (message != null && message.Length > 0)
        str = str + ": " + message;
      Exception innerException = this.InnerException;
      if (innerException != null)
        str = str + " ---> " + innerException.ToString();
      if (this.StackTrace != null)
        str = str + Environment.NewLine + this.StackTrace;
      return str;
    }
  }
}
