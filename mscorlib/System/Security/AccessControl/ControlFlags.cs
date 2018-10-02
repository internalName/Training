// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ControlFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Эти флаги влияют на поведение дескриптора безопасности.
  /// </summary>
  [Flags]
  public enum ControlFlags
  {
    None = 0,
    OwnerDefaulted = 1,
    GroupDefaulted = 2,
    DiscretionaryAclPresent = 4,
    DiscretionaryAclDefaulted = 8,
    SystemAclPresent = 16, // 0x00000010
    SystemAclDefaulted = 32, // 0x00000020
    DiscretionaryAclUntrusted = 64, // 0x00000040
    ServerSecurity = 128, // 0x00000080
    DiscretionaryAclAutoInheritRequired = 256, // 0x00000100
    SystemAclAutoInheritRequired = 512, // 0x00000200
    DiscretionaryAclAutoInherited = 1024, // 0x00000400
    SystemAclAutoInherited = 2048, // 0x00000800
    DiscretionaryAclProtected = 4096, // 0x00001000
    SystemAclProtected = 8192, // 0x00002000
    RMControlValid = 16384, // 0x00004000
    SelfRelative = 32768, // 0x00008000
  }
}
