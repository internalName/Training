// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ComponentGuaranteesOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Описывает гарантированную совместимость компонента, типа или члена типа, который может занимать несколько версий.
  /// </summary>
  [Flags]
  [Serializable]
  public enum ComponentGuaranteesOptions
  {
    None = 0,
    Exchange = 1,
    Stable = 2,
    SideBySide = 4,
  }
}
