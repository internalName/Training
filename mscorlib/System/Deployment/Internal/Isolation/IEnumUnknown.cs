// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumUnknown
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000100-0000-0000-C000-000000000046")]
  [ComImport]
  internal interface IEnumUnknown
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(uint celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown), Out] object[] rgelt, ref uint celtFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(uint celt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Clone(out IEnumUnknown enumUnknown);
  }
}
