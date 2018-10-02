// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.BuiltInPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Permissions
{
  [Serializable]
  internal enum BuiltInPermissionFlag
  {
    EnvironmentPermission = 1,
    FileDialogPermission = 2,
    FileIOPermission = 4,
    IsolatedStorageFilePermission = 8,
    ReflectionPermission = 16, // 0x00000010
    RegistryPermission = 32, // 0x00000020
    SecurityPermission = 64, // 0x00000040
    UIPermission = 128, // 0x00000080
    PrincipalPermission = 256, // 0x00000100
    PublisherIdentityPermission = 512, // 0x00000200
    SiteIdentityPermission = 1024, // 0x00000400
    StrongNameIdentityPermission = 2048, // 0x00000800
    UrlIdentityPermission = 4096, // 0x00001000
    ZoneIdentityPermission = 8192, // 0x00002000
    KeyContainerPermission = 16384, // 0x00004000
  }
}
