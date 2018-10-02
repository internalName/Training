// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.SxSRequirements
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  [Flags]
  internal enum SxSRequirements
  {
    None = 0,
    AppDomainID = 1,
    ProcessID = 2,
    CLRInstanceID = 4,
    AssemblyName = 8,
    TypeName = 16, // 0x00000010
  }
}
