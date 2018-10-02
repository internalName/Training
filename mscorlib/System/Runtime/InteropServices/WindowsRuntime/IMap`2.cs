// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IMap`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("3c2925fe-8519-45c1-aa79-197b6718c1c1")]
  [ComImport]
  internal interface IMap<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
  {
    V Lookup(K key);

    uint Size { get; }

    bool HasKey(K key);

    IReadOnlyDictionary<K, V> GetView();

    bool Insert(K key, V value);

    void Remove(K key);

    void Clear();
  }
}
