// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.InterfaceMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  [FriendAccessAllowed]
  internal static class InterfaceMarshaler
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr ConvertToNative(object objSrc, IntPtr itfMT, IntPtr classMT, int flags);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object ConvertToManaged(IntPtr pUnk, IntPtr itfMT, IntPtr classMT, int flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall")]
    internal static extern void ClearNative(IntPtr pUnk);

    [FriendAccessAllowed]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object ConvertToManagedWithoutUnboxing(IntPtr pNative);
  }
}
