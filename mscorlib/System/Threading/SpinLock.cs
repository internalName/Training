// Decompiled with JetBrains decompiler
// Type: System.Threading.SpinLock
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет примитив взаимно исключающей блокировки, в котором поток, пытающийся получить блокировку, ожидает в состоянии цикла, проверяя доступность блокировки.
  /// </summary>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (SpinLock.SystemThreading_SpinLockDebugView))]
  [DebuggerDisplay("IsHeld = {IsHeld}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct SpinLock
  {
    private static int MAXIMUM_WAITERS = 2147483646;
    private volatile int m_owner;
    private const int SPINNING_FACTOR = 100;
    private const int SLEEP_ONE_FREQUENCY = 40;
    private const int SLEEP_ZERO_FREQUENCY = 10;
    private const int TIMEOUT_CHECK_FREQUENCY = 10;
    private const int LOCK_ID_DISABLE_MASK = -2147483648;
    private const int LOCK_ANONYMOUS_OWNED = 1;
    private const int WAITERS_MASK = 2147483646;
    private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;
    private const int LOCK_UNOWNED = 0;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.SpinLock" /> структуры с параметром для отслеживания идентификаторов потоков для повышения качества отладки.
    /// </summary>
    /// <param name="enableThreadOwnerTracking">
    ///   Следует ли перенаправлять и использовать идентификаторы потоков для отладки.
    /// </param>
    [__DynamicallyInvokable]
    public SpinLock(bool enableThreadOwnerTracking)
    {
      this.m_owner = 0;
      if (enableThreadOwnerTracking)
        return;
      this.m_owner |= int.MinValue;
    }

    /// <summary>
    ///   Получает блокировку надежным способом, таким образом, что даже при возникновении исключения в вызове метода <paramref name="lockTaken" /> может проверяться надежно определить, была ли блокировка получена.
    /// </summary>
    /// <param name="lockTaken">
    ///   Значение true, если блокировка получена; в противном случае — значение false.
    ///   <paramref name="lockTaken" /> должен быть инициализирован перед вызовом этого метода.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="lockTaken" /> Аргумента необходимо инициализировать перед вызовом ввод.
    /// </exception>
    /// <exception cref="T:System.Threading.LockRecursionException">
    ///   Включено отслеживание владения потоков и текущий поток уже получил эту блокировку.
    /// </exception>
    [__DynamicallyInvokable]
    public void Enter(ref bool lockTaken)
    {
      Thread.BeginCriticalRegion();
      int owner = this.m_owner;
      if (!lockTaken && (owner & -2147483647) == int.MinValue && Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) == owner)
        return;
      this.ContinueTryEnter(-1, ref lockTaken);
    }

    /// <summary>
    ///   Пытается получить блокировку надежным способом, таким образом, что даже при возникновении исключения в вызове метода <paramref name="lockTaken" /> может проверяться надежно определить, была ли блокировка получена.
    /// </summary>
    /// <param name="lockTaken">
    ///   Значение true, если блокировка получена; в противном случае — значение false.
    ///   <paramref name="lockTaken" /> должен быть инициализирован перед вызовом этого метода.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="lockTaken" /> Аргумента необходимо инициализировать перед вызовом TryEnter.
    /// </exception>
    /// <exception cref="T:System.Threading.LockRecursionException">
    ///   Включено отслеживание владения потоков и текущий поток уже получил эту блокировку.
    /// </exception>
    [__DynamicallyInvokable]
    public void TryEnter(ref bool lockTaken)
    {
      this.TryEnter(0, ref lockTaken);
    }

    /// <summary>
    ///   Пытается получить блокировку надежным способом, таким образом, что даже при возникновении исключения в вызове метода <paramref name="lockTaken" /> может проверяться надежно определить, была ли блокировка получена.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="lockTaken">
    ///   Значение true, если блокировка получена; в противном случае — значение false.
    ///   <paramref name="lockTaken" /> должен быть инициализирован перед вызовом этого метода.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" /> миллисекунд.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="lockTaken" /> Аргумента необходимо инициализировать перед вызовом TryEnter.
    /// </exception>
    /// <exception cref="T:System.Threading.LockRecursionException">
    ///   Включено отслеживание владения потоков и текущий поток уже получил эту блокировку.
    /// </exception>
    [__DynamicallyInvokable]
    public void TryEnter(TimeSpan timeout, ref bool lockTaken)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), (object) timeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
      this.TryEnter((int) timeout.TotalMilliseconds, ref lockTaken);
    }

    /// <summary>
    ///   Пытается получить блокировку надежным способом, таким образом, что даже при возникновении исключения в вызове метода <paramref name="lockTaken" /> может проверяться надежно определить, была ли блокировка получена.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="lockTaken">
    ///   Значение true, если блокировка получена; в противном случае — значение false.
    ///   <paramref name="lockTaken" /> должен быть инициализирован перед вызовом этого метода.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="lockTaken" /> Аргумента необходимо инициализировать перед вызовом TryEnter.
    /// </exception>
    /// <exception cref="T:System.Threading.LockRecursionException">
    ///   Включено отслеживание владения потоков и текущий поток уже получил эту блокировку.
    /// </exception>
    [__DynamicallyInvokable]
    public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
    {
      Thread.BeginCriticalRegion();
      int owner = this.m_owner;
      if (!(millisecondsTimeout < -1 | lockTaken) && (owner & -2147483647) == int.MinValue && Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) == owner)
        return;
      this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
    }

    private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
    {
      Thread.EndCriticalRegion();
      if (lockTaken)
      {
        lockTaken = false;
        throw new ArgumentException(Environment.GetResourceString("SpinLock_TryReliableEnter_ArgumentException"));
      }
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), (object) millisecondsTimeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
      uint startTime = 0;
      if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
        startTime = TimeoutHelper.GetTime();
      if (CdsSyncEtwBCLProvider.Log.IsEnabled())
        CdsSyncEtwBCLProvider.Log.SpinLock_FastPathFailed(this.m_owner);
      if (this.IsThreadOwnerTrackingEnabled)
      {
        this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, startTime, ref lockTaken);
      }
      else
      {
        int num1 = int.MaxValue;
        int owner1 = this.m_owner;
        if ((owner1 & 1) == 0)
        {
          Thread.BeginCriticalRegion();
          if (Interlocked.CompareExchange(ref this.m_owner, owner1 | 1, owner1, ref lockTaken) == owner1)
            return;
          Thread.EndCriticalRegion();
        }
        else if ((owner1 & 2147483646) != SpinLock.MAXIMUM_WAITERS)
          num1 = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
        switch (millisecondsTimeout)
        {
          case -1:
            int processorCount = PlatformHelper.ProcessorCount;
            if (num1 < processorCount)
            {
              int num2 = 1;
              for (int index = 1; index <= num1 * 100; ++index)
              {
                Thread.SpinWait((num1 + index) * 100 * num2);
                if (num2 < processorCount)
                  ++num2;
                int owner2 = this.m_owner;
                if ((owner2 & 1) == 0)
                {
                  Thread.BeginCriticalRegion();
                  if (Interlocked.CompareExchange(ref this.m_owner, (owner2 & 2147483646) == 0 ? owner2 | 1 : owner2 - 2 | 1, owner2, ref lockTaken) == owner2)
                    return;
                  Thread.EndCriticalRegion();
                }
              }
            }
            if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
            {
              this.DecrementWaiters();
              break;
            }
            int num3 = 0;
            while (true)
            {
              int owner2 = this.m_owner;
              if ((owner2 & 1) == 0)
              {
                Thread.BeginCriticalRegion();
                if (Interlocked.CompareExchange(ref this.m_owner, (owner2 & 2147483646) == 0 ? owner2 | 1 : owner2 - 2 | 1, owner2, ref lockTaken) != owner2)
                  Thread.EndCriticalRegion();
                else
                  break;
              }
              if (num3 % 40 == 0)
                Thread.Sleep(1);
              else if (num3 % 10 == 0)
                Thread.Sleep(0);
              else
                Thread.Yield();
              if (num3 % 10 != 0 || millisecondsTimeout == -1 || TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) > 0)
                ++num3;
              else
                goto label_41;
            }
            break;
label_41:
            this.DecrementWaiters();
            break;
          case 0:
            this.DecrementWaiters();
            break;
          default:
            if (TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) > 0)
              goto case -1;
            else
              goto case 0;
        }
      }
    }

    private void DecrementWaiters()
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int owner = this.m_owner;
        if ((owner & 2147483646) != 0 && Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) != owner)
          spinWait.SpinOnce();
        else
          break;
      }
    }

    private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
    {
      int comparand = 0;
      int managedThreadId = Thread.CurrentThread.ManagedThreadId;
      if (this.m_owner == managedThreadId)
        throw new LockRecursionException(Environment.GetResourceString("SpinLock_TryEnter_LockRecursionException"));
      SpinWait spinWait = new SpinWait();
      do
      {
        spinWait.SpinOnce();
        if (this.m_owner == comparand)
        {
          Thread.BeginCriticalRegion();
          if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, comparand, ref lockTaken) == comparand)
            return;
          Thread.EndCriticalRegion();
        }
        switch (millisecondsTimeout)
        {
          case -1:
            continue;
          case 0:
            goto label_5;
          default:
            continue;
        }
      }
      while (!spinWait.NextSpinWillYield || TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) > 0);
      goto label_10;
