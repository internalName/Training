// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LeaseManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
  internal class LeaseManager
  {
    private Hashtable leaseToTimeTable = new Hashtable();
    private Hashtable sponsorTable = new Hashtable();
    private ArrayList tempObjects = new ArrayList(10);
    private TimeSpan pollTime;
    private AutoResetEvent waitHandle;
    private TimerCallback leaseTimeAnalyzerDelegate;
    private volatile Timer leaseTimer;

    internal static bool IsInitialized()
    {
      return Thread.GetDomain().RemotingData.LeaseManager != null;
    }

    [SecurityCritical]
    internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
    {
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      LeaseManager leaseManager = remotingData.LeaseManager;
      if (leaseManager == null)
      {
        lock (remotingData)
        {
          if (remotingData.LeaseManager == null)
            remotingData.LeaseManager = new LeaseManager(pollTime);
          leaseManager = remotingData.LeaseManager;
        }
      }
      return leaseManager;
    }

    internal static LeaseManager GetLeaseManager()
    {
      return Thread.GetDomain().RemotingData.LeaseManager;
    }

    [SecurityCritical]
    private LeaseManager(TimeSpan pollTime)
    {
      this.pollTime = pollTime;
      this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
      this.waitHandle = new AutoResetEvent(false);
      this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, (object) null, -1, -1);
      this.leaseTimer.Change((int) pollTime.TotalMilliseconds, -1);
    }

    internal void ChangePollTime(TimeSpan pollTime)
    {
      this.pollTime = pollTime;
    }

    internal void ActivateLease(Lease lease)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable[(object) lease] = (object) lease.leaseTime;
    }

    internal void DeleteLease(Lease lease)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable.Remove((object) lease);
    }

    [Conditional("_LOGGING")]
    internal void DumpLeases(Lease[] leases)
    {
      int num = 0;
      while (num < leases.Length)
        ++num;
    }

    internal ILease GetLease(MarshalByRefObject obj)
    {
      bool fServer = true;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      if (identity == null)
        return (ILease) null;
      return (ILease) identity.Lease;
    }

    internal void ChangedLeaseTime(Lease lease, DateTime newTime)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable[(object) lease] = (object) newTime;
    }

    internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
    {
      lock (this.sponsorTable)
      {
        DateTime sponsorWaitTime = DateTime.UtcNow.Add(sponsorshipTimeOut);
        this.sponsorTable[sponsorId] = (object) new LeaseManager.SponsorInfo(lease, sponsorId, sponsorWaitTime);
      }
    }

    internal void DeleteSponsor(object sponsorId)
    {
      lock (this.sponsorTable)
        this.sponsorTable.Remove(sponsorId);
    }

    [SecurityCritical]
    private void LeaseTimeAnalyzer(object state)
    {
      DateTime utcNow = DateTime.UtcNow;
      lock (this.leaseToTimeTable)
      {
        IDictionaryEnumerator enumerator = this.leaseToTimeTable.GetEnumerator();
        while (enumerator.MoveNext())
        {
          DateTime dateTime = (DateTime) enumerator.Value;
          Lease key = (Lease) enumerator.Key;
          if (dateTime.CompareTo(utcNow) < 0)
            this.tempObjects.Add((object) key);
        }
        for (int index = 0; index < this.tempObjects.Count; ++index)
          this.leaseToTimeTable.Remove((object) (Lease) this.tempObjects[index]);
      }
      for (int index = 0; index < this.tempObjects.Count; ++index)
        ((Lease) this.tempObjects[index])?.LeaseExpired(utcNow);
      this.tempObjects.Clear();
      lock (this.sponsorTable)
      {
        IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
        while (enumerator.MoveNext())
        {
          object key = enumerator.Key;
          LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo) enumerator.Value;
          if (sponsorInfo.sponsorWaitTime.CompareTo(utcNow) < 0)
            this.tempObjects.Add((object) sponsorInfo);
        }
        for (int index = 0; index < this.tempObjects.Count; ++index)
          this.sponsorTable.Remove(((LeaseManager.SponsorInfo) this.tempObjects[index]).sponsorId);
      }
      for (int index = 0; index < this.tempObjects.Count; ++index)
      {
        LeaseManager.SponsorInfo tempObject = (LeaseManager.SponsorInfo) this.tempObjects[index];
        if (tempObject != null && tempObject.lease != null)
        {
          tempObject.lease.SponsorTimeout(tempObject.sponsorId);
          this.tempObjects[index] = (object) null;
        }
      }
      this.tempObjects.Clear();
      this.leaseTimer.Change((int) this.pollTime.TotalMilliseconds, -1);
    }

    internal class SponsorInfo
    {
      internal Lease lease;
      internal object sponsorId;
      internal DateTime sponsorWaitTime;

      internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
      {
        this.lease = lease;
        this.sponsorId = sponsorId;
        this.sponsorWaitTime = sponsorWaitTime;
      }
    }
  }
}
