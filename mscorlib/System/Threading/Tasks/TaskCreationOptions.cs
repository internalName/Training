// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCreationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Задает флаги, которые управляют необязательным поведением создания и выполнения задач.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TaskCreationOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PreferFairness = 1,
    [__DynamicallyInvokable] LongRunning = 2,
    [__DynamicallyInvokable] AttachedToParent = 4,
    [__DynamicallyInvokable] DenyChildAttach = 8,
    [__DynamicallyInvokable] HideScheduler = 16, // 0x00000010
    [__DynamicallyInvokable] RunContinuationsAsynchronously = 64, // 0x00000040
  }
}
