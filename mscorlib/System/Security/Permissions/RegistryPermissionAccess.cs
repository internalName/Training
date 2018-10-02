// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermissionAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает разрешенный доступ к разделам реестра и значения.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum RegistryPermissionAccess
  {
    NoAccess = 0,
    Read = 1,
    Write = 2,
    Create = 4,
    AllAccess = Create | Write | Read, // 0x00000007
  }
}
