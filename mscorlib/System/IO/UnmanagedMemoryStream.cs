// Decompiled with JetBrains decompiler
// Type: System.IO.UnmanagedMemoryStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет доступ к неуправляемым блокам памяти из управляемого кода.
  /// </summary>
  public class UnmanagedMemoryStream : Stream
  {
    private const long UnmanagedMemStreamMaxLength = 9223372036854775807;
    [SecurityCritical]
    private SafeBuffer _buffer;
    [SecurityCritical]
    private unsafe byte* _mem;
    private long _length;
    private long _capacity;
    private long _position;
    private long _offset;
    private FileAccess _access;
    internal bool _isOpen;
    [NonSerialized]
    private Task<int> _lastReadTask;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" />.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    [SecuritySafeCritical]
    protected unsafe UnmanagedMemoryStream()
    {
      this._mem = (byte*) null;
      this._isOpen = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" /> в безопасном буфере с указанным смещением и длиной.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать поток неуправляемой памяти.
    /// </param>
    /// <param name="offset">
    ///   Позиция байта в буфере, с которой должен начинаться поток неуправляемой памяти.
    /// </param>
    /// <param name="length">Длина потока неуправляемой памяти.</param>
    [SecuritySafeCritical]
    public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
    {
      this.Initialize(buffer, offset, length, FileAccess.Read, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" /> в безопасном буфере с указанными смещением, длиной и правами доступа к файлам.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать поток неуправляемой памяти.
    /// </param>
    /// <param name="offset">
    ///   Позиция байта в буфере, с которой должен начинаться поток неуправляемой памяти.
    /// </param>
    /// <param name="length">Длина потока неуправляемой памяти.</param>
    /// <param name="access">
    ///   Режим доступа к файлам для потока неуправляемой памяти.
    /// </param>
    [SecuritySafeCritical]
    public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
    {
      this.Initialize(buffer, offset, length, access, false);
    }

    [SecurityCritical]
    internal UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
    {
      this.Initialize(buffer, offset, length, access, skipSecurityCheck);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" /> в безопасном буфере с указанными смещением, длиной и правами доступа к файлам.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать поток неуправляемой памяти.
    /// </param>
    /// <param name="offset">
    ///   Позиция байта в буфере, с которой должен начинаться поток неуправляемой памяти.
    /// </param>
    /// <param name="length">Длина потока неуправляемой памяти.</param>
    /// <param name="access">
    ///   Режим доступа к файлам для потока неуправляемой памяти.
    /// </param>
    [SecuritySafeCritical]
    protected void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
    {
      this.Initialize(buffer, offset, length, access, false);
    }

    [SecurityCritical]
    internal unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0L)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length < 0L)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.ByteLength < (ulong) (offset + length))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSafeBufferOffLen"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException(nameof (access));
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      if (!skipSecurityCheck)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        buffer.AcquirePointer(ref pointer);
        if (pointer + offset + length < pointer)
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          buffer.ReleasePointer();
      }
      this._offset = offset;
      this._buffer = buffer;
      this._length = length;
      this._capacity = length;
      this._access = access;
      this._isOpen = true;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" />, используя заданное расположение и объем памяти.
    /// </summary>
    /// <param name="pointer">
    ///   Указатель на расположение неуправляемой памяти.
    /// </param>
    /// <param name="length">Используемый объем памяти.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <paramref name="pointer" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="length" /> Значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length" /> Слишком малы, чтобы привести к переполнению.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe UnmanagedMemoryStream(byte* pointer, long length)
    {
      this.Initialize(pointer, length, length, FileAccess.Read, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" />, используя указанные значения расположения, объема памяти, общего объема памяти и доступа к файлам.
    /// </summary>
    /// <param name="pointer">
    ///   Указатель на расположение неуправляемой памяти.
    /// </param>
    /// <param name="length">Используемый объем памяти.</param>
    /// <param name="capacity">
    ///   Общий объем памяти, назначенный для потока.
    /// </param>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <paramref name="pointer" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="length" /> Значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="capacity" /> Значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length" /> Значение больше, чем <paramref name="capacity" /> значение.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
    {
      this.Initialize(pointer, length, capacity, access, false);
    }

    [SecurityCritical]
    internal unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
    {
      this.Initialize(pointer, length, capacity, access, skipSecurityCheck);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryStream" />, используя указатель на неуправляемое расположение в памяти.
    /// </summary>
    /// <param name="pointer">
    ///   Указатель на расположение неуправляемой памяти.
    /// </param>
    /// <param name="length">Используемый объем памяти.</param>
    /// <param name="capacity">
    ///   Общий объем памяти, назначенный для потока.
    /// </param>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.IO.FileAccess" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <paramref name="pointer" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="length" /> Значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="capacity" /> Значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length" /> Значение слишком малы, чтобы привести к переполнению.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
    {
      this.Initialize(pointer, length, capacity, access, false);
    }

    [SecurityCritical]
    internal unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
    {
      if ((IntPtr) pointer == IntPtr.Zero)
        throw new ArgumentNullException(nameof (pointer));
      if (length < 0L || capacity < 0L)
        throw new ArgumentOutOfRangeException(length < 0L ? nameof (length) : nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length > capacity)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_LengthGreaterThanCapacity"));
      if ((UIntPtr) ((ulong) pointer + (ulong) capacity) < (UIntPtr) pointer)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException(nameof (access), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      if (!skipSecurityCheck)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      this._mem = pointer;
      this._offset = 0L;
      this._length = length;
      this._capacity = capacity;
      this._access = access;
      this._isOpen = true;
    }

    /// <summary>
    ///   Возвращает значение, определяющее, поддерживает ли поток операции чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" />, если объект был создан конструктором с использованием параметра <paramref name="access" />, не включающего чтение потока, и если поток закрыт; в противном случае — значение <see langword="true" />.
    /// </returns>
    public override bool CanRead
    {
      get
      {
        if (this._isOpen)
          return (uint) (this._access & FileAccess.Read) > 0U;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее, поддерживает ли поток операции поиска.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" />, если поток закрыт, в противном случае — значение <see langword="true" />.
    /// </returns>
    public override bool CanSeek
    {
      get
      {
        return this._isOpen;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее, поддерживает ли поток операции записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" />, если объект был создан конструктором со значением параметра <paramref name="access" />, которое поддерживает запись, или если объект был создан конструктором без параметров, или если поток закрыт; в противном случае — значение <see langword="true" />.
    /// </returns>
    public override bool CanWrite
    {
      get
      {
        if (this._isOpen)
          return (uint) (this._access & FileAccess.Write) > 0U;
        return false;
      }
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.UnmanagedMemoryStream" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    protected override unsafe void Dispose(bool disposing)
    {
      this._isOpen = false;
      this._mem = (byte*) null;
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Переопределяет метод <see cref="M:System.IO.Stream.Flush" /> так, что никакие действия не выполняются.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    public override void Flush()
    {
      if (this._isOpen)
        return;
      __Error.StreamIsClosed();
    }

    /// <summary>
    ///   Переопределяет метод <see cref="M:System.IO.Stream.FlushAsync(System.Threading.CancellationToken)" /> так, что операция отменяется, если это указано, но никакие другие действия не выполняются.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен для отслеживания запросов отмены.
    ///    Значение по умолчанию — <see cref="P:System.Threading.CancellationToken.None" />.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    [ComVisible(false)]
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

    /// <summary>Возвращает длину данных в потоке.</summary>
    /// <returns>Длина данных в потоке.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    public override long Length
    {
      get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return Interlocked.Read(ref this._length);
      }
    }

    /// <summary>
    ///   Возвращает длину потока (размер) или общий объем памяти, назначенный потоку (емкость).
    /// </summary>
    /// <returns>Размер или емкость потока.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    public long Capacity
    {
      get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return this._capacity;
      }
    }

    /// <summary>Возвращает или задает текущую позицию в потоке.</summary>
    /// <returns>Текущая позиция в потоке.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Положение задано значение меньше нуля или превышает позицию <see cref="F:System.Int32.MaxValue" /> или привело к переполнению при добавлении текущего указателя.
    /// </exception>
    public override unsafe long Position
    {
      get
      {
        if (!this.CanSeek)
          __Error.StreamIsClosed();
        return Interlocked.Read(ref this._position);
      }
      [SecuritySafeCritical] set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (!this.CanSeek)
          __Error.StreamIsClosed();
        if (value > (long) int.MaxValue || this._mem + value < this._mem)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
        Interlocked.Exchange(ref this._position, value);
      }
    }

    /// <summary>
    ///   Возвращает или задает указатель байтов для потока, используя текущее положение в потоке.
    /// </summary>
    /// <returns>Указатель байтов.</returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   Текущая позиция превышает емкость потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Указываемая позиция набор не является допустимым позицию в текущем потоке.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Указатель устанавливается значение меньше, чем значение начальной позиции потока.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поток был инициализирован для использования с <see cref="T:System.Runtime.InteropServices.SafeBuffer" />.
    ///   <see cref="P:System.IO.UnmanagedMemoryStream.PositionPointer" /> Свойство допустимо только для потоков, которые инициализируются с помощью <see cref="T:System.Byte" /> указателя.
    /// </exception>
    [CLSCompliant(false)]
    public unsafe byte* PositionPointer
    {
      [SecurityCritical] get
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        long num = Interlocked.Read(ref this._position);
        if (num > this._capacity)
          throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_UMSPosition"));
        byte* numPtr = this._mem + num;
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return numPtr;
      }
      [SecurityCritical] set
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (new IntPtr(value - this._mem).ToInt64() > long.MaxValue)
          throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
        if (value < this._mem)
          throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
        Interlocked.Exchange(ref this._position, value - this._mem);
      }
    }

    internal unsafe byte* Pointer
    {
      [SecurityCritical] get
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        return this._mem;
      }
    }

    /// <summary>
    ///   Считывает указанное число байтов в указанный массив.
    /// </summary>
    /// <param name="buffer">
    ///   При возврате этот метод содержит указанный массив байтов, в котором значения в интервале от <paramref name="offset" /> до (<paramref name="offset" /> + <paramref name="count" /> - 1) заменены байтами, считанными из текущего источника.
    ///    Этот параметр передается неинициализированным.
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
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Основная память не поддерживает чтение.
    /// 
    ///   -или-
    /// 
    ///   Свойству <see cref="P:System.IO.UnmanagedMemoryStream.CanRead" /> задано значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="buffer" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> Меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> Меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина массива буфера минус <paramref name="offset" /> параметр меньше, чем <paramref name="count" /> параметр.
    /// </exception>
    [SecuritySafeCritical]
    public override unsafe int Read([In, Out] byte[] buffer, int offset, int count)
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
      if (!this.CanRead)
        __Error.ReadNotSupported();
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length) - num1;
      if (num2 > (long) count)
        num2 = (long) count;
      if (num2 <= 0L)
        return 0;
      int len = (int) num2;
      if (len < 0)
        len = 0;
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          Buffer.Memcpy(buffer, offset, pointer + num1 + this._offset, 0, len);
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        Buffer.Memcpy(buffer, offset, this._mem + num1, 0, len);
      Interlocked.Exchange(ref this._position, num1 + num2);
      return len;
    }

    /// <summary>
    ///   Считывает в асинхронном режиме указанное число байтов в указанный массив.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
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
    [ComVisible(false)]
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
      catch (Exception ex)
      {
        return Task.FromException<int>(ex);
      }
    }

    /// <summary>
    ///   Считывает байт из потока и перемещает позицию в потоке на один байт или возвращает -1, если достигнут конец потока.
    /// </summary>
    /// <returns>
    ///   Байт без знака, приведенный к объекту <see cref="T:System.Int32" />, или значение -1, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Основная память не поддерживает чтение.
    /// 
    ///   -или-
    /// 
    ///   Текущая позиция находится в конце потока.
    /// </exception>
    [SecuritySafeCritical]
    public override unsafe int ReadByte()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanRead)
        __Error.ReadNotSupported();
      long index = Interlocked.Read(ref this._position);
      long num1 = Interlocked.Read(ref this._length);
      if (index >= num1)
        return -1;
      Interlocked.Exchange(ref this._position, index + 1L);
      int num2;
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          num2 = (int) (pointer + index)[this._offset];
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        num2 = (int) this._mem[index];
      return num2;
    }

    /// <summary>
    ///   Устанавливает текущую позицию текущего потока на заданное значение.
    /// </summary>
    /// <param name="offset">
    ///   Указатель относительно начальной точки <paramref name="origin" />, от которой начинается поиск.
    /// </param>
    /// <param name="loc">
    ///   Задает начальную, конечную или текущую позицию как опорную точку для <paramref name="origin" />, используя значение типа <see cref="T:System.IO.SeekOrigin" />.
    /// </param>
    /// <returns>Новая позиция в потоке.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Попытка поиска до начала потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> Значение больше, чем максимальный размер потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="loc" /> недопустим.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    public override long Seek(long offset, SeekOrigin loc)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (offset > long.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
      switch (loc)
      {
        case SeekOrigin.Begin:
          if (offset < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, offset);
          break;
        case SeekOrigin.Current:
          long num1 = Interlocked.Read(ref this._position);
          if (offset + num1 < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, offset + num1);
          break;
        case SeekOrigin.End:
          long num2 = Interlocked.Read(ref this._length);
          if (num2 + offset < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, num2 + offset);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      }
      return Interlocked.Read(ref this._position);
    }

    /// <summary>Присваивает длине потока указанное значение.</summary>
    /// <param name="value">Длина потока.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Основная память не поддерживает запись.
    /// 
    ///   -или-
    /// 
    ///   Попытка записи в поток и <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> свойство <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Указанный <paramref name="value" /> превышает емкость потока.
    /// 
    ///   -или-
    /// 
    ///   Указанный <paramref name="value" /> является отрицательным.
    /// </exception>
    [SecuritySafeCritical]
    public override unsafe void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._buffer != null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (value > this._capacity)
        throw new IOException(Environment.GetResourceString("IO.IO_FixedCapacity"));
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length);
      if (value > num2)
        Buffer.ZeroMemory(this._mem + num2, value - num2);
      Interlocked.Exchange(ref this._length, value);
      if (num1 <= value)
        return;
      Interlocked.Exchange(ref this._position, value);
    }

    /// <summary>
    ///   Записывает в текущий поток блок байтов, используя данные из буфера.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов, из которого необходимо скопировать байты в текущий поток.
    /// </param>
    /// <param name="offset">
    ///   Смещение в буфере, с которого необходимо начать копирование байтов в текущий поток.
    /// </param>
    /// <param name="count">
    ///   Число байтов для записи в текущий поток.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Основная память не поддерживает запись.
    /// 
    ///   -или-
    /// 
    ///   Попытка записи в поток и <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> свойство <see langword="false" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> Значение превышает емкость потока.
    /// 
    ///   -или-
    /// 
    ///   Позиция находится в конце емкости потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Один из указанных параметров меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> Параметр за вычетом длины <paramref name="buffer" /> параметр меньше, чем <paramref name="count" /> параметр.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override unsafe void Write(byte[] buffer, int offset, int count)
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
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length);
      long num3 = num1 + (long) count;
      if (num3 < 0L)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (num3 > this._capacity)
        throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
      if (this._buffer == null)
      {
        if (num1 > num2)
          Buffer.ZeroMemory(this._mem + num2, num1 - num2);
        if (num3 > num2)
          Interlocked.Exchange(ref this._length, num3);
      }
      if (this._buffer != null)
      {
        if (this._capacity - num1 < (long) count)
          throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          Buffer.Memcpy(pointer + num1 + this._offset, 0, buffer, offset, count);
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        Buffer.Memcpy(this._mem + num1, 0, buffer, offset, count);
      Interlocked.Exchange(ref this._position, num3);
    }

    /// <summary>
    ///   Асинхронно записывает последовательность байтов в текущий поток, перемещает текущую позицию внутри потока на число записанных байтов и отслеживает запросы отмены.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
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
    [ComVisible(false)]
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
      catch (Exception ex)
      {
        return (Task) Task.FromException<int>(ex);
      }
    }

    /// <summary>Запись байта в текущую позицию в потоке файла.</summary>
    /// <param name="value">Значение в байтах, записанное в поток.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Основная память не поддерживает запись.
    /// 
    ///   -или-
    /// 
    ///   Попытка записи в поток и <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> свойство <see langword="false" />.
    /// 
    ///   -или-
    /// 
    ///   Текущая позиция находится в конце емкости потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Предоставленный <paramref name="value" /> потока приводит к превышению максимальной емкости.
    /// </exception>
    [SecuritySafeCritical]
    public override unsafe void WriteByte(byte value)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      long index = Interlocked.Read(ref this._position);
      long num1 = Interlocked.Read(ref this._length);
      long num2 = index + 1L;
      if (index >= num1)
      {
        if (num2 < 0L)
          throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
        if (num2 > this._capacity)
          throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
        if (this._buffer == null)
        {
          if (index > num1)
            Buffer.ZeroMemory(this._mem + num1, index - num1);
          Interlocked.Exchange(ref this._length, num2);
        }
      }
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          (pointer + index)[this._offset] = value;
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        this._mem[index] = value;
      Interlocked.Exchange(ref this._position, num2);
    }
  }
}
