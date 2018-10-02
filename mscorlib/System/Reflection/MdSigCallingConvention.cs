// Decompiled with JetBrains decompiler
// Type: System.Reflection.MdSigCallingConvention
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  [Serializable]
  internal enum MdSigCallingConvention : byte
  {
    CallConvMask = 15, // 0x0F
    Default = 0,
    C = 1,
    StdCall = 2,
    ThisCall = StdCall | C, // 0x03
    FastCall = 4,
    Vararg = FastCall | C, // 0x05
    Field = FastCall | StdCall, // 0x06
    LocalSig = Field | C, // 0x07
    Property = 8,
    Unmgd = Property | C, // 0x09
    GenericInst = Property | StdCall, // 0x0A
    Generic = 16, // 0x10
    HasThis = 32, // 0x20
    ExplicitThis = 64, // 0x40
  }
}
