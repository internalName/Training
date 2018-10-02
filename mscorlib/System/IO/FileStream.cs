// Decompiled with JetBrains decompiler
// Type: System.IO.FileStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет <see cref="T:System.IO.Stream" /> в файле, поддерживая синхронные и асинхронные операции чтения и записи.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  public class FileStream : Stream
  {
    internal const int DefaultBufferSize = 4096;
    private const bool _canUseAsync = true;
    private byte[] _buffer;
    private string _fileName;
    private bool _isAsync;
    private bool _canRead;
    private bool _canWrite;
    private bool _canSeek;
    private bool _exposedHandle;
    private bool _isPipe;
    private int _readPos;
    private int _readLen;
    private int _writePos;
    private int _bufferSize;
    [SecurityCritical]
    private SafeFileHandle _handle;
    private long _pos;
    private long _appendStart;
    private static AsyncCallback s_endReadTask;
    private static AsyncCallback s_endWriteTask;
    private static Action<object> s_cancelReadHandler;
    private static Action<object> s_cancelWriteHandler;
    private const int FILE_ATTRIBUTE_NORMAL = 128;
    private const int FILE_ATTRIBUTE_ENCRYPTED = 16384;
    private const int FILE_FLAG_OVERLAPPED = 1073741824;
    internal const int GENERIC_READ = -2147483648;
    private const int GENERIC_WRITE = 1073741824;
    private const int FILE_BEGIN = 0;
    private const int FILE_CURRENT = 1;
    private const int FILE_END = 2;
    internal const int ERROR_BROKEN_PIPE = 109;
    internal const int ERROR_NO_DATA = 232;
    private const int ERROR_HANDLE_EOF = 38;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_IO_PENDING = 997;

    internal FileStream()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> указанным путем и режимом создания.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), строку, содержащую только пробелы, или строку, содержащую один или несколько недопустимых символов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д. в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" />, и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Поток был закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="mode" /> содержит недопустимое значение.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode)
      : this(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания и разрешением на чтение и запись.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Константа, определяющая способ доступа к файлу объекта <see langword="FileStream" />.
    ///    Также определяет значения, возвращаемые свойствами <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    ///    Свойство <see cref="P:System.IO.FileStream.CanSeek" /> имеет значение <see langword="true" />, если параметр <paramref name="path" /> задает файл на диске.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство не файлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" />, и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="mode" /> содержит недопустимое значение.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access)
      : this(path, mode, access, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания, разрешением на чтение и запись и разрешением на совместное использование.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Константа, определяющая способ доступа к файлу объекта <see langword="FileStream" />.
    ///    Также определяет значения, возвращаемые свойствами <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    ///    Свойство <see cref="P:System.IO.FileStream.CanSeek" /> имеет значение <see langword="true" />, если параметр <paramref name="path" /> задает файл на диске.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство не файлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" />, и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Система работает под управлением Windows 98 или Windows 98 Second Edition, и для <paramref name="share" /> задано значение <see langword="FileShare.Delete" />.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="mode" /> содержит недопустимое значение.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share)
      : this(path, mode, access, share, 4096, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания, разрешениями на чтение и запись и на совместное использование, а также размером буфера.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Константа, определяющая способ доступа к файлу объекта <see langword="FileStream" />.
    ///    Также определяет значения, возвращаемые свойствами <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    ///    Свойство <see cref="P:System.IO.FileStream.CanSeek" /> имеет значение <see langword="true" />, если параметр <paramref name="path" /> задает файл на диске.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" /> и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Система работает под управлением Windows 98 или Windows 98 Second Edition, и для <paramref name="share" /> задано значение <see langword="FileShare.Delete" />.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
      : this(path, mode, access, share, bufferSize, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания, разрешениями на чтение, запись и совместное использование, доступом для других FileStreams к этому же файлу, размером буфера и дополнительными параметрами файла.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Константа, определяющая способ доступа к файлу объекта <see langword="FileStream" />.
    ///    Также определяет значения, возвращаемые свойствами <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    ///    Свойство <see cref="P:System.IO.FileStream.CanSeek" /> имеет значение <see langword="true" />, если параметр <paramref name="path" /> задает файл на диске.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <param name="options">
    ///   Значение, задающее дополнительные параметры файла.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" /> и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="options" /> указан режим <see cref="F:System.IO.FileOptions.Encrypted" />, но в текущей платформе шифрование файлов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
      : this(path, mode, access, share, bufferSize, options, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания, разрешениями на чтение и запись и совместное использование, размером буфера и синхронным или асинхронным состоянием.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see langword="FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Константа, определяющая способ доступа к файлу объекта <see langword="FileStream" />.
    ///    Также определяет значения, возвращаемые свойствами <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    ///    Свойство <see cref="P:System.IO.FileStream.CanSeek" /> имеет значение <see langword="true" />, если параметр <paramref name="path" /> задает файл на диске.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию: 4096.
    /// </param>
    /// <param name="useAsync">
    ///   Указывает, использовать ли асинхронный ввод-вывод или синхронный ввод-вывод.
    ///    Однако обратите внимание, что базовая операционная система может не поддерживать асинхронный ввод-вывод, поэтому, когда задается значение <see langword="true" />, дескриптор может быть открыт синхронно в зависимости от платформы.
    ///    При асинхронном открытии методы <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> и <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> выполняются лучше при чтении или записи больших объемов, но они могут работать намного медленнее при чтении или записи маленьких объемов данных.
    ///    Если приложение разработано таким образом, чтобы получить преимущества асинхронного ввода-вывода, установите для параметра <paramref name="useAsync" /> значение <see langword="true" />.
    ///    При корректном использовании асинхронного ввода-вывода быстродействие приложения может возрасти вплоть до 10 раз, но использование такого режима ввода-вывода без переработки приложения может во столько же раз ухудшить быстродействие.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" /> и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Система работает под управлением Windows 98 или Windows 98 Second Edition, и для <paramref name="share" /> задано значение <see langword="FileShare.Delete" />.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
      : this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданными путем, режимом создания, правами на доступ и совместное использование, размером буфера, дополнительными параметрами файла, управлением доступом и аудитом безопасности.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see cref="T:System.IO.FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="rights">
    ///   Константа, определяющая права доступа, используемые при создании правил доступа и аудита для файла.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <param name="options">
    ///   Константа, задающая дополнительные параметры файла.
    /// </param>
    /// <param name="fileSecurity">
    ///   Константа, определяющая систему безопасности управления доступом и аудита доступа для файла.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" /> и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="options" /> указан режим <see cref="F:System.IO.FileOptions.Encrypted" />, но в текущей платформе шифрование файлов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный <paramref name="path" />, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
    {
      object pinningHandle;
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share, fileSecurity, out pinningHandle);
      try
      {
        this.Init(path, mode, (FileAccess) 0, (int) rights, true, share, bufferSize, options, secAttrs, Path.GetFileName(path), false, false, false);
      }
      finally
      {
        if (pinningHandle != null)
          ((GCHandle) pinningHandle).Free();
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> заданным путем, режимом создания, разрешениями на чтение, запись и совместное использование, размером буфера и дополнительными параметрами файла.
    /// </summary>
    /// <param name="path">
    ///   Абсолютный или относительный путь к файлу, который будет инкапсулироваться объектом <see cref="T:System.IO.FileStream" />.
    /// </param>
    /// <param name="mode">
    ///   Константа, определяющая способ открытия или создания файла.
    /// </param>
    /// <param name="rights">
    ///   Константа, определяющая права доступа, используемые при создании правил доступа и аудита для файла.
    /// </param>
    /// <param name="share">
    ///   Константа, определяющая способ совместного использования файла процессами.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <param name="options">
    ///   Константа, задающая дополнительные параметры файла.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой пустую строку (""), содержащую только пробел или хотя бы один недопустимый символ.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> ссылается на устройство нефайлового типа, например "con:", "com1:", "lpt1:" и т. д., в среде, отличной от NTFS.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" />, <paramref name="access" /> или <paramref name="share" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл, например когда <paramref name="mode" /> равно <see langword="FileMode.Truncate" /> или <see langword="FileMode.Open" /> и файл, заданный параметром <paramref name="path" />, не существует.
    ///    Файл должен уже существовать в этих режимах.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая операционная система не является системой Windows NT или более поздней версией.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например задание параметра <see langword="FileMode.CreateNew" />, когда файл, заданный параметром <paramref name="path" />, уже существует.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного <paramref name="path" />, например когда <paramref name="access" /> равно <see langword="Write" /> или <see langword="ReadWrite" />, а для файла или каталога установлен доступ только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="options" /> указан режим <see cref="F:System.IO.FileOptions.Encrypted" />, но в текущей платформе шифрование файлов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный <paramref name="path" />, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имен файлов — 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, (FileAccess) 0, (int) rights, true, share, bufferSize, options, secAttrs, Path.GetFileName(path), false, false, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, false, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, useLongPath, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, useLongPath, checkHost);
    }

    [SecuritySafeCritical]
    private unsafe void Init(string path, FileMode mode, FileAccess access, int rights, bool useRights, FileShare share, int bufferSize, FileOptions options, Win32Native.SECURITY_ATTRIBUTES secAttrs, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path), Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      FileSystemRights fileSystemRights = (FileSystemRights) rights;
      this._fileName = msgPath;
      this._exposedHandle = false;
      FileShare fileShare = share & ~FileShare.Inheritable;
      string paramName = (string) null;
      if (mode < FileMode.CreateNew || mode > FileMode.Append)
        paramName = nameof (mode);
      else if (!useRights && (access < FileAccess.Read || access > FileAccess.ReadWrite))
        paramName = nameof (access);
      else if (useRights && (fileSystemRights < FileSystemRights.ReadData || fileSystemRights > FileSystemRights.FullControl))
      {
        paramName = nameof (rights);
      }
      else
      {
        switch (fileShare)
        {
          case FileShare.None:
          case FileShare.Read:
          case FileShare.Write:
          case FileShare.ReadWrite:
          case FileShare.Delete:
          case FileShare.Read | FileShare.Delete:
          case FileShare.Write | FileShare.Delete:
          case FileShare.ReadWrite | FileShare.Delete:
            break;
          default:
            paramName = nameof (share);
            break;
        }
      }
      if (paramName != null)
        throw new ArgumentOutOfRangeException(paramName, Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (options != FileOptions.None && (options & (FileOptions) 67092479) != FileOptions.None)
        throw new ArgumentOutOfRangeException(nameof (options), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if ((!useRights && (access & FileAccess.Write) == (FileAccess) 0 || useRights && (fileSystemRights & FileSystemRights.Write) == (FileSystemRights) 0) && (mode == FileMode.Truncate || mode == FileMode.CreateNew || (mode == FileMode.Create || mode == FileMode.Append)))
      {
        if (!useRights)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileMode&AccessCombo", (object) mode, (object) access));
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileMode&RightsCombo", (object) mode, (object) fileSystemRights));
      }
      if (useRights && mode == FileMode.Truncate)
      {
        if (fileSystemRights == FileSystemRights.Write)
        {
          useRights = false;
          access = FileAccess.Write;
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileModeTruncate&RightsCombo", (object) mode, (object) fileSystemRights));
      }
      int dwDesiredAccess;
      if (!useRights)
      {
        int num;
        switch (access)
        {
          case FileAccess.Read:
            num = int.MinValue;
            break;
          case FileAccess.Write:
            num = 1073741824;
            break;
          default:
            num = -1073741824;
            break;
        }
        dwDesiredAccess = num;
      }
      else
        dwDesiredAccess = rights;
      int maxPathLength = useLongPath ? (int) short.MaxValue : (AppContextSwitches.BlockLongPaths ? 260 : (int) short.MaxValue);
      string str1 = Path.NormalizePath(path, true, maxPathLength);
      this._fileName = str1;
      if ((!CodeAccessSecurityEngine.QuickCheckForAllDemands() || AppContextSwitches.UseLegacyPathHandling) && str1.StartsWith("\\\\.\\", StringComparison.Ordinal))
        throw new ArgumentException(Environment.GetResourceString("Arg_DevicesNotSupported"));
      bool flag1 = false;
      if (!useRights && (access & FileAccess.Read) != (FileAccess) 0 || useRights && (fileSystemRights & FileSystemRights.ReadAndExecute) != (FileSystemRights) 0)
      {
        if (mode == FileMode.Append)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppendMode"));
        flag1 = true;
      }
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        FileIOPermission.EmulateFileIOPermissionChecks(str1);
      }
      else
      {
        FileIOPermissionAccess access1 = FileIOPermissionAccess.NoAccess;
        if (flag1)
          access1 |= FileIOPermissionAccess.Read;
        if (!useRights && (access & FileAccess.Write) != (FileAccess) 0 || useRights && (fileSystemRights & (FileSystemRights.Write | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.Delete | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership)) != (FileSystemRights) 0 || useRights && (fileSystemRights & FileSystemRights.Synchronize) != (FileSystemRights) 0 && mode == FileMode.OpenOrCreate)
        {
          if (mode == FileMode.Append)
            access1 |= FileIOPermissionAccess.Append;
          else
            access1 |= FileIOPermissionAccess.Write;
        }
        AccessControlActions control = secAttrs != null && (IntPtr) secAttrs.pSecurityDescriptor != IntPtr.Zero ? AccessControlActions.Change : AccessControlActions.None;
        FileIOPermission.QuickDemand(access1, control, new string[1]
        {
          str1
        }, false, false);
      }
      share &= ~FileShare.Inheritable;
      bool flag2 = mode == FileMode.Append;
      if (mode == FileMode.Append)
        mode = FileMode.OpenOrCreate;
      if ((options & FileOptions.Asynchronous) != FileOptions.None)
        this._isAsync = true;
      else
        options &= ~FileOptions.Asynchronous;
      int dwFlagsAndAttributes = (int) (options | (FileOptions) 1048576);
      int newMode = Win32Native.SetErrorMode(1);
      try
      {
        string str2 = str1;
        if (useLongPath)
          str2 = Path.AddLongPathPrefix(str2);
        this._handle = Win32Native.SafeCreateFile(str2, dwDesiredAccess, share, secAttrs, mode, dwFlagsAndAttributes, IntPtr.Zero);
        if (this._handle.IsInvalid)
        {
          int errorCode = Marshal.GetLastWin32Error();
          if (errorCode == 3 && str1.Equals(Directory.InternalGetDirectoryRoot(str1)))
            errorCode = 5;
          bool flag3 = false;
          if (!bFromProxy)
          {
            try
            {
              FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, this._fileName, false, false);
              flag3 = true;
            }
            catch (SecurityException ex)
            {
            }
          }
          if (flag3)
            __Error.WinIOError(errorCode, this._fileName);
          else
            __Error.WinIOError(errorCode, msgPath);
        }
      }
      finally
      {
        Win32Native.SetErrorMode(newMode);
      }
      if (Win32Native.GetFileType(this._handle) != 1)
      {
        this._handle.Close();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FileStreamOnNonFiles"));
      }
      if (this._isAsync)
      {
        bool flag3 = false;
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
        try
        {
          flag3 = ThreadPool.BindHandle((SafeHandle) this._handle);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
          if (!flag3)
            this._handle.Close();
        }
        if (!flag3)
          throw new IOException(Environment.GetResourceString("IO.IO_BindHandleFailed"));
      }
      if (!useRights)
      {
        this._canRead = (uint) (access & FileAccess.Read) > 0U;
        this._canWrite = (uint) (access & FileAccess.Write) > 0U;
      }
      else
      {
        this._canRead = (uint) (fileSystemRights & FileSystemRights.ReadData) > 0U;
        this._canWrite = (fileSystemRights & FileSystemRights.WriteData) != (FileSystemRights) 0 || (uint) (fileSystemRights & FileSystemRights.AppendData) > 0U;
      }
      this._canSeek = true;
      this._isPipe = false;
      this._pos = 0L;
      this._bufferSize = bufferSize;
      this._readPos = 0;
      this._readLen = 0;
      this._writePos = 0;
      if (flag2)
        this._appendStart = this.SeekCore(0L, SeekOrigin.End);
      else
        this._appendStart = -1L;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор для файла, являющегося текущим объектом <see langword="FileStream" />, будет инкапсулирован.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="access" /> не является полем <see cref="T:System.IO.FileAccess" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access)
      : this(handle, access, true, 4096, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись и владельца экземпляра <see langword="FileStream" />.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор для файла, являющегося текущим объектом <see langword="FileStream" />, будет инкапсулирован.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, если владельцем дескриптора файла будет этот экземпляр <see langword="FileStream" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="access" /> не является полем <see cref="T:System.IO.FileAccess" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle)
      : this(handle, access, ownsHandle, 4096, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись, владельца экземпляра <see langword="FileStream" /> и размер буфера.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор файла для файла, который будет инкапсулироваться с помощью объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, если владельцем дескриптора файла будет этот экземпляр <see langword="FileStream" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bufferSize" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, например ошибка диска.
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize)
      : this(handle, access, ownsHandle, bufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись, владельца экземпляра <see langword="FileStream" />, размер буфера и синхронное или асинхронное состояние.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор файла для файла, который будет инкапсулироваться с помощью объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, если владельцем дескриптора файла будет этот экземпляр <see langword="FileStream" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <param name="isAsync">
    ///   Значение <see langword="true" />, если этот дескриптор был открыт асинхронно (т. е. в режиме перекрывающегося ввода-вывода); в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="access" /> меньше <see langword="FileAccess.Read" /> или больше <see langword="FileAccess.ReadWrite" />, либо <paramref name="bufferSize" /> не больше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Дескриптор недействителен.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
      : this(new SafeFileHandle(handle, ownsHandle), access, bufferSize, isAsync)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор для файла, являющегося текущим объектом <see langword="FileStream" />, будет инкапсулирован.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="access" /> не является полем <see cref="T:System.IO.FileAccess" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(SafeFileHandle handle, FileAccess access)
      : this(handle, access, 4096, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись и размер буфера.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор для файла, являющегося текущим объектом <see langword="FileStream" />, будет инкапсулирован.
    /// </param>
    /// <param name="access">
    ///   Константа <see cref="T:System.IO.FileAccess" />, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="handle" /> является недопустимым дескриптором.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="handle" /> является синхронным дескриптором, но был использован асинхронно.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize)
      : this(handle, access, bufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileStream" /> для заданного дескриптора файла, используя указанные разрешения на чтение и запись, размер буфера и синхронное или асинхронное состояние.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор файла для файла, который будет инкапсулироваться с помощью объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="access">
    ///   Константа, которая задает свойства <see cref="P:System.IO.FileStream.CanRead" /> и <see cref="P:System.IO.FileStream.CanWrite" /> объекта <see langword="FileStream" />.
    /// </param>
    /// <param name="bufferSize">
    ///   Положительное значение <see cref="T:System.Int32" />, большее 0, определяющее размер буфера.
    ///    Размер буфера по умолчанию — 4096.
    /// </param>
    /// <param name="isAsync">
    ///   Значение <see langword="true" />, если этот дескриптор был открыт асинхронно (т. е. в режиме перекрывающегося ввода-вывода); в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="handle" /> является недопустимым дескриптором.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="handle" /> является синхронным дескриптором, но был использован асинхронно.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, ошибка диска).
    /// 
    ///   -или-
    /// 
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Запрошенный <paramref name="access" /> не поддерживается операционной системой для указанного дескриптора файлов, например, когда <paramref name="access" /> равен <see langword="Write" /> или <see langword="ReadWrite" />, а для дескриптора файлов установлен доступ только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
    {
      if (handle.IsInvalid)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), nameof (handle));
      this._handle = handle;
      this._exposedHandle = true;
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException(nameof (access), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      int fileType = Win32Native.GetFileType(this._handle);
      this._isAsync = isAsync;
      this._canRead = (uint) (access & FileAccess.Read) > 0U;
      this._canWrite = (uint) (access & FileAccess.Write) > 0U;
      this._canSeek = fileType == 1;
      this._bufferSize = bufferSize;
      this._readPos = 0;
      this._readLen = 0;
      this._writePos = 0;
      this._fileName = (string) null;
      this._isPipe = fileType == 3;
      if (this._isAsync)
      {
        bool flag;
        try
        {
          flag = ThreadPool.BindHandle((SafeHandle) this._handle);
        }
        catch (ApplicationException ex)
        {
          throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotAsync"));
        }
        if (!flag)
          throw new IOException(Environment.GetResourceString("IO.IO_BindHandleFailed"));
      }
      else if (fileType != 3)
        this.VerifyHandleIsSync();
      if (this._canSeek)
        this.SeekCore(0L, SeekOrigin.Current);
      else
        this._pos = 0L;
    }

    [SecuritySafeCritical]
    private static Win32Native.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share)
    {
      Win32Native.SECURITY_ATTRIBUTES structure = (Win32Native.SECURITY_ATTRIBUTES) null;
      if ((share & FileShare.Inheritable) != FileShare.None)
      {
        structure = new Win32Native.SECURITY_ATTRIBUTES();
        structure.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(structure);
        structure.bInheritHandle = 1;
      }
      return structure;
    }

    [SecuritySafeCritical]
    private static unsafe Win32Native.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share, FileSecurity fileSecurity, out object pinningHandle)
    {
      pinningHandle = (object) null;
      Win32Native.SECURITY_ATTRIBUTES structure = (Win32Native.SECURITY_ATTRIBUTES) null;
      if ((share & FileShare.Inheritable) != FileShare.None || fileSecurity != null)
      {
        structure = new Win32Native.SECURITY_ATTRIBUTES();
        structure.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(structure);
        if ((share & FileShare.Inheritable) != FileShare.None)
          structure.bInheritHandle = 1;
        if (fileSecurity != null)
        {
          byte[] descriptorBinaryForm = fileSecurity.GetSecurityDescriptorBinaryForm();
          pinningHandle = (object) GCHandle.Alloc((object) descriptorBinaryForm, GCHandleType.Pinned);
          fixed (byte* numPtr = descriptorBinaryForm)
            structure.pSecurityDescriptor = numPtr;
        }
      }
      return structure;
    }

    [SecuritySafeCritical]
    private unsafe void VerifyHandleIsSync()
    {
      byte[] bytes = new byte[1];
      int hr = 0;
      int num = 0;
      if (this.CanRead)
        num = this.ReadFileNative(this._handle, bytes, 0, 0, (NativeOverlapped*) null, out hr);
      else if (this.CanWrite)
        num = this.WriteFileNative(this._handle, bytes, 0, 0, (NativeOverlapped*) null, out hr);
      if (hr == 87)
        throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotSync"));
      if (hr != 6)
        return;
      __Error.WinIOError(hr, "<OS handle>");
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если в потоке поддерживаются операции чтения; значение <see langword="false" />, если поток закрыт или открыт только для записи.
    /// </returns>
    public override bool CanRead
    {
      get
      {
        return this._canRead;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает операции записи; значение <see langword="false" />, если поток закрыт или открыт только для чтения.
    /// </returns>
    public override bool CanWrite
    {
      get
      {
        return this._canWrite;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций поиска.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает поиск; значение <see langword="false" />, если поток закрыт или объект <see langword="FileStream" /> был сконструирован из дескриптора операционной системы, такого как канал или вывод на консоль.
    /// </returns>
    public override bool CanSeek
    {
      get
      {
        return this._canSeek;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее, как был открыт объект <see langword="FileStream" />: синхронно или асинхронно.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see langword="FileStream" /> был открыт асинхронно, в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsAsync
    {
      get
      {
        return this._isAsync;
      }
    }

    /// <summary>Возвращает длину потока в байтах.</summary>
    /// <returns>
    ///   Длинное значение, представляющее длину потока в байтах.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <see cref="P:System.IO.FileStream.CanSeek" /> для этого потока имеет значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода (например, файл закрывается).
    /// </exception>
    public override long Length
    {
      [SecuritySafeCritical] get
      {
        if (this._handle.IsClosed)
          __Error.FileNotOpen();
        if (!this.CanSeek)
          __Error.SeekNotSupported();
        int highSize = 0;
        int fileSize = Win32Native.GetFileSize(this._handle, out highSize);
        if (fileSize == -1)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error != 0)
            __Error.WinIOError(lastWin32Error, string.Empty);
        }
        long num = (long) highSize << 32 | (long) (uint) fileSize;
        if (this._writePos > 0 && this._pos + (long) this._writePos > num)
          num = (long) this._writePos + this._pos;
        return num;
      }
    }

    /// <summary>
    ///   Возвращает имя объекта <see langword="FileStream" />, которое было передано в конструктор.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя объекта <see langword="FileStream" />.
    /// </returns>
    public string Name
    {
      [SecuritySafeCritical] get
      {
        if (this._fileName == null)
          return Environment.GetResourceString("IO_UnknownFileName");
        FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, this._fileName, false, false);
        return this._fileName;
      }
    }

    internal string NameInternal
    {
      get
      {
        if (this._fileName == null)
          return "<UnknownFileName>";
        return this._fileName;
      }
    }

    /// <summary>Возвращает или задает текущую позицию этого потока.</summary>
    /// <returns>Текущая позиция потока.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// 
    ///   -или-
    /// 
    ///   Для положения задано очень большое значение за пределами конца потока в Windows 98 или более ранней версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Попытка установить для положения отрицательное значение.
    /// </exception>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Попытка поиска за пределами потока, который не поддерживает это.
    /// </exception>
    public override long Position
    {
      [SecuritySafeCritical] get
      {
        if (this._handle.IsClosed)
          __Error.FileNotOpen();
        if (!this.CanSeek)
          __Error.SeekNotSupported();
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        return this._pos + (long) (this._readPos - this._readLen + this._writePos);
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._writePos > 0)
          this.FlushWrite(false);
        this._readPos = 0;
        this._readLen = 0;
        this.Seek(value, SeekOrigin.Begin);
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.AccessControl.FileSecurity" />, который инкапсулирует записи списка управления доступом (ACL) для файла, описываемого текущим объектом <see cref="T:System.IO.FileStream" />.
    /// </summary>
    /// <returns>
    ///   Объект, инкапсулирующий параметры управления доступом для файла, который описывается текущим объектом <see cref="T:System.IO.FileStream" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Файл закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   При открытии файла произошла ошибка ввода-вывода.
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
    [SecuritySafeCritical]
    public FileSecurity GetAccessControl()
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      return new FileSecurity(this._handle, this._fileName, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Применяет записи списка управления доступом (ACL), описанные объектом <see cref="T:System.Security.AccessControl.FileSecurity" />, к файлу, который описывается текущим объектом <see cref="T:System.IO.FileStream" />.
    /// </summary>
    /// <param name="fileSecurity">
    ///   Объект, который описывает запись ACL, применяемую к текущему файлу.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Файл закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileSecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Не удалось найти или изменить файл.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий процесс не может открыть файл из-за отсутствия соответствующих прав доступа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetAccessControl(FileSecurity fileSecurity)
    {
      if (fileSecurity == null)
        throw new ArgumentNullException(nameof (fileSecurity));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      fileSecurity.Persist(this._handle, this._fileName);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.FileStream" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this._handle == null || this._handle.IsClosed || this._writePos <= 0)
          return;
        this.FlushWrite(!disposing);
      }
      finally
      {
        if (this._handle != null && !this._handle.IsClosed)
          this._handle.Dispose();
        this._canRead = false;
        this._canWrite = false;
        this._canSeek = false;
        base.Dispose(disposing);
      }
    }

    /// <summary>
    ///   Гарантирует, что ресурсы освобождены и выполнены другие операции очистки, когда сборщик мусора восстанавливает <see langword="FileStream" />.
    /// </summary>
    [SecuritySafeCritical]
    ~FileStream()
    {
      if (this._handle == null)
        return;
      this.Dispose(false);
    }

    /// <summary>
    ///   Очищает буферы для этого потока и вызывает запись всех буферизованных данных в файл.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    public override void Flush()
    {
      this.Flush(false);
    }

    /// <summary>
    ///   Очищает буферы для этого потока и вызывает запись всех буферизованных данных в файл, а также очищает все буферы промежуточных файлов.
    /// </summary>
    /// <param name="flushToDisk">
    ///   Значение <see langword="true" /> для записи на диск  буферов всех промежуточных файлов; в противном случае — значение <see langword="false" />.
    /// </param>
    [SecuritySafeCritical]
    public virtual void Flush(bool flushToDisk)
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      this.FlushInternalBuffer();
      if (!flushToDisk || !this.CanWrite)
        return;
      this.FlushOSBuffer();
    }

    private void FlushInternalBuffer()
    {
      if (this._writePos > 0)
      {
        this.FlushWrite(false);
      }
      else
      {
        if (this._readPos >= this._readLen || !this.CanSeek)
          return;
        this.FlushRead();
      }
    }

    [SecuritySafeCritical]
    private void FlushOSBuffer()
    {
      if (Win32Native.FlushFileBuffers(this._handle))
        return;
      __Error.WinIOError();
    }

    private void FlushRead()
    {
      if (this._readPos - this._readLen != 0)
        this.SeekCore((long) (this._readPos - this._readLen), SeekOrigin.Current);
      this._readPos = 0;
      this._readLen = 0;
    }

    private void FlushWrite(bool calledFromFinalizer)
    {
      if (this._isAsync)
      {
        IAsyncResult asyncResult = (IAsyncResult) this.BeginWriteCore(this._buffer, 0, this._writePos, (AsyncCallback) null, (object) null);
        if (!calledFromFinalizer)
          this.EndWrite(asyncResult);
      }
      else
        this.WriteCore(this._buffer, 0, this._writePos);
      this._writePos = 0;
    }

    /// <summary>
    ///   Возвращает дескриптор файла операционной системы для файла, инкапсулируемого текущим объектом <see langword="FileStream" />.
    /// </summary>
    /// <returns>
    ///   Дескриптор файла операционной системы для файла, инкапсулируемого этим объектом <see langword="FileStream" />, или значение -1, если объект <see langword="FileStream" /> закрыт.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [Obsolete("This property has been deprecated.  Please use FileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.Flush();
        this._readPos = 0;
        this._readLen = 0;
        this._writePos = 0;
        this._exposedHandle = true;
        return this._handle.DangerousGetHandle();
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" />, представляющий дескриптор файла операционной системы для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.FileStream" />.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий дескриптор файла операционной системы для файла, инкапсулируемого текущим объектом <see cref="T:System.IO.FileStream" />.
    /// </returns>
    public virtual SafeFileHandle SafeFileHandle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.Flush();
        this._readPos = 0;
        this._readLen = 0;
        this._writePos = 0;
        this._exposedHandle = true;
        return this._handle;
      }
    }

    /// <summary>
    ///   Устанавливает длину этого потока на заданное значение.
    /// </summary>
    /// <param name="value">Новая длина потока.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись и поиск.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Предпринята попытка установить для параметра <paramref name="value" /> значение меньше 0.
    /// </exception>
    [SecuritySafeCritical]
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this.CanSeek)
        __Error.SeekNotSupported();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (this._writePos > 0)
        this.FlushWrite(false);
      else if (this._readPos < this._readLen)
        this.FlushRead();
      this._readPos = 0;
      this._readLen = 0;
      if (this._appendStart != -1L && value < this._appendStart)
        throw new IOException(Environment.GetResourceString("IO.IO_SetLengthAppendTruncate"));
      this.SetLengthCore(value);
    }

    [SecuritySafeCritical]
    private void SetLengthCore(long value)
    {
      long pos = this._pos;
      if (this._exposedHandle)
        this.VerifyOSHandlePosition();
      if (this._pos != value)
        this.SeekCore(value, SeekOrigin.Begin);
      if (!Win32Native.SetEndOfFile(this._handle))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error == 87)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_FileLengthTooBig"));
        __Error.WinIOError(lastWin32Error, string.Empty);
      }
      if (pos == value)
        return;
      if (pos < value)
        this.SeekCore(pos, SeekOrigin.Begin);
      else
        this.SeekCore(0L, SeekOrigin.End);
    }

    /// <summary>
    ///   Выполняет чтение блока байтов из потока и запись данных в заданный буфер.
    /// </summary>
    /// <param name="array">
    ///   При возврате этот метод содержит указанный массив байтов, в котором значения в диапазоне от <paramref name="offset" /> до (<paramref name="offset" /> + <paramref name="count" /> - 1<paramref name=")" />) заменены байтами, считанными из текущего источника.
    /// </param>
    /// <param name="offset">
    ///   Смещение в байтах в массиве <paramref name="array" />, в который будут помещены считанные байты.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <returns>
    ///   Общее количество байтов, считанных в буфер.
    ///    Оно может быть меньше запрошенного числа байтов, если в настоящее время не имеется нужного количества байтов, или же равно нулю, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> и <paramref name="count" /> описывают недопустимый диапазон в <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [SecuritySafeCritical]
    public override int Read([In, Out] byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      bool flag = false;
      int byteCount = this._readLen - this._readPos;
      if (byteCount == 0)
      {
        if (!this.CanRead)
          __Error.ReadNotSupported();
        if (this._writePos > 0)
          this.FlushWrite(false);
        if (!this.CanSeek || count >= this._bufferSize)
        {
          int num = this.ReadCore(array, offset, count);
          this._readPos = 0;
          this._readLen = 0;
          return num;
        }
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        byteCount = this.ReadCore(this._buffer, 0, this._bufferSize);
        if (byteCount == 0)
          return 0;
        flag = byteCount < this._bufferSize;
        this._readPos = 0;
        this._readLen = byteCount;
      }
      if (byteCount > count)
        byteCount = count;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, byteCount);
      this._readPos += byteCount;
      if (!this._isPipe && byteCount < count && !flag)
      {
        int num = this.ReadCore(array, offset + byteCount, count - byteCount);
        byteCount += num;
        this._readPos = 0;
        this._readLen = 0;
      }
      return byteCount;
    }

    [SecuritySafeCritical]
    private unsafe int ReadCore(byte[] buffer, int offset, int count)
    {
      if (this._isAsync)
        return this.EndRead((IAsyncResult) this.BeginReadCore(buffer, offset, count, (AsyncCallback) null, (object) null, 0));
      if (this._exposedHandle)
        this.VerifyOSHandlePosition();
      int hr = 0;
      int num = this.ReadFileNative(this._handle, buffer, offset, count, (NativeOverlapped*) null, out hr);
      if (num == -1)
      {
        if (hr == 109)
        {
          num = 0;
        }
        else
        {
          if (hr == 87)
            throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotSync"));
          __Error.WinIOError(hr, string.Empty);
        }
      }
      this._pos += (long) num;
      return num;
    }

    /// <summary>
    ///   Устанавливает текущее положение этого потока на заданное значение.
    /// </summary>
    /// <param name="offset">
    ///   Указатель относительно начальной точки <paramref name="origin" />, от которой начинается поиск.
    /// </param>
    /// <param name="origin">
    ///   Задает начальную, конечную или текущую позицию как опорную точку для <paramref name="offset" />, используя значение типа <see cref="T:System.IO.SeekOrigin" />.
    /// </param>
    /// <returns>Новая позиция в потоке.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск, например, если <see langword="FileStream" /> создан на основе вывода консоли или канала.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка поиска выполняется до начала потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [SecuritySafeCritical]
    public override long Seek(long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
        case SeekOrigin.Current:
        case SeekOrigin.End:
          if (this._handle.IsClosed)
            __Error.FileNotOpen();
          if (!this.CanSeek)
            __Error.SeekNotSupported();
          if (this._writePos > 0)
            this.FlushWrite(false);
          else if (origin == SeekOrigin.Current)
            offset -= (long) (this._readLen - this._readPos);
          if (this._exposedHandle)
            this.VerifyOSHandlePosition();
          long offset1 = this._pos + (long) (this._readPos - this._readLen);
          long num1 = this.SeekCore(offset, origin);
          if (this._appendStart != -1L && num1 < this._appendStart)
          {
            this.SeekCore(offset1, SeekOrigin.Begin);
            throw new IOException(Environment.GetResourceString("IO.IO_SeekAppendOverwrite"));
          }
          if (this._readLen > 0)
          {
            if (offset1 == num1)
            {
              if (this._readPos > 0)
              {
                Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) this._buffer, 0, this._readLen - this._readPos);
                this._readLen -= this._readPos;
                this._readPos = 0;
              }
              if (this._readLen > 0)
                this.SeekCore((long) this._readLen, SeekOrigin.Current);
            }
            else if (offset1 - (long) this._readPos < num1 && num1 < offset1 + (long) this._readLen - (long) this._readPos)
            {
              int num2 = (int) (num1 - offset1);
              Buffer.InternalBlockCopy((Array) this._buffer, this._readPos + num2, (Array) this._buffer, 0, this._readLen - (this._readPos + num2));
              this._readLen -= this._readPos + num2;
              this._readPos = 0;
              if (this._readLen > 0)
                this.SeekCore((long) this._readLen, SeekOrigin.Current);
            }
            else
            {
              this._readPos = 0;
              this._readLen = 0;
            }
          }
          return num1;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      }
    }

    [SecuritySafeCritical]
    private long SeekCore(long offset, SeekOrigin origin)
    {
      int hr = 0;
      long num = Win32Native.SetFilePointer(this._handle, offset, origin, out hr);
      if (num == -1L)
      {
        if (hr == 6)
          this._handle.Dispose();
        __Error.WinIOError(hr, string.Empty);
      }
      this._pos = num;
      return num;
    }

    private void VerifyOSHandlePosition()
    {
      if (!this.CanSeek || this.SeekCore(0L, SeekOrigin.Current) == this._pos)
        return;
      this._readPos = 0;
      this._readLen = 0;
      if (this._writePos > 0)
      {
        this._writePos = 0;
        throw new IOException(Environment.GetResourceString("IO.IO_FileStreamHandlePosition"));
      }
    }

    /// <summary>Записывает блок байтов в файловый поток.</summary>
    /// <param name="array">
    ///   Буфер, содержащий данные для записи в поток.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов (начиная с нуля) в объекте <paramref name="array" />, с которого начинается копирование байтов в поток.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> и <paramref name="count" /> описывают недопустимый диапазон в <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// 
    ///   -или-
    /// 
    ///   Возможно, другой поток вызвал непредвиденное изменение положения дескриптора файла операционной системы.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий экземпляр потока не поддерживает запись.
    /// </exception>
    [SecuritySafeCritical]
    public override void Write(byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._writePos == 0)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
      }
      if (this._writePos > 0)
      {
        int byteCount = this._bufferSize - this._writePos;
        if (byteCount > 0)
        {
          if (byteCount > count)
            byteCount = count;
          Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, byteCount);
          this._writePos += byteCount;
          if (count == byteCount)
            return;
          offset += byteCount;
          count -= byteCount;
        }
        if (this._isAsync)
          this.EndWrite((IAsyncResult) this.BeginWriteCore(this._buffer, 0, this._writePos, (AsyncCallback) null, (object) null));
        else
          this.WriteCore(this._buffer, 0, this._writePos);
        this._writePos = 0;
      }
      if (count >= this._bufferSize)
      {
        this.WriteCore(array, offset, count);
      }
      else
      {
        if (count == 0)
          return;
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
        this._writePos = count;
      }
    }

    [SecuritySafeCritical]
    private unsafe void WriteCore(byte[] buffer, int offset, int count)
    {
      if (this._isAsync)
      {
        this.EndWrite((IAsyncResult) this.BeginWriteCore(buffer, offset, count, (AsyncCallback) null, (object) null));
      }
      else
      {
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        int hr = 0;
        int num = this.WriteFileNative(this._handle, buffer, offset, count, (NativeOverlapped*) null, out hr);
        if (num == -1)
        {
          if (hr == 232)
          {
            num = 0;
          }
          else
          {
            if (hr == 87)
              throw new IOException(Environment.GetResourceString("IO.IO_FileTooLongOrHandleNotSync"));
            __Error.WinIOError(hr, string.Empty);
          }
        }
        this._pos += (long) num;
      }
    }

    /// <summary>
    ///   Начинает операцию асинхронного чтения.
    ///    (Попробуйте вместо этого использовать <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="array">
    ///   Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение в <paramref name="array" /> (в байтах), с которого начинается чтение.
    /// </param>
    /// <param name="numBytes">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <param name="userCallback">
    ///   Метод, вызываемый после завершения операции асинхронного чтения.
    /// </param>
    /// <param name="stateObject">
    ///   Предоставляемый пользователем объект, являющийся отличительным признаком данного конкретного запроса на асинхронное чтение от других запросов.
    /// </param>
    /// <returns>Объект, который ссылается на асинхронное чтение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина массива минус <paramref name="offset" /> меньше <paramref name="numBytes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="numBytes" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Предпринята попытка асинхронного чтения за пределами файла.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (numBytes < 0)
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < numBytes)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
      return (IAsyncResult) this.BeginReadAsync(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private FileStreamAsyncResult BeginReadAsync(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (!this.CanRead)
        __Error.ReadNotSupported();
      if (this._isPipe)
      {
        if (this._readPos >= this._readLen)
          return this.BeginReadCore(array, offset, numBytes, userCallback, stateObject, 0);
        int num = this._readLen - this._readPos;
        if (num > numBytes)
          num = numBytes;
        Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, num);
        this._readPos += num;
        return FileStreamAsyncResult.CreateBufferedReadResult(num, userCallback, stateObject, false);
      }
      if (this._writePos > 0)
        this.FlushWrite(false);
      if (this._readPos == this._readLen)
      {
        if (numBytes < this._bufferSize)
        {
          if (this._buffer == null)
            this._buffer = new byte[this._bufferSize];
          this._readLen = this.EndRead((IAsyncResult) this.BeginReadCore(this._buffer, 0, this._bufferSize, (AsyncCallback) null, (object) null, 0));
          int num = this._readLen;
          if (num > numBytes)
            num = numBytes;
          Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) array, offset, num);
          this._readPos = num;
          return FileStreamAsyncResult.CreateBufferedReadResult(num, userCallback, stateObject, false);
        }
        this._readPos = 0;
        this._readLen = 0;
        return this.BeginReadCore(array, offset, numBytes, userCallback, stateObject, 0);
      }
      int num1 = this._readLen - this._readPos;
      if (num1 > numBytes)
        num1 = numBytes;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, num1);
      this._readPos += num1;
      if (num1 >= numBytes)
        return FileStreamAsyncResult.CreateBufferedReadResult(num1, userCallback, stateObject, false);
      this._readPos = 0;
      this._readLen = 0;
      return this.BeginReadCore(array, offset + num1, numBytes - num1, userCallback, stateObject, num1);
    }

    [SecuritySafeCritical]
    private unsafe FileStreamAsyncResult BeginReadCore(byte[] bytes, int offset, int numBytes, AsyncCallback userCallback, object stateObject, int numBufferedBytesRead)
    {
      FileStreamAsyncResult streamAsyncResult = new FileStreamAsyncResult(numBufferedBytesRead, bytes, this._handle, userCallback, stateObject, false);
      NativeOverlapped* overLapped = streamAsyncResult.OverLapped;
      if (this.CanSeek)
      {
        long length = this.Length;
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        if (this._pos + (long) numBytes > length)
          numBytes = this._pos > length ? 0 : (int) (length - this._pos);
        overLapped->OffsetLow = (int) this._pos;
        overLapped->OffsetHigh = (int) (this._pos >> 32);
        this.SeekCore((long) numBytes, SeekOrigin.Current);
      }
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferSend((long) streamAsyncResult.OverLapped, 2, string.Empty, false);
      int hr = 0;
      if (this.ReadFileNative(this._handle, bytes, offset, numBytes, overLapped, out hr) == -1 && numBytes != -1)
      {
        switch (hr)
        {
          case 109:
            overLapped->InternalLow = IntPtr.Zero;
            streamAsyncResult.CallUserCallback();
            break;
          case 997:
            break;
          default:
            if (!this._handle.IsClosed && this.CanSeek)
              this.SeekCore(0L, SeekOrigin.Current);
            if (hr == 38)
            {
              __Error.EndOfFile();
              break;
            }
            __Error.WinIOError(hr, string.Empty);
            break;
        }
      }
      return streamAsyncResult;
    }

    /// <summary>
    ///   Ожидает завершения отложенной асинхронной операции чтения.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">
    ///   Ссылка на ожидаемый отложенный асинхронный запрос.
    /// </param>
    /// <returns>
    ///   Число байтов, считанных из потока, находится между 0 и числом запрошенных байтов.
    ///    Потоки возвращают 0 только в конце потока, в противном случае они должны блокироваться, пока не будет доступен по крайней мере 1 байт.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот объект <see cref="T:System.IAsyncResult" /> не был создан путем вызова <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> для данного класса.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> вызывается несколько раз.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт, или произошла внутренняя ошибка.
    /// </exception>
    [SecuritySafeCritical]
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (!this._isAsync)
        return base.EndRead(asyncResult);
      FileStreamAsyncResult streamAsyncResult = asyncResult as FileStreamAsyncResult;
      if (streamAsyncResult == null || streamAsyncResult.IsWrite)
        __Error.WrongAsyncResult();
      if (1 == Interlocked.CompareExchange(ref streamAsyncResult._EndXxxCalled, 1, 0))
        __Error.EndReadCalledTwice();
      streamAsyncResult.Wait();
      streamAsyncResult.ReleaseNativeResource();
      if (streamAsyncResult.ErrorCode != 0)
        __Error.WinIOError(streamAsyncResult.ErrorCode, string.Empty);
      return streamAsyncResult.NumBytesRead;
    }

    /// <summary>
    ///   Считывает байт из файла и перемещает положение чтения на один байт.
    /// </summary>
    /// <returns>
    ///   Байт приводится к типу <see cref="T:System.Int32" /> или -1, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток закрыт.
    /// </exception>
    [SecuritySafeCritical]
    public override int ReadByte()
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._readLen == 0 && !this.CanRead)
        __Error.ReadNotSupported();
      if (this._readPos == this._readLen)
      {
        if (this._writePos > 0)
          this.FlushWrite(false);
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        this._readLen = this.ReadCore(this._buffer, 0, this._bufferSize);
        this._readPos = 0;
      }
      if (this._readPos == this._readLen)
        return -1;
      int num = (int) this._buffer[this._readPos];
      ++this._readPos;
      return num;
    }

    /// <summary>
    ///   Начинает операцию асинхронной записи.
    ///    (Попробуйте вместо этого использовать <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="array">
    ///   Буфер, содержащий данные для записи в текущий поток.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемое от нуля смещение байтов в буфере <paramref name="array" />, с которого начинается копирование байтов в текущий поток.
    /// </param>
    /// <param name="numBytes">
    ///   Максимальное число байтов для записи.
    /// </param>
    /// <param name="userCallback">
    ///   Метод, вызываемый после завершения операции асинхронной записи.
    /// </param>
    /// <param name="stateObject">
    ///   Предоставляемый пользователем объект, являющийся отличительным признаком данного конкретного запроса на асинхронную запись от других запросов.
    /// </param>
    /// <returns>Объект, который ссылается на асинхронную запись.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина массива <paramref name="array" /> минус <paramref name="offset" /> меньше <paramref name="numBytes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="numBytes" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (numBytes < 0)
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < numBytes)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
      return (IAsyncResult) this.BeginWriteAsync(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private FileStreamAsyncResult BeginWriteAsync(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (this._isPipe)
      {
        if (this._writePos > 0)
          this.FlushWrite(false);
        return this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
      }
      if (this._writePos == 0)
      {
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
      }
      int num = this._bufferSize - this._writePos;
      if (numBytes <= num)
      {
        if (this._writePos == 0)
          this._buffer = new byte[this._bufferSize];
        Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, numBytes);
        this._writePos += numBytes;
        return FileStreamAsyncResult.CreateBufferedReadResult(numBytes, userCallback, stateObject, true);
      }
      if (this._writePos > 0)
        this.FlushWrite(false);
      return this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    private unsafe FileStreamAsyncResult BeginWriteCore(byte[] bytes, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      FileStreamAsyncResult streamAsyncResult = new FileStreamAsyncResult(0, bytes, this._handle, userCallback, stateObject, true);
      NativeOverlapped* overLapped = streamAsyncResult.OverLapped;
      if (this.CanSeek)
      {
        long length = this.Length;
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        if (this._pos + (long) numBytes > length)
          this.SetLengthCore(this._pos + (long) numBytes);
        overLapped->OffsetLow = (int) this._pos;
        overLapped->OffsetHigh = (int) (this._pos >> 32);
        this.SeekCore((long) numBytes, SeekOrigin.Current);
      }
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferSend((long) streamAsyncResult.OverLapped, 2, string.Empty, false);
      int hr = 0;
      if (this.WriteFileNative(this._handle, bytes, offset, numBytes, overLapped, out hr) == -1 && numBytes != -1)
      {
        switch (hr)
        {
          case 232:
            streamAsyncResult.CallUserCallback();
            break;
          case 997:
            break;
          default:
            if (!this._handle.IsClosed && this.CanSeek)
              this.SeekCore(0L, SeekOrigin.Current);
            if (hr == 38)
            {
              __Error.EndOfFile();
              break;
            }
            __Error.WinIOError(hr, string.Empty);
            break;
        }
      }
      return streamAsyncResult;
    }

    /// <summary>
    ///   Завершает асинхронную операцию записи и блокирует до тех пор, пока не будет завершена операция ввода-вывода.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">
    ///   Отложенный асинхронный запрос ввода-вывода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот объект <see cref="T:System.IAsyncResult" /> не был создан путем вызова <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> для данного класса.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> вызывается несколько раз.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт, или произошла внутренняя ошибка.
    /// </exception>
    [SecuritySafeCritical]
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (!this._isAsync)
      {
        base.EndWrite(asyncResult);
      }
      else
      {
        FileStreamAsyncResult streamAsyncResult = asyncResult as FileStreamAsyncResult;
        if (streamAsyncResult == null || !streamAsyncResult.IsWrite)
          __Error.WrongAsyncResult();
        if (1 == Interlocked.CompareExchange(ref streamAsyncResult._EndXxxCalled, 1, 0))
          __Error.EndWriteCalledTwice();
        streamAsyncResult.Wait();
        streamAsyncResult.ReleaseNativeResource();
        if (streamAsyncResult.ErrorCode == 0)
          return;
        __Error.WinIOError(streamAsyncResult.ErrorCode, string.Empty);
      }
    }

    /// <summary>Запись байта в текущую позицию в потоке файла.</summary>
    /// <param name="value">
    ///   Байт, который необходимо записать в поток.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    [SecuritySafeCritical]
    public override void WriteByte(byte value)
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._writePos == 0)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
      }
      if (this._writePos == this._bufferSize)
        this.FlushWrite(false);
      this._buffer[this._writePos] = value;
      ++this._writePos;
    }

    /// <summary>
    ///   Запрещает другим процессам чтение объекта <see cref="T:System.IO.FileStream" /> и запись в этот объект.
    /// </summary>
    /// <param name="position">
    ///   Начало диапазона для блокировки.
    ///    Значение этого параметра должно быть больше или равно нулю (0).
    /// </param>
    /// <param name="length">Диапазон, который нужно блокировать.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> или <paramref name="length" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Файл закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Процессу не удается получить доступ к файлу, так как другой процесс заблокировал часть этого файла.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Lock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? nameof (position) : nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (Win32Native.LockFile(this._handle, (int) position, (int) (position >> 32), (int) length, (int) (length >> 32)))
        return;
      __Error.WinIOError();
    }

    /// <summary>
    ///   Разрешает доступ других процессов ко всему ранее заблокированному файлу или его части.
    /// </summary>
    /// <param name="position">
    ///   Начало диапазона, который должен быть разблокирован.
    /// </param>
    /// <param name="length">
    ///   Диапазон, который должен быть разблокирован.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> или <paramref name="length" /> является отрицательным значением.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Unlock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? nameof (position) : nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (Win32Native.UnlockFile(this._handle, (int) position, (int) (position >> 32), (int) length, (int) (length >> 32)))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    private unsafe int ReadFileNative(SafeFileHandle handle, byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
    {
      if (bytes.Length - offset < count)
        throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_IORaceCondition"));
      if (bytes.Length == 0)
      {
        hr = 0;
        return 0;
      }
      int numBytesRead = 0;
      int num;
      fixed (byte* numPtr = bytes)
        num = !this._isAsync ? Win32Native.ReadFile(handle, numPtr + offset, count, out numBytesRead, IntPtr.Zero) : Win32Native.ReadFile(handle, numPtr + offset, count, IntPtr.Zero, overlapped);
      if (num == 0)
      {
        hr = Marshal.GetLastWin32Error();
        if (hr == 109 || hr == 233 || hr != 6)
          return -1;
        this._handle.Dispose();
        return -1;
      }
      hr = 0;
      return numBytesRead;
    }

    [SecurityCritical]
    private unsafe int WriteFileNative(SafeFileHandle handle, byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
    {
      if (bytes.Length - offset < count)
        throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_IORaceCondition"));
      if (bytes.Length == 0)
      {
        hr = 0;
        return 0;
      }
      int numBytesWritten = 0;
      int num;
      fixed (byte* numPtr = bytes)
        num = !this._isAsync ? Win32Native.WriteFile(handle, numPtr + offset, count, out numBytesWritten, IntPtr.Zero) : Win32Native.WriteFile(handle, numPtr + offset, count, IntPtr.Zero, overlapped);
      if (num == 0)
      {
        hr = Marshal.GetLastWin32Error();
        if (hr == 232 || hr != 6)
          return -1;
        this._handle.Dispose();
        return -1;
      }
      hr = 0;
      return numBytesWritten;
    }

    /// <summary>
    ///   Асинхронно считывает последовательность байтов из текущего потока, перемещает позицию в потоке на число считанных байтов и отслеживает запросы отмены.
    /// </summary>
    /// <param name="buffer">Буфер, в который записываются данные.</param>
    /// <param name="offset">
    ///   Смещение байтов в <paramref name="buffer" />, с которого начинается запись данных из потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число байтов, считанных в буфер.
    ///    Значение результата может быть меньше запрошенного числа байтов, если число доступных в данный момент байтов меньше запрошенного числа, или результат может быть равен 0 (нулю), если был достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="offset" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Поток в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (FileStream))
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      FileStream.FileStreamReadWriteTask<int> streamReadWriteTask = new FileStream.FileStreamReadWriteTask<int>(cancellationToken);
      AsyncCallback userCallback = FileStream.s_endReadTask;
      if (userCallback == null)
        FileStream.s_endReadTask = userCallback = new AsyncCallback(FileStream.EndReadTask);
      streamReadWriteTask._asyncResult = this.BeginReadAsync(buffer, offset, count, userCallback, (object) streamReadWriteTask);
      if (streamReadWriteTask._asyncResult.IsAsync && cancellationToken.CanBeCanceled)
      {
        Action<object> callback = FileStream.s_cancelReadHandler;
        if (callback == null)
          FileStream.s_cancelReadHandler = callback = new Action<object>(FileStream.CancelTask<int>);
        streamReadWriteTask._registration = cancellationToken.Register(callback, (object) streamReadWriteTask);
        if (streamReadWriteTask._asyncResult.IsCompleted)
          streamReadWriteTask._registration.Dispose();
      }
      return (Task<int>) streamReadWriteTask;
    }

    /// <summary>
    ///   Асинхронно записывает последовательность байтов в текущий поток, перемещает текущую позицию внутри потока на число записанных байтов и отслеживает запросы отмены.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, из которого записываются данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов (начиная с нуля) в объекте <paramref name="buffer" />, с которого начинается копирование байтов в поток.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="offset" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Поток в настоящее время используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (FileStream))
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      FileStream.FileStreamReadWriteTask<VoidTaskResult> streamReadWriteTask = new FileStream.FileStreamReadWriteTask<VoidTaskResult>(cancellationToken);
      AsyncCallback userCallback = FileStream.s_endWriteTask;
      if (userCallback == null)
        FileStream.s_endWriteTask = userCallback = new AsyncCallback(FileStream.EndWriteTask);
      streamReadWriteTask._asyncResult = this.BeginWriteAsync(buffer, offset, count, userCallback, (object) streamReadWriteTask);
      if (streamReadWriteTask._asyncResult.IsAsync && cancellationToken.CanBeCanceled)
      {
        Action<object> callback = FileStream.s_cancelWriteHandler;
        if (callback == null)
          FileStream.s_cancelWriteHandler = callback = new Action<object>(FileStream.CancelTask<VoidTaskResult>);
        streamReadWriteTask._registration = cancellationToken.Register(callback, (object) streamReadWriteTask);
        if (streamReadWriteTask._asyncResult.IsCompleted)
          streamReadWriteTask._registration.Dispose();
      }
      return (Task) streamReadWriteTask;
    }

    [SecuritySafeCritical]
    private static void CancelTask<T>(object state)
    {
      FileStream.FileStreamReadWriteTask<T> streamReadWriteTask = state as FileStream.FileStreamReadWriteTask<T>;
      FileStreamAsyncResult asyncResult = streamReadWriteTask._asyncResult;
      try
      {
        if (asyncResult.IsCompleted)
          return;
        asyncResult.Cancel();
      }
      catch (Exception ex)
      {
        streamReadWriteTask.TrySetException((object) ex);
      }
    }

    [SecuritySafeCritical]
    private static void EndReadTask(IAsyncResult iar)
    {
      FileStreamAsyncResult streamAsyncResult = iar as FileStreamAsyncResult;
      FileStream.FileStreamReadWriteTask<int> asyncState = streamAsyncResult.AsyncState as FileStream.FileStreamReadWriteTask<int>;
      try
      {
        if (streamAsyncResult.IsAsync)
        {
          streamAsyncResult.ReleaseNativeResource();
          asyncState._registration.Dispose();
        }
        if (streamAsyncResult.ErrorCode == 995)
        {
          CancellationToken cancellationToken = asyncState._cancellationToken;
          asyncState.TrySetCanceled(cancellationToken);
        }
        else
          asyncState.TrySetResult(streamAsyncResult.NumBytesRead);
      }
      catch (Exception ex)
      {
        asyncState.TrySetException((object) ex);
      }
    }

    [SecuritySafeCritical]
    private static void EndWriteTask(IAsyncResult iar)
    {
      FileStreamAsyncResult streamAsyncResult = iar as FileStreamAsyncResult;
      FileStream.FileStreamReadWriteTask<VoidTaskResult> asyncState = iar.AsyncState as FileStream.FileStreamReadWriteTask<VoidTaskResult>;
      try
      {
        if (streamAsyncResult.IsAsync)
        {
          streamAsyncResult.ReleaseNativeResource();
          asyncState._registration.Dispose();
        }
        if (streamAsyncResult.ErrorCode == 995)
        {
          CancellationToken cancellationToken = asyncState._cancellationToken;
          asyncState.TrySetCanceled(cancellationToken);
        }
        else
          asyncState.TrySetResult(new VoidTaskResult());
      }
      catch (Exception ex)
      {
        asyncState.TrySetException((object) ex);
      }
    }

    /// <summary>
    ///   Асинхронно очищает все буферы данного потока, вызывает запись буферизованных данных в базовое устройство и отслеживает запросы отмены.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (this.GetType() != typeof (FileStream))
        return base.FlushAsync(cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      try
      {
        this.FlushInternalBuffer();
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
      if (!this.CanWrite)
        return Task.CompletedTask;
      TaskFactory factory = Task.Factory;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = 8;
      TaskScheduler scheduler = TaskScheduler.Default;
      return factory.StartNew((Action<object>) (state => ((FileStream) state).FlushOSBuffer()), (object) this, cancellationToken1, (TaskCreationOptions) num, scheduler);
    }

    private sealed class FileStreamReadWriteTask<T> : Task<T>
    {
      internal CancellationToken _cancellationToken;
      internal CancellationTokenRegistration _registration;
      internal FileStreamAsyncResult _asyncResult;

      internal FileStreamReadWriteTask(CancellationToken cancellationToken)
      {
        this._cancellationToken = cancellationToken;
      }
    }
  }
}
