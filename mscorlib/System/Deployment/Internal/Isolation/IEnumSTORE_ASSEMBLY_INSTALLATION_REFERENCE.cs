// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("d8b1aacb-5142-4abb-bcc1-e9dc9052a89e")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] StoreApplicationReference[] rgelt);

    [SecurityCritical]
    void Skip([In] uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE Clone();
  }
}
