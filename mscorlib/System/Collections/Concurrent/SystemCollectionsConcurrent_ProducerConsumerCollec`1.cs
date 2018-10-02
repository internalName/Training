// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections.Concurrent
{
  internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
  {
    private IProducerConsumerCollection<T> m_collection;

    public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this.m_collection = collection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
      get
      {
        return this.m_collection.ToArray();
      }
    }
  }
}
