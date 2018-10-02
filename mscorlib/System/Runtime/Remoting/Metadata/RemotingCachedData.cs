// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Metadata
{
  internal abstract class RemotingCachedData
  {
    private SoapAttribute _soapAttr;

    internal SoapAttribute GetSoapAttribute()
    {
      if (this._soapAttr == null)
      {
        lock (this)
        {
          if (this._soapAttr == null)
            this._soapAttr = this.GetSoapAttributeNoLock();
        }
      }
      return this._soapAttr;
    }

    internal abstract SoapAttribute GetSoapAttributeNoLock();
  }
}
