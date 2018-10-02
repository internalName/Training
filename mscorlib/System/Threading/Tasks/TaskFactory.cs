// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Предоставляет поддержку создания и планирования объектов <see cref="T:System.Threading.Tasks.Task" />.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskFactory
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
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory" /> с конфигурацией по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public TaskFactory()
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен для задач, созданных данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory" />, если при вызове фабричных методов не задан явно другой токен CancellationToken.
    /// </param>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken)
      : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="scheduler">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskScheduler" />, который нужно использовать при планировании задач, созданных с помощью данной фабрики TaskFactory.
    ///    Значение NULL означает, что следует использовать текущий TaskScheduler.
    /// </param>
    [__DynamicallyInvokable]
    public TaskFactory(TaskScheduler scheduler)
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> по умолчанию, который нужно использовать при создании задач с помощью данной фабрики TaskFactory.
    /// </param>
    /// <param name="continuationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> по умолчанию, который нужно использовать при создании задач продолжения с помощью данной фабрики TaskFactory.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент указывает недопустимое <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> значение.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
      : this(new CancellationToken(), creationOptions, continuationOptions, (TaskScheduler) null)
    {
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.Tasks.TaskFactory" /> с заданной конфигурацией.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> по умолчанию, который будет назначен для задач, созданных данной фабрикой <see cref="T:System.Threading.Tasks.TaskFactory" />, если при вызове фабричных методов не задан явно другой токен CancellationToken.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> по умолчанию, который нужно использовать при создании задач с помощью данной фабрики TaskFactory.
    /// </param>
    /// <param name="continuationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> по умолчанию, который нужно использовать при создании задач продолжения с помощью данной фабрики TaskFactory.
    /// </param>
    /// <param name="scheduler">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskScheduler" /> по умолчанию, который нужно использовать при планировании задач, созданных с помощью данной фабрики TaskFactory.
    ///    Значение NULL означает, что следует использовать TaskScheduler.Current.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент указывает недопустимое <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> значение.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
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

    internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
    {
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions));
    }

    /// <summary>
    ///   Возвращает токен отмены по умолчанию для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Токен отмены задачи по умолчанию для этой фабрики задач.
    /// </returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCancellationToken;
      }
    }

    /// <summary>
    ///   Возвращает планировщик задач по умолчанию для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Планировщик задач по умолчанию для этой фабрики задач.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskScheduler Scheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultScheduler;
      }
    }

    /// <summary>
    ///   Возвращает параметры создания задач по умолчанию для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Параметры создания задач по умолчанию для этой фабрики задач.
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
    ///   Возвращает параметры продолжения задач по умолчанию для этой фабрики задач.
    /// </summary>
    /// <returns>
    ///   Параметры продолжения задач по умолчанию для этой фабрики задач.
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
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <returns>Запущенная задача.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="action" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, (object) null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, (object) null, cancellationToken, scheduler, creationOptions, internalOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="action" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="action" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="action" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат действия для асинхронного выполнения.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="action" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="action" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="function" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="function" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="function" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>
    ///   Создает и запускает задачу <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="function">
    ///   Делегат функции, возвращающий будущий результат с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="state">
    ///   Объект, содержащий данные, которые используются делегатом метода <paramref name="function" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Запущенная задача <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="function" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который выполняет действие метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат действия, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который выполняет действие метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат действия, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который выполняет действие метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат действия, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования задачи, выполняющей метод end.
    /// </param>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, creationOptions, scheduler, ref stackMark);
    }

    private Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, creationOptions, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
    {
      return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
    {
      return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, arg2, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task" />.
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
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, arg2, arg3, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который выполняет функцию метода End по завершении заданного объекта <see cref="T:System.IAsyncResult" />.
    /// </summary>
    /// <param name="asyncResult">
    ///   Интерфейс IAsyncResult, завершение выполнения которого инициирует обработку <paramref name="endMethod" />.
    /// </param>
    /// <param name="endMethod">
    ///   Делегат функции, который обрабатывает завершенный результат <paramref name="asyncResult" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования задачи, выполняющей метод end.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="asyncResult" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TArg1">
    ///   Тип второго аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TArg2">
    ///   Тип первого аргумента, переданного делегату <paramref name="beginMethod" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, creationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет пару методов begin и end, соответствующую шаблону модели асинхронного программирования.
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
    ///   Значение TaskCreationOptions, которое управляет поведением созданного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
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
    /// <typeparam name="TResult">
    ///   Тип результата, доступный с использованием <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Созданный объект <see cref="T:System.Threading.Tasks.Task`1" />, который представляет асинхронную операцию.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="beginMethod" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="endMethod" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Исключение, возникающее, когда <paramref name="creationOptions" /> аргумент задает недопустимое значение TaskCreationOptions.
    ///    Дополнительные сведения см. в разделе Примечания для <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />
    /// </exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, creationOptions);
    }

    internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
    {
      if (hasBeginMethod)
      {
        if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
          throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("Task_FromAsync_LongRunning"));
        if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
          throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("Task_FromAsync_PreferFairness"));
      }
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions));
    }

    internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
    {
      TaskFactory.CompleteOnCountdownPromise countdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
      for (int index = 0; index < tasksCopy.Length; ++index)
      {
        if (tasksCopy[index].IsCompleted)
          countdownPromise.Invoke(tasksCopy[index]);
        else
          tasksCopy[index].AddCompletionAction((ITaskCompletionAction) countdownPromise);
      }
      return (Task<Task[]>) countdownPromise;
    }

    internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
    {
      TaskFactory.CompleteOnCountdownPromise<T> countdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
      for (int index = 0; index < tasksCopy.Length; ++index)
      {
        if (tasksCopy[index].IsCompleted)
          countdownPromise.Invoke((Task) tasksCopy[index]);
        else
          tasksCopy[index].AddCompletionAction((ITaskCompletionAction) countdownPromise);
      }
      return (Task<Task<T>[]>) countdownPromise;
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    /// </param>
    /// <param name="scheduler">
    ///   Объект, который используется для планирования новой задачи продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <param name="scheduler">
    ///   Объект, который используется для планирования новой задачи продолжения.
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
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
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
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <param name="scheduler">
    ///   Объект, который используется для планирования новой задачи продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
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
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
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
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
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
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
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
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает задачу продолжения, которая запускается при завершении набора заданных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения всех задач в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены для присвоения новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Побитовое сочетание значений перечисления, которые управляют поведением новой задачи продолжения.
    ///    Члены NotOn* и OnlyOn* не поддерживаются.
    /// </param>
    /// <param name="scheduler">
    ///   Объект, который используется для планирования новой задачи продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданной задачей.
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
    ///   <paramref name="tasks" /> Массив пуст или содержит значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
    {
      TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
      bool flag = false;
      int count = tasks.Count;
      for (int index = 0; index < count; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        if (!flag)
        {
          if (completeOnInvokePromise.IsCompleted)
            flag = true;
          else if (task.IsCompleted)
          {
            completeOnInvokePromise.Invoke(task);
            flag = true;
          }
          else
          {
            task.AddCompletionAction((ITaskCompletionAction) completeOnInvokePromise);
            if (completeOnInvokePromise.IsCompleted)
              task.RemoveContinuation((object) completeOnInvokePromise);
          }
        }
      }
      return (Task<Task>) completeOnInvokePromise;
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов массива <paramref name="tasks" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Массив <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="tasks" /> содержит значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="tasks" /> является пустым.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Массив <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Содержит массив <see langword="null" /> значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="continuationOptions" /> аргумент задает недопустимое значение TaskContinuationOptions.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="continuationOptions" /> аргумент задает недопустимое значение TaskContinuationOptions.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
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
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="continuationOptions" /> аргумент задает недопустимое значение TaskContinuationOptions.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task`1" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationFunction">
    ///   Делегат функции, выполняемый асинхронно после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного делегатом <paramref name="continuationFunction" /> и связанного с созданным объектом <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationFunction" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// 
    ///   -или-
    /// 
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Исключение, возникающее, когда один из элементов в <paramref name="tasks" /> массива был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Исключение, возникающее, когда <paramref name="continuationOptions" /> аргумент задает недопустимое значение TaskContinuationOptions.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение задачи <see cref="T:System.Threading.Tasks.Task" />, которое будет запущено после завершения выполнения любой задачи в указанном наборе.
    /// </summary>
    /// <param name="tasks">
    ///   Массив задач, выполнение которых должно быть продолжено после завершения выполнения одной задачи.
    /// </param>
    /// <param name="continuationAction">
    ///   Делегат действия для выполнения после завершения выполнения одной задачи в массиве <paramref name="tasks" />.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Значение <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />, которое управляет поведением созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который используется для планирования созданной задачи продолжения <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <typeparam name="TAntecedentResult">
    ///   Тип результата предыдущего объекта <paramref name="tasks" />.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массиве имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="continuationAction" /> аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="scheduler" /> аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив содержит значение null.
    /// 
    ///   -или-
    /// 
    ///   Исключение, возникающее, когда <paramref name="tasks" /> массив пуст.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      Task[] taskArray = new Task[tasks.Length];
      for (int index = 0; index < tasks.Length; ++index)
      {
        taskArray[index] = tasks[index];
        if (taskArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
      }
      return taskArray;
    }

    internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      Task<TResult>[] taskArray = new Task<TResult>[tasks.Length];
      for (int index = 0; index < tasks.Length; ++index)
      {
        taskArray[index] = tasks[index];
        if (taskArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
      }
      return taskArray;
    }

    internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
    {
      if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
        throw new ArgumentOutOfRangeException(nameof (continuationOptions), Environment.GetResourceString("Task_ContinueWith_ESandLR"));
      if ((continuationOptions & ~(TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (continuationOptions));
      if ((continuationOptions & (TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.NotOnRanToCompletion)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (continuationOptions), Environment.GetResourceString("Task_MultiTaskContinuation_FireOptions"));
    }

    private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
    {
      private readonly Task[] _tasks;
      private int _count;

      internal CompleteOnCountdownPromise(Task[] tasksCopy)
      {
        this._tasks = tasksCopy;
        this._count = tasksCopy.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAll", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (completingTask.IsWaitNotificationEnabled)
          this.SetNotificationForWaitCompletion(true);
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(this._tasks);
      }

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
          return false;
        }
      }
    }

    private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
    {
      private readonly Task<T>[] _tasks;
      private int _count;

      internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
      {
        this._tasks = tasksCopy;
        this._count = tasksCopy.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAll<>", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (completingTask.IsWaitNotificationEnabled)
          this.SetNotificationForWaitCompletion(true);
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(this._tasks);
      }

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion((Task[]) this._tasks);
          return false;
        }
      }
    }

    internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
    {
      private IList<Task> _tasks;
      private int m_firstTaskAlreadyCompleted;

      public CompleteOnInvokePromise(IList<Task> tasks)
      {
        this._tasks = tasks;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAny", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (Interlocked.CompareExchange(ref this.m_firstTaskAlreadyCompleted, 1, 0) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
        {
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Choice);
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        }
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(completingTask);
        IList<Task> tasks = this._tasks;
        int count = tasks.Count;
        for (int index = 0; index < count; ++index)
        {
          Task task = tasks[index];
          if (task != null && !task.IsCompleted)
            task.RemoveContinuation((object) this);
        }
        this._tasks = (IList<Task>) null;
      }
    }
  }
}
