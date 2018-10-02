// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ArraySortHelper`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Collections.Generic
{
  [TypeDependency("System.Collections.Generic.GenericArraySortHelper`1")]
  internal class ArraySortHelper<T> : IArraySortHelper<T>
  {
    private static volatile IArraySortHelper<T> defaultArraySortHelper;

    public static IArraySortHelper<T> Default
    {
      get
      {
        return ArraySortHelper<T>.defaultArraySortHelper ?? ArraySortHelper<T>.CreateArraySortHelper();
      }
    }

    [SecuritySafeCritical]
    private static IArraySortHelper<T> CreateArraySortHelper()
    {
      if (typeof (IComparable<T>).IsAssignableFrom(typeof (T)))
        ArraySortHelper<T>.defaultArraySortHelper = (IArraySortHelper<T>) RuntimeTypeHandle.Allocate(typeof (GenericArraySortHelper<string>).TypeHandle.Instantiate(new Type[1]
        {
          typeof (T)
        }));
      else
        ArraySortHelper<T>.defaultArraySortHelper = (IArraySortHelper<T>) new ArraySortHelper<T>();
      return ArraySortHelper<T>.defaultArraySortHelper;
    }

    public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
    {
      try
      {
        if (comparer == null)
          comparer = (IComparer<T>) Comparer<T>.Default;
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
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
        if (comparer == null)
          comparer = (IComparer<T>) Comparer<T>.Default;
        return ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
      }
    }

    internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
    {
      int num1 = index;
      int num2 = index + length - 1;
      while (num1 <= num2)
      {
        int index1 = num1 + (num2 - num1 >> 1);
        int num3 = comparer.Compare(array[index1], value);
        if (num3 == 0)
          return index1;
        if (num3 < 0)
          num1 = index1 + 1;
        else
          num2 = index1 - 1;
      }
      return ~num1;
    }

    private static void SwapIfGreater(T[] keys, IComparer<T> comparer, int a, int b)
    {
      if (a == b || comparer.Compare(keys[a], keys[b]) <= 0)
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

    internal static void DepthLimitedQuickSort(T[] keys, int left, int right, IComparer<T> comparer, int depthLimit)
    {
      while (depthLimit != 0)
      {
        int index1 = left;
        int index2 = right;
        int index3 = index1 + (index2 - index1 >> 1);
        ArraySortHelper<T>.SwapIfGreater(keys, comparer, index1, index3);
        ArraySortHelper<T>.SwapIfGreater(keys, comparer, index1, index2);
        ArraySortHelper<T>.SwapIfGreater(keys, comparer, index3, index2);
        T key1 = keys[index3];
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
            ArraySortHelper<T>.DepthLimitedQuickSort(keys, left, index2, comparer, depthLimit);
          left = index1;
        }
        else
        {
          if (index1 < right)
            ArraySortHelper<T>.DepthLimitedQuickSort(keys, index1, right, comparer, depthLimit);
          right = index2;
        }
        if (left >= right)
          return;
      }
      ArraySortHelper<T>.Heapsort(keys, left, right, comparer);
    }

    internal static void IntrospectiveSort(T[] keys, int left, int length, IComparer<T> comparer)
    {
      if (length < 2)
        return;
      ArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
    }

    private static void IntroSort(T[] keys, int lo, int hi, int depthLimit, IComparer<T> comparer)
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
            ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
            break;
          }
          if (num2 == 3)
          {
            ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi - 1);
            ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
            ArraySortHelper<T>.SwapIfGreater(keys, comparer, hi - 1, hi);
            break;
          }
          ArraySortHelper<T>.InsertionSort(keys, lo, hi, comparer);
          break;
        }
        if (depthLimit == 0)
        {
          ArraySortHelper<T>.Heapsort(keys, lo, hi, comparer);
          break;
        }
        --depthLimit;
        num1 = ArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi, comparer);
        ArraySortHelper<T>.IntroSort(keys, num1 + 1, hi, depthLimit, comparer);
      }
    }

    private static int PickPivotAndPartition(T[] keys, int lo, int hi, IComparer<T> comparer)
    {
      int index = lo + (hi - lo) / 2;
      ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, index);
      ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
      ArraySortHelper<T>.SwapIfGreater(keys, comparer, index, hi);
      T key = keys[index];
      ArraySortHelper<T>.Swap(keys, index, hi - 1);
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
          ArraySortHelper<T>.Swap(keys, i, j);
        else
          break;
      }
      ArraySortHelper<T>.Swap(keys, i, hi - 1);
      return i;
    }

    private static void Heapsort(T[] keys, int lo, int hi, IComparer<T> comparer)
    {
      int n = hi - lo + 1;
      for (int i = n / 2; i >= 1; --i)
        ArraySortHelper<T>.DownHeap(keys, i, n, lo, comparer);
      for (int index = n; index > 1; --index)
      {
        ArraySortHelper<T>.Swap(keys, lo, lo + index - 1);
        ArraySortHelper<T>.DownHeap(keys, 1, index - 1, lo, comparer);
      }
    }

    private static void DownHeap(T[] keys, int i, int n, int lo, IComparer<T> comparer)
    {
      T key = keys[lo + i - 1];
      int num;
      for (; i <= n / 2; i = num)
      {
        num = 2 * i;
        if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
          ++num;
        if (comparer.Compare(key, keys[lo + num - 1]) < 0)
          keys[lo + i - 1] = keys[lo + num - 1];
        else
          break;
      }
      keys[lo + i - 1] = key;
    }

    private static void InsertionSort(T[] keys, int lo, int hi, IComparer<T> comparer)
    {
      for (int index1 = lo; index1 < hi; ++index1)
      {
        int index2 = index1;
        T key;
        for (key = keys[index1 + 1]; index2 >= lo && comparer.Compare(key, keys[index2]) < 0; --index2)
          keys[index2 + 1] = keys[index2];
        keys[index2 + 1] = key;
      }
    }
  }
}
