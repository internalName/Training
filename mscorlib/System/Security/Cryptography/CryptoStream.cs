// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет поток, который связывает потоки данных с криптографическими преобразованиями.
  /// </summary>
  [ComVisible(true)]
  public class CryptoStream : Stream, IDisposable
  {
    private Stream _stream;
    private ICryptoTransform _Transform;
    private byte[] _InputBuffer;
    private int _InputBufferIndex;
    private int _InputBlockSize;
    private byte[] _OutputBuffer;
    private int _OutputBufferIndex;
    private int _OutputBlockSize;
    private CryptoStreamMode _transformMode;
    private bool _canRead;
    private bool _canWrite;
    private bool _finalBlockTransformed;
    private bool _leaveOpen;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CryptoStream" />, используя целевой поток данных, преобразования и режим потока.
    /// </summary>
    /// <param name="stream">
    ///   Поток для выполнения криптографического преобразования.
    /// </param>
    /// <param name="transform">
    ///   Криптографическое преобразование, применяемое к потоку.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.Security.Cryptography.CryptoStreamMode" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> недоступен для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> не может быть перезаписан.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> недопустим.
    /// </exception>
    public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
      : this(stream, transform, mode, false)
    {
    }

    public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
    {
      this._stream = stream;
      this._transformMode = mode;
      this._Transform = transform;
      this._leaveOpen = leaveOpen;
      switch (this._transformMode)
      {
        case CryptoStreamMode.Read:
          if (!this._stream.CanRead)
            throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"), nameof (stream));
          this._canRead = true;
          break;
        case CryptoStreamMode.Write:
          if (!this._stream.CanWrite)
            throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"), nameof (stream));
          this._canWrite = true;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      }
      this.InitializeBuffer();
    }

    /// <summary>
    ///   Возвращает значение, определяющее, доступен ли текущий поток <see cref="T:System.Security.Cryptography.CryptoStream" /> для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток доступен для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanRead
    {
      get
      {
        return this._canRead;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, возможен ли поиск в текущем потоке <see cref="T:System.Security.Cryptography.CryptoStream" />.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее, доступен ли текущий поток <see cref="T:System.Security.Cryptography.CryptoStream" /> для записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток доступен для записи; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanWrite
    {
      get
      {
        return this._canWrite;
      }
    }

    /// <summary>Возвращает длину потока в байтах.</summary>
    /// <returns>Данное свойство не поддерживается.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Данное свойство не поддерживается.
    /// </exception>
    public override long Length
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    /// <summary>Возвращает или задает позицию в текущем потоке.</summary>
    /// <returns>Данное свойство не поддерживается.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Данное свойство не поддерживается.
    /// </exception>
    public override long Position
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
      set
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, записан ли последний буферный блок в базовый поток.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если последний блок передан. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool HasFlushedFinalBlock
    {
      get
      {
        return this._finalBlockTransformed;
      }
    }

    /// <summary>
    ///   Обновляет базовый источник данных или хранилище текущим содержимым буфера, а затем очищает буфер.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Ключ поврежден, что может привести к неправильному заполнению потока.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток недоступен для записи.
    /// 
    ///   -или-
    /// 
    ///   Последний блок уже преобразован.
    /// </exception>
    public void FlushFinalBlock()
    {
      if (this._finalBlockTransformed)
        throw new NotSupportedException(Environment.GetResourceString("Cryptography_CryptoStream_FlushFinalBlockTwice"));
      byte[] buffer = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
      this._finalBlockTransformed = true;
      if (this._canWrite && this._OutputBufferIndex > 0)
      {
        this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
        this._OutputBufferIndex = 0;
      }
      if (this._canWrite)
        this._stream.Write(buffer, 0, buffer.Length);
      CryptoStream stream = this._stream as CryptoStream;
      if (stream != null)
      {
        if (!stream.HasFlushedFinalBlock)
          stream.FlushFinalBlock();
      }
      else
        this._stream.Flush();
      if (this._InputBuffer != null)
        Array.Clear((Array) this._InputBuffer, 0, this._InputBuffer.Length);
      if (this._OutputBuffer == null)
        return;
      Array.Clear((Array) this._OutputBuffer, 0, this._OutputBuffer.Length);
    }

    /// <summary>
    ///   Очищает все буферы для текущего потока и вызывает запись всех буферизированных данных в базовое устройство.
    /// </summary>
    public override void Flush()
    {
    }

    /// <summary>
    ///   Асинхронно очищает все буферы текущего потока, вызывает запись буферизованных данных в базовое устройство и отслеживает запросы отмены.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    ///    Значение по умолчанию — <see cref="P:System.Threading.CancellationToken.None" />.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток был удален.
    /// </exception>
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (this.GetType() != typeof (CryptoStream))
        return base.FlushAsync(cancellationToken);
      if (!cancellationToken.IsCancellationRequested)
        return Task.CompletedTask;
      return Task.FromCancellation(cancellationToken);
    }

    /// <summary>Задает позицию в текущем потоке.</summary>
    /// <param name="offset">
    ///   Смещение в байтах относительно параметра <paramref name="origin" />.
    /// </param>
    /// <param name="origin">
    ///   Объект <see cref="T:System.IO.SeekOrigin" />, задающий опорную точку, используемую для получения новой позиции.
    /// </param>
    /// <returns>Этот метод не поддерживается.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    /// <summary>Устанавливает длину текущего потока.</summary>
    /// <param name="value">Нужная длина текущего потока в байтах.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это свойство существует только для поддержки наследования от <see cref="T:System.IO.Stream" />, и не может использоваться.
    /// </exception>
    public override void SetLength(long value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    /// <summary>
    ///   Считывает последовательность байтов из текущего потока и перемещает позицию внутри потока на число считанных байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов.
    ///    Максимальное число байтов <paramref name="count" /> считывается из текущего потока и сохраняется в параметре <paramref name="buffer" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция байта в параметре <paramref name="buffer" />, с которой начинается сохранение данных, считанных из текущего потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное количество байтов, которое должно быть считано из текущего потока.
    /// </param>
    /// <returns>
    ///   Общее количество байтов, считанных в буфер.
    ///    Это число может быть меньше количества запрошенных байтов, если нужное число байтов в настоящее время недоступно, а также равняться нулю, если был достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> Связанных с текущим <see cref="T:System.Security.Cryptography.CryptoStream" /> объекта не соответствует основной поток.
    ///     Например, это исключение вызывается при использовании <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> с основной поток, который доступен только для записи.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> Меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> Меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Thesum из <paramref name="count" /> и <paramref name="offset" /> длиннее, чем длина буфера.
    /// </exception>
    public override int Read([In, Out] byte[] buffer, int offset, int count)
    {
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num1 = count;
      int dstOffsetBytes = offset;
      if (this._OutputBufferIndex != 0)
      {
        if (this._OutputBufferIndex <= count)
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, this._OutputBufferIndex);
          num1 -= this._OutputBufferIndex;
          dstOffsetBytes += this._OutputBufferIndex;
          this._OutputBufferIndex = 0;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, count);
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, count, (Array) this._OutputBuffer, 0, this._OutputBufferIndex - count);
          this._OutputBufferIndex -= count;
          return count;
        }
      }
      if (this._finalBlockTransformed)
        return count - num1;
      if (num1 > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
      {
        int length = num1 / this._OutputBlockSize * this._InputBlockSize;
        byte[] numArray = new byte[length];
        Buffer.InternalBlockCopy((Array) this._InputBuffer, 0, (Array) numArray, 0, this._InputBufferIndex);
        int num2 = this._InputBufferIndex + this._stream.Read(numArray, this._InputBufferIndex, length - this._InputBufferIndex);
        this._InputBufferIndex = 0;
        if (num2 <= this._InputBlockSize)
        {
          this._InputBuffer = numArray;
          this._InputBufferIndex = num2;
        }
        else
        {
          int num3 = num2 / this._InputBlockSize * this._InputBlockSize;
          int byteCount1 = num2 - num3;
          if (byteCount1 != 0)
          {
            this._InputBufferIndex = byteCount1;
            Buffer.InternalBlockCopy((Array) numArray, num3, (Array) this._InputBuffer, 0, byteCount1);
          }
          byte[] outputBuffer = new byte[num3 / this._InputBlockSize * this._OutputBlockSize];
          int byteCount2 = this._Transform.TransformBlock(numArray, 0, num3, outputBuffer, 0);
          Buffer.InternalBlockCopy((Array) outputBuffer, 0, (Array) buffer, dstOffsetBytes, byteCount2);
          Array.Clear((Array) numArray, 0, numArray.Length);
          Array.Clear((Array) outputBuffer, 0, outputBuffer.Length);
          num1 -= byteCount2;
          dstOffsetBytes += byteCount2;
        }
      }
      while (num1 > 0)
      {
        while (this._InputBufferIndex < this._InputBlockSize)
        {
          int num2 = this._stream.Read(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
          if (num2 != 0)
          {
            this._InputBufferIndex += num2;
          }
          else
          {
            byte[] numArray = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
            this._OutputBuffer = numArray;
            this._OutputBufferIndex = numArray.Length;
            this._finalBlockTransformed = true;
            if (num1 < this._OutputBufferIndex)
            {
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, num1);
              this._OutputBufferIndex -= num1;
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, num1, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
              return count;
            }
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, this._OutputBufferIndex);
            int num3 = num1 - this._OutputBufferIndex;
            this._OutputBufferIndex = 0;
            return count - num3;
          }
        }
        int byteCount = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
        this._InputBufferIndex = 0;
        if (num1 >= byteCount)
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, byteCount);
          dstOffsetBytes += byteCount;
          num1 -= byteCount;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, num1);
          this._OutputBufferIndex = byteCount - num1;
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, num1, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
          return count;
        }
      }
      return count;
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
    ///    Значение параметра <paramref name="TResult" /> объекта задач содержит общее число байтов, считанных в буфер.
    ///    Результат может быть меньше запрошенного числа байтов, если число текущих доступных байтов меньше запрошенного числа, или результат может быть равен 0 (нулю), если был достигнут конец потока.
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
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (CryptoStream))
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
    }

    private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      await new CryptoStream.HopToThreadPoolAwaitable();
      SemaphoreSlim sem = this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        int bytesToDeliver = count;
        int currentOutputIndex = offset;
        if (this._OutputBufferIndex != 0)
        {
          if (this._OutputBufferIndex <= count)
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, this._OutputBufferIndex);
            bytesToDeliver -= this._OutputBufferIndex;
            currentOutputIndex += this._OutputBufferIndex;
            this._OutputBufferIndex = 0;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, count);
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, count, (Array) this._OutputBuffer, 0, this._OutputBufferIndex - count);
            this._OutputBufferIndex -= count;
            return count;
          }
        }
        if (this._finalBlockTransformed)
          return count - bytesToDeliver;
        ConfiguredTaskAwaitable<int> configuredTaskAwaitable;
        if (bytesToDeliver > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
        {
          int length = bytesToDeliver / this._OutputBlockSize * this._InputBlockSize;
          byte[] tempInputBuffer = new byte[length];
          Buffer.InternalBlockCopy((Array) this._InputBuffer, 0, (Array) tempInputBuffer, 0, this._InputBufferIndex);
          int inputBufferIndex = this._InputBufferIndex;
          configuredTaskAwaitable = this._stream.ReadAsync(tempInputBuffer, this._InputBufferIndex, length - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
          int num1 = await configuredTaskAwaitable;
          int num2 = inputBufferIndex + num1;
          this._InputBufferIndex = 0;
          if (num2 <= this._InputBlockSize)
          {
            this._InputBuffer = tempInputBuffer;
            this._InputBufferIndex = num2;
          }
          else
          {
            int num3 = num2 / this._InputBlockSize * this._InputBlockSize;
            int byteCount1 = num2 - num3;
            if (byteCount1 != 0)
            {
              this._InputBufferIndex = byteCount1;
              Buffer.InternalBlockCopy((Array) tempInputBuffer, num3, (Array) this._InputBuffer, 0, byteCount1);
            }
            byte[] outputBuffer = new byte[num3 / this._InputBlockSize * this._OutputBlockSize];
            int byteCount2 = this._Transform.TransformBlock(tempInputBuffer, 0, num3, outputBuffer, 0);
            Buffer.InternalBlockCopy((Array) outputBuffer, 0, (Array) buffer, currentOutputIndex, byteCount2);
            Array.Clear((Array) tempInputBuffer, 0, tempInputBuffer.Length);
            Array.Clear((Array) outputBuffer, 0, outputBuffer.Length);
            bytesToDeliver -= byteCount2;
            currentOutputIndex += byteCount2;
            tempInputBuffer = (byte[]) null;
          }
        }
        while (bytesToDeliver > 0)
        {
          while (this._InputBufferIndex < this._InputBlockSize)
          {
            configuredTaskAwaitable = this._stream.ReadAsync(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
            int num = await configuredTaskAwaitable;
            if (num != 0)
            {
              this._InputBufferIndex += num;
            }
            else
            {
              byte[] numArray = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
              this._OutputBuffer = numArray;
              this._OutputBufferIndex = numArray.Length;
              this._finalBlockTransformed = true;
              if (bytesToDeliver < this._OutputBufferIndex)
              {
                Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, bytesToDeliver);
                this._OutputBufferIndex -= bytesToDeliver;
                Buffer.InternalBlockCopy((Array) this._OutputBuffer, bytesToDeliver, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
                return count;
              }
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, this._OutputBufferIndex);
              bytesToDeliver -= this._OutputBufferIndex;
              this._OutputBufferIndex = 0;
              return count - bytesToDeliver;
            }
          }
          int byteCount = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
          this._InputBufferIndex = 0;
          if (bytesToDeliver >= byteCount)
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, byteCount);
            currentOutputIndex += byteCount;
            bytesToDeliver -= byteCount;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, bytesToDeliver);
            this._OutputBufferIndex = byteCount - bytesToDeliver;
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, bytesToDeliver, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
            return count;
          }
        }
        return count;
      }
      finally
      {
        sem.Release();
      }
    }

    /// <summary>
    ///   Записывает последовательность байтов в текущий <see cref="T:System.Security.Cryptography.CryptoStream" /> и перемещает текущую позицию внутри потока на число записанных байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов.
    ///    Этот метод копирует байты <paramref name="count" /> из <paramref name="buffer" /> в текущий поток.
    /// </param>
    /// <param name="offset">
    ///   Позиция байта в <paramref name="buffer" />, с которой начинается копирование байтов в текущий поток.
    /// </param>
    /// <param name="count">
    ///   Количество байтов, которое необходимо записать в текущий поток.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> Связанный с текущим <see cref="T:System.Security.Cryptography.CryptoStream" /> объект не соответствует базового потока.
    ///     Например, это исключение возникает при использовании <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" /> с основной поток, который доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="offset" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> Меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="count" /> и <paramref name="offset" /> длиннее, чем длина буфера.
    /// </exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int byteCount = count;
      int num1 = offset;
      if (this._InputBufferIndex > 0)
      {
        if (count >= this._InputBlockSize - this._InputBufferIndex)
        {
          Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
          num1 += this._InputBlockSize - this._InputBufferIndex;
          byteCount -= this._InputBlockSize - this._InputBufferIndex;
          this._InputBufferIndex = this._InputBlockSize;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, count);
          this._InputBufferIndex += count;
          return;
        }
      }
      if (this._OutputBufferIndex > 0)
      {
        this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
        this._OutputBufferIndex = 0;
      }
      if (this._InputBufferIndex == this._InputBlockSize)
      {
        this._stream.Write(this._OutputBuffer, 0, this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0));
        this._InputBufferIndex = 0;
      }
      while (byteCount > 0)
      {
        if (byteCount >= this._InputBlockSize)
        {
          if (this._Transform.CanTransformMultipleBlocks)
          {
            int num2 = byteCount / this._InputBlockSize;
            int inputCount = num2 * this._InputBlockSize;
            byte[] numArray = new byte[num2 * this._OutputBlockSize];
            int count1 = this._Transform.TransformBlock(buffer, num1, inputCount, numArray, 0);
            this._stream.Write(numArray, 0, count1);
            num1 += inputCount;
            byteCount -= inputCount;
          }
          else
          {
            this._stream.Write(this._OutputBuffer, 0, this._Transform.TransformBlock(buffer, num1, this._InputBlockSize, this._OutputBuffer, 0));
            num1 += this._InputBlockSize;
            byteCount -= this._InputBlockSize;
          }
        }
        else
        {
          Buffer.InternalBlockCopy((Array) buffer, num1, (Array) this._InputBuffer, 0, byteCount);
          this._InputBufferIndex += byteCount;
          break;
        }
      }
    }

    /// <summary>
    ///   Асинхронно записывает последовательность байтов в текущий поток, перемещает текущую позицию внутри потока на число записанных байтов и отслеживает запросы отмены.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, из которого записываются данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов (начиная с нуля) в <paramref name="buffer" />, с которого начинается запись байтов в поток.
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
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (CryptoStream))
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
    }

    private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      await new CryptoStream.HopToThreadPoolAwaitable();
      SemaphoreSlim sem = this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        int bytesToWrite = count;
        int currentInputIndex = offset;
        if (this._InputBufferIndex > 0)
        {
          if (count >= this._InputBlockSize - this._InputBufferIndex)
          {
            Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
            currentInputIndex += this._InputBlockSize - this._InputBufferIndex;
            bytesToWrite -= this._InputBlockSize - this._InputBufferIndex;
            this._InputBufferIndex = this._InputBlockSize;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, count);
            this._InputBufferIndex += count;
            return;
          }
        }
        if (this._OutputBufferIndex > 0)
        {
          await this._stream.WriteAsync(this._OutputBuffer, 0, this._OutputBufferIndex, cancellationToken).ConfigureAwait(false);
          this._OutputBufferIndex = 0;
        }
        if (this._InputBufferIndex == this._InputBlockSize)
        {
          await this._stream.WriteAsync(this._OutputBuffer, 0, this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0), cancellationToken).ConfigureAwait(false);
          this._InputBufferIndex = 0;
        }
        while (bytesToWrite > 0)
        {
          if (bytesToWrite >= this._InputBlockSize)
          {
            if (this._Transform.CanTransformMultipleBlocks)
            {
              int num = bytesToWrite / this._InputBlockSize;
              int numWholeBlocksInBytes = num * this._InputBlockSize;
              byte[] numArray = new byte[num * this._OutputBlockSize];
              int count1 = this._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, numArray, 0);
              await this._stream.WriteAsync(numArray, 0, count1, cancellationToken).ConfigureAwait(false);
              currentInputIndex += numWholeBlocksInBytes;
              bytesToWrite -= numWholeBlocksInBytes;
            }
            else
            {
              await this._stream.WriteAsync(this._OutputBuffer, 0, this._Transform.TransformBlock(buffer, currentInputIndex, this._InputBlockSize, this._OutputBuffer, 0), cancellationToken).ConfigureAwait(false);
              currentInputIndex += this._InputBlockSize;
              bytesToWrite -= this._InputBlockSize;
            }
          }
          else
          {
            Buffer.InternalBlockCopy((Array) buffer, currentInputIndex, (Array) this._InputBuffer, 0, bytesToWrite);
            this._InputBufferIndex += bytesToWrite;
            break;
          }
        }
      }
      finally
      {
        sem.Release();
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Security.Cryptography.CryptoStream" />.
    /// </summary>
    public void Clear()
    {
      this.Close();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.CryptoStream" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (!this._finalBlockTransformed)
          this.FlushFinalBlock();
        if (this._leaveOpen)
          return;
        this._stream.Close();
      }
      finally
      {
        try
        {
          this._finalBlockTransformed = true;
          if (this._InputBuffer != null)
            Array.Clear((Array) this._InputBuffer, 0, this._InputBuffer.Length);
          if (this._OutputBuffer != null)
            Array.Clear((Array) this._OutputBuffer, 0, this._OutputBuffer.Length);
          this._InputBuffer = (byte[]) null;
          this._OutputBuffer = (byte[]) null;
          this._canRead = false;
          this._canWrite = false;
        }
        finally
        {
          base.Dispose(disposing);
        }
      }
    }

    private void InitializeBuffer()
    {
      if (this._Transform == null)
        return;
      this._InputBlockSize = this._Transform.InputBlockSize;
      this._InputBuffer = new byte[this._InputBlockSize];
      this._OutputBlockSize = this._Transform.OutputBlockSize;
      this._OutputBuffer = new byte[this._OutputBlockSize];
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct HopToThreadPoolAwaitable : INotifyCompletion
    {
      public CryptoStream.HopToThreadPoolAwaitable GetAwaiter()
      {
        return this;
      }

      public bool IsCompleted
      {
        get
        {
          return false;
        }
      }

      public void OnCompleted(Action continuation)
      {
        Task.Run(continuation);
      }

      public void GetResult()
      {
      }
    }
  }
}
