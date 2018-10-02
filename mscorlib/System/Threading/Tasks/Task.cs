// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.Task
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет асинхронную операцию.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [DebuggerTypeProxy(typeof (SystemThreadingTasks_TaskDebugView))]
  [DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
  {
    private static readonly TaskFactory s_factory = new TaskFactory();
    private static readonly object s_taskCompletionSentinel = new object();
    private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();
    private static readonly object s_activeTasksLock = new object();
    private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);
    private static readonly Func<Task.ContingentProperties> s_createContingentProperties = (Func<Task.ContingentProperties>) (() => new Task.ContingentProperties());
    private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = (Predicate<Task>) (t => t.IsExceptionObservedByParent);
    private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = (Predicate<object>) (tc => tc == null);
    [ThreadStatic]
    internal static Task t_currentTask;
    [ThreadStatic]
    private static StackGuard t_stackGuard;
    internal static int s_taskIdCounter;
    private volatile int m_taskId;
    internal object m_action;
    internal object m_stateObject;
    internal TaskScheduler m_taskScheduler;
    internal readonly Task m_parent;
    internal volatile int m_stateFlags;
    private const int OptionsMask = 65535;
    internal const int TASK_STATE_STARTED = 65536;
    internal const int TASK_STATE_DELEGATE_INVOKED = 131072;
    internal const int TASK_STATE_DISPOSED = 262144;
    internal const int TASK_STATE_EXCEPTIONOBSERVEDBYPARENT = 524288;
    internal const int TASK_STATE_CANCELLATIONACKNOWLEDGED = 1048576;
    internal const int TASK_STATE_FAULTED = 2097152;
    internal const int TASK_STATE_CANCELED = 4194304;
    internal const int TASK_STATE_WAITING_ON_CHILDREN = 8388608;
    internal const int TASK_STATE_RAN_TO_COMPLETION = 16777216;
    internal const int TASK_STATE_WAITINGFORACTIVATION = 33554432;
    internal const int TASK_STATE_COMPLETION_RESERVED = 67108864;
    internal const int TASK_STATE_THREAD_WAS_ABORTED = 134217728;
    internal const int TASK_STATE_WAIT_COMPLETION_NOTIFICATION = 268435456;
    internal const int TASK_STATE_EXECUTIONCONTEXT_IS_NULL = 536870912;
    internal const int TASK_STATE_TASKSCHEDULED_WAS_FIRED = 1073741824;
    private const int TASK_STATE_COMPLETED_MASK = 23068672;
    private const int CANCELLATION_REQUESTED = 1;
    private volatile object m_continuationObject;
    [FriendAccessAllowed]
    internal static bool s_asyncDebuggingEnabled;
    internal volatile Task.ContingentProperties m_contingentProperties;
    private static Task s_completedTask;
    [SecurityCritical]
    private static ContextCallback s_ecCallback;

    [FriendAccessAllowed]
    internal static bool AddToActiveTasks(Task task)
    {
      lock (Task.s_activeTasksLock)
        Task.s_currentActiveTasks[task.Id] = task;
      return true;
    }

    [FriendAccessAllowed]
    internal static void RemoveFromActiveTasks(int taskId)
    {
      lock (Task.s_activeTasksLock)
        Task.s_currentActiveTasks.Remove(taskId);
    }

    internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
    {
      int num = (int) creationOptions;
      if (canceled)
      {
        this.m_stateFlags = 5242880 | num;
        Task.ContingentProperties contingentProperties;
        this.m_contingentProperties = contingentProperties = new Task.ContingentProperties();
        contingentProperties.m_cancellationToken = ct;
        contingentProperties.m_internalCancellationRequested = 1;
      }
      else
        this.m_stateFlags = 16777216 | num;
    }

    internal Task()
    {
      this.m_stateFlags = 33555456;
    }

    internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
    {
      if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions));
      if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
        this.m_parent = Task.InternalCurrent;
      this.TaskConstructorCore((object) null, state, new CancellationToken(), creationOptions, InternalTaskOptions.PromiseTask, (TaskScheduler) null);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданным действием.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action)
      : this((Delegate) action, (object) null, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданным действием и токеном <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет контролироваться новой задачей.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <see cref="T:System.Threading.CancellationToken" /> уже удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, CancellationToken cancellationToken)
      : this((Delegate) action, (object) null, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданными действием и параметрами создания.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, TaskCreationOptions creationOptions)
      : this((Delegate) action, (object) null, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданными действием и параметрами создания.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет контролироваться новой задачей.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) action, (object) null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданным действием и состоянием.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state)
      : this((Delegate) action, state, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет контролироваться новой задачей.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, CancellationToken cancellationToken)
      : this((Delegate) action, state, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, TaskCreationOptions creationOptions)
      : this((Delegate) action, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>
    ///   Инициализирует новую задачу <see cref="T:System.Threading.Tasks.Task" /> с заданными действием, состоянием и параметрами.
    /// </summary>
    /// <param name="action">
    ///   Делегат, который представляет код, выполняемый в рамках задачи.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет контролироваться новой задачей.
    /// </param>
    /// <param name="creationOptions">
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, который используется для настройки поведения задачи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создания <paramref name="cancellationToken" /> уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="action" /> имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Action<object> action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this((Delegate) action, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      if ((object) action == null)
        throw new ArgumentNullException(nameof (action));
      if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None || (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        this.m_parent = parent;
      this.TaskConstructorCore((object) action, state, cancellationToken, creationOptions, internalOptions, scheduler);
    }

    internal void TaskConstructorCore(object action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      this.m_action = action;
      this.m_stateObject = state;
      this.m_taskScheduler = scheduler;
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (creationOptions));
      if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None && (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_ctor_LRandSR"));
      int num = (int) (creationOptions | (TaskCreationOptions) internalOptions);
      if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
        num |= 33554432;
      this.m_stateFlags = num;
      if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
        this.m_parent.AddNewChild();
      if (!cancellationToken.CanBeCanceled)
        return;
      this.AssignCancellationToken(cancellationToken, (Task) null, (TaskContinuation) null);
    }

    private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
      contingentProperties.m_cancellationToken = cancellationToken;
      try
      {
        if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
          cancellationToken.ThrowIfSourceDisposed();
        if ((this.Options & (TaskCreationOptions) 13312) != TaskCreationOptions.None)
          return;
        if (cancellationToken.IsCancellationRequested)
        {
          this.InternalCancel(false);
        }
        else
        {
          CancellationTokenRegistration tokenRegistration = antecedent != null ? cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, (object) new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation)) : cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, (object) this);
          contingentProperties.m_cancellationRegistration = new Shared<CancellationTokenRegistration>(tokenRegistration);
        }
      }
      catch
      {
        if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
          this.m_parent.DisregardChild();
        throw;
      }
    }

    private static void TaskCancelCallback(object o)
    {
      Task task = o as Task;
      if (task == null)
      {
        Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
        if (tuple != null)
        {
          task = tuple.Item1;
          tuple.Item2.RemoveContinuation((object) tuple.Item3);
        }
      }
      task.InternalCancel(false);
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

    [SecuritySafeCritical]
    internal void PossiblyCaptureContext(ref StackCrawlMark stackMark)
    {
      this.CapturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    internal TaskCreationOptions Options
    {
      get
      {
        return Task.OptionsMethod(this.m_stateFlags);
      }
    }

    internal static TaskCreationOptions OptionsMethod(int flags)
    {
      return (TaskCreationOptions) (flags & (int) ushort.MaxValue);
    }

    internal bool AtomicStateUpdate(int newBits, int illegalBits)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int stateFlags = this.m_stateFlags;
        if ((stateFlags & illegalBits) == 0)
        {
          if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) != stateFlags)
            spinWait.SpinOnce();
          else
            goto label_4;
        }
        else
          break;
      }
      return false;
label_4:
      return true;
    }

    internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        oldFlags = this.m_stateFlags;
        if ((oldFlags & illegalBits) == 0)
        {
          if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) != oldFlags)
            spinWait.SpinOnce();
          else
            goto label_4;
        }
        else
          break;
      }
      return false;
