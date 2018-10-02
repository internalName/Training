// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMuiResourceTypeIdStringEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("11df5cad-c183-479b-9a44-3842b71639ce")]
  [ComImport]
  internal interface IMuiResourceTypeIdStringEntry
  {
    MuiResourceTypeIdStringEntry AllData { [SecurityCritical] get; }

    object StringIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

    object IntegerIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
