// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberInfoSerializationHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  [Serializable]
  internal class MemberInfoSerializationHolder : ISerializable, IObjectReference
  {
    private string m_memberName;
    private RuntimeType m_reflectedType;
    private string m_signature;
    private string m_signature2;
    private MemberTypes m_memberType;
    private SerializationInfo m_info;

    public static void GetSerializationInfo(SerializationInfo info, string name, RuntimeType reflectedClass, string signature, MemberTypes type)
    {
      MemberInfoSerializationHolder.GetSerializationInfo(info, name, reflectedClass, signature, (string) null, type, (Type[]) null);
    }

    public static void GetSerializationInfo(SerializationInfo info, string name, RuntimeType reflectedClass, string signature, string signature2, MemberTypes type, Type[] genericArguments)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      string fullName1 = reflectedClass.Module.Assembly.FullName;
      string fullName2 = reflectedClass.FullName;
      info.SetType(typeof (MemberInfoSerializationHolder));
      info.AddValue("Name", (object) name, typeof (string));
      info.AddValue("AssemblyName", (object) fullName1, typeof (string));
      info.AddValue("ClassName", (object) fullName2, typeof (string));
      info.AddValue("Signature", (object) signature, typeof (string));
      info.AddValue("Signature2", (object) signature2, typeof (string));
      info.AddValue("MemberType", (int) type);
      info.AddValue("GenericArguments", (object) genericArguments, typeof (Type[]));
    }

    internal MemberInfoSerializationHolder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      string assemblyName = info.GetString("AssemblyName");
      string name = info.GetString("ClassName");
      if (assemblyName == null || name == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      this.m_reflectedType = FormatterServices.LoadAssemblyFromString(assemblyName).GetType(name, true, false) as RuntimeType;
      this.m_memberName = info.GetString("Name");
      this.m_signature = info.GetString("Signature");
      this.m_signature2 = (string) info.GetValueNoThrow("Signature2", typeof (string));
      this.m_memberType = (MemberTypes) info.GetInt32("MemberType");
      this.m_info = info;
    }

    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    public virtual object GetRealObject(StreamingContext context)
    {
      if (this.m_memberName == null || this.m_reflectedType == (RuntimeType) null || this.m_memberType == (MemberTypes) 0)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding;
      switch (this.m_memberType)
      {
        case MemberTypes.Constructor:
          if (this.m_signature == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_NullSignature"));
          ConstructorInfo[] member1 = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Constructor, bindingAttr) as ConstructorInfo[];
          if (member1.Length == 1)
            return (object) member1[0];
          if (member1.Length > 1)
          {
            for (int index = 0; index < member1.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimeConstructorInfo) member1[index]).SerializationToString().Equals(this.m_signature2))
                  return (object) member1[index];
              }
              else if (member1[index].ToString().Equals(this.m_signature))
                return (object) member1[index];
            }
          }
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
        case MemberTypes.Event:
          EventInfo[] member2 = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Event, bindingAttr) as EventInfo[];
          if (member2.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          return (object) member2[0];
        case MemberTypes.Field:
          FieldInfo[] member3 = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Field, bindingAttr) as FieldInfo[];
          if (member3.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          return (object) member3[0];
        case MemberTypes.Method:
          MethodInfo methodInfo1 = (MethodInfo) null;
          if (this.m_signature == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_NullSignature"));
          Type[] valueNoThrow = this.m_info.GetValueNoThrow("GenericArguments", typeof (Type[])) as Type[];
          MethodInfo[] member4 = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Method, bindingAttr) as MethodInfo[];
          if (member4.Length == 1)
            methodInfo1 = member4[0];
          else if (member4.Length > 1)
          {
            for (int index = 0; index < member4.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimeMethodInfo) member4[index]).SerializationToString().Equals(this.m_signature2))
                {
                  methodInfo1 = member4[index];
                  break;
                }
              }
              else if (member4[index].ToString().Equals(this.m_signature))
              {
                methodInfo1 = member4[index];
                break;
              }
              if (valueNoThrow != null && member4[index].IsGenericMethod && member4[index].GetGenericArguments().Length == valueNoThrow.Length)
              {
                MethodInfo methodInfo2 = member4[index].MakeGenericMethod(valueNoThrow);
                if (this.m_signature2 != null)
                {
                  if (((RuntimeMethodInfo) methodInfo2).SerializationToString().Equals(this.m_signature2))
                  {
                    methodInfo1 = methodInfo2;
                    break;
                  }
                }
                else if (methodInfo2.ToString().Equals(this.m_signature))
                {
                  methodInfo1 = methodInfo2;
                  break;
                }
              }
            }
          }
          if (methodInfo1 == (MethodInfo) null)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          if (!methodInfo1.IsGenericMethodDefinition)
            return (object) methodInfo1;
          if (valueNoThrow == null)
            return (object) methodInfo1;
          if (valueNoThrow[0] == (Type) null)
            return (object) null;
          return (object) methodInfo1.MakeGenericMethod(valueNoThrow);
        case MemberTypes.Property:
          PropertyInfo[] member5 = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Property, bindingAttr) as PropertyInfo[];
          if (member5.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          if (member5.Length == 1)
            return (object) member5[0];
          if (member5.Length > 1)
          {
            for (int index = 0; index < member5.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimePropertyInfo) member5[index]).SerializationToString().Equals(this.m_signature2))
                  return (object) member5[index];
              }
              else if (member5[index].ToString().Equals(this.m_signature))
                return (object) member5[index];
            }
          }
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
        default:
          throw new ArgumentException(Environment.GetResourceString("Serialization_MemberTypeNotRecognized"));
      }
    }
  }
}
