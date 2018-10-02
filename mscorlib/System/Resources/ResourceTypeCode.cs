// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceTypeCode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Resources
{
  [Serializable]
  internal enum ResourceTypeCode
  {
    Null = 0,
    String = 1,
    Boolean = 2,
    Char = 3,
    Byte = 4,
    SByte = 5,
    Int16 = 6,
    UInt16 = 7,
    Int32 = 8,
    UInt32 = 9,
    Int64 = 10, // 0x0000000A
    UInt64 = 11, // 0x0000000B
    Single = 12, // 0x0000000C
    Double = 13, // 0x0000000D
    Decimal = 14, // 0x0000000E
    DateTime = 15, // 0x0000000F
    LastPrimitive = 16, // 0x00000010
    TimeSpan = 16, // 0x00000010
    ByteArray = 32, // 0x00000020
    Stream = 33, // 0x00000021
    StartOfUserTypes = 64, // 0x00000040
  }
}
