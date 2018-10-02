// Decompiled with JetBrains decompiler
// Type: System.Threading.PinnableBufferCacheEventSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Security;

namespace System.Threading
{
  [EventSource(Name = "Microsoft-DotNETRuntime-PinnableBufferCache")]
  internal sealed class PinnableBufferCacheEventSource : EventSource
  {
    public static readonly PinnableBufferCacheEventSource Log = new PinnableBufferCacheEventSource();

    [Event(1, Level = EventLevel.Verbose)]
    public void DebugMessage(string message)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(1, message);
    }

    [Event(2, Level = EventLevel.Verbose)]
    public void DebugMessage1(string message, long value)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(2, message, value);
    }

    [Event(3, Level = EventLevel.Verbose)]
    public void DebugMessage2(string message, long value1, long value2)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(3, new object[3]
      {
        (object) message,
        (object) value1,
        (object) value2
      });
    }

    [Event(18, Level = EventLevel.Verbose)]
    public void DebugMessage3(string message, long value1, long value2, long value3)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(18, (object) message, (object) value1, (object) value2, (object) value3);
    }

    [Event(4)]
    public void Create(string cacheName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(4, cacheName);
    }

    [Event(5, Level = EventLevel.Verbose)]
    public void AllocateBuffer(string cacheName, ulong objectId, int objectHash, int objectGen, int freeCountAfter)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(5, (object) cacheName, (object) objectId, (object) objectHash, (object) objectGen, (object) freeCountAfter);
    }

    [Event(6)]
    public void AllocateBufferFromNotGen2(string cacheName, int notGen2CountAfter)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(6, cacheName, notGen2CountAfter);
    }

    [Event(7)]
    public void AllocateBufferCreatingNewBuffers(string cacheName, int totalBuffsBefore, int objectCount)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(7, cacheName, totalBuffsBefore, objectCount);
    }

    [Event(8)]
    public void AllocateBufferAged(string cacheName, int agedCount)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(8, cacheName, agedCount);
    }

    [Event(9)]
    public void AllocateBufferFreeListEmpty(string cacheName, int notGen2CountBefore)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(9, cacheName, notGen2CountBefore);
    }

    [Event(10, Level = EventLevel.Verbose)]
    public void FreeBuffer(string cacheName, ulong objectId, int objectHash, int freeCountBefore)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(10, (object) cacheName, (object) objectId, (object) objectHash, (object) freeCountBefore);
    }

    [Event(11)]
    public void FreeBufferStillTooYoung(string cacheName, int notGen2CountBefore)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(11, cacheName, notGen2CountBefore);
    }

    [Event(13)]
    public void TrimCheck(string cacheName, int totalBuffs, bool neededMoreThanFreeList, int deltaMSec)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(13, (object) cacheName, (object) totalBuffs, (object) neededMoreThanFreeList, (object) deltaMSec);
    }

    [Event(14)]
    public void TrimFree(string cacheName, int totalBuffs, int freeListCount, int toBeFreed)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(14, (object) cacheName, (object) totalBuffs, (object) freeListCount, (object) toBeFreed);
    }

    [Event(15)]
    public void TrimExperiment(string cacheName, int totalBuffs, int freeListCount, int numTrimTrial)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(15, (object) cacheName, (object) totalBuffs, (object) freeListCount, (object) numTrimTrial);
    }

    [Event(16)]
    public void TrimFreeSizeOK(string cacheName, int totalBuffs, int freeListCount)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(16, cacheName, totalBuffs, freeListCount);
    }

    [Event(17)]
    public void TrimFlush(string cacheName, int totalBuffs, int freeListCount, int notGen2CountBefore)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(17, (object) cacheName, (object) totalBuffs, (object) freeListCount, (object) notGen2CountBefore);
    }

    [Event(20)]
    public void AgePendingBuffersResults(string cacheName, int promotedToFreeListCount, int heldBackCount)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(20, cacheName, promotedToFreeListCount, heldBackCount);
    }

    [Event(21)]
    public void WalkFreeListResult(string cacheName, int freeListCount, int gen0BuffersInFreeList)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(21, cacheName, freeListCount, gen0BuffersInFreeList);
    }

    [Event(22)]
    public void FreeBufferNull(string cacheName, int freeCountBefore)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(22, cacheName, freeCountBefore);
    }

    internal static ulong AddressOf(object obj)
    {
      byte[] array = obj as byte[];
      if (array != null)
        return (ulong) PinnableBufferCacheEventSource.AddressOfByteArray(array);
      return 0;
    }

    [SecuritySafeCritical]
    internal static unsafe long AddressOfByteArray(byte[] array)
    {
      if (array == null)
        return 0;
      fixed (byte* numPtr = array)
        return (long) (numPtr - 2 * sizeof (void*));
    }
  }
}
