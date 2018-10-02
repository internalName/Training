// Decompiled with JetBrains decompiler
// Type: System.IO.EndOfStreamException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>
  ///   Исключение, которое выдается при попытке чтения за концом потока.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class EndOfStreamException : IOException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.EndOfStreamException" /> класса со строкой сообщения, установленной на системного сообщения и значением HRESULT, равным COR_E_ENDOFSTREAM.
    /// </summary>
    [__DynamicallyInvokable]
    public EndOfStreamException()
      : base(Environment.GetResourceString("Arg_EndOfStreamException"))
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.EndOfStreamException" /> класса со строкой сообщения, значение <paramref name="message" /> и значением HRESULT, равным COR_E_ENDOFSTREAM.
    /// </summary>
    /// <param name="message">
    ///   Строка с описанием ошибки.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public EndOfStreamException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.EndOfStreamException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Строка с описанием ошибки.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public EndOfStreamException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.EndOfStreamException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected EndOfStreamException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
