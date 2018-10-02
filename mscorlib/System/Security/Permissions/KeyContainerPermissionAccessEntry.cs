// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAccessEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Задает права доступа для конкретных контейнеров ключей.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAccessEntry
  {
    private string m_keyStore;
    private string m_providerName;
    private int m_providerType;
    private string m_keyContainerName;
    private int m_keySpec;
    private KeyContainerPermissionFlags m_flags;

    internal KeyContainerPermissionAccessEntry(KeyContainerPermissionAccessEntry accessEntry)
      : this(accessEntry.KeyStore, accessEntry.ProviderName, accessEntry.ProviderType, accessEntry.KeyContainerName, accessEntry.KeySpec, accessEntry.Flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> класс с помощью указанный контейнер ключей имя и разрешения доступа.
    /// </summary>
    /// <param name="keyContainerName">Имя контейнера ключа.</param>
    /// <param name="flags">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
      : this((string) null, (string) null, -1, keyContainerName, -1, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> класса, используя параметры поставщика криптографических услуг и разрешения на доступ.
    /// </summary>
    /// <param name="parameters">
    ///   Объект <see cref="T:System.Security.Cryptography.CspParameters" /> содержащий параметры поставщика служб шифрования.
    /// </param>
    /// <param name="flags">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
      : this((parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore ? "Machine" : "User", parameters.ProviderName, parameters.ProviderType, parameters.KeyContainerName, parameters.KeyNumber, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> класса с заданными значениями свойства.
    /// </summary>
    /// <param name="keyStore">Имя хранилища ключей.</param>
    /// <param name="providerName">Имя поставщика.</param>
    /// <param name="providerType">
    ///   Код типа поставщика.
    ///    В разделе <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.ProviderType" /> свойство для значения.
    /// </param>
    /// <param name="keyContainerName">Имя контейнера ключа.</param>
    /// <param name="keySpec">
    ///   Спецификация ключа.
    ///    В разделе <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.KeySpec" /> свойство для значения.
    /// </param>
    /// <param name="flags">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
    {
      this.m_providerName = providerName == null ? "*" : providerName;
      this.m_providerType = providerType;
      this.m_keyContainerName = keyContainerName == null ? "*" : keyContainerName;
      this.m_keySpec = keySpec;
      this.KeyStore = keyStore;
      this.Flags = flags;
    }

    /// <summary>Возвращает или задает имя хранилища ключей.</summary>
    /// <returns>Имя хранилища ключей.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public string KeyStore
    {
      get
      {
        return this.m_keyStore;
      }
      set
      {
        if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(value, this.ProviderName, this.ProviderType, this.KeyContainerName, this.KeySpec))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
        if (value == null)
        {
          this.m_keyStore = "*";
        }
        else
        {
          if (value != "User" && value != "Machine" && value != "*")
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKeyStore", (object) value), nameof (value));
          this.m_keyStore = value;
        }
      }
    }

    /// <summary>Возвращает или задает имя поставщика.</summary>
    /// <returns>Имя поставщика.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public string ProviderName
    {
      get
      {
        return this.m_providerName;
      }
      set
      {
        if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, value, this.ProviderType, this.KeyContainerName, this.KeySpec))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
        if (value == null)
          this.m_providerName = "*";
        else
          this.m_providerName = value;
      }
    }

    /// <summary>Возвращает или задает тип поставщика.</summary>
    /// <returns>
    ///   Одно из значений PROV_, определенное в файле заголовка Wincrypt.h.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public int ProviderType
    {
      get
      {
        return this.m_providerType;
      }
      set
      {
        if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, value, this.KeyContainerName, this.KeySpec))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
        this.m_providerType = value;
      }
    }

    /// <summary>Возвращает или задает имя контейнера ключа.</summary>
    /// <returns>Имя контейнера ключа.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public string KeyContainerName
    {
      get
      {
        return this.m_keyContainerName;
      }
      set
      {
        if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, value, this.KeySpec))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
        if (value == null)
          this.m_keyContainerName = "*";
        else
          this.m_keyContainerName = value;
      }
    }

    /// <summary>Возвращает или задает спецификацию ключа.</summary>
    /// <returns>
    ///   Одно из значений AT_, определенное в файле заголовка Wincrypt.h.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Результирующая запись будет неограниченный доступ.
    /// </exception>
    public int KeySpec
    {
      get
      {
        return this.m_keySpec;
      }
      set
      {
        if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, this.KeyContainerName, value))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
        this.m_keySpec = value;
      }
    }

    /// <summary>Возвращает или задает разрешения контейнера ключа.</summary>
    /// <returns>
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.
    /// </returns>
    public KeyContainerPermissionFlags Flags
    {
      get
      {
        return this.m_flags;
      }
      set
      {
        KeyContainerPermission.VerifyFlags(value);
        this.m_flags = value;
      }
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объект равен текущему экземпляру.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> Объект для сравнения с значение currentinstance.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> равен текущему объекту <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      KeyContainerPermissionAccessEntry permissionAccessEntry = o as KeyContainerPermissionAccessEntry;
      return permissionAccessEntry != null && !(permissionAccessEntry.m_keyStore != this.m_keyStore) && (!(permissionAccessEntry.m_providerName != this.m_providerName) && permissionAccessEntry.m_providerType == this.m_providerType) && (!(permissionAccessEntry.m_keyContainerName != this.m_keyContainerName) && permissionAccessEntry.m_keySpec == this.m_keySpec);
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего экземпляра, подходящий для использования в алгоритмах хэширования и структур данных, таких как хэш-таблицы.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />.
    /// </returns>
    public override int GetHashCode()
    {
      return 0 | (this.m_keyStore.GetHashCode() & (int) byte.MaxValue) << 24 | (this.m_providerName.GetHashCode() & (int) byte.MaxValue) << 16 | (this.m_providerType & 15) << 12 | (this.m_keyContainerName.GetHashCode() & (int) byte.MaxValue) << 4 | this.m_keySpec & 15;
    }

    internal bool IsSubsetOf(KeyContainerPermissionAccessEntry target)
    {
      return (!(target.m_keyStore != "*") || !(this.m_keyStore != target.m_keyStore)) && (!(target.m_providerName != "*") || !(this.m_providerName != target.m_providerName)) && ((target.m_providerType == -1 || this.m_providerType == target.m_providerType) && (!(target.m_keyContainerName != "*") || !(this.m_keyContainerName != target.m_keyContainerName))) && (target.m_keySpec == -1 || this.m_keySpec == target.m_keySpec);
    }

    internal static bool IsUnrestrictedEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec)
    {
      return (!(keyStore != "*") || keyStore == null) && (!(providerName != "*") || providerName == null) && (providerType == -1 && (!(keyContainerName != "*") || keyContainerName == null)) && keySpec == -1;
    }
  }
}
