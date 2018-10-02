// Decompiled with JetBrains decompiler
// Type: System.DivideByZeroException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое возникает при попытке деления целого значения или значения <see cref="T:System.Decimal" /> на ноль.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class DivideByZeroException : ArithmeticException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DivideByZeroException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DivideByZeroException()
      : base(Environment.GetResourceString("Arg_DivideByZero"))
    {
      this.SetErrorCode(-2147352558);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DivideByZeroException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public DivideByZeroException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147352558);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DivideByZeroException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public DivideByZeroException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147352558);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DivideByZeroException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected DivideByZeroException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
