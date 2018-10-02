// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeySecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Предоставляет возможность управления доступом к объекту ключа шифрования без непосредственной работы со из списка управления доступом (ACL).
  /// </summary>
  public sealed class CryptoKeySecurity : NativeObjectSecurity
  {
    private const ResourceType s_ResourceType = ResourceType.FileObject;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.CryptoKeySecurity" />.
    /// </summary>
    public CryptoKeySecurity()
      : base(false, ResourceType.FileObject)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> используя указанный дескриптор безопасности.
    /// </summary>
    /// <param name="securityDescriptor">
    ///   Дескриптор безопасности для создания нового <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </param>
    [SecuritySafeCritical]
    public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
      : base(ResourceType.FileObject, securityDescriptor)
    {
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
    public override sealed AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new CryptoKeyAccessRule(identityReference, CryptoKeyAccessRule.RightsFromAccessMask(accessMask), type);
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
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AuditRule" />, создаваемый с помощью данного метода.
    /// </returns>
    public override sealed AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new CryptoKeyAuditRule(identityReference, CryptoKeyAuditRule.RightsFromAccessMask(accessMask), flags);
    }

    /// <summary>
    ///   Добавляет указанное правило доступа для управления доступом список (DACL) связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило доступа.</param>
    public void AddAccessRule(CryptoKeyAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа, содержащие те же идентификатор безопасности и квалификатор, указанное правило доступа в список (ДОСТУПОМ) связан с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Чтобы задать правило доступа.</param>
    public void SetAccessRule(CryptoKeyAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа в списке (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, а затем добавляет указанное правило доступа.
    /// </summary>
    /// <param name="rule">Правила доступа для сброса.</param>
    public void ResetAccessRule(CryptoKeyAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет правила доступа, содержащие же идентификатор безопасности маску доступа, что указанное правило доступа из списка (ДОСТУПОМ), связанного с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    /// <returns>
    ///   <see langword="true" /> Если правило доступа успешно удалено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAccessRule(CryptoKeyAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа с тем же идентификатором безопасности, что указанное правило доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила доступа, в точности совпадающие с указанным правилом доступа из списка (ДОСТУПОМ) связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило доступа.</param>
    public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>
    ///   Добавляет указанного аудита правила в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Чтобы добавить правило аудита.</param>
    public void AddAuditRule(CryptoKeyAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита, содержащие те же идентификатор безопасности и квалификатор, как правила указанного аудита в список управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, а затем добавляет указанное правило аудита.
    /// </summary>
    /// <param name="rule">Чтобы задать правило аудита.</param>
    public void SetAuditRule(CryptoKeyAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет правила аудита, содержащие же идентификатор безопасности маску доступа, как правила указанного аудита из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    /// <returns>
    ///   <see langword="true" /> Если правило аудита успешно удалено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAuditRule(CryptoKeyAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита с тем же идентификатором безопасности, что правила указанного аудита из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита, которые точно соответствуют указанным аудита правила из системного списка (Управления доступом) связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <param name="rule">Удаляемое правило аудита.</param>
    public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> защищаемого объекта, связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </summary>
    /// <returns>
    ///   Тип защищаемого объекта, связанный с этим <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (CryptoKeyRights);
      }
    }

    /// <summary>
    ///   Получает <see cref="T:System.Type" /> объекта, связанного с правилами доступа этого объекта <see cref="T:System.Security.AccessControl.CryptoKeySecurity" />.
    ///    Объект <see cref="T:System.Security.Principal.SecurityIdentifier" /> должен быть объектом, который может быть приведен как объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами доступа этого <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (CryptoKeyAccessRule);
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Type" />, связанный с правилами аудита этого объекта <see cref="T:System.Security.AccessControl.CryptoKeySecurity" />.
    ///    Объект <see cref="T:System.Type" /> должен быть объектом, который может быть приведен как объект <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта, связанного с правилами аудита этого объекта <see cref="T:System.Security.AccessControl.CryptoKeySecurity" />.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (CryptoKeyAuditRule);
      }
    }

    internal AccessControlSections ChangedAccessControlSections
    {
      [SecurityCritical] get
      {
        AccessControlSections accessControlSections = AccessControlSections.None;
        bool flag = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            this.ReadLock();
            flag = true;
          }
          if (this.AccessRulesModified)
            accessControlSections |= AccessControlSections.Access;
          if (this.AuditRulesModified)
            accessControlSections |= AccessControlSections.Audit;
          if (this.GroupModified)
            accessControlSections |= AccessControlSections.Group;
          if (this.OwnerModified)
            accessControlSections |= AccessControlSections.Owner;
        }
        finally
        {
          if (flag)
            this.ReadUnlock();
        }
        return accessControlSections;
      }
    }
  }
}
