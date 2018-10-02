// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSink
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
  ///   Предоставляет требуемые функции и свойства для приемников каналов клиента.
  /// </summary>
  [ComVisible(true)]
  public interface IClientChannelSink : IChannelSinkBase
  {
    /// <summary>
    ///   Запрашивает обработку из текущего приемника сообщения.
    /// </summary>
    /// <param name="msg">Сообщение для обработки.</param>
    /// <param name="requestHeaders">
    ///   Заголовки, добавляемые в исходящее сообщение, отправляемое на сервер.
    /// </param>
    /// <param name="requestStream">
    ///   Поток направляется в транспортный приемник.
    /// </param>
    /// <param name="responseHeaders">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> интерфейс, содержащий заголовки, возвращаемые сервером.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="responseStream">
    ///   При возвращении данного метода содержит <see cref="T:System.IO.Stream" /> из транспортного приемника.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

    /// <summary>
    ///   Запрашивает асинхронную обработку метода вызова на текущем приемнике.
    /// </summary>
    /// <param name="sinkStack">
    ///   Стек приемников каналов, вызывающий этот приемник.
    /// </param>
    /// <param name="msg">Сообщение для обработки.</param>
    /// <param name="headers">
    ///   Заголовки, добавляемые в исходящее сообщение, отправляемое на сервер.
    /// </param>
    /// <param name="stream">
    ///   Поток направляется в транспортный приемник.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>
    ///   Запрашивает асинхронную обработку ответа на метод вызова на текущем приемнике.
    /// </summary>
    /// <param name="sinkStack">
    ///   Стек приемников, вызывающий этот приемник.
    /// </param>
    /// <param name="state">
    ///   Сведения, созданные на стороне запроса, который связан с этим приемником.
    /// </param>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <param name="stream">
    ///   Поток возвращается из транспортного приемника.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

    /// <summary>
    ///   Возвращает <see cref="T:System.IO.Stream" /> на котором упорядочивается обеспеченное сообщение.
    /// </summary>
    /// <param name="msg">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> С подробной информацией о вызове метода.
    /// </param>
    /// <param name="headers">
    ///   Заголовки, добавляемые в исходящее сообщение, отправляемое на сервер.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" /> На котором упорядочивается обеспеченное сообщение.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

    /// <summary>
    ///   Возвращает следующий клиент приемник канала в цепочке приемников клиента.
    /// </summary>
    /// <returns>
    ///   Следующий клиент приемник канала в цепочке приемников клиента.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IClientChannelSink NextChannelSink { [SecurityCritical] get; }
  }
}
