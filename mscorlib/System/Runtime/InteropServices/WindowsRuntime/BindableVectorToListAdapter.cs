// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.BindableVectorToListAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class BindableVectorToListAdapter
  {
    private BindableVectorToListAdapter()
    {
    }

    [SecurityCritical]
    internal object Indexer_Get(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      return BindableVectorToListAdapter.GetAt(JitHelpers.UnsafeCast<IBindableVector>((object) this), (uint) index);
    }

    [SecurityCritical]
    internal void Indexer_Set(int index, object value)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      BindableVectorToListAdapter.SetAt(JitHelpers.UnsafeCast<IBindableVector>((object) this), (uint) index, value);
    }

    [SecurityCritical]
    internal int Add(object value)
    {
      IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>((object) this);
      bindableVector.Append(value);
      uint size = bindableVector.Size;
      if ((uint) int.MaxValue < size)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) size - 1;
    }

    [SecurityCritical]
    internal bool Contains(object item)
    {
      uint index;
      return JitHelpers.UnsafeCast<IBindableVector>((object) this).IndexOf(item, out index);
    }

    [SecurityCritical]
    internal void Clear()
    {
      JitHelpers.UnsafeCast<IBindableVector>((object) this).Clear();
    }

    [SecurityCritical]
    internal bool IsFixedSize()
    {
      return false;
    }

    [SecurityCritical]
    internal bool IsReadOnly()
    {
      return false;
    }

    [SecurityCritical]
    internal int IndexOf(object item)
    {
      uint index;
      if (!JitHelpers.UnsafeCast<IBindableVector>((object) this).IndexOf(item, out index))
        return -1;
      if ((uint) int.MaxValue < index)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) index;
    }

    [SecurityCritical]
    internal void Insert(int index, object item)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      BindableVectorToListAdapter.InsertAtHelper(JitHelpers.UnsafeCast<IBindableVector>((object) this), (uint) index, item);
    }

    [SecurityCritical]
    internal void Remove(object item)
    {
      IBindableVector _this = JitHelpers.UnsafeCast<IBindableVector>((object) this);
      uint index;
      if (!_this.IndexOf(item, out index))
        return;
      if ((uint) int.MaxValue < index)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      BindableVectorToListAdapter.RemoveAtHelper(_this, index);
    }

    [SecurityCritical]
    internal void RemoveAt(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      BindableVectorToListAdapter.RemoveAtHelper(JitHelpers.UnsafeCast<IBindableVector>((object) this), (uint) index);
    }

    private static object GetAt(IBindableVector _this, uint index)
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

    private static void SetAt(IBindableVector _this, uint index, object value)
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

    private static void InsertAtHelper(IBindableVector _this, uint index, object item)
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

    private static void RemoveAtHelper(IBindableVector _this, uint index)
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
