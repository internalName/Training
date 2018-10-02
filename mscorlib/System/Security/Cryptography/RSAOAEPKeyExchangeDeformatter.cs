// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Расшифровывает данные обмена ключа Optimal Asymmetric Encryption Padding (OAEP).
  /// </summary>
  [ComVisible(true)]
  public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
  {
    private RSA _rsaKey;
    private bool? _rsaOverridesDecrypt;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" />.
    /// </summary>
    public RSAOAEPKeyExchangeDeformatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит закрытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key " /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>
    ///   Возвращает параметры для обмена ключами Optimal Asymmetric Encryption Padding (OAEP).
    /// </summary>
    /// <returns>
    ///   XML-строка, содержащая параметры операции обмена ключами OAEP.
    /// </returns>
    public override string Parameters
    {
      get
      {
        return (string) null;
      }
      set
      {
      }
    }

    /// <summary>
    ///   Извлекает конфиденциальные сведения из зашифрованных данных обмена ключами.
    /// </summary>
    /// <param name="rgbData">
    ///   Данные обмена ключами, в которых скрыты конфиденциальные сведения.
    /// </param>
    /// <returns>
    ///   Конфиденциальные сведения, извлекаемые из данных обмена ключами.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Сбой при проверке данных обмена ключами.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Ключ отсутствует.
    /// </exception>
    [SecuritySafeCritical]
    public override byte[] DecryptKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this.OverridesDecrypt)
        return this._rsaKey.Decrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
      return Utils.RsaOaepDecrypt(this._rsaKey, (HashAlgorithm) SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
    }

    /// <summary>
    ///   Задает закрытый ключ для расшифровки конфиденциальных данных.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит закрытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
      this._rsaOverridesDecrypt = new bool?();
    }

    private bool OverridesDecrypt
    {
      get
      {
        if (!this._rsaOverridesDecrypt.HasValue)
          this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[2]
          {
            typeof (byte[]),
            typeof (RSAEncryptionPadding)
          }));
        return this._rsaOverridesDecrypt.Value;
      }
    }
  }
}
