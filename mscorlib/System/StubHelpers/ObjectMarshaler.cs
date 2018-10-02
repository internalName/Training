// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.ObjectMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class ObjectMarshaler
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertToNative(object objSrc, IntPtr pDstVariant);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object ConvertToManaged(IntPtr pSrcVariant);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearNative(IntPtr pVariant);
  }
}
