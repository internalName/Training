// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SignatureDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Проверяет подпись PKCS #1 версии 1.5 <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  public class RSAPKCS1SignatureDeformatter : AsymmetricSignatureDeformatter
  {
    private RSA _rsaKey;
    private string _strOID;
    private bool? _rsaOverridesVerifyHash;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureDeformatter" />.
    /// </summary>
    public RSAPKCS1SignatureDeformatter()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureDeformatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр класса <see cref="T:System.Security.Cryptography.RSA" />, который содержит открытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key " /> имеет значение <see langword="null" />.
    /// </exception>
    public RSAPKCS1SignatureDeformatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
    }

    /// <summary>
    ///   Задает открытый ключ, используемый для проверки подписи.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр класса <see cref="T:System.Security.Cryptography.RSA" />, который содержит открытый ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key " /> имеет значение <see langword="null" />.
    /// </exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._rsaKey = (RSA) key;
      this._rsaOverridesVerifyHash = new bool?();
    }

    /// <summary>Задает хэш-алгоритм для подтверждения подписи.</summary>
    /// <param name="strName">
    ///   Имя хэш-алгоритма для подтверждения подписи.
    /// </param>
    public override void SetHashAlgorithm(string strName)
    {
      this._strOID = CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm);
    }

    /// <summary>
    ///   Проверяет подпись PKCS #1 <see cref="T:System.Security.Cryptography.RSA" /> для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, подписанные с помощью <paramref name="rgbSignature" />.
    /// </param>
    /// <param name="rgbSignature">
    ///   Подпись, которую требуется проверить с использованием <paramref name="rgbHash" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="rgbSignature" /> совпадает с подписью, вычисленной с помощью указанного хэш-алгоритма и ключа для <paramref name="rgbHash" />; в противном случае — значение <see langword="false" />.
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
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="rgbSignature" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (rgbSignature == null)
        throw new ArgumentNullException(nameof (rgbSignature));
      if (this._strOID == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this._rsaKey is RSACryptoServiceProvider)
      {
        int algIdFromOid = X509Utils.GetAlgIdFromOid(this._strOID, OidGroup.HashAlgorithm);
        return ((RSACryptoServiceProvider) this._rsaKey).VerifyHash(rgbHash, algIdFromOid, rgbSignature);
      }
      if (this.OverridesVerifyHash)
      {
        HashAlgorithmName hashAlgorithmName = Utils.OidToHashAlgorithmName(this._strOID);
        return this._rsaKey.VerifyHash(rgbHash, rgbSignature, hashAlgorithmName, RSASignaturePadding.Pkcs1);
      }
      byte[] rhs = Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash);
      return Utils.CompareBigIntArrays(this._rsaKey.EncryptValue(rgbSignature), rhs);
    }

    private bool OverridesVerifyHash
    {
      get
      {
        if (!this._rsaOverridesVerifyHash.HasValue)
          this._rsaOverridesVerifyHash = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "VerifyHash", new Type[4]
          {
            typeof (byte[]),
            typeof (byte[]),
            typeof (HashAlgorithmName),
            typeof (RSASignaturePadding)
          }));
        return this._rsaOverridesVerifyHash.Value;
      }
    }
  }
}
