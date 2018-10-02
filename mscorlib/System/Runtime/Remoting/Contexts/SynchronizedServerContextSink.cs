// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.SynchronizedServerContextSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  internal class SynchronizedServerContextSink : InternalSink, IMessageSink
  {
    internal IMessageSink _nextSink;
    [SecurityCritical]
    internal SynchronizationAttribute _property;

    [SecurityCritical]
    internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
    {
      this._property = prop;
      this._nextSink = nextSink;
    }

    [SecuritySafeCritical]
    ~SynchronizedServerContextSink()
    {
      this._property.Dispose();
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      WorkItem work = new WorkItem(reqMsg, this._nextSink, (IMessageSink) null);
      this._property.HandleWorkRequest(work);
      return work.ReplyMessage;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      WorkItem work = new WorkItem(reqMsg, this._nextSink, replySink);
      work.SetAsync();
      this._property.HandleWorkRequest(work);
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
