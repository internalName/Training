// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections.Generic
{
  internal sealed class Mscorlib_DictionaryValueCollectionDebugView<TKey, TValue>
  {
    private ICollection<TValue> collection;

    public Mscorlib_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      this.collection = collection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public TValue[] Items
    {
      get
      {
        TValue[] array = new TValue[this.collection.Count];
        this.collection.CopyTo(array, 0);
        return array;
      }
    }
  }
}
