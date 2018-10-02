// Decompiled with JetBrains decompiler
// Type: System.Threading.SparselyPopulatedArray`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal class SparselyPopulatedArray<T> where T : class
  {
    private readonly SparselyPopulatedArrayFragment<T> m_head;
    private volatile SparselyPopulatedArrayFragment<T> m_tail;

    internal SparselyPopulatedArray(int initialSize)
    {
      this.m_head = this.m_tail = new SparselyPopulatedArrayFragment<T>(initialSize);
    }

    internal SparselyPopulatedArrayFragment<T> Tail
    {
      get
      {
        return this.m_tail;
      }
    }

    internal SparselyPopulatedArrayAddInfo<T> Add(T element)
    {
      while (true)
      {
        SparselyPopulatedArrayFragment<T> prev;
        SparselyPopulatedArrayFragment<T> populatedArrayFragment;
        do
        {
          prev = this.m_tail;
          while (prev.m_next != null)
            this.m_tail = prev = prev.m_next;
          for (SparselyPopulatedArrayFragment<T> source = prev; source != null; source = source.m_prev)
          {
            if (source.m_freeCount < 1)
              --source.m_freeCount;
            if (source.m_freeCount > 0 || source.m_freeCount < -10)
            {
              int length = source.Length;
              int num1 = (length - source.m_freeCount) % length;
              if (num1 < 0)
              {
                num1 = 0;
                --source.m_freeCount;
              }
              for (int index1 = 0; index1 < length; ++index1)
              {
                int index2 = (num1 + index1) % length;
                if ((object) source.m_elements[index2] == null && (object) Interlocked.CompareExchange<T>(ref source.m_elements[index2], element, default (T)) == null)
                {
                  int num2 = source.m_freeCount - 1;
                  source.m_freeCount = num2 > 0 ? num2 : 0;
                  return new SparselyPopulatedArrayAddInfo<T>(source, index2);
                }
              }
            }
          }
          populatedArrayFragment = new SparselyPopulatedArrayFragment<T>(prev.m_elements.Length == 4096 ? 4096 : prev.m_elements.Length * 2, prev);
        }
        while (Interlocked.CompareExchange<SparselyPopulatedArrayFragment<T>>(ref prev.m_next, populatedArrayFragment, (SparselyPopulatedArrayFragment<T>) null) != null);
        this.m_tail = populatedArrayFragment;
      }
    }
  }
}
