// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ArraySortHelper`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Collections.Generic
{
  [TypeDependency("System.Collections.Generic.GenericArraySortHelper`2")]
  internal class ArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue>
  {
    private static volatile IArraySortHelper<TKey, TValue> defaultArraySortHelper;

    public static IArraySortHelper<TKey, TValue> Default
    {
      get
      {
        return ArraySortHelper<TKey, TValue>.defaultArraySortHelper ?? ArraySortHelper<TKey, TValue>.CreateArraySortHelper();
      }
    }

    [SecuritySafeCritical]
    private static IArraySortHelper<TKey, TValue> CreateArraySortHelper()
    {
      if (typeof (IComparable<TKey>).IsAssignableFrom(typeof (TKey)))
        ArraySortHelper<TKey, TValue>.defaultArraySortHelper = (IArraySortHelper<TKey, TValue>) RuntimeTypeHandle.Allocate(typeof (GenericArraySortHelper<string, string>).TypeHandle.Instantiate(new Type[2]
        {
          typeof (TKey),
          typeof (TValue)
        }));
      else
        ArraySortHelper<TKey, TValue>.defaultArraySortHelper = (IArraySortHelper<TKey, TValue>) new ArraySortHelper<TKey, TValue>();
      return ArraySortHelper<TKey, TValue>.defaultArraySortHelper;
    }

    public void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer)
    {
      try
      {
        if (comparer == null || comparer == Comparer<TKey>.Default)
          comparer = (IComparer<TKey>) Comparer<TKey>.Default;
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
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

    private static void SwapIfGreaterWithItems(TKey[] keys, TValue[] values, IComparer<TKey> comparer, int a, int b)
    {
      if (a == b || comparer.Compare(keys[a], keys[b]) <= 0)
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

    internal static void DepthLimitedQuickSort(TKey[] keys, TValue[] values, int left, int right, IComparer<TKey> comparer, int depthLimit)
    {
      while (depthLimit != 0)
      {
        int index1 = left;
        int index2 = right;
        int index3 = index1 + (index2 - index1 >> 1);
        ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, index1, index3);
        ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, index1, index2);
        ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, index3, index2);
        TKey key1 = keys[index3];
        do
        {
          while (comparer.Compare(keys[index1], key1) < 0)
            ++index1;
          while (comparer.Compare(key1, keys[index2]) < 0)
            --index2;
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
            ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, left, index2, comparer, depthLimit);
          left = index1;
        }
        else
        {
          if (index1 < right)
            ArraySortHelper<TKey, TValue>.DepthLimitedQuickSort(keys, values, index1, right, comparer, depthLimit);
          right = index2;
        }
        if (left >= right)
          return;
      }
      ArraySortHelper<TKey, TValue>.Heapsort(keys, values, left, right, comparer);
    }

    internal static void IntrospectiveSort(TKey[] keys, TValue[] values, int left, int length, IComparer<TKey> comparer)
    {
      if (length < 2)
        return;
      ArraySortHelper<TKey, TValue>.IntroSort(keys, values, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
    }

    private static void IntroSort(TKey[] keys, TValue[] values, int lo, int hi, int depthLimit, IComparer<TKey> comparer)
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
            ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
            break;
          }
          if (num2 == 3)
          {
            ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi - 1);
            ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
            ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, hi - 1, hi);
            break;
          }
          ArraySortHelper<TKey, TValue>.InsertionSort(keys, values, lo, hi, comparer);
          break;
        }
        if (depthLimit == 0)
        {
          ArraySortHelper<TKey, TValue>.Heapsort(keys, values, lo, hi, comparer);
          break;
        }
        --depthLimit;
        num1 = ArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys, values, lo, hi, comparer);
        ArraySortHelper<TKey, TValue>.IntroSort(keys, values, num1 + 1, hi, depthLimit, comparer);
      }
    }

    private static int PickPivotAndPartition(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
    {
      int index = lo + (hi - lo) / 2;
      ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, index);
      ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
      ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, index, hi);
      TKey key = keys[index];
      ArraySortHelper<TKey, TValue>.Swap(keys, values, index, hi - 1);
      int i = lo;
      int j = hi - 1;
      while (i < j)
      {
        do
          ;
        while (comparer.Compare(keys[++i], key) < 0);
        do
          ;
        while (comparer.Compare(key, keys[--j]) < 0);
        if (i < j)
          ArraySortHelper<TKey, TValue>.Swap(keys, values, i, j);
        else
          break;
      }
      ArraySortHelper<TKey, TValue>.Swap(keys, values, i, hi - 1);
      return i;
    }

    private static void Heapsort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
    {
      int n = hi - lo + 1;
      for (int i = n / 2; i >= 1; --i)
        ArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, n, lo, comparer);
      for (int index = n; index > 1; --index)
      {
        ArraySortHelper<TKey, TValue>.Swap(keys, values, lo, lo + index - 1);
        ArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, index - 1, lo, comparer);
      }
    }

    private static void DownHeap(TKey[] keys, TValue[] values, int i, int n, int lo, IComparer<TKey> comparer)
    {
      TKey key = keys[lo + i - 1];
      TValue obj = values != null ? values[lo + i - 1] : default (TValue);
      int num;
      for (; i <= n / 2; i = num)
      {
        num = 2 * i;
        if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
          ++num;
        if (comparer.Compare(key, keys[lo + num - 1]) < 0)
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

    private static void InsertionSort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
    {
      for (int index1 = lo; index1 < hi; ++index1)
      {
        int index2 = index1;
        TKey key = keys[index1 + 1];
        TValue obj = values != null ? values[index1 + 1] : default (TValue);
        for (; index2 >= lo && comparer.Compare(key, keys[index2]) < 0; --index2)
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
