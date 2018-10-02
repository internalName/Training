// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStorageContainment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает разрешенное использование изолированного хранилища.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum IsolatedStorageContainment
  {
    None = 0,
    DomainIsolationByUser = 16, // 0x00000010
    ApplicationIsolationByUser = 21, // 0x00000015
    AssemblyIsolationByUser = 32, // 0x00000020
    DomainIsolationByMachine = 48, // 0x00000030
    AssemblyIsolationByMachine = 64, // 0x00000040
    ApplicationIsolationByMachine = 69, // 0x00000045
    DomainIsolationByRoamingUser = 80, // 0x00000050
    AssemblyIsolationByRoamingUser = 96, // 0x00000060
    ApplicationIsolationByRoamingUser = 101, // 0x00000065
    AdministerIsolatedStorageByUser = 112, // 0x00000070
    UnrestrictedIsolatedStorage = 240, // 0x000000F0
  }
}
