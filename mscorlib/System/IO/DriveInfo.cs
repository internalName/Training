// Decompiled with JetBrains decompiler
// Type: System.IO.DriveInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>Предоставляет доступ к сведениям на диске.</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class DriveInfo : ISerializable
  {
    private string _name;
    private const string NameField = "_name";

    /// <summary>
    ///   Предоставляет доступ к сведениям на указанном диске.
    /// </summary>
    /// <param name="driveName">
    ///   Недействительный путь к диску или недействительная буква диска.
    ///    Буквы могут быть как в верхнем, так и в нижнем регистре, от ''a'' до ''z''.
    ///    Значение ''null'' не является допустимым значением.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Буква диска не может быть <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Первая буква <paramref name="driveName" /> не является буквой верхнего или нижнего регистра от '' a'' до '' z''.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="driveName" /> не ссылается на допустимый диск.
    /// </exception>
    [SecuritySafeCritical]
    public DriveInfo(string driveName)
    {
      if (driveName == null)
        throw new ArgumentNullException(nameof (driveName));
      if (driveName.Length == 1)
      {
        this._name = driveName + ":\\";
      }
      else
      {
        Path.CheckInvalidPathChars(driveName, false);
        this._name = Path.GetPathRoot(driveName);
        if (this._name == null || this._name.Length == 0 || this._name.StartsWith("\\\\", StringComparison.Ordinal))
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
      }
      if (this._name.Length == 2 && this._name[1] == ':')
        this._name += "\\";
      char ch = driveName[0];
      if ((ch < 'A' || ch > 'Z') && (ch < 'a' || ch > 'z'))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._name + ".").Demand();
    }

    [SecurityCritical]
    private DriveInfo(SerializationInfo info, StreamingContext context)
    {
      this._name = (string) info.GetValue(nameof (_name), typeof (string));
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._name + ".").Demand();
    }

    /// <summary>Возвращает имя диска, например C:\.</summary>
    /// <returns>Имя диска.</returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>
    ///   Возвращает тип диска, например компакт-диск, съемный, сетевой или несъемный.
    /// </summary>
    /// <returns>Одно из значений перечисления, задающее тип диска.</returns>
    public DriveType DriveType
    {
      [SecuritySafeCritical] get
      {
        return (DriveType) Win32Native.GetDriveType(this.Name);
      }
    }

    /// <summary>
    ///   Получает имя файловой системы, например NTFS или FAT32.
    /// </summary>
    /// <returns>Имя файловой системы на указанном диске.</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе к сведениям о диске.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Диск не существует или не сопоставлен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    public string DriveFormat
    {
      [SecuritySafeCritical] get
      {
        StringBuilder volumeName = new StringBuilder(50);
        StringBuilder fileSystemName = new StringBuilder(50);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          int volSerialNumber;
          int maxFileNameLen;
          int fileSystemFlags;
          if (!Win32Native.GetVolumeInformation(this.Name, volumeName, 50, out volSerialNumber, out maxFileNameLen, out fileSystemFlags, fileSystemName, 50))
            __Error.WinIODriveError(this.Name, Marshal.GetLastWin32Error());
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return fileSystemName.ToString();
      }
    }

    /// <summary>Возвращает значение, указывающее, готов ли диск.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если диск готов; значение <see langword="false" />, если диск не готов.
    /// </returns>
    public bool IsReady
    {
      [SecuritySafeCritical] get
      {
        return Directory.InternalExists(this.Name);
      }
    }

    /// <summary>
    ///   Указывает объем доступного свободного места на диске в байтах.
    /// </summary>
    /// <returns>
    ///   Объем свободного места, доступного на диске, в байтах.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе к сведениям о диске.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    public long AvailableFreeSpace
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long freeBytesForUser;
        try
        {
          long totalBytes;
          long freeBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return freeBytesForUser;
      }
    }

    /// <summary>
    ///   Возвращает общий объем свободного места, доступного на диске, в байтах.
    /// </summary>
    /// <returns>
    ///   Общий объем свободного места, доступного на диске, в байтах.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе к сведениям о диске.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Диск не сопоставлен или не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    public long TotalFreeSpace
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long freeBytes;
        try
        {
          long freeBytesForUser;
          long totalBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return freeBytes;
      }
    }

    /// <summary>
    ///   Возвращает общий размер места для хранения на диске в байтах.
    /// </summary>
    /// <returns>Общий размер диска в байтах.</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе к сведениям о диске.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Диск не сопоставлен или не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    public long TotalSize
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long totalBytes;
        try
        {
          long freeBytesForUser;
          long freeBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return totalBytes;
      }
    }

    /// <summary>
    ///   Возвращает имена всех логических дисков на компьютере.
    /// </summary>
    /// <returns>
    ///   Массив типа <see cref="T:System.IO.DriveInfo" />, представляющий логические диски на компьютере.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static DriveInfo[] GetDrives()
    {
      string[] logicalDrives = Directory.GetLogicalDrives();
      DriveInfo[] driveInfoArray = new DriveInfo[logicalDrives.Length];
      for (int index = 0; index < logicalDrives.Length; ++index)
        driveInfoArray[index] = new DriveInfo(logicalDrives[index]);
      return driveInfoArray;
    }

    /// <summary>Возвращает корневой каталог диска.</summary>
    /// <returns>Объект, содержащий корневой каталог диска.</returns>
    public DirectoryInfo RootDirectory
    {
      get
      {
        return new DirectoryInfo(this.Name);
      }
    }

    /// <summary>Возвращает или задает метку тома диска.</summary>
    /// <returns>Метка тома.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (для примера, ошибка диска или диск не был готов).
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Диск не сопоставлен или не существует.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Метка тома задается по сети или компакт-диска.
    /// 
    ///   -или-
    /// 
    ///   Отказано в доступе к сведениям о диске.
    /// </exception>
    public string VolumeLabel
    {
      [SecuritySafeCritical] get
      {
        StringBuilder volumeName = new StringBuilder(50);
        StringBuilder fileSystemName = new StringBuilder(50);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          int volSerialNumber;
          int maxFileNameLen;
          int fileSystemFlags;
          if (!Win32Native.GetVolumeInformation(this.Name, volumeName, 50, out volSerialNumber, out maxFileNameLen, out fileSystemFlags, fileSystemName, 50))
          {
            int errorCode = Marshal.GetLastWin32Error();
            if (errorCode == 13)
              errorCode = 15;
            __Error.WinIODriveError(this.Name, errorCode);
          }
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return volumeName.ToString();
      }
      [SecuritySafeCritical] set
      {
        new FileIOPermission(FileIOPermissionAccess.Write, this._name + ".").Demand();
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          if (Win32Native.SetVolumeLabel(this.Name, value))
            return;
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error == 5)
            throw new UnauthorizedAccessException(Environment.GetResourceString("InvalidOperation_SetVolumeLabelFailed"));
          __Error.WinIODriveError(this.Name, lastWin32Error);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
      }
    }

    /// <summary>Возвращает имя диска в виде строки.</summary>
    /// <returns>Имя диска.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_name", (object) this._name, typeof (string));
    }
  }
}
