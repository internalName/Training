﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ConfiguredTaskAwaitable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет типа awaitable, позволяет настроить ожидание объекта задачи.
  /// </summary>
  [__DynamicallyInvokable]
  public struct ConfiguredTaskAwaitable
  {
    private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

    internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
    {
      this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
    }

    /// <summary>
    ///   Возвращает объект типа awaiter для данного типа awaitable объекта.
    /// </summary>
    /// <returns>Типа awaiter.</returns>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
    {
      return this.m_configuredTaskAwaiter;
    }

    /// <summary>
    ///   Предоставляет объект типа awaiter для типа awaitable (<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable" />) объекта.
    /// </summary>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
    {
      private readonly Task m_task;
      private readonly bool m_continueOnCapturedContext;

      internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
      {
        this.m_task = task;
        this.m_continueOnCapturedContext = continueOnCapturedContext;
      }

      /// <summary>
      ///   Возвращает значение, указывающее, завершена ли задача будет ожидать.
      /// </summary>
      /// <returns>
      ///   <see langword="true" /> Если задача будет ожидать завершения; в противном случае — <see langword="false" />.
      /// </returns>
      /// <exception cref="T:System.NullReferenceException">
      ///   Типа awaiter не был правильно инициализирован.
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
      ///   Планирование действия продолжения задачи, связанные с этой типа awaiter.
      /// </summary>
      /// <param name="continuation">
      ///   Действие, вызываемый после завершения операции await.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Аргумент <paramref name="continuation" /> имеет значение <see langword="null" />.
      /// </exception>
      /// <exception cref="T:System.NullReferenceException">
      ///   Типа awaiter не был правильно инициализирован.
      /// </exception>
      [SecuritySafeCritical]
      [__DynamicallyInvokable]
      public void OnCompleted(Action continuation)
      {
        TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
      }

      /// <summary>
      ///   Планирование действия продолжения задачи, связанные с этой типа awaiter.
      /// </summary>
      /// <param name="continuation">
      ///   Действие, вызываемый после завершения операции await.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Аргумент <paramref name="continuation" /> имеет значение <see langword="null" />.
      /// </exception>
      /// <exception cref="T:System.NullReferenceException">
      ///   Типа awaiter не был правильно инициализирован.
      /// </exception>
      [SecurityCritical]
      [__DynamicallyInvokable]
      public void UnsafeOnCompleted(Action continuation)
      {
        TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
      }

      /// <summary>Завершает await для завершения задачи.</summary>
      /// <exception cref="T:System.NullReferenceException">
      ///   Типа awaiter не был правильно инициализирован.
      /// </exception>
      /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
      ///   Задача отменена.
      /// </exception>
      /// <exception cref="T:System.Exception">
      ///   Задача завершилась в состоянии сбоя.
      /// </exception>
      [__DynamicallyInvokable]
      public void GetResult()
      {
        TaskAwaiter.ValidateEnd(this.m_task);
      }
    }
  }
}
