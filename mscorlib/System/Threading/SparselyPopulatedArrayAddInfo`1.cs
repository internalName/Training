// Decompiled with JetBrains decompiler
// Type: System.Threading.SparselyPopulatedArrayAddInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal struct SparselyPopulatedArrayAddInfo<T> where T : class
  {
    private SparselyPopulatedArrayFragment<T> m_source;
    private int m_index;

    internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
    {
      this.m_source = source;
      this.m_index = index;
    }

    internal SparselyPopulatedArrayFragment<T> Source
    {
      get
      {
        return this.m_source;
      }
    }

    internal int Index
    {
      get
      {
        return this.m_index;
      }
    }
  }
}
