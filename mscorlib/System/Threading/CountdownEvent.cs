// Decompiled with JetBrains decompiler
// Type: System.Threading.CountdownEvent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Представляет примитив синхронизации, которому отправляется сигнал при достижении счетчиком нуля.
  /// </summary>
  [ComVisible(false)]
  [DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class CountdownEvent : IDisposable
  {
    private int m_initialCount;
    private volatile int m_currentCount;
    private ManualResetEventSlim m_event;
    private volatile bool m_disposed;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.CountdownEvent" /> класса с указанным числом.
    /// </summary>
    /// <param name="initialCount">
    ///   Количество сигналов, изначально нужное для установки <see cref="T:System.Threading.CountdownEvent" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="initialCount" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public CountdownEvent(int initialCount)
    {
      if (initialCount < 0)
        throw new ArgumentOutOfRangeException(nameof (initialCount));
      this.m_initialCount = initialCount;
      this.m_currentCount = initialCount;
      this.m_event = new ManualResetEventSlim();
      if (initialCount != 0)
        return;
      this.m_event.Set();
    }

    /// <summary>
    ///   Получает количество сигналов, оставшееся до установки события.
    /// </summary>
    /// <returns>
    ///    Количество сигналов, оставшееся до установки события.
    /// </returns>
    [__DynamicallyInvokable]
    public int CurrentCount
    {
      [__DynamicallyInvokable] get
      {
        int currentCount = this.m_currentCount;
        if (currentCount >= 0)
          return currentCount;
        return 0;
      }
    }

    /// <summary>
    ///   Получает количество сигналов, изначально нужное для установки события.
    /// </summary>
    /// <returns>
    ///    Количество сигналов, изначально нужное для установки события.
    /// </returns>
    [__DynamicallyInvokable]
    public int InitialCount
    {
      [__DynamicallyInvokable] get
      {
        return this.m_initialCount;
      }
    }

    /// <summary>
    ///   Указывает, достиг ли текущий счетчик объекта <see cref="T:System.Threading.CountdownEvent" /> нуля.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущее значение счетчика равно нулю; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSet
    {
      [__DynamicallyInvokable] get
      {
        return this.m_currentCount <= 0;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Threading.WaitHandle" /> используемый для задания события ожидания.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.WaitHandle" /> используемый для задания события ожидания.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        return this.m_event.WaitHandle;
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.CountdownEvent" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые журналом <see cref="T:System.Threading.CountdownEvent" />, и при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение true, чтобы освободить управляемые и неуправляемые ресурсы; значение false, чтобы освободить только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.m_event.Dispose();
      this.m_disposed = true;
    }

    /// <summary>
    ///   Регистрирует сигнала с <see cref="T:System.Threading.CountdownEvent" />, уменьшение значения из <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.
    /// </summary>
    /// <returns>
    ///   задано значение true, если передача сигнала подсчет стал равен нулю и события; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр уже задан.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Signal()
    {
      this.ThrowIfDisposed();
      if (this.m_currentCount <= 0)
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
      int num = Interlocked.Decrement(ref this.m_currentCount);
      if (num == 0)
      {
        this.m_event.Set();
        return true;
      }
      if (num < 0)
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
      return false;
    }

    /// <summary>
    ///   Регистрирует несколько сигналов с <see cref="T:System.Threading.CountdownEvent" />, уменьшение значения из <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> на указанную величину.
    /// </summary>
    /// <param name="signalCount">
    ///   Количество сигналов для регистрации.
    /// </param>
    /// <returns>
    ///   задано значение true, если после сигналов подсчет стал равен нулю и события; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="signalCount" /> меньше 1.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр уже задан.
    ///    - или - или <paramref name="signalCount" /> больше, чем <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Signal(int signalCount)
    {
      if (signalCount <= 0)
        throw new ArgumentOutOfRangeException(nameof (signalCount));
      this.ThrowIfDisposed();
      SpinWait spinWait = new SpinWait();
      int currentCount;
      while (true)
      {
        currentCount = this.m_currentCount;
        if (currentCount >= signalCount)
        {
          if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount - signalCount, currentCount) != currentCount)
            spinWait.SpinOnce();
          else
            goto label_7;
        }
        else
          break;
      }
      throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
label_7:
      if (currentCount != signalCount)
        return false;
      this.m_event.Set();
      return true;
    }

    /// <summary>
    ///   Увеличивает <see cref="T:System.Threading.CountdownEvent" />в текущий счетчик на единицу.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр уже задан.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> больше или равно значению свойства <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void AddCount()
    {
      this.AddCount(1);
    }

    /// <summary>
    ///   Предпринимается попытка увеличить <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> на единицу.
    /// </summary>
    /// <returns>
    ///   значение true, если увеличение выполнено успешно; в противном случае — значение false.
    ///    Если <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> уже равен нулю, этот метод вернет значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> равно <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryAddCount()
    {
      return this.TryAddCount(1);
    }

    /// <summary>
    ///   С шагом <see cref="T:System.Threading.CountdownEvent" />Текущее число на указанное значение.
    /// </summary>
    /// <param name="signalCount">
    ///   Значение, на которое увеличивается <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="signalCount" /> меньше или равно 0.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр уже задан.
    /// 
    ///   -или-
    /// 
    ///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> больше или равен <see cref="F:System.Int32.MaxValue" /> после увеличивается число <paramref name="signalCount." />
    /// </exception>
    [__DynamicallyInvokable]
    public void AddCount(int signalCount)
    {
      if (!this.TryAddCount(signalCount))
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyZero"));
    }

    /// <summary>
    ///   Предпринимается попытка увеличить <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> на указанное значение.
    /// </summary>
    /// <param name="signalCount">
    ///   Значение, на которое увеличивается <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.
    /// </param>
    /// <returns>
    ///   значение true, если увеличение выполнено успешно; в противном случае — значение false.
    ///    Если <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> уже равен нулю, то возвращается false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="signalCount" /> меньше или равно 0.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий экземпляр уже задан.
    /// 
    ///   -или-
    /// 
    ///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> больше или равен <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryAddCount(int signalCount)
    {
      if (signalCount <= 0)
        throw new ArgumentOutOfRangeException(nameof (signalCount));
      this.ThrowIfDisposed();
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int currentCount = this.m_currentCount;
        if (currentCount > 0)
        {
          if (currentCount <= int.MaxValue - signalCount)
          {
            if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount + signalCount, currentCount) != currentCount)
              spinWait.SpinOnce();
            else
              goto label_9;
          }
          else
            goto label_6;
        }
        else
          break;
      }
      return false;
