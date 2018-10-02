// Decompiled with JetBrains decompiler
// Type: System.UnitySerializationHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  [Serializable]
  internal class UnitySerializationHolder : ISerializable, IObjectReference
  {
    internal const int EmptyUnity = 1;
    internal const int NullUnity = 2;
    internal const int MissingUnity = 3;
    internal const int RuntimeTypeUnity = 4;
    internal const int ModuleUnity = 5;
    internal const int AssemblyUnity = 6;
    internal const int GenericParameterTypeUnity = 7;
    internal const int PartialInstantiationTypeUnity = 8;
    internal const int Pointer = 1;
    internal const int Array = 2;
    internal const int SzArray = 3;
    internal const int ByRef = 4;
    private Type[] m_instantiation;
    private int[] m_elementTypes;
    private int m_genericParameterPosition;
    private Type m_declaringType;
    private MethodBase m_declaringMethod;
    private string m_data;
    private string m_assemblyName;
    private int m_unityType;

    internal static void GetUnitySerializationInfo(SerializationInfo info, Missing missing)
    {
      info.SetType(typeof (UnitySerializationHolder));
      info.AddValue("UnityType", 3);
    }

    internal static RuntimeType AddElementTypes(SerializationInfo info, RuntimeType type)
    {
      List<int> intList = new List<int>();
      for (; type.HasElementType; type = (RuntimeType) type.GetElementType())
      {
        if (type.IsSzArray)
          intList.Add(3);
        else if (type.IsArray)
        {
          intList.Add(type.GetArrayRank());
          intList.Add(2);
        }
        else if (type.IsPointer)
          intList.Add(1);
        else if (type.IsByRef)
          intList.Add(4);
      }
      info.AddValue("ElementTypes", (object) intList.ToArray(), typeof (int[]));
      return type;
    }

    internal Type MakeElementTypes(Type type)
    {
      for (int index = this.m_elementTypes.Length - 1; index >= 0; --index)
      {
        if (this.m_elementTypes[index] == 3)
          type = type.MakeArrayType();
        else if (this.m_elementTypes[index] == 2)
          type = type.MakeArrayType(this.m_elementTypes[--index]);
        else if (this.m_elementTypes[index] == 1)
          type = type.MakePointerType();
        else if (this.m_elementTypes[index] == 4)
          type = type.MakeByRefType();
      }
      return type;
    }

    internal static void GetUnitySerializationInfo(SerializationInfo info, RuntimeType type)
    {
      if (type.GetRootElementType().IsGenericParameter)
      {
        type = UnitySerializationHolder.AddElementTypes(info, type);
        info.SetType(typeof (UnitySerializationHolder));
        info.AddValue("UnityType", 7);
        info.AddValue("GenericParameterPosition", type.GenericParameterPosition);
        info.AddValue("DeclaringMethod", (object) type.DeclaringMethod, typeof (MethodBase));
        info.AddValue("DeclaringType", (object) type.DeclaringType, typeof (Type));
      }
      else
      {
        int unityType = 4;
        if (!type.IsGenericTypeDefinition && type.ContainsGenericParameters)
        {
          unityType = 8;
          type = UnitySerializationHolder.AddElementTypes(info, type);
          info.AddValue("GenericArguments", (object) type.GetGenericArguments(), typeof (Type[]));
          type = (RuntimeType) type.GetGenericTypeDefinition();
        }
        UnitySerializationHolder.GetUnitySerializationInfo(info, unityType, type.FullName, type.GetRuntimeAssembly());
      }
    }

    internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType, string data, RuntimeAssembly assembly)
    {
      info.SetType(typeof (UnitySerializationHolder));
      info.AddValue("Data", (object) data, typeof (string));
      info.AddValue("UnityType", unityType);
      string str = !((Assembly) assembly == (Assembly) null) ? assembly.FullName : string.Empty;
      info.AddValue("AssemblyName", (object) str);
    }

    internal UnitySerializationHolder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_unityType = info.GetInt32("UnityType");
      if (this.m_unityType == 3)
        return;
      if (this.m_unityType == 7)
      {
        this.m_declaringMethod = info.GetValue("DeclaringMethod", typeof (MethodBase)) as MethodBase;
        this.m_declaringType = info.GetValue("DeclaringType", typeof (Type)) as Type;
        this.m_genericParameterPosition = info.GetInt32("GenericParameterPosition");
        this.m_elementTypes = info.GetValue("ElementTypes", typeof (int[])) as int[];
      }
      else
      {
        if (this.m_unityType == 8)
        {
          this.m_instantiation = info.GetValue("GenericArguments", typeof (Type[])) as Type[];
          this.m_elementTypes = info.GetValue("ElementTypes", typeof (int[])) as int[];
        }
        this.m_data = info.GetString("Data");
        this.m_assemblyName = info.GetString("AssemblyName");
      }
    }

    private void ThrowInsufficientInformation(string field)
    {
      throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientDeserializationState", (object) field));
    }

    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnitySerHolder"));
    }

    [SecurityCritical]
    public virtual object GetRealObject(StreamingContext context)
    {
      switch (this.m_unityType)
      {
        case 1:
          return (object) Empty.Value;
        case 2:
          return (object) DBNull.Value;
        case 3:
          return (object) Missing.Value;
        case 4:
          if (this.m_data == null || this.m_data.Length == 0)
            this.ThrowInsufficientInformation("Data");
          if (this.m_assemblyName == null)
            this.ThrowInsufficientInformation("AssemblyName");
          if (this.m_assemblyName.Length == 0)
            return (object) Type.GetType(this.m_data, true, false);
          return (object) Assembly.Load(this.m_assemblyName).GetType(this.m_data, true, false);
        case 5:
          if (this.m_data == null || this.m_data.Length == 0)
            this.ThrowInsufficientInformation("Data");
          if (this.m_assemblyName == null)
            this.ThrowInsufficientInformation("AssemblyName");
          Module module = Assembly.Load(this.m_assemblyName).GetModule(this.m_data);
          if (module == (Module) null)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFindModule", (object) this.m_data, (object) this.m_assemblyName));
          return (object) module;
        case 6:
          if (this.m_data == null || this.m_data.Length == 0)
            this.ThrowInsufficientInformation("Data");
          if (this.m_assemblyName == null)
            this.ThrowInsufficientInformation("AssemblyName");
          return (object) Assembly.Load(this.m_assemblyName);
        case 7:
          if (this.m_declaringMethod == (MethodBase) null && this.m_declaringType == (Type) null)
            this.ThrowInsufficientInformation("DeclaringMember");
          if (this.m_declaringMethod != (MethodBase) null)
            return (object) this.m_declaringMethod.GetGenericArguments()[this.m_genericParameterPosition];
          return (object) this.MakeElementTypes(this.m_declaringType.GetGenericArguments()[this.m_genericParameterPosition]);
        case 8:
          this.m_unityType = 4;
          Type realObject = this.GetRealObject(context) as Type;
          this.m_unityType = 8;
          if (this.m_instantiation[0] == (Type) null)
            return (object) null;
          return (object) this.MakeElementTypes(realObject.MakeGenericType(this.m_instantiation));
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUnity"));
      }
    }
  }
}
