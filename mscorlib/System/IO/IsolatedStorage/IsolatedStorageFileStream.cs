// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageFileStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
  /// <summary>Представляет файл в изолированном хранилище.</summary>
  [ComVisible(true)]
  public class IsolatedStorageFileStream : FileStream
  {
    private const int s_BlockSize = 1024;
    private const string s_BackSlash = "\\";
    private FileStream m_fs;
    private IsolatedStorageFile m_isf;
    private string m_GivenPath;
    private string m_FullPath;
    private bool m_OwnedStore;

    private IsolatedStorageFileStream()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог в <paramref name="path" /> не существует.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode)
      : this(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, (IsolatedStorageFile) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" />, а также в контексте класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, заданного с помощью <paramref name="isf" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="isf">
    ///   Объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, в котором необходимо открыть <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   <paramref name="isf" /> не имеет квоты.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf)
      : this(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, isf)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с типом запрошенного параметра <paramref name="access" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access)
      : this(path, mode, access, access == FileAccess.Read ? FileShare.Read : FileShare.None, 4096, (IsolatedStorageFile) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с заданным параметром <paramref name="access" /> файла, а также в контексте класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, заданного с помощью параметра <paramref name="isf" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <param name="isf">
    ///   Объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, в котором необходимо открыть <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   <paramref name="isf" /> не имеет квоты.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
      : this(path, mode, access, access == FileAccess.Read ? FileShare.Read : FileShare.None, 4096, isf)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с заданным для файла <paramref name="access" />, используя режим общего доступа к файлу, заданный с помощью параметра <paramref name="share" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <param name="share">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileShare" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share)
      : this(path, mode, access, share, 4096, (IsolatedStorageFile) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с заданным параметром <paramref name="access" /> файла, используя режим общего доступа к файлу, заданный с помощью параметра <paramref name="share" />, а также в контексте класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, заданного с помощью параметра <paramref name="isf" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <param name="share">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileShare" />.
    /// </param>
    /// <param name="isf">
    ///   Объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, в котором необходимо открыть <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   <paramref name="isf" /> не имеет квоты.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
      : this(path, mode, access, share, 4096, isf)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с заданным для файла <paramref name="access" />, используя режим общего доступа к файлу, заданный с помощью параметра <paramref name="share" /> с указанным <paramref name="buffersize" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <param name="share">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileShare" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер буфера <see cref="T:System.IO.FileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
      : this(path, mode, access, share, bufferSize, (IsolatedStorageFile) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, предоставляющего доступ к файлу, назначенному с помощью параметра <paramref name="path" /> в указанном <paramref name="mode" /> с заданным параметром <paramref name="access" /> файла, используя режим общего доступа к файлу, заданный с помощью параметра <paramref name="share" />, с заданным размером буфера <paramref name="buffersize" />, а также в контексте класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, заданного с помощью параметра <paramref name="isf" />.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.IO.FileMode" />.
    /// </param>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <param name="share">
    ///   Битовая комбинация значений <see cref="T:System.IO.FileShare" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер буфера <see cref="T:System.IO.FileStream" />.
    /// </param>
    /// <param name="isf">
    ///   Объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />, в котором необходимо открыть <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> неправильно сформирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="path" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   <paramref name="isf" /> не имеет квоты.
    /// </exception>
    [SecuritySafeCritical]
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Length == 0 || path.Equals("\\"))
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
      if (isf == null)
      {
        this.m_OwnedStore = true;
        isf = IsolatedStorageFile.GetUserStoreForDomain();
      }
      if (isf.Disposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      switch (mode)
      {
        case FileMode.CreateNew:
        case FileMode.Create:
        case FileMode.Open:
        case FileMode.OpenOrCreate:
        case FileMode.Truncate:
        case FileMode.Append:
          this.m_isf = isf;
          FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
          fileIoPermission.Assert();
          fileIoPermission.PermitOnly();
          this.m_GivenPath = path;
          this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
          ulong num = 0;
          bool flag = false;
          bool locked = false;
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            switch (mode)
            {
              case FileMode.CreateNew:
                flag = true;
                break;
              case FileMode.Create:
              case FileMode.OpenOrCreate:
              case FileMode.Truncate:
              case FileMode.Append:
                this.m_isf.Lock(ref locked);
                try
                {
                  num = IsolatedStorageFile.RoundToBlockSize((ulong) LongPathFile.GetLength(this.m_FullPath));
                  break;
                }
                catch (FileNotFoundException ex)
                {
                  flag = true;
                  break;
                }
                catch
                {
                  break;
                }
            }
            if (flag)
              this.m_isf.ReserveOneBlock();
            try
            {
              this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true, true);
            }
            catch
            {
              if (flag)
                this.m_isf.UnreserveOneBlock();
              throw;
            }
            if (!flag)
            {
              if (mode != FileMode.Truncate)
              {
                if (mode != FileMode.Create)
                  goto label_34;
              }
              ulong blockSize = IsolatedStorageFile.RoundToBlockSize((ulong) this.m_fs.Length);
              if (num > blockSize)
                this.m_isf.Unreserve(num - blockSize);
              else if (blockSize > num)
                this.m_isf.Reserve(blockSize - num);
            }
          }
          finally
          {
            if (locked)
              this.m_isf.Unlock();
          }
label_34:
          CodeAccessPermission.RevertAll();
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, показывающее возможность чтения файла.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможно чтение объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanRead
    {
      get
      {
        return this.m_fs.CanRead;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, возможна ли запись в файл.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможна запись в объекте <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanWrite
    {
      get
      {
        return this.m_fs.CanWrite;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, поддерживаются ли операции поиска.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> поддерживает операции поиска; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanSeek
    {
      get
      {
        return this.m_fs.CanSeek;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, показывающее, как был открыт объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> — синхронно или асинхронно.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> поддерживает асинхронный доступ; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsAsync
    {
      get
      {
        return this.m_fs.IsAsync;
      }
    }

    /// <summary>
    ///   Возвращает длину объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </summary>
    /// <returns>
    ///   Длина объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> в байтах.
    /// </returns>
    public override long Length
    {
      get
      {
        return this.m_fs.Length;
      }
    }

    /// <summary>
    ///   Возвращает или задает текущую позицию текущего объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </summary>
    /// <returns>
    ///   Текущее положение объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Позиции не может быть присвоено отрицательное значение.
    /// </exception>
    public override long Position
    {
      get
      {
        return this.m_fs.Position;
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        this.Seek(value, SeekOrigin.Begin);
      }
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> для освобождения как управляемых, так и неуправляемых ресурсов; значение <see langword="false" /> для освобождения только неуправляемых ресурсов.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        try
        {
          if (this.m_fs == null)
            return;
          this.m_fs.Close();
        }
        finally
        {
          if (this.m_OwnedStore && this.m_isf != null)
            this.m_isf.Close();
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    /// <summary>
    ///   Очищает буферы для этого потока и вызывает запись всех буферизованных данных в файл.
    /// </summary>
    public override void Flush()
    {
      this.m_fs.Flush();
    }

    /// <summary>
    ///   Очищает буферы для этого потока и вызывает запись всех буферизованных данных в файл, а также очищает все буферы промежуточных файлов.
    /// </summary>
    /// <param name="flushToDisk">
    ///   Значение <see langword="true" /> для записи на диск  буферов всех промежуточных файлов; в противном случае — значение <see langword="false" />.
    /// </param>
    public override void Flush(bool flushToDisk)
    {
      this.m_fs.Flush(flushToDisk);
    }

    /// <summary>
    ///   Возвращает дескриптор файла для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    ///    Доступ к этому свойству для объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> не разрешен. Создается исключение <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" />.
    /// </summary>
    /// <returns>
    ///   Дескриптор файла для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Свойство <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.Handle" /> всегда создает это исключение.
    /// </exception>
    [Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public override IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.NotPermittedError();
        return Win32Native.INVALID_HANDLE_VALUE;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" />, представляющий дескриптор файла операционной системы для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" />, который представляет дескриптор файла операционной системы для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Свойство <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.SafeFileHandle" /> всегда создает это исключение.
    /// </exception>
    public override SafeFileHandle SafeFileHandle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.NotPermittedError();
        return (SafeFileHandle) null;
      }
    }

    /// <summary>
    ///   Задает в качестве длины этого объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> указанное значение <paramref name="value" />.
    /// </summary>
    /// <param name="value">
    ///   Новая длина объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="value" /> является отрицательным числом.
    /// </exception>
    [SecuritySafeCritical]
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong length = (ulong) this.m_fs.Length;
        ulong num = (ulong) value;
        this.m_isf.Reserve(length, num);
        try
        {
          this.ZeroInit(length, num);
          this.m_fs.SetLength(value);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(length, num);
          throw;
        }
        if (length <= num)
          return;
        this.m_isf.UndoReserveOperation(num, length);
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>
    ///   Запрещает другим процессам чтение из потока или запись в него.
    /// </summary>
    /// <param name="position">
    ///   Начальная позиция блокируемого диапазона.
    ///    Значение этого параметра должно быть больше или равно 0 (нулю).
    /// </param>
    /// <param name="length">Число байтов для блокировки.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> или <paramref name="length" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Файл закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Процессу не удается получить доступ к файлу, так как другой процесс заблокировал часть этого файла.
    /// </exception>
    public override void Lock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? nameof (position) : nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_fs.Lock(position, length);
    }

    /// <summary>
    ///   Разрешает другим процессам доступ ко всему ранее заблокированному файлу или его части.
    /// </summary>
    /// <param name="position">
    ///   Начальная позиция диапазона для разблокирования.
    ///    Значение этого параметра должно быть больше или равно 0 (нулю).
    /// </param>
    /// <param name="length">Число байтов для разблокирования.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> или <paramref name="length" /> является отрицательным значением.
    /// </exception>
    public override void Unlock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? nameof (position) : nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_fs.Unlock(position, length);
    }

    private void ZeroInit(ulong oldLen, ulong newLen)
    {
      if (oldLen >= newLen)
        return;
      ulong num1 = newLen - oldLen;
      byte[] buffer = new byte[1024];
      long position = this.m_fs.Position;
      this.m_fs.Seek((long) oldLen, SeekOrigin.Begin);
      if (num1 <= 1024UL)
      {
        this.m_fs.Write(buffer, 0, (int) num1);
        this.m_fs.Position = position;
      }
      else
      {
        int count = 1024 - (int) ((long) oldLen & 1023L);
        this.m_fs.Write(buffer, 0, count);
        ulong num2 = num1 - (ulong) count;
        int num3 = (int) (num2 / 1024UL);
        for (int index = 0; index < num3; ++index)
          this.m_fs.Write(buffer, 0, 1024);
        this.m_fs.Write(buffer, 0, (int) ((long) num2 & 1023L));
        this.m_fs.Position = position;
      }
    }

    /// <summary>
    ///   Копирует байты из текущего буферизованного объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> в массив.
    /// </summary>
    /// <param name="buffer">Буфер, который требуется прочитать.</param>
    /// <param name="offset">
    ///   Смещение в буфере, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <returns>
    ///   Общее число байтов, считанных в <paramref name="buffer" />.
    ///    Это число может быть меньше запрошенного числа байтов, если многие байты недоступны в данный момент, или равно нулю, если достигнут конец потока.
    /// </returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.m_fs.Read(buffer, offset, count);
    }

    /// <summary>
    ///   Считывает один байт из объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> в изолированном хранилище.
    /// </summary>
    /// <returns>
    ///   8-разрядное целое значение без знака, считанное из файла изолированного хранилища.
    /// </returns>
    public override int ReadByte()
    {
      return this.m_fs.ReadByte();
    }

    /// <summary>
    ///   Задает указанное значение для текущего положения данного объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </summary>
    /// <param name="offset">
    ///   Новое положение объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </param>
    /// <param name="origin">
    ///   Одно из значений <see cref="T:System.IO.SeekOrigin" />.
    /// </param>
    /// <returns>
    ///   Новое положение объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="origin" /> должен быть одним из значений <see cref="T:System.IO.SeekOrigin" />.
    /// </exception>
    [SecuritySafeCritical]
    public override long Seek(long offset, SeekOrigin origin)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong length = (ulong) this.m_fs.Length;
        ulong newLen;
        switch (origin)
        {
          case SeekOrigin.Begin:
            newLen = offset < 0L ? 0UL : (ulong) offset;
            break;
          case SeekOrigin.Current:
            newLen = this.m_fs.Position + offset < 0L ? 0UL : (ulong) (this.m_fs.Position + offset);
            break;
          case SeekOrigin.End:
            newLen = this.m_fs.Length + offset < 0L ? 0UL : (ulong) (this.m_fs.Length + offset);
            break;
          default:
            throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
        }
        this.m_isf.Reserve(length, newLen);
        try
        {
          this.ZeroInit(length, newLen);
          return this.m_fs.Seek(offset, origin);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(length, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>
    ///   Записывает в объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> блок байтов, используя данные, считанные из массива байтов.
    /// </summary>
    /// <param name="buffer">Буфер для записи.</param>
    /// <param name="offset">
    ///   Смещение байтов в буфере, с которого начинается запись или чтение.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Попытка записи превышает квоту для объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </exception>
    [SecuritySafeCritical]
    public override void Write(byte[] buffer, int offset, int count)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong length = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + (ulong) count;
        this.m_isf.Reserve(length, newLen);
        try
        {
          this.m_fs.Write(buffer, offset, count);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(length, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>
    ///   Записывает в объект <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> один байт.
    /// </summary>
    /// <param name="value">
    ///   Значение в байтах, записываемое в файл изолированного хранилища.
    /// </param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Попытка записи превышает квоту для объекта <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.
    /// </exception>
    [SecuritySafeCritical]
    public override void WriteByte(byte value)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong length = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + 1UL;
        this.m_isf.Reserve(length, newLen);
        try
        {
          this.m_fs.WriteByte(value);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(length, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>Начинает асинхронное чтение.</summary>
    /// <param name="buffer">
    ///   Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение в <paramref name="buffer" /> (в байтах), с которого начинается чтение.
    /// </param>
    /// <param name="numBytes">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <param name="userCallback">
    ///   Метод, вызываемый после завершения операции асинхронного чтения.
    ///    Этот параметр является необязательным.
    /// </param>
    /// <param name="stateObject">Состояние асинхронного чтения.</param>
    /// <returns>
    ///   Объект <see cref="T:System.IAsyncResult" />, представляющий асинхронное чтение, которое может быть отложено.
    ///    Этот объект <see cref="T:System.IAsyncResult" /> должен быть передан в метод <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.EndRead(System.IAsyncResult)" /> потока для определения количества считанных байтов.
    ///    Это можно сделать либо с помощью того же кода, что и при вызове метода <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />, либо с помощью обратного вызова, переданного в метод <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Предпринята попытка асинхронного чтения за пределами файла.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
    }

    /// <summary>Завершает отложенный запрос асинхронного чтения.</summary>
    /// <param name="asyncResult">Отложенный асинхронный запрос.</param>
    /// <returns>
    ///   Число байтов, считанных из потока, между 0 и числом запрошенных байтов.
    ///    В конце потока возвращается только нуль.
    ///    В противном случае потоки блокируются до тех пор, пока не будет доступен по крайней мере один байт.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="asyncResult" /> — <see langword="null" />.
    /// </exception>
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      return this.m_fs.EndRead(asyncResult);
    }

    /// <summary>Начинает асинхронную запись.</summary>
    /// <param name="buffer">Буфер, в который записываются данные.</param>
    /// <param name="offset">
    ///   Смещение байтов в <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="numBytes">
    ///   Максимальное число байтов для записи.
    /// </param>
    /// <param name="userCallback">
    ///   Метод, вызываемый после завершения операции асинхронной записи.
    ///    Этот параметр является необязательным.
    /// </param>
    /// <param name="stateObject">Состояние асинхронной записи.</param>
    /// <returns>
    ///   Класс <see cref="T:System.IAsyncResult" />, представляющий асинхронную запись, которая может быть отложена.
    ///    Этот <see cref="T:System.IAsyncResult" /> должен быть передан в метод <see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" /> потока для обеспечения завершения записи и последующего освобождения ресурсов.
    ///    Это можно сделать либо с помощью того же кода, что и при вызове метода <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />, либо с помощью обратного вызова, переданного в метод <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Предпринята попытка асинхронной записи за пределами файла.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong length = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + (ulong) numBytes;
        this.m_isf.Reserve(length, newLen);
        try
        {
          return this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(length, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>Завершает асинхронную запись.</summary>
    /// <param name="asyncResult">
    ///   Завершаемый отложенный асинхронный запрос ввода-вывода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      this.m_fs.EndWrite(asyncResult);
    }

    internal void NotPermittedError(string str)
    {
      throw new IsolatedStorageException(str);
    }

    internal void NotPermittedError()
    {
      this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation_ISFS"));
    }
  }
}
