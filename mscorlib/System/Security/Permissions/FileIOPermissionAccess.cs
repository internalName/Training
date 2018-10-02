// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOPermissionAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>Указывает файл запрошенный тип доступа.</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileIOPermissionAccess
  {
    NoAccess = 0,
    Read = 1,
    Write = 2,
    Append = 4,
    PathDiscovery = 8,
    AllAccess = PathDiscovery | Append | Write | Read, // 0x0000000F
  }
}
