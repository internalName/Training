// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.ServerFault
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Содержит сведения серверной неисправности.
  ///    Этот класс не наследуется.
  /// </summary>
  [SoapType(Embedded = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ServerFault
  {
    private string exceptionType;
    private string message;
    private string stackTrace;
    private Exception exception;

    internal ServerFault(Exception exception)
    {
      this.exception = exception;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.Formatters.ServerFault" />.
    /// </summary>
    /// <param name="exceptionType">
    ///   Тип исключения, произошедшего на сервере.
    /// </param>
    /// <param name="message">
    ///   Сообщение, сопровождающее исключение.
    /// </param>
    /// <param name="stackTrace">
    ///   Трассировка стека потока, вызвавшего исключение на сервере.
    /// </param>
    public ServerFault(string exceptionType, string message, string stackTrace)
    {
      this.exceptionType = exceptionType;
      this.message = message;
      this.stackTrace = stackTrace;
    }

    /// <summary>
    ///   Возвращает или задает тип исключения, которое было создано сервером.
    /// </summary>
    /// <returns>Тип исключения, которое было создано сервером.</returns>
    public string ExceptionType
    {
      get
      {
        return this.exceptionType;
      }
      set
      {
        this.exceptionType = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает сообщение, сопровождающее созданное на сервере исключение.
    /// </summary>
    /// <returns>
    ///   Сообщение, сопровождающее созданное на сервере исключение.
    /// </returns>
    public string ExceptionMessage
    {
      get
      {
        return this.message;
      }
      set
      {
        this.message = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает трассировку стека потока, вызвавшего исключение на сервере.
    /// </summary>
    /// <returns>
    ///   Трассировка стека потока, вызвавшего исключение на сервере.
    /// </returns>
    public string StackTrace
    {
      get
      {
        return this.stackTrace;
      }
      set
      {
        this.stackTrace = value;
      }
    }

    internal Exception Exception
    {
      get
      {
        return this.exception;
      }
    }
  }
}
