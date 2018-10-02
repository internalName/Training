// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CipherMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Задает режим блочного шифра для использования при шифровании.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum CipherMode
  {
    CBC = 1,
    ECB = 2,
    OFB = 3,
    CFB = 4,
    CTS = 5,
  }
}
