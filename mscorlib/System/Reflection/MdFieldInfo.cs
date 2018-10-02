// Decompiled with JetBrains decompiler
// Type: System.Reflection.MdFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  [Serializable]
  internal sealed class MdFieldInfo : RuntimeFieldInfo, ISerializable
  {
    private int m_tkField;
    private string m_name;
    private RuntimeType m_fieldType;
    private FieldAttributes m_fieldAttributes;

    internal MdFieldInfo(int tkField, FieldAttributes fieldAttributes, RuntimeTypeHandle declaringTypeHandle, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags)
      : base(reflectedTypeCache, declaringTypeHandle.GetRuntimeType(), bindingFlags)
    {
      this.m_tkField = tkField;
      this.m_name = (string) null;
      this.m_fieldAttributes = fieldAttributes;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      MdFieldInfo mdFieldInfo = o as MdFieldInfo;
      if (mdFieldInfo == null || mdFieldInfo.m_tkField != this.m_tkField)
        return false;
      RuntimeTypeHandle typeHandleInternal = this.m_declaringType.GetTypeHandleInternal();
      ModuleHandle moduleHandle1 = typeHandleInternal.GetModuleHandle();
      ref ModuleHandle local = ref moduleHandle1;
      typeHandleInternal = mdFieldInfo.m_declaringType.GetTypeHandleInternal();
      ModuleHandle moduleHandle2 = typeHandleInternal.GetModuleHandle();
      return local.Equals(moduleHandle2);
    }

    public override string Name
    {
      [SecuritySafeCritical] get
      {
        if (this.m_name == null)
          this.m_name = this.GetRuntimeModule().MetadataImport.GetName(this.m_tkField).ToString();
        return this.m_name;
      }
    }

    public override int MetadataToken
    {
      get
      {
        return this.m_tkField;
      }
    }

    internal override RuntimeModule GetRuntimeModule()
    {
      return this.m_declaringType.GetRuntimeModule();
    }

    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_fieldAttributes;
      }
    }

    public override bool IsSecurityCritical
    {
      get
      {
        return this.DeclaringType.IsSecurityCritical;
      }
    }

    public override bool IsSecuritySafeCritical
    {
      get
      {
        return this.DeclaringType.IsSecuritySafeCritical;
      }
    }

    public override bool IsSecurityTransparent
    {
      get
      {
        return this.DeclaringType.IsSecurityTransparent;
      }
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override object GetValueDirect(TypedReference obj)
    {
      return this.GetValue((object) null);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override void SetValueDirect(TypedReference obj, object value)
    {
      throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override object GetValue(object obj)
    {
      return this.GetValue(false);
    }

    public override object GetRawConstantValue()
    {
      return this.GetValue(true);
    }

    [SecuritySafeCritical]
    private object GetValue(bool raw)
    {
      object obj = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_tkField, this.FieldType.GetTypeHandleInternal(), raw);
      if (obj == DBNull.Value)
        throw new NotSupportedException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
      return obj;
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
    }

    public override unsafe Type FieldType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_fieldType == (RuntimeType) null)
        {
          ConstArray sigOfFieldDef = this.GetRuntimeModule().MetadataImport.GetSigOfFieldDef(this.m_tkField);
          this.m_fieldType = new Signature(sigOfFieldDef.Signature.ToPointer(), sigOfFieldDef.Length, this.m_declaringType).FieldType;
        }
        return (Type) this.m_fieldType;
      }
    }

    public override Type[] GetRequiredCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    public override Type[] GetOptionalCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }
  }
}
