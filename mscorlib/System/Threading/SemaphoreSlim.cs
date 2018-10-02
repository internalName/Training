// Decompiled with JetBrains decompiler
// Type: System.Threading.SemaphoreSlim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
  /// <summary>
  ///   Представляет упрощенную альтернативу семафору <see cref="T:System.Threading.Semaphore" />, ограничивающему количество потоков, которые могут параллельно обращаться к ресурсу или пулу ресурсов.
  /// </summary>
  [ComVisible(false)]
  [DebuggerDisplay("Current Count = {m_currentCount}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class SemaphoreSlim : IDisposable
  {
    private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions) 16384, new CancellationToken());
    private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);
    private volatile int m_currentCount;
    private readonly int m_maxCount;
    private volatile int m_waitCount;
    private object m_lockObj;
    private volatile ManualResetEvent m_waitHandle;
    private SemaphoreSlim.TaskNode m_asyncHead;
    private SemaphoreSlim.TaskNode m_asyncTail;
    private const int NO_MAXIMUM = 2147483647;

    /// <summary>
    ///   Возвращает количество оставшихся потоков, которым разрешено входить в объект <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </summary>
    /// <returns>
    ///   Количество оставшихся потоков, которым разрешено входить в семафор.
    /// </returns>
    [__DynamicallyInvokable]
    public int CurrentCount
    {
      [__DynamicallyInvokable] get
      {
        return this.m_currentCount;
      }
    }

    /// <summary>
    ///   Возвращает дескриптор <see cref="T:System.Threading.WaitHandle" />, который можно использовать для ожидания семафора.
    /// </summary>
    /// <returns>
    ///   Дескриптор <see cref="T:System.Threading.WaitHandle" />, который можно использовать для ожидания семафора.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.SemaphoreSlim" /> удален.
    /// </exception>
    [__DynamicallyInvokable]
    public WaitHandle AvailableWaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.CheckDispose();
        if (this.m_waitHandle != null)
          return (WaitHandle) this.m_waitHandle;
        lock (this.m_lockObj)
        {
          if (this.m_waitHandle == null)
            this.m_waitHandle = new ManualResetEvent((uint) this.m_currentCount > 0U);
        }
        return (WaitHandle) this.m_waitHandle;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreSlim" />, указывая первоначальное число запросов, которые могут выполняться одновременно.
    /// </summary>
    /// <param name="initialCount">
    ///   Начальное количество запросов для семафора, которое может быть обеспечено одновременно.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="initialCount" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public SemaphoreSlim(int initialCount)
      : this(initialCount, int.MaxValue)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreSlim" />, указывая изначальное и максимальное число запросов, которые могут выполняться одновременно.
    /// </summary>
    /// <param name="initialCount">
    ///   Начальное количество запросов для семафора, которое может быть обеспечено одновременно.
    /// </param>
    /// <param name="maxCount">
    ///   Максимальное количество запросов семафора, которое может быть обеспеченно одновременно.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="initialCount" /> меньше 0, или <paramref name="initialCount" /> больше, чем <paramref name="maxCount" />, или <paramref name="maxCount" /> равно или меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public SemaphoreSlim(int initialCount, int maxCount)
    {
      if (initialCount < 0 || initialCount > maxCount)
        throw new ArgumentOutOfRangeException(nameof (initialCount), (object) initialCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_InitialCountWrong"));
      if (maxCount <= 0)
        throw new ArgumentOutOfRangeException(nameof (maxCount), (object) maxCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_MaxCountWrong"));
      this.m_maxCount = maxCount;
      this.m_lockObj = new object();
      this.m_currentCount = initialCount;
    }

    /// <summary>
    ///   Блокирует текущий поток, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до тех пор, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />, и контролирует токен <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>
    ///   Блокирует текущий поток до тех пор, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />, используя значение <see cref="T:System.TimeSpan" /> для определения времени ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   SemaphoreSlim экземпляр был удален<paramref name="." />
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.Wait((int) timeout.TotalMilliseconds, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до тех пор, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />, используя значение <see cref="T:System.TimeSpan" />, которое определяет время ожидания, и контролирует токен <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   SemaphoreSlim экземпляр был удален<paramref name="." />
    /// 
    ///   <paramref name="-or-" />
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.Wait((int) timeout.TotalMilliseconds, cancellationToken);
    }

    /// <summary>
    ///   Блокирует текущий поток до тех пор, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />, используя 32-разрядное целое число со знаком, которое определяет время ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до тех пор, пока он не сможет войти в <see cref="T:System.Threading.SemaphoreSlim" />, используя 32-разрядное целое число со знаком, которое определяет время ожидания, и контролирует токен <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.SemaphoreSlim" /> Экземпляр был удален, или <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.CheckDispose();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("totalMilliSeconds", (object) millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      cancellationToken.ThrowIfCancellationRequested();
      uint startTime = 0;
      if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
        startTime = TimeoutHelper.GetTime();
      bool flag = false;
      Task<bool> task = (Task<bool>) null;
      bool lockTaken = false;
      CancellationTokenRegistration tokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, (object) this);
      try
      {
        SpinWait spinWait = new SpinWait();
        while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
          spinWait.SpinOnce();
        try
        {
        }
        finally
        {
          Monitor.Enter(this.m_lockObj, ref lockTaken);
          if (lockTaken)
            ++this.m_waitCount;
        }
        if (this.m_asyncHead != null)
        {
          task = this.WaitAsync(millisecondsTimeout, cancellationToken);
        }
        else
        {
          OperationCanceledException canceledException = (OperationCanceledException) null;
          if (this.m_currentCount == 0)
          {
            if (millisecondsTimeout == 0)
              return false;
            try
            {
              flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, startTime, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
              canceledException = ex;
            }
          }
          if (this.m_currentCount > 0)
          {
            flag = true;
            --this.m_currentCount;
          }
          else if (canceledException != null)
            throw canceledException;
          if (this.m_waitHandle != null)
          {
            if (this.m_currentCount == 0)
              this.m_waitHandle.Reset();
          }
        }
      }
      finally
      {
        if (lockTaken)
        {
          --this.m_waitCount;
          Monitor.Exit(this.m_lockObj);
        }
        tokenRegistration.Dispose();
      }
      if (task == null)
        return flag;
      return task.GetAwaiter().GetResult();
    }

    private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
    {
      int millisecondsTimeout1 = -1;
      while (this.m_currentCount == 0)
      {
        cancellationToken.ThrowIfCancellationRequested();
        if (millisecondsTimeout != -1)
        {
          millisecondsTimeout1 = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
          if (millisecondsTimeout1 <= 0)
            return false;
        }
        if (!Monitor.Wait(this.m_lockObj, millisecondsTimeout1))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </summary>
    /// <returns>Задача, которая завершается при входе в семафор.</returns>
    [__DynamicallyInvokable]
    public Task WaitAsync()
    {
      return (Task) this.WaitAsync(-1, new CancellationToken());
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>Задача, которая завершается при входе в семафор.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    [__DynamicallyInvokable]
    public Task WaitAsync(CancellationToken cancellationToken)
    {
      return (Task) this.WaitAsync(-1, cancellationToken);
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />, используя 32-разрядное целое число со знаком для измерения интервала времени.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Задача, которая будет завершаться с результатом <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />, и с результатом <see langword="false" /> в противном случае.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(int millisecondsTimeout)
    {
      return this.WaitAsync(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />, используя <see cref="T:System.TimeSpan" /> для измерения интервала времени.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Задача, которая будет завершаться с результатом <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />, и с результатом <see langword="false" /> в противном случае.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от -1, которое представляет неограниченное время ожидания
    /// 
    ///   -или-
    /// 
    ///   время ожидания больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(TimeSpan timeout)
    {
      return this.WaitAsync(timeout, new CancellationToken());
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />, используя <see cref="T:System.TimeSpan" /> для измерения интервала времени и контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   Задача, которая будет завершаться с результатом <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />, и с результатом <see langword="false" /> в противном случае.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от -1, которое представляет неограниченное время ожидания
    /// 
    ///   -или-
    /// 
    ///   время ожидания больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.WaitAsync((int) timeout.TotalMilliseconds, cancellationToken);
    }

    /// <summary>
    ///   Асинхронно ожидает входа в <see cref="T:System.Threading.SemaphoreSlim" />, используя 32-разрядное целое число со знаком для измерения интервала времени, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   Задача, которая будет завершаться с результатом <see langword="true" />, если текущий поток успешно вошел в <see cref="T:System.Threading.SemaphoreSlim" />, и с результатом <see langword="false" /> в противном случае.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.CheckDispose();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("totalMilliSeconds", (object) millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<bool>(cancellationToken);
      lock (this.m_lockObj)
      {
        if (this.m_currentCount > 0)
        {
          --this.m_currentCount;
          if (this.m_waitHandle != null && this.m_currentCount == 0)
            this.m_waitHandle.Reset();
          return SemaphoreSlim.s_trueTask;
        }
        SemaphoreSlim.TaskNode andAddAsyncWaiter = this.CreateAndAddAsyncWaiter();
        return millisecondsTimeout != -1 || cancellationToken.CanBeCanceled ? this.WaitUntilCountOrTimeoutAsync(andAddAsyncWaiter, millisecondsTimeout, cancellationToken) : (Task<bool>) andAddAsyncWaiter;
      }
    }

    private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
    {
      SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
      if (this.m_asyncHead == null)
      {
        this.m_asyncHead = taskNode;
        this.m_asyncTail = taskNode;
      }
      else
      {
        this.m_asyncTail.Next = taskNode;
        taskNode.Prev = this.m_asyncTail;
        this.m_asyncTail = taskNode;
      }
      return taskNode;
    }

    private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
    {
      bool flag = this.m_asyncHead == task || task.Prev != null;
      if (task.Next != null)
        task.Next.Prev = task.Prev;
      if (task.Prev != null)
        task.Prev.Next = task.Next;
      if (this.m_asyncHead == task)
        this.m_asyncHead = task.Next;
      if (this.m_asyncTail == task)
        this.m_asyncTail = task.Prev;
      task.Next = task.Prev = (SemaphoreSlim.TaskNode) null;
      return flag;
    }

    private async Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      CancellationTokenSource cts = cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, new CancellationToken()) : new CancellationTokenSource();
      try
      {
        Task<Task> task1 = Task.WhenAny((Task) asyncWaiter, Task.Delay(millisecondsTimeout, cts.Token));
        object obj = (object) asyncWaiter;
        Task task2 = await task1.ConfigureAwait(false);
        if (obj == task2)
        {
          obj = (object) null;
          cts.Cancel();
          return true;
        }
      }
      finally
      {
        cts?.Dispose();
      }
      cts = (CancellationTokenSource) null;
      lock (this.m_lockObj)
      {
        if (this.RemoveAsyncWaiter(asyncWaiter))
        {
          cancellationToken.ThrowIfCancellationRequested();
          return false;
        }
      }
      return await asyncWaiter.ConfigureAwait(false);
    }

    /// <summary>
    ///   Освобождает объект <see cref="T:System.Threading.SemaphoreSlim" /> один раз.
    /// </summary>
    /// <returns>
    ///   Предыдущее количество в семафоре <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.Threading.SemaphoreFullException">
    ///   <see cref="T:System.Threading.SemaphoreSlim" /> Уже достигнут максимальный размер.
    /// </exception>
    [__DynamicallyInvokable]
    public int Release()
    {
      return this.Release(1);
    }

    /// <summary>
    ///   Освобождает объект <see cref="T:System.Threading.SemaphoreSlim" /> указанное число раз.
    /// </summary>
    /// <param name="releaseCount">
    ///   Количество требуемых выходов из семафора.
    /// </param>
    /// <returns>
    ///   Предыдущее количество в семафоре <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="releaseCount" /> меньше 1.
    /// </exception>
    /// <exception cref="T:System.Threading.SemaphoreFullException">
    ///   <see cref="T:System.Threading.SemaphoreSlim" /> Уже достигнут максимальный размер.
    /// </exception>
    [__DynamicallyInvokable]
    public int Release(int releaseCount)
    {
      this.CheckDispose();
      if (releaseCount < 1)
        throw new ArgumentOutOfRangeException(nameof (releaseCount), (object) releaseCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_Release_CountWrong"));
      int num1;
      lock (this.m_lockObj)
      {
        int currentCount = this.m_currentCount;
        num1 = currentCount;
        if (this.m_maxCount - currentCount < releaseCount)
          throw new SemaphoreFullException();
        int num2 = currentCount + releaseCount;
        int waitCount = this.m_waitCount;
        if (num2 == 1 || waitCount == 1)
          Monitor.Pulse(this.m_lockObj);
        else if (waitCount > 1)
          Monitor.PulseAll(this.m_lockObj);
        if (this.m_asyncHead != null)
        {
          int num3 = num2 - waitCount;
          while (num3 > 0 && this.m_asyncHead != null)
          {
            --num2;
            --num3;
            SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
            this.RemoveAsyncWaiter(asyncHead);
            SemaphoreSlim.QueueWaiterTask(asyncHead);
          }
        }
        this.m_currentCount = num2;
        if (this.m_waitHandle != null)
        {
          if (num1 == 0)
          {
            if (num2 > 0)
              this.m_waitHandle.Set();
          }
        }
      }
      return num1;
    }

    [SecuritySafeCritical]
    private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
    {
      ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) waiterTask, false);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.SemaphoreSlim" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые журналом <see cref="T:System.Threading.SemaphoreSlim" />, и при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.m_waitHandle != null)
      {
        this.m_waitHandle.Close();
        this.m_waitHandle = (ManualResetEvent) null;
      }
      this.m_lockObj = (object) null;
      this.m_asyncHead = (SemaphoreSlim.TaskNode) null;
      this.m_asyncTail = (SemaphoreSlim.TaskNode) null;
    }

    private static void CancellationTokenCanceledEventHandler(object obj)
    {
      SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
      lock (semaphoreSlim.m_lockObj)
        Monitor.PulseAll(semaphoreSlim.m_lockObj);
    }

    private void CheckDispose()
    {
      if (this.m_lockObj == null)
        throw new ObjectDisposedException((string) null, SemaphoreSlim.GetResourceString("SemaphoreSlim_Disposed"));
    }

    private static string GetResourceString(string str)
    {
      return Environment.GetResourceString(str);
    }

    private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
    {
      internal SemaphoreSlim.TaskNode Prev;
      internal SemaphoreSlim.TaskNode Next;

      internal TaskNode()
      {
      }

      [SecurityCritical]
      void IThreadPoolWorkItem.ExecuteWorkItem()
      {
        this.TrySetResult(true);
      }

      [SecurityCritical]
      void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
      {
      }
    }
  }
}
