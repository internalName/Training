// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.KeySizes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет набор допустимых размеров ключа для симметричных алгоритмов шифрования.
  /// </summary>
  [ComVisible(true)]
  public sealed class KeySizes
  {
    private int m_minSize;
    private int m_maxSize;
    private int m_skipSize;

    /// <summary>Задает минимальный размер ключа в битах.</summary>
    /// <returns>Минимальный размер ключа в битах.</returns>
    public int MinSize
    {
      get
      {
        return this.m_minSize;
      }
    }

    /// <summary>Задает максимальный размер ключа в битах.</summary>
    /// <returns>Максимальный размер ключа в битах.</returns>
    public int MaxSize
    {
      get
      {
        return this.m_maxSize;
      }
    }

    /// <summary>
    ///   Задает интервал между допустимыми размерами ключа в битах.
    /// </summary>
    /// <returns>Интервал между допустимыми размерами ключа в битах.</returns>
    public int SkipSize
    {
      get
      {
        return this.m_skipSize;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.KeySizes" /> с указанными значениями ключа.
    /// </summary>
    /// <param name="minSize">Минимально допустимый размер ключа.</param>
    /// <param name="maxSize">Максимально допустимый размер ключа.</param>
    /// <param name="skipSize">
    ///   Интервал между допустимыми размерами ключа.
    /// </param>
    public KeySizes(int minSize, int maxSize, int skipSize)
    {
      this.m_minSize = minSize;
      this.m_maxSize = maxSize;
      this.m_skipSize = skipSize;
    }
  }
}
