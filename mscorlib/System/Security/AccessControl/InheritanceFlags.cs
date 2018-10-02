// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.InheritanceFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Флаги наследования определяют семантику наследования записи управления доступом (ACE).
  /// </summary>
  [Flags]
  public enum InheritanceFlags
  {
    None = 0,
    ContainerInherit = 1,
    ObjectInherit = 2,
  }
}
