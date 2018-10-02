// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Исключение, которое вызывается при возникновении ошибки во время трассировки событий Windows (ETW).
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class EventSourceException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSourceException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventSourceException()
      : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public EventSourceException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее текущее исключение, или <see langword="null" /> Если указано внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public EventSourceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected EventSourceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal EventSourceException(Exception innerException)
      : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"), innerException)
    {
    }
  }
}
