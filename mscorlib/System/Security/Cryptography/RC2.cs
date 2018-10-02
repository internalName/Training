// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RC2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, от которого должны производиться все реализации алгоритма <see cref="T:System.Security.Cryptography.RC2" />.
  /// </summary>
  [ComVisible(true)]
  public abstract class RC2 : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
    {
      new KeySizes(64, 64, 0)
    };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(40, 1024, 8)
    };
    /// <summary>
    ///   Представляет эффективный размер секретного ключа, используемого <see cref="T:System.Security.Cryptography.RC2" /> алгоритма в битах.
    /// </summary>
    protected int EffectiveKeySizeValue;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.RC2" />.
    /// </summary>
    protected RC2()
    {
      this.KeySizeValue = 128;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
      this.LegalKeySizesValue = RC2.s_legalKeySizes;
    }

    /// <summary>
    ///   Получает или задает эффективный размер секретного ключа, используемого алгоритмом <see cref="T:System.Security.Cryptography.RC2" />, в битах.
    /// </summary>
    /// <returns>
    ///   Эффективный размер ключа, используемого алгоритмом <see cref="T:System.Security.Cryptography.RC2" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый эффективный размер ключа.
    /// </exception>
    public virtual int EffectiveKeySize
    {
      get
      {
        if (this.EffectiveKeySizeValue == 0)
          return this.KeySizeValue;
        return this.EffectiveKeySizeValue;
      }
      set
      {
        if (value > this.KeySizeValue)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
        if (value == 0)
        {
          this.EffectiveKeySizeValue = value;
        }
        else
        {
          if (value < 40)
            throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKS40"));
          if (!this.ValidKeySize(value))
            throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
          this.EffectiveKeySizeValue = value;
        }
      }
    }

    /// <summary>
    ///   Получает или задает размер секретного ключа, используемого алгоритмом <see cref="T:System.Security.Cryptography.RC2" />, в битах.
    /// </summary>
    /// <returns>
    ///   Размер секретного ключа, используемого алгоритмом <see cref="T:System.Security.Cryptography.RC2" />, в битах.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение для размера ключа RC2 меньше значения эффективного размера ключа.
    /// </exception>
    public override int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (value < this.EffectiveKeySizeValue)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
        base.KeySize = value;
      }
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения алгоритма <see cref="T:System.Security.Cryptography.RC2" />.
    /// </summary>
    /// <returns>Экземпляр криптографического объекта.</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Этот алгоритм был использован с включенным режимом FIPS, однако он не совместим с FIPS.
    /// </exception>
    public static RC2 Create()
    {
      return RC2.Create("System.Security.Cryptography.RC2");
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения заданной реализации <see cref="T:System.Security.Cryptography.RC2" /> алгоритма.
    /// </summary>
    /// <param name="AlgName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.RC2" /> для использования.
    /// </param>
    /// <returns>Экземпляр криптографического объекта.</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="algName" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static RC2 Create(string AlgName)
    {
      return (RC2) CryptoConfig.CreateFromName(AlgName);
    }
  }
}
