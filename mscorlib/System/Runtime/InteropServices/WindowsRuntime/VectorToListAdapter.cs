// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.VectorToListAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class VectorToListAdapter
  {
    private VectorToListAdapter()
    {
    }

    [SecurityCritical]
    internal T Indexer_Get<T>(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      return VectorToListAdapter.GetAt<T>(JitHelpers.UnsafeCast<IVector<T>>((object) this), (uint) index);
    }

    [SecurityCritical]
    internal void Indexer_Set<T>(int index, T value)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      VectorToListAdapter.SetAt<T>(JitHelpers.UnsafeCast<IVector<T>>((object) this), (uint) index, value);
    }

    [SecurityCritical]
    internal int IndexOf<T>(T item)
    {
      uint index;
      if (!JitHelpers.UnsafeCast<IVector<T>>((object) this).IndexOf(item, out index))
        return -1;
      if ((uint) int.MaxValue < index)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) index;
    }

    [SecurityCritical]
    internal void Insert<T>(int index, T item)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      VectorToListAdapter.InsertAtHelper<T>(JitHelpers.UnsafeCast<IVector<T>>((object) this), (uint) index, item);
    }

    [SecurityCritical]
    internal void RemoveAt<T>(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      VectorToListAdapter.RemoveAtHelper<T>(JitHelpers.UnsafeCast<IVector<T>>((object) this), (uint) index);
    }

    internal static T GetAt<T>(IVector<T> _this, uint index)
    {
      try
      {
        return _this.GetAt(index);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new ArgumentOutOfRangeException(nameof (index));
        throw;
      }
    }

    private static void SetAt<T>(IVector<T> _this, uint index, T value)
    {
      try
      {
        _this.SetAt(index, value);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new ArgumentOutOfRangeException(nameof (index));
        throw;
      }
    }

    private static void InsertAtHelper<T>(IVector<T> _this, uint index, T item)
    {
      try
      {
        _this.InsertAt(index, item);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new ArgumentOutOfRangeException(nameof (index));
        throw;
      }
    }

    internal static void RemoveAtHelper<T>(IVector<T> _this, uint index)
    {
      try
      {
        _this.RemoveAt(index);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new ArgumentOutOfRangeException(nameof (index));
        throw;
      }
    }
  }
}
