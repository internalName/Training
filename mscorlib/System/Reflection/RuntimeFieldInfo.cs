// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal abstract class RuntimeFieldInfo : FieldInfo, ISerializable
  {
    private BindingFlags m_bindingFlags;
    protected RuntimeType.RuntimeTypeCache m_reflectedTypeCache;
    protected RuntimeType m_declaringType;
    private RemotingFieldCachedData m_cachedData;

    protected RuntimeFieldInfo()
    {
    }

    protected RuntimeFieldInfo(RuntimeType.RuntimeTypeCache reflectedTypeCache, RuntimeType declaringType, BindingFlags bindingFlags)
    {
      this.m_bindingFlags = bindingFlags;
      this.m_declaringType = declaringType;
      this.m_reflectedTypeCache = reflectedTypeCache;
    }

    internal RemotingFieldCachedData RemotingCache
    {
      get
      {
        RemotingFieldCachedData remotingFieldCachedData1 = this.m_cachedData;
        if (remotingFieldCachedData1 == null)
        {
          remotingFieldCachedData1 = new RemotingFieldCachedData(this);
          RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData1, (RemotingFieldCachedData) null);
          if (remotingFieldCachedData2 != null)
            remotingFieldCachedData1 = remotingFieldCachedData2;
        }
        return remotingFieldCachedData1;
      }
    }

    internal BindingFlags BindingFlags
    {
      get
      {
        return this.m_bindingFlags;
      }
    }

    private RuntimeType ReflectedTypeInternal
    {
      get
      {
        return this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    internal RuntimeType GetDeclaringTypeInternal()
    {
      return this.m_declaringType;
    }

    internal RuntimeType GetRuntimeType()
    {
      return this.m_declaringType;
    }

    internal abstract RuntimeModule GetRuntimeModule();

    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Field;
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

    public override Type DeclaringType
    {
      get
      {
        if (!this.m_reflectedTypeCache.IsGlobal)
          return (Type) this.m_declaringType;
        return (Type) null;
      }
    }

    public override Module Module
    {
      get
      {
        return (Module) this.GetRuntimeModule();
      }
    }

    public override string ToString()
    {
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return this.FieldType.ToString() + " " + this.Name;
      return this.FieldType.FormatTypeName() + " " + this.Name;
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

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
    }
  }
}
