// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает тип разрешенного доступа контейнера ключа.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum KeyContainerPermissionFlags
  {
    NoFlags = 0,
    Create = 1,
    Open = 2,
    Delete = 4,
    Import = 16, // 0x00000010
    Export = 32, // 0x00000020
    Sign = 256, // 0x00000100
    Decrypt = 512, // 0x00000200
    ViewAcl = 4096, // 0x00001000
    ChangeAcl = 8192, // 0x00002000
    AllFlags = ChangeAcl | ViewAcl | Decrypt | Sign | Export | Import | Delete | Open | Create, // 0x00003337
  }
}
