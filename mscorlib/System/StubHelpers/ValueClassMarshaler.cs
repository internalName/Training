// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.ValueClassMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class ValueClassMarshaler
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertToNative(IntPtr dst, IntPtr src, IntPtr pMT, ref CleanupWorkList pCleanupWorkList);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertToManaged(IntPtr dst, IntPtr src, IntPtr pMT);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearNative(IntPtr dst, IntPtr pMT);
  }
}
