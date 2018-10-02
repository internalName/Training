// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Исключение, возникающее, когда что-то пошло не так во время удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class RemotingException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.RemotingException" /> стандартными свойствами.
    /// </summary>
    public RemotingException()
      : base(RemotingException._nullMessage)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.RemotingException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причин исключения.
    /// </param>
    public RemotingException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.RemotingException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причин исключения.
    /// </param>
    /// <param name="InnerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="InnerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public RemotingException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.RemotingException" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected RemotingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
