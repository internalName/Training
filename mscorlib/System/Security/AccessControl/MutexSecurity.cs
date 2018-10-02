// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.MutexSecurity
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
  ///   Обеспечивает безопасность управления доступом Windows для именованного мьютекса.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class MutexSecurity : NativeObjectSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.MutexSecurity" /> со значениями по умолчанию.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот класс не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    public MutexSecurity()
      : base(true, ResourceType.KernelObject)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.MutexSecurity" /> класса заданные разделы безопасности правил управления доступом из системного мьютекса с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя системного мьютекса, должны быть получены, правила безопасности управления доступом.
    /// </param>
    /// <param name="includeSections">
    ///   Сочетание <see cref="T:System.Security.AccessControl.AccessControlSections" /> флаги, определяющие разделы для получения.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Нет системный объект с указанным именем.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот класс не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    [SecuritySafeCritical]
    public MutexSecurity(string name, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), (object) null)
    {
    }

    [SecurityCritical]
    internal MutexSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, (SafeHandle) handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), (object) null)
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
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.MutexRights" /> значения, определяющие права доступа, чтобы разрешить или запретить, приводится к целому типу.
    /// </param>
    /// <param name="isInherited">
    ///   Нет смысла использовать для именованных мьютексов, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Нет смысла использовать для именованных мьютексов, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="propagationFlags">
    ///   Нет смысла использовать для именованных мьютексов, так как у них отсутствует иерархия.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAccessRule" /> объект, представляющий указанные права для указанного пользователя.
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
      return (AccessRule) new MutexAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>
    ///   Создает новое правило аудита, задав пользователя, которому относится правило, права доступа для аудита и результат, вызывающее срабатывание правила аудита.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Идентифицирующие пользователя или группы правило применяется к.
    /// </param>
    /// <param name="accessMask">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.MutexRights" /> значения, определяющие права доступа для аудита, приводится к целому типу.
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
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAuditRule" /> объект, представляющий указанное правило аудита для указанного пользователя.
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
      return (AuditRule) new MutexAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
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
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
    ///   <paramref name="rule " />Невозможно сопоставить известному удостоверению.
    /// </exception>
    public void AddAccessRule(MutexAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.MutexAccessRule" />.
    ///    Пользователь и <see cref="T:System.Security.AccessControl.AccessControlType" /> данного правила определяют правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAccessRule(MutexAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила управления доступом с тем же пользователем, что и указанного правила, вне зависимости от <see cref="T:System.Security.AccessControl.AccessControlType" />, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.MutexAccessRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void ResetAccessRule(MutexAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанное правило и с совместимой флагами наследования и распространения; при обнаружении такого правила, содержащиеся в указанном правиле доступа права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAccessRule" /> указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска и набор флагов наследования и распространения, соответствующее правило, если найден, должен быть совместим с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAccessRule(MutexAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил управления доступом с тем же пользователем и <see cref="T:System.Security.AccessControl.AccessControlType" /> ("Разрешить" или "Запретить") как указанного правила и, если он найден, удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAccessRule" /> указывающий пользователя, и <see cref="T:System.Security.AccessControl.AccessControlType" /> для поиска.
    ///    Все указанные в этом правиле права игнорируются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleAll(MutexAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления доступом, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAccessRule" /> для удаления.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleSpecific(MutexAccessRule rule)
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
    public void AddAuditRule(MutexAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила с тем же пользователем, что и у указанного правила аудита независимо от <see cref="T:System.Security.AccessControl.AuditFlags" /> значение, а затем добавляет указанное правило.
    /// </summary>
    /// <param name="rule">
    ///   Добавляемый объект <see cref="T:System.Security.AccessControl.MutexAuditRule" />.
    ///    Пользователь, указанный в этом правиле определяет правила, чтобы удалить перед добавлением данного правила.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAuditRule(MutexAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила управления аудитом с тем же пользователем, что и у указанного правила и совместимые флагами наследования и распространения; Если совместимое правило найдено, содержащиеся в указанном правиле права удаляются из него.
    /// </summary>
    /// <param name="rule">
    ///   A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> Указывает пользователя для поиска и набор флагов наследования и распространения, соответствующее правило, если таковые имеются, должны быть совместимы с.
    ///    Указывает, что права на удаление из совместимого правила, если найден.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совместимое правило найдено; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAuditRule(MutexAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск всех правил с тем же пользователем, что и у указанного правила аудита и, если таковые имеются, и удаляет их.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexAuditRule" /> указывающий пользователя для поиска.
    ///    Все указанные в этом правиле права игнорируются.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleAll(MutexAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Осуществляет поиск правила аудита, в точности соответствующего указанному правилу и удаляет найденное его.
    /// </summary>
    /// <param name="rule">
    ///   Удаляемый объект <see cref="T:System.Security.AccessControl.MutexAuditRule" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleSpecific(MutexAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Возвращает перечисление, которое <see cref="T:System.Security.AccessControl.MutexSecurity" /> класс использует для представления права доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.MutexRights" /> перечисления.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (MutexRights);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.MutexSecurity" /> класс использует для представления правила доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.MutexAccessRule" /> класса.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (MutexAccessRule);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.MutexSecurity" /> класс использует для представления правила аудита.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.MutexAuditRule" /> класса.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (MutexAuditRule);
      }
    }
  }
}
