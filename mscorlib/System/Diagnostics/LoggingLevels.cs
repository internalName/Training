// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.LoggingLevels
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics
{
  [Serializable]
  internal enum LoggingLevels
  {
    TraceLevel0 = 0,
    TraceLevel1 = 1,
    TraceLevel2 = 2,
    TraceLevel3 = 3,
    TraceLevel4 = 4,
    StatusLevel0 = 20, // 0x00000014
    StatusLevel1 = 21, // 0x00000015
    StatusLevel2 = 22, // 0x00000016
    StatusLevel3 = 23, // 0x00000017
    StatusLevel4 = 24, // 0x00000018
    WarningLevel = 40, // 0x00000028
    ErrorLevel = 50, // 0x00000032
    PanicLevel = 100, // 0x00000064
  }
}
