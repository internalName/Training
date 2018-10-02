// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.SiteMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки узла, на котором она была создана.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private SiteString m_site;
    private SecurityElement m_element;

    internal SiteMembershipCondition()
    {
      this.m_site = (SiteString) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.SiteMembershipCondition" /> класс с именем узла, определяющим членство.
    /// </summary>
    /// <param name="site">Имя сайта или подстановочное выражение.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="site" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="site" /> Параметр не является допустимым <see cref="T:System.Security.Policy.Site" />.
    /// </exception>
    public SiteMembershipCondition(string site)
    {
      if (site == null)
        throw new ArgumentNullException(nameof (site));
      this.m_site = new SiteString(site);
    }

    /// <summary>
    ///   Возвращает или задает веб-узел, для которого в качестве условия членства.
    /// </summary>
    /// <returns>Веб-узел, для которого в качестве условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> для <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка установить <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> на недопустимый <see cref="T:System.Security.Policy.Site" />.
    /// </exception>
    public string Site
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_site = new SiteString(value);
      }
      get
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (this.m_site != null)
          return this.m_site.ToString();
        return "";
      }
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
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
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
      System.Security.Policy.Site hostEvidence = evidence.GetHostEvidence<System.Security.Policy.Site>();
      if (hostEvidence != null)
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (hostEvidence.GetSiteString().IsSubsetOf(this.m_site))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
    /// </exception>
    public IMembershipCondition Copy()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      return (IMembershipCondition) new SiteMembershipCondition(this.m_site.ToString());
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
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
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекст, используемый для разрешения <see cref="T:System.Security.NamedPermissionSet" /> ссылки.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
    /// </exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.SiteMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_site != null)
        element.AddAttribute("Site", this.m_site.ToString());
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
        this.m_site = (SiteString) null;
        this.m_element = e;
      }
    }

    private void ParseSite()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string site = this.m_element.Attribute("Site");
        if (site == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_SiteCannotBeNull"));
        this.m_site = new SiteString(site);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>
    ///   Определяет ли веб-узел указанного <see cref="T:System.Security.Policy.SiteMembershipCondition" /> объект эквивалентен сайта, содержащиеся в текущем <see cref="T:System.Security.Policy.SiteMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Policy.SiteMembershipCondition" /> Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.SiteMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если веб-узел указанного <see cref="T:System.Security.Policy.SiteMembershipCondition" /> объект эквивалентен сайта, содержащиеся в текущем <see cref="T:System.Security.Policy.SiteMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> Свойство для текущего объекта или указанный объект является <see langword="null" />.
    /// </exception>
    public override bool Equals(object o)
    {
      SiteMembershipCondition membershipCondition = o as SiteMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (membershipCondition.m_site == null && membershipCondition.m_element != null)
          membershipCondition.ParseSite();
        if (object.Equals((object) this.m_site, (object) membershipCondition.m_site))
          return true;
      }
      return false;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
    /// </exception>
    public override int GetHashCode()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      if (this.m_site != null)
        return this.m_site.GetHashCode();
      return typeof (SiteMembershipCondition).GetHashCode();
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление условия членства.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> — <see langword="null" />.
    /// </exception>
    public override string ToString()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      if (this.m_site != null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Site_ToStringArg"), (object) this.m_site);
      return Environment.GetResourceString("Site_ToString");
    }
  }
}
