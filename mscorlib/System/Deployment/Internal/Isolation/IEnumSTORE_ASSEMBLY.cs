// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_ASSEMBLY
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("a5c637bf-6eaa-4e5f-b535-55299657e33e")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_ASSEMBLY
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] STORE_ASSEMBLY[] rgelt);

    [SecurityCritical]
    void Skip([In] uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_ASSEMBLY Clone();
  }
}
