// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.AppDomainEvidenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;

namespace System.Security.Policy
{
  internal sealed class AppDomainEvidenceFactory : IRuntimeEvidenceFactory
  {
    private AppDomain m_targetDomain;
    private Evidence m_entryPointEvidence;

    internal AppDomainEvidenceFactory(AppDomain target)
    {
      this.m_targetDomain = target;
    }

    public IEvidenceFactory Target
    {
      get
      {
        return (IEvidenceFactory) this.m_targetDomain;
      }
    }

    public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
    {
      return (IEnumerable<EvidenceBase>) new EvidenceBase[0];
    }

    [SecuritySafeCritical]
    public EvidenceBase GenerateEvidence(Type evidenceType)
    {
      if (!this.m_targetDomain.IsDefaultAppDomain())
        return AppDomain.GetDefaultDomain().GetHostEvidence(evidenceType);
      if (this.m_entryPointEvidence == null)
      {
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        RuntimeAssembly runtimeAssembly = entryAssembly as RuntimeAssembly;
        if ((Assembly) runtimeAssembly != (Assembly) null)
          this.m_entryPointEvidence = runtimeAssembly.EvidenceNoDemand.Clone();
        else if (entryAssembly != (Assembly) null)
          this.m_entryPointEvidence = entryAssembly.Evidence;
      }
      if (this.m_entryPointEvidence != null)
        return this.m_entryPointEvidence.GetHostEvidence(evidenceType);
      return (EvidenceBase) null;
    }
  }
}
