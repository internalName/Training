// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ServerException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Исключение вызывается, чтобы сообщать клиенту об ошибках, когда клиент подключается к приложениям, работающим не под платформой .NET Framework, которые не способны вызывать исключения.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ServerException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.ServerException" /> стандартными свойствами.
    /// </summary>
    public ServerException()
      : base(ServerException._nullMessage)
    {
      this.SetErrorCode(-2146233074);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ServerException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">Сообщение, описывающее исключение</param>
    public ServerException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233074);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.ServerException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="InnerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="InnerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ServerException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233074);
    }

    internal ServerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
