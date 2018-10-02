// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPoolBoundHandleOverlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  [SecurityCritical]
  internal sealed class ThreadPoolBoundHandleOverlapped : Overlapped
  {
    private static readonly IOCompletionCallback s_completionCallback = new IOCompletionCallback(ThreadPoolBoundHandleOverlapped.CompletionCallback);
    private readonly IOCompletionCallback _userCallback;
    internal readonly object _userState;
    internal PreAllocatedOverlapped _preAllocated;
    internal unsafe NativeOverlapped* _nativeOverlapped;
    internal ThreadPoolBoundHandle _boundHandle;
    internal bool _completed;

    public unsafe ThreadPoolBoundHandleOverlapped(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
    {
      this._userCallback = callback;
      this._userState = state;
      this._preAllocated = preAllocated;
      this._nativeOverlapped = this.Pack(ThreadPoolBoundHandleOverlapped.s_completionCallback, pinData);
      this._nativeOverlapped->OffsetLow = 0;
      this._nativeOverlapped->OffsetHigh = 0;
    }

    private static unsafe void CompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
    {
      ThreadPoolBoundHandleOverlapped handleOverlapped = (ThreadPoolBoundHandleOverlapped) Overlapped.Unpack(nativeOverlapped);
      if (handleOverlapped._completed)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NativeOverlappedReused"));
      handleOverlapped._completed = true;
      if (handleOverlapped._boundHandle == null)
        throw new InvalidOperationException(Environment.GetResourceString("Argument_NativeOverlappedAlreadyFree"));
      handleOverlapped._userCallback(errorCode, numBytes, nativeOverlapped);
    }
  }
}
