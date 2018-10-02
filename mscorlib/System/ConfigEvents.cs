// Decompiled with JetBrains decompiler
// Type: System.ConfigEvents
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Serializable]
  internal enum ConfigEvents
  {
    StartDocument = 0,
    StartDTD = 1,
    EndDTD = 2,
    StartDTDSubset = 3,
    EndDTDSubset = 4,
    EndProlog = 5,
    StartEntity = 6,
    EndEntity = 7,
    EndDocument = 8,
    DataAvailable = 9,
    LastEvent = 9,
  }
}
