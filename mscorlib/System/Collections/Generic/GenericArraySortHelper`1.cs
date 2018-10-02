// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.GenericArraySortHelper`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Versioning;

namespace System.Collections.Generic
{
  [Serializable]
  internal class GenericArraySortHelper<T> : IArraySortHelper<T> where T : IComparable<T>
  {
    public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
    {
      try
      {
        if (comparer == null || comparer == Comparer<T>.Default)
        {
          if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
            GenericArraySortHelper<T>.IntrospectiveSort(keys, index, length);
          else
            GenericArraySortHelper<T>.DepthLimitedQuickSort(keys, index, length + index - 1, 32);
        }
        else if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
          ArraySortHelper<T>.IntrospectiveSort(keys, index, length, comparer);
        else
          ArraySortHelper<T>.DepthLimitedQuickSort(keys, index, length + index - 1, comparer, 32);
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

    public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
    {
      try
      {
        if (comparer == null || comparer == Comparer<T>.Default)
          return GenericArraySortHelper<T>.BinarySearch(array, index, length, value);
        return ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
      }
    }

    private static int BinarySearch(T[] array, int index, int length, T value)
    {
      int num1 = index;
      int num2 = index + length - 1;
      while (num1 <= num2)
      {
        int index1 = num1 + (num2 - num1 >> 1);
        int num3 = (object) array[index1] != null ? array[index1].CompareTo(value) : ((object) value == null ? 0 : -1);
        if (num3 == 0)
          return index1;
        if (num3 < 0)
          num1 = index1 + 1;
        else
          num2 = index1 - 1;
      }
      return ~num1;
    }

    private static void SwapIfGreaterWithItems(T[] keys, int a, int b)
    {
      if (a == b || (object) keys[a] == null || keys[a].CompareTo(keys[b]) <= 0)
        return;
      T key = keys[a];
      keys[a] = keys[b];
      keys[b] = key;
    }

    private static void Swap(T[] a, int i, int j)
    {
      if (i == j)
        return;
      T obj = a[i];
      a[i] = a[j];
      a[j] = obj;
    }

    private static void DepthLimitedQuickSort(T[] keys, int left, int right, int depthLimit)
    {
      while (depthLimit != 0)
      {
        int index1 = left;
        int index2 = right;
        int index3 = index1 + (index2 - index1 >> 1);
        GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, index1, index3);
        GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, index1, index2);
        GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, index3, index2);
        T key1 = keys[index3];
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
              T key2 = keys[index1];
              keys[index1] = keys[index2];
              keys[index2] = key2;
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
            GenericArraySortHelper<T>.DepthLimitedQuickSort(keys, left, index2, depthLimit);
          left = index1;
        }
        else
        {
          if (index1 < right)
            GenericArraySortHelper<T>.DepthLimitedQuickSort(keys, index1, right, depthLimit);
          right = index2;
        }
        if (left >= right)
          return;
      }
      GenericArraySortHelper<T>.Heapsort(keys, left, right);
    }

    internal static void IntrospectiveSort(T[] keys, int left, int length)
    {
      if (length < 2)
        return;
      GenericArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length));
    }

    private static void IntroSort(T[] keys, int lo, int hi, int depthLimit)
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
            GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
            break;
          }
          if (num2 == 3)
          {
            GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi - 1);
            GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
            GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, hi - 1, hi);
            break;
          }
          GenericArraySortHelper<T>.InsertionSort(keys, lo, hi);
          break;
        }
        if (depthLimit == 0)
        {
          GenericArraySortHelper<T>.Heapsort(keys, lo, hi);
          break;
        }
        --depthLimit;
        num1 = GenericArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi);
        GenericArraySortHelper<T>.IntroSort(keys, num1 + 1, hi, depthLimit);
      }
    }

    private static int PickPivotAndPartition(T[] keys, int lo, int hi)
    {
      int index = lo + (hi - lo) / 2;
      GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, index);
      GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
      GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, index, hi);
      T key = keys[index];
      GenericArraySortHelper<T>.Swap(keys, index, hi - 1);
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
          GenericArraySortHelper<T>.Swap(keys, i, j);
        else
          break;
      }
      GenericArraySortHelper<T>.Swap(keys, i, hi - 1);
      return i;
    }

    private static void Heapsort(T[] keys, int lo, int hi)
    {
      int n = hi - lo + 1;
      for (int i = n / 2; i >= 1; --i)
        GenericArraySortHelper<T>.DownHeap(keys, i, n, lo);
      for (int index = n; index > 1; --index)
      {
        GenericArraySortHelper<T>.Swap(keys, lo, lo + index - 1);
        GenericArraySortHelper<T>.DownHeap(keys, 1, index - 1, lo);
      }
    }

    private static void DownHeap(T[] keys, int i, int n, int lo)
    {
      T key = keys[lo + i - 1];
      int num;
      for (; i <= n / 2; i = num)
      {
        num = 2 * i;
        if (num < n && ((object) keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(keys[lo + num]) < 0))
          ++num;
        if ((object) keys[lo + num - 1] != null && keys[lo + num - 1].CompareTo(key) >= 0)
          keys[lo + i - 1] = keys[lo + num - 1];
        else
          break;
      }
      keys[lo + i - 1] = key;
    }

    private static void InsertionSort(T[] keys, int lo, int hi)
    {
      for (int index1 = lo; index1 < hi; ++index1)
      {
        int index2 = index1;
        T key;
        for (key = keys[index1 + 1]; index2 >= lo && ((object) key == null || key.CompareTo(keys[index2]) < 0); --index2)
          keys[index2 + 1] = keys[index2];
        keys[index2 + 1] = key;
      }
    }
  }
}
