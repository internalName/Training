// Decompiled with JetBrains decompiler
// Type: System.Threading._IOCompletionCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal class _IOCompletionCallback
  {
    internal static ContextCallback _ccb = new ContextCallback(_IOCompletionCallback.IOCompletionCallback_Context);
    [SecurityCritical]
    private IOCompletionCallback _ioCompletionCallback;
    private ExecutionContext _executionContext;
    private uint _errorCode;
    private uint _numBytes;
    [SecurityCritical]
    private unsafe NativeOverlapped* _pOVERLAP;

    [SecuritySafeCritical]
    static _IOCompletionCallback()
    {
    }

    [SecurityCritical]
    internal _IOCompletionCallback(IOCompletionCallback ioCompletionCallback, ref StackCrawlMark stackMark)
    {
      this._ioCompletionCallback = ioCompletionCallback;
      this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    [SecurityCritical]
    internal static unsafe void IOCompletionCallback_Context(object state)
    {
      _IOCompletionCallback completionCallback = (_IOCompletionCallback) state;
      completionCallback._ioCompletionCallback(completionCallback._errorCode, completionCallback._numBytes, completionCallback._pOVERLAP);
    }

    [SecurityCritical]
    internal static unsafe void PerformIOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
    {
      do
      {
        Overlapped overlapped = OverlappedData.GetOverlappedFromNative(pOVERLAP).m_overlapped;
        _IOCompletionCallback iocbHelper = overlapped.iocbHelper;
        if (iocbHelper == null || iocbHelper._executionContext == null || iocbHelper._executionContext.IsDefaultFTContext(true))
        {
          overlapped.UserCallback(errorCode, numBytes, pOVERLAP);
        }
        else
        {
          iocbHelper._errorCode = errorCode;
          iocbHelper._numBytes = numBytes;
          iocbHelper._pOVERLAP = pOVERLAP;
          using (ExecutionContext copy = iocbHelper._executionContext.CreateCopy())
            ExecutionContext.Run(copy, _IOCompletionCallback._ccb, (object) iocbHelper, true);
        }
        OverlappedData.CheckVMForIOPacket(out pOVERLAP, out errorCode, out numBytes);
      }
      while ((IntPtr) pOVERLAP != IntPtr.Zero);
    }
  }
}
