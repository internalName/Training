// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.InternalSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class InternalSink
  {
    [SecurityCritical]
    internal static IMessage ValidateMessage(IMessage reqMsg)
    {
      IMessage message = (IMessage) null;
      if (reqMsg == null)
        message = (IMessage) new ReturnMessage((Exception) new ArgumentNullException(nameof (reqMsg)), (IMethodCallMessage) null);
      return message;
    }

    [SecurityCritical]
    internal static IMessage DisallowAsyncActivation(IMessage reqMsg)
    {
      if (reqMsg is IConstructionCallMessage)
        return (IMessage) new ReturnMessage((Exception) new RemotingException(Environment.GetResourceString("Remoting_Activation_AsyncUnsupported")), (IMethodCallMessage) null);
      return (IMessage) null;
    }

    [SecurityCritical]
    internal static Identity GetIdentity(IMessage reqMsg)
    {
      Identity identity = (Identity) null;
      if (reqMsg is IInternalMessage)
        identity = ((IInternalMessage) reqMsg).IdentityObject;
      else if (reqMsg is InternalMessageWrapper)
        identity = (Identity) ((InternalMessageWrapper) reqMsg).GetIdentityObject();
      if (identity == null)
      {
        string uri = InternalSink.GetURI(reqMsg);
        identity = IdentityHolder.ResolveIdentity(uri);
        if (identity == null)
          throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", (object) uri));
      }
      return identity;
    }

    [SecurityCritical]
    internal static ServerIdentity GetServerIdentity(IMessage reqMsg)
    {
      ServerIdentity serverIdentity = (ServerIdentity) null;
      bool flag = false;
      IInternalMessage internalMessage = reqMsg as IInternalMessage;
      if (internalMessage != null)
      {
        serverIdentity = ((IInternalMessage) reqMsg).ServerIdentityObject;
        flag = true;
      }
      else if (reqMsg is InternalMessageWrapper)
        serverIdentity = (ServerIdentity) ((InternalMessageWrapper) reqMsg).GetServerIdentityObject();
      if (serverIdentity == null)
      {
        Identity identity = IdentityHolder.ResolveIdentity(InternalSink.GetURI(reqMsg));
        if (identity is ServerIdentity)
        {
          serverIdentity = (ServerIdentity) identity;
          if (flag)
            internalMessage.ServerIdentityObject = serverIdentity;
        }
      }
      return serverIdentity;
    }

    [SecurityCritical]
    internal static string GetURI(IMessage msg)
    {
      string str = (string) null;
      IMethodMessage methodMessage = msg as IMethodMessage;
      if (methodMessage != null)
      {
        str = methodMessage.Uri;
      }
      else
      {
        IDictionary properties = msg.Properties;
        if (properties != null)
          str = (string) properties[(object) "__Uri"];
      }
      return str;
    }
  }
}
