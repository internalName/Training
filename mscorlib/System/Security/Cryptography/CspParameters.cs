// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Содержит параметры, передаваемые поставщику служб шифрования (CSP), который выполняет криптографические вычисления.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class CspParameters
  {
    /// <summary>
    ///   Представляет код типа поставщика для <see cref="T:System.Security.Cryptography.CspParameters" />.
    /// </summary>
    public int ProviderType;
    /// <summary>
    ///   Представляет имя поставщика для <see cref="T:System.Security.Cryptography.CspParameters" />.
    /// </summary>
    public string ProviderName;
    /// <summary>
    ///   Представляет имя контейнера ключа для <see cref="T:System.Security.Cryptography.CspParameters" />.
    /// </summary>
    public string KeyContainerName;
    /// <summary>
    ///   Указывает, создан ли асимметричный ключ как ключ подписи или ключ обмена.
    /// </summary>
    public int KeyNumber;
    private int m_flags;
    private CryptoKeySecurity m_cryptoKeySecurity;
    private SecureString m_keyPassword;
    private IntPtr m_parentWindowHandle;

    /// <summary>
    ///   Представляет флаги для <see cref="T:System.Security.Cryptography.CspParameters" />, изменяющие поведение поставщика служб шифрования (CSP).
    /// </summary>
    /// <returns>
    ///   Значение перечисления или побитовое сочетание значений перечисления.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение не является допустимым значением перечисления.
    /// </exception>
    public CspProviderFlags Flags
    {
      get
      {
        return (CspProviderFlags) this.m_flags;
      }
      set
      {
        int maxValue = (int) byte.MaxValue;
        int num = (int) value;
        if ((num & ~maxValue) != 0)
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) value), nameof (value));
        this.m_flags = num;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, что представляет права доступа и аудита для контейнера.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> объекта, что представляет права доступа и аудита для контейнера.
    /// </returns>
    public CryptoKeySecurity CryptoKeySecurity
    {
      get
      {
        return this.m_cryptoKeySecurity;
      }
      set
      {
        this.m_cryptoKeySecurity = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает пароль, связанный со смарт-картой.
    /// </summary>
    /// <returns>Пароль, связанный со смарт-картой.</returns>
    public SecureString KeyPassword
    {
      get
      {
        return this.m_keyPassword;
      }
      set
      {
        this.m_keyPassword = value;
        this.m_parentWindowHandle = IntPtr.Zero;
      }
    }

    /// <summary>
    ///   Возвращает или задает дескриптор неуправляемого родительского окна для диалогового окна пароля смарт-карты.
    /// </summary>
    /// <returns>
    ///   Дескриптор родительского окна для диалогового окна пароля смарт-карты.
    /// </returns>
    public IntPtr ParentWindowHandle
    {
      get
      {
        return this.m_parentWindowHandle;
      }
      set
      {
        this.m_parentWindowHandle = value;
        this.m_keyPassword = (SecureString) null;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CspParameters" />.
    /// </summary>
    public CspParameters()
      : this(24, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CspParameters" /> с заданным кодом типа поставщика.
    /// </summary>
    /// <param name="dwTypeIn">
    ///   Код типа поставщика, определяющий вид создаваемого поставщика.
    /// </param>
    public CspParameters(int dwTypeIn)
      : this(dwTypeIn, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CspParameters" /> с заданным кодом типа поставщика и именем.
    /// </summary>
    /// <param name="dwTypeIn">
    ///   Код типа поставщика, определяющий вид создаваемого поставщика.
    /// </param>
    /// <param name="strProviderNameIn">Имя поставщика.</param>
    public CspParameters(int dwTypeIn, string strProviderNameIn)
      : this(dwTypeIn, strProviderNameIn, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.CspParameters" /> с заданным кодом типа и именем поставщика и именем контейнера.
    /// </summary>
    /// <param name="dwTypeIn">
    ///   Код типа поставщика, определяющий вид создаваемого поставщика.
    /// </param>
    /// <param name="strProviderNameIn">Имя поставщика.</param>
    /// <param name="strContainerNameIn">Имя контейнера.</param>
    public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
      : this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.CspParameters" /> класса, используя тип поставщика, имя поставщика, имя контейнера, доступ к информации и пароль, связанный со смарт-картой.
    /// </summary>
    /// <param name="providerType">
    ///   Код типа поставщика, определяющий вид создаваемого поставщика.
    /// </param>
    /// <param name="providerName">Имя поставщика.</param>
    /// <param name="keyContainerName">Имя контейнера.</param>
    /// <param name="cryptoKeySecurity">
    ///   Объект, представляющий права доступа и аудита правила для контейнера.
    /// </param>
    /// <param name="keyPassword">
    ///   Пароль, связанный со смарт-картой.
    /// </param>
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword)
      : this(providerType, providerName, keyContainerName)
    {
      this.m_cryptoKeySecurity = cryptoKeySecurity;
      this.m_keyPassword = keyPassword;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.CspParameters" /> класса, используя тип поставщика, имя поставщика, имя контейнера, доступ к информации и дескриптор неуправляемого окна смарт-карты пароль.
    /// </summary>
    /// <param name="providerType">
    ///   Код типа поставщика, определяющий вид создаваемого поставщика.
    /// </param>
    /// <param name="providerName">Имя поставщика.</param>
    /// <param name="keyContainerName">Имя контейнера.</param>
    /// <param name="cryptoKeySecurity">
    ///   Объект, представляющий права доступа и аудита правила для контейнера.
    /// </param>
    /// <param name="parentWindowHandle">
    ///   Дескриптор родительского окна для окна ввода пароля смарт-карты.
    /// </param>
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
      : this(providerType, providerName, keyContainerName)
    {
      this.m_cryptoKeySecurity = cryptoKeySecurity;
      this.m_parentWindowHandle = parentWindowHandle;
    }

    internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
    {
      this.ProviderType = providerType;
      this.ProviderName = providerName;
      this.KeyContainerName = keyContainerName;
      this.KeyNumber = -1;
      this.Flags = flags;
    }

    internal CspParameters(CspParameters parameters)
    {
      this.ProviderType = parameters.ProviderType;
      this.ProviderName = parameters.ProviderName;
      this.KeyContainerName = parameters.KeyContainerName;
      this.KeyNumber = parameters.KeyNumber;
      this.Flags = parameters.Flags;
      this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
      this.m_keyPassword = parameters.m_keyPassword;
      this.m_parentWindowHandle = parameters.m_parentWindowHandle;
    }
  }
}
