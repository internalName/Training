// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCompletionSource`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет сторону производителя задач <see cref="T:System.Threading.Tasks.Task`1" />, не привязанных к делегату и предоставляющих доступ к потребительской стороне через свойство <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" />.
  /// </summary>
  /// <typeparam name="TResult">
  ///   Тип значений результата, связанного с данным объектом <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
  /// </typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskCompletionSource<TResult>
  {
    private readonly System.Threading.Tasks.Task<TResult> m_task;

    /// <summary>
    ///   Создает буфер <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public TaskCompletionSource()
    {
      this.m_task = new System.Threading.Tasks.Task<TResult>();
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> с заданными параметрами.
    /// </summary>
    /// <param name="creationOptions">
    ///   Параметры, используемые при создании базовых задач <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Представляют параметры, недопустимые для использования с <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TaskCompletionSource(TaskCreationOptions creationOptions)
      : this((object) null, creationOptions)
    {
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> с указанным состоянием.
    /// </summary>
    /// <param name="state">
    ///   Состояние, используемое в качестве состояния AsyncState базового объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    [__DynamicallyInvokable]
    public TaskCompletionSource(object state)
      : this(state, TaskCreationOptions.None)
    {
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> с заданным состоянием и параметрами.
    /// </summary>
    /// <param name="state">
    ///   Состояние, используемое в качестве состояния AsyncState базового объекта <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <param name="creationOptions">
    ///   Параметры, используемые при создании базовых задач <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="creationOptions" /> Представляют параметры, недопустимые для использования с <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
    {
      this.m_task = new System.Threading.Tasks.Task<TResult>(state, creationOptions);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Threading.Tasks.Task`1" />, созданный данным объектом <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Threading.Tasks.Task`1" />, созданный данным объектом <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public System.Threading.Tasks.Task<TResult> Task
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task;
      }
    }

    private void SpinUntilCompleted()
    {
      SpinWait spinWait = new SpinWait();
      while (!this.m_task.IsCompleted)
        spinWait.SpinOnce();
    }

    /// <summary>
    ///   Пытается перевести базовый элемент <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> и привязывает его к определенному исключению.
    /// </summary>
    /// <param name="exception">
    ///   Выражение для привязки к данному <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <returns>
    ///   Значение true, если операция выполнена успешно; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="exception" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TrySetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      bool flag = this.m_task.TrySetException((object) exception);
      if (!flag && !this.m_task.IsCompleted)
        this.SpinUntilCompleted();
      return flag;
    }

    /// <summary>
    ///   Пытается перевести базовый <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> и привязывает к нему коллекцию объектов исключений.
    /// </summary>
    /// <param name="exceptions">
    ///   Коллекция исключений для привязки к данному объекту <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <returns>
    ///   Значение true, если операция выполнена успешно; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="exceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="exceptions" /> включает один или несколько элементов со значением null.
    /// 
    ///   -или-
    /// 
    ///   Коллекция <paramref name="exceptions" /> пуста.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TrySetException(IEnumerable<Exception> exceptions)
    {
      if (exceptions == null)
        throw new ArgumentNullException(nameof (exceptions));
      List<Exception> exceptionList = new List<Exception>();
      foreach (Exception exception in exceptions)
      {
        if (exception == null)
          throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NullException"), nameof (exceptions));
        exceptionList.Add(exception);
      }
      if (exceptionList.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NoExceptions"), nameof (exceptions));
      bool flag = this.m_task.TrySetException((object) exceptionList);
      if (!flag && !this.m_task.IsCompleted)
        this.SpinUntilCompleted();
      return flag;
    }

    internal bool TrySetException(IEnumerable<ExceptionDispatchInfo> exceptions)
    {
      bool flag = this.m_task.TrySetException((object) exceptions);
      if (!flag && !this.m_task.IsCompleted)
        this.SpinUntilCompleted();
      return flag;
    }

    /// <summary>
    ///   Переводит базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> и привязывает его к определенному исключению.
    /// </summary>
    /// <param name="exception">
    ///   Выражение для привязки к данному <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="exception" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Базовый элемент <see cref="T:System.Threading.Tasks.Task`1" /> уже находится в одном из трех конечных состояний: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof (exception));
      if (!this.TrySetException(exception))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>
    ///   Переводит базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> и привязывает к нему коллекцию объектов исключений.
    /// </summary>
    /// <param name="exceptions">
    ///   Коллекция исключений для привязки к данному объекту <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Элемент <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="exceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="exceptions" /> включает один или несколько элементов со значением null.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Базовый элемент <see cref="T:System.Threading.Tasks.Task`1" /> уже находится в одном из трех конечных состояний: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetException(IEnumerable<Exception> exceptions)
    {
      if (!this.TrySetException(exceptions))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>
    ///   Пытается перевести базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />.
    /// </summary>
    /// <param name="result">
    ///   Итоговое значение для привязки к данному <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <returns>
    ///   Значение true, если операция выполнена успешно; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TrySetResult(TResult result)
    {
      bool flag = this.m_task.TrySetResult(result);
      if (!flag && !this.m_task.IsCompleted)
        this.SpinUntilCompleted();
      return flag;
    }

    /// <summary>
    ///   Переводит базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />.
    /// </summary>
    /// <param name="result">
    ///   Итоговое значение для привязки к данному <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> Был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Базовый <see cref="T:System.Threading.Tasks.Task`1" /> уже находится в одном из трех состояний окончательного: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetResult(TResult result)
    {
      if (!this.TrySetResult(result))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>
    ///   Пытается перевести базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </summary>
    /// <returns>
    ///   Значение true, если операция завершилась успешно; значение false, если не удалось завершить операцию или объект уже был удален.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TrySetCanceled()
    {
      return this.TrySetCanceled(new CancellationToken());
    }

    /// <summary>
    ///   Пытается перевести базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" /> и позволяет хранить токен отмены в задаче отмены.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если операция выполнена успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TrySetCanceled(CancellationToken cancellationToken)
    {
      bool flag = this.m_task.TrySetCanceled(cancellationToken);
      if (!flag && !this.m_task.IsCompleted)
        this.SpinUntilCompleted();
      return flag;
    }

    /// <summary>
    ///   Переводит базовый объект <see cref="T:System.Threading.Tasks.Task`1" /> в состояние <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Базовый <see cref="T:System.Threading.Tasks.Task`1" /> уже находится в одном из трех состояний окончательного: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, или <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />, или, если базовый <see cref="T:System.Threading.Tasks.Task`1" /> уже был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void SetCanceled()
    {
      if (!this.TrySetCanceled())
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }
  }
}
