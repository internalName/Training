// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IAssemblyReferenceDependentAssemblyEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C31FF59E-CD25-47b8-9EF3-CF4433EB97CC")]
  [ComImport]
  internal interface IAssemblyReferenceDependentAssemblyEntry
  {
    AssemblyReferenceDependentAssemblyEntry AllData { [SecurityCritical] get; }

    string Group { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Codebase { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    ulong Size { [SecurityCritical] get; }

    object HashValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

    uint HashAlgorithm { [SecurityCritical] get; }

    uint Flags { [SecurityCritical] get; }

    string ResourceFallbackCulture { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    ISection HashElements { [SecurityCritical] get; }
  }
}
