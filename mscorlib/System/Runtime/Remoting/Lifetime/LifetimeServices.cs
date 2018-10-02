// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LifetimeServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>
  ///   Управляет службами времени жизни удаленного взаимодействия платформы.NET.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class LifetimeServices
  {
    private static bool s_isLeaseTime = false;
    private static bool s_isRenewOnCallTime = false;
    private static bool s_isSponsorshipTimeout = false;
    private static long s_leaseTimeTicks = TimeSpan.FromMinutes(5.0).Ticks;
    private static long s_renewOnCallTimeTicks = TimeSpan.FromMinutes(2.0).Ticks;
    private static long s_sponsorshipTimeoutTicks = TimeSpan.FromMinutes(2.0).Ticks;
    private static long s_pollTimeTicks = TimeSpan.FromMilliseconds(10000.0).Ticks;
    private static object s_LifetimeSyncObject = (object) null;

    private static TimeSpan GetTimeSpan(ref long ticks)
    {
      return TimeSpan.FromTicks(Volatile.Read(ref ticks));
    }

    private static void SetTimeSpan(ref long ticks, TimeSpan value)
    {
      Volatile.Write(ref ticks, value.Ticks);
    }

    private static object LifetimeSyncObject
    {
      get
      {
        if (LifetimeServices.s_LifetimeSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref LifetimeServices.s_LifetimeSyncObject, obj, (object) null);
        }
        return LifetimeServices.s_LifetimeSyncObject;
      }
    }

    /// <summary>
    ///   Создает экземпляр <see cref="T:System.Runtime.Remoting.Lifetime.LifetimeServices" />.
    /// </summary>
    [Obsolete("Do not create instances of the LifetimeServices class.  Call the static methods directly on this type instead", true)]
    public LifetimeServices()
    {
    }

    /// <summary>
    ///   Возвращает или задает начальный промежуток времени аренды для <see cref="T:System.AppDomain" />.
    /// </summary>
    /// <returns>
    ///   Начальной аренды <see cref="T:System.TimeSpan" /> для объектов, которые могут иметь аренду в <see cref="T:System.AppDomain" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    ///    Это исключение вызывается только при установке значения свойства.
    /// </exception>
    public static TimeSpan LeaseTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_leaseTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isLeaseTime)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) nameof (LeaseTime)));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_leaseTimeTicks, value);
          LifetimeServices.s_isLeaseTime = true;
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает количество времени, на который увеличивается срок аренды после каждого вызова серверного объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.TimeSpan" /> Что аренда времени жизни в текущем <see cref="T:System.AppDomain" /> расширяется после каждого вызова.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    ///    Это исключение вызывается только при установке значения свойства.
    /// </exception>
    public static TimeSpan RenewOnCallTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isRenewOnCallTime)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) nameof (RenewOnCallTime)));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks, value);
          LifetimeServices.s_isRenewOnCallTime = true;
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает время ожидания спонсора для возврата со временем продления срока аренды диспетчер аренды.
    /// </summary>
    /// <returns>Время ожидания начальной спонсорства.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    ///    Это исключение вызывается только при установке значения свойства.
    /// </exception>
    public static TimeSpan SponsorshipTimeout
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isSponsorshipTimeout)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) nameof (SponsorshipTimeout)));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks, value);
          LifetimeServices.s_isSponsorshipTimeout = true;
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает временной интервал между каждой активацией диспетчера аренды для очистки аренды с истекшим сроком действия.
    /// </summary>
    /// <returns>
    ///   Период времени по умолчанию диспетчера аренды переходит в спящий режим после проверки истечения срока действия аренды.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающим объектам выше в стеке вызовов не имеет разрешения для настраивать каналы и типы удаленного взаимодействия.
    ///    Это исключение возникает только при установке значения свойства.
    /// </exception>
    public static TimeSpan LeaseManagerPollTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_pollTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_pollTimeTicks, value);
          if (!LeaseManager.IsInitialized())
            return;
          LeaseManager.GetLeaseManager().ChangePollTime(value);
        }
      }
    }

    [SecurityCritical]
    internal static ILease GetLeaseInitial(MarshalByRefObject obj)
    {
      return LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime).GetLease(obj) ?? LifetimeServices.CreateLease(obj);
    }

    [SecurityCritical]
    internal static ILease GetLease(MarshalByRefObject obj)
    {
      return LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime).GetLease(obj);
    }

    [SecurityCritical]
    internal static ILease CreateLease(MarshalByRefObject obj)
    {
      return LifetimeServices.CreateLease(LifetimeServices.LeaseTime, LifetimeServices.RenewOnCallTime, LifetimeServices.SponsorshipTimeout, obj);
    }

    [SecurityCritical]
    internal static ILease CreateLease(TimeSpan leaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject obj)
    {
      LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
      return (ILease) new Lease(leaseTime, renewOnCallTime, sponsorshipTimeout, obj);
    }
  }
}
