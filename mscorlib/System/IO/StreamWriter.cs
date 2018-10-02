// Decompiled with JetBrains decompiler
// Type: System.IO.StreamWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Реализует <see cref="T:System.IO.TextWriter" /> для записи символов в поток в определенной кодировке.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StreamWriter : TextWriter
  {
    /// <summary>
    ///   Предоставляет <see langword="StreamWriter" /> без резервного хранилища, в который можно осуществлять запись, но из которого нельзя считывать данные.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly StreamWriter Null = new StreamWriter(Stream.Null, (Encoding) new UTF8Encoding(false, true), 128, true);
    internal const int DefaultBufferSize = 1024;
    private const int DefaultFileStreamBufferSize = 4096;
    private const int MinBufferSize = 128;
    private const int DontCopyOnWriteLineThreshold = 512;
    private Stream stream;
    private Encoding encoding;
    private Encoder encoder;
    private byte[] byteBuffer;
    private char[] charBuffer;
    private int charPos;
    private int charLen;
    private bool autoFlush;
    private bool haveWrittenPreamble;
    private bool closable;
    [NonSerialized]
    private StreamWriter.MdaHelper mdaHelper;
    [NonSerialized]
    private volatile Task _asyncWriteTask;
    private static volatile Encoding _UTF8NoBOM;

    private void CheckAsyncTaskInProgress()
    {
      Task asyncWriteTask = this._asyncWriteTask;
      if (asyncWriteTask != null && !asyncWriteTask.IsCompleted)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
    }

    internal static Encoding UTF8NoBOM
    {
      [FriendAccessAllowed] get
      {
        if (StreamWriter._UTF8NoBOM == null)
        {
          UTF8Encoding utF8Encoding = new UTF8Encoding(false, true);
          Thread.MemoryBarrier();
          StreamWriter._UTF8NoBOM = (Encoding) utF8Encoding;
        }
        return StreamWriter._UTF8NoBOM;
      }
    }

    internal StreamWriter()
      : base((IFormatProvider) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного потока, используя кодировку UTF-8 и размер буфера по умолчанию.
    /// </summary>
    /// <param name="stream">
    ///   Поток, в который требуется выполнить запись.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не может быть перезаписан.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream)
      : this(stream, StreamWriter.UTF8NoBOM, 1024, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного потока, используя заданную кодировку и размер буфера по умолчанию.
    /// </summary>
    /// <param name="stream">
    ///   Поток, в который требуется выполнить запись.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не может быть перезаписан.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding)
      : this(stream, encoding, 1024, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного потока, используя заданную кодировку и размер буфера.
    /// </summary>
    /// <param name="stream">
    ///   Поток, в который требуется выполнить запись.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="bufferSize">Размер буфера в байтах.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bufferSize" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не может быть перезаписан.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
      : this(stream, encoding, bufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного потока, используя заданную кодировку и размер буфера и при необходимости оставляет поток открытым.
    /// </summary>
    /// <param name="stream">
    ///   Поток, в который требуется выполнить запись.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="bufferSize">Размер буфера в байтах.</param>
    /// <param name="leaveOpen">
    ///   Значение <see langword="true" />, чтобы оставить поток открытым после удаления объекта <see cref="T:System.IO.StreamWriter" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bufferSize" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не может быть перезаписан.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
      : base((IFormatProvider) null)
    {
      if (stream == null || encoding == null)
        throw new ArgumentNullException(stream == null ? nameof (stream) : nameof (encoding));
      if (!stream.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(stream, encoding, bufferSize, leaveOpen);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного файла, используя кодировку и размер буфера.
    /// </summary>
    /// <param name="path">
    ///   Полный путь к файлу для записи.
    ///   <paramref name="path" /> может быть именем файла.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> содержит имя системного устройства (com1, com2 и т. д.).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 символов, а длина имен файлов — 260 символов.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> включает неправильный или недопустимый синтаксис имени файла, имени папки или синтаксис метки тома.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public StreamWriter(string path)
      : this(path, false, StreamWriter.UTF8NoBOM, 1024)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного файла, используя кодировку и размер буфера.
    ///    Если файл существует, он может быть либо перезаписан, либо в него могут быть добавлены данные.
    ///    Если файл не существует, конструктор создает новый файл.
    /// </summary>
    /// <param name="path">Полный путь к файлу для записи.</param>
    /// <param name="append">
    ///   <see langword="true" /> для добавления данных в файл. <see langword="false" /> Перезаписать файл.
    ///    Если указанный файл не существует, этот параметр не действует и конструктор создает новый файл.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> пуст.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> содержит имя системного устройства (com1, com2 и т. д.).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> включает неправильный или недопустимый синтаксис имени файла, имени папки или синтаксис метки тома.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 символов, а длина имен файлов — 260 символов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public StreamWriter(string path, bool append)
      : this(path, append, StreamWriter.UTF8NoBOM, 1024)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> класса для указанного файла, используя указанный размер буфера кодирования и по умолчанию.
    ///    Если файл существует, он может быть либо перезаписан, либо в него могут быть добавлены данные.
    ///    Если файл не существует, конструктор создает новый файл.
    /// </summary>
    /// <param name="path">Полный путь к файлу для записи.</param>
    /// <param name="append">
    ///   <see langword="true" /> для добавления данных в файл. <see langword="false" /> Перезаписать файл.
    ///    Если указанный файл не существует, этот параметр не действует и конструктор создает новый файл.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> пуст.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> содержит имя системного устройства (com1, com2 и т. д.).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> включает неправильный или недопустимый синтаксис имени файла, имени папки или синтаксис метки тома.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 символов, а длина имен файлов — 260 символов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public StreamWriter(string path, bool append, Encoding encoding)
      : this(path, append, encoding, 1024)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StreamWriter" /> для указанного файла по заданному пути, используя заданную кодировку и размер буфера.
    ///    Если файл существует, он может быть либо перезаписан, либо в него могут быть добавлены данные.
    ///    Если файл не существует, конструктор создает новый файл.
    /// </summary>
    /// <param name="path">Полный путь к файлу для записи.</param>
    /// <param name="append">
    ///   <see langword="true" /> для добавления данных в файл. <see langword="false" /> Перезаписать файл.
    ///    Если указанный файл не существует, этот параметр не действует и конструктор создает новый файл.
    /// </param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="bufferSize">Размер буфера в байтах.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="path" /> содержит имя системного устройства (com1, com2 и т. д.).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bufferSize" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> включает неправильный или недопустимый синтаксис имени файла, имени папки или синтаксис метки тома.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Отказано в доступе.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути не должна превышать 248 символов, а длина имен файлов — 260 символов.
    /// </exception>
    [SecuritySafeCritical]
    public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
      : this(path, append, encoding, bufferSize, true)
    {
    }

    [SecurityCritical]
    internal StreamWriter(string path, bool append, Encoding encoding, int bufferSize, bool checkHost)
      : base((IFormatProvider) null)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(StreamWriter.CreateFile(path, append, checkHost), encoding, bufferSize, false);
    }

    [SecuritySafeCritical]
    private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
    {
      this.stream = streamArg;
      this.encoding = encodingArg;
      this.encoder = this.encoding.GetEncoder();
      if (bufferSize < 128)
        bufferSize = 128;
      this.charBuffer = new char[bufferSize];
      this.byteBuffer = new byte[this.encoding.GetMaxByteCount(bufferSize)];
      this.charLen = bufferSize;
      if (this.stream.CanSeek && this.stream.Position > 0L)
        this.haveWrittenPreamble = true;
      this.closable = !shouldLeaveOpen;
      if (!Mda.StreamWriterBufferedDataLost.Enabled)
        return;
      string cs = (string) null;
      if (Mda.StreamWriterBufferedDataLost.CaptureAllocatedCallStack)
        cs = Environment.GetStackTrace((Exception) null, false);
      this.mdaHelper = new StreamWriter.MdaHelper(this, cs);
    }

    [SecurityCritical]
    private static Stream CreateFile(string path, bool append, bool checkHost)
    {
      FileMode mode = append ? FileMode.Append : FileMode.Create;
      return (Stream) new FileStream(path, mode, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
    }

    /// <summary>
    ///   Закрывает текущий <see langword="StreamWriter" /> объекта и основной поток.
    /// </summary>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Текущая кодировка не поддерживает отображение половины суррогатной пары Юникода.
    /// </exception>
    public override void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.StreamWriter" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Текущая кодировка не поддерживает отображение половины суррогатной пары Юникода.
    /// </exception>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this.stream == null || !disposing && (!this.LeaveOpen || !(this.stream is __ConsoleStream)))
          return;
        this.CheckAsyncTaskInProgress();
        this.Flush(true, true);
        if (this.mdaHelper == null)
          return;
        GC.SuppressFinalize((object) this.mdaHelper);
      }
      finally
      {
        if (!this.LeaveOpen)
        {
          if (this.stream != null)
          {
            try
            {
              if (disposing)
                this.stream.Close();
            }
            finally
            {
              this.stream = (Stream) null;
              this.byteBuffer = (byte[]) null;
              this.charBuffer = (char[]) null;
              this.encoding = (Encoding) null;
              this.encoder = (Encoder) null;
              this.charLen = 0;
              base.Dispose(disposing);
            }
          }
        }
      }
    }

    /// <summary>
    ///   Очищает все буферы для текущего средства записи и вызывает запись всех данных буфера в основной поток.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство записи закрывается.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Текущая кодировка не поддерживает отображение половины суррогатной пары Юникода.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Flush()
    {
      this.CheckAsyncTaskInProgress();
      this.Flush(true, true);
    }

    private void Flush(bool flushStream, bool flushEncoder)
    {
      if (this.stream == null)
        __Error.WriterClosed();
      if (this.charPos == 0 && (!flushStream && !flushEncoder || CompatibilitySwitches.IsAppEarlierThanWindowsPhone8))
        return;
      if (!this.haveWrittenPreamble)
      {
        this.haveWrittenPreamble = true;
        byte[] preamble = this.encoding.GetPreamble();
        if (preamble.Length != 0)
          this.stream.Write(preamble, 0, preamble.Length);
      }
      int bytes = this.encoder.GetBytes(this.charBuffer, 0, this.charPos, this.byteBuffer, 0, flushEncoder);
      this.charPos = 0;
      if (bytes > 0)
        this.stream.Write(this.byteBuffer, 0, bytes);
      if (!flushStream)
        return;
      this.stream.Flush();
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, является ли <see cref="T:System.IO.StreamWriter" /> сбрасывать буфер в основной поток после каждого вызова <see cref="M:System.IO.StreamWriter.Write(System.Char)" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Чтобы принудительно <see cref="T:System.IO.StreamWriter" /> сбросить буфер; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool AutoFlush
    {
      [__DynamicallyInvokable] get
      {
        return this.autoFlush;
      }
      [__DynamicallyInvokable] set
      {
        this.CheckAsyncTaskInProgress();
        this.autoFlush = value;
        if (!value)
          return;
        this.Flush(true, false);
      }
    }

    /// <summary>
    ///   Получает основной поток, связанный с резервным хранилищем.
    /// </summary>
    /// <returns>
    ///   Поток это <see langword="StreamWriter" /> производится запись.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        return this.stream;
      }
    }

    internal bool LeaveOpen
    {
      get
      {
        return !this.closable;
      }
    }

    internal bool HaveWrittenPreamble
    {
      set
      {
        this.haveWrittenPreamble = value;
      }
    }

    /// <summary>
    ///   Получает кодировку <see cref="T:System.Text.Encoding" />, в которой осуществляется запись выходных данных.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Text.Encoding" /> Указанный в конструкторе для текущего экземпляра, или <see cref="T:System.Text.UTF8Encoding" /> Если кодировка не задана.
    /// </returns>
    [__DynamicallyInvokable]
    public override Encoding Encoding
    {
      [__DynamicallyInvokable] get
      {
        return this.encoding;
      }
    }

    /// <summary>Записывает символ в поток.</summary>
    /// <param name="value">Символ, записываемый в поток.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и текущее средство записи закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и содержимое буфера не может быть записано основной поток фиксированного размера, поскольку <see cref="T:System.IO.StreamWriter" /> находится в конце потока.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(char value)
    {
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen)
        this.Flush(false, false);
      this.charBuffer[this.charPos] = value;
      ++this.charPos;
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>Записывает в поток массив символов.</summary>
    /// <param name="buffer">
    ///   Массив символов, содержащий записываемые в поток данные.
    ///    Если <paramref name="buffer" /> имеет значение <see langword="null" />, запись не выполняется.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и текущее средство записи закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и содержимое буфера не может быть записано основной поток фиксированного размера, поскольку <see cref="T:System.IO.StreamWriter" /> находится в конце потока.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(char[] buffer)
    {
      if (buffer == null)
        return;
      this.CheckAsyncTaskInProgress();
      int num1 = 0;
      int length = buffer.Length;
      while (length > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int num2 = this.charLen - this.charPos;
        if (num2 > length)
          num2 = length;
        Buffer.InternalBlockCopy((Array) buffer, num1 * 2, (Array) this.charBuffer, this.charPos * 2, num2 * 2);
        this.charPos += num2;
        num1 += num2;
        length -= num2;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>Записывает в поток дочерний массив символов.</summary>
    /// <param name="buffer">
    ///   Массив символов, содержащий записываемые данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере, с которого начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и текущее средство записи закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и содержимое буфера не может быть записано основной поток фиксированного размера, поскольку <see cref="T:System.IO.StreamWriter" /> находится в конце потока.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this.CheckAsyncTaskInProgress();
      while (count > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int num = this.charLen - this.charPos;
        if (num > count)
          num = count;
        Buffer.InternalBlockCopy((Array) buffer, index * 2, (Array) this.charBuffer, this.charPos * 2, num * 2);
        this.charPos += num;
        index += num;
        count -= num;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>Записывает в поток строку.</summary>
    /// <param name="value">
    ///   Строка, записываемая в поток.
    ///    Если <paramref name="value" /> имеет значение null, ничего не записывается.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и текущее средство записи закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> имеет значение true или <see cref="T:System.IO.StreamWriter" /> буфер заполнен, и содержимое буфера не может быть записано основной поток фиксированного размера, поскольку <see cref="T:System.IO.StreamWriter" /> находится в конце потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(string value)
    {
      if (value == null)
        return;
      this.CheckAsyncTaskInProgress();
      int length = value.Length;
      int sourceIndex = 0;
      while (length > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int count = this.charLen - this.charPos;
        if (count > length)
          count = length;
        value.CopyTo(sourceIndex, this.charBuffer, this.charPos, count);
        this.charPos += count;
        sourceIndex += count;
        length -= count;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>Асинхронно записывает символ в поток.</summary>
    /// <param name="value">Символ, записываемый в поток.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      if (charPos == charLen)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      charBuffer[charPos] = value;
      ++charPos;
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>Асинхронно записывает строку в поток.</summary>
    /// <param name="value">
    ///   Строка, записываемая в поток.
    ///    Если <paramref name="value" /> имеет значение <see langword="null" />, запись не выполняется.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(string value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(value);
      if (value == null)
        return Task.CompletedTask;
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      int count = value.Length;
      int index = 0;
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      while (count > 0)
      {
        if (charPos == charLen)
        {
          configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
          await configuredTaskAwaitable;
          charPos = 0;
        }
        int count1 = charLen - charPos;
        if (count1 > count)
          count1 = count;
        value.CopyTo(index, charBuffer, charPos, count1);
        charPos += count1;
        index += count1;
        count -= count1;
      }
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>
    ///   Асинхронно записывает дочерний массив символов в поток.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, содержащий записываемые данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере с которого начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(buffer, index, count);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, char[] buffer, int index, int count, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      while (count > 0)
      {
        if (charPos == charLen)
        {
          configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
          await configuredTaskAwaitable;
          charPos = 0;
        }
        int num = charLen - charPos;
        if (num > count)
          num = count;
        Buffer.InternalBlockCopy((Array) buffer, index * 2, (Array) charBuffer, charPos * 2, num * 2);
        charPos += num;
        index += num;
        count -= num;
      }
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>Асинхронно записывает в поток признак конца строки.</summary>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync()
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync();
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, (char[]) null, 0, 0, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>
    ///   Асинхронно записывает в поток символ, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">Символ, записываемый в поток.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>
    ///   Асинхронно записывает в поток строку, за которой следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если значение равно <see langword="null" />, записывается только знак конца строки.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(string value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>
    ///   Асинхронного записывает в поток дочерний массив символов, за которыми следует признак конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере, с которого начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Удален модуль записи потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи потока в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(buffer, index, count);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>
    ///   Асинхронно очищает все буферы для этого потока и вызывает запись всех буферизованных данных в базовое устройство.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync()
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.FlushAsync();
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = this.FlushAsyncInternal(true, true, this.charBuffer, this.charPos);
      this._asyncWriteTask = task;
      return task;
    }

    private int CharPos_Prop
    {
      set
      {
        this.charPos = value;
      }
    }

    private bool HaveWrittenPreamble_Prop
    {
      set
      {
        this.haveWrittenPreamble = value;
      }
    }

    private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos)
    {
      if (sCharPos == 0 && !flushStream && !flushEncoder)
        return Task.CompletedTask;
      Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this.haveWrittenPreamble, this.encoding, this.encoder, this.byteBuffer, this.stream);
      this.charPos = 0;
      return task;
    }

    private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      if (!haveWrittenPreamble)
      {
        _this.HaveWrittenPreamble_Prop = true;
        byte[] preamble = encoding.GetPreamble();
        if (preamble.Length != 0)
        {
          configuredTaskAwaitable = stream.WriteAsync(preamble, 0, preamble.Length).ConfigureAwait(false);
          await configuredTaskAwaitable;
        }
      }
      int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
      if (bytes > 0)
      {
        configuredTaskAwaitable = stream.WriteAsync(byteBuffer, 0, bytes).ConfigureAwait(false);
        await configuredTaskAwaitable;
      }
      if (!flushStream)
        return;
      configuredTaskAwaitable = stream.FlushAsync().ConfigureAwait(false);
      await configuredTaskAwaitable;
    }

    private sealed class MdaHelper
    {
      private StreamWriter streamWriter;
      private string allocatedCallstack;

      internal MdaHelper(StreamWriter sw, string cs)
      {
        this.streamWriter = sw;
        this.allocatedCallstack = cs;
      }

      ~MdaHelper()
      {
        if (this.streamWriter.charPos == 0 || this.streamWriter.stream == null || this.streamWriter.stream == Stream.Null)
          return;
        Mda.StreamWriterBufferedDataLost.ReportError(Environment.GetResourceString("IO_StreamWriterBufferedDataLost", (object) this.streamWriter.stream.GetType().FullName, (object) (this.streamWriter.stream is FileStream ? ((FileStream) this.streamWriter.stream).NameInternal : "<unknown>"), (object) (this.allocatedCallstack ?? Environment.GetResourceString("IO_StreamWriterBufferedDataLostCaptureAllocatedFromCallstackNotEnabled"))));
      }
    }
  }
}
