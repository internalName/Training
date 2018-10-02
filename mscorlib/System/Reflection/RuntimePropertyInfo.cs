// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimePropertyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RuntimePropertyInfo : PropertyInfo, ISerializable
  {
    private int m_token;
    private string m_name;
    [SecurityCritical]
    private unsafe void* m_utf8name;
    private PropertyAttributes m_flags;
    private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;
    private RuntimeMethodInfo m_getterMethod;
    private RuntimeMethodInfo m_setterMethod;
    private MethodInfo[] m_otherMethod;
    private RuntimeType m_declaringType;
    private BindingFlags m_bindingFlags;
    private Signature m_signature;
    private ParameterInfo[] m_parameters;

    [SecurityCritical]
    internal unsafe RuntimePropertyInfo(int tkProperty, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
    {
      MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
      this.m_token = tkProperty;
      this.m_reflectedTypeCache = reflectedTypeCache;
      this.m_declaringType = declaredType;
      ConstArray signature;
      metadataImport.GetPropertyProps(tkProperty, out this.m_utf8name, out this.m_flags, out signature);
      RuntimeMethodInfo runtimeMethodInfo;
      Associates.AssignAssociates(metadataImport, tkProperty, declaredType, reflectedTypeCache.GetRuntimeType(), out runtimeMethodInfo, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_getterMethod, out this.m_setterMethod, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RuntimePropertyInfo runtimePropertyInfo = o as RuntimePropertyInfo;
      if (runtimePropertyInfo == null || runtimePropertyInfo.m_token != this.m_token)
        return false;
      return RuntimeTypeHandle.GetModule(this.m_declaringType).Equals((object) RuntimeTypeHandle.GetModule(runtimePropertyInfo.m_declaringType));
    }

    internal unsafe Signature Signature
    {
      [SecuritySafeCritical] get
      {
        if (this.m_signature == null)
        {
          void* name;
          PropertyAttributes propertyAttributes;
          ConstArray signature;
          this.GetRuntimeModule().MetadataImport.GetPropertyProps(this.m_token, out name, out propertyAttributes, out signature);
          this.m_signature = new Signature(signature.Signature.ToPointer(), signature.Length, this.m_declaringType);
        }
        return this.m_signature;
      }
    }

    internal bool EqualsSig(RuntimePropertyInfo target)
    {
      return Signature.CompareSig(this.Signature, target.Signature);
    }

    internal BindingFlags BindingFlags
    {
      get
      {
        return this.m_bindingFlags;
      }
    }

    public override string ToString()
    {
      return this.FormatNameAndSig(false);
    }

    private string FormatNameAndSig(bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
      stringBuilder.Append(" ");
      stringBuilder.Append(this.Name);
      RuntimeType[] arguments = this.Signature.Arguments;
      if (arguments.Length != 0)
      {
        stringBuilder.Append(" [");
        stringBuilder.Append(MethodBase.ConstructParameters((Type[]) arguments, this.Signature.CallingConvention, serialization));
        stringBuilder.Append("]");
      }
      return stringBuilder.ToString();
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

    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Property;
      }
    }

    public override unsafe string Name
    {
      [SecuritySafeCritical] get
      {
        if (this.m_name == null)
          this.m_name = new Utf8String(this.m_utf8name).ToString();
        return this.m_name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_declaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return (Type) this.ReflectedTypeInternal;
      }
    }

    private RuntimeType ReflectedTypeInternal
    {
      get
      {
        return this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    public override int MetadataToken
    {
      get
      {
        return this.m_token;
      }
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
      return this.m_declaringType.GetRuntimeModule();
    }

    public override Type[] GetRequiredCustomModifiers()
    {
      return this.Signature.GetCustomModifiers(0, true);
    }

    public override Type[] GetOptionalCustomModifiers()
    {
      return this.Signature.GetCustomModifiers(0, false);
    }

    [SecuritySafeCritical]
    internal object GetConstantValue(bool raw)
    {
      object obj = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_token, this.PropertyType.GetTypeHandleInternal(), raw);
      if (obj == DBNull.Value)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
      return obj;
    }

    public override object GetConstantValue()
    {
      return this.GetConstantValue(false);
    }

    public override object GetRawConstantValue()
    {
      return this.GetConstantValue(true);
    }

    public override MethodInfo[] GetAccessors(bool nonPublic)
    {
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      if (Associates.IncludeAccessor((MethodInfo) this.m_getterMethod, nonPublic))
        methodInfoList.Add((MethodInfo) this.m_getterMethod);
      if (Associates.IncludeAccessor((MethodInfo) this.m_setterMethod, nonPublic))
        methodInfoList.Add((MethodInfo) this.m_setterMethod);
      if (this.m_otherMethod != null)
      {
        for (int index = 0; index < this.m_otherMethod.Length; ++index)
        {
          if (Associates.IncludeAccessor(this.m_otherMethod[index], nonPublic))
            methodInfoList.Add(this.m_otherMethod[index]);
        }
      }
      return methodInfoList.ToArray();
    }

    public override Type PropertyType
    {
      get
      {
        return (Type) this.Signature.ReturnType;
      }
    }

    public override MethodInfo GetGetMethod(bool nonPublic)
    {
      if (!Associates.IncludeAccessor((MethodInfo) this.m_getterMethod, nonPublic))
        return (MethodInfo) null;
      return (MethodInfo) this.m_getterMethod;
    }

    public override MethodInfo GetSetMethod(bool nonPublic)
    {
      if (!Associates.IncludeAccessor((MethodInfo) this.m_setterMethod, nonPublic))
        return (MethodInfo) null;
      return (MethodInfo) this.m_setterMethod;
    }

    public override ParameterInfo[] GetIndexParameters()
    {
      ParameterInfo[] parametersNoCopy = this.GetIndexParametersNoCopy();
      int length = parametersNoCopy.Length;
      if (length == 0)
        return parametersNoCopy;
      ParameterInfo[] parameterInfoArray = new ParameterInfo[length];
      Array.Copy((Array) parametersNoCopy, (Array) parameterInfoArray, length);
      return parameterInfoArray;
    }

    internal ParameterInfo[] GetIndexParametersNoCopy()
    {
      if (this.m_parameters == null)
      {
        int length = 0;
        ParameterInfo[] parameterInfoArray1 = (ParameterInfo[]) null;
        MethodInfo getMethod = this.GetGetMethod(true);
        if (getMethod != (MethodInfo) null)
        {
          parameterInfoArray1 = getMethod.GetParametersNoCopy();
          length = parameterInfoArray1.Length;
        }
        else
        {
          MethodInfo setMethod = this.GetSetMethod(true);
          if (setMethod != (MethodInfo) null)
          {
            parameterInfoArray1 = setMethod.GetParametersNoCopy();
            length = parameterInfoArray1.Length - 1;
          }
        }
        ParameterInfo[] parameterInfoArray2 = new ParameterInfo[length];
        for (int index = 0; index < length; ++index)
          parameterInfoArray2[index] = (ParameterInfo) new RuntimeParameterInfo((RuntimeParameterInfo) parameterInfoArray1[index], this);
        this.m_parameters = parameterInfoArray2;
      }
      return this.m_parameters;
    }

    public override PropertyAttributes Attributes
    {
      get
      {
        return this.m_flags;
      }
    }

    public override bool CanRead
    {
      get
      {
        return (MethodInfo) this.m_getterMethod != (MethodInfo) null;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return (MethodInfo) this.m_setterMethod != (MethodInfo) null;
      }
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override object GetValue(object obj, object[] index)
    {
      return this.GetValue(obj, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, index, (CultureInfo) null);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      MethodInfo getMethod = this.GetGetMethod(true);
      if (getMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_GetMethNotFnd"));
      return getMethod.Invoke(obj, invokeAttr, binder, index, (CultureInfo) null);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override void SetValue(object obj, object value, object[] index)
    {
      this.SetValue(obj, value, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, index, (CultureInfo) null);
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      MethodInfo setMethod = this.GetSetMethod(true);
      if (setMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_SetMethNotFnd"));
      object[] parameters;
      if (index != null)
      {
        parameters = new object[index.Length + 1];
        for (int index1 = 0; index1 < index.Length; ++index1)
          parameters[index1] = index[index1];
        parameters[index.Length] = value;
      }
      else
        parameters = new object[1]{ value };
      setMethod.Invoke(obj, invokeAttr, binder, parameters, culture);
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, (Type[]) null);
    }

    internal string SerializationToString()
    {
      return this.FormatNameAndSig(true);
    }
  }
}
