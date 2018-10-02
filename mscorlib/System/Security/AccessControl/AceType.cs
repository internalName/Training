// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AceType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Определяет типы доступа элемента управления доступом.
  /// </summary>
  public enum AceType : byte
  {
    AccessAllowed = 0,
    AccessDenied = 1,
    SystemAudit = 2,
    SystemAlarm = 3,
    AccessAllowedCompound = 4,
    AccessAllowedObject = 5,
    AccessDeniedObject = 6,
    SystemAuditObject = 7,
    SystemAlarmObject = 8,
    AccessAllowedCallback = 9,
    AccessDeniedCallback = 10, // 0x0A
    AccessAllowedCallbackObject = 11, // 0x0B
    AccessDeniedCallbackObject = 12, // 0x0C
    SystemAuditCallback = 13, // 0x0D
    SystemAlarmCallback = 14, // 0x0E
    SystemAuditCallbackObject = 15, // 0x0F
    MaxDefinedAceType = 16, // 0x10
    SystemAlarmCallbackObject = 16, // 0x10
  }
}
