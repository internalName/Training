// Decompiled with JetBrains decompiler
// Type: System.Threading.ManualResetEventSlim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет уменьшенную версию <see cref="T:System.Threading.ManualResetEvent" />.
  /// </summary>
  [ComVisible(false)]
  [DebuggerDisplay("Set = {IsSet}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ManualResetEventSlim : IDisposable
  {
    private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
    private const int DEFAULT_SPIN_SP = 1;
    private const int DEFAULT_SPIN_MP = 10;
    private volatile object m_lock;
    private volatile ManualResetEvent m_eventObj;
    private volatile int m_combinedState;
    private const int SignalledState_BitMask = -2147483648;
    private const int SignalledState_ShiftCount = 31;
    private const int Dispose_BitMask = 1073741824;
    private const int SpinCountState_BitMask = 1073217536;
    private const int SpinCountState_ShiftCount = 19;
    private const int SpinCountState_MaxValue = 2047;
    private const int NumWaitersState_BitMask = 524287;
    private const int NumWaitersState_ShiftCount = 0;
    private const int NumWaitersState_MaxValue = 524287;

    /// <summary>
    ///   Возвращает базовый <see cref="T:System.Threading.WaitHandle" /> для этого <see cref="T:System.Threading.ManualResetEventSlim" />.
    /// </summary>
    /// <returns>
    ///   Базовый <see cref="T:System.Threading.WaitHandle" /> объект события данного <see cref="T:System.Threading.ManualResetEventSlim" />.
    /// </returns>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        if (this.m_eventObj == null)
          this.LazyInitializeEvent();
        return (WaitHandle) this.m_eventObj;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, установлено ли событие.
    /// </summary>
    /// <returns>
    ///   Значение true, если событие установлено; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSet
    {
      [__DynamicallyInvokable] get
      {
        return (uint) ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) > 0U;
      }
      private set
      {
        this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
      }
    }

    /// <summary>
    ///   Получает число спин-блокировок до возврата к операции ожидания на основе ядра.
    /// </summary>
    /// <returns>
    ///   Возвращает число спин-блокировок до возврата к операции ожидания на основе ядра.
    /// </returns>
    [__DynamicallyInvokable]
    public int SpinCount
    {
      [__DynamicallyInvokable] get
      {
        return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
      }
      private set
      {
        this.m_combinedState = this.m_combinedState & -1073217537 | value << 19;
      }
    }

    private int Waiters
    {
      get
      {
        return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
      }
      set
      {
        if (value >= 524287)
          throw new InvalidOperationException(string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_TooManyWaiters"), (object) 524287));
        this.UpdateStateAtomically(value, 524287);
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.ManualResetEventSlim" /> класса с начальным состоянием nonsignaled.
    /// </summary>
    [__DynamicallyInvokable]
    public ManualResetEventSlim()
      : this(false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.ManualResetEventSlim" /> класса логическое значение, указывающее, следует ли для задания начального состояния сигнальным.
    /// </summary>
    /// <param name="initialState">
    ///   значение true для задания начального сигнального состояния; значение false для задания начального несигнального состояния.
    /// </param>
    [__DynamicallyInvokable]
    public ManualResetEventSlim(bool initialState)
    {
      this.Initialize(initialState, 10);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.ManualResetEventSlim" /> с логическое значение, указывающее, следует ли для задания начального состояния сигнальным и указанным числом прокруток.
    /// </summary>
    /// <param name="initialState">
    ///   Значение true для задания начального сигнального состояния; значение false для задания начального несигнального состояния.
    /// </param>
    /// <param name="spinCount">
    ///   Число ожиданий прокруток до возврата к операции ожидания на основе ядра.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="spinCount" /> — меньше 0 или больше максимально допустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public ManualResetEventSlim(bool initialState, int spinCount)
    {
      if (spinCount < 0)
        throw new ArgumentOutOfRangeException(nameof (spinCount));
      if (spinCount > 2047)
        throw new ArgumentOutOfRangeException(nameof (spinCount), string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_SpinCountOutOfRange"), (object) 2047));
      this.Initialize(initialState, spinCount);
    }

    private void Initialize(bool initialState, int spinCount)
    {
      this.m_combinedState = initialState ? int.MinValue : 0;
      this.SpinCount = PlatformHelper.IsSingleProcessor ? 1 : spinCount;
    }

    private void EnsureLockObjectCreated()
    {
      if (this.m_lock != null)
        return;
      Interlocked.CompareExchange(ref this.m_lock, new object(), (object) null);
    }

    private bool LazyInitializeEvent()
    {
      bool isSet = this.IsSet;
      ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
      if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, (ManualResetEvent) null) != null)
      {
        manualResetEvent.Close();
        return false;
      }
      if (this.IsSet != isSet)
      {
        lock (manualResetEvent)
        {
          if (this.m_eventObj == manualResetEvent)
            manualResetEvent.Set();
        }
      }
      return true;
    }

    /// <summary>
    ///   Устанавливает несигнальное состояние события, позволяя продолжить выполнение одному или нескольким потокам, ожидающим событие.
    /// </summary>
    [__DynamicallyInvokable]
    public void Set()
    {
      this.Set(false);
    }

    private void Set(bool duringCancellation)
    {
      this.IsSet = true;
      if (this.Waiters > 0)
      {
        lock (this.m_lock)
          Monitor.PulseAll(this.m_lock);
      }
      ManualResetEvent eventObj = this.m_eventObj;
      if (eventObj == null || duringCancellation)
        return;
      lock (eventObj)
      {
        if (this.m_eventObj == null)
          return;
        this.m_eventObj.Set();
      }
    }

    /// <summary>
    ///   Задает несигнальное состояние события, вызывая блокирование потоков.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.ThrowIfDisposed();
      if (this.m_eventObj != null)
        this.m_eventObj.Reset();
      this.IsSet = false;
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> имеет значение.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> сигнала, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален или <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> имеет значение, с помощью <see cref="T:System.TimeSpan" /> для измерения интервала времени.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Threading.ManualResetEventSlim" /> установлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Количество миллисекунд в <paramref name="timeout" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout));
      return this.Wait((int) totalMilliseconds, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> имеет значение, с помощью <see cref="T:System.TimeSpan" /> для измерения интервала времени, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Threading.ManualResetEventSlim" /> установлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Количество миллисекунд в <paramref name="timeout" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален или <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout));
      return this.Wait((int) totalMilliseconds, cancellationToken);
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> имеет значение, используя 32-разрядное целое число со знаком для измерения интервала времени.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Threading.ManualResetEventSlim" /> установлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до текущего <see cref="T:System.Threading.ManualResetEventSlim" /> имеет значение, используя 32-разрядное знаковое целое число для измерения интервала времени, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Threading.ManualResetEventSlim" /> установлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Превышено максимальное количество ожидающих.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален или <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.ThrowIfDisposed();
      cancellationToken.ThrowIfCancellationRequested();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout));
      if (!this.IsSet)
      {
        if (millisecondsTimeout == 0)
          return false;
        uint startTime = 0;
        bool flag = false;
        int millisecondsTimeout1 = millisecondsTimeout;
        if (millisecondsTimeout != -1)
        {
          startTime = TimeoutHelper.GetTime();
          flag = true;
        }
        int num1 = 10;
        int num2 = 5;
        int num3 = 20;
        int spinCount = this.SpinCount;
        for (int index = 0; index < spinCount; ++index)
        {
          if (this.IsSet)
            return true;
          if (index < num1)
          {
            if (index == num1 / 2)
              Thread.Yield();
            else
              Thread.SpinWait(4 << index);
          }
          else if (index % num3 == 0)
            Thread.Sleep(1);
          else if (index % num2 == 0)
            Thread.Sleep(0);
          else
            Thread.Yield();
          if (index >= 100 && index % 10 == 0)
            cancellationToken.ThrowIfCancellationRequested();
        }
        this.EnsureLockObjectCreated();
        using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, (object) this))
        {
          lock (this.m_lock)
          {
            while (!this.IsSet)
            {
              cancellationToken.ThrowIfCancellationRequested();
              if (flag)
              {
                millisecondsTimeout1 = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
                if (millisecondsTimeout1 <= 0)
                  return false;
              }
              ++this.Waiters;
              if (this.IsSet)
              {
                --this.Waiters;
                return true;
              }
              try
              {
                if (!Monitor.Wait(this.m_lock, millisecondsTimeout1))
                  return false;
              }
              finally
              {
                --this.Waiters;
              }
            }
          }
        }
      }
      return true;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.ManualResetEventSlim" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые журналом <see cref="T:System.Threading.ManualResetEventSlim" />, и при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение true, чтобы освободить управляемые и неуправляемые ресурсы; значение false, чтобы освободить только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if ((this.m_combinedState & 1073741824) != 0)
        return;
      this.m_combinedState |= 1073741824;
      if (!disposing)
        return;
      ManualResetEvent eventObj = this.m_eventObj;
      if (eventObj == null)
        return;
      lock (eventObj)
      {
        eventObj.Close();
        this.m_eventObj = (ManualResetEvent) null;
      }
    }

    private void ThrowIfDisposed()
    {
      if ((this.m_combinedState & 1073741824) != 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ManualResetEventSlim_Disposed"));
    }

    private static void CancellationTokenCallback(object obj)
    {
      ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
      lock (manualResetEventSlim.m_lock)
        Monitor.PulseAll(manualResetEventSlim.m_lock);
    }

    private void UpdateStateAtomically(int newBits, int updateBitsMask)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int combinedState = this.m_combinedState;
        if (Interlocked.CompareExchange(ref this.m_combinedState, combinedState & ~updateBitsMask | newBits, combinedState) != combinedState)
          spinWait.SpinOnce();
        else
          break;
      }
    }

    private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
    {
      return (int) ((uint) (state & mask) >> rightBitShiftCount);
    }

    private static int ExtractStatePortion(int state, int mask)
    {
      return state & mask;
    }
  }
}
