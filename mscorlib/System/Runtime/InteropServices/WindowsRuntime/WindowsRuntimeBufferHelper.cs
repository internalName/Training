// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeBufferHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [FriendAccessAllowed]
  internal static class WindowsRuntimeBufferHelper
  {
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall")]
    private static extern unsafe void StoreOverlappedPtrInCCW(ObjectHandleOnStack windowsRuntimeBuffer, NativeOverlapped* overlapped);

    [FriendAccessAllowed]
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void StoreOverlappedInCCW(object windowsRuntimeBuffer, NativeOverlapped* overlapped)
    {
      WindowsRuntimeBufferHelper.StoreOverlappedPtrInCCW(JitHelpers.GetObjectHandleOnStack<object>(ref windowsRuntimeBuffer), overlapped);
    }
  }
}
