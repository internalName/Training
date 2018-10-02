// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.CDSCollectionETWBCLProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
  [FriendAccessAllowed]
  [EventSource(Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E", LocalizationResources = "mscorlib", Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource")]
  internal sealed class CDSCollectionETWBCLProvider : EventSource
  {
    public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();
    private const EventKeywords ALL_KEYWORDS = EventKeywords.All;
    private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;
    private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;
    private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;
    private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;
    private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;

    private CDSCollectionETWBCLProvider()
    {
    }

    [Event(1, Level = EventLevel.Warning)]
    public void ConcurrentStack_FastPushFailed(int spinCount)
    {
      if (!this.IsEnabled(EventLevel.Warning, EventKeywords.All))
        return;
      this.WriteEvent(1, spinCount);
    }

    [Event(2, Level = EventLevel.Warning)]
    public void ConcurrentStack_FastPopFailed(int spinCount)
    {
      if (!this.IsEnabled(EventLevel.Warning, EventKeywords.All))
        return;
      this.WriteEvent(2, spinCount);
    }

    [Event(3, Level = EventLevel.Warning)]
    public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
    {
      if (!this.IsEnabled(EventLevel.Warning, EventKeywords.All))
        return;
      this.WriteEvent(3, numOfBuckets);
    }

    [Event(4, Level = EventLevel.Verbose)]
    public void ConcurrentBag_TryTakeSteals()
    {
      if (!this.IsEnabled(EventLevel.Verbose, EventKeywords.All))
        return;
      this.WriteEvent(4);
    }

    [Event(5, Level = EventLevel.Verbose)]
    public void ConcurrentBag_TryPeekSteals()
    {
      if (!this.IsEnabled(EventLevel.Verbose, EventKeywords.All))
        return;
      this.WriteEvent(5);
    }
  }
}
