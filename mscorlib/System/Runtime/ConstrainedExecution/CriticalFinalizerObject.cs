// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.CriticalFinalizerObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>
  ///   Гарантирует, что весь код завершения в производных классах помечен как критический.
  /// </summary>
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class CriticalFinalizerObject
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" />.
    /// </summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected CriticalFinalizerObject()
    {
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые классом <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" />.
    /// </summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    ~CriticalFinalizerObject()
    {
    }
  }
}
