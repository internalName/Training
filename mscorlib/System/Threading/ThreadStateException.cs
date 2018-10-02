// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadStateException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, возникающее, когда <see cref="T:System.Threading.Thread" /> находится в недопустимом <see cref="P:System.Threading.Thread.ThreadState" /> для вызова данного метода.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ThreadStateException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadStateException" /> стандартными свойствами.
    /// </summary>
    public ThreadStateException()
      : base(Environment.GetResourceString("Arg_ThreadStateException"))
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadStateException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public ThreadStateException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadStateException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ThreadStateException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadStateException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected ThreadStateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
