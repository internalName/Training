// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ADAsyncWorkItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class ADAsyncWorkItem
  {
    private IMessageSink _replySink;
    private IMessageSink _nextSink;
    [SecurityCritical]
    private LogicalCallContext _callCtx;
    private IMessage _reqMsg;

    [SecurityCritical]
    internal ADAsyncWorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
    {
      this._reqMsg = reqMsg;
      this._nextSink = nextSink;
      this._replySink = replySink;
      this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
    }

    [SecurityCritical]
    internal virtual void FinishAsyncWork(object stateIgnored)
    {
      LogicalCallContext callCtx = CallContext.SetLogicalCallContext(this._callCtx);
      IMessage msg = this._nextSink.SyncProcessMessage(this._reqMsg);
      if (this._replySink != null)
        this._replySink.SyncProcessMessage(msg);
      CallContext.SetLogicalCallContext(callCtx);
    }
  }
}
