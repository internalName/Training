// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.StackBuilderSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class StackBuilderSink : IMessageSink
  {
    private static string sIRemoteDispatch = "System.EnterpriseServices.IRemoteDispatch";
    private static string sIRemoteDispatchAssembly = "System.EnterpriseServices";
    private object _server;
    private bool _bStatic;

    public StackBuilderSink(MarshalByRefObject server)
    {
      this._server = (object) server;
    }

    public StackBuilderSink(object server)
    {
      this._server = server;
      if (this._server != null)
        return;
      this._bStatic = true;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      IMessage message = InternalSink.ValidateMessage(msg);
      if (message != null)
        return message;
      IMethodCallMessage mcm = msg as IMethodCallMessage;
      LogicalCallContext logicalCallContext1 = (LogicalCallContext) null;
      object data = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.GetData("__xADCall");
      bool flag = false;
      IMessage msg1;
      try
      {
        object server = this._server;
        StackBuilderSink.VerifyIsOkToCallMethod(server, (IMethodMessage) mcm);
        LogicalCallContext logicalCallContext2 = mcm == null ? (LogicalCallContext) msg.Properties[(object) "__CallContext"] : mcm.LogicalCallContext;
        logicalCallContext1 = CallContext.SetLogicalCallContext(logicalCallContext2);
        flag = true;
        logicalCallContext2.PropagateIncomingHeadersToCallContext(msg);
        StackBuilderSink.PreserveThreadPrincipalIfNecessary(logicalCallContext2, logicalCallContext1);
        if (this.IsOKToStackBlt((IMethodMessage) mcm, server) && ((Message) mcm).Dispatch(server))
        {
          msg1 = (IMessage) new StackBasedReturnMessage();
          ((StackBasedReturnMessage) msg1).InitFields((Message) mcm);
          LogicalCallContext logicalCallContext3 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
          logicalCallContext3.PropagateOutgoingHeadersToMessage(msg1);
          ((StackBasedReturnMessage) msg1).SetLogicalCallContext(logicalCallContext3);
        }
        else
        {
          MethodBase methodBase = StackBuilderSink.GetMethodBase((IMethodMessage) mcm);
          object[] outArgs = (object[]) null;
          RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
          object[] args = Message.CoerceArgs((IMethodMessage) mcm, reflectionCachedData.Parameters);
          object ret = this.PrivateProcessMessage(methodBase.MethodHandle, args, server, out outArgs);
          this.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData, args, ref outArgs);
          LogicalCallContext logicalCallContext3 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
          if (data != null && (bool) data && logicalCallContext3 != null)
            logicalCallContext3.RemovePrincipalIfNotSerializable();
          msg1 = (IMessage) new ReturnMessage(ret, outArgs, outArgs == null ? 0 : outArgs.Length, logicalCallContext3, mcm);
          logicalCallContext3.PropagateOutgoingHeadersToMessage(msg1);
          CallContext.SetLogicalCallContext(logicalCallContext1);
        }
      }
      catch (Exception ex)
      {
        msg1 = (IMessage) new ReturnMessage(ex, mcm);
        ((ReturnMessage) msg1).SetLogicalCallContext(mcm.LogicalCallContext);
        if (flag)
          CallContext.SetLogicalCallContext(logicalCallContext1);
      }
      return msg1;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      IMethodCallMessage mcm = (IMethodCallMessage) msg;
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      IMessage msg1 = (IMessage) null;
      LogicalCallContext logicalCallContext1 = (LogicalCallContext) null;
      bool flag = false;
      try
      {
        try
        {
          LogicalCallContext property1 = (LogicalCallContext) mcm.Properties[(object) Message.CallContextKey];
          object server = this._server;
          StackBuilderSink.VerifyIsOkToCallMethod(server, (IMethodMessage) mcm);
          logicalCallContext1 = CallContext.SetLogicalCallContext(property1);
          flag = true;
          property1.PropagateIncomingHeadersToCallContext(msg);
          StackBuilderSink.PreserveThreadPrincipalIfNecessary(property1, logicalCallContext1);
          ServerChannelSinkStack property2 = msg.Properties[(object) "__SinkStack"] as ServerChannelSinkStack;
          if (property2 != null)
            property2.ServerObject = server;
          MethodBase methodBase = StackBuilderSink.GetMethodBase((IMethodMessage) mcm);
          object[] outArgs = (object[]) null;
          RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
          object[] args = Message.CoerceArgs((IMethodMessage) mcm, reflectionCachedData.Parameters);
          object ret = this.PrivateProcessMessage(methodBase.MethodHandle, args, server, out outArgs);
          this.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData, args, ref outArgs);
          if (replySink != null)
          {
            LogicalCallContext logicalCallContext2 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
            logicalCallContext2?.RemovePrincipalIfNotSerializable();
            msg1 = (IMessage) new ReturnMessage(ret, outArgs, outArgs == null ? 0 : outArgs.Length, logicalCallContext2, mcm);
            logicalCallContext2.PropagateOutgoingHeadersToMessage(msg1);
          }
        }
        catch (Exception ex)
        {
          if (replySink != null)
          {
            msg1 = (IMessage) new ReturnMessage(ex, mcm);
            ((ReturnMessage) msg1).SetLogicalCallContext((LogicalCallContext) mcm.Properties[(object) Message.CallContextKey]);
          }
        }
        finally
        {
          replySink?.SyncProcessMessage(msg1);
        }
      }
      finally
      {
        if (flag)
          CallContext.SetLogicalCallContext(logicalCallContext1);
      }
      return messageCtrl;
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecurityCritical]
    internal bool IsOKToStackBlt(IMethodMessage mcMsg, object server)
    {
      bool flag = false;
      Message message = mcMsg as Message;
      if (message != null)
      {
        IInternalMessage internalMessage = (IInternalMessage) message;
        if (message.GetFramePtr() != IntPtr.Zero && message.GetThisPtr() == server && (internalMessage.IdentityObject == null || internalMessage.IdentityObject != null && internalMessage.IdentityObject == internalMessage.ServerIdentityObject))
          flag = true;
      }
      return flag;
    }

    [SecurityCritical]
    private static MethodBase GetMethodBase(IMethodMessage msg)
    {
      MethodBase methodBase = msg.MethodBase;
      if ((MethodBase) null == methodBase)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) msg.MethodName, (object) msg.TypeName));
      return methodBase;
    }

    [SecurityCritical]
    private static void VerifyIsOkToCallMethod(object server, IMethodMessage msg)
    {
      bool flag = false;
      MarshalByRefObject marshalByRefObject = server as MarshalByRefObject;
      if (marshalByRefObject == null)
        return;
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out fServer);
      if (identity != null)
      {
        ServerIdentity serverIdentity = identity as ServerIdentity;
        if (serverIdentity != null && serverIdentity.MarshaledAsSpecificType)
        {
          Type serverType = serverIdentity.ServerType;
          if (serverType != (Type) null)
          {
            MethodBase methodBase = StackBuilderSink.GetMethodBase(msg);
            RuntimeType declaringType = (RuntimeType) methodBase.DeclaringType;
            if ((Type) declaringType != serverType && !declaringType.IsAssignableFrom(serverType))
              throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidCallingType"), (object) methodBase.DeclaringType.FullName, (object) serverType.FullName));
            if (declaringType.IsInterface)
              StackBuilderSink.VerifyNotIRemoteDispatch(declaringType);
            flag = true;
          }
        }
      }
      if (flag)
        return;
      RuntimeType reflectedType = (RuntimeType) StackBuilderSink.GetMethodBase(msg).ReflectedType;
      if (!reflectedType.IsInterface)
      {
        if (!reflectedType.IsInstanceOfType((object) marshalByRefObject))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidCallingType"), (object) reflectedType.FullName, (object) marshalByRefObject.GetType().FullName));
      }
      else
        StackBuilderSink.VerifyNotIRemoteDispatch(reflectedType);
    }

    [SecurityCritical]
    private static void VerifyNotIRemoteDispatch(RuntimeType reflectedType)
    {
      if (reflectedType.FullName.Equals(StackBuilderSink.sIRemoteDispatch) && reflectedType.GetRuntimeAssembly().GetSimpleName().Equals(StackBuilderSink.sIRemoteDispatchAssembly))
        throw new RemotingException(Environment.GetResourceString("Remoting_CantInvokeIRemoteDispatch"));
    }

    internal void CopyNonByrefOutArgsFromOriginalArgs(RemotingMethodCachedData methodCache, object[] args, ref object[] marshalResponseArgs)
    {
      int[] nonRefOutArgMap = methodCache.NonRefOutArgMap;
      if (nonRefOutArgMap.Length == 0)
        return;
      if (marshalResponseArgs == null)
        marshalResponseArgs = new object[methodCache.Parameters.Length];
      foreach (int index in nonRefOutArgMap)
        marshalResponseArgs[index] = args[index];
    }

    [SecurityCritical]
    internal static void PreserveThreadPrincipalIfNecessary(LogicalCallContext messageCallContext, LogicalCallContext threadCallContext)
    {
      if (threadCallContext == null || messageCallContext.Principal != null)
        return;
      IPrincipal principal = threadCallContext.Principal;
      if (principal == null)
        return;
      messageCallContext.Principal = principal;
    }

    internal object ServerObject
    {
      get
      {
        return this._server;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern object _PrivateProcessMessage(IntPtr md, object[] args, object server, out object[] outArgs);

    [SecurityCritical]
    public object PrivateProcessMessage(RuntimeMethodHandle md, object[] args, object server, out object[] outArgs)
    {
      return this._PrivateProcessMessage(md.Value, args, server, out outArgs);
    }
  }
}
