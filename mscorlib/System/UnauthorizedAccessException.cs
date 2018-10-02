// Decompiled with JetBrains decompiler
// Type: System.UnauthorizedAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее в случае запрета доступа операционной системой из-за ошибки ввода-вывода или особого типа ошибки безопасности.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UnauthorizedAccessException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.UnauthorizedAccessException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException()
      : base(Environment.GetResourceString("Arg_UnauthorizedAccessException"))
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.UnauthorizedAccessException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.UnauthorizedAccessException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.UnauthorizedAccessException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
