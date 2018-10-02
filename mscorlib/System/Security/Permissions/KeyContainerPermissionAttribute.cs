// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.KeyContainerPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
  {
    private int m_providerType = -1;
    private int m_keySpec = -1;
    private KeyContainerPermissionFlags m_flags;
    private string m_keyStore;
    private string m_providerName;
    private string m_keyContainerName;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> класса с заданными параметрами безопасности действием.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public KeyContainerPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>Возвращает или задает имя хранилища ключей.</summary>
    /// <returns>
    ///   Имя хранилища ключей.
    ///    Значение по умолчанию — «*».
    /// </returns>
    public string KeyStore
    {
      get
      {
        return this.m_keyStore;
      }
      set
      {
        this.m_keyStore = value;
      }
    }

    /// <summary>Возвращает или задает имя поставщика.</summary>
    /// <returns>Имя поставщика.</returns>
    public string ProviderName
    {
      get
      {
        return this.m_providerName;
      }
      set
      {
        this.m_providerName = value;
      }
    }

    /// <summary>Возвращает или задает тип поставщика.</summary>
    /// <returns>
    ///   Одно из значений PROV_, определенное в файле заголовка Wincrypt.h.
    /// </returns>
    public int ProviderType
    {
      get
      {
        return this.m_providerType;
      }
      set
      {
        this.m_providerType = value;
      }
    }

    /// <summary>Возвращает или задает имя контейнера ключа.</summary>
    /// <returns>Имя контейнера ключа.</returns>
    public string KeyContainerName
    {
      get
      {
        return this.m_keyContainerName;
      }
      set
      {
        this.m_keyContainerName = value;
      }
    }

    /// <summary>Возвращает или задает спецификацию ключа.</summary>
    /// <returns>
    ///   Одно из значений AT_, определенное в файле заголовка Wincrypt.h.
    /// </returns>
    public int KeySpec
    {
      get
      {
        return this.m_keySpec;
      }
      set
      {
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
        this.m_flags = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.KeyContainerPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.KeyContainerPermission" /> соответствующее атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new KeyContainerPermission(PermissionState.Unrestricted);
      if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec))
        return (IPermission) new KeyContainerPermission(this.m_flags);
      KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
      KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec, this.m_flags);
      containerPermission.AccessEntries.Add(accessEntry);
      return (IPermission) containerPermission;
    }
  }
}
