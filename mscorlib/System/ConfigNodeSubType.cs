// Decompiled with JetBrains decompiler
// Type: System.ConfigNodeSubType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Serializable]
  internal enum ConfigNodeSubType
  {
    Version = 28, // 0x0000001C
    Encoding = 29, // 0x0000001D
    Standalone = 30, // 0x0000001E
    NS = 31, // 0x0000001F
    XMLSpace = 32, // 0x00000020
    XMLLang = 33, // 0x00000021
    System = 34, // 0x00000022
    Public = 35, // 0x00000023
    NData = 36, // 0x00000024
    AtCData = 37, // 0x00000025
    AtId = 38, // 0x00000026
    AtIdref = 39, // 0x00000027
    AtIdrefs = 40, // 0x00000028
    AtEntity = 41, // 0x00000029
    AtEntities = 42, // 0x0000002A
    AtNmToken = 43, // 0x0000002B
    AtNmTokens = 44, // 0x0000002C
    AtNotation = 45, // 0x0000002D
    AtRequired = 46, // 0x0000002E
    AtImplied = 47, // 0x0000002F
    AtFixed = 48, // 0x00000030
    PentityDecl = 49, // 0x00000031
    Empty = 50, // 0x00000032
    Any = 51, // 0x00000033
    Mixed = 52, // 0x00000034
    Sequence = 53, // 0x00000035
    Choice = 54, // 0x00000036
    Star = 55, // 0x00000037
    Plus = 56, // 0x00000038
    Questionmark = 57, // 0x00000039
    LastSubNodeType = 58, // 0x0000003A
  }
}
