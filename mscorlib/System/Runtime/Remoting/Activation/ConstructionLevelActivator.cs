// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ConstructionLevelActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  [Serializable]
  internal class ConstructionLevelActivator : IActivator
  {
    internal ConstructionLevelActivator()
    {
    }

    public virtual IActivator NextActivator
    {
      [SecurityCritical] get
      {
        return (IActivator) null;
      }
      [SecurityCritical] set
      {
        throw new InvalidOperationException();
      }
    }

    public virtual ActivatorLevel Level
    {
      [SecurityCritical] get
      {
        return ActivatorLevel.Construction;
      }
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
    {
      ctorMsg.Activator = ctorMsg.Activator.NextActivator;
      return ActivationServices.DoServerContextActivation(ctorMsg);
    }
  }
}
