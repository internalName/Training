// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Определяет права доступа, используемый при создании правил доступа и аудита.
  /// </summary>
  [Flags]
  public enum FileSystemRights
  {
    ReadData = 1,
    ListDirectory = ReadData, // 0x00000001
    WriteData = 2,
    CreateFiles = WriteData, // 0x00000002
    AppendData = 4,
    CreateDirectories = AppendData, // 0x00000004
    ReadExtendedAttributes = 8,
    WriteExtendedAttributes = 16, // 0x00000010
    ExecuteFile = 32, // 0x00000020
    Traverse = ExecuteFile, // 0x00000020
    DeleteSubdirectoriesAndFiles = 64, // 0x00000040
    ReadAttributes = 128, // 0x00000080
    WriteAttributes = 256, // 0x00000100
    Delete = 65536, // 0x00010000
    ReadPermissions = 131072, // 0x00020000
    ChangePermissions = 262144, // 0x00040000
    TakeOwnership = 524288, // 0x00080000
    Synchronize = 1048576, // 0x00100000
    FullControl = Synchronize | TakeOwnership | ChangePermissions | ReadPermissions | Delete | WriteAttributes | ReadAttributes | DeleteSubdirectoriesAndFiles | Traverse | WriteExtendedAttributes | ReadExtendedAttributes | CreateDirectories | CreateFiles | ListDirectory, // 0x001F01FF
    Read = ReadPermissions | ReadAttributes | ReadExtendedAttributes | ListDirectory, // 0x00020089
    ReadAndExecute = Read | Traverse, // 0x000200A9
    Write = WriteAttributes | WriteExtendedAttributes | CreateDirectories | CreateFiles, // 0x00000116
    Modify = Write | ReadAndExecute | Delete, // 0x000301BF
  }
}
