// Decompiled with JetBrains decompiler
// Type: System.Threading.Timer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет механизм для выполнения метода в потоке пула с заданными интервалами.
  ///    Этот класс не наследуется.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class Timer : MarshalByRefObject, IDisposable
  {
    private const uint MAX_SUPPORTED_TIMEOUT = 4294967294;
    private TimerHolder m_timer;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="Timer" /> класса, используя 32-разрядное знаковое целое число для указания интервала времени.
    /// </summary>
    /// <param name="callback">
    ///   A <see cref="T:System.Threading.TimerCallback" /> делегат, представляющий выполняемый метод.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий информацию, используемую методом обратного вызова, или <see langword="null" />.
    /// </param>
    /// <param name="dueTime">
    ///   Время задержки <paramref name="callback" /> вызывается в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для предотвращения запуска таймера.
    ///    Задайте значение ноль (0) для немедленного запуска таймера.
    /// </param>
    /// <param name="period">
    ///   Временной интервал между вызовами <paramref name="callback" />, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="Timer" /> класс с помощью <see cref="T:System.TimeSpan" /> значения для измерения временных интервалов.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, представляющий метод для выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий информацию, используемую методом обратного вызова, или <see langword="null" />.
    /// </param>
    /// <param name="dueTime">
    ///   Время задержки <paramref name="callback" /> вызовет свои методы.
    ///    Следует задать минус одну (-1) миллисекунду для того, чтобы не допустить запуск таймера.
    ///    Задайте значение ноль (0) для немедленного запуска таймера.
    /// </param>
    /// <param name="period">
    ///   Временной интервал между вызовами методов, на который указывает <paramref name="callback" />.
    ///    Следует задать минус одну (-1) миллисекунду для отключения периодической сигнализации.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество миллисекунд в значении <paramref name="dueTime" /> или <paramref name="period" /> отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />, или больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
    {
      long totalMilliseconds1 = (long) dueTime.TotalMilliseconds;
      if (totalMilliseconds1 < -1L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (totalMilliseconds1 > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      long totalMilliseconds2 = (long) period.TotalMilliseconds;
      if (totalMilliseconds2 < -1L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (totalMilliseconds2 > 4294967294L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) totalMilliseconds1, (uint) totalMilliseconds2, ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="Timer" /> класс с помощью 32-разрядных целых чисел без знака для измерения временных интервалов.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, представляющий метод для выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий информацию, используемую методом обратного вызова, или <see langword="null" />.
    /// </param>
    /// <param name="dueTime">
    ///   Время задержки <paramref name="callback" /> вызывается в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для предотвращения запуска таймера.
    ///    Задайте значение ноль (0) для немедленного запуска таймера.
    /// </param>
    /// <param name="period">
    ///   Временной интервал между вызовами <paramref name="callback" />, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, uint dueTime, uint period)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, dueTime, period, ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="Timer" /> класс с помощью 64-разрядных целых чисел со знаком для измерения временных интервалов.
    /// </summary>
    /// <param name="callback">
    ///   A <see cref="T:System.Threading.TimerCallback" /> делегат, представляющий выполняемый метод.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий информацию, используемую методом обратного вызова, или <see langword="null" />.
    /// </param>
    /// <param name="dueTime">
    ///   Время задержки <paramref name="callback" /> вызывается в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для предотвращения запуска таймера.
    ///    Задайте значение ноль (0) для немедленного запуска таймера.
    /// </param>
    /// <param name="period">
    ///   Временной интервал между вызовами <paramref name="callback" />, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра больше 4294967294.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Timer" /> класса с бесконечным периодом и бесконечным временем действия, используя только что созданный <see cref="T:System.Threading.Timer" /> объект как объект состояния.
    /// </summary>
    /// <param name="callback">
    ///   A <see cref="T:System.Threading.TimerCallback" /> делегат, представляющий выполняемый метод.
    /// </param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback)
    {
      int num1 = -1;
      int num2 = -1;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, (object) this, (uint) num1, (uint) num2, ref stackMark);
    }

    [SecurityCritical]
    private void TimerSetup(TimerCallback callback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
    {
      if (callback == null)
        throw new ArgumentNullException("TimerCallback");
      this.m_timer = new TimerHolder(new TimerQueueTimer(callback, state, dueTime, period, ref stackMark));
    }

    [SecurityCritical]
    internal static void Pause()
    {
      TimerQueue.Instance.Pause();
    }

    [SecurityCritical]
    internal static void Resume()
    {
      TimerQueue.Instance.Resume();
    }

    /// <summary>
    ///   Меняет время запуска и интервал между вызовами метода таймера, используя 32-разрядные знаковые целые числа для измерения временных интервалов.
    /// </summary>
    /// <param name="dueTime">
    ///   Время задержки до вызова метода ответного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> чтобы допустить повторный запуск таймера.
    ///    Задайте значение ноль (0) для немедленного перезапуска таймера.
    /// </param>
    /// <param name="period">
    ///   Указанный временной интервал между вызовами метода обратного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если таймер успешно обновлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.Timer" /> Уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Change(int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>
    ///   Изменяет время запуска и интервал межу вызовами метода таймера, используя <see cref="T:System.TimeSpan" /> значения для измерения временных интервалов.
    /// </summary>
    /// <param name="dueTime">
    ///   Объект <see cref="T:System.TimeSpan" /> представляет период времени должно пройти до вызова метода обратного вызова, заданных во <see cref="T:System.Threading.Timer" /> был создан.
    ///    Следует задать минус одну (-1) миллисекунду для того, чтобы не допустить повторный запуск таймера.
    ///    Задайте значение ноль (0) для немедленного перезапуска таймера.
    /// </param>
    /// <param name="period">
    ///   Указанный временной интервал между вызовами метода обратного вызова при <see cref="T:System.Threading.Timer" /> был создан.
    ///    Следует задать минус одну (-1) миллисекунду для отключения периодической сигнализации.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если таймер успешно обновлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.Timer" /> Уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> — параметр, в миллисекундах, меньше -1.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметр в миллисекундах больше 4294967294.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Change(TimeSpan dueTime, TimeSpan period)
    {
      return this.Change((long) dueTime.TotalMilliseconds, (long) period.TotalMilliseconds);
    }

    /// <summary>
    ///   Меняет время запуска и интервал между вызовами метода таймера, используя 32-разрядные целые числа без знака для измерения временных интервалов.
    /// </summary>
    /// <param name="dueTime">
    ///   Время задержки до вызова метода ответного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> чтобы допустить повторный запуск таймера.
    ///    Задайте значение ноль (0) для немедленного перезапуска таймера.
    /// </param>
    /// <param name="period">
    ///   Указанный временной интервал между вызовами метода обратного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если таймер успешно обновлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.Timer" /> Уже был удален.
    /// </exception>
    [CLSCompliant(false)]
    public bool Change(uint dueTime, uint period)
    {
      return this.m_timer.m_timer.Change(dueTime, period);
    }

    /// <summary>
    ///   Меняет время запуска и интервал между вызовами метода таймера, используя 64-разрядные знаковые целые числа для измерения временных интервалов.
    /// </summary>
    /// <param name="dueTime">
    ///   Время задержки до вызова метода ответного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> чтобы допустить повторный запуск таймера.
    ///    Задайте значение ноль (0) для немедленного перезапуска таймера.
    /// </param>
    /// <param name="period">
    ///   Указанный временной интервал между вызовами метода обратного вызова при <see cref="T:System.Threading.Timer" /> был создан, в миллисекундах.
    ///    Укажите <see cref="F:System.Threading.Timeout.Infinite" /> для отключения периодической сигнализации.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если таймер успешно обновлен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.Timer" /> Уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра — меньше -1.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="dueTime" /> Или <paramref name="period" /> параметра больше 4294967294.
    /// </exception>
    public bool Change(long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException(nameof (dueTime), Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException(nameof (period), Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром <see cref="T:System.Threading.Timer" /> и сигналы, когда был удален таймера.
    /// </summary>
    /// <param name="notifyObject">
    ///   <see cref="T:System.Threading.WaitHandle" /> Должен получить сигнал при <see langword="Timer" /> был удален.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если функция выполнена успешно; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="notifyObject" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool Dispose(WaitHandle notifyObject)
    {
      if (notifyObject == null)
        throw new ArgumentNullException(nameof (notifyObject));
      return this.m_timer.Close(notifyObject);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром <see cref="T:System.Threading.Timer" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.m_timer.Close();
    }

    internal void KeepRootedWhileScheduled()
    {
      GC.SuppressFinalize((object) this.m_timer);
    }
  }
}
