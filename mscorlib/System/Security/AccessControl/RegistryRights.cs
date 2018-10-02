// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistryRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает права доступа, которые можно применять к объектам реестра.
  /// </summary>
  [Flags]
  public enum RegistryRights
  {
    QueryValues = 1,
    SetValue = 2,
    CreateSubKey = 4,
    EnumerateSubKeys = 8,
    Notify = 16, // 0x00000010
    CreateLink = 32, // 0x00000020
    ExecuteKey = 131097, // 0x00020019
    ReadKey = ExecuteKey, // 0x00020019
    WriteKey = 131078, // 0x00020006
    Delete = 65536, // 0x00010000
    ReadPermissions = 131072, // 0x00020000
    ChangePermissions = 262144, // 0x00040000
    TakeOwnership = 524288, // 0x00080000
    FullControl = TakeOwnership | ChangePermissions | ReadPermissions | Delete | CreateLink | Notify | EnumerateSubKeys | CreateSubKey | SetValue | QueryValues, // 0x000F003F
  }
}
