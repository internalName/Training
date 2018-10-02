// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeParameterInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RuntimeParameterInfo : ParameterInfo, ISerializable
  {
    private static readonly Type s_DecimalConstantAttributeType = typeof (DecimalConstantAttribute);
    private static readonly Type s_CustomConstantAttributeType = typeof (CustomConstantAttribute);
    [NonSerialized]
    private int m_tkParamDef;
    [NonSerialized]
    private MetadataImport m_scope;
    [NonSerialized]
    private Signature m_signature;
    [NonSerialized]
    private volatile bool m_nameIsCached;
    [NonSerialized]
    private readonly bool m_noMetadata;
    [NonSerialized]
    private bool m_noDefaultValue;
    [NonSerialized]
    private MethodBase m_originalMember;
    private RemotingParameterCachedData m_cachedData;

    [SecurityCritical]
    internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
    {
      ParameterInfo returnParameter;
      return RuntimeParameterInfo.GetParameters(method, member, sig, out returnParameter, false);
    }

    [SecurityCritical]
    internal static ParameterInfo GetReturnParameter(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
    {
      ParameterInfo returnParameter;
      RuntimeParameterInfo.GetParameters(method, member, sig, out returnParameter, true);
      return returnParameter;
    }

    [SecurityCritical]
    internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo methodHandle, MemberInfo member, Signature sig, out ParameterInfo returnParameter, bool fetchReturnParameter)
    {
      returnParameter = (ParameterInfo) null;
      int length = sig.Arguments.Length;
      ParameterInfo[] parameterInfoArray = fetchReturnParameter ? (ParameterInfo[]) null : new ParameterInfo[length];
      int methodDef = RuntimeMethodHandle.GetMethodDef(methodHandle);
      int num1 = 0;
      if (!System.Reflection.MetadataToken.IsNullToken(methodDef))
      {
        MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(RuntimeMethodHandle.GetDeclaringType(methodHandle));
        MetadataEnumResult result;
        metadataImport.EnumParams(methodDef, out result);
        num1 = result.Length;
        if (num1 > length + 1)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
        for (int index = 0; index < num1; ++index)
        {
          int num2 = result[index];
          int sequence;
          ParameterAttributes attributes;
          metadataImport.GetParamDefProps(num2, out sequence, out attributes);
          --sequence;
          if (fetchReturnParameter && sequence == -1)
          {
            if (returnParameter != null)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
            returnParameter = (ParameterInfo) new RuntimeParameterInfo(sig, metadataImport, num2, sequence, attributes, member);
          }
          else if (!fetchReturnParameter && sequence >= 0)
          {
            if (sequence >= length)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
            parameterInfoArray[sequence] = (ParameterInfo) new RuntimeParameterInfo(sig, metadataImport, num2, sequence, attributes, member);
          }
        }
      }
      if (fetchReturnParameter)
      {
        if (returnParameter == null)
          returnParameter = (ParameterInfo) new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, -1, ParameterAttributes.None, member);
      }
      else if (num1 < parameterInfoArray.Length + 1)
      {
        for (int position = 0; position < parameterInfoArray.Length; ++position)
        {
          if (parameterInfoArray[position] == null)
            parameterInfoArray[position] = (ParameterInfo) new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, position, ParameterAttributes.None, member);
        }
      }
      return parameterInfoArray;
    }

    internal MethodBase DefiningMethod
    {
      get
      {
        return this.m_originalMember != (MethodBase) null ? this.m_originalMember : this.MemberImpl as MethodBase;
      }
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.SetType(typeof (ParameterInfo));
      info.AddValue("AttrsImpl", (object) this.Attributes);
      info.AddValue("ClassImpl", (object) this.ParameterType);
      info.AddValue("DefaultValueImpl", this.DefaultValue);
      info.AddValue("MemberImpl", (object) this.Member);
      info.AddValue("NameImpl", (object) this.Name);
      info.AddValue("PositionImpl", this.Position);
      info.AddValue("_token", this.m_tkParamDef);
    }

    internal RuntimeParameterInfo(RuntimeParameterInfo accessor, RuntimePropertyInfo property)
      : this(accessor, (MemberInfo) property)
    {
      this.m_signature = property.Signature;
    }

    private RuntimeParameterInfo(RuntimeParameterInfo accessor, MemberInfo member)
    {
      this.MemberImpl = member;
      this.m_originalMember = accessor.MemberImpl as MethodBase;
      this.NameImpl = accessor.Name;
      this.m_nameIsCached = true;
      this.ClassImpl = accessor.ParameterType;
      this.PositionImpl = accessor.Position;
      this.AttrsImpl = accessor.Attributes;
      this.m_tkParamDef = System.Reflection.MetadataToken.IsNullToken(accessor.MetadataToken) ? 134217728 : accessor.MetadataToken;
      this.m_scope = accessor.m_scope;
    }

    private RuntimeParameterInfo(Signature signature, MetadataImport scope, int tkParamDef, int position, ParameterAttributes attributes, MemberInfo member)
    {
      this.PositionImpl = position;
      this.MemberImpl = member;
      this.m_signature = signature;
      this.m_tkParamDef = System.Reflection.MetadataToken.IsNullToken(tkParamDef) ? 134217728 : tkParamDef;
      this.m_scope = scope;
      this.AttrsImpl = attributes;
      this.ClassImpl = (Type) null;
      this.NameImpl = (string) null;
    }

    internal RuntimeParameterInfo(MethodInfo owner, string name, Type parameterType, int position)
    {
      this.MemberImpl = (MemberInfo) owner;
      this.NameImpl = name;
      this.m_nameIsCached = true;
      this.m_noMetadata = true;
      this.ClassImpl = parameterType;
      this.PositionImpl = position;
      this.AttrsImpl = ParameterAttributes.None;
      this.m_tkParamDef = 134217728;
      this.m_scope = MetadataImport.EmptyImport;
    }

    public override Type ParameterType
    {
      get
      {
        if (this.ClassImpl == (Type) null)
          this.ClassImpl = this.PositionImpl != -1 ? (Type) this.m_signature.Arguments[this.PositionImpl] : (Type) this.m_signature.ReturnType;
        return this.ClassImpl;
      }
    }

    public override string Name
    {
      [SecuritySafeCritical] get
      {
        if (!this.m_nameIsCached)
        {
          if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
            this.NameImpl = this.m_scope.GetName(this.m_tkParamDef).ToString();
          this.m_nameIsCached = true;
        }
        return this.NameImpl;
      }
    }

    public override bool HasDefaultValue
    {
      get
      {
        if (this.m_noMetadata || this.m_noDefaultValue)
          return false;
        return this.GetDefaultValueInternal(false) != DBNull.Value;
      }
    }

    public override object DefaultValue
    {
      get
      {
        return this.GetDefaultValue(false);
      }
    }

    public override object RawDefaultValue
    {
      get
      {
        return this.GetDefaultValue(true);
      }
    }

    private object GetDefaultValue(bool raw)
    {
      if (this.m_noMetadata)
        return (object) null;
      object obj = this.GetDefaultValueInternal(raw);
      if (obj == DBNull.Value && this.IsOptional)
        obj = Type.Missing;
      return obj;
    }

    [SecuritySafeCritical]
    private object GetDefaultValueInternal(bool raw)
    {
      if (this.m_noDefaultValue)
        return (object) DBNull.Value;
      object obj = (object) null;
      if (this.ParameterType == typeof (DateTime))
      {
        if (raw)
        {
          CustomAttributeTypedArgument attributeTypedArgument = CustomAttributeData.Filter(CustomAttributeData.GetCustomAttributes((ParameterInfo) this), typeof (DateTimeConstantAttribute), 0);
          if (attributeTypedArgument.ArgumentType != (Type) null)
            return (object) new DateTime((long) attributeTypedArgument.Value);
        }
        else
        {
          object[] customAttributes = this.GetCustomAttributes(typeof (DateTimeConstantAttribute), false);
          if (customAttributes != null && customAttributes.Length != 0)
            return ((CustomConstantAttribute) customAttributes[0]).Value;
        }
      }
      if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
        obj = MdConstant.GetValue(this.m_scope, this.m_tkParamDef, this.ParameterType.GetTypeHandleInternal(), raw);
      if (obj == DBNull.Value)
      {
        if (raw)
        {
          foreach (CustomAttributeData customAttribute in (IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes((ParameterInfo) this))
          {
            Type declaringType = customAttribute.Constructor.DeclaringType;
            if (declaringType == typeof (DateTimeConstantAttribute))
              obj = (object) DateTimeConstantAttribute.GetRawDateTimeConstant(customAttribute);
            else if (declaringType == typeof (DecimalConstantAttribute))
              obj = (object) DecimalConstantAttribute.GetRawDecimalConstant(customAttribute);
            else if (declaringType.IsSubclassOf(RuntimeParameterInfo.s_CustomConstantAttributeType))
              obj = CustomConstantAttribute.GetRawConstant(customAttribute);
          }
        }
        else
        {
          object[] customAttributes1 = this.GetCustomAttributes(RuntimeParameterInfo.s_CustomConstantAttributeType, false);
          if (customAttributes1.Length != 0)
          {
            obj = ((CustomConstantAttribute) customAttributes1[0]).Value;
          }
          else
          {
            object[] customAttributes2 = this.GetCustomAttributes(RuntimeParameterInfo.s_DecimalConstantAttributeType, false);
            if (customAttributes2.Length != 0)
              obj = (object) ((DecimalConstantAttribute) customAttributes2[0]).Value;
          }
        }
      }
      if (obj == DBNull.Value)
        this.m_noDefaultValue = true;
      return obj;
    }

    internal RuntimeModule GetRuntimeModule()
    {
      RuntimeMethodInfo member1 = this.Member as RuntimeMethodInfo;
      RuntimeConstructorInfo member2 = this.Member as RuntimeConstructorInfo;
      RuntimePropertyInfo member3 = this.Member as RuntimePropertyInfo;
      if ((MethodInfo) member1 != (MethodInfo) null)
        return member1.GetRuntimeModule();
      if ((ConstructorInfo) member2 != (ConstructorInfo) null)
        return member2.GetRuntimeModule();
      if ((PropertyInfo) member3 != (PropertyInfo) null)
        return member3.GetRuntimeModule();
      return (RuntimeModule) null;
    }

    public override int MetadataToken
    {
      get
      {
        return this.m_tkParamDef;
      }
    }

    public override Type[] GetRequiredCustomModifiers()
    {
      return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, true);
    }

    public override Type[] GetOptionalCustomModifiers()
    {
      return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, false);
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
        return EmptyArray<object>.Value;
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
        return EmptyArray<object>.Value;
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
      if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
        return false;
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.IsDefined(this, underlyingSystemType);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    internal RemotingParameterCachedData RemotingCache
    {
      get
      {
        RemotingParameterCachedData parameterCachedData1 = this.m_cachedData;
        if (parameterCachedData1 == null)
        {
          parameterCachedData1 = new RemotingParameterCachedData(this);
          RemotingParameterCachedData parameterCachedData2 = Interlocked.CompareExchange<RemotingParameterCachedData>(ref this.m_cachedData, parameterCachedData1, (RemotingParameterCachedData) null);
          if (parameterCachedData2 != null)
            parameterCachedData1 = parameterCachedData2;
        }
        return parameterCachedData1;
      }
    }
  }
}
