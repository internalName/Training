// Decompiled with JetBrains decompiler
// Type: System.IO.Directory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет статические методы для создания, перемещения и перечисления в каталогах и вложенных каталогах.
  ///    Этот класс не наследуется.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ComVisible(true)]
  public static class Directory
  {
    private const int FILE_ATTRIBUTE_DIRECTORY = 16;
    private const int GENERIC_WRITE = 1073741824;
    private const int FILE_SHARE_WRITE = 2;
    private const int FILE_SHARE_DELETE = 4;
    private const int OPEN_EXISTING = 3;
    private const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

    /// <summary>
    ///   Извлекает родительский каталог, на который указывает абсолютный или относительный путь.
    /// </summary>
    /// <param name="path">
    ///   Путь, для которого необходимо извлечь родительский каталог.
    /// </param>
    /// <returns>
    ///   Родительский каталог или значение <see langword="null" />, если <paramref name="path" /> является корневым каталогом, в том числе корнем сервера UNC или именем общего ресурса.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог, заданный параметром <paramref name="path" />, доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    public static DirectoryInfo GetParent(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), nameof (path));
      string directoryName = Path.GetDirectoryName(Path.GetFullPathInternal(path));
      if (directoryName == null)
        return (DirectoryInfo) null;
      return new DirectoryInfo(directoryName);
    }

    /// <summary>
    ///   Создает все каталоги и подкаталоги по указанному пути, если они еще не существуют.
    /// </summary>
    /// <param name="path">Каталог, который необходимо создать.</param>
    /// <returns>
    ///   Объект, представляющий каталог по указанному пути.
    ///    Этот объект возвращается вне зависимости от того, существует ли уже каталог по указанному пути.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог, заданный параметром <paramref name="path" />, является файлом.
    /// 
    ///   -или-
    /// 
    ///   Имя сети неизвестно.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> начинается с символа двоеточия (:) или содержит только двоеточие.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит двоеточие (:), которое не является частью буквы диска ("C:\").
    /// </exception>
    [SecuritySafeCritical]
    public static DirectoryInfo CreateDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      return Directory.InternalCreateDirectoryHelper(path, true);
    }

    [SecurityCritical]
    internal static DirectoryInfo UnsafeCreateDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      return Directory.InternalCreateDirectoryHelper(path, false);
    }

    [SecurityCritical]
    internal static DirectoryInfo InternalCreateDirectoryHelper(string path, bool checkHost)
    {
      string checkPermissions = Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read);
      Directory.InternalCreateDirectory(checkPermissions, path, (object) null, checkHost);
      return new DirectoryInfo(checkPermissions, false);
    }

    internal static string GetFullPathAndCheckPermissions(string path, bool checkHost, FileSecurityStateAccess access = FileSecurityStateAccess.Read)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      Directory.CheckPermissions(path, fullPathInternal, checkHost, access);
      return fullPathInternal;
    }

    [SecuritySafeCritical]
    internal static void CheckPermissions(string displayPath, string fullPath, bool checkHost, FileSecurityStateAccess access = FileSecurityStateAccess.Read)
    {
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
        FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
      else
        FileIOPermission.QuickDemand((FileIOPermissionAccess) access, Directory.GetDemandDir(fullPath, true), false, false);
    }

    /// <summary>
    ///   Создает все каталоги по указанному пути, если они еще не существуют, с применением заданных параметров безопасности Windows.
    /// </summary>
    /// <param name="path">Каталог, который необходимо создать.</param>
    /// <param name="directorySecurity">
    ///   Элемент управления доступом, который необходимо применить к каталогу.
    /// </param>
    /// <returns>
    ///   Объект, представляющий каталог по указанному пути.
    ///    Этот объект возвращается вне зависимости от того, существует ли уже каталог по указанному пути.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог, заданный параметром <paramref name="path" />, является файлом.
    /// 
    ///   -или-
    /// 
    ///   Имя сети неизвестно.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> начинается с символа двоеточия (:) или содержит только двоеточие.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит двоеточие (:), которое не является частью буквы диска (C:\).
    /// </exception>
    [SecuritySafeCritical]
    public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      string checkPermissions = Directory.GetFullPathAndCheckPermissions(path, true, FileSecurityStateAccess.Read);
      Directory.InternalCreateDirectory(checkPermissions, path, (object) directorySecurity);
      return new DirectoryInfo(checkPermissions, false);
    }

    internal static string GetDemandDir(string fullPath, bool thisDirOnly)
    {
      return !thisDirOnly ? (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar) ? fullPath : fullPath + "\\") : (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar) ? fullPath + "." : fullPath + "\\.");
    }

    internal static void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj)
    {
      Directory.InternalCreateDirectory(fullPath, path, dirSecurityObj, false);
    }

    [SecuritySafeCritical]
    internal static unsafe void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj, bool checkHost)
    {
      DirectorySecurity directorySecurity = (DirectorySecurity) dirSecurityObj;
      int length = fullPath.Length;
      if (length >= 2 && Path.IsDirectorySeparator(fullPath[length - 1]))
        --length;
      int rootLength = Path.GetRootLength(fullPath);
      if (length == 2 && Path.IsDirectorySeparator(fullPath[1]))
        throw new IOException(Environment.GetResourceString("IO.IO_CannotCreateDirectory", (object) path));
      if (Directory.InternalExists(fullPath))
        return;
      List<string> stringList = new List<string>();
      bool flag1 = false;
      if (length > rootLength)
      {
        for (int index = length - 1; index >= rootLength && !flag1; --index)
        {
          string path1 = fullPath.Substring(0, index + 1);
          if (!Directory.InternalExists(path1))
            stringList.Add(path1);
          else
            flag1 = true;
          while (index > rootLength && (int) fullPath[index] != (int) Path.DirectorySeparatorChar && (int) fullPath[index] != (int) Path.AltDirectorySeparatorChar)
            --index;
        }
      }
      int count = stringList.Count;
      if (stringList.Count != 0 && !CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        string[] strArray = new string[stringList.Count];
        stringList.CopyTo(strArray, 0);
        for (int index = 0; index < strArray.Length; ++index)
        {
          // ISSUE: explicit reference operation
          ^ref strArray[index] += "\\.";
        }
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, directorySecurity == null ? AccessControlActions.None : AccessControlActions.Change, strArray, false, false);
      }
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (directorySecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = directorySecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      bool flag2 = true;
      int errorCode = 0;
      string maybeFullPath = path;
      while (stringList.Count > 0)
      {
        string str = stringList[stringList.Count - 1];
        stringList.RemoveAt(stringList.Count - 1);
        if (PathInternal.IsDirectoryTooLong(str))
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        flag2 = Win32Native.CreateDirectory(str, securityAttributes);
        if (!flag2 && errorCode == 0)
        {
          int lastError = Marshal.GetLastWin32Error();
          if (lastError != 183)
            errorCode = lastError;
          else if (File.InternalExists(str) || !Directory.InternalExists(str, out lastError) && lastError == 5)
          {
            errorCode = lastError;
            try
            {
              Directory.CheckPermissions(string.Empty, str, checkHost, FileSecurityStateAccess.PathDiscovery);
              maybeFullPath = str;
            }
            catch (SecurityException ex)
            {
            }
          }
        }
      }
      if (count == 0 && !flag1)
      {
        if (Directory.InternalExists(Directory.InternalGetDirectoryRoot(fullPath)))
          return;
        __Error.WinIOError(3, Directory.InternalGetDirectoryRoot(path));
      }
      else
      {
        if (flag2 || errorCode == 0)
          return;
        __Error.WinIOError(errorCode, maybeFullPath);
      }
    }

    /// <summary>
    ///   Определяет, указывает ли заданный путь на существующий каталог на диске.
    /// </summary>
    /// <param name="path">Проверяемый путь.</param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="path" /> ссылается на существующий каталог; значение <see langword="false" />, если каталог не существует или если при попытке определить, существует ли указанный каталог.
    /// </returns>
    [SecuritySafeCritical]
    public static bool Exists(string path)
    {
      return Directory.InternalExistsHelper(path, true);
    }

    [SecurityCritical]
    internal static bool UnsafeExists(string path)
    {
      return Directory.InternalExistsHelper(path, false);
    }

    [SecurityCritical]
    internal static bool InternalExistsHelper(string path, bool checkHost)
    {
      if (path != null)
      {
        if (path.Length != 0)
        {
          try
          {
            return Directory.InternalExists(Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read));
          }
          catch (ArgumentException ex)
          {
          }
          catch (NotSupportedException ex)
          {
          }
          catch (SecurityException ex)
          {
          }
          catch (IOException ex)
          {
          }
          catch (UnauthorizedAccessException ex)
          {
          }
          return false;
        }
      }
      return false;
    }

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      int lastError = 0;
      return Directory.InternalExists(path, out lastError);
    }

    [SecurityCritical]
    internal static bool InternalExists(string path, out int lastError)
    {
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      lastError = File.FillAttributeInfo(path, ref data, false, true);
      if (lastError == 0 && data.fileAttributes != -1)
        return (uint) (data.fileAttributes & 16) > 0U;
      return false;
    }

    /// <summary>
    ///   Устанавливает дату и время создания заданного файла или каталога.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого требуется установить дату и время создания.
    /// </param>
    /// <param name="creationTime">
    ///   Дата и время последней записи в файл или каталог.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationTime" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      Directory.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время создания указанного файла или папки в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого требуется установить дату и время создания.
    /// </param>
    /// <param name="creationTimeUtc">
    ///   Дата и время создания каталога или файла.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationTime" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, &fileTime, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>Получает дату и время создания каталога.</summary>
    /// <param name="path">Путь к каталогу.</param>
    /// <returns>
    ///   Структура, для которой заданы дата и время создания указанного каталога.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    public static DateTime GetCreationTime(string path)
    {
      return File.GetCreationTime(path);
    }

    /// <summary>
    ///   Получает время и дату создания каталога в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">Путь к каталогу.</param>
    /// <returns>
    ///   Структура, для которой заданы дата и время создания указанного каталога.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    public static DateTime GetCreationTimeUtc(string path)
    {
      return File.GetCreationTimeUtc(path);
    }

    /// <summary>
    ///   Устанавливает дату и время последней записи в файл или каталог.
    /// </summary>
    /// <param name="path">Путь к каталогу.</param>
    /// <param name="lastWriteTime">
    ///   Дата и время последней записи в файл или каталог.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastWriteTime" /> указывает значение вне диапазона дат или времени, разрешенного для операции.
    /// </exception>
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время последней записи в заданный каталог в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">Путь к каталогу.</param>
    /// <param name="lastWriteTimeUtc">
    ///   Дата и время последней записи в файл или каталог.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastWriteTimeUtc" /> указывает значение вне диапазона дат или времени, разрешенного для операции.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null, &fileTime))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    ///   Возвращает время и дату последней операции записи в указанный файл или каталог.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, дату и время изменения которого следует получить.
    /// </param>
    /// <returns>
    ///   Структура , для которой заданы дата и время последней операции записи в указанный файл или каталог.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    public static DateTime GetLastWriteTime(string path)
    {
      return File.GetLastWriteTime(path);
    }

    /// <summary>
    ///   Возвращает дату и время последней операции записи в заданный файл или каталог в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, дату и время изменения которого следует получить.
    /// </param>
    /// <returns>
    ///   Структура , для которой заданы дата и время последней операции записи в указанный файл или каталог.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return File.GetLastWriteTimeUtc(path);
    }

    /// <summary>
    ///   Устанавливает время и дату последнего обращения к заданному файлу или каталогу.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого требуется установить дату и время доступа.
    /// </param>
    /// <param name="lastAccessTime">
    ///   Объект, содержащий значение, которое необходимо присвоить дате и времени доступа к <paramref name="path" />.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastAccessTime" /> указывает значение вне диапазона дат или времени, разрешенного для операции.
    /// </exception>
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время последнего доступа к заданному файлу или каталогу в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого требуется установить дату и время доступа.
    /// </param>
    /// <param name="lastAccessTimeUtc">
    ///   Объект, содержащий значение, которое необходимо присвоить дате и времени доступа к <paramref name="path" />.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastAccessTimeUtc" /> указывает значение вне диапазона дат или времени, разрешенного для операции.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, (Win32Native.FILE_TIME*) null, &fileTime, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    ///   Возвращает время и дату последнего обращения к указанному файлу или каталогу.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, информацию о дате и времени обращения к которому следует получить.
    /// </param>
    /// <returns>
    ///   Структура , для которой заданы дата и время последнего доступа к указанному файлу или каталогу.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> Параметра имеет недопустимый формат.
    /// </exception>
    public static DateTime GetLastAccessTime(string path)
    {
      return File.GetLastAccessTime(path);
    }

    /// <summary>
    ///   Возвращает дату и время последнего доступа к заданному файлу или каталогу в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, информацию о дате и времени обращения к которому следует получить.
    /// </param>
    /// <returns>
    ///   Структура , для которой заданы дата и время последнего доступа к указанному файлу или каталогу.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> Параметра имеет недопустимый формат.
    /// </exception>
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return File.GetLastAccessTimeUtc(path);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует записи списка управления доступом (ACL) для заданного каталога.
    /// </summary>
    /// <param name="path">
    ///   Путь к каталогу, в котором содержится объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, описывающий сведения о списке управления доступом (ACL) для конкретного файла.
    /// </param>
    /// <returns>
    ///   Объект, который инкапсулирует правила управления доступом для файла, описанные параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии каталога возникла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows 2000 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Произошла ошибка системного уровня, например невозможно найти каталог.
    ///    Конкретное исключение может быть подклассом <see cref="T:System.SystemException" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает каталог, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static DirectorySecurity GetAccessControl(string path)
    {
      return new DirectorySecurity(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует записи списка управления доступом (ACL) указанного типа для заданного каталога.
    /// </summary>
    /// <param name="path">
    ///   Путь к каталогу, в котором содержится объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, описывающий сведения о списке управления доступом (ACL) для конкретного файла.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее тип сведений о списке ACL, которые необходимо получить.
    /// </param>
    /// <returns>
    ///   Объект, который инкапсулирует правила управления доступом для файла, описанные параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии каталога возникла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows 2000 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Произошла ошибка системного уровня, например невозможно найти каталог.
    ///    Конкретное исключение может быть подклассом <see cref="T:System.SystemException" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает каталог, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
    {
      return new DirectorySecurity(path, includeSections);
    }

    /// <summary>
    ///   Применяет к заданному каталогу записи списка управления доступом (ACL), описанные объектом <see cref="T:System.Security.AccessControl.DirectorySecurity" />.
    /// </summary>
    /// <param name="path">
    ///   Каталог, в который необходимо добавить или из которого нужно удалить записи списка управления доступом.
    /// </param>
    /// <param name="directorySecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, описывающий запись ACL, которую требуется применить к каталогу, описанному параметром <paramref name="path" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="directorySecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Не удается найти каталог.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Задан недопустимый <paramref name="path" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий процесс не может получить доступ к каталогу, заданному <paramref name="path" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий процесс не имеет необходимых прав для задания записи ACL.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows 2000 или более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
    {
      if (directorySecurity == null)
        throw new ArgumentNullException(nameof (directorySecurity));
      string fullPathInternal = Path.GetFullPathInternal(path);
      directorySecurity.Persist(fullPathInternal);
    }

    /// <summary>
    ///   Возвращает имена файлов (с указанием пути к ним) в указанном каталоге.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) файлов в указанном каталоге или пустой массив, если файлы не найдены.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// 
    ///   -или-
    /// 
    ///   Произошла сетевая ошибка.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь не найден или является недопустимым (например, ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetFiles(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает имена файлов (включая пути) из указанного каталога, отвечающие условиям заданного шаблона поиска.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с именами файлов в <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) файлов в указанном каталоге, которые соответствуют указанному шаблону поиска, или пустой массив, если файлы не найдены.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// 
    ///   -или-
    /// 
    ///   Произошла сетевая ошибка.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь не найден или является недопустимым (например, ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetFiles(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает имена файлов (включая пути) в заданном каталоге, отвечающие условиям шаблона поиска, используя значение, которое определяет, выполнять ли поиск в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с именами файлов в <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) файлов в указанном каталоге, которые соответствуют указанному шаблону и параметру поиска, или пустой массив, если файлы не найдены.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="searchpattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь не найден или является недопустимым (например, ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// 
    ///   -или-
    /// 
    ///   Произошла сетевая ошибка.
    /// </exception>
    public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetFiles(path, searchPattern, searchOption);
    }

    private static string[] InternalGetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, true);
    }

    [SecurityCritical]
    internal static string[] UnsafeGetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, false);
    }

    /// <summary>
    ///   Возвращает имена подкаталогов (включая пути) в указанном каталоге.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) подкаталогов по указанному пути или пустой массив, если каталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetDirectories(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает имена подкаталогов (включая пути) в указанном каталоге, соответствующих указанному шаблону поиска.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами подкаталогов в <paramref name="path" />.
    ///    Этот параметр может содержать сочетание допустимых литеральных и подстановочных символов (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) подкаталогов в указанном каталоге, которые соответствуют указанному шаблону поиска, или пустой массив, если каталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetDirectories(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает имена подкаталогов (включая пути) в указанном каталоге, соответствующих указанному шаблону поиска, и при необходимости ведет поиск в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами подкаталогов в <paramref name="path" />.
    ///    Этот параметр может содержать сочетание допустимых литеральных и подстановочных символов (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// </param>
    /// <returns>
    ///   Массив полных имен (включая пути) подкаталогов, соответствующих указанным критериям, или пустой массив, если каталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetDirectories(path, searchPattern, searchOption);
    }

    private static string[] InternalGetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, true);
    }

    [SecurityCritical]
    internal static string[] UnsafeGetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, false);
    }

    /// <summary>
    ///   Возвращает имена всех файлов и подкаталогов по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Массив имен файлов и подкаталогов в указанном каталоге или пустой массив, если файлы или подкаталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetFileSystemEntries(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает массив имен файлов и имен каталогов по указанному пути, соответствующих шаблону поиска.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами файла и каталогов в <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Массив имен файлов и имен каталогов, соответствующих указанным критериям поиска, или пустой массив, если файлы или каталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public static string[] GetFileSystemEntries(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает массив всех имен файлов и каталогов по указанному пути, соответствующих шаблону поиска, и при необходимости ведет поиск в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с именами файлов и каталогов в параметре <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// 
    ///   Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Массив имен файлов и имен каталогов, соответствующих указанным критериям поиска, или пустой массив, если файлы или каталоги не найдены.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetFileSystemEntries(path, searchPattern, searchOption);
    }

    private static string[] InternalGetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, true, searchOption, true);
    }

    internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
    {
      return new List<string>(FileSystemEnumerableFactory.CreateFileNameIterator(path, userPathOriginal, searchPattern, includeFiles, includeDirs, searchOption, checkHost)).ToArray();
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен каталогов, расположенных по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для каталогов в каталоге, заданном параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateDirectories(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен каталогов, соответствующих шаблону поиска по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которую необходимо сравнивать с именами каталогов, расположенных по пути <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для каталогов в каталоге, указанном в <paramref name="path" />, которые соответствуют указанному шаблону поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен каталогов, соответствующих шаблону поиска по указанному пути. Возможно, поиск ведется также и в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которую необходимо сравнивать с именами каталогов, расположенных по пути <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// 
    ///   Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для каталогов в каталоге, указанном в <paramref name="path" />, которые соответствуют заданному шаблону и параметрам поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateDirectories(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, false, true);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен файлов, расположенных по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для файлов в каталоге, заданном параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFiles(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен файлов, соответствующих шаблону поиска по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с именами файлов в <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для файлов в каталоге, указанном в параметре <paramref name="path" />, которые соответствуют заданному шаблону поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен файлов, соответствующих шаблону поиска по указанному пути. Возможно, поиск ведется также и в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с именами файлов в <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// 
    ///   Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция полных имен (включая пути) для файлов в каталоге, указанном в параметре <paramref name="path" />, которые соответствуют указанному шаблону и параметрам поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateFiles(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, false);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен файлов и имен каталогов по указанному пути.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция записей файловой системы в каталоге, заданном параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Directory.InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию имен файлов и имен каталогов по указанному пути, соответствующих шаблону поиска.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами в записях файловой системы, расположенными по пути <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция записей файловой системы в каталоге, заданном параметром <paramref name="path" />, которые соответствуют указанному шаблону поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию записей файловой системы, соответствующих шаблону поиска по указанному пути. Возможно, поиск ведется также и в подкаталогах.
    /// </summary>
    /// <param name="path">
    ///   Относительный или абсолютный путь к каталогу для поиска.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="searchPattern">
    ///   Строка поиска, которая должна сравниваться с записями файловой системы, расположенными по пути <paramref name="path" />.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// 
    ///   Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция записей файловой системы в каталоге, указанном параметром <paramref name="path" />, который соответствует шаблону и параметру поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " /> представляет собой строку нулевой длины, содержащую только пробел, или строку, содержащую недопустимые символы.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="searchPattern" /> не содержит допустимый шаблон.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" />: например, он ссылается на несопоставленный диск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> — это имя файла.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или их комбинация превышает максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, true);
    }

    private static IEnumerable<string> EnumerateFileSystemNames(string path, string searchPattern, SearchOption searchOption, bool includeFiles, bool includeDirs)
    {
      return FileSystemEnumerableFactory.CreateFileNameIterator(path, path, searchPattern, includeFiles, includeDirs, searchOption, true);
    }

    /// <summary>
    ///   Извлекает имена логических устройств данного компьютера в формате "&lt;имя устройства&gt;:\".
    /// </summary>
    /// <returns>Логические устройства данного компьютера.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static string[] GetLogicalDrives()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      int logicalDrives = Win32Native.GetLogicalDrives();
      if (logicalDrives == 0)
        __Error.WinIOError();
      uint num1 = (uint) logicalDrives;
      int length = 0;
      while (num1 != 0U)
      {
        if (((int) num1 & 1) != 0)
          ++length;
        num1 >>= 1;
      }
      string[] strArray = new string[length];
      char[] chArray = new char[3]{ 'A', ':', '\\' };
      uint num2 = (uint) logicalDrives;
      int num3 = 0;
      while (num2 != 0U)
      {
        if (((int) num2 & 1) != 0)
          strArray[num3++] = new string(chArray);
        num2 >>= 1;
        ++chArray[0];
      }
      return strArray;
    }

    /// <summary>
    ///   Возвращает для заданного пути сведения о томе и корневом каталоге по отдельности или сразу.
    /// </summary>
    /// <param name="path">Путь к файлу или каталогу.</param>
    /// <returns>
    ///   Строка, в которой содержатся сведения о томе, корневом каталоге или одновременно сведения и о томе, и о корневом каталоге для заданного пути.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    public static string GetDirectoryRoot(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      string fullPathInternal = Path.GetFullPathInternal(path);
      string fullPath = fullPathInternal.Substring(0, Path.GetRootLength(fullPathInternal));
      Directory.CheckPermissions(path, fullPath, true, FileSecurityStateAccess.PathDiscovery);
      return fullPath;
    }

    internal static string InternalGetDirectoryRoot(string path)
    {
      return path?.Substring(0, Path.GetRootLength(path));
    }

    /// <summary>Получает текущий рабочий каталог приложения.</summary>
    /// <returns>
    ///   Строка, содержащая путь к текущему рабочему каталогу, не оканчивающаяся обратной косой чертой (\).
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Операционной системой является Windows CE, которая не поддерживает функциональность текущего каталога.
    /// 
    ///   Этот метод доступен в .NET Compact Framework, но в настоящее время не поддерживается.
    /// </exception>
    [SecuritySafeCritical]
    public static string GetCurrentDirectory()
    {
      return Directory.InternalGetCurrentDirectory(true);
    }

    [SecurityCritical]
    internal static string UnsafeGetCurrentDirectory()
    {
      return Directory.InternalGetCurrentDirectory(false);
    }

    [SecuritySafeCritical]
    private static string InternalGetCurrentDirectory(bool checkHost)
    {
      string fullPath = AppContextSwitches.UseLegacyPathHandling ? Directory.LegacyGetCurrentDirectory() : Directory.NewGetCurrentDirectory();
      Directory.CheckPermissions(string.Empty, fullPath, true, FileSecurityStateAccess.PathDiscovery);
      return fullPath;
    }

    [SecurityCritical]
    private static string LegacyGetCurrentDirectory()
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(261);
      if (Win32Native.GetCurrentDirectory(stringBuilder.Capacity, stringBuilder) == 0)
        __Error.WinIOError();
      string path = stringBuilder.ToString();
      if (path.IndexOf('~') >= 0)
      {
        int longPathName = Win32Native.GetLongPathName(path, stringBuilder, stringBuilder.Capacity);
        if (longPathName == 0 || longPathName >= 260)
        {
          int errorCode = Marshal.GetLastWin32Error();
          if (longPathName >= 260)
            errorCode = 206;
          if (errorCode != 2 && errorCode != 3 && (errorCode != 1 && errorCode != 5))
            __Error.WinIOError(errorCode, string.Empty);
        }
        path = stringBuilder.ToString();
      }
      StringBuilderCache.Release(stringBuilder);
      return path;
    }

    [SecurityCritical]
    private static string NewGetCurrentDirectory()
    {
      using (StringBuffer path = new StringBuffer(260U))
      {
        uint currentDirectoryW;
        while ((currentDirectoryW = Win32Native.GetCurrentDirectoryW(path.CharCapacity, path.GetHandle())) > path.CharCapacity)
          path.EnsureCharCapacity(currentDirectoryW);
        if (currentDirectoryW == 0U)
          __Error.WinIOError();
        path.Length = currentDirectoryW;
        if (path.Contains('~'))
          return LongPathHelper.GetLongPathName(path);
        return path.ToString();
      }
    }

    /// <summary>
    ///   Устанавливает заданный каталог в качестве текущего рабочего каталога приложения.
    /// </summary>
    /// <param name="path">
    ///   Путь, который должен быть назначен рабочему каталогу.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для доступа к неуправляемому коду.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный каталог не найден.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetCurrentDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException("value");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      if (PathInternal.IsPathTooLong(path))
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      string fullPathInternal = Path.GetFullPathInternal(path);
      if (Win32Native.SetCurrentDirectory(fullPathInternal))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
        errorCode = 3;
      __Error.WinIOError(errorCode, fullPathInternal);
    }

    /// <summary>
    ///   Перемещает файл или каталог со всем его содержимым в новое местоположение.
    /// </summary>
    /// <param name="sourceDirName">
    ///   Путь к файлу или каталогу, который необходимо переместить.
    /// </param>
    /// <param name="destDirName">
    ///   Путь к новому местоположению <paramref name="sourceDirName" />.
    ///    Если <paramref name="sourceDirName" /> является файлом, то параметр <paramref name="destDirName" /> также должен быть именем файла.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Была предпринята попытка переместить каталог в другой том.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destDirName" /> уже существует.
    /// 
    ///   -или-
    /// 
    ///    Параметры <paramref name="sourceDirName" /> и <paramref name="destDirName" /> указывают на один и тот же файл или каталог.
    /// 
    ///   -или-
    /// 
    ///   Каталог или находящийся в нем файл используется другим процессом.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceDirName" /> или <paramref name="destDirName" /> представляет собой строку нулевой длины, строку, содержащую только пробелы, или строку, содержащую один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sourceDirName" /> или <paramref name="destDirName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, указанный <paramref name="sourceDirName" />, является недопустимым (например, он ведет на несопоставленный диск).
    /// </exception>
    [SecuritySafeCritical]
    public static void Move(string sourceDirName, string destDirName)
    {
      Directory.InternalMove(sourceDirName, destDirName, true);
    }

    [SecurityCritical]
    internal static void UnsafeMove(string sourceDirName, string destDirName)
    {
      Directory.InternalMove(sourceDirName, destDirName, false);
    }

    [SecurityCritical]
    private static void InternalMove(string sourceDirName, string destDirName, bool checkHost)
    {
      if (sourceDirName == null)
        throw new ArgumentNullException(nameof (sourceDirName));
      if (sourceDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (sourceDirName));
      if (destDirName == null)
        throw new ArgumentNullException(nameof (destDirName));
      if (destDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destDirName));
      string fullPathInternal = Path.GetFullPathInternal(sourceDirName);
      string demandDir1 = Directory.GetDemandDir(fullPathInternal, false);
      if (PathInternal.IsDirectoryTooLong(demandDir1))
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      string demandDir2 = Directory.GetDemandDir(Path.GetFullPathInternal(destDirName), false);
      if (PathInternal.IsDirectoryTooLong(demandDir1))
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, demandDir1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir2, false, false);
      if (string.Compare(demandDir1, demandDir2, StringComparison.OrdinalIgnoreCase) == 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
      if (string.Compare(Path.GetPathRoot(demandDir1), Path.GetPathRoot(demandDir2), StringComparison.OrdinalIgnoreCase) != 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
      if (Win32Native.MoveFile(sourceDirName, destDirName))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
      {
        errorCode = 3;
        __Error.WinIOError(errorCode, fullPathInternal);
      }
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) sourceDirName), Win32Native.MakeHRFromErrorCode(errorCode));
      __Error.WinIOError(errorCode, string.Empty);
    }

    /// <summary>Удаляет пустой каталог по заданному пути.</summary>
    /// <param name="path">
    ///   Имя пустого каталога, который необходимо удалить.
    ///    Этот каталог должен поддерживать запись и быть пустым.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл с именем и расположением, заданными параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Каталог является текущим рабочим каталогом приложения.
    /// 
    ///   -или-
    /// 
    ///   Каталог, заданный параметром <paramref name="path" />, не пустой.
    /// 
    ///   -или-
    /// 
    ///   Каталог доступен только для чтения или содержит файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Каталог используется другим процессом.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Параметр <paramref name="path" /> не существует или не найден.
    /// 
    ///   -или-
    /// 
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    [SecuritySafeCritical]
    public static void Delete(string path)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, false, true);
    }

    /// <summary>
    ///   Удаляет заданный каталог и, при наличии соответствующей инструкции, все подкаталоги и файлы в нем.
    /// </summary>
    /// <param name="path">
    ///   Имя каталога, который необходимо удалить.
    /// </param>
    /// <param name="recursive">
    ///   Значение <see langword="true" /> позволяет удалить каталоги, подкаталоги и файлы по заданному <paramref name="path" />, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл с тем же именем и расположении, заданном <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Каталог, заданный параметром <paramref name="path" />, доступен только для чтения, или <paramref name="recursive" /> имеет значение <see langword="false" /> и <paramref name="path" /> не является пустым каталогом.
    /// 
    ///   -или-
    /// 
    ///   Каталог является текущим рабочим каталогом приложения.
    /// 
    ///   -или-
    /// 
    ///   Каталог содержит файл только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Каталог используется другим процессом.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов.
    ///    Вы можете запросить недопустимые символы с помощью метода <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Параметр <paramref name="path" /> не существует или не найден.
    /// 
    ///   -или-
    /// 
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    [SecuritySafeCritical]
    public static void Delete(string path, bool recursive)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, recursive, true);
    }

    [SecurityCritical]
    internal static void UnsafeDelete(string path, bool recursive)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, recursive, false);
    }

    [SecurityCritical]
    internal static void Delete(string fullPath, string userPath, bool recursive, bool checkHost)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, Directory.GetDemandDir(fullPath, !recursive), false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data1 = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPath, ref data1, false, true);
      switch (errorCode)
      {
        case 0:
          if ((data1.fileAttributes & 1024) != 0)
            recursive = false;
          Win32Native.WIN32_FIND_DATA data2 = new Win32Native.WIN32_FIND_DATA();
          Directory.DeleteHelper(fullPath, userPath, recursive, true, ref data2);
          break;
        case 2:
          errorCode = 3;
          goto default;
        default:
          __Error.WinIOError(errorCode, fullPath);
          goto case 0;
      }
    }

    [SecurityCritical]
    private static void DeleteHelper(string fullPath, string userPath, bool recursive, bool throwOnTopLevelDirectoryNotFound, ref Win32Native.WIN32_FIND_DATA data)
    {
      Exception exception = (Exception) null;
      if (recursive)
      {
        int lastWin32Error;
        using (SafeFindHandle firstFile = Win32Native.FindFirstFile(fullPath + "\\*", ref data))
        {
          if (firstFile.IsInvalid)
          {
            lastWin32Error = Marshal.GetLastWin32Error();
            __Error.WinIOError(lastWin32Error, fullPath);
          }
          do
          {
            if ((uint) (data.dwFileAttributes & 16) > 0U)
            {
              if (!data.IsRelativeDirectory)
              {
                string cFileName = data.cFileName;
                if ((data.dwFileAttributes & 1024) == 0)
                {
                  string fullPath1 = Path.CombineNoChecks(fullPath, cFileName);
                  string userPath1 = Path.CombineNoChecks(userPath, cFileName);
                  try
                  {
                    Directory.DeleteHelper(fullPath1, userPath1, recursive, false, ref data);
                  }
                  catch (Exception ex)
                  {
                    if (exception == null)
                      exception = ex;
                  }
                }
                else
                {
                  if (data.dwReserved0 == -1610612733 && !Win32Native.DeleteVolumeMountPoint(Path.CombineNoChecks(fullPath, cFileName + Path.DirectorySeparatorChar.ToString())))
                  {
                    lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 3)
                    {
                      try
                      {
                        __Error.WinIOError(lastWin32Error, cFileName);
                      }
                      catch (Exception ex)
                      {
                        if (exception == null)
                          exception = ex;
                      }
                    }
                  }
                  if (!Win32Native.RemoveDirectory(Path.CombineNoChecks(fullPath, cFileName)))
                  {
                    lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 3)
                    {
                      try
                      {
                        __Error.WinIOError(lastWin32Error, cFileName);
                      }
                      catch (Exception ex)
                      {
                        if (exception == null)
                          exception = ex;
                      }
                    }
                  }
                }
              }
            }
            else
            {
              string cFileName = data.cFileName;
              if (!Win32Native.DeleteFile(Path.CombineNoChecks(fullPath, cFileName)))
              {
                lastWin32Error = Marshal.GetLastWin32Error();
                if (lastWin32Error != 2)
                {
                  try
                  {
                    __Error.WinIOError(lastWin32Error, cFileName);
                  }
                  catch (Exception ex)
                  {
                    if (exception == null)
                      exception = ex;
                  }
                }
              }
            }
          }
          while (Win32Native.FindNextFile(firstFile, ref data));
          lastWin32Error = Marshal.GetLastWin32Error();
        }
        if (exception != null)
          throw exception;
        if (lastWin32Error != 0 && lastWin32Error != 18)
          __Error.WinIOError(lastWin32Error, userPath);
      }
      if (Win32Native.RemoveDirectory(fullPath))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
        errorCode = 3;
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) userPath));
      if (errorCode == 3 && !throwOnTopLevelDirectoryNotFound)
        return;
      __Error.WinIOError(errorCode, fullPath);
    }

    [SecurityCritical]
    private static SafeFileHandle OpenHandle(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      string pathRoot = Path.GetPathRoot(fullPathInternal);
      if (pathRoot == fullPathInternal && (int) pathRoot[1] == (int) Path.VolumeSeparatorChar)
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIsVolume"));
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, Directory.GetDemandDir(fullPathInternal, true), false, false);
      SafeFileHandle file = Win32Native.SafeCreateFile(fullPathInternal, 1073741824, FileShare.Write | FileShare.Delete, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 33554432, IntPtr.Zero);
      if (file.IsInvalid)
        __Error.WinIOError(Marshal.GetLastWin32Error(), fullPathInternal);
      return file;
    }

    internal sealed class SearchData
    {
      public readonly string fullPath;
      public readonly string userPath;
      public readonly SearchOption searchOption;

      public SearchData(string fullPath, string userPath, SearchOption searchOption)
      {
        this.fullPath = fullPath;
        this.userPath = userPath;
        this.searchOption = searchOption;
      }
    }
  }
}
