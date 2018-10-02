// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingParameterCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Remoting.Metadata
{
  internal class RemotingParameterCachedData : RemotingCachedData
  {
    private RuntimeParameterInfo RI;

    internal RemotingParameterCachedData(RuntimeParameterInfo ri)
    {
      this.RI = ri;
    }

    internal override SoapAttribute GetSoapAttributeNoLock()
    {
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (SoapParameterAttribute), true);
      SoapAttribute soapAttribute = customAttributes == null || customAttributes.Length == 0 ? (SoapAttribute) new SoapParameterAttribute() : (SoapAttribute) customAttributes[0];
      soapAttribute.SetReflectInfo((object) this.RI);
      return soapAttribute;
    }
  }
}
