// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IProgIdRedirectionEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("54F198EC-A63A-45ea-A984-452F68D9B35B")]
  [ComImport]
  internal interface IProgIdRedirectionEntry
  {
    ProgIdRedirectionEntry AllData { [SecurityCritical] get; }

    string ProgId { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    Guid RedirectedGuid { [SecurityCritical] get; }
  }
}
