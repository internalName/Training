// Decompiled with JetBrains decompiler
// Type: System.IO.Stream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет универсальное представление последовательности байтов.
  ///    Этот класс является абстрактным.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Stream : MarshalByRefObject, IDisposable
  {
    /// <summary>
    ///   Объект <see langword="Stream" /> без резервного хранилища.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly Stream Null = (Stream) new Stream.NullStream();
    private const int _DefaultCopyBufferSize = 81920;
    [NonSerialized]
    private Stream.ReadWriteTask _activeReadWriteTask;
    [NonSerialized]
    private SemaphoreSlim _asyncActiveSemaphore;

    internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
    {
      return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, (Func<SemaphoreSlim>) (() => new SemaphoreSlim(1, 1)));
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение, показывающее, поддерживает ли текущий поток возможность чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает чтение; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanRead { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение, которое показывает, поддерживается ли в текущем потоке возможность поиска.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает поиск; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanSeek { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение, которое показывает, может ли для данного потока истечь время ожидания.
    /// </summary>
    /// <returns>
    ///   Значение, которое показывает, может ли для данного потока истечь время ожидания.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual bool CanTimeout
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение, которое показывает, поддерживает ли текущий поток возможность записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если поток поддерживает запись; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanWrite { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе получает длину потока в байтах.
    /// </summary>
    /// <returns>
    ///   Длинное значение, представляющее длину потока в байтах.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Класс, производный от <see langword="Stream" /> не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract long Length { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе получает или задает позицию в текущем потоке.
    /// </summary>
    /// <returns>Текущее положение в потоке.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract long Position { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает или задает значение в миллисекундах, определяющее период, в течение которого поток будет пытаться выполнить операцию чтения, прежде чем истечет время ожидания.
    /// </summary>
    /// <returns>
    ///   Значение в миллисекундах, определяющее период, в течение которого поток будет пытаться выполнить операцию чтения, прежде чем истечет время ожидания.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="P:System.IO.Stream.ReadTimeout" /> Метод всегда создает исключение <see cref="T:System.InvalidOperationException" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int ReadTimeout
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
      [__DynamicallyInvokable] set
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
    }

    /// <summary>
    ///   Возвращает или задает значение в миллисекундах, определяющее период, в течение которого поток будет пытаться выполнить операцию записи, прежде чем истечет время ожидания.
    /// </summary>
    /// <returns>
    ///   Значение в миллисекундах, определяющее период, в течение которого поток будет пытаться выполнить операцию записи, прежде чем истечет время ожидания.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="P:System.IO.Stream.WriteTimeout" /> Метод всегда создает исключение <see cref="T:System.InvalidOperationException" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int WriteTimeout
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
      [__DynamicallyInvokable] set
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
    }

    /// <summary>
    ///   Асинхронно считывает байты из текущего потока и записывает их в другой поток.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию копирования.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток или поток назначения удаляется.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает чтение или на целевой поток не поддерживает запись.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task CopyToAsync(Stream destination)
    {
      return this.CopyToAsync(destination, 81920);
    }

    /// <summary>
    ///   Асинхронно считывает байты из текущего потока и записывает их в другой поток, используя указанный размер буфера.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер (в байтах) буфера.
    ///    Это значение должно быть больше нуля.
    ///    Размер по умолчанию — 81920.
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
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task CopyToAsync(Stream destination, int bufferSize)
    {
      return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
    }

    /// <summary>
    ///   Асинхронно считывает байты из текущего потока и записывает их в другой поток, используя указанный размер буфера и токен отмены.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер (в байтах) буфера.
    ///    Это значение должно быть больше нуля.
    ///    Размер по умолчанию — 81920.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    ///    Значение по умолчанию — <see cref="P:System.Threading.CancellationToken.None" />.
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
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
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
      return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
    }

    private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
      byte[] buffer = new byte[bufferSize];
      while (true)
      {
        int num = await this.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
        int bytesRead;
        if ((bytesRead = num) != 0)
          await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
        else
          break;
      }
    }

    /// <summary>
    ///   Считывает байты из текущего потока и записывает их в другой поток.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает чтение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destination" /> не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток или <paramref name="destination" /> были закрыты перед <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> был вызван метод.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(Stream destination)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (!this.CanRead && !this.CanWrite)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!destination.CanRead && !destination.CanWrite)
        throw new ObjectDisposedException(nameof (destination), Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (!destination.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      this.InternalCopyTo(destination, 81920);
    }

    /// <summary>
    ///   Считывает байты из текущего потока и записывает их в другой поток, используя указанный размер буфера.
    /// </summary>
    /// <param name="destination">
    ///   Поток, в который будет скопировано содержимое текущего потока.
    /// </param>
    /// <param name="bufferSize">
    ///   Размер буфера.
    ///    Это значение должно быть больше нуля.
    ///    Размер по умолчанию — 81920.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="bufferSize" /> имеет отрицательное значение или равен нулю.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий поток не поддерживает чтение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destination" /> не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий поток или <paramref name="destination" /> были закрыты перед <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> был вызван метод.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(Stream destination, int bufferSize)
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
      this.InternalCopyTo(destination, bufferSize);
    }

    private void InternalCopyTo(Stream destination, int bufferSize)
    {
      byte[] buffer = new byte[bufferSize];
      int count;
      while ((count = this.Read(buffer, 0, buffer.Length)) != 0)
        destination.Write(buffer, 0, count);
    }

    /// <summary>
    ///   Закрывает текущий поток и отключает все ресурсы (например, сокеты и файловые дескрипторы), связанные с текущим потоком.
    ///    Вместо вызова данного метода, убедитесь в том, что поток надлежащим образом ликвидирован.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.IO.Stream" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Close();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.Stream" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   При переопределении в производном классе очищает все буферы данного потока и вызывает запись данных буферов в базовое устройство.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract void Flush();

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
    public Task FlushAsync()
    {
      return this.FlushAsync(CancellationToken.None);
    }

    /// <summary>
    ///   Асинхронно очищает все буферы данного потока, вызывает запись буферизованных данных в базовое устройство и отслеживает запросы отмены.
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
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task FlushAsync(CancellationToken cancellationToken)
    {
      TaskFactory factory = Task.Factory;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = 8;
      TaskScheduler scheduler = TaskScheduler.Default;
      return factory.StartNew((Action<object>) (state => ((Stream) state).Flush()), (object) this, cancellationToken1, (TaskCreationOptions) num, scheduler);
    }

    /// <summary>
    ///   Выделяет объект <see cref="T:System.Threading.WaitHandle" />.
    /// </summary>
    /// <returns>
    ///   Ссылка на выделенный объект <see langword="WaitHandle" />.
    /// </returns>
    [Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
    protected virtual WaitHandle CreateWaitHandle()
    {
      return (WaitHandle) new ManualResetEvent(false);
    }

    /// <summary>
    ///   Начинает операцию асинхронного чтения.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" />; см. раздел "Примечания".)
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
    ///   <see cref="T:System.IAsyncResult" /> представляет асинхронное чтение, которое все еще может быть отложено.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Попытка асинхронного чтения за концом потока или возникновение ошибки диска.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Один или несколько аргументов являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий <see langword="Stream" /> реализация не поддерживает операции чтения.
    /// </exception>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return this.BeginReadInternal(buffer, offset, count, callback, state, false);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
    {
      if (!this.CanRead)
        __Error.ReadNotSupported();
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return this.BlockingBeginRead(buffer, offset, count, callback, state);
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task asyncWaiter = (Task) null;
      if (serializeAsynchronously)
        asyncWaiter = semaphoreSlim.WaitAsync();
      else
        semaphoreSlim.Wait();
      Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, (Func<object, int>) (_param1 =>
      {
        Stream.ReadWriteTask internalCurrent = Task.InternalCurrent as Stream.ReadWriteTask;
        int num = internalCurrent._stream.Read(internalCurrent._buffer, internalCurrent._offset, internalCurrent._count);
        internalCurrent.ClearBeginState();
        return num;
      }), state, this, buffer, offset, count, callback);
      if (asyncWaiter != null)
        this.RunReadWriteTaskWhenReady(asyncWaiter, readWriteTask);
      else
        this.RunReadWriteTask(readWriteTask);
      return (IAsyncResult) readWriteTask;
    }

    /// <summary>
    ///   Ожидает завершения отложенного асинхронного чтения.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">
    ///   Ссылка на отложенный асинхронный запрос, который необходимо завершить.
    /// </param>
    /// <returns>
    ///   Количество байтов, считанных из потока, от нуля (0) до количества запрошенных байтов.
    ///    Потоки возвращают нуль (0) только в конце. В противном случае они должны блокироваться до тех пор, пока доступен хотя бы один байт.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Дескриптор для отложенной операции чтения недоступен.
    /// 
    ///   -или-
    /// 
    ///   Ожидающие операции не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="asyncResult" /> был получен не из <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> метода для текущего потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт, или произошла внутренняя ошибка.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return Stream.BlockingEndRead(asyncResult);
      Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
      if (activeReadWriteTask == null)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      if (activeReadWriteTask != asyncResult)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      if (!activeReadWriteTask._isRead)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      try
      {
        return activeReadWriteTask.GetAwaiter().GetResult();
      }
      finally
      {
        this._activeReadWriteTask = (Stream.ReadWriteTask) null;
        this._asyncActiveSemaphore.Release();
      }
    }

    /// <summary>
    ///   Асинхронно считывает последовательность байтов из текущего потока и перемещает позицию внутри потока на число считанных байтов.
    /// </summary>
    /// <param name="buffer">Буфер, в который записываются данные.</param>
    /// <param name="offset">
    ///   Смещение байтов в <paramref name="buffer" />, с которого начинается запись данных из потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное число байтов, предназначенных для чтения.
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
    public Task<int> ReadAsync(byte[] buffer, int offset, int count)
    {
      return this.ReadAsync(buffer, offset, count, CancellationToken.None);
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
    public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        return this.BeginEndReadAsync(buffer, offset, count);
      return Task.FromCancellation<int>(cancellationToken);
    }

    private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
    {
      Stream.ReadWriteParameters args1 = new Stream.ReadWriteParameters()
      {
        Buffer = buffer,
        Offset = offset,
        Count = count
      };
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> func = (Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult>) ((stream, args, callback, state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state));
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> beginMethod;
      return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, args1, beginMethod, (Func<Stream, IAsyncResult, int>) ((stream, asyncResult) => stream.EndRead(asyncResult)));
    }

    /// <summary>
    ///   Начинает операцию асинхронной записи.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, из которого записываются данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
    /// <param name="callback">
    ///   Дополнительный асинхронный обратный вызов по завершении записи.
    /// </param>
    /// <param name="state">
    ///   Предоставляемый пользователем объект, являющийся отличительным признаком данного конкретного запроса на асинхронную запись от других запросов.
    /// </param>
    /// <returns>
    ///   <see langword="IAsyncResult" /> представляет асинхронную запись, которая все еще может быть отложена.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Попытка асинхронной записи за пределами конца потока или возникновение ошибки диска.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Один или несколько аргументов являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий <see langword="Stream" /> реализация не поддерживает операции записи.
    /// </exception>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return this.BeginWriteInternal(buffer, offset, count, callback, state, false);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
    {
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return this.BlockingBeginWrite(buffer, offset, count, callback, state);
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task asyncWaiter = (Task) null;
      if (serializeAsynchronously)
        asyncWaiter = semaphoreSlim.WaitAsync();
      else
        semaphoreSlim.Wait();
      Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, (Func<object, int>) (_param1 =>
      {
        Stream.ReadWriteTask internalCurrent = Task.InternalCurrent as Stream.ReadWriteTask;
        internalCurrent._stream.Write(internalCurrent._buffer, internalCurrent._offset, internalCurrent._count);
        internalCurrent.ClearBeginState();
        return 0;
      }), state, this, buffer, offset, count, callback);
      if (asyncWaiter != null)
        this.RunReadWriteTaskWhenReady(asyncWaiter, readWriteTask);
      else
        this.RunReadWriteTask(readWriteTask);
      return (IAsyncResult) readWriteTask;
    }

    private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
    {
      if (asyncWaiter.IsCompleted)
        this.RunReadWriteTask(readWriteTask);
      else
        asyncWaiter.ContinueWith((Action<Task, object>) ((t, state) =>
        {
          Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>) state;
          tuple.Item1.RunReadWriteTask(tuple.Item2);
        }), (object) Tuple.Create<Stream, Stream.ReadWriteTask>(this, readWriteTask), new CancellationToken(), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
    {
      this._activeReadWriteTask = readWriteTask;
      readWriteTask.m_taskScheduler = TaskScheduler.Default;
      readWriteTask.ScheduleAndStart(false);
    }

    /// <summary>
    ///   Заканчивает операцию асинхронной записи.
    ///    (Попробуйте вместо этого использовать метод <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" />; см. раздел "Примечания".)
    /// </summary>
    /// <param name="asyncResult">
    ///   Ссылка на невыполненный асинхронный запрос ввода-вывода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Дескриптор для операции записи недоступен.
    /// 
    ///   -или-
    /// 
    ///   Ожидающие операции не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="asyncResult" /> был получен не из <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> метода для текущего потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт, или произошла внутренняя ошибка.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        Stream.BlockingEndWrite(asyncResult);
      }
      else
      {
        Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
        if (activeReadWriteTask == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        if (activeReadWriteTask != asyncResult)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        if (activeReadWriteTask._isRead)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        try
        {
          activeReadWriteTask.GetAwaiter().GetResult();
        }
        finally
        {
          this._activeReadWriteTask = (Stream.ReadWriteTask) null;
          this._asyncActiveSemaphore.Release();
        }
      }
    }

    /// <summary>
    ///   Асинхронно записывает последовательность байтов в текущий поток и перемещает текущую позицию внутри потока на число записанных байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, из которого записываются данные.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов (начиная с нуля) в объекте <paramref name="buffer" />, с которого начинается копирование байтов в поток.
    /// </param>
    /// <param name="count">Максимальное число байтов для записи.</param>
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
    public Task WriteAsync(byte[] buffer, int offset, int count)
    {
      return this.WriteAsync(buffer, offset, count, CancellationToken.None);
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
    public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        return this.BeginEndWriteAsync(buffer, offset, count);
      return Task.FromCancellation(cancellationToken);
    }

    private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
    {
      Stream.ReadWriteParameters args1 = new Stream.ReadWriteParameters()
      {
        Buffer = buffer,
        Offset = offset,
        Count = count
      };
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> func = (Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult>) ((stream, args, callback, state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state));
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> beginMethod;
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, args1, beginMethod, (Func<Stream, IAsyncResult, VoidTaskResult>) ((stream, asyncResult) =>
      {
        stream.EndWrite(asyncResult);
        return new VoidTaskResult();
      }));
    }

    /// <summary>
    ///   При переопределении в производном классе задает позицию в текущем потоке.
    /// </summary>
    /// <param name="offset">
    ///   Смещение в байтах относительно параметра <paramref name="origin" />.
    /// </param>
    /// <param name="origin">
    ///   Значение типа <see cref="T:System.IO.SeekOrigin" />, указывающее точку ссылки, которая используется для получения новой позиции.
    /// </param>
    /// <returns>Новая позиция в текущем потоке.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает поиск, например, если поток создан из канала или вывода консоли.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract long Seek(long offset, SeekOrigin origin);

    /// <summary>
    ///   При переопределении в производном классе задает длину текущего потока.
    /// </summary>
    /// <param name="value">Нужная длина текущего потока в байтах.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись и поиск, например, если поток создан из канала или вывода консоли.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract void SetLength(long value);

    /// <summary>
    ///   При переопределении в производном классе считывает последовательность байтов из текущего потока и перемещает позицию в потоке на число считанных байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов.
    ///    После завершения выполнения данного метода буфер содержит указанный массив байтов, в котором значения в интервале между <paramref name="offset" /> и (<paramref name="offset" /> + <paramref name="count" /> - 1) заменены байтами, считанными из текущего источника.
    /// </param>
    /// <param name="offset">
    ///   Смещение байтов (начиная с нуля) в <paramref name="buffer" />, с которого начинается сохранение данных, считанных из текущего потока.
    /// </param>
    /// <param name="count">
    ///   Максимальное количество байтов, которое должно быть считано из текущего потока.
    /// </param>
    /// <returns>
    ///   Общее количество байтов, считанных в буфер.
    ///    Это число может быть меньше количества запрошенных байтов, если столько байтов в настоящее время недоступно, а также равняться нулю (0), если был достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="offset" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int Read([In, Out] byte[] buffer, int offset, int count);

    /// <summary>
    ///   Считывает байт из потока и перемещает позицию в потоке на один байт или возвращает -1, если достигнут конец потока.
    /// </summary>
    /// <returns>
    ///   Байт без знака, приведенный к <see langword="Int32" />, или значение -1, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int ReadByte()
    {
      byte[] buffer = new byte[1];
      if (this.Read(buffer, 0, 1) == 0)
        return -1;
      return (int) buffer[0];
    }

    /// <summary>
    ///   При переопределении в производном классе записывает последовательность байтов в текущий поток и перемещает текущую позицию в нем вперед на число записанных байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов.
    ///    Этот метод копирует байты <paramref name="count" /> из <paramref name="buffer" /> в текущий поток.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемое от нуля смещение байтов в буфере <paramref name="buffer" />, с которого начинается копирование байтов в текущий поток.
    /// </param>
    /// <param name="count">
    ///   Количество байтов, которое необходимо записать в текущий поток.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="offset" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="buffer" />  — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода, такие как не удается найти указанный файл.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="M:System.IO.Stream.Write(System.Byte[],System.Int32,System.Int32)" /> был вызван после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract void Write(byte[] buffer, int offset, int count);

    /// <summary>
    ///   Записывает байт в текущее положение в потоке и перемещает позицию в потоке вперед на один байт.
    /// </summary>
    /// <param name="value">Байт, записываемый в поток.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток не поддерживает запись или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Методы были вызваны после закрытия потока.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteByte(byte value)
    {
      this.Write(new byte[1]{ value }, 0, 1);
    }

    /// <summary>
    ///   Создает потокобезопасную (синхронизированную) оболочку для заданного объекта <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="stream">
    ///   Синхронизируемый объект <see cref="T:System.IO.Stream" />.
    /// </param>
    /// <returns>
    ///   Потокобезопасный объект <see cref="T:System.IO.Stream" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Stream Synchronized(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (stream is Stream.SyncStream)
        return stream;
      return (Stream) new Stream.SyncStream(stream);
    }

    /// <summary>
    ///   Обеспечивает поддержку для <see cref="T:System.Diagnostics.Contracts.Contract" />.
    /// </summary>
    [Obsolete("Do not call or override this method.")]
    protected virtual void ObjectInvariant()
    {
    }

    internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Stream.SynchronousAsyncResult synchronousAsyncResult;
      try
      {
        synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
      }
      catch (IOException ex)
      {
        synchronousAsyncResult = new Stream.SynchronousAsyncResult((Exception) ex, state, false);
      }
      if (callback != null)
        callback((IAsyncResult) synchronousAsyncResult);
      return (IAsyncResult) synchronousAsyncResult;
    }

    internal static int BlockingEndRead(IAsyncResult asyncResult)
    {
      return Stream.SynchronousAsyncResult.EndRead(asyncResult);
    }

    internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Stream.SynchronousAsyncResult synchronousAsyncResult;
      try
      {
        this.Write(buffer, offset, count);
        synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
      }
      catch (IOException ex)
      {
        synchronousAsyncResult = new Stream.SynchronousAsyncResult((Exception) ex, state, true);
      }
      if (callback != null)
        callback((IAsyncResult) synchronousAsyncResult);
      return (IAsyncResult) synchronousAsyncResult;
    }

    internal static void BlockingEndWrite(IAsyncResult asyncResult)
    {
      Stream.SynchronousAsyncResult.EndWrite(asyncResult);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.Stream" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Stream()
    {
    }

    private struct ReadWriteParameters
    {
      internal byte[] Buffer;
      internal int Offset;
      internal int Count;
    }

    private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
    {
      internal readonly bool _isRead;
      internal Stream _stream;
      internal byte[] _buffer;
      internal int _offset;
      internal int _count;
      private AsyncCallback _callback;
      private ExecutionContext _context;
      [SecurityCritical]
      private static ContextCallback s_invokeAsyncCallback;

      internal void ClearBeginState()
      {
        this._stream = (Stream) null;
        this._buffer = (byte[]) null;
      }

      [SecuritySafeCritical]
      [MethodImpl(MethodImplOptions.NoInlining)]
      public ReadWriteTask(bool isRead, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
        : base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        this._isRead = isRead;
        this._stream = stream;
        this._buffer = buffer;
        this._offset = offset;
        this._count = count;
        if (callback == null)
          return;
        this._callback = callback;
        this._context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
        this.AddCompletionAction((ITaskCompletionAction) this);
      }

      [SecurityCritical]
      private static void InvokeAsyncCallback(object completedTask)
      {
        Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask) completedTask;
        AsyncCallback callback = readWriteTask._callback;
        readWriteTask._callback = (AsyncCallback) null;
        callback((IAsyncResult) readWriteTask);
      }

      [SecuritySafeCritical]
      void ITaskCompletionAction.Invoke(Task completingTask)
      {
        ExecutionContext context = this._context;
        if (context == null)
        {
          AsyncCallback callback = this._callback;
          this._callback = (AsyncCallback) null;
          callback((IAsyncResult) completingTask);
        }
        else
        {
          this._context = (ExecutionContext) null;
          ContextCallback callback = Stream.ReadWriteTask.s_invokeAsyncCallback;
          if (callback == null)
            Stream.ReadWriteTask.s_invokeAsyncCallback = callback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback);
          using (context)
            ExecutionContext.Run(context, callback, (object) this, true);
        }
      }
    }

    [Serializable]
    private sealed class NullStream : Stream
    {
      private static Task<int> s_nullReadTask;

      internal NullStream()
      {
      }

      public override bool CanRead
      {
        get
        {
          return true;
        }
      }

      public override bool CanWrite
      {
        get
        {
          return true;
        }
      }

      public override bool CanSeek
      {
        get
        {
          return true;
        }
      }

      public override long Length
      {
        get
        {
          return 0;
        }
      }

      public override long Position
      {
        get
        {
          return 0;
        }
        set
        {
        }
      }

      protected override void Dispose(bool disposing)
      {
      }

      public override void Flush()
      {
      }

      [ComVisible(false)]
      public override Task FlushAsync(CancellationToken cancellationToken)
      {
        if (!cancellationToken.IsCancellationRequested)
          return Task.CompletedTask;
        return Task.FromCancellation(cancellationToken);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this.CanRead)
          __Error.ReadNotSupported();
        return this.BlockingBeginRead(buffer, offset, count, callback, state);
      }

      public override int EndRead(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException(nameof (asyncResult));
        return Stream.BlockingEndRead(asyncResult);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        return this.BlockingBeginWrite(buffer, offset, count, callback, state);
      }

      public override void EndWrite(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException(nameof (asyncResult));
        Stream.BlockingEndWrite(asyncResult);
      }

      public override int Read([In, Out] byte[] buffer, int offset, int count)
      {
        return 0;
      }

      [ComVisible(false)]
      public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      {
        Task<int> task = Stream.NullStream.s_nullReadTask;
        if (task == null)
          Stream.NullStream.s_nullReadTask = task = new Task<int>(false, 0, (TaskCreationOptions) 16384, CancellationToken.None);
        return task;
      }

      public override int ReadByte()
      {
        return -1;
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
      }

      [ComVisible(false)]
      public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      {
        if (!cancellationToken.IsCancellationRequested)
          return Task.CompletedTask;
        return Task.FromCancellation(cancellationToken);
      }

      public override void WriteByte(byte value)
      {
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        return 0;
      }

      public override void SetLength(long length)
      {
      }
    }

    internal sealed class SynchronousAsyncResult : IAsyncResult
    {
      private readonly object _stateObject;
      private readonly bool _isWrite;
      private ManualResetEvent _waitHandle;
      private ExceptionDispatchInfo _exceptionInfo;
      private bool _endXxxCalled;
      private int _bytesRead;

      internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
      {
        this._bytesRead = bytesRead;
        this._stateObject = asyncStateObject;
      }

      internal SynchronousAsyncResult(object asyncStateObject)
      {
        this._stateObject = asyncStateObject;
        this._isWrite = true;
      }

      internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
      {
        this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
        this._stateObject = asyncStateObject;
        this._isWrite = isWrite;
      }

      public bool IsCompleted
      {
        get
        {
          return true;
        }
      }

      public WaitHandle AsyncWaitHandle
      {
        get
        {
          return (WaitHandle) LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, (Func<ManualResetEvent>) (() => new ManualResetEvent(true)));
        }
      }

      public object AsyncState
      {
        get
        {
          return this._stateObject;
        }
      }

      public bool CompletedSynchronously
      {
        get
        {
          return true;
        }
      }

      internal void ThrowIfError()
      {
        if (this._exceptionInfo == null)
          return;
        this._exceptionInfo.Throw();
      }

      internal static int EndRead(IAsyncResult asyncResult)
      {
        Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
        if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
          __Error.WrongAsyncResult();
        if (synchronousAsyncResult._endXxxCalled)
          __Error.EndReadCalledTwice();
        synchronousAsyncResult._endXxxCalled = true;
        synchronousAsyncResult.ThrowIfError();
        return synchronousAsyncResult._bytesRead;
      }

      internal static void EndWrite(IAsyncResult asyncResult)
      {
        Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
        if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
          __Error.WrongAsyncResult();
        if (synchronousAsyncResult._endXxxCalled)
          __Error.EndWriteCalledTwice();
        synchronousAsyncResult._endXxxCalled = true;
        synchronousAsyncResult.ThrowIfError();
      }
    }

    [Serializable]
    internal sealed class SyncStream : Stream, IDisposable
    {
      private Stream _stream;
      [NonSerialized]
      private bool? _overridesBeginRead;
      [NonSerialized]
      private bool? _overridesBeginWrite;

      internal SyncStream(Stream stream)
      {
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        this._stream = stream;
      }

      public override bool CanRead
      {
        get
        {
          return this._stream.CanRead;
        }
      }

      public override bool CanWrite
      {
        get
        {
          return this._stream.CanWrite;
        }
      }

      public override bool CanSeek
      {
        get
        {
          return this._stream.CanSeek;
        }
      }

      [ComVisible(false)]
      public override bool CanTimeout
      {
        get
        {
          return this._stream.CanTimeout;
        }
      }

      public override long Length
      {
        get
        {
          lock (this._stream)
            return this._stream.Length;
        }
      }

      public override long Position
      {
        get
        {
          lock (this._stream)
            return this._stream.Position;
        }
        set
        {
          lock (this._stream)
            this._stream.Position = value;
        }
      }

      [ComVisible(false)]
      public override int ReadTimeout
      {
        get
        {
          return this._stream.ReadTimeout;
        }
        set
        {
          this._stream.ReadTimeout = value;
        }
      }

      [ComVisible(false)]
      public override int WriteTimeout
      {
        get
        {
          return this._stream.WriteTimeout;
        }
        set
        {
          this._stream.WriteTimeout = value;
        }
      }

      public override void Close()
      {
        lock (this._stream)
        {
          try
          {
            this._stream.Close();
          }
          finally
          {
            base.Dispose(true);
          }
        }
      }

      protected override void Dispose(bool disposing)
      {
        lock (this._stream)
        {
          try
          {
            if (!disposing)
              return;
            this._stream.Dispose();
          }
          finally
          {
            base.Dispose(disposing);
          }
        }
      }

      public override void Flush()
      {
        lock (this._stream)
          this._stream.Flush();
      }

      public override int Read([In, Out] byte[] bytes, int offset, int count)
      {
        lock (this._stream)
          return this._stream.Read(bytes, offset, count);
      }

      public override int ReadByte()
      {
        lock (this._stream)
          return this._stream.ReadByte();
      }

      private static bool OverridesBeginMethod(Stream stream, string methodName)
      {
        foreach (MethodInfo method in stream.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
        {
          if (method.DeclaringType == typeof (Stream) && method.Name == methodName)
            return false;
        }
        return true;
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this._overridesBeginRead.HasValue)
          this._overridesBeginRead = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, nameof (BeginRead)));
        lock (this._stream)
          return this._overridesBeginRead.Value ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true);
      }

      public override int EndRead(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException(nameof (asyncResult));
        lock (this._stream)
          return this._stream.EndRead(asyncResult);
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        lock (this._stream)
          return this._stream.Seek(offset, origin);
      }

      public override void SetLength(long length)
      {
        lock (this._stream)
          this._stream.SetLength(length);
      }

      public override void Write(byte[] bytes, int offset, int count)
      {
        lock (this._stream)
          this._stream.Write(bytes, offset, count);
      }

      public override void WriteByte(byte b)
      {
        lock (this._stream)
          this._stream.WriteByte(b);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this._overridesBeginWrite.HasValue)
          this._overridesBeginWrite = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, nameof (BeginWrite)));
        lock (this._stream)
          return this._overridesBeginWrite.Value ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true);
      }

      public override void EndWrite(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException(nameof (asyncResult));
        lock (this._stream)
          this._stream.EndWrite(asyncResult);
      }
    }
  }
}
