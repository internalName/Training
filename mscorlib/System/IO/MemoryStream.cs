// Decompiled with JetBrains decompiler
// Type: System.IO.MemoryStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Создает поток, резервным хранилищем которого является память.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MemoryStream : Stream
  {
    private byte[] _buffer;
    private int _origin;
    private int _position;
    private int _length;
    private int _capacity;
    private bool _expandable;
    private bool _writable;
    private bool _exposable;
    private bool _isOpen;
    [NonSerialized]
    private Task<int> _lastReadTask;
    private const int MemStreamMaxLength = 2147483647;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.MemoryStream" /> расширяемой емкостью, инициализированной нулевым значением.
    /// </summary>
    [__DynamicallyInvokable]
    public MemoryStream()
      : this(0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.MemoryStream" /> расширяемой емкостью, инициализированной с указанным значением.
    /// </summary>
    /// <param name="capacity">
    ///   Исходный размер внутреннего массива в байтах.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="capacity" /> является отрицательным значением.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      this._buffer = new byte[capacity];
      this._capacity = capacity;
      this._expandable = true;
      this._writable = true;
      this._exposable = true;
      this._origin = 0;
      this._isOpen = true;
    }

    /// <summary>
    ///   Инициализирует новый неизменяемый экземпляр класса <see cref="T:System.IO.MemoryStream" /> на основе указанного массива байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов без знака, из которого создается текущий поток.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer)
      : this(buffer, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый неизменяемый экземпляр класса <see cref="T:System.IO.MemoryStream" /> на основе указанного массива байтов с помощью указанного значения свойства <see cref="P:System.IO.MemoryStream.CanWrite" />.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов без знака, из которого создается данный поток.
    /// </param>
    /// <param name="writable">
    ///   Параметр свойства <see cref="P:System.IO.MemoryStream.CanWrite" />, который определяет возможность поддержки потоком записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, bool writable)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      this._buffer = buffer;
      this._length = this._capacity = buffer.Length;
      this._writable = writable;
      this._exposable = false;
      this._origin = 0;
      this._isOpen = true;
    }

    /// <summary>
    ///   Инициализирует новый неизменяемый экземпляр класса <see cref="T:System.IO.MemoryStream" /> на основе указанной области (индекса) массива байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов без знака, из которого создается данный поток.
    /// </param>
    /// <param name="index">
    ///   Индекс в <paramref name="buffer" />, с которого начинается поток.
    /// </param>
    /// <param name="count">Длина потока в байтах.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count)
      : this(buffer, index, count, true, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый неизменяемый экземпляр класса <see cref="T:System.IO.MemoryStream" /> на основе указанной области массива байтов с помощью указанного значения свойства <see cref="P:System.IO.MemoryStream.CanWrite" />.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов без знака, из которого создается данный поток.
    /// </param>
    /// <param name="index">
    ///   Индекс в <paramref name="buffer" />, с которого начинается поток.
    /// </param>
    /// <param name="count">Длина потока в байтах.</param>
    /// <param name="writable">
    ///   Параметр свойства <see cref="P:System.IO.MemoryStream.CanWrite" />, который определяет возможность поддержки потоком записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> являются отрицательными.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count, bool writable)
      : this(buffer, index, count, writable, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.MemoryStream" /> на основе указанной области массива байтов с помощью указанного значения свойства <see cref="P:System.IO.MemoryStream.CanWrite" /> и возможности вызова <see cref="M:System.IO.MemoryStream.GetBuffer" /> с указанным значением.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов без знака, из которого создается данный поток.
    /// </param>
    /// <param name="index">
    ///   Индекс в <paramref name="buffer" />, с которого начинается поток.
    /// </param>
    /// <param name="count">Длина потока в байтах.</param>
    /// <param name="writable">
    ///   Параметр свойства <see cref="P:System.IO.MemoryStream.CanWrite" />, который определяет возможность поддержки потоком записи.
    /// </param>
    /// <param name="publiclyVisible">
    ///   Значение <see langword="true" />, чтобы разрешить метод <see cref="M:System.IO.MemoryStream.GetBuffer" />, возвращающий массив байтов без знака, из которого создан поток; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this._buffer = buffer;
      this._origin = this._position = index;
      this._length = this._capacity = index + count;
      this._writable = writable;
      this._exposable = publiclyVisible;
      this._expandable = false;
      this._isOpen = true;
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток открыт.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool CanRead
    {
      [__DynamicallyInvokable] get
      {
        return this._isOpen;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций поиска.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток открыт.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool CanSeek
    {
      [__DynamicallyInvokable] get
      {
        return this._isOpen;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает запись; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool CanWrite
    {
      [__DynamicallyInvokable] get
      {
        return this._writable;
      }
    }

    private void EnsureWriteable()
    {
      if (this.CanWrite)
        return;
      __Error.WriteNotSupported();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.IO.MemoryStream" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        this._isOpen = false;
        this._writable = false;
        this._expandable = false;
        this._lastReadTask = (Task<int>) null;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    private bool EnsureCapacity(int value)
    {
      if (value < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (value <= this._capacity)
        return false;
      int num = value;
      if (num < 256)
        num = 256;
      if (num < this._capacity * 2)
        num = this._capacity * 2;
      if ((uint) (this._capacity * 2) > 2147483591U)
        num = value > 2147483591 ? value : 2147483591;
      this.Capacity = num;
      return true;
    }

    /// <summary>
    ///   Переопределяет метод <see cref="M:System.IO.Stream.Flush" /> так, что никакие действия не выполняются.
    /// </summary>
    [__DynamicallyInvokable]
    public override void Flush()
    {
    }

    /// <summary>
    ///   Асинхронно очищает все буферы для этого потока и отслеживает запросы отмены.
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
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      try
      {
        this.Flush();
        return Task.CompletedTask;
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>
    ///   Возвращает массив байтов без знака, из которого был создан данный поток.
    /// </summary>
    /// <returns>
    ///   Массив байтов, из которого был создан данный поток, или базовый массив, если массив байтов не был предоставлен конструктору <see cref="T:System.IO.MemoryStream" /> в процессе конструирования текущего экземпляра.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see langword="MemoryStream" /> Экземпляр не был создан с помощью открытого буфера.
    /// </exception>
    public virtual byte[] GetBuffer()
    {
      if (!this._exposable)
        throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_MemStreamBuffer"));
      return this._buffer;
    }

    /// <summary>
    ///   Возвращает массив байтов без знака, из которого был создан данный поток.
    ///    Возвращаемое значение указывает, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="buffer">
    ///   Сегмент массива байтов, из которого был создан данный поток.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если преобразование прошло успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
    {
      if (!this._exposable)
      {
        buffer = new ArraySegment<byte>();
        return false;
      }
      buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
      return true;
    }

    internal byte[] InternalGetBuffer()
    {
      return this._buffer;
    }

    [FriendAccessAllowed]
    internal void InternalGetOriginAndLength(out int origin, out int length)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      origin = this._origin;
      length = this._length;
    }

    internal int InternalGetPosition()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      return this._position;
    }

    internal int InternalReadInt32()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      int num = this._position += 4;
      if (num > this._length)
      {
        this._position = this._length;
        __Error.EndOfFile();
      }
      return (int) this._buffer[num - 4] | (int) this._buffer[num - 3] << 8 | (int) this._buffer[num - 2] << 16 | (int) this._buffer[num - 1] << 24;
    }

    internal int InternalEmulateRead(int count)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      int num = this._length - this._position;
      if (num > count)
        num = count;
      if (num < 0)
        num = 0;
      this._position += num;
      return num;
    }

    /// <summary>
    ///   Возвращает или задает число байтов, выделенных для этого потока.
    /// </summary>
    /// <returns>
    ///   Длина применимой к использованию части буфера для потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Заданная емкость отрицательная или меньше текущей длины потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="set" /> вызывается для потока, емкость которого нельзя изменить.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Capacity
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return this._capacity - this._origin;
      }
      [__DynamicallyInvokable] set
      {
        if ((long) value < this.Length)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (!this._expandable && value != this.Capacity)
          __Error.MemoryStreamNotExpandable();
        if (!this._expandable || value == this._capacity)
          return;
        if (value > 0)
        {
          byte[] numArray = new byte[value];
          if (this._length > 0)
            Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) numArray, 0, this._length);
          this._buffer = numArray;
        }
        else
          this._buffer = (byte[]) null;
        this._capacity = value;
      }
    }

    /// <summary>Возвращает длину потока в байтах.</summary>
    /// <returns>Длина потока в байтах.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override long Length
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return (long) (this._length - this._origin);
      }
    }

    /// <summary>Возвращает или задает текущее положение в потоке.</summary>
    /// <returns>Текущее положение в потоке.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Позиция присвоено отрицательное значение или значение больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override long Position
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return (long) (this._position - this._origin);
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (value > (long) int.MaxValue)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
        this._position = this._origin + (int) value;
      }
    }

    /// <summary>
    ///   Считывает блок байтов из текущего потока и записывает данные в буфер.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении данного метода этот параметр содержит указанный массив байтов со значениями от <paramref name="offset" /> до (<paramref name="offset" /> + <paramref name="count" /> - 1), замененными символами, считанными из текущего потока.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемое от нуля смещение байтов в буфере <paramref name="buffer" />, с которого начинается сохранение данных из текущего потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <returns>
    ///   Общее число байтов, записанных в буфер.
    ///    Оно может быть меньше запрошенного числа байтов, если это количество в текущий момент не доступно, или же равно нулю, если конец потока достигнут до того, как байты были считаны.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> Уменьшаемое. длина буфера меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр потока закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Read([In, Out] byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      int byteCount = this._length - this._position;
      if (byteCount > count)
        byteCount = count;
      if (byteCount <= 0)
        return 0;
      if (byteCount <= 8)
      {
        int num = byteCount;
        while (--num >= 0)
          buffer[offset + num] = this._buffer[this._position + num];
      }
      else
        Buffer.InternalBlockCopy((Array) this._buffer, this._position, (Array) buffer, offset, byteCount);
      this._position += byteCount;
      return byteCount;
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
    ///    Значение по умолчанию — <see cref="P:System.Threading.CancellationToken.None" />.
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
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      try
      {
        int result = this.Read(buffer, offset, count);
        Task<int> lastReadTask = this._lastReadTask;
        return lastReadTask == null || lastReadTask.Result != result ? (this._lastReadTask = Task.FromResult<int>(result)) : lastReadTask;
      }
      catch (OperationCanceledException ex)
      {
        return Task.FromCancellation<int>(ex);
      }
      catch (Exception ex)
      {
        return Task.FromException<int>(ex);
      }
    }

    /// <summary>Считывает байт из текущего потока.</summary>
    /// <returns>
    ///   Байт приводится к типу <see cref="T:System.Int32" /> или -1, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр потока закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override int ReadByte()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (this._position >= this._length)
        return -1;
      return (int) this._buffer[this._position++];
    }

    /// <summary>
    ///   Асинхронно считывает все байты из текущего потока и записывает их в другой поток, используя указанный размер буфера и токен отмены.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер (в байтах) буфера.
    ///    Это значение должно быть больше нуля.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию копирования.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="buffersize" /> имеет отрицательное значение или равен нулю.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток или поток назначения удаляется.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает чтение или на целевой поток не поддерживает запись.
    /// </exception>
    [__DynamicallyInvokable]
    public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (!this.CanRead && !this.CanWrite)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!destination.CanRead && !destination.CanWrite)
        throw new ObjectDisposedException(nameof (destination), Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (!destination.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (this.GetType() != typeof (MemoryStream))
        return base.CopyToAsync(destination, bufferSize, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      int position = this._position;
      int count = this.InternalEmulateRead(this._length - this._position);
      MemoryStream memoryStream = destination as MemoryStream;
      if (memoryStream == null)
        return destination.WriteAsync(this._buffer, position, count, cancellationToken);
      try
      {
        memoryStream.Write(this._buffer, position, count);
        return Task.CompletedTask;
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>
    ///   Задает указанное значение для положения в текущем потоке.
    /// </summary>
    /// <param name="offset">
    ///   Новое положение в потоке.
    ///    Оно определяется относительно параметра <paramref name="loc" /> и может быть положительным или отрицательным.
    /// </param>
    /// <param name="loc">
    ///   Значение типа <see cref="T:System.IO.SeekOrigin" />, которое действует как точка ссылки поиска.
    /// </param>
    /// <returns>
    ///   Новое положение в потоке, вычисляемое путем объединения смещения и исходной точки ссылки.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Попытка поиска выполняется до начала потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Содержит недопустимый <see cref="T:System.IO.SeekOrigin" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" />вызвало арифметическое переполнение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр потока закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override long Seek(long offset, SeekOrigin loc)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (offset > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      switch (loc)
      {
        case SeekOrigin.Begin:
          int num1 = this._origin + (int) offset;
          if (offset < 0L || num1 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num1;
          break;
        case SeekOrigin.Current:
          int num2 = this._position + (int) offset;
          if ((long) this._position + offset < (long) this._origin || num2 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num2;
          break;
        case SeekOrigin.End:
          int num3 = this._length + (int) offset;
          if ((long) this._length + offset < (long) this._origin || num3 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num3;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      }
      return (long) this._position;
    }

    /// <summary>
    ///   Задает указанное значение для длины текущего потока.
    /// </summary>
    /// <param name="value">Значение задаваемой длины.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не является изменяемым размером и <paramref name="value" /> больше текущей емкости.
    /// 
    ///   -или-
    /// 
    ///   Текущий поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="value" /> меньше нуля или больше, чем максимальная длина <see cref="T:System.IO.MemoryStream" />, где Максимальная длина равна (<see cref="F:System.Int32.MaxValue" /> -источник), и источник — это индекс в основном буфере, с которого начинается поток.
    /// </exception>
    [__DynamicallyInvokable]
    public override void SetLength(long value)
    {
      if (value < 0L || value > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      this.EnsureWriteable();
      if (value > (long) (int.MaxValue - this._origin))
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      int num = this._origin + (int) value;
      if (!this.EnsureCapacity(num) && num > this._length)
        Array.Clear((Array) this._buffer, this._length, num - this._length);
      this._length = num;
      if (this._position <= num)
        return;
      this._position = num;
    }

    /// <summary>
    ///   Записывает содержимое потока в массив байтов независимо от свойства <see cref="P:System.IO.MemoryStream.Position" />.
    /// </summary>
    /// <returns>Новый массив байтов.</returns>
    [__DynamicallyInvokable]
    public virtual byte[] ToArray()
    {
      byte[] numArray = new byte[this._length - this._origin];
      Buffer.InternalBlockCopy((Array) this._buffer, this._origin, (Array) numArray, 0, this._length - this._origin);
      return numArray;
    }

    /// <summary>
    ///   Записывает в текущий поток блок байтов, используя данные, считанные из буфера.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, из которого записываются данные.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемое от нуля смещение байтов в буфере <paramref name="buffer" />, с которого начинается копирование байтов в текущий поток.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    ///    Дополнительные сведения содержатся в <see cref="P:System.IO.Stream.CanWrite" />.
    /// 
    ///   -или-
    /// 
    ///   Текущая позиция находится ближе, чем <paramref name="count" /> байт в конце потока и емкость не может быть изменен.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> Уменьшаемое. длина буфера меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> являются отрицательными.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр потока закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      this.EnsureWriteable();
      int num1 = this._position + count;
      if (num1 < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (num1 > this._length)
      {
        bool flag = this._position > this._length;
        if (num1 > this._capacity && this.EnsureCapacity(num1))
          flag = false;
        if (flag)
          Array.Clear((Array) this._buffer, this._length, num1 - this._length);
        this._length = num1;
      }
      if (count <= 8 && buffer != this._buffer)
      {
        int num2 = count;
        while (--num2 >= 0)
          this._buffer[this._position + num2] = buffer[offset + num2];
      }
      else
        Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._buffer, this._position, count);
      this._position = num1;
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
    ///    Значение по умолчанию — <see cref="P:System.Threading.CancellationToken.None" />.
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
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      try
      {
        this.Write(buffer, offset, count);
        return Task.CompletedTask;
      }
      catch (OperationCanceledException ex)
      {
        return (Task) Task.FromCancellation<VoidTaskResult>(ex);
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>
    ///   Записывает байт в текущее положение текущего потока.
    /// </summary>
    /// <param name="value">Записываемый байт.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    ///    Дополнительные сведения содержатся в <see cref="P:System.IO.Stream.CanWrite" />.
    /// 
    ///   -или-
    /// 
    ///   Текущая позиция находится в конце потока и емкость изменить невозможно.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override void WriteByte(byte value)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      this.EnsureWriteable();
      if (this._position >= this._length)
      {
        int num = this._position + 1;
        bool flag = this._position > this._length;
        if (num >= this._capacity && this.EnsureCapacity(num))
          flag = false;
        if (flag)
          Array.Clear((Array) this._buffer, this._length, this._position - this._length);
        this._length = num;
      }
      this._buffer[this._position++] = value;
    }

    /// <summary>
    ///   Записывает все содержимое данного потока памяти в другой поток.
    /// </summary>
    /// <param name="stream">
    ///   Поток, в который требуется осуществить запись данного потока памяти.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий или конечный поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteTo(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream), Environment.GetResourceString("ArgumentNull_Stream"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      stream.Write(this._buffer, this._origin, this._length - this._origin);
    }
  }
}
