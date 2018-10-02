// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.DisposeSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class DisposeSink : IMessageSink
  {
    private IDisposable _iDis;
    private IMessageSink _replySink;

    internal DisposeSink(IDisposable iDis, IMessageSink replySink)
    {
      this._iDis = iDis;
      this._replySink = replySink;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message = (IMessage) null;
      try
      {
        if (this._replySink != null)
          message = this._replySink.SyncProcessMessage(reqMsg);
      }
      finally
      {
        this._iDis.Dispose();
      }
      return message;
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
        return this._replySink;
      }
    }
  }
}
