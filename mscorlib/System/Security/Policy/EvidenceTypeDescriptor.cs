// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.EvidenceTypeDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Policy
{
  [Serializable]
  internal sealed class EvidenceTypeDescriptor
  {
    [NonSerialized]
    private bool m_hostCanGenerate;
    [NonSerialized]
    private bool m_generated;
    private EvidenceBase m_hostEvidence;
    private EvidenceBase m_assemblyEvidence;

    public EvidenceTypeDescriptor()
    {
    }

    private EvidenceTypeDescriptor(EvidenceTypeDescriptor descriptor)
    {
      this.m_hostCanGenerate = descriptor.m_hostCanGenerate;
      if (descriptor.m_assemblyEvidence != null)
        this.m_assemblyEvidence = descriptor.m_assemblyEvidence.Clone();
      if (descriptor.m_hostEvidence == null)
        return;
      this.m_hostEvidence = descriptor.m_hostEvidence.Clone();
    }

    public EvidenceBase AssemblyEvidence
    {
      get
      {
        return this.m_assemblyEvidence;
      }
      set
      {
        this.m_assemblyEvidence = value;
      }
    }

    public bool Generated
    {
      get
      {
        return this.m_generated;
      }
      set
      {
        this.m_generated = value;
      }
    }

    public bool HostCanGenerate
    {
      get
      {
        return this.m_hostCanGenerate;
      }
      set
      {
        this.m_hostCanGenerate = value;
      }
    }

    public EvidenceBase HostEvidence
    {
      get
      {
        return this.m_hostEvidence;
      }
      set
      {
        this.m_hostEvidence = value;
      }
    }

    public EvidenceTypeDescriptor Clone()
    {
      return new EvidenceTypeDescriptor(this);
    }
  }
}
