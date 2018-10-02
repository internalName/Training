// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.IApplicationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("7c23ff90-33af-11d3-95da-00a024a85b51")]
  [ComImport]
  internal interface IApplicationContext
  {
    void SetContextNameObject(IAssemblyName pName);

    void GetContextNameObject(out IAssemblyName ppName);

    void Set([MarshalAs(UnmanagedType.LPWStr)] string szName, int pvValue, uint cbValue, uint dwFlags);

    void Get([MarshalAs(UnmanagedType.LPWStr)] string szName, out int pvValue, ref uint pcbValue, uint dwFlags);

    void GetDynamicDirectory(out int wzDynamicDir, ref uint pdwSize);
  }
}
