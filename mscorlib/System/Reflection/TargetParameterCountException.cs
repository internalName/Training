// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetParameterCountException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Исключение, которое выдается в том случае, если количество параметров для вызова не совпадает с ожидаемым.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TargetParameterCountException : ApplicationException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.TargetParameterCountException" /> класса с пустой строкой сообщения и базовой причиной исключения.
    /// </summary>
    [__DynamicallyInvokable]
    public TargetParameterCountException()
      : base(Environment.GetResourceString("Arg_TargetParameterCountException"))
    {
      this.SetErrorCode(-2147352562);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.TargetParameterCountException" /> класса со строкой сообщения, указанное сообщение и корневой причиной исключения.
    /// </summary>
    /// <param name="message">
    ///   A <see langword="String" /> описания причины созданного исключения.
    /// </param>
    [__DynamicallyInvokable]
    public TargetParameterCountException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147352562);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TargetParameterCountException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TargetParameterCountException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147352562);
    }

    internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
