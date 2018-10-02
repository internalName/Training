// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Указывает состояния выполнения <see cref="T:System.Threading.Thread" />.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum ThreadState
  {
    Running = 0,
    StopRequested = 1,
    SuspendRequested = 2,
    Background = 4,
    Unstarted = 8,
    Stopped = 16, // 0x00000010
    WaitSleepJoin = 32, // 0x00000020
    Suspended = 64, // 0x00000040
    AbortRequested = 128, // 0x00000080
    Aborted = 256, // 0x00000100
  }
}
