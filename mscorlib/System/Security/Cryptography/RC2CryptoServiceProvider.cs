// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RC2CryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет объект обертки для доступа к реализации служб шифрования (CSP) <see cref="T:System.Security.Cryptography.RC2" /> алгоритма.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class RC2CryptoServiceProvider : RC2
  {
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]
    {
      new KeySizes(40, 128, 8)
    };
    private bool m_use40bitSalt;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" />.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Обнаружен не соответствующий стандарту FIPS алгоритма.
    /// </exception>
    [SecuritySafeCritical]
    public RC2CryptoServiceProvider()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      if (!Utils.HasAlgorithm(26114, 0))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
      this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
      this.FeedbackSizeValue = 8;
    }

    /// <summary>
    ///   Возвращает или задает эффективный размер (в битах), секретного ключа, используемого <see cref="T:System.Security.Cryptography.RC2" /> алгоритма.
    /// </summary>
    /// <returns>
    ///   Эффективный размер ключа в битах, используемые <see cref="T:System.Security.Cryptography.RC2" /> алгоритма.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> Которого установлено значение, отличное от <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> свойство.
    /// </exception>
    public override int EffectiveKeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (value != this.KeySizeValue)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_RC2_EKSKS2"));
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, определяющее, следует ли создать ключ с длину 11 байтов и нулевое значение.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ключ должен быть создан с длину 11 байтов и нулевое значение; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public bool UseSalt
    {
      get
      {
        return this.m_use40bitSalt;
      }
      set
      {
        this.m_use40bitSalt = value;
      }
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.RC2" /> объект-шифратор с заданным ключом (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.RC2" /> объект-шифратор.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> Использовался режим шифрования.
    /// 
    ///   -или-
    /// 
    ///   A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> использовался режим шифрования с размер ответа, отличный от 8 бит.
    /// 
    ///   -или-
    /// 
    ///   Использован недопустимый размер ключа.
    /// 
    ///   -или-
    /// 
    ///   Размер ключа алгоритма была недоступна.
    /// </exception>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.RC2" /> объект-дешифратор с указанным ключом (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.RC2" /> объект-дешифратор.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> Использовался режим шифрования.
    /// 
    ///   -или-
    /// 
    ///   A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> использовался режим шифрования с размер ответа, отличный от 8 бит.
    /// 
    ///   -или-
    /// 
    ///   Использован недопустимый размер ключа.
    /// 
    ///   -или-
    /// 
    ///   Размер ключа алгоритма была недоступна.
    /// </exception>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
    }

    /// <summary>
    ///   Создает случайный ключ (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) для алгоритма.
    /// </summary>
    public override void GenerateKey()
    {
      this.KeyValue = new byte[this.KeySizeValue / 8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
    }

    /// <summary>
    ///   Создает случайный вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) для использования данным алгоритмом.
    /// </summary>
    public override void GenerateIV()
    {
      this.IVValue = new byte[8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
    }

    [SecurityCritical]
    private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int effectiveKeySize, int feedbackSize, CryptoAPITransformMode encryptMode)
    {
      int index = 0;
      int[] rgArgIds = new int[10];
      object[] rgArgValues = new object[10];
      if (mode == CipherMode.OFB)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
      if (mode == CipherMode.CFB && feedbackSize != 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
      if (rgbKey == null)
      {
        rgbKey = new byte[this.KeySizeValue / 8];
        Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
      }
      int num = rgbKey.Length * 8;
      if (!this.ValidKeySize(num))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      rgArgIds[index] = 19;
      rgArgValues[index] = this.EffectiveKeySizeValue != 0 ? (object) effectiveKeySize : (object) num;
      int cArgs = index + 1;
      if (mode != CipherMode.CBC)
      {
        rgArgIds[cArgs] = 4;
        rgArgValues[cArgs] = (object) mode;
        ++cArgs;
      }
      if (mode != CipherMode.ECB)
      {
        if (rgbIV == null)
        {
          rgbIV = new byte[8];
          Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
        }
        if (rgbIV.Length < 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        rgArgIds[cArgs] = 1;
        rgArgValues[cArgs] = (object) rgbIV;
        ++cArgs;
      }
      if (mode == CipherMode.OFB || mode == CipherMode.CFB)
      {
        rgArgIds[cArgs] = 5;
        rgArgValues[cArgs] = (object) feedbackSize;
        ++cArgs;
      }
      if (!Utils.HasAlgorithm(26114, num))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgKeySizeNotAvailable", (object) num));
      return (ICryptoTransform) new CryptoAPITransform(26114, cArgs, rgArgIds, rgArgValues, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, this.m_use40bitSalt, encryptMode);
    }
  }
}
