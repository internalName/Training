// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CRMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class CRMDictionary : MessageDictionary
  {
    public static string[] CRMkeysFault = new string[5]
    {
      "__Uri",
      "__MethodName",
      "__MethodSignature",
      "__TypeName",
      "__CallContext"
    };
    public static string[] CRMkeysNoFault = new string[7]
    {
      "__Uri",
      "__MethodName",
      "__MethodSignature",
      "__TypeName",
      "__Return",
      "__OutArgs",
      "__CallContext"
    };
    internal IConstructionReturnMessage _crmsg;
    internal bool fault;

    [SecurityCritical]
    public CRMDictionary(IConstructionReturnMessage msg, IDictionary idict)
      : base(msg.Exception != null ? CRMDictionary.CRMkeysFault : CRMDictionary.CRMkeysNoFault, idict)
    {
      this.fault = msg.Exception != null;
      this._crmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          return (object) this._crmsg.Uri;
        case 1:
          return (object) this._crmsg.MethodName;
        case 2:
          return this._crmsg.MethodSignature;
        case 3:
          return (object) this._crmsg.TypeName;
        case 4:
          if (!this.fault)
            return this._crmsg.ReturnValue;
          return (object) this.FetchLogicalCallContext();
        case 5:
          return (object) this._crmsg.Args;
        case 6:
          return (object) this.FetchLogicalCallContext();
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      ReturnMessage crmsg1 = this._crmsg as ReturnMessage;
      if (crmsg1 != null)
        return crmsg1.GetLogicalCallContext();
      MethodResponse crmsg2 = this._crmsg as MethodResponse;
      if (crmsg2 != null)
        return crmsg2.GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      ReturnMessage crmsg1 = this._crmsg as ReturnMessage;
      MethodResponse crmsg2 = this._crmsg as MethodResponse;
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        if (crmsg1 != null)
        {
          crmsg1.SetLogicalCallContext((LogicalCallContext) value);
        }
        else
        {
          if (crmsg2 == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
          crmsg2.SetLogicalCallContext((LogicalCallContext) value);
        }
      }
      else if (crmsg1 != null)
      {
        crmsg1.Uri = (string) value;
      }
      else
      {
        if (crmsg2 == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        crmsg2.Uri = (string) value;
      }
    }
  }
}
