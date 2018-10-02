// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InvalidComObjectException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Исключение, вызванное использованием недопустимого COM-объекта.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class InvalidComObjectException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="InvalidComObjectException" /> со свойствами по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public InvalidComObjectException()
      : base(Environment.GetResourceString("Arg_InvalidComObjectException"))
    {
      this.SetErrorCode(-2146233049);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="InvalidComObjectException" /> с сообщением.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidComObjectException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233049);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.InvalidComObjectException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidComObjectException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233049);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="COMException" /> класс данные сериализации.
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
    protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
