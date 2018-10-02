// Decompiled with JetBrains decompiler
// Type: System.Security.HostSecurityManagerOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>
  ///   Указывает компоненты политики безопасности, используемый диспетчером безопасности узла.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum HostSecurityManagerOptions
  {
    None = 0,
    HostAppDomainEvidence = 1,
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] HostPolicyLevel = 2,
    HostAssemblyEvidence = 4,
    HostDetermineApplicationTrust = 8,
    HostResolvePolicy = 16, // 0x00000010
    AllFlags = HostResolvePolicy | HostDetermineApplicationTrust | HostAssemblyEvidence | HostPolicyLevel | HostAppDomainEvidence, // 0x0000001F
  }
}
