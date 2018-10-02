// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopStateFlags64
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
  {
    internal long m_lowestBreakIteration = long.MaxValue;

    internal long LowestBreakIteration
    {
      get
      {
        if (IntPtr.Size >= 8)
          return this.m_lowestBreakIteration;
        return Interlocked.Read(ref this.m_lowestBreakIteration);
      }
    }

    internal long? NullableLowestBreakIteration
    {
      get
      {
        if (this.m_lowestBreakIteration == long.MaxValue)
          return new long?();
        if (IntPtr.Size >= 8)
          return new long?(this.m_lowestBreakIteration);
        return new long?(Interlocked.Read(ref this.m_lowestBreakIteration));
      }
    }

    internal bool ShouldExitLoop(long CallerIteration)
    {
      int loopStateFlags = this.LoopStateFlags;
      if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
        return false;
      if ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0)
        return true;
      if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
        return CallerIteration > this.LowestBreakIteration;
      return false;
    }

    internal bool ShouldExitLoop()
    {
      int loopStateFlags = this.LoopStateFlags;
      if (loopStateFlags != ParallelLoopStateFlags.PLS_NONE)
        return (uint) (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) > 0U;
      return false;
    }
  }
}
