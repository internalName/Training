// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.StrongNameMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки ее строгого имени.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private StrongNamePublicKeyBlob m_publicKeyBlob;
    private string m_name;
    private Version m_version;
    private SecurityElement m_element;
    private const string s_tagName = "Name";
    private const string s_tagVersion = "AssemblyVersion";
    private const string s_tagPublicKeyBlob = "PublicKeyBlob";

    internal StrongNameMembershipCondition()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> класс со строгим именем открытый ключ больших двоичных объектов, имя и версия number, определяющими членство.
    /// </summary>
    /// <param name="blob">
    ///   Строгое имя объекта blob открытого ключа издателя программного обеспечения.
    /// </param>
    /// <param name="name">Секция простого имени строгое имя.</param>
    /// <param name="version">Номер версии строгого имени.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="blob" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
    {
      if (blob == null)
        throw new ArgumentNullException(nameof (blob));
      if (name != null && name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      this.m_publicKeyBlob = blob;
      this.m_name = name;
      this.m_version = version;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> из <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> Из <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> для <see langword="null" />.
    /// </exception>
    public StrongNamePublicKeyBlob PublicKey
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PublicKey));
        this.m_publicKeyBlob = value;
      }
      get
      {
        if (this.m_publicKeyBlob == null && this.m_element != null)
          this.ParseKeyBlob();
        return this.m_publicKeyBlob;
      }
    }

    /// <summary>
    ///   Возвращает или задает простое имя <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </summary>
    /// <returns>
    ///   Простое имя <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Значением является пустая строка ("").
    /// </exception>
    public string Name
    {
      set
      {
        if (value == null)
        {
          if (this.m_publicKeyBlob == null && this.m_element != null)
            this.ParseKeyBlob();
          if ((object) this.m_version == null && this.m_element != null)
            this.ParseVersion();
          this.m_element = (SecurityElement) null;
        }
        else if (value.Length == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
        this.m_name = value;
      }
      get
      {
        if (this.m_name == null && this.m_element != null)
          this.ParseName();
        return this.m_name;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Version" /> из <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Version" /> Из <see cref="T:System.Security.Policy.StrongName" /> для какие тесты в качестве условия членства.
    /// </returns>
    public Version Version
    {
      set
      {
        if (value == (Version) null)
        {
          if (this.m_name == null && this.m_element != null)
            this.ParseName();
          if (this.m_publicKeyBlob == null && this.m_element != null)
            this.ParseKeyBlob();
          this.m_element = (SecurityElement) null;
        }
        this.m_version = value;
      }
      get
      {
        if ((object) this.m_version == null && this.m_element != null)
          this.ParseVersion();
        return this.m_version;
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
      StrongName evaluatedHostEvidence = evidence.GetDelayEvaluatedHostEvidence<StrongName>();
      if (evaluatedHostEvidence == null || !((this.PublicKey != null && this.PublicKey.Equals(evaluatedHostEvidence.PublicKey)) & (this.Name == null || evaluatedHostEvidence.Name != null && StrongName.CompareNames(evaluatedHostEvidence.Name, this.Name)) & ((object) this.Version == null || (object) evaluatedHostEvidence.Version != null && evaluatedHostEvidence.Version.CompareTo(this.Version) == 0)))
        return false;
      usedEvidence = (object) evaluatedHostEvidence;
      return true;
    }

    /// <summary>
    ///   Создает эквивалентную копию текущего объекта <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </summary>
    /// <returns>
    ///   Новая, идентичная копия текущего объекта <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />
    /// </returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new StrongNameMembershipCondition(this.PublicKey, this.Name, this.Version);
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
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.StrongNameMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.PublicKey != null)
        element.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.PublicKey.PublicKey));
      if (this.Name != null)
        element.AddAttribute("Name", this.Name);
      if ((object) this.Version != null)
        element.AddAttribute("AssemblyVersion", this.Version.ToString());
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
        this.m_name = (string) null;
        this.m_publicKeyBlob = (StrongNamePublicKeyBlob) null;
        this.m_version = (Version) null;
        this.m_element = e;
      }
    }

    private void ParseName()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string str = this.m_element.Attribute("Name");
        this.m_name = str == null ? (string) null : str;
        if ((object) this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private void ParseKeyBlob()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string hexString = this.m_element.Attribute("PublicKeyBlob");
        StrongNamePublicKeyBlob namePublicKeyBlob = new StrongNamePublicKeyBlob();
        if (hexString == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_BlobCannotBeNull"));
        namePublicKeyBlob.PublicKey = Hex.DecodeHexString(hexString);
        this.m_publicKeyBlob = namePublicKeyBlob;
        if ((object) this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private void ParseVersion()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string version = this.m_element.Attribute("AssemblyVersion");
        this.m_version = version == null ? (Version) null : new Version(version);
        if ((object) this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>
    ///   Создает и возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </returns>
    public override string ToString()
    {
      string str1 = "";
      string str2 = "";
      if (this.Name != null)
        str1 = " " + string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Name"), (object) this.Name);
      if ((object) this.Version != null)
        str2 = " " + string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Version"), (object) this.Version);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_ToString"), (object) Hex.EncodeHexString(this.PublicKey.PublicKey), (object) str1, (object) str2);
    }

    /// <summary>
    ///   Определяет, является ли <see cref="T:System.Security.Policy.StrongName" /> из указанного объекта эквивалентен <see cref="T:System.Security.Policy.StrongName" /> содержится в текущем <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Security.Policy.StrongName" /> из указанного объекта эквивалентно <see cref="T:System.Security.Policy.StrongName" /> содержится в текущем <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> Свойства текущего объекта или указанный объект является <see langword="null" />.
    /// </exception>
    public override bool Equals(object o)
    {
      StrongNameMembershipCondition membershipCondition = o as StrongNameMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_publicKeyBlob == null && this.m_element != null)
          this.ParseKeyBlob();
        if (membershipCondition.m_publicKeyBlob == null && membershipCondition.m_element != null)
          membershipCondition.ParseKeyBlob();
        if (object.Equals((object) this.m_publicKeyBlob, (object) membershipCondition.m_publicKeyBlob))
        {
          if (this.m_name == null && this.m_element != null)
            this.ParseName();
          if (membershipCondition.m_name == null && membershipCondition.m_element != null)
            membershipCondition.ParseName();
          if (object.Equals((object) this.m_name, (object) membershipCondition.m_name))
          {
            if (this.m_version == (Version) null && this.m_element != null)
              this.ParseVersion();
            if (membershipCondition.m_version == (Version) null && membershipCondition.m_element != null)
              membershipCondition.ParseVersion();
            if (object.Equals((object) this.m_version, (object) membershipCondition.m_version))
              return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего объекта класса <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего значения свойства <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение свойства <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> — <see langword="null" />.
    /// </exception>
    public override int GetHashCode()
    {
      if (this.m_publicKeyBlob == null && this.m_element != null)
        this.ParseKeyBlob();
      if (this.m_publicKeyBlob != null)
        return this.m_publicKeyBlob.GetHashCode();
      if (this.m_name == null && this.m_element != null)
        this.ParseName();
      if (this.m_version == (Version) null && this.m_element != null)
        this.ParseVersion();
      if (this.m_name != null || this.m_version != (Version) null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_version == (Version) null ? 0 : this.m_version.GetHashCode());
      return typeof (StrongNameMembershipCondition).GetHashCode();
    }
  }
}
