// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IDescriptionMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
  [ComImport]
  internal interface IDescriptionMetadataEntry
  {
    DescriptionMetadataEntry AllData { [SecurityCritical] get; }

    string Publisher { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Product { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string IconFile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string ErrorReportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string SuiteName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
  }
}
