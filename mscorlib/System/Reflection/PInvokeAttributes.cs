// Decompiled with JetBrains decompiler
// Type: System.Reflection.PInvokeAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  [Serializable]
  internal enum PInvokeAttributes
  {
    NoMangle = 1,
    CharSetMask = 6,
    CharSetNotSpec = 0,
    CharSetAnsi = 2,
    CharSetUnicode = 4,
    CharSetAuto = CharSetUnicode | CharSetAnsi, // 0x00000006
    BestFitUseAssem = 0,
    BestFitEnabled = 16, // 0x00000010
    BestFitDisabled = 32, // 0x00000020
    BestFitMask = BestFitDisabled | BestFitEnabled, // 0x00000030
    ThrowOnUnmappableCharUseAssem = 0,
    ThrowOnUnmappableCharEnabled = 4096, // 0x00001000
    ThrowOnUnmappableCharDisabled = 8192, // 0x00002000
    ThrowOnUnmappableCharMask = ThrowOnUnmappableCharDisabled | ThrowOnUnmappableCharEnabled, // 0x00003000
    SupportsLastError = 64, // 0x00000040
    CallConvMask = 1792, // 0x00000700
    CallConvWinapi = 256, // 0x00000100
    CallConvCdecl = 512, // 0x00000200
    CallConvStdcall = CallConvCdecl | CallConvWinapi, // 0x00000300
    CallConvThiscall = 1024, // 0x00000400
    CallConvFastcall = CallConvThiscall | CallConvWinapi, // 0x00000500
    MaxValue = 65535, // 0x0000FFFF
  }
}
