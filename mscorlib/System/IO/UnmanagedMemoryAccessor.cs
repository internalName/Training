// Decompiled with JetBrains decompiler
// Type: System.IO.UnmanagedMemoryAccessor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>
  ///   Предоставляет произвольный доступ к неуправляемым блокам памяти из управляемого кода.
  /// </summary>
  public class UnmanagedMemoryAccessor : IDisposable
  {
    [SecurityCritical]
    private SafeBuffer _buffer;
    private long _offset;
    private long _capacity;
    private FileAccess _access;
    private bool _isOpen;
    private bool _canRead;
    private bool _canWrite;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryAccessor" />.
    /// </summary>
    protected UnmanagedMemoryAccessor()
    {
      this._isOpen = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryAccessor" /> указанными буфером, смещением и емкостью.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать метод доступа.
    /// </param>
    /// <param name="offset">
    ///   Байт, с которого должен начинаться метод доступа.
    /// </param>
    /// <param name="capacity">
    ///   Размер выделяемой памяти (в байтах).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> больше, чем <paramref name="buffer" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="offset" /> или <paramref name="capacity" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> будет обтекать верхний предел адресного пространства.
    /// </exception>
    [SecuritySafeCritical]
    public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
    {
      this.Initialize(buffer, offset, capacity, FileAccess.Read);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.UnmanagedMemoryAccessor" /> указанными буфером, смещением, емкостью и правами доступа.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать метод доступа.
    /// </param>
    /// <param name="offset">
    ///   Байт, с которого должен начинаться метод доступа.
    /// </param>
    /// <param name="capacity">
    ///   Размер выделяемой памяти (в байтах).
    /// </param>
    /// <param name="access">
    ///   Тип разрешенного доступа к памяти.
    ///    Значение по умолчанию — <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> больше, чем <paramref name="buffer" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> не является допустимым значением перечисления <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> будет обтекать верхний предел адресного пространства.
    /// </exception>
    [SecuritySafeCritical]
    public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
      this.Initialize(buffer, offset, capacity, access);
    }

    /// <summary>Задает начальные значения для метода доступа.</summary>
    /// <param name="buffer">
    ///   Буфер, который должен содержать метод доступа.
    /// </param>
    /// <param name="offset">
    ///   Байт, с которого должен начинаться метод доступа.
    /// </param>
    /// <param name="capacity">
    ///   Размер выделяемой памяти (в байтах).
    /// </param>
    /// <param name="access">
    ///   Тип разрешенного доступа к памяти.
    ///    Значение по умолчанию — <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> больше, чем <paramref name="buffer" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> не является допустимым значением перечисления <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="offset" /> плюс <paramref name="capacity" /> будет обтекать верхний предел адресного пространства.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0L)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (capacity < 0L)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.ByteLength < (ulong) (offset + capacity))
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndCapacityOutOfBounds"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException(nameof (access));
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        buffer.AcquirePointer(ref pointer);
        if ((UIntPtr) (ulong) ((long) pointer + offset + capacity) < (UIntPtr) pointer)
          throw new ArgumentException(Environment.GetResourceString("Argument_UnmanagedMemAccessorWrapAround"));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          buffer.ReleasePointer();
      }
      this._offset = offset;
      this._buffer = buffer;
      this._capacity = capacity;
      this._access = access;
      this._isOpen = true;
      this._canRead = (uint) (this._access & FileAccess.Read) > 0U;
      this._canWrite = (uint) (this._access & FileAccess.Write) > 0U;
    }

    /// <summary>Возвращает емкость метода доступа.</summary>
    /// <returns>Емкость метода доступа.</returns>
    public long Capacity
    {
      get
      {
        return this._capacity;
      }
    }

    /// <summary>Определяет, доступен ли метод доступа для чтения.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если метод доступа доступен для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool CanRead
    {
      get
      {
        if (this._isOpen)
          return this._canRead;
        return false;
      }
    }

    /// <summary>Определяет, доступен ли метод доступа для записи.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если метод доступа доступен для записи; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool CanWrite
    {
      get
      {
        if (this._isOpen)
          return this._canWrite;
        return false;
      }
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.UnmanagedMemoryAccessor" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      this._isOpen = false;
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.IO.UnmanagedMemoryAccessor" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Определяет, открыт ли метод доступа процессом в текущий момент.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если метод доступа открыт; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected bool IsOpen
    {
      get
      {
        return this._isOpen;
      }
    }

    /// <summary>Считывает из метода доступа логическое значение.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> или <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    public bool ReadBoolean(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      return this.InternalReadByte(position) > (byte) 0;
    }

    /// <summary>Считывает из метода доступа значение байта.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    public byte ReadByte(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      return this.InternalReadByte(position);
    }

    /// <summary>Считывает из метода доступа символ.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe char ReadChar(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (char) *(ushort*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 16-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe short ReadInt16(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(short*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 32-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe int ReadInt32(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(int*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 64-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe long ReadInt64(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(long*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>Считывает из метода доступа десятичное число.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// 
    ///   -или-
    /// 
    ///   Считываемое десятичное число недопустимо.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public Decimal ReadDecimal(long position)
    {
      int sizeOfType = 16;
      this.EnsureSafeToRead(position, sizeOfType);
      int[] numArray = new int[4];
      this.ReadArray<int>(position, numArray, 0, numArray.Length);
      return new Decimal(numArray);
    }

    /// <summary>
    ///   Считывает из метода доступа значение с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe float ReadSingle(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(float*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа значение с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe double ReadDouble(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(double*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 8-разрядное целое число со знаком.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe sbyte ReadSByte(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (sbyte) *pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe ushort ReadUInt16(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(ushort*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe uint ReadUInt32(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(uint*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <returns>Прочитанное значение.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe ulong ReadUInt64(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (ulong) *(long*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Считывает из метода доступа структуру типа <paramref name="T" /> и передает ее по указанной ссылке.
    /// </summary>
    /// <param name="position">
    ///   Позиция в методе доступа, с которой начинается чтение.
    /// </param>
    /// <param name="structure">
    ///   Структура, которая будет содержать считываемые данные.
    /// </param>
    /// <typeparam name="T">Тип структуры.</typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для чтения в структуре типа <paramref name="T" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="T" /> является типом значения, который содержит один или несколько ссылочных типов.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecurityCritical]
    public void Read<T>(long position, out T structure) where T : struct
    {
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (!this._isOpen)
        throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      uint num = Marshal.SizeOfType(typeof (T));
      if (position > this._capacity - (long) num)
      {
        if (position >= this._capacity)
          throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
        throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead", (object) typeof (T).FullName), nameof (position));
      }
      structure = this._buffer.Read<T>((ulong) (this._offset + position));
    }

    /// <summary>
    ///   Считывает из метода доступа структуры типа <paramref name="T" /> и передает их в массив типа <paramref name="T" />.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должно начаться чтение.
    /// </param>
    /// <param name="array">
    ///   Массив, который будет содержать считываемые из метода доступа структуры.
    /// </param>
    /// <param name="offset">
    ///   Индекс в массиве <paramref name="array" />, по которому будет помещена первая скопированная структура.
    /// </param>
    /// <param name="count">
    ///   Число структур типа <paramref name="T" />, которое необходимо считать из метода доступа.
    /// </param>
    /// <typeparam name="T">Тип структуры.</typeparam>
    /// <returns>
    ///   Число структур, считанных в массив <paramref name="array" />.
    ///    Это число может быть меньше значения <paramref name="count" />, если доступно меньшее число структур, или равняться нулю, если достигнут конец метода доступа.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="array" /> недостаточно велик, чтобы содержать <paramref name="count" /> структур (начиная с <paramref name="position" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает чтение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecurityCritical]
    public int ReadArray<T>(long position, T[] array, int offset, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), "Buffer cannot be null.");
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
      if (!this.CanRead)
      {
        if (!this._isOpen)
          throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      }
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      uint num1 = Marshal.AlignedSizeOf<T>();
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      int count1 = count;
      long num2 = this._capacity - position;
      if (num2 < 0L)
      {
        count1 = 0;
      }
      else
      {
        ulong num3 = (ulong) num1 * (ulong) count;
        if ((ulong) num2 < num3)
          count1 = (int) (num2 / (long) num1);
      }
      this._buffer.ReadArray<T>((ulong) (this._offset + position), array, offset, count1);
      return count1;
    }

    /// <summary>Записывает в метод доступа логическое значение.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    public void Write(long position, bool value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte num = value ? (byte) 1 : (byte) 0;
      this.InternalWrite(position, num);
    }

    /// <summary>Записывает в метод доступа значение байта.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    public void Write(long position, byte value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      this.InternalWrite(position, value);
    }

    /// <summary>Записывает в метод доступа символ.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, char value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = (short) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 16-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, short value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 32-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, int value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(int*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 64-разрядное целое число.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после позиции для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, long value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(long*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>Записывает в метод доступа десятичное число.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// 
    ///   -или-
    /// 
    ///   Десятичное число недопустимо.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public void Write(long position, Decimal value)
    {
      int sizeOfType = 16;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte[] buffer = new byte[16];
      Decimal.GetBytes(value, buffer);
      int[] array = new int[4];
      int num1 = (int) buffer[12] | (int) buffer[13] << 8 | (int) buffer[14] << 16 | (int) buffer[15] << 24;
      int num2 = (int) buffer[0] | (int) buffer[1] << 8 | (int) buffer[2] << 16 | (int) buffer[3] << 24;
      int num3 = (int) buffer[4] | (int) buffer[5] << 8 | (int) buffer[6] << 16 | (int) buffer[7] << 24;
      int num4 = (int) buffer[8] | (int) buffer[9] << 8 | (int) buffer[10] << 16 | (int) buffer[11] << 24;
      array[0] = num2;
      array[1] = num3;
      array[2] = num4;
      array[3] = num1;
      this.WriteArray<int>(position, array, 0, array.Length);
    }

    /// <summary>
    ///   Записывает в метод доступа значение типа <see langword="Single" />.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, float value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(float*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа значение типа <see langword="Double" />.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, double value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(double*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>Записывает в метод доступа 8-разрядное целое число.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, sbyte value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *pointer = (byte) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 16-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, ushort value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = (short) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, uint value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(int*) pointer = (int) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Записывает в метод доступа 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов после <paramref name="position" /> для записи значения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, ulong value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(long*) pointer = (long) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>Записывает в метод доступа структуру.</summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="structure">
    ///   Структура, которую требуется записать.
    /// </param>
    /// <typeparam name="T">Тип структуры.</typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов в методе доступа после <paramref name="position" /> для записи структуры типа <paramref name="T" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecurityCritical]
    public void Write<T>(long position, ref T structure) where T : struct
    {
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (!this._isOpen)
        throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      uint num = Marshal.SizeOfType(typeof (T));
      if (position > this._capacity - (long) num)
      {
        if (position >= this._capacity)
          throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
        throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", (object) typeof (T).FullName), nameof (position));
      }
      this._buffer.Write<T>((ulong) (this._offset + position), structure);
    }

    /// <summary>
    ///   Записывает в метод доступа структуры из массива типа <paramref name="T" />.
    /// </summary>
    /// <param name="position">
    ///   Число байтов в методе доступа, с которого должна начаться запись.
    /// </param>
    /// <param name="array">
    ///   Массив, из которого ведется запись в метод доступа.
    /// </param>
    /// <param name="offset">
    ///   Индекс в массиве <paramref name="array" />, с которого требуется начать запись.
    /// </param>
    /// <param name="count">
    ///   Число структур в массиве <paramref name="array" />, которые требуется записать.
    /// </param>
    /// <typeparam name="T">Тип структуры.</typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   Не хватает байтов в методе доступа после <paramref name="position" /> для записи числа структур, указанных <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="position" /> меньше нуля или больше, чем емкость метода доступа.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="offset" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод доступа не поддерживает запись.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод доступа был удален.
    /// </exception>
    [SecurityCritical]
    public void WriteArray<T>(long position, T[] array, int offset, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), "Buffer cannot be null.");
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position >= this.Capacity)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      if (!this._isOpen)
        throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      this._buffer.WriteArray<T>((ulong) (this._offset + position), array, offset, count);
    }

    [SecuritySafeCritical]
    private unsafe byte InternalReadByte(long position)
    {
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        return (pointer + this._offset)[position];
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    [SecuritySafeCritical]
    private unsafe void InternalWrite(long position, byte value)
    {
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        (pointer + this._offset)[position] = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    private void EnsureSafeToRead(long position, int sizeOfType)
    {
      if (!this._isOpen)
        throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position <= this._capacity - (long) sizeOfType)
        return;
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead"), nameof (position));
    }

    private void EnsureSafeToWrite(long position, int sizeOfType)
    {
      if (!this._isOpen)
        throw new ObjectDisposedException(nameof (UnmanagedMemoryAccessor), Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position <= this._capacity - (long) sizeOfType)
        return;
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException(nameof (position), Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", (object) "Byte"), nameof (position));
    }
  }
}
