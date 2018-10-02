// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAOAEPKeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Создает данные обмена ключа Optimal Asymmetric Encryption Padding (OAEP) с помощью <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
  {
    private byte[] ParameterValue;
    private RSA _rsaKey;
    private bool? _rsaOverridesEncrypt;
    private RandomNumberGenerator RngValue;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" />.
    /// </summary>
    public RSAOAEPKeyExchangeFormatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит открытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="key " /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>
    ///   Возвращает или задает параметр, используемый для создания отбивки при создании процесса обмена ключами.
    /// </summary>
    /// <returns>Значение параметра.</returns>
    public byte[] Parameter
    {
      get
      {
        if (this.ParameterValue != null)
          return (byte[]) this.ParameterValue.Clone();
        return (byte[]) null;
      }
      set
      {
        if (value != null)
          this.ParameterValue = (byte[]) value.Clone();
        else
          this.ParameterValue = (byte[]) null;
      }
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
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Ключ отсутствует.
    /// </exception>
    [SecuritySafeCritical]
    public override byte[] CreateKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this.OverridesEncrypt)
        return this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
      return Utils.RsaOaepEncrypt(this._rsaKey, (HashAlgorithm) SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
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
