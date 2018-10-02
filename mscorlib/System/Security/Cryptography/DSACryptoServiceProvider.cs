// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSACryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет объект обертки для доступа к реализации служб шифрования (CSP) <see cref="T:System.Security.Cryptography.DSA" /> алгоритма.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
  {
    private int _dwKeySize;
    private CspParameters _parameters;
    private bool _randomKeyContainer;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    private SHA1CryptoServiceProvider _sha1;
    private static volatile CspProviderFlags s_UseMachineKeyStore;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" />.
    /// </summary>
    public DSACryptoServiceProvider()
      : this(0, new CspParameters(13, (string) null, (string) null, DSACryptoServiceProvider.s_UseMachineKeyStore))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> класса с заданным размером ключа.
    /// </summary>
    /// <param name="dwKeySize">
    ///   Размер ключа для асимметричного алгоритма в битах.
    /// </param>
    public DSACryptoServiceProvider(int dwKeySize)
      : this(dwKeySize, new CspParameters(13, (string) null, (string) null, DSACryptoServiceProvider.s_UseMachineKeyStore))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> класса с заданными параметрами для поставщика служб шифрования (CSP).
    /// </summary>
    /// <param name="parameters">Параметры CSP.</param>
    public DSACryptoServiceProvider(CspParameters parameters)
      : this(0, parameters)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> класса с заданным размером ключа и параметрами поставщика служб шифрования (CSP).
    /// </summary>
    /// <param name="dwKeySize">
    ///   Размер ключа для алгоритма шифрования в битах.
    /// </param>
    /// <param name="parameters">Параметры CSP.</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Невозможно получить CSP.
    /// 
    ///   -или-
    /// 
    ///   Невозможно создать ключ.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dwKeySize" /> выходит за пределы диапазона.
    /// </exception>
    [SecuritySafeCritical]
    public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
    {
      if (dwKeySize < 0)
        throw new ArgumentOutOfRangeException(nameof (dwKeySize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Dss, parameters, DSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
      this.LegalKeySizesValue = new KeySizes[1]
      {
        new KeySizes(512, 1024, 64)
      };
      this._dwKeySize = dwKeySize;
      this._sha1 = new SHA1CryptoServiceProvider();
      if (this._randomKeyContainer && !Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
        return;
      this.GetKeyPair();
    }

    [SecurityCritical]
    private void GetKeyPair()
    {
      if (this._safeKeyHandle != null)
        return;
      lock (this)
      {
        if (this._safeKeyHandle != null)
          return;
        Utils.GetKeyPairHelper(CspAlgorithmType.Dss, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
      }
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      if (this._safeProvHandle == null || this._safeProvHandle.IsClosed)
        return;
      this._safeProvHandle.Dispose();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> объект содержит только открытый ключ.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> объект содержит только открытым ключом; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public bool PublicOnly
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        return Utils._GetKeyParameter(this._safeKeyHandle, 2U)[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" />, описывающий дополнительные сведения о паре ключей шифрования.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" />, описывающий дополнительные сведения о паре ключей шифрования.
    /// </returns>
    [ComVisible(false)]
    public CspKeyContainerInfo CspKeyContainerInfo
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
      }
    }

    /// <summary>
    ///   Получает размер ключа, используемого асимметричным алгоритмом, в битах.
    /// </summary>
    /// <returns>
    ///   Размер ключа, используемого асимметричным алгоритмом.
    /// </returns>
    public override int KeySize
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        byte[] keyParameter = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
        this._dwKeySize = (int) keyParameter[0] | (int) keyParameter[1] << 8 | (int) keyParameter[2] << 16 | (int) keyParameter[3] << 24;
        return this._dwKeySize;
      }
    }

    /// <summary>Получает имя алгоритма обмена ключами.</summary>
    /// <returns>Имя алгоритма обмена ключами.</returns>
    public override string KeyExchangeAlgorithm
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>Получает имя алгоритма подписи.</summary>
    /// <returns>Имя алгоритма подписи.</returns>
    public override string SignatureAlgorithm
    {
      get
      {
        return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, следует ли сохранять ключ в хранилище ключей компьютера, а не в хранилище профилей пользователей.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ необходимо сохранить в хранилище ключей компьютера; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool UseMachineKeyStore
    {
      get
      {
        return DSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
      }
      set
      {
        DSACryptoServiceProvider.s_UseMachineKeyStore = value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, следует ли сохранить ключ в поставщике служб шифрования (CSP).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ необходимо сохранить в поставщике служб шифрования; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool PersistKeyInCsp
    {
      [SecuritySafeCritical] get
      {
        if (this._safeProvHandle == null)
        {
          lock (this)
          {
            if (this._safeProvHandle == null)
              this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
          }
        }
        return Utils.GetPersistKeyInCsp(this._safeProvHandle);
      }
      [SecuritySafeCritical] set
      {
        bool persistKeyInCsp = this.PersistKeyInCsp;
        if (value == persistKeyInCsp)
          return;
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        if (!value)
        {
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
          containerPermission.AccessEntries.Add(accessEntry);
        }
        else
        {
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
          containerPermission.AccessEntries.Add(accessEntry);
        }
        containerPermission.Demand();
        Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
      }
    }

    /// <summary>
    ///   Экспортирует <see cref="T:System.Security.Cryptography.DSAParameters" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Параметры для <see cref="T:System.Security.Cryptography.DSA" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось экспортировать ключ.
    /// </exception>
    [SecuritySafeCritical]
    public override DSAParameters ExportParameters(bool includePrivateParameters)
    {
      this.GetKeyPair();
      if (includePrivateParameters)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      DSACspObject dsaCspObject = new DSACspObject();
      Utils._ExportKey(this._safeKeyHandle, includePrivateParameters ? 7 : 6, (object) dsaCspObject);
      return DSACryptoServiceProvider.DSAObjectToStruct(dsaCspObject);
    }

    /// <summary>
    ///   Экспортирует большой двоичный объект, содержащий ключевые сведения, связанные с <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> объекта.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   <see langword="true" />, чтобы включить закрытый ключ; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив байтов, содержащий ключевые сведения, связанные с <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public byte[] ExportCspBlob(bool includePrivateParameters)
    {
      this.GetKeyPair();
      return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
    }

    /// <summary>
    ///   Импортирует <see cref="T:System.Security.Cryptography.DSAParameters" />.
    /// </summary>
    /// <param name="parameters">
    ///   Параметры для <see cref="T:System.Security.Cryptography.DSA" />.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   У параметра <paramref name="parameters" /> отсутствуют некоторые поля.
    /// </exception>
    [SecuritySafeCritical]
    public override void ImportParameters(DSAParameters parameters)
    {
      DSACspObject dsaCspObject = DSACryptoServiceProvider.DSAStructToObject(parameters);
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
      if (DSACryptoServiceProvider.IsPublic(parameters))
      {
        Utils._ImportKey(Utils.StaticDssProvHandle, 8704, CspProviderFlags.NoFlags, (object) dsaCspObject, ref this._safeKeyHandle);
      }
      else
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
        if (this._safeProvHandle == null)
          this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
        Utils._ImportKey(this._safeProvHandle, 8704, this._parameters.Flags, (object) dsaCspObject, ref this._safeKeyHandle);
      }
    }

    /// <summary>
    ///   Импортирует большой двоичный объект, представляющий данные ключа DSA.
    /// </summary>
    /// <param name="keyBlob">
    ///   Массив байтов, представляющий большой двоичный объект ключа DSA.
    /// </param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void ImportCspBlob(byte[] keyBlob)
    {
      Utils.ImportCspBlobHelper(CspAlgorithmType.Dss, keyBlob, DSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного входного потока и подписывает результирующее значение хеша.
    /// </summary>
    /// <param name="inputStream">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.DSA" /> для указанных данных.
    /// </returns>
    public byte[] SignData(Stream inputStream)
    {
      return this.SignHash(this._sha1.ComputeHash(inputStream), (string) null);
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного массива байтов и подписывает результирующее значение хеша.
    /// </summary>
    /// <param name="buffer">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.DSA" /> для указанных данных.
    /// </returns>
    public byte[] SignData(byte[] buffer)
    {
      return this.SignHash(this._sha1.ComputeHash(buffer), (string) null);
    }

    /// <summary>
    ///   Знаки массив байтов от начала указанной точки к заданной конечной точкой.
    /// </summary>
    /// <param name="buffer">Входные данные для входа.</param>
    /// <param name="offset">
    ///   Смещение в массиве, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="count">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.DSA" /> для указанных данных.
    /// </returns>
    public byte[] SignData(byte[] buffer, int offset, int count)
    {
      return this.SignHash(this._sha1.ComputeHash(buffer, offset, count), (string) null);
    }

    /// <summary>
    ///   Проверяет заданные данные подписи, сравнивая его с подписью, вычисленной для указанных данных.
    /// </summary>
    /// <param name="rgbData">подписанные данные;</param>
    /// <param name="rgbSignature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если подпись является допустимым; в противном случае — <see langword="false" />.
    /// </returns>
    public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
    {
      return this.VerifyHash(this._sha1.ComputeHash(rgbData), (string) null, rgbSignature);
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Cryptography.DSA" /> подпись для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, которые должны быть подписаны.
    /// </param>
    /// <returns>Цифровая подпись для указанных данных.</returns>
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      return this.SignHash(rgbHash, (string) null);
    }

    /// <summary>
    ///   Проверяет <see cref="T:System.Security.Cryptography.DSA" /> подпись для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, подписанные с помощью <paramref name="rgbSignature" />.
    /// </param>
    /// <param name="rgbSignature">
    ///   Подпись для <paramref name="rgbData" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="rgbSignature" /> совпадает с подписью, вычисленной с помощью указанного хэш-алгоритма и ключа для <paramref name="rgbHash" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
    {
      return this.VerifyHash(rgbHash, (string) null, rgbSignature);
    }

    protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      if (hashAlgorithm != HashAlgorithmName.SHA1)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
      return this._sha1.ComputeHash(data, offset, count);
    }

    protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      if (hashAlgorithm != HashAlgorithmName.SHA1)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
      return this._sha1.ComputeHash(data);
    }

    /// <summary>
    ///   Вычисляет подпись для указанного хэш-значения путем его шифрования с помощью закрытого ключа.
    /// </summary>
    /// <param name="rgbHash">Хэш-значение подписываемых данных.</param>
    /// <param name="str">
    ///   Имя хэш-алгоритма, используемого для создания хэш-значения данных.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.Cryptography.DSA" /> Подпись для указанного значения хэша.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbHash" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   Отсутствует закрытый ключ.
    /// </exception>
    [SecuritySafeCritical]
    public byte[] SignHash(byte[] rgbHash, string str)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (this.PublicOnly)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      if (rgbHash.Length != this._sha1.HashSize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", (object) "SHA1", (object) (this._sha1.HashSize / 8)));
      this.GetKeyPair();
      if (!this.CspKeyContainerInfo.RandomlyGenerated)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 8704, algId, rgbHash);
    }

    /// <summary>
    ///   Проверяет заданные данные подписи, сравнивая его с подписью, вычисленной для заданного значения хэша.
    /// </summary>
    /// <param name="rgbHash">Хэш-значение подписываемых данных.</param>
    /// <param name="str">
    ///   Имя хэш-алгоритма, используемого для создания хэш-значения данных.
    /// </param>
    /// <param name="rgbSignature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если подпись является допустимым; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbHash" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="rgbSignature" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   Не удалось проверить подпись.
    /// </exception>
    [SecuritySafeCritical]
    public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (rgbSignature == null)
        throw new ArgumentNullException(nameof (rgbSignature));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      if (rgbHash.Length != this._sha1.HashSize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", (object) "SHA1", (object) (this._sha1.HashSize / 8)));
      this.GetKeyPair();
      return Utils.VerifySign(this._safeKeyHandle, 8704, algId, rgbHash, rgbSignature);
    }

    private static DSAParameters DSAObjectToStruct(DSACspObject dsaCspObject)
    {
      return new DSAParameters()
      {
        P = dsaCspObject.P,
        Q = dsaCspObject.Q,
        G = dsaCspObject.G,
        Y = dsaCspObject.Y,
        J = dsaCspObject.J,
        X = dsaCspObject.X,
        Seed = dsaCspObject.Seed,
        Counter = dsaCspObject.Counter
      };
    }

    private static DSACspObject DSAStructToObject(DSAParameters dsaParams)
    {
      return new DSACspObject()
      {
        P = dsaParams.P,
        Q = dsaParams.Q,
        G = dsaParams.G,
        Y = dsaParams.Y,
        J = dsaParams.J,
        X = dsaParams.X,
        Seed = dsaParams.Seed,
        Counter = dsaParams.Counter
      };
    }

    private static bool IsPublic(DSAParameters dsaParams)
    {
      return dsaParams.X == null;
    }

    private static bool IsPublic(byte[] keyBlob)
    {
      if (keyBlob == null)
        throw new ArgumentNullException(nameof (keyBlob));
      return keyBlob[0] == (byte) 6 && (keyBlob[11] == (byte) 49 || keyBlob[11] == (byte) 51) && (keyBlob[10] == (byte) 83 && keyBlob[9] == (byte) 83 && keyBlob[8] == (byte) 68);
    }
  }
}
