// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSASignatureFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Создает алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />) подписи.
  /// </summary>
  [ComVisible(true)]
  public class DSASignatureFormatter : AsymmetricSignatureFormatter
  {
    private DSA _dsaKey;
    private string _oid;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.DSASignatureFormatter" />.
    /// </summary>
    public DSASignatureFormatter()
    {
      this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.DSASignatureFormatter" /> с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />), содержащий ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public DSASignatureFormatter(AsymmetricAlgorithm key)
      : this()
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._dsaKey = (DSA) key;
    }

    /// <summary>
    ///   Задает ключ, используемый для алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />) создания подписи.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр <see cref="T:System.Security.Cryptography.DSA" /> содержащий ключ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this._dsaKey = (DSA) key;
    }

    /// <summary>
    ///   Указывает хэш-алгоритма для алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />) создания подписи.
    /// </summary>
    /// <param name="strName">
    ///   Имя хэш-алгоритма для создания подписи.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   <paramref name="strName" /> Параметр не сопоставлен <see cref="T:System.Security.Cryptography.SHA1" /> хэш-алгоритма.
    /// </exception>
    public override void SetHashAlgorithm(string strName)
    {
      if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
    }

    /// <summary>
    ///   Создает алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />) подписи PKCS #1 для заданных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, которые должны быть подписаны.
    /// </param>
    /// <returns>Цифровая подпись для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rgbHash" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Идентификатор Объекта – <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Является ключом DSA <see langword="null" />.
    /// </exception>
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (this._oid == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
      if (this._dsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      return this._dsaKey.CreateSignature(rgbHash);
    }
  }
}
