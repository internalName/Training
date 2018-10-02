// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingTimeoutException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Исключение возникает, когда доступ к серверу или клиенту оказывается невозможен в течение периода времени, указанного ранее.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class RemotingTimeoutException : RemotingException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> стандартными свойствами.
    /// </summary>
    public RemotingTimeoutException()
      : base(RemotingTimeoutException._nullMessage)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    public RemotingTimeoutException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="InnerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="InnerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public RemotingTimeoutException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233077);
    }

    internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
