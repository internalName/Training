// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityCriticalScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Задает область <see cref="T:System.Security.SecurityCriticalAttribute" />.
  /// </summary>
  [Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
  public enum SecurityCriticalScope
  {
    Explicit,
    Everything,
  }
}
