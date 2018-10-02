// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.InternalTaskOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  [Flags]
  [Serializable]
  internal enum InternalTaskOptions
  {
    None = 0,
    InternalOptionsMask = 65280, // 0x0000FF00
    ChildReplica = 256, // 0x00000100
    ContinuationTask = 512, // 0x00000200
    PromiseTask = 1024, // 0x00000400
    SelfReplicating = 2048, // 0x00000800
    LazyCancellation = 4096, // 0x00001000
    QueuedByRuntime = 8192, // 0x00002000
    DoNotDispose = 16384, // 0x00004000
  }
}
