// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.SecurityLogonType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Principal
{
  [Serializable]
  internal enum SecurityLogonType
  {
    Interactive = 2,
    Network = 3,
    Batch = 4,
    Service = 5,
    Proxy = 6,
    Unlock = 7,
  }
}
