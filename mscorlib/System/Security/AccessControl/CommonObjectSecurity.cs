// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Управление доступом к объектам без непосредственной работы со списками контроля доступа (ACL).
  ///    Этот класс является абстрактным базовым классом для <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> класса.
  /// </summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class CommonObjectSecurity : ObjectSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.CommonObjectSecurity" />.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый объект является объектом-контейнером.
    /// </param>
    protected CommonObjectSecurity(bool isContainer)
      : base(isContainer, false)
    {
    }

    internal CommonObjectSecurity(CommonSecurityDescriptor securityDescriptor)
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
            CommonAce ace = commonAcl[index] as CommonAce;
            if (this.AceNeedsTranslation(ace, access, includeExplicit, includeInherited))
              referenceCollection2.Add((IdentityReference) ace.SecurityIdentifier);
          }
          referenceCollection1 = referenceCollection2.Translate(targetType);
        }
        int num = 0;
        for (int index = 0; index < commonAcl.Count; ++index)
        {
          CommonAce ace = commonAcl[index] as CommonAce;
          if (this.AceNeedsTranslation(ace, access, includeExplicit, includeInherited))
          {
            IdentityReference identityReference = targetType == typeof (SecurityIdentifier) ? (IdentityReference) ace.SecurityIdentifier : referenceCollection1[num++];
            if (access)
            {
              AccessControlType type = ace.AceQualifier != AceQualifier.AccessAllowed ? AccessControlType.Deny : AccessControlType.Allow;
              authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type));
            }
            else
              authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags));
          }
        }
        return authorizationRuleCollection;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    private bool AceNeedsTranslation(CommonAce ace, bool isAccessAce, bool includeExplicit, bool includeInherited)
    {
      if ((GenericAce) ace == (GenericAce) null)
        return false;
      if (isAccessAce)
      {
        if (ace.AceQualifier != AceQualifier.AccessAllowed && ace.AceQualifier != AceQualifier.AccessDenied)
          return false;
      }
      else if (ace.AceQualifier != AceQualifier.SystemAudit)
        return false;
      return includeExplicit && (ace.AceFlags & AceFlags.Inherited) == AceFlags.None || includeInherited && (ace.AceFlags & AceFlags.Inherited) != AceFlags.None;
    }

    /// <summary>
    ///   Применяет указанное изменение к списку управления доступом на уровне пользователей (DACL), связанному с этим объектом <see cref="T:System.Security.AccessControl.CommonObjectSecurity" />.
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
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool flag = true;
        if (this._securityDescriptor.DiscretionaryAcl == null)
        {
          if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
          {
            modified = false;
            return flag;
          }
          this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevision, 1);
          this._securityDescriptor.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
        }
        SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        if (rule.AccessControlType == AccessControlType.Allow)
        {
          switch (modification)
          {
            case AccessControlModification.Add:
              this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Set:
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Reset:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Remove:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.RemoveAll:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              if (!flag)
                throw new SystemException();
              break;
            case AccessControlModification.RemoveSpecific:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
          }
        }
        else if (rule.AccessControlType == AccessControlType.Deny)
        {
          switch (modification)
          {
            case AccessControlModification.Add:
              this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Set:
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Reset:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Remove:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.RemoveAll:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              if (!flag)
                throw new SystemException();
              break;
            case AccessControlModification.RemoveSpecific:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
          }
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) rule.AccessControlType), "rule.AccessControlType");
        modified = flag;
        this.AccessRulesModified |= modified;
        return flag;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Применяет указанное изменение для системы управления списка управления ДОСТУПОМ связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
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
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        bool flag = true;
        if (this._securityDescriptor.SystemAcl == null)
        {
          if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
          {
            modified = false;
            return flag;
          }
          this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevision, 1);
          this._securityDescriptor.AddControlFlags(ControlFlags.SystemAclPresent);
        }
        SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.SystemAcl.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.SystemAcl.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
            if (!flag)
              throw new InvalidProgramException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (modification), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
        modified = flag;
        this.AuditRulesModified |= modified;
        return flag;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Добавляет указанное правило доступа для управления доступом список (DACL) связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило доступа.</param>
    protected void AddAccessRule(AccessRule rule)
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
    ///   Удаляет все правила доступа, содержащие те же идентификатор безопасности и квалификатор, указанное правило доступа в список (ДОСТУПОМ) связан с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Чтобы задать правило доступа.</param>
    protected void SetAccessRule(AccessRule rule)
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
    ///   Удаляет все правила доступа в списке (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Правила доступа для сброса.</param>
    protected void ResetAccessRule(AccessRule rule)
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
    ///   Удаляет правила доступа, содержащие же идентификатор безопасности маску доступа, что указанное правило доступа из списка (ДОСТУПОМ), связанного с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    /// <returns>
    ///   <see langword="true" /> Если правило доступа успешно удалено; в противном случае — <see langword="false" />.
    /// </returns>
    protected bool RemoveAccessRule(AccessRule rule)
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
    ///   Удаляет все правила доступа с тем же идентификатором безопасности, что указанное правило доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    protected void RemoveAccessRuleAll(AccessRule rule)
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
    ///   Удаляет все правила доступа, в точности совпадающие с указанным правилом доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    protected void RemoveAccessRuleSpecific(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return;
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Добавляет указанного аудита правила в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило аудита.</param>
    protected void AddAuditRule(AuditRule rule)
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
    ///   Удаляет все правила аудита, содержащие те же идентификатор безопасности и квалификатор, как правила указанного аудита в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта, а затем добавляет указанное правило аудита.
    /// </summary>
    /// <param name="rule">Чтобы задать правило аудита.</param>
    protected void SetAuditRule(AuditRule rule)
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
    protected bool RemoveAuditRule(AuditRule rule)
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
    ///   Удаляет все правила аудита с тем же идентификатором безопасности, что правила указанного аудита из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    protected void RemoveAuditRuleAll(AuditRule rule)
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
    ///   Удаляет все правила аудита, которые точно соответствуют указанным аудита правила из системного списка (Управления доступом) связанный с этим <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    protected void RemoveAuditRuleSpecific(AuditRule rule)
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
    ///   Указывает, является ли идентификатор безопасности, для которого извлекаются правила доступа введите T:System.Security.Principal.SecurityIdentifier или T:System.Security.Principal.NTAccount.
    ///    Значение этого параметра должен быть типом, который можно преобразовать в <see cref="T:System.Security.Principal.SecurityIdentifier" /> типа.
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
