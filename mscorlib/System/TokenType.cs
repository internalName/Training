// Decompiled with JetBrains decompiler
// Type: System.TokenType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum TokenType
  {
    NumberToken = 1,
    YearNumberToken = 2,
    Am = 3,
    Pm = 4,
    MonthToken = 5,
    EndOfString = 6,
    DayOfWeekToken = 7,
    TimeZoneToken = 8,
    EraToken = 9,
    DateWordToken = 10, // 0x0000000A
    UnknownToken = 11, // 0x0000000B
    HebrewNumber = 12, // 0x0000000C
    JapaneseEraToken = 13, // 0x0000000D
    TEraToken = 14, // 0x0000000E
    IgnorableSymbol = 15, // 0x0000000F
    RegularTokenMask = 255, // 0x000000FF
    SEP_Unk = 256, // 0x00000100
    SEP_End = 512, // 0x00000200
    SEP_Space = 768, // 0x00000300
    SEP_Am = 1024, // 0x00000400
    SEP_Pm = 1280, // 0x00000500
    SEP_Date = 1536, // 0x00000600
    SEP_Time = 1792, // 0x00000700
    SEP_YearSuff = 2048, // 0x00000800
    SEP_MonthSuff = 2304, // 0x00000900
    SEP_DaySuff = 2560, // 0x00000A00
    SEP_HourSuff = 2816, // 0x00000B00
    SEP_MinuteSuff = 3072, // 0x00000C00
    SEP_SecondSuff = 3328, // 0x00000D00
    SEP_LocalTimeMark = 3584, // 0x00000E00
    SEP_DateOrOffset = 3840, // 0x00000F00
    SeparatorTokenMask = 65280, // 0x0000FF00
  }
}
