// Decompiled with JetBrains decompiler
// Type: System.Reflection.RtFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RtFieldInfo : RuntimeFieldInfo, IRuntimeFieldInfo
  {
    private IntPtr m_fieldHandle;
    private FieldAttributes m_fieldAttributes;
    private string m_name;
    private RuntimeType m_fieldType;
    private INVOCATION_FLAGS m_invocationFlags;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void PerformVisibilityCheckOnField(IntPtr field, object target, RuntimeType declaringType, FieldAttributes attr, uint invocationFlags);

    private bool IsNonW8PFrameworkAPI()
    {
      if (this.GetRuntimeType().IsNonW8PFrameworkAPI())
        return true;
      if (this.m_declaringType.IsEnum)
        return false;
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
          Type declaringType = this.DeclaringType;
          bool flag = declaringType is ReflectionOnlyType;
          INVOCATION_FLAGS invocationFlags = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
          if (((!(declaringType != (Type) null) || !declaringType.ContainsGenericParameters ? (!(declaringType == (Type) null) ? 0 : (this.Module.Assembly.ReflectionOnly ? 1 : 0)) : 1) | (flag ? 1 : 0)) != 0)
            invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
          if (invocationFlags == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          {
            if ((this.m_fieldAttributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
              invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
            if ((this.m_fieldAttributes & FieldAttributes.HasFieldRVA) != FieldAttributes.PrivateScope)
              invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
            if ((this.IsSecurityCritical && !this.IsSecuritySafeCritical) | ((this.m_fieldAttributes & FieldAttributes.FieldAccessMask) != FieldAttributes.Public || declaringType != (Type) null && declaringType.NeedsReflectionSecurityCheck))
              invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
            Type fieldType = this.FieldType;
            if (fieldType.IsPointer || fieldType.IsEnum || fieldType.IsPrimitive)
              invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD;
          }
          if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
            invocationFlags |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
          this.m_invocationFlags = invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED;
        }
        return this.m_invocationFlags;
      }
    }

    private RuntimeAssembly GetRuntimeAssembly()
    {
      return this.m_declaringType.GetRuntimeAssembly();
    }

    [SecurityCritical]
    internal RtFieldInfo(RuntimeFieldHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags)
      : base(reflectedTypeCache, declaringType, bindingFlags)
    {
      this.m_fieldHandle = handle.Value;
      this.m_fieldAttributes = RuntimeFieldHandle.GetAttributes(handle);
    }

    RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
    {
      [SecuritySafeCritical] get
      {
        return new RuntimeFieldHandleInternal(this.m_fieldHandle);
      }
    }

    internal void CheckConsistency(object target)
    {
      if ((this.m_fieldAttributes & FieldAttributes.Static) == FieldAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
        return;
      if (target == null)
        throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatFldReqTarg"));
      throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_FieldDeclTarget"), (object) this.Name, (object) this.m_declaringType, (object) target.GetType()));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RtFieldInfo rtFieldInfo = o as RtFieldInfo;
      if (rtFieldInfo == null)
        return false;
      return rtFieldInfo.m_fieldHandle == this.m_fieldHandle;
    }

    [SecurityCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture, ref StackCrawlMark stackMark)
    {
      INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
      RuntimeType declaringType = this.DeclaringType as RuntimeType;
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        if (declaringType != (RuntimeType) null && declaringType.ContainsGenericParameters)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
        if (declaringType == (RuntimeType) null && this.Module.Assembly.ReflectionOnly || declaringType is ReflectionOnlyType)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
        throw new FieldAccessException();
      }
      this.CheckConsistency(obj);
      RuntimeType fieldType = (RuntimeType) this.FieldType;
      value = fieldType.CheckValue(value, binder, culture, invokeAttr);
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
      }
      if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint) this.m_invocationFlags);
      bool domainInitialized1 = false;
      if (declaringType == (RuntimeType) null)
      {
        RuntimeFieldHandle.SetValue(this, obj, value, fieldType, this.m_fieldAttributes, (RuntimeType) null, ref domainInitialized1);
      }
      else
      {
        bool domainInitialized2 = declaringType.DomainInitialized;
        RuntimeFieldHandle.SetValue(this, obj, value, fieldType, this.m_fieldAttributes, declaringType, ref domainInitialized2);
        declaringType.DomainInitialized = domainInitialized2;
      }
    }

    [SecurityCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      RuntimeType declaringType = this.DeclaringType as RuntimeType;
      RuntimeType fieldType = (RuntimeType) this.FieldType;
      value = fieldType.CheckValue(value, binder, culture, invokeAttr);
      bool domainInitialized1 = false;
      if (declaringType == (RuntimeType) null)
      {
        RuntimeFieldHandle.SetValue(this, obj, value, fieldType, this.m_fieldAttributes, (RuntimeType) null, ref domainInitialized1);
      }
      else
      {
        bool domainInitialized2 = declaringType.DomainInitialized;
        RuntimeFieldHandle.SetValue(this, obj, value, fieldType, this.m_fieldAttributes, declaringType, ref domainInitialized2);
        declaringType.DomainInitialized = domainInitialized2;
      }
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal object InternalGetValue(object obj, ref StackCrawlMark stackMark)
    {
      INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
      RuntimeType declaringType = this.DeclaringType as RuntimeType;
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        if (declaringType != (RuntimeType) null && this.DeclaringType.ContainsGenericParameters)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
        if (declaringType == (RuntimeType) null && this.Module.Assembly.ReflectionOnly || declaringType is ReflectionOnlyType)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
        throw new FieldAccessException();
      }
      this.CheckConsistency(obj);
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) this.FullName));
      }
      RuntimeType fieldType = (RuntimeType) this.FieldType;
      if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint) (this.m_invocationFlags & ~INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR));
      return this.UnsafeGetValue(obj);
    }

    [SecurityCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    internal object UnsafeGetValue(object obj)
    {
      RuntimeType declaringType = this.DeclaringType as RuntimeType;
      RuntimeType fieldType = (RuntimeType) this.FieldType;
      bool domainInitialized1 = false;
      if (declaringType == (RuntimeType) null)
        return RuntimeFieldHandle.GetValue(this, obj, fieldType, (RuntimeType) null, ref domainInitialized1);
      bool domainInitialized2 = declaringType.DomainInitialized;
      object obj1 = RuntimeFieldHandle.GetValue(this, obj, fieldType, declaringType, ref domainInitialized2);
      declaringType.DomainInitialized = domainInitialized2;
      return obj1;
    }

    public override string Name
    {
      [SecuritySafeCritical] get
      {
        if (this.m_name == null)
          this.m_name = RuntimeFieldHandle.GetName(this);
        return this.m_name;
      }
    }

    internal string FullName
    {
      get
      {
        return string.Format("{0}.{1}", (object) this.DeclaringType.FullName, (object) this.Name);
      }
    }

    public override int MetadataToken
    {
      [SecuritySafeCritical] get
      {
        return RuntimeFieldHandle.GetToken(this);
      }
    }

    [SecuritySafeCritical]
    internal override RuntimeModule GetRuntimeModule()
    {
      return RuntimeTypeHandle.GetModule(RuntimeFieldHandle.GetApproxDeclaringType((IRuntimeFieldInfo) this));
    }

    public override object GetValue(object obj)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalGetValue(obj, ref stackMark);
    }

    public override object GetRawConstantValue()
    {
      throw new InvalidOperationException();
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    public override unsafe object GetValueDirect(TypedReference obj)
    {
      if (obj.IsNull)
        throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
      return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType) this.FieldType, (void*) &obj, (RuntimeType) this.DeclaringType);
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.InternalSetValue(obj, value, invokeAttr, binder, culture, ref stackMark);
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    public override unsafe void SetValueDirect(TypedReference obj, object value)
    {
      if (obj.IsNull)
        throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
      RuntimeFieldHandle.SetValueDirect(this, (RuntimeType) this.FieldType, (void*) &obj, value, (RuntimeType) this.DeclaringType);
    }

    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        Type declaringType = this.DeclaringType;
        if (declaringType == (Type) null && this.Module.Assembly.ReflectionOnly || declaringType is ReflectionOnlyType)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
        return new RuntimeFieldHandle((IRuntimeFieldInfo) this);
      }
    }

    internal IntPtr GetFieldHandle()
    {
      return this.m_fieldHandle;
    }

    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_fieldAttributes;
      }
    }

    public override Type FieldType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_fieldType == (RuntimeType) null)
          this.m_fieldType = new Signature((IRuntimeFieldInfo) this, this.m_declaringType).FieldType;
        return (Type) this.m_fieldType;
      }
    }

    [SecuritySafeCritical]
    public override Type[] GetRequiredCustomModifiers()
    {
      return new Signature((IRuntimeFieldInfo) this, this.m_declaringType).GetCustomModifiers(1, true);
    }

    [SecuritySafeCritical]
    public override Type[] GetOptionalCustomModifiers()
    {
      return new Signature((IRuntimeFieldInfo) this, this.m_declaringType).GetCustomModifiers(1, false);
    }
  }
}
