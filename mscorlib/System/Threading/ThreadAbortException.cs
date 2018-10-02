// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadAbortException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, выдаваемое при вызове метода <see cref="M:System.Threading.Thread.Abort(System.Object)" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ThreadAbortException : SystemException
  {
    private ThreadAbortException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
    {
      this.SetErrorCode(-2146233040);
    }

    internal ThreadAbortException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает объект, содержащий сведения о приложении, связанные с прерывание потока.
    /// </summary>
    /// <returns>Объект, содержащий сведения о приложении.</returns>
    public object ExceptionState
    {
      [SecuritySafeCritical] get
      {
        return Thread.CurrentThread.AbortReason;
      }
    }
  }
}
