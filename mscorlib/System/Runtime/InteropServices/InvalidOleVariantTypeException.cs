// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InvalidOleVariantTypeException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Исключение, которое выдается упаковщиком при обнаружении аргумента типа variant, маршалинг которого в управляемый код выполнить невозможно.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class InvalidOleVariantTypeException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="InvalidOleVariantTypeException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public InvalidOleVariantTypeException()
      : base(Environment.GetResourceString("Arg_InvalidOleVariantTypeException"))
    {
      this.SetErrorCode(-2146233039);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="InvalidOleVariantTypeException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidOleVariantTypeException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233039);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidOleVariantTypeException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233039);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="InvalidOleVariantTypeException" /> класс данные сериализации.
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
    protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
