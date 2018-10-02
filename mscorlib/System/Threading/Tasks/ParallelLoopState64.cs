// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopState64
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopState64 : ParallelLoopState
  {
    private ParallelLoopStateFlags64 m_sharedParallelStateFlags;
    private long m_currentIteration;

    internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags)
      : base((ParallelLoopStateFlags) sharedParallelStateFlags)
    {
      this.m_sharedParallelStateFlags = sharedParallelStateFlags;
    }

    internal long CurrentIteration
    {
      get
      {
        return this.m_currentIteration;
      }
      set
      {
        this.m_currentIteration = value;
      }
    }

    internal override bool InternalShouldExitCurrentIteration
    {
      get
      {
        return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
      }
    }

    internal override long? InternalLowestBreakIteration
    {
      get
      {
        return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
      }
    }

    internal override void InternalBreak()
    {
      ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
    }
  }
}
