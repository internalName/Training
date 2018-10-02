// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Aes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, от которого должны наследоваться все реализации стандарта AES.
  /// </summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  public abstract class Aes : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
    {
      new KeySizes(128, 128, 0)
    };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(128, 256, 64)
    };

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Aes" />.
    /// </summary>
    protected Aes()
    {
      this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
      this.LegalKeySizesValue = Aes.s_legalKeySizes;
      this.BlockSizeValue = 128;
      this.FeedbackSizeValue = 8;
      this.KeySizeValue = 256;
      this.ModeValue = CipherMode.CBC;
    }

    /// <summary>
    ///   Создает криптографический объект, используемый для выполнения симметричного алгоритма.
    /// </summary>
    /// <returns>
    ///   Криптографический объект, используемый для выполнения симметричного алгоритма.
    /// </returns>
    public static Aes Create()
    {
      return Aes.Create("AES");
    }

    /// <summary>
    ///   Создает криптографический объект, задающий реализацию AES для выполнения симметричного алгоритма.
    /// </summary>
    /// <param name="algorithmName">
    ///   Имя конкретной реализации AES, которую требуется использовать.
    /// </param>
    /// <returns>
    ///   Криптографический объект, используемый для выполнения симметричного алгоритма.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="algorithmName" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Aes Create(string algorithmName)
    {
      if (algorithmName == null)
        throw new ArgumentNullException(nameof (algorithmName));
      return CryptoConfig.CreateFromName(algorithmName) as Aes;
    }
  }
}
