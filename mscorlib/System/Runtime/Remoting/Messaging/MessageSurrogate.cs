// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MessageSurrogate : ISerializationSurrogate
  {
    private static Type _constructionCallType = typeof (ConstructionCall);
    private static Type _methodCallType = typeof (MethodCall);
    private static Type _constructionResponseType = typeof (ConstructionResponse);
    private static Type _methodResponseType = typeof (MethodResponse);
    private static Type _exceptionType = typeof (Exception);
    private static Type _objectType = typeof (object);
    [SecurityCritical]
    private RemotingSurrogateSelector _ss;

    [SecuritySafeCritical]
    static MessageSurrogate()
    {
    }

    [SecurityCritical]
    internal MessageSurrogate(RemotingSurrogateSelector ss)
    {
      this._ss = ss;
    }

    [SecurityCritical]
    public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      bool flag1 = false;
      bool flag2 = false;
      IMethodMessage msg = obj as IMethodMessage;
      if (msg == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
      IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
      if (msg is IMethodCallMessage)
      {
        if (obj is IConstructionCallMessage)
          flag2 = true;
        info.SetType(flag2 ? MessageSurrogate._constructionCallType : MessageSurrogate._methodCallType);
      }
      else
      {
        if (!(msg is IMethodReturnMessage))
          throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
        flag1 = true;
        info.SetType(obj is IConstructionReturnMessage ? MessageSurrogate._constructionResponseType : MessageSurrogate._methodResponseType);
        if (((IMethodReturnMessage) msg).Exception != null)
          info.AddValue("__fault", (object) ((IMethodReturnMessage) msg).Exception, MessageSurrogate._exceptionType);
      }
      while (enumerator.MoveNext())
      {
        if (obj != this._ss.GetRootObject() || this._ss.Filter == null || !this._ss.Filter((string) enumerator.Key, enumerator.Value))
        {
          if (enumerator.Value != null)
          {
            string name = enumerator.Key.ToString();
            if (name.Equals("__CallContext"))
            {
              LogicalCallContext logicalCallContext = (LogicalCallContext) enumerator.Value;
              if (logicalCallContext.HasInfo)
                info.AddValue(name, (object) logicalCallContext);
              else
                info.AddValue(name, (object) logicalCallContext.RemotingData.LogicalCallID);
            }
            else if (name.Equals("__MethodSignature"))
            {
              if (flag2 || RemotingServices.IsMethodOverloaded(msg))
                info.AddValue(name, enumerator.Value);
            }
            else
            {
              flag1 = flag1;
              info.AddValue(name, enumerator.Value);
            }
          }
          else
            info.AddValue(enumerator.Key.ToString(), enumerator.Value, MessageSurrogate._objectType);
        }
      }
    }

    [SecurityCritical]
    public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
    }
  }
}
