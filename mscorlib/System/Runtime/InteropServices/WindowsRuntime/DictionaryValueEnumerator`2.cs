// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryValueEnumerator`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Serializable]
  internal sealed class DictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
  {
    private readonly IDictionary<TKey, TValue> dictionary;
    private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;

    public DictionaryValueEnumerator(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      this.dictionary = dictionary;
      this.enumeration = dictionary.GetEnumerator();
    }

    void IDisposable.Dispose()
    {
      this.enumeration.Dispose();
    }

    public bool MoveNext()
    {
      return this.enumeration.MoveNext();
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public TValue Current
    {
      get
      {
        return this.enumeration.Current.Value;
      }
    }

    public void Reset()
    {
      this.enumeration = this.dictionary.GetEnumerator();
    }
  }
}
