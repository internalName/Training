// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PEFileEvidenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
  internal sealed class PEFileEvidenceFactory : IRuntimeEvidenceFactory
  {
    [SecurityCritical]
    private SafePEFileHandle m_peFile;
    private List<EvidenceBase> m_assemblyProvidedEvidence;
    private bool m_generatedLocationEvidence;
    private Site m_siteEvidence;
    private Url m_urlEvidence;
    private Zone m_zoneEvidence;

    [SecurityCritical]
    private PEFileEvidenceFactory(SafePEFileHandle peFile)
    {
      this.m_peFile = peFile;
    }

    internal SafePEFileHandle PEFile
    {
      [SecurityCritical] get
      {
        return this.m_peFile;
      }
    }

    public IEvidenceFactory Target
    {
      get
      {
        return (IEvidenceFactory) null;
      }
    }

    [SecurityCritical]
    private static Evidence CreateSecurityIdentity(SafePEFileHandle peFile, Evidence hostProvidedEvidence)
    {
      Evidence evidence = new Evidence((IRuntimeEvidenceFactory) new PEFileEvidenceFactory(peFile));
      if (hostProvidedEvidence != null)
        evidence.MergeWithNoDuplicates(hostProvidedEvidence);
      return evidence;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void FireEvidenceGeneratedEvent(SafePEFileHandle peFile, EvidenceTypeGenerated type);

    [SecuritySafeCritical]
    internal void FireEvidenceGeneratedEvent(EvidenceTypeGenerated type)
    {
      PEFileEvidenceFactory.FireEvidenceGeneratedEvent(this.m_peFile, type);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAssemblySuppliedEvidence(SafePEFileHandle peFile, ObjectHandleOnStack retSerializedEvidence);

    [SecuritySafeCritical]
    public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
    {
      if (this.m_assemblyProvidedEvidence == null)
      {
        byte[] o = (byte[]) null;
        PEFileEvidenceFactory.GetAssemblySuppliedEvidence(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
        this.m_assemblyProvidedEvidence = new List<EvidenceBase>();
        if (o != null)
        {
          Evidence evidence = new Evidence();
          new SecurityPermission(SecurityPermissionFlag.SerializationFormatter).Assert();
          try
          {
            using (MemoryStream memoryStream = new MemoryStream(o))
              evidence = (Evidence) new BinaryFormatter().Deserialize((Stream) memoryStream);
          }
          catch
          {
          }
          CodeAccessPermission.RevertAssert();
          if (evidence != null)
          {
            IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
            while (assemblyEnumerator.MoveNext())
            {
              if (assemblyEnumerator.Current != null)
                this.m_assemblyProvidedEvidence.Add(assemblyEnumerator.Current as EvidenceBase ?? (EvidenceBase) new LegacyEvidenceWrapper(assemblyEnumerator.Current));
            }
          }
        }
      }
      return (IEnumerable<EvidenceBase>) this.m_assemblyProvidedEvidence;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetLocationEvidence(SafePEFileHandle peFile, out SecurityZone zone, StringHandleOnStack retUrl);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetPublisherCertificate(SafePEFileHandle peFile, ObjectHandleOnStack retCertificate);

    public EvidenceBase GenerateEvidence(Type evidenceType)
    {
      if (evidenceType == typeof (Site))
        return (EvidenceBase) this.GenerateSiteEvidence();
      if (evidenceType == typeof (Url))
        return (EvidenceBase) this.GenerateUrlEvidence();
      if (evidenceType == typeof (Zone))
        return (EvidenceBase) this.GenerateZoneEvidence();
      if (evidenceType == typeof (Publisher))
        return (EvidenceBase) this.GeneratePublisherEvidence();
      return (EvidenceBase) null;
    }

    [SecuritySafeCritical]
    private void GenerateLocationEvidence()
    {
      if (this.m_generatedLocationEvidence)
        return;
      SecurityZone zone = SecurityZone.NoZone;
      string s = (string) null;
      PEFileEvidenceFactory.GetLocationEvidence(this.m_peFile, out zone, JitHelpers.GetStringHandleOnStack(ref s));
      if (zone != SecurityZone.NoZone)
        this.m_zoneEvidence = new Zone(zone);
      if (!string.IsNullOrEmpty(s))
      {
        this.m_urlEvidence = new Url(s, true);
        if (!s.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
          this.m_siteEvidence = Site.CreateFromUrl(s);
      }
      this.m_generatedLocationEvidence = true;
    }

    [SecuritySafeCritical]
    private Publisher GeneratePublisherEvidence()
    {
      byte[] o = (byte[]) null;
      PEFileEvidenceFactory.GetPublisherCertificate(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      if (o == null)
        return (Publisher) null;
      return new Publisher(new X509Certificate(o));
    }

    private Site GenerateSiteEvidence()
    {
      if (this.m_siteEvidence == null)
        this.GenerateLocationEvidence();
      return this.m_siteEvidence;
    }

    private Url GenerateUrlEvidence()
    {
      if (this.m_urlEvidence == null)
        this.GenerateLocationEvidence();
      return this.m_urlEvidence;
    }

    private Zone GenerateZoneEvidence()
    {
      if (this.m_zoneEvidence == null)
        this.GenerateLocationEvidence();
      return this.m_zoneEvidence;
    }
  }
}
