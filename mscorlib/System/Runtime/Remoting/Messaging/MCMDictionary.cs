// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MCMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MCMDictionary : MessageDictionary
  {
    public static string[] MCMkeys = new string[6]
    {
      "__Uri",
      "__MethodName",
      "__MethodSignature",
      "__TypeName",
      "__Args",
      "__CallContext"
    };
    internal IMethodCallMessage _mcmsg;

    public MCMDictionary(IMethodCallMessage msg, IDictionary idict)
      : base(MCMDictionary.MCMkeys, idict)
    {
      this._mcmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          return (object) this._mcmsg.Uri;
        case 1:
          return (object) this._mcmsg.MethodName;
        case 2:
          return this._mcmsg.MethodSignature;
        case 3:
          return (object) this._mcmsg.TypeName;
        case 4:
          return (object) this._mcmsg.Args;
        case 5:
          return (object) this.FetchLogicalCallContext();
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      Message mcmsg1 = this._mcmsg as Message;
      if (mcmsg1 != null)
        return mcmsg1.GetLogicalCallContext();
      MethodCall mcmsg2 = this._mcmsg as MethodCall;
      if (mcmsg2 != null)
        return mcmsg2.GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      Message mcmsg1 = this._mcmsg as Message;
      MethodCall mcmsg2 = this._mcmsg as MethodCall;
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        if (mcmsg1 == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        mcmsg1.SetLogicalCallContext((LogicalCallContext) value);
      }
      else if (mcmsg1 != null)
      {
        mcmsg1.Uri = (string) value;
      }
      else
      {
        if (mcmsg2 == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        mcmsg2.Uri = (string) value;
      }
    }
  }
}
