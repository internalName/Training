// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectCloneHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Serialization
{
  internal static class ObjectCloneHelper
  {
    private static readonly IFormatterConverter s_converter = (IFormatterConverter) new FormatterConverter();
    private static readonly StreamingContext s_cloneContext = new StreamingContext(StreamingContextStates.CrossAppDomain);
    private static readonly ISerializationSurrogate s_RemotingSurrogate = (ISerializationSurrogate) new RemotingSurrogate();
    private static readonly ISerializationSurrogate s_ObjRefRemotingSurrogate = (ISerializationSurrogate) new ObjRefSurrogate();

    [SecurityCritical]
    internal static object GetObjectData(object serObj, out string typeName, out string assemName, out string[] fieldNames, out object[] fieldValues)
    {
      object obj = (object) null;
      SerializationInfo info = new SerializationInfo(!RemotingServices.IsTransparentProxy(serObj) ? serObj.GetType() : typeof (MarshalByRefObject), ObjectCloneHelper.s_converter);
      if (serObj is ObjRef)
        ObjectCloneHelper.s_ObjRefRemotingSurrogate.GetObjectData(serObj, info, ObjectCloneHelper.s_cloneContext);
      else if (RemotingServices.IsTransparentProxy(serObj) || serObj is MarshalByRefObject)
      {
        if (!RemotingServices.IsTransparentProxy(serObj) || RemotingServices.GetRealProxy(serObj) is RemotingProxy)
        {
          ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject) serObj, (string) null, (Type) null);
          if (objRef.CanSmuggle())
          {
            if (RemotingServices.IsTransparentProxy(serObj))
            {
              RealProxy realProxy = RemotingServices.GetRealProxy(serObj);
              objRef.SetServerIdentity(realProxy._srvIdentity);
              objRef.SetDomainID(realProxy._domainID);
            }
            else
            {
              ServerIdentity identity = (ServerIdentity) MarshalByRefObject.GetIdentity((MarshalByRefObject) serObj);
              identity.SetHandle();
              objRef.SetServerIdentity(identity.GetHandle());
              objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
            }
            objRef.SetMarshaledObject();
            obj = (object) objRef;
          }
        }
        if (obj == null)
          ObjectCloneHelper.s_RemotingSurrogate.GetObjectData(serObj, info, ObjectCloneHelper.s_cloneContext);
      }
      else
      {
        if (!(serObj is ISerializable))
          throw new ArgumentException(Environment.GetResourceString("Arg_SerializationException"));
        ((ISerializable) serObj).GetObjectData(info, ObjectCloneHelper.s_cloneContext);
      }
      if (obj == null)
      {
        typeName = info.FullTypeName;
        assemName = info.AssemblyName;
        fieldNames = info.MemberNames;
        fieldValues = info.MemberValues;
      }
      else
      {
        typeName = (string) null;
        assemName = (string) null;
        fieldNames = (string[]) null;
        fieldValues = (object[]) null;
      }
      return obj;
    }

    [SecurityCritical]
    internal static SerializationInfo PrepareConstructorArgs(object serObj, string[] fieldNames, object[] fieldValues, out StreamingContext context)
    {
      SerializationInfo serializationInfo = (SerializationInfo) null;
      if (serObj is ISerializable)
      {
        serializationInfo = new SerializationInfo(serObj.GetType(), ObjectCloneHelper.s_converter);
        for (int index = 0; index < fieldNames.Length; ++index)
        {
          if (fieldNames[index] != null)
            serializationInfo.AddValue(fieldNames[index], fieldValues[index]);
        }
      }
      else
      {
        Hashtable hashtable = new Hashtable();
        int index1 = 0;
        int num = 0;
        for (; index1 < fieldNames.Length; ++index1)
        {
          if (fieldNames[index1] != null)
          {
            hashtable[(object) fieldNames[index1]] = fieldValues[index1];
            ++num;
          }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(serObj.GetType());
        for (int index2 = 0; index2 < serializableMembers.Length; ++index2)
        {
          string name = serializableMembers[index2].Name;
          if (!hashtable.Contains((object) name))
          {
            object[] customAttributes = serializableMembers[index2].GetCustomAttributes(typeof (OptionalFieldAttribute), false);
            if (customAttributes == null || customAttributes.Length == 0)
              throw new SerializationException(Environment.GetResourceString("Serialization_MissingMember", (object) serializableMembers[index2], (object) serObj.GetType(), (object) typeof (OptionalFieldAttribute).FullName));
          }
          else
          {
            object obj = hashtable[(object) name];
            FormatterServices.SerializationSetValue(serializableMembers[index2], serObj, obj);
          }
        }
      }
      context = ObjectCloneHelper.s_cloneContext;
      return serializationInfo;
    }
  }
}
