// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IEnvoyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>Сведения о делегате.</summary>
  [ComVisible(true)]
  public interface IEnvoyInfo
  {
    /// <summary>
    ///   Возвращает или задает список делегатов, внесенных при маршалировании объекта цепочки контекста и объекта сервера.
    /// </summary>
    /// <returns>Цепь приемников делегатов.</returns>
    IMessageSink EnvoySinks { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
