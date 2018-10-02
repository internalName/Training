// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetInvocationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Исключение выдается методами, вызываемыми средствами отражения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TargetInvocationException : ApplicationException
  {
    private TargetInvocationException()
      : base(Environment.GetResourceString("Arg_TargetInvocationException"))
    {
      this.SetErrorCode(-2146232828);
    }

    private TargetInvocationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232828);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.TargetInvocationException" /> класса со ссылкой на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TargetInvocationException(Exception inner)
      : base(Environment.GetResourceString("Arg_TargetInvocationException"), inner)
    {
      this.SetErrorCode(-2146232828);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TargetInvocationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TargetInvocationException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232828);
    }

    internal TargetInvocationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
