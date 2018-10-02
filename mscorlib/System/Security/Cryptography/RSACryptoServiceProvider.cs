// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSACryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Выполняет асимметричное шифрование и расшифровку с помощью реализации алгоритма <see cref="T:System.Security.Cryptography.RSA" />, предоставляемого поставщиком служб шифрования (CSP).
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
  {
    private int _dwKeySize;
    private CspParameters _parameters;
    private bool _randomKeyContainer;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    private static volatile CspProviderFlags s_UseMachineKeyStore;

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DecryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbEncryptedKey, int cbEncryptedKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetDecryptedKey);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void EncryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbKey, int cbKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetEncryptedKey);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" />, используя ключ по умолчанию.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// </exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider()
      : this(0, new CspParameters(24, (string) null, (string) null, RSACryptoServiceProvider.s_UseMachineKeyStore), true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> с указанным размером ключа.
    /// </summary>
    /// <param name="dwKeySize">
    ///   Размер используемого ключа в битах.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// </exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(int dwKeySize)
      : this(dwKeySize, new CspParameters(24, (string) null, (string) null, RSACryptoServiceProvider.s_UseMachineKeyStore), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> с заданными параметрами.
    /// </summary>
    /// <param name="parameters">
    ///   Параметры, передаваемые поставщику служб шифрования (CSP).
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Невозможно получить CSP.
    /// </exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(CspParameters parameters)
      : this(0, parameters, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> указанным размером ключа и параметрами.
    /// </summary>
    /// <param name="dwKeySize">
    ///   Размер используемого ключа в битах.
    /// </param>
    /// <param name="parameters">
    ///   Параметры, передаваемые поставщику служб шифрования (CSP).
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Невозможно получить CSP.
    /// 
    ///   -или-
    /// 
    ///   Невозможно создать ключ.
    /// </exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
      : this(dwKeySize, parameters, false)
    {
    }

    [SecurityCritical]
    private RSACryptoServiceProvider(int dwKeySize, CspParameters parameters, bool useDefaultKeySize)
    {
      if (dwKeySize < 0)
        throw new ArgumentOutOfRangeException(nameof (dwKeySize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Rsa, parameters, RSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
      this.LegalKeySizesValue = new KeySizes[1]
      {
        new KeySizes(384, 16384, 8)
      };
      this._dwKeySize = useDefaultKeySize ? 1024 : dwKeySize;
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
        Utils.GetKeyPairHelper(CspAlgorithmType.Rsa, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
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
    ///   Возвращает значение, указывающее, содержит ли объект <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> только открытый ключ.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если объект <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> содержит только открытый ключ; в противном случае — <see langword="false" />.
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

    /// <summary>Получает размер текущего ключа.</summary>
    /// <returns>Размер ключа в битах.</returns>
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

    /// <summary>
    ///   Получает имя алгоритма обмена ключами, доступного в этой реализации <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <returns>
    ///   Имя алгоритма обмена ключами, если он существует; в противном случае — <see langword="null" />.
    /// </returns>
    public override string KeyExchangeAlgorithm
    {
      get
      {
        if (this._parameters.KeyNumber == 1)
          return "RSA-PKCS1-KeyEx";
        return (string) null;
      }
    }

    /// <summary>
    ///   Получает имя алгоритма подписи, доступного в этой реализации <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <returns>Имя алгоритма подписи.</returns>
    public override string SignatureAlgorithm
    {
      get
      {
        return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
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
        return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
      }
      set
      {
        RSACryptoServiceProvider.s_UseMachineKeyStore = value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
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
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
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
        }
        Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
      }
    }

    /// <summary>
    ///   Экспортирует <see cref="T:System.Security.Cryptography.RSAParameters" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Параметры для <see cref="T:System.Security.Cryptography.RSA" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось экспортировать ключ.
    /// </exception>
    [SecuritySafeCritical]
    public override RSAParameters ExportParameters(bool includePrivateParameters)
    {
      this.GetKeyPair();
      if (includePrivateParameters && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      RSACspObject rsaCspObject = new RSACspObject();
      Utils._ExportKey(this._safeKeyHandle, includePrivateParameters ? 7 : 6, (object) rsaCspObject);
      return RSACryptoServiceProvider.RSAObjectToStruct(rsaCspObject);
    }

    /// <summary>
    ///   Экспортирует большой двоичный объект, содержащий ключевые сведения, связанные с объектом <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   <see langword="true" />, чтобы включить закрытый ключ; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив байтов, содержащий ключевые сведения, связанные с объектом <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" />.
    /// </returns>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public byte[] ExportCspBlob(bool includePrivateParameters)
    {
      this.GetKeyPair();
      return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
    }

    /// <summary>
    ///   Импортирует заданный <see cref="T:System.Security.Cryptography.RSAParameters" />.
    /// </summary>
    /// <param name="parameters">
    ///   Параметры для <see cref="T:System.Security.Cryptography.RSA" />.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   У параметра <paramref name="parameters" /> отсутствуют некоторые поля.
    /// </exception>
    [SecuritySafeCritical]
    public override void ImportParameters(RSAParameters parameters)
    {
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
      {
        this._safeKeyHandle.Dispose();
        this._safeKeyHandle = (SafeKeyHandle) null;
      }
      RSACspObject rsaCspObject = RSACryptoServiceProvider.RSAStructToObject(parameters);
      this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
      if (RSACryptoServiceProvider.IsPublic(parameters))
      {
        Utils._ImportKey(Utils.StaticProvHandle, 41984, CspProviderFlags.NoFlags, (object) rsaCspObject, ref this._safeKeyHandle);
      }
      else
      {
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
          KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
          containerPermission.AccessEntries.Add(accessEntry);
          containerPermission.Demand();
        }
        if (this._safeProvHandle == null)
          this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
        Utils._ImportKey(this._safeProvHandle, 41984, this._parameters.Flags, (object) rsaCspObject, ref this._safeKeyHandle);
      }
    }

    /// <summary>
    ///   Импортирует большой двоичный объект, представляющий данные ключа RSA.
    /// </summary>
    /// <param name="keyBlob">
    ///   Массив байтов, представляющий большой двоичный объект ключа RSA.
    /// </param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void ImportCspBlob(byte[] keyBlob)
    {
      Utils.ImportCspBlobHelper(CspAlgorithmType.Rsa, keyBlob, RSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного потока с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
    /// </summary>
    /// <param name="inputStream">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="halg">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.RSA" /> для указанных данных.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="halg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="halg" /> не является допустимым типом.
    /// </exception>
    public byte[] SignData(Stream inputStream, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(inputStream), algId);
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного массива байтов с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
    /// </summary>
    /// <param name="buffer">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="halg">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.RSA" /> для указанных данных.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="halg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="halg" /> не является допустимым типом.
    /// </exception>
    public byte[] SignData(byte[] buffer, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer), algId);
    }

    /// <summary>
    ///   Вычисляет хэш-значение подмножества заданного массива байтов с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
    /// </summary>
    /// <param name="buffer">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="offset">
    ///   Смещение в массиве, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="count">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <param name="halg">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.RSA" /> для указанных данных.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="halg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="halg" /> не является допустимым типом.
    /// </exception>
    public byte[] SignData(byte[] buffer, int offset, int count, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer, offset, count), algId);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем определения хэш-значения в этой подписи с помощью предоставленного открытого ключа и его сравнения с хэш-значением предоставленных данных.
    /// </summary>
    /// <param name="buffer">подписанные данные;</param>
    /// <param name="halg">
    ///   Имя хэш-алгоритма, используемого для создания хэш-значения данных.
    /// </param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="halg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="halg" /> не является допустимым типом.
    /// </exception>
    public bool VerifyData(byte[] buffer, object halg, byte[] signature)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.VerifyHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer), algId, signature);
    }

    /// <summary>
    ///   Вычисляет подпись для указанного хэш-значения путем его шифрования с помощью закрытого ключа.
    /// </summary>
    /// <param name="rgbHash">Хэш-значение подписываемых данных.</param>
    /// <param name="str">
    ///   Идентификатор хэш-алгоритма (OID), используемый для создания хэш-значения данных.
    /// </param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.RSA" /> для указанного хэш-значения.
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
    public byte[] SignHash(byte[] rgbHash, string str)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (this.PublicOnly)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      return this.SignHash(rgbHash, algId);
    }

    [SecuritySafeCritical]
    internal byte[] SignHash(byte[] rgbHash, int calgHash)
    {
      this.GetKeyPair();
      if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 9216, calgHash, rgbHash);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем определения хэш-значения в этой подписи с помощью предоставленного открытого ключа и его сравнения с предоставленным хэш-значением.
    /// </summary>
    /// <param name="rgbHash">Хэш-значение подписанных данных.</param>
    /// <param name="str">
    ///   Идентификатор хэш-алгоритма (OID), используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="rgbSignature">
    ///   Данные подписи, которые требуется проверить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
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
    public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException(nameof (rgbHash));
      if (rgbSignature == null)
        throw new ArgumentNullException(nameof (rgbSignature));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      return this.VerifyHash(rgbHash, algId, rgbSignature);
    }

    [SecuritySafeCritical]
    internal bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
    {
      this.GetKeyPair();
      return Utils.VerifySign(this._safeKeyHandle, 9216, calgHash, rgbHash, rgbSignature);
    }

    /// <summary>
    ///   Зашифровывает данные с помощью алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <param name="rgb">Данные, предназначенные для шифрования.</param>
    /// <param name="fOAEP">
    ///   <see langword="true" /> для выполнения прямой расшифровки <see cref="T:System.Security.Cryptography.RSA" /> с помощью заполнения OAEP (доступно только на компьютере под управлением Windows XP или более поздних версий). В противном случае — <see langword="false" /> для использования заполнения PKCS #1 v1.5.
    /// </param>
    /// <returns>Зашифрованные данные.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="rgb" /> превышает максимально допустимую длину.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="fOAEP" /> имеет значение <see langword="true" />, и заполнение OAEP не поддерживается.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="rgb " /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public byte[] Encrypt(byte[] rgb, bool fOAEP)
    {
      if (rgb == null)
        throw new ArgumentNullException(nameof (rgb));
      this.GetKeyPair();
      byte[] o = (byte[]) null;
      RSACryptoServiceProvider.EncryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    /// <summary>
    ///   Расшифровывает данные с помощью алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <param name="rgb">Данные, предназначенные для расшифровки.</param>
    /// <param name="fOAEP">
    ///   <see langword="true" /> для выполнения прямой расшифровки <see cref="T:System.Security.Cryptography.RSA" /> с помощью заполнения OAEP (доступно только на компьютере под управлением Microsoft Windows XP или более поздних версий); в противном случае — <see langword="false" /> для использования заполнения PKCS #1 v1.5.
    /// </param>
    /// <returns>
    ///   Расшифрованные данные, которые до шифрования являются исходным открытым текстом.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="fOAEP" /> имеет значение <see langword="true" />, и длина параметра <paramref name="rgb" /> больше <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="fOAEP" /> имеет значение <see langword="true" />, и OAEP не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Ключ не соответствует зашифрованным данным.
    ///    Однако текст исключения может оказаться неточным.
    ///    Например, это может быть Not enough storage is available to process this command.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="rgb " /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public byte[] Decrypt(byte[] rgb, bool fOAEP)
    {
      if (rgb == null)
        throw new ArgumentNullException(nameof (rgb));
      this.GetKeyPair();
      if (rgb.Length > this.KeySize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_DecDataTooBig", (object) (this.KeySize / 8)));
      if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Decrypt);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      byte[] o = (byte[]) null;
      RSACryptoServiceProvider.DecryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    /// <summary>Этот метод не поддерживается в текущей версии.</summary>
    /// <param name="rgb">Данные, предназначенные для расшифровки.</param>
    /// <returns>
    ///   Расшифрованные данные, которые до шифрования являются исходным открытым текстом.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается в текущей версии.
    /// </exception>
    public override byte[] DecryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>Этот метод не поддерживается в текущей версии.</summary>
    /// <param name="rgb">Данные, предназначенные для шифрования.</param>
    /// <returns>Зашифрованные данные.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается в текущей версии.
    /// </exception>
    public override byte[] EncryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    private static RSAParameters RSAObjectToStruct(RSACspObject rsaCspObject)
    {
      return new RSAParameters()
      {
        Exponent = rsaCspObject.Exponent,
        Modulus = rsaCspObject.Modulus,
        P = rsaCspObject.P,
        Q = rsaCspObject.Q,
        DP = rsaCspObject.DP,
        DQ = rsaCspObject.DQ,
        InverseQ = rsaCspObject.InverseQ,
        D = rsaCspObject.D
      };
    }

    private static RSACspObject RSAStructToObject(RSAParameters rsaParams)
    {
      return new RSACspObject()
      {
        Exponent = rsaParams.Exponent,
        Modulus = rsaParams.Modulus,
        P = rsaParams.P,
        Q = rsaParams.Q,
        DP = rsaParams.DP,
        DQ = rsaParams.DQ,
        InverseQ = rsaParams.InverseQ,
        D = rsaParams.D
      };
    }

    private static bool IsPublic(byte[] keyBlob)
    {
      if (keyBlob == null)
        throw new ArgumentNullException(nameof (keyBlob));
      return keyBlob[0] == (byte) 6 && keyBlob[11] == (byte) 49 && (keyBlob[10] == (byte) 65 && keyBlob[9] == (byte) 83) && keyBlob[8] == (byte) 82;
    }

    private static bool IsPublic(RSAParameters rsaParams)
    {
      return rsaParams.P == null;
    }

    [SecuritySafeCritical]
    protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      using (SafeHashHandle hash = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
      {
        Utils.HashData(hash, data, offset, count);
        return Utils.EndHash(hash);
      }
    }

    [SecuritySafeCritical]
    protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      using (SafeHashHandle hash = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
      {
        byte[] numArray = new byte[4096];
        int cbSize;
        do
        {
          cbSize = data.Read(numArray, 0, numArray.Length);
          if (cbSize > 0)
            Utils.HashData(hash, numArray, 0, cbSize);
        }
        while (cbSize > 0);
        return Utils.EndHash(hash);
      }
    }

    private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
    {
      string name = hashAlgorithm.Name;
      if (name == "MD5")
        return 32771;
      if (name == "SHA1")
        return 32772;
      if (name == "SHA256")
        return 32780;
      if (name == "SHA384")
        return 32781;
      if (name == "SHA512")
        return 32782;
      throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
    }

    /// <summary>
    ///   Шифрует данные с помощью алгоритма <see cref="T:System.Security.Cryptography.RSA" />, используя указанное заполнение.
    /// </summary>
    /// <param name="data">Данные, которые необходимо зашифровать.</param>
    /// <param name="padding">Заполнение.</param>
    /// <returns>Зашифрованные данные.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Этот режим заполнения не поддерживается.
    /// </exception>
    public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (padding == (RSAEncryptionPadding) null)
        throw new ArgumentNullException(nameof (padding));
      if (padding == RSAEncryptionPadding.Pkcs1)
        return this.Encrypt(data, false);
      if (padding == RSAEncryptionPadding.OaepSHA1)
        return this.Encrypt(data, true);
      throw RSACryptoServiceProvider.PaddingModeNotSupported();
    }

    /// <summary>
    ///   Расшифровывает данные, зашифрованные с помощью алгоритма <see cref="T:System.Security.Cryptography.RSA" /> с использованием указанного заполнения.
    /// </summary>
    /// <param name="data">Расшифровываемые данные.</param>
    /// <param name="padding">Заполнение.</param>
    /// <returns>Расшифрованные данные.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Этот режим заполнения не поддерживается.
    /// </exception>
    public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (padding == (RSAEncryptionPadding) null)
        throw new ArgumentNullException(nameof (padding));
      if (padding == RSAEncryptionPadding.Pkcs1)
        return this.Decrypt(data, false);
      if (padding == RSAEncryptionPadding.OaepSHA1)
        return this.Decrypt(data, true);
      throw RSACryptoServiceProvider.PaddingModeNotSupported();
    }

    /// <summary>
    ///   Вычисляет подпись для указанного хэш-значения путем его шифрования с закрытым ключом с использованием указанного заполнения.
    /// </summary>
    /// <param name="hash">Хэш-значение подписываемых данных.</param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="padding">Заполнение.</param>
    /// <returns>
    ///   Подпись <see cref="T:System.Security.Cryptography.RSA" /> для указанного хэш-значения.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="hash" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение параметра <paramref name="padding" /> не равно <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.
    /// </exception>
    public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (hash == null)
        throw new ArgumentNullException(nameof (hash));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      if (padding != RSASignaturePadding.Pkcs1)
        throw RSACryptoServiceProvider.PaddingModeNotSupported();
      return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем определения хэш-значения в этой подписи с помощью указанного хэш-алгоритма и заполнения, сравнивая его с предоставленным хэш-значением.
    /// </summary>
    /// <param name="hash">Хэш-значение подписанных данных.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Имя хэш-алгоритма, используемого для создания хэш-значения.
    /// </param>
    /// <param name="padding">Заполнение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="hash" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение параметра <paramref name="padding" /> не равно <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.
    /// </exception>
    public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (hash == null)
        throw new ArgumentNullException(nameof (hash));
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      if (padding != RSASignaturePadding.Pkcs1)
        throw RSACryptoServiceProvider.PaddingModeNotSupported();
      return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
    }

    private static Exception PaddingModeNotSupported()
    {
      return (Exception) new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
    }
  }
}
