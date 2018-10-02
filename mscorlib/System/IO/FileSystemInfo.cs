// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет базовый класс для объектов <see cref="T:System.IO.FileInfo" /> и <see cref="T:System.IO.DirectoryInfo" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [FileIOPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
  {
    internal int _dataInitialised = -1;
    private string _displayPath = "";
    [SecurityCritical]
    internal Win32Native.WIN32_FILE_ATTRIBUTE_DATA _data;
    private const int ERROR_INVALID_PARAMETER = 87;
    internal const int ERROR_ACCESS_DENIED = 5;
    /// <summary>Представляет полный путь к каталогу или файлу.</summary>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Полный путь содержит 260 символов или более.
    /// </exception>
    protected string FullPath;
    /// <summary>
    ///   Первоначально заданный пользователем относительный или абсолютный путь.
    /// </summary>
    protected string OriginalPath;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileSystemInfo" />.
    /// </summary>
    protected FileSystemInfo()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileSystemInfo" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> имеет значение null.
    /// </exception>
    protected FileSystemInfo(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.FullPath = Path.GetFullPathInternal(info.GetString(nameof (FullPath)));
      this.OriginalPath = info.GetString(nameof (OriginalPath));
      this._dataInitialised = -1;
    }

    [SecurityCritical]
    internal void InitializeFrom(ref Win32Native.WIN32_FIND_DATA findData)
    {
      this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      this._data.PopulateFrom(ref findData);
      this._dataInitialised = 0;
    }

    /// <summary>Получает полный путь к каталогу или файлу.</summary>
    /// <returns>Строка, содержащая полный путь.</returns>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Полный путь и имя файла — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public virtual string FullName
    {
      [SecuritySafeCritical] get
      {
        FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, this.FullPath, false, true);
        return this.FullPath;
      }
    }

    internal virtual string UnsafeGetFullName
    {
      [SecurityCritical] get
      {
        FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, this.FullPath, false, true);
        return this.FullPath;
      }
    }

    /// <summary>Получает строку, содержащую расширение файла.</summary>
    /// <returns>
    ///   Строка, содержащая расширение <see cref="T:System.IO.FileSystemInfo" />.
    /// </returns>
    public string Extension
    {
      get
      {
        int length = this.FullPath.Length;
        int startIndex = length;
        while (--startIndex >= 0)
        {
          char ch = this.FullPath[startIndex];
          if (ch == '.')
            return this.FullPath.Substring(startIndex, length - startIndex);
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            break;
        }
        return string.Empty;
      }
    }

    /// <summary>
    ///   Для файлов — получает имя файла.
    ///    Для каталогов — получает имя последнего каталога в иерархии, если таковая существует.
    ///    В противном случае свойство <see langword="Name" /> получает имя данного каталога.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая собой имя родительского каталога, имя последнего каталога в иерархии или имя файла, включая расширение имени файла.
    /// </returns>
    public abstract string Name { get; }

    /// <summary>
    ///   Получает значение, показывающее, существует ли данный файл или каталог.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если файл или каталог существует; в противном случае — значение <see langword="false" />.
    /// </returns>
    public abstract bool Exists { get; }

    /// <summary>Удаляет файл или каталог.</summary>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь недопустим; Например это на неподключенном диске.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Есть открытый дескриптор для файла или каталога, а операционной системой является Windows XP или более ранней версии.
    ///    Этот открытый дескриптор может быть результатом перечисления каталогов и файлов.
    ///    Для получения дополнительной информации см. Практическое руководство. Перечисление каталогов и файлов.
    /// </exception>
    public abstract void Delete();

    /// <summary>
    ///   Получает или задает время создания текущего файла или каталога.
    /// </summary>
    /// <returns>
    ///   Дата и время создания текущего объекта <see cref="T:System.IO.FileSystemInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь недопустим; Например это на неподключенном диске.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается задать время создания недопустимый.
    /// </exception>
    public DateTime CreationTime
    {
      get
      {
        return this.CreationTimeUtc.ToLocalTime();
      }
      set
      {
        this.CreationTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>
    ///   Получает или задает время создания текущего файла или каталога в формате UTC.
    /// </summary>
    /// <returns>
    ///   Дата и время создания текущего объекта <see cref="T:System.IO.FileSystemInfo" /> в формате UTC.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь недопустим; Например это на неподключенном диске.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается задать недопустимое время доступа.
    /// </exception>
    [ComVisible(false)]
    public DateTime CreationTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc(this._data.ftCreationTime.ToTicks());
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetCreationTimeUtc(this.FullPath, value);
        else
          File.SetCreationTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>
    ///   Получает или задает время последнего доступа к текущему файлу или каталогу.
    /// </summary>
    /// <returns>
    ///   Время последнего доступа к текущему файлу или каталогу.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается задать недопустимое время доступа
    /// </exception>
    public DateTime LastAccessTime
    {
      get
      {
        return this.LastAccessTimeUtc.ToLocalTime();
      }
      set
      {
        this.LastAccessTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>
    ///   Получает или задает дату и время последнего доступа к заданному файлу или каталогу в формате UTC.
    /// </summary>
    /// <returns>
    ///   Время последнего доступа к текущему файлу или каталогу в формате UTC.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается задать недопустимое время доступа.
    /// </exception>
    [ComVisible(false)]
    public DateTime LastAccessTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc(this._data.ftLastAccessTime.ToTicks());
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetLastAccessTimeUtc(this.FullPath, value);
        else
          File.SetLastAccessTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>
    ///   Получает или задает время последней операции записи в текущий файл или каталог.
    /// </summary>
    /// <returns>Время последней операции записи в текущий файл.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается установить время недопустимые записи.
    /// </exception>
    public DateTime LastWriteTime
    {
      get
      {
        return this.LastWriteTimeUtc.ToLocalTime();
      }
      set
      {
        this.LastWriteTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>
    ///   Получает или задает время последней операции записи в текущий файл или каталог в формате UTC.
    /// </summary>
    /// <returns>
    ///   Время последней операции записи в текущий файл в формате UTC.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Вызывающий объект пытается установить время недопустимые записи.
    /// </exception>
    [ComVisible(false)]
    public DateTime LastWriteTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc(this._data.ftLastWriteTime.ToTicks());
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetLastWriteTimeUtc(this.FullPath, value);
        else
          File.SetLastWriteTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>Обновляет состояние объекта.</summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Устройство, такое как жесткий диск не готов.
    /// </exception>
    [SecuritySafeCritical]
    public void Refresh()
    {
      this._dataInitialised = File.FillAttributeInfo(this.FullPath, ref this._data, false, false);
    }

    /// <summary>
    ///   Получает или задает атрибуты для текущего файла или каталога.
    /// </summary>
    /// <returns>
    ///   Атрибуты <see cref="T:System.IO.FileAttributes" /> текущего объекта <see cref="T:System.IO.FileSystemInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный файл не существует.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь недопустим; Например это на неподключенном диске.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Вызывающий оператор пытается установить недействительный атрибут файла.
    /// 
    ///   -или-
    /// 
    ///   Пользователь пытается установить значение атрибута, но не имеет разрешения на запись.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> не удается инициализировать данные.
    /// </exception>
    public FileAttributes Attributes
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return (FileAttributes) this._data.fileAttributes;
      }
      [SecuritySafeCritical] set
      {
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, this.FullPath, false, true);
        if (!Win32Native.SetFileAttributes(this.FullPath, (int) value))
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          switch (lastWin32Error)
          {
            case 5:
              throw new ArgumentException(Environment.GetResourceString("UnauthorizedAccess_IODenied_NoPathName"));
            case 87:
              throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
            default:
              __Error.WinIOError(lastWin32Error, this.DisplayPath);
              break;
          }
        }
        this._dataInitialised = -1;
      }
    }

    /// <summary>
    ///   Устанавливает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с именем файла и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, this.FullPath, false, true);
      info.AddValue("OriginalPath", (object) this.OriginalPath, typeof (string));
      info.AddValue("FullPath", (object) this.FullPath, typeof (string));
    }

    internal string DisplayPath
    {
      get
      {
        return this._displayPath;
      }
      set
      {
        this._displayPath = value;
      }
    }
  }
}
