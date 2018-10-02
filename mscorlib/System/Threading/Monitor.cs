// Decompiled with JetBrains decompiler
// Type: System.Threading.Monitor
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
  ///   Предоставляет механизм для синхронизации доступа к объектам.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class Monitor
  {
    /// <summary>
    ///   Получает эксклюзивную блокировку указанного объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого получается блокировка монитора.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Enter(object obj);

    /// <summary>
    ///   Получает монопольную блокировку указанного объекта и единым блоком задает значение, указывающее, была ли выполнена блокировка.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <param name="lockTaken">
    ///   Результат попытки получить блокировку, переданную по ссылке.
    ///    Входное значение должно равняться <see langword="false" />.
    ///    Выходное значение <see langword="true" />, если блокировка получена; в противном случае — выходное значение <see langword="false" />.
    ///    Выходное значение задается, даже если при попытке получить блокировку возникает исключение.
    /// 
    ///   Примечание. Если исключение не возникает, выходное значение этого метода всегда <see langword="true" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Входные данные для <paramref name="lockTaken" /> — <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static void Enter(object obj, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnter(obj, ref lockTaken);
    }

    private static void ThrowLockTakenException()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeFalse"), "lockTaken");
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ReliableEnter(object obj, ref bool lockTaken);

    /// <summary>
    ///   Освобождает эксклюзивную блокировку указанного объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, блокировка которого освобождается.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Текущий поток владеет блокировкой для указанного объекта.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Exit(object obj);

    /// <summary>
    ///   Пытается получить эксклюзивную блокировку указанного объекта.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток получает блокировку; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj)
    {
      bool lockTaken = false;
      Monitor.TryEnter(obj, 0, ref lockTaken);
      return lockTaken;
    }

    /// <summary>
    ///   Пытается получить монопольную блокировку указанного объекта и единым блоком задает значение, указывающее, была ли выполнена блокировка.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <param name="lockTaken">
    ///   Результат попытки получить блокировку, переданную по ссылке.
    ///    Входное значение должно равняться <see langword="false" />.
    ///    Выходное значение <see langword="true" />, если блокировка получена; в противном случае — выходное значение <see langword="false" />.
    ///    Выходное значение задается, даже если при попытке получить блокировку возникает исключение.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Входные данные для <paramref name="lockTaken" /> — <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
    }

    /// <summary>
    ///   Пытается получить эксклюзивную блокировку указанного объекта на заданное количество миллисекунд.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд, в течение которых ожидать блокировку.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток получает блокировку; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeout" /> является отрицательным числом и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj, int millisecondsTimeout)
    {
      bool lockTaken = false;
      Monitor.TryEnter(obj, millisecondsTimeout, ref lockTaken);
      return lockTaken;
    }

    private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (int) totalMilliseconds;
    }

    /// <summary>
    ///   Пытается получить эксклюзивную блокировку указанного объекта в течение заданного количества времени.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <param name="timeout">
    ///   Класс <see cref="T:System.TimeSpan" />, представляющий количество времени, в течение которого ожидается блокировка.
    ///    Значение –1 миллисекунды обозначает бесконечное ожидание.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток получает блокировку; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="timeout" /> (в миллисекундах) является отрицательным. Оно не равно <see cref="F:System.Threading.Timeout.Infinite" /> (-1 миллисекунда) или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj, TimeSpan timeout)
    {
      return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
    }

    /// <summary>
    ///   В течение заданного количества миллисекунд пытается получить монопольную блокировку указанного объекта и единым блоком задает значение, указывающее, была ли выполнена блокировка.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд, в течение которых ожидать блокировку.
    /// </param>
    /// <param name="lockTaken">
    ///   Результат попытки получить блокировку, переданную по ссылке.
    ///    Входное значение должно равняться <see langword="false" />.
    ///    Выходное значение <see langword="true" />, если блокировка получена; в противном случае — выходное значение <see langword="false" />.
    ///    Выходное значение задается, даже если при попытке получить блокировку возникает исключение.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Входные данные для <paramref name="lockTaken" /> — <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsTimeout" /> является отрицательным числом и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
    }

    /// <summary>
    ///   В течение заданного периода времени пытается получить монопольную блокировку указанного объекта и единым блоком задает значение, указывающее, была ли выполнена блокировка.
    /// </summary>
    /// <param name="obj">Объект, блокировка которого получается.</param>
    /// <param name="timeout">
    ///   Период времени, в течение которого ожидается блокировка.
    ///    Значение –1 миллисекунды обозначает бесконечное ожидание.
    /// </param>
    /// <param name="lockTaken">
    ///   Результат попытки получить блокировку, переданную по ссылке.
    ///    Входное значение должно равняться <see langword="false" />.
    ///    Выходное значение <see langword="true" />, если блокировка получена; в противном случае — выходное значение <see langword="false" />.
    ///    Выходное значение задается, даже если при попытке получить блокировку возникает исключение.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Входные данные для <paramref name="lockTaken" /> — <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="timeout" /> (в миллисекундах) является отрицательным. Оно не равно <see cref="F:System.Threading.Timeout.Infinite" /> (-1 миллисекунда) или больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken);

    /// <summary>
    ///   Определяет, содержит ли текущий поток блокировку указанного объекта.
    /// </summary>
    /// <param name="obj">Объект для тестирования.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий поток владеет блокировкой в <paramref name="obj" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool IsEntered(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      return Monitor.IsEnteredNative(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsEnteredNative(object obj);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

    /// <summary>
    ///   Освобождает блокировку объекта и блокирует текущий поток до тех пор, пока тот не получит блокировку снова.
    ///    Если указанные временные интервалы истекают, поток встает в очередь готовности.
    ///    Этот метод также указывает на выход из области синхронизации для контекста (если она находится в синхронизированном контексте) до ожидания и ее повторное получение впоследствии.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд для ожидания постановки в очередь готовности.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если блокировка была получена заново до истечения заданного времени; значение <see langword="false" />, если блокировка была получена заново по истечении заданного времени.
    ///    Этот метод не осуществляет возврат, если блокировка не была получена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   <see langword="Wait" /> должен быть вызван из синхронизированного блока кода.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Поток, который вызывает <see langword="Wait" /> позже прерывается из состояния ожидания.
    ///    Это происходит, когда другой поток вызывает этот поток <see cref="M:System.Threading.Thread.Interrupt" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="millisecondsTimeout" /> параметра отрицательно и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [SecuritySafeCritical]
    public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
    }

    /// <summary>
    ///   Освобождает блокировку объекта и блокирует текущий поток до тех пор, пока тот не получит блокировку снова.
    ///    Если указанные временные интервалы истекают, поток встает в очередь готовности.
    ///    Дополнительно выходит из синхронизированного домена для синхронизации контекста до ожидания и получает домен впоследствии.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <param name="timeout">
    ///   Класс <see cref="T:System.TimeSpan" />, представляющий количество времени, до истечения которого поток поступает в очередь ожидания.
    /// </param>
    /// <param name="exitContext">
    ///   Значение <see langword="true" /> для выхода из домена синхронизации в текущем контексте перед ожиданием (в синхронизированном контексте) с его последующим повторным получением; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если блокировка была получена заново до истечения заданного времени; значение <see langword="false" />, если блокировка была получена заново по истечении заданного времени.
    ///    Этот метод не осуществляет возврат, если блокировка не была получена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   <see langword="Wait" /> — не вызывается из синхронизированного блока кода.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Для потока, который вызывает Wait, состояние ожидания позже прерывается.
    ///    Это происходит, когда другой поток вызывает метод <see cref="M:System.Threading.Thread.Interrupt" /> этого потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="timeout" /> имеет отрицательное значение и не представляет <see cref="F:System.Threading.Timeout.Infinite" /> (-1 миллисекунда) или больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
    {
      return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
    }

    /// <summary>
    ///   Освобождает блокировку объекта и блокирует текущий поток до тех пор, пока тот не получит блокировку снова.
    ///    Если указанные временные интервалы истекают, поток встает в очередь готовности.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд для ожидания постановки в очередь готовности.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если блокировка была получена заново до истечения заданного времени; значение <see langword="false" />, если блокировка была получена заново по истечении заданного времени.
    ///    Этот метод не осуществляет возврат, если блокировка не была получена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Вызывающий поток не владеет блокировкой для указанного объекта.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Поток, который вызывает <see langword="Wait" /> позже прерывается из состояния ожидания.
    ///    Это происходит, когда другой поток вызывает метод <see cref="M:System.Threading.Thread.Interrupt" /> этого потока.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="millisecondsTimeout" /> параметр является отрицательным и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool Wait(object obj, int millisecondsTimeout)
    {
      return Monitor.Wait(obj, millisecondsTimeout, false);
    }

    /// <summary>
    ///   Освобождает блокировку объекта и блокирует текущий поток до тех пор, пока тот не получит блокировку снова.
    ///    Если указанные временные интервалы истекают, поток встает в очередь готовности.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <param name="timeout">
    ///   Класс <see cref="T:System.TimeSpan" />, представляющий количество времени, до истечения которого поток поступает в очередь ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если блокировка была получена заново до истечения заданного времени; значение <see langword="false" />, если блокировка была получена заново по истечении заданного времени.
    ///    Этот метод не осуществляет возврат, если блокировка не была получена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Вызывающий поток не владеет блокировкой для указанного объекта.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Поток, который вызывает <see langword="Wait" /> позже прерывается из состояния ожидания.
    ///    Это происходит, когда другой поток вызывает этот поток <see cref="M:System.Threading.Thread.Interrupt" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="timeout" /> параметр в миллисекундах отрицательно и не представляет <see cref="F:System.Threading.Timeout.Infinite" /> (– 1 миллисекунды), или больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool Wait(object obj, TimeSpan timeout)
    {
      return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
    }

    /// <summary>
    ///   Освобождает блокировку объекта и блокирует текущий поток до тех пор, пока тот не получит блокировку снова.
    /// </summary>
    /// <param name="obj">Объект, в котором следует ожидать.</param>
    /// <returns>
    ///   <see langword="true" />, если вызов осуществил возврат из-за того, что вызывающий поток заново получил блокировку заданного объекта.
    ///    Этот метод не осуществляет возврат, если блокировка вновь не получена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Вызывающий поток не владеет блокировкой для указанного объекта.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Поток, который вызывает <see langword="Wait" /> позже прерывается из состояния ожидания.
    ///    Это происходит, когда другой поток вызывает этот поток <see cref="M:System.Threading.Thread.Interrupt" /> метод.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool Wait(object obj)
    {
      return Monitor.Wait(obj, -1, false);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ObjPulse(object obj);

    /// <summary>
    ///   Уведомляет поток в очереди готовности об изменении состояния объекта с блокировкой.
    /// </summary>
    /// <param name="obj">Объект, ожидаемый потоком.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Вызывающий поток не владеет блокировкой для указанного объекта.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Pulse(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      Monitor.ObjPulse(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ObjPulseAll(object obj);

    /// <summary>
    ///   Уведомляет все ожидающие потоки об изменении состояния объекта.
    /// </summary>
    /// <param name="obj">Объект, посылающий импульс.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">
    ///   Вызывающий поток не владеет блокировкой для указанного объекта.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void PulseAll(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      Monitor.ObjPulseAll(obj);
    }
  }
}