label_4:
      return true;
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      if (enabled)
      {
        this.AtomicStateUpdate(268435456, 90177536);
      }
      else
      {
        SpinWait spinWait = new SpinWait();
        while (true)
        {
          int stateFlags = this.m_stateFlags;
          if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags & -268435457, stateFlags) != stateFlags)
            spinWait.SpinOnce();
          else
            break;
        }
      }
    }

    internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
    {
      if (!this.IsWaitNotificationEnabled || !this.ShouldNotifyDebuggerOfWaitCompletion)
        return false;
      this.NotifyDebuggerOfWaitCompletion();
      return true;
    }

    internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
    {
      foreach (Task task in tasks)
      {
        if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
          return true;
      }
      return false;
    }

    internal bool IsWaitNotificationEnabledOrNotRanToCompletion
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (this.m_stateFlags & 285212672) != 16777216;
      }
    }

    internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
    {
      get
      {
        return this.IsWaitNotificationEnabled;
      }
    }

    internal bool IsWaitNotificationEnabled
    {
      get
      {
        return (uint) (this.m_stateFlags & 268435456) > 0U;
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private void NotifyDebuggerOfWaitCompletion()
    {
      this.SetNotificationForWaitCompletion(false);
    }

    internal bool MarkStarted()
    {
      return this.AtomicStateUpdate(65536, 4259840);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool FireTaskScheduledIfNeeded(TaskScheduler ts)
    {
      TplEtwProvider log = TplEtwProvider.Log;
      if (!log.IsEnabled() || (this.m_stateFlags & 1073741824) != 0)
        return false;
      this.m_stateFlags |= 1073741824;
      Task internalCurrent = Task.InternalCurrent;
      Task parent = this.m_parent;
      log.TaskScheduled(ts.Id, internalCurrent == null ? 0 : internalCurrent.Id, this.Id, parent == null ? 0 : parent.Id, (int) this.Options, Thread.GetDomainID());
      return true;
    }

    internal void AddNewChild()
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
      if (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot)
        ++contingentProperties.m_completionCountdown;
      else
        Interlocked.Increment(ref contingentProperties.m_completionCountdown);
    }

    internal void DisregardChild()
    {
      Interlocked.Decrement(ref this.EnsureContingentPropertiesInitialized(true).m_completionCountdown);
    }

    /// <summary>
    ///   Запускает задачу <see cref="T:System.Threading.Tasks.Task" />, планируя ее выполнение в текущем планировщике <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача <see cref="T:System.Threading.Tasks.Task" /> не находится в допустимом состоянии для запуска.
    ///    Возможно, она уже запущена, выполнена или отменена, или она была создана способом, не поддерживающим прямое планирование.
    /// </exception>
    [__DynamicallyInvokable]
    public void Start()
    {
      this.Start(TaskScheduler.Current);
    }

    /// <summary>
    ///   Запускает задачу <see cref="T:System.Threading.Tasks.Task" />, планируя ее выполнение в заданном планировщике <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, с которым нужно связать и в котором нужно выполнить данную задачу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача <see cref="T:System.Threading.Tasks.Task" /> не находится в допустимом состоянии для запуска.
    ///    Возможно, она уже запущена, выполнена или отменена, или она была создана способом, не поддерживающим прямое планирование.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskSchedulerException">
    ///   Планировщику не удалось поставить эту задачу в очередь.
    /// </exception>
    [__DynamicallyInvokable]
    public void Start(TaskScheduler scheduler)
    {
      int stateFlags = this.m_stateFlags;
      if (Task.IsCompletedMethod(stateFlags))
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_TaskCompleted"));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
      if ((taskCreationOptions & (TaskCreationOptions) 1024) != TaskCreationOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_Promise"));
      if ((taskCreationOptions & (TaskCreationOptions) 512) != TaskCreationOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_ContinuationTask"));
      if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, (TaskScheduler) null) != null)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_AlreadyStarted"));
      this.ScheduleAndStart(true);
    }

    /// <summary>
    ///   Синхронно выполняет задачу <see cref="T:System.Threading.Tasks.Task" /> в текущем планировщике <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача <see cref="T:System.Threading.Tasks.Task" /> не находится в допустимом состоянии для запуска.
    ///    Возможно, она уже запущена, выполнена или отменена, или она была создана способом, не поддерживающим прямое планирование.
    /// </exception>
    [__DynamicallyInvokable]
    public void RunSynchronously()
    {
      this.InternalRunSynchronously(TaskScheduler.Current, true);
    }

    /// <summary>
    ///   Синхронно выполняет задачу <see cref="T:System.Threading.Tasks.Task" /> в предоставленном планировщике <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    /// <param name="scheduler">
    ///   Планировщик, в котором следует попытаться выполнить задачу.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача <see cref="T:System.Threading.Tasks.Task" /> не находится в допустимом состоянии для запуска.
    ///    Возможно, она уже запущена, выполнена или отменена, или она была создана способом, не поддерживающим прямое планирование.
    /// </exception>
    [__DynamicallyInvokable]
    public void RunSynchronously(TaskScheduler scheduler)
    {
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      this.InternalRunSynchronously(scheduler, true);
    }

    [SecuritySafeCritical]
    internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
    {
      int stateFlags = this.m_stateFlags;
      TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
      if ((taskCreationOptions & (TaskCreationOptions) 512) != TaskCreationOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Continuation"));
      if ((taskCreationOptions & (TaskCreationOptions) 1024) != TaskCreationOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Promise"));
      if (Task.IsCompletedMethod(stateFlags))
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
      if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, (TaskScheduler) null) != null)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_AlreadyStarted"));
      if (!this.MarkStarted())
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
      bool flag = false;
      try
      {
        if (!scheduler.TryRunInline(this, false))
        {
          scheduler.InternalQueueTask(this);
          flag = true;
        }
        if (!waitForCompletion || this.IsCompleted)
          return;
        this.SpinThenBlockingWait(-1, new CancellationToken());
      }
      catch (System.Exception ex)
      {
        if (!flag && !(ex is ThreadAbortException))
        {
          TaskSchedulerException schedulerException = new TaskSchedulerException(ex);
          this.AddException((object) schedulerException);
          this.Finish(false);
          this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
          throw schedulerException;
        }
        throw;
      }
    }

    internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
    {
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
      task.PossiblyCaptureContext(ref stackMark);
      task.ScheduleAndStart(false);
      return task;
    }

    internal static int NewId()
    {
      int TaskID;
      do
      {
        TaskID = Interlocked.Increment(ref Task.s_taskIdCounter);
      }
      while (TaskID == 0);
      TplEtwProvider.Log.NewID(TaskID);
      return TaskID;
    }

    /// <summary>
    ///   Возвращает идентификатор данного экземпляра <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор, присвоенный системой данному экземпляру <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Id
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_taskId == 0)
          Interlocked.CompareExchange(ref this.m_taskId, Task.NewId(), 0);
        return this.m_taskId;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор выполняющейся в настоящее время задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <returns>
    ///   Целое число, присвоенное системой выполняемой в настоящее время задаче.
    /// </returns>
    [__DynamicallyInvokable]
    public static int? CurrentId
    {
      [__DynamicallyInvokable] get
      {
        return Task.InternalCurrent?.Id;
      }
    }

    internal static Task InternalCurrent
    {
      get
      {
        return Task.t_currentTask;
      }
    }

    internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
    {
      if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
        return (Task) null;
      return Task.InternalCurrent;
    }

    internal static StackGuard CurrentStackGuard
    {
      get
      {
        StackGuard stackGuard = Task.t_stackGuard;
        if (stackGuard == null)
          Task.t_stackGuard = stackGuard = new StackGuard();
        return stackGuard;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.AggregateException" />, который привел к преждевременному завершению задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Если задача <see cref="T:System.Threading.Tasks.Task" /> завершилась успешно или еще не создала ни одного исключения, возвращает значение <see langword="null" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.AggregateException" />, который привел к преждевременному завершению задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    [__DynamicallyInvokable]
    public AggregateException Exception
    {
      [__DynamicallyInvokable] get
      {
        AggregateException aggregateException = (AggregateException) null;
        if (this.IsFaulted)
          aggregateException = this.GetExceptions(false);
        return aggregateException;
      }
    }

    /// <summary>
    ///   Возвращает состояние <see cref="T:System.Threading.Tasks.TaskStatus" /> данной задачи.
    /// </summary>
    /// <returns>
    ///   Текущее состояние <see cref="T:System.Threading.Tasks.TaskStatus" /> данного экземпляра задачи.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskStatus Status
    {
      [__DynamicallyInvokable] get
      {
        int stateFlags = this.m_stateFlags;
        return (stateFlags & 2097152) == 0 ? ((stateFlags & 4194304) == 0 ? ((stateFlags & 16777216) == 0 ? ((stateFlags & 8388608) == 0 ? ((stateFlags & 131072) == 0 ? ((stateFlags & 65536) == 0 ? ((stateFlags & 33554432) == 0 ? TaskStatus.Created : TaskStatus.WaitingForActivation) : TaskStatus.WaitingToRun) : TaskStatus.Running) : TaskStatus.WaitingForChildrenToComplete) : TaskStatus.RanToCompletion) : TaskStatus.Canceled) : TaskStatus.Faulted;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, завершилось ли выполнение данного экземпляра <see cref="T:System.Threading.Tasks.Task" /> из-за отмены.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если задача была завершена из-за отмены; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCanceled
    {
      [__DynamicallyInvokable] get
      {
        return (this.m_stateFlags & 6291456) == 4194304;
      }
    }

    internal bool IsCancellationRequested
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null)
          return false;
        if (contingentProperties.m_internalCancellationRequested != 1)
          return contingentProperties.m_cancellationToken.IsCancellationRequested;
        return true;
      }
    }

    internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
    {
      return this.m_contingentProperties ?? this.EnsureContingentPropertiesInitializedCore(needsProtection);
    }

    private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
    {
      if (needsProtection)
        return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
      return this.m_contingentProperties = new Task.ContingentProperties();
    }

    internal CancellationToken CancellationToken
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null)
          return contingentProperties.m_cancellationToken;
        return new CancellationToken();
      }
    }

    internal bool IsCancellationAcknowledged
    {
      get
      {
        return (uint) (this.m_stateFlags & 1048576) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, которое показывает, завершилась ли задача <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если задача была завершена; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return Task.IsCompletedMethod(this.m_stateFlags);
      }
    }

    private static bool IsCompletedMethod(int flags)
    {
      return (uint) (flags & 23068672) > 0U;
    }

    internal bool IsRanToCompletion
    {
      get
      {
        return (this.m_stateFlags & 23068672) == 16777216;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, используемый для создания данной задачи.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Tasks.TaskCreationOptions" />, используемый для создания данной задачи.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskCreationOptions CreationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.Options & (TaskCreationOptions) -65281;
      }
    }

    [__DynamicallyInvokable]
    WaitHandle IAsyncResult.AsyncWaitHandle
    {
      [__DynamicallyInvokable] get
      {
        if ((uint) (this.m_stateFlags & 262144) > 0U)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("Task_ThrowIfDisposed"));
        return this.CompletedEvent.WaitHandle;
      }
    }

    /// <summary>
    ///   Возвращает объект состояния, предоставленный при создании задачи <see cref="T:System.Threading.Tasks.Task" />, или значение null, если объект не предоставлен.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, представляющий данные о состоянии, переданные задаче на этапе создания.
    /// </returns>
    [__DynamicallyInvokable]
    public object AsyncState
    {
      [__DynamicallyInvokable] get
      {
        return this.m_stateObject;
      }
    }

    [__DynamicallyInvokable]
    bool IAsyncResult.CompletedSynchronously
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    internal TaskScheduler ExecutingTaskScheduler
    {
      get
      {
        return this.m_taskScheduler;
      }
    }

    /// <summary>
    ///   Предоставляет доступ к методам фабрики для создания и настройки экземпляров <see cref="T:System.Threading.Tasks.Task" /> и <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </summary>
    /// <returns>
    ///   Объект фабрики, который может создавать разнообразные объекты <see cref="T:System.Threading.Tasks.Task" /> и <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TaskFactory Factory
    {
      [__DynamicallyInvokable] get
      {
        return Task.s_factory;
      }
    }

    /// <summary>Возвращает задачу, которая уже завершилась успешно.</summary>
    /// <returns>Успешно завершенная задача.</returns>
    [__DynamicallyInvokable]
    public static Task CompletedTask
    {
      [__DynamicallyInvokable] get
      {
        Task task = Task.s_completedTask;
        if (task == null)
        {
          int num1 = 0;
          int num2 = 16384;
          CancellationToken ct = new CancellationToken();
          Task.s_completedTask = task = new Task(num1 != 0, (TaskCreationOptions) num2, ct);
        }
        return task;
      }
    }

    internal ManualResetEventSlim CompletedEvent
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
        if (contingentProperties.m_completionEvent == null)
        {
          bool isCompleted = this.IsCompleted;
          ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
          if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, (ManualResetEventSlim) null) != null)
            manualResetEventSlim.Dispose();
          else if (!isCompleted && this.IsCompleted)
            manualResetEventSlim.Set();
        }
        return contingentProperties.m_completionEvent;
      }
    }

    internal bool IsSelfReplicatingRoot
    {
      get
      {
        return (this.Options & (TaskCreationOptions) 2304) == (TaskCreationOptions) 2048;
      }
    }

    internal bool IsChildReplica
    {
      get
      {
        return (uint) (this.Options & (TaskCreationOptions) 256) > 0U;
      }
    }

    internal int ActiveChildCount
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null)
          return 0;
        return contingentProperties.m_completionCountdown - 1;
      }
    }

    internal bool ExceptionRecorded
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null && contingentProperties.m_exceptionsHolder != null)
          return contingentProperties.m_exceptionsHolder.ContainsFaultList;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, завершилась ли задача <see cref="T:System.Threading.Tasks.Task" /> из-за необработанного исключения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если задача создала необрабатываемое исключение; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFaulted
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_stateFlags & 2097152) > 0U;
      }
    }

    internal ExecutionContext CapturedContext
    {
      get
      {
        if ((this.m_stateFlags & 536870912) == 536870912)
          return (ExecutionContext) null;
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null && contingentProperties.m_capturedContext != null)
          return contingentProperties.m_capturedContext;
        return ExecutionContext.PreAllocatedDefault;
      }
      set
      {
        if (value == null)
        {
          this.m_stateFlags |= 536870912;
        }
        else
        {
          if (value.IsPreAllocatedDefault)
            return;
          this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
        }
      }
    }

    private static ExecutionContext CopyExecutionContext(ExecutionContext capturedContext)
    {
      if (capturedContext == null)
        return (ExecutionContext) null;
      if (capturedContext.IsPreAllocatedDefault)
        return ExecutionContext.PreAllocatedDefault;
      return capturedContext.CreateCopy();
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача не находится в одном из состояний окончательного: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </exception>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Удаляет задачу<see cref="T:System.Threading.Tasks.Task" />, освобождая все используемые ею неуправляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Логическое значение, указывающее, вызывается ли данный метод из-за вызова задачи <see cref="M:System.Threading.Tasks.Task.Dispose" />.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Задача не находится в одном из состояний окончательного: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </exception>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if ((this.Options & (TaskCreationOptions) 16384) != TaskCreationOptions.None)
          return;
        if (!this.IsCompleted)
          throw new InvalidOperationException(Environment.GetResourceString("Task_Dispose_NotCompleted"));
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null)
        {
          ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
          if (completionEvent != null)
          {
            contingentProperties.m_completionEvent = (ManualResetEventSlim) null;
            if (!completionEvent.IsSet)
              completionEvent.Set();
            completionEvent.Dispose();
          }
        }
      }
      this.m_stateFlags |= 262144;
    }

    [SecuritySafeCritical]
    internal void ScheduleAndStart(bool needsProtection)
    {
      if (needsProtection)
      {
        if (!this.MarkStarted())
          return;
      }
      else
        this.m_stateFlags |= 65536;
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(this);
      if (AsyncCausalityTracer.LoggingOn && (this.Options & (TaskCreationOptions) 512) == TaskCreationOptions.None)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task: " + ((Delegate) this.m_action).Method.Name, 0UL);
      try
      {
        this.m_taskScheduler.InternalQueueTask(this);
      }
      catch (ThreadAbortException ex)
      {
        this.AddException((object) ex);
        this.FinishThreadAbortedTask(true, false);
      }
      catch (System.Exception ex)
      {
        TaskSchedulerException schedulerException = new TaskSchedulerException(ex);
        this.AddException((object) schedulerException);
        this.Finish(false);
        if ((this.Options & (TaskCreationOptions) 512) == TaskCreationOptions.None)
          this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
        throw schedulerException;
      }
    }

    internal void AddException(object exceptionObject)
    {
      this.AddException(exceptionObject, false);
    }

    internal void AddException(object exceptionObject, bool representsCancellation)
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
      if (contingentProperties.m_exceptionsHolder == null)
      {
        TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
        if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, (TaskExceptionHolder) null) != null)
          taskExceptionHolder.MarkAsHandled(false);
      }
      lock (contingentProperties)
        contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
    }

    private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
    {
      System.Exception includeThisException = (System.Exception) null;
      if (includeTaskCanceledExceptions && this.IsCanceled)
        includeThisException = (System.Exception) new TaskCanceledException(this);
      if (this.ExceptionRecorded)
        return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, includeThisException);
      if (includeThisException == null)
        return (AggregateException) null;
      return new AggregateException(new System.Exception[1]
      {
        includeThisException
      });
    }

    internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
    {
      if (!this.IsFaulted || !this.ExceptionRecorded)
        return new ReadOnlyCollection<ExceptionDispatchInfo>((IList<ExceptionDispatchInfo>) new ExceptionDispatchInfo[0]);
      return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
    }

    internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
    {
      return this.m_contingentProperties?.m_exceptionsHolder?.GetCancellationExceptionDispatchInfo();
    }

    internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
    {
      System.Exception exceptions = (System.Exception) this.GetExceptions(includeTaskCanceledExceptions);
      if (exceptions != null)
      {
        this.UpdateExceptionObservedStatus();
        throw exceptions;
      }
    }

    internal void UpdateExceptionObservedStatus()
    {
      if (this.m_parent == null || (this.Options & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None || ((this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) != TaskCreationOptions.None || Task.InternalCurrent != this.m_parent))
        return;
      this.m_stateFlags |= 524288;
    }

    internal bool IsExceptionObservedByParent
    {
      get
      {
        return (uint) (this.m_stateFlags & 524288) > 0U;
      }
    }

    internal bool IsDelegateInvoked
    {
      get
      {
        return (uint) (this.m_stateFlags & 131072) > 0U;
      }
    }

    internal void Finish(bool bUserDelegateExecuted)
    {
      if (!bUserDelegateExecuted)
      {
        this.FinishStageTwo();
      }
      else
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null || contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
          this.FinishStageTwo();
        else
          this.AtomicStateUpdate(8388608, 23068672);
        List<Task> exceptionalChildren = contingentProperties?.m_exceptionalChildren;
        if (exceptionalChildren == null)
          return;
        lock (exceptionalChildren)
          exceptionalChildren.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
      }
    }

    internal void FinishStageTwo()
    {
      this.AddExceptionsFromChildren();
      int num;
      if (this.ExceptionRecorded)
      {
        num = 2097152;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
      {
        num = 4194304;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      else
      {
        num = 16777216;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties != null)
      {
        contingentProperties.SetCompleted();
        contingentProperties.DeregisterCancellationCallback();
      }
      this.FinishStageThree();
    }

    internal void FinishStageThree()
    {
      this.m_action = (object) null;
      if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & (int) ushort.MaxValue & 4) != 0)
        this.m_parent.ProcessChildCompletion(this);
      this.FinishContinuations();
    }

    internal void ProcessChildCompletion(Task childTask)
    {
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
      {
        if (contingentProperties.m_exceptionalChildren == null)
          Interlocked.CompareExchange<List<Task>>(ref contingentProperties.m_exceptionalChildren, new List<Task>(), (List<Task>) null);
        List<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
        if (exceptionalChildren != null)
        {
          lock (exceptionalChildren)
            exceptionalChildren.Add(childTask);
        }
      }
      if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) != 0)
        return;
      this.FinishStageTwo();
    }

    internal void AddExceptionsFromChildren()
    {
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      List<Task> exceptionalChildren = contingentProperties?.m_exceptionalChildren;
      if (exceptionalChildren == null)
        return;
      lock (exceptionalChildren)
      {
        foreach (Task task in exceptionalChildren)
        {
          if (task.IsFaulted && !task.IsExceptionObservedByParent)
            this.AddException((object) task.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, (System.Exception) null));
        }
      }
      contingentProperties.m_exceptionalChildren = (List<Task>) null;
    }

    internal void FinishThreadAbortedTask(bool bTAEAddedToExceptionHolder, bool delegateRan)
    {
      if (bTAEAddedToExceptionHolder)
        this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
      if (!this.AtomicStateUpdate(134217728, 157286400))
        return;
      this.Finish(delegateRan);
    }

    private void Execute()
    {
      if (this.IsSelfReplicatingRoot)
      {
        Task.ExecuteSelfReplicating(this);
      }
      else
      {
        try
        {
          this.InnerInvoke();
        }
        catch (ThreadAbortException ex)
        {
          if (this.IsChildReplica)
            return;
          this.HandleException((System.Exception) ex);
          this.FinishThreadAbortedTask(true, true);
        }
        catch (System.Exception ex)
        {
          this.HandleException(ex);
        }
      }
    }

    internal virtual bool ShouldReplicate()
    {
      return true;
    }

    internal virtual Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
    {
      return new Task((Delegate) taskReplicaDelegate, stateObject, parentTask, new CancellationToken(), creationOptionsForReplica, internalOptionsForReplica, parentTask.ExecutingTaskScheduler);
    }

    internal virtual object SavedStateForNextReplica
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    internal virtual object SavedStateFromPreviousReplica
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    internal virtual Task HandedOverChildReplica
    {
      get
      {
        return (Task) null;
      }
      set
      {
      }
    }

    private static void ExecuteSelfReplicating(Task root)
    {
      TaskCreationOptions creationOptionsForReplicas = root.CreationOptions | TaskCreationOptions.AttachedToParent;
      InternalTaskOptions internalOptionsForReplicas = InternalTaskOptions.ChildReplica | InternalTaskOptions.SelfReplicating | InternalTaskOptions.QueuedByRuntime;
      bool replicasAreQuitting = false;
      Action<object> taskReplicaDelegate = (Action<object>) null;
      taskReplicaDelegate = (Action<object>) (_param1 =>
      {
        Task internalCurrent = Task.InternalCurrent;
        Task task = internalCurrent.HandedOverChildReplica;
        if (task == null)
        {
          if (!root.ShouldReplicate() || Volatile.Read(ref replicasAreQuitting))
            return;
          ExecutionContext capturedContext = root.CapturedContext;
          task = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
          task.CapturedContext = Task.CopyExecutionContext(capturedContext);
          task.ScheduleAndStart(false);
        }
        try
        {
          root.InnerInvokeWithArg(internalCurrent);
        }
        catch (System.Exception ex)
        {
          root.HandleException(ex);
          if (ex is ThreadAbortException)
            internalCurrent.FinishThreadAbortedTask(false, true);
        }
        object stateForNextReplica = internalCurrent.SavedStateForNextReplica;
        if (stateForNextReplica != null)
        {
          Task replicaTask = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
          ExecutionContext capturedContext = root.CapturedContext;
          replicaTask.CapturedContext = Task.CopyExecutionContext(capturedContext);
          replicaTask.HandedOverChildReplica = task;
          replicaTask.SavedStateFromPreviousReplica = stateForNextReplica;
          replicaTask.ScheduleAndStart(false);
        }
        else
        {
          replicasAreQuitting = true;
          try
          {
            task.InternalCancel(true);
          }
          catch (System.Exception ex)
          {
            root.HandleException(ex);
          }
        }
      });
      taskReplicaDelegate((object) null);
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.ExecuteWorkItem()
    {
      this.ExecuteEntry(false);
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
    {
      if (this.IsCompleted)
        return;
      this.HandleException((System.Exception) tae);
      this.FinishThreadAbortedTask(true, false);
    }

    [SecuritySafeCritical]
    internal bool ExecuteEntry(bool bPreventDoubleExecution)
    {
      if (bPreventDoubleExecution || (this.Options & (TaskCreationOptions) 2048) != TaskCreationOptions.None)
      {
        int oldFlags = 0;
        if (!this.AtomicStateUpdate(131072, 23199744, ref oldFlags) && (oldFlags & 4194304) == 0)
          return false;
      }
      else
        this.m_stateFlags |= 131072;
      if (!this.IsCancellationRequested && !this.IsCanceled)
        this.ExecuteWithThreadLocal(ref Task.t_currentTask);
      else if (!this.IsCanceled && (Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304) & 4194304) == 0)
        this.CancellationCleanupLogic();
      return true;
    }

    [SecurityCritical]
    private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
    {
      Task task = currentTaskSlot;
      TplEtwProvider log = TplEtwProvider.Log;
      Guid oldActivityThatWillContinue = new Guid();
      bool flag = log.IsEnabled();
      if (flag)
      {
        if (log.TasksSetActivityIds)
          EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.Id), out oldActivityThatWillContinue);
        if (task != null)
          log.TaskStarted(task.m_taskScheduler.Id, task.Id, this.Id);
        else
          log.TaskStarted(TaskScheduler.Current.Id, 0, this.Id);
      }
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.Execution);
      try
      {
        currentTaskSlot = this;
        ExecutionContext capturedContext = this.CapturedContext;
        if (capturedContext == null)
        {
          this.Execute();
        }
        else
        {
          if (this.IsSelfReplicatingRoot || this.IsChildReplica)
            this.CapturedContext = Task.CopyExecutionContext(capturedContext);
          ContextCallback callback = Task.s_ecCallback;
          if (callback == null)
            Task.s_ecCallback = callback = new ContextCallback(Task.ExecutionContextCallback);
          ExecutionContext.Run(capturedContext, callback, (object) this, true);
        }
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
        this.Finish(true);
      }
      finally
      {
        currentTaskSlot = task;
        if (flag)
        {
          if (task != null)
            log.TaskCompleted(task.m_taskScheduler.Id, task.Id, this.Id, this.IsFaulted);
          else
            log.TaskCompleted(TaskScheduler.Current.Id, 0, this.Id, this.IsFaulted);
          if (log.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
        }
      }
    }

    [SecurityCritical]
    private static void ExecutionContextCallback(object obj)
    {
      (obj as Task).Execute();
    }

    internal virtual void InnerInvoke()
    {
      Action action1 = this.m_action as Action;
      if (action1 != null)
      {
        action1();
      }
      else
      {
        Action<object> action2 = this.m_action as Action<object>;
        if (action2 == null)
          return;
        action2(this.m_stateObject);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    internal void InnerInvokeWithArg(Task childTask)
    {
      this.InnerInvoke();
    }

    private void HandleException(System.Exception unhandledException)
    {
      OperationCanceledException canceledException = unhandledException as OperationCanceledException;
      if (canceledException != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == canceledException.CancellationToken)
      {
        this.SetCancellationAcknowledged();
        this.AddException((object) canceledException, true);
      }
      else
        this.AddException((object) unhandledException);
    }

    /// <summary>
    ///   Получает объект типа awaiter, используемый для данного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <returns>Экземпляр объекта типа awaiter.</returns>
    [__DynamicallyInvokable]
    public TaskAwaiter GetAwaiter()
    {
      return new TaskAwaiter(this);
    }

    /// <summary>
    ///   Настраивает объект типа awaiter, используемый для данного объекта <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="continueOnCapturedContext">
    ///   Значение <see langword="true" />, чтобы попытаться выполнить маршалинг продолжения обратно в исходный захваченный контекст; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Объект, используемый для ожидания данной задачи.</returns>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
    {
      return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
    }

    [SecurityCritical]
    internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext, ref StackCrawlMark stackMark)
    {
      TaskContinuation taskContinuation = (TaskContinuation) null;
      if (continueOnCapturedContext)
      {
        SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
        if (currentNoFlow != null && currentNoFlow.GetType() != typeof (SynchronizationContext))
        {
          taskContinuation = (TaskContinuation) new SynchronizationContextAwaitTaskContinuation(currentNoFlow, continuationAction, flowExecutionContext, ref stackMark);
        }
        else
        {
          TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
          if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
            taskContinuation = (TaskContinuation) new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext, ref stackMark);
        }
      }
      if (taskContinuation == null & flowExecutionContext)
        taskContinuation = (TaskContinuation) new AwaitTaskContinuation(continuationAction, true, ref stackMark);
      if (taskContinuation != null)
      {
        if (this.AddTaskContinuation((object) taskContinuation, false))
          return;
        taskContinuation.Run(this, false);
      }
      else
      {
        if (this.AddTaskContinuation((object) continuationAction, false))
          return;
        AwaitTaskContinuation.UnsafeScheduleAction(continuationAction, this);
      }
    }

    /// <summary>
    ///   Создает поддерживающий ожидание объект задачи, который асинхронным образом выдает возврат текущему контексту, когда его ожидают.
    /// </summary>
    /// <returns>
    ///   Контекст, который при ожидании будет асинхронно переходить назад в текущий контекст во время ожидания.
    ///    Если текущий <see cref="T:System.Threading.SynchronizationContext" /> отличен от null, он также обрабатывается как текущий контекст.
    ///    В противном случае планировщик задач, связанный с задачей, выполняющейся в данный момент, рассматривается как текущий контекст.
    /// </returns>
    [__DynamicallyInvokable]
    public static YieldAwaitable Yield()
    {
      return new YieldAwaitable();
    }

    /// <summary>
    ///   Ожидает завершения выполнения задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит объект <see cref="T:System.Threading.Tasks.TaskCanceledException" />.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения задачи возникло исключение.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит сведения об исключении или исключениях.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>
    ///   Ожидает завершения выполнения задач <see cref="T:System.Threading.Tasks.Task" /> в течение указанного временного периода.
    /// </summary>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Threading.Tasks.Task" /> завершил выполнение в течение отведенного времени; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое представляет неограниченное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит объект <see cref="T:System.Threading.Tasks.TaskCanceledException" />.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения задачи возникло исключение.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит сведения об исключении или исключениях.
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
    ///   Ожидает завершения выполнения задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Ожидание завершается, если токен отмены отменяется до завершения задачи.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Токен отмены, который нужно контролировать во время ожидания выполнения задачи.
    /// </param>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Задача была удалена.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит объект <see cref="T:System.Threading.Tasks.TaskCanceledException" />.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения задачи возникло исключение.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит сведения об исключении или исключениях.
    /// </exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>
    ///   Ожидает завершения задачи <see cref="T:System.Threading.Tasks.Task" /> в течение указанного числа миллисекунд.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Threading.Tasks.Task" /> завершил выполнение в течение отведенного времени; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит объект <see cref="T:System.Threading.Tasks.TaskCanceledException" />.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения задачи возникло исключение.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит сведения об исключении или исключениях.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Ожидает завершения выполнения задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Ожидание завершается, если время ожидания истекает или токен отмены отменяется до завершения задачи.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который нужно контролировать во время ожидания выполнения задачи.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Threading.Tasks.Task" /> завершил выполнение в течение отведенного времени; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Задача отменена.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит объект <see cref="T:System.Threading.Tasks.TaskCanceledException" />.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения задачи возникло исключение.
    ///    Коллекция <see cref="P:System.AggregateException.InnerExceptions" /> содержит сведения об исключении или исключениях.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout));
      if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
        return true;
      if (!this.InternalWait(millisecondsTimeout, cancellationToken))
        return false;
      if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
      {
        this.NotifyDebuggerOfWaitCompletionIfNecessary();
        if (this.IsCanceled)
          cancellationToken.ThrowIfCancellationRequested();
        this.ThrowIfExceptional(true);
      }
      return true;
    }

    private bool WrappedTryRunInline()
    {
      if (this.m_taskScheduler == null)
        return false;
      try
      {
        return this.m_taskScheduler.TryRunInline(this, true);
      }
      catch (System.Exception ex)
      {
        if (!(ex is ThreadAbortException))
          throw new TaskSchedulerException(ex);
        throw;
      }
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      TplEtwProvider log = TplEtwProvider.Log;
      bool flag1 = log.IsEnabled();
      if (flag1)
      {
        Task internalCurrent = Task.InternalCurrent;
        log.TaskWaitBegin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, this.Id, TplEtwProvider.TaskWaitBehavior.Synchronous, 0, Thread.GetDomainID());
      }
      bool flag2 = this.IsCompleted;
      if (!flag2)
      {
        Debugger.NotifyOfCrossThreadDependency();
        flag2 = millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && (this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken);
      }
      if (flag1)
      {
        Task internalCurrent = Task.InternalCurrent;
        if (internalCurrent != null)
          log.TaskWaitEnd(internalCurrent.m_taskScheduler.Id, internalCurrent.Id, this.Id);
        else
          log.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
        log.TaskWaitContinuationComplete(this.Id);
      }
      return flag2;
    }

    private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      bool flag1 = millisecondsTimeout == -1;
      uint num1 = flag1 ? 0U : (uint) Environment.TickCount;
      bool flag2 = this.SpinWait(millisecondsTimeout);
      if (!flag2)
      {
        Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
        try
        {
          this.AddCompletionAction((ITaskCompletionAction) setOnInvokeMres, true);
          if (flag1)
          {
            flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
          }
          else
          {
            uint num2 = (uint) Environment.TickCount - num1;
            if ((long) num2 < (long) millisecondsTimeout)
              flag2 = setOnInvokeMres.Wait((int) ((long) millisecondsTimeout - (long) num2), cancellationToken);
          }
        }
        finally
        {
          if (!this.IsCompleted)
            this.RemoveContinuation((object) setOnInvokeMres);
        }
      }
      return flag2;
    }

    private bool SpinWait(int millisecondsTimeout)
    {
      if (this.IsCompleted)
        return true;
      if (millisecondsTimeout == 0)
        return false;
      int num = PlatformHelper.IsSingleProcessor ? 1 : 10;
      for (int index = 0; index < num; ++index)
      {
        if (this.IsCompleted)
          return true;
        if (index == num / 2)
          Thread.Yield();
        else
          Thread.SpinWait(4 << index);
      }
      return this.IsCompleted;
    }

    [SecuritySafeCritical]
    internal bool InternalCancel(bool bCancelNonExecutingOnly)
    {
      bool flag1 = false;
      bool flag2 = false;
      TaskSchedulerException schedulerException = (TaskSchedulerException) null;
      if ((this.m_stateFlags & 65536) != 0)
      {
        TaskScheduler taskScheduler = this.m_taskScheduler;
        try
        {
          flag1 = taskScheduler != null && taskScheduler.TryDequeue(this);
        }
        catch (System.Exception ex)
        {
          if (!(ex is ThreadAbortException))
            schedulerException = new TaskSchedulerException(ex);
        }
        bool flag3 = taskScheduler != null && taskScheduler.RequiresAtomicStartTransition || (uint) (this.Options & (TaskCreationOptions) 2048) > 0U;
        if (!flag1 & bCancelNonExecutingOnly & flag3)
          flag2 = this.AtomicStateUpdate(4194304, 4325376);
      }
      if (!bCancelNonExecutingOnly | flag1 | flag2)
      {
        this.RecordInternalCancellationRequest();
        if (flag1)
          flag2 = this.AtomicStateUpdate(4194304, 4325376);
        else if (!flag2 && (this.m_stateFlags & 65536) == 0)
          flag2 = this.AtomicStateUpdate(4194304, 23265280);
        if (flag2)
          this.CancellationCleanupLogic();
      }
      if (schedulerException != null)
        throw schedulerException;
      return flag2;
    }

    internal void RecordInternalCancellationRequest()
    {
      this.EnsureContingentPropertiesInitialized(true).m_internalCancellationRequested = 1;
    }

    internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
    {
      this.RecordInternalCancellationRequest();
      if (!(tokenToRecord != new CancellationToken()))
        return;
      this.m_contingentProperties.m_cancellationToken = tokenToRecord;
    }

    internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
    {
      this.RecordInternalCancellationRequest(tokenToRecord);
      if (cancellationException == null)
        return;
      this.AddException(cancellationException, true);
    }

    internal void CancellationCleanupLogic()
    {
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties != null)
      {
        contingentProperties.SetCompleted();
        contingentProperties.DeregisterCancellationCallback();
      }
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
      if (Task.s_asyncDebuggingEnabled)
        Task.RemoveFromActiveTasks(this.Id);
      this.FinishStageThree();
    }

    private void SetCancellationAcknowledged()
    {
      this.m_stateFlags |= 1048576;
    }

    [SecuritySafeCritical]
    internal void FinishContinuations()
    {
      object Object1 = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
      TplEtwProvider.Log.RunningContinuation(this.Id, Object1);
      if (Object1 == null)
        return;
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.CompletionNotification);
      bool flag = (this.m_stateFlags & 134217728) == 0 && Thread.CurrentThread.ThreadState != ThreadState.AbortRequested && (this.m_stateFlags & 64) == 0;
      Action action1 = Object1 as Action;
      if (action1 != null)
      {
        AwaitTaskContinuation.RunOrScheduleAction(action1, flag, ref Task.t_currentTask);
        this.LogFinishCompletionNotification();
      }
      else
      {
        ITaskCompletionAction action2 = Object1 as ITaskCompletionAction;
        if (action2 != null)
        {
          if (flag)
            action2.Invoke(this);
          else
            ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) new CompletionActionInvoker(action2, this), false);
          this.LogFinishCompletionNotification();
        }
        else
        {
          TaskContinuation taskContinuation1 = Object1 as TaskContinuation;
          if (taskContinuation1 != null)
          {
            taskContinuation1.Run(this, flag);
            this.LogFinishCompletionNotification();
          }
          else
          {
            List<object> objectList = Object1 as List<object>;
            if (objectList == null)
            {
              this.LogFinishCompletionNotification();
            }
            else
            {
              lock (objectList)
                ;
              int count = objectList.Count;
              for (int Index = 0; Index < count; ++Index)
              {
                StandardTaskContinuation taskContinuation2 = objectList[Index] as StandardTaskContinuation;
                if (taskContinuation2 != null && (taskContinuation2.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
                {
                  TplEtwProvider.Log.RunningContinuationList(this.Id, Index, (object) taskContinuation2);
                  objectList[Index] = (object) null;
                  taskContinuation2.Run(this, flag);
                }
              }
              for (int Index = 0; Index < count; ++Index)
              {
                object Object2 = objectList[Index];
                if (Object2 != null)
                {
                  objectList[Index] = (object) null;
                  TplEtwProvider.Log.RunningContinuationList(this.Id, Index, Object2);
                  Action action3 = Object2 as Action;
                  if (action3 != null)
                  {
                    AwaitTaskContinuation.RunOrScheduleAction(action3, flag, ref Task.t_currentTask);
                  }
                  else
                  {
                    TaskContinuation taskContinuation2 = Object2 as TaskContinuation;
                    if (taskContinuation2 != null)
                    {
                      taskContinuation2.Run(this, flag);
                    }
                    else
                    {
                      ITaskCompletionAction action4 = (ITaskCompletionAction) Object2;
                      if (flag)
                        action4.Invoke(this);
                      else
                        ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) new CompletionActionInvoker(action4, this), false);
                    }
                  }
                }
              }
              this.LogFinishCompletionNotification();
            }
          }
        }
      }
    }

    private void LogFinishCompletionNotification()
    {
      if (!AsyncCausalityTracer.LoggingOn)
        return;
      AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое получает токен отмены и выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создавший токен уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationAction" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение использует указанный планировщик.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="scheduler" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое после завершения целевой задачи в соответствии с заданными параметрами <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие для запуска в соответствии с заданными параметрами <paramref name="continuationOptions" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
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
    public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое после завершения целевой задачи в соответствии с заданными параметрами <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    ///    Продолжение получает токен отмены и использует указанный планировщик.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие для запуска в соответствии с заданными параметрами <paramref name="continuationOptions" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче продолжения.
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
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создавший токен уже был удален.
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
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromTask(this, (Delegate) continuationAction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и выполняется после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения задачи.
    ///    При запуске делегату передается в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые действием продолжения.
    /// </param>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и токен отмены и которое выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
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
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение использует указанный планировщик.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///     При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
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
    public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и выполняется после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение выполняется на основе набора указанных условий.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
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
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и токен отмены и которое выполняется после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение выполняется на основе набора указанных условий и использует указанный планировщик.
    /// </summary>
    /// <param name="continuationAction">
    ///   Действие, которое необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
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
    ///   Аргумент <paramref name="continuationAction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException(nameof (continuationAction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromTask(this, (Delegate) continuationAction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task`1" /> и возвращает значение.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task`1" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <typeparam name="TResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>Новая задача продолжения.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" /> и возвращает значение.
    ///    Продолжение получает токен отмены.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создавший токен уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" /> и возвращает значение.
    ///    Продолжение использует указанный планировщик.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scheduler" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с заданными параметрами и возвращает значение.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция для запуска в соответствии с условием, заданным в <paramref name="continuationOptions" />.
    ///    При запуске делегата завершенная задача будет передана ему в качестве аргумента.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <typeparam name="TResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuationFunction" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, выполняемое в соответствии с заданными параметрами и возвращает значение.
    ///    Продолжение получает токен отмены и использует указанный планировщик.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция для запуска в соответствии с заданным условием <paramref name="continuationOptions." />. При запуске делегату будет передана завершенная задача в качестве аргумента.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TResult">
    ///    Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.CancellationTokenSource" /> Создавший токен уже был удален.
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
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TResult> task = (Task<TResult>) new ContinuationResultTaskFromTask<TResult>(this, (Delegate) continuationFunction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    /// <summary>
    ///   Создает продолжение, которое получает предоставленные вызывающей стороной сведения о состоянии и выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />, а также возвращает значение.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" /> и возвращает значение.
    ///    Продолжение получает предоставленные вызывающей стороной сведения и токен отмены.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="T:System.Threading.CancellationToken" />, который будет назначен новой задаче продолжения.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение получает предоставленные вызывающей стороной сведения и использует указанный планировщик.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///     При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="scheduler">
    ///   Планировщик <see cref="T:System.Threading.Tasks.TaskScheduler" />, который следует связать с задачей продолжения и использовать для ее запуска.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется на основе указанных параметров продолжения токена после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" />.
    ///    Продолжение получает предоставленные вызывающей стороной сведения.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
    /// </param>
    /// <param name="continuationOptions">
    ///   Параметры, определяющие запланированное время продолжения и его поведение.
    ///    Включаются критерии, такие как <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, а также параметры выполнения, например <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>
    ///   Создает продолжение, которое выполняется на основе указанных параметров продолжения токена после завершения целевой задачи <see cref="T:System.Threading.Tasks.Task" /> и возвращает значение.
    ///    Продолжение получает предоставленные вызывающей стороной сведения и токен отмены, а также использует указанный планировщик.
    /// </summary>
    /// <param name="continuationFunction">
    ///   Функция, которую необходимо выполнить после завершения <see cref="T:System.Threading.Tasks.Task" />.
    ///    При запуске делегату будут переданы в качестве аргументов завершенная задача и предоставленный вызывающей стороной объект состояния.
    /// </param>
    /// <param name="state">
    ///   Объект, который представляет данные, используемые функцией продолжения.
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
    /// <typeparam name="TResult">
    ///   Тип результата, созданного продолжением.
    /// </typeparam>
    /// <returns>
    ///   Новое продолжение <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="continuationFunction" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="continuationOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="scheduler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный <see cref="T:System.Threading.CancellationToken" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException(nameof (continuationFunction));
      if (scheduler == null)
        throw new ArgumentNullException(nameof (scheduler));
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TResult> task = (Task<TResult>) new ContinuationResultTaskFromTask<TResult>(this, (Delegate) continuationFunction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
    {
      TaskContinuationOptions continuationOptions1 = TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.NotOnRanToCompletion;
      TaskContinuationOptions continuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
      TaskContinuationOptions continuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
      if ((continuationOptions & continuationOptions3) == continuationOptions3)
        throw new ArgumentOutOfRangeException(nameof (continuationOptions), Environment.GetResourceString("Task_ContinueWith_ESandLR"));
      if ((continuationOptions & ~(continuationOptions2 | continuationOptions1 | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException(nameof (continuationOptions));
      if ((continuationOptions & continuationOptions1) == continuationOptions1)
        throw new ArgumentOutOfRangeException(nameof (continuationOptions), Environment.GetResourceString("Task_ContinueWith_NotOnAnything"));
      creationOptions = (TaskCreationOptions) (continuationOptions & continuationOptions2);
      internalOptions = InternalTaskOptions.ContinuationTask;
      if ((continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return;
      internalOptions |= InternalTaskOptions.LazyCancellation;
    }

    internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
    {
      TaskContinuation continuation = (TaskContinuation) new StandardTaskContinuation(continuationTask, options, scheduler);
      if (cancellationToken.CanBeCanceled)
      {
        if (this.IsCompleted || cancellationToken.IsCancellationRequested)
          continuationTask.AssignCancellationToken(cancellationToken, (Task) null, (TaskContinuation) null);
        else
          continuationTask.AssignCancellationToken(cancellationToken, this, continuation);
      }
      if (continuationTask.IsCompleted)
        return;
      if ((this.Options & (TaskCreationOptions) 1024) != TaskCreationOptions.None && !(this is ITaskCompletionAction))
      {
        TplEtwProvider log = TplEtwProvider.Log;
        if (log.IsEnabled())
          log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, Task.CurrentId ?? 0, continuationTask.Id);
      }
      if (this.AddTaskContinuation((object) continuation, false))
        return;
      continuation.Run(this, true);
    }

    internal void AddCompletionAction(ITaskCompletionAction action)
    {
      this.AddCompletionAction(action, false);
    }

    private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
    {
      if (this.AddTaskContinuation((object) action, addBeforeOthers))
        return;
      action.Invoke(this);
    }

    private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
    {
      object continuationObject1 = this.m_continuationObject;
      if (continuationObject1 != Task.s_taskCompletionSentinel && !(continuationObject1 is List<object>))
        Interlocked.CompareExchange(ref this.m_continuationObject, (object) new List<object>()
        {
          continuationObject1
        }, continuationObject1);
      List<object> continuationObject2 = this.m_continuationObject as List<object>;
      if (continuationObject2 != null)
      {
        lock (continuationObject2)
        {
          if (this.m_continuationObject != Task.s_taskCompletionSentinel)
          {
            if (continuationObject2.Count == continuationObject2.Capacity)
              continuationObject2.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
            if (addBeforeOthers)
              continuationObject2.Insert(0, tc);
            else
              continuationObject2.Add(tc);
            return true;
          }
        }
      }
      return false;
    }

    private bool AddTaskContinuation(object tc, bool addBeforeOthers)
    {
      if (this.IsCompleted)
        return false;
      if (this.m_continuationObject != null || Interlocked.CompareExchange(ref this.m_continuationObject, tc, (object) null) != null)
        return this.AddTaskContinuationComplex(tc, addBeforeOthers);
      return true;
    }

    internal void RemoveContinuation(object continuationObject)
    {
      object continuationObject1 = this.m_continuationObject;
      if (continuationObject1 == Task.s_taskCompletionSentinel)
        return;
      List<object> objectList = continuationObject1 as List<object>;
      if (objectList == null)
      {
        if (Interlocked.CompareExchange(ref this.m_continuationObject, (object) new List<object>(), continuationObject) == continuationObject)
          return;
        objectList = this.m_continuationObject as List<object>;
      }
      if (objectList == null)
        return;
      lock (objectList)
      {
        if (this.m_continuationObject == Task.s_taskCompletionSentinel)
          return;
        int index = objectList.IndexOf(continuationObject);
        if (index == -1)
          return;
        objectList[index] = (object) null;
      }
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один или несколько объектов <see cref="T:System.Threading.Tasks.Task" /> в <paramref name="tasks" /> были удалены.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент null.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="tasks" /> Аргумент — пустой массив.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   По крайней мере один из экземпляров <see cref="T:System.Threading.Tasks.Task" /> был удален.
    ///    Если задача была отменена, <see cref="T:System.AggregateException" /> исключение содержит <see cref="T:System.OperationCanceledException" /> исключение в его <see cref="P:System.AggregateException.InnerExceptions" /> коллекции.
    /// 
    ///   -или-
    /// 
    ///   Возникло исключение во время выполнения по крайней мере одного из экземпляров <see cref="T:System.Threading.Tasks.Task" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static void WaitAll(params Task[] tasks)
    {
      Task.WaitAll(tasks, -1);
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных отменяемых объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного временного интервала.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если все экземпляры <see cref="T:System.Threading.Tasks.Task" /> завершили выполнение в выделенное время; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один или несколько объектов <see cref="T:System.Threading.Tasks.Task" /> в <paramref name="tasks" /> были удалены.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   По крайней мере один из экземпляров <see cref="T:System.Threading.Tasks.Task" /> был удален.
    ///    Если задача была отменена, <see cref="T:System.AggregateException" /> содержит <see cref="T:System.OperationCanceledException" /> в коллекции <see cref="P:System.AggregateException.InnerExceptions" />.
    /// 
    ///   -или-
    /// 
    ///   Возникло исключение во время выполнения по крайней мере одного из экземпляров <see cref="T:System.Threading.Tasks.Task" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое означает бесконечное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент null.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="tasks" /> — пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout));
      return Task.WaitAll(tasks, (int) totalMilliseconds);
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного числа миллисекунд.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если все экземпляры <see cref="T:System.Threading.Tasks.Task" /> завершили выполнение в выделенное время; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один или несколько объектов <see cref="T:System.Threading.Tasks.Task" /> в <paramref name="tasks" /> были удалены.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   По крайней мере один из экземпляров <see cref="T:System.Threading.Tasks.Task" /> был удален.
    ///    Если задача была отменена, <see cref="T:System.AggregateException" /> содержит <see cref="T:System.OperationCanceledException" /> в коллекции <see cref="P:System.AggregateException.InnerExceptions" />.
    /// 
    ///   -или-
    /// 
    ///   Возникло исключение во время выполнения по крайней мере одного из экземпляров <see cref="T:System.Threading.Tasks.Task" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="tasks" /> — пустой массив.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
    {
      return Task.WaitAll(tasks, millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" />, пока ожидание не будет отменено.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который нужно контролировать во время ожидания выполнения задач.
    /// </param>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   По крайней мере один из экземпляров <see cref="T:System.Threading.Tasks.Task" /> был удален.
    ///    Если задача была отменена, <see cref="T:System.AggregateException" /> содержит <see cref="T:System.OperationCanceledException" /> в коллекции <see cref="P:System.AggregateException.InnerExceptions" />.
    /// 
    ///   -или-
    /// 
    ///   Возникло исключение во время выполнения по крайней мере одного из экземпляров <see cref="T:System.Threading.Tasks.Task" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент null.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="tasks" /> — пустой массив.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один или несколько объектов <see cref="T:System.Threading.Tasks.Task" /> в <paramref name="tasks" /> были удалены.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
    {
      Task.WaitAll(tasks, -1, cancellationToken);
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного числа миллисекунд или до отмены ожидания.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который нужно контролировать во время ожидания выполнения задач.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если все экземпляры <see cref="T:System.Threading.Tasks.Task" /> завершили выполнение в выделенное время; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Один или несколько объектов <see cref="T:System.Threading.Tasks.Task" /> в <paramref name="tasks" /> были удалены.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   По крайней мере один из экземпляров <see cref="T:System.Threading.Tasks.Task" /> был удален.
    ///    Если задача была отменена, <see cref="T:System.AggregateException" /> содержит <see cref="T:System.OperationCanceledException" /> в коллекции <see cref="P:System.AggregateException.InnerExceptions" />.
    /// 
    ///   -или-
    /// 
    ///   Возникло исключение во время выполнения по крайней мере одного из экземпляров <see cref="T:System.Threading.Tasks.Task" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// 
    ///   -или-
    /// 
    ///   Аргумент <paramref name="tasks" /> — пустой массив.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout));
      cancellationToken.ThrowIfCancellationRequested();
      List<System.Exception> exceptions = (List<System.Exception>) null;
      List<Task> list1 = (List<Task>) null;
      List<Task> list2 = (List<Task>) null;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = true;
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), nameof (tasks));
        bool flag4 = task.IsCompleted;
        if (!flag4)
        {
          if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
          {
            Task.AddToList<Task>(task, ref list1, tasks.Length);
          }
          else
          {
            flag4 = task.WrappedTryRunInline() && task.IsCompleted;
            if (!flag4)
              Task.AddToList<Task>(task, ref list1, tasks.Length);
          }
        }
        if (flag4)
        {
          if (task.IsFaulted)
            flag1 = true;
          else if (task.IsCanceled)
            flag2 = true;
          if (task.IsWaitNotificationEnabled)
            Task.AddToList<Task>(task, ref list2, 1);
        }
      }
      if (list1 != null)
      {
        flag3 = Task.WaitAllBlockingCore(list1, millisecondsTimeout, cancellationToken);
        if (flag3)
        {
          foreach (Task task in list1)
          {
            if (task.IsFaulted)
              flag1 = true;
            else if (task.IsCanceled)
              flag2 = true;
            if (task.IsWaitNotificationEnabled)
              Task.AddToList<Task>(task, ref list2, 1);
          }
        }
        GC.KeepAlive((object) tasks);
      }
      if (flag3 && list2 != null)
      {
        foreach (Task task in list2)
        {
          if (task.NotifyDebuggerOfWaitCompletionIfNecessary())
            break;
        }
      }
      if (flag3 && flag1 | flag2)
      {
        if (!flag1)
          cancellationToken.ThrowIfCancellationRequested();
        foreach (Task task in tasks)
          Task.AddExceptionsForCompletedTask(ref exceptions, task);
        throw new AggregateException((IEnumerable<System.Exception>) exceptions);
      }
      return flag3;
    }

    private static void AddToList<T>(T item, ref List<T> list, int initSize)
    {
      if (list == null)
        list = new List<T>(initSize);
      list.Add(item);
    }

    private static bool WaitAllBlockingCore(List<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      bool flag = false;
      Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
      try
      {
        foreach (Task task in tasks)
          task.AddCompletionAction((ITaskCompletionAction) setOnCountdownMres, true);
        flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
        return flag;
      }
      finally
      {
        if (!flag)
        {
          foreach (Task task in tasks)
          {
            if (!task.IsCompleted)
              task.RemoveContinuation((object) setOnCountdownMres);
          }
        }
      }
    }

    internal static void FastWaitAll(Task[] tasks)
    {
      List<System.Exception> exceptions = (List<System.Exception>) null;
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        if (!tasks[index].IsCompleted)
          tasks[index].WrappedTryRunInline();
      }
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        Task task = tasks[index];
        task.SpinThenBlockingWait(-1, new CancellationToken());
        Task.AddExceptionsForCompletedTask(ref exceptions, task);
      }
      if (exceptions != null)
        throw new AggregateException((IEnumerable<System.Exception>) exceptions);
    }

    internal static void AddExceptionsForCompletedTask(ref List<System.Exception> exceptions, Task t)
    {
      AggregateException exceptions1 = t.GetExceptions(true);
      if (exceptions1 == null)
        return;
      t.UpdateExceptionObservedStatus();
      if (exceptions == null)
        exceptions = new List<System.Exception>(exceptions1.InnerExceptions.Count);
      exceptions.AddRange((IEnumerable<System.Exception>) exceptions1.InnerExceptions);
    }

    /// <summary>
    ///   Ожидает завершения выполнения любого из указанных объектов <see cref="T:System.Threading.Tasks.Task" />.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <returns>
    ///   Индекс завершенного объекта <see cref="T:System.Threading.Tasks.Task" /> в массиве <paramref name="tasks" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(params Task[] tasks)
    {
      return Task.WaitAny(tasks, -1);
    }

    /// <summary>
    ///   Ожидает завершения выполнения любого из указанных отменяемых объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного временного интервала.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="timeout">
    ///   Период <see cref="T:System.TimeSpan" />, представляющий время ожидания в миллисекундах, или период <see cref="T:System.TimeSpan" />, представляющий -1 миллисекунду для неограниченного ожидания.
    /// </param>
    /// <returns>
    ///   Индекс завершенной задачи в аргументе-массиве <paramref name="tasks" /> или -1, если истекло время ожидания.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="timeout" /> является отрицательным числом, отличным от -1 миллисекунды, которое означает бесконечное время ожидания.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="timeout" /> больше значения <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout));
      return Task.WaitAny(tasks, (int) totalMilliseconds);
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" />, пока ожидание не будет отменено.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который нужно контролировать во время ожидания выполнения задачи.
    /// </param>
    /// <returns>
    ///   Индекс завершенной задачи в аргументе-массиве <paramref name="tasks" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
    {
      return Task.WaitAny(tasks, -1, cancellationToken);
    }

    /// <summary>
    ///   Ожидает завершения выполнения любого из указанных объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного числа миллисекунд.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Индекс завершенной задачи в аргументе-массиве <paramref name="tasks" /> или -1, если истекло время ожидания.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, int millisecondsTimeout)
    {
      return Task.WaitAny(tasks, millisecondsTimeout, new CancellationToken());
    }

    /// <summary>
    ///   Ожидает завершения выполнения всех указанных объектов <see cref="T:System.Threading.Tasks.Task" /> в течение указанного числа миллисекунд или до отмены токена отмены.
    /// </summary>
    /// <param name="tasks">
    ///   Массив экземпляров <see cref="T:System.Threading.Tasks.Task" />, завершения выполнения которых следует дождаться.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или функция <see cref="F:System.Threading.Timeout.Infinite" /> (-1) в случае неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />, который нужно контролировать во время ожидания выполнения задачи.
    /// </param>
    /// <returns>
    ///   Индекс завершенной задачи в аргументе-массиве <paramref name="tasks" /> или -1, если истекло время ожидания.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="millisecondsTimeout" /> является отрицательным числом, отличным от –1, что означает бесконечное время ожидания.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="tasks" /> содержит элемент NULL.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Объект <paramref name="cancellationToken" /> отменен.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout));
      cancellationToken.ThrowIfCancellationRequested();
      int num = -1;
      for (int index = 0; index < tasks.Length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), nameof (tasks));
        if (num == -1 && task.IsCompleted)
          num = index;
      }
      if (num == -1 && tasks.Length != 0)
      {
        Task<Task> task = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
        if (task.Wait(millisecondsTimeout, cancellationToken))
          num = Array.IndexOf<Task>(tasks, task.Result);
      }
      GC.KeepAlive((object) tasks);
      return num;
    }

    /// <summary>
    ///   Создает задачу <see cref="T:System.Threading.Tasks.Task`1" />, которая завершается удачно с указанным результатом.
    /// </summary>
    /// <param name="result">
    ///   Результат, сохраняемый в завершенную задачу.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного задачей.
    /// </typeparam>
    /// <returns>Успешно завершенная задача.</returns>
    [__DynamicallyInvokable]
    public static Task<TResult> FromResult<TResult>(TResult result)
    {
      return new Task<TResult>(result);
    }

    /// <summary>
    ///   Создает задачу <see cref="T:System.Threading.Tasks.Task" />, которая завершилась с указанным исключением.
    /// </summary>
    /// <param name="exception">
    ///   Исключение, с которым завершается задача.
    /// </param>
    /// <returns>Задача, завершившаяся сбоем.</returns>
    [__DynamicallyInvokable]
    public static Task FromException(System.Exception exception)
    {
      return (Task) Task.FromException<VoidTaskResult>(exception);
    }

    /// <summary>
    ///   Создает задачу <see cref="T:System.Threading.Tasks.Task`1" />, которая завершилась с указанным исключением.
    /// </summary>
    /// <param name="exception">
    ///   Исключение, с которым завершается задача.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного задачей.
    /// </typeparam>
    /// <returns>Задача, завершившаяся сбоем.</returns>
    [__DynamicallyInvokable]
    public static Task<TResult> FromException<TResult>(System.Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      Task<TResult> task = new Task<TResult>();
      task.TrySetException((object) exception);
      return task;
    }

    [FriendAccessAllowed]
    internal static Task FromCancellation(CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        throw new ArgumentOutOfRangeException(nameof (cancellationToken));
      return new Task(true, TaskCreationOptions.None, cancellationToken);
    }

    /// <summary>
    ///   Создает задачу <see cref="T:System.Threading.Tasks.Task" />, которая завершилась из-за отмены с помощью указанного маркера отмены.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Маркер отмены, с которым завершается задача.
    /// </param>
    /// <returns>Отменяемая задача.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Не запрошена отмена для параметра <paramref name="cancellationToken" />; его свойство <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> имеет значение <see langword="false" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task FromCanceled(CancellationToken cancellationToken)
    {
      return Task.FromCancellation(cancellationToken);
    }

    [FriendAccessAllowed]
    internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        throw new ArgumentOutOfRangeException(nameof (cancellationToken));
      return new Task<TResult>(true, default (TResult), TaskCreationOptions.None, cancellationToken);
    }

    /// <summary>
    ///   Создает задачу <see cref="T:System.Threading.Tasks.Task`1" />, которая завершилась из-за отмены с помощью указанного маркера отмены.
    /// </summary>
    /// <param name="cancellationToken">
    ///   Маркер отмены, с которым завершается задача.
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного задачей.
    /// </typeparam>
    /// <returns>Отменяемая задача.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Не запрошена отмена для параметра <paramref name="cancellationToken" />; его свойство <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> имеет значение <see langword="false" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
    {
      return Task.FromCancellation<TResult>(cancellationToken);
    }

    internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      Task<TResult> task = new Task<TResult>();
      task.TrySetCanceled(exception.CancellationToken, (object) exception);
      return task;
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает объект <see cref="T:System.Threading.Tasks.Task" />, представляющий эту работу.
    /// </summary>
    /// <param name="action">Работа для асинхронного выполнения</param>
    /// <returns>
    ///   Задача, которая представляет работу в очереди на выполнение в ThreadPool.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="action" /> имел значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task Run(Action action)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew((Task) null, (Delegate) action, (object) null, new CancellationToken(), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает объект <see cref="T:System.Threading.Tasks.Task" />, представляющий эту работу.
    ///    Токен отмены позволяет отменить работу.
    /// </summary>
    /// <param name="action">Работа для асинхронного выполнения</param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который может использоваться для отмены работы.
    /// </param>
    /// <returns>
    ///   Задача, которая представляет работу в очереди на выполнение в пуле потоков.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="action" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с <paramref name="cancellationToken" />, был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task Run(Action action, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew((Task) null, (Delegate) action, (object) null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает объект <see cref="T:System.Threading.Tasks.Task`1" />, представляющий эту работу.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения.</param>
    /// <typeparam name="TResult">Возвращаемый тип задачи.</typeparam>
    /// <returns>
    ///   Объект задачи, представляющий работу в очереди на выполнение в пуле потоков.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TResult> Run<TResult>(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew((Task) null, function, new CancellationToken(), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackMark);
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает объект <see langword="Task(TResult)" />, представляющий эту работу.
    ///    Токен отмены позволяет отменить работу.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения</param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который должен использоваться для отмены работы
    /// </param>
    /// <typeparam name="TResult">Тип результата задачи.</typeparam>
    /// <returns>
    ///   Задача <see langword="Task(TResult)" />, которая представляет работу в очереди на выполнение в пуле потоков.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с <paramref name="cancellationToken" />, был удален.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew((Task) null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackMark);
    }

    /// <summary>
    ///   Ставит в очередь указанную работу для запуска в пуле потоков и возвращает прокси для задачи, возвращаемой функцией <paramref name="function" />.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения</param>
    /// <returns>
    ///   Задача, которая представляет прокси для задачи, возвращаемой <paramref name="function" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имел значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Run(Func<Task> function)
    {
      return Task.Run(function, new CancellationToken());
    }

    /// <summary>
    ///   Ставит в очередь указанную работу для запуска в пуле потоков и возвращает прокси для задачи, возвращаемой функцией <paramref name="function" />.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения.</param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который должен использоваться для отмены работы.
    /// </param>
    /// <returns>
    ///   Задача, которая представляет прокси для задачи, возвращаемой <paramref name="function" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с <paramref name="cancellationToken" />, был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Run(Func<Task> function, CancellationToken cancellationToken)
    {
      if (function == null)
        throw new ArgumentNullException(nameof (function));
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        cancellationToken.ThrowIfSourceDisposed();
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      return (Task) new UnwrapPromise<VoidTaskResult>((Task) Task<Task>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает прокси для задачи <see langword="Task(TResult)" />, возвращаемой <paramref name="function" />.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения</param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного задачей прокси-сервера.
    /// </typeparam>
    /// <returns>
    ///   Объект <see langword="Task(TResult)" />, представляющий прокси для объекта <see langword="Task(TResult)" />, возвращаемого <paramref name="function" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имел значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
    {
      return Task.Run<TResult>(function, new CancellationToken());
    }

    /// <summary>
    ///   Ставит в очередь заданную работу для запуска в пуле потоков и возвращает прокси для задачи <see langword="Task(TResult)" />, возвращаемой <paramref name="function" />.
    /// </summary>
    /// <param name="function">Работа для асинхронного выполнения</param>
    /// <param name="cancellationToken">
    ///   Токен отмены, который должен использоваться для отмены работы
    /// </param>
    /// <typeparam name="TResult">
    ///   Тип результата, возвращенного задачей прокси-сервера.
    /// </typeparam>
    /// <returns>
    ///   Объект <see langword="Task(TResult)" />, представляющий прокси для объекта <see langword="Task(TResult)" />, возвращаемого <paramref name="function" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="function" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.Threading.CancellationTokenSource" />, связанный с <paramref name="cancellationToken" />, был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
    {
      if (function == null)
        throw new ArgumentNullException(nameof (function));
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        cancellationToken.ThrowIfSourceDisposed();
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<TResult>(cancellationToken);
      return (Task<TResult>) new UnwrapPromise<TResult>((Task) Task<Task<TResult>>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
    }

    /// <summary>
    ///   Создает задачу, которая завершается через заданное время.
    /// </summary>
    /// <param name="delay">
    ///   Интервал времени, в течение которого ожидается завершение возвращаемой задачи, или <see langword="TimeSpan.FromMilliseconds(-1)" /> для неограниченного времени ожидания.
    /// </param>
    /// <returns>Задача, представляющая временную задержку.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="delay" /> представляет отрицательный интервал времени, отличные от <see langword="TimeSpan.FromMillseconds(-1)" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="delay" /> Аргумента <see cref="P:System.TimeSpan.TotalMilliseconds" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Delay(TimeSpan delay)
    {
      return Task.Delay(delay, new CancellationToken());
    }

    /// <summary>
    ///   Создает отменяемую задачу, которая завершается через заданное время.
    /// </summary>
    /// <param name="delay">
    ///   Интервал времени, в течение которого ожидается завершение возвращаемой задачи, или <see langword="TimeSpan.FromMilliseconds(-1)" /> для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, проверяемый до завершения возвращаемой задачи.
    /// </param>
    /// <returns>Задача, представляющая временную задержку.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="delay" /> представляет отрицательный интервал времени, отличные от <see langword="TimeSpan.FromMillseconds(-1)" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="delay" /> Аргумента <see cref="P:System.TimeSpan.TotalMilliseconds" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <paramref name="cancellationToken" /> уже удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
    {
      long totalMilliseconds = (long) delay.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (delay), Environment.GetResourceString("Task_Delay_InvalidDelay"));
      return Task.Delay((int) totalMilliseconds, cancellationToken);
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена после некоторой временной задержки.
    /// </summary>
    /// <param name="millisecondsDelay">
    ///   Число миллисекунд, в течение которого ожидается завершение возвращаемой задачи, или -1 для неограниченного времени ожидания.
    /// </param>
    /// <returns>Задача, представляющая временную задержку.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsDelay" /> Аргумент — меньше -1.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Delay(int millisecondsDelay)
    {
      return Task.Delay(millisecondsDelay, new CancellationToken());
    }

    /// <summary>
    ///   Создает отменяемую задачу, которая будет выполнена после некоторой временной задержки.
    /// </summary>
    /// <param name="millisecondsDelay">
    ///   Число миллисекунд, в течение которого ожидается завершение возвращаемой задачи, или -1 для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    ///   Токен отмены, проверяемый до завершения возвращаемой задачи.
    /// </param>
    /// <returns>Задача, представляющая временную задержку.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="millisecondsDelay" /> Аргумент — меньше -1.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача была отменена.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Предоставленный объект <paramref name="cancellationToken" /> уже удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
    {
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsDelay), Environment.GetResourceString("Task_Delay_InvalidMillisecondsDelay"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (millisecondsDelay == 0)
        return Task.CompletedTask;
      Task.DelayPromise delayPromise1 = new Task.DelayPromise(cancellationToken);
      if (cancellationToken.CanBeCanceled)
      {
        Task.DelayPromise delayPromise2 = delayPromise1;
        ref CancellationToken local = ref cancellationToken;
        Task.DelayPromise delayPromise3 = delayPromise1;
        CancellationTokenRegistration tokenRegistration = local.InternalRegisterWithoutEC((Action<object>) (state => ((Task.DelayPromise) state).Complete()), (object) delayPromise3);
        delayPromise2.Registration = tokenRegistration;
      }
      if (millisecondsDelay != -1)
      {
        delayPromise1.Timer = new Timer((TimerCallback) (state => ((Task.DelayPromise) state).Complete()), (object) delayPromise1, millisecondsDelay, -1);
        delayPromise1.Timer.KeepRootedWhileScheduled();
      }
      return (Task) delayPromise1;
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена, когда все <see cref="T:System.Threading.Tasks.Task" /> объекты в перечислимой коллекции будут завершены.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <returns>
    ///   Задача, представляющая завершение всех предоставленных задач.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Коллекция <paramref name="tasks" />содержала задачу <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task WhenAll(IEnumerable<Task> tasks)
    {
      Task[] taskArray = tasks as Task[];
      if (taskArray != null)
        return Task.WhenAll(taskArray);
      ICollection<Task> tasks1 = tasks as ICollection<Task>;
      if (tasks1 != null)
      {
        int num = 0;
        Task[] tasks2 = new Task[tasks1.Count];
        foreach (Task task in tasks)
        {
          if (task == null)
            throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
          tasks2[num++] = task;
        }
        return Task.InternalWhenAll(tasks2);
      }
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      List<Task> taskList = new List<Task>();
      foreach (Task task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        taskList.Add(task);
      }
      return Task.InternalWhenAll(taskList.ToArray());
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена, когда все <see cref="T:System.Threading.Tasks.Task" /> объекты в массиве будут завершены.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <returns>
    ///   Задача, представляющая завершение всех предоставленных задач.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="tasks" /> содержал задачу <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task WhenAll(params Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      int length = tasks.Length;
      if (length == 0)
        return Task.InternalWhenAll(tasks);
      Task[] tasks1 = new Task[length];
      for (int index = 0; index < length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        tasks1[index] = task;
      }
      return Task.InternalWhenAll(tasks1);
    }

    private static Task InternalWhenAll(Task[] tasks)
    {
      if (tasks.Length != 0)
        return (Task) new Task.WhenAllPromise(tasks);
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена, когда все <see cref="T:System.Threading.Tasks.Task`1" /> объекты в перечислимой коллекции будут завершены.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <typeparam name="TResult">Тип завершенной задачи.</typeparam>
    /// <returns>
    ///   Задача, представляющая завершение всех предоставленных задач.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Коллекция <paramref name="tasks" />содержала задачу <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
    {
      Task<TResult>[] taskArray = tasks as Task<TResult>[];
      if (taskArray != null)
        return Task.WhenAll<TResult>(taskArray);
      ICollection<Task<TResult>> tasks1 = tasks as ICollection<Task<TResult>>;
      if (tasks1 != null)
      {
        int num = 0;
        Task<TResult>[] tasks2 = new Task<TResult>[tasks1.Count];
        foreach (Task<TResult> task in tasks)
        {
          if (task == null)
            throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
          tasks2[num++] = task;
        }
        return Task.InternalWhenAll<TResult>(tasks2);
      }
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      List<Task<TResult>> taskList = new List<Task<TResult>>();
      foreach (Task<TResult> task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        taskList.Add(task);
      }
      return Task.InternalWhenAll<TResult>(taskList.ToArray());
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена, когда все <see cref="T:System.Threading.Tasks.Task`1" /> объекты в массиве будут завершены.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <typeparam name="TResult">Тип завершенной задачи.</typeparam>
    /// <returns>
    ///   Задача, представляющая завершение всех предоставленных задач.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="tasks" /> содержал задачу <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      int length = tasks.Length;
      if (length == 0)
        return Task.InternalWhenAll<TResult>(tasks);
      Task<TResult>[] tasks1 = new Task<TResult>[length];
      for (int index = 0; index < length; ++index)
      {
        Task<TResult> task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        tasks1[index] = task;
      }
      return Task.InternalWhenAll<TResult>(tasks1);
    }

    private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
    {
      if (tasks.Length != 0)
        return (Task<TResult[]>) new Task.WhenAllPromise<TResult>(tasks);
      return new Task<TResult[]>(false, new TResult[0], TaskCreationOptions.None, new CancellationToken());
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена после выполнения любой из предоставленных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <returns>
    ///   Задача, представляющая завершение одной из предоставленных задач.
    ///     Результат возвращенной задачи — задача, которая была завершена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Аргумент имел значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержится неопределенную задачу или пуст.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<Task> WhenAny(params Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      int length = tasks.Length;
      Task[] taskArray = new Task[length];
      for (int index = 0; index < length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        taskArray[index] = task;
      }
      return TaskFactory.CommonCWAnyLogic((IList<Task>) taskArray);
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена после выполнения любой из предоставленных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <returns>
    ///   Задача, представляющая завершение одной из предоставленных задач.
    ///     Результат возвращенной задачи — задача, которая была завершена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массива содержит неопределенную задачу или был пустым.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<Task> WhenAny(IEnumerable<Task> tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException(nameof (tasks));
      List<Task> taskList = new List<Task>();
      foreach (Task task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), nameof (tasks));
        taskList.Add(task);
      }
      if (taskList.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), nameof (tasks));
      return TaskFactory.CommonCWAnyLogic((IList<Task>) taskList);
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена после выполнения любой из предоставленных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <typeparam name="TResult">Тип завершенной задачи.</typeparam>
    /// <returns>
    ///   Задача, представляющая завершение одной из предоставленных задач.
    ///     Результат возвращенной задачи — задача, которая была завершена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="tasks" /> Аргумент имел значение null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержится неопределенную задачу или пуст.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
    {
      return Task.WhenAny((Task[]) tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, new CancellationToken(), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    /// <summary>
    ///   Создает задачу, которая будет выполнена после выполнения любой из предоставленных задач.
    /// </summary>
    /// <param name="tasks">
    ///   Задачи, завершение которых требуется подождать.
    /// </param>
    /// <typeparam name="TResult">Тип завершенной задачи.</typeparam>
    /// <returns>
    ///   Задача, представляющая завершение одной из предоставленных задач.
    ///     Результат возвращенной задачи — задача, которая была завершена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="tasks" /> имел значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tasks" /> Массив содержится неопределенную задачу или пуст.
    /// </exception>
    [__DynamicallyInvokable]
    public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks)
    {
      return Task.WhenAny((IEnumerable<Task>) tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, new CancellationToken(), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    [FriendAccessAllowed]
    internal static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
    {
      return (Task<TResult>) new UnwrapPromise<TResult>(outerTask, lookForOce);
    }

    internal virtual Delegate[] GetDelegateContinuationsForDebugger()
    {
      if (this.m_continuationObject != this)
        return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
      return (Delegate[]) null;
    }

    internal static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
    {
      if (continuationObject != null)
      {
        Action action = continuationObject as Action;
        if (action != null)
          return new Delegate[1]
          {
            (Delegate) AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action)
          };
        TaskContinuation taskContinuation = continuationObject as TaskContinuation;
        if (taskContinuation != null)
          return taskContinuation.GetDelegateContinuationsForDebugger();
        Task task = continuationObject as Task;
        if (task != null)
        {
          Delegate[] continuationsForDebugger = task.GetDelegateContinuationsForDebugger();
          if (continuationsForDebugger != null)
            return continuationsForDebugger;
        }
        ITaskCompletionAction completionAction = continuationObject as ITaskCompletionAction;
        if (completionAction != null)
          return new Delegate[1]
          {
            (Delegate) new Action<Task>(completionAction.Invoke)
          };
        List<object> objectList = continuationObject as List<object>;
        if (objectList != null)
        {
          List<Delegate> delegateList = new List<Delegate>();
          foreach (object continuationObject1 in objectList)
          {
            Delegate[] continuationObject2 = Task.GetDelegatesFromContinuationObject(continuationObject1);
            if (continuationObject2 != null)
            {
              foreach (Delegate @delegate in continuationObject2)
              {
                if ((object) @delegate != null)
                  delegateList.Add(@delegate);
              }
            }
          }
          return delegateList.ToArray();
        }
      }
      return (Delegate[]) null;
    }

    private static Task GetActiveTaskFromId(int taskId)
    {
      Task task = (Task) null;
      Task.s_currentActiveTasks.TryGetValue(taskId, out task);
      return task;
    }

    private static Task[] GetActiveTasks()
    {
      return new List<Task>((IEnumerable<Task>) Task.s_currentActiveTasks.Values).ToArray();
    }

    internal class ContingentProperties
    {
      internal volatile int m_completionCountdown = 1;
      internal ExecutionContext m_capturedContext;
      internal volatile ManualResetEventSlim m_completionEvent;
      internal volatile TaskExceptionHolder m_exceptionsHolder;
      internal CancellationToken m_cancellationToken;
      internal Shared<CancellationTokenRegistration> m_cancellationRegistration;
      internal volatile int m_internalCancellationRequested;
      internal volatile List<Task> m_exceptionalChildren;

      internal void SetCompleted()
      {
        this.m_completionEvent?.Set();
      }

      internal void DeregisterCancellationCallback()
      {
        if (this.m_cancellationRegistration == null)
          return;
        try
        {
          this.m_cancellationRegistration.Value.Dispose();
        }
        catch (ObjectDisposedException ex)
        {
        }
        this.m_cancellationRegistration = (Shared<CancellationTokenRegistration>) null;
      }
    }

    private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
    {
      internal SetOnInvokeMres()
        : base(false, 0)
      {
      }

      public void Invoke(Task completingTask)
      {
        this.Set();
      }
    }

    private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
    {
      private int _count;

      internal SetOnCountdownMres(int count)
      {
        this._count = count;
      }

      public void Invoke(Task completingTask)
      {
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        this.Set();
      }
    }

    private sealed class DelayPromise : Task<VoidTaskResult>
    {
      internal readonly CancellationToken Token;
      internal CancellationTokenRegistration Registration;
      internal Timer Timer;

      internal DelayPromise(CancellationToken token)
      {
        this.Token = token;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.Delay", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      internal void Complete()
      {
        bool flag;
        if (this.Token.IsCancellationRequested)
        {
          flag = this.TrySetCanceled(this.Token);
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          flag = this.TrySetResult(new VoidTaskResult());
        }
        if (!flag)
          return;
        if (this.Timer != null)
          this.Timer.Dispose();
        this.Registration.Dispose();
      }
    }

    private sealed class WhenAllPromise : Task<VoidTaskResult>, ITaskCompletionAction
    {
      private readonly Task[] m_tasks;
      private int m_count;

      internal WhenAllPromise(Task[] tasks)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.WhenAll", 0UL);
        if (Task.s_asyncDebuggingEnabled)
          Task.AddToActiveTasks((Task) this);
        this.m_tasks = tasks;
        this.m_count = tasks.Length;
        foreach (Task task in tasks)
        {
          if (task.IsCompleted)
            this.Invoke(task);
          else
            task.AddCompletionAction((ITaskCompletionAction) this);
        }
      }

      public void Invoke(Task completedTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (Interlocked.Decrement(ref this.m_count) != 0)
          return;
        List<ExceptionDispatchInfo> exceptionDispatchInfoList = (List<ExceptionDispatchInfo>) null;
        Task task1 = (Task) null;
        for (int index = 0; index < this.m_tasks.Length; ++index)
        {
          Task task2 = this.m_tasks[index];
          if (task2.IsFaulted)
          {
            if (exceptionDispatchInfoList == null)
              exceptionDispatchInfoList = new List<ExceptionDispatchInfo>();
            exceptionDispatchInfoList.AddRange((IEnumerable<ExceptionDispatchInfo>) task2.GetExceptionDispatchInfos());
          }
          else if (task2.IsCanceled && task1 == null)
            task1 = task2;
          if (task2.IsWaitNotificationEnabled)
            this.SetNotificationForWaitCompletion(true);
          else
            this.m_tasks[index] = (Task) null;
        }
        if (exceptionDispatchInfoList != null)
          this.TrySetException((object) exceptionDispatchInfoList);
        else if (task1 != null)
        {
          this.TrySetCanceled(task1.CancellationToken, (object) task1.GetCancellationExceptionDispatchInfo());
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          this.TrySetResult(new VoidTaskResult());
        }
      }

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
          return false;
        }
      }
    }

    private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
    {
      private readonly Task<T>[] m_tasks;
      private int m_count;

      internal WhenAllPromise(Task<T>[] tasks)
      {
        this.m_tasks = tasks;
        this.m_count = tasks.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.WhenAll", 0UL);
        if (Task.s_asyncDebuggingEnabled)
          Task.AddToActiveTasks((Task) this);
        foreach (Task<T> task in tasks)
        {
          if (task.IsCompleted)
            this.Invoke((Task) task);
          else
            task.AddCompletionAction((ITaskCompletionAction) this);
        }
      }

      public void Invoke(Task ignored)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (Interlocked.Decrement(ref this.m_count) != 0)
          return;
        T[] result = new T[this.m_tasks.Length];
        List<ExceptionDispatchInfo> exceptionDispatchInfoList = (List<ExceptionDispatchInfo>) null;
        Task task1 = (Task) null;
        for (int index = 0; index < this.m_tasks.Length; ++index)
        {
          Task<T> task2 = this.m_tasks[index];
          if (task2.IsFaulted)
          {
            if (exceptionDispatchInfoList == null)
              exceptionDispatchInfoList = new List<ExceptionDispatchInfo>();
            exceptionDispatchInfoList.AddRange((IEnumerable<ExceptionDispatchInfo>) task2.GetExceptionDispatchInfos());
          }
          else if (task2.IsCanceled)
          {
            if (task1 == null)
              task1 = (Task) task2;
          }
          else
            result[index] = task2.GetResultCore(false);
          if (task2.IsWaitNotificationEnabled)
            this.SetNotificationForWaitCompletion(true);
          else
            this.m_tasks[index] = (Task<T>) null;
        }
        if (exceptionDispatchInfoList != null)
          this.TrySetException((object) exceptionDispatchInfoList);
        else if (task1 != null)
        {
          this.TrySetCanceled(task1.CancellationToken, (object) task1.GetCancellationExceptionDispatchInfo());
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          this.TrySetResult(result);
        }
      }

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion((Task[]) this.m_tasks);
          return false;
        }
      }
    }
  }
}
