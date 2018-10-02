// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.STORE_ASSEMBLY_STATUS_FLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation
{
  [Flags]
  internal enum STORE_ASSEMBLY_STATUS_FLAGS
  {
    STORE_ASSEMBLY_STATUS_MANIFEST_ONLY = 1,
    STORE_ASSEMBLY_STATUS_PAYLOAD_RESIDENT = 2,
    STORE_ASSEMBLY_STATUS_PARTIAL_INSTALL = 4,
  }
}
