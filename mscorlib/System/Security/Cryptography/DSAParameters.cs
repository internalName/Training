// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSAParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Содержит типичные параметры для <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct DSAParameters
  {
    /// <summary>
    ///   Указывает <see langword="P" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] P;
    /// <summary>
    ///   Указывает <see langword="Q" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] Q;
    /// <summary>
    ///   Указывает <see langword="G" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] G;
    /// <summary>
    ///   Указывает <see langword="Y" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] Y;
    /// <summary>
    ///   Указывает <see langword="J" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] J;
    /// <summary>
    ///   Указывает <see langword="X" /> параметр <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    [NonSerialized]
    public byte[] X;
    /// <summary>
    ///   Указывает начальное значение <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public byte[] Seed;
    /// <summary>
    ///   Задает счетчик для <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
    /// </summary>
    public int Counter;
  }
}
