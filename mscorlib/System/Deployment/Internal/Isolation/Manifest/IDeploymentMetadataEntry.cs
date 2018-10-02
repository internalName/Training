// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IDeploymentMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CFA3F59F-334D-46bf-A5A5-5D11BB2D7EBC")]
  [ComImport]
  internal interface IDeploymentMetadataEntry
  {
    DeploymentMetadataEntry AllData { [SecurityCritical] get; }

    string DeploymentProviderCodebase { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string MinimumRequiredVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    ushort MaximumAge { [SecurityCritical] get; }

    byte MaximumAge_Unit { [SecurityCritical] get; }

    uint DeploymentFlags { [SecurityCritical] get; }
  }
}
