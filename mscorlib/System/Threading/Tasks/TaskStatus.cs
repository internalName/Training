// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskStatus
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет текущий этап жизненного цикла задачи <see cref="T:System.Threading.Tasks.Task" />.
  /// </summary>
  [__DynamicallyInvokable]
  public enum TaskStatus
  {
    [__DynamicallyInvokable] Created,
    [__DynamicallyInvokable] WaitingForActivation,
    [__DynamicallyInvokable] WaitingToRun,
    [__DynamicallyInvokable] Running,
    [__DynamicallyInvokable] WaitingForChildrenToComplete,
    [__DynamicallyInvokable] RanToCompletion,
    [__DynamicallyInvokable] Canceled,
    [__DynamicallyInvokable] Faulted,
  }
}
