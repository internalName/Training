// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SafeSerializationManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
  [Serializable]
  internal sealed class SafeSerializationManager : IObjectReference, ISerializable
  {
    private IList<object> m_serializedStates;
    private SerializationInfo m_savedSerializationInfo;
    private object m_realObject;
    private RuntimeType m_realType;
    private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";

    internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState;

    internal SafeSerializationManager()
    {
    }

    [SecurityCritical]
    private SafeSerializationManager(SerializationInfo info, StreamingContext context)
    {
      RuntimeType valueNoThrow = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof (RuntimeType)) as RuntimeType;
      if (valueNoThrow == (RuntimeType) null)
      {
        this.m_serializedStates = (IList<object>) (info.GetValue(nameof (m_serializedStates), typeof (List<object>)) as List<object>);
      }
      else
      {
        this.m_realType = valueNoThrow;
        this.m_savedSerializationInfo = info;
      }
    }

    internal bool IsActive
    {
      get
      {
        return this.SerializeObjectState != null;
      }
    }

    [SecurityCritical]
    internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
    {
      this.m_serializedStates = (IList<object>) null;
      // ISSUE: reference to a compiler-generated field
      EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
      if (serializeObjectState == null)
        return;
      SafeSerializationEventArgs e = new SafeSerializationEventArgs(context);
      serializeObjectState(serializedObject, e);
      this.m_serializedStates = e.SerializedStates;
      info.AddValue("CLR_SafeSerializationManager_RealType", (object) serializedObject.GetType(), typeof (RuntimeType));
      info.SetType(typeof (SafeSerializationManager));
    }

    internal void CompleteDeserialization(object deserializedObject)
    {
      if (this.m_serializedStates == null)
        return;
      foreach (ISafeSerializationData serializedState in (IEnumerable<object>) this.m_serializedStates)
        serializedState.CompleteDeserialization(deserializedObject);
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("m_serializedStates", (object) this.m_serializedStates, typeof (List<IDeserializationCallback>));
    }

    [SecurityCritical]
    object IObjectReference.GetRealObject(StreamingContext context)
    {
      if (this.m_realObject != null)
        return this.m_realObject;
      if (this.m_realType == (RuntimeType) null)
        return (object) this;
      Stack stack = new Stack();
      RuntimeType runtimeType = this.m_realType;
      do
      {
        stack.Push((object) runtimeType);
        runtimeType = runtimeType.BaseType as RuntimeType;
      }
      while ((Type) runtimeType != typeof (object));
      RuntimeType t;
      RuntimeConstructorInfo serializationCtor;
      do
      {
        t = runtimeType;
        runtimeType = stack.Pop() as RuntimeType;
        serializationCtor = runtimeType.GetSerializationCtor();
      }
      while ((ConstructorInfo) serializationCtor != (ConstructorInfo) null && serializationCtor.IsSecurityCritical);
      RuntimeConstructorInfo constructor = ObjectManager.GetConstructor(t);
      object uninitializedObject = FormatterServices.GetUninitializedObject((Type) this.m_realType);
      constructor.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
      this.m_savedSerializationInfo = (SerializationInfo) null;
      this.m_realType = (RuntimeType) null;
      this.m_realObject = uninitializedObject;
      return uninitializedObject;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      if (this.m_realObject == null)
        return;
      SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType()).InvokeOnDeserialized(this.m_realObject, context);
      this.m_realObject = (object) null;
    }
  }
}
