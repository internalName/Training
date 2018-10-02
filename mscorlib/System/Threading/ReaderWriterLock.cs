// Decompiled with JetBrains decompiler
// Type: System.Threading.ReaderWriterLock
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Определяет блокировку, которая поддерживает один пишущий поток и несколько читающих.
  /// </summary>
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class ReaderWriterLock : CriticalFinalizerObject
  {
    private IntPtr _hWriterEvent;
    private IntPtr _hReaderEvent;
    private IntPtr _hObjectHandle;
    private int _dwState;
    private int _dwULockID;
    private int _dwLLockID;
    private int _dwWriterID;
    private int _dwWriterSeqNum;
    private short _wWriterLevel;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.ReaderWriterLock" />.
    /// </summary>
    [SecuritySafeCritical]
    public ReaderWriterLock()
    {
      this.PrivateInitialize();
    }

    /// <summary>
    ///   Обеспечивает освобождение ресурсов и выполнение других завершающих операций, когда сборщик мусора восстанавливает объект <see cref="T:System.Threading.ReaderWriterLock" />.
    /// </summary>
    [SecuritySafeCritical]
    ~ReaderWriterLock()
    {
      this.PrivateDestruct();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, владеет ли текущий поток блокировкой чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток владеет блокировкой чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsReaderLockHeld
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.PrivateGetIsReaderLockHeld();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, владеет ли текущий поток блокировкой записи.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток владеет блокировкой записи; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsWriterLockHeld
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.PrivateGetIsWriterLockHeld();
      }
    }

    /// <summary>Возвращает текущий последовательный номер.</summary>
    /// <returns>Текущий последовательный номер.</returns>
    public int WriterSeqNum
    {
      [SecuritySafeCritical] get
      {
        return this.PrivateGetWriterSeqNum();
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void AcquireReaderLockInternal(int millisecondsTimeout);

    /// <summary>
    ///   Получает блокировку чтения, используя значение <see cref="T:System.Int32" /> для задания времени ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах.
    /// </param>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="millisecondsTimeout" /> истекает до запроса на блокировку.
    /// </exception>
    [SecuritySafeCritical]
    public void AcquireReaderLock(int millisecondsTimeout)
    {
      this.AcquireReaderLockInternal(millisecondsTimeout);
    }

    /// <summary>
    ///   Получает блокировку чтения, используя значение <see cref="T:System.TimeSpan" /> для задания времени ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see langword="TimeSpan" />, задающий время ожидания.
    /// </param>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="timeout" /> истекает до запроса на блокировку.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Указывает отрицательное значение, отличное от-1 миллисекунду.
    /// </exception>
    [SecuritySafeCritical]
    public void AcquireReaderLock(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      this.AcquireReaderLockInternal((int) totalMilliseconds);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void AcquireWriterLockInternal(int millisecondsTimeout);

    /// <summary>
    ///   Получает блокировку записи, используя значение <see cref="T:System.Int32" /> для задания времени ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах.
    /// </param>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="timeout" /> истекает до запроса на блокировку.
    /// </exception>
    [SecuritySafeCritical]
    public void AcquireWriterLock(int millisecondsTimeout)
    {
      this.AcquireWriterLockInternal(millisecondsTimeout);
    }

    /// <summary>
    ///   Получает блокировку записи, используя значение <see cref="T:System.TimeSpan" /> для задания времени ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see langword="TimeSpan" />, задающий время ожидания.
    /// </param>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="timeout" /> истекает до запроса на блокировку.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Указывает отрицательное значение, отличное от-1 миллисекунду.
    /// </exception>
    [SecuritySafeCritical]
    public void AcquireWriterLock(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      this.AcquireWriterLockInternal((int) totalMilliseconds);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void ReleaseReaderLockInternal();

    /// <summary>Уменьшает на единицу счетчик блокировок.</summary>
    /// <exception cref="T:System.ApplicationException">
    ///   Поток не поддерживает все блокировки чтения или записи.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void ReleaseReaderLock()
    {
      this.ReleaseReaderLockInternal();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void ReleaseWriterLockInternal();

    /// <summary>
    ///   Уменьшает на единицу счетчик блокировок данной блокировки записи.
    /// </summary>
    /// <exception cref="T:System.ApplicationException">
    ///   Поток не поддерживает блокировку записи.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void ReleaseWriterLock()
    {
      this.ReleaseWriterLockInternal();
    }

    /// <summary>
    ///   Повышает уровень блокировки чтения до блокировки записи, используя значение <see langword="Int32" /> для задания времени ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.Threading.LockCookie" />.
    /// </returns>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="millisecondsTimeout" /> истекает до запроса на блокировку.
    /// </exception>
    [SecuritySafeCritical]
    public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
    {
      LockCookie result = new LockCookie();
      this.FCallUpgradeToWriterLock(ref result, millisecondsTimeout);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void FCallUpgradeToWriterLock(ref LockCookie result, int millisecondsTimeout);

    /// <summary>
    ///   Повышает уровень блокировки чтения до блокировки записи, используя значение <see langword="TimeSpan" /> для задания времени ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see langword="TimeSpan" />, задающий время ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.Threading.LockCookie" />.
    /// </returns>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="timeout" /> истекает до запроса на блокировку.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Указывает отрицательное значение, отличное от-1 миллисекунду.
    /// </exception>
    public LockCookie UpgradeToWriterLock(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.UpgradeToWriterLock((int) totalMilliseconds);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void DowngradeFromWriterLockInternal(ref LockCookie lockCookie);

    /// <summary>
    ///   Возвращает состояние блокировки потока к тому, которое было до вызова метода <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />.
    /// </summary>
    /// <param name="lockCookie">
    ///   Объект <see cref="T:System.Threading.LockCookie" />, возвращаемый <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />.
    /// </param>
    /// <exception cref="T:System.ApplicationException">
    ///   Поток не поддерживает блокировку записи.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="lockCookie" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    public void DowngradeFromWriterLock(ref LockCookie lockCookie)
    {
      this.DowngradeFromWriterLockInternal(ref lockCookie);
    }

    /// <summary>
    ///   Освобождает блокировку, независимо от количества ее получений потоком.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.LockCookie" />, представляющий освобожденную блокировку.
    /// </returns>
    [SecuritySafeCritical]
    public LockCookie ReleaseLock()
    {
      LockCookie result = new LockCookie();
      this.FCallReleaseLock(ref result);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void FCallReleaseLock(ref LockCookie result);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void RestoreLockInternal(ref LockCookie lockCookie);

    /// <summary>
    ///   Возвращает состояние блокировки потока к тому, которое было до вызова метода <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.
    /// </summary>
    /// <param name="lockCookie">
    ///   Объект <see cref="T:System.Threading.LockCookie" />, возвращаемый <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.
    /// </param>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="lockCookie" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    public void RestoreLock(ref LockCookie lockCookie)
    {
      this.RestoreLockInternal(ref lockCookie);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool PrivateGetIsReaderLockHeld();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool PrivateGetIsWriterLockHeld();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int PrivateGetWriterSeqNum();

    /// <summary>
    ///   Показывает, была ли предоставлена блокировка записи какому-либо потоку со времени получения последовательного номера.
    /// </summary>
    /// <param name="seqNum">Порядковый номер.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если блокировка записи была предоставлена какому-либо потоку с момента получения порядкового номера; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool AnyWritersSince(int seqNum);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void PrivateInitialize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void PrivateDestruct();
  }
}
