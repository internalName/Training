// Decompiled with JetBrains decompiler
// Type: System.Reflection.Associates
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Reflection
{
  internal static class Associates
  {
    internal static bool IncludeAccessor(MethodInfo associate, bool nonPublic)
    {
      return (object) associate != null && (nonPublic || associate.IsPublic);
    }

    [SecurityCritical]
    private static RuntimeMethodInfo AssignAssociates(int tkMethod, RuntimeType declaredType, RuntimeType reflectedType)
    {
      if (MetadataToken.IsNullToken(tkMethod))
        return (RuntimeMethodInfo) null;
      bool flag = declaredType != reflectedType;
      IntPtr[] typeInstantiationContext = (IntPtr[]) null;
      int typeInstCount = 0;
      RuntimeType[] instantiationInternal = declaredType.GetTypeHandleInternal().GetInstantiationInternal();
      if (instantiationInternal != null)
      {
        typeInstCount = instantiationInternal.Length;
        typeInstantiationContext = new IntPtr[instantiationInternal.Length];
        for (int index = 0; index < instantiationInternal.Length; ++index)
          typeInstantiationContext[index] = instantiationInternal[index].GetTypeHandleInternal().Value;
      }
      RuntimeMethodHandleInternal methodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(RuntimeTypeHandle.GetModule(declaredType), tkMethod, typeInstantiationContext, typeInstCount, (IntPtr[]) null, 0);
      if (flag)
      {
        MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(methodHandleInternal);
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
          return (RuntimeMethodInfo) null;
        if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope && (RuntimeTypeHandle.GetAttributes(declaredType) & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic)
        {
          int slot = RuntimeMethodHandle.GetSlot(methodHandleInternal);
          methodHandleInternal = RuntimeTypeHandle.GetMethodAt(reflectedType, slot);
        }
      }
      RuntimeMethodInfo runtimeMethodInfo = RuntimeType.GetMethodBase(reflectedType, methodHandleInternal) as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        runtimeMethodInfo = reflectedType.Module.ResolveMethod(tkMethod, (Type[]) null, (Type[]) null) as RuntimeMethodInfo;
      return runtimeMethodInfo;
    }

    [SecurityCritical]
    internal static void AssignAssociates(MetadataImport scope, int mdPropEvent, RuntimeType declaringType, RuntimeType reflectedType, out RuntimeMethodInfo addOn, out RuntimeMethodInfo removeOn, out RuntimeMethodInfo fireOn, out RuntimeMethodInfo getter, out RuntimeMethodInfo setter, out MethodInfo[] other, out bool composedOfAllPrivateMethods, out BindingFlags bindingFlags)
    {
      addOn = removeOn = fireOn = getter = setter = (RuntimeMethodInfo) null;
      Associates.Attributes attributes1 = Associates.Attributes.ComposedOfAllVirtualMethods | Associates.Attributes.ComposedOfAllPrivateMethods | Associates.Attributes.ComposedOfNoPublicMembers | Associates.Attributes.ComposedOfNoStaticMembers;
      while (RuntimeTypeHandle.IsGenericVariable(reflectedType))
        reflectedType = (RuntimeType) reflectedType.BaseType;
      bool isInherited = declaringType != reflectedType;
      List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
      MetadataEnumResult result;
      scope.Enum(MetadataTokenType.MethodDef, mdPropEvent, out result);
      int capacity = result.Length / 2;
      for (int index = 0; index < capacity; ++index)
      {
        int tkMethod = result[index * 2];
        MethodSemanticsAttributes semanticsAttributes = (MethodSemanticsAttributes) result[index * 2 + 1];
        RuntimeMethodInfo runtimeMethodInfo = Associates.AssignAssociates(tkMethod, declaringType, reflectedType);
        if (!((MethodInfo) runtimeMethodInfo == (MethodInfo) null))
        {
          MethodAttributes attributes2 = runtimeMethodInfo.Attributes;
          bool flag1 = (attributes2 & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
          bool flag2 = (uint) (attributes2 & MethodAttributes.Virtual) > 0U;
          bool flag3 = (attributes2 & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
          bool flag4 = (uint) (attributes2 & MethodAttributes.Static) > 0U;
          if (flag3)
            attributes1 = attributes1 & ~Associates.Attributes.ComposedOfNoPublicMembers & ~Associates.Attributes.ComposedOfAllPrivateMethods;
          else if (!flag1)
            attributes1 &= ~Associates.Attributes.ComposedOfAllPrivateMethods;
          if (flag4)
            attributes1 &= ~Associates.Attributes.ComposedOfNoStaticMembers;
          if (!flag2)
            attributes1 &= ~Associates.Attributes.ComposedOfAllVirtualMethods;
          switch (semanticsAttributes)
          {
            case MethodSemanticsAttributes.Setter:
              setter = runtimeMethodInfo;
              continue;
            case MethodSemanticsAttributes.Getter:
              getter = runtimeMethodInfo;
              continue;
            case MethodSemanticsAttributes.AddOn:
              addOn = runtimeMethodInfo;
              continue;
            case MethodSemanticsAttributes.RemoveOn:
              removeOn = runtimeMethodInfo;
              continue;
            case MethodSemanticsAttributes.Fire:
              fireOn = runtimeMethodInfo;
              continue;
            default:
              if (methodInfoList == null)
                methodInfoList = new List<MethodInfo>(capacity);
              methodInfoList.Add((MethodInfo) runtimeMethodInfo);
              continue;
          }
        }
      }
      bool isPublic = (attributes1 & Associates.Attributes.ComposedOfNoPublicMembers) == (Associates.Attributes) 0;
      bool isStatic = (attributes1 & Associates.Attributes.ComposedOfNoStaticMembers) == (Associates.Attributes) 0;
      bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
      composedOfAllPrivateMethods = (uint) (attributes1 & Associates.Attributes.ComposedOfAllPrivateMethods) > 0U;
      other = methodInfoList?.ToArray();
    }

    [Flags]
    internal enum Attributes
    {
      ComposedOfAllVirtualMethods = 1,
      ComposedOfAllPrivateMethods = 2,
      ComposedOfNoPublicMembers = 4,
      ComposedOfNoStaticMembers = 8,
    }
  }
}
