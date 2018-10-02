// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SEHException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Представляет ошибки структурной обработки исключений (SEH).
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SEHException : ExternalException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.SEHException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public SEHException()
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.SEHException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SEHException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.SEHException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SEHException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.SEHException" /> класс данные сериализации.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected SEHException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Указывает ли исключение может быть восстановлен из и следует ли код может продолжиться с точки, в которой возникло исключение.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />, поскольку исключения с возможностью восстановления не реализованы.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool CanResume()
    {
      return false;
    }
  }
}
