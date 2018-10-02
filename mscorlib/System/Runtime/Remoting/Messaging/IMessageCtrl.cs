// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessageCtrl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Предоставляет способ управления асинхронными сообщениями после их отправки с помощью <see cref="M:System.Runtime.Remoting.Messaging.IMessageSink.AsyncProcessMessage(System.Runtime.Remoting.Messaging.IMessage,System.Runtime.Remoting.Messaging.IMessageSink)" />.
  /// </summary>
  [ComVisible(true)]
  public interface IMessageCtrl
  {
    /// <summary>Отменяет асинхронный вызов.</summary>
    /// <param name="msToCancel">
    ///   Количество миллисекунд, после которого отменяется сообщение.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void Cancel(int msToCancel);
  }
}
