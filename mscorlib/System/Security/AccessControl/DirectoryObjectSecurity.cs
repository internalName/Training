// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.DirectoryObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Предоставляет возможность управления доступом к объектам каталога без непосредственной работы со списки управления доступом (ACL).
  /// </summary>
  public abstract class DirectoryObjectSecurity : ObjectSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" />.
    /// </summary>
    protected DirectoryObjectSecurity()
      : base(true, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> класса на указанный дескриптор безопасности.
    /// </summary>
    /// <param name="securityDescriptor">
    ///   Дескриптор безопасности, который следует связать с новым <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" />объекта.
    /// </param>
    protected DirectoryObjectSecurity(CommonSecurityDescriptor securityDescriptor)
      : base(securityDescriptor)
    {
    }

    private AuthorizationRuleCollection GetRules(bool access, bool includeExplicit, bool includeInherited, Type targetType)
    {
      this.ReadLock();
      try
      {
        AuthorizationRuleCollection authorizationRuleCollection = new AuthorizationRuleCollection();
        if (!SecurityIdentifier.IsValidTargetTypeStatic(targetType))
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), nameof (targetType));
        CommonAcl commonAcl = (CommonAcl) null;
        if (access)
        {
          if ((this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None)
            commonAcl = (CommonAcl) this._securityDescriptor.DiscretionaryAcl;
        }
        else if ((this._securityDescriptor.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None)
          commonAcl = (CommonAcl) this._securityDescriptor.SystemAcl;
        if (commonAcl == null)
          return authorizationRuleCollection;
        IdentityReferenceCollection referenceCollection1 = (IdentityReferenceCollection) null;
        if (targetType != typeof (SecurityIdentifier))
        {
          IdentityReferenceCollection referenceCollection2 = new IdentityReferenceCollection(commonAcl.Count);
          for (int index = 0; index < commonAcl.Count; ++index)
          {
            QualifiedAce qualifiedAce = commonAcl[index] as QualifiedAce;
            if (!((GenericAce) qualifiedAce == (GenericAce) null) && !qualifiedAce.IsCallback)
            {
              if (access)
              {
                if (qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
                  continue;
              }
              else if (qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
                continue;
              referenceCollection2.Add((IdentityReference) qualifiedAce.SecurityIdentifier);
            }
          }
          referenceCollection1 = referenceCollection2.Translate(targetType);
        }
        for (int index = 0; index < commonAcl.Count; ++index)
        {
          QualifiedAce qualifiedAce = (QualifiedAce) (commonAcl[index] as CommonAce);
          if ((GenericAce) qualifiedAce == (GenericAce) null)
          {
            qualifiedAce = (QualifiedAce) (commonAcl[index] as ObjectAce);
            if ((GenericAce) qualifiedAce == (GenericAce) null)
              continue;
          }
          if (!qualifiedAce.IsCallback)
          {
            if (access)
            {
              if (qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
                continue;
            }
            else if (qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
              continue;
            if (includeExplicit && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None || includeInherited && (qualifiedAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
            {
              IdentityReference identityReference = targetType == typeof (SecurityIdentifier) ? (IdentityReference) qualifiedAce.SecurityIdentifier : referenceCollection1[index];
              if (access)
              {
                AccessControlType type = qualifiedAce.AceQualifier != AceQualifier.AccessAllowed ? AccessControlType.Deny : AccessControlType.Allow;
                if (qualifiedAce is ObjectAce)
                {
                  ObjectAce objectAce = qualifiedAce as ObjectAce;
                  authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, objectAce.AccessMask, objectAce.IsInherited, objectAce.InheritanceFlags, objectAce.PropagationFlags, type, objectAce.ObjectAceType, objectAce.InheritedObjectAceType));
                }
                else
                {
                  CommonAce commonAce = qualifiedAce as CommonAce;
                  if (!((GenericAce) commonAce == (GenericAce) null))
                    authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, commonAce.AccessMask, commonAce.IsInherited, commonAce.InheritanceFlags, commonAce.PropagationFlags, type));
                }
              }
              else if (qualifiedAce is ObjectAce)
              {
                ObjectAce objectAce = qualifiedAce as ObjectAce;
                authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, objectAce.AccessMask, objectAce.IsInherited, objectAce.InheritanceFlags, objectAce.PropagationFlags, objectAce.AuditFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType));
              }
              else
              {
                CommonAce commonAce = qualifiedAce as CommonAce;
                if (!((GenericAce) commonAce == (GenericAce) null))
                  authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, commonAce.AccessMask, commonAce.IsInherited, commonAce.InheritanceFlags, commonAce.PropagationFlags, commonAce.AuditFlags));
              }
            }
          }
        }
        return authorizationRuleCollection;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    private bool ModifyAccess(AccessControlModification modification, ObjectAccessRule rule, out bool modified)
    {
      bool flag = true;
      if (this._securityDescriptor.DiscretionaryAcl == null)
      {
        if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
        {
          modified = false;
          return flag;
        }
        this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevisionDS, 1);
        this._securityDescriptor.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
      }
      else if ((modification == AccessControlModification.Add || modification == AccessControlModification.Set || modification == AccessControlModification.Reset) && (rule.ObjectFlags != ObjectAceFlags.None && (int) this._securityDescriptor.DiscretionaryAcl.Revision < (int) GenericAcl.AclRevisionDS))
      {
        byte[] binaryForm = new byte[this._securityDescriptor.DiscretionaryAcl.BinaryLength];
        this._securityDescriptor.DiscretionaryAcl.GetBinaryForm(binaryForm, 0);
        binaryForm[0] = GenericAcl.AclRevisionDS;
        this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, new RawAcl(binaryForm, 0));
      }
      SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
      if (rule.AccessControlType == AccessControlType.Allow)
      {
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            if (!flag)
              throw new SystemException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
      else
      {
        if (rule.AccessControlType != AccessControlType.Deny)
          throw new SystemException();
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            if (!flag)
              throw new SystemException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
      modified = flag;
      this.AccessRulesModified |= modified;
      return flag;
    }

    private bool ModifyAudit(AccessControlModification modification, ObjectAuditRule rule, out bool modified)
    {
      bool flag = true;
      if (this._securityDescriptor.SystemAcl == null)
      {
        if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
        {
          modified = false;
          return flag;
        }
        this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevisionDS, 1);
        this._securityDescriptor.AddControlFlags(ControlFlags.SystemAclPresent);
      }
      else if ((modification == AccessControlModification.Add || modification == AccessControlModification.Set || modification == AccessControlModification.Reset) && (rule.ObjectFlags != ObjectAceFlags.None && (int) this._securityDescriptor.SystemAcl.Revision < (int) GenericAcl.AclRevisionDS))
      {
        byte[] binaryForm = new byte[this._securityDescriptor.SystemAcl.BinaryLength];
        this._securityDescriptor.SystemAcl.GetBinaryForm(binaryForm, 0);
        binaryForm[0] = GenericAcl.AclRevisionDS;
        this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, new RawAcl(binaryForm, 0));
      }
      SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
      switch (modification)
      {
        case AccessControlModification.Add:
          this._securityDescriptor.SystemAcl.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Set:
          this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Reset:
          this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
          this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Remove:
          flag = this._securityDescriptor.SystemAcl.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.RemoveAll:
          flag = this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
          if (!flag)
            throw new SystemException();
          break;
        case AccessControlModification.RemoveSpecific:
          this._securityDescriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
      modified = flag;
      this.AuditRulesModified |= modified;
      return flag;
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
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новое правило доступа.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новое правило доступа.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AccessRule" />, создаваемый с помощью данного метода.
    /// </returns>
    public virtual AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectType, Guid inheritedObjectType)
    {
      throw new NotImplementedException();
    }

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
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новое правило аудита.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новое правило аудита.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AuditRule" />, создаваемый с помощью данного метода.
    /// </returns>
    public virtual AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectType, Guid inheritedObjectType)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Применяет указанное изменение к списку управления доступом на уровне пользователей (DACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" />.
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
    protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), nameof (rule));
      return this.ModifyAccess(modification, rule as ObjectAccessRule, out modified);
    }

    /// <summary>
    ///   Применяет указанное изменение для системы управления списка управления ДОСТУПОМ связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
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
    protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), nameof (rule));
      return this.ModifyAudit(modification, rule as ObjectAuditRule, out modified);
    }

    /// <summary>
    ///   Добавляет указанное правило доступа для управления доступом список (DACL) связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило доступа.</param>
    protected void AddAccessRule(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Add, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила доступа, содержащие те же идентификатор безопасности и квалификатор, указанное правило доступа в список (ДОСТУПОМ) связан с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Чтобы задать правило доступа.</param>
    protected void SetAccessRule(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Set, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила доступа в списке (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Правила доступа для сброса.</param>
    protected void ResetAccessRule(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Reset, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет правила доступа, содержащие же идентификатор безопасности маску доступа, что указанное правило доступа из списка (ДОСТУПОМ), связанного с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    /// <returns>
    ///   <see langword="true" /> Если правило доступа успешно удалено; в противном случае — <see langword="false" />.
    /// </returns>
    protected bool RemoveAccessRule(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return true;
        bool modified;
        return this.ModifyAccess(AccessControlModification.Remove, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила доступа с тем же идентификатором безопасности, что указанное правило доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    protected void RemoveAccessRuleAll(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return;
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveAll, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила доступа, в точности совпадающие с указанным правилом доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    protected void RemoveAccessRuleSpecific(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      if (this._securityDescriptor == null)
        return;
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Добавляет указанного аудита правила в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило аудита.</param>
    protected void AddAuditRule(ObjectAuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.Add, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила аудита, содержащие те же идентификатор безопасности и квалификатор, как правила указанного аудита в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта, а затем добавляет указанное правило аудита.
    /// </summary>
    /// <param name="rule">Чтобы задать правило аудита.</param>
    protected void SetAuditRule(ObjectAuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.Set, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет правила аудита, содержащие же идентификатор безопасности маску доступа, как правила указанного аудита из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    /// <returns>
    ///   <see langword="true" /> Если правило аудита успешно удалено; в противном случае — <see langword="false" />.
    /// </returns>
    protected bool RemoveAuditRule(ObjectAuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        return this.ModifyAudit(AccessControlModification.Remove, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила аудита с тем же идентификатором безопасности, что правила указанного аудита из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    protected void RemoveAuditRuleAll(ObjectAuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.RemoveAll, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Удаляет все правила аудита, которые точно соответствуют указанным аудита правила из системного списка (Управления доступом) связанный с этим <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    protected void RemoveAuditRuleSpecific(ObjectAuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Получает коллекцию правил доступа, связанных с указанным идентификатором безопасности.
    /// </summary>
    /// <param name="includeExplicit">
    ///   <see langword="true" /> для включения доступа правила явно задать для объекта.
    /// </param>
    /// <param name="includeInherited">
    ///   <see langword="true" /> Чтобы включить унаследованные правила доступа.
    /// </param>
    /// <param name="targetType">
    ///   Идентификатор безопасности, для которого извлекаются правила доступа.
    ///    Это должен быть объект, который может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </param>
    /// <returns>
    ///   Коллекция правил доступа, связанный с заданным <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      return this.GetRules(true, includeExplicit, includeInherited, targetType);
    }

    /// <summary>
    ///   Получает коллекцию правил аудита, связанных с указанным идентификатором безопасности.
    /// </summary>
    /// <param name="includeExplicit">
    ///   <see langword="true" /> для включения аудита правила явно задать для объекта.
    /// </param>
    /// <param name="includeInherited">
    ///   <see langword="true" /> Чтобы включить унаследованные правила аудита.
    /// </param>
    /// <param name="targetType">
    ///   Идентификатор безопасности, для которого извлекаются правила аудита.
    ///    Это должен быть объект, который может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </param>
    /// <returns>
    ///   Коллекция правил аудита, связанных с указанным <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      return this.GetRules(false, includeExplicit, includeInherited, targetType);
    }
  }
}
