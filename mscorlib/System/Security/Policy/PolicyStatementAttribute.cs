// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyStatementAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет специальные флаги атрибутов для политики безопасности на группы кода.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum PolicyStatementAttribute
  {
    Nothing = 0,
    Exclusive = 1,
    LevelFinal = 2,
    All = LevelFinal | Exclusive, // 0x00000003
  }
}
