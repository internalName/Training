// Decompiled with JetBrains decompiler
// Type: System.Security.PolicyManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
  internal class PolicyManager
  {
    private object m_policyLevels;
    private static volatile QuickCacheEntryType[] FullTrustMap;

    private IList PolicyLevels
    {
      [SecurityCritical] get
      {
        if (this.m_policyLevels == null)
        {
          ArrayList arrayList = new ArrayList();
          string locationFromType1 = PolicyLevel.GetLocationFromType(PolicyLevelType.Enterprise);
          arrayList.Add((object) new PolicyLevel(PolicyLevelType.Enterprise, locationFromType1, ConfigId.EnterprisePolicyLevel));
          string locationFromType2 = PolicyLevel.GetLocationFromType(PolicyLevelType.Machine);
          arrayList.Add((object) new PolicyLevel(PolicyLevelType.Machine, locationFromType2, ConfigId.MachinePolicyLevel));
          if (Config.UserDirectory != null)
          {
            string locationFromType3 = PolicyLevel.GetLocationFromType(PolicyLevelType.User);
            arrayList.Add((object) new PolicyLevel(PolicyLevelType.User, locationFromType3, ConfigId.UserPolicyLevel));
          }
          Interlocked.CompareExchange(ref this.m_policyLevels, (object) arrayList, (object) null);
        }
        return (IList) (this.m_policyLevels as ArrayList);
      }
    }

    internal PolicyManager()
    {
    }

    [SecurityCritical]
    internal void AddLevel(PolicyLevel level)
    {
      this.PolicyLevels.Add((object) level);
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    internal IEnumerator PolicyHierarchy()
    {
      return this.PolicyLevels.GetEnumerator();
    }

    [SecurityCritical]
    internal PermissionSet Resolve(Evidence evidence)
    {
      PermissionSet grantSet = (PermissionSet) null;
      if (CodeAccessSecurityEngine.TryResolveGrantSet(evidence, out grantSet))
        return grantSet;
      return this.CodeGroupResolve(evidence, false);
    }

    [SecurityCritical]
    internal PermissionSet CodeGroupResolve(Evidence evidence, bool systemPolicy)
    {
      PermissionSet permissionSet = (PermissionSet) null;
      IEnumerator enumerator = this.PolicyLevels.GetEnumerator();
      evidence.GetHostEvidence<Zone>();
      evidence.GetHostEvidence<StrongName>();
      evidence.GetHostEvidence<Url>();
      byte[] serializedEvidence = evidence.RawSerialize();
      int rawCount = evidence.RawCount;
      bool flag1 = AppDomain.CurrentDomain.GetData("IgnoreSystemPolicy") != null;
      bool flag2 = false;
      while (enumerator.MoveNext())
      {
        PolicyLevel current = (PolicyLevel) enumerator.Current;
        if (systemPolicy)
        {
          if (current.Type == PolicyLevelType.AppDomain)
            continue;
        }
        else if (flag1 && current.Type != PolicyLevelType.AppDomain)
          continue;
        PolicyStatement policyStatement = current.Resolve(evidence, rawCount, serializedEvidence);
        if (permissionSet == null)
          permissionSet = policyStatement.PermissionSet;
        else
          permissionSet.InplaceIntersect(policyStatement.GetPermissionSetNoCopy());
        if (permissionSet != null && !permissionSet.FastIsEmpty())
        {
          if ((policyStatement.Attributes & PolicyStatementAttribute.LevelFinal) == PolicyStatementAttribute.LevelFinal)
          {
            if (current.Type != PolicyLevelType.AppDomain)
            {
              flag2 = true;
              break;
            }
            break;
          }
        }
        else
          break;
      }
      if (permissionSet != null & flag2)
      {
        PolicyLevel policyLevel1 = (PolicyLevel) null;
        for (int index = this.PolicyLevels.Count - 1; index >= 0; --index)
        {
          PolicyLevel policyLevel2 = (PolicyLevel) this.PolicyLevels[index];
          if (policyLevel2.Type == PolicyLevelType.AppDomain)
          {
            policyLevel1 = policyLevel2;
            break;
          }
        }
        if (policyLevel1 != null)
        {
          PolicyStatement policyStatement = policyLevel1.Resolve(evidence, rawCount, serializedEvidence);
          permissionSet.InplaceIntersect(policyStatement.GetPermissionSetNoCopy());
        }
      }
      if (permissionSet == null)
        permissionSet = new PermissionSet(PermissionState.None);
      if (!permissionSet.IsUnrestricted())
      {
        IEnumerator hostEnumerator = evidence.GetHostEnumerator();
        while (hostEnumerator.MoveNext())
        {
          IIdentityPermissionFactory current = hostEnumerator.Current as IIdentityPermissionFactory;
          if (current != null)
          {
            IPermission identityPermission = current.CreateIdentityPermission(evidence);
            if (identityPermission != null)
              permissionSet.AddPermission(identityPermission);
          }
        }
      }
      permissionSet.IgnoreTypeLoadFailures = true;
      return permissionSet;
    }

    internal static bool IsGacAssembly(Evidence evidence)
    {
      return new GacMembershipCondition().Check(evidence);
    }

    [SecurityCritical]
    internal IEnumerator ResolveCodeGroups(Evidence evidence)
    {
      ArrayList arrayList = new ArrayList();
      foreach (PolicyLevel policyLevel in (IEnumerable) this.PolicyLevels)
      {
        CodeGroup codeGroup = policyLevel.ResolveMatchingCodeGroups(evidence);
        if (codeGroup != null)
          arrayList.Add((object) codeGroup);
      }
      return arrayList.GetEnumerator(0, arrayList.Count);
    }

    internal static PolicyStatement ResolveCodeGroup(CodeGroup codeGroup, Evidence evidence)
    {
      if (codeGroup.GetType().Assembly != typeof (UnionCodeGroup).Assembly)
        evidence.MarkAllEvidenceAsUsed();
      return codeGroup.Resolve(evidence);
    }

    internal static bool CheckMembershipCondition(IMembershipCondition membershipCondition, Evidence evidence, out object usedEvidence)
    {
      IReportMatchMembershipCondition membershipCondition1 = membershipCondition as IReportMatchMembershipCondition;
      if (membershipCondition1 != null)
        return membershipCondition1.Check(evidence, out usedEvidence);
      usedEvidence = (object) null;
      evidence.MarkAllEvidenceAsUsed();
      return membershipCondition.Check(evidence);
    }

    [SecurityCritical]
    internal void Save()
    {
      this.EncodeLevel(Environment.GetResourceString("Policy_PL_Enterprise"));
      this.EncodeLevel(Environment.GetResourceString("Policy_PL_Machine"));
      this.EncodeLevel(Environment.GetResourceString("Policy_PL_User"));
    }

    [SecurityCritical]
    private void EncodeLevel(string label)
    {
      for (int index = 0; index < this.PolicyLevels.Count; ++index)
      {
        PolicyLevel policyLevel = (PolicyLevel) this.PolicyLevels[index];
        if (policyLevel.Label.Equals(label))
        {
          PolicyManager.EncodeLevel(policyLevel);
          break;
        }
      }
    }

    [SecurityCritical]
    internal static void EncodeLevel(PolicyLevel level)
    {
      if (level.Path == null)
        throw new PolicyException(Environment.GetResourceString("Policy_UnableToSave", (object) level.Label, (object) Environment.GetResourceString("Policy_SaveNotFileBased")));
      SecurityElement securityElement1 = new SecurityElement("configuration");
      SecurityElement child1 = new SecurityElement("mscorlib");
      SecurityElement child2 = new SecurityElement("security");
      SecurityElement child3 = new SecurityElement("policy");
      securityElement1.AddChild(child1);
      child1.AddChild(child2);
      child2.AddChild(child3);
      child3.AddChild(level.ToXml());
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        Encoding utF8 = Encoding.UTF8;
        SecurityElement securityElement2 = new SecurityElement("xml");
        securityElement2.m_type = SecurityElementType.Format;
        securityElement2.AddAttribute("version", "1.0");
        securityElement2.AddAttribute("encoding", utF8.WebName);
        stringBuilder.Append(securityElement2.ToString());
        stringBuilder.Append(securityElement1.ToString());
        byte[] bytes = utF8.GetBytes(stringBuilder.ToString());
        Exception exceptionForHr = Marshal.GetExceptionForHR(Config.SaveDataByte(level.Path, bytes, bytes.Length));
        if (exceptionForHr != null)
        {
          string str = exceptionForHr != null ? exceptionForHr.Message : string.Empty;
          throw new PolicyException(Environment.GetResourceString("Policy_UnableToSave", (object) level.Label, (object) str), exceptionForHr);
        }
      }
      catch (Exception ex)
      {
        if (ex is PolicyException)
          throw ex;
        throw new PolicyException(Environment.GetResourceString("Policy_UnableToSave", (object) level.Label, (object) ex.Message), ex);
      }
      Config.ResetCacheData(level.ConfigId);
      if (!PolicyManager.CanUseQuickCache(level.RootCodeGroup))
        return;
      Config.SetQuickCache(level.ConfigId, PolicyManager.GenerateQuickCache(level));
    }

    internal static bool CanUseQuickCache(CodeGroup group)
    {
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) group);
      for (int index = 0; index < arrayList.Count; ++index)
      {
        group = (CodeGroup) arrayList[index];
        if (!(group is IUnionSemanticCodeGroup) || !PolicyManager.TestPolicyStatement(group.PolicyStatement))
          return false;
        IMembershipCondition membershipCondition = group.MembershipCondition;
        if (membershipCondition != null && !(membershipCondition is IConstantMembershipCondition))
          return false;
        IList children = group.Children;
        if (children != null && children.Count > 0)
        {
          foreach (object obj in (IEnumerable) children)
            arrayList.Add(obj);
        }
      }
      return true;
    }

    private static bool TestPolicyStatement(PolicyStatement policy)
    {
      if (policy == null)
        return true;
      return (policy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Nothing;
    }

    private static QuickCacheEntryType GenerateQuickCache(PolicyLevel level)
    {
      if (PolicyManager.FullTrustMap == null)
        PolicyManager.FullTrustMap = new QuickCacheEntryType[5]
        {
          QuickCacheEntryType.FullTrustZoneMyComputer,
          QuickCacheEntryType.FullTrustZoneIntranet,
          QuickCacheEntryType.FullTrustZoneTrusted,
          QuickCacheEntryType.FullTrustZoneInternet,
          QuickCacheEntryType.FullTrustZoneUntrusted
        };
      QuickCacheEntryType quickCacheEntryType = (QuickCacheEntryType) 0;
      Evidence evidence1 = new Evidence();
      try
      {
        if (level.Resolve(evidence1).PermissionSet.IsUnrestricted())
          quickCacheEntryType |= QuickCacheEntryType.FullTrustAll;
      }
      catch (PolicyException ex)
      {
      }
      foreach (SecurityZone zone in Enum.GetValues(typeof (SecurityZone)))
      {
        if (zone != SecurityZone.NoZone)
        {
          Evidence evidence2 = new Evidence();
          evidence2.AddHostEvidence<Zone>(new Zone(zone));
          try
          {
            if (level.Resolve(evidence2).PermissionSet.IsUnrestricted())
              quickCacheEntryType |= PolicyManager.FullTrustMap[(int) zone];
          }
          catch (PolicyException ex)
          {
          }
        }
      }
      return quickCacheEntryType;
    }
  }
}
