// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ReadOnlyDictionaryValueCollection`2
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
  internal sealed class ReadOnlyDictionaryValueCollection<TKey, TValue> : IEnumerable<TValue>, IEnumerable
  {
    private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

    public ReadOnlyDictionaryValueCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      this.dictionary = dictionary;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<TValue> GetEnumerator()
    {
      return (IEnumerator<TValue>) new ReadOnlyDictionaryValueEnumerator<TKey, TValue>(this.dictionary);
    }
  }
}