label_6:
      throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyMax"));
label_9:
      return true;
    }

    /// <summary>
    ///   Сбрасывает <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> значение <see cref="P:System.Threading.CountdownEvent.InitialCount" />.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален...
    /// </exception>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.Reset(this.m_initialCount);
    }

    /// <summary>
    ///   Сбрасывает <see cref="P:System.Threading.CountdownEvent.InitialCount" /> свойство с указанным значением.
    /// </summary>
    /// <param name="count">
    ///   Количество сигналов до установки <see cref="T:System.Threading.CountdownEvent" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр имеет уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public void Reset(int count)
    {
      this.ThrowIfDisposed();
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count));
      this.m_currentCount = count;
      this.m_initialCount = count;
      if (count == 0)
        this.m_event.Set();
      else
        this.m_event.Reset();
    }

    /// <summary>
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> имеет значение.
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
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> задано, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    ///    - или - <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> имеет значение, с помощью <see cref="T:System.TimeSpan" /> для измерения времени ожидания.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   значение true, если <see cref="T:System.Threading.CountdownEvent" /> было задано; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" />.
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
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> имеет значение, с помощью <see cref="T:System.TimeSpan" /> для измерения времени ожидания, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   значение true, если <see cref="T:System.Threading.CountdownEvent" /> было задано; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    ///    - или - <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" />.
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
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> имеет значение, используя 32-разрядное знаковое целое число для измерения времени ожидания.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   значение true, если <see cref="T:System.Threading.CountdownEvent" /> было задано; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Блокирует текущий поток до <see cref="T:System.Threading.CountdownEvent" /> имеет значение, используя 32-разрядное знаковое целое число для измерения времени ожидания, контролируя <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены <see cref="T:System.Threading.CancellationToken" />, который следует контролировать.
    /// </param>
    /// <returns>
    ///   значение true, если <see cref="T:System.Threading.CountdownEvent" /> было задано; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   <paramref name="cancellationToken" /> была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже был удален.
    ///    - или - <see cref="T:System.Threading.CancellationTokenSource" /> создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout));
      this.ThrowIfDisposed();
      cancellationToken.ThrowIfCancellationRequested();
      bool flag = this.IsSet;
      if (!flag)
        flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
      return flag;
    }

    private void ThrowIfDisposed()
    {
      if (this.m_disposed)
        throw new ObjectDisposedException(nameof (CountdownEvent));
    }
  }
}
