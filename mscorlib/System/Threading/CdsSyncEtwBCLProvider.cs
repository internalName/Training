// Decompiled with JetBrains decompiler
// Type: System.Threading.CdsSyncEtwBCLProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
  [FriendAccessAllowed]
  [EventSource(Guid = "EC631D38-466B-4290-9306-834971BA0217", LocalizationResources = "mscorlib", Name = "System.Threading.SynchronizationEventSource")]
  internal sealed class CdsSyncEtwBCLProvider : EventSource
  {
    public static CdsSyncEtwBCLProvider Log = new CdsSyncEtwBCLProvider();
    private const EventKeywords ALL_KEYWORDS = EventKeywords.All;
    private const int SPINLOCK_FASTPATHFAILED_ID = 1;
    private const int SPINWAIT_NEXTSPINWILLYIELD_ID = 2;
    private const int BARRIER_PHASEFINISHED_ID = 3;

    private CdsSyncEtwBCLProvider()
    {
    }

    [Event(1, Level = EventLevel.Warning)]
    public void SpinLock_FastPathFailed(int ownerID)
    {
      if (!this.IsEnabled(EventLevel.Warning, EventKeywords.All))
        return;
      this.WriteEvent(1, ownerID);
    }

    [Event(2, Level = EventLevel.Informational)]
    public void SpinWait_NextSpinWillYield()
    {
      if (!this.IsEnabled(EventLevel.Informational, EventKeywords.All))
        return;
      this.WriteEvent(2);
    }

    [SecuritySafeCritical]
    [Event(3, Level = EventLevel.Verbose, Version = 1)]
    public unsafe void Barrier_PhaseFinished(bool currentSense, long phaseNum)
    {
      if (!this.IsEnabled(EventLevel.Verbose, EventKeywords.All))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      int num = currentSense ? 1 : 0;
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &num);
      data[1].Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &phaseNum);
      this.WriteEventCore(3, 2, data);
    }
  }
}
