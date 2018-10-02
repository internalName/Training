// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreTransactionOperationType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation
{
  internal enum StoreTransactionOperationType
  {
    Invalid = 0,
    SetCanonicalizationContext = 14, // 0x0000000E
    StageComponent = 20, // 0x00000014
    PinDeployment = 21, // 0x00000015
    UnpinDeployment = 22, // 0x00000016
    StageComponentFile = 23, // 0x00000017
    InstallDeployment = 24, // 0x00000018
    UninstallDeployment = 25, // 0x00000019
    SetDeploymentMetadata = 26, // 0x0000001A
    Scavenge = 27, // 0x0000001B
  }
}
