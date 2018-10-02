// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PublisherIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_x509cert;
    private string m_certFile;
    private string m_signedFile;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public PublisherIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
      this.m_x509cert = (string) null;
      this.m_certFile = (string) null;
      this.m_signedFile = (string) null;
    }

    /// <summary>
    ///   Возвращает или задает сертификат Authenticode X.509v3, который идентифицирует издателя вызывающего кода.
    /// </summary>
    /// <returns>Шестнадцатеричное представление сертификата X.509.</returns>
    public string X509Certificate
    {
      get
      {
        return this.m_x509cert;
      }
      set
      {
        this.m_x509cert = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает файл сертификации, содержащий сертификат Authenticode X.509v3.
    /// </summary>
    /// <returns>
    ///   Путь к файлу сертификата X.509 (обычно имеет расширение CER).
    /// </returns>
    public string CertFile
    {
      get
      {
        return this.m_certFile;
      }
      set
      {
        this.m_certFile = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает подписанный файл, из которого нужно извлечь сертификат Authenticode X.509v3.
    /// </summary>
    /// <returns>Путь файла подпись Authenticode.</returns>
    public string SignedFile
    {
      get
      {
        return this.m_signedFile;
      }
      set
      {
        this.m_signedFile = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый экземпляр <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.Unrestricted);
      if (this.m_x509cert != null)
        return (IPermission) new PublisherIdentityPermission(new System.Security.Cryptography.X509Certificates.X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
      if (this.m_certFile != null)
        return (IPermission) new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
      if (this.m_signedFile != null)
        return (IPermission) new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
      return (IPermission) new PublisherIdentityPermission(PermissionState.None);
    }
  }
}
