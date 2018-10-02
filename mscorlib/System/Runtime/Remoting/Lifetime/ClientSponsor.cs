// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ClientSponsor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>
  ///   Предоставляет реализацию по умолчанию для класса спонсора жизненного цикла.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ClientSponsor : MarshalByRefObject, ISponsor
  {
    private Hashtable sponsorTable = new Hashtable(10);
    private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> со значениями по умолчанию.
    /// </summary>
    public ClientSponsor()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> класс со временем продления спонсируемых объекта.
    /// </summary>
    /// <param name="renewalTime">
    ///   <see cref="T:System.TimeSpan" /> На который увеличивается время жизни спонсируемых объектов при запросе продления.
    /// </param>
    public ClientSponsor(TimeSpan renewalTime)
    {
      this.m_renewalTime = renewalTime;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.TimeSpan" /> на который увеличивается время жизни спонсируемых объектов при запросе продления.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.TimeSpan" /> На который увеличивается время жизни спонсируемых объектов при запросе продления.
    /// </returns>
    public TimeSpan RenewalTime
    {
      get
      {
        return this.m_renewalTime;
      }
      set
      {
        this.m_renewalTime = value;
      }
    }

    /// <summary>
    ///   Регистрирует заданный <see cref="T:System.MarshalByRefObject" /> для спонсорства.
    /// </summary>
    /// <param name="obj">
    ///   Объект, регистрируемый для спонсорства с <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если регистрация успешна; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    public bool Register(MarshalByRefObject obj)
    {
      ILease lifetimeService = (ILease) obj.GetLifetimeService();
      if (lifetimeService == null)
        return false;
      lifetimeService.Register((ISponsor) this);
      lock (this.sponsorTable)
        this.sponsorTable[(object) obj] = (object) lifetimeService;
      return true;
    }

    /// <summary>
    ///   Отменяет регистрацию указанного <see cref="T:System.MarshalByRefObject" /> из списка объектов, при поддержке текущего <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.
    /// </summary>
    /// <param name="obj">Объект для отмены регистрации.</param>
    [SecurityCritical]
    public void Unregister(MarshalByRefObject obj)
    {
      ILease lease = (ILease) null;
      lock (this.sponsorTable)
        lease = (ILease) this.sponsorTable[(object) obj];
      lease?.Unregister((ISponsor) this);
    }

    /// <summary>
    ///   Запрашивает клиент-спонсор для обновления аренды для заданного объекта.
    /// </summary>
    /// <param name="lease">
    ///   Аренда времени существования объекта, который требуется продление срока аренды.
    /// </param>
    /// <returns>Дополнительное время аренды для заданного объекта.</returns>
    [SecurityCritical]
    public TimeSpan Renewal(ILease lease)
    {
      return this.m_renewalTime;
    }

    /// <summary>
    ///   Очищает список объектов, зарегистрированные с текущим <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.
    /// </summary>
    [SecurityCritical]
    public void Close()
    {
      lock (this.sponsorTable)
      {
        IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
        while (enumerator.MoveNext())
          ((ILease) enumerator.Value).Unregister((ISponsor) this);
        this.sponsorTable.Clear();
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, предоставляя аренду для текущего объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> Для текущего объекта.
    /// </returns>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }

    /// <summary>
    ///   Высвобождает ресурсы текущего <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> перед сборщик мусора восстанавливает их.
    /// </summary>
    [SecuritySafeCritical]
    ~ClientSponsor()
    {
    }
  }
}
