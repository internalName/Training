// Decompiled with JetBrains decompiler
// Type: System.ParseFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Flags]
  internal enum ParseFlags
  {
    HaveYear = 1,
    HaveMonth = 2,
    HaveDay = 4,
    HaveHour = 8,
    HaveMinute = 16, // 0x00000010
    HaveSecond = 32, // 0x00000020
    HaveTime = 64, // 0x00000040
    HaveDate = 128, // 0x00000080
    TimeZoneUsed = 256, // 0x00000100
    TimeZoneUtc = 512, // 0x00000200
    ParsedMonthName = 1024, // 0x00000400
    CaptureOffset = 2048, // 0x00000800
    YearDefault = 4096, // 0x00001000
    Rfc1123Pattern = 8192, // 0x00002000
    UtcSortPattern = 16384, // 0x00004000
  }
}
