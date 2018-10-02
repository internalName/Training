// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>Расшифровывает данные обмена ключами PKCS #1.</summary>
  [ComVisible(true)]
  public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
  {
    private RSA _rsaKey;
    private bool? _rsaOverridesDecrypt;
    private RandomNumberGenerator RngValue;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" />.
    /// </summary>
    public RSAPKCS1KeyExchangeDeformatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит закрытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>
    ///   Получает или задает алгоритм генерации случайных чисел, используемый при создании механизма обмена ключами.
    /// </summary>
    /// <returns>
    ///   Экземпляр используемого алгоритма генерации случайных чисел.
    /// </returns>
    public RandomNumberGenerator RNG
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

    /// <summary>Получает параметры для обмена ключами PKCS #1.</summary>
    /// <returns>
    ///   Строка XML, содержащая параметры операции обмена ключами PKCS #1.
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
    /// <param name="rgbIn">
    ///   Данные обмена ключами, в которых скрыты конфиденциальные сведения.
    /// </param>
    /// <returns>
    ///   Конфиденциальные сведения, извлекаемые из данных обмена ключами.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Ключ отсутствует.
    /// </exception>
    public override byte[] DecryptKeyExchange(byte[] rgbIn)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      byte[] numArray1;
      if (this.OverridesDecrypt)
      {
        numArray1 = this._rsaKey.Decrypt(rgbIn, RSAEncryptionPadding.Pkcs1);
      }
      else
      {
        byte[] numArray2 = this._rsaKey.DecryptValue(rgbIn);
        int index = 2;
        while (index < numArray2.Length && numArray2[index] != (byte) 0)
          ++index;
        if (index >= numArray2.Length)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_PKCS1Decoding"));
        int srcOffsetBytes = index + 1;
        numArray1 = new byte[numArray2.Length - srcOffsetBytes];
        Buffer.InternalBlockCopy((Array) numArray2, srcOffsetBytes, (Array) numArray1, 0, numArray1.Length);
      }
      return numArray1;
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
