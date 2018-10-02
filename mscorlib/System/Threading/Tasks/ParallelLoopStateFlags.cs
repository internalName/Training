// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopStateFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopStateFlags
  {
    internal static int PLS_EXCEPTIONAL = 1;
    internal static int PLS_BROKEN = 2;
    internal static int PLS_STOPPED = 4;
    internal static int PLS_CANCELED = 8;
    private volatile int m_LoopStateFlags = ParallelLoopStateFlags.PLS_NONE;
    internal static int PLS_NONE;

    internal int LoopStateFlags
    {
      get
      {
        return this.m_LoopStateFlags;
      }
    }

    internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
    {
      int oldState = 0;
      return this.AtomicLoopStateUpdate(newState, illegalStates, ref oldState);
    }

    internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        oldState = this.m_LoopStateFlags;
        if ((oldState & illegalStates) == 0)
        {
          if (Interlocked.CompareExchange(ref this.m_LoopStateFlags, oldState | newState, oldState) != oldState)
            spinWait.SpinOnce();
          else
            goto label_4;
        }
        else
          break;
      }
      return false;
label_4:
      return true;
    }

    internal void SetExceptional()
    {
      this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_EXCEPTIONAL, ParallelLoopStateFlags.PLS_NONE);
    }

    internal void Stop()
    {
      if (!this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_STOPPED, ParallelLoopStateFlags.PLS_BROKEN))
        throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Stop_InvalidOperationException_StopAfterBreak"));
    }

    internal bool Cancel()
    {
      return this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_CANCELED, ParallelLoopStateFlags.PLS_NONE);
    }
  }
}
