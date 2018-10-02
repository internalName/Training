// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectSecurity`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Обеспечивает возможность управления доступом к объектам без непосредственной работы со списки управления доступом (ACL); также предоставляет возможность права доступа приведения типа.
  /// </summary>
  /// <typeparam name="T">Права доступа для объекта.</typeparam>
  public abstract class ObjectSecurity<T> : NativeObjectSecurity where T : struct
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectSecurity 1 ".
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">Тип ресурса.</param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType)
      : base(isContainer, resourceType, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectSecurity 1 ".
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">Тип ресурса.</param>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которой новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> связан объект.
    /// </param>
    /// <param name="includeSections">
    ///   Разделы, которые нужно включить.
    /// </param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
      : base(isContainer, resourceType, name, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectSecurity 1 ".
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">Тип ресурса.</param>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которой новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> связан объект.
    /// </param>
    /// <param name="includeSections">
    ///   Разделы, которые нужно включить.
    /// </param>
    /// <param name="exceptionFromErrorCode">
    ///   Делегат, реализованный интеграторами, предоставляющий пользовательские исключения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : base(isContainer, resourceType, name, includeSections, exceptionFromErrorCode, exceptionContext)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectSecurity 1 ".
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">Тип ресурса.</param>
    /// <param name="safeHandle">Дескриптор.</param>
    /// <param name="includeSections">
    ///   Разделы, которые нужно включить.
    /// </param>
    [SecuritySafeCritical]
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections)
      : base(isContainer, resourceType, safeHandle, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectSecurity 1 ".
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">Тип ресурса.</param>
    /// <param name="safeHandle">Дескриптор.</param>
    /// <param name="includeSections">
    ///   Разделы, которые нужно включить.
    /// </param>
    /// <param name="exceptionFromErrorCode">
    ///   Делегат, реализованный интеграторами, предоставляющий пользовательские исключения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    [SecuritySafeCritical]
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : base(isContainer, resourceType, safeHandle, includeSections, exceptionFromErrorCode, exceptionContext)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса ObjectAccessRule, который представляет новое правило управления доступом для связанного с ним объекта.
    /// </summary>
    /// <param name="identityReference">
    ///   Представляет учетную запись пользователя.
    /// </param>
    /// <param name="accessMask">Тип доступа.</param>
    /// <param name="isInherited">
    ///   <see langword="" /><see langword="true" /> Если правило доступа наследуется; в противном случае — <see langword="false" />.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Указывает, как распространение маски доступа к дочерним объектам.
    /// </param>
    /// <param name="propagationFlags">
    ///   Указывает, как распространение записи управления доступом (ACE) к дочерним объектам.
    /// </param>
    /// <param name="type">
    ///   Указывает, является ли доступ разрешен или запрещен.
    /// </param>
    /// <returns>
    ///   Представляет новое правило управления доступом для указанного пользователя с указанными правами доступа, управление доступом и флаги.
    /// </returns>
    public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new AccessRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.AuditRule" /> класс, представляющий указанное правило аудита для указанного пользователя.
    /// </summary>
    /// <param name="identityReference">
    ///   Представляет учетную запись пользователя.
    /// </param>
    /// <param name="accessMask">
    ///   Целое число, задающее тип доступа.
    /// </param>
    /// <param name="isInherited">
    ///   <see langword="true" /> Если правило доступа наследуется; в противном случае — <see langword="false" />.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Указывает, как распространение маски доступа к дочерним объектам.
    /// </param>
    /// <param name="propagationFlags">
    ///   Указывает, как распространение записи управления доступом (ACE) к дочерним объектам.
    /// </param>
    /// <param name="flags">Описание типа аудита для выполнения.</param>
    /// <returns>
    ///   Возвращает указанное правило аудита для указанного пользователя.
    /// </returns>
    public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new AuditRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
    }

    private AccessControlSections GetAccessControlSectionsFromChanges()
    {
      AccessControlSections accessControlSections = AccessControlSections.None;
      if (this.AccessRulesModified)
        accessControlSections = AccessControlSections.Access;
      if (this.AuditRulesModified)
        accessControlSections |= AccessControlSections.Audit;
      if (this.OwnerModified)
        accessControlSections |= AccessControlSections.Owner;
      if (this.GroupModified)
        accessControlSections |= AccessControlSections.Group;
      return accessControlSections;
    }

    /// <summary>
    ///   Сохраняет дескриптор безопасности, связанный с объектом ObjectSecurity "1 в постоянное хранилище, с использованием указанного дескриптора.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым связан этот объект ObjectSecurity 1 ".
    /// </param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    protected internal void Persist(SafeHandle handle)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Сохраняет дескриптор безопасности, связанный с объектом "1 ObjectSecurity в постоянное хранилище, используя указанное имя.
    /// </summary>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым связан этот объект ObjectSecurity 1 ".
    /// </param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    protected internal void Persist(string name)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(name, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Добавляет указанное правило доступа для управления доступом список (DACL), связанную с объектом ObjectSecurity "1.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило.</param>
    public virtual void AddAccessRule(AccessRule<T> rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа, содержащие тем же идентификатором безопасности и квалификатор, как у указанного правила доступа в списке (ДОСТУПОМ) связанных с этим объектом ObjectSecurity "1, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Чтобы задать правило доступа.</param>
    public virtual void SetAccessRule(AccessRule<T> rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа в списке (ДОСТУПОМ) связанных с этим объектом ObjectSecurity "1, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Правила доступа для сброса.</param>
    public virtual void ResetAccessRule(AccessRule<T> rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет правила доступа с тем же идентификатором безопасности и маской доступа как указанное правило доступа из списка (ДОСТУПОМ) связанных с этим объектом ObjectSecurity 1 ".
    /// </summary>
    /// <param name="rule">Удаляемое правило.</param>
    /// <returns>
    ///   Возвращает <see langword="true" /> если правило доступа был успешно удален; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool RemoveAccessRule(AccessRule<T> rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа с тем же идентификатором безопасности, указанное правило доступа из списка (ДОСТУПОМ) связан с объектом ObjectSecurity 1 ".
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    public virtual void RemoveAccessRuleAll(AccessRule<T> rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа, в точности совпадающие с указанным правилом доступа из списка (ДОСТУПОМ) связанных с этим объектом ObjectSecurity "1
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    public virtual void RemoveAccessRuleSpecific(AccessRule<T> rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>
    ///   Добавляет правило аудита, указанный в список управления доступом системы (SACL), связанный с объектом ObjectSecurity "1.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило аудита.</param>
    public virtual void AddAuditRule(AuditRule<T> rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита, содержащие те же идентификатор безопасности и квалификатор что указанного правила аудита в системы управления списка управления ДОСТУПОМ связанное с объектом ObjectSecurity "1, а затем добавляет указанное правило аудита.
    /// </summary>
    /// <param name="rule">Чтобы задать правило аудита.</param>
    public virtual void SetAuditRule(AuditRule<T> rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет правила аудита с тем же идентификатором безопасности и маской доступа что указанного правила аудита из системного списка (Управления доступом) связанных с этим объектом ObjectSecurity 1 ".
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита</param>
    /// <returns>
    ///   Возвращает <see langword="true" /> если объект был удален; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool RemoveAuditRule(AuditRule<T> rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита с тем же идентификатором безопасности, что правило из списка управления доступом системы (SACL) указанного аудита связанное с объектом ObjectSecurity 1 ".
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    public virtual void RemoveAuditRuleAll(AuditRule<T> rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита, которые точно соответствуют указанным аудита правила из системного списка (Управления доступом) связанных с этим объектом ObjectSecurity "1
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    public virtual void RemoveAuditRuleSpecific(AuditRule<T> rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Возвращает тип защищаемого объекта, связанного с текущим объектом ObjectSecurity 1 ".
    /// </summary>
    /// <returns>
    ///   Тип защищаемого объекта, связанный с текущим экземпляром.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (T);
      }
    }

    /// <summary>
    ///   Возвращает тип объекта, связанного с правилами доступа этого объекта ObjectSecurity 1 ".
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами доступа текущего экземпляра.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (AccessRule<T>);
      }
    }

    /// <summary>
    ///   Возвращает тип объекта, связанного с правила аудита данного объекта ObjectSecurity 1 ".
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами аудита текущего экземпляра.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (AuditRule<T>);
      }
    }
  }
}
