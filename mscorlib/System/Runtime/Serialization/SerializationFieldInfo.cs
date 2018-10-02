﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
  internal sealed class SerializationFieldInfo : System.Reflection.FieldInfo
  {
    internal const string FakeNameSeparatorString = "+";
    private RuntimeFieldInfo m_field;
    private string m_serializationName;
    private RemotingFieldCachedData m_cachedData;

    public override Module Module
    {
      get
      {
        return this.m_field.Module;
      }
    }

    public override int MetadataToken
    {
      get
      {
        return this.m_field.MetadataToken;
      }
    }

    internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
    {
      this.m_field = field;
      this.m_serializationName = namePrefix + "+" + this.m_field.Name;
    }

    public override string Name
    {
      get
      {
        return this.m_serializationName;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return this.m_field.DeclaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return this.m_field.ReflectedType;
      }
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_field.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_field.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_field.IsDefined(attributeType, inherit);
    }

    public override Type FieldType
    {
      get
      {
        return this.m_field.FieldType;
      }
    }

    public override object GetValue(object obj)
    {
      return this.m_field.GetValue(obj);
    }

    [SecurityCritical]
    internal object InternalGetValue(object obj)
    {
      RtFieldInfo field = this.m_field as RtFieldInfo;
      if (!((System.Reflection.FieldInfo) field != (System.Reflection.FieldInfo) null))
        return this.m_field.GetValue(obj);
      field.CheckConsistency(obj);
      return field.UnsafeGetValue(obj);
    }

    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
    }

    [SecurityCritical]
    internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      RtFieldInfo field = this.m_field as RtFieldInfo;
      if ((System.Reflection.FieldInfo) field != (System.Reflection.FieldInfo) null)
      {
        field.CheckConsistency(obj);
        field.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
      }
      else
        this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
    }

    internal RuntimeFieldInfo FieldInfo
    {
      get
      {
        return this.m_field;
      }
    }

    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        return this.m_field.FieldHandle;
      }
    }

    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_field.Attributes;
      }
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
  }
}
