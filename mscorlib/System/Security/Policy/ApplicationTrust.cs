// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrust
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Инкапсулирует решений по безопасности о приложении.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
  {
    private ApplicationIdentity m_appId;
    private bool m_appTrustedToRun;
    private bool m_persist;
    private object m_extraInfo;
    private SecurityElement m_elExtraInfo;
    private PolicyStatement m_psDefaultGrant;
    private IList<StrongName> m_fullTrustAssemblies;
    [NonSerialized]
    private int m_grantSetSpecialFlags;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.ApplicationTrust" /> класса <see cref="T:System.ApplicationIdentity" />.
    /// </summary>
    /// <param name="applicationIdentity">
    ///   <see cref="T:System.ApplicationIdentity" /> Однозначно идентифицирующий приложение.
    /// </param>
    public ApplicationTrust(ApplicationIdentity applicationIdentity)
      : this()
    {
      this.ApplicationIdentity = applicationIdentity;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.ApplicationTrust" />.
    /// </summary>
    public ApplicationTrust()
      : this(new PermissionSet(PermissionState.None))
    {
    }

    internal ApplicationTrust(PermissionSet defaultGrantSet)
    {
      this.InitDefaultGrantSet(defaultGrantSet);
      this.m_fullTrustAssemblies = (IList<StrongName>) new List<StrongName>().AsReadOnly();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.ApplicationTrust" /> с использованием набор предоставленных разрешений и коллекцию сборок с полным доверием.
    /// </summary>
    /// <param name="defaultGrantSet">
    ///   Набор разрешений по умолчанию, которые предоставляются всем сборкам, не имеющих специальных разрешений.
    /// </param>
    /// <param name="fullTrustAssemblies">
    ///   Массив строгих имен, представляющих сборки, которые должны будут считаться обладающими полным доверием в домене приложения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="defaultGrantSet" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="fullTrustAssemblies" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fullTrustAssemblies" /> содержит сборки, не <see cref="T:System.Security.Policy.StrongName" />.
    /// </exception>
    public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
    {
      if (fullTrustAssemblies == null)
        throw new ArgumentNullException(nameof (fullTrustAssemblies));
      this.InitDefaultGrantSet(defaultGrantSet);
      List<StrongName> strongNameList = new List<StrongName>();
      foreach (StrongName fullTrustAssembly in fullTrustAssemblies)
      {
        if (fullTrustAssembly == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_NullFullTrustAssembly"));
        strongNameList.Add(new StrongName(fullTrustAssembly.PublicKey, fullTrustAssembly.Name, fullTrustAssembly.Version));
      }
      this.m_fullTrustAssemblies = (IList<StrongName>) strongNameList.AsReadOnly();
    }

    private void InitDefaultGrantSet(PermissionSet defaultGrantSet)
    {
      if (defaultGrantSet == null)
        throw new ArgumentNullException(nameof (defaultGrantSet));
      this.DefaultGrantSet = new PolicyStatement(defaultGrantSet);
    }

    /// <summary>
    ///   Возвращает или задает идентификатор приложения для объекта доверия приложения.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.ApplicationIdentity" /> Для объекта доверия приложения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <see cref="T:System.ApplicationIdentity" /> Невозможно задать, поскольку он имеет значение <see langword="null" />.
    /// </exception>
    public ApplicationIdentity ApplicationIdentity
    {
      get
      {
        return this.m_appId;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(Environment.GetResourceString("Argument_InvalidAppId"));
        this.m_appId = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает инструкцию политики, определяющий набор разрешений по умолчанию.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Policy.PolicyStatement" /> предоставляет описания по умолчанию.
    /// </returns>
    public PolicyStatement DefaultGrantSet
    {
      get
      {
        if (this.m_psDefaultGrant == null)
          return new PolicyStatement(new PermissionSet(PermissionState.None));
        return this.m_psDefaultGrant;
      }
      set
      {
        if (value == null)
        {
          this.m_psDefaultGrant = (PolicyStatement) null;
          this.m_grantSetSpecialFlags = 0;
        }
        else
        {
          this.m_psDefaultGrant = value;
          this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(this.m_psDefaultGrant.PermissionSet, (PermissionSet) null);
        }
      }
    }

    /// <summary>
    ///   Возвращает список сборок с полным доверием доверенными для этого приложения.
    /// </summary>
    /// <returns>Список сборок с полным доверием.</returns>
    public IList<StrongName> FullTrustAssemblies
    {
      get
      {
        return this.m_fullTrustAssemblies;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее имеет необходимые разрешения и является доверенным для запуска приложения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если приложение является доверенным для выполнения; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool IsApplicationTrustedToRun
    {
      get
      {
        return this.m_appTrustedToRun;
      }
      set
      {
        this.m_appTrustedToRun = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, сохраняются ли сведения о доверии для приложения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если сведения о доверии для приложения сохраняется; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool Persist
    {
      get
      {
        return this.m_persist;
      }
      set
      {
        this.m_persist = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает сведения о дополнительной безопасности приложения.
    /// </summary>
    /// <returns>
    ///   Объект, содержащий дополнительные сведения о приложении.
    /// </returns>
    public object ExtraInfo
    {
      get
      {
        if (this.m_elExtraInfo != null)
        {
          this.m_extraInfo = ApplicationTrust.ObjectFromXml(this.m_elExtraInfo);
          this.m_elExtraInfo = (SecurityElement) null;
        }
        return this.m_extraInfo;
      }
      set
      {
        this.m_elExtraInfo = (SecurityElement) null;
        this.m_extraInfo = value;
      }
    }

    /// <summary>
    ///   Создает XML-кодирование <see cref="T:System.Security.Policy.ApplicationTrust" /> объекта и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement(nameof (ApplicationTrust));
      securityElement.AddAttribute("version", "1");
      if (this.m_appId != null)
        securityElement.AddAttribute("FullName", SecurityElement.Escape(this.m_appId.FullName));
      if (this.m_appTrustedToRun)
        securityElement.AddAttribute("TrustedToRun", "true");
      if (this.m_persist)
        securityElement.AddAttribute("Persist", "true");
      if (this.m_psDefaultGrant != null)
      {
        SecurityElement child = new SecurityElement("DefaultGrant");
        child.AddChild(this.m_psDefaultGrant.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_fullTrustAssemblies.Count > 0)
      {
        SecurityElement child = new SecurityElement("FullTrustAssemblies");
        foreach (StrongName fullTrustAssembly in (IEnumerable<StrongName>) this.m_fullTrustAssemblies)
          child.AddChild(fullTrustAssembly.ToXml());
        securityElement.AddChild(child);
      }
      if (this.ExtraInfo != null)
        securityElement.AddChild(ApplicationTrust.ObjectToXml("ExtraInfo", this.ExtraInfo));
      return securityElement;
    }

    /// <summary>
    ///   Восстанавливает <see cref="T:System.Security.Policy.ApplicationTrust" /> объект с данным состоянием из кодировки XML.
    /// </summary>
    /// <param name="element">
    ///   Код XML, используемая для восстановления <see cref="T:System.Security.Policy.ApplicationTrust" /> объекта.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Кодировка XML, используемая для <paramref name="element" /> является недопустимым.
    /// </exception>
    public void FromXml(SecurityElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (string.Compare(element.Tag, nameof (ApplicationTrust), StringComparison.Ordinal) != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      this.m_appTrustedToRun = false;
      string strA1 = element.Attribute("TrustedToRun");
      if (strA1 != null && string.Compare(strA1, "true", StringComparison.Ordinal) == 0)
        this.m_appTrustedToRun = true;
      this.m_persist = false;
      string strA2 = element.Attribute("Persist");
      if (strA2 != null && string.Compare(strA2, "true", StringComparison.Ordinal) == 0)
        this.m_persist = true;
      this.m_appId = (ApplicationIdentity) null;
      string applicationIdentityFullName = element.Attribute("FullName");
      if (applicationIdentityFullName != null && applicationIdentityFullName.Length > 0)
        this.m_appId = new ApplicationIdentity(applicationIdentityFullName);
      this.m_psDefaultGrant = (PolicyStatement) null;
      this.m_grantSetSpecialFlags = 0;
      SecurityElement securityElement1 = element.SearchForChildByTag("DefaultGrant");
      if (securityElement1 != null)
      {
        SecurityElement et = securityElement1.SearchForChildByTag("PolicyStatement");
        if (et != null)
        {
          PolicyStatement policyStatement = new PolicyStatement((PermissionSet) null);
          policyStatement.FromXml(et);
          this.m_psDefaultGrant = policyStatement;
          this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(policyStatement.PermissionSet, (PermissionSet) null);
        }
      }
      List<StrongName> strongNameList = new List<StrongName>();
      SecurityElement securityElement2 = element.SearchForChildByTag("FullTrustAssemblies");
      if (securityElement2 != null && securityElement2.InternalChildren != null)
      {
        foreach (object child in securityElement2.Children)
        {
          StrongName strongName = new StrongName();
          strongName.FromXml(child as SecurityElement);
          strongNameList.Add(strongName);
        }
      }
      this.m_fullTrustAssemblies = (IList<StrongName>) strongNameList.AsReadOnly();
      this.m_elExtraInfo = element.SearchForChildByTag("ExtraInfo");
    }

    private static SecurityElement ObjectToXml(string tag, object obj)
    {
      ISecurityEncodable securityEncodable = obj as ISecurityEncodable;
      if (securityEncodable != null && !securityEncodable.ToXml().Tag.Equals(tag))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      MemoryStream memoryStream = new MemoryStream();
      new BinaryFormatter().Serialize((Stream) memoryStream, obj);
      byte[] array = memoryStream.ToArray();
      SecurityElement securityElement = new SecurityElement(tag);
      securityElement.AddAttribute("Data", Hex.EncodeHexString(array));
      return securityElement;
    }

    private static object ObjectFromXml(SecurityElement elObject)
    {
      if (elObject.Attribute("class") != null)
      {
        ISecurityEncodable codeGroup = XMLUtil.CreateCodeGroup(elObject) as ISecurityEncodable;
        if (codeGroup != null)
        {
          codeGroup.FromXml(elObject);
          return (object) codeGroup;
        }
      }
      return new BinaryFormatter().Deserialize((Stream) new MemoryStream(Hex.DecodeHexString(elObject.Attribute("Data"))));
    }

    /// <summary>
    ///   Создает новый объект, который представляет полную копию текущего экземпляра.
    /// </summary>
    /// <returns>Резервную копию этого объекта доверия приложения.</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override EvidenceBase Clone()
    {
      return base.Clone();
    }
  }
}
