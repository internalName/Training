// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.VectorToCollectionAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class VectorToCollectionAdapter
  {
    private VectorToCollectionAdapter()
    {
    }

    [SecurityCritical]
    internal int Count<T>()
    {
      uint size = JitHelpers.UnsafeCast<IVector<T>>((object) this).Size;
      if ((uint) int.MaxValue < size)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) size;
    }

    [SecurityCritical]
    internal bool IsReadOnly<T>()
    {
      return false;
    }

    [SecurityCritical]
    internal void Add<T>(T item)
    {
      JitHelpers.UnsafeCast<IVector<T>>((object) this).Append(item);
    }

    [SecurityCritical]
    internal void Clear<T>()
    {
      JitHelpers.UnsafeCast<IVector<T>>((object) this).Clear();
    }

    [SecurityCritical]
    internal bool Contains<T>(T item)
    {
      uint index;
      return JitHelpers.UnsafeCast<IVector<T>>((object) this).IndexOf(item, out index);
    }

    [SecurityCritical]
    internal void CopyTo<T>(T[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex));
      if (array.Length <= arrayIndex && this.Count<T>() > 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
      if (array.Length - arrayIndex < this.Count<T>())
        throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
      IVector<T> _this = JitHelpers.UnsafeCast<IVector<T>>((object) this);
      int num = this.Count<T>();
      for (int index = 0; index < num; ++index)
        array[index + arrayIndex] = VectorToListAdapter.GetAt<T>(_this, (uint) index);
    }

    [SecurityCritical]
    internal bool Remove<T>(T item)
    {
      IVector<T> _this = JitHelpers.UnsafeCast<IVector<T>>((object) this);
      uint index;
      if (!_this.IndexOf(item, out index))
        return false;
      if ((uint) int.MaxValue < index)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      VectorToListAdapter.RemoveAtHelper<T>(_this, index);
      return true;
    }
  }
}
