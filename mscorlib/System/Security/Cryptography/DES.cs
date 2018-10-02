// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс для алгоритмов DES, от которых должны наследовать все реализации <see cref="T:System.Security.Cryptography.DES" />.
  /// </summary>
  [ComVisible(true)]
  public abstract class DES : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
    {
      new KeySizes(64, 64, 0)
    };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(64, 64, 0)
    };

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.DES" />.
    /// </summary>
    protected DES()
    {
      this.KeySizeValue = 64;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = DES.s_legalBlockSizes;
      this.LegalKeySizesValue = DES.s_legalKeySizes;
    }

    /// <summary>
    ///   Получает или задает секретный ключ для алгоритма DES (<see cref="T:System.Security.Cryptography.DES" />).
    /// </summary>
    /// <returns>
    ///   Секретный ключ для алгоритма <see cref="T:System.Security.Cryptography.DES" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Предпринята попытка установить для этого ключа значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка установить ключ, длина которого не равна <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.BlockSizeValue" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Предпринята попытка установить слабый ключ (см. <see cref="M:System.Security.Cryptography.DES.IsWeakKey(System.Byte[])" />) или частично слабый ключ (см. <see cref="M:System.Security.Cryptography.DES.IsSemiWeakKey(System.Byte[])" />).
    /// </exception>
    public override byte[] Key
    {
      get
      {
        if (this.KeyValue == null)
        {
          do
          {
            this.GenerateKey();
          }
          while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue));
        }
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (!this.ValidKeySize(value.Length * 8))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        if (DES.IsWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), nameof (DES));
        if (DES.IsSemiWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_SemiWeak"), nameof (DES));
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения алгоритма DES (<see cref="T:System.Security.Cryptography.DES" />).
    /// </summary>
    /// <returns>Криптографический объект.</returns>
    public static DES Create()
    {
      return DES.Create("System.Security.Cryptography.DES");
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения заданной реализации стандарта шифрования данных (<see cref="T:System.Security.Cryptography.DES" />) алгоритма.
    /// </summary>
    /// <param name="algName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.DES" /> для использования.
    /// </param>
    /// <returns>Криптографический объект.</returns>
    public static DES Create(string algName)
    {
      return (DES) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>Определяет, является ли указанный ключ слабым.</summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, проверяемый на слабость.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ слабый; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер параметра <paramref name="rgbKey" />.
    /// </exception>
    public static bool IsWeakKey(byte[] rgbKey)
    {
      if (!DES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      switch (DES.QuadWordFromBigEndian(Utils.FixupKeyParity(rgbKey)))
      {
        case 72340172838076673:
        case 2242545357694045710:
        case 16204198716015505905:
        case 18374403900871474942:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Определяет, является ли указанный ключ частично слабым.
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, проверяемый на частичную слабость.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ частично слабый; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер параметра <paramref name="rgbKey" />.
    /// </exception>
    public static bool IsSemiWeakKey(byte[] rgbKey)
    {
      if (!DES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      switch (DES.QuadWordFromBigEndian(Utils.FixupKeyParity(rgbKey)))
      {
        case 80784550989267214:
        case 135110050437988849:
        case 143554428589179390:
        case 2234100979542855169:
        case 2296870857142767345:
        case 2305315235293957886:
        case 16141428838415593729:
        case 16149873216566784270:
        case 16212643094166696446:
        case 18303189645120372225:
        case 18311634023271562766:
        case 18365959522720284401:
          return true;
        default:
          return false;
      }
    }

    private static bool IsLegalKeySize(byte[] rgbKey)
    {
      return rgbKey != null && rgbKey.Length == 8;
    }

    private static ulong QuadWordFromBigEndian(byte[] block)
    {
      return (ulong) ((long) block[0] << 56 | (long) block[1] << 48 | (long) block[2] << 40 | (long) block[3] << 32 | (long) block[4] << 24 | (long) block[5] << 16 | (long) block[6] << 8) | (ulong) block[7];
    }
  }
}
