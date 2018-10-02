// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ExternalException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Базовый тип исключений для всех исключений COM-взаимодействия и исключений структурированной обработки исключений (SEH).
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ExternalException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="ExternalException" /> стандартными свойствами.
    /// </summary>
    public ExternalException()
      : base(Environment.GetResourceString("Arg_ExternalException"))
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="ExternalException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину возникновения исключения.
    /// </param>
    public ExternalException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.ExternalException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ExternalException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="ExternalException" /> с указанным сообщением об ошибке и возвращает значение HRESULT ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину возникновения исключения.
    /// </param>
    /// <param name="errorCode">Значение HRESULT ошибки.</param>
    public ExternalException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="ExternalException" /> класс данные сериализации.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected ExternalException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает <see langword="HRESULT" /> ошибки.
    /// </summary>
    /// <returns>
    ///   <see langword="HRESULT" /> ошибки.
    /// </returns>
    public virtual int ErrorCode
    {
      get
      {
        return this.HResult;
      }
    }

    /// <summary>
    ///   Возвращает строку, содержащую значение HRESULT ошибки.
    /// </summary>
    /// <returns>Строка, представляющая значение HRESULT.</returns>
    public override string ToString()
    {
      string message = this.Message;
      string str = this.GetType().ToString() + " (0x" + this.HResult.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture) + ")";
      if (!string.IsNullOrEmpty(message))
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
