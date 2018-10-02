// Decompiled with JetBrains decompiler
// Type: System.InvalidProgramException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается, если программа содержит недопустимые инструкции MSIL или метаданные.
  ///    Это обычно указывает на ошибку в компиляторе, создавшем программу.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class InvalidProgramException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidProgramException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public InvalidProgramException()
      : base(Environment.GetResourceString("InvalidProgram_Default"))
    {
      this.SetErrorCode(-2146233030);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidProgramException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidProgramException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233030);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidProgramException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidProgramException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233030);
    }

    internal InvalidProgramException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
