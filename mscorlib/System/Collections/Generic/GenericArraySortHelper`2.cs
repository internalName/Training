// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.GenericArraySortHelper`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Versioning;

namespace System.Collections.Generic
{
  internal class GenericArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue> where TKey : IComparable<TKey>
  {
    public void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer)
    {
      try
      {
        if (comparer == null || comparer == Comparer<TKey>.Default)
        {
          if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
            GenericArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length);
          else
            GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index, length + index - 1, 32);
        }
        else if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
          ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length, comparer);
        else
          ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index, length + index - 1, comparer, 32);
      }
      catch (IndexOutOfRangeException ex)
      {
        IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer((object) comparer);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
      }
    }

    private static void SwapIfGreaterWithItems(TKey[] keys, TValue[] values, int a, int b)
    {
      if (a == b || (object) keys[a] == null || keys[a].CompareTo(keys[b]) <= 0)
        return;
      TKey key = keys[a];
      keys[a] = keys[b];
      keys[b] = key;
      if (values == null)
        return;
      TValue obj = values[a];
      values[a] = values[b];
      values[b] = obj;
    }

    private static void Swap(TKey[] keys, TValue[] values, int i, int j)
    {
      if (i == j)
        return;
      TKey key = keys[i];
      keys[i] = keys[j];
      keys[j] = key;
      if (values == null)
        return;
      TValue obj = values[i];
      values[i] = values[j];
      values[j] = obj;
    }

    private static void DepthLimitedQuickSort(TKey[] keys, TValue[] values, int left, int right, int depthLimit)
    {
      while (depthLimit != 0)
      {
        int index1 = left;
        int index2 = right;
        int index3 = index1 + (index2 - index1 >> 1);
        GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, index1, index3);
        GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, index1, index2);
        GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, index3, index2);
        TKey key1 = keys[index3];
        do
        {
          if ((object) key1 == null)
          {
            while ((object) keys[index2] != null)
              --index2;
          }
          else
          {
            while (key1.CompareTo(keys[index1]) > 0)
              ++index1;
            while (key1.CompareTo(keys[index2]) < 0)
              --index2;
          }
          if (index1 <= index2)
          {
            if (index1 < index2)
            {
              TKey key2 = keys[index1];
              keys[index1] = keys[index2];
              keys[index2] = key2;
              if (values != null)
              {
                TValue obj = values[index1];
                values[index1] = values[index2];
                values[index2] = obj;
              }
            }
            ++index1;
            --index2;
          }
          else
            break;
        }
        while (index1 <= index2);
        --depthLimit;
        if (index2 - left <= right - index1)
        {
          if (left < index2)
            GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, left, index2, depthLimit);
          left = index1;
        }
        else
        {
          if (index1 < right)
            GenericArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index1, right, depthLimit);
          right = index2;
        }
        if (left >= right)
          return;
      }
      GenericArraySortHelper<TKey, TValue>.Heapsort(keys, values, left, right);
    }

    internal static void IntrospectiveSort(TKey[] keys, TValue[] values, int left, int length)
    {
      if (length < 2)
        return;
      GenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length));
    }

    private static void IntroSort(TKey[] keys, TValue[] values, int lo, int hi, int depthLimit)
    {
      int num1;
      for (; hi > lo; hi = num1 - 1)
      {
        int num2 = hi - lo + 1;
        if (num2 <= 16)
        {
          if (num2 == 1)
            break;
          if (num2 == 2)
          {
            GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
            break;
          }
          if (num2 == 3)
          {
            GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi - 1);
            GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
            GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, hi - 1, hi);
            break;
          }
          GenericArraySortHelper<TKey, TValue>.InsertionSort(keys, values, lo, hi);
          break;
        }
        if (depthLimit == 0)
        {
          GenericArraySortHelper<TKey, TValue>.Heapsort(keys, values, lo, hi);
          break;
        }
        --depthLimit;
        num1 = GenericArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys, values, lo, hi);
        GenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, num1 + 1, hi, depthLimit);
      }
    }

    private static int PickPivotAndPartition(TKey[] keys, TValue[] values, int lo, int hi)
    {
      int index = lo + (hi - lo) / 2;
      GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, index);
      GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, lo, hi);
      GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, index, hi);
      TKey key = keys[index];
      GenericArraySortHelper<TKey, TValue>.Swap(keys, values, index, hi - 1);
      int i = lo;
      int j = hi - 1;
      while (i < j)
      {
        if ((object) key == null)
        {
          do
            ;
          while (i < hi - 1 && (object) keys[++i] == null);
          while (j > lo && (object) keys[--j] != null)
            ;
        }
        else
        {
          do
            ;
          while (key.CompareTo(keys[++i]) > 0);
          while (key.CompareTo(keys[--j]) < 0)
            ;
        }
        if (i < j)
          GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, j);
        else
          break;
      }
      GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, hi - 1);
      return i;
    }

    private static void Heapsort(TKey[] keys, TValue[] values, int lo, int hi)
    {
      int n = hi - lo + 1;
      for (int i = n / 2; i >= 1; --i)
        GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, n, lo);
      for (int index = n; index > 1; --index)
      {
        GenericArraySortHelper<TKey, TValue>.Swap(keys, values, lo, lo + index - 1);
        GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, index - 1, lo);
      }
    }

    private static void DownHeap(TKey[] keys, TValue[] values, int i, int n, int lo)
    {
      TKey key = keys[lo + i - 1];
      TValue obj = values != null ? values[lo + i - 1] : default (TValue);
      int num;
      for (; i <= n / 2; i = num)
      {
        num = 2 * i;
        if (num < n && ((object) keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(keys[lo + num]) < 0))
          ++num;
        if ((object) keys[lo + num - 1] != null && keys[lo + num - 1].CompareTo(key) >= 0)
        {
          keys[lo + i - 1] = keys[lo + num - 1];
          if (values != null)
            values[lo + i - 1] = values[lo + num - 1];
        }
        else
          break;
      }
      keys[lo + i - 1] = key;
      if (values == null)
        return;
      values[lo + i - 1] = obj;
    }

    private static void InsertionSort(TKey[] keys, TValue[] values, int lo, int hi)
    {
      for (int index1 = lo; index1 < hi; ++index1)
      {
        int index2 = index1;
        TKey key = keys[index1 + 1];
        TValue obj = values != null ? values[index1 + 1] : default (TValue);
        for (; index2 >= lo && ((object) key == null || key.CompareTo(keys[index2]) < 0); --index2)
        {
          keys[index2 + 1] = keys[index2];
          if (values != null)
            values[index2 + 1] = values[index2];
        }
        keys[index2 + 1] = key;
        if (values != null)
          values[index2 + 1] = obj;
      }
    }
  }
}
