// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumDefinitionIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("f3549d9c-fc73-4793-9c00-1cd204254c0c")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumDefinitionIdentity
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] IDefinitionIdentity[] DefinitionIdentity);

    [SecurityCritical]
    void Skip([In] uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumDefinitionIdentity Clone();
  }
}
