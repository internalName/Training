// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ServerObjectTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class ServerObjectTerminatorSink : InternalSink, IMessageSink
  {
    internal StackBuilderSink _stackBuilderSink;

    internal ServerObjectTerminatorSink(MarshalByRefObject srvObj)
    {
      this._stackBuilderSink = new StackBuilderSink(srvObj);
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message = InternalSink.ValidateMessage(reqMsg);
      if (message != null)
        return message;
      ArrayWithSize sideDynamicSinks = InternalSink.GetServerIdentity(reqMsg).ServerSideDynamicSinks;
      if (sideDynamicSinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, sideDynamicSinks, false, true, false);
      IMessageSink serverObject = this._stackBuilderSink.ServerObject as IMessageSink;
      IMessage msg = serverObject == null ? this._stackBuilderSink.SyncProcessMessage(reqMsg) : serverObject.SyncProcessMessage(reqMsg);
      if (sideDynamicSinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(msg, sideDynamicSinks, false, false, false);
      return msg;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      IMessage msg = InternalSink.ValidateMessage(reqMsg);
      if (msg != null)
      {
        replySink?.SyncProcessMessage(msg);
      }
      else
      {
        IMessageSink serverObject = this._stackBuilderSink.ServerObject as IMessageSink;
        messageCtrl = serverObject == null ? this._stackBuilderSink.AsyncProcessMessage(reqMsg, replySink) : serverObject.AsyncProcessMessage(reqMsg, replySink);
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
  }
}
