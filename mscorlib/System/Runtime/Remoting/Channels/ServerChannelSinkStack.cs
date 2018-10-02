// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ServerChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>Хранит стек приемников каналов сервера.</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
  {
    private ServerChannelSinkStack.SinkStack _stack;
    private ServerChannelSinkStack.SinkStack _rememberedStack;
    private IMessage _asyncMsg;
    private MethodInfo _asyncEnd;
    private object _serverObject;
    private IMethodCallMessage _msg;

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
    public void Push(IServerChannelSink sink, object state)
    {
      this._stack = new ServerChannelSinkStack.SinkStack()
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
    public object Pop(IServerChannelSink sink)
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
    ///   Сохраняет приемник сообщений и связанное с ним состояние для последующей асинхронной обработки.
    /// </summary>
    /// <param name="sink">Приемник канала сервера.</param>
    /// <param name="state">
    ///   Состояние, связанное с <paramref name="sink" />.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Текущий стек приемников пуст.
    /// 
    ///   -или-
    /// 
    ///   Указанный приемник не был передан в текущий стек.
    /// </exception>
    [SecurityCritical]
    public void Store(IServerChannelSink sink, object state)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnEmptySinkStack"));
      while (this._stack.Sink != sink)
      {
        this._stack = this._stack.PrevStack;
        if (this._stack == null)
          break;
      }
      if (this._stack.Sink == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnSinkStackWithoutPush"));
      this._rememberedStack = new ServerChannelSinkStack.SinkStack()
      {
        PrevStack = this._rememberedStack,
        Sink = sink,
        State = state
      };
      this.Pop(sink);
    }

    /// <summary>
    ///   Сохраняет приемник сообщений и связанное состояние, а затем отправляет сообщение асинхронно с помощью только что сохраненного приемника и других сохраненных приемников.
    /// </summary>
    /// <param name="sink">Приемник канала сервера.</param>
    /// <param name="state">
    ///   Состояние, связанное с <paramref name="sink" />.
    /// </param>
    [SecurityCritical]
    public void StoreAndDispatch(IServerChannelSink sink, object state)
    {
      this.Store(sink, state);
      this.FlipRememberedStack();
      CrossContextChannel.DoAsyncDispatch(this._asyncMsg, (IMessageSink) null);
    }

    private void FlipRememberedStack()
    {
      if (this._stack != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallFRSWhenStackEmtpy"));
      for (; this._rememberedStack != null; this._rememberedStack = this._rememberedStack.PrevStack)
        this._stack = new ServerChannelSinkStack.SinkStack()
        {
          PrevStack = this._stack,
          Sink = this._rememberedStack.Sink,
          State = this._rememberedStack.State
        };
    }

    /// <summary>
    ///   Запрашивает асинхронную обработку метода вызова на приемники в текущем стеке приемников.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, Упорядочиваемое на запрошенном потоке.
    /// </param>
    /// <param name="headers">
    ///   Заголовки, извлекаемые из потока ответа сервера.
    /// </param>
    /// <param name="stream">
    ///   Поток возвращается из транспортного приемника.
    /// </param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Текущий стек приемников пуст.
    /// </exception>
    [SecurityCritical]
    public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
      IServerChannelSink sink = this._stack.Sink;
      object state = this._stack.State;
      this._stack = this._stack.PrevStack;
      sink.AsyncProcessResponse((IServerResponseChannelSinkStack) this, state, msg, headers, stream);
    }

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
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Стек приемников пуст.
    /// </exception>
    [SecurityCritical]
    public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallGetResponseStreamWhenStackEmpty"));
      IServerChannelSink sink = this._stack.Sink;
      object state = this._stack.State;
      this._stack = this._stack.PrevStack;
      Stream responseStream = sink.GetResponseStream((IServerResponseChannelSinkStack) this, state, msg, headers);
      this.Push(sink, state);
      return responseStream;
    }

    internal object ServerObject
    {
      set
      {
        this._serverObject = value;
      }
    }

    /// <summary>
    ///   Предоставляет <see cref="T:System.AsyncCallback" /> делегата для обработки обратного вызова после асинхронно отправлено сообщение.
    /// </summary>
    /// <param name="ar">
    ///   Статус и состояние асинхронной операции для удаленного объекта.
    /// </param>
    [SecurityCritical]
    public void ServerCallback(IAsyncResult ar)
    {
      if (!(this._asyncEnd != (MethodInfo) null))
        return;
      RemotingMethodCachedData reflectionCachedData1 = InternalRemotingServices.GetReflectionCachedData((MethodBase) this._asyncEnd);
      RemotingMethodCachedData reflectionCachedData2 = InternalRemotingServices.GetReflectionCachedData(this._msg.MethodBase);
      ParameterInfo[] parameters = reflectionCachedData1.Parameters;
      object[] objArray = new object[parameters.Length];
      objArray[parameters.Length - 1] = (object) ar;
      object[] args = this._msg.Args;
      AsyncMessageHelper.GetOutArgs(reflectionCachedData2.Parameters, args, objArray);
      StackBuilderSink stackBuilderSink = new StackBuilderSink(this._serverObject);
      object[] outArgs;
      object ret = stackBuilderSink.PrivateProcessMessage(this._asyncEnd.MethodHandle, Message.CoerceArgs((MethodBase) this._asyncEnd, objArray, parameters), this._serverObject, out outArgs);
      if (outArgs != null)
        outArgs = ArgMapper.ExpandAsyncEndArgsToSyncArgs(reflectionCachedData2, outArgs);
      stackBuilderSink.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData2, args, ref outArgs);
      this.AsyncProcessResponse((IMessage) new ReturnMessage(ret, outArgs, this._msg.ArgCount, Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext, this._msg), (ITransportHeaders) null, (Stream) null);
    }

    private class SinkStack
    {
      public ServerChannelSinkStack.SinkStack PrevStack;
      public IServerChannelSink Sink;
      public object State;
    }
  }
}
