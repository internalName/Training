// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ConstantSplittableMap`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  internal sealed class ConstantSplittableMap<TKey, TValue> : IMapView<TKey, TValue>, IIterable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable
  {
    private static readonly ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator keyValuePairComparator = new ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator();
    private readonly KeyValuePair<TKey, TValue>[] items;
    private readonly int firstItemIndex;
    private readonly int lastItemIndex;

    internal ConstantSplittableMap(IReadOnlyDictionary<TKey, TValue> data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this.firstItemIndex = 0;
      this.lastItemIndex = data.Count - 1;
      this.items = this.CreateKeyValueArray(data.Count, data.GetEnumerator());
    }

    internal ConstantSplittableMap(IMapView<TKey, TValue> data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if ((uint) int.MaxValue < data.Size)
      {
        Exception exception = (Exception) new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
      int size = (int) data.Size;
      this.firstItemIndex = 0;
      this.lastItemIndex = size - 1;
      this.items = this.CreateKeyValueArray(size, data.GetEnumerator());
    }

    private ConstantSplittableMap(KeyValuePair<TKey, TValue>[] items, int firstItemIndex, int lastItemIndex)
    {
      this.items = items;
      this.firstItemIndex = firstItemIndex;
      this.lastItemIndex = lastItemIndex;
    }

    private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<KeyValuePair<TKey, TValue>> data)
    {
      KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
      int num = 0;
      while (data.MoveNext())
        array[num++] = data.Current;
      Array.Sort<KeyValuePair<TKey, TValue>>(array, (IComparer<KeyValuePair<TKey, TValue>>) ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
      return array;
    }

    private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<IKeyValuePair<TKey, TValue>> data)
    {
      KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
      int num = 0;
      while (data.MoveNext())
      {
        IKeyValuePair<TKey, TValue> current = data.Current;
        array[num++] = new KeyValuePair<TKey, TValue>(current.Key, current.Value);
      }
      Array.Sort<KeyValuePair<TKey, TValue>>(array, (IComparer<KeyValuePair<TKey, TValue>>) ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
      return array;
    }

    public int Count
    {
      get
      {
        return this.lastItemIndex - this.firstItemIndex + 1;
      }
    }

    public uint Size
    {
      get
      {
        return (uint) (this.lastItemIndex - this.firstItemIndex + 1);
      }
    }

    public TValue Lookup(TKey key)
    {
      TValue obj;
      if (!this.TryGetValue(key, out obj))
      {
        Exception exception = (Exception) new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
      return obj;
    }

    public bool HasKey(TKey key)
    {
      TValue obj;
      return this.TryGetValue(key, out obj);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IIterator<IKeyValuePair<TKey, TValue>> First()
    {
      return (IIterator<IKeyValuePair<TKey, TValue>>) new EnumeratorToIteratorAdapter<IKeyValuePair<TKey, TValue>>(this.GetEnumerator());
    }

    public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return (IEnumerator<IKeyValuePair<TKey, TValue>>) new ConstantSplittableMap<TKey, TValue>.IKeyValuePairEnumerator(this.items, this.firstItemIndex, this.lastItemIndex);
    }

    public void Split(out IMapView<TKey, TValue> firstPartition, out IMapView<TKey, TValue> secondPartition)
    {
      if (this.Count < 2)
      {
        firstPartition = (IMapView<TKey, TValue>) null;
        secondPartition = (IMapView<TKey, TValue>) null;
      }
      else
      {
        int lastItemIndex = (int) (((long) this.firstItemIndex + (long) this.lastItemIndex) / 2L);
        firstPartition = (IMapView<TKey, TValue>) new ConstantSplittableMap<TKey, TValue>(this.items, this.firstItemIndex, lastItemIndex);
        secondPartition = (IMapView<TKey, TValue>) new ConstantSplittableMap<TKey, TValue>(this.items, lastItemIndex + 1, this.lastItemIndex);
      }
    }

    public bool ContainsKey(TKey key)
    {
      return Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, new KeyValuePair<TKey, TValue>(key, default (TValue)), (IComparer<KeyValuePair<TKey, TValue>>) ConstantSplittableMap<TKey, TValue>.keyValuePairComparator) >= 0;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      int index = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, new KeyValuePair<TKey, TValue>(key, default (TValue)), (IComparer<KeyValuePair<TKey, TValue>>) ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
      if (index < 0)
      {
        value = default (TValue);
        return false;
      }
      value = this.items[index].Value;
      return true;
    }

    public TValue this[TKey key]
    {
      get
      {
        return this.Lookup(key);
      }
    }

    public IEnumerable<TKey> Keys
    {
      get
      {
        throw new NotImplementedException("NYI");
      }
    }

    public IEnumerable<TValue> Values
    {
      get
      {
        throw new NotImplementedException("NYI");
      }
    }

    private class KeyValuePairComparator : IComparer<KeyValuePair<TKey, TValue>>
    {
      private static readonly IComparer<TKey> keyComparator = (IComparer<TKey>) Comparer<TKey>.Default;

      public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
      {
        return ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator.keyComparator.Compare(x.Key, y.Key);
      }
    }

    [Serializable]
    internal struct IKeyValuePairEnumerator : IEnumerator<IKeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
    {
      private KeyValuePair<TKey, TValue>[] _array;
      private int _start;
      private int _end;
      private int _current;

      internal IKeyValuePairEnumerator(KeyValuePair<TKey, TValue>[] items, int first, int end)
      {
        this._array = items;
        this._start = first;
        this._end = end;
        this._current = this._start - 1;
      }

      public bool MoveNext()
      {
        if (this._current >= this._end)
          return false;
        ++this._current;
        return true;
      }

      public IKeyValuePair<TKey, TValue> Current
      {
        get
        {
          if (this._current < this._start)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._current > this._end)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return (IKeyValuePair<TKey, TValue>) new CLRIKeyValuePairImpl<TKey, TValue>(ref this._array[this._current]);
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      void IEnumerator.Reset()
      {
        this._current = this._start - 1;
      }

      public void Dispose()
      {
      }
    }
  }
}
