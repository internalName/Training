// Decompiled with JetBrains decompiler
// Type: System.IO.StreamReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Реализует объект <see cref="T:System.IO.TextReader" />, который считывает символы из потока байтов в определенной кодировке.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StreamReader : TextReader
  {
    /// <summary>
    ///   Объект <see cref="T:System.IO.StreamReader" /> для пустого потока.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly StreamReader Null = (StreamReader) new StreamReader.NullStreamReader();
    private const int DefaultFileStreamBufferSize = 4096;
    private const int MinBufferSize = 128;
    private Stream stream;
    private Encoding encoding;
    private Decoder decoder;
    private byte[] byteBuffer;
    private char[] charBuffer;
    private byte[] _preamble;
    private int charPos;
    private int charLen;
    private int byteLen;
    private int bytePos;
    private int _maxCharsPerBuffer;
    private bool _detectEncoding;
    private bool _checkPreamble;
    private bool _isBlocked;
    private bool _closable;
    [NonSerialized]
    private volatile Task _asyncReadTask;

    internal static int DefaultBufferSize
    {
      get
      {
        return 1024;
      }
    }

    private void CheckAsyncTaskInProgress()
    {
      Task asyncReadTask = this._asyncReadTask;
      if (asyncReadTask != null && !asyncReadTask.IsCompleted)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
    }

    internal StreamReader()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного потока.
    /// </summary>
    /// <param name="stream">Поток, который нужно считать.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream)
      : this(stream, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для указанного потока, используя заданный параметр обнаружения метки порядка следования байтов.
    /// </summary>
    /// <param name="stream">Поток, который нужно считать.</param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
      : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного потока, используя указанную кодировку символов.
    /// </summary>
    /// <param name="stream">Поток, который нужно считать.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding)
      : this(stream, encoding, true, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного потока, используя указанную кодировку символов и параметр обнаружения метки порядка байтов.
    /// </summary>
    /// <param name="stream">Поток, который нужно считать.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
      : this(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного потока, используя указанную кодировку символов, параметр обнаружения метки порядка байтов и размер буфера.
    /// </summary>
    /// <param name="stream">Поток, который нужно считать.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <param name="bufferSize">Минимальный размер буфера.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="bufferSize" /> не больше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
      : this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для указанного потока на основе заданной кодировки символов, параметра обнаружения меток порядка байтов и размера буфера, а также при необходимости оставляет поток открытым.
    /// </summary>
    /// <param name="stream">Считываемый поток.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Значение <see langword="true" /> для поиска меток порядка байтов в начале файла; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bufferSize">Минимальный размер буфера.</param>
    /// <param name="leaveOpen">
    ///   Значение <see langword="true" />, чтобы оставить поток открытым после удаления объекта <see cref="T:System.IO.StreamReader" />; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
    {
      if (stream == null || encoding == null)
        throw new ArgumentNullException(stream == null ? nameof (stream) : nameof (encoding));
      if (!stream.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для указанного имени файла.
    /// </summary>
    /// <param name="path">Полный путь к файлу для чтения.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> содержит неправильный или недопустимый синтаксис имени файла, имени каталога или метки тома.
    /// </exception>
    public StreamReader(string path)
      : this(path, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного имени файла, используя указанный параметр обнаружения меток порядка байтов.
    /// </summary>
    /// <param name="path">Полный путь к файлу для чтения.</param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="path" /> содержит неправильный или недопустимый синтаксис имени файла, имени каталога или метки тома.
    /// </exception>
    public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
      : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного имени файла, используя указанную кодировку символов.
    /// </summary>
    /// <param name="path">Полный путь к файлу для чтения.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит неправильный или недопустимый синтаксис имени файла, имени каталога или метки тома.
    /// </exception>
    public StreamReader(string path, Encoding encoding)
      : this(path, encoding, true, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного имени файла, используя указанную кодировку символов и параметр обнаружения меток порядка байтов.
    /// </summary>
    /// <param name="path">Полный путь к файлу для чтения.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит неправильный или недопустимый синтаксис имени файла, имени каталога или метки тома.
    /// </exception>
    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
      : this(path, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StreamReader" /> для заданного имени файла, используя указанную кодировку символов, параметр обнаружения меток порядка байтов и размер буфера.
    /// </summary>
    /// <param name="path">Полный путь к файлу для чтения.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="detectEncodingFromByteOrderMarks">
    ///   Указывает, следует ли выполнять поиск меток порядка байтов с начала файла.
    /// </param>
    /// <param name="bufferSize">
    ///   Минимальный размер буфера (в 16-разрядных символах).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Указан недопустимый путь (например, он ведет на несопоставленный диск).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит неправильный или недопустимый синтаксис имени файла, имени каталога или метки тома.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="buffersize" /> не больше нуля.
    /// </exception>
    [SecuritySafeCritical]
    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
      : this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
    {
    }

    [SecurityCritical]
    internal StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool checkHost)
    {
      if (path == null || encoding == null)
        throw new ArgumentNullException(path == null ? nameof (path) : nameof (encoding));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init((Stream) new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost), encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
    }

    private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
    {
      this.stream = stream;
      this.encoding = encoding;
      this.decoder = encoding.GetDecoder();
      if (bufferSize < 128)
        bufferSize = 128;
      this.byteBuffer = new byte[bufferSize];
      this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
      this.charBuffer = new char[this._maxCharsPerBuffer];
      this.byteLen = 0;
      this.bytePos = 0;
      this._detectEncoding = detectEncodingFromByteOrderMarks;
      this._preamble = encoding.GetPreamble();
      this._checkPreamble = (uint) this._preamble.Length > 0U;
      this._isBlocked = false;
      this._closable = !leaveOpen;
    }

    internal void Init(Stream stream)
    {
      this.stream = stream;
      this._closable = true;
    }

    /// <summary>
    ///   Закрывает объект <see cref="T:System.IO.StreamReader" /> и основной поток и освобождает все системные ресурсы, связанные с устройством чтения.
    /// </summary>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Закрывает основной поток, освобождает неуправляемые ресурсы, используемые <see cref="T:System.IO.StreamReader" />, и при необходимости освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!(!this.LeaveOpen & disposing) || this.stream == null)
          return;
        this.stream.Close();
      }
      finally
      {
        if (!this.LeaveOpen && this.stream != null)
        {
          this.stream = (Stream) null;
          this.encoding = (Encoding) null;
          this.decoder = (Decoder) null;
          this.byteBuffer = (byte[]) null;
          this.charBuffer = (char[]) null;
          this.charPos = 0;
          this.charLen = 0;
          base.Dispose(disposing);
        }
      }
    }

    /// <summary>
    ///   Возвращает текущую кодировку символов, используемую текущим объектом <see cref="T:System.IO.StreamReader" />.
    /// </summary>
    /// <returns>
    ///   Текущая кодировка символов, используемая текущим устройством чтения.
    ///    После первого вызова любого метода <see cref="Overload:System.IO.StreamReader.Read" /> объекта <see cref="T:System.IO.StreamReader" /> значение может измениться, поскольку до первого вызова метода <see cref="Overload:System.IO.StreamReader.Read" /> автоматическое определение кодировки не выполняется.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Encoding CurrentEncoding
    {
      [__DynamicallyInvokable] get
      {
        return this.encoding;
      }
    }

    /// <summary>Возвращает основной поток.</summary>
    /// <returns>Основной поток.</returns>
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
        return !this._closable;
      }
    }

    /// <summary>Очищает внутренний буфер.</summary>
    [__DynamicallyInvokable]
    public void DiscardBufferedData()
    {
      this.CheckAsyncTaskInProgress();
      this.byteLen = 0;
      this.charLen = 0;
      this.charPos = 0;
      if (this.encoding != null)
        this.decoder = this.encoding.GetDecoder();
      this._isBlocked = false;
    }

    /// <summary>
    ///   Возвращает значение, определяющее, находится ли позиция текущего потока в конце потока.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее положение находится в конце потока; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Базовый поток был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool EndOfStream
    {
      [__DynamicallyInvokable] get
      {
        if (this.stream == null)
          __Error.ReaderClosed();
        this.CheckAsyncTaskInProgress();
        if (this.charPos < this.charLen)
          return false;
        return this.ReadBuffer() == 0;
      }
    }

    /// <summary>
    ///   Возвращает следующий доступный символ, но не использует его.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее следующий символ для прочтения или значение -1, если доступных для чтения символов нет или поток не поддерживает поиск.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Peek()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
        return -1;
      return (int) this.charBuffer[this.charPos];
    }

    /// <summary>
    ///   Выполняет чтение следующего символа из входного потока и перемещает положение символа на одну позицию вперед.
    /// </summary>
    /// <returns>
    ///   Следующий символ из входного потока, представленный в виде объекта <see cref="T:System.Int32" />, или значение -1, если больше нет доступных символов.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Read()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && this.ReadBuffer() == 0)
        return -1;
      int num = (int) this.charBuffer[this.charPos];
      ++this.charPos;
      return num;
    }

    /// <summary>
    ///   Считывает заданное максимальное количество символов из текущего потока в буфер начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возврате данный метод содержит указанный массив символов, в котором значения в интервале между <paramref name="index" /> и (<paramref name="index + count - 1" />) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Индекс <paramref name="buffer" />, с которого требуется начать запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    /// </param>
    /// <returns>
    ///   Число символов, которые были считаны, или значение 0, если к концу потока не было считано никаких данных.
    ///    Это число будет не больше параметра <paramref name="count" />, в зависимости от доступности данных в потоке.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода, например "Поток закрыт".
    /// </exception>
    [__DynamicallyInvokable]
    public override int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      int num1 = 0;
      bool readToUserBuffer = false;
      while (count > 0)
      {
        int num2 = this.charLen - this.charPos;
        if (num2 == 0)
          num2 = this.ReadBuffer(buffer, index + num1, count, out readToUserBuffer);
        if (num2 != 0)
        {
          if (num2 > count)
            num2 = count;
          if (!readToUserBuffer)
          {
            Buffer.InternalBlockCopy((Array) this.charBuffer, this.charPos * 2, (Array) buffer, (index + num1) * 2, num2 * 2);
            this.charPos += num2;
          }
          num1 += num2;
          count -= num2;
          if (this._isBlocked)
            break;
        }
        else
          break;
      }
      return num1;
    }

    /// <summary>
    ///   Считывает все символы, начиная с текущей позиции до конца потока.
    /// </summary>
    /// <returns>
    ///   Остальная часть потока в виде строки (от текущего положения до конца).
    ///    Если текущее положение находится в конце потока, возвращается пустая строка ("").
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера под возвращаемую строку.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ReadToEnd()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
      do
      {
        stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
        this.charPos = this.charLen;
        this.ReadBuffer();
      }
      while (this.charLen > 0);
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Считывает указанное максимальное количество символов из текущего потока и записывает данные в буфер, начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возврате данный метод содержит указанный массив символов, в котором значения в интервале между <paramref name="index" /> и (<paramref name="index + count - 1" />) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    /// </param>
    /// <returns>
    ///   Количество считанных символов.
    ///    Число будет меньше или равно значению <paramref name="count" />, в зависимости от того, считаны ли все входящие символы.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.StreamReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override int ReadBlock([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      return base.ReadBlock(buffer, index, count);
    }

    private void CompressBuffer(int n)
    {
      Buffer.InternalBlockCopy((Array) this.byteBuffer, n, (Array) this.byteBuffer, 0, this.byteLen - n);
      this.byteLen -= n;
    }

    private void DetectEncoding()
    {
      if (this.byteLen < 2)
        return;
      this._detectEncoding = false;
      bool flag = false;
      if (this.byteBuffer[0] == (byte) 254 && this.byteBuffer[1] == byte.MaxValue)
      {
        this.encoding = (Encoding) new UnicodeEncoding(true, true);
        this.CompressBuffer(2);
        flag = true;
      }
      else if (this.byteBuffer[0] == byte.MaxValue && this.byteBuffer[1] == (byte) 254)
      {
        if (this.byteLen < 4 || this.byteBuffer[2] != (byte) 0 || this.byteBuffer[3] != (byte) 0)
        {
          this.encoding = (Encoding) new UnicodeEncoding(false, true);
          this.CompressBuffer(2);
          flag = true;
        }
        else
        {
          this.encoding = (Encoding) new UTF32Encoding(false, true);
          this.CompressBuffer(4);
          flag = true;
        }
      }
      else if (this.byteLen >= 3 && this.byteBuffer[0] == (byte) 239 && (this.byteBuffer[1] == (byte) 187 && this.byteBuffer[2] == (byte) 191))
      {
        this.encoding = Encoding.UTF8;
        this.CompressBuffer(3);
        flag = true;
      }
      else if (this.byteLen >= 4 && this.byteBuffer[0] == (byte) 0 && (this.byteBuffer[1] == (byte) 0 && this.byteBuffer[2] == (byte) 254) && this.byteBuffer[3] == byte.MaxValue)
      {
        this.encoding = (Encoding) new UTF32Encoding(true, true);
        this.CompressBuffer(4);
        flag = true;
      }
      else if (this.byteLen == 2)
        this._detectEncoding = true;
      if (!flag)
        return;
      this.decoder = this.encoding.GetDecoder();
      this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
      this.charBuffer = new char[this._maxCharsPerBuffer];
    }

    private bool IsPreamble()
    {
      if (!this._checkPreamble)
        return this._checkPreamble;
      int num1 = this.byteLen >= this._preamble.Length ? this._preamble.Length - this.bytePos : this.byteLen - this.bytePos;
      int num2 = 0;
      while (num2 < num1)
      {
        if ((int) this.byteBuffer[this.bytePos] != (int) this._preamble[this.bytePos])
        {
          this.bytePos = 0;
          this._checkPreamble = false;
          break;
        }
        ++num2;
        ++this.bytePos;
      }
      if (this._checkPreamble && this.bytePos == this._preamble.Length)
      {
        this.CompressBuffer(this._preamble.Length);
        this.bytePos = 0;
        this._checkPreamble = false;
        this._detectEncoding = false;
      }
      return this._checkPreamble;
    }

    internal virtual int ReadBuffer()
    {
      this.charLen = 0;
      this.charPos = 0;
      if (!this._checkPreamble)
        this.byteLen = 0;
      do
      {
        if (this._checkPreamble)
        {
          int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
          if (num == 0)
          {
            if (this.byteLen > 0)
            {
              this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
              this.bytePos = this.byteLen = 0;
            }
            return this.charLen;
          }
          this.byteLen += num;
        }
        else
        {
          this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
          if (this.byteLen == 0)
            return this.charLen;
        }
        this._isBlocked = this.byteLen < this.byteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this._detectEncoding && this.byteLen >= 2)
            this.DetectEncoding();
          this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
        }
      }
      while (this.charLen == 0);
      return this.charLen;
    }

    private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
    {
      this.charLen = 0;
      this.charPos = 0;
      if (!this._checkPreamble)
        this.byteLen = 0;
      int charIndex = 0;
      readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
      do
      {
        if (this._checkPreamble)
        {
          int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
          if (num == 0)
          {
            if (this.byteLen > 0)
            {
              if (readToUserBuffer)
              {
                charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
                this.charLen = 0;
              }
              else
              {
                charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
                this.charLen += charIndex;
              }
            }
            return charIndex;
          }
          this.byteLen += num;
        }
        else
        {
          this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
          if (this.byteLen == 0)
            break;
        }
        this._isBlocked = this.byteLen < this.byteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this._detectEncoding && this.byteLen >= 2)
          {
            this.DetectEncoding();
            readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
          }
          this.charPos = 0;
          if (readToUserBuffer)
          {
            charIndex += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
            this.charLen = 0;
          }
          else
          {
            charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
            this.charLen += charIndex;
          }
        }
      }
      while (charIndex == 0);
      this._isBlocked &= charIndex < desiredChars;
      return charIndex;
    }

    /// <summary>
    ///   Выполняет чтение строки символов из текущего потока и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Следующая строка из входного потока или значение <see langword="null" />, если достигнут конец входного потока.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ReadLine()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && this.ReadBuffer() == 0)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      do
      {
        int charPos = this.charPos;
        do
        {
          char ch = this.charBuffer[charPos];
          switch (ch)
          {
            case '\n':
            case '\r':
              string str;
              if (stringBuilder != null)
              {
                stringBuilder.Append(this.charBuffer, this.charPos, charPos - this.charPos);
                str = stringBuilder.ToString();
              }
              else
                str = new string(this.charBuffer, this.charPos, charPos - this.charPos);
              this.charPos = charPos + 1;
              if (ch == '\r' && (this.charPos < this.charLen || this.ReadBuffer() > 0) && this.charBuffer[this.charPos] == '\n')
                ++this.charPos;
              return str;
            default:
              ++charPos;
              continue;
          }
        }
        while (charPos < this.charLen);
        int charCount = this.charLen - this.charPos;
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(charCount + 80);
        stringBuilder.Append(this.charBuffer, this.charPos, charCount);
      }
      while (this.ReadBuffer() > 0);
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Асинхронно выполняет чтение строки символов из текущего потока и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит следующую строку из потока или значение <see langword="null" />, если все знаки прочитаны.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов в следующей строке больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<string> ReadLineAsync()
    {
      if (this.GetType() != typeof (StreamReader))
        return base.ReadLineAsync();
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<string> task = this.ReadLineAsyncInternal();
      this._asyncReadTask = (Task) task;
      return task;
    }

    private async Task<string> ReadLineAsyncInternal()
    {
      bool flag1 = this.CharPos_Prop == this.CharLen_Prop;
      ConfiguredTaskAwaitable<int> configuredTaskAwaitable;
      if (flag1)
      {
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
        flag1 = await configuredTaskAwaitable == 0;
      }
      if (flag1)
        return (string) null;
      StringBuilder sb = (StringBuilder) null;
      do
      {
        char[] tmpCharBuffer = this.CharBuffer_Prop;
        int tmpCharLen = this.CharLen_Prop;
        int tmpCharPos = this.CharPos_Prop;
        int i = tmpCharPos;
        do
        {
          char ch = tmpCharBuffer[i];
          switch (ch)
          {
            case '\n':
            case '\r':
              string s;
              if (sb != null)
              {
                sb.Append(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
                s = sb.ToString();
              }
              else
                s = new string(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
              this.CharPos_Prop = tmpCharPos = i + 1;
              bool flag2 = ch == '\r';
              if (flag2)
              {
                bool flag3 = tmpCharPos < tmpCharLen;
                if (!flag3)
                {
                  configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
                  flag3 = await configuredTaskAwaitable > 0;
                }
                flag2 = flag3;
              }
              if (flag2)
              {
                tmpCharPos = this.CharPos_Prop;
                if (this.CharBuffer_Prop[tmpCharPos] == '\n')
                  this.CharPos_Prop = ++tmpCharPos;
              }
              return s;
            default:
              ++i;
              continue;
          }
        }
        while (i < tmpCharLen);
        i = tmpCharLen - tmpCharPos;
        if (sb == null)
          sb = new StringBuilder(i + 80);
        sb.Append(tmpCharBuffer, tmpCharPos, i);
        tmpCharBuffer = (char[]) null;
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
      }
      while (await configuredTaskAwaitable > 0);
      return sb.ToString();
    }

    /// <summary>
    ///   Асинхронно считывает все символы, начиная с текущей позиции до конца потока, и возвращает их в виде одной строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит строку с символами от текущего положения до конца потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<string> ReadToEndAsync()
    {
      if (this.GetType() != typeof (StreamReader))
        return base.ReadToEndAsync();
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<string> endAsyncInternal = this.ReadToEndAsyncInternal();
      this._asyncReadTask = (Task) endAsyncInternal;
      return endAsyncInternal;
    }

    private async Task<string> ReadToEndAsyncInternal()
    {
      StringBuilder sb = new StringBuilder(this.CharLen_Prop - this.CharPos_Prop);
      do
      {
        int charPosProp = this.CharPos_Prop;
        sb.Append(this.CharBuffer_Prop, charPosProp, this.CharLen_Prop - charPosProp);
        this.CharPos_Prop = this.CharLen_Prop;
        int num = await this.ReadBufferAsync().ConfigureAwait(false);
      }
      while (this.CharLen_Prop > 0);
      return sb.ToString();
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное количество символов из текущего потока и записывает данные в буфер, начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если конец потока достигнут, прежде чем указанное количество символов записывается в буфер, возвращается текущий метод.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число символов (в байтах), считанных в буфер.
    ///    Результирующее значение может быть меньше запрошенного числа символов, если число доступных в данный момент символов меньше запрошенного числа, или результат может быть равен 0 (нулю), если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamReader))
        return base.ReadAsync(buffer, index, count);
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<int> task = this.ReadAsyncInternal(buffer, index, count);
      this._asyncReadTask = (Task) task;
      return task;
    }

    internal override async Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
    {
      bool flag = this.CharPos_Prop == this.CharLen_Prop;
      ConfiguredTaskAwaitable<int> configuredTaskAwaitable;
      if (flag)
      {
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
        flag = await configuredTaskAwaitable == 0;
      }
      if (flag)
        return 0;
      int charsRead = 0;
      bool readToUserBuffer = false;
      byte[] tmpByteBuffer = this.ByteBuffer_Prop;
      Stream tmpStream = this.Stream_Prop;
      while (count > 0)
      {
        int n = this.CharLen_Prop - this.CharPos_Prop;
        if (n == 0)
        {
          this.CharLen_Prop = 0;
          this.CharPos_Prop = 0;
          if (!this.CheckPreamble_Prop)
            this.ByteLen_Prop = 0;
          readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
          do
          {
            if (this.CheckPreamble_Prop)
            {
              int bytePosProp = this.BytePos_Prop;
              configuredTaskAwaitable = tmpStream.ReadAsync(tmpByteBuffer, bytePosProp, tmpByteBuffer.Length - bytePosProp).ConfigureAwait(false);
              int num = await configuredTaskAwaitable;
              if (num == 0)
              {
                if (this.ByteLen_Prop > 0)
                {
                  if (readToUserBuffer)
                  {
                    n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
                    this.CharLen_Prop = 0;
                  }
                  else
                  {
                    n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
                    this.CharLen_Prop += n;
                  }
                }
                this.IsBlocked_Prop = true;
                break;
              }
              this.ByteLen_Prop += num;
            }
            else
            {
              configuredTaskAwaitable = tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
              this.ByteLen_Prop = await configuredTaskAwaitable;
              if (this.ByteLen_Prop == 0)
              {
                this.IsBlocked_Prop = true;
                break;
              }
            }
            this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
            if (!this.IsPreamble())
            {
              if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
              {
                this.DetectEncoding();
                readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
              }
              this.CharPos_Prop = 0;
              if (readToUserBuffer)
              {
                n += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
                this.CharLen_Prop = 0;
              }
              else
              {
                n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
                this.CharLen_Prop += n;
              }
            }
          }
          while (n == 0);
          if (n == 0)
            break;
        }
        if (n > count)
          n = count;
        if (!readToUserBuffer)
        {
          Buffer.InternalBlockCopy((Array) this.CharBuffer_Prop, this.CharPos_Prop * 2, (Array) buffer, (index + charsRead) * 2, n * 2);
          this.CharPos_Prop += n;
        }
        charsRead += n;
        count -= n;
        if (this.IsBlocked_Prop)
          break;
      }
      return charsRead;
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное количество символов из текущего потока и записывает данные в буфер, начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если конец потока достигнут, прежде чем в буфер записано указанное количество символов, метод возвращает управление.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число символов (в байтах), считанных в буфер.
    ///    Результирующее значение может быть меньше запрошенного числа символов, если число доступных в данный момент символов меньше запрошенного числа, или результат может быть равен 0 (нулю), если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamReader))
        return base.ReadBlockAsync(buffer, index, count);
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<int> task = base.ReadBlockAsync(buffer, index, count);
      this._asyncReadTask = (Task) task;
      return task;
    }

    private int CharLen_Prop
    {
      get
      {
        return this.charLen;
      }
      set
      {
        this.charLen = value;
      }
    }

    private int CharPos_Prop
    {
      get
      {
        return this.charPos;
      }
      set
      {
        this.charPos = value;
      }
    }

    private int ByteLen_Prop
    {
      get
      {
        return this.byteLen;
      }
      set
      {
        this.byteLen = value;
      }
    }

    private int BytePos_Prop
    {
      get
      {
        return this.bytePos;
      }
      set
      {
        this.bytePos = value;
      }
    }

    private byte[] Preamble_Prop
    {
      get
      {
        return this._preamble;
      }
    }

    private bool CheckPreamble_Prop
    {
      get
      {
        return this._checkPreamble;
      }
    }

    private Decoder Decoder_Prop
    {
      get
      {
        return this.decoder;
      }
    }

    private bool DetectEncoding_Prop
    {
      get
      {
        return this._detectEncoding;
      }
    }

    private char[] CharBuffer_Prop
    {
      get
      {
        return this.charBuffer;
      }
    }

    private byte[] ByteBuffer_Prop
    {
      get
      {
        return this.byteBuffer;
      }
    }

    private bool IsBlocked_Prop
    {
      get
      {
        return this._isBlocked;
      }
      set
      {
        this._isBlocked = value;
      }
    }

    private Stream Stream_Prop
    {
      get
      {
        return this.stream;
      }
    }

    private int MaxCharsPerBuffer_Prop
    {
      get
      {
        return this._maxCharsPerBuffer;
      }
    }

    private async Task<int> ReadBufferAsync()
    {
      this.CharLen_Prop = 0;
      this.CharPos_Prop = 0;
      byte[] tmpByteBuffer = this.ByteBuffer_Prop;
      Stream tmpStream = this.Stream_Prop;
      if (!this.CheckPreamble_Prop)
        this.ByteLen_Prop = 0;
      do
      {
        if (this.CheckPreamble_Prop)
        {
          int bytePosProp = this.BytePos_Prop;
          int num = await tmpStream.ReadAsync(tmpByteBuffer, bytePosProp, tmpByteBuffer.Length - bytePosProp).ConfigureAwait(false);
          if (num == 0)
          {
            if (this.ByteLen_Prop > 0)
            {
              this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
              this.BytePos_Prop = 0;
              this.ByteLen_Prop = 0;
            }
            return this.CharLen_Prop;
          }
          this.ByteLen_Prop += num;
        }
        else
        {
          this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
          if (this.ByteLen_Prop == 0)
            return this.CharLen_Prop;
        }
        this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
            this.DetectEncoding();
          this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
        }
      }
      while (this.CharLen_Prop == 0);
      return this.CharLen_Prop;
    }

    private class NullStreamReader : StreamReader
    {
      internal NullStreamReader()
      {
        this.Init(Stream.Null);
      }

      public override Stream BaseStream
      {
        get
        {
          return Stream.Null;
        }
      }

      public override Encoding CurrentEncoding
      {
        get
        {
          return Encoding.Unicode;
        }
      }

      protected override void Dispose(bool disposing)
      {
      }

      public override int Peek()
      {
        return -1;
      }

      public override int Read()
      {
        return -1;
      }

      public override int Read(char[] buffer, int index, int count)
      {
        return 0;
      }

      public override string ReadLine()
      {
        return (string) null;
      }

      public override string ReadToEnd()
      {
        return string.Empty;
      }

      internal override int ReadBuffer()
      {
        return 0;
      }
    }
  }
}
