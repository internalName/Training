// Decompiled with JetBrains decompiler
// Type: System.InsufficientExecutionStackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при недостаточности стека выполнения для выполнения большинства методов.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class InsufficientExecutionStackException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientExecutionStackException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException()
      : base(Environment.GetResourceString("Arg_InsufficientExecutionStackException"))
    {
      this.SetErrorCode(-2146232968);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientExecutionStackException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232968);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InsufficientExecutionStackException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232968);
    }

    private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
