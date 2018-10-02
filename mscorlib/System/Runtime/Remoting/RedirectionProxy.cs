// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RedirectionProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting
{
  internal class RedirectionProxy : MarshalByRefObject, IMessageSink
  {
    private MarshalByRefObject _proxy;
    [SecurityCritical]
    private RealProxy _realProxy;
    private Type _serverType;
    private WellKnownObjectMode _objectMode;

    [SecurityCritical]
    internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
    {
      this._proxy = proxy;
      this._realProxy = RemotingServices.GetRealProxy((object) this._proxy);
      this._serverType = serverType;
      this._objectMode = WellKnownObjectMode.Singleton;
    }

    public WellKnownObjectMode ObjectMode
    {
      set
      {
        this._objectMode = value;
      }
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      IMessage message;
      try
      {
        msg.Properties[(object) "__Uri"] = (object) this._realProxy.IdentityObject.URI;
        message = this._objectMode != WellKnownObjectMode.Singleton ? RemotingServices.GetRealProxy((object) (MarshalByRefObject) Activator.CreateInstance(this._serverType, true)).Invoke(msg) : this._realProxy.Invoke(msg);
      }
      catch (Exception ex)
      {
        message = (IMessage) new ReturnMessage(ex, msg as IMethodCallMessage);
      }
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      IMessage msg1 = this.SyncProcessMessage(msg);
      replySink?.SyncProcessMessage(msg1);
      return (IMessageCtrl) null;
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
