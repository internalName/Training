// Decompiled with JetBrains decompiler
// Type: System.Security.SecureString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security
{
  /// <summary>
  ///   Представляет текст, который должен оставаться конфиденциальным, например, путем его удаления из памяти компьютера, если он больше не нужен.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class SecureString : IDisposable
  {
    private static bool supportedOnCurrentPlatform = SecureString.EncryptionSupported();
    [SecurityCritical]
    private SafeBSTRHandle m_buffer;
    private int m_length;
    private bool m_readOnly;
    private bool m_encrypted;
    private const int BlockSize = 8;
    private const int MaxLength = 65536;
    private const uint ProtectionScope = 0;

    [SecuritySafeCritical]
    static SecureString()
    {
    }

    [SecurityCritical]
    private static bool EncryptionSupported()
    {
      bool flag = true;
      try
      {
        Win32Native.SystemFunction041(SafeBSTRHandle.Allocate((string) null, 16U), 16U, 0U);
      }
      catch (EntryPointNotFoundException ex)
      {
        flag = false;
      }
      return flag;
    }

    [SecurityCritical]
    internal SecureString(SecureString str)
    {
      this.AllocateBuffer(str.BufferLength);
      SafeBSTRHandle.Copy(str.m_buffer, this.m_buffer);
      this.m_length = str.m_length;
      this.m_encrypted = str.m_encrypted;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecureString" />.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите или снятия защиты с них значение данного экземпляра.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Эта операция не поддерживается этой платформой.
    /// </exception>
    [SecuritySafeCritical]
    public SecureString()
    {
      this.CheckSupportedOnCurrentPlatform();
      this.AllocateBuffer(8);
      this.m_length = 0;
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    private unsafe void InitializeSecureString(char* value, int length)
    {
      this.CheckSupportedOnCurrentPlatform();
      this.AllocateBuffer(length);
      this.m_length = length;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy(pointer, (byte*) value, length * 2);
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
      this.ProtectMemory();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecureString" /> из подмассива объектов <see cref="T:System.Char" />.
    /// 
    ///   Этот конструктор несовместим с CLS.
    ///    CLS-совместимая альтернатива — <see cref="M:System.Security.SecureString.#ctor" />.
    /// </summary>
    /// <param name="value">
    ///   Указатель на массив объектов <see cref="T:System.Char" />.
    /// </param>
    /// <param name="length">
    ///   Число элементов массива <paramref name="value" />, включаемых в новый экземпляр.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="length" /> меньше нуля или больше 65 536.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Эта операция не поддерживается этой платформой.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe SecureString(char* value, int length)
    {
      if ((IntPtr) value == IntPtr.Zero)
        throw new ArgumentNullException(nameof (value));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length > 65536)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_Length"));
      this.InitializeSecureString(value, length);
    }

    /// <summary>
    ///   Возвращает количество символов в текущей защищенной строке.
    /// </summary>
    /// <returns>
    ///   Количество объектов <see cref="T:System.Char" /> в этой защищенной строке.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже удалена.
    /// </exception>
    public int Length
    {
      [SecuritySafeCritical, MethodImpl(MethodImplOptions.Synchronized)] get
      {
        this.EnsureNotDisposed();
        return this.m_length;
      }
    }

    /// <summary>Добавляет знак в конец текущей защищенной строки.</summary>
    /// <param name="c">Знак, добавляемый к защищенной строке.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Защищенная строка доступна только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Выполнение этой операции бы больше 65 536 символов длина этой защищенной строки.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AppendChar(char c)
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.EnsureCapacity(this.m_length + 1);
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.Write<char>((ulong) (uint) (this.m_length * 2), c);
        ++this.m_length;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
      }
    }

    /// <summary>Удаляет значение текущей защищенной строки.</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Защищенная строка доступна только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Clear()
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.m_length = 0;
      this.m_buffer.ClearBuffer();
      this.m_encrypted = false;
    }

    /// <summary>Создает копию текущей защищенной строки.</summary>
    /// <returns>Копия этой защищенной строки.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public SecureString Copy()
    {
      this.EnsureNotDisposed();
      return new SecureString(this);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим объектом <see cref="T:System.Security.SecureString" />.
    /// </summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Dispose()
    {
      if (this.m_buffer == null || this.m_buffer.IsInvalid)
        return;
      this.m_buffer.Close();
      this.m_buffer = (SafeBSTRHandle) null;
    }

    /// <summary>
    ///   Вставляет знак в заданную индексом позицию защищенной строки.
    /// </summary>
    /// <param name="index">
    ///   Индекс позиции вставки параметра <paramref name="c" />.
    /// </param>
    /// <param name="c">Вставляемый знак.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Защищенная строка доступна только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> меньше нуля или больше, чем длина этой защищенной строки.
    /// 
    ///   -или-
    /// 
    ///   Выполнение этой операции бы больше 65 536 символов длина этой защищенной строки.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public unsafe void InsertAt(int index, char c)
    {
      if (index < 0 || index > this.m_length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.EnsureCapacity(this.m_length + 1);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        char* chPtr = (char*) pointer;
        for (int length = this.m_length; length > index; --length)
          chPtr[length] = chPtr[length - 1];
        chPtr[index] = c;
        ++this.m_length;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Указывает, что защищенная строка доступна только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если защищенная строка доступна только для чтения; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool IsReadOnly()
    {
      this.EnsureNotDisposed();
      return this.m_readOnly;
    }

    /// <summary>
    ///   Делает текстовое значение этой защищенной строки доступным только для чтения.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void MakeReadOnly()
    {
      this.EnsureNotDisposed();
      this.m_readOnly = true;
    }

    /// <summary>
    ///   Удаляет из защищенной строки знак, расположенный по указанному индексу.
    /// </summary>
    /// <param name="index">Индекс знака в защищенной строке.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Защищенная строка доступна только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> меньше нуля или больше или равно длине этой защищенной строки.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public unsafe void RemoveAt(int index)
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      if (index < 0 || index >= this.m_length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        char* chPtr = (char*) pointer;
        for (int index1 = index; index1 < this.m_length - 1; ++index1)
          chPtr[index1] = chPtr[index1 + 1];
        chPtr[--this.m_length] = char.MinValue;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    /// <summary>
    ///   Заменяет расположенный по указанному индексу знак другим знаком.
    /// </summary>
    /// <param name="index">
    ///   Индекс имеющегося знака в защищенной строке.
    /// </param>
    /// <param name="c">Знак, заменяющий имеющийся знак.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Защищенная строка уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Защищенная строка доступна только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> меньше нуля или больше или равно длине этой защищенной строки.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Произошла ошибка при защите значения этой защищенной строки или снятии с него защиты.
    /// </exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void SetAt(int index, char c)
    {
      if (index < 0 || index >= this.m_length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.Write<char>((ulong) (uint) (index * 2), c);
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
      }
    }

    private int BufferLength
    {
      [SecurityCritical] get
      {
        return this.m_buffer.Length;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void AllocateBuffer(int size)
    {
      this.m_buffer = SafeBSTRHandle.Allocate((string) null, SecureString.GetAlignedSize(size));
      if (this.m_buffer.IsInvalid)
        throw new OutOfMemoryException();
    }

    private void CheckSupportedOnCurrentPlatform()
    {
      if (!SecureString.supportedOnCurrentPlatform)
        throw new NotSupportedException(Environment.GetResourceString("Arg_PlatformSecureString"));
    }

    [SecurityCritical]
    private void EnsureCapacity(int capacity)
    {
      if (capacity > 65536)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      if (capacity <= this.m_buffer.Length)
        return;
      SafeBSTRHandle target = SafeBSTRHandle.Allocate((string) null, SecureString.GetAlignedSize(capacity));
      if (target.IsInvalid)
        throw new OutOfMemoryException();
      SafeBSTRHandle.Copy(this.m_buffer, target);
      this.m_buffer.Close();
      this.m_buffer = target;
    }

    [SecurityCritical]
    private void EnsureNotDisposed()
    {
      if (this.m_buffer == null)
        throw new ObjectDisposedException((string) null);
    }

    private void EnsureNotReadOnly()
    {
      if (this.m_readOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static uint GetAlignedSize(int size)
    {
      uint num = (uint) size / 8U * 8U;
      if (size % 8 != 0 || size == 0)
        num += 8U;
      return num;
    }

    [SecurityCritical]
    private unsafe int GetAnsiByteCount()
    {
      uint flags = 1024;
      uint num = 63;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        return Win32Native.WideCharToMultiByte(0U, flags, (char*) pointer, this.m_length, (byte*) null, 0, IntPtr.Zero, new IntPtr((void*) &num));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    private unsafe void GetAnsiBytes(byte* ansiStrPtr, int byteCount)
    {
      uint flags = 1024;
      uint num = 63;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        Win32Native.WideCharToMultiByte(0U, flags, (char*) pointer, this.m_length, ansiStrPtr, byteCount - 1, IntPtr.Zero, new IntPtr((void*) &num));
        *(ansiStrPtr + byteCount - 1) = (byte) 0;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    private void ProtectMemory()
    {
      if (this.m_length == 0 || this.m_encrypted)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        int status = Win32Native.SystemFunction040(this.m_buffer, (uint) (this.m_buffer.Length * 2), 0U);
        if (status < 0)
          throw new CryptographicException(Win32Native.LsaNtStatusToWinError(status));
        this.m_encrypted = true;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToBSTR()
    {
      this.EnsureNotDisposed();
      int length = this.m_length;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num1 = Win32Native.SysAllocStringLen((string) null, length);
        }
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy((byte*) num1.ToPointer(), pointer, length * 2);
        num2 = num1;
        return num2;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num2 == IntPtr.Zero && num1 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num1, (UIntPtr) ((ulong) (length * 2)));
          Win32Native.SysFreeString(num1);
        }
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToUniStr(bool allocateFromHeap)
    {
      this.EnsureNotDisposed();
      int length = this.m_length;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num1 = !allocateFromHeap ? Marshal.AllocCoTaskMem((length + 1) * 2) : Marshal.AllocHGlobal((length + 1) * 2);
        }
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy((byte*) num1.ToPointer(), pointer, length * 2);
        *(short*) ((IntPtr) num1.ToPointer() + (IntPtr) length * 2) = (short) 0;
        num2 = num1;
        return num2;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num2 == IntPtr.Zero && num1 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num1, (UIntPtr) ((ulong) (length * 2)));
          if (allocateFromHeap)
            Marshal.FreeHGlobal(num1);
          else
            Marshal.FreeCoTaskMem(num1);
        }
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToAnsiStr(bool allocateFromHeap)
    {
      this.EnsureNotDisposed();
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      int num3 = 0;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        num3 = this.GetAnsiByteCount() + 1;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num1 = !allocateFromHeap ? Marshal.AllocCoTaskMem(num3) : Marshal.AllocHGlobal(num3);
        }
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.GetAnsiBytes((byte*) num1.ToPointer(), num3);
        num2 = num1;
        return num2;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num2 == IntPtr.Zero && num1 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num1, (UIntPtr) ((ulong) num3));
          if (allocateFromHeap)
            Marshal.FreeHGlobal(num1);
          else
            Marshal.FreeCoTaskMem(num1);
        }
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void UnProtectMemory()
    {
      if (this.m_length == 0)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        if (this.m_encrypted)
        {
          int status = Win32Native.SystemFunction041(this.m_buffer, (uint) (this.m_buffer.Length * 2), 0U);
          if (status < 0)
            throw new CryptographicException(Win32Native.LsaNtStatusToWinError(status));
          this.m_encrypted = false;
        }
      }
    }
  }
}
