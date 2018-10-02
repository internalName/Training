// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.HostProtectionResource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Определяет категории функциональных возможностей быть потенциально опасны для узла, если они вызываются методом или классом.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum HostProtectionResource
  {
    None = 0,
    Synchronization = 1,
    SharedState = 2,
    ExternalProcessMgmt = 4,
    SelfAffectingProcessMgmt = 8,
    ExternalThreading = 16, // 0x00000010
    SelfAffectingThreading = 32, // 0x00000020
    SecurityInfrastructure = 64, // 0x00000040
    UI = 128, // 0x00000080
    MayLeakOnAbort = 256, // 0x00000100
    All = MayLeakOnAbort | UI | SecurityInfrastructure | SelfAffectingThreading | ExternalThreading | SelfAffectingProcessMgmt | ExternalProcessMgmt | SharedState | Synchronization, // 0x000001FF
  }
}
