// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.DependentOSMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class DependentOSMetadataEntry
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SupportUrl;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Description;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public ushort BuildNumber;
    public byte ServicePackMajor;
    public byte ServicePackMinor;
  }
}
