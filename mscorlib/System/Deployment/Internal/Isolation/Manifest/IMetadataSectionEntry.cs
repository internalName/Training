﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMetadataSectionEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("AB1ED79F-943E-407d-A80B-0744E3A95B28")]
  [ComImport]
  internal interface IMetadataSectionEntry
  {
    MetadataSectionEntry AllData { [SecurityCritical] get; }

    uint SchemaVersion { [SecurityCritical] get; }

    uint ManifestFlags { [SecurityCritical] get; }

    uint UsagePatterns { [SecurityCritical] get; }

    IDefinitionIdentity CdfIdentity { [SecurityCritical] get; }

    string LocalPath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    uint HashAlgorithm { [SecurityCritical] get; }

    object ManifestHash { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

    string ContentType { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string RuntimeImageVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    object MvidValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

    IDescriptionMetadataEntry DescriptionData { [SecurityCritical] get; }

    IDeploymentMetadataEntry DeploymentData { [SecurityCritical] get; }

    IDependentOSMetadataEntry DependentOSData { [SecurityCritical] get; }

    string defaultPermissionSetID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string RequestedExecutionLevel { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    bool RequestedExecutionLevelUIAccess { [SecurityCritical] get; }

    IReferenceIdentity ResourceTypeResourcesDependency { [SecurityCritical] get; }

    IReferenceIdentity ResourceTypeManifestResourcesDependency { [SecurityCritical] get; }

    string KeyInfoElement { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    ICompatibleFrameworksMetadataEntry CompatibleFrameworksData { [SecurityCritical] get; }
  }
}
