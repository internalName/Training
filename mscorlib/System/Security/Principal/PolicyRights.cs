// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.PolicyRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Principal
{
  [Flags]
  internal enum PolicyRights
  {
    POLICY_VIEW_LOCAL_INFORMATION = 1,
    POLICY_VIEW_AUDIT_INFORMATION = 2,
    POLICY_GET_PRIVATE_INFORMATION = 4,
    POLICY_TRUST_ADMIN = 8,
    POLICY_CREATE_ACCOUNT = 16, // 0x00000010
    POLICY_CREATE_SECRET = 32, // 0x00000020
    POLICY_CREATE_PRIVILEGE = 64, // 0x00000040
    POLICY_SET_DEFAULT_QUOTA_LIMITS = 128, // 0x00000080
    POLICY_SET_AUDIT_REQUIREMENTS = 256, // 0x00000100
    POLICY_AUDIT_LOG_ADMIN = 512, // 0x00000200
    POLICY_SERVER_ADMIN = 1024, // 0x00000400
    POLICY_LOOKUP_NAMES = 2048, // 0x00000800
    POLICY_NOTIFICATION = 4096, // 0x00001000
  }
}
