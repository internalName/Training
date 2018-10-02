// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает операции шифрования ключа, для которого правило авторизации определяет, доступ и аудит.
  /// </summary>
  [Flags]
  public enum CryptoKeyRights
  {
    ReadData = 1,
    WriteData = 2,
    ReadExtendedAttributes = 8,
    WriteExtendedAttributes = 16, // 0x00000010
    ReadAttributes = 128, // 0x00000080
    WriteAttributes = 256, // 0x00000100
    Delete = 65536, // 0x00010000
    ReadPermissions = 131072, // 0x00020000
    ChangePermissions = 262144, // 0x00040000
    TakeOwnership = 524288, // 0x00080000
    Synchronize = 1048576, // 0x00100000
    FullControl = Synchronize | TakeOwnership | ChangePermissions | ReadPermissions | Delete | WriteAttributes | ReadAttributes | WriteExtendedAttributes | ReadExtendedAttributes | WriteData | ReadData, // 0x001F019B
    GenericAll = 268435456, // 0x10000000
    GenericExecute = 536870912, // 0x20000000
    GenericWrite = 1073741824, // 0x40000000
    GenericRead = -2147483648, // -0x80000000
  }
}
