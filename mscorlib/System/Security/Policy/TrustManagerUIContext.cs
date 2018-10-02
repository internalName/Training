// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.TrustManagerUIContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Указывает тип пользовательского интерфейса (UI), диспетчер доверия следует использовать для решений о доверии.
  /// </summary>
  [ComVisible(true)]
  public enum TrustManagerUIContext
  {
    Install,
    Upgrade,
    Run,
  }
}
