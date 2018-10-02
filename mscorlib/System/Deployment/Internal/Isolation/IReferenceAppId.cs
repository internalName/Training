// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IReferenceAppId
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("054f0bef-9e45-4363-8f5a-2f8e142d9a3b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IReferenceAppId
  {
    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string get_SubscriptionId();

    void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr), In] string Subscription);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string get_Codebase();

    void put_Codebase([MarshalAs(UnmanagedType.LPWStr), In] string CodeBase);

    [SecurityCritical]
    IEnumReferenceIdentity EnumAppPath();
  }
}
