// Decompiled with JetBrains decompiler
// Type: System.IO.DirectoryInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет методы экземпляра класса для создания, перемещения и перечисления в каталогах и подкаталогах.
  ///    Этот класс не наследуется.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class DirectoryInfo : FileSystemInfo
  {
    private string[] demandDir;

    /// <summary>
    ///   Выполняет инициализацию нового экземпляра класса <see cref="T:System.IO.DirectoryInfo" /> для заданного пути.
    /// </summary>
    /// <param name="path">
    ///   Строка, содержащая путь, для которого создается класс <see langword="DirectoryInfo" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит недопустимые символы, такие как ", &lt;, &gt; или |.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    ///    Указанный путь, имя файла или оба значения имеют слишком большую длину.
    /// </exception>
    [SecuritySafeCritical]
    public DirectoryInfo(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      this.Init(path, true);
    }

    [SecurityCritical]
    private void Init(string path, bool checkHost)
    {
      if (path.Length == 2 && path[1] == ':')
        this.OriginalPath = ".";
      else
        this.OriginalPath = path;
      this.FullPath = Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read);
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    internal DirectoryInfo(string fullPath, bool junk)
    {
      this.OriginalPath = Path.GetFileName(fullPath);
      this.FullPath = fullPath;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    internal DirectoryInfo(string fullPath, string fileName)
    {
      this.OriginalPath = fileName;
      this.FullPath = fullPath;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    [SecurityCritical]
    private DirectoryInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      Directory.CheckPermissions(string.Empty, this.FullPath, false, FileSecurityStateAccess.Read);
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    /// <summary>
    ///   Получает имя данного экземпляра <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <returns>Имя каталога.</returns>
    public override string Name
    {
      get
      {
        return DirectoryInfo.GetDirName(this.FullPath);
      }
    }

    /// <summary>Получает полный путь к каталогу.</summary>
    /// <returns>Строка, содержащая полный путь.</returns>
    public override string FullName
    {
      [SecuritySafeCritical] get
      {
        Directory.CheckPermissions(string.Empty, this.FullPath, true, FileSecurityStateAccess.PathDiscovery);
        return this.FullPath;
      }
    }

    internal override string UnsafeGetFullName
    {
      [SecurityCritical] get
      {
        Directory.CheckPermissions(string.Empty, this.FullPath, false, FileSecurityStateAccess.PathDiscovery);
        return this.FullPath;
      }
    }

    /// <summary>
    ///   Получает родительский каталог заданного подкаталога.
    /// </summary>
    /// <returns>
    ///   Родительский каталог или значение <see langword="null" />, если путь к файлу пуст или указывает на корневой каталог (например "\", "C:" или * "\\server\share").
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo Parent
    {
      [SecuritySafeCritical] get
      {
        string path = this.FullPath;
        if (path.Length > 3 && path.EndsWith(Path.DirectorySeparatorChar))
          path = this.FullPath.Substring(0, this.FullPath.Length - 1);
        string directoryName = Path.GetDirectoryName(path);
        if (directoryName == null)
          return (DirectoryInfo) null;
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryName, false);
        Directory.CheckPermissions(string.Empty, directoryInfo.FullPath, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.PathDiscovery);
        return directoryInfo;
      }
    }

    /// <summary>
    ///   Создает один или несколько подкаталогов по заданному пути.
    ///    Путь может быть задан относительно текущего экземпляра класса <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <param name="path">
    ///   Заданный путь.
    ///    Этот путь не может указывать на другой том диска или иметь формат UNC.
    /// </param>
    /// <returns>
    ///   Последний каталог, на который указывает <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> не указывает допустимый путь к файлу или содержит недопустимые символы <see langword="DirectoryInfo" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Невозможно создать подкаталог.
    /// 
    ///   -или-
    /// 
    ///   Файл или каталог уже имеет имя, указанное <paramref name="path" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    ///    Указанный путь, имя файла или оба значения имеют слишком большую длину.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешения доступа к коду для создания каталога.
    /// 
    ///   -или-
    /// 
    ///   Вызывающий объект не имеет разрешения доступа к коду для чтения каталога, описанного возвращаемым объектом <see cref="T:System.IO.DirectoryInfo" />.
    ///     Это может произойти, если параметр <paramref name="path" /> описывает существующий каталог.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит двоеточие (:), которое не является частью буквы диска ("C:\").
    /// </exception>
    public DirectoryInfo CreateSubdirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return this.CreateSubdirectory(path, (DirectorySecurity) null);
    }

    /// <summary>
    ///   Создает один или несколько подкаталогов по заданному пути с заданными параметрами безопасности.
    ///    Путь может быть задан относительно текущего экземпляра класса <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <param name="path">
    ///   Заданный путь.
    ///    Этот путь не может указывать на другой том диска или иметь формат UNC.
    /// </param>
    /// <param name="directorySecurity">
    ///   Параметры безопасности, которые необходимо применить.
    /// </param>
    /// <returns>
    ///   Последний каталог, на который указывает <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> не указывает допустимый путь к файлу или содержит недопустимые символы <see langword="DirectoryInfo" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Невозможно создать подкаталог.
    /// 
    ///   -или-
    /// 
    ///   Файл или каталог уже имеет имя, указанное <paramref name="path" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    ///    Указанный путь, имя файла или оба значения имеют слишком большую длину.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешения доступа к коду для создания каталога.
    /// 
    ///   -или-
    /// 
    ///   Вызывающий объект не имеет разрешения доступа к коду для чтения каталога, описанного возвращаемым объектом <see cref="T:System.IO.DirectoryInfo" />.
    ///     Это может произойти, если параметр <paramref name="path" /> описывает существующий каталог.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит двоеточие (:), которое не является частью буквы диска ("C:\").
    /// </exception>
    [SecuritySafeCritical]
    public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
    {
      return this.CreateSubdirectoryHelper(path, (object) directorySecurity);
    }

    [SecurityCritical]
    private DirectoryInfo CreateSubdirectoryHelper(string path, object directorySecurity)
    {
      string fullPathInternal = Path.GetFullPathInternal(Path.InternalCombine(this.FullPath, path));
      if (string.Compare(this.FullPath, 0, fullPathInternal, 0, this.FullPath.Length, StringComparison.OrdinalIgnoreCase) != 0)
      {
        string displayablePath = __Error.GetDisplayablePath(this.DisplayPath, false);
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSubPath", (object) path, (object) displayablePath));
      }
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, Directory.GetDemandDir(fullPathInternal, true), false, false);
      Directory.InternalCreateDirectory(fullPathInternal, path, directorySecurity);
      return new DirectoryInfo(fullPathInternal);
    }

    /// <summary>Создает каталог.</summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Не удалось создать каталог.
    /// </exception>
    public void Create()
    {
      Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, (object) null, true);
    }

    /// <summary>
    ///   Создает каталог с помощью объекта <see cref="T:System.Security.AccessControl.DirectorySecurity" />.
    /// </summary>
    /// <param name="directorySecurity">
    ///   Элемент управления доступом, который необходимо применить к каталогу.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог, заданный параметром <paramref name="path" />, доступен только для чтения или не является пустым каталогом.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Предпринята попытка создать каталог с единственным знаком двоеточия (:).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог, заданный параметром <paramref name="path" />, доступен только для чтения или не является пустым каталогом.
    /// </exception>
    public void Create(DirectorySecurity directorySecurity)
    {
      Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, (object) directorySecurity, true);
    }

    /// <summary>Получает значение, определяющее наличие каталога.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если каталог существует; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Exists
    {
      [SecuritySafeCritical] get
      {
        try
        {
          if (this._dataInitialised == -1)
            this.Refresh();
          if (this._dataInitialised != 0)
            return false;
          return this._data.fileAttributes != -1 && (uint) (this._data.fileAttributes & 16) > 0U;
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует записи списка управления доступом (ACL) для каталога, описываемого текущим объектом <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует правила управления доступом к каталогу.
    /// </returns>
    /// <exception cref="T:System.SystemException">
    ///   Не удается найти или изменить каталог.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий процесс не может открыть каталог из-за отсутствия соответствующих прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии каталога возникла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Каталог доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectorySecurity GetAccessControl()
    {
      return Directory.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует заданный тип записей списка управления доступом для каталога, описываемого текущим объектом <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <param name="includeSections">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее тип сведений о списке ACL, которые необходимо получить.
    /// </param>
    /// <returns>
    /// Объект <see cref="T:System.Security.AccessControl.DirectorySecurity" />, который инкапсулирует правила управления доступом для файла, описанные параметром <paramref name="path" />.
    /// 
    /// Исключения
    /// 
    ///         Тип исключения
    /// 
    ///         Условие
    /// 
    ///         <see cref="T:System.SystemException" />
    /// 
    ///         Не удается найти или изменить каталог.
    /// 
    ///         <see cref="T:System.UnauthorizedAccessException" />
    /// 
    ///         Текущий процесс не может открыть каталог из-за отсутствия соответствующих прав доступа.
    /// 
    ///         <see cref="T:System.IO.IOException" />
    /// 
    ///         При открытии каталога возникла ошибка ввода-вывода.
    /// 
    ///         <see cref="T:System.PlatformNotSupportedException" />
    /// 
    ///         Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// 
    ///         <see cref="T:System.UnauthorizedAccessException" />
    /// 
    ///         Каталог доступен только для чтения.
    /// 
    ///         -или-
    /// 
    ///         Эта операция не поддерживается на текущей платформе.
    /// 
    ///         -или-
    /// 
    ///         У вызывающего объекта отсутствует необходимое разрешение.
    ///       </returns>
    public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
    {
      return Directory.GetAccessControl(this.FullPath, includeSections);
    }

    /// <summary>
    ///   Применяет записи списка управления доступом (ACL), описанные объектом <see cref="T:System.Security.AccessControl.DirectorySecurity" />, к каталогу, который описывается текущим объектом <see cref="T:System.IO.DirectoryInfo" />.
    /// </summary>
    /// <param name="directorySecurity">
    ///   Объект, характеризующий запись ACL, которую необходимо применить к каталогу, описанному параметром <paramref name="path" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="directorySecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти или изменить файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий процесс не может открыть файл из-за отсутствия соответствующих прав доступа.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    public void SetAccessControl(DirectorySecurity directorySecurity)
    {
      Directory.SetAccessControl(this.FullPath, directorySecurity);
    }

    /// <summary>
    ///   Возвращает список файлов текущего каталога, соответствующих заданному шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Массив типа <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый путь (например, указывающий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileInfo[] GetFiles(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalGetFiles(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает список файлов из текущего каталога, соответствующих заданному шаблону поиска, с использованием значения, которое позволяет определить, следует ли выполнять поиск в подкаталогах.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// </param>
    /// <returns>
    ///   Массив типа <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый путь (например, указывающий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetFiles(searchPattern, searchOption);
    }

    private FileInfo[] InternalGetFiles(string searchPattern, SearchOption searchOption)
    {
      return new List<FileInfo>(FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>Возвращает список файлов текущего каталога.</summary>
    /// <returns>
    ///   Массив типа <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    public FileInfo[] GetFiles()
    {
      return this.InternalGetFiles("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>Возвращает подкаталоги текущего каталога.</summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.IO.DirectoryInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo[] GetDirectories()
    {
      return this.InternalGetDirectories("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Извлекает массив строго типизированных объектов <see cref="T:System.IO.FileSystemInfo" />, представляющих файлы и подкаталоги, соответствующие заданным критериям поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов и файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Массив строго типизированных объектов <see langword="FileSystemInfo" />, соответствующих критерию поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalGetFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Извлекает массив объектов <see cref="T:System.IO.FileSystemInfo" />, представляющих файлы и подкаталоги, соответствующие заданным критериям поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов и файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    ///    Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Массив элементов файловой системы, удовлетворяющих критериям поиска.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetFileSystemInfos(searchPattern, searchOption);
    }

    private FileSystemInfo[] InternalGetFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      return new List<FileSystemInfo>(FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>
    ///   Возвращает массив строго типизированных объектов <see cref="T:System.IO.FileSystemInfo" />, представляющих все файлы и подкаталоги в том или ином каталоге.
    /// </summary>
    /// <returns>
    ///   Массив строго типизированных записей <see cref="T:System.IO.FileSystemInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый путь (например, указывающий на несопоставленный диск).
    /// </exception>
    public FileSystemInfo[] GetFileSystemInfos()
    {
      return this.InternalGetFileSystemInfos("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает массив каталогов текущего объекта <see cref="T:System.IO.DirectoryInfo" />, отвечающих заданным условиям поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Массив элементов типа <see langword="DirectoryInfo" />, отвечающих критерию <paramref name="searchPattern" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see langword="DirectoryInfo" />, является недействительным (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo[] GetDirectories(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalGetDirectories(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает массив каталогов в текущем объекте <see cref="T:System.IO.DirectoryInfo" />, отвечающих заданным условиям поиска, с использованием значения, которое позволяет определить, следует ли выполнять поиск в подкаталогах.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    /// </param>
    /// <returns>
    ///   Массив элементов типа <see langword="DirectoryInfo" />, отвечающих критерию <paramref name="searchPattern" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="searchPattern " /> содержит один или несколько недопустимых символов, определенных методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see langword="DirectoryInfo" />, является недействительным (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetDirectories(searchPattern, searchOption);
    }

    private DirectoryInfo[] InternalGetDirectories(string searchPattern, SearchOption searchOption)
    {
      return new List<DirectoryInfo>(FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о каталогах в текущем каталоге.
    /// </summary>
    /// <returns>
    ///   Перечисляемая коллекция каталогов в текущем каталоге.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories()
    {
      return this.InternalEnumerateDirectories("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о каталогах, соответствующую указанному шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция каталогов, соответствующая параметру <paramref name="searchPattern" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalEnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о каталогах, соответствующую указанному шаблону поиска и параметру поиска в подкаталогах.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    ///    Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция каталогов, соответствующая параметру <paramref name="searchPattern" /> и <paramref name="searchOption" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateDirectories(searchPattern, searchOption);
    }

    private IEnumerable<DirectoryInfo> InternalEnumerateDirectories(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файлах в текущем каталоге.
    /// </summary>
    /// <returns>Перечисляемая коллекция файлов в текущем каталоге.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileInfo> EnumerateFiles()
    {
      return this.InternalEnumerateFiles("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файлах, соответствующую шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция файлов, соответствующая параметру <paramref name="searchPattern" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalEnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файлах, соответствующую указанному шаблону поиска и параметру поиска в подкаталогах.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами файлов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    ///    Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция файлов, соответствующая параметру <paramref name="searchPattern" /> и <paramref name="searchOption" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateFiles(searchPattern, searchOption);
    }

    private IEnumerable<FileInfo> InternalEnumerateFiles(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файловой системе текущего каталога.
    /// </summary>
    /// <returns>
    ///   Перечисляемая коллекция сведений о файловой системе текущего каталога.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
    {
      return this.InternalEnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файловой системе, соответствующую указанному шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция сведений об объектах файловой системы, соответствующая параметру <paramref name="searchPattern" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      return this.InternalEnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    ///   Возвращает перечисляемую коллекцию сведений о файловой системе, соответствующую указанному шаблону поиска и параметру поиска в подкаталогах.
    /// </summary>
    /// <param name="searchPattern">
    ///   Строка поиска, которая будет сравниваться с именами каталогов.
    ///     Этот параметр может содержать сочетание допустимого литерального пути и подстановочных символов (* и ?)
    ///    (см. раздел "Примечания"), но не поддерживает регулярные выражения.
    ///    Шаблон по умолчанию, возвращающий все файлы, — "*".
    /// </param>
    /// <param name="searchOption">
    ///   Одно из значений перечисления, определяющее, следует ли выполнять поиск только в текущем каталоге или также во всех его подкаталогах.
    ///    Значение по умолчанию — <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <returns>
    ///   Перечисляемая коллекция сведений об объектах файловой системы, соответствующая параметру <paramref name="searchPattern" /> и <paramref name="searchOption" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="searchOption" /> не является допустимым значением <see cref="T:System.IO.SearchOption" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, содержащийся в объекте <see cref="T:System.IO.DirectoryInfo" />, является недействительным (например,он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException(nameof (searchOption), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateFileSystemInfos(searchPattern, searchOption);
    }

    private IEnumerable<FileSystemInfo> InternalEnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>Получает корневую часть каталога.</summary>
    /// <returns>Объект, представляющий корень каталога.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo Root
    {
      [SecuritySafeCritical] get
      {
        string str = this.FullPath.Substring(0, Path.GetRootLength(this.FullPath));
        FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, Directory.GetDemandDir(str, true), false, false);
        return new DirectoryInfo(str);
      }
    }

    /// <summary>
    ///   Перемещает экземпляр <see cref="T:System.IO.DirectoryInfo" /> и его содержимое в местоположение, на которое указывает новый путь.
    /// </summary>
    /// <param name="destDirName">
    ///   Имя и путь к местоположению, в которое необходимо переместить указанный каталог.
    ///    Место назначения не должно находиться на другом томе устройства или в каталоге с идентичным именем.
    ///    Оно должно представлять собой существующий каталог, в который перемещаемый каталог будет добавлен в виде подкаталога.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destDirName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="destDirName" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Была предпринята попытка переместить каталог в другой том.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destDirName" /> уже существует.
    /// 
    ///   -или-
    /// 
    ///   Нет прав доступа к этому пути.
    /// 
    ///   -или-
    /// 
    ///   Каталог перемещается, и каталог назначения имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Не удается найти каталог назначения.
    /// </exception>
    [SecuritySafeCritical]
    public void MoveTo(string destDirName)
    {
      if (destDirName == null)
        throw new ArgumentNullException(nameof (destDirName));
      if (destDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destDirName));
      Directory.CheckPermissions(this.DisplayPath, this.FullPath, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.Write);
      string fullPathInternal = Path.GetFullPathInternal(destDirName);
      if (!fullPathInternal.EndsWith(Path.DirectorySeparatorChar))
        fullPathInternal += Path.DirectorySeparatorChar.ToString();
      Directory.CheckPermissions(destDirName, fullPathInternal, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.Write);
      string str = !this.FullPath.EndsWith(Path.DirectorySeparatorChar) ? this.FullPath + Path.DirectorySeparatorChar.ToString() : this.FullPath;
      if (string.Compare(str, fullPathInternal, StringComparison.OrdinalIgnoreCase) == 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
      if (string.Compare(Path.GetPathRoot(str), Path.GetPathRoot(fullPathInternal), StringComparison.OrdinalIgnoreCase) != 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
      if (!Win32Native.MoveFile(this.FullPath, destDirName))
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 2)
        {
          errorCode = 3;
          __Error.WinIOError(errorCode, this.DisplayPath);
        }
        if (errorCode == 5)
          throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) this.DisplayPath));
        __Error.WinIOError(errorCode, string.Empty);
      }
      this.FullPath = fullPathInternal;
      this.OriginalPath = destDirName;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
      this._dataInitialised = -1;
    }

    /// <summary>
    ///   Удаляет этот <see cref="T:System.IO.DirectoryInfo" />, если он пуст.
    /// </summary>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Каталог содержит файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог, описанный этим объектом <see cref="T:System.IO.DirectoryInfo" />, не существует или не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог не пуст.
    /// 
    ///   -или-
    /// 
    ///   Каталог является текущим рабочим каталогом приложения.
    /// 
    ///   -или-
    /// 
    ///   Существует открытый дескриптор в каталоге, и операционной системой является Windows XP или более ранней версии.
    ///    Этот открытый дескриптор может быть результатом перечисление каталогов.
    ///    Для получения дополнительной информации см. Практическое руководство. Перечисление каталогов и файлов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public override void Delete()
    {
      Directory.Delete(this.FullPath, this.OriginalPath, false, true);
    }

    /// <summary>
    ///   Удаляет данный экземпляр <see cref="T:System.IO.DirectoryInfo" />, указывая, следует ли также удалить подкаталоги и файлы.
    /// </summary>
    /// <param name="recursive">
    ///   Значение <see langword="true" /> позволяет удалить каталог, его подкаталоги и все файлы, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Каталог содержит файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог, описанный этим объектом <see cref="T:System.IO.DirectoryInfo" />, не существует или не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Каталог доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Каталог содержит файлы или подкаталоги и <paramref name="recursive" /> равен <see langword="false" />.
    /// 
    ///   -или-
    /// 
    ///   Каталог является текущим рабочим каталогом приложения.
    /// 
    ///   -или-
    /// 
    ///   Для каталога или одного из его файлов имеется открытый дескриптор, а операционной системой является Windows XP или более ранней версии.
    ///    Этот открытый дескриптор может быть результатом перечисления каталогов и файлов.
    ///    Для получения дополнительной информации см. Практическое руководство. Перечисление каталогов и файлов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public void Delete(bool recursive)
    {
      Directory.Delete(this.FullPath, this.OriginalPath, recursive, true);
    }

    /// <summary>Возвращает исходный путь, переданный пользователем.</summary>
    /// <returns>Возвращает исходный путь, переданный пользователем.</returns>
    public override string ToString()
    {
      return this.DisplayPath;
    }

    private static string GetDisplayName(string originalPath, string fullPath)
    {
      return originalPath.Length != 2 || originalPath[1] != ':' ? originalPath : ".";
    }

    private static string GetDirName(string fullPath)
    {
      string str;
      if (fullPath.Length > 3)
      {
        string path = fullPath;
        if (fullPath.EndsWith(Path.DirectorySeparatorChar))
          path = fullPath.Substring(0, fullPath.Length - 1);
        str = Path.GetFileName(path);
      }
      else
        str = fullPath;
      return str;
    }
  }
}
