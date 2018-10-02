// Decompiled with JetBrains decompiler
// Type: System.Security.PartialTrustVisibilityLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Задает видимость частичного доверия по умолчанию для кода, помеченный атрибутом <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> атрибут (APTCA).
  /// </summary>
  public enum PartialTrustVisibilityLevel
  {
    VisibleToAllHosts,
    NotVisibleByDefault,
  }
}
