// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessControlActions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает действия, разрешенные для защищаемых объектов.
  /// </summary>
  [Flags]
  public enum AccessControlActions
  {
    None = 0,
    View = 1,
    Change = 2,
  }
}
