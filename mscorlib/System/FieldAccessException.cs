// Decompiled with JetBrains decompiler
// Type: System.FieldAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при недопустимой попытке доступа к личным или защищенным полям внутри класса.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FieldAccessException : MemberAccessException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.FieldAccessException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public FieldAccessException()
      : base(Environment.GetResourceString("Arg_FieldAccessException"))
    {
      this.SetErrorCode(-2146233081);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.FieldAccessException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public FieldAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233081);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.FieldAccessException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public FieldAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233081);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.FieldAccessException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected FieldAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
