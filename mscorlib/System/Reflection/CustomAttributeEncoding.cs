// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal enum CustomAttributeEncoding
  {
    Undefined = 0,
    Boolean = 2,
    Char = 3,
    SByte = 4,
    Byte = 5,
    Int16 = 6,
    UInt16 = 7,
    Int32 = 8,
    UInt32 = 9,
    Int64 = 10, // 0x0000000A
    UInt64 = 11, // 0x0000000B
    Float = 12, // 0x0000000C
    Double = 13, // 0x0000000D
    String = 14, // 0x0000000E
    Array = 29, // 0x0000001D
    Type = 80, // 0x00000050
    Object = 81, // 0x00000051
    Field = 83, // 0x00000053
    Property = 84, // 0x00000054
    Enum = 85, // 0x00000055
  }
}
