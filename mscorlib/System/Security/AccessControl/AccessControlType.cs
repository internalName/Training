// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessControlType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает, является ли <see cref="T:System.Security.AccessControl.AccessRule" /> объект используется для разрешения или запрета доступа.
  ///    Эти значения не являются флагами и их нельзя объединять.
  /// </summary>
  public enum AccessControlType
  {
    Allow,
    Deny,
  }
}
