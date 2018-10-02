// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.UrlMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки ее URL-адрес.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private URLString m_url;
    private SecurityElement m_element;

    internal UrlMembershipCondition()
    {
      this.m_url = (URLString) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.UrlMembershipCondition" /> класса с URL-адресом, определяющим членство.
    /// </summary>
    /// <param name="url">
    ///   URL-адрес, для которого требуется проверить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="url" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="url" /> должен быть абсолютный URL-адрес.
    /// </exception>
    public UrlMembershipCondition(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url));
      this.m_url = new URLString(url, false, true);
      if (this.m_url.IsRelativeFileUrl)
        throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), nameof (url));
    }

    /// <summary>
    ///   Возвращает или задает URL-адрес, для которого в качестве условия членства.
    /// </summary>
    /// <returns>
    ///   URL-адрес, для которого в качестве условия членства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> для <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение не является абсолютным URL-адрес.
    /// </exception>
    public string Url
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        URLString urlString = new URLString(value);
        if (urlString.IsRelativeFileUrl)
          throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), nameof (value));
        this.m_url = urlString;
      }
      get
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        return this.m_url.ToString();
      }
    }

    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное свидетельство удовлетворяет условию членства; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> — <see langword="null" />.
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
      System.Security.Policy.Url hostEvidence = evidence.GetHostEvidence<System.Security.Policy.Url>();
      if (hostEvidence != null)
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        if (hostEvidence.GetURLString().IsSubsetOf((SiteString) this.m_url))
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
    ///   Значение свойства <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> — <see langword="null" />.
    /// </exception>
    public IMembershipCondition Copy()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      return (IMembershipCondition) new UrlMembershipCondition()
      {
        m_url = new URLString(this.m_url.ToString())
      };
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
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
    ///   Контекст уровня политики для разрешения именованного разрешение ссылок на наборы.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> — <see langword="null" />.
    /// </exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.UrlMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_url != null)
        element.AddAttribute("Url", this.m_url.ToString());
      return element;
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   Контекст уровня политики, используемый для разрешения именованные наборы разрешений.
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
        this.m_url = (URLString) null;
      }
    }

    private void ParseURL()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string url = this.m_element.Attribute("Url");
        if (url == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_UrlCannotBeNull"));
        URLString urlString = new URLString(url);
        if (urlString.IsRelativeFileUrl)
          throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"));
        this.m_url = urlString;
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>
    ///   Определяет, эквивалентен ли URL-адрес из указанного объекта URL-адреса, содержащиеся в текущем <see cref="T:System.Security.Policy.UrlMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.UrlMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если URL-адрес из указанного объекта эквивалентен URL-адреса, содержащиеся в текущем <see cref="T:System.Security.Policy.UrlMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> Свойства текущего объекта или указанный объект является <see langword="null" />.
    /// </exception>
    public override bool Equals(object o)
    {
      UrlMembershipCondition membershipCondition = o as UrlMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        if (membershipCondition.m_url == null && membershipCondition.m_element != null)
          membershipCondition.ParseURL();
        if (object.Equals((object) this.m_url, (object) membershipCondition.m_url))
          return true;
      }
      return false;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> — <see langword="null" />.
    /// </exception>
    public override int GetHashCode()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      if (this.m_url != null)
        return this.m_url.GetHashCode();
      return typeof (UrlMembershipCondition).GetHashCode();
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление состояния условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> — <see langword="null" />.
    /// </exception>
    public override string ToString()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      if (this.m_url != null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Url_ToStringArg"), (object) this.m_url.ToString());
      return Environment.GetResourceString("Url_ToString");
    }
  }
}
