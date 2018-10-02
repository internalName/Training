// Decompiled with JetBrains decompiler
// Type: System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
  /// <summary>
  ///   Предоставляет данные для события уведомления, которое возникает при первом появлении управляемого исключения, прежде чем общеязыковая среда выполнения начинает поиск обработчиков событий.
  /// </summary>
  public class FirstChanceExceptionEventArgs : EventArgs
  {
    private Exception m_Exception;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> класса с заданным исключением.
    /// </summary>
    /// <param name="exception">
    ///   Исключение, просто управляемым кодом, а также будет проверяться <see cref="E:System.AppDomain.UnhandledException" /> событие.
    /// </param>
    public FirstChanceExceptionEventArgs(Exception exception)
    {
      this.m_Exception = exception;
    }

    /// <summary>
    ///   Объект управляемого исключения, соответствующее исключение в управляемом коде.
    /// </summary>
    /// <returns>Недавно выданное исключение.</returns>
    public Exception Exception
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.m_Exception;
      }
    }
  }
}
