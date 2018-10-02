// Decompiled with JetBrains decompiler
// Type: System.OutOfMemoryException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается при недостаточном объеме памяти для выполнения программы.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class OutOfMemoryException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OutOfMemoryException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public OutOfMemoryException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
    {
      this.SetErrorCode(-2147024882);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OutOfMemoryException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public OutOfMemoryException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024882);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OutOfMemoryException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public OutOfMemoryException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024882);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OutOfMemoryException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
