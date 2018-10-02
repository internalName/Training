// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.StreamingContextStates
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Определяет набор флагов, которые указывают контекст источника или назначения для потока во время сериализации.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum StreamingContextStates
  {
    CrossProcess = 1,
    CrossMachine = 2,
    File = 4,
    Persistence = 8,
    Remoting = 16, // 0x00000010
    Other = 32, // 0x00000020
    Clone = 64, // 0x00000040
    CrossAppDomain = 128, // 0x00000080
    All = CrossAppDomain | Clone | Other | Remoting | Persistence | File | CrossMachine | CrossProcess, // 0x000000FF
  }
}
