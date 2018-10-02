// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IReferenceIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("6eaf5ace-7917-4f3c-b129-e046a9704766")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IReferenceIdentity
  {
    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetAttribute([MarshalAs(UnmanagedType.LPWStr), In] string Namespace, [MarshalAs(UnmanagedType.LPWStr), In] string Name);

    [SecurityCritical]
    void SetAttribute([MarshalAs(UnmanagedType.LPWStr), In] string Namespace, [MarshalAs(UnmanagedType.LPWStr), In] string Name, [MarshalAs(UnmanagedType.LPWStr), In] string Value);

    [SecurityCritical]
    IEnumIDENTITY_ATTRIBUTE EnumAttributes();

    [SecurityCritical]
    IReferenceIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray), In] IDENTITY_ATTRIBUTE[] Deltas);
  }
}
