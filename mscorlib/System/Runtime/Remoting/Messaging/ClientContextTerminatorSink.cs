// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ClientContextTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  internal class ClientContextTerminatorSink : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static volatile ClientContextTerminatorSink messageSink;

    internal static IMessageSink MessageSink
    {
      get
      {
        if (ClientContextTerminatorSink.messageSink == null)
        {
          ClientContextTerminatorSink contextTerminatorSink = new ClientContextTerminatorSink();
          lock (ClientContextTerminatorSink.staticSyncObject)
          {
            if (ClientContextTerminatorSink.messageSink == null)
              ClientContextTerminatorSink.messageSink = contextTerminatorSink;
          }
        }
        return (IMessageSink) ClientContextTerminatorSink.messageSink;
      }
    }

    [SecurityCritical]
    internal static object SyncProcessMessageCallback(object[] args)
    {
      IMessage msg = (IMessage) args[0];
      return (object) ((IMessageSink) args[1]).SyncProcessMessage(msg);
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message1 = InternalSink.ValidateMessage(reqMsg);
      if (message1 != null)
        return message1;
      Context currentContext = Thread.CurrentContext;
      bool flag = currentContext.NotifyDynamicSinks(reqMsg, true, true, false, true);
      IMessage msg;
      if (reqMsg is IConstructionCallMessage)
      {
        IMessage message2 = currentContext.NotifyActivatorProperties(reqMsg, false);
        if (message2 != null)
          return message2;
        msg = (IMessage) ((IConstructionCallMessage) reqMsg).Activator.Activate((IConstructionCallMessage) reqMsg);
        IMessage message3 = currentContext.NotifyActivatorProperties(msg, false);
        if (message3 != null)
          return message3;
      }
      else
      {
        ChannelServices.NotifyProfiler(reqMsg, RemotingProfilerEvent.ClientSend);
        object[] args = new object[2];
        IMessageSink channelSink = this.GetChannelSink(reqMsg);
        args[0] = (object) reqMsg;
        args[1] = (object) channelSink;
        InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(ClientContextTerminatorSink.SyncProcessMessageCallback);
        msg = channelSink == CrossContextChannel.MessageSink ? (IMessage) ftnToCall(args) : (IMessage) Thread.CurrentThread.InternalCrossContextCallback(Context.DefaultContext, ftnToCall, args);
        ChannelServices.NotifyProfiler(msg, RemotingProfilerEvent.ClientReceive);
      }
      if (flag)
        currentContext.NotifyDynamicSinks(reqMsg, true, false, false, true);
      return msg;
    }

    [SecurityCritical]
    internal static object AsyncProcessMessageCallback(object[] args)
    {
      IMessage msg = (IMessage) args[0];
      IMessageSink replySink = (IMessageSink) args[1];
      return (object) ((IMessageSink) args[2]).AsyncProcessMessage(msg, replySink);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessage msg = InternalSink.ValidateMessage(reqMsg);
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      if (msg == null)
        msg = InternalSink.DisallowAsyncActivation(reqMsg);
      if (msg != null)
      {
        replySink?.SyncProcessMessage(msg);
      }
      else
      {
        if (RemotingServices.CORProfilerTrackRemotingAsync())
        {
          Guid id;
          RemotingServices.CORProfilerRemotingClientSendingMessage(out id, true);
          if (RemotingServices.CORProfilerTrackRemotingCookie())
            reqMsg.Properties[(object) "CORProfilerCookie"] = (object) id;
          if (replySink != null)
            replySink = (IMessageSink) new ClientAsyncReplyTerminatorSink(replySink);
        }
        Context currentContext = Thread.CurrentContext;
        currentContext.NotifyDynamicSinks(reqMsg, true, true, true, true);
        if (replySink != null)
          replySink = (IMessageSink) new AsyncReplySink(replySink, currentContext);
        object[] args = new object[3];
        InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(ClientContextTerminatorSink.AsyncProcessMessageCallback);
        IMessageSink channelSink = this.GetChannelSink(reqMsg);
        args[0] = (object) reqMsg;
        args[1] = (object) replySink;
        args[2] = (object) channelSink;
        messageCtrl = channelSink == CrossContextChannel.MessageSink ? (IMessageCtrl) ftnToCall(args) : (IMessageCtrl) Thread.CurrentThread.InternalCrossContextCallback(Context.DefaultContext, ftnToCall, args);
      }
      return messageCtrl;
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecurityCritical]
    private IMessageSink GetChannelSink(IMessage reqMsg)
    {
      return InternalSink.GetIdentity(reqMsg).ChannelSink;
    }
  }
}
