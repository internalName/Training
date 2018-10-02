// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.MetadataSectionEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class MetadataSectionEntry : IDisposable
  {
    public uint SchemaVersion;
    public uint ManifestFlags;
    public uint UsagePatterns;
    public IDefinitionIdentity CdfIdentity;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string LocalPath;
    public uint HashAlgorithm;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr ManifestHash;
    public uint ManifestHashSize;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ContentType;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string RuntimeImageVersion;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr MvidValue;
    public uint MvidValueSize;
    public DescriptionMetadataEntry DescriptionData;
    public DeploymentMetadataEntry DeploymentData;
    public DependentOSMetadataEntry DependentOSData;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string defaultPermissionSetID;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string RequestedExecutionLevel;
    public bool RequestedExecutionLevelUIAccess;
    public IReferenceIdentity ResourceTypeResourcesDependency;
    public IReferenceIdentity ResourceTypeManifestResourcesDependency;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string KeyInfoElement;
    public CompatibleFrameworksMetadataEntry CompatibleFrameworksData;

    ~MetadataSectionEntry()
    {
      this.Dispose(false);
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
    }

    [SecuritySafeCritical]
    public void Dispose(bool fDisposing)
    {
      if (this.ManifestHash != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.ManifestHash);
        this.ManifestHash = IntPtr.Zero;
      }
      if (this.MvidValue != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.MvidValue);
        this.MvidValue = IntPtr.Zero;
      }
      if (!fDisposing)
        return;
      GC.SuppressFinalize((object) this);
    }
  }
}
