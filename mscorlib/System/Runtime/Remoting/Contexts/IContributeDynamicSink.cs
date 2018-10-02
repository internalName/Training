// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeDynamicSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Показывает, что свойство реализации будет зарегистрирован во время выполнения посредством <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> метод.
  /// </summary>
  [ComVisible(true)]
  public interface IContributeDynamicSink
  {
    /// <summary>
    ///   Возвращает приемник сообщений, который будет уведомлен о вызов начала и окончания событий с помощью <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> интерфейса.
    /// </summary>
    /// <returns>
    ///   Динамические приемника, который предоставляет <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> интерфейса.
    /// </returns>
    [SecurityCritical]
    IDynamicMessageSink GetDynamicSink();
  }
}
