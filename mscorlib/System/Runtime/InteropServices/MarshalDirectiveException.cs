// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.MarshalDirectiveException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Исключение, которое создается модулем упаковки и передачи, когда он встречает неподдерживаемый атрибут <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MarshalDirectiveException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="MarshalDirectiveException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public MarshalDirectiveException()
      : base(Environment.GetResourceString("Arg_MarshalDirectiveException"))
    {
      this.SetErrorCode(-2146233035);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="MarshalDirectiveException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину возникновения исключения.
    /// </param>
    [__DynamicallyInvokable]
    public MarshalDirectiveException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233035);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.MarshalDirectiveException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MarshalDirectiveException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233035);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="MarshalDirectiveException" /> класс данные сериализации.
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
    protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
