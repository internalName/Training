// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IAssemblyRequestEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("2474ECB4-8EFD-4410-9F31-B3E7C4A07731")]
  [ComImport]
  internal interface IAssemblyRequestEntry
  {
    AssemblyRequestEntry AllData { [SecurityCritical] get; }

    string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string permissionSetID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
  }
}
