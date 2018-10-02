// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.KerbLogonSubmitType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Principal
{
  [Serializable]
  internal enum KerbLogonSubmitType
  {
    KerbInteractiveLogon = 2,
    KerbSmartCardLogon = 6,
    KerbWorkstationUnlockLogon = 7,
    KerbSmartCardUnlockLogon = 8,
    KerbProxyLogon = 9,
    KerbTicketLogon = 10, // 0x0000000A
    KerbTicketUnlockLogon = 11, // 0x0000000B
    KerbS4ULogon = 12, // 0x0000000C
  }
}
