// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.Task`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет асинхронную операцию, которая может вернуть значение.
  /// </summary>
  /// <typeparam name="TResult">
  ///   Тип результата, созданного данной задачей <see cref="T:System.Threading.Tasks.Task`1" />.
  /// </typeparam>
  [DebuggerTypeProxy(typeof (SystemThreadingTasks_FutureDebugView<>))]
  [DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Task<TResult> : Task
  {
    private static readonly TaskFactory<TResult> s_Factory = new TaskFactory<TResult>();
    internal static readonly Func<Task<Task>, Task<TResult>> TaskWhenAnyCast = (Func<Task<Task>, Task<TResult>>) (completed => (Task<TResult>) completed.Result);
    internal TResult m_result;

    internal Task()
    {
    }

    internal Task(object state, TaskCreationOptions options)
      : base(state, options, true)
    {
    }

    internal Task(TResult result)
      : base(false, TaskCreationOptions.None, new CancellationToken())
    {
      this.m_result = result;
    }

    internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct)
      : base(canceled, creationOptions, ct)
    {
      if (canceled)
        return;
      this.m_result = result;
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с указанной функцией.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function)
      : this(function, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с указанной функцией.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" /> для назначения данной задаче.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, CancellationToken cancellationToken)
      : this(function, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с указанной функцией и параметрами создания.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, TaskCreationOptions creationOptions)
      : this(function, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с указанной функцией и параметрами создания.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this(function, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с указанными функцией и состоянием.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state)
      : this((Delegate) function, state, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" /> для назначения новой задаче.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken)
      : this((Delegate) function, state, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
      : this((Delegate) function, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task`1" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="function">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    ///    После завершения выполнения функции будет установлено свойство <see cref="P:System.Threading.Tasks.Task`1.Result" /> результата задачи для возврата результата выполнения функции.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" /> для назначения новой задаче.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="function" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) function, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this(valueSelector, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
      : base((Delegate) valueSelector, (object) null, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
    }

    internal Task(Func<object, TResult> valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this((Delegate) valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Delegate valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
      : base(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
    }

    internal static Task<TResult> StartNew(Task parent, Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (function == null)
        throw new ArgumentNullException(nameof (function));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
      Task<TResult> task = new Task<TResult>(function, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
      task.ScheduleAndStart(false);
      return task;
    }

    internal static Task<TResult> StartNew(Task parent, Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (function == null)
        throw new ArgumentNullException(nameof (function));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions), Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
      Task<TResult> task = new Task<TResult>(function, state, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
      task.ScheduleAndStart(false);
      return task;
    }

    private string DebuggerDisplayResultDescription
    {
      get
      {
        if (!this.IsRanToCompletion)
          return Environment.GetResourceString("TaskT_DebuggerNoResult");
        return string.Concat((object) this.m_result);
      }
    }

    private string DebuggerDisplayMethodDescription
    {
      get
      {
        Delegate action = (Delegate) this.m_action;
        if ((object) action == null)
          return "{null}";
        return action.Method.ToString();
      }
    }

    internal bool TrySetResult(TResult result)
    {
      if (this.IsCompleted || !this.AtomicStateUpdate(67108864, 90177536))
        return false;
      this.m_result = result;
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 16777216);
      this.m_contingentProperties?.SetCompleted();
      this.FinishStageThree();
      return true;
    }

    internal void DangerousSetResult(TResult result)
    {
      if (this.m_parent != null)
      {
        this.TrySetResult(result);
      }
      else
      {
        this.m_result = result;
        this.m_stateFlags |= 16777216;
      }
    }

    /// <summary>
    ///   Получает итоговое значение данного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <returns>
    ///   Полученное значение данной задачи <see cref="T:System.Threading.Tasks.Task`1" /> с тем же типом, что и параметр типа данной задачи.
    /// </returns>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///   <see cref="P:System.AggregateException.InnerExceptions" /> Коллекция содержит <see cref="T:System.Threading.Tasks.TaskCanceledException" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   Исключение во время выполнения задачи.
    ///   <see cref="P:System.AggregateException.InnerExceptions" /> Коллекция содержит сведения об исключении или исключения.
    /// </exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public TResult Result
    {
      [__DynamicallyInvokable] get
      {
        if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
          return this.m_result;
        return this.GetResultCore(true);
      }
    }

    internal TResult ResultOnSuccess
    {
      get
      {
        return this.m_result;
      }
    }

    internal TResult GetResultCore(bool waitCompletionNotification)
    {
      if (!this.IsCompleted)
        this.InternalWait(-1, new CancellationToken());
      if (waitCompletionNotification)
        this.NotifyDebuggerOfWaitCompletionIfNecessary();
      if (!this.IsRanToCompletion)
        this.ThrowIfExceptional(true);
      return this.m_result;
    }

    internal bool TrySetException(object exceptionObject)
    {
      bool flag = false;
      this.EnsureContingentPropertiesInitialized(true);
      if (this.AtomicStateUpdate(67108864, 90177536))
      {
        this.AddException(exceptionObject);
        this.Finish(false);
        flag = true;
      }
      return flag;
    }

    internal bool TrySetCanceled(CancellationToken tokenToRecord)
    {
      return this.TrySetCanceled(tokenToRecord, (object) null);
    }

    internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
    {
      bool flag = false;
      if (this.AtomicStateUpdate(67108864, 90177536))
      {
        this.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
        this.CancellationCleanupLogic();
        flag = true;
      }
      return flag;
    }

    /// <summary>
    ///   Предоставляет доступ к методам фабрики для создания и настройки экземпляров <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <returns>
    ///   Объект фабрики, который может создавать разнообразные объекты <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TaskFactory<TResult> Factory
    {
      [__DynamicallyInvokable] get
      {
        return Task<TResult>.s_Factory;
      }
    }

    internal override void InnerInvoke()
    {
      Func<TResult> action1 = this.m_action as Func<TResult>;
      if (action1 != null)
      {
        this.m_result = action1();
      }
      else
      {
        Func<object, TResult> action2 = this.m_action as Func<object, TResult>;
        if (action2 == null)
          return;
        this.m_result = action2(this.m_stateObject);
      }
    }

    /// <summary>
    ///   Получает объект типа awaiter, используемый для данного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <returns>Экземпляр объекта типа awaiter.</returns>
    [__DynamicallyInvokable]
    public TaskAwaiter<TResult> GetAwaiter()
    {
      return new TaskAwaiter<TResult>(this);
    }

    /// <summary>
    ///   Настраивает объект типа awaiter, используемый для данного объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continueOnCapturedContext">
    ///   Значение true, чтобы попытаться выполнить маршалинг продолжения обратно в исходный захваченный контекст; в противном случае — значение false.
    /// </param>
    /// <returns>Объект, используемый для ожидания данной задачи.</returns>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
    {
      return new ConfiguredTaskAwaitable<TResult>(this, continueOnCapturedContext);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения предыдущей задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает отменяемое продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, передаваемый новой задаче продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие для запуска в соответствии с заданным условием в <paramref name="continuationOptions" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromResultTask<TResult>(this, (Delegate) continuationAction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>
    ///   Создает продолжение, которое получает сведения о состоянии и выполняется после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату передается в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromResultTask<TResult>(this, (Delegate) continuationAction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <typeparam name="TNewResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <typeparam name="TNewResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TNewResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение NULL.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция для запуска в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// 
    ///   При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <typeparam name="TNewResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция для запуска в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    /// 
    ///   При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TNewResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task`1" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания<paramref name=" cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TNewResult> task = (Task<TNewResult>) new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, (Delegate) continuationFunction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <typeparam name="TNewResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <typeparam name="TNewResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TNewResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <typeparam name="TNewResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется после завершения выполнения целевого объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TNewResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" />  Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TNewResult> task = (Task<TNewResult>) new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, (Delegate) continuationFunction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }
  }
}
