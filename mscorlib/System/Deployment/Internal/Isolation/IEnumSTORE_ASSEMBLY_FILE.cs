// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_ASSEMBLY_FILE
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("a5c6aaa3-03e4-478d-b9f5-2e45908d5e4f")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_ASSEMBLY_FILE
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] STORE_ASSEMBLY_FILE[] rgelt);

    [SecurityCritical]
    void Skip([In] uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_ASSEMBLY_FILE Clone();
  }
}
