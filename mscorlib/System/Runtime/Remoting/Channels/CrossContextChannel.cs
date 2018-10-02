// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossContextChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class CrossContextChannel : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossContextChannel.SyncProcessMessageCallback);
    private const string _channelName = "XCTX";
    private const int _channelCapability = 0;
    private const string _channelURI = "XCTX_URI";

    [SecuritySafeCritical]
    static CrossContextChannel()
    {
    }

    private static CrossContextChannel messageSink
    {
      get
      {
        return Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink;
      }
      set
      {
        Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink = value;
      }
    }

    internal static IMessageSink MessageSink
    {
      get
      {
        if (CrossContextChannel.messageSink == null)
        {
          CrossContextChannel crossContextChannel = new CrossContextChannel();
          lock (CrossContextChannel.staticSyncObject)
          {
            if (CrossContextChannel.messageSink == null)
              CrossContextChannel.messageSink = crossContextChannel;
          }
        }
        return (IMessageSink) CrossContextChannel.messageSink;
      }
    }

    [SecurityCritical]
    internal static object SyncProcessMessageCallback(object[] args)
    {
      IMessage msg1 = args[0] as IMessage;
      Context context = args[1] as Context;
      if (RemotingServices.CORProfilerTrackRemoting())
      {
        Guid id = Guid.Empty;
        if (RemotingServices.CORProfilerTrackRemotingCookie())
        {
          object property = msg1.Properties[(object) "CORProfilerCookie"];
          if (property != null)
            id = (Guid) property;
        }
        RemotingServices.CORProfilerRemotingServerReceivingMessage(id, false);
      }
      context.NotifyDynamicSinks(msg1, false, true, false, true);
      IMessage msg2 = context.GetServerContextChain().SyncProcessMessage(msg1);
      context.NotifyDynamicSinks(msg2, false, false, false, true);
      if (RemotingServices.CORProfilerTrackRemoting())
      {
        Guid id;
        RemotingServices.CORProfilerRemotingServerSendingReply(out id, false);
        if (RemotingServices.CORProfilerTrackRemotingCookie())
          msg2.Properties[(object) "CORProfilerCookie"] = (object) id;
      }
      return (object) msg2;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      object[] args = new object[2];
      IMessage message1;
      try
      {
        IMessage message2 = InternalSink.ValidateMessage(reqMsg);
        if (message2 != null)
          return message2;
        ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
        args[0] = (object) reqMsg;
        args[1] = (object) serverIdentity.ServerContext;
        message1 = (IMessage) Thread.CurrentThread.InternalCrossContextCallback(serverIdentity.ServerContext, CrossContextChannel.s_xctxDel, args);
      }
      catch (Exception ex)
      {
        message1 = (IMessage) new ReturnMessage(ex, (IMethodCallMessage) reqMsg);
        if (reqMsg != null)
          ((ReturnMessage) message1).SetLogicalCallContext((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]);
      }
      return message1;
    }

    [SecurityCritical]
    internal static object AsyncProcessMessageCallback(object[] args)
    {
      AsyncWorkItem asyncWorkItem = (AsyncWorkItem) null;
      IMessage msg = (IMessage) args[0];
      IMessageSink replySink = (IMessageSink) args[1];
      Context oldCtx = (Context) args[2];
      Context context = (Context) args[3];
      if (replySink != null)
        asyncWorkItem = new AsyncWorkItem(replySink, oldCtx);
      context.NotifyDynamicSinks(msg, false, true, true, true);
      return (object) context.GetServerContextChain().AsyncProcessMessage(msg, (IMessageSink) asyncWorkItem);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessage msg = InternalSink.ValidateMessage(reqMsg);
      object[] args = new object[4];
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      if (msg != null)
      {
        replySink?.SyncProcessMessage(msg);
      }
      else
      {
        ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
        if (RemotingServices.CORProfilerTrackRemotingAsync())
        {
          Guid id = Guid.Empty;
          if (RemotingServices.CORProfilerTrackRemotingCookie())
          {
            object property = reqMsg.Properties[(object) "CORProfilerCookie"];
            if (property != null)
              id = (Guid) property;
          }
          RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
          if (replySink != null)
            replySink = (IMessageSink) new ServerAsyncReplyTerminatorSink(replySink);
        }
        Context serverContext = serverIdentity.ServerContext;
        if (serverContext.IsThreadPoolAware)
        {
          args[0] = (object) reqMsg;
          args[1] = (object) replySink;
          args[2] = (object) Thread.CurrentContext;
          args[3] = (object) serverContext;
          InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.AsyncProcessMessageCallback);
          messageCtrl = (IMessageCtrl) Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, args);
        }
        else
          ThreadPool.QueueUserWorkItem(new WaitCallback(new AsyncWorkItem(reqMsg, replySink, Thread.CurrentContext, serverIdentity).FinishAsyncWork));
      }
      return messageCtrl;
    }

    [SecurityCritical]
    internal static object DoAsyncDispatchCallback(object[] args)
    {
      AsyncWorkItem asyncWorkItem = (AsyncWorkItem) null;
      IMessage msg = (IMessage) args[0];
      IMessageSink replySink = (IMessageSink) args[1];
      Context oldCtx = (Context) args[2];
      Context context = (Context) args[3];
      if (replySink != null)
        asyncWorkItem = new AsyncWorkItem(replySink, oldCtx);
      return (object) context.GetServerContextChain().AsyncProcessMessage(msg, (IMessageSink) asyncWorkItem);
    }

    [SecurityCritical]
    internal static IMessageCtrl DoAsyncDispatch(IMessage reqMsg, IMessageSink replySink)
    {
      object[] args = new object[4];
      ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
      if (RemotingServices.CORProfilerTrackRemotingAsync())
      {
        Guid id = Guid.Empty;
        if (RemotingServices.CORProfilerTrackRemotingCookie())
        {
          object property = reqMsg.Properties[(object) "CORProfilerCookie"];
          if (property != null)
            id = (Guid) property;
        }
        RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
        if (replySink != null)
          replySink = (IMessageSink) new ServerAsyncReplyTerminatorSink(replySink);
      }
      Context serverContext = serverIdentity.ServerContext;
      args[0] = (object) reqMsg;
      args[1] = (object) replySink;
      args[2] = (object) Thread.CurrentContext;
      args[3] = (object) serverContext;
      InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.DoAsyncDispatchCallback);
      return (IMessageCtrl) Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, args);
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }
  }
}
