// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.RangeManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class RangeManager
  {
    internal readonly IndexRange[] m_indexRanges;
    internal readonly bool _use32BitCurrentIndex;
    internal int m_nCurrentIndexRangeToAssign;
    internal long m_nStep;

    internal RangeManager(long nFromInclusive, long nToExclusive, long nStep, int nNumExpectedWorkers)
    {
      this.m_nCurrentIndexRangeToAssign = 0;
      this.m_nStep = nStep;
      if (nNumExpectedWorkers == 1)
        nNumExpectedWorkers = 2;
      ulong num1 = (ulong) (nToExclusive - nFromInclusive);
      ulong num2 = num1 / (ulong) nNumExpectedWorkers;
      ulong num3 = num2 - num2 % (ulong) nStep;
      if (num3 == 0UL)
        num3 = (ulong) nStep;
      int length = (int) (num1 / num3);
      if (num1 % num3 != 0UL)
        ++length;
      long num4 = (long) num3;
      this._use32BitCurrentIndex = IntPtr.Size == 4 && num4 <= (long) int.MaxValue;
      this.m_indexRanges = new IndexRange[length];
      long num5 = nFromInclusive;
      for (int index = 0; index < length; ++index)
      {
        this.m_indexRanges[index].m_nFromInclusive = num5;
        this.m_indexRanges[index].m_nSharedCurrentIndexOffset = (Shared<long>) null;
        this.m_indexRanges[index].m_bRangeFinished = 0;
        num5 += num4;
        if (num5 < num5 - num4 || num5 > nToExclusive)
          num5 = nToExclusive;
        this.m_indexRanges[index].m_nToExclusive = num5;
      }
    }

    internal RangeWorker RegisterNewWorker()
    {
      return new RangeWorker(this.m_indexRanges, (Interlocked.Increment(ref this.m_nCurrentIndexRangeToAssign) - 1) % this.m_indexRanges.Length, this.m_nStep, this._use32BitCurrentIndex);
    }
  }
}
