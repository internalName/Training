// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CallContextSecurityData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class CallContextSecurityData : ICloneable
  {
    private IPrincipal _principal;

    internal IPrincipal Principal
    {
      get
      {
        return this._principal;
      }
      set
      {
        this._principal = value;
      }
    }

    internal bool HasInfo
    {
      get
      {
        return this._principal != null;
      }
    }

    public object Clone()
    {
      return (object) new CallContextSecurityData()
      {
        _principal = this._principal
      };
    }
  }
}
