// Decompiled with JetBrains decompiler
// Type: System.Threading.AutoResetEvent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Уведомляет ожидающий поток о том, что произошло событие.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class AutoResetEvent : EventWaitHandle
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.AutoResetEvent" /> класса логическое значение, указывающее, следует ли для задания начального состояния сигнальным.
    /// </summary>
    /// <param name="initialState">
    ///   <see langword="true" /> для задания начального состояния сигнальным; <see langword="false" /> присвоено начальное состояние несигнальное.
    /// </param>
    [__DynamicallyInvokable]
    public AutoResetEvent(bool initialState)
      : base(initialState, EventResetMode.AutoReset)
    {
    }
  }
}
