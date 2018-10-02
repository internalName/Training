// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Инкапсулирует связанные с операционной системой объекты, ожидающие монопольного доступа к общим ресурсам.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public abstract class WaitHandle : MarshalByRefObject, IDisposable
  {
    /// <summary>
    ///   Представляет недопустимый собственный дескриптор операционной системы.
    ///    Это поле доступно только для чтения.
    /// </summary>
    protected static readonly IntPtr InvalidHandle = WaitHandle.GetInvalidHandle();
    /// <summary>
    ///   Указывает, что время ожидания операции <see cref="M:System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[],System.Int32,System.Boolean)" /> истекло до получения сигнала каким-либо из дескрипторов ожидания.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const int WaitTimeout = 258;
    private const int MAX_WAITHANDLES = 64;
    private IntPtr waitHandle;
    [SecurityCritical]
    internal volatile SafeWaitHandle safeWaitHandle;
    internal bool hasThreadAffinity;
    private const int WAIT_OBJECT_0 = 0;
    private const int WAIT_ABANDONED = 128;
    private const int WAIT_FAILED = 2147483647;
    private const int ERROR_TOO_MANY_POSTS = 298;

    [SecuritySafeCritical]
    private static IntPtr GetInvalidHandle()
    {
      return Win32Native.INVALID_HANDLE_VALUE;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.WaitHandle" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected WaitHandle()
    {
      this.Init();
    }

    [SecuritySafeCritical]
    private void Init()
    {
      this.safeWaitHandle = (SafeWaitHandle) null;
      this.waitHandle = WaitHandle.InvalidHandle;
      this.hasThreadAffinity = false;
    }

    /// <summary>
    ///   Возвращает или задает собственный дескриптор операционной системы.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="IntPtr" />, представляющий собственный дескриптор операционной системы.
    ///    Значением по умолчанию является значение поля <see cref="F:System.Threading.WaitHandle.InvalidHandle" />.
    /// </returns>
    [Obsolete("Use the SafeWaitHandle property instead.")]
    public virtual IntPtr Handle
    {
      [SecuritySafeCritical] get
      {
        if (this.safeWaitHandle != null)
          return this.safeWaitHandle.DangerousGetHandle();
        return WaitHandle.InvalidHandle;
      }
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] set
      {
        if (value == WaitHandle.InvalidHandle)
        {
          if (this.safeWaitHandle != null)
          {
            this.safeWaitHandle.SetHandleAsInvalid();
            this.safeWaitHandle = (SafeWaitHandle) null;
          }
        }
        else
          this.safeWaitHandle = new SafeWaitHandle(value, true);
        this.waitHandle = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает собственный дескриптор операционной системы.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" />, представляющий собственный дескриптор операционной системы.
    /// </returns>
    public SafeWaitHandle SafeWaitHandle
    {
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        if (this.safeWaitHandle == null)
          this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
        return this.safeWaitHandle;
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] set
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          if (value == null)
          {
            this.safeWaitHandle = (SafeWaitHandle) null;
            this.waitHandle = WaitHandle.InvalidHandle;
          }
          else
          {
            this.safeWaitHandle = value;
            this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
          }
        }
      }
    }

    [SecurityCritical]
    internal void SetHandleInternal(SafeWaitHandle handle)
    {
      this.safeWaitHandle = handle;
      this.waitHandle = handle.DangerousGetHandle();
    }

    /// <summary>
    ///   Блокирует текущий поток до получения сигнала текущим объектом <see cref="T:System.Threading.WaitHandle" />, используя 32-разрядное целое число со знаком для задания периода времени и указывая, следует ли выйти из домена синхронизации до начала ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> при получении сигнала текущим экземпляром; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание закончилось, так как поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в ОС Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр является прозрачным прокси-сервер для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.WaitOne((long) millisecondsTimeout, exitContext);
    }

    /// <summary>
    ///   Блокирует текущий поток до получения сигнала текущим экземпляром, используя значение типа <see cref="T:System.TimeSpan" /> для задания интервала времени и указывая, следует ли выйти из домена синхронизации до начала ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> при получении сигнала текущим экземпляром; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр является прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (-1L > totalMilliseconds || (long) int.MaxValue < totalMilliseconds)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.WaitOne(totalMilliseconds, exitContext);
    }

    /// <summary>
    ///   Блокирует текущий поток до получения сигнала объектом <see cref="T:System.Threading.WaitHandle" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр получает сигнал.
    ///    Пока текущий экземпляр не сигнализирует, метод <see cref="M:System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean)" /> не возвращает управление.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр является прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool WaitOne()
    {
      return this.WaitOne(-1, false);
    }

    /// <summary>
    ///   Блокирует текущий поток до получения текущим дескриптором <see cref="T:System.Threading.WaitHandle" /> сигнала, используя 32-разрядное целое число со знаком для указания интервала времени в миллисекундах.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> при получении сигнала текущим экземпляром; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр является прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool WaitOne(int millisecondsTimeout)
    {
      return this.WaitOne(millisecondsTimeout, false);
    }

    /// <summary>
    ///   Блокирует текущий поток до получения сигнала текущим экземпляром, используя значение типа <see cref="T:System.TimeSpan" /> для указания интервала времени.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> при получении сигнала текущим экземпляром; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр является прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool WaitOne(TimeSpan timeout)
    {
      return this.WaitOne(timeout, false);
    }

    [SecuritySafeCritical]
    private bool WaitOne(long timeout, bool exitContext)
    {
      return WaitHandle.InternalWaitOne((SafeHandle) this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
    }

    [SecurityCritical]
    internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
    {
      if (waitableSafeHandle == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      int num = WaitHandle.WaitOneNative(waitableSafeHandle, (uint) millisecondsTimeout, hasThreadAffinity, exitContext);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      if (num == 128)
        WaitHandle.ThrowAbandonedMutexException();
      return num != 258;
    }

    [SecurityCritical]
    internal bool WaitOneWithoutFAS()
    {
      if (this.safeWaitHandle == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      int num = WaitHandle.WaitOneNative((SafeHandle) this.safeWaitHandle, uint.MaxValue, this.hasThreadAffinity, false);
      if (num == 128)
        WaitHandle.ThrowAbandonedMutexException();
      return num != 258;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll);

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива, используя значение типа <see cref="T:System.Int32" /> для задания интервала времени и указывая, следует ли выйти из домена синхронизации до начала ожидания.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    ///    Данный массив не может содержать несколько ссылок на один и тот же объект (дубликатов).
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если каждый элемент массива <paramref name="waitHandles" /> получил сигнал; в противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и платформе .NET Framework версии 2.0 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">
    ///   <paramref name="waitHandles" /> Массив содержит элементы, которые являются дубликатами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.STAThreadAttribute" /> Атрибут применяется к процедуре потока для текущего потока и <paramref name="waitHandles" /> содержит более одного элемента.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и является .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [SecuritySafeCritical]
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      if (waitHandles == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
      if (waitHandles.Length == 0)
        throw new ArgumentNullException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
      if (waitHandles.Length > 64)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      WaitHandle[] waitHandles1 = new WaitHandle[waitHandles.Length];
      for (int index = 0; index < waitHandles.Length; ++index)
      {
        WaitHandle waitHandle = waitHandles[index];
        if (waitHandle == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
        if (RemotingServices.IsTransparentProxy((object) waitHandle))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
        waitHandles1[index] = waitHandle;
      }
      int num = WaitHandle.WaitMultiple(waitHandles1, millisecondsTimeout, exitContext, true);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      if (128 <= num && 128 + waitHandles1.Length > num)
        WaitHandle.ThrowAbandonedMutexException();
      GC.KeepAlive((object) waitHandles1);
      return num != 258;
    }

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива, используя значение типа <see cref="T:System.TimeSpan" /> для задания интервала времени и указывая, следует ли выйти из домена синхронизации до начала ожидания.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    ///    Этот массив не может содержать несколько ссылок на один и тот же объект.
    /// </param>
    /// <param name="timeout">
    ///   Объект <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или объект <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если каждый элемент массива <paramref name="waitHandles" /> получил сигнал; в противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и платформе .NET Framework версии 2.0 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">
    ///   <paramref name="waitHandles" /> Массив содержит элементы, которые являются дубликатами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.STAThreadAttribute" /> Атрибут применяется к процедуре потока для текущего потока и <paramref name="waitHandles" /> содержит более одного элемента.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и является .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание прервано, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (-1L > totalMilliseconds || (long) int.MaxValue < totalMilliseconds)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.WaitAll(waitHandles, (int) totalMilliseconds, exitContext);
    }

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    ///    Этот массив не может содержать несколько ссылок на один и тот же объект.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, когда каждый элемент <paramref name="waitHandles" /> получил сигнал. В противном случае возврат из метода не происходит.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    ///    -или-
    /// 
    ///   Один или несколько объектов массива <paramref name="waitHandles" /> имеют значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="waitHandles" /> не содержит элементов, и используется платформа .NET Framework версии 2.0 или более поздней.
    /// </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.ArgumentException" />.
    /// 
    ///   Массив <paramref name="waitHandles" /> содержит повторяющиеся элементы.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Массив <paramref name="waitHandles" /> содержит больше объектов, чем разрешено системой.
    /// 
    ///   -или-
    /// 
    ///   Атрибут <see cref="T:System.STAThreadAttribute" /> применяется к процедуре потока для текущего потока, а массив <paramref name="waitHandles" /> содержит более одного элемента.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   Массив <paramref name="waitHandles" /> не содержит элементов, и используется платформа .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание прервано, так как поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в ОС Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Массив <paramref name="waitHandles" /> содержит прозрачный прокси для элемента <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles)
    {
      return WaitHandle.WaitAll(waitHandles, -1, true);
    }

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива, используя значение <see cref="T:System.Int32" /> для указания интервала времени.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    ///    Данный массив не может содержать несколько ссылок на один и тот же объект (дубликатов).
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если каждый элемент массива <paramref name="waitHandles" /> получил сигнал; в противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов массива <paramref name="waitHandles" /> имеют значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   В массиве <paramref name="waitHandles" /> отсутствуют элементы.
    /// </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.ArgumentException" />.
    /// 
    ///   Массив <paramref name="waitHandles" /> содержит повторяющиеся элементы.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Массив <paramref name="waitHandles" /> содержит больше объектов, чем разрешено системой.
    /// 
    ///   -или-
    /// 
    ///   Атрибут <see cref="T:System.STAThreadAttribute" /> применяется к процедуре потока для текущего потока, а массив <paramref name="waitHandles" /> содержит более одного элемента.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание закончилось, так как поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в ОС Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Массив <paramref name="waitHandles" /> содержит прозрачный прокси для элемента <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
    }

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива, используя значение <see cref="T:System.TimeSpan" /> для указания интервала времени.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    ///    Этот массив не может содержать несколько ссылок на один и тот же объект.
    /// </param>
    /// <param name="timeout">
    ///   Объект <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или объект <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если каждый элемент массива <paramref name="waitHandles" /> получил сигнал; в противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов массива <paramref name="waitHandles" /> имеют значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   В массиве <paramref name="waitHandles" /> отсутствуют элементы.
    /// </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.ArgumentException" />.
    /// 
    ///   Массив <paramref name="waitHandles" /> содержит повторяющиеся элементы.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Массив <paramref name="waitHandles" /> содержит больше объектов, чем разрешено системой.
    /// 
    ///   -или-
    /// 
    ///   Атрибут <see cref="T:System.STAThreadAttribute" /> применяется к процедуре потока для текущего потока, а массив <paramref name="waitHandles" /> содержит более одного элемента.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое означает бесконечное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание прервано, так как поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в ОС Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Массив <paramref name="waitHandles" /> содержит прозрачный прокси для элемента <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return WaitHandle.WaitAll(waitHandles, timeout, true);
    }

    /// <summary>
    ///   Ожидает, пока какой-либо из элементов заданного массива не получит сигнал, используя 32-разрядное целое число со знаком для задания интервала времени и определения, нужно ли осуществить выход из домена синхронизации до окончания ожидания.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Индекс объекта в массиве, удовлетворившего условиям ожидания, или значение <see cref="F:System.Threading.WaitHandle.WaitTimeout" />, если ни один из объектов не удовлетворил условиям ожидания и истек интервал времени, равный <paramref name="millisecondsTimeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="waitHandles" /> массив без элементов, которое версии платформы .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и платформе .NET Framework версии 2.0 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      if (waitHandles == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
      if (waitHandles.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
      if (64 < waitHandles.Length)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      WaitHandle[] waitHandles1 = new WaitHandle[waitHandles.Length];
      for (int index = 0; index < waitHandles.Length; ++index)
      {
        WaitHandle waitHandle = waitHandles[index];
        if (waitHandle == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
        if (RemotingServices.IsTransparentProxy((object) waitHandle))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
        waitHandles1[index] = waitHandle;
      }
      int num = WaitHandle.WaitMultiple(waitHandles1, millisecondsTimeout, exitContext, false);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      if (128 <= num && 128 + waitHandles1.Length > num)
      {
        int location = num - 128;
        if (0 <= location && location < waitHandles1.Length)
          WaitHandle.ThrowAbandonedMutexException(location, waitHandles1[location]);
        else
          WaitHandle.ThrowAbandonedMutexException();
      }
      GC.KeepAlive((object) waitHandles1);
      return num;
    }

    /// <summary>
    ///   Ожидает получения сигнала какими-либо элементами заданного массива, используя <see cref="T:System.TimeSpan" /> для задания интервала времени и указывая, следует ли выйти из домена синхронизации до начала ожидания.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    /// </param>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Индекс объекта в массиве, удовлетворившего условиям ожидания, или значение <see cref="F:System.Threading.WaitHandle.WaitTimeout" />, если ни один из объектов не удовлетворил условиям ожидания и истек интервал времени, равный <paramref name="timeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов массива <paramref name="waitHandles" /> имеют значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Массив <paramref name="waitHandles" /> содержит больше объектов, чем разрешено системой.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="waitHandles" />массив без элементов, который платформа .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое означает бесконечное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание закончилось, так как поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в ОС Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="waitHandles" />представляет собой массив без элементов, и платформа .NET Framework версии 2.0 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Массив <paramref name="waitHandles" /> содержит прозрачный прокси для элемента <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (-1L > totalMilliseconds || (long) int.MaxValue < totalMilliseconds)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.WaitAny(waitHandles, (int) totalMilliseconds, exitContext);
    }

    /// <summary>
    ///   Ожидает получения сигнала любыми элементами заданного массива, используя значение типа <see cref="T:System.TimeSpan" /> для указания интервала времени.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    /// </param>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Индекс объекта в массиве, удовлетворившего условиям ожидания, или значение <see cref="F:System.Threading.WaitHandle.WaitTimeout" />, если ни один из объектов не удовлетворил условиям ожидания и истек интервал времени, равный <paramref name="timeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return WaitHandle.WaitAny(waitHandles, timeout, true);
    }

    /// <summary>
    ///   Ожидает получения сигнала какими-либо элементами заданного массива.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Индекс объекта, удовлетворившего операцию ожидания, в массиве.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   <paramref name="waitHandles" /> массив без элементов, которое версии платформы .NET Framework версии 1.0 или 1.1.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов и платформе .NET Framework версии 2.0 или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles)
    {
      return WaitHandle.WaitAny(waitHandles, -1, true);
    }

    /// <summary>
    ///   Ожидает получения сигнала любыми элементами указанного массива, используя 32-разрядное целое число со знаком для задания интервала времени.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив <see langword="WaitHandle" />, содержащий объекты, ожидаемые текущим экземпляром.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Индекс объекта в массиве, удовлетворившего условиям ожидания, или значение <see cref="F:System.Threading.WaitHandle.WaitTimeout" />, если ни один из объектов не удовлетворил условиям ожидания и истек интервал времени, равный <paramref name="millisecondsTimeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько объектов в <paramref name="waitHandles" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Число объектов в <paramref name="waitHandles" /> больше, чем системой.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="waitHandles" /> представляет собой массив без элементов.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="waitHandles" /> Массив содержит прозрачный прокси для <see cref="T:System.Threading.WaitHandle" /> в другом домене приложения.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

    /// <summary>
    ///   Подает сигнал одному объекту <see cref="T:System.Threading.WaitHandle" /> и ожидает другого.
    /// </summary>
    /// <param name="toSignal">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, который получает сигнал.
    /// </param>
    /// <param name="toWaitOn">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, сигнализация которого ожидается.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если и сигнал, и ожидание завершаются удачно; если операция ожидания не завершается, то возврат из метода не происходит.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="toSignal" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="toWaitOn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод был вызван в потоке, который имеет <see cref="T:System.STAThreadAttribute" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Этот метод не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="toSignal" /> является семафор, и оно уже имеет полный подсчет.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
    {
      return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
    }

    /// <summary>
    ///   Передает сигнал одному объекту <see cref="T:System.Threading.WaitHandle" /> и ожидает сигнализации другого, задавая время ожидания в виде <see cref="T:System.TimeSpan" /> и указывая, следует ли выйти из домена синхронизации контекста до начала ожидания.
    /// </summary>
    /// <param name="toSignal">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, который получает сигнал.
    /// </param>
    /// <param name="toWaitOn">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, сигнализация которого ожидается.
    /// </param>
    /// <param name="timeout">
    ///   Объект <see cref="T:System.TimeSpan" />, представляющий период ожидания.
    ///    Если значение равно -1, то ожидание выполняется неограниченное время.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если и передача сигнала, и ожидание завершились успешно; значение <see langword="false" />, если передача сигнала была выполнена, но время ожидания истекло.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="toSignal" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="toWaitOn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод был вызван в потоке, который имеет <see cref="T:System.STAThreadAttribute" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Этот метод не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="toSignal" /> является семафор, и оно уже имеет полный подсчет.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Возвращает отрицательное число миллисекунд, отличным от -1.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (-1L > totalMilliseconds || (long) int.MaxValue < totalMilliseconds)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int) totalMilliseconds, exitContext);
    }

    /// <summary>
    ///   Передает сигнал одному объекту <see cref="T:System.Threading.WaitHandle" /> и ожидает сигнализации другого, задавая время ожидания в виде 32-разрядного целого числа со знаком и указывая, следует ли выйти из домена синхронизации контекста до начала ожидания.
    /// </summary>
    /// <param name="toSignal">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, который получает сигнал.
    /// </param>
    /// <param name="toWaitOn">
    ///   Объект <see cref="T:System.Threading.WaitHandle" />, сигнализация которого ожидается.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Целое число, представляющее интервал ожидания.
    ///    Если значение равно <see cref="F:System.Threading.Timeout.Infinite" />, то есть -1, то ожидание длится неограниченное время.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если и передача сигнала, и ожидание завершились успешно; значение <see langword="false" />, если передача сигнала была выполнена, но время ожидания истекло.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="toSignal" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="toWaitOn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод вызывается в потоке, который имеет <see cref="T:System.STAThreadAttribute" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Этот метод не поддерживается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="toSignal" /> является семафор, и оно уже имеет полный подсчет.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">
    ///   Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
    ///    Это исключение не вызывается в Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Threading.WaitHandle" /> Не может получить сигнал, поскольку это приведет к превышению максимального значения.
    /// </exception>
    [SecuritySafeCritical]
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
    {
      if (toSignal == null)
        throw new ArgumentNullException(nameof (toSignal));
      if (toWaitOn == null)
        throw new ArgumentNullException(nameof (toWaitOn));
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
      if (int.MaxValue != num && toSignal.hasThreadAffinity)
      {
        Thread.EndCriticalRegion();
        Thread.EndThreadAffinity();
      }
      if (128 == num)
        WaitHandle.ThrowAbandonedMutexException();
      if (298 == num)
        throw new InvalidOperationException(Environment.GetResourceString("Threading.WaitHandleTooManyPosts"));
      return num == 0;
    }

    private static void ThrowAbandonedMutexException()
    {
      throw new AbandonedMutexException();
    }

    private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
    {
      throw new AbandonedMutexException(location, handle);
    }

    /// <summary>
    ///   Освобождает все ресурсы, удерживаемые текущим объектом <see cref="T:System.Threading.WaitHandle" />.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   При переопределении в производном классе освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Threading.WaitHandle" />, и при необходимости освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="explicitDisposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool explicitDisposing)
    {
      if (this.safeWaitHandle == null)
        return;
      this.safeWaitHandle.Close();
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.WaitHandle" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal enum OpenExistingResult
    {
      Success,
      NameNotFound,
      PathNotFound,
      NameInvalid,
    }
  }
}
