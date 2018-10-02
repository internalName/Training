// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Publisher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет цифровую подпись Authenticode X.509v3 сборки кода в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory
  {
    private X509Certificate m_cert;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.Publisher" /> класса с помощью Authenticode x.509v3, содержащим открытый ключ издателя.
    /// </summary>
    /// <param name="cert">
    ///   <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> Содержащий открытый ключ издателя программного обеспечения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="cert" /> имеет значение <see langword="null" />.
    /// </exception>
    public Publisher(X509Certificate cert)
    {
      if (cert == null)
        throw new ArgumentNullException(nameof (cert));
      this.m_cert = cert;
    }

    /// <summary>
    ///   Создает разрешение подлинности, соответствующее текущему экземпляру <see cref="T:System.Security.Policy.Publisher" /> класса.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Из которого создается разрешение идентификации.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> для указанного <see cref="T:System.Security.Policy.Publisher" />.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new PublisherIdentityPermission(this.m_cert);
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Security.Policy.Publisher" /> для указанного объекта для эквивалентности.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Policy.Publisher" /> Который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если два экземпляра <see cref="T:System.Security.Policy.Publisher" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> Параметр не <see cref="T:System.Security.Policy.Publisher" /> объекта.
    /// </exception>
    public override bool Equals(object o)
    {
      Publisher publisher = o as Publisher;
      if (publisher != null)
        return Publisher.PublicKeyEquals(this.m_cert, publisher.m_cert);
      return false;
    }

    internal static bool PublicKeyEquals(X509Certificate cert1, X509Certificate cert2)
    {
      if (cert1 == null)
        return cert2 == null;
      if (cert2 == null)
        return false;
      byte[] publicKey1 = cert1.GetPublicKey();
      string keyAlgorithm1 = cert1.GetKeyAlgorithm();
      byte[] algorithmParameters1 = cert1.GetKeyAlgorithmParameters();
      byte[] publicKey2 = cert2.GetPublicKey();
      string keyAlgorithm2 = cert2.GetKeyAlgorithm();
      byte[] algorithmParameters2 = cert2.GetKeyAlgorithmParameters();
      int length1 = publicKey1.Length;
      if (length1 != publicKey2.Length)
        return false;
      for (int index = 0; index < length1; ++index)
      {
        if ((int) publicKey1[index] != (int) publicKey2[index])
          return false;
      }
      if (!keyAlgorithm1.Equals(keyAlgorithm2))
        return false;
      int length2 = algorithmParameters1.Length;
      if (algorithmParameters2.Length != length2)
        return false;
      for (int index = 0; index < length2; ++index)
      {
        if ((int) algorithmParameters1[index] != (int) algorithmParameters2[index])
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Возвращает хэш-код текущего <see cref="P:System.Security.Policy.Publisher.Certificate" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код текущего <see cref="P:System.Security.Policy.Publisher.Certificate" />.
    /// </returns>
    public override int GetHashCode()
    {
      return this.m_cert.GetHashCode();
    }

    /// <summary>
    ///   Возвращает сертификат Authenticode X.509v3 издателя.
    /// </summary>
    /// <returns>
    ///   Издатель <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.
    /// </returns>
    public X509Certificate Certificate
    {
      get
      {
        return new X509Certificate(this.m_cert);
      }
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Publisher(this.m_cert);
    }

    /// <summary>
    ///   Создает эквивалентную копию <see cref="T:System.Security.Policy.Publisher" />.
    /// </summary>
    /// <returns>
    ///   Новая, идентичная копия <see cref="T:System.Security.Policy.Publisher" />.
    /// </returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Publisher");
      securityElement.AddAttribute("version", "1");
      securityElement.AddChild(new SecurityElement("X509v3Certificate", this.m_cert != null ? this.m_cert.GetRawCertDataString() : ""));
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.Publisher" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.Publisher" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      MemoryStream memoryStream = new MemoryStream(this.m_cert.GetRawCertData());
      memoryStream.Position = 0L;
      return (object) memoryStream;
    }
  }
}
