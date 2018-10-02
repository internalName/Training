// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_CATEGORY_INSTANCE
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("5ba7cb30-8508-4114-8c77-262fcda4fadb")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_CATEGORY_INSTANCE
  {
    [SecurityCritical]
    uint Next([In] uint ulElements, [MarshalAs(UnmanagedType.LPArray), Out] STORE_CATEGORY_INSTANCE[] rgInstances);

    [SecurityCritical]
    void Skip([In] uint ulElements);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_CATEGORY_INSTANCE Clone();
  }
}
