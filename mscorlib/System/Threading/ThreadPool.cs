// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPool
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет пул потоков, который можно использовать для выполнения задач, отправки рабочих элементов, обработки асинхронного ввода-вывода, ожидания от имени других потоков и обработки таймеров.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class ThreadPool
  {
    /// <summary>
    ///   Задает количество запросов к пулу потоков, которые могут быть активными одновременно.
    ///    Все запросы, превышающие это количество, остаются в очереди до тех пор, пока потоки пула не станут доступны.
    /// </summary>
    /// <param name="workerThreads">
    ///   Максимальное количество рабочих потоков в пуле потоков.
    /// </param>
    /// <param name="completionPortThreads">
    ///   Максимальное количество потоков асинхронного ввода-вывода в пуле потоков.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если изменение выполнено успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
    {
      return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
    }

    /// <summary>
    ///   Возвращает количество запросов к пулу потоков, которые могут быть активными одновременно.
    ///    Все запросы, превышающие это количество, остаются в очереди до тех пор, пока потоки пула не станут доступны.
    /// </summary>
    /// <param name="workerThreads">
    ///   Максимальное количество рабочих потоков в пуле потоков.
    /// </param>
    /// <param name="completionPortThreads">
    ///   Максимальное количество потоков асинхронного ввода-вывода в пуле потоков.
    /// </param>
    [SecuritySafeCritical]
    public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>
    ///   Задает минимальное число потоков, создаваемых пулом потоков по требованию по мере поступления новых запросов перед переходом на алгоритм управления созданием и уничтожением потоков.
    /// </summary>
    /// <param name="workerThreads">
    ///   Минимальное количество рабочих потоков, которые создаются пулом потоков по требованию.
    /// </param>
    /// <param name="completionPortThreads">
    ///   Минимальное количество потоков асинхронного ввода-вывода, которые создаются пулом потоков по требованию.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если изменение выполнено успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static bool SetMinThreads(int workerThreads, int completionPortThreads)
    {
      return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
    }

    /// <summary>
    ///   Возвращает минимальное число потоков, создаваемых пулом потоков по требованию по мере поступления новых запросов перед переходом на алгоритм управления созданием и уничтожением потоков.
    /// </summary>
    /// <param name="workerThreads">
    ///   При возвращении метода содержит минимальное количество рабочих потоков, которые создаются пулом потоков по требованию.
    /// </param>
    /// <param name="completionPortThreads">
    ///   При возвращении метода содержит минимальное количество потоков асинхронного ввода-вывода, которые создаются пулом потоков по требованию.
    /// </param>
    [SecuritySafeCritical]
    public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>
    ///   Возвращает разницу между максимальным числом потоков пула, возвращаемых методом <see cref="M:System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)" />, и числом активных в данный момент потоков.
    /// </summary>
    /// <param name="workerThreads">
    ///   Количество доступных рабочих потоков.
    /// </param>
    /// <param name="completionPortThreads">
    ///   Количество доступных потоков асинхронного ввода-вывода.
    /// </param>
    [SecuritySafeCritical]
    public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 32-разрядного целого числа без знака.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitOrTimerCallback" />, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, который можно использовать для отмены зарегистрированной операции ожидания.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeOutInterval" /> Параметр — меньше -1.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 32-разрядного целого числа без знака.
    ///    Этот метод не распространяет вызывающий стек на рабочий поток.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, который можно использовать для отмены зарегистрированной операции ожидания.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    [SecurityCritical]
    private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
    {
      if (RemotingServices.IsTransparentProxy((object) waitObject))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
      RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle();
      if (callBack == null)
        throw new ArgumentNullException("WaitOrTimerCallback");
      state = (object) new _ThreadPoolWaitOrTimerCallback(callBack, state, compressStack, ref stackMark);
      registeredWaitHandle.SetWaitObject(waitObject);
      IntPtr handle = ThreadPool.RegisterWaitForSingleObjectNative(waitObject, state, millisecondsTimeOutInterval, executeOnlyOnce, registeredWaitHandle, ref stackMark, compressStack);
      registeredWaitHandle.SetHandle(handle);
      return registeredWaitHandle;
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 32-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitOrTimerCallback" />, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, инкапсулирующий собственный дескриптор.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeOutInterval" /> Параметр — меньше -1.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeOutInterval), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 32-разрядного целого числа со знаком.
    ///    Этот метод не распространяет вызывающий стек на рабочий поток.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, который можно использовать для отмены зарегистрированной операции ожидания.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeOutInterval" /> Параметр — меньше -1.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeOutInterval), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 64-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitOrTimerCallback" />, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, инкапсулирующий собственный дескриптор.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeOutInterval" /> Параметр — меньше -1.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1L)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeOutInterval), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая время ожидания в миллисекундах в виде 64-разрядного целого числа со знаком.
    ///    Этот метод не распространяет вызывающий стек на рабочий поток.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="millisecondsTimeOutInterval">
    ///   Время ожидания в миллисекундах.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="millisecondsTimeOutInterval" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, который можно использовать для отмены зарегистрированной операции ожидания.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeOutInterval" /> Параметр — меньше -1.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1L)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeOutInterval), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая значение <see cref="T:System.TimeSpan" /> для времени ожидания.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitOrTimerCallback" />, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="timeout">
    ///   Время ожидания, представленное объектом <see cref="T:System.TimeSpan" />.
    ///    Если параметр <paramref name="timeout" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="timeout" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, инкапсулирующий собственный дескриптор.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Параметр — меньше -1.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="timeout" /> Параметра больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) totalMilliseconds, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>
    ///   Регистрирует делегат для ожидания объекта <see cref="T:System.Threading.WaitHandle" />, задавая значение <see cref="T:System.TimeSpan" /> для времени ожидания.
    ///    Этот метод не распространяет вызывающий стек на рабочий поток.
    /// </summary>
    /// <param name="waitObject">
    ///   Регистрируемый объект <see cref="T:System.Threading.WaitHandle" />.
    ///    Используйте объект <see cref="T:System.Threading.WaitHandle" />, отличный от <see cref="T:System.Threading.Mutex" />.
    /// </param>
    /// <param name="callBack">
    ///   Делегат, который вызывается при получении сигнала объектом, указанным в параметре <paramref name="waitObject" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <param name="timeout">
    ///   Время ожидания, представленное объектом <see cref="T:System.TimeSpan" />.
    ///    Если параметр <paramref name="timeout" /> равен 0 (нулю), функция проверяет состояние объекта и немедленно возвращает значение.
    ///    Если параметр <paramref name="timeout" /> равен -1, время ожидания функции никогда не истекает.
    /// </param>
    /// <param name="executeOnlyOnce">
    ///   Значение <see langword="true" /> указывает, что после вызова делегата поток не будет ожидать параметр <paramref name="waitObject" />; значение <see langword="false" /> указывает, что таймер сбрасывается всякий раз по завершении операции ожидания до тех пор, пока регистрация ожидания не будет отменена.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.RegisteredWaitHandle" />, который можно использовать для отмены зарегистрированной операции ожидания.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> Параметр — меньше -1.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="timeout" /> Параметра больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) totalMilliseconds, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>
    ///   Помещает метод в очередь на выполнение и указывает объект, содержащий данные для использования методом.
    ///    Метод выполняется, когда становится доступен поток из пула потоков.
    /// </summary>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitCallback" />, представляющий выполняемый метод.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, используемые методом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если метод успешно помещен в очередь. Если рабочий элемент не может быть помещен в очередь, создается исключение <see cref="T:System.NotSupportedException" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Размещается общеязыковой среды выполнения (CLR), и узел не поддерживает это действие.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callBack" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool QueueUserWorkItem(WaitCallback callBack, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackMark, true);
    }

    /// <summary>
    ///   Помещает метод в очередь на выполнение.
    ///    Метод выполняется, когда становится доступен поток из пула потоков.
    /// </summary>
    /// <param name="callBack">
    ///   Делегат <see cref="T:System.Threading.WaitCallback" />, представляющий метод, который требуется выполнить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если метод успешно помещен в очередь. Если рабочий элемент не может быть помещен в очередь, создается исключение <see cref="T:System.NotSupportedException" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callBack" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Размещается общеязыковой среды выполнения (CLR), и узел не поддерживает это действие.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool QueueUserWorkItem(WaitCallback callBack)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, (object) null, ref stackMark, true);
    }

    /// <summary>
    ///   Помещает указанный делегат в очередь пула потоков, но не распространяет вызывающий стек на рабочий поток.
    /// </summary>
    /// <param name="callBack">
    ///   Метод <see cref="T:System.Threading.WaitCallback" />, представляющий делегат, который вызывается, когда потоку в пуле потоков назначается рабочий элемент.
    /// </param>
    /// <param name="state">
    ///   Объект, передаваемый делегату при вызове его из пула потоков.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если метод завершается успешно. Если рабочий элемент не удалось поместить в очередь, создается исключение <see cref="T:System.OutOfMemoryException" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ApplicationException">
    ///   Возникла ситуация нехватки памяти.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не может быть назначен рабочий элемент.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callBack" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackMark, false);
    }

    [SecurityCritical]
    private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack)
    {
      bool flag = true;
      if (callBack == null)
        throw new ArgumentNullException("WaitCallback");
      ThreadPool.EnsureVMInitialized();
      try
      {
      }
      finally
      {
        QueueUserWorkItemCallback workItemCallback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
        ThreadPoolGlobals.workQueue.Enqueue((IThreadPoolWorkItem) workItemCallback, true);
        flag = true;
      }
      return flag;
    }

    [SecurityCritical]
    internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
    {
      ThreadPool.EnsureVMInitialized();
      try
      {
      }
      finally
      {
        ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
      }
    }

    [SecurityCritical]
    internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
    {
      if (!ThreadPoolGlobals.vmTpInitialized)
        return false;
      return ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
    }

    internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
    {
      if (wsQueues != null)
      {
        ThreadPoolWorkQueue.WorkStealingQueue[] workStealingQueueArray = wsQueues;
        for (int index = 0; index < workStealingQueueArray.Length; ++index)
        {
          ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = workStealingQueueArray[index];
          if (workStealingQueue != null && workStealingQueue.m_array != null)
          {
            IThreadPoolWorkItem[] items = workStealingQueue.m_array;
            for (int i = 0; i < items.Length; ++i)
            {
              IThreadPoolWorkItem threadPoolWorkItem = items[i];
              if (threadPoolWorkItem != null)
                yield return threadPoolWorkItem;
            }
            items = (IThreadPoolWorkItem[]) null;
          }
        }
        workStealingQueueArray = (ThreadPoolWorkQueue.WorkStealingQueue[]) null;
      }
      if (globalQueueTail != null)
      {
        ThreadPoolWorkQueue.QueueSegment segment;
        for (segment = globalQueueTail; segment != null; segment = segment.Next)
        {
          IThreadPoolWorkItem[] items = segment.nodes;
          for (int i = 0; i < items.Length; ++i)
          {
            IThreadPoolWorkItem threadPoolWorkItem = items[i];
            if (threadPoolWorkItem != null)
              yield return threadPoolWorkItem;
          }
          items = (IThreadPoolWorkItem[]) null;
        }
        segment = (ThreadPoolWorkQueue.QueueSegment) null;
      }
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[1]
      {
        ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue
      }, (ThreadPoolWorkQueue.QueueSegment) null);
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems((ThreadPoolWorkQueue.WorkStealingQueue[]) null, ThreadPoolGlobals.workQueue.queueTail);
    }

    private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
    {
      int length = 0;
      foreach (IThreadPoolWorkItem workitem in workitems)
        ++length;
      object[] objArray = new object[length];
      int index = 0;
      foreach (IThreadPoolWorkItem workitem in workitems)
      {
        if (index < objArray.Length)
          objArray[index] = (object) workitem;
        ++index;
      }
      return objArray;
    }

    [SecurityCritical]
    internal static object[] GetQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
    }

    [SecurityCritical]
    internal static object[] GetGloballyQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
    }

    [SecurityCritical]
    internal static object[] GetLocallyQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool RequestWorkerThread();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

    /// <summary>
    ///   Помещает в очередь на выполнение операцию перекрывающегося ввода-вывода.
    /// </summary>
    /// <param name="overlapped">
    ///   Помещаемая в очередь структура <see cref="T:System.Threading.NativeOverlapped" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если операция была успешна помещена в очередь порта завершения ввода-вывода; в противном случае значение <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
    {
      return ThreadPool.PostQueuedCompletionStatus(overlapped);
    }

    [SecurityCritical]
    private static void EnsureVMInitialized()
    {
      if (ThreadPoolGlobals.vmTpInitialized)
        return;
      ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
      ThreadPoolGlobals.vmTpInitialized = true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool NotifyWorkItemComplete();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ReportThreadStatus(bool isWorking);

    [SecuritySafeCritical]
    internal static void NotifyWorkItemProgress()
    {
      if (!ThreadPoolGlobals.vmTpInitialized)
        ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
      ThreadPool.NotifyWorkItemProgressNative();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void NotifyWorkItemProgressNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsThreadPoolHosted();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InitializeVMTp(ref bool enableWorkerTracking);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr RegisterWaitForSingleObjectNative(WaitHandle waitHandle, object state, uint timeOutInterval, bool executeOnlyOnce, RegisteredWaitHandle registeredWaitHandle, ref StackCrawlMark stackMark, bool compressStack);

    /// <summary>
    ///   Связывает дескриптор операционной системы с объектом <see cref="T:System.Threading.ThreadPool" />.
    /// </summary>
    /// <param name="osHandle">
    ///   Структура <see cref="T:System.IntPtr" />, хранящая дескриптор.
    ///    Дескриптор должен быть открыт для перекрывающегося ввода-вывода в неуправляемой области.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор является связанным; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static bool BindHandle(IntPtr osHandle)
    {
      return ThreadPool.BindIOCompletionCallbackNative(osHandle);
    }

    /// <summary>
    ///   Связывает дескриптор операционной системы с объектом <see cref="T:System.Threading.ThreadPool" />.
    /// </summary>
    /// <param name="osHandle">
    ///   Объект <see cref="T:System.Runtime.InteropServices.SafeHandle" />, содержащий дескриптор операционной системы.
    ///    Дескриптор должен быть открыт для перекрывающегося ввода-вывода в неуправляемой области.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор является связанным; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="osHandle" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static bool BindHandle(SafeHandle osHandle)
    {
      if (osHandle == null)
        throw new ArgumentNullException(nameof (osHandle));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        osHandle.DangerousAddRef(ref success);
        return ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
      }
      finally
      {
        if (success)
          osHandle.DangerousRelease();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool BindIOCompletionCallbackNative(IntPtr fileHandle);
  }
}
