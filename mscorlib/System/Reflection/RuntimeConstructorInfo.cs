// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeConstructorInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RuntimeConstructorInfo : ConstructorInfo, ISerializable, IRuntimeMethodInfo
  {
    private volatile RuntimeType m_declaringType;
    private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;
    private string m_toString;
    private ParameterInfo[] m_parameters;
    private object _empty1;
    private object _empty2;
    private object _empty3;
    private IntPtr m_handle;
    private MethodAttributes m_methodAttributes;
    private BindingFlags m_bindingFlags;
    private volatile Signature m_signature;
    private INVOCATION_FLAGS m_invocationFlags;
    private RemotingMethodCachedData m_cachedData;

    private bool IsNonW8PFrameworkAPI()
    {
      if (this.DeclaringType.IsArray && this.IsPublic && !this.IsStatic)
        return false;
      RuntimeAssembly runtimeAssembly = this.GetRuntimeAssembly();
      if (runtimeAssembly.IsFrameworkAssembly())
      {
        int attributeCtorToken = runtimeAssembly.InvocableAttributeCtorToken;
        if (System.Reflection.MetadataToken.IsNullToken(attributeCtorToken) || !CustomAttribute.IsAttributeDefined(this.GetRuntimeModule(), this.MetadataToken, attributeCtorToken))
          return true;
      }
      return this.GetRuntimeType().IsNonW8PFrameworkAPI();
    }

    internal override bool IsDynamicallyInvokable
    {
      get
      {
        if (AppDomain.ProfileAPICheck)
          return !this.IsNonW8PFrameworkAPI();
        return true;
      }
    }

    internal INVOCATION_FLAGS InvocationFlags
    {
      [SecuritySafeCritical] get
      {
        if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        {
          INVOCATION_FLAGS invocationFlags1 = INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
          Type declaringType = this.DeclaringType;
          INVOCATION_FLAGS invocationFlags2;
          if (declaringType == typeof (void) || declaringType != (Type) null && declaringType.ContainsGenericParameters || ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject))
            invocationFlags2 = invocationFlags1 | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
          else if (this.IsStatic || declaringType != (Type) null && declaringType.IsAbstract)
          {
            invocationFlags2 = invocationFlags1 | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE;
          }
          else
          {
            invocationFlags2 = invocationFlags1 | RuntimeMethodHandle.GetSecurityFlags((IRuntimeMethodInfo) this);
            if ((invocationFlags2 & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN && ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || declaringType != (Type) null && declaringType.NeedsReflectionSecurityCheck))
              invocationFlags2 |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
            if (typeof (Delegate).IsAssignableFrom(this.DeclaringType))
              invocationFlags2 |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR;
          }
          if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
            invocationFlags2 |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
          this.m_invocationFlags = invocationFlags2 | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED;
        }
        return this.m_invocationFlags;
      }
    }

    [SecurityCritical]
    internal RuntimeConstructorInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags)
    {
      this.m_bindingFlags = bindingFlags;
      this.m_reflectedTypeCache = reflectedTypeCache;
      this.m_declaringType = declaringType;
      this.m_handle = handle.Value;
      this.m_methodAttributes = methodAttributes;
    }

    internal RemotingMethodCachedData RemotingCache
    {
      get
      {
        RemotingMethodCachedData methodCachedData1 = this.m_cachedData;
        if (methodCachedData1 == null)
        {
          methodCachedData1 = new RemotingMethodCachedData(this);
          RemotingMethodCachedData methodCachedData2 = Interlocked.CompareExchange<RemotingMethodCachedData>(ref this.m_cachedData, methodCachedData1, (RemotingMethodCachedData) null);
          if (methodCachedData2 != null)
            methodCachedData1 = methodCachedData2;
        }
        return methodCachedData1;
      }
    }

    RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
    {
      [SecuritySafeCritical] get
      {
        return new RuntimeMethodHandleInternal(this.m_handle);
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RuntimeConstructorInfo runtimeConstructorInfo = o as RuntimeConstructorInfo;
      if (runtimeConstructorInfo == null)
        return false;
      return runtimeConstructorInfo.m_handle == this.m_handle;
    }

    private Signature Signature
    {
      get
      {
        if (this.m_signature == null)
          this.m_signature = new Signature((IRuntimeMethodInfo) this, this.m_declaringType);
        return this.m_signature;
      }
    }

    private RuntimeType ReflectedTypeInternal
    {
      get
      {
        return this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    private void CheckConsistency(object target)
    {
      if (target == null && this.IsStatic || this.m_declaringType.IsInstanceOfType(target))
        return;
      if (target == null)
        throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
      throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
    }

    internal BindingFlags BindingFlags
    {
      get
      {
        return this.m_bindingFlags;
      }
    }

    internal RuntimeMethodHandle GetMethodHandle()
    {
      return new RuntimeMethodHandle((IRuntimeMethodInfo) this);
    }

    internal bool IsOverloaded
    {
      get
      {
        return this.m_reflectedTypeCache.GetConstructorList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
      }
    }

    public override string ToString()
    {
      if (this.m_toString == null)
        this.m_toString = "Void " + this.FormatNameAndSig();
      return this.m_toString;
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this, underlyingSystemType);
    }

    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.IsDefined(this, underlyingSystemType);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    public override string Name
    {
      [SecuritySafeCritical] get
      {
        return RuntimeMethodHandle.GetName((IRuntimeMethodInfo) this);
      }
    }

    [ComVisible(true)]
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Constructor;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        if (!this.m_reflectedTypeCache.IsGlobal)
          return (Type) this.m_declaringType;
        return (Type) null;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        if (!this.m_reflectedTypeCache.IsGlobal)
          return (Type) this.ReflectedTypeInternal;
        return (Type) null;
      }
    }

    public override int MetadataToken
    {
      [SecuritySafeCritical] get
      {
        return RuntimeMethodHandle.GetMethodDef((IRuntimeMethodInfo) this);
      }
    }

    public override Module Module
    {
      get
      {
        return (Module) this.GetRuntimeModule();
      }
    }

    internal RuntimeType GetRuntimeType()
    {
      return this.m_declaringType;
    }

    internal RuntimeModule GetRuntimeModule()
    {
      return RuntimeTypeHandle.GetModule(this.m_declaringType);
    }

    internal RuntimeAssembly GetRuntimeAssembly()
    {
      return this.GetRuntimeModule().GetRuntimeAssembly();
    }

    internal override Type GetReturnType()
    {
      return (Type) this.Signature.ReturnType;
    }

    [SecuritySafeCritical]
    internal override ParameterInfo[] GetParametersNoCopy()
    {
      if (this.m_parameters == null)
        this.m_parameters = RuntimeParameterInfo.GetParameters((IRuntimeMethodInfo) this, (MemberInfo) this, this.Signature);
      return this.m_parameters;
    }

    public override ParameterInfo[] GetParameters()
    {
      ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
      if (parametersNoCopy.Length == 0)
        return parametersNoCopy;
      ParameterInfo[] parameterInfoArray = new ParameterInfo[parametersNoCopy.Length];
      Array.Copy((Array) parametersNoCopy, (Array) parameterInfoArray, parametersNoCopy.Length);
      return parameterInfoArray;
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return RuntimeMethodHandle.GetImplAttributes((IRuntimeMethodInfo) this);
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        Type declaringType = this.DeclaringType;
        if (declaringType == (Type) null && this.Module.Assembly.ReflectionOnly || declaringType is ReflectionOnlyType)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
        return new RuntimeMethodHandle((IRuntimeMethodInfo) this);
      }
    }

    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_methodAttributes;
      }
    }

    public override CallingConventions CallingConvention
    {
      get
      {
        return this.Signature.CallingConvention;
      }
    }

    internal static void CheckCanCreateInstance(Type declaringType, bool isVarArg)
    {
      if (declaringType == (Type) null)
        throw new ArgumentNullException(nameof (declaringType));
      if (declaringType is ReflectionOnlyType)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
      if (declaringType.IsInterface)
        throw new MemberAccessException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateInterfaceEx"), (object) declaringType));
      if (declaringType.IsAbstract)
        throw new MemberAccessException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateAbstEx"), (object) declaringType));
      if (declaringType.GetRootElementType() == typeof (ArgIterator))
        throw new NotSupportedException();
      if (isVarArg)
        throw new NotSupportedException();
      if (declaringType.ContainsGenericParameters)
        throw new MemberAccessException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateGenericEx"), (object) declaringType));
      if (declaringType == typeof (void))
        throw new MemberAccessException(Environment.GetResourceString("Access_Void"));
    }

    internal void ThrowNoInvokeException()
    {
      RuntimeConstructorInfo.CheckCanCreateInstance(this.DeclaringType, (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs);
      if ((this.Attributes & MethodAttributes.Static) == MethodAttributes.Static)
        throw new MemberAccessException(Environment.GetResourceString("Acc_NotClassInit"));
      throw new TargetException();
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        this.ThrowNoInvokeException();
      this.CheckConsistency(obj);
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
      }
      if (obj != null)
        new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
      if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          RuntimeMethodHandle.PerformSecurityCheck(obj, (IRuntimeMethodInfo) this, this.m_declaringType, (uint) this.m_invocationFlags);
      }
      Signature signature = this.Signature;
      int length = signature.Arguments.Length;
      int num = parameters != null ? parameters.Length : 0;
      if (length != num)
        throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
      if (num <= 0)
        return RuntimeMethodHandle.InvokeMethod(obj, (object[]) null, signature, false);
      object[] arguments = this.CheckArguments(parameters, binder, invokeAttr, culture, signature);
      object obj1 = RuntimeMethodHandle.InvokeMethod(obj, arguments, signature, false);
      for (int index = 0; index < arguments.Length; ++index)
        parameters[index] = arguments[index];
      return obj1;
    }

    [SecuritySafeCritical]
    [ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
    public override MethodBody GetMethodBody()
    {
      MethodBody methodBody = RuntimeMethodHandle.GetMethodBody((IRuntimeMethodInfo) this, this.ReflectedTypeInternal);
      if (methodBody != null)
        methodBody.m_methodBase = (MethodBase) this;
      return methodBody;
    }

    public override bool IsSecurityCritical
    {
      get
      {
        return RuntimeMethodHandle.IsSecurityCritical((IRuntimeMethodInfo) this);
      }
    }

    public override bool IsSecuritySafeCritical
    {
      get
      {
        return RuntimeMethodHandle.IsSecuritySafeCritical((IRuntimeMethodInfo) this);
      }
    }

    public override bool IsSecurityTransparent
    {
      get
      {
        return RuntimeMethodHandle.IsSecurityTransparent((IRuntimeMethodInfo) this);
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        if (this.DeclaringType != (Type) null)
          return this.DeclaringType.ContainsGenericParameters;
        return false;
      }
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
      RuntimeTypeHandle typeHandle = this.m_declaringType.TypeHandle;
      if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        this.ThrowNoInvokeException();
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
      }
      if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          RuntimeMethodHandle.PerformSecurityCheck((object) null, (IRuntimeMethodInfo) this, this.m_declaringType, (uint) (this.m_invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_CONSTRUCTOR_INVOKE));
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      }
      Signature signature = this.Signature;
      int length = signature.Arguments.Length;
      int num = parameters != null ? parameters.Length : 0;
      if (length != num)
        throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
      if (num <= 0)
        return RuntimeMethodHandle.InvokeMethod((object) null, (object[]) null, signature, true);
      object[] arguments = this.CheckArguments(parameters, binder, invokeAttr, culture, signature);
      object obj = RuntimeMethodHandle.InvokeMethod((object) null, arguments, signature, true);
      for (int index = 0; index < arguments.Length; ++index)
        parameters[index] = arguments[index];
      return obj;
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, (Type[]) null);
    }

    internal string SerializationToString()
    {
      return this.FormatNameAndSig(true);
    }

    internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
    {
      RuntimeMethodHandle.SerializationInvoke((IRuntimeMethodInfo) this, target, info, ref context);
    }
  }
}
