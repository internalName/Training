// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ListToVectorAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class ListToVectorAdapter
  {
    private ListToVectorAdapter()
    {
    }

    [SecurityCritical]
    internal T GetAt<T>(uint index)
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      ListToVectorAdapter.EnsureIndexInt32(index, objList.Count);
      try
      {
        return objList[(int) index];
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) ex, "ArgumentOutOfRange_IndexOutOfRange");
      }
    }

    [SecurityCritical]
    internal uint Size<T>()
    {
      return (uint) JitHelpers.UnsafeCast<IList<T>>((object) this).Count;
    }

    [SecurityCritical]
    internal IReadOnlyList<T> GetView<T>()
    {
      IList<T> list = JitHelpers.UnsafeCast<IList<T>>((object) this);
      return list as IReadOnlyList<T> ?? (IReadOnlyList<T>) new ReadOnlyCollection<T>(list);
    }

    [SecurityCritical]
    internal bool IndexOf<T>(T value, out uint index)
    {
      int num = JitHelpers.UnsafeCast<IList<T>>((object) this).IndexOf(value);
      if (-1 == num)
      {
        index = 0U;
        return false;
      }
      index = (uint) num;
      return true;
    }

    [SecurityCritical]
    internal void SetAt<T>(uint index, T value)
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      ListToVectorAdapter.EnsureIndexInt32(index, objList.Count);
      try
      {
        objList[(int) index] = value;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) ex, "ArgumentOutOfRange_IndexOutOfRange");
      }
    }

    [SecurityCritical]
    internal void InsertAt<T>(uint index, T value)
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      ListToVectorAdapter.EnsureIndexInt32(index, objList.Count + 1);
      try
      {
        objList.Insert((int) index, value);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        ex.SetErrorCode(-2147483637);
        throw;
      }
    }

    [SecurityCritical]
    internal void RemoveAt<T>(uint index)
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      ListToVectorAdapter.EnsureIndexInt32(index, objList.Count);
      try
      {
        objList.RemoveAt((int) index);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        ex.SetErrorCode(-2147483637);
        throw;
      }
    }

    [SecurityCritical]
    internal void Append<T>(T value)
    {
      JitHelpers.UnsafeCast<IList<T>>((object) this).Add(value);
    }

    [SecurityCritical]
    internal void RemoveAtEnd<T>()
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      if (objList.Count == 0)
      {
        Exception exception = (Exception) new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
      this.RemoveAt<T>((uint) objList.Count - 1U);
    }

    [SecurityCritical]
    internal void Clear<T>()
    {
      JitHelpers.UnsafeCast<IList<T>>((object) this).Clear();
    }

    [SecurityCritical]
    internal uint GetMany<T>(uint startIndex, T[] items)
    {
      return ListToVectorAdapter.GetManyHelper<T>(JitHelpers.UnsafeCast<IList<T>>((object) this), startIndex, items);
    }

    [SecurityCritical]
    internal void ReplaceAll<T>(T[] items)
    {
      IList<T> objList = JitHelpers.UnsafeCast<IList<T>>((object) this);
      objList.Clear();
      if (items == null)
        return;
      foreach (T obj in items)
        objList.Add(obj);
    }

    private static void EnsureIndexInt32(uint index, int listCapacity)
    {
      if ((uint) int.MaxValue <= index || index >= (uint) listCapacity)
      {
        Exception exception = (Exception) new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
    }

    private static uint GetManyHelper<T>(IList<T> sourceList, uint startIndex, T[] items)
    {
      if ((long) startIndex == (long) sourceList.Count)
        return 0;
      ListToVectorAdapter.EnsureIndexInt32(startIndex, sourceList.Count);
      if (items == null)
        return 0;
      uint num = Math.Min((uint) items.Length, (uint) sourceList.Count - startIndex);
      for (uint index = 0; index < num; ++index)
        items[(int) index] = sourceList[(int) index + (int) startIndex];
      if (typeof (T) == typeof (string))
      {
        string[] strArray = items as string[];
        for (uint index = num; (long) index < (long) items.Length; ++index)
          strArray[(int) index] = string.Empty;
      }
      return num;
    }
  }
}
