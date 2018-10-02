// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IMapView`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("e480ce40-a338-4ada-adcf-272272e48cb9")]
  [ComImport]
  internal interface IMapView<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
  {
    V Lookup(K key);

    uint Size { get; }

    bool HasKey(K key);

    void Split(out IMapView<K, V> first, out IMapView<K, V> second);
  }
}
