// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.LongList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  [Serializable]
  internal class LongList
  {
    private const int InitialSize = 2;
    private long[] m_values;
    private int m_count;
    private int m_totalItems;
    private int m_currentItem;

    internal LongList()
      : this(2)
    {
    }

    internal LongList(int startingSize)
    {
      this.m_count = 0;
      this.m_totalItems = 0;
      this.m_values = new long[startingSize];
    }

    internal void Add(long value)
    {
      if (this.m_totalItems == this.m_values.Length)
        this.EnlargeArray();
      this.m_values[this.m_totalItems++] = value;
      ++this.m_count;
    }

    internal int Count
    {
      get
      {
        return this.m_count;
      }
    }

    internal void StartEnumeration()
    {
      this.m_currentItem = -1;
    }

    internal bool MoveNext()
    {
      do
        ;
      while (++this.m_currentItem < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
      return this.m_currentItem != this.m_totalItems;
    }

    internal long Current
    {
      get
      {
        return this.m_values[this.m_currentItem];
      }
    }

    internal bool RemoveElement(long value)
    {
      int index = 0;
      while (index < this.m_totalItems && this.m_values[index] != value)
        ++index;
      if (index == this.m_totalItems)
        return false;
      this.m_values[index] = -1L;
      return true;
    }

    private void EnlargeArray()
    {
      int length = this.m_values.Length * 2;
      if (length < 0)
      {
        if (length == int.MaxValue)
          throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
        length = int.MaxValue;
      }
      long[] numArray = new long[length];
      Array.Copy((Array) this.m_values, (Array) numArray, this.m_count);
      this.m_values = numArray;
    }
  }
}
