// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509Certificate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
  /// <summary>
  ///   Предоставляет методы, помогающие использовать сертификаты X.509 v.3.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class X509Certificate : IDisposable, IDeserializationCallback, ISerializable
  {
    private const string m_format = "X509";
    private string m_subjectName;
    private string m_issuerName;
    private byte[] m_serialNumber;
    private byte[] m_publicKeyParameters;
    private byte[] m_publicKeyValue;
    private string m_publicKeyOid;
    private byte[] m_rawData;
    private byte[] m_thumbprint;
    private DateTime m_notBefore;
    private DateTime m_notAfter;
    [SecurityCritical]
    private SafeCertContextHandle m_safeCertContext;
    private bool m_certContextCloned;
    internal const X509KeyStorageFlags KeyStorageFlagsAll = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet;

    [SecuritySafeCritical]
    private void Init()
    {
      this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.
    /// </summary>
    public X509Certificate()
    {
      this.Init();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класс, определенный в последовательность байтов, представляющий сертификат X.509v3.
    /// </summary>
    /// <param name="data">
    ///   Массив байтов, содержащий данные сертификата X.509.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    public X509Certificate(byte[] data)
      : this()
    {
      if (data == null || data.Length == 0)
        return;
      this.LoadCertificateFromBlob(data, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием массива байтов и пароля.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    public X509Certificate(byte[] rawData, string password)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием массива байтов и пароля.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные из сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    public X509Certificate(byte[] rawData, SecureString password)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием массива байтов, пароля и флага хранилища ключей.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием массива байтов, пароля и флага хранилища ключей.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные из сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса, используя имя PKCS7 подписанного файла.
    /// </summary>
    /// <param name="fileName">Имя подписанного файла PKCS7.</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса, используя имя PKCS7 подписи файла и пароль для доступа к сертификату.
    /// </summary>
    /// <param name="fileName">Имя подписанного файла PKCS7.</param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, string password)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием имени файла сертификата и пароля.
    /// </summary>
    /// <param name="fileName">Имя файла сертификата.</param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, SecureString password)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса, используя имя PKCS7 подписанного файла пароля для доступа к сертификата и флагом хранилища ключей.
    /// </summary>
    /// <param name="fileName">Имя подписанного файла PKCS7.</param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с использованием имени файла сертификата, пароля и флага хранилища ключей.
    /// </summary>
    /// <param name="fileName">Имя файла сертификата.</param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса с помощью дескриптора неуправляемой <see langword="PCCERT_CONTEXT" /> структуры.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор неуправляемой <see langword="PCCERT_CONTEXT" /> структуры.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр дескриптора не представляет допустимый <see langword="PCCERT_CONTEXT" /> структуры.
    /// </exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public X509Certificate(IntPtr handle)
      : this()
    {
      if (handle == IntPtr.Zero)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), nameof (handle));
      X509Utils._DuplicateCertContext(handle, ref this.m_safeCertContext);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса с помощью другого <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса.
    /// </summary>
    /// <param name="cert">
    ///   A <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класс, из которого инициализируется данный класс.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   параметр <paramref name="cert" /> имеет значение <see langword="null" />;
    /// </exception>
    [SecuritySafeCritical]
    public X509Certificate(X509Certificate cert)
      : this()
    {
      if (cert == null)
        throw new ArgumentNullException(nameof (cert));
      if (!(cert.m_safeCertContext.pCertContext != IntPtr.Zero))
        return;
      this.m_safeCertContext = cert.GetCertContextForCloning();
      this.m_certContextCloned = true;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> класса <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объекта и <see cref="T:System.Runtime.Serialization.StreamingContext" /> структуры.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> описывающий сведения о сериализации.
    /// </param>
    /// <param name="context">
    ///   A <see cref="T:System.Runtime.Serialization.StreamingContext" /> Структура, описывающая, как следует выполнять сериализацию.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    public X509Certificate(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] rawData = (byte[]) info.GetValue(nameof (RawData), typeof (byte[]));
      if (rawData == null)
        return;
      this.LoadCertificateFromBlob(rawData, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Создает сертификат X.509v3 из заданного подписанного файла PKCS7.
    /// </summary>
    /// <param name="filename">
    ///   Путь PKCS7 подписанного файла, из которого создается сертификат X.509.
    /// </param>
    /// <returns>Созданный сертификат X.509.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="filename" /> имеет значение <see langword="null" />.
    /// </exception>
    public static X509Certificate CreateFromCertFile(string filename)
    {
      return new X509Certificate(filename);
    }

    /// <summary>
    ///   Создает сертификат X.509v3 из заданного подписанного файла.
    /// </summary>
    /// <param name="filename">
    ///   Путь к подписанному файлу для создания сертификата X.509.
    /// </param>
    /// <returns>Созданный сертификат X.509.</returns>
    public static X509Certificate CreateFromSignedFile(string filename)
    {
      return new X509Certificate(filename);
    }

    /// <summary>
    ///   Возвращает дескриптор контекста сертификата Microsoft Cryptographic API, описываемого неуправляемой <see langword="PCCERT_CONTEXT" /> структуры.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.IntPtr" /> Структура, представляющая неуправляемую <see langword="PCCERT_CONTEXT" /> структуры.
    /// </returns>
    [ComVisible(false)]
    public IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.m_safeCertContext.pCertContext;
      }
    }

    /// <summary>
    ///   Возвращает имя участника, которому выдан сертификат.
    /// </summary>
    /// <returns>Имя участника, которому выдан сертификат.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated.  Please use the Subject property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual string GetName()
    {
      this.ThrowIfContextInvalid();
      return X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, true);
    }

    /// <summary>
    ///   Возвращает имя центра сертификации, выдавшего сертификат X.509v3.
    /// </summary>
    /// <returns>
    ///   Имя центра сертификации, выдавшего сертификат X.509.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// Возникает ошибка с сертификатом.
    ///  Например:
    /// 
    ///     Файл сертификата не существует.
    /// 
    ///     Сертификат недействителен.
    /// 
    ///     Неверный пароль сертификата.
    ///   </exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated.  Please use the Issuer property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual string GetIssuerName()
    {
      this.ThrowIfContextInvalid();
      return X509Utils._GetIssuerName(this.m_safeCertContext, true);
    }

    /// <summary>
    ///   Возвращает серийный номер сертификата X.509v3 в виде массива байтов.
    /// </summary>
    /// <returns>
    ///   Серийный номер сертификата X.509 в виде массива байтов.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    public virtual byte[] GetSerialNumber()
    {
      this.ThrowIfContextInvalid();
      if (this.m_serialNumber == null)
        this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
      return (byte[]) this.m_serialNumber.Clone();
    }

    /// <summary>
    ///   Возвращает серийный номер сертификата X.509v3 в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Серийный номер сертификата X.509 в виде шестнадцатеричной строки.
    /// </returns>
    public virtual string GetSerialNumberString()
    {
      return this.SerialNumber;
    }

    /// <summary>
    ///   Возвращает параметры алгоритма ключа для сертификата X.509v3 в виде массива байтов.
    /// </summary>
    /// <returns>
    ///   Параметры алгоритма ключа для сертификата X.509 в виде массива байтов.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    public virtual byte[] GetKeyAlgorithmParameters()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyParameters == null)
        this.m_publicKeyParameters = X509Utils._GetPublicKeyParameters(this.m_safeCertContext);
      return (byte[]) this.m_publicKeyParameters.Clone();
    }

    /// <summary>
    ///   Возвращает параметры алгоритма ключа для сертификата X.509v3 в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Параметры алгоритма ключа для сертификата X.509 в виде шестнадцатеричной строки.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    public virtual string GetKeyAlgorithmParametersString()
    {
      this.ThrowIfContextInvalid();
      return Hex.EncodeHexString(this.GetKeyAlgorithmParameters());
    }

    /// <summary>
    ///   Возвращает сведения об алгоритме ключа для сертификата X.509v3 в виде строки.
    /// </summary>
    /// <returns>
    ///   Сведения об алгоритме ключа для сертификата X.509 в виде строки.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    public virtual string GetKeyAlgorithm()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyOid == null)
        this.m_publicKeyOid = X509Utils._GetPublicKeyOid(this.m_safeCertContext);
      return this.m_publicKeyOid;
    }

    /// <summary>
    ///   Возвращает открытый ключ для сертификата X.509v3 в виде массива байтов.
    /// </summary>
    /// <returns>
    ///   Открытый ключ для сертификата X.509 в виде массива байтов.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый контекст сертификата.
    /// </exception>
    [SecuritySafeCritical]
    public virtual byte[] GetPublicKey()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyValue == null)
        this.m_publicKeyValue = X509Utils._GetPublicKeyValue(this.m_safeCertContext);
      return (byte[]) this.m_publicKeyValue.Clone();
    }

    /// <summary>
    ///   Возвращает открытый ключ для сертификата X.509v3 в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Открытый ключ для сертификата X.509 в виде шестнадцатеричной строки.
    /// </returns>
    public virtual string GetPublicKeyString()
    {
      return Hex.EncodeHexString(this.GetPublicKey());
    }

    /// <summary>
    ///   Возвращает необработанные данные для всего сертификата X.509v3 в виде массива байтов.
    /// </summary>
    /// <returns>Массив байтов, содержащий данные сертификата X.509.</returns>
    [SecuritySafeCritical]
    public virtual byte[] GetRawCertData()
    {
      return this.RawData;
    }

    /// <summary>
    ///   Возвращает необработанные данные для всего сертификата X.509v3 в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Данные сертификата X.509 в виде шестнадцатеричной строки.
    /// </returns>
    public virtual string GetRawCertDataString()
    {
      return Hex.EncodeHexString(this.GetRawCertData());
    }

    /// <summary>
    ///   Возвращает хэш-значение для сертификата X.509v3 в виде массива байтов.
    /// </summary>
    /// <returns>Хэш-значение для сертификата X.509.</returns>
    public virtual byte[] GetCertHash()
    {
      this.SetThumbprint();
      return (byte[]) this.m_thumbprint.Clone();
    }

    /// <summary>
    ///   Возвращает хэш-значение SHA1 для сертификата X.509v3 в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Представление шестнадцатеричной строки хэш-значения сертификата X.509.
    /// </returns>
    public virtual string GetCertHashString()
    {
      this.SetThumbprint();
      return Hex.EncodeHexString(this.m_thumbprint);
    }

    /// <summary>
    ///   Возвращает дату вступления в силу сертификата X.509v3.
    /// </summary>
    /// <returns>Дата вступления в силу сертификата X.509.</returns>
    public virtual string GetEffectiveDateString()
    {
      return this.NotBefore.ToString();
    }

    /// <summary>Возвращает срок действия сертификата X.509v3.</summary>
    /// <returns>Срок действия сертификата X.509.</returns>
    public virtual string GetExpirationDateString()
    {
      return this.NotAfter.ToString();
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта на равенство.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> Объект, сравниваемый с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект равен объекта, заданного параметром <paramref name="other" /> параметр; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      if (!(obj is X509Certificate))
        return false;
      return this.Equals((X509Certificate) obj);
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта на равенство.
    /// </summary>
    /// <param name="other">
    ///   <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> Объект, сравниваемый с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект равен объекта, заданного параметром <paramref name="other" /> параметр; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public virtual bool Equals(X509Certificate other)
    {
      if (other == null)
        return false;
      if (this.m_safeCertContext.IsInvalid)
        return other.m_safeCertContext.IsInvalid;
      return this.Issuer.Equals(other.Issuer) && this.SerialNumber.Equals(other.SerialNumber);
    }

    /// <summary>
    ///   Возвращает хэш-код для сертификата X.509v3 в виде целого числа.
    /// </summary>
    /// <returns>Хэш-код для сертификата X.509 в виде целого числа.</returns>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      if (this.m_safeCertContext.IsInvalid)
        return 0;
      this.SetThumbprint();
      int num = 0;
      for (int index = 0; index < this.m_thumbprint.Length && index < 4; ++index)
        num = num << 8 | (int) this.m_thumbprint[index];
      return num;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </summary>
    /// <returns>
    ///   Строковое представление текущего объекта <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </returns>
    public override string ToString()
    {
      return this.ToString(false);
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта с дополнительными сведениями, если указан.
    /// </summary>
    /// <param name="fVerbose">
    ///   <see langword="true" /> для создания подробной формы строкового представления; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Строковое представление текущего объекта <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    public virtual string ToString(bool fVerbose)
    {
      if (!fVerbose || this.m_safeCertContext.IsInvalid)
        return this.GetType().FullName;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[Subject]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.Subject);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Issuer]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.Issuer);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Serial Number]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.SerialNumber);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Not Before]" + Environment.NewLine + "  ");
      stringBuilder.Append(X509Certificate.FormatDate(this.NotBefore));
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Not After]" + Environment.NewLine + "  ");
      stringBuilder.Append(X509Certificate.FormatDate(this.NotAfter));
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Thumbprint]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.GetCertHashString());
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    /// <summary>Преобразует указанные дату и время в строку.</summary>
    /// <param name="date">Преобразовываемые дата и время.</param>
    /// <returns>
    ///   Строковое представление значения <see cref="T:System.DateTime" /> объект.
    /// </returns>
    protected static string FormatDate(DateTime date)
    {
      CultureInfo cultureInfo = CultureInfo.CurrentCulture;
      if (!cultureInfo.DateTimeFormat.Calendar.IsValidDay(date.Year, date.Month, date.Day, 0))
      {
        if (cultureInfo.DateTimeFormat.Calendar is UmAlQuraCalendar)
        {
          cultureInfo = cultureInfo.Clone() as CultureInfo;
          cultureInfo.DateTimeFormat.Calendar = (Calendar) new HijriCalendar();
        }
        else
          cultureInfo = CultureInfo.InvariantCulture;
      }
      return date.ToString((IFormatProvider) cultureInfo);
    }

    /// <summary>Возвращает имя формата сертификата X.509v3.</summary>
    /// <returns>Формат сертификата X.509.</returns>
    public virtual string GetFormat()
    {
      return "X509";
    }

    /// <summary>
    ///   Получает имя центра сертификации, выдавшего сертификат X.509v3.
    /// </summary>
    /// <returns>
    ///   Имя центра сертификации, выдавшего сертификат X.509v3.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый дескриптор сертификата.
    /// </exception>
    public string Issuer
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_issuerName == null)
          this.m_issuerName = X509Utils._GetIssuerName(this.m_safeCertContext, false);
        return this.m_issuerName;
      }
    }

    /// <summary>
    ///   Возвращает различающееся имя субъекта из сертификата.
    /// </summary>
    /// <returns>Различающееся имя субъекта из сертификата.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый дескриптор сертификата.
    /// </exception>
    public string Subject
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_subjectName == null)
          this.m_subjectName = X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, false);
        return this.m_subjectName;
      }
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта данными из массива байтов.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные сертификата X.509.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с помощью данных из массива байтов, пароля и флагов для определения способ импорта закрытого ключа.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> с помощью данных из массива байтов, пароля и флага хранилища ключей.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, содержащий данные из сертификата X.509.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="rawData" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="rawData" /> параметр равен 0.
    /// </exception>
    [SecurityCritical]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> сведениями из файла сертификата.
    /// </summary>
    /// <param name="fileName">
    ///   Имя файла сертификата, представленное в виде строки.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> сведениями из файла сертификата, пароля и <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> значение.
    /// </summary>
    /// <param name="fileName">
    ///   Имя файла сертификата, представленное в виде строки.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> сведениями из файла сертификата, пароля и флага хранилища ключей.
    /// </summary>
    /// <param name="fileName">Имя файла сертификата.</param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <param name="keyStorageFlags">
    ///   Побитовая комбинация перечисления значения этого элемента управления, где и как для импорта сертификата.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>
    ///   Экспортирует текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект в массив байтов в формате, описанном в одном из <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> значения.
    /// </summary>
    /// <param name="contentType">
    ///   Один из <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> значений, описывающих способ форматирования выходных данных.
    /// </param>
    /// <returns>
    ///   Массив байтов, представляющий текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение, отличное от <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, или <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> был передан <paramref name="contentType" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   Не удалось экспортировать сертификат.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual byte[] Export(X509ContentType contentType)
    {
      return this.ExportHelper(contentType, (object) null);
    }

    /// <summary>
    ///   Экспортирует текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект в массив байтов в формате, описанном в одном из <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> значения и с помощью указанного пароля.
    /// </summary>
    /// <param name="contentType">
    ///   Один из <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> значений, описывающих способ форматирования выходных данных.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <returns>
    ///   Массив байтов, представляющий текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение, отличное от <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, или <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> был передан <paramref name="contentType" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   Не удалось экспортировать сертификат.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual byte[] Export(X509ContentType contentType, string password)
    {
      return this.ExportHelper(contentType, (object) password);
    }

    /// <summary>
    ///   Экспортирует текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект в массив байтов, используя указанный формат и пароль.
    /// </summary>
    /// <param name="contentType">
    ///   Один из <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> значений, описывающих способ форматирования выходных данных.
    /// </param>
    /// <param name="password">
    ///   Пароль для доступа к данным сертификата X.509.
    /// </param>
    /// <returns>
    ///   Массив байтов, который представляет текущий <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объекта.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение, отличное от <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, или <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> был передан <paramref name="contentType" /> параметр.
    /// 
    ///   -или-
    /// 
    ///   Не удалось экспортировать сертификат.
    /// </exception>
    [SecuritySafeCritical]
    public virtual byte[] Export(X509ContentType contentType, SecureString password)
    {
      return this.ExportHelper(contentType, (object) password);
    }

    /// <summary>
    ///   Сбрасывает состояние <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> объекта.
    /// </summary>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Reset()
    {
      this.m_subjectName = (string) null;
      this.m_issuerName = (string) null;
      this.m_serialNumber = (byte[]) null;
      this.m_publicKeyParameters = (byte[]) null;
      this.m_publicKeyValue = (byte[]) null;
      this.m_publicKeyOid = (string) null;
      this.m_rawData = (byte[]) null;
      this.m_thumbprint = (byte[]) null;
      this.m_notBefore = DateTime.MinValue;
      this.m_notAfter = DateTime.MinValue;
      if (!this.m_safeCertContext.IsInvalid)
      {
        if (!this.m_certContextCloned)
          this.m_safeCertContext.Dispose();
        this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
      }
      this.m_certContextCloned = false;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим объектом <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает все неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />, и при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.Reset();
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this.m_safeCertContext.IsInvalid)
        info.AddValue("RawData", (object) null);
      else
        info.AddValue("RawData", (object) this.RawData);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    internal SafeCertContextHandle CertContext
    {
      [SecurityCritical] get
      {
        return this.m_safeCertContext;
      }
    }

    [SecurityCritical]
    internal SafeCertContextHandle GetCertContextForCloning()
    {
      this.m_certContextCloned = true;
      return this.m_safeCertContext;
    }

    [SecurityCritical]
    private void ThrowIfContextInvalid()
    {
      if (this.m_safeCertContext.IsInvalid)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
    }

    private DateTime NotAfter
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_notAfter == DateTime.MinValue)
        {
          Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME();
          X509Utils._GetDateNotAfter(this.m_safeCertContext, ref fileTime);
          this.m_notAfter = DateTime.FromFileTime(fileTime.ToTicks());
        }
        return this.m_notAfter;
      }
    }

    private DateTime NotBefore
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_notBefore == DateTime.MinValue)
        {
          Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME();
          X509Utils._GetDateNotBefore(this.m_safeCertContext, ref fileTime);
          this.m_notBefore = DateTime.FromFileTime(fileTime.ToTicks());
        }
        return this.m_notBefore;
      }
    }

    private byte[] RawData
    {
      [SecurityCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_rawData == null)
          this.m_rawData = X509Utils._GetCertRawData(this.m_safeCertContext);
        return (byte[]) this.m_rawData.Clone();
      }
    }

    private string SerialNumber
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_serialNumber == null)
          this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
        return Hex.EncodeHexStringFromInt(this.m_serialNumber);
      }
    }

    [SecuritySafeCritical]
    private void SetThumbprint()
    {
      this.ThrowIfContextInvalid();
      if (this.m_thumbprint != null)
        return;
      this.m_thumbprint = X509Utils._GetThumbprint(this.m_safeCertContext);
    }

    [SecurityCritical]
    private byte[] ExportHelper(X509ContentType contentType, object password)
    {
      switch (contentType)
      {
        case X509ContentType.Cert:
        case X509ContentType.SerializedCert:
          IntPtr num = IntPtr.Zero;
          byte[] numArray = (byte[]) null;
          SafeCertStoreHandle memoryStore = X509Utils.ExportCertToMemoryStore(this);
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            num = X509Utils.PasswordToHGlobalUni(password);
            numArray = X509Utils._ExportCertificatesToBlob(memoryStore, contentType, num);
          }
          finally
          {
            if (num != IntPtr.Zero)
              Marshal.ZeroFreeGlobalAllocUnicode(num);
            memoryStore.Dispose();
          }
          if (numArray == null)
            throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_ExportFailed"));
          return numArray;
        case X509ContentType.Pfx:
          new KeyContainerPermission(KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Export).Demand();
          goto case X509ContentType.Cert;
        default:
          throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_InvalidContentType"));
      }
    }

    [SecuritySafeCritical]
    private void LoadCertificateFromBlob(byte[] rawData, object password, X509KeyStorageFlags keyStorageFlags)
    {
      if (rawData == null || rawData.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullArray"), nameof (rawData));
      if (X509Utils.MapContentType(X509Utils._QueryCertBlobType(rawData)) == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
        new KeyContainerPermission(KeyContainerPermissionFlags.Create).Demand();
      uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = X509Utils.PasswordToHGlobalUni(password);
        X509Utils._LoadCertFromBlob(rawData, num, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocUnicode(num);
      }
    }

    [SecurityCritical]
    private void LoadCertificateFromFile(string fileName, object password, X509KeyStorageFlags keyStorageFlags)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      new FileIOPermission(FileIOPermissionAccess.Read, Path.GetFullPathInternal(fileName)).Demand();
      if (X509Utils.MapContentType(X509Utils._QueryCertFileType(fileName)) == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
        new KeyContainerPermission(KeyContainerPermissionFlags.Create).Demand();
      uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = X509Utils.PasswordToHGlobalUni(password);
        X509Utils._LoadCertFromFile(fileName, num, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocUnicode(num);
      }
    }
  }
}
