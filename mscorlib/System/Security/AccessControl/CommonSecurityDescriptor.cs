// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет дескриптор безопасности.
  ///    Дескриптор безопасности включает владельца, основную группу, список управления доступом на уровне пользователей (DACL) и системный список управления доступом управления (SACL).
  /// </summary>
  public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
  {
    private bool _isContainer;
    private bool _isDS;
    private RawSecurityDescriptor _rawSd;
    private SystemAcl _sacl;
    private DiscretionaryAcl _dacl;

    private void CreateFromParts(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
    {
      if (systemAcl != null && systemAcl.IsContainer != isContainer)
        throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), nameof (systemAcl));
      if (discretionaryAcl != null && discretionaryAcl.IsContainer != isContainer)
        throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), nameof (discretionaryAcl));
      this._isContainer = isContainer;
      if (systemAcl != null && systemAcl.IsDS != isDS)
        throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), nameof (systemAcl));
      if (discretionaryAcl != null && discretionaryAcl.IsDS != isDS)
        throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), nameof (discretionaryAcl));
      this._isDS = isDS;
      this._sacl = systemAcl;
      if (discretionaryAcl == null)
        discretionaryAcl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this._isDS, this._isContainer);
      this._dacl = discretionaryAcl;
      ControlFlags controlFlags = flags | ControlFlags.DiscretionaryAclPresent;
      this._rawSd = new RawSecurityDescriptor(systemAcl != null ? controlFlags | ControlFlags.SystemAclPresent : controlFlags & ~ControlFlags.SystemAclPresent, owner, group, systemAcl == null ? (RawAcl) null : systemAcl.RawAcl, discretionaryAcl.RawAcl);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> класс из указанной информации.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом-контейнером.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом directory.
    /// </param>
    /// <param name="flags">
    ///   Флаги, определяющие поведение нового <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="owner">
    ///   Владельца для нового <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="group">
    ///   Основной группы для нового <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="systemAcl">
    ///   Список управления доступом системы (SACL) для нового <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="discretionaryAcl">
    ///   Список (ДОСТУПОМ) для нового <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
    {
      this.CreateFromParts(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
    }

    private CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
      : this(isContainer, isDS, flags, owner, group, systemAcl == null ? (SystemAcl) null : new SystemAcl(isContainer, isDS, systemAcl), discretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(isContainer, isDS, discretionaryAcl))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> класс из указанного <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом-контейнером.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом directory.
    /// </param>
    /// <param name="rawSecurityDescriptor">
    ///   <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> Объект, из которого будет создан новый <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
      : this(isContainer, isDS, rawSecurityDescriptor, false)
    {
    }

    internal CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor, bool trusted)
    {
      if (rawSecurityDescriptor == null)
        throw new ArgumentNullException(nameof (rawSecurityDescriptor));
      this.CreateFromParts(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, rawSecurityDescriptor.SystemAcl == null ? (SystemAcl) null : new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl, trusted), rawSecurityDescriptor.DiscretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl, trusted));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> класс из указанной строки языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом-контейнером.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом directory.
    /// </param>
    /// <param name="sddlForm">
    ///   Строка SDDL, из которой будет создан новый <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
      : this(isContainer, isDS, new RawSecurityDescriptor(sddlForm), true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> класс из указанного массива значений типа byte.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом-контейнером.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый дескриптор безопасности связан с объектом directory.
    /// </param>
    /// <param name="binaryForm">
    ///   Массив байтовых значений, из которого будет создан новый <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="offset">
    ///   Смещение в <paramref name="binaryForm" /> массиве, с которого начинается копирование.
    /// </param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
      : this(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset), true)
    {
    }

    internal override sealed GenericAcl GenericSacl
    {
      get
      {
        return (GenericAcl) this._sacl;
      }
    }

    internal override sealed GenericAcl GenericDacl
    {
      get
      {
        return (GenericAcl) this._dacl;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли объект, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> является объектом контейнера.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если объект, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объект является контейнером; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsContainer
    {
      get
      {
        return this._isContainer;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли объект, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> является объектом directory.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если объект, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> является объектом каталога; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsDS
    {
      get
      {
        return this._isDS;
      }
    }

    /// <summary>
    ///   Возвращает значения, которые определяют поведение <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Одно или несколько значений перечисления <see cref="T:System.Security.AccessControl.ControlFlags" />, объединенных с помощью логической операции ИЛИ.
    /// </returns>
    public override ControlFlags ControlFlags
    {
      get
      {
        return this._rawSd.ControlFlags;
      }
    }

    /// <summary>
    ///   Возвращает или задает владельца объекта, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Владелец объекта, связанный с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </returns>
    public override SecurityIdentifier Owner
    {
      get
      {
        return this._rawSd.Owner;
      }
      set
      {
        this._rawSd.Owner = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает основной группы для этой <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Основной группы <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </returns>
    public override SecurityIdentifier Group
    {
      get
      {
        return this._rawSd.Group;
      }
      set
      {
        this._rawSd.Group = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает список управления доступом (SACL) системы для этого <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    ///    SACL содержит правила аудита.
    /// </summary>
    /// <returns>
    ///   Системный список управления ДОСТУПОМ для данного <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </returns>
    public SystemAcl SystemAcl
    {
      get
      {
        return this._sacl;
      }
      set
      {
        if (value != null)
        {
          if (value.IsContainer != this.IsContainer)
            throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), nameof (value));
          if (value.IsDS != this.IsDS)
            throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), nameof (value));
        }
        this._sacl = value;
        if (this._sacl != null)
        {
          this._rawSd.SystemAcl = this._sacl.RawAcl;
          this.AddControlFlags(ControlFlags.SystemAclPresent);
        }
        else
        {
          this._rawSd.SystemAcl = (RawAcl) null;
          this.RemoveControlFlags(ControlFlags.SystemAclPresent);
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает список управления доступом на уровне пользователей (DACL) для этого <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    ///    Список DACL содержит правила доступа.
    /// </summary>
    /// <returns>
    ///   Список DACL для этого <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </returns>
    public DiscretionaryAcl DiscretionaryAcl
    {
      get
      {
        return this._dacl;
      }
      set
      {
        if (value != null)
        {
          if (value.IsContainer != this.IsContainer)
            throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), nameof (value));
          if (value.IsDS != this.IsDS)
            throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), nameof (value));
        }
        this._dacl = value != null ? value : DiscretionaryAcl.CreateAllowEveryoneFullAccess(this.IsDS, this.IsContainer);
        this._rawSd.DiscretionaryAcl = this._dacl.RawAcl;
        this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, которое указывает ли список управления доступом (SACL) системы, связанное с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объект находится в каноническом порядке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если список SACL, связанные с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объект находится в каноническом порядке; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsSystemAclCanonical
    {
      get
      {
        if (this.SystemAcl != null)
          return this.SystemAcl.IsCanonical;
        return true;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, которое указывает ли управления список доступа (DACL), связанное с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объект находится в каноническом порядке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если список DACL, связанные с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объект находится в каноническом порядке; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsDiscretionaryAclCanonical
    {
      get
      {
        if (this.DiscretionaryAcl != null)
          return this.DiscretionaryAcl.IsCanonical;
        return true;
      }
    }

    /// <summary>
    ///   Задает защиту наследования для списка управления доступом (SACL) системы, связанные с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    ///    Защищенные правила аудита не наследуют от родительских контейнеров.
    /// </summary>
    /// <param name="isProtected">
    ///   <see langword="true" /> защищается от наследования.
    /// </param>
    /// <param name="preserveInheritance">
    ///   <see langword="true" /> для сохранения наследуемых правил аудита из списка SACL; <see langword="false" /> для удаления наследуемых правил аудита из списка SACL.
    /// </param>
    public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
    {
      if (!isProtected)
      {
        this.RemoveControlFlags(ControlFlags.SystemAclProtected);
      }
      else
      {
        if (!preserveInheritance && this.SystemAcl != null)
          this.SystemAcl.RemoveInheritedAces();
        this.AddControlFlags(ControlFlags.SystemAclProtected);
      }
    }

    /// <summary>
    ///   Задает наследование защиты, для управления доступом список (DACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    ///    Защищенные правила доступа не наследуют от родительских контейнеров.
    /// </summary>
    /// <param name="isProtected">
    ///   <see langword="true" /> защищается от наследования.
    /// </param>
    /// <param name="preserveInheritance">
    ///   <see langword="true" /> Чтобы сохранить унаследованные правила доступа в списке DACL; <see langword="false" /> для удаления наследуемых правил доступа из списка DACL.
    /// </param>
    public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
    {
      if (!isProtected)
      {
        this.RemoveControlFlags(ControlFlags.DiscretionaryAclProtected);
      }
      else
      {
        if (!preserveInheritance && this.DiscretionaryAcl != null)
          this.DiscretionaryAcl.RemoveInheritedAces();
        this.AddControlFlags(ControlFlags.DiscretionaryAclProtected);
      }
      if (this.DiscretionaryAcl == null || !this.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
        return;
      this.DiscretionaryAcl.EveryOneFullAccessForNullDacl = false;
    }

    /// <summary>
    ///   Удаляет все правила доступа для указанный идентификатор безопасности от управления доступом список (DACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   Идентификатор безопасности, для которого удаляются правила доступа.
    /// </param>
    public void PurgeAccessControl(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException(nameof (sid));
      if (this.DiscretionaryAcl == null)
        return;
      this.DiscretionaryAcl.Purge(sid);
    }

    /// <summary>
    ///   Удаляет все правила аудита для указанный идентификатор безопасности из списка управления доступом системы (SACL), связанного с этим <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   Идентификатор безопасности, для которого удаляются правила аудита.
    /// </param>
    public void PurgeAudit(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException(nameof (sid));
      if (this.SystemAcl == null)
        return;
      this.SystemAcl.Purge(sid);
    }

    /// <summary>
    ///   Задает <see cref="P:System.Security.AccessControl.CommonSecurityDescriptor.DiscretionaryAcl" /> Свойства <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> экземпляра и задает <see cref="F:System.Security.AccessControl.ControlFlags.DiscretionaryAclPresent" /> флаг.
    /// </summary>
    /// <param name="revision">
    ///   Номер редакции нового <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </param>
    /// <param name="trusted">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public void AddDiscretionaryAcl(byte revision, int trusted)
    {
      this.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, revision, trusted);
      this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
    }

    /// <summary>
    ///   Задает <see cref="P:System.Security.AccessControl.CommonSecurityDescriptor.SystemAcl" /> Свойства <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> экземпляра и задает <see cref="F:System.Security.AccessControl.ControlFlags.SystemAclPresent" /> флаг.
    /// </summary>
    /// <param name="revision">
    ///   Номер редакции нового <see cref="T:System.Security.AccessControl.SystemAcl" /> объекта.
    /// </param>
    /// <param name="trusted">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.SystemAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public void AddSystemAcl(byte revision, int trusted)
    {
      this.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, revision, trusted);
      this.AddControlFlags(ControlFlags.SystemAclPresent);
    }

    internal void UpdateControlFlags(ControlFlags flagsToUpdate, ControlFlags newFlags)
    {
      this._rawSd.SetFlags(newFlags | this._rawSd.ControlFlags & ~flagsToUpdate);
    }

    internal void AddControlFlags(ControlFlags flags)
    {
      this._rawSd.SetFlags(this._rawSd.ControlFlags | flags);
    }

    internal void RemoveControlFlags(ControlFlags flags)
    {
      this._rawSd.SetFlags(this._rawSd.ControlFlags & ~flags);
    }

    internal bool IsSystemAclPresent
    {
      get
      {
        return (uint) (this._rawSd.ControlFlags & ControlFlags.SystemAclPresent) > 0U;
      }
    }

    internal bool IsDiscretionaryAclPresent
    {
      get
      {
        return (uint) (this._rawSd.ControlFlags & ControlFlags.DiscretionaryAclPresent) > 0U;
      }
    }
  }
}
