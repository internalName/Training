// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.ISTORE_ENUM_ASSEMBLIES_FLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation
{
  [Flags]
  internal enum ISTORE_ENUM_ASSEMBLIES_FLAGS
  {
    ISTORE_ENUM_ASSEMBLIES_FLAG_LIMIT_TO_VISIBLE_ONLY = 1,
    ISTORE_ENUM_ASSEMBLIES_FLAG_MATCH_SERVICING = 2,
    ISTORE_ENUM_ASSEMBLIES_FLAG_FORCE_LIBRARY_SEMANTICS = 4,
  }
}
