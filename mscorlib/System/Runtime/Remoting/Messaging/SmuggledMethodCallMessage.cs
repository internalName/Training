// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.SmuggledMethodCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class SmuggledMethodCallMessage : MessageSmuggler
  {
    private string _uri;
    private string _methodName;
    private string _typeName;
    private object[] _args;
    private byte[] _serializedArgs;
    private MessageSmuggler.SerializedArg _methodSignature;
    private MessageSmuggler.SerializedArg _instantiation;
    private object _callContext;
    private int _propertyCount;

    [SecurityCritical]
    internal static SmuggledMethodCallMessage SmuggleIfPossible(IMessage msg)
    {
      IMethodCallMessage mcm = msg as IMethodCallMessage;
      if (mcm == null)
        return (SmuggledMethodCallMessage) null;
      return new SmuggledMethodCallMessage(mcm);
    }

    private SmuggledMethodCallMessage()
    {
    }

    [SecurityCritical]
    private SmuggledMethodCallMessage(IMethodCallMessage mcm)
    {
      this._uri = mcm.Uri;
      this._methodName = mcm.MethodName;
      this._typeName = mcm.TypeName;
      ArrayList argsToSerialize = (ArrayList) null;
      IInternalMessage internalMessage = mcm as IInternalMessage;
      if (internalMessage == null || internalMessage.HasProperties())
        this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage((IMethodMessage) mcm, ref argsToSerialize);
      if (mcm.MethodBase.IsGenericMethod)
      {
        Type[] genericArguments = mcm.MethodBase.GetGenericArguments();
        if (genericArguments != null && genericArguments.Length != 0)
        {
          if (argsToSerialize == null)
            argsToSerialize = new ArrayList();
          this._instantiation = new MessageSmuggler.SerializedArg(argsToSerialize.Count);
          argsToSerialize.Add((object) genericArguments);
        }
      }
      if (RemotingServices.IsMethodOverloaded((IMethodMessage) mcm))
      {
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        this._methodSignature = new MessageSmuggler.SerializedArg(argsToSerialize.Count);
        argsToSerialize.Add(mcm.MethodSignature);
      }
      LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
      if (logicalCallContext == null)
        this._callContext = (object) null;
      else if (logicalCallContext.HasInfo)
      {
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        this._callContext = (object) new MessageSmuggler.SerializedArg(argsToSerialize.Count);
        argsToSerialize.Add((object) logicalCallContext);
      }
      else
        this._callContext = (object) logicalCallContext.RemotingData.LogicalCallID;
      this._args = MessageSmuggler.FixupArgs(mcm.Args, ref argsToSerialize);
      if (argsToSerialize == null)
        return;
      this._serializedArgs = CrossAppDomainSerializer.SerializeMessageParts(argsToSerialize).GetBuffer();
    }

    [SecurityCritical]
    internal ArrayList FixupForNewAppDomain()
    {
      ArrayList arrayList = (ArrayList) null;
      if (this._serializedArgs != null)
      {
        arrayList = CrossAppDomainSerializer.DeserializeMessageParts(new MemoryStream(this._serializedArgs));
        this._serializedArgs = (byte[]) null;
      }
      return arrayList;
    }

    internal string Uri
    {
      get
      {
        return this._uri;
      }
    }

    internal string MethodName
    {
      get
      {
        return this._methodName;
      }
    }

    internal string TypeName
    {
      get
      {
        return this._typeName;
      }
    }

    internal Type[] GetInstantiation(ArrayList deserializedArgs)
    {
      if (this._instantiation != null)
        return (Type[]) deserializedArgs[this._instantiation.Index];
      return (Type[]) null;
    }

    internal object[] GetMethodSignature(ArrayList deserializedArgs)
    {
      if (this._methodSignature != null)
        return (object[]) deserializedArgs[this._methodSignature.Index];
      return (object[]) null;
    }

    [SecurityCritical]
    internal object[] GetArgs(ArrayList deserializedArgs)
    {
      return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
    }

    [SecurityCritical]
    internal LogicalCallContext GetCallContext(ArrayList deserializedArgs)
    {
      if (this._callContext == null)
        return (LogicalCallContext) null;
      if (!(this._callContext is string))
        return (LogicalCallContext) deserializedArgs[((MessageSmuggler.SerializedArg) this._callContext).Index];
      return new LogicalCallContext()
      {
        RemotingData = {
          LogicalCallID = (string) this._callContext
        }
      };
    }

    internal int MessagePropertyCount
    {
      get
      {
        return this._propertyCount;
      }
    }

    internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
    {
      for (int index = 0; index < this._propertyCount; ++index)
      {
        DictionaryEntry deserializedArg = (DictionaryEntry) deserializedArgs[index];
        dict[deserializedArg.Key] = deserializedArg.Value;
      }
    }
  }
}
