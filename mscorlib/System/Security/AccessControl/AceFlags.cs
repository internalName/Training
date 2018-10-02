// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AceFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Определяет порядок наследования и поведения аудита записи управления доступом (ACE).
  /// </summary>
  [Flags]
  public enum AceFlags : byte
  {
    None = 0,
    ObjectInherit = 1,
    ContainerInherit = 2,
    NoPropagateInherit = 4,
    InheritOnly = 8,
    Inherited = 16, // 0x10
    SuccessfulAccess = 64, // 0x40
    FailedAccess = 128, // 0x80
    InheritanceFlags = InheritOnly | NoPropagateInherit | ContainerInherit | ObjectInherit, // 0x0F
    AuditFlags = FailedAccess | SuccessfulAccess, // 0xC0
  }
}
