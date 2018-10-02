// Decompiled with JetBrains decompiler
// Type: System.Threading.SpinWait
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет поддержку ожидания на основе прокруток.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct SpinWait
  {
    internal const int YIELD_THRESHOLD = 10;
    internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;
    internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;
    private int m_count;

    /// <summary>
    ///   Получает количество раз <see cref="M:System.Threading.SpinWait.SpinOnce" /> был вызван для данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Возвращает целое число, представляющее число раз <see cref="M:System.Threading.SpinWait.SpinOnce" /> был вызван для данного экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.m_count;
      }
    }

    /// <summary>
    ///   Получает ли следующий вызов <see cref="M:System.Threading.SpinWait.SpinOnce" /> приведет к получению процессора, активируя принудительного контекстного переключения.
    /// </summary>
    /// <returns>
    ///   Является ли следующий вызов <see cref="M:System.Threading.SpinWait.SpinOnce" /> даст процессора, активируя принудительного контекстного переключения.
    /// </returns>
    [__DynamicallyInvokable]
    public bool NextSpinWillYield
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_count <= 10)
          return PlatformHelper.IsSingleProcessor;
        return true;
      }
    }

    /// <summary>Выполняет один счетчик.</summary>
    [__DynamicallyInvokable]
    public void SpinOnce()
    {
      if (this.NextSpinWillYield)
      {
        CdsSyncEtwBCLProvider.Log.SpinWait_NextSpinWillYield();
        int num = this.m_count >= 10 ? this.m_count - 10 : this.m_count;
        if (num % 20 == 19)
          Thread.Sleep(1);
        else if (num % 5 == 4)
          Thread.Sleep(0);
        else
          Thread.Yield();
      }
      else
        Thread.SpinWait(4 << this.m_count);
      this.m_count = this.m_count == int.MaxValue ? 10 : this.m_count + 1;
    }

    /// <summary>Сбрасывает счетчик прокруток.</summary>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.m_count = 0;
    }

    /// <summary>
    ///   Выполняет прокрутки до удовлетворения заданного условия.
    /// </summary>
    /// <param name="condition">
    ///   Делегат для выполнения до его снова и снова возвращает значение true.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="condition" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public static void SpinUntil(Func<bool> condition)
    {
      SpinWait.SpinUntil(condition, -1);
    }

    /// <summary>
    ///   Выполняет прокрутки до удовлетворения заданного условия или истек срок действия указанного времени ожидания.
    /// </summary>
    /// <param name="condition">
    ///   Делегат для выполнения до его снова и снова возвращает значение true.
    /// </param>
    /// <param name="timeout">
    ///   Объект <see cref="T:System.TimeSpan" /> представляющий время ожидания в миллисекундах, или TimeSpan, представляющий-1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Значение true, если условие выполняется в течение времени ожидания; в противном случае — false.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="condition" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от-1, который представляет неограниченное время ожидания - или - время ожидания больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), (object) timeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
      return SpinWait.SpinUntil(condition, (int) timeout.TotalMilliseconds);
    }

    /// <summary>
    ///   Выполняет прокрутки до удовлетворения заданного условия или истек срок действия указанного времени ожидания.
    /// </summary>
    /// <param name="condition">
    ///   Делегат для выполнения до его снова и снова возвращает значение true.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Значение true, если условие выполняется в течение времени ожидания; в противном случае — false.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="condition" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), (object) millisecondsTimeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
      if (condition == null)
        throw new ArgumentNullException(nameof (condition), Environment.GetResourceString("SpinWait_SpinUntil_ArgumentNull"));
      uint num = 0;
      if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
        num = TimeoutHelper.GetTime();
      SpinWait spinWait = new SpinWait();
      while (!condition())
      {
        if (millisecondsTimeout == 0)
          return false;
        spinWait.SpinOnce();
        if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long) millisecondsTimeout <= (long) (TimeoutHelper.GetTime() - num))
          return false;
      }
      return true;
    }
  }
}
