// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.TripleDES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс для алгоритмов Triple DES, от которых должны наследоваться все реализации <see cref="T:System.Security.Cryptography.TripleDES" />.
  /// </summary>
  [ComVisible(true)]
  public abstract class TripleDES : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
    {
      new KeySizes(64, 64, 0)
    };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(128, 192, 64)
    };

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </summary>
    protected TripleDES()
    {
      this.KeySizeValue = 192;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
      this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
    }

    /// <summary>
    ///   Получает или задает секретный ключ для алгоритма <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </summary>
    /// <returns>
    ///   Секретный ключ для алгоритма <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Предпринята попытка установить для этого ключа значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Предпринята попытка установить ключ, длина которого недопустима.
    /// 
    ///   -или-
    /// 
    ///   Предпринята попытка установить слабый ключ (см. <see cref="M:System.Security.Cryptography.TripleDES.IsWeakKey(System.Byte[])" />).
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
          while (TripleDES.IsWeakKey(this.KeyValue));
        }
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (!this.ValidKeySize(value.Length * 8))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        if (TripleDES.IsWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), nameof (TripleDES));
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения алгоритма <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </summary>
    /// <returns>Экземпляр криптографического объекта.</returns>
    public static TripleDES Create()
    {
      return TripleDES.Create("System.Security.Cryptography.TripleDES");
    }

    /// <summary>
    ///   Создает экземпляр криптографического объекта для выполнения заданной реализации <see cref="T:System.Security.Cryptography.TripleDES" /> алгоритма.
    /// </summary>
    /// <param name="str">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.TripleDES" /> для использования.
    /// </param>
    /// <returns>Экземпляр криптографического объекта.</returns>
    public static TripleDES Create(string str)
    {
      return (TripleDES) CryptoConfig.CreateFromName(str);
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
      if (!TripleDES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      byte[] rgbKey1 = Utils.FixupKeyParity(rgbKey);
      return TripleDES.EqualBytes(rgbKey1, 0, 8, 8) || rgbKey1.Length == 24 && TripleDES.EqualBytes(rgbKey1, 8, 16, 8);
    }

    private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
    {
      if (start1 < 0)
        throw new ArgumentOutOfRangeException(nameof (start1), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (start2 < 0)
        throw new ArgumentOutOfRangeException(nameof (start2), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (start1 + count > rgbKey.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (start2 + count > rgbKey.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      for (int index = 0; index < count; ++index)
      {
        if ((int) rgbKey[start1 + index] != (int) rgbKey[start2 + index])
          return false;
      }
      return true;
    }

    private static bool IsLegalKeySize(byte[] rgbKey)
    {
      return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
    }
  }
}
