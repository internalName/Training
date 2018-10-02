// Decompiled with JetBrains decompiler
// Type: System.MethodAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при недопустимой попытке доступа к методу, например при попытке доступа к закрытому методу из частично доверенного кода.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MethodAccessException : MemberAccessException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.MethodAccessException" /> классе и задайте <see cref="P:System.Exception.Message" /> Свойства нового экземпляра системное сообщение с описанием ошибки, например «Попытка получить доступ к методу».
    ///    Это сообщение учитывает текущую культуру системы.
    /// </summary>
    [__DynamicallyInvokable]
    public MethodAccessException()
      : base(Environment.GetResourceString("Arg_MethodAccessException"))
    {
      this.SetErrorCode(-2146233072);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MethodAccessException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public MethodAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233072);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MethodAccessException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MethodAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233072);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MethodAccessException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected MethodAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
