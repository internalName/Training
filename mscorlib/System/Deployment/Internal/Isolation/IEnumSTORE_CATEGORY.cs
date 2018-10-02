// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumSTORE_CATEGORY
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("b840a2f5-a497-4a6d-9038-cd3ec2fbd222")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumSTORE_CATEGORY
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] STORE_CATEGORY[] rgElements);

    [SecurityCritical]
    void Skip([In] uint ulElements);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumSTORE_CATEGORY Clone();
  }
}
