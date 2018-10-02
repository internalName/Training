// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IResourceTableMappingEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("70A4ECEE-B195-4c59-85BF-44B6ACA83F07")]
  [ComImport]
  internal interface IResourceTableMappingEntry
  {
    ResourceTableMappingEntry AllData { [SecurityCritical] get; }

    string id { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string FinalStringMapped { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
  }
}
