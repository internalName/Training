// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandleCannotBeOpenedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, возникающее при попытке открыть системы мьютекса, семафора или события дескриптор ожидания, не существует.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class WaitHandleCannotBeOpenedException : ApplicationException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException()
      : base(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException"))
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта по возникающему исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
