// Decompiled with JetBrains decompiler
// Type: System.RuntimeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
  [Serializable]
  internal class RuntimeType : System.Reflection.TypeInfo, ISerializable, ICloneable
  {
    internal static readonly RuntimeType ValueType = (RuntimeType) typeof (System.ValueType);
    internal static readonly RuntimeType EnumType = (RuntimeType) typeof (Enum);
    private static readonly RuntimeType ObjectType = (RuntimeType) typeof (object);
    private static readonly RuntimeType StringType = (RuntimeType) typeof (string);
    private static readonly RuntimeType DelegateType = (RuntimeType) typeof (Delegate);
    private static RuntimeType s_typedRef = (RuntimeType) typeof (TypedReference);
    private RemotingTypeCachedData m_cachedData;
    private object m_keepalive;
    private IntPtr m_cache;
    internal IntPtr m_handle;
    private INVOCATION_FLAGS m_invocationFlags;
    private static Type[] s_SICtorParamTypes;
    private const BindingFlags MemberBindingMask = (BindingFlags) 255;
    private const BindingFlags InvocationMask = BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;
    private const BindingFlags BinderNonCreateInstance = BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;
    private const BindingFlags BinderGetSetProperty = BindingFlags.GetProperty | BindingFlags.SetProperty;
    private const BindingFlags BinderSetInvokeProperty = BindingFlags.InvokeMethod | BindingFlags.SetProperty;
    private const BindingFlags BinderGetSetField = BindingFlags.GetField | BindingFlags.SetField;
    private const BindingFlags BinderSetInvokeField = BindingFlags.InvokeMethod | BindingFlags.SetField;
    private const BindingFlags BinderNonFieldGetSet = (BindingFlags) 16773888;
    private const BindingFlags ClassicBindingMask = BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;
    private static volatile RuntimeType.ActivatorCache s_ActivatorCache;
    private static volatile OleAutBinder s_ForwardCallBinder;

    internal RemotingTypeCachedData RemotingCache
    {
      get
      {
        RemotingTypeCachedData remotingTypeCachedData1 = this.m_cachedData;
        if (remotingTypeCachedData1 == null)
        {
          remotingTypeCachedData1 = new RemotingTypeCachedData(this);
          RemotingTypeCachedData remotingTypeCachedData2 = Interlocked.CompareExchange<RemotingTypeCachedData>(ref this.m_cachedData, remotingTypeCachedData1, (RemotingTypeCachedData) null);
          if (remotingTypeCachedData2 != null)
            remotingTypeCachedData1 = remotingTypeCachedData2;
        }
        return remotingTypeCachedData1;
      }
    }

    internal static RuntimeType GetType(string typeName, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      return RuntimeTypeHandle.GetTypeByName(typeName, throwOnError, ignoreCase, reflectionOnly, ref stackMark, false);
    }

    internal static MethodBase GetMethodBase(RuntimeModule scope, int typeMetadataToken)
    {
      return RuntimeType.GetMethodBase(ModuleHandle.ResolveMethodHandleInternal(scope, typeMetadataToken));
    }

    internal static MethodBase GetMethodBase(IRuntimeMethodInfo methodHandle)
    {
      return RuntimeType.GetMethodBase((RuntimeType) null, methodHandle);
    }

    [SecuritySafeCritical]
    internal static MethodBase GetMethodBase(RuntimeType reflectedType, IRuntimeMethodInfo methodHandle)
    {
      MethodBase methodBase = RuntimeType.GetMethodBase(reflectedType, methodHandle.Value);
      GC.KeepAlive((object) methodHandle);
      return methodBase;
    }

    [SecurityCritical]
    internal static MethodBase GetMethodBase(RuntimeType reflectedType, RuntimeMethodHandleInternal methodHandle)
    {
      if (RuntimeMethodHandle.IsDynamicMethod(methodHandle))
      {
        Resolver resolver = RuntimeMethodHandle.GetResolver(methodHandle);
        if (resolver != null)
          return (MethodBase) resolver.GetDynamicMethod();
        return (MethodBase) null;
      }
      RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
      RuntimeType[] methodInstantiation = (RuntimeType[]) null;
      if (reflectedType == (RuntimeType) null)
        reflectedType = declaringType;
      if (reflectedType != declaringType && !reflectedType.IsSubclassOf((Type) declaringType))
      {
        if (reflectedType.IsArray)
        {
          MethodBase[] member = reflectedType.GetMember(RuntimeMethodHandle.GetName(methodHandle), MemberTypes.Constructor | MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) as MethodBase[];
          bool flag = false;
          for (int index = 0; index < member.Length; ++index)
          {
            if (((IRuntimeMethodInfo) member[index]).Value.Value == methodHandle.Value)
              flag = true;
          }
          if (!flag)
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_ResolveMethodHandle"), (object) reflectedType.ToString(), (object) declaringType.ToString()));
        }
        else if (declaringType.IsGenericType)
        {
          RuntimeType genericTypeDefinition = (RuntimeType) declaringType.GetGenericTypeDefinition();
          RuntimeType runtimeType1;
          for (runtimeType1 = reflectedType; runtimeType1 != (RuntimeType) null; runtimeType1 = runtimeType1.GetBaseType())
          {
            RuntimeType runtimeType2 = runtimeType1;
            if (runtimeType2.IsGenericType && !runtimeType1.IsGenericTypeDefinition)
              runtimeType2 = (RuntimeType) runtimeType2.GetGenericTypeDefinition();
            if (runtimeType2 == genericTypeDefinition)
              break;
          }
          if (runtimeType1 == (RuntimeType) null)
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_ResolveMethodHandle"), (object) reflectedType.ToString(), (object) declaringType.ToString()));
          declaringType = runtimeType1;
          if (!RuntimeMethodHandle.IsGenericMethodDefinition(methodHandle))
            methodInstantiation = RuntimeMethodHandle.GetMethodInstantiationInternal(methodHandle);
          methodHandle = RuntimeMethodHandle.GetMethodFromCanonical(methodHandle, declaringType);
        }
        else if (!declaringType.IsAssignableFrom((System.Reflection.TypeInfo) reflectedType))
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_ResolveMethodHandle"), (object) reflectedType.ToString(), (object) declaringType.ToString()));
      }
      methodHandle = RuntimeMethodHandle.GetStubIfNeeded(methodHandle, declaringType, methodInstantiation);
      MethodBase methodBase = !RuntimeMethodHandle.IsConstructor(methodHandle) ? (!RuntimeMethodHandle.HasMethodInstantiation(methodHandle) || RuntimeMethodHandle.IsGenericMethodDefinition(methodHandle) ? reflectedType.Cache.GetMethod(declaringType, methodHandle) : (MethodBase) reflectedType.Cache.GetGenericMethodInfo(methodHandle)) : reflectedType.Cache.GetConstructor(declaringType, methodHandle);
      GC.KeepAlive((object) methodInstantiation);
      return methodBase;
    }

    internal object GenericCache
    {
      get
      {
        return this.Cache.GenericCache;
      }
      set
      {
        this.Cache.GenericCache = value;
      }
    }

    internal bool DomainInitialized
    {
      get
      {
        return this.Cache.DomainInitialized;
      }
      set
      {
        this.Cache.DomainInitialized = value;
      }
    }

    [SecuritySafeCritical]
    internal static FieldInfo GetFieldInfo(IRuntimeFieldInfo fieldHandle)
    {
      return RuntimeType.GetFieldInfo(RuntimeFieldHandle.GetApproxDeclaringType(fieldHandle), fieldHandle);
    }

    [SecuritySafeCritical]
    internal static FieldInfo GetFieldInfo(RuntimeType reflectedType, IRuntimeFieldInfo field)
    {
      RuntimeFieldHandleInternal field1 = field.Value;
      if (reflectedType == (RuntimeType) null)
      {
        reflectedType = RuntimeFieldHandle.GetApproxDeclaringType(field1);
      }
      else
      {
        RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field1);
        if (reflectedType != approxDeclaringType && (!RuntimeFieldHandle.AcquiresContextFromThis(field1) || !RuntimeTypeHandle.CompareCanonicalHandles(approxDeclaringType, reflectedType)))
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_ResolveFieldHandle"), (object) reflectedType.ToString(), (object) approxDeclaringType.ToString()));
      }
      FieldInfo field2 = reflectedType.Cache.GetField(field1);
      GC.KeepAlive((object) field);
      return field2;
    }

    private static PropertyInfo GetPropertyInfo(RuntimeType reflectedType, int tkProperty)
    {
      foreach (RuntimePropertyInfo property in reflectedType.Cache.GetPropertyList(RuntimeType.MemberListType.All, (string) null))
      {
        if (property.MetadataToken == tkProperty)
          return (PropertyInfo) property;
      }
      throw new SystemException();
    }

    private static void ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
    {
      if (type.IsPointer || type.IsByRef || (Type) type == typeof (void))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeverValidGenericArgument", (object) type.ToString()));
    }

    internal static void SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
    {
      if (genericArguments == null)
        throw new ArgumentNullException();
      for (int index = 0; index < genericArguments.Length; ++index)
      {
        if (genericArguments[index] == (RuntimeType) null)
          throw new ArgumentNullException();
        RuntimeType.ThrowIfTypeNeverValidGenericArgument(genericArguments[index]);
      }
      if (genericArguments.Length != genericParamters.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughGenArguments", (object) genericArguments.Length, (object) genericParamters.Length));
    }

    [SecuritySafeCritical]
    internal static void ValidateGenericArguments(MemberInfo definition, RuntimeType[] genericArguments, Exception e)
    {
      RuntimeType[] runtimeTypeArray1 = (RuntimeType[]) null;
      RuntimeType[] runtimeTypeArray2 = (RuntimeType[]) null;
      RuntimeType[] argumentsInternal;
      RuntimeTypeHandle typeHandleInternal;
      if ((object) (definition as Type) != null)
      {
        argumentsInternal = ((RuntimeType) definition).GetGenericArgumentsInternal();
        runtimeTypeArray1 = genericArguments;
      }
      else
      {
        RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo) definition;
        argumentsInternal = runtimeMethodInfo.GetGenericArgumentsInternal();
        runtimeTypeArray2 = genericArguments;
        RuntimeType declaringType = (RuntimeType) runtimeMethodInfo.DeclaringType;
        if (declaringType != (RuntimeType) null)
        {
          typeHandleInternal = declaringType.GetTypeHandleInternal();
          runtimeTypeArray1 = typeHandleInternal.GetInstantiationInternal();
        }
      }
      for (int index = 0; index < genericArguments.Length; ++index)
      {
        Type genericArgument = (Type) genericArguments[index];
        Type type = (Type) argumentsInternal[index];
        typeHandleInternal = type.GetTypeHandleInternal();
        RuntimeType typeChecked1 = typeHandleInternal.GetTypeChecked();
        RuntimeType[] typeContext = runtimeTypeArray1;
        RuntimeType[] methodContext = runtimeTypeArray2;
        typeHandleInternal = genericArgument.GetTypeHandleInternal();
        RuntimeType typeChecked2 = typeHandleInternal.GetTypeChecked();
        if (!RuntimeTypeHandle.SatisfiesConstraints(typeChecked1, typeContext, methodContext, typeChecked2))
          throw new ArgumentException(Environment.GetResourceString("Argument_GenConstraintViolation", (object) index.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) genericArgument.ToString(), (object) definition.ToString(), (object) type.ToString()), e);
      }
    }

    private static void SplitName(string fullname, out string name, out string ns)
    {
      name = (string) null;
      ns = (string) null;
      if (fullname == null)
        return;
      int length1 = fullname.LastIndexOf(".", StringComparison.Ordinal);
      if (length1 != -1)
      {
        ns = fullname.Substring(0, length1);
        int length2 = fullname.Length - ns.Length - 1;
        if (length2 != 0)
          name = fullname.Substring(length1 + 1, length2);
        else
          name = "";
      }
      else
        name = fullname;
    }

    internal static BindingFlags FilterPreCalculate(bool isPublic, bool isInherited, bool isStatic)
    {
      BindingFlags bindingFlags1 = isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
      BindingFlags bindingFlags2;
      if (isInherited)
      {
        BindingFlags bindingFlags3 = bindingFlags1 | BindingFlags.DeclaredOnly;
        bindingFlags2 = !isStatic ? bindingFlags3 | BindingFlags.Instance : bindingFlags3 | BindingFlags.Static | BindingFlags.FlattenHierarchy;
      }
      else
        bindingFlags2 = !isStatic ? bindingFlags1 | BindingFlags.Instance : bindingFlags1 | BindingFlags.Static;
      return bindingFlags2;
    }

    private static void FilterHelper(BindingFlags bindingFlags, ref string name, bool allowPrefixLookup, out bool prefixLookup, out bool ignoreCase, out RuntimeType.MemberListType listType)
    {
      prefixLookup = false;
      ignoreCase = false;
      if (name != null)
      {
        if ((bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default)
        {
          name = name.ToLower(CultureInfo.InvariantCulture);
          ignoreCase = true;
          listType = RuntimeType.MemberListType.CaseInsensitive;
        }
        else
          listType = RuntimeType.MemberListType.CaseSensitive;
        if (!allowPrefixLookup || !name.EndsWith("*", StringComparison.Ordinal))
          return;
        name = name.Substring(0, name.Length - 1);
        prefixLookup = true;
        listType = RuntimeType.MemberListType.All;
      }
      else
        listType = RuntimeType.MemberListType.All;
    }

    private static void FilterHelper(BindingFlags bindingFlags, ref string name, out bool ignoreCase, out RuntimeType.MemberListType listType)
    {
      bool prefixLookup;
      RuntimeType.FilterHelper(bindingFlags, ref name, false, out prefixLookup, out ignoreCase, out listType);
    }

    private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
    {
      if (ignoreCase)
      {
        if (!memberInfo.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
          return false;
      }
      else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
        return false;
      return true;
    }

    private static bool FilterApplyBase(MemberInfo memberInfo, BindingFlags bindingFlags, bool isPublic, bool isNonProtectedInternal, bool isStatic, string name, bool prefixLookup)
    {
      if (isPublic)
      {
        if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
          return false;
      }
      else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
        return false;
      bool flag = (object) memberInfo.DeclaringType != (object) memberInfo.ReflectedType;
      if ((uint) (bindingFlags & BindingFlags.DeclaredOnly) > 0U & flag)
        return false;
      if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
      {
        if (isStatic)
        {
          if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default & flag || (bindingFlags & BindingFlags.Static) == BindingFlags.Default)
            return false;
        }
        else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
          return false;
      }
      if (prefixLookup && !RuntimeType.FilterApplyPrefixLookup(memberInfo, name, (uint) (bindingFlags & BindingFlags.IgnoreCase) > 0U))
        return false;
      if ((bindingFlags & BindingFlags.DeclaredOnly) == BindingFlags.Default & flag & isNonProtectedInternal && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.Default && (!isStatic && (bindingFlags & BindingFlags.Instance) != BindingFlags.Default))
      {
        MethodInfo methodInfo = memberInfo as MethodInfo;
        if (methodInfo == (MethodInfo) null || !methodInfo.IsVirtual && !methodInfo.IsAbstract)
          return false;
      }
      return true;
    }

    private static bool FilterApplyType(Type type, BindingFlags bindingFlags, string name, bool prefixLookup, string ns)
    {
      bool isPublic = type.IsNestedPublic || type.IsPublic;
      bool isStatic = false;
      return RuntimeType.FilterApplyBase((MemberInfo) type, bindingFlags, isPublic, type.IsNestedAssembly, isStatic, name, prefixLookup) && (ns == null || type.Namespace.Equals(ns));
    }

    private static bool FilterApplyMethodInfo(RuntimeMethodInfo method, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
    {
      return RuntimeType.FilterApplyMethodBase((MethodBase) method, method.BindingFlags, bindingFlags, callConv, argumentTypes);
    }

    private static bool FilterApplyConstructorInfo(RuntimeConstructorInfo constructor, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
    {
      return RuntimeType.FilterApplyMethodBase((MethodBase) constructor, constructor.BindingFlags, bindingFlags, callConv, argumentTypes);
    }

    private static bool FilterApplyMethodBase(MethodBase methodBase, BindingFlags methodFlags, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
    {
      bindingFlags ^= BindingFlags.DeclaredOnly;
      if ((bindingFlags & methodFlags) != methodFlags || (callConv & CallingConventions.Any) == (CallingConventions) 0 && ((callConv & CallingConventions.VarArgs) != (CallingConventions) 0 && (methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0 || (callConv & CallingConventions.Standard) != (CallingConventions) 0 && (methodBase.CallingConvention & CallingConventions.Standard) == (CallingConventions) 0))
        return false;
      if (argumentTypes != null)
      {
        ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
        if (argumentTypes.Length != parametersNoCopy.Length)
        {
          if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
            return false;
          bool flag = false;
          if (argumentTypes.Length > parametersNoCopy.Length)
          {
            if ((methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
              flag = true;
          }
          else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
            flag = true;
          else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
            flag = true;
          if (flag)
          {
            if (parametersNoCopy.Length == 0 || argumentTypes.Length < parametersNoCopy.Length - 1)
              return false;
            ParameterInfo parameterInfo = parametersNoCopy[parametersNoCopy.Length - 1];
            if (!parameterInfo.ParameterType.IsArray || !parameterInfo.IsDefined(typeof (ParamArrayAttribute), false))
              return false;
          }
        }
        else if ((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default && (bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default)
        {
          for (int index = 0; index < parametersNoCopy.Length; ++index)
          {
            if ((object) argumentTypes[index] != null && (object) parametersNoCopy[index].ParameterType != (object) argumentTypes[index])
              return false;
          }
        }
      }
      return true;
    }

    internal bool IsNonW8PFrameworkAPI()
    {
      if (this.IsGenericParameter)
        return false;
      if (this.HasElementType)
        return ((RuntimeType) this.GetElementType()).IsNonW8PFrameworkAPI();
      if (this.IsSimpleTypeNonW8PFrameworkAPI())
        return true;
      if (this.IsGenericType && !this.IsGenericTypeDefinition)
      {
        foreach (RuntimeType genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.IsNonW8PFrameworkAPI())
            return true;
        }
      }
      return false;
    }

    private bool IsSimpleTypeNonW8PFrameworkAPI()
    {
      RuntimeAssembly runtimeAssembly = this.GetRuntimeAssembly();
      if (runtimeAssembly.IsFrameworkAssembly())
      {
        int attributeCtorToken = runtimeAssembly.InvocableAttributeCtorToken;
        if (System.Reflection.MetadataToken.IsNullToken(attributeCtorToken) || !CustomAttribute.IsAttributeDefined(this.GetRuntimeModule(), this.MetadataToken, attributeCtorToken))
          return true;
      }
      return false;
    }

    internal INVOCATION_FLAGS InvocationFlags
    {
      get
      {
        if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        {
          INVOCATION_FLAGS invocationFlags = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
          if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
            invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
          this.m_invocationFlags = invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED;
        }
        return this.m_invocationFlags;
      }
    }

    internal RuntimeType()
    {
      throw new NotSupportedException();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RuntimeType runtimeType = o as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        return false;
      return runtimeType.m_handle.Equals((object) this.m_handle);
    }

    private RuntimeType.RuntimeTypeCache Cache
    {
      [SecuritySafeCritical] get
      {
        if (this.m_cache.IsNull())
        {
          IntPtr gcHandle = new RuntimeTypeHandle(this).GetGCHandle(GCHandleType.WeakTrackResurrection);
          if (!Interlocked.CompareExchange(ref this.m_cache, gcHandle, (IntPtr) 0).IsNull() && !this.IsCollectible())
            GCHandle.InternalFree(gcHandle);
        }
        RuntimeType.RuntimeTypeCache runtimeTypeCache1 = GCHandle.InternalGet(this.m_cache) as RuntimeType.RuntimeTypeCache;
        if (runtimeTypeCache1 == null)
        {
          runtimeTypeCache1 = new RuntimeType.RuntimeTypeCache(this);
          RuntimeType.RuntimeTypeCache runtimeTypeCache2 = GCHandle.InternalCompareExchange(this.m_cache, (object) runtimeTypeCache1, (object) null, false) as RuntimeType.RuntimeTypeCache;
          if (runtimeTypeCache2 != null)
            runtimeTypeCache1 = runtimeTypeCache2;
        }
        return runtimeTypeCache1;
      }
    }

    internal bool IsSpecialSerializableType()
    {
      RuntimeType runtimeType = this;
      while (!(runtimeType == RuntimeType.DelegateType) && !(runtimeType == RuntimeType.EnumType))
      {
        runtimeType = runtimeType.GetBaseType();
        if (!(runtimeType != (RuntimeType) null))
          return false;
      }
      return true;
    }

    private string GetDefaultMemberName()
    {
      return this.Cache.GetDefaultMemberName();
    }

    internal RuntimeConstructorInfo GetSerializationCtor()
    {
      return this.Cache.GetSerializationCtor();
    }

    private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
    {
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimeMethodInfo[] methodList = this.Cache.GetMethodList(listType, name);
      RuntimeType.ListBuilder<MethodInfo> listBuilder = new RuntimeType.ListBuilder<MethodInfo>(methodList.Length);
      for (int index = 0; index < methodList.Length; ++index)
      {
        RuntimeMethodInfo method = methodList[index];
        if (RuntimeType.FilterApplyMethodInfo(method, bindingAttr, callConv, types) && (!prefixLookup || RuntimeType.FilterApplyPrefixLookup((MemberInfo) method, name, ignoreCase)))
          listBuilder.Add((MethodInfo) method);
      }
      return listBuilder;
    }

    private RuntimeType.ListBuilder<ConstructorInfo> GetConstructorCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
    {
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimeConstructorInfo[] constructorList = this.Cache.GetConstructorList(listType, name);
      RuntimeType.ListBuilder<ConstructorInfo> listBuilder = new RuntimeType.ListBuilder<ConstructorInfo>(constructorList.Length);
      for (int index = 0; index < constructorList.Length; ++index)
      {
        RuntimeConstructorInfo constructor = constructorList[index];
        if (RuntimeType.FilterApplyConstructorInfo(constructor, bindingAttr, callConv, types) && (!prefixLookup || RuntimeType.FilterApplyPrefixLookup((MemberInfo) constructor, name, ignoreCase)))
          listBuilder.Add((ConstructorInfo) constructor);
      }
      return listBuilder;
    }

    private RuntimeType.ListBuilder<PropertyInfo> GetPropertyCandidates(string name, BindingFlags bindingAttr, Type[] types, bool allowPrefixLookup)
    {
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimePropertyInfo[] propertyList = this.Cache.GetPropertyList(listType, name);
      bindingAttr ^= BindingFlags.DeclaredOnly;
      RuntimeType.ListBuilder<PropertyInfo> listBuilder = new RuntimeType.ListBuilder<PropertyInfo>(propertyList.Length);
      for (int index = 0; index < propertyList.Length; ++index)
      {
        RuntimePropertyInfo runtimePropertyInfo = propertyList[index];
        if ((bindingAttr & runtimePropertyInfo.BindingFlags) == runtimePropertyInfo.BindingFlags && (!prefixLookup || RuntimeType.FilterApplyPrefixLookup((MemberInfo) runtimePropertyInfo, name, ignoreCase)) && (types == null || runtimePropertyInfo.GetIndexParameters().Length == types.Length))
          listBuilder.Add((PropertyInfo) runtimePropertyInfo);
      }
      return listBuilder;
    }

    private RuntimeType.ListBuilder<EventInfo> GetEventCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
    {
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimeEventInfo[] eventList = this.Cache.GetEventList(listType, name);
      bindingAttr ^= BindingFlags.DeclaredOnly;
      RuntimeType.ListBuilder<EventInfo> listBuilder = new RuntimeType.ListBuilder<EventInfo>(eventList.Length);
      for (int index = 0; index < eventList.Length; ++index)
      {
        RuntimeEventInfo runtimeEventInfo = eventList[index];
        if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags && (!prefixLookup || RuntimeType.FilterApplyPrefixLookup((MemberInfo) runtimeEventInfo, name, ignoreCase)))
          listBuilder.Add((EventInfo) runtimeEventInfo);
      }
      return listBuilder;
    }

    private RuntimeType.ListBuilder<FieldInfo> GetFieldCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
    {
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimeFieldInfo[] fieldList = this.Cache.GetFieldList(listType, name);
      bindingAttr ^= BindingFlags.DeclaredOnly;
      RuntimeType.ListBuilder<FieldInfo> listBuilder = new RuntimeType.ListBuilder<FieldInfo>(fieldList.Length);
      for (int index = 0; index < fieldList.Length; ++index)
      {
        RuntimeFieldInfo runtimeFieldInfo = fieldList[index];
        if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags && (!prefixLookup || RuntimeType.FilterApplyPrefixLookup((MemberInfo) runtimeFieldInfo, name, ignoreCase)))
          listBuilder.Add((FieldInfo) runtimeFieldInfo);
      }
      return listBuilder;
    }

    private RuntimeType.ListBuilder<Type> GetNestedTypeCandidates(string fullname, BindingFlags bindingAttr, bool allowPrefixLookup)
    {
      bindingAttr &= ~BindingFlags.Static;
      string name;
      string ns;
      RuntimeType.SplitName(fullname, out name, out ns);
      bool prefixLookup;
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out ignoreCase, out listType);
      RuntimeType[] nestedTypeList = this.Cache.GetNestedTypeList(listType, name);
      RuntimeType.ListBuilder<Type> listBuilder = new RuntimeType.ListBuilder<Type>(nestedTypeList.Length);
      for (int index = 0; index < nestedTypeList.Length; ++index)
      {
        RuntimeType runtimeType = nestedTypeList[index];
        if (RuntimeType.FilterApplyType((Type) runtimeType, bindingAttr, name, prefixLookup, ns))
          listBuilder.Add((Type) runtimeType);
      }
      return listBuilder;
    }

    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.GetMethodCandidates((string) null, bindingAttr, CallingConventions.Any, (Type[]) null, false).ToArray();
    }

    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return this.GetConstructorCandidates((string) null, bindingAttr, CallingConventions.Any, (Type[]) null, false).ToArray();
    }

    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.GetPropertyCandidates((string) null, bindingAttr, (Type[]) null, false).ToArray();
    }

    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return this.GetEventCandidates((string) null, bindingAttr, false).ToArray();
    }

    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.GetFieldCandidates((string) null, bindingAttr, false).ToArray();
    }

    [SecuritySafeCritical]
    public override Type[] GetInterfaces()
    {
      RuntimeType[] interfaceList = this.Cache.GetInterfaceList(RuntimeType.MemberListType.All, (string) null);
      Type[] typeArray = new Type[interfaceList.Length];
      for (int index = 0; index < interfaceList.Length; ++index)
        JitHelpers.UnsafeSetArrayElement((object[]) typeArray, index, (object) interfaceList[index]);
      return typeArray;
    }

    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return this.GetNestedTypeCandidates((string) null, bindingAttr, false).ToArray();
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates((string) null, bindingAttr, CallingConventions.Any, (Type[]) null, false);
      RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates((string) null, bindingAttr, CallingConventions.Any, (Type[]) null, false);
      RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates((string) null, bindingAttr, (Type[]) null, false);
      RuntimeType.ListBuilder<EventInfo> eventCandidates = this.GetEventCandidates((string) null, bindingAttr, false);
      RuntimeType.ListBuilder<FieldInfo> fieldCandidates = this.GetFieldCandidates((string) null, bindingAttr, false);
      RuntimeType.ListBuilder<Type> nestedTypeCandidates = this.GetNestedTypeCandidates((string) null, bindingAttr, false);
      MemberInfo[] memberInfoArray = new MemberInfo[methodCandidates.Count + constructorCandidates.Count + propertyCandidates.Count + eventCandidates.Count + fieldCandidates.Count + nestedTypeCandidates.Count];
      int index1 = 0;
      methodCandidates.CopyTo((object[]) memberInfoArray, index1);
      int index2 = index1 + methodCandidates.Count;
      constructorCandidates.CopyTo((object[]) memberInfoArray, index2);
      int index3 = index2 + constructorCandidates.Count;
      propertyCandidates.CopyTo((object[]) memberInfoArray, index3);
      int index4 = index3 + propertyCandidates.Count;
      eventCandidates.CopyTo((object[]) memberInfoArray, index4);
      int index5 = index4 + eventCandidates.Count;
      fieldCandidates.CopyTo((object[]) memberInfoArray, index5);
      int index6 = index5 + fieldCandidates.Count;
      nestedTypeCandidates.CopyTo((object[]) memberInfoArray, index6);
      int num = index6 + nestedTypeCandidates.Count;
      return memberInfoArray;
    }

    [SecuritySafeCritical]
    public override InterfaceMapping GetInterfaceMap(Type ifaceType)
    {
      if (this.IsGenericParameter)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_GenericParameter"));
      if ((object) ifaceType == null)
        throw new ArgumentNullException(nameof (ifaceType));
      RuntimeType runtimeType = ifaceType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (ifaceType));
      RuntimeTypeHandle typeHandleInternal1 = runtimeType.GetTypeHandleInternal();
      RuntimeTypeHandle typeHandleInternal2 = this.GetTypeHandleInternal();
      typeHandleInternal2.VerifyInterfaceIsImplemented(typeHandleInternal1);
      if (this.IsSzArray && ifaceType.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArrayGetInterfaceMap"));
      int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
      InterfaceMapping interfaceMapping;
      interfaceMapping.InterfaceType = ifaceType;
      interfaceMapping.TargetType = (Type) this;
      interfaceMapping.InterfaceMethods = new MethodInfo[numVirtuals];
      interfaceMapping.TargetMethods = new MethodInfo[numVirtuals];
      for (int slot = 0; slot < numVirtuals; ++slot)
      {
        RuntimeMethodHandleInternal methodAt = RuntimeTypeHandle.GetMethodAt(runtimeType, slot);
        MethodBase methodBase1 = RuntimeType.GetMethodBase(runtimeType, methodAt);
        interfaceMapping.InterfaceMethods[slot] = (MethodInfo) methodBase1;
        typeHandleInternal2 = this.GetTypeHandleInternal();
        int implementationSlot = typeHandleInternal2.GetInterfaceMethodImplementationSlot(typeHandleInternal1, methodAt);
        if (implementationSlot != -1)
        {
          MethodBase methodBase2 = RuntimeType.GetMethodBase(this, RuntimeTypeHandle.GetMethodAt(this, implementationSlot));
          interfaceMapping.TargetMethods[slot] = (MethodInfo) methodBase2;
        }
      }
      return interfaceMapping;
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
    {
      RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(name, bindingAttr, callConv, types, false);
      if (methodCandidates.Count == 0)
        return (MethodInfo) null;
      if (types == null || types.Length == 0)
      {
        MethodInfo methodInfo = methodCandidates[0];
        if (methodCandidates.Count == 1)
          return methodInfo;
        if (types == null)
        {
          for (int index = 1; index < methodCandidates.Count; ++index)
          {
            if (!DefaultBinder.CompareMethodSigAndName((MethodBase) methodCandidates[index], (MethodBase) methodInfo))
              throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
          }
          return DefaultBinder.FindMostDerivedNewSlotMeth((MethodBase[]) methodCandidates.ToArray(), methodCandidates.Count) as MethodInfo;
        }
      }
      if (binder == null)
        binder = Type.DefaultBinder;
      return binder.SelectMethod(bindingAttr, (MethodBase[]) methodCandidates.ToArray(), types, modifiers) as MethodInfo;
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates((string) null, bindingAttr, CallingConventions.Any, types, false);
      if (constructorCandidates.Count == 0)
        return (ConstructorInfo) null;
      if (types.Length == 0 && constructorCandidates.Count == 1)
      {
        ConstructorInfo constructorInfo = constructorCandidates[0];
        ParameterInfo[] parametersNoCopy = constructorInfo.GetParametersNoCopy();
        if (parametersNoCopy == null || parametersNoCopy.Length == 0)
          return constructorInfo;
      }
      if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
        return DefaultBinder.ExactBinding((MethodBase[]) constructorCandidates.ToArray(), types, modifiers) as ConstructorInfo;
      if (binder == null)
        binder = Type.DefaultBinder;
      return binder.SelectMethod(bindingAttr, (MethodBase[]) constructorCandidates.ToArray(), types, modifiers) as ConstructorInfo;
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException();
      RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(name, bindingAttr, types, false);
      if (propertyCandidates.Count == 0)
        return (PropertyInfo) null;
      if (types == null || types.Length == 0)
      {
        if (propertyCandidates.Count == 1)
        {
          PropertyInfo propertyInfo = propertyCandidates[0];
          if ((object) returnType != null && !returnType.IsEquivalentTo(propertyInfo.PropertyType))
            return (PropertyInfo) null;
          return propertyInfo;
        }
        if ((object) returnType == null)
          throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      }
      if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
        return DefaultBinder.ExactPropertyBinding(propertyCandidates.ToArray(), returnType, types, modifiers);
      if (binder == null)
        binder = Type.DefaultBinder;
      return binder.SelectProperty(bindingAttr, propertyCandidates.ToArray(), returnType, types, modifiers);
    }

    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException();
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, out ignoreCase, out listType);
      RuntimeEventInfo[] eventList = this.Cache.GetEventList(listType, name);
      EventInfo eventInfo = (EventInfo) null;
      bindingAttr ^= BindingFlags.DeclaredOnly;
      for (int index = 0; index < eventList.Length; ++index)
      {
        RuntimeEventInfo runtimeEventInfo = eventList[index];
        if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags)
        {
          if (eventInfo != (EventInfo) null)
            throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
          eventInfo = (EventInfo) runtimeEventInfo;
        }
      }
      return eventInfo;
    }

    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException();
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, out ignoreCase, out listType);
      RuntimeFieldInfo[] fieldList = this.Cache.GetFieldList(listType, name);
      FieldInfo fieldInfo = (FieldInfo) null;
      bindingAttr ^= BindingFlags.DeclaredOnly;
      bool flag = false;
      for (int index = 0; index < fieldList.Length; ++index)
      {
        RuntimeFieldInfo runtimeFieldInfo = fieldList[index];
        if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags)
        {
          if (fieldInfo != (FieldInfo) null)
          {
            if ((object) runtimeFieldInfo.DeclaringType == (object) fieldInfo.DeclaringType)
              throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
            if (fieldInfo.DeclaringType.IsInterface && runtimeFieldInfo.DeclaringType.IsInterface)
              flag = true;
          }
          if (fieldInfo == (FieldInfo) null || runtimeFieldInfo.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) || fieldInfo.DeclaringType.IsInterface)
            fieldInfo = (FieldInfo) runtimeFieldInfo;
        }
      }
      if (flag && fieldInfo.DeclaringType.IsInterface)
        throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      return fieldInfo;
    }

    public override Type GetInterface(string fullname, bool ignoreCase)
    {
      if (fullname == null)
        throw new ArgumentNullException();
      BindingFlags bindingFlags = (BindingFlags) (48 & -9);
      if (ignoreCase)
        bindingFlags |= BindingFlags.IgnoreCase;
      string name;
      string ns;
      RuntimeType.SplitName(fullname, out name, out ns);
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingFlags, ref name, out ignoreCase, out listType);
      RuntimeType[] interfaceList = this.Cache.GetInterfaceList(listType, name);
      RuntimeType runtimeType1 = (RuntimeType) null;
      for (int index = 0; index < interfaceList.Length; ++index)
      {
        RuntimeType runtimeType2 = interfaceList[index];
        if (RuntimeType.FilterApplyType((Type) runtimeType2, bindingFlags, name, false, ns))
        {
          if (runtimeType1 != (RuntimeType) null)
            throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
          runtimeType1 = runtimeType2;
        }
      }
      return (Type) runtimeType1;
    }

    public override Type GetNestedType(string fullname, BindingFlags bindingAttr)
    {
      if (fullname == null)
        throw new ArgumentNullException();
      bindingAttr &= ~BindingFlags.Static;
      string name;
      string ns;
      RuntimeType.SplitName(fullname, out name, out ns);
      bool ignoreCase;
      RuntimeType.MemberListType listType;
      RuntimeType.FilterHelper(bindingAttr, ref name, out ignoreCase, out listType);
      RuntimeType[] nestedTypeList = this.Cache.GetNestedTypeList(listType, name);
      RuntimeType runtimeType1 = (RuntimeType) null;
      for (int index = 0; index < nestedTypeList.Length; ++index)
      {
        RuntimeType runtimeType2 = nestedTypeList[index];
        if (RuntimeType.FilterApplyType((Type) runtimeType2, bindingAttr, name, false, ns))
        {
          if (runtimeType1 != (RuntimeType) null)
            throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
          runtimeType1 = runtimeType2;
        }
      }
      return (Type) runtimeType1;
    }

    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException();
      RuntimeType.ListBuilder<MethodInfo> listBuilder1 = new RuntimeType.ListBuilder<MethodInfo>();
      RuntimeType.ListBuilder<ConstructorInfo> listBuilder2 = new RuntimeType.ListBuilder<ConstructorInfo>();
      RuntimeType.ListBuilder<PropertyInfo> listBuilder3 = new RuntimeType.ListBuilder<PropertyInfo>();
      RuntimeType.ListBuilder<EventInfo> listBuilder4 = new RuntimeType.ListBuilder<EventInfo>();
      RuntimeType.ListBuilder<FieldInfo> listBuilder5 = new RuntimeType.ListBuilder<FieldInfo>();
      RuntimeType.ListBuilder<Type> listBuilder6 = new RuntimeType.ListBuilder<Type>();
      int length = 0;
      if ((type & MemberTypes.Method) != (MemberTypes) 0)
      {
        listBuilder1 = this.GetMethodCandidates(name, bindingAttr, CallingConventions.Any, (Type[]) null, true);
        if (type == MemberTypes.Method)
          return (MemberInfo[]) listBuilder1.ToArray();
        length += listBuilder1.Count;
      }
      if ((type & MemberTypes.Constructor) != (MemberTypes) 0)
      {
        listBuilder2 = this.GetConstructorCandidates(name, bindingAttr, CallingConventions.Any, (Type[]) null, true);
        if (type == MemberTypes.Constructor)
          return (MemberInfo[]) listBuilder2.ToArray();
        length += listBuilder2.Count;
      }
      if ((type & MemberTypes.Property) != (MemberTypes) 0)
      {
        listBuilder3 = this.GetPropertyCandidates(name, bindingAttr, (Type[]) null, true);
        if (type == MemberTypes.Property)
          return (MemberInfo[]) listBuilder3.ToArray();
        length += listBuilder3.Count;
      }
      if ((type & MemberTypes.Event) != (MemberTypes) 0)
      {
        listBuilder4 = this.GetEventCandidates(name, bindingAttr, true);
        if (type == MemberTypes.Event)
          return (MemberInfo[]) listBuilder4.ToArray();
        length += listBuilder4.Count;
      }
      if ((type & MemberTypes.Field) != (MemberTypes) 0)
      {
        listBuilder5 = this.GetFieldCandidates(name, bindingAttr, true);
        if (type == MemberTypes.Field)
          return (MemberInfo[]) listBuilder5.ToArray();
        length += listBuilder5.Count;
      }
      if ((type & (MemberTypes.TypeInfo | MemberTypes.NestedType)) != (MemberTypes) 0)
      {
        listBuilder6 = this.GetNestedTypeCandidates(name, bindingAttr, true);
        if (type == MemberTypes.NestedType || type == MemberTypes.TypeInfo)
          return (MemberInfo[]) listBuilder6.ToArray();
        length += listBuilder6.Count;
      }
      MemberInfo[] memberInfoArray = type == (MemberTypes.Constructor | MemberTypes.Method) ? (MemberInfo[]) new MethodBase[length] : new MemberInfo[length];
      int index1 = 0;
      listBuilder1.CopyTo((object[]) memberInfoArray, index1);
      int index2 = index1 + listBuilder1.Count;
      listBuilder2.CopyTo((object[]) memberInfoArray, index2);
      int index3 = index2 + listBuilder2.Count;
      listBuilder3.CopyTo((object[]) memberInfoArray, index3);
      int index4 = index3 + listBuilder3.Count;
      listBuilder4.CopyTo((object[]) memberInfoArray, index4);
      int index5 = index4 + listBuilder4.Count;
      listBuilder5.CopyTo((object[]) memberInfoArray, index5);
      int index6 = index5 + listBuilder5.Count;
      listBuilder6.CopyTo((object[]) memberInfoArray, index6);
      int num = index6 + listBuilder6.Count;
      return memberInfoArray;
    }

    public override Module Module
    {
      get
      {
        return (Module) this.GetRuntimeModule();
      }
    }

    internal RuntimeModule GetRuntimeModule()
    {
      return RuntimeTypeHandle.GetModule(this);
    }

    public override Assembly Assembly
    {
      get
      {
        return (Assembly) this.GetRuntimeAssembly();
      }
    }

    internal RuntimeAssembly GetRuntimeAssembly()
    {
      return RuntimeTypeHandle.GetAssembly(this);
    }

    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return new RuntimeTypeHandle(this);
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override sealed RuntimeTypeHandle GetTypeHandleInternal()
    {
      return new RuntimeTypeHandle(this);
    }

    [SecuritySafeCritical]
    internal bool IsCollectible()
    {
      return RuntimeTypeHandle.IsCollectible(this.GetTypeHandleInternal());
    }

    [SecuritySafeCritical]
    protected override TypeCode GetTypeCodeImpl()
    {
      TypeCode typeCode1 = this.Cache.TypeCode;
      if (typeCode1 != TypeCode.Empty)
        return typeCode1;
      TypeCode typeCode2;
      switch (RuntimeTypeHandle.GetCorElementType(this))
      {
        case CorElementType.Boolean:
          typeCode2 = TypeCode.Boolean;
          break;
        case CorElementType.Char:
          typeCode2 = TypeCode.Char;
          break;
        case CorElementType.I1:
          typeCode2 = TypeCode.SByte;
          break;
        case CorElementType.U1:
          typeCode2 = TypeCode.Byte;
          break;
        case CorElementType.I2:
          typeCode2 = TypeCode.Int16;
          break;
        case CorElementType.U2:
          typeCode2 = TypeCode.UInt16;
          break;
        case CorElementType.I4:
          typeCode2 = TypeCode.Int32;
          break;
        case CorElementType.U4:
          typeCode2 = TypeCode.UInt32;
          break;
        case CorElementType.I8:
          typeCode2 = TypeCode.Int64;
          break;
        case CorElementType.U8:
          typeCode2 = TypeCode.UInt64;
          break;
        case CorElementType.R4:
          typeCode2 = TypeCode.Single;
          break;
        case CorElementType.R8:
          typeCode2 = TypeCode.Double;
          break;
        case CorElementType.String:
          typeCode2 = TypeCode.String;
          break;
        case CorElementType.ValueType:
          typeCode2 = !(this == Convert.ConvertTypes[15]) ? (!(this == Convert.ConvertTypes[16]) ? (!this.IsEnum ? TypeCode.Object : Type.GetTypeCode(Enum.GetUnderlyingType((Type) this))) : TypeCode.DateTime) : TypeCode.Decimal;
          break;
        default:
          typeCode2 = !(this == Convert.ConvertTypes[2]) ? (!(this == Convert.ConvertTypes[18]) ? TypeCode.Object : TypeCode.String) : TypeCode.DBNull;
          break;
      }
      this.Cache.TypeCode = typeCode2;
      return typeCode2;
    }

    public override MethodBase DeclaringMethod
    {
      get
      {
        if (!this.IsGenericParameter)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
        IRuntimeMethodInfo declaringMethod = RuntimeTypeHandle.GetDeclaringMethod(this);
        if (declaringMethod == null)
          return (MethodBase) null;
        return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetDeclaringType(declaringMethod), declaringMethod);
      }
    }

    [SecuritySafeCritical]
    public override bool IsInstanceOfType(object o)
    {
      return RuntimeTypeHandle.IsInstanceOfType(this, o);
    }

    [ComVisible(true)]
    public override bool IsSubclassOf(Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        return false;
      for (RuntimeType baseType = this.GetBaseType(); baseType != (RuntimeType) null; baseType = baseType.GetBaseType())
      {
        if (baseType == runtimeType)
          return true;
      }
      return runtimeType == RuntimeType.ObjectType && runtimeType != this;
    }

    public override bool IsAssignableFrom(System.Reflection.TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    public override bool IsAssignableFrom(Type c)
    {
      if ((object) c == null)
        return false;
      if ((object) c == (object) this)
        return true;
      RuntimeType underlyingSystemType = c.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType != (RuntimeType) null)
        return RuntimeTypeHandle.CanCastTo(underlyingSystemType, this);
      if (c is TypeBuilder)
      {
        if (c.IsSubclassOf((Type) this))
          return true;
        if (this.IsInterface)
          return c.ImplementInterface((Type) this);
        if (this.IsGenericParameter)
        {
          foreach (Type parameterConstraint in this.GetGenericParameterConstraints())
          {
            if (!parameterConstraint.IsAssignableFrom(c))
              return false;
          }
          return true;
        }
      }
      return false;
    }

    public override bool IsEquivalentTo(Type other)
    {
      RuntimeType rtType2 = other as RuntimeType;
      if ((object) rtType2 == null)
        return false;
      if (rtType2 == this)
        return true;
      return RuntimeTypeHandle.IsEquivalentTo(this, rtType2);
    }

    public override Type BaseType
    {
      get
      {
        return (Type) this.GetBaseType();
      }
    }

    private RuntimeType GetBaseType()
    {
      if (this.IsInterface)
        return (RuntimeType) null;
      if (!RuntimeTypeHandle.IsGenericVariable(this))
        return RuntimeTypeHandle.GetBaseType(this);
      Type[] parameterConstraints = this.GetGenericParameterConstraints();
      RuntimeType runtimeType1 = RuntimeType.ObjectType;
      for (int index = 0; index < parameterConstraints.Length; ++index)
      {
        RuntimeType runtimeType2 = (RuntimeType) parameterConstraints[index];
        if (!runtimeType2.IsInterface)
        {
          if (runtimeType2.IsGenericParameter)
          {
            GenericParameterAttributes parameterAttributes = runtimeType2.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
            if ((parameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.None && (parameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.None)
              continue;
          }
          runtimeType1 = runtimeType2;
        }
      }
      if (runtimeType1 == RuntimeType.ObjectType && (this.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
        runtimeType1 = RuntimeType.ValueType;
      return runtimeType1;
    }

    public override Type UnderlyingSystemType
    {
      get
      {
        return (Type) this;
      }
    }

    public override string FullName
    {
      get
      {
        return this.GetCachedName(TypeNameKind.FullName);
      }
    }

    public override string AssemblyQualifiedName
    {
      get
      {
        string fullName = this.FullName;
        if (fullName == null)
          return (string) null;
        return Assembly.CreateQualifiedName(this.Assembly.FullName, fullName);
      }
    }

    public override string Namespace
    {
      get
      {
        string str = this.Cache.GetNameSpace();
        if (str == null || str.Length == 0)
          return (string) null;
        return str;
      }
    }

    [SecuritySafeCritical]
    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return RuntimeTypeHandle.GetAttributes(this);
    }

    public override Guid GUID
    {
      [SecuritySafeCritical] get
      {
        Guid result = new Guid();
        this.GetGUID(ref result);
        return result;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void GetGUID(ref Guid result);

    [SecuritySafeCritical]
    protected override bool IsContextfulImpl()
    {
      return RuntimeTypeHandle.IsContextful(this);
    }

    protected override bool IsByRefImpl()
    {
      return RuntimeTypeHandle.IsByRef(this);
    }

    protected override bool IsPrimitiveImpl()
    {
      return RuntimeTypeHandle.IsPrimitive(this);
    }

    protected override bool IsPointerImpl()
    {
      return RuntimeTypeHandle.IsPointer(this);
    }

    [SecuritySafeCritical]
    protected override bool IsCOMObjectImpl()
    {
      return RuntimeTypeHandle.IsComObject(this, false);
    }

    [SecuritySafeCritical]
    internal override bool IsWindowsRuntimeObjectImpl()
    {
      return RuntimeType.IsWindowsRuntimeObjectType(this);
    }

    [SecuritySafeCritical]
    internal override bool IsExportedToWindowsRuntimeImpl()
    {
      return RuntimeType.IsTypeExportedToWindowsRuntime(this);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsWindowsRuntimeObjectType(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsTypeExportedToWindowsRuntime(RuntimeType type);

    [SecuritySafeCritical]
    internal override bool HasProxyAttributeImpl()
    {
      return RuntimeTypeHandle.HasProxyAttribute(this);
    }

    internal bool IsDelegate()
    {
      return (Type) this.GetBaseType() == typeof (MulticastDelegate);
    }

    protected override bool IsValueTypeImpl()
    {
      if ((Type) this == typeof (System.ValueType) || (Type) this == typeof (Enum))
        return false;
      return this.IsSubclassOf(typeof (System.ValueType));
    }

    public override bool IsEnum
    {
      get
      {
        return this.GetBaseType() == RuntimeType.EnumType;
      }
    }

    protected override bool HasElementTypeImpl()
    {
      return RuntimeTypeHandle.HasElementType(this);
    }

    public override GenericParameterAttributes GenericParameterAttributes
    {
      [SecuritySafeCritical] get
      {
        if (!this.IsGenericParameter)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
        GenericParameterAttributes attributes;
        RuntimeTypeHandle.GetMetadataImport(this).GetGenericParamProps(this.MetadataToken, out attributes);
        return attributes;
      }
    }

    public override bool IsSecurityCritical
    {
      get
      {
        return new RuntimeTypeHandle(this).IsSecurityCritical();
      }
    }

    public override bool IsSecuritySafeCritical
    {
      get
      {
        return new RuntimeTypeHandle(this).IsSecuritySafeCritical();
      }
    }

    public override bool IsSecurityTransparent
    {
      get
      {
        return new RuntimeTypeHandle(this).IsSecurityTransparent();
      }
    }

    internal override bool IsSzArray
    {
      get
      {
        return RuntimeTypeHandle.IsSzArray(this);
      }
    }

    protected override bool IsArrayImpl()
    {
      return RuntimeTypeHandle.IsArray(this);
    }

    [SecuritySafeCritical]
    public override int GetArrayRank()
    {
      if (!this.IsArrayImpl())
        throw new ArgumentException(Environment.GetResourceString("Argument_HasToBeArrayClass"));
      return RuntimeTypeHandle.GetArrayRank(this);
    }

    public override Type GetElementType()
    {
      return (Type) RuntimeTypeHandle.GetElementType(this);
    }

    public override string[] GetEnumNames()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      string[] names = Enum.InternalGetNames(this);
      string[] strArray = new string[names.Length];
      Array.Copy((Array) names, (Array) strArray, names.Length);
      return strArray;
    }

    [SecuritySafeCritical]
    public override Array GetEnumValues()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      ulong[] values = Enum.InternalGetValues(this);
      Array instance = Array.UnsafeCreateInstance((Type) this, values.Length);
      for (int index = 0; index < values.Length; ++index)
      {
        object obj = Enum.ToObject((Type) this, values[index]);
        instance.SetValue(obj, index);
      }
      return instance;
    }

    public override Type GetEnumUnderlyingType()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      return (Type) Enum.InternalGetUnderlyingType(this);
    }

    public override bool IsEnumDefined(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      RuntimeType runtimeType = (RuntimeType) value.GetType();
      if (runtimeType.IsEnum)
      {
        if (!runtimeType.IsEquivalentTo((Type) this))
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", (object) runtimeType.ToString(), (object) this.ToString()));
        runtimeType = (RuntimeType) runtimeType.GetEnumUnderlyingType();
      }
      if (runtimeType == RuntimeType.StringType)
        return Array.IndexOf<object>((object[]) Enum.InternalGetNames(this), value) >= 0;
      if (Type.IsIntegerType((Type) runtimeType))
      {
        RuntimeType underlyingType = Enum.InternalGetUnderlyingType(this);
        if (underlyingType != runtimeType)
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) runtimeType.ToString(), (object) underlyingType.ToString()));
        return Array.BinarySearch<ulong>(Enum.InternalGetValues(this), Enum.ToUInt64(value)) >= 0;
      }
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) runtimeType.ToString(), (object) this.GetEnumUnderlyingType()));
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
    }

    public override string GetEnumName(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      Type type = value.GetType();
      if (!type.IsEnum && !Type.IsIntegerType(type))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), nameof (value));
      int index = Array.BinarySearch<ulong>(Enum.InternalGetValues(this), Enum.ToUInt64(value));
      if (index >= 0)
        return Enum.InternalGetNames(this)[index];
      return (string) null;
    }

    internal RuntimeType[] GetGenericArgumentsInternal()
    {
      return this.GetRootElementType().GetTypeHandleInternal().GetInstantiationInternal();
    }

    public override Type[] GetGenericArguments()
    {
      return this.GetRootElementType().GetTypeHandleInternal().GetInstantiationPublic() ?? EmptyArray<Type>.Value;
    }

    [SecuritySafeCritical]
    public override Type MakeGenericType(params Type[] instantiation)
    {
      if (instantiation == null)
        throw new ArgumentNullException(nameof (instantiation));
      RuntimeType[] genericArguments = new RuntimeType[instantiation.Length];
      if (!this.IsGenericTypeDefinition)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition", (object) this));
      if (this.GetGenericArguments().Length != instantiation.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_GenericArgsCount"), nameof (instantiation));
      for (int index1 = 0; index1 < instantiation.Length; ++index1)
      {
        Type type = instantiation[index1];
        if (type == (Type) null)
          throw new ArgumentNullException();
        RuntimeType runtimeType = type as RuntimeType;
        if (runtimeType == (RuntimeType) null)
        {
          Type[] typeArray = new Type[instantiation.Length];
          for (int index2 = 0; index2 < instantiation.Length; ++index2)
            typeArray[index2] = instantiation[index2];
          instantiation = typeArray;
          return TypeBuilderInstantiation.MakeGenericType((Type) this, instantiation);
        }
        genericArguments[index1] = runtimeType;
      }
      RuntimeType[] argumentsInternal = this.GetGenericArgumentsInternal();
      RuntimeType.SanityCheckGenericArguments(genericArguments, argumentsInternal);
      try
      {
        return (Type) new RuntimeTypeHandle(this).Instantiate((Type[]) genericArguments);
      }
      catch (TypeLoadException ex)
      {
        RuntimeType.ValidateGenericArguments((MemberInfo) this, genericArguments, (Exception) ex);
        throw ex;
      }
    }

    public override bool IsGenericTypeDefinition
    {
      get
      {
        return RuntimeTypeHandle.IsGenericTypeDefinition(this);
      }
    }

    public override bool IsGenericParameter
    {
      get
      {
        return RuntimeTypeHandle.IsGenericVariable(this);
      }
    }

    public override int GenericParameterPosition
    {
      get
      {
        if (!this.IsGenericParameter)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
        return new RuntimeTypeHandle(this).GetGenericVariableIndex();
      }
    }

    public override Type GetGenericTypeDefinition()
    {
      if (!this.IsGenericType)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotGenericType"));
      return (Type) RuntimeTypeHandle.GetGenericTypeDefinition(this);
    }

    public override bool IsGenericType
    {
      get
      {
        return RuntimeTypeHandle.HasInstantiation(this);
      }
    }

    public override bool IsConstructedGenericType
    {
      get
      {
        if (this.IsGenericType)
          return !this.IsGenericTypeDefinition;
        return false;
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        return this.GetRootElementType().GetTypeHandleInternal().ContainsGenericVariables();
      }
    }

    public override Type[] GetGenericParameterConstraints()
    {
      if (!this.IsGenericParameter)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      return new RuntimeTypeHandle(this).GetConstraints() ?? EmptyArray<Type>.Value;
    }

    [SecuritySafeCritical]
    public override Type MakePointerType()
    {
      return (Type) new RuntimeTypeHandle(this).MakePointer();
    }

    public override Type MakeByRefType()
    {
      return (Type) new RuntimeTypeHandle(this).MakeByRef();
    }

    public override Type MakeArrayType()
    {
      return (Type) new RuntimeTypeHandle(this).MakeSZArray();
    }

    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      return (Type) new RuntimeTypeHandle(this).MakeArray(rank);
    }

    public override StructLayoutAttribute StructLayoutAttribute
    {
      [SecuritySafeCritical] get
      {
        return (StructLayoutAttribute) StructLayoutAttribute.GetCustomAttribute(this);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool CanValueSpecialCast(RuntimeType valueType, RuntimeType targetType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object AllocateValueType(RuntimeType type, object value, bool fForceTypeChange);

    [SecuritySafeCritical]
    internal object CheckValue(object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
    {
      if (this.IsInstanceOfType(value))
      {
        RealProxy realProxy = RemotingServices.GetRealProxy(value);
        if ((realProxy == null ? (object) value.GetType() : (object) realProxy.GetProxiedType()) != (object) this && RuntimeTypeHandle.IsValueType(this))
          return RuntimeType.AllocateValueType(this, value, true);
        return value;
      }
      if (this.IsByRef)
      {
        RuntimeType elementType = RuntimeTypeHandle.GetElementType(this);
        if (elementType.IsInstanceOfType(value) || value == null)
          return RuntimeType.AllocateValueType(elementType, value, false);
      }
      else if (value == null || this == RuntimeType.s_typedRef)
        return value;
      bool needsSpecialCast = this.IsPointer || this.IsEnum || this.IsPrimitive;
      if (needsSpecialCast)
      {
        Pointer pointer = value as Pointer;
        if (RuntimeType.CanValueSpecialCast(pointer == null ? (RuntimeType) value.GetType() : pointer.GetPointerType(), this))
        {
          if (pointer != null)
            return pointer.GetPointerValue();
          return value;
        }
      }
      if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.ExactBinding)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_ObjObjEx"), (object) value.GetType(), (object) this));
      return this.TryChangeType(value, binder, culture, needsSpecialCast);
    }

    [SecurityCritical]
    private object TryChangeType(object value, Binder binder, CultureInfo culture, bool needsSpecialCast)
    {
      if (binder != null && binder != Type.DefaultBinder)
      {
        value = binder.ChangeType(value, (Type) this, culture);
        if (this.IsInstanceOfType(value))
          return value;
        if (this.IsByRef)
        {
          RuntimeType elementType = RuntimeTypeHandle.GetElementType(this);
          if (elementType.IsInstanceOfType(value) || value == null)
            return RuntimeType.AllocateValueType(elementType, value, false);
        }
        else if (value == null)
          return value;
        if (needsSpecialCast)
        {
          Pointer pointer = value as Pointer;
          if (RuntimeType.CanValueSpecialCast(pointer == null ? (RuntimeType) value.GetType() : pointer.GetPointerType(), this))
          {
            if (pointer != null)
              return pointer.GetPointerValue();
            return value;
          }
        }
      }
      throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_ObjObjEx"), (object) value.GetType(), (object) this));
    }

    public override MemberInfo[] GetDefaultMembers()
    {
      MemberInfo[] memberInfoArray = (MemberInfo[]) null;
      string defaultMemberName = this.GetDefaultMemberName();
      if (defaultMemberName != null)
        memberInfoArray = this.GetMember(defaultMemberName);
      if (memberInfoArray == null)
        memberInfoArray = EmptyArray<MemberInfo>.Value;
      return memberInfoArray;
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    public override object InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, object[] providedArgs, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParams)
    {
      if (this.IsGenericParameter)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_GenericParameter"));
      if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
        throw new ArgumentException(Environment.GetResourceString("Arg_NoAccessSpec"), nameof (bindingFlags));
      if ((bindingFlags & (BindingFlags) 255) == BindingFlags.Default)
      {
        bindingFlags |= BindingFlags.Instance | BindingFlags.Public;
        if ((bindingFlags & BindingFlags.CreateInstance) == BindingFlags.Default)
          bindingFlags |= BindingFlags.Static;
      }
      if (namedParams != null)
      {
        if (providedArgs != null)
        {
          if (namedParams.Length > providedArgs.Length)
            throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamTooBig"), nameof (namedParams));
        }
        else if (namedParams.Length != 0)
          throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamTooBig"), nameof (namedParams));
      }
      if (target != null && target.GetType().IsCOMObject)
      {
        if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_COMAccess"), nameof (bindingFlags));
        if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_PropSetGet"), nameof (bindingFlags));
        if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_PropSetInvoke"), nameof (bindingFlags));
        if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.SetProperty) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_COMPropSetPut"), nameof (bindingFlags));
        if ((bindingFlags & BindingFlags.PutDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutDispProperty) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_COMPropSetPut"), nameof (bindingFlags));
        if ((bindingFlags & BindingFlags.PutRefDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutRefDispProperty) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_COMPropSetPut"), nameof (bindingFlags));
        if (RemotingServices.IsTransparentProxy(target))
          return ((MarshalByRefObject) target).InvokeMember(name, bindingFlags, binder, providedArgs, modifiers, culture, namedParams);
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        bool[] byrefModifiers = modifiers == null ? (bool[]) null : modifiers[0].IsByRefArray;
        int culture1 = culture == null ? 1033 : culture.LCID;
        return this.InvokeDispMethod(name, bindingFlags, target, providedArgs, byrefModifiers, culture1, namedParams);
      }
      if (namedParams != null && Array.IndexOf<string>(namedParams, (string) null) != -1)
        throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamNull"), nameof (namedParams));
      int length1 = providedArgs != null ? providedArgs.Length : 0;
      if (binder == null)
        binder = Type.DefaultBinder;
      bool flag1 = binder == Type.DefaultBinder;
      if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default)
      {
        if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_CreatInstAccess"), nameof (bindingFlags));
        return Activator.CreateInstance((Type) this, bindingFlags, binder, providedArgs, culture);
      }
      if ((bindingFlags & (BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) != BindingFlags.Default)
        bindingFlags |= BindingFlags.SetProperty;
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0 || name.Equals("[DISPID=0]"))
      {
        name = this.GetDefaultMemberName();
        if (name == null)
          name = "ToString";
      }
      bool flag2 = (uint) (bindingFlags & BindingFlags.GetField) > 0U;
      bool flag3 = (uint) (bindingFlags & BindingFlags.SetField) > 0U;
      if (flag2 | flag3)
      {
        if (flag2)
        {
          if (flag3)
            throw new ArgumentException(Environment.GetResourceString("Arg_FldSetGet"), nameof (bindingFlags));
          if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default)
            throw new ArgumentException(Environment.GetResourceString("Arg_FldGetPropSet"), nameof (bindingFlags));
        }
        else
        {
          if (providedArgs == null)
            throw new ArgumentNullException(nameof (providedArgs));
          if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default)
            throw new ArgumentException(Environment.GetResourceString("Arg_FldSetPropGet"), nameof (bindingFlags));
          if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
            throw new ArgumentException(Environment.GetResourceString("Arg_FldSetInvoke"), nameof (bindingFlags));
        }
        FieldInfo fieldInfo = (FieldInfo) null;
        FieldInfo[] member = this.GetMember(name, MemberTypes.Field, bindingFlags) as FieldInfo[];
        if (member.Length == 1)
          fieldInfo = member[0];
        else if (member.Length != 0)
          fieldInfo = binder.BindToField(bindingFlags, member, flag2 ? (object) Empty.Value : providedArgs[0], culture);
        if (fieldInfo != (FieldInfo) null)
        {
          if (fieldInfo.FieldType.IsArray || (object) fieldInfo.FieldType == (object) typeof (Array))
          {
            int length2 = (bindingFlags & BindingFlags.GetField) == BindingFlags.Default ? length1 - 1 : length1;
            if (length2 > 0)
            {
              int[] numArray = new int[length2];
              for (int index = 0; index < length2; ++index)
              {
                try
                {
                  numArray[index] = ((IConvertible) providedArgs[index]).ToInt32((IFormatProvider) null);
                }
                catch (InvalidCastException ex)
                {
                  throw new ArgumentException(Environment.GetResourceString("Arg_IndexMustBeInt"));
                }
              }
              Array array = (Array) fieldInfo.GetValue(target);
              if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
                return array.GetValue(numArray);
              array.SetValue(providedArgs[length2], numArray);
              return (object) null;
            }
          }
          if (flag2)
          {
            if (length1 != 0)
              throw new ArgumentException(Environment.GetResourceString("Arg_FldGetArgErr"), nameof (bindingFlags));
            return fieldInfo.GetValue(target);
          }
          if (length1 != 1)
            throw new ArgumentException(Environment.GetResourceString("Arg_FldSetArgErr"), nameof (bindingFlags));
          fieldInfo.SetValue(target, providedArgs[0], bindingFlags, binder, culture);
          return (object) null;
        }
        if ((bindingFlags & (BindingFlags) 16773888) == BindingFlags.Default)
          throw new MissingFieldException(this.FullName, name);
      }
      bool flag4 = (uint) (bindingFlags & BindingFlags.GetProperty) > 0U;
      bool flag5 = (uint) (bindingFlags & BindingFlags.SetProperty) > 0U;
      if (flag4 | flag5)
      {
        if (flag4)
        {
          if (flag5)
            throw new ArgumentException(Environment.GetResourceString("Arg_PropSetGet"), nameof (bindingFlags));
        }
        else if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
          throw new ArgumentException(Environment.GetResourceString("Arg_PropSetInvoke"), nameof (bindingFlags));
      }
      MethodInfo[] array1 = (MethodInfo[]) null;
      MethodInfo methodInfo1 = (MethodInfo) null;
      if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
      {
        MethodInfo[] member = this.GetMember(name, MemberTypes.Method, bindingFlags) as MethodInfo[];
        List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
        for (int index = 0; index < member.Length; ++index)
        {
          MethodInfo methodInfo2 = member[index];
          if (RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo) methodInfo2, bindingFlags, CallingConventions.Any, new Type[length1]))
          {
            if (methodInfo1 == (MethodInfo) null)
            {
              methodInfo1 = methodInfo2;
            }
            else
            {
              if (methodInfoList == null)
              {
                methodInfoList = new List<MethodInfo>(member.Length);
                methodInfoList.Add(methodInfo1);
              }
              methodInfoList.Add(methodInfo2);
            }
          }
        }
        if (methodInfoList != null)
        {
          array1 = new MethodInfo[methodInfoList.Count];
          methodInfoList.CopyTo(array1);
        }
      }
      if (methodInfo1 == (MethodInfo) null & flag4 | flag5)
      {
        PropertyInfo[] member = this.GetMember(name, MemberTypes.Property, bindingFlags) as PropertyInfo[];
        List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
        for (int index = 0; index < member.Length; ++index)
        {
          MethodInfo methodInfo2 = !flag5 ? member[index].GetGetMethod(true) : member[index].GetSetMethod(true);
          if (!(methodInfo2 == (MethodInfo) null) && RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo) methodInfo2, bindingFlags, CallingConventions.Any, new Type[length1]))
          {
            if (methodInfo1 == (MethodInfo) null)
            {
              methodInfo1 = methodInfo2;
            }
            else
            {
              if (methodInfoList == null)
              {
                methodInfoList = new List<MethodInfo>(member.Length);
                methodInfoList.Add(methodInfo1);
              }
              methodInfoList.Add(methodInfo2);
            }
          }
        }
        if (methodInfoList != null)
        {
          array1 = new MethodInfo[methodInfoList.Count];
          methodInfoList.CopyTo(array1);
        }
      }
      if (!(methodInfo1 != (MethodInfo) null))
        throw new MissingMethodException(this.FullName, name);
      if (array1 == null && length1 == 0 && (methodInfo1.GetParametersNoCopy().Length == 0 && (bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default))
        return methodInfo1.Invoke(target, bindingFlags, binder, providedArgs, culture);
      if (array1 == null)
        array1 = new MethodInfo[1]{ methodInfo1 };
      if (providedArgs == null)
        providedArgs = EmptyArray<object>.Value;
      object state = (object) null;
      MethodBase methodBase = (MethodBase) null;
      try
      {
        methodBase = binder.BindToMethod(bindingFlags, (MethodBase[]) array1, ref providedArgs, modifiers, culture, namedParams, out state);
      }
      catch (MissingMethodException ex)
      {
      }
      if (methodBase == (MethodBase) null)
        throw new MissingMethodException(this.FullName, name);
      object obj = methodBase.Invoke(target, bindingFlags, binder, providedArgs, culture);
      if (state != null)
        binder.ReorderArgumentArray(ref providedArgs, state);
      return obj;
    }

    public override bool Equals(object obj)
    {
      return obj == (object) this;
    }

    public override int GetHashCode()
    {
      return RuntimeHelpers.GetHashCode((object) this);
    }

    public static bool operator ==(RuntimeType left, RuntimeType right)
    {
      return (object) left == (object) right;
    }

    public static bool operator !=(RuntimeType left, RuntimeType right)
    {
      return (object) left != (object) right;
    }

    public override string ToString()
    {
      return this.GetCachedName(TypeNameKind.ToString);
    }

    public object Clone()
    {
      return (object) this;
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      UnitySerializationHolder.GetUnitySerializationInfo(info, this);
    }

    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, RuntimeType.ObjectType, inherit);
    }

    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if ((object) attributeType == null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this, underlyingSystemType, inherit);
    }

    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if ((object) attributeType == null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.IsDefined(this, underlyingSystemType, inherit);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    public override string Name
    {
      get
      {
        return this.GetCachedName(TypeNameKind.Name);
      }
    }

    internal override string FormatTypeName(bool serialization)
    {
      if (serialization)
        return this.GetCachedName(TypeNameKind.SerializationName);
      Type rootElementType = this.GetRootElementType();
      if (rootElementType.IsNested)
        return this.Name;
      string str = this.ToString();
      if (rootElementType.IsPrimitive || rootElementType == typeof (void) || rootElementType == typeof (TypedReference))
        str = str.Substring("System.".Length);
      return str;
    }

    private string GetCachedName(TypeNameKind kind)
    {
      return this.Cache.GetName(kind);
    }

    public override MemberTypes MemberType
    {
      get
      {
        return this.IsPublic || this.IsNotPublic ? MemberTypes.TypeInfo : MemberTypes.NestedType;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return (Type) this.Cache.GetEnclosingType();
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return this.DeclaringType;
      }
    }

    public override int MetadataToken
    {
      [SecuritySafeCritical] get
      {
        return RuntimeTypeHandle.GetToken(this);
      }
    }

    private void CreateInstanceCheckThis()
    {
      if (this is ReflectionOnlyType)
        throw new ArgumentException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
      if (this.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Acc_CreateGenericEx", (object) this));
      Type rootElementType = this.GetRootElementType();
      if ((object) rootElementType == (object) typeof (ArgIterator))
        throw new NotSupportedException(Environment.GetResourceString("Acc_CreateArgIterator"));
      if ((object) rootElementType == (object) typeof (void))
        throw new NotSupportedException(Environment.GetResourceString("Acc_CreateVoid"));
    }

    [SecurityCritical]
    internal object CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, ref StackCrawlMark stackMark)
    {
      this.CreateInstanceCheckThis();
      object obj = (object) null;
      try
      {
        try
        {
          if (activationAttributes != null)
            ActivationServices.PushActivationAttributes((Type) this, activationAttributes);
          if (args == null)
            args = EmptyArray<object>.Value;
          int length = args.Length;
          if (binder == null)
            binder = Type.DefaultBinder;
          if (length == 0 && (bindingAttr & BindingFlags.Public) != BindingFlags.Default && (bindingAttr & BindingFlags.Instance) != BindingFlags.Default && (this.IsGenericCOMObjectImpl() || this.IsValueType))
          {
            obj = this.CreateInstanceDefaultCtor((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default, false, true, ref stackMark);
          }
          else
          {
            ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
            List<MethodBase> methodBaseList = new List<MethodBase>(constructors.Length);
            Type[] argumentTypes = new Type[length];
            for (int index = 0; index < length; ++index)
            {
              if (args[index] != null)
                argumentTypes[index] = args[index].GetType();
            }
            for (int index = 0; index < constructors.Length; ++index)
            {
              if (RuntimeType.FilterApplyConstructorInfo((RuntimeConstructorInfo) constructors[index], bindingAttr, CallingConventions.Any, argumentTypes))
                methodBaseList.Add((MethodBase) constructors[index]);
            }
            MethodBase[] methodBaseArray = new MethodBase[methodBaseList.Count];
            methodBaseList.CopyTo(methodBaseArray);
            if (methodBaseArray != null && methodBaseArray.Length == 0)
              methodBaseArray = (MethodBase[]) null;
            if (methodBaseArray == null)
            {
              if (activationAttributes != null)
              {
                ActivationServices.PopActivationAttributes((Type) this);
                activationAttributes = (object[]) null;
              }
              throw new MissingMethodException(Environment.GetResourceString("MissingConstructor_Name", (object) this.FullName));
            }
            object state = (object) null;
            MethodBase methodBase;
            try
            {
              methodBase = binder.BindToMethod(bindingAttr, methodBaseArray, ref args, (ParameterModifier[]) null, culture, (string[]) null, out state);
            }
            catch (MissingMethodException ex)
            {
              methodBase = (MethodBase) null;
            }
            if (methodBase == (MethodBase) null)
            {
              if (activationAttributes != null)
              {
                ActivationServices.PopActivationAttributes((Type) this);
                activationAttributes = (object[]) null;
              }
              throw new MissingMethodException(Environment.GetResourceString("MissingConstructor_Name", (object) this.FullName));
            }
            if (RuntimeType.DelegateType.IsAssignableFrom(methodBase.DeclaringType))
              new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            if (methodBase.GetParametersNoCopy().Length == 0)
            {
              if (args.Length != 0)
                throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_CallToVarArg"), Array.Empty<object>()));
              obj = Activator.CreateInstance((Type) this, true);
            }
            else
            {
              obj = ((ConstructorInfo) methodBase).Invoke(bindingAttr, binder, args, culture);
              if (state != null)
                binder.ReorderArgumentArray(ref args, state);
            }
          }
        }
        finally
        {
          if (activationAttributes != null)
          {
            ActivationServices.PopActivationAttributes((Type) this);
            activationAttributes = (object[]) null;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      return obj;
    }

    [SecuritySafeCritical]
    internal object CreateInstanceSlow(bool publicOnly, bool skipCheckThis, bool fillCache, ref StackCrawlMark stackMark)
    {
      RuntimeMethodHandleInternal ctor = new RuntimeMethodHandleInternal();
      bool bNeedSecurityCheck = true;
      bool canBeCached = false;
      bool noCheck = false;
      if (!skipCheckThis)
        this.CreateInstanceCheckThis();
      if (!fillCache)
        noCheck = true;
      if ((this.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
        noCheck = false;
        canBeCached = false;
      }
      object instance = RuntimeTypeHandle.CreateInstance(this, publicOnly, noCheck, ref canBeCached, ref ctor, ref bNeedSecurityCheck);
      if (canBeCached & fillCache)
      {
        RuntimeType.ActivatorCache activatorCache = RuntimeType.s_ActivatorCache;
        if (activatorCache == null)
        {
          activatorCache = new RuntimeType.ActivatorCache();
          RuntimeType.s_ActivatorCache = activatorCache;
        }
        RuntimeType.ActivatorCacheEntry ace = new RuntimeType.ActivatorCacheEntry(this, ctor, bNeedSecurityCheck);
        activatorCache.SetEntry(ace);
      }
      return instance;
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal object CreateInstanceDefaultCtor(bool publicOnly, bool skipCheckThis, bool fillCache, ref StackCrawlMark stackMark)
    {
      if (this.GetType() == typeof (ReflectionOnlyType))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
      RuntimeType.ActivatorCache activatorCache = RuntimeType.s_ActivatorCache;
      if (activatorCache != null)
      {
        RuntimeType.ActivatorCacheEntry entry = activatorCache.GetEntry(this);
        if (entry != null)
        {
          if (publicOnly && entry.m_ctor != null && (entry.m_ctorAttributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
            throw new MissingMethodException(Environment.GetResourceString("Arg_NoDefCTor"));
          object instance = RuntimeTypeHandle.Allocate(this);
          if (entry.m_ctor != null)
          {
            if (entry.m_bNeedSecurityCheck)
              RuntimeMethodHandle.PerformSecurityCheck(instance, entry.m_hCtorMethodHandle, this, 268435456U);
            try
            {
              entry.m_ctor(instance);
            }
            catch (Exception ex)
            {
              throw new TargetInvocationException(ex);
            }
          }
          return instance;
        }
      }
      return this.CreateInstanceSlow(publicOnly, skipCheckThis, fillCache, ref stackMark);
    }

    internal void InvalidateCachedNestedType()
    {
      this.Cache.InvalidateCachedNestedType();
    }

    [SecuritySafeCritical]
    internal bool IsGenericCOMObjectImpl()
    {
      return RuntimeTypeHandle.IsComObject(this, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object _CreateEnum(RuntimeType enumType, long value);

    [SecuritySafeCritical]
    internal static object CreateEnum(RuntimeType enumType, long value)
    {
      return RuntimeType._CreateEnum(enumType, value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern object InvokeDispMethod(string name, BindingFlags invokeAttr, object target, object[] args, bool[] byrefModifiers, int culture, string[] namedParameters);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Type GetTypeFromProgIDImpl(string progID, string server, bool throwOnError);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Type GetTypeFromCLSIDImpl(Guid clsid, string server, bool throwOnError);

    [SecuritySafeCritical]
    private object ForwardCallToInvokeMember(string memberName, BindingFlags flags, object target, int[] aWrapperTypes, ref MessageData msgData)
    {
      ParameterModifier[] modifiers = (ParameterModifier[]) null;
      Message message = new Message();
      message.InitFields(msgData);
      MethodInfo methodBase = (MethodInfo) message.GetMethodBase();
      object[] args = message.Args;
      int length = args.Length;
      ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
      if (length > 0)
      {
        ParameterModifier parameterModifier = new ParameterModifier(length);
        for (int index = 0; index < length; ++index)
        {
          if (parametersNoCopy[index].ParameterType.IsByRef)
            parameterModifier[index] = true;
        }
        modifiers = new ParameterModifier[1]
        {
          parameterModifier
        };
        if (aWrapperTypes != null)
          this.WrapArgsForInvokeCall(args, aWrapperTypes);
      }
      if ((object) methodBase.ReturnType == (object) typeof (void))
        flags |= BindingFlags.IgnoreReturn;
      object returnValue;
      try
      {
        returnValue = this.InvokeMember(memberName, flags, (Binder) null, target, args, modifiers, (CultureInfo) null, (string[]) null);
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
      for (int index = 0; index < length; ++index)
      {
        if (modifiers[0][index] && args[index] != null)
        {
          Type elementType = parametersNoCopy[index].ParameterType.GetElementType();
          if ((object) elementType != (object) args[index].GetType())
            args[index] = this.ForwardCallBinder.ChangeType(args[index], elementType, (CultureInfo) null);
        }
      }
      if (returnValue != null)
      {
        Type returnType = methodBase.ReturnType;
        if ((object) returnType != (object) returnValue.GetType())
          returnValue = this.ForwardCallBinder.ChangeType(returnValue, returnType, (CultureInfo) null);
      }
      RealProxy.PropagateOutParameters((IMessage) message, args, returnValue);
      return returnValue;
    }

    [SecuritySafeCritical]
    private void WrapArgsForInvokeCall(object[] aArgs, int[] aWrapperTypes)
    {
      int length1 = aArgs.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        if (aWrapperTypes[index1] != 0)
        {
          if ((aWrapperTypes[index1] & 65536) != 0)
          {
            Type elementType = (Type) null;
            bool flag = false;
            switch ((RuntimeType.DispatchWrapperType) (aWrapperTypes[index1] & -65537))
            {
              case RuntimeType.DispatchWrapperType.Unknown:
                elementType = typeof (UnknownWrapper);
                break;
              case RuntimeType.DispatchWrapperType.Dispatch:
                elementType = typeof (DispatchWrapper);
                break;
              case RuntimeType.DispatchWrapperType.Error:
                elementType = typeof (ErrorWrapper);
                break;
              case RuntimeType.DispatchWrapperType.Currency:
                elementType = typeof (CurrencyWrapper);
                break;
              case RuntimeType.DispatchWrapperType.BStr:
                elementType = typeof (BStrWrapper);
                flag = true;
                break;
            }
            Array aArg = (Array) aArgs[index1];
            int length2 = aArg.Length;
            object[] instance = (object[]) Array.UnsafeCreateInstance(elementType, length2);
            ConstructorInfo constructor;
            if (flag)
              constructor = elementType.GetConstructor(new Type[1]
              {
                typeof (string)
              });
            else
              constructor = elementType.GetConstructor(new Type[1]
              {
                typeof (object)
              });
            for (int index2 = 0; index2 < length2; ++index2)
            {
              if (flag)
                instance[index2] = constructor.Invoke(new object[1]
                {
                  (object) (string) aArg.GetValue(index2)
                });
              else
                instance[index2] = constructor.Invoke(new object[1]
                {
                  aArg.GetValue(index2)
                });
            }
            aArgs[index1] = (object) instance;
          }
          else
          {
            switch ((RuntimeType.DispatchWrapperType) aWrapperTypes[index1])
            {
              case RuntimeType.DispatchWrapperType.Unknown:
                aArgs[index1] = (object) new UnknownWrapper(aArgs[index1]);
                continue;
              case RuntimeType.DispatchWrapperType.Dispatch:
                aArgs[index1] = (object) new DispatchWrapper(aArgs[index1]);
                continue;
              case RuntimeType.DispatchWrapperType.Error:
                aArgs[index1] = (object) new ErrorWrapper(aArgs[index1]);
                continue;
              case RuntimeType.DispatchWrapperType.Currency:
                aArgs[index1] = (object) new CurrencyWrapper(aArgs[index1]);
                continue;
              case RuntimeType.DispatchWrapperType.BStr:
                aArgs[index1] = (object) new BStrWrapper((string) aArgs[index1]);
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    private OleAutBinder ForwardCallBinder
    {
      get
      {
        if (RuntimeType.s_ForwardCallBinder == null)
          RuntimeType.s_ForwardCallBinder = new OleAutBinder();
        return RuntimeType.s_ForwardCallBinder;
      }
    }

    internal enum MemberListType
    {
      All,
      CaseSensitive,
      CaseInsensitive,
      HandleToInfo,
    }

    private struct ListBuilder<T> where T : class
    {
      private T[] _items;
      private T _item;
      private int _count;
      private int _capacity;

      public ListBuilder(int capacity)
      {
        this._items = (T[]) null;
        this._item = default (T);
        this._count = 0;
        this._capacity = capacity;
      }

      public T this[int index]
      {
        get
        {
          if (this._items == null)
            return this._item;
          return this._items[index];
        }
      }

      public T[] ToArray()
      {
        if (this._count == 0)
          return EmptyArray<T>.Value;
        if (this._count == 1)
          return new T[1]{ this._item };
        Array.Resize<T>(ref this._items, this._count);
        this._capacity = this._count;
        return this._items;
      }

      public void CopyTo(object[] array, int index)
      {
        if (this._count == 0)
          return;
        if (this._count == 1)
          array[index] = (object) this._item;
        else
          Array.Copy((Array) this._items, 0, (Array) array, index, this._count);
      }

      public int Count
      {
        get
        {
          return this._count;
        }
      }

      public void Add(T item)
      {
        if (this._count == 0)
        {
          this._item = item;
        }
        else
        {
          if (this._count == 1)
          {
            if (this._capacity < 2)
              this._capacity = 4;
            this._items = new T[this._capacity];
            this._items[0] = this._item;
          }
          else if (this._capacity == this._count)
          {
            int newSize = 2 * this._capacity;
            Array.Resize<T>(ref this._items, newSize);
            this._capacity = newSize;
          }
          this._items[this._count] = item;
        }
        ++this._count;
      }
    }

    internal class RuntimeTypeCache
    {
      private const int MAXNAMELEN = 1024;
      private RuntimeType m_runtimeType;
      private RuntimeType m_enclosingType;
      private TypeCode m_typeCode;
      private string m_name;
      private string m_fullname;
      private string m_toString;
      private string m_namespace;
      private string m_serializationname;
      private bool m_isGlobal;
      private bool m_bIsDomainInitialized;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeMethodInfo> m_methodInfoCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeConstructorInfo> m_constructorInfoCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeFieldInfo> m_fieldInfoCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeType> m_interfaceCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeType> m_nestedClassesCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimePropertyInfo> m_propertyInfoCache;
      private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeEventInfo> m_eventInfoCache;
      private static CerHashtable<RuntimeMethodInfo, RuntimeMethodInfo> s_methodInstantiations;
      private static object s_methodInstantiationsLock;
      private RuntimeConstructorInfo m_serializationCtor;
      private string m_defaultMemberName;
      private object m_genericCache;

      internal RuntimeTypeCache(RuntimeType runtimeType)
      {
        this.m_typeCode = TypeCode.Empty;
        this.m_runtimeType = runtimeType;
        this.m_isGlobal = RuntimeTypeHandle.GetModule(runtimeType).RuntimeType == runtimeType;
      }

      private string ConstructName(ref string name, TypeNameFormatFlags formatFlags)
      {
        if (name == null)
          name = new RuntimeTypeHandle(this.m_runtimeType).ConstructName(formatFlags);
        return name;
      }

      private T[] GetMemberList<T>(ref RuntimeType.RuntimeTypeCache.MemberInfoCache<T> m_cache, RuntimeType.MemberListType listType, string name, RuntimeType.RuntimeTypeCache.CacheType cacheType) where T : MemberInfo
      {
        return this.GetMemberCache<T>(ref m_cache).GetMemberList(listType, name, cacheType);
      }

      private RuntimeType.RuntimeTypeCache.MemberInfoCache<T> GetMemberCache<T>(ref RuntimeType.RuntimeTypeCache.MemberInfoCache<T> m_cache) where T : MemberInfo
      {
        RuntimeType.RuntimeTypeCache.MemberInfoCache<T> memberInfoCache1 = m_cache;
        if (memberInfoCache1 == null)
        {
          RuntimeType.RuntimeTypeCache.MemberInfoCache<T> memberInfoCache2 = new RuntimeType.RuntimeTypeCache.MemberInfoCache<T>(this);
          memberInfoCache1 = Interlocked.CompareExchange<RuntimeType.RuntimeTypeCache.MemberInfoCache<T>>(ref m_cache, memberInfoCache2, (RuntimeType.RuntimeTypeCache.MemberInfoCache<T>) null) ?? memberInfoCache2;
        }
        return memberInfoCache1;
      }

      internal object GenericCache
      {
        get
        {
          return this.m_genericCache;
        }
        set
        {
          this.m_genericCache = value;
        }
      }

      internal bool DomainInitialized
      {
        get
        {
          return this.m_bIsDomainInitialized;
        }
        set
        {
          this.m_bIsDomainInitialized = value;
        }
      }

      internal string GetName(TypeNameKind kind)
      {
        switch (kind)
        {
          case TypeNameKind.Name:
            return this.ConstructName(ref this.m_name, TypeNameFormatFlags.FormatBasic);
          case TypeNameKind.ToString:
            return this.ConstructName(ref this.m_toString, TypeNameFormatFlags.FormatNamespace);
          case TypeNameKind.SerializationName:
            return this.ConstructName(ref this.m_serializationname, TypeNameFormatFlags.FormatSerialization);
          case TypeNameKind.FullName:
            if (!this.m_runtimeType.GetRootElementType().IsGenericTypeDefinition && this.m_runtimeType.ContainsGenericParameters)
              return (string) null;
            return this.ConstructName(ref this.m_fullname, TypeNameFormatFlags.FormatNamespace | TypeNameFormatFlags.FormatFullInst);
          default:
            throw new InvalidOperationException();
        }
      }

      [SecuritySafeCritical]
      internal string GetNameSpace()
      {
        if (this.m_namespace == null)
        {
          Type type = this.m_runtimeType.GetRootElementType();
          while (type.IsNested)
            type = type.DeclaringType;
          this.m_namespace = RuntimeTypeHandle.GetMetadataImport((RuntimeType) type).GetNamespace(type.MetadataToken).ToString();
        }
        return this.m_namespace;
      }

      internal TypeCode TypeCode
      {
        get
        {
          return this.m_typeCode;
        }
        set
        {
          this.m_typeCode = value;
        }
      }

      [SecuritySafeCritical]
      internal RuntimeType GetEnclosingType()
      {
        if (this.m_enclosingType == (RuntimeType) null)
        {
          RuntimeType runtimeType = RuntimeTypeHandle.GetDeclaringType(this.GetRuntimeType());
          if ((object) runtimeType == null)
            runtimeType = (RuntimeType) typeof (void);
          this.m_enclosingType = runtimeType;
        }
        if (!((Type) this.m_enclosingType == typeof (void)))
          return this.m_enclosingType;
        return (RuntimeType) null;
      }

      internal RuntimeType GetRuntimeType()
      {
        return this.m_runtimeType;
      }

      internal bool IsGlobal
      {
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
        {
          return this.m_isGlobal;
        }
      }

      internal void InvalidateCachedNestedType()
      {
        this.m_nestedClassesCache = (RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeType>) null;
      }

      internal RuntimeConstructorInfo GetSerializationCtor()
      {
        if ((ConstructorInfo) this.m_serializationCtor == (ConstructorInfo) null)
        {
          if (RuntimeType.s_SICtorParamTypes == null)
            RuntimeType.s_SICtorParamTypes = new Type[2]
            {
              typeof (SerializationInfo),
              typeof (StreamingContext)
            };
          this.m_serializationCtor = this.m_runtimeType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, RuntimeType.s_SICtorParamTypes, (ParameterModifier[]) null) as RuntimeConstructorInfo;
        }
        return this.m_serializationCtor;
      }

      internal string GetDefaultMemberName()
      {
        if (this.m_defaultMemberName == null)
        {
          CustomAttributeData customAttributeData = (CustomAttributeData) null;
          Type type = typeof (DefaultMemberAttribute);
          for (RuntimeType runtimeType = this.m_runtimeType; runtimeType != (RuntimeType) null; runtimeType = runtimeType.GetBaseType())
          {
            IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((MemberInfo) runtimeType);
            for (int index = 0; index < customAttributes.Count; ++index)
            {
              if ((object) customAttributes[index].Constructor.DeclaringType == (object) type)
              {
                customAttributeData = customAttributes[index];
                break;
              }
            }
            if (customAttributeData != null)
            {
              this.m_defaultMemberName = customAttributeData.ConstructorArguments[0].Value as string;
              break;
            }
          }
        }
        return this.m_defaultMemberName;
      }

      [SecurityCritical]
      internal MethodInfo GetGenericMethodInfo(RuntimeMethodHandleInternal genericMethod)
      {
        LoaderAllocator loaderAllocator = RuntimeMethodHandle.GetLoaderAllocator(genericMethod);
        RuntimeMethodInfo index = new RuntimeMethodInfo(genericMethod, RuntimeMethodHandle.GetDeclaringType(genericMethod), this, RuntimeMethodHandle.GetAttributes(genericMethod), ~BindingFlags.Default, (object) loaderAllocator);
        RuntimeMethodInfo runtimeMethodInfo = loaderAllocator == null ? RuntimeType.RuntimeTypeCache.s_methodInstantiations[index] : loaderAllocator.m_methodInstantiations[index];
        if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
          return (MethodInfo) runtimeMethodInfo;
        if (RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock == null)
          Interlocked.CompareExchange(ref RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock, new object(), (object) null);
        bool lockTaken = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          Monitor.Enter(RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock, ref lockTaken);
          if (loaderAllocator != null)
          {
            RuntimeMethodInfo methodInstantiation = loaderAllocator.m_methodInstantiations[index];
            if ((MethodInfo) methodInstantiation != (MethodInfo) null)
              return (MethodInfo) methodInstantiation;
            loaderAllocator.m_methodInstantiations[index] = index;
          }
          else
          {
            RuntimeMethodInfo methodInstantiation = RuntimeType.RuntimeTypeCache.s_methodInstantiations[index];
            if ((MethodInfo) methodInstantiation != (MethodInfo) null)
              return (MethodInfo) methodInstantiation;
            RuntimeType.RuntimeTypeCache.s_methodInstantiations[index] = index;
          }
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock);
        }
        return (MethodInfo) index;
      }

      internal RuntimeMethodInfo[] GetMethodList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeMethodInfo>(ref this.m_methodInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Method);
      }

      internal RuntimeConstructorInfo[] GetConstructorList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeConstructorInfo>(ref this.m_constructorInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Constructor);
      }

      internal RuntimePropertyInfo[] GetPropertyList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimePropertyInfo>(ref this.m_propertyInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Property);
      }

      internal RuntimeEventInfo[] GetEventList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeEventInfo>(ref this.m_eventInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Event);
      }

      internal RuntimeFieldInfo[] GetFieldList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeFieldInfo>(ref this.m_fieldInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Field);
      }

      internal RuntimeType[] GetInterfaceList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeType>(ref this.m_interfaceCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Interface);
      }

      internal RuntimeType[] GetNestedTypeList(RuntimeType.MemberListType listType, string name)
      {
        return this.GetMemberList<RuntimeType>(ref this.m_nestedClassesCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.NestedType);
      }

      internal MethodBase GetMethod(RuntimeType declaringType, RuntimeMethodHandleInternal method)
      {
        this.GetMemberCache<RuntimeMethodInfo>(ref this.m_methodInfoCache);
        return this.m_methodInfoCache.AddMethod(declaringType, method, RuntimeType.RuntimeTypeCache.CacheType.Method);
      }

      internal MethodBase GetConstructor(RuntimeType declaringType, RuntimeMethodHandleInternal constructor)
      {
        this.GetMemberCache<RuntimeConstructorInfo>(ref this.m_constructorInfoCache);
        return this.m_constructorInfoCache.AddMethod(declaringType, constructor, RuntimeType.RuntimeTypeCache.CacheType.Constructor);
      }

      internal FieldInfo GetField(RuntimeFieldHandleInternal field)
      {
        this.GetMemberCache<RuntimeFieldInfo>(ref this.m_fieldInfoCache);
        return this.m_fieldInfoCache.AddField(field);
      }

      internal enum CacheType
      {
        Method,
        Constructor,
        Field,
        Property,
        Event,
        Interface,
        NestedType,
      }

      private struct Filter
      {
        private Utf8String m_name;
        private RuntimeType.MemberListType m_listType;
        private uint m_nameHash;

        [SecurityCritical]
        public unsafe Filter(byte* pUtf8Name, int cUtf8Name, RuntimeType.MemberListType listType)
        {
          this.m_name = new Utf8String((void*) pUtf8Name, cUtf8Name);
          this.m_listType = listType;
          this.m_nameHash = 0U;
          if (!this.RequiresStringComparison())
            return;
          this.m_nameHash = this.m_name.HashCaseInsensitive();
        }

        public bool Match(Utf8String name)
        {
          bool flag = true;
          if (this.m_listType == RuntimeType.MemberListType.CaseSensitive)
            flag = this.m_name.Equals(name);
          else if (this.m_listType == RuntimeType.MemberListType.CaseInsensitive)
            flag = this.m_name.EqualsCaseInsensitive(name);
          return flag;
        }

        public bool RequiresStringComparison()
        {
          if (this.m_listType != RuntimeType.MemberListType.CaseSensitive)
            return this.m_listType == RuntimeType.MemberListType.CaseInsensitive;
          return true;
        }

        public bool CaseSensitive()
        {
          return this.m_listType == RuntimeType.MemberListType.CaseSensitive;
        }

        public uint GetHashToMatch()
        {
          return this.m_nameHash;
        }
      }

      private class MemberInfoCache<T> where T : MemberInfo
      {
        private CerHashtable<string, T[]> m_csMemberInfos;
        private CerHashtable<string, T[]> m_cisMemberInfos;
        private T[] m_allMembers;
        private bool m_cacheComplete;
        private RuntimeType.RuntimeTypeCache m_runtimeTypeCache;

        [SecuritySafeCritical]
        internal MemberInfoCache(RuntimeType.RuntimeTypeCache runtimeTypeCache)
        {
          Mda.MemberInfoCacheCreation();
          this.m_runtimeTypeCache = runtimeTypeCache;
        }

        [SecuritySafeCritical]
        internal MethodBase AddMethod(RuntimeType declaringType, RuntimeMethodHandleInternal method, RuntimeType.RuntimeTypeCache.CacheType cacheType)
        {
          T[] list = (T[]) null;
          MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method);
          bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
          bool isStatic = (uint) (attributes & MethodAttributes.Static) > 0U;
          bool isInherited = declaringType != this.ReflectedType;
          BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
          switch (cacheType)
          {
            case RuntimeType.RuntimeTypeCache.CacheType.Method:
              list = (T[]) new RuntimeMethodInfo[1]
              {
                new RuntimeMethodInfo(method, declaringType, this.m_runtimeTypeCache, attributes, bindingFlags, (object) null)
              };
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Constructor:
              list = (T[]) new RuntimeConstructorInfo[1]
              {
                new RuntimeConstructorInfo(method, declaringType, this.m_runtimeTypeCache, attributes, bindingFlags)
              };
              break;
          }
          this.Insert(ref list, (string) null, RuntimeType.MemberListType.HandleToInfo);
          return (MethodBase) (object) list[0];
        }

        [SecuritySafeCritical]
        internal FieldInfo AddField(RuntimeFieldHandleInternal field)
        {
          FieldAttributes attributes = RuntimeFieldHandle.GetAttributes(field);
          bool isPublic = (attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
          bool isStatic = (uint) (attributes & FieldAttributes.Static) > 0U;
          RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field);
          bool isInherited = RuntimeFieldHandle.AcquiresContextFromThis(field) ? !RuntimeTypeHandle.CompareCanonicalHandles(approxDeclaringType, this.ReflectedType) : approxDeclaringType != this.ReflectedType;
          BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
          T[] list = (T[]) new RuntimeFieldInfo[1]
          {
            (RuntimeFieldInfo) new RtFieldInfo(field, this.ReflectedType, this.m_runtimeTypeCache, bindingFlags)
          };
          this.Insert(ref list, (string) null, RuntimeType.MemberListType.HandleToInfo);
          return (FieldInfo) (object) list[0];
        }

        [SecuritySafeCritical]
        private unsafe T[] Populate(string name, RuntimeType.MemberListType listType, RuntimeType.RuntimeTypeCache.CacheType cacheType)
        {
          T[] listByName;
          if (name == null || name.Length == 0 || cacheType == RuntimeType.RuntimeTypeCache.CacheType.Constructor && name.FirstChar != '.' && name.FirstChar != '*')
          {
            listByName = this.GetListByName((char*) null, 0, (byte*) null, 0, listType, cacheType);
          }
          else
          {
            int length = name.Length;
            string str = name;
            char* chPtr = (char*) str;
            if ((IntPtr) chPtr != IntPtr.Zero)
              chPtr += RuntimeHelpers.OffsetToStringData;
            int byteCount = Encoding.UTF8.GetByteCount(chPtr, length);
            if (byteCount > 1024)
            {
              fixed (byte* pUtf8Name = new byte[byteCount])
                listByName = this.GetListByName(chPtr, length, pUtf8Name, byteCount, listType, cacheType);
            }
            else
            {
              byte* pUtf8Name = stackalloc byte[byteCount];
              listByName = this.GetListByName(chPtr, length, pUtf8Name, byteCount, listType, cacheType);
            }
            str = (string) null;
          }
          this.Insert(ref listByName, name, listType);
          return listByName;
        }

        [SecurityCritical]
        private unsafe T[] GetListByName(char* pName, int cNameLen, byte* pUtf8Name, int cUtf8Name, RuntimeType.MemberListType listType, RuntimeType.RuntimeTypeCache.CacheType cacheType)
        {
          if (cNameLen != 0)
            Encoding.UTF8.GetBytes(pName, cNameLen, pUtf8Name, cUtf8Name);
          RuntimeType.RuntimeTypeCache.Filter filter = new RuntimeType.RuntimeTypeCache.Filter(pUtf8Name, cUtf8Name, listType);
          object obj = (object) null;
          switch (cacheType)
          {
            case RuntimeType.RuntimeTypeCache.CacheType.Method:
              obj = (object) this.PopulateMethods(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Constructor:
              obj = (object) this.PopulateConstructors(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Field:
              obj = (object) this.PopulateFields(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Property:
              obj = (object) this.PopulateProperties(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Event:
              obj = (object) this.PopulateEvents(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.Interface:
              obj = (object) this.PopulateInterfaces(filter);
              break;
            case RuntimeType.RuntimeTypeCache.CacheType.NestedType:
              obj = (object) this.PopulateNestedClasses(filter);
              break;
          }
          return (T[]) obj;
        }

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal void Insert(ref T[] list, string name, RuntimeType.MemberListType listType)
        {
          bool lockTaken = false;
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            Monitor.Enter((object) this, ref lockTaken);
            switch (listType)
            {
              case RuntimeType.MemberListType.All:
                if (!this.m_cacheComplete)
                {
                  this.MergeWithGlobalList(list);
                  int length = this.m_allMembers.Length;
                  while (length > 0 && !((MemberInfo) this.m_allMembers[length - 1] != (MemberInfo) null))
                    --length;
                  Array.Resize<T>(ref this.m_allMembers, length);
                  Volatile.Write(ref this.m_cacheComplete, true);
                  break;
                }
                list = this.m_allMembers;
                break;
              case RuntimeType.MemberListType.CaseSensitive:
                T[] csMemberInfo = this.m_csMemberInfos[name];
                if (csMemberInfo == null)
                {
                  this.MergeWithGlobalList(list);
                  this.m_csMemberInfos[name] = list;
                  break;
                }
                list = csMemberInfo;
                break;
              case RuntimeType.MemberListType.CaseInsensitive:
                T[] cisMemberInfo = this.m_cisMemberInfos[name];
                if (cisMemberInfo == null)
                {
                  this.MergeWithGlobalList(list);
                  this.m_cisMemberInfos[name] = list;
                  break;
                }
                list = cisMemberInfo;
                break;
              default:
                this.MergeWithGlobalList(list);
                break;
            }
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit((object) this);
          }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        private void MergeWithGlobalList(T[] list)
        {
          T[] objArray = this.m_allMembers;
          if (objArray == null)
          {
            this.m_allMembers = list;
          }
          else
          {
            int length = objArray.Length;
            int index1 = 0;
            for (int index2 = 0; index2 < list.Length; ++index2)
            {
              T obj1 = list[index2];
              bool flag = false;
              int index3;
              for (index3 = 0; index3 < length; ++index3)
              {
                T obj2 = objArray[index3];
                if (!((MemberInfo) obj2 == (MemberInfo) null))
                {
                  if (obj1.CacheEquals((object) obj2))
                  {
                    list[index2] = obj2;
                    flag = true;
                    break;
                  }
                }
                else
                  break;
              }
              if (!flag)
              {
                if (index1 == 0)
                  index1 = index3;
                if (index1 >= objArray.Length)
                {
                  int newSize = !this.m_cacheComplete ? Math.Max(Math.Max(4, 2 * objArray.Length), list.Length) : objArray.Length + 1;
                  T[] array = objArray;
                  Array.Resize<T>(ref array, newSize);
                  objArray = array;
                }
                objArray[index1] = obj1;
                ++index1;
              }
            }
            this.m_allMembers = objArray;
          }
        }

        [SecuritySafeCritical]
        private unsafe RuntimeMethodInfo[] PopulateMethods(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          RuntimeType.ListBuilder<RuntimeMethodInfo> listBuilder = new RuntimeType.ListBuilder<RuntimeMethodInfo>();
          RuntimeType runtimeType = this.ReflectedType;
          if (RuntimeTypeHandle.IsInterface(runtimeType))
          {
            foreach (RuntimeMethodHandleInternal introducedMethod in RuntimeTypeHandle.GetIntroducedMethods(runtimeType))
            {
              if (!filter.RequiresStringComparison() || RuntimeMethodHandle.MatchesNameHash(introducedMethod, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(introducedMethod)))
              {
                MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(introducedMethod);
                bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
                bool isStatic = (uint) (attributes & MethodAttributes.Static) > 0U;
                bool isInherited = false;
                BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
                if ((attributes & MethodAttributes.RTSpecialName) == MethodAttributes.PrivateScope)
                {
                  RuntimeMethodInfo runtimeMethodInfo = new RuntimeMethodInfo(RuntimeMethodHandle.GetStubIfNeeded(introducedMethod, runtimeType, (RuntimeType[]) null), runtimeType, this.m_runtimeTypeCache, attributes, bindingFlags, (object) null);
                  listBuilder.Add(runtimeMethodInfo);
                }
              }
            }
          }
          else
          {
            while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
              runtimeType = runtimeType.GetBaseType();
            bool* flagPtr = stackalloc bool[RuntimeTypeHandle.GetNumVirtuals(runtimeType)];
            bool isValueType = runtimeType.IsValueType;
            do
            {
              int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
              foreach (RuntimeMethodHandleInternal introducedMethod in RuntimeTypeHandle.GetIntroducedMethods(runtimeType))
              {
                if (!filter.RequiresStringComparison() || RuntimeMethodHandle.MatchesNameHash(introducedMethod, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(introducedMethod)))
                {
                  MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(introducedMethod);
                  MethodAttributes methodAttributes = attributes & MethodAttributes.MemberAccessMask;
                  if ((attributes & MethodAttributes.RTSpecialName) == MethodAttributes.PrivateScope)
                  {
                    bool flag1 = false;
                    int index = 0;
                    if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
                    {
                      index = RuntimeMethodHandle.GetSlot(introducedMethod);
                      flag1 = index < numVirtuals;
                    }
                    bool isInherited = runtimeType != this.ReflectedType;
                    if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
                    {
                      bool flag2 = methodAttributes == MethodAttributes.Private;
                      if (isInherited & flag2 && !flag1)
                        continue;
                    }
                    if (flag1)
                    {
                      if (!flagPtr[index])
                        flagPtr[index] = true;
                      else
                        continue;
                    }
                    else if (isValueType && (attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != MethodAttributes.PrivateScope)
                      continue;
                    bool isPublic = methodAttributes == MethodAttributes.Public;
                    bool isStatic = (uint) (attributes & MethodAttributes.Static) > 0U;
                    BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
                    RuntimeMethodInfo runtimeMethodInfo = new RuntimeMethodInfo(RuntimeMethodHandle.GetStubIfNeeded(introducedMethod, runtimeType, (RuntimeType[]) null), runtimeType, this.m_runtimeTypeCache, attributes, bindingFlags, (object) null);
                    listBuilder.Add(runtimeMethodInfo);
                  }
                }
              }
              runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
            }
            while (runtimeType != (RuntimeType) null);
          }
          return listBuilder.ToArray();
        }

        [SecuritySafeCritical]
        private RuntimeConstructorInfo[] PopulateConstructors(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          if (this.ReflectedType.IsGenericParameter)
            return EmptyArray<RuntimeConstructorInfo>.Value;
          RuntimeType.ListBuilder<RuntimeConstructorInfo> listBuilder = new RuntimeType.ListBuilder<RuntimeConstructorInfo>();
          RuntimeType reflectedType = this.ReflectedType;
          foreach (RuntimeMethodHandleInternal introducedMethod in RuntimeTypeHandle.GetIntroducedMethods(reflectedType))
          {
            if (!filter.RequiresStringComparison() || RuntimeMethodHandle.MatchesNameHash(introducedMethod, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(introducedMethod)))
            {
              MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(introducedMethod);
              if ((attributes & MethodAttributes.RTSpecialName) != MethodAttributes.PrivateScope)
              {
                bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
                bool isStatic = (uint) (attributes & MethodAttributes.Static) > 0U;
                bool isInherited = false;
                BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
                RuntimeConstructorInfo runtimeConstructorInfo = new RuntimeConstructorInfo(RuntimeMethodHandle.GetStubIfNeeded(introducedMethod, reflectedType, (RuntimeType[]) null), this.ReflectedType, this.m_runtimeTypeCache, attributes, bindingFlags);
                listBuilder.Add(runtimeConstructorInfo);
              }
            }
          }
          return listBuilder.ToArray();
        }

        [SecuritySafeCritical]
        private RuntimeFieldInfo[] PopulateFields(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          RuntimeType.ListBuilder<RuntimeFieldInfo> list = new RuntimeType.ListBuilder<RuntimeFieldInfo>();
          RuntimeType runtimeType = this.ReflectedType;
          while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
            runtimeType = runtimeType.GetBaseType();
          for (; runtimeType != (RuntimeType) null; runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType))
          {
            this.PopulateRtFields(filter, runtimeType, ref list);
            this.PopulateLiteralFields(filter, runtimeType, ref list);
          }
          if (this.ReflectedType.IsGenericParameter)
          {
            Type[] interfaces = this.ReflectedType.BaseType.GetInterfaces();
            for (int index = 0; index < interfaces.Length; ++index)
            {
              this.PopulateLiteralFields(filter, (RuntimeType) interfaces[index], ref list);
              this.PopulateRtFields(filter, (RuntimeType) interfaces[index], ref list);
            }
          }
          else
          {
            Type[] interfaces = RuntimeTypeHandle.GetInterfaces(this.ReflectedType);
            if (interfaces != null)
            {
              for (int index = 0; index < interfaces.Length; ++index)
              {
                this.PopulateLiteralFields(filter, (RuntimeType) interfaces[index], ref list);
                this.PopulateRtFields(filter, (RuntimeType) interfaces[index], ref list);
              }
            }
          }
          return list.ToArray();
        }

        [SecuritySafeCritical]
        private unsafe void PopulateRtFields(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
        {
          IntPtr* numPtr1 = stackalloc IntPtr[64];
          int count = 64;
          if (!RuntimeTypeHandle.GetFields(declaringType, numPtr1, &count))
          {
            fixed (IntPtr* numPtr2 = new IntPtr[count])
            {
              RuntimeTypeHandle.GetFields(declaringType, numPtr2, &count);
              this.PopulateRtFields(filter, numPtr2, count, declaringType, ref list);
            }
          }
          else
          {
            if (count <= 0)
              return;
            this.PopulateRtFields(filter, numPtr1, count, declaringType, ref list);
          }
        }

        [SecurityCritical]
        private unsafe void PopulateRtFields(RuntimeType.RuntimeTypeCache.Filter filter, IntPtr* ppFieldHandles, int count, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
        {
          bool flag = RuntimeTypeHandle.HasInstantiation(declaringType) && !RuntimeTypeHandle.ContainsGenericVariables(declaringType);
          bool isInherited = declaringType != this.ReflectedType;
          for (int index = 0; index < count; ++index)
          {
            RuntimeFieldHandleInternal fieldHandleInternal = new RuntimeFieldHandleInternal(ppFieldHandles[index]);
            if (!filter.RequiresStringComparison() || RuntimeFieldHandle.MatchesNameHash(fieldHandleInternal, filter.GetHashToMatch()) && filter.Match(RuntimeFieldHandle.GetUtf8Name(fieldHandleInternal)))
            {
              FieldAttributes attributes = RuntimeFieldHandle.GetAttributes(fieldHandleInternal);
              FieldAttributes fieldAttributes = attributes & FieldAttributes.FieldAccessMask;
              if (!isInherited || fieldAttributes != FieldAttributes.Private)
              {
                bool isPublic = fieldAttributes == FieldAttributes.Public;
                bool isStatic = (uint) (attributes & FieldAttributes.Static) > 0U;
                BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
                if (flag & isStatic)
                  fieldHandleInternal = RuntimeFieldHandle.GetStaticFieldForGenericType(fieldHandleInternal, declaringType);
                RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo) new RtFieldInfo(fieldHandleInternal, declaringType, this.m_runtimeTypeCache, bindingFlags);
                list.Add(runtimeFieldInfo);
              }
            }
          }
        }

        [SecuritySafeCritical]
        private void PopulateLiteralFields(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
        {
          int token = RuntimeTypeHandle.GetToken(declaringType);
          if (System.Reflection.MetadataToken.IsNullToken(token))
            return;
          MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(declaringType);
          MetadataEnumResult result;
          metadataImport.EnumFields(token, out result);
          for (int index = 0; index < result.Length; ++index)
          {
            int num = result[index];
            FieldAttributes fieldAttributes1;
            metadataImport.GetFieldDefProps(num, out fieldAttributes1);
            FieldAttributes fieldAttributes2 = fieldAttributes1 & FieldAttributes.FieldAccessMask;
            if ((fieldAttributes1 & FieldAttributes.Literal) != FieldAttributes.PrivateScope)
            {
              bool isInherited = declaringType != this.ReflectedType;
              if (!isInherited || fieldAttributes2 != FieldAttributes.Private)
              {
                if (filter.RequiresStringComparison())
                {
                  Utf8String name = metadataImport.GetName(num);
                  if (!filter.Match(name))
                    continue;
                }
                bool isPublic = fieldAttributes2 == FieldAttributes.Public;
                bool isStatic = (uint) (fieldAttributes1 & FieldAttributes.Static) > 0U;
                BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
                RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo) new MdFieldInfo(num, fieldAttributes1, declaringType.GetTypeHandleInternal(), this.m_runtimeTypeCache, bindingFlags);
                list.Add(runtimeFieldInfo);
              }
            }
          }
        }

        private static void AddElementTypes(Type template, IList<Type> types)
        {
          if (!template.HasElementType)
            return;
          RuntimeType.RuntimeTypeCache.MemberInfoCache<T>.AddElementTypes(template.GetElementType(), types);
          for (int index = 0; index < types.Count; ++index)
          {
            if (template.IsArray)
              types[index] = !template.IsSzArray ? types[index].MakeArrayType(template.GetArrayRank()) : types[index].MakeArrayType();
            else if (template.IsPointer)
              types[index] = types[index].MakePointerType();
          }
        }

        private void AddSpecialInterface(ref RuntimeType.ListBuilder<RuntimeType> list, RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType iList, bool addSubInterface)
        {
          if (!iList.IsAssignableFrom((System.Reflection.TypeInfo) this.ReflectedType))
            return;
          if (filter.Match(RuntimeTypeHandle.GetUtf8Name(iList)))
            list.Add(iList);
          if (!addSubInterface)
            return;
          foreach (RuntimeType type in iList.GetInterfaces())
          {
            if (type.IsGenericType && filter.Match(RuntimeTypeHandle.GetUtf8Name(type)))
              list.Add(type);
          }
        }

        [SecuritySafeCritical]
        private RuntimeType[] PopulateInterfaces(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          RuntimeType.ListBuilder<RuntimeType> list = new RuntimeType.ListBuilder<RuntimeType>();
          RuntimeType reflectedType = this.ReflectedType;
          if (!RuntimeTypeHandle.IsGenericVariable(reflectedType))
          {
            Type[] interfaces = RuntimeTypeHandle.GetInterfaces(reflectedType);
            if (interfaces != null)
            {
              for (int index = 0; index < interfaces.Length; ++index)
              {
                RuntimeType type = (RuntimeType) interfaces[index];
                if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(type)))
                  list.Add(type);
              }
            }
            if (this.ReflectedType.IsSzArray)
            {
              RuntimeType elementType = (RuntimeType) this.ReflectedType.GetElementType();
              if (!elementType.IsPointer)
              {
                this.AddSpecialInterface(ref list, filter, (RuntimeType) typeof (IList<>).MakeGenericType((Type) elementType), true);
                this.AddSpecialInterface(ref list, filter, (RuntimeType) typeof (IReadOnlyList<>).MakeGenericType((Type) elementType), false);
                this.AddSpecialInterface(ref list, filter, (RuntimeType) typeof (IReadOnlyCollection<>).MakeGenericType((Type) elementType), false);
              }
            }
          }
          else
          {
            List<RuntimeType> runtimeTypeList = new List<RuntimeType>();
            foreach (RuntimeType parameterConstraint in reflectedType.GetGenericParameterConstraints())
            {
              if (parameterConstraint.IsInterface)
                runtimeTypeList.Add(parameterConstraint);
              foreach (Type type in parameterConstraint.GetInterfaces())
                runtimeTypeList.Add(type as RuntimeType);
            }
            Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>();
            for (int index = 0; index < runtimeTypeList.Count; ++index)
            {
              RuntimeType key = runtimeTypeList[index];
              if (!dictionary.ContainsKey(key))
                dictionary[key] = key;
            }
            RuntimeType[] array = new RuntimeType[dictionary.Values.Count];
            dictionary.Values.CopyTo(array, 0);
            for (int index = 0; index < array.Length; ++index)
            {
              if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(array[index])))
                list.Add(array[index]);
            }
          }
          return list.ToArray();
        }

        [SecuritySafeCritical]
        private RuntimeType[] PopulateNestedClasses(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          RuntimeType type1 = this.ReflectedType;
          while (RuntimeTypeHandle.IsGenericVariable(type1))
            type1 = type1.GetBaseType();
          int token = RuntimeTypeHandle.GetToken(type1);
          if (System.Reflection.MetadataToken.IsNullToken(token))
            return EmptyArray<RuntimeType>.Value;
          RuntimeType.ListBuilder<RuntimeType> listBuilder = new RuntimeType.ListBuilder<RuntimeType>();
          RuntimeModule module = RuntimeTypeHandle.GetModule(type1);
          MetadataEnumResult result;
          ModuleHandle.GetMetadataImport(module).EnumNestedTypes(token, out result);
          for (int index = 0; index < result.Length; ++index)
          {
            RuntimeType type2;
            try
            {
              type2 = ModuleHandle.ResolveTypeHandleInternal(module, result[index], (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null);
            }
            catch (TypeLoadException ex)
            {
              continue;
            }
            if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(type2)))
              listBuilder.Add(type2);
          }
          return listBuilder.ToArray();
        }

        [SecuritySafeCritical]
        private RuntimeEventInfo[] PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          Dictionary<string, RuntimeEventInfo> csEventInfos = filter.CaseSensitive() ? (Dictionary<string, RuntimeEventInfo>) null : new Dictionary<string, RuntimeEventInfo>();
          RuntimeType runtimeType = this.ReflectedType;
          RuntimeType.ListBuilder<RuntimeEventInfo> list = new RuntimeType.ListBuilder<RuntimeEventInfo>();
          if (!RuntimeTypeHandle.IsInterface(runtimeType))
          {
            while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
              runtimeType = runtimeType.GetBaseType();
            for (; runtimeType != (RuntimeType) null; runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType))
              this.PopulateEvents(filter, runtimeType, csEventInfos, ref list);
          }
          else
            this.PopulateEvents(filter, runtimeType, csEventInfos, ref list);
          return list.ToArray();
        }

        [SecuritySafeCritical]
        private void PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, Dictionary<string, RuntimeEventInfo> csEventInfos, ref RuntimeType.ListBuilder<RuntimeEventInfo> list)
        {
          int token = RuntimeTypeHandle.GetToken(declaringType);
          if (System.Reflection.MetadataToken.IsNullToken(token))
            return;
          MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(declaringType);
          MetadataEnumResult result;
          metadataImport.EnumEvents(token, out result);
          for (int index = 0; index < result.Length; ++index)
          {
            int num = result[index];
            if (filter.RequiresStringComparison())
            {
              Utf8String name = metadataImport.GetName(num);
              if (!filter.Match(name))
                continue;
            }
            bool isPrivate;
            RuntimeEventInfo runtimeEventInfo = new RuntimeEventInfo(num, declaringType, this.m_runtimeTypeCache, out isPrivate);
            if (!(declaringType != this.m_runtimeTypeCache.GetRuntimeType() & isPrivate))
            {
              if (csEventInfos != null)
              {
                string name = runtimeEventInfo.Name;
                if (!((EventInfo) csEventInfos.GetValueOrDefault(name) != (EventInfo) null))
                  csEventInfos[name] = runtimeEventInfo;
                else
                  continue;
              }
              else if (list.Count > 0)
                break;
              list.Add(runtimeEventInfo);
            }
          }
        }

        [SecuritySafeCritical]
        private RuntimePropertyInfo[] PopulateProperties(RuntimeType.RuntimeTypeCache.Filter filter)
        {
          RuntimeType runtimeType = this.ReflectedType;
          RuntimeType.ListBuilder<RuntimePropertyInfo> list = new RuntimeType.ListBuilder<RuntimePropertyInfo>();
          if (!RuntimeTypeHandle.IsInterface(runtimeType))
          {
            while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
              runtimeType = runtimeType.GetBaseType();
            Dictionary<string, List<RuntimePropertyInfo>> csPropertyInfos = filter.CaseSensitive() ? (Dictionary<string, List<RuntimePropertyInfo>>) null : new Dictionary<string, List<RuntimePropertyInfo>>();
            bool[] usedSlots = new bool[RuntimeTypeHandle.GetNumVirtuals(runtimeType)];
            do
            {
              this.PopulateProperties(filter, runtimeType, csPropertyInfos, usedSlots, ref list);
              runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
            }
            while (runtimeType != (RuntimeType) null);
          }
          else
            this.PopulateProperties(filter, runtimeType, (Dictionary<string, List<RuntimePropertyInfo>>) null, (bool[]) null, ref list);
          return list.ToArray();
        }

        [SecuritySafeCritical]
        private void PopulateProperties(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, Dictionary<string, List<RuntimePropertyInfo>> csPropertyInfos, bool[] usedSlots, ref RuntimeType.ListBuilder<RuntimePropertyInfo> list)
        {
          int token = RuntimeTypeHandle.GetToken(declaringType);
          if (System.Reflection.MetadataToken.IsNullToken(token))
            return;
          MetadataEnumResult result;
          RuntimeTypeHandle.GetMetadataImport(declaringType).EnumProperties(token, out result);
          RuntimeModule module = RuntimeTypeHandle.GetModule(declaringType);
          int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(declaringType);
          for (int index1 = 0; index1 < result.Length; ++index1)
          {
            int num = result[index1];
            if (filter.RequiresStringComparison())
            {
              if (ModuleHandle.ContainsPropertyMatchingHash(module, num, filter.GetHashToMatch()))
              {
                Utf8String name = declaringType.GetRuntimeModule().MetadataImport.GetName(num);
                if (!filter.Match(name))
                  continue;
              }
              else
                continue;
            }
            bool isPrivate;
            RuntimePropertyInfo runtimePropertyInfo = new RuntimePropertyInfo(num, declaringType, this.m_runtimeTypeCache, out isPrivate);
            if (usedSlots != null)
            {
              if (!(declaringType != this.ReflectedType & isPrivate))
              {
                MethodInfo methodInfo = runtimePropertyInfo.GetGetMethod();
                if (methodInfo == (MethodInfo) null)
                  methodInfo = runtimePropertyInfo.GetSetMethod();
                if (methodInfo != (MethodInfo) null)
                {
                  int slot = RuntimeMethodHandle.GetSlot((IRuntimeMethodInfo) methodInfo);
                  if (slot < numVirtuals)
                  {
                    if (!usedSlots[slot])
                      usedSlots[slot] = true;
                    else
                      continue;
                  }
                }
                if (csPropertyInfos != null)
                {
                  string name = runtimePropertyInfo.Name;
                  List<RuntimePropertyInfo> runtimePropertyInfoList = csPropertyInfos.GetValueOrDefault(name);
                  if (runtimePropertyInfoList == null)
                  {
                    runtimePropertyInfoList = new List<RuntimePropertyInfo>(1);
                    csPropertyInfos[name] = runtimePropertyInfoList;
                  }
                  for (int index2 = 0; index2 < runtimePropertyInfoList.Count; ++index2)
                  {
                    if (runtimePropertyInfo.EqualsSig(runtimePropertyInfoList[index2]))
                    {
                      runtimePropertyInfoList = (List<RuntimePropertyInfo>) null;
                      break;
                    }
                  }
                  if (runtimePropertyInfoList != null)
                    runtimePropertyInfoList.Add(runtimePropertyInfo);
                  else
                    continue;
                }
                else
                {
                  bool flag = false;
                  for (int index2 = 0; index2 < list.Count; ++index2)
                  {
                    if (runtimePropertyInfo.EqualsSig(list[index2]))
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (flag)
                    continue;
                }
              }
              else
                continue;
            }
            list.Add(runtimePropertyInfo);
          }
        }

        internal T[] GetMemberList(RuntimeType.MemberListType listType, string name, RuntimeType.RuntimeTypeCache.CacheType cacheType)
        {
          switch (listType)
          {
            case RuntimeType.MemberListType.CaseSensitive:
              return this.m_csMemberInfos[name] ?? this.Populate(name, listType, cacheType);
            case RuntimeType.MemberListType.CaseInsensitive:
              return this.m_cisMemberInfos[name] ?? this.Populate(name, listType, cacheType);
            default:
              if (Volatile.Read(ref this.m_cacheComplete))
                return this.m_allMembers;
              return this.Populate((string) null, listType, cacheType);
          }
        }

        internal RuntimeType ReflectedType
        {
          get
          {
            return this.m_runtimeTypeCache.GetRuntimeType();
          }
        }
      }
    }

    private class ActivatorCacheEntry
    {
      internal readonly RuntimeType m_type;
      internal volatile CtorDelegate m_ctor;
      internal readonly RuntimeMethodHandleInternal m_hCtorMethodHandle;
      internal readonly MethodAttributes m_ctorAttributes;
      internal readonly bool m_bNeedSecurityCheck;
      internal volatile bool m_bFullyInitialized;

      [SecurityCritical]
      internal ActivatorCacheEntry(RuntimeType t, RuntimeMethodHandleInternal rmh, bool bNeedSecurityCheck)
      {
        this.m_type = t;
        this.m_bNeedSecurityCheck = bNeedSecurityCheck;
        this.m_hCtorMethodHandle = rmh;
        if (this.m_hCtorMethodHandle.IsNullHandle())
          return;
        this.m_ctorAttributes = RuntimeMethodHandle.GetAttributes(this.m_hCtorMethodHandle);
      }
    }

    private class ActivatorCache
    {
      private readonly RuntimeType.ActivatorCacheEntry[] cache = new RuntimeType.ActivatorCacheEntry[16];
      private const int CACHE_SIZE = 16;
      private volatile int hash_counter;
      private volatile ConstructorInfo delegateCtorInfo;
      private volatile PermissionSet delegateCreatePermissions;

      private void InitializeDelegateCreator()
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        permissionSet.AddPermission((IPermission) new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
        permissionSet.AddPermission((IPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
        this.delegateCreatePermissions = permissionSet;
        this.delegateCtorInfo = typeof (CtorDelegate).GetConstructor(new Type[2]
        {
          typeof (object),
          typeof (IntPtr)
        });
      }

      [SecuritySafeCritical]
      private void InitializeCacheEntry(RuntimeType.ActivatorCacheEntry ace)
      {
        if (!ace.m_type.IsValueType)
        {
          if (this.delegateCtorInfo == (ConstructorInfo) null)
            this.InitializeDelegateCreator();
          this.delegateCreatePermissions.Assert();
          CtorDelegate ctorDelegate = (CtorDelegate) this.delegateCtorInfo.Invoke(new object[2]
          {
            null,
            (object) RuntimeMethodHandle.GetFunctionPointer(ace.m_hCtorMethodHandle)
          });
          ace.m_ctor = ctorDelegate;
        }
        ace.m_bFullyInitialized = true;
      }

      internal RuntimeType.ActivatorCacheEntry GetEntry(RuntimeType t)
      {
        int index1 = this.hash_counter;
        for (int index2 = 0; index2 < 16; ++index2)
        {
          RuntimeType.ActivatorCacheEntry ace = Volatile.Read<RuntimeType.ActivatorCacheEntry>(ref this.cache[index1]);
          if (ace != null && ace.m_type == t)
          {
            if (!ace.m_bFullyInitialized)
              this.InitializeCacheEntry(ace);
            return ace;
          }
          index1 = index1 + 1 & 15;
        }
        return (RuntimeType.ActivatorCacheEntry) null;
      }

      internal void SetEntry(RuntimeType.ActivatorCacheEntry ace)
      {
        int index = this.hash_counter - 1 & 15;
        this.hash_counter = index;
        Volatile.Write<RuntimeType.ActivatorCacheEntry>(ref this.cache[index], ace);
      }
    }

    [Flags]
    private enum DispatchWrapperType
    {
      Unknown = 1,
      Dispatch = 2,
      Record = 4,
      Error = 8,
      Currency = 16, // 0x00000010
      BStr = 32, // 0x00000020
      SafeArray = 65536, // 0x00010000
    }
  }
}
