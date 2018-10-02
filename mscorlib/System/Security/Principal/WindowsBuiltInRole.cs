// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsBuiltInRole
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Задает основные роли для использования с <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum WindowsBuiltInRole
  {
    Administrator = 544, // 0x00000220
    User = 545, // 0x00000221
    Guest = 546, // 0x00000222
    PowerUser = 547, // 0x00000223
    AccountOperator = 548, // 0x00000224
    SystemOperator = 549, // 0x00000225
    PrintOperator = 550, // 0x00000226
    BackupOperator = 551, // 0x00000227
    Replicator = 552, // 0x00000228
  }
}
