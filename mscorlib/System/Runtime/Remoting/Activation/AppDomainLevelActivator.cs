// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.AppDomainLevelActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  [Serializable]
  internal class AppDomainLevelActivator : IActivator
  {
    private IActivator m_NextActivator;
    private string m_RemActivatorURL;

    internal AppDomainLevelActivator(string remActivatorURL)
    {
      this.m_RemActivatorURL = remActivatorURL;
    }

    internal AppDomainLevelActivator(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_NextActivator = (IActivator) info.GetValue(nameof (m_NextActivator), typeof (IActivator));
    }

    public virtual IActivator NextActivator
    {
      [SecurityCritical] get
      {
        return this.m_NextActivator;
      }
      [SecurityCritical] set
      {
        this.m_NextActivator = value;
      }
    }

    public virtual ActivatorLevel Level
    {
      [SecurityCritical] get
      {
        return ActivatorLevel.AppDomain;
      }
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
    {
      ctorMsg.Activator = this.m_NextActivator;
      return ActivationServices.GetActivator().Activate(ctorMsg);
    }
  }
}
