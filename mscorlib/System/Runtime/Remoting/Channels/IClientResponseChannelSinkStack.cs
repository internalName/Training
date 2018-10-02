// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientResponseChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Обеспечивает функциональность для стека ответа клиента приемников каналов, который должен вызываться при декодировании асинхронного ответа.
  /// </summary>
  [ComVisible(true)]
  public interface IClientResponseChannelSinkStack
  {
    /// <summary>
    ///   Запрашивает асинхронную обработку метода вызова на приемники в текущем стеке приемников.
    /// </summary>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <param name="stream">
    ///   Поток возвращается из транспортного приемника.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Текущий стек приемников пуст.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

    /// <summary>Отправляет указанный ответ в приемник ответов.</summary>
    /// <param name="msg">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> Для диспетчеризации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void DispatchReplyMessage(IMessage msg);

    /// <summary>Отправляет указанное исключение в приемник ответов.</summary>
    /// <param name="e">Исключение, отправляемое на сервер.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void DispatchException(Exception e);
  }
}
