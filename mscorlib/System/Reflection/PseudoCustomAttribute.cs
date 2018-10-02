// Decompiled with JetBrains decompiler
// Type: System.Reflection.PseudoCustomAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
  internal static class PseudoCustomAttribute
  {
    private static Dictionary<RuntimeType, RuntimeType> s_pca;
    private static int s_pcasCount;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes);

    [SecurityCritical]
    internal static void GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes)
    {
      PseudoCustomAttribute._GetSecurityAttributes(module.GetNativeHandle(), token, assembly, out securityAttributes);
    }

    [SecurityCritical]
    static PseudoCustomAttribute()
    {
      RuntimeType[] runtimeTypeArray = new RuntimeType[11]
      {
        typeof (FieldOffsetAttribute) as RuntimeType,
        typeof (SerializableAttribute) as RuntimeType,
        typeof (MarshalAsAttribute) as RuntimeType,
        typeof (ComImportAttribute) as RuntimeType,
        typeof (NonSerializedAttribute) as RuntimeType,
        typeof (InAttribute) as RuntimeType,
        typeof (OutAttribute) as RuntimeType,
        typeof (OptionalAttribute) as RuntimeType,
        typeof (DllImportAttribute) as RuntimeType,
        typeof (PreserveSigAttribute) as RuntimeType,
        typeof (TypeForwardedToAttribute) as RuntimeType
      };
      PseudoCustomAttribute.s_pcasCount = runtimeTypeArray.Length;
      Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>(PseudoCustomAttribute.s_pcasCount);
      for (int index = 0; index < PseudoCustomAttribute.s_pcasCount; ++index)
        dictionary[runtimeTypeArray[index]] = runtimeTypeArray[index];
      PseudoCustomAttribute.s_pca = dictionary;
    }

    [SecurityCritical]
    [Conditional("_DEBUG")]
    private static void VerifyPseudoCustomAttribute(RuntimeType pca)
    {
      CustomAttribute.GetAttributeUsage(pca);
    }

    internal static bool IsSecurityAttribute(RuntimeType type)
    {
      if (!(type == (RuntimeType) typeof (SecurityAttribute)))
        return type.IsSubclassOf(typeof (SecurityAttribute));
      return true;
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool includeSecCa, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
        return new Attribute[0];
      List<Attribute> attributeList = new List<Attribute>();
      if (flag || caType == (RuntimeType) typeof (SerializableAttribute))
      {
        Attribute customAttribute = SerializableAttribute.GetCustomAttribute(type);
        if (customAttribute != null)
          attributeList.Add(customAttribute);
      }
      if (flag || caType == (RuntimeType) typeof (ComImportAttribute))
      {
        Attribute customAttribute = ComImportAttribute.GetCustomAttribute(type);
        if (customAttribute != null)
          attributeList.Add(customAttribute);
      }
      if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && (!type.IsGenericParameter && type.GetElementType() == (Type) null))
      {
        if (type.IsGenericType)
          type = (RuntimeType) type.GetGenericTypeDefinition();
        object[] securityAttributes;
        PseudoCustomAttribute.GetSecurityAttributes(type.Module.ModuleHandle.GetRuntimeModule(), type.MetadataToken, false, out securityAttributes);
        if (securityAttributes != null)
        {
          foreach (object obj in securityAttributes)
          {
            if ((Type) caType == obj.GetType() || obj.GetType().IsSubclassOf((Type) caType))
              attributeList.Add((Attribute) obj);
          }
        }
      }
      count = attributeList.Count;
      return attributeList.ToArray();
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeType type, RuntimeType caType)
    {
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      int count;
      return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null) || PseudoCustomAttribute.IsSecurityAttribute(caType)) && ((flag || caType == (RuntimeType) typeof (SerializableAttribute)) && SerializableAttribute.IsDefined(type) || (flag || caType == (RuntimeType) typeof (ComImportAttribute)) && ComImportAttribute.IsDefined(type) || (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out count).Length != 0);
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool includeSecCa, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
        return new Attribute[0];
      List<Attribute> attributeList = new List<Attribute>();
      if (flag || caType == (RuntimeType) typeof (DllImportAttribute))
      {
        Attribute customAttribute = DllImportAttribute.GetCustomAttribute(method);
        if (customAttribute != null)
          attributeList.Add(customAttribute);
      }
      if (flag || caType == (RuntimeType) typeof (PreserveSigAttribute))
      {
        Attribute customAttribute = PreserveSigAttribute.GetCustomAttribute(method);
        if (customAttribute != null)
          attributeList.Add(customAttribute);
      }
      if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
      {
        object[] securityAttributes;
        PseudoCustomAttribute.GetSecurityAttributes(method.Module.ModuleHandle.GetRuntimeModule(), method.MetadataToken, false, out securityAttributes);
        if (securityAttributes != null)
        {
          foreach (object obj in securityAttributes)
          {
            if ((Type) caType == obj.GetType() || obj.GetType().IsSubclassOf((Type) caType))
              attributeList.Add((Attribute) obj);
          }
        }
      }
      count = attributeList.Count;
      return attributeList.ToArray();
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType)
    {
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      int count;
      return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)) && ((flag || caType == (RuntimeType) typeof (DllImportAttribute)) && DllImportAttribute.IsDefined(method) || (flag || caType == (RuntimeType) typeof (PreserveSigAttribute)) && PreserveSigAttribute.IsDefined(method) || (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out count).Length != 0);
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)
        return (Attribute[]) null;
      Attribute[] attributeArray = new Attribute[PseudoCustomAttribute.s_pcasCount];
      if (flag || caType == (RuntimeType) typeof (InAttribute))
      {
        Attribute customAttribute = InAttribute.GetCustomAttribute(parameter);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      if (flag || caType == (RuntimeType) typeof (OutAttribute))
      {
        Attribute customAttribute = OutAttribute.GetCustomAttribute(parameter);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      if (flag || caType == (RuntimeType) typeof (OptionalAttribute))
      {
        Attribute customAttribute = OptionalAttribute.GetCustomAttribute(parameter);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      if (flag || caType == (RuntimeType) typeof (MarshalAsAttribute))
      {
        Attribute customAttribute = MarshalAsAttribute.GetCustomAttribute(parameter);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      return attributeArray;
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
    {
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)) && ((flag || caType == (RuntimeType) typeof (InAttribute)) && InAttribute.IsDefined(parameter) || (flag || caType == (RuntimeType) typeof (OutAttribute)) && OutAttribute.IsDefined(parameter) || ((flag || caType == (RuntimeType) typeof (OptionalAttribute)) && OptionalAttribute.IsDefined(parameter) || (flag || caType == (RuntimeType) typeof (MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(parameter)));
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType, bool includeSecCa, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
        return new Attribute[0];
      List<Attribute> attributeList = new List<Attribute>();
      if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
      {
        object[] securityAttributes;
        PseudoCustomAttribute.GetSecurityAttributes(assembly.ManifestModule.ModuleHandle.GetRuntimeModule(), RuntimeAssembly.GetToken(assembly.GetNativeHandle()), true, out securityAttributes);
        if (securityAttributes != null)
        {
          foreach (object obj in securityAttributes)
          {
            if ((Type) caType == obj.GetType() || obj.GetType().IsSubclassOf((Type) caType))
              attributeList.Add((Attribute) obj);
          }
        }
      }
      count = attributeList.Count;
      return attributeList.ToArray();
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
    {
      int count;
      return (uint) PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out count).Length > 0U;
    }

    internal static Attribute[] GetCustomAttributes(RuntimeModule module, RuntimeType caType, out int count)
    {
      count = 0;
      return (Attribute[]) null;
    }

    internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
    {
      return false;
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)
        return (Attribute[]) null;
      Attribute[] attributeArray = new Attribute[PseudoCustomAttribute.s_pcasCount];
      if (flag || caType == (RuntimeType) typeof (MarshalAsAttribute))
      {
        Attribute customAttribute = MarshalAsAttribute.GetCustomAttribute(field);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      if (flag || caType == (RuntimeType) typeof (FieldOffsetAttribute))
      {
        Attribute customAttribute = FieldOffsetAttribute.GetCustomAttribute(field);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      if (flag || caType == (RuntimeType) typeof (NonSerializedAttribute))
      {
        Attribute customAttribute = NonSerializedAttribute.GetCustomAttribute(field);
        if (customAttribute != null)
          attributeArray[count++] = customAttribute;
      }
      return attributeArray;
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
    {
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)) && ((flag || caType == (RuntimeType) typeof (MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(field) || (flag || caType == (RuntimeType) typeof (FieldOffsetAttribute)) && FieldOffsetAttribute.IsDefined(field) || (flag || caType == (RuntimeType) typeof (NonSerializedAttribute)) && NonSerializedAttribute.IsDefined(field));
    }

    [SecurityCritical]
    internal static Attribute[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType, bool includeSecCa, out int count)
    {
      count = 0;
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
        return new Attribute[0];
      List<Attribute> attributeList = new List<Attribute>();
      if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
      {
        object[] securityAttributes;
        PseudoCustomAttribute.GetSecurityAttributes(ctor.Module.ModuleHandle.GetRuntimeModule(), ctor.MetadataToken, false, out securityAttributes);
        if (securityAttributes != null)
        {
          foreach (object obj in securityAttributes)
          {
            if ((Type) caType == obj.GetType() || obj.GetType().IsSubclassOf((Type) caType))
              attributeList.Add((Attribute) obj);
          }
        }
      }
      count = attributeList.Count;
      return attributeList.ToArray();
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
    {
      bool flag = caType == (RuntimeType) typeof (object) || caType == (RuntimeType) typeof (Attribute);
      int count;
      return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == (RuntimeType) null)) && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out count).Length != 0;
    }

    internal static Attribute[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType, out int count)
    {
      count = 0;
      return (Attribute[]) null;
    }

    internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
    {
      return false;
    }

    internal static Attribute[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType, out int count)
    {
      count = 0;
      return (Attribute[]) null;
    }

    internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
    {
      return false;
    }
  }
}
