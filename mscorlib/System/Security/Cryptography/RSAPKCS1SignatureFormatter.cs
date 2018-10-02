// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SignatureFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Создает подпись PKCS #1 версии 1.5 <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
  {
    private RSA _rsaKey;
    private string _strOID;
    private bool? _rsaOverridesSignHash;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" />.
    /// </summary>
    public RSAPKCS1SignatureFormatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма <see cref="T:System.Security.Cryptography.RSA" />, который содержит закрытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>
    ///   Задает закрытый ключ, используемый для создания подписи.
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
      this._rsaOverridesSignHash = new bool?();
    }

    /// <summary>Задает хэш-алгоритм для создания подписи.</summary>
    /// <param name="strName">
    ///   Имя хэш-алгоритма для создания подписи.
    /// </param>
    public override void SetHashAlgorithm(string strName)
    {
      this._strOID = CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm);
    }

    /// <summary>
    ///   Создает подпись PKCS #1 <see cref="T:System.Security.Cryptography.RSA" /> для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, которые должны быть подписаны.
    /// </param>
    /// <returns>
    ///   Цифровая подпись для <paramref name="rgbHash" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Ключ имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Хэш-алгоритм имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbHash" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (this._strOID == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this._rsaKey is RSACryptoServiceProvider)
      {
        int algIdFromOid = X509Utils.GetAlgIdFromOid(this._strOID, OidGroup.HashAlgorithm);
        return ((RSACryptoServiceProvider) this._rsaKey).SignHash(rgbHash, algIdFromOid);
      }
      if (!this.OverridesSignHash)
        return this._rsaKey.DecryptValue(Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash));
      HashAlgorithmName hashAlgorithmName = Utils.OidToHashAlgorithmName(this._strOID);
      return this._rsaKey.SignHash(rgbHash, hashAlgorithmName, RSASignaturePadding.Pkcs1);
    }

    private bool OverridesSignHash
    {
      get
      {
        if (!this._rsaOverridesSignHash.HasValue)
          this._rsaOverridesSignHash = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "SignHash", new Type[3]
          {
            typeof (byte[]),
            typeof (HashAlgorithmName),
            typeof (RSASignaturePadding)
          }));
        return this._rsaOverridesSignHash.Value;
      }
    }
  }
}
