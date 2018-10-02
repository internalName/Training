// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class CrossAppDomainSink : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossAppDomainSink.DoTransitionDispatchCallback);
    internal const int GROW_BY = 8;
    internal static volatile int[] _sinkKeys;
    internal static volatile CrossAppDomainSink[] _sinks;
    internal const string LCC_DATA_KEY = "__xADCall";
    internal CrossAppDomainData _xadData;

    [SecuritySafeCritical]
    static CrossAppDomainSink()
    {
    }

    internal CrossAppDomainSink(CrossAppDomainData xadData)
    {
      this._xadData = xadData;
    }

    internal static void GrowArrays(int oldSize)
    {
      if (CrossAppDomainSink._sinks == null)
      {
        CrossAppDomainSink._sinks = new CrossAppDomainSink[8];
        CrossAppDomainSink._sinkKeys = new int[8];
      }
      else
      {
        CrossAppDomainSink[] crossAppDomainSinkArray = new CrossAppDomainSink[CrossAppDomainSink._sinks.Length + 8];
        int[] numArray = new int[CrossAppDomainSink._sinkKeys.Length + 8];
        Array.Copy((Array) CrossAppDomainSink._sinks, (Array) crossAppDomainSinkArray, CrossAppDomainSink._sinks.Length);
        Array.Copy((Array) CrossAppDomainSink._sinkKeys, (Array) numArray, CrossAppDomainSink._sinkKeys.Length);
        CrossAppDomainSink._sinks = crossAppDomainSinkArray;
        CrossAppDomainSink._sinkKeys = numArray;
      }
    }

    internal static CrossAppDomainSink FindOrCreateSink(CrossAppDomainData xadData)
    {
      lock (CrossAppDomainSink.staticSyncObject)
      {
        int domainId = xadData.DomainID;
        if (CrossAppDomainSink._sinks == null)
          CrossAppDomainSink.GrowArrays(0);
        int oldSize = 0;
        while (CrossAppDomainSink._sinks[oldSize] != null)
        {
          if (CrossAppDomainSink._sinkKeys[oldSize] == domainId)
            return CrossAppDomainSink._sinks[oldSize];
          ++oldSize;
          if (oldSize == CrossAppDomainSink._sinks.Length)
          {
            CrossAppDomainSink.GrowArrays(oldSize);
            break;
          }
        }
        CrossAppDomainSink._sinks[oldSize] = new CrossAppDomainSink(xadData);
        CrossAppDomainSink._sinkKeys[oldSize] = domainId;
        return CrossAppDomainSink._sinks[oldSize];
      }
    }

    internal static void DomainUnloaded(int domainID)
    {
      int num = domainID;
      lock (CrossAppDomainSink.staticSyncObject)
      {
        if (CrossAppDomainSink._sinks == null)
          return;
        int index1 = 0;
        int index2 = -1;
        while (CrossAppDomainSink._sinks[index1] != null)
        {
          if (CrossAppDomainSink._sinkKeys[index1] == num)
            index2 = index1;
          ++index1;
          if (index1 == CrossAppDomainSink._sinks.Length)
            break;
        }
        if (index2 == -1)
          return;
        CrossAppDomainSink._sinkKeys[index2] = CrossAppDomainSink._sinkKeys[index1 - 1];
        CrossAppDomainSink._sinks[index2] = CrossAppDomainSink._sinks[index1 - 1];
        CrossAppDomainSink._sinkKeys[index1 - 1] = 0;
        CrossAppDomainSink._sinks[index1 - 1] = (CrossAppDomainSink) null;
      }
    }

    [SecurityCritical]
    internal static byte[] DoDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
    {
      IMessage msg1;
      if (smuggledMcm != null)
      {
        ArrayList deserializedArgs = smuggledMcm.FixupForNewAppDomain();
        msg1 = (IMessage) new MethodCall(smuggledMcm, deserializedArgs);
      }
      else
        msg1 = CrossAppDomainSerializer.DeserializeMessage(new MemoryStream(reqStmBuff));
      LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      logicalCallContext.SetData("__xADCall", (object) true);
      IMessage msg2 = ChannelServices.SyncDispatchMessage(msg1);
      logicalCallContext.FreeNamedDataSlot("__xADCall");
      smuggledMrm = SmuggledMethodReturnMessage.SmuggleIfPossible(msg2);
      if (smuggledMrm != null)
        return (byte[]) null;
      if (msg2 == null)
        return (byte[]) null;
      LogicalCallContext property = (LogicalCallContext) msg2.Properties[(object) Message.CallContextKey];
      if (property != null && property.Principal != null)
        property.Principal = (IPrincipal) null;
      return CrossAppDomainSerializer.SerializeMessage(msg2).GetBuffer();
    }

    [SecurityCritical]
    internal static object DoTransitionDispatchCallback(object[] args)
    {
      byte[] reqStmBuff = (byte[]) args[0];
      SmuggledMethodCallMessage smuggledMcm = (SmuggledMethodCallMessage) args[1];
      SmuggledMethodReturnMessage smuggledMrm = (SmuggledMethodReturnMessage) null;
      byte[] numArray;
      try
      {
        numArray = CrossAppDomainSink.DoDispatch(reqStmBuff, smuggledMcm, out smuggledMrm);
      }
      catch (Exception ex)
      {
        numArray = CrossAppDomainSerializer.SerializeMessage((IMessage) new ReturnMessage(ex, (IMethodCallMessage) new ErrorMessage())).GetBuffer();
      }
      args[2] = (object) smuggledMrm;
      return (object) numArray;
    }

    [SecurityCritical]
    internal byte[] DoTransitionDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
    {
      object[] args = new object[3]
      {
        (object) reqStmBuff,
        (object) smuggledMcm,
        null
      };
      byte[] numArray = (byte[]) Thread.CurrentThread.InternalCrossContextCallback((Context) null, this._xadData.ContextID, this._xadData.DomainID, CrossAppDomainSink.s_xctxDel, args);
      smuggledMrm = (SmuggledMethodReturnMessage) args[2];
      return numArray;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message1 = InternalSink.ValidateMessage(reqMsg);
      if (message1 != null)
        return message1;
      IPrincipal principal = (IPrincipal) null;
      IMessage message2 = (IMessage) null;
      try
      {
        IMethodCallMessage methodCallMessage = reqMsg as IMethodCallMessage;
        if (methodCallMessage != null)
        {
          LogicalCallContext logicalCallContext = methodCallMessage.LogicalCallContext;
          if (logicalCallContext != null)
            principal = logicalCallContext.RemovePrincipalIfNotSerializable();
        }
        MemoryStream memoryStream = (MemoryStream) null;
        SmuggledMethodCallMessage smuggledMcm = SmuggledMethodCallMessage.SmuggleIfPossible(reqMsg);
        if (smuggledMcm == null)
          memoryStream = CrossAppDomainSerializer.SerializeMessage(reqMsg);
        LogicalCallContext callCtx = CallContext.SetLogicalCallContext((LogicalCallContext) null);
        byte[] buffer = (byte[]) null;
        SmuggledMethodReturnMessage smuggledMrm;
        try
        {
          buffer = smuggledMcm == null ? this.DoTransitionDispatch(memoryStream.GetBuffer(), (SmuggledMethodCallMessage) null, out smuggledMrm) : this.DoTransitionDispatch((byte[]) null, smuggledMcm, out smuggledMrm);
        }
        finally
        {
          CallContext.SetLogicalCallContext(callCtx);
        }
        if (smuggledMrm != null)
        {
          ArrayList deserializedArgs = smuggledMrm.FixupForNewAppDomain();
          message2 = (IMessage) new MethodResponse((IMethodCallMessage) reqMsg, smuggledMrm, deserializedArgs);
        }
        else if (buffer != null)
          message2 = CrossAppDomainSerializer.DeserializeMessage(new MemoryStream(buffer), reqMsg as IMethodCallMessage);
      }
      catch (Exception ex1)
      {
        try
        {
          message2 = (IMessage) new ReturnMessage(ex1, reqMsg as IMethodCallMessage);
        }
        catch (Exception ex2)
        {
        }
      }
      if (principal != null)
      {
        IMethodReturnMessage methodReturnMessage = message2 as IMethodReturnMessage;
        if (methodReturnMessage != null)
          methodReturnMessage.LogicalCallContext.Principal = principal;
      }
      return message2;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(new ADAsyncWorkItem(reqMsg, (IMessageSink) this, replySink).FinishAsyncWork));
      return (IMessageCtrl) null;
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }
  }
}
