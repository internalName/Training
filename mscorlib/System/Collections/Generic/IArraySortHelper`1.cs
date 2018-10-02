// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IArraySortHelper`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  internal interface IArraySortHelper<TKey>
  {
    void Sort(TKey[] keys, int index, int length, IComparer<TKey> comparer);

    int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey> comparer);
  }
}
