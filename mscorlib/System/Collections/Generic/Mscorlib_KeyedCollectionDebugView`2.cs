// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Mscorlib_KeyedCollectionDebugView`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Collections.Generic
{
  internal sealed class Mscorlib_KeyedCollectionDebugView<K, T>
  {
    private KeyedCollection<K, T> kc;

    public Mscorlib_KeyedCollectionDebugView(KeyedCollection<K, T> keyedCollection)
    {
      if (keyedCollection == null)
        throw new ArgumentNullException(nameof (keyedCollection));
      this.kc = keyedCollection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
      get
      {
        T[] array = new T[this.kc.Count];
        this.kc.CopyTo(array, 0);
        return array;
      }
    }
  }
}
