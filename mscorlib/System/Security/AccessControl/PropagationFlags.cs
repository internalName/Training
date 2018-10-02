// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.PropagationFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Указывает, как записи управления доступом (ACE) распространяются на дочерние объекты.
  ///     Эти флаги имеют значение только в том случае, если имеются флаги наследования.
  /// </summary>
  [Flags]
  public enum PropagationFlags
  {
    None = 0,
    NoPropagateInherit = 1,
    InheritOnly = 2,
  }
}
