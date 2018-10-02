// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerResponseChannelSinkStack
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
  ///   Обеспечивает функционирование стека для стека канала ответа сервера приемников.
  /// </summary>
  [ComVisible(true)]
  public interface IServerResponseChannelSinkStack
  {
    /// <summary>
    ///   Запрашивает асинхронную обработку метода вызова на приемники в текущем стеке приемников.
    /// </summary>
    /// <param name="msg">Ответное сообщение.</param>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <param name="stream">
    ///   Поток возвращается из транспортного приемника.
    /// </param>
    [SecurityCritical]
    void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>
    ///   Возвращает <see cref="T:System.IO.Stream" /> на котором упорядочивается указанное сообщение.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, Упорядочиваемое на запрошенном потоке.
    /// </param>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" /> На котором упорядочивается указанное сообщение.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
  }
}
