// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ReadOnlyDictionaryKeyCollection`2
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
  internal sealed class ReadOnlyDictionaryKeyCollection<TKey, TValue> : IEnumerable<TKey>, IEnumerable
  {
    private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

    public ReadOnlyDictionaryKeyCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      this.dictionary = dictionary;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<TKey> GetEnumerator()
    {
      return (IEnumerator<TKey>) new ReadOnlyDictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
    }
  }
}
