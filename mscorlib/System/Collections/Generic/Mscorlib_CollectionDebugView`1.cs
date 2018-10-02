// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Mscorlib_CollectionDebugView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections.Generic
{
  internal sealed class Mscorlib_CollectionDebugView<T>
  {
    private ICollection<T> collection;

    public Mscorlib_CollectionDebugView(ICollection<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      this.collection = collection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
      get
      {
        T[] array = new T[this.collection.Count];
        this.collection.CopyTo(array, 0);
        return array;
      }
    }
  }
}
