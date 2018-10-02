// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.CallBackHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  [Serializable]
  internal class CallBackHelper
  {
    internal const int RequestedFromEE = 1;
    internal const int XDomainTransition = 256;
    private int _flags;
    private IntPtr _privateData;

    internal bool IsEERequested
    {
      get
      {
        return (this._flags & 1) == 1;
      }
      set
      {
        if (!value)
          return;
        this._flags |= 1;
      }
    }

    internal bool IsCrossDomain
    {
      set
      {
        if (!value)
          return;
        this._flags |= 256;
      }
    }

    internal CallBackHelper(IntPtr privateData, bool bFromEE, int targetDomainID)
    {
      this.IsEERequested = bFromEE;
      this.IsCrossDomain = (uint) targetDomainID > 0U;
      this._privateData = privateData;
    }

    [SecurityCritical]
    internal void Func()
    {
      if (!this.IsEERequested)
        return;
      Context.ExecuteCallBackInEE(this._privateData);
    }
  }
}
