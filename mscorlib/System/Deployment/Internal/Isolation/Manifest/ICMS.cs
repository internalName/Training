// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ICMS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("a504e5b0-8ccf-4cb4-9902-c9d1b9abd033")]
  [ComImport]
  internal interface ICMS
  {
    IDefinitionIdentity Identity { [SecurityCritical] get; }

    ISection FileSection { [SecurityCritical] get; }

    ISection CategoryMembershipSection { [SecurityCritical] get; }

    ISection COMRedirectionSection { [SecurityCritical] get; }

    ISection ProgIdRedirectionSection { [SecurityCritical] get; }

    ISection CLRSurrogateSection { [SecurityCritical] get; }

    ISection AssemblyReferenceSection { [SecurityCritical] get; }

    ISection WindowClassSection { [SecurityCritical] get; }

    ISection StringSection { [SecurityCritical] get; }

    ISection EntryPointSection { [SecurityCritical] get; }

    ISection PermissionSetSection { [SecurityCritical] get; }

    ISectionEntry MetadataSectionEntry { [SecurityCritical] get; }

    ISection AssemblyRequestSection { [SecurityCritical] get; }

    ISection RegistryKeySection { [SecurityCritical] get; }

    ISection DirectorySection { [SecurityCritical] get; }

    ISection FileAssociationSection { [SecurityCritical] get; }

    ISection CompatibleFrameworksSection { [SecurityCritical] get; }

    ISection EventSection { [SecurityCritical] get; }

    ISection EventMapSection { [SecurityCritical] get; }

    ISection EventTagSection { [SecurityCritical] get; }

    ISection CounterSetSection { [SecurityCritical] get; }

    ISection CounterSection { [SecurityCritical] get; }
  }
}
