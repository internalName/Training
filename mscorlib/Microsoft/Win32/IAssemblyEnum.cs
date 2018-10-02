// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.IAssemblyEnum
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("21b8916c-f28e-11d2-a473-00c04f8ef448")]
  [ComImport]
  internal interface IAssemblyEnum
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNextAssembly(out IApplicationContext ppAppCtx, out IAssemblyName ppName, uint dwFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Clone(out IAssemblyEnum ppEnum);
  }
}
