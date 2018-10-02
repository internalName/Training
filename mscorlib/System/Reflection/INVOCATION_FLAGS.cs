﻿// Decompiled with JetBrains decompiler
// Type: System.Reflection.INVOCATION_FLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  internal enum INVOCATION_FLAGS : uint
  {
    INVOCATION_FLAGS_UNKNOWN = 0,
    INVOCATION_FLAGS_INITIALIZED = 1,
    INVOCATION_FLAGS_NO_INVOKE = 2,
    INVOCATION_FLAGS_NEED_SECURITY = 4,
    INVOCATION_FLAGS_NO_CTOR_INVOKE = 8,
    INVOCATION_FLAGS_IS_CTOR = 16, // 0x00000010
    INVOCATION_FLAGS_RISKY_METHOD = 32, // 0x00000020
    INVOCATION_FLAGS_NON_W8P_FX_API = 64, // 0x00000040
    INVOCATION_FLAGS_IS_DELEGATE_CTOR = 128, // 0x00000080
    INVOCATION_FLAGS_CONTAINS_STACK_POINTERS = 256, // 0x00000100
    INVOCATION_FLAGS_SPECIAL_FIELD = INVOCATION_FLAGS_IS_CTOR, // 0x00000010
    INVOCATION_FLAGS_FIELD_SPECIAL_CAST = INVOCATION_FLAGS_RISKY_METHOD, // 0x00000020
    INVOCATION_FLAGS_CONSTRUCTOR_INVOKE = 268435456, // 0x10000000
  }
}
