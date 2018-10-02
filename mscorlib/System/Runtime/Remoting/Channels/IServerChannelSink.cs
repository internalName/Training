// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSink
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
  ///   Предоставляет методы, используемые для приемников, безопасности и транспорта.
  /// </summary>
  [ComVisible(true)]
  public interface IServerChannelSink : IChannelSinkBase
  {
    /// <summary>
    ///   Запрашивает обработку из текущего приемника сообщения.
    /// </summary>
    /// <param name="sinkStack">
    ///   Стек приемников каналов, вызывающий текущий приемник.
    /// </param>
    /// <param name="requestMsg">Сообщение, содержащее запрос.</param>
    /// <param name="requestHeaders">
    ///   Заголовки, извлеченные из входящего сообщения от клиента.
    /// </param>
    /// <param name="requestStream">
    ///   Поток, который необходимо обработать и передать приемнику десериализации.
    /// </param>
    /// <param name="responseMsg">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> содержащий ответное сообщение.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="responseHeaders">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> содержащий заголовки, добавляемые в возвращенное сообщение, отправляемое клиенту.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="responseStream">
    ///   При возвращении данного метода содержит <see cref="T:System.IO.Stream" /> это заголовок обратно в транспортный приемник.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" /> значение состояния, который предоставляет сведения об обработке сообщений.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream);

    /// <summary>
    ///   Запрашивает обработку из текущего приемника ответа из вызова метода, передаваемого асинхронно.
    /// </summary>
    /// <param name="sinkStack">
    ///   Стек приемников, ведущий назад в транспортный приемник сервера.
    /// </param>
    /// <param name="state">
    ///   Сведения, созданные на стороне запроса, который связан с этим приемником.
    /// </param>
    /// <param name="msg">Ответное сообщение.</param>
    /// <param name="headers">
    ///   Заголовки, добавляемые в возвращаемое сообщение, отправляемое клиенту.
    /// </param>
    /// <param name="stream">
    ///   Поток, отправляемый обратно в транспортный приемник.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>
    ///   Возвращает <see cref="T:System.IO.Stream" /> на котором упорядочивается предоставленное ответное сообщение.
    /// </summary>
    /// <param name="sinkStack">
    ///   Стек приемников, ведущий назад в транспортный приемник сервера.
    /// </param>
    /// <param name="state">
    ///   Состояние, переданное стеку данным приемником.
    /// </param>
    /// <param name="msg">Сериализуемое ответное сообщение.</param>
    /// <param name="headers">
    ///   Заголовки, помещаемые в поток ответа клиенту.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" /> На котором упорядочивается предоставленное ответное сообщение.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers);

    /// <summary>
    ///   Возвращает приемник канала в цепочке приемников сервера следующего сервера.
    /// </summary>
    /// <returns>
    ///   Следующий приемник канала сервера в цепи приемников сервера.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий не имеет необходимых <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Infrastructure" /> разрешение.
    /// </exception>
    IServerChannelSink NextChannelSink { [SecurityCritical] get; }
  }
}
