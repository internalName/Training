// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MRMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MRMDictionary : MessageDictionary
  {
    public static string[] MCMkeysFault = new string[1]
    {
      "__CallContext"
    };
    public static string[] MCMkeysNoFault = new string[7]
    {
      "__Uri",
      "__MethodName",
      "__MethodSignature",
      "__TypeName",
      "__Return",
      "__OutArgs",
      "__CallContext"
    };
    internal IMethodReturnMessage _mrmsg;
    internal bool fault;

    [SecurityCritical]
    public MRMDictionary(IMethodReturnMessage msg, IDictionary idict)
      : base(msg.Exception != null ? MRMDictionary.MCMkeysFault : MRMDictionary.MCMkeysNoFault, idict)
    {
      this.fault = msg.Exception != null;
      this._mrmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          if (this.fault)
            return (object) this.FetchLogicalCallContext();
          return (object) this._mrmsg.Uri;
        case 1:
          return (object) this._mrmsg.MethodName;
        case 2:
          return this._mrmsg.MethodSignature;
        case 3:
          return (object) this._mrmsg.TypeName;
        case 4:
          if (this.fault)
            return (object) this._mrmsg.Exception;
          return this._mrmsg.ReturnValue;
        case 5:
          return (object) this._mrmsg.Args;
        case 6:
          return (object) this.FetchLogicalCallContext();
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      ReturnMessage mrmsg1 = this._mrmsg as ReturnMessage;
      if (mrmsg1 != null)
        return mrmsg1.GetLogicalCallContext();
      MethodResponse mrmsg2 = this._mrmsg as MethodResponse;
      if (mrmsg2 != null)
        return mrmsg2.GetLogicalCallContext();
      StackBasedReturnMessage mrmsg3 = this._mrmsg as StackBasedReturnMessage;
      if (mrmsg3 != null)
        return mrmsg3.GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      ReturnMessage mrmsg1 = this._mrmsg as ReturnMessage;
      MethodResponse mrmsg2 = this._mrmsg as MethodResponse;
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        if (mrmsg1 != null)
        {
          mrmsg1.SetLogicalCallContext((LogicalCallContext) value);
        }
        else
        {
          if (mrmsg2 == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
          mrmsg2.SetLogicalCallContext((LogicalCallContext) value);
        }
      }
      else if (mrmsg1 != null)
      {
        mrmsg1.Uri = (string) value;
      }
      else
      {
        if (mrmsg2 == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        mrmsg2.Uri = (string) value;
      }
    }
  }
}
