// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.DescriptionMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class DescriptionMetadataEntry
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Publisher;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Product;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SupportUrl;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string IconFile;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ErrorReportUrl;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SuiteName;
  }
}
