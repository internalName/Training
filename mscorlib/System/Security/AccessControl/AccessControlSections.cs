// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessControlSections
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Определяет разделы дескриптора безопасности для сохранения и загрузки.
  /// </summary>
  [Flags]
  public enum AccessControlSections
  {
    None = 0,
    Audit = 1,
    Access = 2,
    Owner = 4,
    Group = 8,
    All = Group | Owner | Access | Audit, // 0x0000000F
  }
}
