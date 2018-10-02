﻿// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.MngdSafeArrayMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class MngdSafeArrayMarshaler
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, int iRank, int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, object pOriginalManaged);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);
  }
}
