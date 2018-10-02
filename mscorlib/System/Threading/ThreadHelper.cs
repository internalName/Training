// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal class ThreadHelper
  {
    [SecurityCritical]
    internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
    private Delegate _start;
    private object _startArg;
    private ExecutionContext _executionContext;

    [SecuritySafeCritical]
    static ThreadHelper()
    {
    }

    internal ThreadHelper(Delegate start)
    {
      this._start = start;
    }

    internal void SetExecutionContextHelper(ExecutionContext ec)
    {
      this._executionContext = ec;
    }

    [SecurityCritical]
    private static void ThreadStart_Context(object state)
    {
      ThreadHelper threadHelper = (ThreadHelper) state;
      if (threadHelper._start is ThreadStart)
        ((ThreadStart) threadHelper._start)();
      else
        ((ParameterizedThreadStart) threadHelper._start)(threadHelper._startArg);
    }

    [SecurityCritical]
    internal void ThreadStart(object obj)
    {
      this._startArg = obj;
      if (this._executionContext != null)
        ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, (object) this);
      else
        ((ParameterizedThreadStart) this._start)(obj);
    }

    [SecurityCritical]
    internal void ThreadStart()
    {
      if (this._executionContext != null)
        ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, (object) this);
      else
        ((ThreadStart) this._start)();
    }
  }
}
