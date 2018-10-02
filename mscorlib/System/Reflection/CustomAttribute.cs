// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  internal static class CustomAttribute
  {
    private static RuntimeType Type_RuntimeType = (RuntimeType) typeof (RuntimeType);
    private static RuntimeType Type_Type = (RuntimeType) typeof (Type);

    [SecurityCritical]
    internal static bool IsDefined(RuntimeType type, RuntimeType caType, bool inherit)
    {
      if (type.GetElementType() != (Type) null)
        return false;
      if (PseudoCustomAttribute.IsDefined(type, caType) || CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType))
        return true;
      if (!inherit)
        return false;
      for (type = type.BaseType as RuntimeType; type != (RuntimeType) null; type = type.BaseType as RuntimeType)
      {
        if (CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType, 0, inherit))
          return true;
      }
      return false;
    }

    [SecuritySafeCritical]
    internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
    {
      if (PseudoCustomAttribute.IsDefined(method, caType) || CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType))
        return true;
      if (!inherit)
        return false;
      for (method = method.GetParentDefinition(); (MethodInfo) method != (MethodInfo) null; method = method.GetParentDefinition())
      {
        if (CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType, 0, inherit))
          return true;
      }
      return false;
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(ctor, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(ctor.GetRuntimeModule(), ctor.MetadataToken, caType);
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(property, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(property.GetRuntimeModule(), property.MetadataToken, caType);
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(e, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(e.GetRuntimeModule(), e.MetadataToken, caType);
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(field, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(field.GetRuntimeModule(), field.MetadataToken, caType);
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(parameter, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(parameter.GetRuntimeModule(), parameter.MetadataToken, caType);
    }

    [SecuritySafeCritical]
    internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(assembly, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(assembly.ManifestModule as RuntimeModule, RuntimeAssembly.GetToken(assembly.GetNativeHandle()), caType);
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
    {
      if (PseudoCustomAttribute.IsDefined(module, caType))
        return true;
      return CustomAttribute.IsCustomAttributeDefined(module, module.MetadataToken, caType);
    }

    [SecurityCritical]
    internal static object[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool inherit)
    {
      if (type.GetElementType() != (Type) null)
      {
        if (!caType.IsValueType)
          return CustomAttribute.CreateAttributeArrayHelper((Type) caType, 0);
        return EmptyArray<object>.Value;
      }
      if (type.IsGenericType && !type.IsGenericTypeDefinition)
        type = type.GetGenericTypeDefinition() as RuntimeType;
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out count);
      if (!inherit || caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited)
      {
        object[] customAttributes2 = CustomAttribute.GetCustomAttributes(type.GetRuntimeModule(), type.MetadataToken, count, caType, !CustomAttribute.AllowCriticalCustomAttributes(type));
        if (count > 0)
          Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
        return customAttributes2;
      }
      List<object> objectList = new List<object>();
      bool mustBeInheritable = false;
      Type elementType = caType == (RuntimeType) null || caType.IsValueType || caType.ContainsGenericParameters ? typeof (object) : (Type) caType;
      while (count > 0)
        objectList.Add((object) customAttributes1[--count]);
      for (; type != (RuntimeType) typeof (object) && type != (RuntimeType) null; type = type.BaseType as RuntimeType)
      {
        object[] customAttributes2 = CustomAttribute.GetCustomAttributes(type.GetRuntimeModule(), type.MetadataToken, 0, caType, mustBeInheritable, (IList) objectList, !CustomAttribute.AllowCriticalCustomAttributes(type));
        mustBeInheritable = true;
        for (int index = 0; index < customAttributes2.Length; ++index)
          objectList.Add(customAttributes2[index]);
      }
      object[] attributeArrayHelper = CustomAttribute.CreateAttributeArrayHelper(elementType, objectList.Count);
      Array.Copy((Array) objectList.ToArray(), 0, (Array) attributeArrayHelper, 0, objectList.Count);
      return attributeArrayHelper;
    }

    private static bool AllowCriticalCustomAttributes(RuntimeType type)
    {
      if (type.IsGenericParameter)
      {
        MethodBase declaringMethod = type.DeclaringMethod;
        if (declaringMethod != (MethodBase) null)
          return CustomAttribute.AllowCriticalCustomAttributes(declaringMethod);
        type = type.DeclaringType as RuntimeType;
      }
      if (type.IsSecurityTransparent)
        return CustomAttribute.SpecialAllowCriticalAttributes(type);
      return true;
    }

    private static bool SpecialAllowCriticalAttributes(RuntimeType type)
    {
      if (type != (RuntimeType) null && type.Assembly.IsFullyTrusted)
        return RuntimeTypeHandle.IsEquivalentType(type);
      return false;
    }

    private static bool AllowCriticalCustomAttributes(MethodBase method)
    {
      if (method.IsSecurityTransparent)
        return CustomAttribute.SpecialAllowCriticalAttributes((RuntimeType) method.DeclaringType);
      return true;
    }

    private static bool AllowCriticalCustomAttributes(RuntimeFieldInfo field)
    {
      if (field.IsSecurityTransparent)
        return CustomAttribute.SpecialAllowCriticalAttributes((RuntimeType) field.DeclaringType);
      return true;
    }

    private static bool AllowCriticalCustomAttributes(RuntimeParameterInfo parameter)
    {
      return CustomAttribute.AllowCriticalCustomAttributes(parameter.DefiningMethod);
    }

    [SecurityCritical]
    internal static object[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
    {
      if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
        method = method.GetGenericMethodDefinition() as RuntimeMethodInfo;
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out count);
      if (!inherit || caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited)
      {
        object[] customAttributes2 = CustomAttribute.GetCustomAttributes(method.GetRuntimeModule(), method.MetadataToken, count, caType, !CustomAttribute.AllowCriticalCustomAttributes((MethodBase) method));
        if (count > 0)
          Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
        return customAttributes2;
      }
      List<object> objectList = new List<object>();
      bool mustBeInheritable = false;
      Type elementType = caType == (RuntimeType) null || caType.IsValueType || caType.ContainsGenericParameters ? typeof (object) : (Type) caType;
      while (count > 0)
        objectList.Add((object) customAttributes1[--count]);
      for (; (MethodInfo) method != (MethodInfo) null; method = method.GetParentDefinition())
      {
        object[] customAttributes2 = CustomAttribute.GetCustomAttributes(method.GetRuntimeModule(), method.MetadataToken, 0, caType, mustBeInheritable, (IList) objectList, !CustomAttribute.AllowCriticalCustomAttributes((MethodBase) method));
        mustBeInheritable = true;
        for (int index = 0; index < customAttributes2.Length; ++index)
          objectList.Add(customAttributes2[index]);
      }
      object[] attributeArrayHelper = CustomAttribute.CreateAttributeArrayHelper(elementType, objectList.Count);
      Array.Copy((Array) objectList.ToArray(), 0, (Array) attributeArrayHelper, 0, objectList.Count);
      return attributeArrayHelper;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out count);
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(ctor.GetRuntimeModule(), ctor.MetadataToken, count, caType, !CustomAttribute.AllowCriticalCustomAttributes((MethodBase) ctor));
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(property, caType, out count);
      bool isDecoratedTargetSecurityTransparent = property.GetRuntimeModule().GetRuntimeAssembly().IsAllSecurityTransparent();
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(property.GetRuntimeModule(), property.MetadataToken, count, caType, isDecoratedTargetSecurityTransparent);
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(e, caType, out count);
      bool isDecoratedTargetSecurityTransparent = e.GetRuntimeModule().GetRuntimeAssembly().IsAllSecurityTransparent();
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(e.GetRuntimeModule(), e.MetadataToken, count, caType, isDecoratedTargetSecurityTransparent);
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(field, caType, out count);
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(field.GetRuntimeModule(), field.MetadataToken, count, caType, !CustomAttribute.AllowCriticalCustomAttributes(field));
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(parameter, caType, out count);
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(parameter.GetRuntimeModule(), parameter.MetadataToken, count, caType, !CustomAttribute.AllowCriticalCustomAttributes(parameter));
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out count);
      int token = RuntimeAssembly.GetToken(assembly.GetNativeHandle());
      bool isDecoratedTargetSecurityTransparent = assembly.IsAllSecurityTransparent();
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(assembly.ManifestModule as RuntimeModule, token, count, caType, isDecoratedTargetSecurityTransparent);
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static object[] GetCustomAttributes(RuntimeModule module, RuntimeType caType)
    {
      int count = 0;
      Attribute[] customAttributes1 = PseudoCustomAttribute.GetCustomAttributes(module, caType, out count);
      bool isDecoratedTargetSecurityTransparent = module.GetRuntimeAssembly().IsAllSecurityTransparent();
      object[] customAttributes2 = CustomAttribute.GetCustomAttributes(module, module.MetadataToken, count, caType, isDecoratedTargetSecurityTransparent);
      if (count > 0)
        Array.Copy((Array) customAttributes1, 0, (Array) customAttributes2, customAttributes2.Length - count, count);
      return customAttributes2;
    }

    [SecuritySafeCritical]
    internal static bool IsAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, int attributeCtorToken)
    {
      return CustomAttribute.IsCustomAttributeDefined(decoratedModule, decoratedMetadataToken, (RuntimeType) null, attributeCtorToken, false);
    }

    [SecurityCritical]
    private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType)
    {
      return CustomAttribute.IsCustomAttributeDefined(decoratedModule, decoratedMetadataToken, attributeFilterType, 0, false);
    }

    [SecurityCritical]
    private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType, int attributeCtorToken, bool mustBeInheritable)
    {
      if (decoratedModule.Assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyCA"));
      CustomAttributeRecord[] attributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
      if (attributeFilterType != (RuntimeType) null)
      {
        MetadataImport metadataImport = decoratedModule.MetadataImport;
        Assembly lastAptcaOkAssembly = (Assembly) null;
        for (int index = 0; index < attributeRecords.Length; ++index)
        {
          RuntimeType attributeType;
          IRuntimeMethodInfo ctor;
          bool ctorHasParameters;
          bool isVarArg;
          if (CustomAttribute.FilterCustomAttributeRecord(attributeRecords[index], metadataImport, ref lastAptcaOkAssembly, decoratedModule, (MetadataToken) decoratedMetadataToken, attributeFilterType, mustBeInheritable, (object[]) null, (IList) null, out attributeType, out ctor, out ctorHasParameters, out isVarArg))
            return true;
        }
      }
      else
      {
        for (int index = 0; index < attributeRecords.Length; ++index)
        {
          if ((int) attributeRecords[index].tkCtor == attributeCtorToken)
            return true;
        }
      }
      return false;
    }

    [SecurityCritical]
    private static object[] GetCustomAttributes(RuntimeModule decoratedModule, int decoratedMetadataToken, int pcaCount, RuntimeType attributeFilterType, bool isDecoratedTargetSecurityTransparent)
    {
      return CustomAttribute.GetCustomAttributes(decoratedModule, decoratedMetadataToken, pcaCount, attributeFilterType, false, (IList) null, isDecoratedTargetSecurityTransparent);
    }

    [SecurityCritical]
    private static unsafe object[] GetCustomAttributes(RuntimeModule decoratedModule, int decoratedMetadataToken, int pcaCount, RuntimeType attributeFilterType, bool mustBeInheritable, IList derivedAttributes, bool isDecoratedTargetSecurityTransparent)
    {
      if (decoratedModule.Assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyCA"));
      MetadataImport metadataImport = decoratedModule.MetadataImport;
      CustomAttributeRecord[] attributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
      Type elementType = attributeFilterType == (RuntimeType) null || attributeFilterType.IsValueType || attributeFilterType.ContainsGenericParameters ? typeof (object) : (Type) attributeFilterType;
      if (attributeFilterType == (RuntimeType) null && attributeRecords.Length == 0)
        return CustomAttribute.CreateAttributeArrayHelper(elementType, 0);
      object[] attributeArrayHelper1 = CustomAttribute.CreateAttributeArrayHelper(elementType, attributeRecords.Length);
      int length = 0;
      SecurityContextFrame securityContextFrame = new SecurityContextFrame();
      securityContextFrame.Push(decoratedModule.GetRuntimeAssembly());
      Assembly lastAptcaOkAssembly = (Assembly) null;
      for (int index1 = 0; index1 < attributeRecords.Length; ++index1)
      {
        CustomAttributeRecord caRecord = attributeRecords[index1];
        IRuntimeMethodInfo ctor = (IRuntimeMethodInfo) null;
        RuntimeType attributeType = (RuntimeType) null;
        int namedArgs = 0;
        IntPtr ptr1 = caRecord.blob.Signature;
        IntPtr blobEnd = (IntPtr) ((void*) ((IntPtr) (void*) ptr1 + caRecord.blob.Length));
        int num = (int) ((sbyte*) (void*) blobEnd - (sbyte*) (void*) ptr1);
        bool ctorHasParameters;
        bool isVarArg;
        if (CustomAttribute.FilterCustomAttributeRecord(caRecord, metadataImport, ref lastAptcaOkAssembly, decoratedModule, (MetadataToken) decoratedMetadataToken, attributeFilterType, mustBeInheritable, attributeArrayHelper1, derivedAttributes, out attributeType, out ctor, out ctorHasParameters, out isVarArg))
        {
          if (ctor != null)
            RuntimeMethodHandle.CheckLinktimeDemands(ctor, decoratedModule, isDecoratedTargetSecurityTransparent);
          RuntimeConstructorInfo.CheckCanCreateInstance((Type) attributeType, isVarArg);
          object target;
          if (ctorHasParameters)
          {
            target = CustomAttribute.CreateCaObject(decoratedModule, ctor, ref ptr1, blobEnd, out namedArgs);
          }
          else
          {
            target = RuntimeTypeHandle.CreateCaInstance(attributeType, ctor);
            if (num == 0)
            {
              namedArgs = 0;
            }
            else
            {
              if (Marshal.ReadInt16(ptr1) != (short) 1)
                throw new CustomAttributeFormatException();
              IntPtr ptr2 = (IntPtr) ((void*) ((IntPtr) (void*) ptr1 + 2));
              namedArgs = (int) Marshal.ReadInt16(ptr2);
              ptr1 = (IntPtr) ((void*) ((IntPtr) (void*) ptr2 + 2));
            }
          }
          for (int index2 = 0; index2 < namedArgs; ++index2)
          {
            IntPtr signature = caRecord.blob.Signature;
            string name;
            bool isProperty;
            RuntimeType type;
            object obj;
            CustomAttribute.GetPropertyOrFieldData(decoratedModule, ref ptr1, blobEnd, out name, out isProperty, out type, out obj);
            try
            {
              if (isProperty)
              {
                if (type == (RuntimeType) null && obj != null)
                {
                  type = (RuntimeType) obj.GetType();
                  if (type == CustomAttribute.Type_RuntimeType)
                    type = CustomAttribute.Type_Type;
                }
                RuntimePropertyInfo runtimePropertyInfo = !(type == (RuntimeType) null) ? attributeType.GetProperty(name, (Type) type, Type.EmptyTypes) as RuntimePropertyInfo : attributeType.GetProperty(name) as RuntimePropertyInfo;
                if ((PropertyInfo) runtimePropertyInfo == (PropertyInfo) null)
                  throw new CustomAttributeFormatException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString(isProperty ? "RFLCT.InvalidPropFail" : "RFLCT.InvalidFieldFail"), (object) name));
                RuntimeMethodInfo setMethod = runtimePropertyInfo.GetSetMethod(true) as RuntimeMethodInfo;
                if (setMethod.IsPublic)
                {
                  RuntimeMethodHandle.CheckLinktimeDemands((IRuntimeMethodInfo) setMethod, decoratedModule, isDecoratedTargetSecurityTransparent);
                  setMethod.UnsafeInvoke(target, BindingFlags.Default, (Binder) null, new object[1]
                  {
                    obj
                  }, (CultureInfo) null);
                }
              }
              else
              {
                RtFieldInfo field = attributeType.GetField(name) as RtFieldInfo;
                if (isDecoratedTargetSecurityTransparent)
                  RuntimeFieldHandle.CheckAttributeAccess(field.FieldHandle, decoratedModule.GetNativeHandle());
                field.CheckConsistency(target);
                field.UnsafeSetValue(target, obj, BindingFlags.Default, Type.DefaultBinder, (CultureInfo) null);
              }
            }
            catch (Exception ex)
            {
              throw new CustomAttributeFormatException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString(isProperty ? "RFLCT.InvalidPropFail" : "RFLCT.InvalidFieldFail"), (object) name), ex);
            }
          }
          if (!ptr1.Equals((object) blobEnd))
            throw new CustomAttributeFormatException();
          attributeArrayHelper1[length++] = target;
        }
      }
      securityContextFrame.Pop();
      if (length == attributeRecords.Length && pcaCount == 0)
        return attributeArrayHelper1;
      object[] attributeArrayHelper2 = CustomAttribute.CreateAttributeArrayHelper(elementType, length + pcaCount);
      Array.Copy((Array) attributeArrayHelper1, 0, (Array) attributeArrayHelper2, 0, length);
      return attributeArrayHelper2;
    }

    [SecurityCritical]
    private static unsafe bool FilterCustomAttributeRecord(CustomAttributeRecord caRecord, MetadataImport scope, ref Assembly lastAptcaOkAssembly, RuntimeModule decoratedModule, MetadataToken decoratedToken, RuntimeType attributeFilterType, bool mustBeInheritable, object[] attributes, IList derivedAttributes, out RuntimeType attributeType, out IRuntimeMethodInfo ctor, out bool ctorHasParameters, out bool isVarArg)
    {
      ctor = (IRuntimeMethodInfo) null;
      attributeType = (RuntimeType) null;
      ctorHasParameters = false;
      isVarArg = false;
      IntPtr num = (IntPtr) ((void*) ((IntPtr) (void*) caRecord.blob.Signature + caRecord.blob.Length));
      attributeType = decoratedModule.ResolveType(scope.GetParentToken((int) caRecord.tkCtor), (Type[]) null, (Type[]) null) as RuntimeType;
      if (!attributeFilterType.IsAssignableFrom((TypeInfo) attributeType) || !CustomAttribute.AttributeUsageCheck(attributeType, mustBeInheritable, attributes, derivedAttributes) || (attributeType.Attributes & TypeAttributes.WindowsRuntime) == TypeAttributes.WindowsRuntime)
        return false;
      RuntimeAssembly assembly1 = (RuntimeAssembly) attributeType.Assembly;
      RuntimeAssembly assembly2 = (RuntimeAssembly) decoratedModule.Assembly;
      if ((Assembly) assembly1 != lastAptcaOkAssembly && !RuntimeAssembly.AptcaCheck(assembly1, assembly2))
        return false;
      lastAptcaOkAssembly = (Assembly) assembly2;
      ConstArray methodSignature = scope.GetMethodSignature(caRecord.tkCtor);
      isVarArg = ((uint) methodSignature[0] & 5U) > 0U;
      ctorHasParameters = methodSignature[1] > (byte) 0;
      RuntimeTypeHandle runtimeTypeHandle1;
      if (ctorHasParameters)
      {
        ctor = ModuleHandle.ResolveMethodHandleInternal(decoratedModule.GetNativeHandle(), (int) caRecord.tkCtor);
      }
      else
      {
        ref IRuntimeMethodInfo local = ref ctor;
        runtimeTypeHandle1 = attributeType.GetTypeHandleInternal();
        IRuntimeMethodInfo defaultConstructor = runtimeTypeHandle1.GetDefaultConstructor();
        local = defaultConstructor;
        if (ctor == null && !attributeType.IsValueType)
          throw new MissingMethodException(".ctor");
      }
      MetadataToken metadataToken = new MetadataToken();
      if (decoratedToken.IsParamDef)
      {
        metadataToken = new MetadataToken(scope.GetParentToken((int) decoratedToken));
        metadataToken = new MetadataToken(scope.GetParentToken((int) metadataToken));
      }
      else if (decoratedToken.IsMethodDef || decoratedToken.IsProperty || (decoratedToken.IsEvent || decoratedToken.IsFieldDef))
        metadataToken = new MetadataToken(scope.GetParentToken((int) decoratedToken));
      else if (decoratedToken.IsTypeDef)
        metadataToken = decoratedToken;
      else if (decoratedToken.IsGenericPar)
      {
        metadataToken = new MetadataToken(scope.GetParentToken((int) decoratedToken));
        if (metadataToken.IsMethodDef)
          metadataToken = new MetadataToken(scope.GetParentToken((int) metadataToken));
      }
      RuntimeTypeHandle runtimeTypeHandle2;
      if (!metadataToken.IsTypeDef)
      {
        runtimeTypeHandle1 = new RuntimeTypeHandle();
        runtimeTypeHandle2 = runtimeTypeHandle1;
      }
      else
        runtimeTypeHandle2 = decoratedModule.ModuleHandle.ResolveTypeHandle((int) metadataToken);
      RuntimeTypeHandle sourceTypeHandle = runtimeTypeHandle2;
      return RuntimeMethodHandle.IsCAVisibleFromDecoratedType(attributeType.TypeHandle, ctor, sourceTypeHandle, decoratedModule);
    }

    [SecurityCritical]
    private static bool AttributeUsageCheck(RuntimeType attributeType, bool mustBeInheritable, object[] attributes, IList derivedAttributes)
    {
      AttributeUsageAttribute attributeUsageAttribute = (AttributeUsageAttribute) null;
      if (mustBeInheritable)
      {
        attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
        if (!attributeUsageAttribute.Inherited)
          return false;
      }
      if (derivedAttributes == null)
        return true;
      for (int index = 0; index < derivedAttributes.Count; ++index)
      {
        if (derivedAttributes[index].GetType() == (Type) attributeType)
        {
          if (attributeUsageAttribute == null)
            attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
          return attributeUsageAttribute.AllowMultiple;
        }
      }
      return true;
    }

    [SecurityCritical]
    internal static AttributeUsageAttribute GetAttributeUsage(RuntimeType decoratedAttribute)
    {
      RuntimeModule runtimeModule = decoratedAttribute.GetRuntimeModule();
      MetadataImport metadataImport = runtimeModule.MetadataImport;
      CustomAttributeRecord[] attributeRecords = CustomAttributeData.GetCustomAttributeRecords(runtimeModule, decoratedAttribute.MetadataToken);
      AttributeUsageAttribute attributeUsageAttribute = (AttributeUsageAttribute) null;
      for (int index = 0; index < attributeRecords.Length; ++index)
      {
        CustomAttributeRecord customAttributeRecord = attributeRecords[index];
        RuntimeType runtimeType = runtimeModule.ResolveType(metadataImport.GetParentToken((int) customAttributeRecord.tkCtor), (Type[]) null, (Type[]) null) as RuntimeType;
        if (!(runtimeType != (RuntimeType) typeof (AttributeUsageAttribute)))
        {
          if (attributeUsageAttribute != null)
            throw new FormatException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Format_AttributeUsage"), (object) runtimeType));
          AttributeTargets targets;
          bool inherited;
          bool allowMultiple;
          CustomAttribute.ParseAttributeUsageAttribute(customAttributeRecord.blob, out targets, out inherited, out allowMultiple);
          attributeUsageAttribute = new AttributeUsageAttribute(targets, allowMultiple, inherited);
        }
      }
      return attributeUsageAttribute ?? AttributeUsageAttribute.Default;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _ParseAttributeUsageAttribute(IntPtr pCa, int cCa, out int targets, out bool inherited, out bool allowMultiple);

    [SecurityCritical]
    private static void ParseAttributeUsageAttribute(ConstArray ca, out AttributeTargets targets, out bool inherited, out bool allowMultiple)
    {
      int targets1;
      CustomAttribute._ParseAttributeUsageAttribute(ca.Signature, ca.Length, out targets1, out inherited, out allowMultiple);
      targets = (AttributeTargets) targets1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe object _CreateCaObject(RuntimeModule pModule, IRuntimeMethodInfo pCtor, byte** ppBlob, byte* pEndBlob, int* pcNamedArgs);

    [SecurityCritical]
    private static unsafe object CreateCaObject(RuntimeModule module, IRuntimeMethodInfo ctor, ref IntPtr blob, IntPtr blobEnd, out int namedArgs)
    {
      byte* numPtr = (byte*) (void*) blob;
      byte* pEndBlob = (byte*) (void*) blobEnd;
      int num;
      object caObject = CustomAttribute._CreateCaObject(module, ctor, &numPtr, pEndBlob, &num);
      blob = (IntPtr) ((void*) numPtr);
      namedArgs = num;
      return caObject;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetPropertyOrFieldData(RuntimeModule pModule, byte** ppBlobStart, byte* pBlobEnd, out string name, out bool bIsProperty, out RuntimeType type, out object value);

    [SecurityCritical]
    private static unsafe void GetPropertyOrFieldData(RuntimeModule module, ref IntPtr blobStart, IntPtr blobEnd, out string name, out bool isProperty, out RuntimeType type, out object value)
    {
      byte* numPtr = (byte*) (void*) blobStart;
      CustomAttribute._GetPropertyOrFieldData(module.GetNativeHandle(), &numPtr, (byte*) (void*) blobEnd, out name, out isProperty, out type, out value);
      blobStart = (IntPtr) ((void*) numPtr);
    }

    [SecuritySafeCritical]
    private static object[] CreateAttributeArrayHelper(Type elementType, int elementCount)
    {
      return (object[]) Array.UnsafeCreateInstance(elementType, elementCount);
    }
  }
}
