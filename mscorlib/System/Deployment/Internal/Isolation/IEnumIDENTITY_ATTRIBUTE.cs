// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumIDENTITY_ATTRIBUTE
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("9cdaae75-246e-4b00-a26d-b9aec137a3eb")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumIDENTITY_ATTRIBUTE
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] IDENTITY_ATTRIBUTE[] rgAttributes);

    [SecurityCritical]
    IntPtr CurrentIntoBuffer([In] IntPtr Available, [MarshalAs(UnmanagedType.LPArray), Out] byte[] Data);

    [SecurityCritical]
    void Skip([In] uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumIDENTITY_ATTRIBUTE Clone();
  }
}
