// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemSecurity
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
  ///   Представляет элемент управления доступом и аудита безопасности для файла или каталога.
  /// </summary>
  public abstract class FileSystemSecurity : NativeObjectSecurity
  {
    private const ResourceType s_ResourceType = ResourceType.FileObject;

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer)
      : base(isContainer, ResourceType.FileObject, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isContainer)
    {
    }

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections, bool isDirectory)
      : base(isContainer, ResourceType.FileObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isDirectory)
    {
    }

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer, SafeFileHandle handle, AccessControlSections includeSections, bool isDirectory)
      : base(isContainer, ResourceType.FileObject, (SafeHandle) handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isDirectory)
    {
    }

    [SecurityCritical]
    private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
    {
      Exception exception = (Exception) null;
      switch (errorCode)
      {
        case 2:
          exception = context == null || !(context is bool) || !(bool) context ? (name == null || name.Length == 0 ? (Exception) new FileNotFoundException() : (Exception) new FileNotFoundException(name)) : (name == null || name.Length == 0 ? (Exception) new DirectoryNotFoundException() : (Exception) new DirectoryNotFoundException(name));
          break;
        case 6:
          exception = (Exception) new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
          break;
        case 123:
          exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), nameof (name));
          break;
      }
      return exception;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класс, представляющий новое правило управления доступом для указанного пользователя с указанными правами доступа, управление доступом и флаги.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объект, который представляет учетную запись пользователя.
    /// </param>
    /// <param name="accessMask">
    ///   Целое число, задающее тип доступа.
    /// </param>
    /// <param name="isInherited">
    ///   <see langword="true" /> Если правило доступа наследуется; в противном случае — <see langword="false" />.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, определяющих способ распространения маски доступа к дочерним объектам.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих способ распространения записи управления доступом (ACE) к дочерним объектам.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывает ли доступ разрешен или запрещен.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, представляющий новое правило управления доступом для указанного пользователя с указанными правами доступа, управление доступом и флаги.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, Или <paramref name="type" /> параметров задано недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identityReference" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="accessMask" /> Параметра равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identityReference" /> Параметр не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" />, ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public override sealed AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new FileSystemAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> класс, представляющий указанное правило аудита для указанного пользователя.
    /// </summary>
    /// <param name="identityReference">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объект, который представляет учетную запись пользователя.
    /// </param>
    /// <param name="accessMask">
    ///   Целое число, задающее тип доступа.
    /// </param>
    /// <param name="isInherited">
    ///   <see langword="true" /> Если правило доступа наследуется; в противном случае — <see langword="false" />.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, определяющих способ распространения маски доступа к дочерним объектам.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих способ распространения записи управления доступом (ACE) к дочерним объектам.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Security.AccessControl.AuditFlags" /> значения, которое указывает тип аудита для выполнения.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> объект, представляющий указанное правило аудита для указанного пользователя.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, Или <paramref name="flags" /> свойств указано недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <paramref name="identityReference" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="accessMask" /> Равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identityReference" /> Свойство не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" />, ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public override sealed AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new FileSystemAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
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
    internal void Persist(string fullPath)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath, false, true);
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(fullPath, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal void Persist(SafeFileHandle handle, string fullPath)
    {
      if (fullPath != null)
        FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath, false, true);
      else
        FileIOPermission.QuickDemand(PermissionState.Unrestricted);
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist((SafeHandle) handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Добавляет указанное разрешение списка управления Доступом к текущему файлу или каталогу.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, представляющий список управления Доступом разрешение для добавления к файлу или каталогу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddAccessRule(FileSystemAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Устанавливает заданный тип доступа (ACL) разрешение списка для текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, представляющий список управления Доступом разрешение должно быть задано для файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAccessRule(FileSystemAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Добавляет указанное разрешение списка управления Доступом к текущему файлу или каталогу и удаляет все соответствующие разрешения списка управления Доступом.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, представляющий список управления Доступом разрешение для добавления к файлу или каталогу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void ResetAccessRule(FileSystemAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет все подходящие разрешить или запретить доступ списка управления Доступом из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, представляющий список управления Доступом разрешение для удаления из файла или каталога.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если правило доступа удалено; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAccessRule(FileSystemAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      AuthorizationRuleCollection accessRules = this.GetAccessRules(true, true, rule.IdentityReference.GetType());
      for (int index = 0; index < accessRules.Count; ++index)
      {
        FileSystemAccessRule systemAccessRule = accessRules[index] as FileSystemAccessRule;
        if (systemAccessRule != null && systemAccessRule.FileSystemRights == rule.FileSystemRights && (systemAccessRule.IdentityReference == rule.IdentityReference && systemAccessRule.AccessControlType == rule.AccessControlType))
          return this.RemoveAccessRule((AccessRule) rule);
      }
      return this.RemoveAccessRule((AccessRule) new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType));
    }

    /// <summary>
    ///   Удаляет все разрешения списка управления Доступом управления доступом для указанного пользователя из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, который указывает, доступ к контролировать разрешения списка управления Доступом пользователя должны быть удалены из файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleAll(FileSystemAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>
    ///   Удаляет одно подходящее разрешить или запретить разрешение списка управления Доступом из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объект, который указывает, доступ к контролировать разрешения списка управления Доступом пользователя должны быть удалены из файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException(nameof (rule));
      AuthorizationRuleCollection accessRules = this.GetAccessRules(true, true, rule.IdentityReference.GetType());
      for (int index = 0; index < accessRules.Count; ++index)
      {
        FileSystemAccessRule systemAccessRule = accessRules[index] as FileSystemAccessRule;
        if (systemAccessRule != null && systemAccessRule.FileSystemRights == rule.FileSystemRights && (systemAccessRule.IdentityReference == rule.IdentityReference && systemAccessRule.AccessControlType == rule.AccessControlType))
        {
          this.RemoveAccessRuleSpecific((AccessRule) rule);
          return;
        }
      }
      this.RemoveAccessRuleSpecific((AccessRule) new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType));
    }

    /// <summary>
    ///   Добавляет указанное правило аудита к текущему файлу или каталогу.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  представляющий правило аудита для добавления к файлу или каталогу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddAuditRule(FileSystemAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Задает указанное правило аудита для текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> представляющий правило аудита для файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetAuditRule(FileSystemAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все подходящие разрешения или запрещающие правила аудита из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  представляющий правило аудита для удаления из файла или каталога.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если правило аудита удалено; в противном случае — <see langword="false" />
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool RemoveAuditRule(FileSystemAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет все правила аудита для указанного пользователя из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> указывающий пользователя, чьи правила аудита должны быть удалены из файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleAll(FileSystemAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>
    ///   Удаляет одно подходящее разрешающее или запрещающее правило аудита из текущего файла или каталога.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  представляющий правило аудита для удаления из файла или каталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rule" /> имеет значение <see langword="null" />.
    /// </exception>
    public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }

    /// <summary>
    ///   Возвращает перечисление, которое <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> класс использует для представления права доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.FileSystemRights" /> перечисления.
    /// </returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (FileSystemRights);
      }
    }

    /// <summary>
    ///   Возвращает перечисление, которое <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> класс использует для представления правила доступа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класса.
    /// </returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (FileSystemAccessRule);
      }
    }

    /// <summary>
    ///   Возвращает тип, который <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> класс использует для представления правила аудита.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> класса.
    /// </returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (FileSystemAuditRule);
      }
    }
  }
}
