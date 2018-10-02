// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.CmsUtils
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.Hosting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [SecuritySafeCritical]
  [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
  internal static class CmsUtils
  {
    internal static void GetEntryPoint(ActivationContext activationContext, out string fileName, out string parameters)
    {
      parameters = (string) null;
      fileName = (string) null;
      ICMS componentManifest = activationContext.ApplicationComponentManifest;
      if (componentManifest == null || componentManifest.EntryPointSection == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
      IEnumUnknown newEnum = (IEnumUnknown) componentManifest.EntryPointSection._NewEnum;
      uint celtFetched = 0;
      object[] rgelt = new object[1];
      if (newEnum.Next(1U, rgelt, ref celtFetched) != 0 || celtFetched != 1U)
        return;
      EntryPointEntry allData = ((IEntryPointEntry) rgelt[0]).AllData;
      if (allData.CommandLine_File != null && allData.CommandLine_File.Length > 0)
      {
        fileName = allData.CommandLine_File;
      }
      else
      {
        object ppUnknown = (object) null;
        if (allData.Identity != null)
        {
          ((ISectionWithReferenceIdentityKey) componentManifest.AssemblyReferenceSection).Lookup(allData.Identity, out ppUnknown);
          IAssemblyReferenceEntry assemblyReferenceEntry = (IAssemblyReferenceEntry) ppUnknown;
          fileName = assemblyReferenceEntry.DependentAssembly.Codebase;
        }
      }
      parameters = allData.CommandLine_Parameters;
    }

    internal static IAssemblyReferenceEntry[] GetDependentAssemblies(ActivationContext activationContext)
    {
      IAssemblyReferenceEntry[] assemblyReferenceEntryArray = (IAssemblyReferenceEntry[]) null;
      ICMS componentManifest = activationContext.ApplicationComponentManifest;
      if (componentManifest == null)
        return (IAssemblyReferenceEntry[]) null;
      ISection referenceSection = componentManifest.AssemblyReferenceSection;
      uint celt = referenceSection != null ? referenceSection.Count : 0U;
      if (celt > 0U)
      {
        uint celtFetched = 0;
        assemblyReferenceEntryArray = new IAssemblyReferenceEntry[(int) celt];
        int num = ((IEnumUnknown) referenceSection._NewEnum).Next(celt, (object[]) assemblyReferenceEntryArray, ref celtFetched);
        if ((int) celtFetched != (int) celt || num < 0)
          return (IAssemblyReferenceEntry[]) null;
      }
      return assemblyReferenceEntryArray;
    }

    internal static string GetEntryPointFullPath(ActivationArguments activationArguments)
    {
      return CmsUtils.GetEntryPointFullPath(activationArguments.ActivationContext);
    }

    internal static string GetEntryPointFullPath(ActivationContext activationContext)
    {
      string fileName;
      string parameters;
      CmsUtils.GetEntryPoint(activationContext, out fileName, out parameters);
      if (!string.IsNullOrEmpty(fileName))
      {
        string path1 = activationContext.ApplicationDirectory;
        if (path1 == null || path1.Length == 0)
          path1 = Directory.UnsafeGetCurrentDirectory();
        fileName = Path.Combine(path1, fileName);
      }
      return fileName;
    }

    internal static bool CompareIdentities(ActivationContext activationContext1, ActivationContext activationContext2)
    {
      if (activationContext1 == null || activationContext2 == null)
        return activationContext1 == activationContext2;
      return IsolationInterop.AppIdAuthority.AreDefinitionsEqual(0U, activationContext1.Identity.Identity, activationContext2.Identity.Identity);
    }

    internal static bool CompareIdentities(ApplicationIdentity applicationIdentity1, ApplicationIdentity applicationIdentity2, ApplicationVersionMatch versionMatch)
    {
      if (applicationIdentity1 == null || applicationIdentity2 == null)
        return applicationIdentity1 == applicationIdentity2;
      uint Flags;
      switch (versionMatch)
      {
        case ApplicationVersionMatch.MatchExactVersion:
          Flags = 0U;
          break;
        case ApplicationVersionMatch.MatchAllVersions:
          Flags = 1U;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) versionMatch), nameof (versionMatch));
      }
      return IsolationInterop.AppIdAuthority.AreDefinitionsEqual(Flags, applicationIdentity1.Identity, applicationIdentity2.Identity);
    }

    internal static string GetFriendlyName(ActivationContext activationContext)
    {
      IDescriptionMetadataEntry descriptionData = ((IMetadataSectionEntry) activationContext.DeploymentComponentManifest.MetadataSectionEntry).DescriptionData;
      string str = string.Empty;
      if (descriptionData != null)
      {
        DescriptionMetadataEntry allData = descriptionData.AllData;
        str = allData.Publisher != null ? string.Format("{0} {1}", (object) allData.Publisher, (object) allData.Product) : allData.Product;
      }
      return str;
    }

    internal static void CreateActivationContext(string fullName, string[] manifestPaths, bool useFusionActivationContext, out ApplicationIdentity applicationIdentity, out ActivationContext activationContext)
    {
      applicationIdentity = new ApplicationIdentity(fullName);
      activationContext = (ActivationContext) null;
      if (!useFusionActivationContext)
        return;
      if (manifestPaths != null)
        activationContext = new ActivationContext(applicationIdentity, manifestPaths);
      else
        activationContext = new ActivationContext(applicationIdentity);
    }

    internal static Evidence MergeApplicationEvidence(Evidence evidence, ApplicationIdentity applicationIdentity, ActivationContext activationContext, string[] activationData)
    {
      return CmsUtils.MergeApplicationEvidence(evidence, applicationIdentity, activationContext, activationData, (ApplicationTrust) null);
    }

    internal static Evidence MergeApplicationEvidence(Evidence evidence, ApplicationIdentity applicationIdentity, ActivationContext activationContext, string[] activationData, ApplicationTrust applicationTrust)
    {
      Evidence evidence1 = new Evidence();
      ActivationArguments evidence2 = activationContext == null ? new ActivationArguments(applicationIdentity, activationData) : new ActivationArguments(activationContext, activationData);
      Evidence evidence3 = new Evidence();
      evidence3.AddHostEvidence<ActivationArguments>(evidence2);
      if (applicationTrust != null)
        evidence3.AddHostEvidence<ApplicationTrust>(applicationTrust);
      if (activationContext != null)
      {
        Evidence applicationEvidence = new ApplicationSecurityInfo(activationContext).ApplicationEvidence;
        if (applicationEvidence != null)
          evidence3.MergeWithNoDuplicates(applicationEvidence);
      }
      if (evidence != null)
        evidence3.MergeWithNoDuplicates(evidence);
      return evidence3;
    }
  }
}
