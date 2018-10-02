// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IDefinitionIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("587bf538-4d90-4a3c-9ef1-58a200a8a9e7")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IDefinitionIdentity
  {
    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetAttribute([MarshalAs(UnmanagedType.LPWStr), In] string Namespace, [MarshalAs(UnmanagedType.LPWStr), In] string Name);

    [SecurityCritical]
    void SetAttribute([MarshalAs(UnmanagedType.LPWStr), In] string Namespace, [MarshalAs(UnmanagedType.LPWStr), In] string Name, [MarshalAs(UnmanagedType.LPWStr), In] string Value);

    [SecurityCritical]
    IEnumIDENTITY_ATTRIBUTE EnumAttributes();

    [SecurityCritical]
    IDefinitionIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray), In] IDENTITY_ATTRIBUTE[] Deltas);
  }
}
