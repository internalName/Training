// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.TokenAccessLevels
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Определяет привилегии учетной записи пользователя, связанной с токеном доступа.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TokenAccessLevels
  {
    AssignPrimary = 1,
    Duplicate = 2,
    Impersonate = 4,
    Query = 8,
    QuerySource = 16, // 0x00000010
    AdjustPrivileges = 32, // 0x00000020
    AdjustGroups = 64, // 0x00000040
    AdjustDefault = 128, // 0x00000080
    AdjustSessionId = 256, // 0x00000100
    Read = 131080, // 0x00020008
    Write = 131296, // 0x000200E0
    AllAccess = 983551, // 0x000F01FF
    MaximumAllowed = 33554432, // 0x02000000
  }
}
