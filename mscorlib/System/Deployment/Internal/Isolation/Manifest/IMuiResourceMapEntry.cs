// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMuiResourceMapEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
  [ComImport]
  internal interface IMuiResourceMapEntry
  {
    MuiResourceMapEntry AllData { [SecurityCritical] get; }

    object ResourceTypeIdInt { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

    object ResourceTypeIdString { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
