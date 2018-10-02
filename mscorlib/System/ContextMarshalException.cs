// Decompiled with JetBrains decompiler
// Type: System.ContextMarshalException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при неудачной попытке маршалинга объекта через границы контекста.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ContextMarshalException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ContextMarshalException" /> стандартными свойствами.
    /// </summary>
    public ContextMarshalException()
      : base(Environment.GetResourceString("Arg_ContextMarshalException"))
    {
      this.SetErrorCode(-2146233084);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ContextMarshalException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public ContextMarshalException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233084);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ContextMarshalException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ContextMarshalException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233084);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ContextMarshalException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, хранящий сериализованные данные объекта, относящиеся к выдаваемому исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected ContextMarshalException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
