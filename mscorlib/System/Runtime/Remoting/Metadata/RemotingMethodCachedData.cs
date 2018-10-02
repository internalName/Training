// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingMethodCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  internal class RemotingMethodCachedData : RemotingCachedData
  {
    private MethodBase RI;
    private ParameterInfo[] _parameters;
    private RemotingMethodCachedData.MethodCacheFlags flags;
    private string _typeAndAssemblyName;
    private string _methodName;
    private Type _returnType;
    private int[] _inRefArgMap;
    private int[] _outRefArgMap;
    private int[] _outOnlyArgMap;
    private int[] _nonRefOutArgMap;
    private int[] _marshalRequestMap;
    private int[] _marshalResponseMap;

    internal RemotingMethodCachedData(RuntimeMethodInfo ri)
    {
      this.RI = (MethodBase) ri;
    }

    internal RemotingMethodCachedData(RuntimeConstructorInfo ri)
    {
      this.RI = (MethodBase) ri;
    }

    internal override SoapAttribute GetSoapAttributeNoLock()
    {
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (SoapMethodAttribute), true);
      SoapAttribute soapAttribute = customAttributes == null || customAttributes.Length == 0 ? (SoapAttribute) new SoapMethodAttribute() : (SoapAttribute) customAttributes[0];
      soapAttribute.SetReflectInfo((object) this.RI);
      return soapAttribute;
    }

    internal string TypeAndAssemblyName
    {
      [SecurityCritical] get
      {
        if (this._typeAndAssemblyName == null)
          this.UpdateNames();
        return this._typeAndAssemblyName;
      }
    }

    internal string MethodName
    {
      [SecurityCritical] get
      {
        if (this._methodName == null)
          this.UpdateNames();
        return this._methodName;
      }
    }

    [SecurityCritical]
    private void UpdateNames()
    {
      MethodBase ri = this.RI;
      this._methodName = ri.Name;
      if (!(ri.DeclaringType != (Type) null))
        return;
      this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName((RuntimeType) ri.DeclaringType);
    }

    internal ParameterInfo[] Parameters
    {
      get
      {
        if (this._parameters == null)
          this._parameters = this.RI.GetParameters();
        return this._parameters;
      }
    }

    internal int[] OutRefArgMap
    {
      get
      {
        if (this._outRefArgMap == null)
          this.GetArgMaps();
        return this._outRefArgMap;
      }
    }

    internal int[] OutOnlyArgMap
    {
      get
      {
        if (this._outOnlyArgMap == null)
          this.GetArgMaps();
        return this._outOnlyArgMap;
      }
    }

    internal int[] NonRefOutArgMap
    {
      get
      {
        if (this._nonRefOutArgMap == null)
          this.GetArgMaps();
        return this._nonRefOutArgMap;
      }
    }

    internal int[] MarshalRequestArgMap
    {
      get
      {
        if (this._marshalRequestMap == null)
          this.GetArgMaps();
        return this._marshalRequestMap;
      }
    }

    internal int[] MarshalResponseArgMap
    {
      get
      {
        if (this._marshalResponseMap == null)
          this.GetArgMaps();
        return this._marshalResponseMap;
      }
    }

    private void GetArgMaps()
    {
      lock (this)
      {
        if (this._inRefArgMap != null)
          return;
        int[] inRefArgMap = (int[]) null;
        int[] outRefArgMap = (int[]) null;
        int[] outOnlyArgMap = (int[]) null;
        int[] nonRefOutArgMap = (int[]) null;
        int[] marshalRequestMap = (int[]) null;
        int[] marshalResponseMap = (int[]) null;
        ArgMapper.GetParameterMaps(this.Parameters, out inRefArgMap, out outRefArgMap, out outOnlyArgMap, out nonRefOutArgMap, out marshalRequestMap, out marshalResponseMap);
        this._inRefArgMap = inRefArgMap;
        this._outRefArgMap = outRefArgMap;
        this._outOnlyArgMap = outOnlyArgMap;
        this._nonRefOutArgMap = nonRefOutArgMap;
        this._marshalRequestMap = marshalRequestMap;
        this._marshalResponseMap = marshalResponseMap;
      }
    }

    internal bool IsOneWayMethod()
    {
      if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) != RemotingMethodCachedData.MethodCacheFlags.None)
        return (uint) (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > 0U;
      RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (OneWayAttribute), true);
      if (customAttributes != null && customAttributes.Length != 0)
        methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
      this.flags |= methodCacheFlags;
      return (uint) (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > 0U;
    }

    internal bool IsOverloaded()
    {
      if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
      {
        RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
        MethodBase ri = this.RI;
        RuntimeMethodInfo runtimeMethodInfo;
        if ((MethodInfo) (runtimeMethodInfo = ri as RuntimeMethodInfo) != (MethodInfo) null)
        {
          if (runtimeMethodInfo.IsOverloaded)
            methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
        }
        else
        {
          RuntimeConstructorInfo runtimeConstructorInfo;
          if (!((ConstructorInfo) (runtimeConstructorInfo = ri as RuntimeConstructorInfo) != (ConstructorInfo) null))
            throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_Method"));
          if (runtimeConstructorInfo.IsOverloaded)
            methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
        }
        this.flags |= methodCacheFlags;
      }
      return (uint) (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) > 0U;
    }

    internal Type ReturnType
    {
      get
      {
        if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
        {
          MethodInfo ri = this.RI as MethodInfo;
          if (ri != (MethodInfo) null)
          {
            Type returnType = ri.ReturnType;
            if (returnType != typeof (void))
              this._returnType = returnType;
          }
          this.flags |= RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
        }
        return this._returnType;
      }
    }

    [Flags]
    [Serializable]
    private enum MethodCacheFlags
    {
      None = 0,
      CheckedOneWay = 1,
      IsOneWay = 2,
      CheckedOverloaded = 4,
      IsOverloaded = 8,
      CheckedForAsync = 16, // 0x00000010
      CheckedForReturnType = 32, // 0x00000020
    }
  }
}
