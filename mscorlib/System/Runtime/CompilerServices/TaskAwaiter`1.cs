// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TaskAwaiter`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Представляет объект, который ожидает завершения асинхронной задачи и предоставляет параметр для результата.
  /// </summary>
  /// <typeparam name="TResult">Результат задачи.</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
  {
    private readonly Task<TResult> m_task;

    internal TaskAwaiter(Task<TResult> task)
    {
      this.m_task = task;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, завершена ли асинхронная задача.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если задача была завершена; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> Объект не был правильно инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task.IsCompleted;
      }
    }

    /// <summary>
    ///   Задает действие, выполняемое при <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> объект прекращает ожидание завершения асинхронной задачи.
    /// </summary>
    /// <param name="continuation">
    ///   Действие, выполняемое при завершении операции ожидания.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="continuation" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> Объект не был правильно инициализирован.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void OnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal((Task) this.m_task, continuation, true, true);
    }

    /// <summary>
    ///   Планирует продолжение действия для асинхронной задачи, связанные с этой типа awaiter.
    /// </summary>
    /// <param name="continuation">
    ///   Действие, вызываемый после завершения операции await.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="continuation" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Типа awaiter не был правильно инициализирован.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void UnsafeOnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal((Task) this.m_task, continuation, true, false);
    }

    /// <summary>Прекращает ожидание завершения асинхронной задачи.</summary>
    /// <returns>Результат выполнения задачи.</returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> Объект не был правильно инициализирован.
    /// </exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
    ///   Задача отменена.
    /// </exception>
    /// <exception cref="T:System.Exception">
    ///   Задача завершилась в <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> состояние.
    /// </exception>
    [__DynamicallyInvokable]
    public TResult GetResult()
    {
      TaskAwaiter.ValidateEnd((Task) this.m_task);
      return this.m_task.ResultOnSuccess;
    }
  }
}
