// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.IAssemblyName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E")]
  [ComImport]
  internal interface IAssemblyName
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetProperty(uint PropertyId, IntPtr pvProperty, uint cbProperty);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetProperty(uint PropertyId, IntPtr pvProperty, ref uint pcbProperty);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Finalize();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDisplayName(IntPtr szDisplayName, ref uint pccDisplayName, uint dwDisplayFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int BindToObject(object refIID, object pAsmBindSink, IApplicationContext pApplicationContext, [MarshalAs(UnmanagedType.LPWStr)] string szCodeBase, long llFlags, int pvReserved, uint cbReserved, out int ppv);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetName(out uint lpcwBuffer, out int pwzName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetVersion(out uint pdwVersionHi, out uint pdwVersionLow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsEqual(IAssemblyName pName, uint dwCmpFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Clone(out IAssemblyName pName);
  }
}
