// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.IProducerConsumerQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
  internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
  {
    void Enqueue(T item);

    bool TryDequeue(out T result);

    bool IsEmpty { get; }

    int Count { get; }

    int GetCountSafe(object syncObj);
  }
}
