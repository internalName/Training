// Decompiled with JetBrains decompiler
// Type: System.MemberAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение возникает при неудачной попытке доступа к члену класса.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MemberAccessException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MemberAccessException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public MemberAccessException()
      : base(Environment.GetResourceString("Arg_AccessException"))
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MemberAccessException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public MemberAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MemberAccessException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MemberAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MemberAccessException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected MemberAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
