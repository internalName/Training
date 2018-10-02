// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskScheduler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет объект, обрабатывающий низкоуровневую постановку задач в очередь на потоки.
  /// </summary>
  [DebuggerDisplay("Id={Id}")]
  [DebuggerTypeProxy(typeof (TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class TaskScheduler
  {
    private static readonly TaskScheduler s_defaultTaskScheduler = (TaskScheduler) new ThreadPoolTaskScheduler();
    private static readonly object _unobservedTaskExceptionLockObject = new object();
    private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;
    internal static int s_taskSchedulerIdCounter;
    private volatile int m_taskSchedulerId;
    private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

    /// <summary>
    ///   Очереди <see cref="T:System.Threading.Tasks.Task" /> планировщику.
    /// </summary>
    /// <param name="task">
    ///   <see cref="T:System.Threading.Tasks.Task" /> В очередь.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="task" /> Аргумент имеет значение null.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected internal abstract void QueueTask(Task task);

    /// <summary>
    ///   Определяет ли предоставленный <see cref="T:System.Threading.Tasks.Task" /> может быть выполнена синхронно в этот вызов и если это возможно, он выполняется.
    /// </summary>
    /// <param name="task">
    ///   <see cref="T:System.Threading.Tasks.Task" /> Для выполнения.
    /// </param>
    /// <param name="taskWasPreviouslyQueued">
    ///   Логическое значение, обозначающее ли задача ранее поставлен в очередь.
    ///    Если этот параметр имеет значение True, то задача может ранее поставлены (запланированное); Если значение равно False, затем задача известна не были помещены в очередь, и этот вызов выполняется для выполнения задачи на месте без постановки в очередь его.
    /// </param>
    /// <returns>
    ///   Логическое значение, указывающее, была ли задача выполнена на месте.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="task" /> Аргумент имеет значение null.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="task" /> Уже была выполнена.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

    /// <summary>
    ///   Для отладчика поддержки, создает только перечислимые <see cref="T:System.Threading.Tasks.Task" /> экземпляры в настоящее время в очереди планировщика, ожидающих выполнения.
    /// </summary>
    /// <returns>
    ///   Перечислимый объект, позволяющий отладчику перемещаться по задачам в настоящее время в очереди на данном планировщике.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот планировщик не удалось создать список задач в очереди на данный момент.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected abstract IEnumerable<Task> GetScheduledTasks();

    /// <summary>
    ///   Это указывает максимальный уровень параллелизма <see cref="T:System.Threading.Tasks.TaskScheduler" /> способен поддерживать.
    /// </summary>
    /// <returns>
    ///   Возвращает целое число, представляющее максимальный уровень параллелизма.
    ///    Возвращает планировщик по умолчанию <see cref="F:System.Int32.MaxValue" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int MaximumConcurrencyLevel
    {
      [__DynamicallyInvokable] get
      {
        return int.MaxValue;
      }
    }

    [SecuritySafeCritical]
    internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
    {
      TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
      if (executingTaskScheduler != this && executingTaskScheduler != null)
        return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
      StackGuard currentStackGuard;
      if (executingTaskScheduler == null || task.m_action == null || (task.IsDelegateInvoked || task.IsCanceled) || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
        return false;
      bool flag = false;
      try
      {
        task.FireTaskScheduledIfNeeded(this);
        flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
      }
      finally
      {
        currentStackGuard.EndInliningScope();
      }
      if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
        throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_InconsistentStateAfterTryExecuteTaskInline"));
      return flag;
    }

    /// <summary>
    ///   Пытается удалить из очереди <see cref="T:System.Threading.Tasks.Task" /> был ранее в очереди данного планировщика.
    /// </summary>
    /// <param name="task">
    ///   <see cref="T:System.Threading.Tasks.Task" /> Чтобы быть выведенным из очереди.
    /// </param>
    /// <returns>
    ///   Логическое отвечающий за ли <paramref name="task" /> аргумент был успешно удален из очереди.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="task" /> Аргумент имеет значение null.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected internal virtual bool TryDequeue(Task task)
    {
      return false;
    }

    internal virtual void NotifyWorkItemProgress()
    {
    }

    internal virtual bool RequiresAtomicStartTransition
    {
      get
      {
        return true;
      }
    }

    [SecurityCritical]
    internal void InternalQueueTask(Task task)
    {
      task.FireTaskScheduledIfNeeded(this);
      this.QueueTask(task);
    }

    /// <summary>
    ///   Инициализирует объект <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected TaskScheduler()
    {
      if (!Debugger.IsAttached)
        return;
      this.AddToActiveTaskSchedulers();
    }

    private void AddToActiveTaskSchedulers()
    {
      ConditionalWeakTable<TaskScheduler, object> activeTaskSchedulers = TaskScheduler.s_activeTaskSchedulers;
      if (activeTaskSchedulers == null)
      {
        Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), (ConditionalWeakTable<TaskScheduler, object>) null);
        activeTaskSchedulers = TaskScheduler.s_activeTaskSchedulers;
      }
      activeTaskSchedulers.Add(this, (object) null);
    }

    /// <summary>
    ///   Возвращает значение по умолчанию <see cref="T:System.Threading.Tasks.TaskScheduler" /> предоставляемый платформой .NET Framework.
    /// </summary>
    /// <returns>
    ///   Возвращает значение по умолчанию <see cref="T:System.Threading.Tasks.TaskScheduler" /> экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public static TaskScheduler Default
    {
      [__DynamicallyInvokable] get
      {
        return TaskScheduler.s_defaultTaskScheduler;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Threading.Tasks.TaskScheduler" /> связанных с текущей выполняемой задачей.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Threading.Tasks.TaskScheduler" /> связанных с текущей выполняемой задачей.
    /// </returns>
    [__DynamicallyInvokable]
    public static TaskScheduler Current
    {
      [__DynamicallyInvokable] get
      {
        return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
      }
    }

    internal static TaskScheduler InternalCurrent
    {
      get
      {
        Task internalCurrent = Task.InternalCurrent;
        if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
          return (TaskScheduler) null;
        return internalCurrent.ExecutingTaskScheduler;
      }
    }

    /// <summary>
    ///   Создает <see cref="T:System.Threading.Tasks.TaskScheduler" /> для связывания с текущим элементом <see cref="T:System.Threading.SynchronizationContext" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Threading.Tasks.TaskScheduler" /> для связывания с текущим элементом <see cref="T:System.Threading.SynchronizationContext" />, как определено в <see cref="P:System.Threading.SynchronizationContext.Current" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий SynchronizationContext нельзя использовать как TaskScheduler.
    /// </exception>
    [__DynamicallyInvokable]
    public static TaskScheduler FromCurrentSynchronizationContext()
    {
      return (TaskScheduler) new SynchronizationContextTaskScheduler();
    }

    /// <summary>
    ///   Получает уникальный идентификатор для этого <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </summary>
    /// <returns>
    ///   Возвращает уникальный идентификатор для этого <see cref="T:System.Threading.Tasks.TaskScheduler" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Id
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_taskSchedulerId == 0)
        {
          int num;
          do
          {
            num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
          }
          while (num == 0);
          Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
        }
        return this.m_taskSchedulerId;
      }
    }

    /// <summary>
    ///   Предпринимает попытку выполнить предоставленный <see cref="T:System.Threading.Tasks.Task" /> на данном планировщике.
    /// </summary>
    /// <param name="task">
    ///   Объект <see cref="T:System.Threading.Tasks.Task" /> объект для выполнения.
    /// </param>
    /// <returns>
    ///   Логическое значение, является значение true, если <paramref name="task" /> выполнено успешно, и false, если она не была.
    ///    Распространенной причиной сбоя при выполнении, что задача выполнялась ранее или выполняется другим потоком.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="task" /> Не связан с данным планировщиком.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected bool TryExecuteTask(Task task)
    {
      if (task.ExecutingTaskScheduler != this)
        throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler"));
      return task.ExecuteEntry(true);
    }

    /// <summary>
    ///   Создается при активации политики эскалации исключений из-за непредвиденного исключения задачи, завершившейся сбоем. По умолчанию из-за этой политики процесс будет прерван.
    /// </summary>
    [__DynamicallyInvokable]
    public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
    {
      [SecurityCritical, __DynamicallyInvokable] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (TaskScheduler._unobservedTaskExceptionLockObject)
          TaskScheduler._unobservedTaskException += value;
      }
      [SecurityCritical, __DynamicallyInvokable] remove
      {
        lock (TaskScheduler._unobservedTaskExceptionLockObject)
          TaskScheduler._unobservedTaskException -= value;
      }
    }

    internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
    {
      lock (TaskScheduler._unobservedTaskExceptionLockObject)
      {
        EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
        if (unobservedTaskException == null)
          return;
        unobservedTaskException(sender, ueea);
      }
    }

    [SecurityCritical]
    internal Task[] GetScheduledTasksForDebugger()
    {
      IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
      if (scheduledTasks == null)
        return (Task[]) null;
      Task[] taskArray = scheduledTasks as Task[] ?? new List<Task>(scheduledTasks).ToArray();
      foreach (Task task in taskArray)
      {
        int id = task.Id;
      }
      return taskArray;
    }

    [SecurityCritical]
    internal static TaskScheduler[] GetTaskSchedulersForDebugger()
    {
      if (TaskScheduler.s_activeTaskSchedulers == null)
        return new TaskScheduler[1]
        {
          TaskScheduler.s_defaultTaskScheduler
        };
      ICollection<TaskScheduler> keys = TaskScheduler.s_activeTaskSchedulers.Keys;
      if (!keys.Contains(TaskScheduler.s_defaultTaskScheduler))
        keys.Add(TaskScheduler.s_defaultTaskScheduler);
      TaskScheduler[] array = new TaskScheduler[keys.Count];
      keys.CopyTo(array, 0);
      foreach (TaskScheduler taskScheduler in array)
      {
        int id = taskScheduler.Id;
      }
      return array;
    }

    internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
    {
      private readonly TaskScheduler m_taskScheduler;

      public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
      {
        this.m_taskScheduler = scheduler;
      }

      public int Id
      {
        get
        {
          return this.m_taskScheduler.Id;
        }
      }

      public IEnumerable<Task> ScheduledTasks
      {
        [SecurityCritical] get
        {
          return this.m_taskScheduler.GetScheduledTasks();
        }
      }
    }
  }
}
