// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IDefinitionAppId
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("d91e12d8-98ed-47fa-9936-39421283d59b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IDefinitionAppId
  {
    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string get_SubscriptionId();

    void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr), In] string Subscription);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string get_Codebase();

    [SecurityCritical]
    void put_Codebase([MarshalAs(UnmanagedType.LPWStr), In] string CodeBase);

    [SecurityCritical]
    IEnumDefinitionIdentity EnumAppPath();

    [SecurityCritical]
    void SetAppPath([In] uint cIDefinitionIdentity, [MarshalAs(UnmanagedType.LPArray), In] IDefinitionIdentity[] DefinitionIdentity);
  }
}
