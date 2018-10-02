// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Создает данные обмена ключами PKCS#1 с помощью <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
  {
    private RandomNumberGenerator RngValue;
    private RSA _rsaKey;
    private bool? _rsaOverridesEncrypt;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" />.
    /// </summary>
    public RSAPKCS1KeyExchangeFormatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит открытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="key " /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>Получает параметры для обмена ключами PKCS #1.</summary>
    /// <returns>
    ///   Строка XML, содержащая параметры операции обмена ключами PKCS #1.
    /// </returns>
    public override string Parameters
    {
      get
      {
        return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
      }
    }

    /// <summary>
    ///   Получает или задает алгоритм генерации случайных чисел, используемый при создании механизма обмена ключами.
    /// </summary>
    /// <returns>
    ///   Экземпляр используемого алгоритма генерации случайных чисел.
    /// </returns>
    public RandomNumberGenerator Rng
    {
      get
      {
        return this.RngValue;
      }
      set
      {
        this.RngValue = value;
      }
    }

    /// <summary>
    ///   Задает открытый ключ, используемый для шифрования данных обмена ключами.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит открытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="key " />— <see langword="null" />.
    /// </exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
      this._rsaOverridesEncrypt = new bool?();
    }

    /// <summary>
    ///   Создает из указанных входных данных зашифрованные данные для обмена ключами.
    /// </summary>
    /// <param name="rgbData">
    ///   Секретные сведения, которые будут переданы при обмене ключами.
    /// </param>
    /// <returns>
    ///   Зашифрованные данные обмена ключами для отправки предполагаемому получателю.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Объект <paramref name="rgbData " /> имеет слишком большой размер.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Ключ имеет значение <see langword="null" />.
    /// </exception>
    public override byte[] CreateKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      byte[] numArray1;
      if (this.OverridesEncrypt)
      {
        numArray1 = this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.Pkcs1);
      }
      else
      {
        int length = this._rsaKey.KeySize / 8;
        if (rgbData.Length + 11 > length)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_EncDataTooBig", (object) (length - 11)));
        byte[] numArray2 = new byte[length];
        if (this.RngValue == null)
          this.RngValue = RandomNumberGenerator.Create();
        this.Rng.GetNonZeroBytes(numArray2);
        numArray2[0] = (byte) 0;
        numArray2[1] = (byte) 2;
        numArray2[length - rgbData.Length - 1] = (byte) 0;
        Buffer.InternalBlockCopy((Array) rgbData, 0, (Array) numArray2, length - rgbData.Length, rgbData.Length);
        numArray1 = this._rsaKey.EncryptValue(numArray2);
      }
      return numArray1;
    }

    /// <summary>
    ///   Создает из указанных входных данных зашифрованные данные для обмена ключами.
    /// </summary>
    /// <param name="rgbData">
    ///   Секретные сведения, которые будут переданы при обмене ключами.
    /// </param>
    /// <param name="symAlgType">
    ///   Этот параметр не используется в текущей версии.
    /// </param>
    /// <returns>
    ///   Зашифрованные данные для обмена ключами, отправляемые указанному получателю.
    /// </returns>
    public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
    {
      return this.CreateKeyExchange(rgbData);
    }

    private bool OverridesEncrypt
    {
      get
      {
        if (!this._rsaOverridesEncrypt.HasValue)
          this._rsaOverridesEncrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Encrypt", new Type[2]
          {
            typeof (byte[]),
            typeof (RSAEncryptionPadding)
          }));
        return this._rsaOverridesEncrypt.Value;
      }
    }
  }
}
