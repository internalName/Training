// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.SynchronizedClientContextSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  internal class SynchronizedClientContextSink : InternalSink, IMessageSink
  {
    internal IMessageSink _nextSink;
    [SecurityCritical]
    internal SynchronizationAttribute _property;

    [SecurityCritical]
    internal SynchronizedClientContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
    {
      this._property = prop;
      this._nextSink = nextSink;
    }

    [SecuritySafeCritical]
    ~SynchronizedClientContextSink()
    {
      this._property.Dispose();
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message;
      if (this._property.IsReEntrant)
      {
        this._property.HandleThreadExit();
        message = this._nextSink.SyncProcessMessage(reqMsg);
        this._property.HandleThreadReEntry();
      }
      else
      {
        LogicalCallContext property = (LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey];
        string str = property.RemotingData.LogicalCallID;
        bool flag1 = false;
        if (str == null)
        {
          str = Identity.GetNewLogicalCallID();
          property.RemotingData.LogicalCallID = str;
          flag1 = true;
        }
        bool flag2 = false;
        if (this._property.SyncCallOutLCID == null)
        {
          this._property.SyncCallOutLCID = str;
          flag2 = true;
        }
        message = this._nextSink.SyncProcessMessage(reqMsg);
        if (flag2)
        {
          this._property.SyncCallOutLCID = (string) null;
          if (flag1)
            ((LogicalCallContext) message.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID = (string) null;
        }
      }
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      if (!this._property.IsReEntrant)
      {
        LogicalCallContext property = (LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey];
        string newLogicalCallId = Identity.GetNewLogicalCallID();
        property.RemotingData.LogicalCallID = newLogicalCallId;
        this._property.AsyncCallOutLCIDList.Add((object) newLogicalCallId);
      }
      SynchronizedClientContextSink.AsyncReplySink asyncReplySink = new SynchronizedClientContextSink.AsyncReplySink(replySink, this._property);
      return this._nextSink.AsyncProcessMessage(reqMsg, (IMessageSink) asyncReplySink);
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._nextSink;
      }
    }

    internal class AsyncReplySink : IMessageSink
    {
      internal IMessageSink _nextSink;
      [SecurityCritical]
      internal SynchronizationAttribute _property;

      [SecurityCritical]
      internal AsyncReplySink(IMessageSink nextSink, SynchronizationAttribute prop)
      {
        this._nextSink = nextSink;
        this._property = prop;
      }

      [SecurityCritical]
      public virtual IMessage SyncProcessMessage(IMessage reqMsg)
      {
        WorkItem work = new WorkItem(reqMsg, this._nextSink, (IMessageSink) null);
        this._property.HandleWorkRequest(work);
        if (!this._property.IsReEntrant)
          this._property.AsyncCallOutLCIDList.Remove((object) ((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID);
        return work.ReplyMessage;
      }

      [SecurityCritical]
      public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
      {
        throw new NotSupportedException();
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
}
