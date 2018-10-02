// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_CATEGORY_SUBCATEGORY
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("19be1967-b2fc-4dc1-9627-f3cb6305d2a7")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_CATEGORY_SUBCATEGORY
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] STORE_CATEGORY_SUBCATEGORY[] rgElements);

    [SecurityCritical]
    void Skip([In] uint ulElements);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_CATEGORY_SUBCATEGORY Clone();
  }
}
