// Decompiled with JetBrains decompiler
// Type: System.DelegateBindingFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum DelegateBindingFlags
  {
    StaticMethodOnly = 1,
    InstanceMethodOnly = 2,
    OpenDelegateOnly = 4,
    ClosedDelegateOnly = 8,
    NeverCloseOverNull = 16, // 0x00000010
    CaselessMatching = 32, // 0x00000020
    SkipSecurityChecks = 64, // 0x00000040
    RelaxedSignature = 128, // 0x00000080
  }
}
