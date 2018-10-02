// Decompiled with JetBrains decompiler
// Type: System.Reflection.CerHashtable`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Threading;

namespace System.Reflection
{
  internal struct CerHashtable<K, V> where K : class
  {
    private CerHashtable<K, V>.Table m_Table;
    private const int MinSize = 7;

    private static int GetHashCodeHelper(K key)
    {
      string str = (object) key as string;
      if (str == null)
        return key.GetHashCode();
      return str.GetLegacyNonRandomizedHashCode();
    }

    private void Rehash(int newSize)
    {
      CerHashtable<K, V>.Table table1 = new CerHashtable<K, V>.Table(newSize);
      CerHashtable<K, V>.Table table2 = this.m_Table;
      if (table2 != null)
      {
        K[] keys = table2.m_keys;
        V[] values = table2.m_values;
        for (int index = 0; index < keys.Length; ++index)
        {
          K key = keys[index];
          if ((object) key != null)
            table1.Insert(key, values[index]);
        }
      }
      Volatile.Write<CerHashtable<K, V>.Table>(ref this.m_Table, table1);
    }

    internal V this[K key]
    {
      set
      {
        CerHashtable<K, V>.Table table = this.m_Table;
        if (table != null)
        {
          int newSize = 2 * (table.m_count + 1);
          if (newSize >= table.m_keys.Length)
            this.Rehash(newSize);
        }
        else
          this.Rehash(7);
        this.m_Table.Insert(key, value);
      }
      get
      {
        CerHashtable<K, V>.Table table = Volatile.Read<CerHashtable<K, V>.Table>(ref this.m_Table);
        if (table == null)
          return default (V);
        int num = CerHashtable<K, V>.GetHashCodeHelper(key);
        if (num < 0)
          num = ~num;
        K[] keys = table.m_keys;
        int index = num % keys.Length;
        while (true)
        {
          do
          {
            K k = Volatile.Read<K>(ref keys[index]);
            if ((object) k != null)
            {
              if (k.Equals((object) key))
                return table.m_values[index];
              ++index;
            }
            else
              goto label_10;
          }
          while (index < keys.Length);
          index -= keys.Length;
        }
label_10:
        return default (V);
      }
    }

    private class Table
    {
      internal K[] m_keys;
      internal V[] m_values;
      internal int m_count;

      internal Table(int size)
      {
        size = HashHelpers.GetPrime(size);
        this.m_keys = new K[size];
        this.m_values = new V[size];
      }

      internal void Insert(K key, V value)
      {
        int num = CerHashtable<K, V>.GetHashCodeHelper(key);
        if (num < 0)
          num = ~num;
        K[] keys = this.m_keys;
        int index = num % keys.Length;
        while ((object) keys[index] != null)
        {
          ++index;
          if (index >= keys.Length)
            index -= keys.Length;
        }
        ++this.m_count;
        this.m_values[index] = value;
        Volatile.Write<K>(ref keys[index], key);
      }
    }
  }
}
