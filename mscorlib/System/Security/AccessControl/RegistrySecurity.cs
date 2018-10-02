// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistrySecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Обеспечивает безопасность управления доступом Windows для раздела реестра.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class RegistrySecurity : NativeObjectSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.RegistrySecurity" /> со значениями по умолчанию.
    /// </summary>
    public RegistrySecurity()
      : base(true, ResourceType.RegistryKey)
    {
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal RegistrySecurity(SafeRegistryHandle hKey, string name, AccessControlSections includeSections)
      : base(true, ResourceType.RegistryKey, (SafeHandle) hKey, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(RegistrySecurity._HandleErrorCode), (object) null)
    {
      new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.View, name).Demand();
    }

    [SecurityCritical]
    private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
    {
      Exception exception = (Exception) null;
      switch (errorCode)
      {
        case 2:
          exception = (Exception) new IOException(Environment.GetResourceString("Arg_RegKeyNotFound", (object) errorCode));
          break;
        case 6:
          exception = (Exception) new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
          break;
        case 123:
          exception = (Exception) new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", (object) nameof (name)));
          break;
      }
      return exception;
    }

    /// <summary>
    ///   Создает новое правило управления доступом для указанного пользователя с указанными правами доступа, управление доступом и флаги.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Идентифицирующие пользователя или группы правило применяется к.
    /// </param>
    /// <param name="accessMask">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, определяющие права доступа, чтобы разрешить или запретить, приводится к целому типу.
    /// </param>
    /// <param name="isInherited">
    ///   Логическое значение, указывающее, наследуется ли правило.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значения, указывающие, как правило, будет наследоваться подразделов.
    /// </param>
    /// <param name="propagationFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> значения, которые влияют на способ правило наследуется подразделов.
    ///    Если значение <paramref name="inheritanceFlags" /> является <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> объект, представляющий указанные права для указанного пользователя.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, или <paramref name="type" /> задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identityReference" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="accessMask" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identityReference" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" />, ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new RegistryAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>
    ///   Создает новое правило аудита, указав пользователя, к которому относится правило, права доступа для аудита, наследования и распространения правила и результат, вызывающее срабатывание правила.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Идентифицирующие пользователя или группы правило применяется к.
    /// </param>
    /// <param name="accessMask">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, определяющие права доступа для аудита, приводится к целому типу.
    /// </param>
    /// <param name="isInherited">
    ///   Логическое значение, указывающее, наследуется ли правило.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значения, указывающие, как правило, будет наследоваться подразделов.
    /// </param>
    /// <param name="propagationFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> значения, которые влияют на способ правило наследуется подразделов.
    ///    Если значение <paramref name="inheritanceFlags" /> является <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="flags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.AuditFlags" /> значения, указывающие, следует ли проводить аудит успешного доступа и отказ в доступе.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> объект, представляющий указанное правило аудита для указанного пользователя, с помощью указанных флагов.
    ///    Возвращаемый тип метода является базовым классом, <see cref="T:System.Security.AccessControl.AuditRule" />, но возвращаемое значение может быть безопасно приведено к производному классу.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, или <paramref name="flags" /> задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identityReference" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="accessMask" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identityReference" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" />, ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new RegistryAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
    }

    internal AccessControlSections GetAccessControlSectionsFromChanges()
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

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal void Persist(SafeRegistryHandle hKey, string keyName)
    {
      new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.Change, keyName).Demand();
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        if (sectionsFromChanges == AccessControlSections.None)
          return;
        this.Persist((SafeHandle) hKey, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Выполняет поиск подходящего элемента управления доступом, с которым можно объединить новое правило.
    ///    Если элемент не найден, добавляет новое правило.
    /// </summary>
    /// <param name="rule">Правила управления доступом.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddAccessRule(RegistryAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" />.
    ///    Пользователь и <see cref="T:System.Security.AccessControl.AccessControlType" /> данного правила определяют правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAccessRule(RegistryAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем, что и указанного правила, вне зависимости от <see cref="T:System.Security.AccessControl.AccessControlType" />, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    public void ResetAccessRule(RegistryAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как у указанного правила доступа, а также с совместимой флагами наследования и распространения; при обнаружении такого правила, содержащиеся в указанном правиле доступа права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска и набор флагов наследования и распространения, соответствующее правило, если найден, должен быть совместим с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAccessRule(RegistryAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила и, если он найден, удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска.
    ///    Все права, флаги наследования или флаги распространения, указанные в этом правиле учитываются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleAll(RegistryAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> для удаления.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила аудита, с которым можно объединить новое правило.
    ///    Если элемент не найден, добавляет новое правило.
    /// </summary>
    /// <param name="rule">
    ///   Чтобы добавить правило аудита.
    ///    Пользователь, указанный в этом правиле определяет поиска.
    /// </param>
    public void AddAuditRule(RegistryAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила с тем же пользователем, что и у указанного правила аудита независимо от <see cref="T:System.Security.AccessControl.AuditFlags" /> значение, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.RegistryAuditRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAuditRule(RegistryAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления аудитом с тем же пользователем, что и у указанного правила и совместимые флагами наследования и распространения; Если совместимое правило найдено, содержащиеся в указанном правиле права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> Указывает пользователя для поиска и набор флагов наследования и распространения, соответствующее правило, если таковые имеются, должны быть совместимы с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAuditRule(RegistryAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил с тем же пользователем, что и у указанного правила аудита и, если таковые имеются, и удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> указывающий пользователя для поиска.
    ///    Все права, флаги наследования или флаги распространения, указанные в этом правиле учитываются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleAll(RegistryAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила аудита, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Удаляемый объект <see cref="T:System.Security.AccessControl.RegistryAuditRule" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Получает тип перечисления, <see cref="T:System.Security.AccessControl.RegistrySecurity" /> класс использует для представления права доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.RegistryRights" /> перечисления.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (RegistryRights);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.RegistrySecurity" /> класс использует для представления правила доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> класса.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (RegistryAccessRule);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.RegistrySecurity" /> класс использует для представления правила аудита.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> класса.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (RegistryAuditRule);
      }
    }
  }
}
