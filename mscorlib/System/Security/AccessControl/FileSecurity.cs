// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет элемент управления доступом и аудита безопасности для файла.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class FileSecurity : FileSystemSecurity
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.FileSecurity" />.
    /// </summary>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    [SecuritySafeCritical]
    public FileSecurity()
      : base(false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.FileSecurity" /> класс из указанного файла, используя указанные значения <see cref="T:System.Security.AccessControl.AccessControlSections" /> перечисления.
    /// </summary>
    /// <param name="fileName">
    ///   Расположение файла для создания <see cref="T:System.Security.AccessControl.FileSecurity" /> объекта из.
    /// </param>
    /// <param name="includeSections">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlSections" /> значений, задающее тип доступа управляют извлекаемых данных списка управления Доступом.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, указанный в <paramref name="fileName" /> параметр не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">
    ///   Текущая учетная запись системы не имеет прав администратора.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="fileName" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="fileName" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    public FileSecurity(string fileName, AccessControlSections includeSections)
      : base(false, fileName, includeSections, false)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, Path.GetFullPathInternal(fileName), false, false);
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal FileSecurity(SafeFileHandle handle, string fullPath, AccessControlSections includeSections)
      : base(false, handle, includeSections, false)
    {
      if (fullPath != null)
        FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPath, false, true);
      else
        FileIOPermission.QuickDemand(PermissionState.Unrestricted);
    }
  }
}
