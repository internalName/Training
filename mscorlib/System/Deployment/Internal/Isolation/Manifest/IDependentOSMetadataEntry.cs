// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IDependentOSMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CF168CF4-4E8F-4d92-9D2A-60E5CA21CF85")]
  [ComImport]
  internal interface IDependentOSMetadataEntry
  {
    DependentOSMetadataEntry AllData { [SecurityCritical] get; }

    string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    ushort MajorVersion { [SecurityCritical] get; }

    ushort MinorVersion { [SecurityCritical] get; }

    ushort BuildNumber { [SecurityCritical] get; }

    byte ServicePackMajor { [SecurityCritical] get; }

    byte ServicePackMinor { [SecurityCritical] get; }
  }
}
