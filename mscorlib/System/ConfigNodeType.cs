// Decompiled with JetBrains decompiler
// Type: System.ConfigNodeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Serializable]
  internal enum ConfigNodeType
  {
    Element = 1,
    Attribute = 2,
    Pi = 3,
    XmlDecl = 4,
    DocType = 5,
    DTDAttribute = 6,
    EntityDecl = 7,
    ElementDecl = 8,
    AttlistDecl = 9,
    Notation = 10, // 0x0000000A
    Group = 11, // 0x0000000B
    IncludeSect = 12, // 0x0000000C
    PCData = 13, // 0x0000000D
    CData = 14, // 0x0000000E
    IgnoreSect = 15, // 0x0000000F
    Comment = 16, // 0x00000010
    EntityRef = 17, // 0x00000011
    Whitespace = 18, // 0x00000012
    Name = 19, // 0x00000013
    NMToken = 20, // 0x00000014
    String = 21, // 0x00000015
    Peref = 22, // 0x00000016
    Model = 23, // 0x00000017
    ATTDef = 24, // 0x00000018
    ATTType = 25, // 0x00000019
    ATTPresence = 26, // 0x0000001A
    DTDSubset = 27, // 0x0000001B
    LastNodeType = 28, // 0x0000001C
  }
}
