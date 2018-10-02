// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeArrayRankMismatchException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Исключение возникает, если ранг входящего <see langword="SAFEARRAY" /> не совпадает с рангом, указанным в управляемой подписи.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SafeArrayRankMismatchException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="SafeArrayTypeMismatchException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public SafeArrayRankMismatchException()
      : base(Environment.GetResourceString("Arg_SafeArrayRankMismatchException"))
    {
      this.SetErrorCode(-2146233032);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="SafeArrayRankMismatchException" /> указанным сообщением.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SafeArrayRankMismatchException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233032);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.SafeArrayRankMismatchException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SafeArrayRankMismatchException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233032);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="SafeArrayTypeMismatchException" /> класс данные сериализации.
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
    protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
