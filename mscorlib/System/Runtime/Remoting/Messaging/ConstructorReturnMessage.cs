// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ConstructorReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [SecurityCritical]
  internal class ConstructorReturnMessage : ReturnMessage, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
  {
    private const int Intercept = 1;
    private MarshalByRefObject _o;
    private int _iFlags;

    public ConstructorReturnMessage(MarshalByRefObject o, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IConstructionCallMessage ccm)
      : base((object) o, outArgs, outArgsCount, callCtx, (IMethodCallMessage) ccm)
    {
      this._o = o;
      this._iFlags = 1;
    }

    public ConstructorReturnMessage(Exception e, IConstructionCallMessage ccm)
      : base(e, (IMethodCallMessage) ccm)
    {
    }

    public override object ReturnValue
    {
      [SecurityCritical] get
      {
        if (this._iFlags == 1)
          return (object) RemotingServices.MarshalInternal(this._o, (string) null, (Type) null);
        return base.ReturnValue;
      }
    }

    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          Interlocked.CompareExchange(ref this._properties, (object) new CRMDictionary((IConstructionReturnMessage) this, (IDictionary) new Hashtable()), (object) null);
        return (IDictionary) this._properties;
      }
    }

    internal object GetObject()
    {
      return (object) this._o;
    }
  }
}
