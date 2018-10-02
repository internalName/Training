// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationTokenSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Отправляет токену <see cref="T:System.Threading.CancellationToken" /> сигнал отмены.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class CancellationTokenSource : IDisposable
  {
    private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);
    private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);
    private static readonly int s_nLists = PlatformHelper.ProcessorCount > 24 ? 24 : PlatformHelper.ProcessorCount;
    private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);
    private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);
    private volatile int m_threadIDExecutingCallbacks = -1;
    private volatile ManualResetEvent m_kernelEvent;
    private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;
    private const int CANNOT_BE_CANCELED = 0;
    private const int NOT_CANCELED = 1;
    private const int NOTIFYING = 2;
    private const int NOTIFYINGCOMPLETE = 3;
    private volatile int m_state;
    private bool m_disposed;
    private CancellationTokenRegistration[] m_linkingRegistrations;
    private volatile CancellationCallbackInfo m_executingCallback;
    private volatile Timer m_timer;

    private static void LinkedTokenCancelDelegate(object source)
    {
      (source as CancellationTokenSource).Cancel();
    }

    /// <summary>
    ///   Получает значение, указывающее, есть ли для данного объекта <see cref="T:System.Threading.CancellationTokenSource" /> запрос на отмену.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see cref="T:System.Threading.CancellationTokenSource" /> есть запрос на отмену; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCancellationRequested
    {
      [__DynamicallyInvokable] get
      {
        return this.m_state >= 2;
      }
    }

    internal bool IsCancellationCompleted
    {
      get
      {
        return this.m_state == 3;
      }
    }

    internal bool IsDisposed
    {
      get
      {
        return this.m_disposed;
      }
    }

    internal int ThreadIDExecutingCallbacks
    {
      set
      {
        this.m_threadIDExecutingCallbacks = value;
      }
      get
      {
        return this.m_threadIDExecutingCallbacks;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Threading.CancellationToken" />, связанный с этим объектом <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </summary>
    /// <returns>
    ///   Рабочая область метаданных <see cref="T:System.Threading.CancellationToken" />, связанная с этим соединением <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Источник токена был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationToken Token
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        return new CancellationToken(this);
      }
    }

    internal bool CanBeCanceled
    {
      get
      {
        return (uint) this.m_state > 0U;
      }
    }

    internal WaitHandle WaitHandle
    {
      get
      {
        this.ThrowIfDisposed();
        if (this.m_kernelEvent != null)
          return (WaitHandle) this.m_kernelEvent;
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, (ManualResetEvent) null) != null)
          manualResetEvent.Dispose();
        if (this.IsCancellationRequested)
          this.m_kernelEvent.Set();
        return (WaitHandle) this.m_kernelEvent;
      }
    }

    internal CancellationCallbackInfo ExecutingCallback
    {
      get
      {
        return this.m_executingCallback;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </summary>
    [__DynamicallyInvokable]
    public CancellationTokenSource()
    {
      this.m_state = 1;
    }

    private CancellationTokenSource(bool set)
    {
      this.m_state = set ? 3 : 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.CancellationTokenSource" />, который будет отменен после указанного периода времени.
    /// </summary>
    /// <param name="delay">
    ///   Интервал времени ожидания в миллисекундах перед отменой <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="delay" /><see langword="." /><see cref="P:System.TimeSpan.TotalMilliseconds" /> меньше -1 или больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenSource(TimeSpan delay)
    {
      long totalMilliseconds = (long) delay.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (delay));
      this.InitializeWithTimer((int) totalMilliseconds);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.CancellationTokenSource" />, который будет отменен после указанной задержки (в миллисекундах).
    /// </summary>
    /// <param name="millisecondsDelay">
    ///   Интервал времени ожидания в миллисекундах перед отменой <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsDelay" /> — меньше -1.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenSource(int millisecondsDelay)
    {
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsDelay));
      this.InitializeWithTimer(millisecondsDelay);
    }

    private void InitializeWithTimer(int millisecondsDelay)
    {
      this.m_state = 1;
      this.m_timer = new Timer(CancellationTokenSource.s_timerCallback, (object) this, millisecondsDelay, -1);
    }

    /// <summary>Передает запрос на отмену.</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Это <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Исключение aggregate, содержащее все исключения, генерируемые зарегистрированных обратных вызовов в связанном <see cref="T:System.Threading.CancellationToken" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Cancel()
    {
      this.Cancel(false);
    }

    /// <summary>
    ///   Передает запрос отмены и определяет, будут ли последующие обратные вызовы и отменяемые операции обрабатываться.
    /// </summary>
    /// <param name="throwOnFirstException">
    ///   Значение <see langword="true" />, если исключения нужно распространять немедленно; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Это <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Исключение aggregate, содержащее все исключения, генерируемые зарегистрированных обратных вызовов в связанном <see cref="T:System.Threading.CancellationToken" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Cancel(bool throwOnFirstException)
    {
      this.ThrowIfDisposed();
      this.NotifyCancellation(throwOnFirstException);
    }

    /// <summary>
    ///   Планирует операцию отмены для данного объекта <see cref="T:System.Threading.CancellationTokenSource" /> после указанного периода времени.
    /// </summary>
    /// <param name="delay">
    ///   Интервал времени ожидания перед отменой этого объекта <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение возникает, если это <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="delay" /> меньше -1 или больше Int32.MaxValue.
    /// </exception>
    [__DynamicallyInvokable]
    public void CancelAfter(TimeSpan delay)
    {
      long totalMilliseconds = (long) delay.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (delay));
      this.CancelAfter((int) totalMilliseconds);
    }

    /// <summary>
    ///   Планирует операцию отмены для данного объекта <see cref="T:System.Threading.CancellationTokenSource" /> после указанного числа миллисекунд.
    /// </summary>
    /// <param name="millisecondsDelay">
    ///   Интервал времени ожидания перед отменой этого объекта <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение возникает, если это <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение возникает, если <paramref name="millisecondsDelay" /> меньше-1.
    /// </exception>
    [__DynamicallyInvokable]
    public void CancelAfter(int millisecondsDelay)
    {
      this.ThrowIfDisposed();
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsDelay));
      if (this.IsCancellationRequested)
        return;
      if (this.m_timer == null)
      {
        Timer timer = new Timer(CancellationTokenSource.s_timerCallback, (object) this, -1, -1);
        if (Interlocked.CompareExchange<Timer>(ref this.m_timer, timer, (Timer) null) != null)
          timer.Dispose();
      }
      try
      {
        this.m_timer.Change(millisecondsDelay, -1);
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    private static void TimerCallbackLogic(object obj)
    {
      CancellationTokenSource cancellationTokenSource = (CancellationTokenSource) obj;
      if (cancellationTokenSource.IsDisposed)
        return;
      try
      {
        cancellationTokenSource.Cancel();
      }
      catch (ObjectDisposedException ex)
      {
        if (cancellationTokenSource.IsDisposed)
          return;
        throw;
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.CancellationTokenSource" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Threading.CancellationTokenSource" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.m_disposed)
        return;
      if (this.m_timer != null)
        this.m_timer.Dispose();
      CancellationTokenRegistration[] linkingRegistrations = this.m_linkingRegistrations;
      if (linkingRegistrations != null)
      {
        this.m_linkingRegistrations = (CancellationTokenRegistration[]) null;
        for (int index = 0; index < linkingRegistrations.Length; ++index)
          linkingRegistrations[index].Dispose();
      }
      this.m_registeredCallbacksLists = (SparselyPopulatedArray<CancellationCallbackInfo>[]) null;
      if (this.m_kernelEvent != null)
      {
        this.m_kernelEvent.Close();
        this.m_kernelEvent = (ManualResetEvent) null;
      }
      this.m_disposed = true;
    }

    internal void ThrowIfDisposed()
    {
      if (!this.m_disposed)
        return;
      CancellationTokenSource.ThrowObjectDisposedException();
    }

    private static void ThrowObjectDisposedException()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("CancellationTokenSource_Disposed"));
    }

    internal static CancellationTokenSource InternalGetStaticSource(bool set)
    {
      if (!set)
        return CancellationTokenSource._staticSource_NotCancelable;
      return CancellationTokenSource._staticSource_Set;
    }

    internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
    {
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        this.ThrowIfDisposed();
      if (!this.IsCancellationRequested)
      {
        if (this.m_disposed && !AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
          return new CancellationTokenRegistration();
        int index = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
        CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
        SparselyPopulatedArray<CancellationCallbackInfo>[] sparselyPopulatedArrayArray1 = this.m_registeredCallbacksLists;
        if (sparselyPopulatedArrayArray1 == null)
        {
          SparselyPopulatedArray<CancellationCallbackInfo>[] sparselyPopulatedArrayArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
          sparselyPopulatedArrayArray1 = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, sparselyPopulatedArrayArray2, (SparselyPopulatedArray<CancellationCallbackInfo>[]) null) ?? sparselyPopulatedArrayArray2;
        }
        SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray1 = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref sparselyPopulatedArrayArray1[index]);
        if (sparselyPopulatedArray1 == null)
        {
          SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
          Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref sparselyPopulatedArrayArray1[index], sparselyPopulatedArray2, (SparselyPopulatedArray<CancellationCallbackInfo>) null);
          sparselyPopulatedArray1 = sparselyPopulatedArrayArray1[index];
        }
        SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo = sparselyPopulatedArray1.Add(cancellationCallbackInfo);
        CancellationTokenRegistration tokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, registrationInfo);
        if (!this.IsCancellationRequested || !tokenRegistration.TryDeregister())
          return tokenRegistration;
      }
      callback(stateForCallback);
      return new CancellationTokenRegistration();
    }

    private void NotifyCancellation(bool throwOnFirstException)
    {
      if (this.IsCancellationRequested || Interlocked.CompareExchange(ref this.m_state, 2, 1) != 1)
        return;
      this.m_timer?.Dispose();
      this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
      if (this.m_kernelEvent != null)
        this.m_kernelEvent.Set();
      this.ExecuteCallbackHandlers(throwOnFirstException);
    }

    private void ExecuteCallbackHandlers(bool throwOnFirstException)
    {
      List<Exception> exceptionList = (List<Exception>) null;
      SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
      if (registeredCallbacksLists == null)
      {
        Interlocked.Exchange(ref this.m_state, 3);
      }
      else
      {
        try
        {
          for (int index = 0; index < registeredCallbacksLists.Length; ++index)
          {
            SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[index]);
            if (sparselyPopulatedArray != null)
            {
              for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment = sparselyPopulatedArray.Tail; currArrayFragment != null; currArrayFragment = currArrayFragment.Prev)
              {
                for (int currArrayIndex = currArrayFragment.Length - 1; currArrayIndex >= 0; --currArrayIndex)
                {
                  this.m_executingCallback = currArrayFragment[currArrayIndex];
                  if (this.m_executingCallback != null)
                  {
                    CancellationCallbackCoreWorkArguments args = new CancellationCallbackCoreWorkArguments(currArrayFragment, currArrayIndex);
                    try
                    {
                      if (this.m_executingCallback.TargetSyncContext != null)
                      {
                        this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), (object) args);
                        this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
                      }
                      else
                        this.CancellationCallbackCoreWork(args);
                    }
                    catch (Exception ex)
                    {
                      if (throwOnFirstException)
                      {
                        throw;
                      }
                      else
                      {
                        if (exceptionList == null)
                          exceptionList = new List<Exception>();
                        exceptionList.Add(ex);
                      }
                    }
                  }
                }
              }
            }
          }
        }
        finally
        {
          this.m_state = 3;
          this.m_executingCallback = (CancellationCallbackInfo) null;
          Thread.MemoryBarrier();
        }
        if (exceptionList != null)
          throw new AggregateException((IEnumerable<Exception>) exceptionList);
      }
    }

    private void CancellationCallbackCoreWork_OnSyncContext(object obj)
    {
      this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments) obj);
    }

    private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
    {
      CancellationCallbackInfo cancellationCallbackInfo = args.m_currArrayFragment.SafeAtomicRemove(args.m_currArrayIndex, this.m_executingCallback);
      if (cancellationCallbackInfo != this.m_executingCallback)
        return;
      if (cancellationCallbackInfo.TargetExecutionContext != null)
        cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
      cancellationCallbackInfo.ExecuteCallback();
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.CancellationTokenSource" />, который будет иметь отмененное состояние, если какой-либо из исходных токенов находится в отмененном состоянии.
    /// </summary>
    /// <param name="token1">
    ///   Первый токен отмены, который следует контролировать.
    /// </param>
    /// <param name="token2">
    ///   Второй токен отмены, который следует контролировать.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с исходными токенами.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   A <see cref="T:System.Threading.CancellationTokenSource" /> связанные с одной из исходных токенов был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
    {
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      bool canBeCanceled = token2.CanBeCanceled;
      if (token1.CanBeCanceled)
      {
        cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[canBeCanceled ? 2 : 1];
        cancellationTokenSource.m_linkingRegistrations[0] = token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      if (canBeCanceled)
      {
        int index = 1;
        if (cancellationTokenSource.m_linkingRegistrations == null)
        {
          cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[1];
          index = 0;
        }
        cancellationTokenSource.m_linkingRegistrations[index] = token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      return cancellationTokenSource;
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.CancellationTokenSource" />, который будет иметь отмененное состояние, если любой из исходных токенов в заданном массиве находится в отмененном состоянии.
    /// </summary>
    /// <param name="tokens">
    ///   Массив, содержащий экземпляры токена отмены для наблюдения.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с исходными токенами.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   A <see cref="T:System.Threading.CancellationTokenSource" /> связанные с одной из исходных токенов был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="tokens" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="tokens" /> пуст.
    /// </exception>
    [__DynamicallyInvokable]
    public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
    {
      if (tokens == null)
        throw new ArgumentNullException(nameof (tokens));
      if (tokens.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty"));
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
      for (int index = 0; index < tokens.Length; ++index)
      {
        if (tokens[index].CanBeCanceled)
          cancellationTokenSource.m_linkingRegistrations[index] = tokens[index].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      return cancellationTokenSource;
    }

    internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
    {
      SpinWait spinWait = new SpinWait();
      while (this.ExecutingCallback == callbackInfo)
        spinWait.SpinOnce();
    }
  }
}
