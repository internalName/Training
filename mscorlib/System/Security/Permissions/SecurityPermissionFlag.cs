// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает флаги доступа для объекта разрешения безопасности.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum SecurityPermissionFlag
  {
    NoFlags = 0,
    Assertion = 1,
    UnmanagedCode = 2,
    SkipVerification = 4,
    Execution = 8,
    ControlThread = 16, // 0x00000010
    ControlEvidence = 32, // 0x00000020
    ControlPolicy = 64, // 0x00000040
    SerializationFormatter = 128, // 0x00000080
    ControlDomainPolicy = 256, // 0x00000100
    ControlPrincipal = 512, // 0x00000200
    ControlAppDomain = 1024, // 0x00000400
    RemotingConfiguration = 2048, // 0x00000800
    Infrastructure = 4096, // 0x00001000
    BindingRedirects = 8192, // 0x00002000
    AllFlags = BindingRedirects | Infrastructure | RemotingConfiguration | ControlAppDomain | ControlPrincipal | ControlDomainPolicy | SerializationFormatter | ControlPolicy | ControlEvidence | ControlThread | Execution | SkipVerification | UnmanagedCode | Assertion, // 0x00003FFF
  }
}
