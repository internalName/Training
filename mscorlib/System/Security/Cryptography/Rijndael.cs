// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Rijndael
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, из которого создаются все реализации <see cref="T:System.Security.Cryptography.Rijndael" /> должен наследовать алгоритм симметричного шифрования.
  /// </summary>
  [ComVisible(true)]
  public abstract class Rijndael : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
    {
      new KeySizes(128, 256, 64)
    };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(128, 256, 64)
    };

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.Rijndael" />.
    /// </summary>
    protected Rijndael()
    {
      this.KeySizeValue = 256;
      this.BlockSizeValue = 128;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
      this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
    }

    /// <summary>
    ///   Создает криптографический объект для выполнения <see cref="T:System.Security.Cryptography.Rijndael" /> алгоритма.
    /// </summary>
    /// <returns>Криптографический объект.</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Этот алгоритм был использован с включенным режимом FIPS, однако он не совместим с FIPS.
    /// </exception>
    public static Rijndael Create()
    {
      return Rijndael.Create("System.Security.Cryptography.Rijndael");
    }

    /// <summary>
    ///   Создает криптографический объект для выполнения заданной реализации <see cref="T:System.Security.Cryptography.Rijndael" /> алгоритма.
    /// </summary>
    /// <param name="algName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.Rijndael" /> для создания.
    /// </param>
    /// <returns>Криптографический объект.</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="algName" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static Rijndael Create(string algName)
    {
      return (Rijndael) CryptoConfig.CreateFromName(algName);
    }
  }
}
