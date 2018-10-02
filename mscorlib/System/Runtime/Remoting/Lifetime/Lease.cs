// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.Lease
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
  internal class Lease : MarshalByRefObject, ILease
  {
    internal int id;
    internal DateTime leaseTime;
    internal TimeSpan initialLeaseTime;
    internal TimeSpan renewOnCallTime;
    internal TimeSpan sponsorshipTimeout;
    internal Hashtable sponsorTable;
    internal int sponsorCallThread;
    internal LeaseManager leaseManager;
    internal MarshalByRefObject managedObject;
    internal LeaseState state;
    internal static volatile int nextId;

    internal Lease(TimeSpan initialLeaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject managedObject)
    {
      this.id = Lease.nextId++;
      this.renewOnCallTime = renewOnCallTime;
      this.sponsorshipTimeout = sponsorshipTimeout;
      this.initialLeaseTime = initialLeaseTime;
      this.managedObject = managedObject;
      this.leaseManager = LeaseManager.GetLeaseManager();
      this.sponsorTable = new Hashtable(10);
      this.state = LeaseState.Initial;
    }

    internal void ActivateLease()
    {
      this.leaseTime = DateTime.UtcNow.Add(this.initialLeaseTime);
      this.state = LeaseState.Active;
      this.leaseManager.ActivateLease(this);
    }

    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }

    public TimeSpan RenewOnCallTime
    {
      [SecurityCritical] get
      {
        return this.renewOnCallTime;
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        if (this.state == LeaseState.Initial)
          this.renewOnCallTime = value;
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateRenewOnCall", (object) this.state.ToString()));
      }
    }

    public TimeSpan SponsorshipTimeout
    {
      [SecurityCritical] get
      {
        return this.sponsorshipTimeout;
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        if (this.state == LeaseState.Initial)
          this.sponsorshipTimeout = value;
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateSponsorshipTimeout", (object) this.state.ToString()));
      }
    }

    public TimeSpan InitialLeaseTime
    {
      [SecurityCritical] get
      {
        return this.initialLeaseTime;
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        if (this.state == LeaseState.Initial)
        {
          this.initialLeaseTime = value;
          if (TimeSpan.Zero.CompareTo(value) < 0)
            return;
          this.state = LeaseState.Null;
        }
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateInitialLeaseTime", (object) this.state.ToString()));
      }
    }

    public TimeSpan CurrentLeaseTime
    {
      [SecurityCritical] get
      {
        return this.leaseTime.Subtract(DateTime.UtcNow);
      }
    }

    public LeaseState CurrentState
    {
      [SecurityCritical] get
      {
        return this.state;
      }
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public void Register(ISponsor obj)
    {
      this.Register(obj, TimeSpan.Zero);
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public void Register(ISponsor obj, TimeSpan renewalTime)
    {
      lock (this)
      {
        if (this.state == LeaseState.Expired || this.sponsorshipTimeout == TimeSpan.Zero)
          return;
        object sponsorId = this.GetSponsorId(obj);
        lock (this.sponsorTable)
        {
          if (renewalTime > TimeSpan.Zero)
            this.AddTime(renewalTime);
          if (this.sponsorTable.ContainsKey(sponsorId))
            return;
          this.sponsorTable[sponsorId] = (object) new Lease.SponsorStateInfo(renewalTime, Lease.SponsorState.Initial);
        }
      }
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public void Unregister(ISponsor sponsor)
    {
      lock (this)
      {
        if (this.state == LeaseState.Expired)
          return;
        object sponsorId = this.GetSponsorId(sponsor);
        lock (this.sponsorTable)
        {
          if (sponsorId == null)
            return;
          this.leaseManager.DeleteSponsor(sponsorId);
          Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo) this.sponsorTable[sponsorId];
          this.sponsorTable.Remove(sponsorId);
        }
      }
    }

    [SecurityCritical]
    private object GetSponsorId(ISponsor obj)
    {
      object obj1 = (object) null;
      if (obj != null)
        obj1 = !RemotingServices.IsTransparentProxy((object) obj) ? (object) obj : (object) RemotingServices.GetRealProxy((object) obj);
      return obj1;
    }

    [SecurityCritical]
    private ISponsor GetSponsorFromId(object sponsorId)
    {
      RealProxy realProxy = sponsorId as RealProxy;
      return realProxy == null ? (ISponsor) sponsorId : (ISponsor) realProxy.GetTransparentProxy();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public TimeSpan Renew(TimeSpan renewalTime)
    {
      return this.RenewInternal(renewalTime);
    }

    internal TimeSpan RenewInternal(TimeSpan renewalTime)
    {
      lock (this)
      {
        if (this.state == LeaseState.Expired)
          return TimeSpan.Zero;
        this.AddTime(renewalTime);
        return this.leaseTime.Subtract(DateTime.UtcNow);
      }
    }

    internal void Remove()
    {
      if (this.state == LeaseState.Expired)
        return;
      this.state = LeaseState.Expired;
      this.leaseManager.DeleteLease(this);
    }

    [SecurityCritical]
    internal void Cancel()
    {
      lock (this)
      {
        if (this.state == LeaseState.Expired)
          return;
        this.Remove();
        RemotingServices.Disconnect(this.managedObject, false);
        RemotingServices.Disconnect((MarshalByRefObject) this);
      }
    }

    internal void RenewOnCall()
    {
      lock (this)
      {
        if (this.state == LeaseState.Initial || this.state == LeaseState.Expired)
          return;
        this.AddTime(this.renewOnCallTime);
      }
    }

    [SecurityCritical]
    internal void LeaseExpired(DateTime now)
    {
      lock (this)
      {
        if (this.state == LeaseState.Expired || this.leaseTime.CompareTo(now) >= 0)
          return;
        this.ProcessNextSponsor();
      }
    }

    [SecurityCritical]
    internal void SponsorCall(ISponsor sponsor)
    {
      bool flag = false;
      if (this.state == LeaseState.Expired)
        return;
      lock (this.sponsorTable)
      {
        try
        {
          object sponsorId = this.GetSponsorId(sponsor);
          this.sponsorCallThread = Thread.CurrentThread.GetHashCode();
          Lease.AsyncRenewal asyncRenewal = new Lease.AsyncRenewal(sponsor.Renewal);
          Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo) this.sponsorTable[sponsorId];
          sponsorStateInfo.sponsorState = Lease.SponsorState.Waiting;
          asyncRenewal.BeginInvoke((ILease) this, new AsyncCallback(this.SponsorCallback), (object) null);
          if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting && this.state != LeaseState.Expired)
            this.leaseManager.RegisterSponsorCall(this, sponsorId, this.sponsorshipTimeout);
          this.sponsorCallThread = 0;
        }
        catch (Exception ex)
        {
          flag = true;
          this.sponsorCallThread = 0;
        }
      }
      if (!flag)
        return;
      this.Unregister(sponsor);
      this.ProcessNextSponsor();
    }

    [SecurityCritical]
    internal void SponsorTimeout(object sponsorId)
    {
      lock (this)
      {
        if (!this.sponsorTable.ContainsKey(sponsorId))
          return;
        lock (this.sponsorTable)
        {
          if (((Lease.SponsorStateInfo) this.sponsorTable[sponsorId]).sponsorState != Lease.SponsorState.Waiting)
            return;
          this.Unregister(this.GetSponsorFromId(sponsorId));
          this.ProcessNextSponsor();
        }
      }
    }

    [SecurityCritical]
    private void ProcessNextSponsor()
    {
      object sponsorId = (object) null;
      TimeSpan timeSpan = TimeSpan.Zero;
      lock (this.sponsorTable)
      {
        IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
        while (enumerator.MoveNext())
        {
          object key = enumerator.Key;
          Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo) enumerator.Value;
          if (sponsorStateInfo.sponsorState == Lease.SponsorState.Initial && timeSpan == TimeSpan.Zero)
          {
            timeSpan = sponsorStateInfo.renewalTime;
            sponsorId = key;
          }
          else if (sponsorStateInfo.renewalTime > timeSpan)
          {
            timeSpan = sponsorStateInfo.renewalTime;
            sponsorId = key;
          }
        }
      }
      if (sponsorId != null)
        this.SponsorCall(this.GetSponsorFromId(sponsorId));
      else
        this.Cancel();
    }

    [SecurityCritical]
    internal void SponsorCallback(object obj)
    {
      this.SponsorCallback((IAsyncResult) obj);
    }

    [SecurityCritical]
    internal void SponsorCallback(IAsyncResult iar)
    {
      if (this.state == LeaseState.Expired)
        return;
      if (Thread.CurrentThread.GetHashCode() == this.sponsorCallThread)
      {
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.SponsorCallback), (object) iar);
      }
      else
      {
        Lease.AsyncRenewal asyncDelegate = (Lease.AsyncRenewal) ((AsyncResult) iar).AsyncDelegate;
        ISponsor target = (ISponsor) asyncDelegate.Target;
        Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo) null;
        if (iar.IsCompleted)
        {
          bool flag = false;
          TimeSpan timeSpan = TimeSpan.Zero;
          try
          {
            timeSpan = asyncDelegate.EndInvoke(iar);
          }
          catch (Exception ex)
          {
            flag = true;
          }
          if (flag)
          {
            this.Unregister(target);
            this.ProcessNextSponsor();
          }
          else
          {
            object sponsorId = this.GetSponsorId(target);
            lock (this.sponsorTable)
            {
              if (this.sponsorTable.ContainsKey(sponsorId))
              {
                sponsorStateInfo = (Lease.SponsorStateInfo) this.sponsorTable[sponsorId];
                sponsorStateInfo.sponsorState = Lease.SponsorState.Completed;
                sponsorStateInfo.renewalTime = timeSpan;
              }
            }
            if (sponsorStateInfo == null)
              this.ProcessNextSponsor();
            else if (sponsorStateInfo.renewalTime == TimeSpan.Zero)
            {
              this.Unregister(target);
              this.ProcessNextSponsor();
            }
            else
              this.RenewInternal(sponsorStateInfo.renewalTime);
          }
        }
        else
        {
          this.Unregister(target);
          this.ProcessNextSponsor();
        }
      }
    }

    private void AddTime(TimeSpan renewalSpan)
    {
      if (this.state == LeaseState.Expired)
        return;
      DateTime utcNow = DateTime.UtcNow;
      DateTime leaseTime = this.leaseTime;
      DateTime newTime = utcNow.Add(renewalSpan);
      if (this.leaseTime.CompareTo(newTime) >= 0)
        return;
      this.leaseManager.ChangedLeaseTime(this, newTime);
      this.leaseTime = newTime;
      this.state = LeaseState.Active;
    }

    internal delegate TimeSpan AsyncRenewal(ILease lease);

    [Serializable]
    internal enum SponsorState
    {
      Initial,
      Waiting,
      Completed,
    }

    internal sealed class SponsorStateInfo
    {
      internal TimeSpan renewalTime;
      internal Lease.SponsorState sponsorState;

      internal SponsorStateInfo(TimeSpan renewalTime, Lease.SponsorState sponsorState)
      {
        this.renewalTime = renewalTime;
        this.sponsorState = sponsorState;
      }
    }
  }
}
