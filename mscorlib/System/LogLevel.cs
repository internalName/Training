// Decompiled with JetBrains decompiler
// Type: System.LogLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Serializable]
  internal enum LogLevel
  {
    Trace = 0,
    Status = 20, // 0x00000014
    Warning = 40, // 0x00000028
    Error = 50, // 0x00000032
    Panic = 100, // 0x00000064
  }
}
