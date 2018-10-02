// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeMethodInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RuntimeMethodInfo : MethodInfo, ISerializable, IRuntimeMethodInfo
  {
    private IntPtr m_handle;
    private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;
    private string m_name;
    private string m_toString;
    private ParameterInfo[] m_parameters;
    private ParameterInfo m_returnParameter;
    private BindingFlags m_bindingFlags;
    private MethodAttributes m_methodAttributes;
    private Signature m_signature;
    private RuntimeType m_declaringType;
    private object m_keepalive;
    private INVOCATION_FLAGS m_invocationFlags;
    private RemotingMethodCachedData m_cachedData;

    private bool IsNonW8PFrameworkAPI()
    {
      if (this.m_declaringType.IsArray && this.IsPublic && !this.IsStatic)
        return false;
      RuntimeAssembly runtimeAssembly = this.GetRuntimeAssembly();
      if (runtimeAssembly.IsFrameworkAssembly())
      {
        int attributeCtorToken = runtimeAssembly.InvocableAttributeCtorToken;
        if (System.Reflection.MetadataToken.IsNullToken(attributeCtorToken) || !CustomAttribute.IsAttributeDefined(this.GetRuntimeModule(), this.MetadataToken, attributeCtorToken))
          return true;
      }
      if (this.GetRuntimeType().IsNonW8PFrameworkAPI())
        return true;
      if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
      {
        foreach (RuntimeType genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.IsNonW8PFrameworkAPI())
            return true;
        }
      }
      return false;
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
          Type declaringType = this.DeclaringType;
          INVOCATION_FLAGS invocationFlags;
          if (this.ContainsGenericParameters || this.ReturnType.IsByRef || declaringType != (Type) null && declaringType.ContainsGenericParameters || ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject))
          {
            invocationFlags = INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
          }
          else
          {
            invocationFlags = RuntimeMethodHandle.GetSecurityFlags((IRuntimeMethodInfo) this);
            if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
            {
              if ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || declaringType != (Type) null && declaringType.NeedsReflectionSecurityCheck)
                invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
              else if (this.IsGenericMethod)
              {
                foreach (Type genericArgument in this.GetGenericArguments())
                {
                  if (genericArgument.NeedsReflectionSecurityCheck)
                  {
                    invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
                    break;
                  }
                }
              }
            }
          }
          if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
            invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
          this.m_invocationFlags = invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED;
        }
        return this.m_invocationFlags;
      }
    }

    [SecurityCritical]
    internal RuntimeMethodInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags, object keepalive)
    {
      this.m_bindingFlags = bindingFlags;
      this.m_declaringType = declaringType;
      this.m_keepalive = keepalive;
      this.m_handle = handle.Value;
      this.m_reflectedTypeCache = reflectedTypeCache;
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

    private RuntimeType ReflectedTypeInternal
    {
      get
      {
        return this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    [SecurityCritical]
    private ParameterInfo[] FetchNonReturnParameters()
    {
      if (this.m_parameters == null)
        this.m_parameters = RuntimeParameterInfo.GetParameters((IRuntimeMethodInfo) this, (MemberInfo) this, this.Signature);
      return this.m_parameters;
    }

    [SecurityCritical]
    private ParameterInfo FetchReturnParameter()
    {
      if (this.m_returnParameter == null)
        this.m_returnParameter = RuntimeParameterInfo.GetReturnParameter((IRuntimeMethodInfo) this, (MemberInfo) this, this.Signature);
      return this.m_returnParameter;
    }

    internal override string FormatNameAndSig(bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder(this.Name);
      TypeNameFormatFlags format = serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic;
      if (this.IsGenericMethod)
        stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation((IRuntimeMethodInfo) this, format));
      stringBuilder.Append("(");
      stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RuntimeMethodInfo runtimeMethodInfo = o as RuntimeMethodInfo;
      if (runtimeMethodInfo == null)
        return false;
      return runtimeMethodInfo.m_handle == this.m_handle;
    }

    internal Signature Signature
    {
      get
      {
        if (this.m_signature == null)
          this.m_signature = new Signature((IRuntimeMethodInfo) this, this.m_declaringType);
        return this.m_signature;
      }
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

    [SecuritySafeCritical]
    internal RuntimeMethodInfo GetParentDefinition()
    {
      if (!this.IsVirtual || this.m_declaringType.IsInterface)
        return (RuntimeMethodInfo) null;
      RuntimeType baseType = (RuntimeType) this.m_declaringType.BaseType;
      if (baseType == (RuntimeType) null)
        return (RuntimeMethodInfo) null;
      int slot = RuntimeMethodHandle.GetSlot((IRuntimeMethodInfo) this);
      if (RuntimeTypeHandle.GetNumVirtuals(baseType) <= slot)
        return (RuntimeMethodInfo) null;
      return (RuntimeMethodInfo) RuntimeType.GetMethodBase(baseType, RuntimeTypeHandle.GetMethodAt(baseType, slot));
    }

    internal RuntimeType GetDeclaringTypeInternal()
    {
      return this.m_declaringType;
    }

    public override string ToString()
    {
      if (this.m_toString == null)
        this.m_toString = this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig();
      return this.m_toString;
    }

    public override int GetHashCode()
    {
      if (this.IsGenericMethod)
        return ValueType.GetHashCodeOfPtr(this.m_handle);
      return base.GetHashCode();
    }

    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      if (!this.IsGenericMethod)
        return obj == this;
      RuntimeMethodInfo runtimeMethodInfo1 = obj as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo1 == (MethodInfo) null || !runtimeMethodInfo1.IsGenericMethod)
        return false;
      IRuntimeMethodInfo runtimeMethodInfo2 = RuntimeMethodHandle.StripMethodInstantiation((IRuntimeMethodInfo) this);
      IRuntimeMethodInfo runtimeMethodInfo3 = RuntimeMethodHandle.StripMethodInstantiation((IRuntimeMethodInfo) runtimeMethodInfo1);
      RuntimeMethodHandleInternal methodHandleInternal = runtimeMethodInfo2.Value;
      IntPtr num1 = methodHandleInternal.Value;
      methodHandleInternal = runtimeMethodInfo3.Value;
      IntPtr num2 = methodHandleInternal.Value;
      if (num1 != num2)
        return false;
      Type[] genericArguments1 = this.GetGenericArguments();
      Type[] genericArguments2 = runtimeMethodInfo1.GetGenericArguments();
      if (genericArguments1.Length != genericArguments2.Length)
        return false;
      for (int index = 0; index < genericArguments1.Length; ++index)
      {
        if (genericArguments1[index] != genericArguments2[index])
          return false;
      }
      return !(this.DeclaringType != runtimeMethodInfo1.DeclaringType) && !(this.ReflectedType != runtimeMethodInfo1.ReflectedType);
    }

    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType, inherit);
    }

    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this, underlyingSystemType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
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
      [SecuritySafeCritical] get
      {
        if (this.m_name == null)
          this.m_name = RuntimeMethodHandle.GetName((IRuntimeMethodInfo) this);
        return this.m_name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        if (this.m_reflectedTypeCache.IsGlobal)
          return (Type) null;
        return (Type) this.m_declaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        if (this.m_reflectedTypeCache.IsGlobal)
          return (Type) null;
        return (Type) this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Method;
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
      return this.m_declaringType.GetRuntimeModule();
    }

    internal RuntimeAssembly GetRuntimeAssembly()
    {
      return this.GetRuntimeModule().GetRuntimeAssembly();
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

    [SecuritySafeCritical]
    internal override ParameterInfo[] GetParametersNoCopy()
    {
      this.FetchNonReturnParameters();
      return this.m_parameters;
    }

    [SecuritySafeCritical]
    public override ParameterInfo[] GetParameters()
    {
      this.FetchNonReturnParameters();
      if (this.m_parameters.Length == 0)
        return this.m_parameters;
      ParameterInfo[] parameterInfoArray = new ParameterInfo[this.m_parameters.Length];
      Array.Copy((Array) this.m_parameters, (Array) parameterInfoArray, this.m_parameters.Length);
      return parameterInfoArray;
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return RuntimeMethodHandle.GetImplAttributes((IRuntimeMethodInfo) this);
    }

    internal bool IsOverloaded
    {
      get
      {
        return this.m_reflectedTypeCache.GetMethodList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
      }
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

    [SecuritySafeCritical]
    [ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
    public override MethodBody GetMethodBody()
    {
      MethodBody methodBody = RuntimeMethodHandle.GetMethodBody((IRuntimeMethodInfo) this, this.ReflectedTypeInternal);
      if (methodBody != null)
        methodBody.m_methodBase = (MethodBase) this;
      return methodBody;
    }

    private void CheckConsistency(object target)
    {
      if ((this.m_methodAttributes & MethodAttributes.Static) == MethodAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
        return;
      if (target == null)
        throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
      throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
    }

    [SecuritySafeCritical]
    private void ThrowNoInvokeException()
    {
      Type declaringType = this.DeclaringType;
      if (declaringType == (Type) null && this.Module.Assembly.ReflectionOnly || declaringType is ReflectionOnlyType)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
      if ((this.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new NotSupportedException();
      if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
        throw new NotSupportedException();
      if (this.DeclaringType.ContainsGenericParameters || this.ContainsGenericParameters)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenParam"));
      if (this.IsAbstract)
        throw new MemberAccessException();
      if (this.ReturnType.IsByRef)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ByRefReturn"));
      throw new TargetException();
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      object[] arguments = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
      INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
      }
      if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
        if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          RuntimeMethodHandle.PerformSecurityCheck(obj, (IRuntimeMethodInfo) this, this.m_declaringType, (uint) this.m_invocationFlags);
      }
      return this.UnsafeInvokeInternal(obj, parameters, arguments);
    }

    [SecurityCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal object UnsafeInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      object[] arguments = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
      return this.UnsafeInvokeInternal(obj, parameters, arguments);
    }

    [SecurityCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    private object UnsafeInvokeInternal(object obj, object[] parameters, object[] arguments)
    {
      if (arguments == null || arguments.Length == 0)
        return RuntimeMethodHandle.InvokeMethod(obj, (object[]) null, this.Signature, false);
      object obj1 = RuntimeMethodHandle.InvokeMethod(obj, arguments, this.Signature, false);
      for (int index = 0; index < arguments.Length; ++index)
        parameters[index] = arguments[index];
      return obj1;
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    private object[] InvokeArgumentsCheck(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      Signature signature = this.Signature;
      int length = signature.Arguments.Length;
      int num = parameters != null ? parameters.Length : 0;
      if ((this.InvocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        this.ThrowNoInvokeException();
      this.CheckConsistency(obj);
      if (length != num)
        throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
      if (num != 0)
        return this.CheckArguments(parameters, binder, invokeAttr, culture, signature);
      return (object[]) null;
    }

    public override Type ReturnType
    {
      get
      {
        return (Type) this.Signature.ReturnType;
      }
    }

    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return (ICustomAttributeProvider) this.ReturnParameter;
      }
    }

    public override ParameterInfo ReturnParameter
    {
      [SecuritySafeCritical] get
      {
        this.FetchReturnParameter();
        return this.m_returnParameter;
      }
    }

    [SecuritySafeCritical]
    public override MethodInfo GetBaseDefinition()
    {
      if (!this.IsVirtual || this.IsStatic || (this.m_declaringType == (RuntimeType) null || this.m_declaringType.IsInterface))
        return (MethodInfo) this;
      int slot = RuntimeMethodHandle.GetSlot((IRuntimeMethodInfo) this);
      RuntimeType type = (RuntimeType) this.DeclaringType;
      RuntimeType reflectedType = type;
      RuntimeMethodHandleInternal methodHandle = new RuntimeMethodHandleInternal();
      while (RuntimeTypeHandle.GetNumVirtuals(type) > slot)
      {
        methodHandle = RuntimeTypeHandle.GetMethodAt(type, slot);
        reflectedType = type;
        type = (RuntimeType) type.BaseType;
        if (!(type != (RuntimeType) null))
          break;
      }
      return (MethodInfo) RuntimeType.GetMethodBase(reflectedType, methodHandle);
    }

    [SecuritySafeCritical]
    public override Delegate CreateDelegate(Type delegateType)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.CreateDelegateInternal(delegateType, (object) null, DelegateBindingFlags.OpenDelegateOnly | DelegateBindingFlags.RelaxedSignature, ref stackMark);
    }

    [SecuritySafeCritical]
    public override Delegate CreateDelegate(Type delegateType, object target)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.CreateDelegateInternal(delegateType, target, DelegateBindingFlags.RelaxedSignature, ref stackMark);
    }

    [SecurityCritical]
    private Delegate CreateDelegateInternal(Type delegateType, object firstArgument, DelegateBindingFlags bindingFlags, ref StackCrawlMark stackMark)
    {
      if (delegateType == (Type) null)
        throw new ArgumentNullException(nameof (delegateType));
      RuntimeType rtType = delegateType as RuntimeType;
      if (rtType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (delegateType));
      if (!rtType.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (delegateType));
      Delegate delegateInternal = Delegate.CreateDelegateInternal(rtType, this, firstArgument, bindingFlags, ref stackMark);
      if ((object) delegateInternal == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
      return delegateInternal;
    }

    [SecuritySafeCritical]
    public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
    {
      if (methodInstantiation == null)
        throw new ArgumentNullException(nameof (methodInstantiation));
      RuntimeType[] runtimeTypeArray = new RuntimeType[methodInstantiation.Length];
      if (!this.IsGenericMethodDefinition)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition", (object) this));
      for (int index1 = 0; index1 < methodInstantiation.Length; ++index1)
      {
        Type type = methodInstantiation[index1];
        if (type == (Type) null)
          throw new ArgumentNullException();
        RuntimeType runtimeType = type as RuntimeType;
        if (runtimeType == (RuntimeType) null)
        {
          Type[] typeArray = new Type[methodInstantiation.Length];
          for (int index2 = 0; index2 < methodInstantiation.Length; ++index2)
            typeArray[index2] = methodInstantiation[index2];
          methodInstantiation = typeArray;
          return MethodBuilderInstantiation.MakeGenericMethod((MethodInfo) this, methodInstantiation);
        }
        runtimeTypeArray[index1] = runtimeType;
      }
      RuntimeType[] argumentsInternal = this.GetGenericArgumentsInternal();
      RuntimeType.SanityCheckGenericArguments(runtimeTypeArray, argumentsInternal);
      try
      {
        return RuntimeType.GetMethodBase(this.ReflectedTypeInternal, RuntimeMethodHandle.GetStubIfNeeded(new RuntimeMethodHandleInternal(this.m_handle), this.m_declaringType, runtimeTypeArray)) as MethodInfo;
      }
      catch (VerificationException ex)
      {
        RuntimeType.ValidateGenericArguments((MemberInfo) this, runtimeTypeArray, (Exception) ex);
        throw;
      }
    }

    internal RuntimeType[] GetGenericArgumentsInternal()
    {
      return RuntimeMethodHandle.GetMethodInstantiationInternal((IRuntimeMethodInfo) this);
    }

    public override Type[] GetGenericArguments()
    {
      return RuntimeMethodHandle.GetMethodInstantiationPublic((IRuntimeMethodInfo) this) ?? EmptyArray<Type>.Value;
    }

    public override MethodInfo GetGenericMethodDefinition()
    {
      if (!this.IsGenericMethod)
        throw new InvalidOperationException();
      return RuntimeType.GetMethodBase(this.m_declaringType, RuntimeMethodHandle.StripMethodInstantiation((IRuntimeMethodInfo) this)) as MethodInfo;
    }

    public override bool IsGenericMethod
    {
      get
      {
        return RuntimeMethodHandle.HasMethodInstantiation((IRuntimeMethodInfo) this);
      }
    }

    public override bool IsGenericMethodDefinition
    {
      get
      {
        return RuntimeMethodHandle.IsGenericMethodDefinition((IRuntimeMethodInfo) this);
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        if (this.DeclaringType != (Type) null && this.DeclaringType.ContainsGenericParameters)
          return true;
        if (!this.IsGenericMethod)
          return false;
        foreach (Type genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.ContainsGenericParameters)
            return true;
        }
        return false;
      }
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.m_reflectedTypeCache.IsGlobal)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
      MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, this.IsGenericMethod & !this.IsGenericMethodDefinition ? this.GetGenericArguments() : (Type[]) null);
    }

    internal string SerializationToString()
    {
      return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
    }

    internal static MethodBase InternalGetCurrentMethod(ref StackCrawlMark stackMark)
    {
      IRuntimeMethodInfo currentMethod = RuntimeMethodHandle.GetCurrentMethod(ref stackMark);
      if (currentMethod == null)
        return (MethodBase) null;
      return RuntimeType.GetMethodBase(currentMethod);
    }
  }
}
