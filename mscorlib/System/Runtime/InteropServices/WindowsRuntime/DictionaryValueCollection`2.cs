// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryValueCollection`2
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
  internal sealed class DictionaryValueCollection<TKey, TValue> : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
  {
    private readonly IDictionary<TKey, TValue> dictionary;

    public DictionaryValueCollection(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      this.dictionary = dictionary;
    }

    public void CopyTo(TValue[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (array.Length <= index && this.Count > 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_IndexOutOfRangeException"));
      if (array.Length - index < this.dictionary.Count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
      int num = index;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) this.dictionary)
        array[num++] = keyValuePair.Value;
    }

    public int Count
    {
      get
      {
        return this.dictionary.Count;
      }
    }

    bool ICollection<TValue>.IsReadOnly
    {
      get
      {
        return true;
      }
    }

    void ICollection<TValue>.Add(TValue item)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
    }

    void ICollection<TValue>.Clear()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
    }

    public bool Contains(TValue item)
    {
      EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
      foreach (TValue y in this)
      {
        if (equalityComparer.Equals(item, y))
          return true;
      }
      return false;
    }

    bool ICollection<TValue>.Remove(TValue item)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<TValue> GetEnumerator()
    {
      return (IEnumerator<TValue>) new DictionaryValueEnumerator<TKey, TValue>(this.dictionary);
    }
  }
}
