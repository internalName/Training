// Decompiled with JetBrains decompiler
// Type: System.IO.FileInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет свойства и методы экземпляра для создания, копирования, удаления, перемещения и открытия файлов, а также позволяет создавать объекты <see cref="T:System.IO.FileStream" />.
  ///    Этот класс не наследуется.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileInfo : FileSystemInfo
  {
    private string _name;

    /// <summary>
    ///   Выполняет инициализацию нового экземпляра класса <see cref="T:System.IO.FileInfo" />, который служит оболочкой для пути файла.
    /// </summary>
    /// <param name="fileName">
    ///   Полное имя нового файла или относительное имя файла.
    ///    Путь не должен заканчиваться символом разделителя каталогов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Имя файла является пустой строкой, содержит только пробелы или недопустимые символы.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Доступ к <paramref name="fileName" /> запрещен.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="fileName" /> содержит двоеточие (:) в середине строки.
    /// </exception>
    [SecuritySafeCritical]
    public FileInfo(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      this.Init(fileName, true);
    }

    [SecurityCritical]
    private void Init(string fileName, bool checkHost)
    {
      this.OriginalPath = fileName;
      string fullPathInternal = Path.GetFullPathInternal(fileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      this._name = Path.GetFileName(fileName);
      this.FullPath = fullPathInternal;
      this.DisplayPath = this.GetDisplayPath(fileName);
    }

    private string GetDisplayPath(string originalPath)
    {
      return originalPath;
    }

    [SecurityCritical]
    private FileInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, this.FullPath, false, false);
      this._name = Path.GetFileName(this.OriginalPath);
      this.DisplayPath = this.GetDisplayPath(this.OriginalPath);
    }

    internal FileInfo(string fullPath, bool ignoreThis)
    {
      this._name = Path.GetFileName(fullPath);
      this.OriginalPath = this._name;
      this.FullPath = fullPath;
      this.DisplayPath = this._name;
    }

    internal FileInfo(string fullPath, string fileName)
    {
      this._name = fileName;
      this.OriginalPath = this._name;
      this.FullPath = fullPath;
      this.DisplayPath = this._name;
    }

    /// <summary>Возвращает имя файла.</summary>
    /// <returns>Имя файла</returns>
    public override string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>Получает размер текущего файла в байтах.</summary>
    /// <returns>Размер текущего файла в байтах.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удалось обновить состояние файла или каталога.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не существует.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Length" /> вызывается для каталога.
    /// </exception>
    public long Length
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
          this.Refresh();
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        if ((this._data.fileAttributes & 16) != 0)
          __Error.WinIOError(2, this.DisplayPath);
        return (long) this._data.fileSizeHigh << 32 | (long) this._data.fileSizeLow & (long) uint.MaxValue;
      }
    }

    /// <summary>
    ///   Получает строку, представляющую полный путь к каталогу.
    /// </summary>
    /// <returns>Строка, представляющая полный путь к каталогу.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <see langword="null" /> было передано в качестве имени каталога.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Полный путь содержит 260 символов или более.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public string DirectoryName
    {
      [SecuritySafeCritical] get
      {
        string directoryName = Path.GetDirectoryName(this.FullPath);
        if (directoryName != null)
          FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, directoryName, false, false);
        return directoryName;
      }
    }

    /// <summary>Получает экземпляр родительского каталога.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.IO.DirectoryInfo" />, представляющий родительский каталог данного файла.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public DirectoryInfo Directory
    {
      get
      {
        string directoryName = this.DirectoryName;
        if (directoryName == null)
          return (DirectoryInfo) null;
        return new DirectoryInfo(directoryName);
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, позволяющее определить, является ли текущий файл доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий файл доступен только для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описываемый текущим объектом <see cref="T:System.IO.FileInfo" />, не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Пользователь не имеет разрешения на запись, но пытается задать для этого свойства значение <see langword="false" />.
    /// </exception>
    public bool IsReadOnly
    {
      get
      {
        return (uint) (this.Attributes & FileAttributes.ReadOnly) > 0U;
      }
      set
      {
        if (value)
          this.Attributes |= FileAttributes.ReadOnly;
        else
          this.Attributes &= ~FileAttributes.ReadOnly;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует записи списка управления доступом (ACL) для файла, описываемого текущим объектом <see cref="T:System.IO.FileInfo" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует правила управления доступом к текущему файлу.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">
    ///   Текущая учетная запись системы не имеет прав администратора.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileSecurity GetAccessControl()
    {
      return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует заданный тип записей списка управления доступом для файла, описываемого текущим объектом <see cref="T:System.IO.FileInfo" />.
    /// </summary>
    /// <param name="includeSections">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.AccessControlSections" />, которое указывает, какую группу записей списка ACL необходимо извлечь.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует правила управления доступом к текущему файлу.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows 2000 или более поздней версией Windows.
    /// </exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">
    ///   Текущая учетная запись системы не имеет прав администратора.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public FileSecurity GetAccessControl(AccessControlSections includeSections)
    {
      return File.GetAccessControl(this.FullPath, includeSections);
    }

    /// <summary>
    ///   Применяет записи списка управления доступом (ACL), описанные объектом <see cref="T:System.Security.AccessControl.FileSecurity" />, к файлу, который описывается текущим объектом <see cref="T:System.IO.FileInfo" />.
    /// </summary>
    /// <param name="fileSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSecurity" />, описывающий запись списка управления доступом (ACL), которую необходимо применить к текущему файлу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileSecurity" /> имеет значение <see langword="null" />.
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
    public void SetAccessControl(FileSecurity fileSecurity)
    {
      File.SetAccessControl(this.FullPath, fileSecurity);
    }

    /// <summary>
    ///   Создает поток <see cref="T:System.IO.StreamReader" /> с кодировкой UTF-8, который считывает данные из существующего текстового файла.
    /// </summary>
    /// <returns>
    ///   Новый объект <see langword="StreamReader" /> с кодировкой UTF8.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    [SecuritySafeCritical]
    public StreamReader OpenText()
    {
      return new StreamReader(this.FullPath, Encoding.UTF8, true, StreamReader.DefaultBufferSize, false);
    }

    /// <summary>
    ///   Создает <see cref="T:System.IO.StreamWriter" />, который записывает новый текстовый файл.
    /// </summary>
    /// <returns>
    ///   Новый объект <see langword="StreamWriter" />.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Имя файла является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Диск доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public StreamWriter CreateText()
    {
      return new StreamWriter(this.FullPath, false);
    }

    /// <summary>
    ///   Создает <see cref="T:System.IO.StreamWriter" />, который добавляет текст в файл, представленный этим экземпляром <see cref="T:System.IO.FileInfo" />..
    /// </summary>
    /// <returns>
    ///   Новый объект <see langword="StreamWriter" />.
    /// </returns>
    public StreamWriter AppendText()
    {
      return new StreamWriter(this.FullPath, true);
    }

    /// <summary>
    ///   Копирует существующий файл в новый файл и запрещает перезапись существующего файла.
    /// </summary>
    /// <param name="destFileName">
    ///   Имя нового файла, в который будет выполняться копирование.
    /// </param>
    /// <returns>Новый файл с полным именем.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="destFileName" /> является пустой строкой, содержит только пробелы или недопустимые символы.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка, или файл назначения уже существует.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Передан путь к каталогу, или файл перемещен на другой диск.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог, указанный в <paramref name="destFileName" />, не существует.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="destFileName" /> содержит двоеточие (:) в строке, но не указывает том.
    /// </exception>
    public FileInfo CopyTo(string destFileName)
    {
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      destFileName = File.InternalCopy(this.FullPath, destFileName, false, true);
      return new FileInfo(destFileName, false);
    }

    /// <summary>
    ///   Копирует существующий файл в новый файл и разрешает перезапись существующего файла.
    /// </summary>
    /// <param name="destFileName">
    ///   Имя нового файла, в который будет выполняться копирование.
    /// </param>
    /// <param name="overwrite">
    ///   Значение <see langword="true" /> позволяет разрешить перезапись существующего файла; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Новый файл или перезапись существующего файла, если для параметра <paramref name="overwrite" /> задано значение <see langword="true" />.
    ///    Если файл существует и для параметра <paramref name="overwrite" /> задано значение <see langword="false" />, создается исключение <see cref="T:System.IO.IOException" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="destFileName" /> является пустой строкой, содержит только пробелы или недопустимые символы.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка, или файл назначения уже существует, а <paramref name="overwrite" /> имеет значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог, указанный в <paramref name="destFileName" />, не существует.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Передан путь к каталогу, или файл перемещен на другой диск.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="destFileName" /> содержит двоеточие (:) в середине строки.
    /// </exception>
    public FileInfo CopyTo(string destFileName, bool overwrite)
    {
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      destFileName = File.InternalCopy(this.FullPath, destFileName, overwrite, true);
      return new FileInfo(destFileName, false);
    }

    /// <summary>Создает файл.</summary>
    /// <returns>Новый файл.</returns>
    public FileStream Create()
    {
      return File.Create(this.FullPath);
    }

    /// <summary>Удаляет файл без возможности восстановления.</summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Целевой файл открыт или сопоставлен в памяти на компьютере под управлением Microsoft Windows NT.
    /// 
    ///   -или-
    /// 
    ///   Для файла имеется открытый дескриптор, а операционной системой является Windows XP или более ранней версии.
    ///    Этот открытый дескриптор может быть результатом перечисления каталогов и файлов.
    ///    Для получения дополнительной информации см. Практическое руководство. Перечисление каталогов и файлов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Путь является каталогом.
    /// </exception>
    [SecuritySafeCritical]
    public override void Delete()
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, this.FullPath, false, false);
      if (Win32Native.DeleteFile(this.FullPath))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, this.DisplayPath);
    }

    /// <summary>
    ///   Расшифровывает файл, зашифрованный текущей учетной записью с помощью метода <see cref="M:System.IO.FileInfo.Encrypt" />.
    /// </summary>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти файл, описанный текущим объектом <see cref="T:System.IO.FileInfo" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Файловая система не является системой NTFS.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Файл, описанный текущим объектом <see cref="T:System.IO.FileInfo" />, доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [ComVisible(false)]
    public void Decrypt()
    {
      File.Decrypt(this.FullPath);
    }

    /// <summary>
    ///   Шифрует файл таким образом, чтобы его можно было расшифровать только с помощью учетной записи, которая использовалась для шифрования.
    /// </summary>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти файл, описанный текущим объектом <see cref="T:System.IO.FileInfo" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Файловая система не является системой NTFS.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows NT или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Файл, описанный текущим объектом <see cref="T:System.IO.FileInfo" />, доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [ComVisible(false)]
    public void Encrypt()
    {
      File.Encrypt(this.FullPath);
    }

    /// <summary>
    ///   Получает значение, показывающее, существует ли файл.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если файл существует; значение <see langword="false" />, если файл не существует или если это каталог.
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
          return (this._data.fileAttributes & 16) == 0;
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>Открывает файл в заданном режиме.</summary>
    /// <param name="mode">
    ///   Константа <see cref="T:System.IO.FileMode" /> задает режим (например <see langword="Open" /> или <see langword="Append" />), в котором необходимо открыть файл.
    /// </param>
    /// <returns>
    ///   Файл открыт в заданном режиме с доступом для чтения и записи и без предоставления общего доступа.
    /// </returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Файл доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл уже открыт.
    /// </exception>
    public FileStream Open(FileMode mode)
    {
      return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
    }

    /// <summary>
    ///   Открывает файл в заданном режиме с доступом для чтения или записи, или и для чтения, и для записи.
    /// </summary>
    /// <param name="mode">
    ///   Константа <see cref="T:System.IO.FileMode" /> задает режим (например <see langword="Open" /> или <see langword="Append" />), в котором необходимо открыть файл.
    /// </param>
    /// <param name="access">
    ///   Константа <see cref="T:System.IO.FileAccess" /> задает доступ к открываемому файлу: <see langword="Read" />, <see langword="Write" /> или <see langword="ReadWrite" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileStream" /> открыт в заданном режиме, с заданными правами и без предоставления общего доступа.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл уже открыт.
    /// </exception>
    public FileStream Open(FileMode mode, FileAccess access)
    {
      return this.Open(mode, access, FileShare.None);
    }

    /// <summary>
    ///   Открывает файл в заданном режиме с доступом для чтения, записи или и для чтения, и для записи и с заданным параметром совместного доступа.
    /// </summary>
    /// <param name="mode">
    ///   Константа <see cref="T:System.IO.FileMode" /> задает режим (например <see langword="Open" /> или <see langword="Append" />), в котором необходимо открыть файл.
    /// </param>
    /// <param name="access">
    ///   Константа <see cref="T:System.IO.FileAccess" /> задает доступ к открываемому файлу: <see langword="Read" />, <see langword="Write" /> или <see langword="ReadWrite" />.
    /// </param>
    /// <param name="share">
    ///   Константа <see cref="T:System.IO.FileShare" /> задает тип доступа к файлу для других объектов <see langword="FileStream" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileStream" /> открыт в заданном режиме, с заданными правами и параметром общего доступа.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл уже открыт.
    /// </exception>
    public FileStream Open(FileMode mode, FileAccess access, FileShare share)
    {
      return new FileStream(this.FullPath, mode, access, share);
    }

    /// <summary>
    ///   Создает доступный только для чтения поток <see cref="T:System.IO.FileStream" />.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.IO.FileStream" />, доступный только для чтения.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Файл уже открыт.
    /// </exception>
    public FileStream OpenRead()
    {
      return new FileStream(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
    }

    /// <summary>
    ///   Создает доступный только для чтения поток <see cref="T:System.IO.FileStream" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileStream" />, доступный только для записи, не предназначенный для совместного использования и служащий для нового или существующего файла.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Путь, указанный при создании экземпляра объекта <see cref="T:System.IO.FileInfo" />, доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь, указанный при создании экземпляра объекта <see cref="T:System.IO.FileInfo" />, является недопустимым, например находится на несопоставленном диске.
    /// </exception>
    public FileStream OpenWrite()
    {
      return new FileStream(this.FullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    /// <summary>
    ///   Перемещает заданный файл в новое местоположение и разрешает переименование файла.
    /// </summary>
    /// <param name="destFileName">
    ///   Путь, указывающий на местоположение, в которое необходимо переместить файл; в этом же пути можно задать и другое имя файла.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода, например, конечный файл уже существует или устройство назначения не готово.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="destFileName" /> является пустой строкой, содержит только пробелы или содержит недопустимые символы.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="destFileName" /> доступен только для чтения или является каталогом.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="destFileName" /> содержит двоеточие (:) в середине строки.
    /// </exception>
    [SecuritySafeCritical]
    public void MoveTo(string destFileName)
    {
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      string fullPathInternal = Path.GetFullPathInternal(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, this.FullPath, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (!Win32Native.MoveFile(this.FullPath, fullPathInternal))
        __Error.WinIOError();
      this.FullPath = fullPathInternal;
      this.OriginalPath = destFileName;
      this._name = Path.GetFileName(fullPathInternal);
      this.DisplayPath = this.GetDisplayPath(destFileName);
      this._dataInitialised = -1;
    }

    /// <summary>
    ///   Заменяет содержимое заданного файла на содержимое файла, которое описано в текущем объекте <see cref="T:System.IO.FileInfo" />, удаляет исходный файл и создает резервную копию замененного файла.
    /// </summary>
    /// <param name="destinationFileName">
    ///   Имя файла, который необходимо заменить текущим файлом.
    /// </param>
    /// <param name="destinationBackupFileName">
    ///   Имя файла, с которым необходимо создать резервную копию файла, описанного параметром <paramref name="destFileName" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileInfo" />, который инкапсулирует сведения о файле, описанные параметром <paramref name="destFileName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Путь, описываемый параметром <paramref name="destFileName" />, имел недопустимую форму.
    /// 
    ///   -или-
    /// 
    ///   Путь, описываемый параметром <paramref name="destBackupFileName" />, имел недопустимую форму.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описываемый текущим объектом <see cref="T:System.IO.FileInfo" />, не найден.
    /// 
    ///   -или-
    /// 
    ///   Файл, описываемый параметром <paramref name="destinationFileName" />, не найден.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows NT или более поздней версии.
    /// </exception>
    [ComVisible(false)]
    public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
    {
      return this.Replace(destinationFileName, destinationBackupFileName, false);
    }

    /// <summary>
    ///   Заменяет содержимое заданного файла на содержимое файла, которое описано в текущем объекте <see cref="T:System.IO.FileInfo" />, удаляет исходный файл и создает резервную копию замененного файла.
    ///     Также позволяет определить, нужно ли игнорировать ошибки слияния.
    /// </summary>
    /// <param name="destinationFileName">
    ///   Имя файла, который необходимо заменить текущим файлом.
    /// </param>
    /// <param name="destinationBackupFileName">
    ///   Имя файла, с которым необходимо создать резервную копию файла, описанного параметром <paramref name="destFileName" />.
    /// </param>
    /// <param name="ignoreMetadataErrors">
    ///   Значение <see langword="true" /> позволяет игнорировать ошибки слияния (например атрибуты и списки ACL), исходящие из заменяемого файла в заменяющий; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileInfo" />, который инкапсулирует сведения о файле, описанные параметром <paramref name="destFileName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Путь, описываемый параметром <paramref name="destFileName" />, имел недопустимую форму.
    /// 
    ///   -или-
    /// 
    ///   Путь, описываемый параметром <paramref name="destBackupFileName" />, имел недопустимую форму.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описываемый текущим объектом <see cref="T:System.IO.FileInfo" />, не найден.
    /// 
    ///   -или-
    /// 
    ///   Файл, описываемый параметром <paramref name="destinationFileName" />, не найден.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Microsoft Windows NT или более поздней версии.
    /// </exception>
    [ComVisible(false)]
    public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      File.Replace(this.FullPath, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
      return new FileInfo(destinationFileName);
    }

    /// <summary>Возвращает путь в виде строки.</summary>
    /// <returns>Строка, содержащая путь.</returns>
    public override string ToString()
    {
      return this.DisplayPath;
    }
  }
}
