// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessageSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>Определяет интерфейс для приемника сообщений.</summary>
  [ComVisible(true)]
  public interface IMessageSink
  {
    /// <summary>Синхронно обрабатывает заданное сообщение.</summary>
    /// <param name="msg">Сообщение для обработки.</param>
    /// <returns>Ответное сообщение в ответ на запрос.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IMessage SyncProcessMessage(IMessage msg);

    /// <summary>Асинхронно обрабатывает заданное сообщение.</summary>
    /// <param name="msg">Сообщение для обработки.</param>
    /// <param name="replySink">
    ///   Ответный приемник для ответного сообщения.
    /// </param>
    /// <returns>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> интерфейс, который предоставляет способ управления асинхронными сообщениями после их отправки.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);

    /// <summary>
    ///   Возвращает следующий приемник сообщений в цепочке приемников.
    /// </summary>
    /// <returns>Следующий приемник сообщений в цепочке приемников.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    IMessageSink NextSink { [SecurityCritical] get; }
  }
}
