// Decompiled with JetBrains decompiler
// Type: System.InsufficientMemoryException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при сбое проверки наличия необходимого объема памяти.
  ///    Этот класс не наследуется.
  /// </summary>
  [Serializable]
  public sealed class InsufficientMemoryException : OutOfMemoryException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientMemoryException" /> системным сообщением, содержащим описание ошибки.
    /// </summary>
    public InsufficientMemoryException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
    {
      this.SetErrorCode(-2146233027);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientMemoryException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    public InsufficientMemoryException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233027);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientMemoryException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public InsufficientMemoryException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233027);
    }

    private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
