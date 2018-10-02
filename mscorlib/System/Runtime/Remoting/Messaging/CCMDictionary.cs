// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CCMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class CCMDictionary : MessageDictionary
  {
    public static string[] CCMkeys = new string[11]
    {
      "__Uri",
      "__MethodName",
      "__MethodSignature",
      "__TypeName",
      "__Args",
      "__CallContext",
      "__CallSiteActivationAttributes",
      "__ActivationType",
      "__ContextProperties",
      "__Activator",
      "__ActivationTypeName"
    };
    internal IConstructionCallMessage _ccmsg;

    public CCMDictionary(IConstructionCallMessage msg, IDictionary idict)
      : base(CCMDictionary.CCMkeys, idict)
    {
      this._ccmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          return (object) this._ccmsg.Uri;
        case 1:
          return (object) this._ccmsg.MethodName;
        case 2:
          return this._ccmsg.MethodSignature;
        case 3:
          return (object) this._ccmsg.TypeName;
        case 4:
          return (object) this._ccmsg.Args;
        case 5:
          return (object) this.FetchLogicalCallContext();
        case 6:
          return (object) this._ccmsg.CallSiteActivationAttributes;
        case 7:
          return (object) null;
        case 8:
          return (object) this._ccmsg.ContextProperties;
        case 9:
          return (object) this._ccmsg.Activator;
        case 10:
          return (object) this._ccmsg.ActivationTypeName;
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      ConstructorCallMessage ccmsg = this._ccmsg as ConstructorCallMessage;
      if (ccmsg != null)
        return ccmsg.GetLogicalCallContext();
      if (this._ccmsg is ConstructionCall)
        return ((MethodCall) this._ccmsg).GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        ((ConstructorCallMessage) this._ccmsg).SetLogicalCallContext((LogicalCallContext) value);
      }
      else
        ((ConstructorCallMessage) this._ccmsg).Uri = (string) value;
    }
  }
}
