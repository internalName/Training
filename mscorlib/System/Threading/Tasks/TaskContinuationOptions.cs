// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskContinuationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Задает поведение для задачи, созданной с помощью метода <see cref="M:System.Threading.Tasks.Task.ContinueWith(System.Action{System.Threading.Tasks.Task},System.Threading.CancellationToken,System.Threading.Tasks.TaskContinuationOptions,System.Threading.Tasks.TaskScheduler)" /> или <see cref="M:System.Threading.Tasks.Task`1.ContinueWith(System.Action{System.Threading.Tasks.Task{`0}},System.Threading.Tasks.TaskContinuationOptions)" />.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TaskContinuationOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PreferFairness = 1,
    [__DynamicallyInvokable] LongRunning = 2,
    [__DynamicallyInvokable] AttachedToParent = 4,
    [__DynamicallyInvokable] DenyChildAttach = 8,
    [__DynamicallyInvokable] HideScheduler = 16, // 0x00000010
    [__DynamicallyInvokable] LazyCancellation = 32, // 0x00000020
    [__DynamicallyInvokable] RunContinuationsAsynchronously = 64, // 0x00000040
    [__DynamicallyInvokable] NotOnRanToCompletion = 65536, // 0x00010000
    [__DynamicallyInvokable] NotOnFaulted = 131072, // 0x00020000
    [__DynamicallyInvokable] NotOnCanceled = 262144, // 0x00040000
    [__DynamicallyInvokable] OnlyOnRanToCompletion = NotOnCanceled | NotOnFaulted, // 0x00060000
    [__DynamicallyInvokable] OnlyOnFaulted = NotOnCanceled | NotOnRanToCompletion, // 0x00050000
    [__DynamicallyInvokable] OnlyOnCanceled = NotOnFaulted | NotOnRanToCompletion, // 0x00030000
    [__DynamicallyInvokable] ExecuteSynchronously = 524288, // 0x00080000
  }
}
