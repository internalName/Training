// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.AsyncWorkItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class AsyncWorkItem : IMessageSink
  {
    private IMessageSink _replySink;
    private ServerIdentity _srvID;
    private Context _oldCtx;
    [SecurityCritical]
    private LogicalCallContext _callCtx;
    private IMessage _reqMsg;

    [SecurityCritical]
    internal AsyncWorkItem(IMessageSink replySink, Context oldCtx)
      : this((IMessage) null, replySink, oldCtx, (ServerIdentity) null)
    {
    }

    [SecurityCritical]
    internal AsyncWorkItem(IMessage reqMsg, IMessageSink replySink, Context oldCtx, ServerIdentity srvID)
    {
      this._reqMsg = reqMsg;
      this._replySink = replySink;
      this._oldCtx = oldCtx;
      this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      this._srvID = srvID;
    }

    [SecurityCritical]
    internal static object SyncProcessMessageCallback(object[] args)
    {
      return (object) ((IMessageSink) args[0]).SyncProcessMessage((IMessage) args[1]);
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      IMessage message = (IMessage) null;
      if (this._replySink != null)
      {
        Thread.CurrentContext.NotifyDynamicSinks(msg, false, false, true, true);
        message = (IMessage) Thread.CurrentThread.InternalCrossContextCallback(this._oldCtx, new InternalCrossContextDelegate(AsyncWorkItem.SyncProcessMessageCallback), new object[2]
        {
          (object) this._replySink,
          (object) msg
        });
      }
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._replySink;
      }
    }

    [SecurityCritical]
    internal static object FinishAsyncWorkCallback(object[] args)
    {
      AsyncWorkItem asyncWorkItem = (AsyncWorkItem) args[0];
      Context serverContext = asyncWorkItem._srvID.ServerContext;
      LogicalCallContext callCtx = CallContext.SetLogicalCallContext(asyncWorkItem._callCtx);
      serverContext.NotifyDynamicSinks(asyncWorkItem._reqMsg, false, true, true, true);
      serverContext.GetServerContextChain().AsyncProcessMessage(asyncWorkItem._reqMsg, (IMessageSink) asyncWorkItem);
      CallContext.SetLogicalCallContext(callCtx);
      return (object) null;
    }

    [SecurityCritical]
    internal virtual void FinishAsyncWork(object stateIgnored)
    {
      Thread.CurrentThread.InternalCrossContextCallback(this._srvID.ServerContext, new InternalCrossContextDelegate(AsyncWorkItem.FinishAsyncWorkCallback), new object[1]
      {
        (object) this
      });
    }
  }
}
