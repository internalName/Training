// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspKeyContainerInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Предоставляет дополнительные сведения о паре криптографических ключей шифрования.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class CspKeyContainerInfo
  {
    private CspParameters m_parameters;
    private bool m_randomKeyContainer;

    private CspKeyContainerInfo()
    {
    }

    [SecurityCritical]
    internal CspKeyContainerInfo(CspParameters parameters, bool randomKeyContainer)
    {
      if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      this.m_parameters = new CspParameters(parameters);
      if (this.m_parameters.KeyNumber == -1)
      {
        if (this.m_parameters.ProviderType == 1 || this.m_parameters.ProviderType == 24)
          this.m_parameters.KeyNumber = 1;
        else if (this.m_parameters.ProviderType == 13)
          this.m_parameters.KeyNumber = 2;
      }
      this.m_randomKeyContainer = randomKeyContainer;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" />, используя заданные параметры.
    /// </summary>
    /// <param name="parameters">
    ///   Объект <see cref="T:System.Security.Cryptography.CspParameters" />, который предоставляет сведения о ключе.
    /// </param>
    [SecuritySafeCritical]
    public CspKeyContainerInfo(CspParameters parameters)
      : this(parameters, false)
    {
    }

    /// <summary>
    ///   Получает значение, указывающее, происходит ли ключ из набора ключей компьютера.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ происходит из набора ключей компьютера; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool MachineKeyStore
    {
      get
      {
        return (this.m_parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
      }
    }

    /// <summary>Получает имя поставщика ключа.</summary>
    /// <returns>Имя поставщика.</returns>
    public string ProviderName
    {
      get
      {
        return this.m_parameters.ProviderName;
      }
    }

    /// <summary>Получает тип поставщика ключа.</summary>
    /// <returns>
    ///   Тип поставщика.
    ///    Значение по умолчанию — 1.
    /// </returns>
    public int ProviderType
    {
      get
      {
        return this.m_parameters.ProviderType;
      }
    }

    /// <summary>Получает имя контейнера ключа.</summary>
    /// <returns>Имя контейнера ключа.</returns>
    public string KeyContainerName
    {
      get
      {
        return this.m_parameters.KeyContainerName;
      }
    }

    /// <summary>Возвращает уникальное имя контейнера ключей.</summary>
    /// <returns>Уникальное имя контейнера ключей.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип ключа не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удается найти поставщика служб шифрования.
    /// 
    ///   -или-
    /// 
    ///   Контейнер ключей не найден.
    /// </exception>
    public string UniqueKeyContainerName
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        string providerParameter = (string) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 8U);
        invalidHandle.Dispose();
        return providerParameter;
      }
    }

    /// <summary>
    ///   Получает значение, описывающее, был ли асимметричный ключ создан как ключ подписи или ключ обмена.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Cryptography.KeyNumber" />, описывающее, был ли асимметричный ключ создан как ключ подписи или ключ обмена.
    /// </returns>
    public KeyNumber KeyNumber
    {
      get
      {
        return (KeyNumber) this.m_parameters.KeyNumber;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, возможен ли экспорт ключа из контейнера ключей.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ключ можно экспортировать; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип ключа не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удается найти поставщика служб шифрования.
    /// 
    ///   -или-
    /// 
    ///   Контейнер ключей не найден.
    /// </exception>
    public bool Exportable
    {
      [SecuritySafeCritical] get
      {
        if (this.HardwareDevice)
          return false;
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] providerParameter = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 3U);
        invalidHandle.Dispose();
        return providerParameter[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли ключ аппаратным.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ключ является ключом оборудования. в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удается найти поставщика служб шифрования.
    /// </exception>
    public bool HardwareDevice
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        CspParameters cspParameters = new CspParameters(this.m_parameters);
        cspParameters.KeyContainerName = (string) null;
        cspParameters.Flags = (cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
        uint flags = 4026531840;
        if (Utils._OpenCSP(cspParameters, flags, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] providerParameter = (byte[]) Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 5U);
        invalidHandle.Dispose();
        return providerParameter[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, можно ли удалить ключ из контейнера ключей.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ключ можно удалить; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Поставщик службы криптографии (CSP) не найден.
    /// </exception>
    public bool Removable
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        CspParameters cspParameters = new CspParameters(this.m_parameters);
        cspParameters.KeyContainerName = (string) null;
        cspParameters.Flags = (cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
        uint flags = 4026531840;
        if (Utils._OpenCSP(cspParameters, flags, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] providerParameter = (byte[]) Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 4U);
        invalidHandle.Dispose();
        return providerParameter[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, доступен ли ключа в контейнере ключей.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ключ доступен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип ключа не поддерживается.
    /// </exception>
    public bool Accessible
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          return false;
        byte[] providerParameter = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 6U);
        invalidHandle.Dispose();
        return providerParameter[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, защищен ли пару ключей.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если пара ключей защищена; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип ключа не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удается найти поставщика служб шифрования.
    /// 
    ///   -или-
    /// 
    ///   Контейнер ключей не найден.
    /// </exception>
    public bool Protected
    {
      [SecuritySafeCritical] get
      {
        if (this.HardwareDevice)
          return true;
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] providerParameter = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 7U);
        invalidHandle.Dispose();
        return providerParameter[0] == (byte) 1;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, что представляет права доступа и аудита для контейнера.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, что представляет права доступа и аудита для контейнера.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Тип ключа не поддерживается.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не удается найти поставщика служб шифрования.
    /// 
    ///   -или-
    /// 
    ///   Контейнер ключей не найден.
    /// </exception>
    public CryptoKeySecurity CryptoKeySecurity
    {
      [SecuritySafeCritical] get
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_parameters, KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        using (invalidHandle)
          return Utils.GetKeySetSecurityInfo(invalidHandle, AccessControlSections.All);
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, был ли контейнер ключей создан случайным образом классом криптографии управляемого кода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если контейнер ключей был создан случайным образом; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool RandomlyGenerated
    {
      get
      {
        return this.m_randomKeyContainer;
      }
    }
  }
}
