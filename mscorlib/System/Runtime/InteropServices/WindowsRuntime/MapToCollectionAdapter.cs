// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.MapToCollectionAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class MapToCollectionAdapter
  {
    private MapToCollectionAdapter()
    {
    }

    [SecurityCritical]
    internal int Count<K, V>()
    {
      IMap<K, V> map = JitHelpers.UnsafeCast<object>((object) this) as IMap<K, V>;
      if (map != null)
      {
        uint size = map.Size;
        if ((uint) int.MaxValue < size)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
        return (int) size;
      }
      uint size1 = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>((object) this).Size;
      if ((uint) int.MaxValue < size1)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) size1;
    }

    [SecurityCritical]
    internal bool IsReadOnly<K, V>()
    {
      return false;
    }

    [SecurityCritical]
    internal void Add<K, V>(KeyValuePair<K, V> item)
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<object>((object) this) as IDictionary<K, V>;
      if (dictionary != null)
        dictionary.Add(item.Key, item.Value);
      else
        JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>((object) this).Append(item);
    }

    [SecurityCritical]
    internal void Clear<K, V>()
    {
      IMap<K, V> map = JitHelpers.UnsafeCast<object>((object) this) as IMap<K, V>;
      if (map != null)
        map.Clear();
      else
        JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>((object) this).Clear();
    }

    [SecurityCritical]
    internal bool Contains<K, V>(KeyValuePair<K, V> item)
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<object>((object) this) as IDictionary<K, V>;
      if (dictionary == null)
      {
        uint index;
        return JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>((object) this).IndexOf(item, out index);
      }
      V x;
      if (!dictionary.TryGetValue(item.Key, out x))
        return false;
      return EqualityComparer<V>.Default.Equals(x, item.Value);
    }

    [SecurityCritical]
    internal void CopyTo<K, V>(KeyValuePair<K, V>[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex));
      if (array.Length <= arrayIndex && this.Count<K, V>() > 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
      if (array.Length - arrayIndex < this.Count<K, V>())
        throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
      foreach (KeyValuePair<K, V> keyValuePair in (IEnumerable<KeyValuePair<K, V>>) JitHelpers.UnsafeCast<IIterable<KeyValuePair<K, V>>>((object) this))
        array[arrayIndex++] = keyValuePair;
    }

    [SecurityCritical]
    internal bool Remove<K, V>(KeyValuePair<K, V> item)
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<object>((object) this) as IDictionary<K, V>;
      if (dictionary != null)
        return dictionary.Remove(item.Key);
      IVector<KeyValuePair<K, V>> _this = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>((object) this);
      uint index;
      if (!_this.IndexOf(item, out index))
        return false;
      if ((uint) int.MaxValue < index)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      VectorToListAdapter.RemoveAtHelper<KeyValuePair<K, V>>(_this, index);
      return true;
    }
  }
}
