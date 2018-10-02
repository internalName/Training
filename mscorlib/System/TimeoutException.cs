// Decompiled with JetBrains decompiler
// Type: System.TimeoutException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при истечении времени, предоставленного процессу или операции.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TimeoutException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeoutException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public TimeoutException()
      : base(Environment.GetResourceString("Arg_TimeoutException"))
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeoutException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public TimeoutException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>
    ///   Выполняет инициализацию нового экземпляра класса <see cref="T:System.TimeoutException" /> с указанным сообщением об ошибке и внутренним исключением.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TimeoutException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeoutException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о вызываемом исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    ///    Параметр <paramref name="context" /> зарезервирован для будущего использования и может быть задан значением <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Имя класса — <see langword="null" />, или <see cref="P:System.Exception.HResult" /> равно нулю (0).
    /// </exception>
    protected TimeoutException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
