// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IIdentityAuthority
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("261a6983-c35d-4d0d-aa5b-7867259e77bc")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IIdentityAuthority
  {
    [SecurityCritical]
    IDefinitionIdentity TextToDefinition([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr), In] string Identity);

    [SecurityCritical]
    IReferenceIdentity TextToReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr), In] string Identity);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string DefinitionToText([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

    [SecurityCritical]
    uint DefinitionToTextBuffer([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray), Out] char[] Buffer);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string ReferenceToText([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

    [SecurityCritical]
    uint ReferenceToTextBuffer([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray), Out] char[] Buffer);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool AreDefinitionsEqual([In] uint Flags, [In] IDefinitionIdentity Definition1, [In] IDefinitionIdentity Definition2);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool AreReferencesEqual([In] uint Flags, [In] IReferenceIdentity Reference1, [In] IReferenceIdentity Reference2);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool AreTextualDefinitionsEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr), In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr), In] string IdentityRight);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool AreTextualReferencesEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr), In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr), In] string IdentityRight);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool DoesDefinitionMatchReference([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] IReferenceIdentity ReferenceIdentity);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool DoesTextualDefinitionMatchTextualReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr), In] string Definition, [MarshalAs(UnmanagedType.LPWStr), In] string Reference);

    [SecurityCritical]
    ulong HashReference([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

    [SecurityCritical]
    ulong HashDefinition([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GenerateDefinitionKey([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GenerateReferenceKey([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

    [SecurityCritical]
    IDefinitionIdentity CreateDefinition();

    [SecurityCritical]
    IReferenceIdentity CreateReference();
  }
}
