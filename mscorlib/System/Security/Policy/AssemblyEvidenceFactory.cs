// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.AssemblyEvidenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
  internal sealed class AssemblyEvidenceFactory : IRuntimeEvidenceFactory
  {
    private PEFileEvidenceFactory m_peFileFactory;
    private RuntimeAssembly m_targetAssembly;

    private AssemblyEvidenceFactory(RuntimeAssembly targetAssembly, PEFileEvidenceFactory peFileFactory)
    {
      this.m_targetAssembly = targetAssembly;
      this.m_peFileFactory = peFileFactory;
    }

    internal SafePEFileHandle PEFile
    {
      [SecurityCritical] get
      {
        return this.m_peFileFactory.PEFile;
      }
    }

    public IEvidenceFactory Target
    {
      get
      {
        return (IEvidenceFactory) this.m_targetAssembly;
      }
    }

    public EvidenceBase GenerateEvidence(Type evidenceType)
    {
      EvidenceBase evidence = this.m_peFileFactory.GenerateEvidence(evidenceType);
      if (evidence != null)
        return evidence;
      if (evidenceType == typeof (GacInstalled))
        return (EvidenceBase) this.GenerateGacEvidence();
      if (evidenceType == typeof (Hash))
        return (EvidenceBase) this.GenerateHashEvidence();
      if (evidenceType == typeof (PermissionRequestEvidence))
        return (EvidenceBase) this.GeneratePermissionRequestEvidence();
      if (evidenceType == typeof (StrongName))
        return (EvidenceBase) this.GenerateStrongNameEvidence();
      return (EvidenceBase) null;
    }

    private GacInstalled GenerateGacEvidence()
    {
      if (!this.m_targetAssembly.GlobalAssemblyCache)
        return (GacInstalled) null;
      this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Gac);
      return new GacInstalled();
    }

    private Hash GenerateHashEvidence()
    {
      if (this.m_targetAssembly.IsDynamic)
        return (Hash) null;
      this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Hash);
      return new Hash((Assembly) this.m_targetAssembly);
    }

    [SecuritySafeCritical]
    private PermissionRequestEvidence GeneratePermissionRequestEvidence()
    {
      PermissionSet o1 = (PermissionSet) null;
      PermissionSet o2 = (PermissionSet) null;
      PermissionSet o3 = (PermissionSet) null;
      AssemblyEvidenceFactory.GetAssemblyPermissionRequests(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o1), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o2), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o3));
      if (o1 != null || o2 != null || o3 != null)
        return new PermissionRequestEvidence(o1, o2, o3);
      return (PermissionRequestEvidence) null;
    }

    [SecuritySafeCritical]
    private StrongName GenerateStrongNameEvidence()
    {
      byte[] o = (byte[]) null;
      string s = (string) null;
      ushort majorVersion = 0;
      ushort minorVersion = 0;
      ushort build = 0;
      ushort revision = 0;
      AssemblyEvidenceFactory.GetStrongNameInformation(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref o), JitHelpers.GetStringHandleOnStack(ref s), out majorVersion, out minorVersion, out build, out revision);
      if (o == null || o.Length == 0)
        return (StrongName) null;
      return new StrongName(new StrongNamePublicKeyBlob(o), s, new Version((int) majorVersion, (int) minorVersion, (int) build, (int) revision), (Assembly) this.m_targetAssembly);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAssemblyPermissionRequests(RuntimeAssembly assembly, ObjectHandleOnStack retMinimumPermissions, ObjectHandleOnStack retOptionalPermissions, ObjectHandleOnStack retRefusedPermissions);

    public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
    {
      return this.m_peFileFactory.GetFactorySuppliedEvidence();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetStrongNameInformation(RuntimeAssembly assembly, ObjectHandleOnStack retPublicKeyBlob, StringHandleOnStack retSimpleName, out ushort majorVersion, out ushort minorVersion, out ushort build, out ushort revision);

    [SecurityCritical]
    private static Evidence UpgradeSecurityIdentity(Evidence peFileEvidence, RuntimeAssembly targetAssembly)
    {
      peFileEvidence.Target = (IRuntimeEvidenceFactory) new AssemblyEvidenceFactory(targetAssembly, peFileEvidence.Target as PEFileEvidenceFactory);
      HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
      if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
      {
        peFileEvidence = hostSecurityManager.ProvideAssemblyEvidence((Assembly) targetAssembly, peFileEvidence);
        if (peFileEvidence == null)
          throw new InvalidOperationException(Environment.GetResourceString("Policy_NullHostEvidence", (object) hostSecurityManager.GetType().FullName, (object) targetAssembly.FullName));
      }
      return peFileEvidence;
    }
  }
}
