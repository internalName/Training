// Decompiled with JetBrains decompiler
// Type: System.UnhandledExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет данные для события, которое генерируется при возникновении исключения, не обработанного ни одним доменом приложения.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class UnhandledExceptionEventArgs : EventArgs
  {
    private object _Exception;
    private bool _IsTerminating;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.UnhandledExceptionEventArgs" /> класса объекта исключения и флагом завершения выполнения.
    /// </summary>
    /// <param name="exception">
    ///   Исключение, которое не обрабатывается.
    /// </param>
    /// <param name="isTerminating">
    ///   Значение <see langword="true" />, если среда выполнения завершает работу; в противном случае — значение <see langword="false" />.
    /// </param>
    public UnhandledExceptionEventArgs(object exception, bool isTerminating)
    {
      this._Exception = exception;
      this._IsTerminating = isTerminating;
    }

    /// <summary>Возвращает необработанный объект исключения.</summary>
    /// <returns>Необработанный объект исключения.</returns>
    public object ExceptionObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._Exception;
      }
    }

    /// <summary>Указывает, завершает ли работу среда CLR.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если среда выполнения завершает работу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsTerminating
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._IsTerminating;
      }
    }
  }
}
