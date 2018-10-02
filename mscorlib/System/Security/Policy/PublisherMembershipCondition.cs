// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PublisherMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки сертификата издателя программного обеспечения Authenticode X.509v3.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private X509Certificate m_certificate;
    private SecurityElement m_element;

    internal PublisherMembershipCondition()
    {
      this.m_element = (SecurityElement) null;
      this.m_certificate = (X509Certificate) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> класса с помощью сертификата Authenticode X.509v3, определяющим членство.
    /// </summary>
    /// <param name="certificate">
    ///   <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> Содержащий открытый ключ издателя программного обеспечения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="certificate" /> имеет значение <see langword="null" />.
    /// </exception>
    public PublisherMembershipCondition(X509Certificate certificate)
    {
      PublisherMembershipCondition.CheckCertificate(certificate);
      this.m_certificate = new X509Certificate(certificate);
    }

    private static void CheckCertificate(X509Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException(nameof (certificate));
    }

    /// <summary>
    ///   Возвращает или задает сертификат Authenticode X.509v3, который проверяет условие членства.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> Для какие тесты в качестве условия членства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства — <see langword="null" />.
    /// </exception>
    public X509Certificate Certificate
    {
      set
      {
        PublisherMembershipCondition.CheckCertificate(value);
        this.m_certificate = new X509Certificate(value);
      }
      get
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (this.m_certificate != null)
          return new X509Certificate(this.m_certificate);
        return (X509Certificate) null;
      }
    }

    /// <summary>
    ///   Создает и возвращает строковое представление <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.
    /// </summary>
    /// <returns>
    ///   Представление <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public override string ToString()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      if (this.m_certificate == null || this.m_certificate.Subject == null)
        return Environment.GetResourceString("Publisher_ToString");
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Publisher_ToStringArg"), (object) Hex.EncodeHexString(this.m_certificate.GetPublicKey()));
    }

    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Для которого производится проверка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное свидетельство удовлетворяет условию членства; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      if (evidence == null)
        return false;
      Publisher hostEvidence = evidence.GetHostEvidence<Publisher>();
      if (hostEvidence != null)
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (hostEvidence.Equals((object) new Publisher(this.m_certificate)))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public IMembershipCondition Copy()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      return (IMembershipCondition) new PublisherMembershipCondition(this.m_certificate);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> Параметр не недопустимый элемент условия членства.
    /// </exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния с указанным <see cref="T:System.Security.Policy.PolicyLevel" />.
    /// </summary>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекста, который используется для разрешения <see cref="T:System.Security.NamedPermissionSet" /> ссылки.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.PublisherMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_certificate != null)
        element.AddAttribute("X509Certificate", this.m_certificate.GetRawCertDataString());
      return element;
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекст, используемый для разрешения <see cref="T:System.Security.NamedPermissionSet" /> ссылки.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> Параметр не недопустимый элемент условия членства.
    /// </exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
      lock (this)
      {
        this.m_element = e;
        this.m_certificate = (X509Certificate) null;
      }
    }

    private void ParseCertificate()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string hexString = this.m_element.Attribute("X509Certificate");
        this.m_certificate = hexString == null ? (X509Certificate) null : new X509Certificate(Hex.DecodeHexString(hexString));
        PublisherMembershipCondition.CheckCertificate(this.m_certificate);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>
    ///   Определяет, эквивалентен ли сертификат издателя из указанного объекта сертификату, содержащемуся в текущем <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если сертификат издателя из указанного объекта эквивалентен сертификату, содержащемуся в текущем <see cref="T:System.Security.Policy.PublisherMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public override bool Equals(object o)
    {
      PublisherMembershipCondition membershipCondition = o as PublisherMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (membershipCondition.m_certificate == null && membershipCondition.m_element != null)
          membershipCondition.ParseCertificate();
        if (Publisher.PublicKeyEquals(this.m_certificate, membershipCondition.m_certificate))
          return true;
      }
      return false;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> — <see langword="null" />.
    /// </exception>
    public override int GetHashCode()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      if (this.m_certificate != null)
        return this.m_certificate.GetHashCode();
      return typeof (PublisherMembershipCondition).GetHashCode();
    }
  }
}
