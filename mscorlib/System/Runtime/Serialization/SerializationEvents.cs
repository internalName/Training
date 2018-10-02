// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationEvents
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
  internal class SerializationEvents
  {
    private List<MethodInfo> m_OnSerializingMethods;
    private List<MethodInfo> m_OnSerializedMethods;
    private List<MethodInfo> m_OnDeserializingMethods;
    private List<MethodInfo> m_OnDeserializedMethods;

    private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
    {
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      for (Type type = t; type != (Type) null && type != typeof (object); type = type.BaseType)
      {
        foreach (MethodInfo method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
          if (method.IsDefined(attribute, false))
            methodInfoList.Add(method);
        }
      }
      methodInfoList.Reverse();
      if (methodInfoList.Count != 0)
        return methodInfoList;
      return (List<MethodInfo>) null;
    }

    internal SerializationEvents(Type t)
    {
      this.m_OnSerializingMethods = this.GetMethodsWithAttribute(typeof (OnSerializingAttribute), t);
      this.m_OnSerializedMethods = this.GetMethodsWithAttribute(typeof (OnSerializedAttribute), t);
      this.m_OnDeserializingMethods = this.GetMethodsWithAttribute(typeof (OnDeserializingAttribute), t);
      this.m_OnDeserializedMethods = this.GetMethodsWithAttribute(typeof (OnDeserializedAttribute), t);
    }

    internal bool HasOnSerializingEvents
    {
      get
      {
        if (this.m_OnSerializingMethods == null)
          return this.m_OnSerializedMethods != null;
        return true;
      }
    }

    [SecuritySafeCritical]
    internal void InvokeOnSerializing(object obj, StreamingContext context)
    {
      if (this.m_OnSerializingMethods == null)
        return;
      object[] objArray = new object[1]{ (object) context };
      SerializationEventHandler serializationEventHandler = (SerializationEventHandler) null;
      foreach (MethodInfo serializingMethod in this.m_OnSerializingMethods)
      {
        SerializationEventHandler delegateNoSecurityCheck = (SerializationEventHandler) Delegate.CreateDelegateNoSecurityCheck((RuntimeType) typeof (SerializationEventHandler), obj, serializingMethod);
        serializationEventHandler += delegateNoSecurityCheck;
      }
      serializationEventHandler(context);
    }

    [SecuritySafeCritical]
    internal void InvokeOnDeserializing(object obj, StreamingContext context)
    {
      if (this.m_OnDeserializingMethods == null)
        return;
      object[] objArray = new object[1]{ (object) context };
      SerializationEventHandler serializationEventHandler = (SerializationEventHandler) null;
      foreach (MethodInfo deserializingMethod in this.m_OnDeserializingMethods)
      {
        SerializationEventHandler delegateNoSecurityCheck = (SerializationEventHandler) Delegate.CreateDelegateNoSecurityCheck((RuntimeType) typeof (SerializationEventHandler), obj, deserializingMethod);
        serializationEventHandler += delegateNoSecurityCheck;
      }
      serializationEventHandler(context);
    }

    [SecuritySafeCritical]
    internal void InvokeOnDeserialized(object obj, StreamingContext context)
    {
      if (this.m_OnDeserializedMethods == null)
        return;
      object[] objArray = new object[1]{ (object) context };
      SerializationEventHandler serializationEventHandler = (SerializationEventHandler) null;
      foreach (MethodInfo deserializedMethod in this.m_OnDeserializedMethods)
      {
        SerializationEventHandler delegateNoSecurityCheck = (SerializationEventHandler) Delegate.CreateDelegateNoSecurityCheck((RuntimeType) typeof (SerializationEventHandler), obj, deserializedMethod);
        serializationEventHandler += delegateNoSecurityCheck;
      }
      serializationEventHandler(context);
    }

    [SecurityCritical]
    internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
    {
      if (this.m_OnSerializedMethods != null)
      {
        foreach (MethodInfo serializedMethod in this.m_OnSerializedMethods)
        {
          SerializationEventHandler delegateNoSecurityCheck = (SerializationEventHandler) Delegate.CreateDelegateNoSecurityCheck((RuntimeType) typeof (SerializationEventHandler), obj, serializedMethod);
          handler += delegateNoSecurityCheck;
        }
      }
      return handler;
    }

    [SecurityCritical]
    internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
    {
      if (this.m_OnDeserializedMethods != null)
      {
        foreach (MethodInfo deserializedMethod in this.m_OnDeserializedMethods)
        {
          SerializationEventHandler delegateNoSecurityCheck = (SerializationEventHandler) Delegate.CreateDelegateNoSecurityCheck((RuntimeType) typeof (SerializationEventHandler), obj, deserializedMethod);
          handler += delegateNoSecurityCheck;
        }
      }
      return handler;
    }
  }
}
