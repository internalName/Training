// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.RemotingProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
  [SecurityCritical]
  internal class RemotingProxy : RealProxy, IRemotingTypeInfo
  {
    private static MethodInfo _getTypeMethod = typeof (object).GetMethod("GetType");
    private static MethodInfo _getHashCodeMethod = typeof (object).GetMethod("GetHashCode");
    private static RuntimeType s_typeofObject = (RuntimeType) typeof (object);
    private static RuntimeType s_typeofMarshalByRefObject = (RuntimeType) typeof (MarshalByRefObject);
    private ConstructorCallMessage _ccm;
    private int _ctorThread;

    public RemotingProxy(Type serverType)
      : base(serverType)
    {
    }

    private RemotingProxy()
    {
    }

    internal int CtorThread
    {
      get
      {
        return this._ctorThread;
      }
      set
      {
        this._ctorThread = value;
      }
    }

    internal static IMessage CallProcessMessage(IMessageSink ms, IMessage reqMsg, ArrayWithSize proxySinks, Thread currentThread, Context currentContext, bool bSkippingContextChain)
    {
      if (proxySinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, proxySinks, true, true, false);
      bool flag = false;
      if (bSkippingContextChain)
      {
        flag = currentContext.NotifyDynamicSinks(reqMsg, true, true, false, true);
        ChannelServices.NotifyProfiler(reqMsg, RemotingProfilerEvent.ClientSend);
      }
      if (ms == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
      IMessage msg = ms.SyncProcessMessage(reqMsg);
      if (bSkippingContextChain)
      {
        ChannelServices.NotifyProfiler(msg, RemotingProfilerEvent.ClientReceive);
        if (flag)
          currentContext.NotifyDynamicSinks(msg, true, false, false, true);
      }
      IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
      if (msg == null || methodReturnMessage == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
      if (proxySinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(msg, proxySinks, true, false, false);
      return msg;
    }

    [SecurityCritical]
    public override IMessage Invoke(IMessage reqMsg)
    {
      IConstructionCallMessage ctorMsg = reqMsg as IConstructionCallMessage;
      if (ctorMsg != null)
        return (IMessage) this.InternalActivate(ctorMsg);
      if (!this.Initialized)
      {
        if (this.CtorThread != Thread.CurrentThread.GetHashCode())
          throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCall"));
        ServerIdentity identityObject = this.IdentityObject as ServerIdentity;
        RemotingServices.Wrap((ContextBoundObject) this.UnwrappedServerObject);
      }
      int callType = 0;
      Message message = reqMsg as Message;
      if (message != null)
        callType = message.GetCallType();
      return this.InternalInvoke((IMethodCallMessage) reqMsg, false, callType);
    }

    internal virtual IMessage InternalInvoke(IMethodCallMessage reqMcmMsg, bool useDispatchMessage, int callType)
    {
      Message message1 = reqMcmMsg as Message;
      if (message1 == null && callType != 0)
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCallType"));
      IMessage message2 = (IMessage) null;
      Thread currentThread = Thread.CurrentThread;
      LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
      Identity identityObject = this.IdentityObject;
      ServerIdentity serverIdentity = identityObject as ServerIdentity;
      if (serverIdentity != null && identityObject.IsFullyDisconnected())
        throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", (object) reqMcmMsg.Uri));
      MethodBase methodBase = reqMcmMsg.MethodBase;
      if ((MethodBase) RemotingProxy._getTypeMethod == methodBase)
        return (IMessage) new ReturnMessage((object) this.GetProxiedType(), (object[]) null, 0, logicalCallContext, reqMcmMsg);
      if ((MethodBase) RemotingProxy._getHashCodeMethod == methodBase)
        return (IMessage) new ReturnMessage((object) identityObject.GetHashCode(), (object[]) null, 0, logicalCallContext, reqMcmMsg);
      if (identityObject.ChannelSink == null)
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        if (!identityObject.ObjectRef.IsObjRefLite())
          RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject) null, identityObject.ObjectRef, out chnlSink, out envoySink);
        else
          RemotingServices.CreateEnvoyAndChannelSinks(identityObject.ObjURI, (object) null, out chnlSink, out envoySink);
        RemotingServices.SetEnvoyAndChannelSinks(identityObject, chnlSink, envoySink);
        if (identityObject.ChannelSink == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
      }
      IInternalMessage internalMessage = (IInternalMessage) reqMcmMsg;
      internalMessage.IdentityObject = identityObject;
      if (serverIdentity != null)
        internalMessage.ServerIdentityObject = serverIdentity;
      else
        internalMessage.SetURI(identityObject.URI);
      switch (callType)
      {
        case 0:
          bool bSkippingContextChain = false;
          Context currentContextInternal = currentThread.GetCurrentContextInternal();
          IMessageSink ms = identityObject.EnvoyChain;
          if (currentContextInternal.IsDefaultContext && ms is EnvoyTerminatorSink)
          {
            bSkippingContextChain = true;
            ms = identityObject.ChannelSink;
          }
          message2 = RemotingProxy.CallProcessMessage(ms, (IMessage) reqMcmMsg, identityObject.ProxySideDynamicSinks, currentThread, currentContextInternal, bSkippingContextChain);
          break;
        case 1:
        case 9:
          LogicalCallContext callContext1 = (LogicalCallContext) logicalCallContext.Clone();
          internalMessage.SetCallContext(callContext1);
          AsyncResult asyncResult = new AsyncResult(message1);
          this.InternalInvokeAsync((IMessageSink) asyncResult, message1, useDispatchMessage, callType);
          message2 = (IMessage) new ReturnMessage((object) asyncResult, (object[]) null, 0, (LogicalCallContext) null, (IMethodCallMessage) message1);
          break;
        case 2:
          message2 = RealProxy.EndInvokeHelper(message1, true);
          break;
        case 8:
          LogicalCallContext callContext2 = (LogicalCallContext) logicalCallContext.Clone();
          internalMessage.SetCallContext(callContext2);
          this.InternalInvokeAsync((IMessageSink) null, message1, useDispatchMessage, callType);
          message2 = (IMessage) new ReturnMessage((object) null, (object[]) null, 0, (LogicalCallContext) null, reqMcmMsg);
          break;
        case 10:
          message2 = (IMessage) new ReturnMessage((object) null, (object[]) null, 0, (LogicalCallContext) null, reqMcmMsg);
          break;
      }
      return message2;
    }

    internal void InternalInvokeAsync(IMessageSink ar, Message reqMsg, bool useDispatchMessage, int callType)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      Identity identityObject = this.IdentityObject;
      ServerIdentity serverIdentity = identityObject as ServerIdentity;
      MethodCall methodCall = new MethodCall((IMessage) reqMsg);
      IInternalMessage internalMessage = (IInternalMessage) methodCall;
      internalMessage.IdentityObject = identityObject;
      if (serverIdentity != null)
        internalMessage.ServerIdentityObject = serverIdentity;
      if (useDispatchMessage)
      {
        messageCtrl = ChannelServices.AsyncDispatchMessage((IMessage) methodCall, (callType & 8) != 0 ? (IMessageSink) null : ar);
      }
      else
      {
        if (identityObject.EnvoyChain == null)
          throw new InvalidOperationException(Environment.GetResourceString("Remoting_Proxy_InvalidState"));
        messageCtrl = identityObject.EnvoyChain.AsyncProcessMessage((IMessage) methodCall, (callType & 8) != 0 ? (IMessageSink) null : ar);
      }
      if ((callType & 1) == 0 || (callType & 8) == 0)
        return;
      ar.SyncProcessMessage((IMessage) null);
    }

    private IConstructionReturnMessage InternalActivate(IConstructionCallMessage ctorMsg)
    {
      this.CtorThread = Thread.CurrentThread.GetHashCode();
      IConstructionReturnMessage constructionReturnMessage = ActivationServices.Activate(this, ctorMsg);
      this.Initialized = true;
      return constructionReturnMessage;
    }

    private static void Invoke(object NotUsed, ref MessageData msgData)
    {
      Message message = new Message();
      message.InitFields(msgData);
      Delegate thisPtr;
      if ((object) (thisPtr = message.GetThisPtr() as Delegate) == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      RemotingProxy realProxy = (RemotingProxy) RemotingServices.GetRealProxy(thisPtr.Target);
      if (realProxy != null)
      {
        realProxy.InternalInvoke((IMethodCallMessage) message, true, message.GetCallType());
      }
      else
      {
        int callType = message.GetCallType();
        if (callType <= 2)
        {
          if (callType != 1)
          {
            if (callType != 2)
              return;
            RealProxy.EndInvokeHelper(message, false);
            return;
          }
        }
        else if (callType != 9)
          return;
        message.Properties[(object) Message.CallContextKey] = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Clone();
        AsyncResult asyncResult = new AsyncResult(message);
        ThreadPool.QueueUserWorkItem(new WaitCallback(AgileAsyncWorkerItem.ThreadPoolCallBack), (object) new AgileAsyncWorkerItem((IMethodCallMessage) message, (callType & 8) != 0 ? (AsyncResult) null : asyncResult, thisPtr.Target));
        if ((callType & 8) != 0)
          asyncResult.SyncProcessMessage((IMessage) null);
        message.PropagateOutParameters((object[]) null, (object) asyncResult);
      }
    }

    internal ConstructorCallMessage ConstructorMessage
    {
      get
      {
        return this._ccm;
      }
      set
      {
        this._ccm = value;
      }
    }

    public string TypeName
    {
      [SecurityCritical] get
      {
        return this.GetProxiedType().FullName;
      }
      [SecurityCritical] set
      {
        throw new NotSupportedException();
      }
    }

    [SecurityCritical]
    public override IntPtr GetCOMIUnknown(bool fIsBeingMarshalled)
    {
      IntPtr zero = IntPtr.Zero;
      object transparentProxy = this.GetTransparentProxy();
      return !RemotingServices.IsObjectOutOfProcess(transparentProxy) ? (!RemotingServices.IsObjectOutOfAppDomain(transparentProxy) ? MarshalByRefObject.GetComIUnknown((MarshalByRefObject) transparentProxy) : ((MarshalByRefObject) transparentProxy).GetComIUnknown(fIsBeingMarshalled)) : (!fIsBeingMarshalled ? MarshalByRefObject.GetComIUnknown((MarshalByRefObject) transparentProxy) : MarshalByRefObject.GetComIUnknown((MarshalByRefObject) transparentProxy));
    }

    [SecurityCritical]
    public override void SetCOMIUnknown(IntPtr i)
    {
    }

    [SecurityCritical]
    public bool CanCastTo(Type castType, object o)
    {
      if (castType == (Type) null)
        throw new ArgumentNullException(nameof (castType));
      RuntimeType castType1 = castType as RuntimeType;
      if (castType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      bool flag = false;
      if (castType1 == RemotingProxy.s_typeofObject || castType1 == RemotingProxy.s_typeofMarshalByRefObject)
        return true;
      ObjRef objectRef = this.IdentityObject.ObjectRef;
      if (objectRef != null)
      {
        object transparentProxy = this.GetTransparentProxy();
        IRemotingTypeInfo typeInfo = objectRef.TypeInfo;
        if (typeInfo != null)
        {
          flag = typeInfo.CanCastTo((Type) castType1, transparentProxy);
          if (!flag && typeInfo.GetType() == typeof (System.Runtime.Remoting.TypeInfo) && objectRef.IsWellKnown())
            flag = this.CanCastToWK((Type) castType1);
        }
        else if (objectRef.IsObjRefLite())
          flag = MarshalByRefObject.CanCastToXmlTypeHelper(castType1, (MarshalByRefObject) o);
      }
      else
        flag = this.CanCastToWK((Type) castType1);
      return flag;
    }

    private bool CanCastToWK(Type castType)
    {
      bool flag = false;
      if (castType.IsClass)
        flag = this.GetProxiedType().IsAssignableFrom(castType);
      else if (!(this.IdentityObject is ServerIdentity))
        flag = true;
      return flag;
    }
  }
}
