// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Исключение, которое выдается при возникновении ошибки во время сериализации или десериализации.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SerializationException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Arg_SerializationException");

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public SerializationException()
      : base(SerializationException._nullMessage)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Serialization.SerializationException" /> класса с использованием заданного сообщения.
    /// </summary>
    /// <param name="message">
    ///   Указывает причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SerializationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SerializationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationException" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект сведений о сериализации, хранящий данные сериализованного объекта в форме имя значение.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected SerializationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
