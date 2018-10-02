// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskFactory`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Предоставляет поддержку создания и планирования объектов <see cref="T:System.Threading.Tasks.Task`1" />.
  /// </summary>
  /// <typeparam name="TResult">
  ///   Возвращаемое значение объектов <see cref="T:System.Threading.Tasks.Task`1" />, созданных методами этого класса.
  /// </typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskFactory<TResult>
  {
    private CancellationToken m_defaultCancellationToken;
    private TaskScheduler m_defaultScheduler;
    private TaskCreationOptions m_defaultCreationOptions;
    private TaskContinuationOptions m_defaultContinuationOptions;

    private TaskScheduler DefaultScheduler
    {
      get
      {
        if (this.m_defaultScheduler == null)
          return TaskScheduler.Current;
        return this.m_defaultScheduler;
      }
    }

    private TaskScheduler GetDefaultScheduler(Task currTask)
    {
      if (this.m_defaultScheduler != null)
        return this.m_defaultScheduler;
      if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
        return currTask.ExecutingTaskScheduler;
      return TaskScheduler.Default;
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory`1" /> с конфигурацией по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public TaskFactory()
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory`1" /> с конфигурацией по умолчанию.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен отмены по умолчанию, который будет назначен задачам, созданным данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory" />, если при вызове методов фабрики не задан явно другой токен отмены.
    /// </param>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken)
      : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory`1" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="scheduler">
    ///   Планировщик, который нужно использовать при планировании задач, созданных с помощью данной фабрики <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    ///    Значение NULL означает, что следует использовать текущий объект <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </param>
    [__DynamicallyInvokable]
    public TaskFactory(TaskScheduler scheduler)
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory`1" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="creationOptions">
    ///   Параметры по умолчанию, которые необходимо использовать при создании задач данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры по умолчанию, которые необходимо использовать при создании задач продолжения данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> или <paramref name="continuationOptions" /> задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
      : this(new CancellationToken(), creationOptions, continuationOptions, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory`1" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен отмены по умолчанию, который будет назначен задачам, созданным данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory" />, если при вызове методов фабрики не задан явно другой токен отмены.
    /// </param>
    /// <param name="creationOptions">
    ///   Параметры по умолчанию, которые необходимо использовать при создании задач данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры по умолчанию, которые необходимо использовать при создании задач продолжения данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик по умолчанию, который нужно использовать при планировании задач, созданных с помощью данной фабрики <see cref="T:System.Threading.Tasks.TaskFactory`1" />.
    ///    Значение NULL указывает на то, что должно использоваться <see cref="P:System.Threading.Tasks.TaskScheduler.Current" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> или <paramref name="continuationOptions" /> задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      TaskFactory.CheckCreationOptions(creationOptions);
      this.m_defaultCancellationToken = cancellationToken;
      this.m_defaultScheduler = scheduler;
      this.m_defaultCreationOptions = creationOptions;
      this.m_defaultContinuationOptions = continuationOptions;
    }

    /// <summary>
    ///   Возвращает токен отмены по умолчанию для этой фабрики задач.
    /// </summary>
    /// <returns>Токен отмены по умолчанию для этой фабрики задач.</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCancellationToken;
      }
    }

    /// <summary>
    ///   Возвращает планировщик задач для этой фабрики задач.
    /// </summary>
    /// <returns>Планировщик задач для этой фабрики задач.</returns>
    [__DynamicallyInvokable]
    public TaskScheduler Scheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultScheduler;
      }
    }

    /// <summary>
    ///   Возвращает значение перечисления <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, которое задает параметры создания по умолчанию для этой фабрики задач.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskCreationOptions CreationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCreationOptions;
      }
    }

    /// <summary>
    ///   Возвращает значение перечисления <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, которое задает параметры продолжения по умолчанию для этой фабрики задач.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskContinuationOptions ContinuationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultContinuationOptions;
      }
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Источник токена отмены, созданные<paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик задач, который используется для планирования созданной задачи.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Источник токена отмены, созданные<paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные для использования этим делегатом <paramref name="function" />.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные для использования этим делегатом <paramref name="function" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Источник токена отмены, созданные<paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные для использования этим делегатом <paramref name="function" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>Создает и запускает задачу.</summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные для использования этим делегатом <paramref name="function" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик задач, который используется для планирования созданной задачи.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Источник токена отмены, созданные<paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
    {
      Exception exception = (Exception) null;
      OperationCanceledException canceledException = (OperationCanceledException) null;
      TResult result = default (TResult);
      try
      {
        if (endFunction != null)
          result = endFunction(iar);
        else
          endAction(iar);
      }
      catch (OperationCanceledException ex)
      {
        canceledException = ex;
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      finally
      {
        if (canceledException != null)
          promise.TrySetCanceled(canceledException.CancellationToken, (object) canceledException);
        else if (exception != null)
        {
          if (promise.TrySetException((object) exception) && exception is ThreadAbortException)
            promise.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(promise.Id);
          if (requiresSynchronization)
            promise.TrySetResult(result);
          else
            promise.DangerousSetResult(result);
        }
      }
    }

    /// <summary>
    ///   Создает задачу, которая выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс <see cref="T:System.IAsyncResult" />, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу, которая выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс <see cref="T:System.IAsyncResult" />, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу, которая выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс <see cref="T:System.IAsyncResult" />, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик задач, который используется для планирования задачи, выполняющей метод End.
    /// </param>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="asyncResult" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, scheduler, ref stackMark);
    }

    internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (asyncResult == null)
        throw new ArgumentNullException(nameof (asyncResult));
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskFactory.CheckFromAsyncOptions(creationOptions, false);
      Task<TResult> promise = new Task<TResult>((object) null, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync", 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      Task t = new Task((Action<object>) (_param1 => TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true)), (object) null, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null, ref stackMark);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Verbose, t.Id, "TaskFactory.FromAsync Callback", 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(t);
      if (asyncResult.IsCompleted)
      {
        try
        {
          t.InternalRunSynchronously(scheduler, false);
        }
        catch (Exception ex)
        {
          promise.TrySetException((object) ex);
        }
      }
      else
        ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, (WaitOrTimerCallback) ((_param1, _param2) =>
        {
          try
          {
            t.InternalRunSynchronously(scheduler, false);
          }
          catch (Exception ex)
          {
            promise.TrySetException((object) ex);
          }
        }), (object) null, -1, true);
      return promise;
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException(nameof (beginMethod));
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod((AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod((AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException(nameof (beginMethod));
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException(nameof (endFunction));
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg2">
    ///   Второй аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg2">
    ///   Второй аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект, который управляет поведением созданной <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException(nameof (beginMethod));
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, arg2, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, arg2, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg2">
    ///   Второй аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg3">
    ///   Третий аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип третьего аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg3">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает задачу, которая представляет пару методов Begin и End, соответствующих шаблону модели асинхронного программирования.
    /// </summary>
    /// <param name="beginMethod">
    ///   Делегат, который начинает асинхронную операцию.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат, который заканчивает асинхронную операцию.
    /// </param>
    /// <param name="arg1">
    ///   Первый аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg2">
    ///   Второй аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="arg3">
    ///   Третий аргумент, переданный делегату <paramref name="beginMethod" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="beginMethod" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект, который управляет поведением созданной задачи.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип третьего аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg3">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданная задача, которая представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="beginMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="endMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Параметр задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException(nameof (beginMethod));
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, arg2, arg3, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
    {
      TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> asyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
      IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, (object) asyncTrimPromise);
      if (asyncResult.CompletedSynchronously)
        asyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
      return (Task<TResult>) asyncTrimPromise;
    }

    private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
    {
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      return new Task<TResult>(true, default (TResult), creationOptions, ct);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> массив является <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="continuationFunction" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null или пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения NotOn* и OnlyOn* являются недопустимыми.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения NotOn* и OnlyOn* являются недопустимыми.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик, который используется для планирования созданной задачи продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null или пуст.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения NotOn* и OnlyOn* являются недопустимыми.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения набора указанных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения NotOn* и OnlyOn* являются недопустимыми.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик, который используется для планирования созданной задачи продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      Task<Task[]> task = TaskFactory.CommonCWAllLogic(tasksCopy);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>((Func<Task<Task[]>, object, TResult>) ((completedTasks, state) =>
        {
          completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
          return ((Func<Task[], TResult>) state)(completedTasks.Result);
        }), (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>((Func<Task<Task[]>, object, TResult>) ((completedTasks, state) =>
      {
        completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
        ((Action<Task[]>) state)(completedTasks.Result);
        return default (TResult);
      }), (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит значение null или пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив пуст.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения <see langword="NotOn*" /> и <see langword="OnlyOn*" /> являются недопустимыми.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение перечисления.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения <see langword="NotOn*" /> и <see langword="OnlyOn*" /> являются недопустимыми.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик задач, который используется для планирования созданной задачи продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент указывает недопустимое <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения <see langword="NotOn*" /> и <see langword="OnlyOn*" /> являются недопустимыми.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение перечисления.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая будет запущена после выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Одно из значений перечисления, которое управляет поведением созданной задачи продолжения.
    ///    Значения <see langword="NotOn*" /> и <see langword="OnlyOn*" /> являются недопустимыми.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение TaskContinuationOptions.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      Task<Task> task1 = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      if (continuationFunction == null)
        return task1.ContinueWith<TResult>((Func<Task<Task>, object, TResult>) ((completedTask, state) =>
        {
          ((Action<Task>) state)(completedTask.Result);
          return default (TResult);
        }), (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      Task<Task> task2 = task1;
      Func<Task, TResult> func = continuationFunction;
      TaskScheduler scheduler1 = scheduler;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = (int) continuationOptions;
      ref StackCrawlMark local = ref stackMark;
      return task2.ContinueWith<TResult>((Func<Task<Task>, object, TResult>) ((completedTask, state) => ((Func<Task, TResult>) state)(completedTask.Result)), (object) func, scheduler1, cancellationToken1, (TaskContinuationOptions) num, ref local);
    }

    internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      Task<Task> task = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
    {
      internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);
      private TInstance m_thisRef;
      private Func<TInstance, IAsyncResult, TResult> m_endMethod;

      internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
      {
        this.m_thisRef = thisRef;
        this.m_endMethod = endMethod;
      }

      internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException(nameof (asyncResult));
        TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> asyncState = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
        if (asyncState == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), nameof (asyncResult));
        TInstance thisRef = asyncState.m_thisRef;
        Func<TInstance, IAsyncResult, TResult> endMethod = asyncState.m_endMethod;
        asyncState.m_thisRef = default (TInstance);
        asyncState.m_endMethod = (Func<TInstance, IAsyncResult, TResult>) null;
        if (endMethod == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), nameof (asyncResult));
        if (asyncResult.CompletedSynchronously)
          return;
        asyncState.Complete(thisRef, endMethod, asyncResult, true);
      }

      internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
      {
        bool flag = false;
        try
        {
          TResult result = endMethod(thisRef, asyncResult);
          if (requiresSynchronization)
          {
            flag = this.TrySetResult(result);
          }
          else
          {
            this.DangerousSetResult(result);
            flag = true;
          }
        }
        catch (OperationCanceledException ex)
        {
          flag = this.TrySetCanceled(ex.CancellationToken, (object) ex);
        }
        catch (Exception ex)
        {
          flag = this.TrySetException((object) ex);
        }
      }
    }
  }
}
