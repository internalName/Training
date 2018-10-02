// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.RangeWorker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
  [StructLayout(LayoutKind.Auto)]
  internal struct RangeWorker
  {
    internal readonly IndexRange[] m_indexRanges;
    internal int m_nCurrentIndexRange;
    internal long m_nStep;
    internal long m_nIncrementValue;
    internal readonly long m_nMaxIncrementValue;
    internal readonly bool _use32BitCurrentIndex;

    internal RangeWorker(IndexRange[] ranges, int nInitialRange, long nStep, bool use32BitCurrentIndex)
    {
      this.m_indexRanges = ranges;
      this.m_nCurrentIndexRange = nInitialRange;
      this._use32BitCurrentIndex = use32BitCurrentIndex;
      this.m_nStep = nStep;
      this.m_nIncrementValue = nStep;
      this.m_nMaxIncrementValue = 16L * nStep;
    }

    internal unsafe bool FindNewWork(out long nFromInclusiveLocal, out long nToExclusiveLocal)
    {
      int length = this.m_indexRanges.Length;
      do
      {
        IndexRange indexRange = this.m_indexRanges[this.m_nCurrentIndexRange];
        if (indexRange.m_bRangeFinished == 0)
        {
          if (this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset == null)
            Interlocked.CompareExchange<Shared<long>>(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset, new Shared<long>(0L), (Shared<long>) null);
          long num;
          if (IntPtr.Size == 4 && this._use32BitCurrentIndex)
          {
            fixed (long* numPtr = &this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value)
            {
              // ISSUE: cast to a reference type
              num = (long) Interlocked.Add((int&) numPtr, (int) this.m_nIncrementValue) - this.m_nIncrementValue;
            }
          }
          else
            num = Interlocked.Add(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value, this.m_nIncrementValue) - this.m_nIncrementValue;
          if (indexRange.m_nToExclusive - indexRange.m_nFromInclusive > num)
          {
            nFromInclusiveLocal = indexRange.m_nFromInclusive + num;
            nToExclusiveLocal = nFromInclusiveLocal + this.m_nIncrementValue;
            if (nToExclusiveLocal > indexRange.m_nToExclusive || nToExclusiveLocal < indexRange.m_nFromInclusive)
              nToExclusiveLocal = indexRange.m_nToExclusive;
            if (this.m_nIncrementValue < this.m_nMaxIncrementValue)
            {
              this.m_nIncrementValue *= 2L;
              if (this.m_nIncrementValue > this.m_nMaxIncrementValue)
                this.m_nIncrementValue = this.m_nMaxIncrementValue;
            }
            return true;
          }
          Interlocked.Exchange(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_bRangeFinished, 1);
        }
        this.m_nCurrentIndexRange = (this.m_nCurrentIndexRange + 1) % this.m_indexRanges.Length;
        --length;
      }
      while (length > 0);
      nFromInclusiveLocal = 0L;
      nToExclusiveLocal = 0L;
      return false;
    }

    internal bool FindNewWork32(out int nFromInclusiveLocal32, out int nToExclusiveLocal32)
    {
      long nFromInclusiveLocal;
      long nToExclusiveLocal;
      bool newWork = this.FindNewWork(out nFromInclusiveLocal, out nToExclusiveLocal);
      nFromInclusiveLocal32 = (int) nFromInclusiveLocal;
      nToExclusiveLocal32 = (int) nToExclusiveLocal;
      return newWork;
    }
  }
}
