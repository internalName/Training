// Decompiled with JetBrains decompiler
// Type: System.ExecutionEngineException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, выбрасываемое при внутренней ошибке в ядре выполнения среды CLR.
  ///    Этот класс не наследуется.
  /// </summary>
  [Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
  [ComVisible(true)]
  [Serializable]
  public sealed class ExecutionEngineException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ExecutionEngineException" />.
    /// </summary>
    public ExecutionEngineException()
      : base(Environment.GetResourceString("Arg_ExecutionEngineException"))
    {
      this.SetErrorCode(-2146233082);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ExecutionEngineException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public ExecutionEngineException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233082);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ExecutionEngineException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ExecutionEngineException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233082);
    }

    internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
