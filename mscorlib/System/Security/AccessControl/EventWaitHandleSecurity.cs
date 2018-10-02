// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет безопасности управления доступом Windows для именованного системного дескриптора ожидания.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class EventWaitHandleSecurity : NativeObjectSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> со значениями по умолчанию.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот класс не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    public EventWaitHandleSecurity()
      : base(true, ResourceType.KernelObject)
    {
    }

    [SecurityCritical]
    internal EventWaitHandleSecurity(string name, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), (object) null)
    {
    }

    [SecurityCritical]
    internal EventWaitHandleSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, (SafeHandle) handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), (object) null)
    {
    }

    [SecurityCritical]
    private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
    {
      Exception exception = (Exception) null;
      if (errorCode == 2 || errorCode == 6 || errorCode == 123)
      {
        if (name != null && name.Length != 0)
          exception = (Exception) new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        else
          exception = (Exception) new WaitHandleCannotBeOpenedException();
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
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> значения, определяющие права доступа, чтобы разрешить или запретить, приводится к целому типу.
    /// </param>
    /// <param name="isInherited">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="propagationFlags">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> Объект, представляющий указанные права для указанного пользователя.
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
      return (AccessRule) new EventWaitHandleAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>
    ///   Создает новое правило аудита, задав пользователя, которому относится правило, права доступа для аудита и результат, вызывающее срабатывание правила аудита.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Идентифицирующие пользователя или группы правило применяется к.
    /// </param>
    /// <param name="accessMask">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> значения, определяющие права доступа для аудита, приводится к целому типу.
    /// </param>
    /// <param name="isInherited">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="propagationFlags">
    ///   Нет смысла использовать для именованных дескрипторов ожидания, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="flags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.AuditFlags" /> значения, указывающие, следует ли проводить аудит успешного доступа и отказ в доступе.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> Объект, представляющий указанное правило аудита для указанного пользователя.
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
      return (AuditRule) new EventWaitHandleAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
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
    internal void Persist(SafeWaitHandle handle)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        if (sectionsFromChanges == AccessControlSections.None)
          return;
        this.Persist((SafeHandle) handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Выполняет поиск соответствующего правила управления доступом, с которым можно объединить новое правило.
    ///    Если элемент не найден, добавляет новое правило.
    /// </summary>
    /// <param name="rule">Правила управления доступом.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddAccessRule(EventWaitHandleAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />.
    ///    Пользователь и <see cref="T:System.Security.AccessControl.AccessControlType" /> данного правила определяют правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAccessRule(EventWaitHandleAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем, что и указанного правила, вне зависимости от <see cref="T:System.Security.AccessControl.AccessControlType" />, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void ResetAccessRule(EventWaitHandleAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как у указанного правила доступа, а также с совместимой флагами наследования и распространения; при обнаружении такого правила, содержащиеся в указанном правиле доступа права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> Указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска и набор флагов наследования и распространения, соответствующее правило, если найден, должен быть совместим с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила и, если он найден, удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> Указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска.
    ///    Все указанные в этом правиле права игнорируются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> для удаления.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
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
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddAuditRule(EventWaitHandleAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила с тем же пользователем, что и у указанного правила аудита независимо от <see cref="T:System.Security.AccessControl.AuditFlags" /> значение, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAuditRule(EventWaitHandleAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила аудита с тем же пользователем, что и у указанного правила и совместимые флагами наследования и распространения; Если совместимое правило найдено, содержащиеся в указанном правиле права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> Указывает пользователя для поиска и набор флагов наследования и распространения, соответствующее правило, если таковые имеются, должны быть совместимы с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил с тем же пользователем, что и у указанного правила аудита и, если таковые имеются, и удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> Указывающий пользователя для поиска.
    ///    Все указанные в этом правиле права игнорируются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила аудита, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> для удаления.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Получает тип перечисления, <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> класс использует для представления права доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> перечисления.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (EventWaitHandleRights);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> класс использует для представления правила доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> класса.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (EventWaitHandleAccessRule);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> класс использует для представления правила аудита.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> класса.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (EventWaitHandleAuditRule);
      }
    }
  }
}
