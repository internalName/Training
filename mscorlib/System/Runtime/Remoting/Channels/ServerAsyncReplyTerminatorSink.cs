// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ServerAsyncReplyTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal class ServerAsyncReplyTerminatorSink : IMessageSink
  {
    internal IMessageSink _nextSink;

    internal ServerAsyncReplyTerminatorSink(IMessageSink nextSink)
    {
      this._nextSink = nextSink;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage replyMsg)
    {
      Guid id;
      RemotingServices.CORProfilerRemotingServerSendingReply(out id, true);
      if (RemotingServices.CORProfilerTrackRemotingCookie())
        replyMsg.Properties[(object) "CORProfilerCookie"] = (object) id;
      return this._nextSink.SyncProcessMessage(replyMsg);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
    {
      return (IMessageCtrl) null;
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._nextSink;
      }
    }
  }
}
