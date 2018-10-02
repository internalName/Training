// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.MultiProducerMultiConsumerQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
  [DebuggerDisplay("Count = {Count}")]
  internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
  {
    void IProducerConsumerQueue<T>.Enqueue(T item)
    {
      this.Enqueue(item);
    }

    bool IProducerConsumerQueue<T>.TryDequeue(out T result)
    {
      return this.TryDequeue(out result);
    }

    bool IProducerConsumerQueue<T>.IsEmpty
    {
      get
      {
        return this.IsEmpty;
      }
    }

    int IProducerConsumerQueue<T>.Count
    {
      get
      {
        return this.Count;
      }
    }

    int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
    {
      return this.Count;
    }
  }
}
