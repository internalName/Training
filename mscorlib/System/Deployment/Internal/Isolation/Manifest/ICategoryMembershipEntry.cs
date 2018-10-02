// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ICategoryMembershipEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("97FDCA77-B6F2-4718-A1EB-29D0AECE9C03")]
  [ComImport]
  internal interface ICategoryMembershipEntry
  {
    CategoryMembershipEntry AllData { [SecurityCritical] get; }

    IDefinitionIdentity Identity { [SecurityCritical] get; }

    ISection SubcategoryMembership { [SecurityCritical] get; }
  }
}
