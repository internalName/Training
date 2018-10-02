// Decompiled with JetBrains decompiler
// Type: System.TypeAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее, когда метод пытается использовать тип, к которому у него нет доступа.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TypeAccessException : TypeLoadException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeAccessException" /> системным сообщением, содержащим описание ошибки.
    /// </summary>
    [__DynamicallyInvokable]
    public TypeAccessException()
      : base(Environment.GetResourceString("Arg_TypeAccessException"))
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeAccessException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Вызывающий объект этого конструктора должен убедиться, что эта строка локализована для текущего языка и региональных параметров системы.
    /// </param>
    [__DynamicallyInvokable]
    public TypeAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeAccessException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Вызывающий объект этого конструктора должен убедиться, что эта строка локализована для текущего языка и региональных параметров системы.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TypeAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeAccessException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected TypeAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.SetErrorCode(-2146233021);
    }
  }
}
