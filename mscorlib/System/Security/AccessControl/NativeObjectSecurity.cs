// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.NativeObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Предоставляет возможность управления доступом к собственным объектам без непосредственной работы со списками управления доступом (ACL).
  ///    Типы собственных объектов определяются перечислением <see cref="T:System.Security.AccessControl.ResourceType" />.
  /// </summary>
  public abstract class NativeObjectSecurity : CommonObjectSecurity
  {
    private readonly uint ProtectedDiscretionaryAcl = 2147483648;
    private readonly uint ProtectedSystemAcl = 1073741824;
    private readonly uint UnprotectedDiscretionaryAcl = 536870912;
    private readonly uint UnprotectedSystemAcl = 268435456;
    private readonly ResourceType _resourceType;
    private NativeObjectSecurity.ExceptionFromErrorCode _exceptionFromErrorCode;
    private object _exceptionContext;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> с использованием указанных значений.
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType)
      : base(isContainer)
    {
      this._resourceType = resourceType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, используя указанные значения.
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="exceptionFromErrorCode">
    ///   Делегат, реализованный интеграторами, предоставляющий пользовательские исключения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(isContainer, resourceType)
    {
      this._exceptionContext = exceptionContext;
      this._exceptionFromErrorCode = exceptionFromErrorCode;
    }

    [SecurityCritical]
    internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor)
      : this(resourceType, securityDescriptor, (NativeObjectSecurity.ExceptionFromErrorCode) null)
    {
    }

    [SecurityCritical]
    internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode)
      : base(securityDescriptor)
    {
      this._resourceType = resourceType;
      this._exceptionFromErrorCode = exceptionFromErrorCode;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> с использованием указанных значений.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для включения в этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="exceptionFromErrorCode">
    ///   Делегат, реализованный интеграторами, предоставляющий пользовательские исключения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, name, (SafeHandle) null, includeSections, true, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> с использованием указанных значений.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для включения в этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
      : this(isContainer, resourceType, name, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> с использованием указанных значений.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для включения в этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="exceptionFromErrorCode">
    ///   Делегат, реализованный интеграторами, предоставляющий пользовательские исключения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, (string) null, handle, includeSections, false, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> с использованием указанных значений.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="isContainer">
    ///   Значение <see langword="true" />, если новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> является объектом контейнера.
    /// </param>
    /// <param name="resourceType">
    ///   Тип защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым связан новый объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для включения в этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
      : this(isContainer, resourceType, handle, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    [SecurityCritical]
    private static CommonSecurityDescriptor CreateInternal(ResourceType resourceType, bool isContainer, string name, SafeHandle handle, AccessControlSections includeSections, bool createByName, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
    {
      if (createByName && name == null)
        throw new ArgumentNullException(nameof (name));
      if (!createByName && handle == null)
        throw new ArgumentNullException(nameof (handle));
      RawSecurityDescriptor resultSd;
      int securityInfo = Win32.GetSecurityInfo(resourceType, name, handle, includeSections, out resultSd);
      if (securityInfo != 0)
      {
        Exception exception = (Exception) null;
        if (exceptionFromErrorCode != null)
          exception = exceptionFromErrorCode(securityInfo, name, handle, exceptionContext);
        if (exception == null)
        {
          switch (securityInfo)
          {
            case 2:
              exception = name == null ? (Exception) new FileNotFoundException() : (Exception) new FileNotFoundException(name);
              break;
            case 5:
              exception = (Exception) new UnauthorizedAccessException();
              break;
            case 87:
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) securityInfo));
              break;
            case 123:
              exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), nameof (name));
              break;
            case 1307:
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
              break;
            case 1308:
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
              break;
            case 1350:
              exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
              break;
            default:
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) securityInfo));
              break;
          }
        }
        throw exception;
      }
      return new CommonSecurityDescriptor(isContainer, false, resultSd, true);
    }

    [SecurityCritical]
    private void Persist(string name, SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
    {
      this.WriteLock();
      try
      {
        SecurityInfos securityInformation = (SecurityInfos) 0;
        SecurityIdentifier owner = (SecurityIdentifier) null;
        SecurityIdentifier group = (SecurityIdentifier) null;
        SystemAcl systemAcl = (SystemAcl) null;
        DiscretionaryAcl discretionaryAcl = (DiscretionaryAcl) null;
        if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this._securityDescriptor.Owner != (SecurityIdentifier) null)
        {
          securityInformation |= SecurityInfos.Owner;
          owner = this._securityDescriptor.Owner;
        }
        if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this._securityDescriptor.Group != (SecurityIdentifier) null)
        {
          securityInformation |= SecurityInfos.Group;
          group = this._securityDescriptor.Group;
        }
        if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
        {
          SecurityInfos securityInfos = securityInformation | SecurityInfos.SystemAcl;
          systemAcl = !this._securityDescriptor.IsSystemAclPresent || this._securityDescriptor.SystemAcl == null || this._securityDescriptor.SystemAcl.Count <= 0 ? (SystemAcl) null : this._securityDescriptor.SystemAcl;
          securityInformation = (this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) == ControlFlags.None ? securityInfos | (SecurityInfos) this.UnprotectedSystemAcl : securityInfos | (SecurityInfos) this.ProtectedSystemAcl;
        }
        if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this._securityDescriptor.IsDiscretionaryAclPresent)
        {
          SecurityInfos securityInfos = securityInformation | SecurityInfos.DiscretionaryAcl;
          discretionaryAcl = !this._securityDescriptor.DiscretionaryAcl.EveryOneFullAccessForNullDacl ? this._securityDescriptor.DiscretionaryAcl : (DiscretionaryAcl) null;
          securityInformation = (this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) == ControlFlags.None ? securityInfos | (SecurityInfos) this.UnprotectedDiscretionaryAcl : securityInfos | (SecurityInfos) this.ProtectedDiscretionaryAcl;
        }
        if (securityInformation == (SecurityInfos) 0)
          return;
        int errorCode = Win32.SetSecurityInfo(this._resourceType, name, handle, securityInformation, owner, group, (GenericAcl) systemAcl, (GenericAcl) discretionaryAcl);
        if (errorCode != 0)
        {
          Exception exception = (Exception) null;
          if (this._exceptionFromErrorCode != null)
            exception = this._exceptionFromErrorCode(errorCode, name, handle, exceptionContext);
          if (exception == null)
          {
            switch (errorCode)
            {
              case 2:
                exception = (Exception) new FileNotFoundException();
                break;
              case 5:
                exception = (Exception) new UnauthorizedAccessException();
                break;
              case 6:
                exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_InvalidHandle"));
                break;
              case 123:
                exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), nameof (name));
                break;
              case 1307:
                exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
                break;
              case 1308:
                exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
                break;
              case 1350:
                exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
                break;
              default:
                exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) errorCode));
                break;
            }
          }
          throw exception;
        }
        this.OwnerModified = false;
        this.GroupModified = false;
        this.AccessRulesModified = false;
        this.AuditRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Защищаемый объект, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, является каталогом или файлом, и ни один из них не найден.
    /// </exception>
    protected override sealed void Persist(string name, AccessControlSections includeSections)
    {
      this.Persist(name, includeSections, this._exceptionContext);
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Защищаемый объект, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, является каталогом или файлом, и ни один из них не найден.
    /// </exception>
    [SecuritySafeCritical]
    protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.Persist(name, (SafeHandle) null, includeSections, exceptionContext);
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор и методы сохранения, были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Защищаемый объект, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, является каталогом или файлом, и ни один из них не найден.
    /// </exception>
    [SecuritySafeCritical]
    protected override sealed void Persist(SafeHandle handle, AccessControlSections includeSections)
    {
      this.Persist(handle, includeSections, this._exceptionContext);
    }

    /// <summary>
    ///   Сохраняет указанные разделы дескриптора безопасности, связанные с этим объектом <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, в постоянном хранилище.
    ///    Рекомендуется, чтобы значения параметров <paramref name="includeSections" />, переданные в конструктор, и методы сохранения были идентичными.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений перечисления <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее разделы дескриптора безопасности (правила доступа, правила аудита, владелец, основная группа) защищаемого объекта для сохранения.
    /// </param>
    /// <param name="exceptionContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Защищаемый объект, с которым связан этот объект <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />, является каталогом или файлом, и ни один из них не найден.
    /// </exception>
    [SecuritySafeCritical]
    protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
    {
      if (handle == null)
        throw new ArgumentNullException(nameof (handle));
      this.Persist((string) null, handle, includeSections, exceptionContext);
    }

    /// <summary>
    ///   Предоставляет способ для системных интеграторов для сопоставления числовых кодов ошибок для определенных исключений, которые они создают.
    /// </summary>
    /// <param name="errorCode">Числовой код ошибки.</param>
    /// <param name="name">
    ///   Имя защищаемого объекта, с которым <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> связан объект.
    /// </param>
    /// <param name="handle">
    ///   Дескриптор защищаемого объекта, с которым <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> связан объект.
    /// </param>
    /// <param name="context">
    ///   Объект, содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Exception" /> Создает этот делегат.
    /// </returns>
    [SecuritySafeCritical]
    protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);
  }
}
