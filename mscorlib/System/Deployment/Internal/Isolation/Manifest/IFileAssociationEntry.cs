// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IFileAssociationEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0C66F299-E08E-48c5-9264-7CCBEB4D5CBB")]
  [ComImport]
  internal interface IFileAssociationEntry
  {
    FileAssociationEntry AllData { [SecurityCritical] get; }

    string Extension { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string ProgID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string DefaultIcon { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    string Parameter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
  }
}
