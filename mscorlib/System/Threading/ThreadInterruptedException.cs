// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadInterruptedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, возникающее, когда <see cref="T:System.Threading.Thread" /> прервано, пока он находится в состоянии ожидания.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ThreadInterruptedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadInterruptedException" /> стандартными свойствами.
    /// </summary>
    public ThreadInterruptedException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
    {
      this.SetErrorCode(-2146233063);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadInterruptedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public ThreadInterruptedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233063);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadInterruptedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ThreadInterruptedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233063);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadInterruptedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected ThreadInterruptedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
