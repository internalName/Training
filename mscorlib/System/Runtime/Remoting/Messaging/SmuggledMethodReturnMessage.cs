// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.SmuggledMethodReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
  internal class SmuggledMethodReturnMessage : MessageSmuggler
  {
    private object[] _args;
    private object _returnValue;
    private byte[] _serializedArgs;
    private MessageSmuggler.SerializedArg _exception;
    private object _callContext;
    private int _propertyCount;

    [SecurityCritical]
    internal static SmuggledMethodReturnMessage SmuggleIfPossible(IMessage msg)
    {
      IMethodReturnMessage mrm = msg as IMethodReturnMessage;
      if (mrm == null)
        return (SmuggledMethodReturnMessage) null;
      return new SmuggledMethodReturnMessage(mrm);
    }

    private SmuggledMethodReturnMessage()
    {
    }

    [SecurityCritical]
    private SmuggledMethodReturnMessage(IMethodReturnMessage mrm)
    {
      ArrayList argsToSerialize = (ArrayList) null;
      ReturnMessage returnMessage = mrm as ReturnMessage;
      if (returnMessage == null || returnMessage.HasProperties())
        this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage((IMethodMessage) mrm, ref argsToSerialize);
      Exception exception = mrm.Exception;
      if (exception != null)
      {
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        this._exception = new MessageSmuggler.SerializedArg(argsToSerialize.Count);
        argsToSerialize.Add((object) exception);
      }
      LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
      if (logicalCallContext == null)
        this._callContext = (object) null;
      else if (logicalCallContext.HasInfo)
      {
        if (logicalCallContext.Principal != null)
          logicalCallContext.Principal = (IPrincipal) null;
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        this._callContext = (object) new MessageSmuggler.SerializedArg(argsToSerialize.Count);
        argsToSerialize.Add((object) logicalCallContext);
      }
      else
        this._callContext = (object) logicalCallContext.RemotingData.LogicalCallID;
      this._returnValue = MessageSmuggler.FixupArg(mrm.ReturnValue, ref argsToSerialize);
      this._args = MessageSmuggler.FixupArgs(mrm.Args, ref argsToSerialize);
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

    [SecurityCritical]
    internal object GetReturnValue(ArrayList deserializedArgs)
    {
      return MessageSmuggler.UndoFixupArg(this._returnValue, deserializedArgs);
    }

    [SecurityCritical]
    internal object[] GetArgs(ArrayList deserializedArgs)
    {
      return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
    }

    internal Exception GetException(ArrayList deserializedArgs)
    {
      if (this._exception != null)
        return (Exception) deserializedArgs[this._exception.Index];
      return (Exception) null;
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
