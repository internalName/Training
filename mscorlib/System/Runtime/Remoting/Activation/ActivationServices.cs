// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ActivationServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
  internal static class ActivationServices
  {
    private static volatile IActivator activator = (IActivator) null;
    private static Hashtable _proxyTable = new Hashtable();
    private static readonly Type proxyAttributeType = typeof (ProxyAttribute);
    [SecurityCritical]
    private static ProxyAttribute _proxyAttribute = new ProxyAttribute();
    internal static readonly Assembly s_MscorlibAssembly = typeof (object).Assembly;
    [ThreadStatic]
    internal static ActivationAttributeStack _attributeStack;
    internal const string ActivationServiceURI = "RemoteActivationService.rem";
    internal const string RemoteActivateKey = "Remote";
    internal const string PermissionKey = "Permission";
    internal const string ConnectKey = "Connect";

    [SecuritySafeCritical]
    static ActivationServices()
    {
    }

    [SecurityCritical]
    private static void Startup()
    {
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData.ActivationInitialized && !remotingData.InitializingActivation)
        return;
      object configLock = remotingData.ConfigLock;
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(configLock, ref lockTaken);
        remotingData.InitializingActivation = true;
        if (!remotingData.ActivationInitialized)
        {
          remotingData.LocalActivator = new LocalActivator();
          remotingData.ActivationListener = new ActivationListener();
          remotingData.ActivationInitialized = true;
        }
        remotingData.InitializingActivation = false;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(configLock);
      }
    }

    [SecurityCritical]
    private static void InitActivationServices()
    {
      if (ActivationServices.activator != null)
        return;
      ActivationServices.activator = ActivationServices.GetActivator();
      if (ActivationServices.activator == null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ActivationFailure"), Array.Empty<object>()));
    }

    [SecurityCritical]
    private static MarshalByRefObject IsCurrentContextOK(RuntimeType serverType, object[] props, bool bNewObj)
    {
      ActivationServices.InitActivationServices();
      ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute((Type) serverType);
      MarshalByRefObject marshalByRefObject;
      if (proxyAttribute == ActivationServices.DefaultProxyAttribute)
      {
        marshalByRefObject = proxyAttribute.CreateInstanceInternal(serverType);
      }
      else
      {
        marshalByRefObject = proxyAttribute.CreateInstance((Type) serverType);
        if (marshalByRefObject != null && !RemotingServices.IsTransparentProxy((object) marshalByRefObject) && !serverType.IsAssignableFrom(marshalByRefObject.GetType()))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_BadObject"), (object) serverType));
      }
      return marshalByRefObject;
    }

    [SecurityCritical]
    private static MarshalByRefObject CreateObjectForCom(RuntimeType serverType, object[] props, bool bNewObj)
    {
      if (ActivationServices.PeekActivationAttributes((Type) serverType) != null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivForCom"));
      ActivationServices.InitActivationServices();
      ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute((Type) serverType);
      return !(proxyAttribute is ICustomFactory) ? (MarshalByRefObject) Activator.CreateInstance((Type) serverType, true) : ((ICustomFactory) proxyAttribute).CreateInstance((Type) serverType);
    }

    [SecurityCritical]
    private static bool IsCurrentContextOK(RuntimeType serverType, object[] props, ref ConstructorCallMessage ctorCallMsg)
    {
      object[] objArray1 = ActivationServices.PeekActivationAttributes((Type) serverType);
      if (objArray1 != null)
        ActivationServices.PopActivationAttributes((Type) serverType);
      object[] objArray2 = new object[1]
      {
        (object) ActivationServices.GetGlobalAttribute()
      };
      object[] attributesForType = (object[]) ActivationServices.GetContextAttributesForType((Type) serverType);
      Context currentContext = Thread.CurrentContext;
      ctorCallMsg = new ConstructorCallMessage(objArray1, objArray2, attributesForType, serverType);
      ctorCallMsg.Activator = (IActivator) new ConstructionLevelActivator();
      bool flag = ActivationServices.QueryAttributesIfContextOK(currentContext, (IConstructionCallMessage) ctorCallMsg, objArray2);
      if (flag)
      {
        flag = ActivationServices.QueryAttributesIfContextOK(currentContext, (IConstructionCallMessage) ctorCallMsg, objArray1);
        if (flag)
          flag = ActivationServices.QueryAttributesIfContextOK(currentContext, (IConstructionCallMessage) ctorCallMsg, attributesForType);
      }
      return flag;
    }

    [SecurityCritical]
    private static void CheckForInfrastructurePermission(RuntimeAssembly asm)
    {
      if (!((Assembly) asm != ActivationServices.s_MscorlibAssembly))
        return;
      SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.Infrastructure);
      CodeAccessSecurityEngine.CheckAssembly(asm, (CodeAccessPermission) securityPermission);
    }

    [SecurityCritical]
    private static bool QueryAttributesIfContextOK(Context ctx, IConstructionCallMessage ctorMsg, object[] attributes)
    {
      bool flag = true;
      if (attributes != null)
      {
        for (int index = 0; index < attributes.Length; ++index)
        {
          IContextAttribute attribute = attributes[index] as IContextAttribute;
          if (attribute == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
          ActivationServices.CheckForInfrastructurePermission((RuntimeAssembly) attribute.GetType().Assembly);
          flag = attribute.IsContextOK(ctx, ctorMsg);
          if (!flag)
            break;
        }
      }
      return flag;
    }

    [SecurityCritical]
    internal static void GetPropertiesFromAttributes(IConstructionCallMessage ctorMsg, object[] attributes)
    {
      if (attributes == null)
        return;
      for (int index = 0; index < attributes.Length; ++index)
      {
        IContextAttribute attribute = attributes[index] as IContextAttribute;
        if (attribute == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
        ActivationServices.CheckForInfrastructurePermission((RuntimeAssembly) attribute.GetType().Assembly);
        attribute.GetPropertiesForNewContext(ctorMsg);
      }
    }

    internal static ProxyAttribute DefaultProxyAttribute
    {
      [SecurityCritical] get
      {
        return ActivationServices._proxyAttribute;
      }
    }

    [SecurityCritical]
    internal static ProxyAttribute GetProxyAttribute(Type serverType)
    {
      if (!serverType.HasProxyAttribute)
        return ActivationServices.DefaultProxyAttribute;
      ProxyAttribute proxyAttribute = ActivationServices._proxyTable[(object) serverType] as ProxyAttribute;
      if (proxyAttribute == null)
      {
        object[] customAttributes = (object[]) Attribute.GetCustomAttributes((MemberInfo) serverType, ActivationServices.proxyAttributeType, true);
        if (customAttributes != null && customAttributes.Length != 0)
        {
          if (!serverType.IsContextful)
            throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
          proxyAttribute = customAttributes[0] as ProxyAttribute;
        }
        if (!ActivationServices._proxyTable.Contains((object) serverType))
        {
          lock (ActivationServices._proxyTable)
          {
            if (!ActivationServices._proxyTable.Contains((object) serverType))
              ActivationServices._proxyTable.Add((object) serverType, (object) proxyAttribute);
          }
        }
      }
      return proxyAttribute;
    }

    [SecurityCritical]
    internal static MarshalByRefObject CreateInstance(RuntimeType serverType)
    {
      ConstructorCallMessage ctorCallMsg = (ConstructorCallMessage) null;
      bool flag = ActivationServices.IsCurrentContextOK(serverType, (object[]) null, ref ctorCallMsg);
      MarshalByRefObject marshalByRefObject;
      if (flag && !serverType.IsContextful)
      {
        marshalByRefObject = RemotingServices.AllocateUninitializedObject(serverType);
      }
      else
      {
        marshalByRefObject = (MarshalByRefObject) ActivationServices.ConnectIfNecessary((IConstructionCallMessage) ctorCallMsg);
        RemotingProxy remotingProxy;
        if (marshalByRefObject == null)
        {
          remotingProxy = new RemotingProxy((Type) serverType);
          marshalByRefObject = (MarshalByRefObject) remotingProxy.GetTransparentProxy();
        }
        else
          remotingProxy = (RemotingProxy) RemotingServices.GetRealProxy((object) marshalByRefObject);
        remotingProxy.ConstructorMessage = ctorCallMsg;
        if (!flag)
          ctorCallMsg.Activator = (IActivator) new ContextLevelActivator()
          {
            NextActivator = ctorCallMsg.Activator
          };
        else
          ctorCallMsg.ActivateInContext = true;
      }
      return marshalByRefObject;
    }

    [SecurityCritical]
    internal static IConstructionReturnMessage Activate(RemotingProxy remProxy, IConstructionCallMessage ctorMsg)
    {
      IConstructionReturnMessage constructionReturnMessage;
      if (((ConstructorCallMessage) ctorMsg).ActivateInContext)
      {
        constructionReturnMessage = ctorMsg.Activator.Activate(ctorMsg);
        if (constructionReturnMessage.Exception != null)
          throw constructionReturnMessage.Exception;
      }
      else
      {
        ActivationServices.GetPropertiesFromAttributes(ctorMsg, ctorMsg.CallSiteActivationAttributes);
        ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage) ctorMsg).GetWOMAttributes());
        ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage) ctorMsg).GetTypeAttributes());
        IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage) Thread.CurrentContext.GetClientContextChain().SyncProcessMessage((IMessage) ctorMsg);
        constructionReturnMessage = methodReturnMessage as IConstructionReturnMessage;
        if (methodReturnMessage == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
        if (methodReturnMessage.Exception != null)
          throw methodReturnMessage.Exception;
      }
      return constructionReturnMessage;
    }

    [SecurityCritical]
    internal static IConstructionReturnMessage DoCrossContextActivation(IConstructionCallMessage reqMsg)
    {
      bool isContextful = reqMsg.ActivationType.IsContextful;
      Context context = (Context) null;
      if (isContextful)
      {
        context = new Context();
        ArrayList contextProperties = (ArrayList) reqMsg.ContextProperties;
        for (int index = 0; index < contextProperties.Count; ++index)
        {
          IContextProperty prop = contextProperties[index] as IContextProperty;
          if (prop == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
          ActivationServices.CheckForInfrastructurePermission((RuntimeAssembly) prop.GetType().Assembly);
          if (context.GetProperty(prop.Name) == null)
            context.SetProperty(prop);
        }
        context.Freeze();
        for (int index = 0; index < contextProperties.Count; ++index)
        {
          if (!((IContextProperty) contextProperties[index]).IsNewContextOK(context))
            throw new RemotingException(Environment.GetResourceString("Remoting_Activation_PropertyUnhappy"));
        }
      }
      InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(ActivationServices.DoCrossContextActivationCallback);
      object[] args = new object[1]{ (object) reqMsg };
      return !isContextful ? ftnToCall(args) as IConstructionReturnMessage : Thread.CurrentThread.InternalCrossContextCallback(context, ftnToCall, args) as IConstructionReturnMessage;
    }

    [SecurityCritical]
    internal static object DoCrossContextActivationCallback(object[] args)
    {
      IConstructionCallMessage constructionCallMessage = (IConstructionCallMessage) args[0];
      IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage) Thread.CurrentContext.GetServerContextChain().SyncProcessMessage((IMessage) constructionCallMessage);
      IConstructionReturnMessage constructionReturnMessage = methodReturnMessage as IConstructionReturnMessage;
      if (constructionReturnMessage == null)
      {
        constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(methodReturnMessage == null ? (Exception) new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed")) : methodReturnMessage.Exception, (IConstructionCallMessage) null);
        ((ReturnMessage) constructionReturnMessage).SetLogicalCallContext((LogicalCallContext) constructionCallMessage.Properties[(object) Message.CallContextKey]);
      }
      return (object) constructionReturnMessage;
    }

    [SecurityCritical]
    internal static IConstructionReturnMessage DoServerContextActivation(IConstructionCallMessage reqMsg)
    {
      Exception e = (Exception) null;
      return ActivationServices.SetupConstructionReply(ActivationServices.ActivateWithMessage(reqMsg.ActivationType, (IMessage) reqMsg, (ServerIdentity) null, out e), reqMsg, e);
    }

    [SecurityCritical]
    internal static IConstructionReturnMessage SetupConstructionReply(object serverObj, IConstructionCallMessage ctorMsg, Exception e)
    {
      IConstructionReturnMessage constructionReturnMessage;
      if (e == null)
      {
        constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage((MarshalByRefObject) serverObj, (object[]) null, 0, (LogicalCallContext) ctorMsg.Properties[(object) Message.CallContextKey], ctorMsg);
      }
      else
      {
        constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(e, (IConstructionCallMessage) null);
        ((ReturnMessage) constructionReturnMessage).SetLogicalCallContext((LogicalCallContext) ctorMsg.Properties[(object) Message.CallContextKey]);
      }
      return constructionReturnMessage;
    }

    [SecurityCritical]
    internal static object ActivateWithMessage(Type serverType, IMessage msg, ServerIdentity srvIdToBind, out Exception e)
    {
      e = (Exception) null;
      object obj = (object) RemotingServices.AllocateUninitializedObject(serverType);
      object server;
      if (serverType.IsContextful)
      {
        object proxy = !(msg is ConstructorCallMessage) ? (object) null : ((ConstructorCallMessage) msg).GetThisPtr();
        server = RemotingServices.Wrap((ContextBoundObject) obj, proxy, false);
      }
      else
      {
        if (Thread.CurrentContext != Context.DefaultContext)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
        server = obj;
      }
      IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage) new StackBuilderSink(server).SyncProcessMessage(msg);
      if (methodReturnMessage.Exception == null)
      {
        if (serverType.IsContextful)
          return RemotingServices.Wrap((ContextBoundObject) obj);
        return obj;
      }
      e = methodReturnMessage.Exception;
      return (object) null;
    }

    [SecurityCritical]
    internal static void StartListeningForRemoteRequests()
    {
      ActivationServices.Startup();
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData.ActivatorListening)
        return;
      object configLock = remotingData.ConfigLock;
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(configLock, ref lockTaken);
        if (remotingData.ActivatorListening)
          return;
        RemotingServices.MarshalInternal((MarshalByRefObject) Thread.GetDomain().RemotingData.ActivationListener, "RemoteActivationService.rem", typeof (IActivator));
        ((ServerIdentity) IdentityHolder.ResolveIdentity("RemoteActivationService.rem")).SetSingletonObjectMode();
        remotingData.ActivatorListening = true;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(configLock);
      }
    }

    [SecurityCritical]
    internal static IActivator GetActivator()
    {
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData.LocalActivator == null)
        ActivationServices.Startup();
      return (IActivator) remotingData.LocalActivator;
    }

    [SecurityCritical]
    internal static void Initialize()
    {
      ActivationServices.GetActivator();
    }

    [SecurityCritical]
    internal static ContextAttribute GetGlobalAttribute()
    {
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData.LocalActivator == null)
        ActivationServices.Startup();
      return (ContextAttribute) remotingData.LocalActivator;
    }

    [SecurityCritical]
    internal static IContextAttribute[] GetContextAttributesForType(Type serverType)
    {
      if (!typeof (ContextBoundObject).IsAssignableFrom(serverType) || serverType.IsCOMObject)
        return (IContextAttribute[]) new ContextAttribute[0];
      Type type1 = serverType;
      int length1 = 8;
      IContextAttribute[] contextAttributeArray1 = new IContextAttribute[length1];
      int length2 = 0;
      foreach (IContextAttribute customAttribute in type1.GetCustomAttributes(typeof (IContextAttribute), true))
      {
        Type type2 = customAttribute.GetType();
        bool flag = false;
        for (int index = 0; index < length2; ++index)
        {
          if (type2.Equals(contextAttributeArray1[index].GetType()))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          ++length2;
          if (length2 > length1 - 1)
          {
            IContextAttribute[] contextAttributeArray2 = new IContextAttribute[2 * length1];
            Array.Copy((Array) contextAttributeArray1, 0, (Array) contextAttributeArray2, 0, length1);
            contextAttributeArray1 = contextAttributeArray2;
            length1 *= 2;
          }
          contextAttributeArray1[length2 - 1] = customAttribute;
        }
      }
      IContextAttribute[] contextAttributeArray3 = new IContextAttribute[length2];
      Array.Copy((Array) contextAttributeArray1, (Array) contextAttributeArray3, length2);
      return contextAttributeArray3;
    }

    [SecurityCritical]
    internal static object ConnectIfNecessary(IConstructionCallMessage ctorMsg)
    {
      string property = (string) ctorMsg.Properties[(object) "Connect"];
      object obj = (object) null;
      if (property != null)
        obj = RemotingServices.Connect(ctorMsg.ActivationType, property);
      return obj;
    }

    [SecurityCritical]
    internal static object CheckIfConnected(RemotingProxy proxy, IConstructionCallMessage ctorMsg)
    {
      string property = (string) ctorMsg.Properties[(object) "Connect"];
      object obj = (object) null;
      if (property != null)
        obj = proxy.GetTransparentProxy();
      return obj;
    }

    internal static void PushActivationAttributes(Type serverType, object[] attributes)
    {
      if (ActivationServices._attributeStack == null)
        ActivationServices._attributeStack = new ActivationAttributeStack();
      ActivationServices._attributeStack.Push(serverType, attributes);
    }

    internal static object[] PeekActivationAttributes(Type serverType)
    {
      if (ActivationServices._attributeStack == null)
        return (object[]) null;
      return ActivationServices._attributeStack.Peek(serverType);
    }

    internal static void PopActivationAttributes(Type serverType)
    {
      ActivationServices._attributeStack.Pop(serverType);
    }
  }
}
