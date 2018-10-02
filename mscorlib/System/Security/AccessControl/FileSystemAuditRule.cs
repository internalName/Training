// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет абстракцию записи управления доступом (ACE), определяющей правило аудита для файла или каталога.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class FileSystemAuditRule : AuditRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> класса, используя ссылку на учетную запись пользователя, значение, определяющее тип операции, связанной с правилом аудита и значение, указывающее условия проведения аудита.
    /// </summary>
    /// <param name="identity">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объекта, инкапсулирующего ссылку на учетную запись пользователя.
    /// </param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом аудита.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Security.AccessControl.AuditFlags" /> значений, определяющее, когда проведения аудита.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> Параметр не <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="flags" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.Security.AccessControl.AuditFlags.None" /> Было передано значение <paramref name="flags" /> параметр.
    /// </exception>
    public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags)
      : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> класса, используя имя ссылки для учетной записи пользователя, значение, определяющее тип операции, связанной с правилом аудита, значение, определяющее порядок наследования прав, значение, определяющее порядок распространения прав и значение, указывающее условия проведения аудита.
    /// </summary>
    /// <param name="identity">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объекта, инкапсулирующего ссылку на учетную запись пользователя.
    /// </param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, указывающих, как маски доступа распространяются на дочерние объекты.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих, как записи управления доступом (ACE) распространяются на дочерние объекты.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Security.AccessControl.AuditFlags" /> значений, определяющее, когда проведения аудита.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> Параметр не <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="flags" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.Security.AccessControl.AuditFlags.None" /> Было передано значение <paramref name="flags" /> параметр.
    /// </exception>
    public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> с использованием имени учетной записи пользователя, значение, определяющее тип операции, связанной с правилом аудита и значение, указывающее условия проведения аудита.
    /// </summary>
    /// <param name="identity">Имя учетной записи пользователя.</param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом аудита.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Security.AccessControl.AuditFlags" /> значений, определяющее, когда проведения аудита.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="flags" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.Security.AccessControl.AuditFlags.None" /> Было передано значение <paramref name="flags" /> параметр.
    /// </exception>
    public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> класса, используя имя учетной записи пользователя, значение, определяющее тип операции, связанной с правилом аудита, значение, определяющее порядок наследования прав, значение, определяющее порядок распространения прав и значение, указывающее условия проведения аудита.
    /// </summary>
    /// <param name="identity">Имя учетной записи пользователя.</param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, указывающих, как маски доступа распространяются на дочерние объекты.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих, как записи управления доступом (ACE) распространяются на дочерние объекты.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Security.AccessControl.AuditFlags" /> значений, определяющее, когда проведения аудита.
    /// </param>
    public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal FileSystemAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    private static int AccessMaskFromRights(FileSystemRights fileSystemRights)
    {
      if (fileSystemRights < (FileSystemRights) 0 || fileSystemRights > FileSystemRights.FullControl)
        throw new ArgumentOutOfRangeException(nameof (fileSystemRights), Environment.GetResourceString("Argument_InvalidEnumValue", (object) fileSystemRights, (object) "FileSystemRights"));
      return (int) fileSystemRights;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.AccessControl.FileSystemRights" /> флаги, связанные с текущим <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.FileSystemRights" /> Флаги, связанные с текущим <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> объекта.
    /// </returns>
    public FileSystemRights FileSystemRights
    {
      get
      {
        return FileSystemAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }
  }
}
