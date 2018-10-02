// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  [Serializable]
  internal enum PermissionType
  {
    SecurityUnmngdCodeAccess = 0,
    SecuritySkipVerification = 1,
    ReflectionTypeInfo = 2,
    SecurityAssert = 3,
    ReflectionMemberAccess = 4,
    SecuritySerialization = 5,
    ReflectionRestrictedMemberAccess = 6,
    FullTrust = 7,
    SecurityBindingRedirects = 8,
    UIPermission = 9,
    EnvironmentPermission = 10, // 0x0000000A
    FileDialogPermission = 11, // 0x0000000B
    FileIOPermission = 12, // 0x0000000C
    ReflectionPermission = 13, // 0x0000000D
    SecurityPermission = 14, // 0x0000000E
    SecurityControlEvidence = 16, // 0x00000010
    SecurityControlPrincipal = 17, // 0x00000011
  }
}
