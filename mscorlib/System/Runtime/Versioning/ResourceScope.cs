// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>Определяет область видимости общего ресурса.</summary>
  [Flags]
  public enum ResourceScope
  {
    None = 0,
    Machine = 1,
    Process = 2,
    AppDomain = 4,
    Library = 8,
    Private = 16, // 0x00000010
    Assembly = 32, // 0x00000020
  }
}
