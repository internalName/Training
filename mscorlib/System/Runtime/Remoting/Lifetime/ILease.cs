// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ILease
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>
  ///   Определяет объект времени жизни аренды, который используется службой времени жизни удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface ILease
  {
    /// <summary>
    ///   Регистрирует спонсора для аренды и восстанавливает ее с указанным <see cref="T:System.TimeSpan" />.
    /// </summary>
    /// <param name="obj">Объект обратного вызова спонсора.</param>
    /// <param name="renewalTime">
    ///   Интервал времени для продления аренды.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void Register(ISponsor obj, TimeSpan renewalTime);

    /// <summary>
    ///   Регистрирует спонсора для аренды без восстановления аренды.
    /// </summary>
    /// <param name="obj">Объект обратного вызова спонсора.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void Register(ISponsor obj);

    /// <summary>Удаляет спонсора из списка спонсоров.</summary>
    /// <param name="obj">
    ///   Спонсор аренды, для которого нужно отменить регистрацию.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственно вызывающий объект вызывает интерфейс через ссылку и не имеет разрешения на использование инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void Unregister(ISponsor obj);

    /// <summary>Продлевает аренду для заданного времени.</summary>
    /// <param name="renewalTime">
    ///   Интервал времени для продления аренды.
    /// </param>
    /// <returns>Новый срок действия аренды.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    TimeSpan Renew(TimeSpan renewalTime);

    /// <summary>
    ///   Возвращает или задает количество времени, на который вызов удаленного объекта продлевает <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.
    /// </summary>
    /// <returns>
    ///   Количество времени, на который вызов удаленного объекта продлевает <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    TimeSpan RenewOnCallTime { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>
    ///   Возвращает или задает время ожидания спонсора для возврата со временем продления срока аренды.
    /// </summary>
    /// <returns>
    ///   Количество времени для ожидания спонсора для возврата со временем продления срока аренды.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    TimeSpan SponsorshipTimeout { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>Возвращает или задает начальное время для аренды.</summary>
    /// <returns>Начальное время для аренды.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    TimeSpan InitialLeaseTime { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>Возвращает оставшееся количество времени аренды.</summary>
    /// <returns>Оставшееся количество времени аренды.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    TimeSpan CurrentLeaseTime { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает текущую <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> аренды.
    /// </summary>
    /// <returns>
    ///   Текущий <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> аренды.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    LeaseState CurrentState { [SecurityCritical] get; }
  }
}
