// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет абстракцию записи управления доступом (ACE), определяющей правило доступа для файла или каталога.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class FileSystemAccessRule : AccessRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класса, используя ссылку на учетную запись пользователя, значение, определяющее тип операции, связанной с правилом доступа и значение, указывающее, следует ли разрешить или запретить операцию.
    /// </summary>
    /// <param name="identity">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объекта, инкапсулирующего ссылку на учетную запись пользователя.
    /// </param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом доступа.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значений, указывающее, следует ли разрешить или запретить операцию.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> Параметр не <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="type " />параметр.
    /// </exception>
    public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, AccessControlType type)
      : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класса, используя имя учетной записи пользователя, значение, определяющее тип операции, связанной с правилом доступа и значение, которое описывает, следует ли разрешить или запретить операцию.
    /// </summary>
    /// <param name="identity">Имя учетной записи пользователя.</param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом доступа.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значений, указывающее, следует ли разрешить или запретить операцию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="type " />параметр.
    /// </exception>
    public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класса, используя ссылку на учетной записи пользователя, значение, определяющее тип операции, связанной с правилом доступа, значение, определяющее порядок наследования прав, значение, определяющее порядок распространения прав и значение, указывающее, следует ли разрешить или запретить операцию.
    /// </summary>
    /// <param name="identity">
    ///   <see cref="T:System.Security.Principal.IdentityReference" /> Объекта, инкапсулирующего ссылку на учетную запись пользователя.
    /// </param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом доступа.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, указывающих, как маски доступа распространяются на дочерние объекты.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих, как записи управления доступом (ACE) распространяются на дочерние объекты.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значений, указывающее, следует ли разрешить или запретить операцию.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> Параметр не <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="type " />параметр.
    /// 
    ///   -или-
    /// 
    ///   Было передано неверное перечисление <paramref name="inheritanceFlags " />параметр.
    /// 
    ///   -или-
    /// 
    ///   Было передано неверное перечисление <paramref name="propagationFlags " />параметр.
    /// </exception>
    public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> класса, используя имя учетной записи пользователя, значение, определяющее тип операции, связанной с правилом доступа, значение, определяющее порядок наследования прав, значение, определяющее порядок распространения прав и значение, указывающее, следует ли разрешить или запретить операцию.
    /// </summary>
    /// <param name="identity">Имя учетной записи пользователя.</param>
    /// <param name="fileSystemRights">
    ///   Один из <see cref="T:System.Security.AccessControl.FileSystemRights" /> значения, которое указывает тип операции, связанной с правилом доступа.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значений, указывающих, как маски доступа распространяются на дочерние объекты.
    /// </param>
    /// <param name="propagationFlags">
    ///   Один из <see cref="T:System.Security.AccessControl.PropagationFlags" /> значений, определяющих, как записи управления доступом (ACE) распространяются на дочерние объекты.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значений, указывающее, следует ли разрешить или запретить операцию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Было передано неверное перечисление <paramref name="type " />параметр.
    /// 
    ///   -или-
    /// 
    ///   Было передано неверное перечисление <paramref name="inheritanceFlags " />параметр.
    /// 
    ///   -или-
    /// 
    ///   Было передано неверное перечисление <paramref name="propagationFlags " />параметр.
    /// </exception>
    public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
    {
    }

    internal FileSystemAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.AccessControl.FileSystemRights" /> флаги, связанные с текущим <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.FileSystemRights" /> Флаги, связанные с текущим <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> объекта.
    /// </returns>
    public FileSystemRights FileSystemRights
    {
      get
      {
        return FileSystemAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    internal static int AccessMaskFromRights(FileSystemRights fileSystemRights, AccessControlType controlType)
    {
      if (fileSystemRights < (FileSystemRights) 0 || fileSystemRights > FileSystemRights.FullControl)
        throw new ArgumentOutOfRangeException(nameof (fileSystemRights), Environment.GetResourceString("Argument_InvalidEnumValue", (object) fileSystemRights, (object) "FileSystemRights"));
      switch (controlType)
      {
        case AccessControlType.Allow:
          fileSystemRights |= FileSystemRights.Synchronize;
          break;
        case AccessControlType.Deny:
          if (fileSystemRights != FileSystemRights.FullControl && fileSystemRights != (FileSystemRights.Modify | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership | FileSystemRights.Synchronize))
          {
            fileSystemRights &= ~FileSystemRights.Synchronize;
            break;
          }
          break;
      }
      return (int) fileSystemRights;
    }

    internal static FileSystemRights RightsFromAccessMask(int accessMask)
    {
      return (FileSystemRights) accessMask;
    }
  }
}
