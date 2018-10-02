// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Предоставляет возможность управления доступом к объектам без непосредственной работы со списками управления доступом (ACL).
  ///    Этот класс является абстрактным базовым классом для классов <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> и <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" />.
  /// </summary>
  public abstract class ObjectSecurity
  {
    private static readonly ControlFlags SACL_CONTROL_FLAGS = ControlFlags.SystemAclPresent | ControlFlags.SystemAclAutoInherited | ControlFlags.SystemAclProtected;
    private static readonly ControlFlags DACL_CONTROL_FLAGS = ControlFlags.DiscretionaryAclPresent | ControlFlags.DiscretionaryAclAutoInherited | ControlFlags.DiscretionaryAclProtected;
    private readonly ReaderWriterLock _lock = new ReaderWriterLock();
    internal CommonSecurityDescriptor _securityDescriptor;
    private bool _ownerModified;
    private bool _groupModified;
    private bool _saclModified;
    private bool _daclModified;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    protected ObjectSecurity()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="isDS">
    ///   Значение true, если новый объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> является объектом каталога.
    /// </param>
    protected ObjectSecurity(bool isContainer, bool isDS)
      : this()
    {
      DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 5);
      this._securityDescriptor = new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, (SecurityIdentifier) null, (SecurityIdentifier) null, (SystemAcl) null, discretionaryAcl);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="securityDescriptor">
    ///   Объект <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> нового экземпляра <see cref="T:System.Security.AccessControl.CommonObjectSecurity" />.
    /// </param>
    protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
      : this()
    {
      if (securityDescriptor == null)
        throw new ArgumentNullException(nameof (securityDescriptor));
      this._securityDescriptor = securityDescriptor;
    }

    private void UpdateWithNewSecurityDescriptor(RawSecurityDescriptor newOne, AccessControlSections includeSections)
    {
      if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
      {
        this._ownerModified = true;
        this._securityDescriptor.Owner = newOne.Owner;
      }
      if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
      {
        this._groupModified = true;
        this._securityDescriptor.Group = newOne.Group;
      }
      if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
      {
        this._saclModified = true;
        this._securityDescriptor.SystemAcl = newOne.SystemAcl == null ? (SystemAcl) null : new SystemAcl(this.IsContainer, this.IsDS, newOne.SystemAcl, true);
        this._securityDescriptor.UpdateControlFlags(ObjectSecurity.SACL_CONTROL_FLAGS, newOne.ControlFlags & ObjectSecurity.SACL_CONTROL_FLAGS);
      }
      if ((includeSections & AccessControlSections.Access) == AccessControlSections.None)
        return;
      this._daclModified = true;
      this._securityDescriptor.DiscretionaryAcl = newOne.DiscretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(this.IsContainer, this.IsDS, newOne.DiscretionaryAcl, true);
      ControlFlags controlFlags = this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent;
      this._securityDescriptor.UpdateControlFlags(ObjectSecurity.DACL_CONTROL_FLAGS, (newOne.ControlFlags | controlFlags) & ObjectSecurity.DACL_CONTROL_FLAGS);
    }

    /// <summary>
    ///   Блокирует этот объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> для доступа для чтения.
    /// </summary>
    protected void ReadLock()
    {
      this._lock.AcquireReaderLock(-1);
    }

    /// <summary>
    ///   Разблокирует этот объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> для доступа для чтения.
    /// </summary>
    protected void ReadUnlock()
    {
      this._lock.ReleaseReaderLock();
    }

    /// <summary>
    ///   Блокирует доступ к этому объекту <see cref="T:System.Security.AccessControl.ObjectSecurity" /> для записи.
    /// </summary>
    protected void WriteLock()
    {
      this._lock.AcquireWriterLock(-1);
    }

    /// <summary>
    ///   Разблокирует этот объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> для доступа для записи.
    /// </summary>
    protected void WriteUnlock()
    {
      this._lock.ReleaseWriterLock();
    }

    /// <summary>
    ///   Получает или задает логическое значение, указывающее, был ли изменен владелец защищаемого объекта.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если владелец защищаемого объекта был изменен; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool OwnerModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._ownerModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._ownerModified = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, была ли изменена группа, связанная с защищаемым объектом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если группа, связанная с защищаемым объектом, была изменена. В противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool GroupModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._groupModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._groupModified = value;
      }
    }

    /// <summary>
    ///   Получает или задает логическое значение, которое указывает, изменены ли правила аудита, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если правила аудита, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, изменены; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool AuditRulesModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._saclModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._saclModified = value;
      }
    }

    /// <summary>
    ///   Получает или задает логическое значение, которое указывает, изменены ли правила доступа, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если правила доступа, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, изменены; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool AccessRulesModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._daclModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._daclModified = value;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли это <see cref="T:System.Security.AccessControl.ObjectSecurity" /> является объектом контейнера.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Security.AccessControl.ObjectSecurity" /> объект является контейнером; в противном случае — <see langword="false" />.
    /// </returns>
    protected bool IsContainer
    {
      get
      {
        return this._securityDescriptor.IsContainer;
      }
    }

    /// <summary>
    ///   Получает логическое значение, указывающее, является ли этот объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> объектом каталога.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Security.AccessControl.ObjectSecurity" /> является объектом каталога; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool IsDS
    {
      get
      {
        return this._securityDescriptor.IsDS;
      }
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="name">
    ///   Имя, используемое для получения хранимой информации.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    protected virtual void Persist(string name, AccessControlSections includeSections)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="enableOwnershipPrivilege">
    ///   Значение <see langword="true" />, чтобы включить привилегию, позволяющую вызывающему объекту стать владельцем объекта.
    /// </param>
    /// <param name="name">
    ///   Имя, используемое для получения хранимой информации.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
    {
      Privilege privilege = (Privilege) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (enableOwnershipPrivilege)
        {
          privilege = new Privilege("SeTakeOwnershipPrivilege");
          try
          {
            privilege.Enable();
          }
          catch (PrivilegeNotHeldException ex)
          {
          }
        }
        this.Persist(name, includeSections);
      }
      catch
      {
        privilege?.Revert();
        throw;
      }
      finally
      {
        privilege?.Revert();
      }
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор, используемый для получения хранимой информации.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    [SecuritySafeCritical]
    protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает владельца, связанного с указанной основной группой.
    /// </summary>
    /// <param name="targetType">
    ///   Основная группа, для которой требуется получить владельца.
    /// </param>
    /// <returns>Владелец, связанный с указанной группой.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="targetType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="targetType" /> не является типом <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </exception>
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
    ///   Некоторые или ссылки на свойства нельзя преобразовать.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Возвращен код ошибки Win32.
    /// </exception>
    public IdentityReference GetOwner(Type targetType)
    {
      this.ReadLock();
      try
      {
        if (this._securityDescriptor.Owner == (SecurityIdentifier) null)
          return (IdentityReference) null;
        return this._securityDescriptor.Owner.Translate(targetType);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>
    ///   Задает владельца для дескриптора безопасности, связанного с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="identity">Задаваемый владелец.</param>
    public void SetOwner(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      this.WriteLock();
      try
      {
        this._securityDescriptor.Owner = identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        this._ownerModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Получает основную группу, связанную с указанным владельцем.
    /// </summary>
    /// <param name="targetType">
    ///   Владелец, для которого требуется получить основную группу.
    /// </param>
    /// <returns>Основная группа, связанная с указанным владельцем.</returns>
    public IdentityReference GetGroup(Type targetType)
    {
      this.ReadLock();
      try
      {
        if (this._securityDescriptor.Group == (SecurityIdentifier) null)
          return (IdentityReference) null;
        return this._securityDescriptor.Group.Translate(targetType);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>
    ///   Задает основную группу для дескриптора безопасности, связанного с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="identity">
    ///   Основная группа, которую необходимо задать.
    /// </param>
    public void SetGroup(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      this.WriteLock();
      try
      {
        this._securityDescriptor.Group = identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        this._groupModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила доступа, связанные с указанным объектом <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </summary>
    /// <param name="identity">
    ///   Объект <see cref="T:System.Security.Principal.IdentityReference" />, для которого требуется удалить все правила доступа.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Все правила доступа указаны не в каноническом порядке.
    /// </exception>
    public virtual void PurgeAccessRules(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      this.WriteLock();
      try
      {
        this._securityDescriptor.PurgeAccessControl(identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier);
        this._daclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила, связанные с заданным <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </summary>
    /// <param name="identity">
    ///   <see cref="T:System.Security.Principal.IdentityReference" />, для которого необходимо удалить все правила аудита.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Все правила аудита указаны не в каноническом порядке.
    /// </exception>
    public virtual void PurgeAuditRules(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      this.WriteLock();
      try
      {
        this._securityDescriptor.PurgeAudit(identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier);
        this._saclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, защищен ли список разграничительного управления доступа (DACL), связанный с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если список DACL защищен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool AreAccessRulesProtected
    {
      get
      {
        this.ReadLock();
        try
        {
          return (uint) (this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > 0U;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>
    ///   Задает или удаляет защиту правил доступа, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    ///    Защищенные правила доступа не могут изменяться родительскими объектами через наследование.
    /// </summary>
    /// <param name="isProtected">
    ///   <see langword="true" /> — для защиты правил доступа, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, от наследования; <see langword="false" /> — для разрешения наследования.
    /// </param>
    /// <param name="preserveInheritance">
    ///   <see langword="true" /> — для сохранения наследуемых правил доступа; <see langword="false" /> — для удаления наследуемых правил доступа.
    ///    Этот параметр не учитывается, если <paramref name="isProtected" /> имеет значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод пытается удалить наследуемые правила из неканонического списка управления доступом на уровне пользователей (DACL).
    /// </exception>
    public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
    {
      this.WriteLock();
      try
      {
        this._securityDescriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
        this._daclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, защищен ли системный список управления доступом (SACL), связанный с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если список SACL защищен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool AreAuditRulesProtected
    {
      get
      {
        this.ReadLock();
        try
        {
          return (uint) (this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) > 0U;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>
    ///   Задает или удаляет защиту правил аудита, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    ///    Защищенные правила аудита не могут изменяться родительскими объектами через наследование.
    /// </summary>
    /// <param name="isProtected">
    ///   <see langword="true" /> — для защиты правил аудита, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, от наследования; <see langword="false" /> — для разрешения наследования.
    /// </param>
    /// <param name="preserveInheritance">
    ///   <see langword="true" /> — для сохранения наследуемых правил аудита; <see langword="false" /> — для удаления наследуемых правил аудита.
    ///    Этот параметр не учитывается, если <paramref name="isProtected" /> является <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод пытается удалить наследуемые правила из неканонического системного списка управления доступом (SACL).
    /// </exception>
    public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
    {
      this.WriteLock();
      try
      {
        this._securityDescriptor.SetSystemAclProtection(isProtected, preserveInheritance);
        this._saclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, которое указывает, расположены ли правила доступа, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, в каноническом порядке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если правила доступа следуют в каноническом порядке; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool AreAccessRulesCanonical
    {
      get
      {
        this.ReadLock();
        try
        {
          return this._securityDescriptor.IsDiscretionaryAclCanonical;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, которое указывает, расположены ли правила аудита, связанные с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, в каноническом порядке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если правила аудита следуют в каноническом порядке; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool AreAuditRulesCanonical
    {
      get
      {
        this.ReadLock();
        try
        {
          return this._securityDescriptor.IsSystemAclCanonical;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, может ли дескриптор безопасности, связанный с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, быть преобразован в формат языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор безопасности, связанный с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />, может быть преобразован в формат языка определения дескриптора безопасности (SDDL). В противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsSddlConversionSupported()
    {
      return true;
    }

    /// <summary>
    ///   Возвращает представление на языке определения дескриптора безопасности (SDDL) указанных разделов дескриптора безопасности, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="includeSections">
    ///   Указывает, какие следует получить разделы дескриптора безопасности (правила доступа, правила аудита, основная группа, владелец).
    /// </param>
    /// <returns>
    ///   Представление на языке SDDL указанных разделов дескриптора безопасности, связанных с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </returns>
    public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
    {
      this.ReadLock();
      try
      {
        return this._securityDescriptor.GetSddlForm(includeSections);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>
    ///   Задает дескриптор безопасности для данного объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" /> из указанной строки языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <param name="sddlForm">
    ///   Строка SDDL, из которой задается дескриптор безопасности.
    /// </param>
    public void SetSecurityDescriptorSddlForm(string sddlForm)
    {
      this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
    }

    /// <summary>
    ///   Задает указанные разделы дескриптора безопасности для данного объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" /> из указанной строки языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <param name="sddlForm">
    ///   Строка SDDL, из которой задается дескриптор безопасности.
    /// </param>
    /// <param name="includeSections">
    ///   Задаваемые разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа).
    /// </param>
    public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
    {
      if (sddlForm == null)
        throw new ArgumentNullException(nameof (sddlForm));
      if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), nameof (includeSections));
      this.WriteLock();
      try
      {
        this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(sddlForm), includeSections);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Возвращает массив значений байтов, представляющих данные дескриптора безопасности для этого объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Массив значений байтов, представляющих дескриптор безопасности для этого объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    ///    Этот метод возвращает <see langword="null" />, если отсутствуют сведения о безопасности в этом объекте <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </returns>
    public byte[] GetSecurityDescriptorBinaryForm()
    {
      this.ReadLock();
      try
      {
        byte[] binaryForm = new byte[this._securityDescriptor.BinaryLength];
        this._securityDescriptor.GetBinaryForm(binaryForm, 0);
        return binaryForm;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>
    ///   Задает дескриптор безопасности для данного объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" /> из указанного массива байтовых значений.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, из которого задается дескриптор безопасности.
    /// </param>
    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
    {
      this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
    }

    /// <summary>
    ///   Задает указанные разделы дескриптора безопасности для данного объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" /> из указанного массива байтовых значений.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, из которого задается дескриптор безопасности.
    /// </param>
    /// <param name="includeSections">
    ///   Задаваемые разделы дескриптора безопасности (правила доступа, правила аудита, основная группа, владелец).
    /// </param>
    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), nameof (includeSections));
      this.WriteLock();
      try
      {
        this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(binaryForm, 0), includeSections);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> защищаемого объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <returns>
    ///   Тип защищаемого объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </returns>
    public abstract Type AccessRightType { get; }

    /// <summary>
    ///   Получает <see cref="T:System.Type" /> объекта, связанного с правилами доступа этого объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    ///    Объект <see cref="T:System.Security.Principal.SecurityIdentifier" /> должен быть объектом, который может быть приведен как объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами доступа этого <see cref="T:System.Security.AccessControl.ObjectSecurity" /> объекта.
    /// </returns>
    public abstract Type AccessRuleType { get; }

    /// <summary>
    ///   Получает объект <see cref="T:System.Type" />, связанный с правилами аудита этого объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    ///    Объект <see cref="T:System.Type" /> должен быть объектом, который может быть приведен как объект <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами аудита этого объекта <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </returns>
    public abstract Type AuditRuleType { get; }

    /// <summary>
    ///   Применяет указанное изменение к списку управления доступом на уровне пользователей (DACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="modification">
    ///   Изменение, применяемое к списку DACL.
    /// </param>
    /// <param name="rule">Изменяемое правило доступа.</param>
    /// <param name="modified">
    ///   Значение <see langword="true" />, если список DACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если список DACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

    /// <summary>
    ///   Применяет указанное изменение к системному списку управления доступом (SACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="modification">
    ///   Изменение, применяемое к списку SACL.
    /// </param>
    /// <param name="rule">Правило аудита, которое нужно изменить.</param>
    /// <param name="modified">
    ///   Значение <see langword="true" />, если список SACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если список SACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

    /// <summary>
    ///   Применяет указанное изменение к списку управления доступом на уровне пользователей (DACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="modification">
    ///   Изменение, применяемое к списку DACL.
    /// </param>
    /// <param name="rule">Изменяемое правило доступа.</param>
    /// <param name="modified">
    ///   Значение <see langword="true" />, если список DACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если список DACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), nameof (rule));
      this.WriteLock();
      try
      {
        return this.ModifyAccess(modification, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Применяет указанное изменение к системному списку управления доступом (SACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.ObjectSecurity" />.
    /// </summary>
    /// <param name="modification">
    ///   Изменение, применяемое к списку SACL.
    /// </param>
    /// <param name="rule">Правило аудита, которое нужно изменить.</param>
    /// <param name="modified">
    ///   Значение <see langword="true" />, если список SACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если список SACL успешно изменен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), nameof (rule));
      this.WriteLock();
      try
      {
        return this.ModifyAudit(modification, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AccessRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identityReference">
    ///   Удостоверение, к которому применяется правило доступа.
    ///     Это должен быть объект, который может быть приведен как <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа данного правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение каждого из которых определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение true, если правило наследуется от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Указывает свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Указывает, выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если <paramref name="inheritanceFlags" /> имеет значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="type">
    ///   Указывает допустимый тип управления доступом.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AccessRule" />, создаваемый с помощью данного метода.
    /// </returns>
    public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AuditRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identityReference">
    ///   Удостоверение, к которому применяется правило аудита.
    ///     Это должен быть объект, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа данного правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение каждого из которых определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение <see langword="true" />, если правило наследуется от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Указывает свойства наследования правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Указывает, выполняется ли автоматическое распространение наследуемых правил аудита.
    ///    Флаги распространения не учитываются, если <paramref name="inheritanceFlags" /> имеет значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="flags">
    ///   Указывает условия, в которых выполняется аудит правила.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AuditRule" />, создаваемый с помощью данного метода.
    /// </returns>
    public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);
  }
}
