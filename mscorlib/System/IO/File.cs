// Decompiled with JetBrains decompiler
// Type: System.IO.File
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
  ///   Предоставляет статические методы для создания, копирования, удаления, перемещения и открытия одного файла, а также помогает при создании объектов <see cref="T:System.IO.FileStream" />.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  public static class File
  {
    private const int GetFileExInfoStandard = 0;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_ACCESS_DENIED = 5;

    /// <summary>
    ///   Открывает для чтения существующий файл, содержащий текст в кодировке UTF-8.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <returns>
    ///   <see cref="T:System.IO.StreamReader" /> в заданном пути.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static StreamReader OpenText(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return new StreamReader(path);
    }

    /// <summary>
    ///   Создается или открывается файл для записи текста в кодировке UTF-8.
    /// </summary>
    /// <param name="path">Файл, который нужно открыть для записи.</param>
    /// <returns>
    ///   <see cref="T:System.IO.StreamWriter" />, выполняющий запись в указанный файл в кодировке UTF-8.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static StreamWriter CreateText(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return new StreamWriter(path, false);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.IO.StreamWriter" />, добавляющий текст с кодировкой UTF-8 в существующий файл, или в новый файл, если указанный файл не существует.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу, в который производится добавление.
    /// </param>
    /// <returns>
    ///   Модуль записи потока, который добавляет текст в кодировке UTF-8 в указанный файл или новый файл.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь является недопустимым (например, каталог не существует или находится на несопоставленном диске).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static StreamWriter AppendText(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return new StreamWriter(path, true);
    }

    /// <summary>
    ///   Копирует существующий файл в новый файл.
    ///    Перезапись файла с тем же именем не разрешена.
    /// </summary>
    /// <param name="sourceFileName">Копируемый файл.</param>
    /// <param name="destFileName">
    ///   Имя конечного файла.
    ///    Это не может быть имя каталога или имя существующего файла.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName" /> или <paramref name="destFileName" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> определяет каталог.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   В <paramref name="sourceFileName" /> или <paramref name="destFileName" /> указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="sourceFileName" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="destFileName" /> существует.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет недопустимый формат.
    /// </exception>
    public static void Copy(string sourceFileName, string destFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (sourceFileName));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      File.InternalCopy(sourceFileName, destFileName, false, true);
    }

    /// <summary>
    ///   Копирует существующий файл в новый файл.
    ///    Перезапись файла с тем же именем разрешена.
    /// </summary>
    /// <param name="sourceFileName">Копируемый файл.</param>
    /// <param name="destFileName">
    ///   Имя конечного файла.
    ///    Это не может быть имя каталога.
    /// </param>
    /// <param name="overwrite">
    ///   <see langword="true" />, если конечный файл можно перезаписать; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Объект <paramref name="destFileName" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName" /> или <paramref name="destFileName" /> представляет собой строку нулевой длины, строку, содержащую только пробелы, или строку, содержащую один или несколько недопустимых символов, определенных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> определяет каталог.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   В <paramref name="sourceFileName" /> или <paramref name="destFileName" /> указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="sourceFileName" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="destFileName" /> существует, и <paramref name="overwrite" /> равно <see langword="false" />.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет недопустимый формат.
    /// </exception>
    public static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (sourceFileName));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      File.InternalCopy(sourceFileName, destFileName, overwrite, true);
    }

    [SecurityCritical]
    internal static void UnsafeCopy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (sourceFileName));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      File.InternalCopy(sourceFileName, destFileName, overwrite, false);
    }

    [SecuritySafeCritical]
    internal static string InternalCopy(string sourceFileName, string destFileName, bool overwrite, bool checkHost)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
      if (!Win32Native.CopyFile(fullPathInternal1, fullPathInternal2, !overwrite))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        string maybeFullPath = destFileName;
        if (lastWin32Error != 80)
        {
          using (SafeFileHandle file = Win32Native.UnsafeCreateFile(fullPathInternal1, int.MinValue, FileShare.Read, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 0, IntPtr.Zero))
          {
            if (file.IsInvalid)
              maybeFullPath = sourceFileName;
          }
          if (lastWin32Error == 5 && Directory.InternalExists(fullPathInternal2))
            throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", (object) destFileName), 5, fullPathInternal2);
        }
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      return fullPathInternal2;
    }

    /// <summary>Создает или перезаписывает файл в указанном пути.</summary>
    /// <param name="path">Путь и имя создаваемого файла.</param>
    /// <returns>
    ///   <see cref="T:System.IO.FileStream" />, обеспечивающий доступ для чтения и записи к файлу, указанному в <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода при создании файла.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream Create(string path)
    {
      return File.Create(path, 4096);
    }

    /// <summary>Создает или перезаписывает указанный файл.</summary>
    /// <param name="path">Имя файла</param>
    /// <param name="bufferSize">
    ///   Число байтов, буферизируемых при чтении и записи в данный файл.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.FileStream" /> с заданным размером буфера, который обеспечивает доступ для чтения и записи к файлу, указанному в <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода при создании файла.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream Create(string path, int bufferSize)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
    }

    /// <summary>
    ///   Создает или перезаписывает указанный файл, определяя размер буфера и значение <see cref="T:System.IO.FileOptions" />, которое описывает, как создавать или перезаписывать файл.
    /// </summary>
    /// <param name="path">Имя файла</param>
    /// <param name="bufferSize">
    ///   Число байтов, буферизируемых при чтении и записи в данный файл.
    /// </param>
    /// <param name="options">
    ///   Одно из значений <see cref="T:System.IO.FileOptions" />, которое описывает, как создавать или перезаписывать файл.
    /// </param>
    /// <returns>Новый файл с заданным размером буфера.</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="options" /> указан режим <see cref="F:System.IO.FileOptions.Encrypted" />, а на текущей платформе шифрование файлов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода при создании файла.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
    }

    /// <summary>
    ///   Создает или перезаписывает указанный файл с заданным размером буфера, параметрами файла и уровнем безопасности.
    /// </summary>
    /// <param name="path">Имя файла</param>
    /// <param name="bufferSize">
    ///   Число байтов, буферизируемых при чтении и записи в данный файл.
    /// </param>
    /// <param name="options">
    ///   Одно из значений <see cref="T:System.IO.FileOptions" />, которое описывает, как создавать или перезаписывать файл.
    /// </param>
    /// <param name="fileSecurity">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.FileSecurity" />, определяющих управление доступом и аудит безопасности для файла.
    /// </param>
    /// <returns>
    ///   Новый файл с заданным размером буфера, параметрами файла и уровнем безопасности.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="options" /> указан режим <see cref="F:System.IO.FileOptions.Encrypted" />, а на текущей платформе шифрование файлов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода при создании файла.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// </exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
    {
      return new FileStream(path, FileMode.Create, FileSystemRights.Read | FileSystemRights.Write, FileShare.None, bufferSize, options, fileSecurity);
    }

    /// <summary>Удаляет указанный файл.</summary>
    /// <param name="path">
    ///   Имя файла, предназначенного для удаления.
    ///    Знаки подстановки не поддерживаются.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Указанный файл используется.
    /// 
    ///   -или-
    /// 
    ///   Для файла имеется открытый дескриптор, а операционной системой является Windows XP или более ранней версии.
    ///    Этот открытый дескриптор может быть результатом перечисления каталогов и файлов.
    ///    Дополнительные сведения см. в разделе Практическое руководство. Перечисление каталогов и файлов.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Файл является исполняемым файлом, который уже используется.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    public static void Delete(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      File.InternalDelete(path, true);
    }

    [SecurityCritical]
    internal static void UnsafeDelete(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      File.InternalDelete(path, false);
    }

    [SecurityCritical]
    internal static void InternalDelete(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.DeleteFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    ///   Расшифровывает файл, зашифрованный текущей учетной записью с помощью метода <see cref="M:System.IO.File.Encrypt(System.String)" />.
    /// </summary>
    /// <param name="path">
    ///   Путь, описывающий файл, который нужно расшифровать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описанный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    ///    Например зашифрованный файл уже открыт.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Файловая система не является системой NTFS.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void Decrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.DecryptFile(fullPathInternal, 0))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 5 && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    ///   Шифрует файл таким образом, чтобы его можно было расшифровать только с помощью учетной записи, которая использовалась для шифрования.
    /// </summary>
    /// <param name="path">
    ///   Путь, описывающий файл, который нужно зашифровать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описанный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Файловая система не является системой NTFS.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void Encrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.EncryptFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 5 && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>Определяет, существует ли заданный файл.</summary>
    /// <param name="path">Проверяемый файл.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если вызывающий оператор имеет требуемые разрешения и <paramref name="path" /> содержит имя существующего файла; в противном случае — <see langword="false" />.
    ///    Этот метод также возвращает <see langword="false" />, если <paramref name="path" /> — <see langword="null" />, недействительный путь или строка нулевой длины.
    ///    Если у вызывающего оператора нет достаточных полномочий на чтение заданного файла, исключения не создаются, а данный метод возвращает <see langword="false" /> вне зависимости от существования <paramref name="path" />.
    /// </returns>
    [SecuritySafeCritical]
    public static bool Exists(string path)
    {
      return File.InternalExistsHelper(path, true);
    }

    [SecurityCritical]
    internal static bool UnsafeExists(string path)
    {
      return File.InternalExistsHelper(path, false);
    }

    [SecurityCritical]
    private static bool InternalExistsHelper(string path, bool checkHost)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        path = Path.GetFullPathInternal(path);
        if (path.Length > 0 && Path.IsDirectorySeparator(path[path.Length - 1]))
          return false;
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, path, false, false);
        return File.InternalExists(path);
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

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      if (File.FillAttributeInfo(path, ref data, false, true) == 0 && data.fileAttributes != -1)
        return (data.fileAttributes & 16) == 0;
      return false;
    }

    /// <summary>
    ///   Открывает объект <see cref="T:System.IO.FileStream" /> по указанному пути с доступом для чтения и записи.
    /// </summary>
    /// <param name="path">Открываемый файл.</param>
    /// <param name="mode">
    ///   Значение <see cref="T:System.IO.FileMode" /> указывает, нужно ли создавать файл, если он не существует, и определяет, будет ли содержимое существующих файлов сохранено или перезаписано.
    /// </param>
    /// <returns>
    ///   Поток <see cref="T:System.IO.FileStream" />, открытый в указанном режиме и по указанному пути с доступом для чтения и записи и без предоставления общего доступа.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Create" />, а указанный файл является скрытым.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="mode" /> задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream Open(string path, FileMode mode)
    {
      return File.Open(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
    }

    /// <summary>
    ///   Открывает <see cref="T:System.IO.FileStream" /> в заданном пути с заданным режимом и доступом.
    /// </summary>
    /// <param name="path">Открываемый файл.</param>
    /// <param name="mode">
    ///   Значение <see cref="T:System.IO.FileMode" /> указывает, нужно ли создавать файл, если он не существует, и определяет, будет ли содержимое существующих файлов сохранено или перезаписано.
    /// </param>
    /// <param name="access">
    ///   Значение <see cref="T:System.IO.FileAccess" />, описывающее операции, которые можно выполнять с файлом.
    /// </param>
    /// <returns>
    ///   Объект с монопольным доступом <see cref="T:System.IO.FileStream" />, обеспечивающий доступ к указанному файлу с заданным режимом и доступом.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> указывает <see langword="Read" />, и <paramref name="mode" /> указывает <see langword="Create" />, <see langword="CreateNew" />, <see langword="Truncate" /> или <see langword="Append" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> указывает файл, доступный только для чтения, а параметр <paramref name="access" /> не равен <see langword="Read" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Create" />, а указанный файл является скрытым.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="mode" /> или <paramref name="access" /> задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream Open(string path, FileMode mode, FileAccess access)
    {
      return File.Open(path, mode, access, FileShare.None);
    }

    /// <summary>
    ///   Открывает <see cref="T:System.IO.FileStream" /> в заданном пути, с заданным режимом доступа для чтения, записи или чтения и записи и с заданным параметром совместного использования.
    /// </summary>
    /// <param name="path">Открываемый файл.</param>
    /// <param name="mode">
    ///   Значение <see cref="T:System.IO.FileMode" /> указывает, нужно ли создавать файл, если он не существует, и определяет, будет ли содержимое существующих файлов сохранено или перезаписано.
    /// </param>
    /// <param name="access">
    ///   Значение <see cref="T:System.IO.FileAccess" />, описывающее операции, которые можно выполнять с файлом.
    /// </param>
    /// <param name="share">
    ///   Значение <see cref="T:System.IO.FileShare" />, задающее тип доступа к файлу других потоков.
    /// </param>
    /// <returns>
    ///   Поток <see cref="T:System.IO.FileStream" /> по указанному пути с указанным режимом доступа для чтения, записи или чтения и записи и с указанным параметром совместного доступа.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> указывает <see langword="Read" />, и <paramref name="mode" /> указывает <see langword="Create" />, <see langword="CreateNew" />, <see langword="Truncate" /> или <see langword="Append" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <paramref name="path" /> указывает файл, доступный только для чтения, а параметр <paramref name="access" /> не равен <see langword="Read" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Create" />, а указанный файл является скрытым.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
      return new FileStream(path, mode, access, share);
    }

    /// <summary>Устанавливает дату и время создания файла.</summary>
    /// <param name="path">
    ///   Файл, для которого задаются сведения о дате и времени создания.
    /// </param>
    /// <param name="creationTime">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени создания <paramref name="path" />.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При выполнении операции произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="creationTime" /> указывает значение вне диапазона дат или времени, разрешенного для этой операции.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      File.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время создания файла, представленные в формате общего скоординированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл, для которого задаются сведения о дате и времени создания.
    /// </param>
    /// <param name="creationTimeUtc">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени создания <paramref name="path" />.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </param>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а длина имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При выполнении операции произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="creationTime" /> указывает значение вне диапазона дат или времени, разрешенного для этой операции.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, &fileTime, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    ///   Возвращает дату и время создания заданного файла или каталога.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого получены сведения о дате и времени создания.
    /// </param>
    /// <returns>
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время создания указанного файла или каталога.
    ///    Значение представляется в формате местного времени.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetCreationTime(string path)
    {
      return File.InternalGetCreationTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    ///   Возвращает дату и время создания заданного файла или каталога в формате общего скоординированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого получены сведения о дате и времени создания.
    /// </param>
    /// <returns>
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время создания указанного файла или каталога.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetCreationTimeUtc(string path)
    {
      return File.InternalGetCreationTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetCreationTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc(data.ftCreationTime.ToTicks());
    }

    /// <summary>
    ///   Устанавливаются дата и время последнего доступа к заданному файлу.
    /// </summary>
    /// <param name="path">
    ///   Файл, для которого устанавливаются сведения о дате и времени доступа.
    /// </param>
    /// <param name="lastAccessTime">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени последнего доступа к <paramref name="path" />.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastAccessTime" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      File.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время последнего доступа к заданному файлу в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл, для которого устанавливаются сведения о дате и времени доступа.
    /// </param>
    /// <param name="lastAccessTimeUtc">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени последнего доступа к <paramref name="path" />.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastAccessTimeUtc" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, &fileTime, (Win32Native.FILE_TIME*) null))
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
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время последнего доступа к указанному файлу или каталогу.
    ///    Значение представляется в формате местного времени.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetLastAccessTime(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    ///   Возвращает дату и время последнего доступа к заданному файлу или каталогу в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, информацию о дате и времени обращения к которому следует получить.
    /// </param>
    /// <returns>
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время последнего доступа к указанному файлу или каталогу.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastAccessTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc(data.ftLastAccessTime.ToTicks());
    }

    /// <summary>
    ///   Устанавливаются дата и время последней операции записи в заданный файл.
    /// </summary>
    /// <param name="path">
    ///   Файл, для которого устанавливаются сведения о дате и времени.
    /// </param>
    /// <param name="lastWriteTime">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени последней записи <paramref name="path" />.
    ///    Значение представляется в формате местного времени.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastWriteTime" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      File.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
    }

    /// <summary>
    ///   Устанавливает дату и время последней операции записи в заданный файл в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл, для которого устанавливаются сведения о дате и времени.
    /// </param>
    /// <param name="lastWriteTimeUtc">
    ///   Объект <see cref="T:System.DateTime" />, содержащий значение, которое должно быть задано для даты и времени последней записи <paramref name="path" />.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Указанный путь не найден.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="lastWriteTimeUtc" /> указывает значение, выходящее за диапазон дат или времени, разрешенных для этой операции.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null, &fileTime))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    ///   Возвращает время и дату последней операции записи в указанный файл или каталог.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого должны быть получены сведения о дате и времени записи.
    /// </param>
    /// <returns>
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время последней операции записи в указанный файл или каталог.
    ///    Значение представляется в формате местного времени.
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetLastWriteTime(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    ///   Возвращает дату и время последней операции записи в заданный файл или каталог в формате всемирного координированного времени (UTC).
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которого должны быть получены сведения о дате и времени записи.
    /// </param>
    /// <returns>
    ///   Структура <see cref="T:System.DateTime" />, для которой заданы дата и время последней операции записи в указанный файл или каталог.
    ///    Значение выражено в формате всемирного координированного времени (UTC).
    /// </returns>
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
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    [SecuritySafeCritical]
    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastWriteTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc(data.ftLastWriteTime.ToTicks());
    }

    /// <summary>
    ///   Получает значение <see cref="T:System.IO.FileAttributes" /> для файла в пути.
    /// </summary>
    /// <param name="path">Путь к файлу.</param>
    /// <returns>
    ///   Значение <see cref="T:System.IO.FileAttributes" /> для файла в пути.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> является пустой строкой, содержит только пробелы или содержит недопустимые символы.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="path" /> представляет файл и является недопустимым, например указывает на несопоставленный диск, или же невозможно найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   <paramref name="path" /> представляет каталог и является недопустимым, например указывает на несопоставленный диск, или же невозможно найти каталог.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Этот файл используется другим процессом.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static FileAttributes GetAttributes(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, true);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return (FileAttributes) data.fileAttributes;
    }

    /// <summary>
    ///   Устанавливает заданные атрибуты <see cref="T:System.IO.FileAttributes" /> файла по заданному пути.
    /// </summary>
    /// <param name="path">Путь к файлу.</param>
    /// <param name="fileAttributes">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> является пустым, содержит только пробелы, содержит недопустимые символы, или же атрибут файла недействителен.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetAttributes(string path, FileAttributes fileAttributes)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.SetFileAttributes(fullPathInternal, (int) fileAttributes))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 87)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует записи списка ACL для заданного файла.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу, содержащему объект <see cref="T:System.Security.AccessControl.FileSecurity" />, описывающий сведения о списке управления доступом (ACL) для конкретного файла.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует правила управления доступом для файла, описанные параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static FileSecurity GetAccessControl(string path)
    {
      return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.FileSecurity" />, инкапсулирующий записи списка ACL определенного типа для конкретного файла.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу, содержащему объект <see cref="T:System.Security.AccessControl.FileSecurity" />, описывающий сведения о списке управления доступом (ACL) для конкретного файла.
    /// </param>
    /// <param name="includeSections">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.AccessControlSections" />, указывающее тип сведений о списке ACL, которые необходимо получить.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует правила управления доступом для файла, описанные параметром <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
    {
      return new FileSecurity(path, includeSections);
    }

    /// <summary>
    ///   Применяет записи списка управления доступом (ACL), описанные объектом <see cref="T:System.Security.AccessControl.FileSecurity" />, к заданному файлу.
    /// </summary>
    /// <param name="path">
    ///   Файл, в который нужно добавить или из которого нужно удалить записи списка управления доступом.
    /// </param>
    /// <param name="fileSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.FileSecurity" />, характеризующий запись ACL, которую необходимо применить к файлу, описанному параметром <paramref name="path" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileSecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetAccessControl(string path, FileSecurity fileSecurity)
    {
      if (fileSecurity == null)
        throw new ArgumentNullException(nameof (fileSecurity));
      string fullPathInternal = Path.GetFullPathInternal(path);
      fileSecurity.Persist(fullPathInternal);
    }

    /// <summary>Открывает для чтения существующий файл.</summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <returns>
    ///   Доступный только для чтения <see cref="T:System.IO.FileStream" /> в заданном пути.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    public static FileStream OpenRead(string path)
    {
      return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>
    ///   Открывает существующий файл или создает новый файл для записи.
    /// </summary>
    /// <param name="path">Файл, который нужно открыть для записи.</param>
    /// <returns>
    ///   Объект с монопольным доступом <see cref="T:System.IO.FileStream" /> в заданном пути с доступом <see cref="F:System.IO.FileAccess.Write" />.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> указывает файл или каталог только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    public static FileStream OpenWrite(string path)
    {
      return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    /// <summary>
    ///   Открывает текстовый файл, считывает все строки файла и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <returns>Строка, содержащая все строки файла.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static string ReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, true);
    }

    /// <summary>
    ///   Открывает файл, считывает все строки файла с заданной кодировкой и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <param name="encoding">
    ///   Кодировка, примененная к содержимому файла.
    /// </param>
    /// <returns>Строка, содержащая все строки файла.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static string ReadAllText(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, encoding, true);
    }

    [SecurityCritical]
    internal static string UnsafeReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, false);
    }

    [SecurityCritical]
    private static string InternalReadAllText(string path, Encoding encoding, bool checkHost)
    {
      using (StreamReader streamReader = new StreamReader(path, encoding, true, StreamReader.DefaultBufferSize, checkHost))
        return streamReader.ReadToEnd();
    }

    /// <summary>
    ///   Создает новый файл, записывает в него указанную строку и затем закрывает файл.
    ///    Если целевой файл уже существует, он будет переопределен.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">
    ///   Строка, которую нужно записать в файл.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="path" /> равняется <see langword="null" />, или <paramref name="contents" /> является пустым.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, true);
    }

    /// <summary>
    ///   Создает новый файл, записывает указанную строку в этот файл, используя заданную кодировку, и затем закрывает файл.
    ///    Если целевой файл уже существует, он будет переопределен.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">
    ///   Строка, которую нужно записать в файл.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка, которую необходимо применить к строке.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="path" /> равняется <see langword="null" />, или <paramref name="contents" /> является пустым.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, encoding, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, false);
    }

    [SecurityCritical]
    private static void InternalWriteAllText(string path, string contents, Encoding encoding, bool checkHost)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024, checkHost))
        streamWriter.Write(contents);
    }

    /// <summary>
    ///   Открывает двоичный файл, считывает содержимое файла в массив байтов и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <returns>Массив байтов с содержимым файла.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static byte[] ReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, true);
    }

    [SecurityCritical]
    internal static byte[] UnsafeReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, false);
    }

    [SecurityCritical]
    private static byte[] InternalReadAllBytes(string path, bool checkHost)
    {
      byte[] buffer;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
      {
        int offset = 0;
        long length = fileStream.Length;
        if (length > (long) int.MaxValue)
          throw new IOException(Environment.GetResourceString("IO.IO_FileTooLong2GB"));
        int count = (int) length;
        buffer = new byte[count];
        while (count > 0)
        {
          int num = fileStream.Read(buffer, offset, count);
          if (num == 0)
            __Error.EndOfFile();
          offset += num;
          count -= num;
        }
      }
      return buffer;
    }

    /// <summary>
    ///   Создает новый файл, записывает в него указанный массив байтов и затем закрывает файл.
    ///    Если целевой файл уже существует, он будет переопределен.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="bytes">Байты, которые нужно записать в файл.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />, или массив байтов является пустым.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public static void WriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path), Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes));
      File.InternalWriteAllBytes(path, bytes, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path), Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes));
      File.InternalWriteAllBytes(path, bytes, false);
    }

    [SecurityCritical]
    private static void InternalWriteAllBytes(string path, byte[] bytes, bool checkHost)
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
        fileStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   Открывает текстовый файл, считывает все строки файла и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <returns>Массив строк, содержащий все строки файла.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static string[] ReadAllLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, Encoding.UTF8);
    }

    /// <summary>
    ///   Открывает файл, считывает все строки файла с заданной кодировкой и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, открываемый для чтения.</param>
    /// <param name="encoding">
    ///   Кодировка, примененная к содержимому файла.
    /// </param>
    /// <returns>Массив строк, содержащий все строки файла.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static string[] ReadAllLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, encoding);
    }

    private static string[] InternalReadAllLines(string path, Encoding encoding)
    {
      List<string> stringList = new List<string>();
      using (StreamReader streamReader = new StreamReader(path, encoding))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
          stringList.Add(str);
      }
      return stringList.ToArray();
    }

    /// <summary>Считывает строки файла.</summary>
    /// <param name="path">Файл, который нужно прочитать.</param>
    /// <returns>
    ///   Все строки файла или строки, которые являются результатом запроса.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую хотя бы один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" /> (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не обнаружен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> ReadLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (path));
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
    }

    /// <summary>Считывает строки файла с заданной кодировкой.</summary>
    /// <param name="path">Файл, который нужно прочитать.</param>
    /// <param name="encoding">
    ///   Кодировка, примененная к содержимому файла.
    /// </param>
    /// <returns>
    ///   Все строки файла или строки, которые являются результатом запроса.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую хотя бы один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" /> (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не обнаружен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static IEnumerable<string> ReadLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (path));
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, encoding);
    }

    /// <summary>
    ///   Создает новый файл, записывает в него указанный массив строк и затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">
    ///   Массив строк, который нужно записать в файл.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Либо <paramref name="path" />, либо <paramref name="contents" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void WriteAllLines(string path, string[] contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), (IEnumerable<string>) contents);
    }

    /// <summary>
    ///   Создает новый файл, записывает указанный массив строк в этот файл, используя заданную кодировку, затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">
    ///   Массив строк, который нужно записать в файл.
    /// </param>
    /// <param name="encoding">
    ///   Объект <see cref="T:System.Text.Encoding" />, представляющий кодировку символов, примененную к массиву строк.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Либо <paramref name="path" />, либо <paramref name="contents" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void WriteAllLines(string path, string[] contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), (IEnumerable<string>) contents);
    }

    /// <summary>
    ///   Создает новый файл, записывает в него коллекцию строк, затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">Строки, записываемые в файл.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую хотя бы один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name=" path " /> или <paramref name="contents" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" /> (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>
    ///   Создает новый файл, используя указанную кодировку, записывает коллекцию строк в этот файл, затем закрывает файл.
    /// </summary>
    /// <param name="path">Файл, в который осуществляется запись.</param>
    /// <param name="contents">Строки, записываемые в файл.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую хотя бы один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name=" path" />, <paramref name=" contents" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Недопустимый <paramref name="path" /> (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), contents);
    }

    private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
    {
      using (writer)
      {
        foreach (string content in contents)
          writer.WriteLine(content);
      }
    }

    /// <summary>
    ///   Открывает файл, добавляет в него указанную строку и затем закрывает файл.
    ///    Если файл не существует, этот метод создает файл, записывает в него указанную строку и затем закрывает файл.
    /// </summary>
    /// <param name="path">
    ///   Файл, в который нужно добавить заданную строку.
    /// </param>
    /// <param name="contents">
    ///   Строка, которую нужно добавить в файл.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь является недопустимым (например, каталог не существует или находится на несопоставленном диске).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void AppendAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, StreamWriter.UTF8NoBOM);
    }

    /// <summary>
    ///   Добавляет указанную строку в файл, создавая файл, если он не существует.
    /// </summary>
    /// <param name="path">
    ///   Файл, в который нужно добавить заданную строку.
    /// </param>
    /// <param name="contents">
    ///   Строка, которую нужно добавить в файл.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую один или несколько недопустимых символов, заданных методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указанный путь является недопустимым (например, каталог не существует или находится на несопоставленном диске).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> определяет каталог.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void AppendAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, encoding);
    }

    private static void InternalAppendAllText(string path, string contents, Encoding encoding)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
        streamWriter.Write(contents);
    }

    /// <summary>
    ///   Добавляет строки в файл, затем закрывает файл.
    ///    Если указанный файл не существует, этот метод создает файл, записывает в него указанные строки и затем закрывает файл.
    /// </summary>
    /// <param name="path">
    ///   Файл, в который добавляются строки.
    ///    Если файл не существует, он создается.
    /// </param>
    /// <param name="contents">Строки, добавляемые в файл.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или еще один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name=" path " /> или <paramref name="contents" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Параметр <paramref name="path" /> является недопустимым (например, каталог не существует или указан несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не обнаружен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешения на запись в файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// </exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>
    ///   Добавляет строки в файл, используя заданную кодировку, затем закрывает файл.
    ///    Если указанный файл не существует, этот метод создает файл, записывает в него указанные строки и затем закрывает файл.
    /// </summary>
    /// <param name="path">
    ///   Файл, в который добавляются строки.
    ///    Если файл не существует, он создается.
    /// </param>
    /// <param name="contents">Строки, добавляемые в файл.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или еще один недопустимый символ, заданный методом <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Какой-либо из параметров <paramref name=" path" />, <paramref name="contents" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Параметр <paramref name="path" /> является недопустимым (например, каталог не существует или указан несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, заданный параметром <paramref name="path" />, не обнаружен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина параметра <paramref name="path" /> превышает максимальную длину, определенную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> задан в недопустимом формате.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="path" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> является каталогом.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (contents == null)
        throw new ArgumentNullException(nameof (contents));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, encoding), contents);
    }

    /// <summary>
    ///   Перемещает заданный файл в новое местоположение и разрешает переименование файла.
    /// </summary>
    /// <param name="sourceFileName">
    ///   Имя перемещаемого файла.
    ///    Может содержать относительный или абсолютный путь.
    /// </param>
    /// <param name="destFileName">Новый путь к файлу и его имя.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Конечный файл уже существует.
    /// 
    ///   -или-
    /// 
    ///   Не удалось найти <paramref name="sourceFileName" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName" /> или <paramref name="destFileName" /> представляет собой строку нулевой длины, содержащую только пробелы или содержащую недопустимые символы, заданные методом <see cref="F:System.IO.Path.InvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или они оба превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имена файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   В <paramref name="sourceFileName" /> или <paramref name="destFileName" /> указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destFileName" /> имеет недопустимый формат.
    /// </exception>
    [SecuritySafeCritical]
    public static void Move(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, true);
    }

    [SecurityCritical]
    internal static void UnsafeMove(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, false);
    }

    [SecurityCritical]
    private static void InternalMove(string sourceFileName, string destFileName, bool checkHost)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException(nameof (destFileName), Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (sourceFileName));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (destFileName));
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
      if (!File.InternalExists(fullPathInternal1))
        __Error.WinIOError(2, fullPathInternal1);
      if (Win32Native.MoveFile(fullPathInternal1, fullPathInternal2))
        return;
      __Error.WinIOError();
    }

    /// <summary>
    ///   Заменяет содержимое заданного файла на содержимое другого файла, удаляя исходный файл и создавая резервную копию замененного файла.
    /// </summary>
    /// <param name="sourceFileName">
    ///   Имя файла, который заменяет файл, указанный <paramref name="destinationFileName" />.
    /// </param>
    /// <param name="destinationFileName">Имя заменяемого файла.</param>
    /// <param name="destinationBackupFileName">
    ///   Имя файла резервной копии.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Путь, описываемый параметром <paramref name="destinationFileName" />, имел недопустимую форму.
    /// 
    ///   -или-
    /// 
    ///   Путь, описываемый параметром <paramref name="destinationBackupFileName" />, имел недопустимую форму.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="destinationFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описываемый текущим объектом <see cref="T:System.IO.FileInfo" />, не найден.
    /// 
    ///   -или-
    /// 
    ///   Файл, описываемый параметром <paramref name="destinationBackupFileName" />, не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="sourceFileName" /> и <paramref name="destinationFileName" /> указывают один и тот же файл.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Операционной системой является Windows 98 Second Edition или более ранних версий, и файловая система отлична от NTFS.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destinationFileName" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Исходные или конечные параметры определяют каталог вместо файла.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, false);
    }

    /// <summary>
    ///   Заменяет содержимое заданного файла на содержимое другого файла, удаляя исходный файл и создавая резервную копию замененного файла, и при необходимости игнорирует ошибки слияния.
    /// </summary>
    /// <param name="sourceFileName">
    ///   Имя файла, который заменяет файл, указанный <paramref name="destinationFileName" />.
    /// </param>
    /// <param name="destinationFileName">Имя заменяемого файла.</param>
    /// <param name="destinationBackupFileName">
    ///   Имя файла резервной копии.
    /// </param>
    /// <param name="ignoreMetadataErrors">
    ///   Значение <see langword="true" /> позволяет игнорировать ошибки слияния (например, атрибуты и списки управления доступом (ACL)), исходящие из заменяемого файла в заменяющий; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Путь, описываемый параметром <paramref name="destinationFileName" />, имел недопустимую форму.
    /// 
    ///   -или-
    /// 
    ///   Путь, описываемый параметром <paramref name="destinationBackupFileName" />, имел недопустимую форму.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="destinationFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">
    ///   Указан недопустимый диск.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл, описываемый текущим объектом <see cref="T:System.IO.FileInfo" />, не найден.
    /// 
    ///   -или-
    /// 
    ///   Файл, описываемый параметром <paramref name="destinationBackupFileName" />, не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="sourceFileName" /> и <paramref name="destinationFileName" /> указывают один и тот же файл.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Операционной системой является Windows 98 Second Edition или более ранних версий, и файловая система отлична от NTFS.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Параметр <paramref name="sourceFileName" /> или <paramref name="destinationFileName" /> указывает файл, доступный только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Эта операция не поддерживается на текущей платформе.
    /// 
    ///   -или-
    /// 
    ///   Исходные или конечные параметры определяют каталог вместо файла.
    /// 
    ///   -или-
    /// 
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
    }

    [SecuritySafeCritical]
    private static void InternalReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destinationFileName);
      string str = (string) null;
      if (destinationBackupFileName != null)
        str = Path.GetFullPathInternal(destinationBackupFileName);
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal1);
        FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal2);
        if (str != null)
          FileIOPermission.EmulateFileIOPermissionChecks(str);
      }
      else
      {
        FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[2]
        {
          fullPathInternal1,
          fullPathInternal2
        });
        if (str != null)
          fileIoPermission.AddPathList(FileIOPermissionAccess.Write, str);
        fileIoPermission.Demand();
      }
      int dwReplaceFlags = 1;
      if (ignoreMetadataErrors)
        dwReplaceFlags |= 2;
      if (Win32Native.ReplaceFile(fullPathInternal2, fullPathInternal1, str, dwReplaceFlags, IntPtr.Zero, IntPtr.Zero))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    internal static int FillAttributeInfo(string path, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
    {
      int num = 0;
      if (tryagain)
      {
        Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
        string fileName = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          bool flag = false;
          SafeFindHandle firstFile = Win32Native.FindFirstFile(fileName, ref wiN32FindData);
          try
          {
            if (firstFile.IsInvalid)
            {
              flag = true;
              num = Marshal.GetLastWin32Error();
              switch (num)
              {
                case 2:
                case 3:
                case 21:
                  if (!returnErrorOnNotFound)
                  {
                    num = 0;
                    data.fileAttributes = -1;
                    break;
                  }
                  break;
              }
              return num;
            }
          }
          finally
          {
            try
            {
              firstFile.Close();
            }
            catch
            {
              if (!flag)
                __Error.WinIOError();
            }
          }
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        data.PopulateFrom(ref wiN32FindData);
      }
      else
      {
        bool flag = false;
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          flag = Win32Native.GetFileAttributesEx(path, 0, ref data);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        if (!flag)
        {
          num = Marshal.GetLastWin32Error();
          switch (num)
          {
            case 2:
            case 3:
            case 21:
              if (!returnErrorOnNotFound)
              {
                num = 0;
                data.fileAttributes = -1;
                break;
              }
              break;
            default:
              return File.FillAttributeInfo(path, ref data, true, returnErrorOnNotFound);
          }
        }
      }
      return num;
    }

    [SecurityCritical]
    private static FileStream OpenFile(string path, FileAccess access, out SafeFileHandle handle)
    {
      FileStream fileStream = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite, 1);
      handle = fileStream.SafeFileHandle;
      if (handle.IsInvalid)
      {
        int errorCode = Marshal.GetLastWin32Error();
        string fullPathInternal = Path.GetFullPathInternal(path);
        if (errorCode == 3 && fullPathInternal.Equals(Directory.GetDirectoryRoot(fullPathInternal)))
          errorCode = 5;
        __Error.WinIOError(errorCode, path);
      }
      return fileStream;
    }
  }
}
