// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  [Serializable]
  internal class CrossAppDomainData
  {
    private object _ContextID = (object) 0;
    private int _DomainID;
    private string _processGuid;

    internal virtual IntPtr ContextID
    {
      get
      {
        return new IntPtr((int) this._ContextID);
      }
    }

    internal virtual int DomainID
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._DomainID;
      }
    }

    internal virtual string ProcessGuid
    {
      get
      {
        return this._processGuid;
      }
    }

    internal CrossAppDomainData(IntPtr ctxId, int domainID, string processGuid)
    {
      this._DomainID = domainID;
      this._processGuid = processGuid;
      this._ContextID = (object) ctxId.ToInt32();
    }

    internal bool IsFromThisProcess()
    {
      return Identity.ProcessGuid.Equals(this._processGuid);
    }

    [SecurityCritical]
    internal bool IsFromThisAppDomain()
    {
      if (this.IsFromThisProcess())
        return Thread.GetDomain().GetId() == this._DomainID;
      return false;
    }
  }
}
