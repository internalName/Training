// Decompiled with JetBrains decompiler
// Type: System.IO.BufferedStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Добавляет буферизацию для выполнения операций на другой поток чтения и записи.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class BufferedStream : Stream
  {
    private const int _DefaultBufferSize = 4096;
    private Stream _stream;
    private byte[] _buffer;
    private readonly int _bufferSize;
    private int _readPos;
    private int _readLen;
    private int _writePos;
    private BeginEndAwaitableAdapter _beginEndAwaitable;
    private Task<int> _lastSyncCompletedReadTask;
    private const int MaxShadowBufferSize = 81920;

    private BufferedStream()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.BufferedStream" /> класса размер буфера по умолчанию 4096 байт.
    /// </summary>
    /// <param name="stream">Текущий поток.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    public BufferedStream(Stream stream)
      : this(stream, 4096)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.BufferedStream" /> класса с заданным размером буфера.
    /// </summary>
    /// <param name="stream">Текущий поток.</param>
    /// <param name="bufferSize">Размер буфера в байтах.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bufferSize" /> является отрицательным значением.
    /// </exception>
    public BufferedStream(Stream stream, int bufferSize)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) nameof (bufferSize)));
      this._stream = stream;
      this._bufferSize = bufferSize;
      if (this._stream.CanRead || this._stream.CanWrite)
        return;
      __Error.StreamIsClosed();
    }

    private void EnsureNotClosed()
    {
      if (this._stream != null)
        return;
      __Error.StreamIsClosed();
    }

    private void EnsureCanSeek()
    {
      if (this._stream.CanSeek)
        return;
      __Error.SeekNotSupported();
    }

    private void EnsureCanRead()
    {
      if (this._stream.CanRead)
        return;
      __Error.ReadNotSupported();
    }

    private void EnsureCanWrite()
    {
      if (this._stream.CanWrite)
        return;
      __Error.WriteNotSupported();
    }

    private void EnsureBeginEndAwaitableAllocated()
    {
      if (this._beginEndAwaitable != null)
        return;
      this._beginEndAwaitable = new BeginEndAwaitableAdapter();
    }

    private void EnsureShadowBufferAllocated()
    {
      if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
        return;
      byte[] numArray = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
      Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) numArray, 0, this._writePos);
      this._buffer = numArray;
    }

    private void EnsureBufferAllocated()
    {
      if (this._buffer != null)
        return;
      this._buffer = new byte[this._bufferSize];
    }

    internal Stream UnderlyingStream
    {
      [FriendAccessAllowed] get
      {
        return this._stream;
      }
    }

    internal int BufferSize
    {
      [FriendAccessAllowed] get
      {
        return this._bufferSize;
      }
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
        if (this._stream != null)
          return this._stream.CanRead;
        return false;
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
        if (this._stream != null)
          return this._stream.CanWrite;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее в текущем потоке наличие поддержки операций поиска.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поток поддерживает поиск; <see langword="false" /> Если поток закрыт или если поток был сконструирован из дескриптора операционной системы, такого как канал или вывод на консоль.
    /// </returns>
    public override bool CanSeek
    {
      get
      {
        if (this._stream != null)
          return this._stream.CanSeek;
        return false;
      }
    }

    /// <summary>Возвращает длину потока в байтах.</summary>
    /// <returns>Длина потока в байтах.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Основной поток является <see langword="null" /> или закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override long Length
    {
      get
      {
        this.EnsureNotClosed();
        if (this._writePos > 0)
          this.FlushWrite();
        return this._stream.Length;
      }
    }

    /// <summary>Возвращает позицию в текущем потоке.</summary>
    /// <returns>Позиция в текущем потоке.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, передаваемое в <see cref="M:System.IO.BufferedStream.Seek(System.Int64,System.IO.SeekOrigin)" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Происходит ошибка ввода-вывода, например поток закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override long Position
    {
      get
      {
        this.EnsureNotClosed();
        this.EnsureCanSeek();
        return this._stream.Position + (long) (this._readPos - this._readLen + this._writePos);
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        this.EnsureNotClosed();
        this.EnsureCanSeek();
        if (this._writePos > 0)
          this.FlushWrite();
        this._readPos = 0;
        this._readLen = 0;
        this._stream.Seek(value, SeekOrigin.Begin);
      }
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (this._stream == null)
          return;
        try
        {
          this.Flush();
        }
        finally
        {
          this._stream.Close();
        }
      }
      finally
      {
        this._stream = (Stream) null;
        this._buffer = (byte[]) null;
        this._lastSyncCompletedReadTask = (Task<int>) null;
        base.Dispose(disposing);
      }
    }

    /// <summary>
    ///   Очищает все буферы для этого потока и вызывает запись всех буферизованных данных в базовое устройство.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Источник данных или репозиторий не открыт.
    /// </exception>
    public override void Flush()
    {
      this.EnsureNotClosed();
      if (this._writePos > 0)
        this.FlushWrite();
      else if (this._readPos < this._readLen)
      {
        if (!this._stream.CanSeek)
          return;
        this.FlushRead();
        if (!this._stream.CanWrite && !(this._stream is BufferedStream))
          return;
        this._stream.Flush();
      }
      else
      {
        if (this._stream.CanWrite || this._stream is BufferedStream)
          this._stream.Flush();
        this._writePos = this._readPos = this._readLen = 0;
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
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
        return (Task) Task.FromCancellation<int>(cancellationToken);
      this.EnsureNotClosed();
      return BufferedStream.FlushAsyncInternal(cancellationToken, this, this._stream, this._writePos, this._readPos, this._readLen);
    }

    private static async Task FlushAsyncInternal(CancellationToken cancellationToken, BufferedStream _this, Stream stream, int writePos, int readPos, int readLen)
    {
      SemaphoreSlim sem = _this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        if (writePos > 0)
          await _this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
        else if (readPos < readLen)
        {
          if (!stream.CanSeek)
            return;
          _this.FlushRead();
          if (!stream.CanRead && !(stream is BufferedStream))
            return;
          await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
        else
        {
          if (!stream.CanWrite && !(stream is BufferedStream))
            return;
          await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
      }
      finally
      {
        sem.Release();
      }
    }

    private void FlushRead()
    {
      if (this._readPos - this._readLen != 0)
        this._stream.Seek((long) (this._readPos - this._readLen), SeekOrigin.Current);
      this._readPos = 0;
      this._readLen = 0;
    }

    private void ClearReadBufferBeforeWrite()
    {
      if (this._readPos == this._readLen)
      {
        this._readPos = this._readLen = 0;
      }
      else
      {
        if (!this._stream.CanSeek)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed"));
        this.FlushRead();
      }
    }

    private void FlushWrite()
    {
      this._stream.Write(this._buffer, 0, this._writePos);
      this._writePos = 0;
      this._stream.Flush();
    }

    private async Task FlushWriteAsync(CancellationToken cancellationToken)
    {
      await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
      this._writePos = 0;
      await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    private int ReadFromBuffer(byte[] array, int offset, int count)
    {
      int byteCount = this._readLen - this._readPos;
      if (byteCount == 0)
        return 0;
      if (byteCount > count)
        byteCount = count;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, byteCount);
      this._readPos += byteCount;
      return byteCount;
    }

    private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
    {
      try
      {
        error = (Exception) null;
        return this.ReadFromBuffer(array, offset, count);
      }
      catch (Exception ex)
      {
        error = ex;
        return 0;
      }
    }

    /// <summary>
    ///   Копирует байты из текущего буферизованного потока в массив.
    /// </summary>
    /// <param name="array">Буфер, в который копируются байты.</param>
    /// <param name="offset">
    ///   Смещение байтов в буфере, с которого начинается чтение байтов.
    /// </param>
    /// <param name="count">
    ///   Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <returns>
    ///   Общее число байтов, считанных в <paramref name="array" />.
    ///    Это может быть меньше запрошенного числа байтов, если что многие байты недоступны в данный момент или 0, если конец потока достигнут до данные можно читать.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="array" /> минус <paramref name="offset" /> — меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток не открыт или является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
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
      this.EnsureNotClosed();
      this.EnsureCanRead();
      int num1 = this.ReadFromBuffer(array, offset, count);
      if (num1 == count)
        return num1;
      int num2 = num1;
      if (num1 > 0)
      {
        count -= num1;
        offset += num1;
      }
      this._readPos = this._readLen = 0;
      if (this._writePos > 0)
        this.FlushWrite();
      if (count >= this._bufferSize)
        return this._stream.Read(array, offset, count) + num2;
      this.EnsureBufferAllocated();
      this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
      return this.ReadFromBuffer(array, offset, count) + num2;
    }

    /// <summary>
    ///   Начинает операцию асинхронного чтения.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, в который необходимо считать данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов в буфере <paramref name="buffer" />, с которого начинается запись данных, считанных из потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
    /// </param>
    /// <param name="callback">
    ///   Дополнительный асинхронный обратный вызов по завершении чтения.
    /// </param>
    /// <param name="state">
    ///   Предоставляемый пользователем объект, являющийся отличительным признаком данного конкретного запроса на асинхронное чтение от других запросов.
    /// </param>
    /// <returns>
    ///   Объект, представляющий асинхронное чтение, которое может все еще быть отложена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Попытка асинхронного чтения за концом потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера за вычетом <paramref name="offset" /> — меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает операции чтения.
    /// </exception>
    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._stream == null)
        __Error.ReadNotSupported();
      this.EnsureCanRead();
      int num = 0;
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          Exception error;
          num = this.ReadFromBuffer(buffer, offset, count, out error);
          flag = num == count || error != null;
          if (flag)
          {
            Stream.SynchronousAsyncResult synchronousAsyncResult = error == null ? new Stream.SynchronousAsyncResult(num, state) : new Stream.SynchronousAsyncResult(error, state, false);
            if (callback != null)
              callback((IAsyncResult) synchronousAsyncResult);
            return (IAsyncResult) synchronousAsyncResult;
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.BeginReadFromUnderlyingStream(buffer, offset + num, count - num, callback, state, num, semaphoreLockTask);
    }

    private IAsyncResult BeginReadFromUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, int bytesAlreadySatisfied, Task semaphoreLockTask)
    {
      return TaskToApm.Begin((Task) this.ReadFromUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, bytesAlreadySatisfied, semaphoreLockTask, true), callback, state);
    }

    /// <summary>
    ///   Ожидает завершения отложенной асинхронной операции чтения.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">
    ///   Ссылка на ожидаемый отложенный асинхронный запрос.
    /// </param>
    /// <returns>
    ///   Количество байтов, считанных из потока, от нуля (0) до количества запрошенных байтов.
    ///    Потоки возвращают 0 только в конце потока только, в противном случае они должны блокироваться, пока не будет доступен по крайней мере 1 байт.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Это <see cref="T:System.IAsyncResult" /> объект не был создан путем вызова <see cref="M:System.IO.BufferedStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> для данного класса.
    /// </exception>
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (asyncResult is Stream.SynchronousAsyncResult)
        return Stream.SynchronousAsyncResult.EndRead(asyncResult);
      return TaskToApm.End<int>(asyncResult);
    }

    private Task<int> LastSyncCompletedReadTask(int val)
    {
      Task<int> completedReadTask = this._lastSyncCompletedReadTask;
      if (completedReadTask != null && completedReadTask.Result == val)
        return completedReadTask;
      Task<int> task = Task.FromResult<int>(val);
      this._lastSyncCompletedReadTask = task;
      return task;
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
      this.EnsureNotClosed();
      this.EnsureCanRead();
      int num = 0;
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          Exception error;
          num = this.ReadFromBuffer(buffer, offset, count, out error);
          flag = num == count || error != null;
          if (flag)
            return error == null ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(error);
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.ReadFromUnderlyingStreamAsync(buffer, offset + num, count - num, cancellationToken, num, semaphoreLockTask, false);
    }

    private async Task<int> ReadFromUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask, bool useApmPattern)
    {
      await semaphoreLockTask.ConfigureAwait(false);
      try
      {
        int num1 = this.ReadFromBuffer(array, offset, count);
        if (num1 == count)
          return bytesAlreadySatisfied + num1;
        if (num1 > 0)
        {
          count -= num1;
          offset += num1;
          bytesAlreadySatisfied += num1;
        }
        this._readPos = this._readLen = 0;
        if (this._writePos > 0)
          await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
        Stream stream;
        if (count >= this._bufferSize)
        {
          int num;
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginRead(array, offset, count, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            num = bytesAlreadySatisfied;
            stream = this._stream;
            IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
            return num + stream.EndRead(beginEndAwaitable);
          }
          num = bytesAlreadySatisfied;
          int num2 = await this._stream.ReadAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
          return num + num2;
        }
        this.EnsureBufferAllocated();
        BufferedStream bufferedStream;
        if (useApmPattern)
        {
          this.EnsureBeginEndAwaitableAllocated();
          this._stream.BeginRead(this._buffer, 0, this._bufferSize, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
          bufferedStream = this;
          int readLen = bufferedStream._readLen;
          stream = this._stream;
          IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
          bufferedStream._readLen = stream.EndRead(beginEndAwaitable);
          bufferedStream = (BufferedStream) null;
          stream = (Stream) null;
        }
        else
        {
          bufferedStream = this;
          int readLen = bufferedStream._readLen;
          int num2 = await this._stream.ReadAsync(this._buffer, 0, this._bufferSize, cancellationToken).ConfigureAwait(false);
          bufferedStream._readLen = num2;
          bufferedStream = (BufferedStream) null;
        }
        return bytesAlreadySatisfied + this.ReadFromBuffer(array, offset, count);
      }
      finally
      {
        this.EnsureAsyncActiveSemaphoreInitialized().Release();
      }
    }

    /// <summary>
    ///   Считывает байт из базового потока и возвращает байт, приведенный к типу <see langword="int" />, или значение -1, если чтение из конца потока.
    /// </summary>
    /// <returns>
    ///   Байт приводится к <see langword="int" />, или значение -1, если чтение из конца потока.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Происходит ошибка ввода-вывода, например поток закрывается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override int ReadByte()
    {
      this.EnsureNotClosed();
      this.EnsureCanRead();
      if (this._readPos == this._readLen)
      {
        if (this._writePos > 0)
          this.FlushWrite();
        this.EnsureBufferAllocated();
        this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
        this._readPos = 0;
      }
      if (this._readPos == this._readLen)
        return -1;
      return (int) this._buffer[this._readPos++];
    }

    private void WriteToBuffer(byte[] array, ref int offset, ref int count)
    {
      int byteCount = Math.Min(this._bufferSize - this._writePos, count);
      if (byteCount <= 0)
        return;
      this.EnsureBufferAllocated();
      Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, byteCount);
      this._writePos += byteCount;
      count -= byteCount;
      offset += byteCount;
    }

    private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
    {
      try
      {
        error = (Exception) null;
        this.WriteToBuffer(array, ref offset, ref count);
      }
      catch (Exception ex)
      {
        error = ex;
      }
    }

    /// <summary>
    ///   Копирует байты в буферизованный поток и перемещает текущую позицию в буферизованном потоке на число записанных байтов.
    /// </summary>
    /// <param name="array">
    ///   Массив байтов, из которого копируются <paramref name="count" /> байтов в текущий буферизованный поток.
    /// </param>
    /// <param name="offset">
    ///   Смещение в буфере, с которого начинается копирование байтов в текущий буферизованный поток.
    /// </param>
    /// <param name="count">
    ///   Число байтов, записываемых в текущий буферизованный поток.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="array" /> минус <paramref name="offset" /> — меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт или <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
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
      this.EnsureNotClosed();
      this.EnsureCanWrite();
      if (this._writePos == 0)
        this.ClearReadBufferBeforeWrite();
      int count1 = checked (this._writePos + count);
      if (checked (count1 + count) < checked (this._bufferSize + this._bufferSize))
      {
        this.WriteToBuffer(array, ref offset, ref count);
        if (this._writePos < this._bufferSize)
          return;
        this._stream.Write(this._buffer, 0, this._writePos);
        this._writePos = 0;
        this.WriteToBuffer(array, ref offset, ref count);
      }
      else
      {
        if (this._writePos > 0)
        {
          if (count1 <= this._bufferSize + this._bufferSize && count1 <= 81920)
          {
            this.EnsureShadowBufferAllocated();
            Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
            this._stream.Write(this._buffer, 0, count1);
            this._writePos = 0;
            return;
          }
          this._stream.Write(this._buffer, 0, this._writePos);
          this._writePos = 0;
        }
        this._stream.Write(array, offset, count);
      }
    }

    /// <summary>
    ///   Начинает операцию асинхронной записи.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, содержащий данные для записи в текущий поток.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемое от нуля смещение байтов в буфере <paramref name="buffer" />, с которого начинается копирование байтов в текущий поток.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <param name="callback">
    ///   Метод, вызываемый после завершения операции асинхронной записи.
    /// </param>
    /// <param name="state">
    ///   Предоставляемый пользователем объект, являющийся отличительным признаком данного конкретного запроса на асинхронную запись от других запросов.
    /// </param>
    /// <returns>
    ///   Объект, который ссылается на асинхронную запись, которая может все еще быть отложена.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="buffer" /> Длина за вычетом <paramref name="offset" /> — меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._stream == null)
        __Error.ReadNotSupported();
      this.EnsureCanWrite();
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          if (this._writePos == 0)
            this.ClearReadBufferBeforeWrite();
          flag = count < this._bufferSize - this._writePos;
          if (flag)
          {
            Exception error;
            this.WriteToBuffer(buffer, ref offset, ref count, out error);
            Stream.SynchronousAsyncResult synchronousAsyncResult = error == null ? new Stream.SynchronousAsyncResult(state) : new Stream.SynchronousAsyncResult(error, state, true);
            if (callback != null)
              callback((IAsyncResult) synchronousAsyncResult);
            return (IAsyncResult) synchronousAsyncResult;
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.BeginWriteToUnderlyingStream(buffer, offset, count, callback, state, semaphoreLockTask);
    }

    private IAsyncResult BeginWriteToUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, Task semaphoreLockTask)
    {
      return TaskToApm.Begin(this.WriteToUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, semaphoreLockTask, true), callback, state);
    }

    /// <summary>
    ///   Завершает асинхронную операцию записи и блокирует до тех пор, пока не будет завершена операция ввода-вывода.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">Отложенный асинхронный запрос.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Это <see cref="T:System.IAsyncResult" /> объект не был создан путем вызова <see cref="M:System.IO.BufferedStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> для данного класса.
    /// </exception>
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (asyncResult is Stream.SynchronousAsyncResult)
        Stream.SynchronousAsyncResult.EndWrite(asyncResult);
      else
        TaskToApm.End(asyncResult);
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
        return (Task) Task.FromCancellation<int>(cancellationToken);
      this.EnsureNotClosed();
      this.EnsureCanWrite();
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          if (this._writePos == 0)
            this.ClearReadBufferBeforeWrite();
          flag = count < this._bufferSize - this._writePos;
          if (flag)
          {
            Exception error;
            this.WriteToBuffer(buffer, ref offset, ref count, out error);
            return error == null ? Task.CompletedTask : Task.FromException(error);
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.WriteToUnderlyingStreamAsync(buffer, offset, count, cancellationToken, semaphoreLockTask, false);
    }

    private async Task WriteToUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, Task semaphoreLockTask, bool useApmPattern)
    {
      await semaphoreLockTask.ConfigureAwait(false);
      try
      {
        if (this._writePos == 0)
          this.ClearReadBufferBeforeWrite();
        int totalUserBytes = checked (this._writePos + count);
        Stream stream;
        if (checked (totalUserBytes + count) < checked (this._bufferSize + this._bufferSize))
        {
          this.WriteToBuffer(array, ref offset, ref count);
          if (this._writePos < this._bufferSize)
            return;
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            stream = this._stream;
            IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
            stream.EndWrite(beginEndAwaitable);
            stream = (Stream) null;
          }
          else
            await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
          this._writePos = 0;
          this.WriteToBuffer(array, ref offset, ref count);
        }
        else
        {
          if (this._writePos > 0)
          {
            if (totalUserBytes <= this._bufferSize + this._bufferSize && totalUserBytes <= 81920)
            {
              this.EnsureShadowBufferAllocated();
              Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
              if (useApmPattern)
              {
                this.EnsureBeginEndAwaitableAllocated();
                this._stream.BeginWrite(this._buffer, 0, totalUserBytes, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
                stream = this._stream;
                IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
                stream.EndWrite(beginEndAwaitable);
                stream = (Stream) null;
              }
              else
                await this._stream.WriteAsync(this._buffer, 0, totalUserBytes, cancellationToken).ConfigureAwait(false);
              this._writePos = 0;
              return;
            }
            if (useApmPattern)
            {
              this.EnsureBeginEndAwaitableAllocated();
              this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
              stream = this._stream;
              IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
              stream.EndWrite(beginEndAwaitable);
              stream = (Stream) null;
            }
            else
              await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
            this._writePos = 0;
          }
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginWrite(array, offset, count, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            stream = this._stream;
            IAsyncResult beginEndAwaitable = await this._beginEndAwaitable;
            stream.EndWrite(beginEndAwaitable);
            stream = (Stream) null;
          }
          else
            await this._stream.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
        }
      }
      finally
      {
        this.EnsureAsyncActiveSemaphoreInitialized().Release();
      }
    }

    /// <summary>
    ///   Записывает байт в текущее положение в буфере потока.
    /// </summary>
    /// <param name="value">
    ///   Байт, который необходимо записать в поток.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override void WriteByte(byte value)
    {
      this.EnsureNotClosed();
      if (this._writePos == 0)
      {
        this.EnsureCanWrite();
        this.ClearReadBufferBeforeWrite();
        this.EnsureBufferAllocated();
      }
      if (this._writePos >= this._bufferSize - 1)
        this.FlushWrite();
      this._buffer[this._writePos++] = value;
    }

    /// <summary>Задает позицию в текущем буферизованном потоке.</summary>
    /// <param name="offset">
    ///   Смещение байтов относительно <paramref name="origin" />.
    /// </param>
    /// <param name="origin">
    ///   Значение типа <see cref="T:System.IO.SeekOrigin" /> определяет точку отсчета для получения новой позиции.
    /// </param>
    /// <returns>Новая позиция в текущем буферизованном потоке.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток не открыт или является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      this.EnsureNotClosed();
      this.EnsureCanSeek();
      if (this._writePos > 0)
      {
        this.FlushWrite();
        return this._stream.Seek(offset, origin);
      }
      if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
        offset -= (long) (this._readLen - this._readPos);
      long position = this.Position;
      long num = this._stream.Seek(offset, origin);
      this._readPos = (int) (num - (position - (long) this._readPos));
      if (0 <= this._readPos && this._readPos < this._readLen)
        this._stream.Seek((long) (this._readLen - this._readPos), SeekOrigin.Current);
      else
        this._readPos = this._readLen = 0;
      return num;
    }

    /// <summary>Задает длину буферизованного потока.</summary>
    /// <param name="value">
    ///   Целое число, представляющее требуемую длину текущего буферизованного потока в байтах.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="value" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток не открыт или является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись и поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NegFileSize"));
      this.EnsureNotClosed();
      this.EnsureCanSeek();
      this.EnsureCanWrite();
      this.Flush();
      this._stream.SetLength(value);
    }
  }
}