label_5:
      return;
label_10:;
    }

    /// <summary>Снимает блокировку.</summary>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Включено отслеживание владения потоков и текущий поток не является владельцем этой блокировки.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Exit()
    {
      if ((this.m_owner & int.MinValue) == 0)
        this.ExitSlowPath(true);
      else
        Interlocked.Decrement(ref this.m_owner);
      Thread.EndCriticalRegion();
    }

    /// <summary>Снимает блокировку.</summary>
    /// <param name="useMemoryBarrier">
    ///   Логическое значение, указывающее, должен ли быть выдан барьер памяти Чтобы немедленно опубликовать операцию выхода для других потоков.
    /// </param>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Включено отслеживание владения потоков и текущий поток не является владельцем этой блокировки.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Exit(bool useMemoryBarrier)
    {
      if ((this.m_owner & int.MinValue) != 0 && !useMemoryBarrier)
        this.m_owner &= -2;
      else
        this.ExitSlowPath(useMemoryBarrier);
      Thread.EndCriticalRegion();
    }

    private void ExitSlowPath(bool useMemoryBarrier)
    {
      bool flag = (this.m_owner & int.MinValue) == 0;
      if (flag && !this.IsHeldByCurrentThread)
        throw new SynchronizationLockException(Environment.GetResourceString("SpinLock_Exit_SynchronizationLockException"));
      if (useMemoryBarrier)
      {
        if (flag)
          Interlocked.Exchange(ref this.m_owner, 0);
        else
          Interlocked.Decrement(ref this.m_owner);
      }
      else if (flag)
        this.m_owner = 0;
      else
        this.m_owner &= -2;
    }

    /// <summary>
    ///   Получает значение, указывающее любым потоком в настоящее время удерживается блокировка.
    /// </summary>
    /// <returns>
    ///   значение true, если блокировка удерживается в настоящее время любым потоком; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsHeld
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (this.IsThreadOwnerTrackingEnabled)
          return (uint) this.m_owner > 0U;
        return (uint) (this.m_owner & 1) > 0U;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее блокировка удерживается текущим потоком.
    /// </summary>
    /// <returns>
    ///   значение true, если блокировка удерживается текущим потоком; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Отслеживание владения потоков отключено.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsHeldByCurrentThread
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (!this.IsThreadOwnerTrackingEnabled)
          throw new InvalidOperationException(Environment.GetResourceString("SpinLock_IsHeldByCurrentThread"));
        return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее включено отслеживание владения потоков для данного экземпляра.
    /// </summary>
    /// <returns>
    ///   значение true, если включено отслеживание владения потоков для данного экземпляра; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsThreadOwnerTrackingEnabled
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (this.m_owner & int.MinValue) == 0;
      }
    }

    internal class SystemThreading_SpinLockDebugView
    {
      private SpinLock m_spinLock;

      public SystemThreading_SpinLockDebugView(SpinLock spinLock)
      {
        this.m_spinLock = spinLock;
      }

      public bool? IsHeldByCurrentThread
      {
        get
        {
          try
          {
            return new bool?(this.m_spinLock.IsHeldByCurrentThread);
          }
          catch (InvalidOperationException ex)
          {
            return new bool?();
          }
        }
      }

      public int? OwnerThreadID
      {
        get
        {
          if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
            return new int?(this.m_spinLock.m_owner);
          return new int?();
        }
      }

      public bool IsHeld
      {
        get
        {
          return this.m_spinLock.IsHeld;
        }
      }
    }
  }
}
