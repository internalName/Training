// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WellKnownSidType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Определяет набор часто используемых идентификаторов безопасности (SID).
  /// </summary>
  [ComVisible(false)]
  public enum WellKnownSidType
  {
    NullSid = 0,
    WorldSid = 1,
    LocalSid = 2,
    CreatorOwnerSid = 3,
    CreatorGroupSid = 4,
    CreatorOwnerServerSid = 5,
    CreatorGroupServerSid = 6,
    NTAuthoritySid = 7,
    DialupSid = 8,
    NetworkSid = 9,
    BatchSid = 10, // 0x0000000A
    InteractiveSid = 11, // 0x0000000B
    ServiceSid = 12, // 0x0000000C
    AnonymousSid = 13, // 0x0000000D
    ProxySid = 14, // 0x0000000E
    EnterpriseControllersSid = 15, // 0x0000000F
    SelfSid = 16, // 0x00000010
    AuthenticatedUserSid = 17, // 0x00000011
    RestrictedCodeSid = 18, // 0x00000012
    TerminalServerSid = 19, // 0x00000013
    RemoteLogonIdSid = 20, // 0x00000014
    LogonIdsSid = 21, // 0x00000015
    LocalSystemSid = 22, // 0x00000016
    LocalServiceSid = 23, // 0x00000017
    NetworkServiceSid = 24, // 0x00000018
    BuiltinDomainSid = 25, // 0x00000019
    BuiltinAdministratorsSid = 26, // 0x0000001A
    BuiltinUsersSid = 27, // 0x0000001B
    BuiltinGuestsSid = 28, // 0x0000001C
    BuiltinPowerUsersSid = 29, // 0x0000001D
    BuiltinAccountOperatorsSid = 30, // 0x0000001E
    BuiltinSystemOperatorsSid = 31, // 0x0000001F
    BuiltinPrintOperatorsSid = 32, // 0x00000020
    BuiltinBackupOperatorsSid = 33, // 0x00000021
    BuiltinReplicatorSid = 34, // 0x00000022
    BuiltinPreWindows2000CompatibleAccessSid = 35, // 0x00000023
    BuiltinRemoteDesktopUsersSid = 36, // 0x00000024
    BuiltinNetworkConfigurationOperatorsSid = 37, // 0x00000025
    AccountAdministratorSid = 38, // 0x00000026
    AccountGuestSid = 39, // 0x00000027
    AccountKrbtgtSid = 40, // 0x00000028
    AccountDomainAdminsSid = 41, // 0x00000029
    AccountDomainUsersSid = 42, // 0x0000002A
    AccountDomainGuestsSid = 43, // 0x0000002B
    AccountComputersSid = 44, // 0x0000002C
    AccountControllersSid = 45, // 0x0000002D
    AccountCertAdminsSid = 46, // 0x0000002E
    AccountSchemaAdminsSid = 47, // 0x0000002F
    AccountEnterpriseAdminsSid = 48, // 0x00000030
    AccountPolicyAdminsSid = 49, // 0x00000031
    AccountRasAndIasServersSid = 50, // 0x00000032
    NtlmAuthenticationSid = 51, // 0x00000033
    DigestAuthenticationSid = 52, // 0x00000034
    SChannelAuthenticationSid = 53, // 0x00000035
    ThisOrganizationSid = 54, // 0x00000036
    OtherOrganizationSid = 55, // 0x00000037
    BuiltinIncomingForestTrustBuildersSid = 56, // 0x00000038
    BuiltinPerformanceMonitoringUsersSid = 57, // 0x00000039
    BuiltinPerformanceLoggingUsersSid = 58, // 0x0000003A
    BuiltinAuthorizationAccessSid = 59, // 0x0000003B
    MaxDefined = 60, // 0x0000003C
    WinBuiltinTerminalServerLicenseServersSid = 60, // 0x0000003C
  }
}
