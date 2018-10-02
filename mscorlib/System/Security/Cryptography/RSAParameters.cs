// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет стандартные параметры для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct RSAParameters
  {
    /// <summary>
    ///   Представляет параметр <see langword="Exponent" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    public byte[] Exponent;
    /// <summary>
    ///   Представляет параметр <see langword="Modulus" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    public byte[] Modulus;
    /// <summary>
    ///   Представляет параметр <see langword="P" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] P;
    /// <summary>
    ///   Представляет параметр <see langword="Q" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] Q;
    /// <summary>
    ///   Представляет параметр <see langword="DP" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] DP;
    /// <summary>
    ///   Представляет параметр <see langword="DQ" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] DQ;
    /// <summary>
    ///   Представляет параметр <see langword="InverseQ" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] InverseQ;
    /// <summary>
    ///   Представляет параметр <see langword="D" /> для алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    [NonSerialized]
    public byte[] D;
  }
}
