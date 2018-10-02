// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PInvokeMap
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  [Serializable]
  internal enum PInvokeMap
  {
    CharSetNotSpec = 0,
    NoMangle = 1,
    CharSetAnsi = 2,
    CharSetUnicode = 4,
    CharSetAuto = 6,
    CharSetMask = 6,
    BestFitEnabled = 16, // 0x00000010
    BestFitDisabled = 32, // 0x00000020
    PinvokeOLE = 32, // 0x00000020
    BestFitMask = 48, // 0x00000030
    BestFitUseAsm = 48, // 0x00000030
    SupportsLastError = 64, // 0x00000040
    CallConvWinapi = 256, // 0x00000100
    CallConvCdecl = 512, // 0x00000200
    CallConvStdcall = 768, // 0x00000300
    CallConvThiscall = 1024, // 0x00000400
    CallConvFastcall = 1280, // 0x00000500
    CallConvMask = 1792, // 0x00000700
    ThrowOnUnmappableCharEnabled = 4096, // 0x00001000
    ThrowOnUnmappableCharDisabled = 8192, // 0x00002000
    ThrowOnUnmappableCharMask = 12288, // 0x00003000
    ThrowOnUnmappableCharUseAsm = 12288, // 0x00003000
  }
}
