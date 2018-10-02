// Decompiled with JetBrains decompiler
// Type: System.Threading.SparselyPopulatedArrayFragment`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal class SparselyPopulatedArrayFragment<T> where T : class
  {
    internal readonly T[] m_elements;
    internal volatile int m_freeCount;
    internal volatile SparselyPopulatedArrayFragment<T> m_next;
    internal volatile SparselyPopulatedArrayFragment<T> m_prev;

    internal SparselyPopulatedArrayFragment(int size)
      : this(size, (SparselyPopulatedArrayFragment<T>) null)
    {
    }

    internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
    {
      this.m_elements = new T[size];
      this.m_freeCount = size;
      this.m_prev = prev;
    }

    internal T this[int index]
    {
      get
      {
        return Volatile.Read<T>(ref this.m_elements[index]);
      }
    }

    internal int Length
    {
      get
      {
        return this.m_elements.Length;
      }
    }

    internal SparselyPopulatedArrayFragment<T> Prev
    {
      get
      {
        return this.m_prev;
      }
    }

    internal T SafeAtomicRemove(int index, T expectedElement)
    {
      T obj = Interlocked.CompareExchange<T>(ref this.m_elements[index], default (T), expectedElement);
      if ((object) obj != null)
        ++this.m_freeCount;
      return obj;
    }
  }
}
