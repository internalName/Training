// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ClientChannelSinkStack
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
  ///   Хранит стек клиента приемников каналов, который должен вызываться при декодировании асинхронного ответа.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
  {
    private ClientChannelSinkStack.SinkStack _stack;
    private IMessageSink _replySink;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> со значениями по умолчанию.
    /// </summary>
    public ClientChannelSinkStack()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> класса с указанным приемником ответа.
    /// </summary>
    /// <param name="replySink">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> Текущего стека можно использовать для ответа на сообщения.
    /// </param>
    public ClientChannelSinkStack(IMessageSink replySink)
    {
      this._replySink = replySink;
    }

    /// <summary>
    ///   Переносит указанный приемник и сведения, связанные с ним, в стек приемников.
    /// </summary>
    /// <param name="sink">
    ///   Приемник, который необходимо поместить в стек приемников.
    /// </param>
    /// <param name="state">
    ///   Сведения, созданные на стороне запроса, которые требуются на стороне ответа.
    /// </param>
    [SecurityCritical]
    public void Push(IClientChannelSink sink, object state)
    {
      this._stack = new ClientChannelSinkStack.SinkStack()
      {
        PrevStack = this._stack,
        Sink = sink,
        State = state
      };
    }

    /// <summary>
    ///   Выводит сведения, связанные со всеми приемниками: от стека приемников до и включая указанный приемник.
    /// </summary>
    /// <param name="sink">
    ///   Приемник, удаляемый и возвращаемый из стека приемников.
    /// </param>
    /// <returns>
    ///   Сведения, созданные на стороне запроса и связанные с указанным приемником.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Текущий стек приемников пуст, или указанный приемник не был передан в текущий стек.
    /// </exception>
    [SecurityCritical]
    public object Pop(IClientChannelSink sink)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
      while (this._stack.Sink != sink)
      {
        this._stack = this._stack.PrevStack;
        if (this._stack == null)
          break;
      }
      if (this._stack.Sink == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
      object state = this._stack.State;
      this._stack = this._stack.PrevStack;
      return state;
    }

    /// <summary>
    ///   Запрашивает асинхронную обработку метода вызова на приемники в текущем стеке приемников.
    /// </summary>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <param name="stream">
    ///   Поток, который возвращается из транспортного приемника.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Текущий стек приемников пуст.
    /// </exception>
    [SecurityCritical]
    public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
    {
      if (this._replySink == null)
        return;
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
      IClientChannelSink sink = this._stack.Sink;
      object state = this._stack.State;
      this._stack = this._stack.PrevStack;
      sink.AsyncProcessResponse((IClientResponseChannelSinkStack) this, state, headers, stream);
    }

    /// <summary>Отправляет указанный ответ в приемник ответов.</summary>
    /// <param name="msg">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> Для диспетчеризации.
    /// </param>
    [SecurityCritical]
    public void DispatchReplyMessage(IMessage msg)
    {
      if (this._replySink == null)
        return;
      this._replySink.SyncProcessMessage(msg);
    }

    /// <summary>Отправляет указанное исключение в приемник ответов.</summary>
    /// <param name="e">Исключение, отправляемое на сервер.</param>
    [SecurityCritical]
    public void DispatchException(Exception e)
    {
      this.DispatchReplyMessage((IMessage) new ReturnMessage(e, (IMethodCallMessage) null));
    }

    private class SinkStack
    {
      public ClientChannelSinkStack.SinkStack PrevStack;
      public IClientChannelSink Sink;
      public object State;
    }
  }
}
