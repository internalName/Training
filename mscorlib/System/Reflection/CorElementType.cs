// Decompiled with JetBrains decompiler
// Type: System.Reflection.CorElementType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal enum CorElementType : byte
  {
    End = 0,
    Void = 1,
    Boolean = 2,
    Char = 3,
    I1 = 4,
    U1 = 5,
    I2 = 6,
    U2 = 7,
    I4 = 8,
    U4 = 9,
    I8 = 10, // 0x0A
    U8 = 11, // 0x0B
    R4 = 12, // 0x0C
    R8 = 13, // 0x0D
    String = 14, // 0x0E
    Ptr = 15, // 0x0F
    ByRef = 16, // 0x10
    ValueType = 17, // 0x11
    Class = 18, // 0x12
    Var = 19, // 0x13
    Array = 20, // 0x14
    GenericInst = 21, // 0x15
    TypedByRef = 22, // 0x16
    I = 24, // 0x18
    U = 25, // 0x19
    FnPtr = 27, // 0x1B
    Object = 28, // 0x1C
    SzArray = 29, // 0x1D
    MVar = 30, // 0x1E
    CModReqd = 31, // 0x1F
    CModOpt = 32, // 0x20
    Internal = 33, // 0x21
    Max = 34, // 0x22
    Modifier = 64, // 0x40
    Sentinel = 65, // 0x41
    Pinned = 69, // 0x45
  }
}
