// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает, что права доступа, которые могут применяться к объектам именованных системных событий.
  /// </summary>
  [Flags]
  public enum EventWaitHandleRights
  {
    Modify = 2,
    Delete = 65536, // 0x00010000
    ReadPermissions = 131072, // 0x00020000
    ChangePermissions = 262144, // 0x00040000
    TakeOwnership = 524288, // 0x00080000
    Synchronize = 1048576, // 0x00100000
    FullControl = 2031619, // 0x001F0003
  }
}
