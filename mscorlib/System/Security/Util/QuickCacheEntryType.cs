// Decompiled with JetBrains decompiler
// Type: System.Security.Util.QuickCacheEntryType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Util
{
  [Flags]
  [Serializable]
  internal enum QuickCacheEntryType
  {
    FullTrustZoneMyComputer = 16777216, // 0x01000000
    FullTrustZoneIntranet = 33554432, // 0x02000000
    FullTrustZoneInternet = 67108864, // 0x04000000
    FullTrustZoneTrusted = 134217728, // 0x08000000
    FullTrustZoneUntrusted = 268435456, // 0x10000000
    FullTrustAll = 536870912, // 0x20000000
  }
}
