// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.SystemAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет системный список управления доступом (SACL).
  /// </summary>
  public sealed class SystemAcl : CommonAcl
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.SystemAcl" /> с использованием указанных значений.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="capacity">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.SystemAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public SystemAcl(bool isContainer, bool isDS, int capacity)
      : this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.SystemAcl" /> с использованием указанных значений.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="revision">
    ///   Номер редакции нового <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </param>
    /// <param name="capacity">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.SystemAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity)
      : base(isContainer, isDS, revision, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.SystemAcl" /> класса с использованием указанных значений из указанного <see cref="T:System.Security.AccessControl.RawAcl" /> объекта.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.SystemAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="rawAcl">
    ///   Базовый <see cref="T:System.Security.AccessControl.RawAcl" /> объекта для нового <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    ///    Укажите <see langword="null" /> для создания пустой ACL.
    /// </param>
    public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl)
      : this(isContainer, isDS, rawAcl, false)
    {
    }

    internal SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
      : base(isContainer, isDS, rawAcl, trusted, false)
    {
    }

    /// <summary>
    ///   Добавляет правило аудита к текущему <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для нового правила аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового правила аудита.
    /// </param>
    public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Задает указанное правило аудита для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="auditFlags">Устанавливаемое условие аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для нового правила аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового правила аудита.
    /// </param>
    public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод успешно удаляет указанное правило аудита; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      return this.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила.
    /// </param>
    public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Добавляет правило аудита к текущему <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить правило аудита.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAuditRule" />Для нового правила аудита.
    /// </param>
    public void AddAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Добавляет правило аудита с указанными параметрами в текущий <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    ///    Используйте этот метод для каталога объекта списки управления доступом (ACL) при указании типа объекта или типа наследуемого объекта для нового правила аудита.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для нового правила аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового правила аудита.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новое правило аудита.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новое правило аудита.
    /// </param>
    public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Задает указанное правило аудита для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать правило аудита.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAuditRule" />Для которого требуется задать правило аудита.
    /// </param>
    public void SetAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Задает указанное правило аудита для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    ///    Используйте этот метод для объектов каталогов, списки управления доступом (ACL), при указании типа объекта или типа наследуемого объекта.
    /// </summary>
    /// <param name="auditFlags">Устанавливаемое условие аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для нового правила аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового правила аудита.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новое правило аудита.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новое правило аудита.
    /// </param>
    public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод успешно удаляет указанное правило аудита; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      return this.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    ///    Используйте этот метод для объектов каталогов, списки управления доступом (ACL), при указании типа объекта или типа наследуемого объекта.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется правило управления удален аудита.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать удаляемое правило аудита.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод успешно удаляет указанное правило аудита; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      return this.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> Для удаляемого правила.
    /// </param>
    public void RemoveAuditSpecific(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанное правило аудита из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    ///    Используйте этот метод для объектов каталогов, списки управления доступом (ACL), при указании типа объекта или типа наследуемого объекта.
    /// </summary>
    /// <param name="auditFlags">Тип правила аудита.</param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить правило аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется правило управления удален аудита.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать удаляемое правило аудита.
    /// </param>
    public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }
  }
}
