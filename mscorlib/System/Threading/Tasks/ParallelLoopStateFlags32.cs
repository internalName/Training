// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopStateFlags32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
  {
    internal volatile int m_lowestBreakIteration = int.MaxValue;

    internal int LowestBreakIteration
    {
      get
      {
        return this.m_lowestBreakIteration;
      }
    }

    internal long? NullableLowestBreakIteration
    {
      get
      {
        if (this.m_lowestBreakIteration == int.MaxValue)
          return new long?();
        long lowestBreakIteration = (long) this.m_lowestBreakIteration;
        if (IntPtr.Size >= 8)
          return new long?(lowestBreakIteration);
        return new long?(Interlocked.Read(ref lowestBreakIteration));
      }
    }

    internal bool ShouldExitLoop(int CallerIteration)
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
