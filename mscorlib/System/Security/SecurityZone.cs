// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityZone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>
  ///   Определяет целые значения, соответствующие зонам безопасности, которые используются политикой безопасности.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum SecurityZone
  {
    NoZone = -1,
    MyComputer = 0,
    Intranet = 1,
    Trusted = 2,
    Internet = 3,
    Untrusted = 4,
  }
}
