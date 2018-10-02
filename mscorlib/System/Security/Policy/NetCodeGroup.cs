// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.NetCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет веб-полномочие на сайт, откуда была загружена сборка.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class NetCodeGroup : CodeGroup, IUnionSemanticCodeGroup
  {
    private static readonly char[] c_SomeRegexChars = new char[12]
    {
      '.',
      '-',
      '+',
      '[',
      ']',
      '{',
      '$',
      '^',
      '#',
      ')',
      '(',
      ' '
    };
    /// <summary>
    ///   Содержит значение, используемое для задания любой другой схеме не указан источник.
    /// </summary>
    public static readonly string AnyOtherOriginScheme = CodeConnectAccess.AnyScheme;
    /// <summary>
    ///   Содержит значение, используемое для задания доступа для подключения для кода с неизвестного или нераспознанной схемой источника.
    /// </summary>
    public static readonly string AbsentOriginScheme = string.Empty;
    [OptionalField(VersionAdded = 2)]
    private ArrayList m_schemesList;
    [OptionalField(VersionAdded = 2)]
    private ArrayList m_accessList;
    private const string c_IgnoreUserInfo = "";
    private const string c_AnyScheme = "([0-9a-z+\\-\\.]+)://";

    [SecurityCritical]
    [Conditional("_DEBUG")]
    private static void DEBUG_OUT(string str)
    {
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_schemesList = (ArrayList) null;
      this.m_accessList = (ArrayList) null;
    }

    internal NetCodeGroup()
    {
      this.SetDefaults();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.NetCodeGroup" />.
    /// </summary>
    /// <param name="membershipCondition">
    ///   Условие членства, проверяющее свидетельство для определения, применяет ли эта группа кода политику разграничения доступа кода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="membershipCondition" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="membershipCondition" /> параметр не является допустимым.
    /// </exception>
    public NetCodeGroup(IMembershipCondition membershipCondition)
      : base(membershipCondition, (PolicyStatement) null)
    {
      this.SetDefaults();
    }

    /// <summary>
    ///   Удаляет все сведения о подключении доступа для текущей группы кода.
    /// </summary>
    public void ResetConnectAccess()
    {
      this.m_schemesList = (ArrayList) null;
      this.m_accessList = (ArrayList) null;
    }

    /// <summary>
    ///   Добавляет указанное соединение доступ к текущей группы кода.
    /// </summary>
    /// <param name="originScheme">
    ///   A <see cref="T:System.String" /> содержащий схему для сравнения со схемой кода.
    /// </param>
    /// <param name="connectAccess">
    ///   Объект <see cref="T:System.Security.Policy.CodeConnectAccess" /> указывающий код схемы и порта можно использовать для подключения к своему исходному серверу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="originScheme" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="originScheme" /> содержит символы, которые не разрешены в схемах.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="originScheme" /> = <see cref="F:System.Security.Policy.NetCodeGroup.AbsentOriginScheme" /> и <paramref name="connectAccess" /> указывает <see cref="F:System.Security.Policy.CodeConnectAccess.OriginScheme" /> в качестве схемы.
    /// </exception>
    public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
    {
      if (originScheme == null)
        throw new ArgumentNullException(nameof (originScheme));
      if (originScheme != NetCodeGroup.AbsentOriginScheme && originScheme != NetCodeGroup.AnyOtherOriginScheme && !CodeConnectAccess.IsValidScheme(originScheme))
        throw new ArgumentOutOfRangeException(nameof (originScheme));
      if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.IsOriginScheme)
        throw new ArgumentOutOfRangeException(nameof (connectAccess));
      if (this.m_schemesList == null)
      {
        this.m_schemesList = new ArrayList();
        this.m_accessList = new ArrayList();
      }
      originScheme = originScheme.ToLower(CultureInfo.InvariantCulture);
      for (int index1 = 0; index1 < this.m_schemesList.Count; ++index1)
      {
        if ((string) this.m_schemesList[index1] == originScheme)
        {
          if (connectAccess == null)
            return;
          ArrayList access = (ArrayList) this.m_accessList[index1];
          for (int index2 = 0; index2 < access.Count; ++index2)
          {
            if (((CodeConnectAccess) access[index2]).Equals((object) connectAccess))
              return;
          }
          access.Add((object) connectAccess);
          return;
        }
      }
      this.m_schemesList.Add((object) originScheme);
      ArrayList arrayList = new ArrayList();
      this.m_accessList.Add((object) arrayList);
      if (connectAccess == null)
        return;
      arrayList.Add((object) connectAccess);
    }

    /// <summary>
    ///   Получает информацию доступа для подключения для текущей группы кода.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.DictionaryEntry" /> массив, содержащий сведения о подключении доступа.
    /// </returns>
    public DictionaryEntry[] GetConnectAccessRules()
    {
      if (this.m_schemesList == null)
        return (DictionaryEntry[]) null;
      DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[this.m_schemesList.Count];
      for (int index = 0; index < dictionaryEntryArray.Length; ++index)
      {
        dictionaryEntryArray[index].Key = this.m_schemesList[index];
        dictionaryEntryArray[index].Value = (object) ((ArrayList) this.m_accessList[index]).ToArray(typeof (CodeConnectAccess));
      }
      return dictionaryEntryArray;
    }

    /// <summary>
    ///   Обрабатывает политику для группы кода и ее дочерних элементов, используя набор свидетельств.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Для сборки.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Policy.PolicyStatement" /> состоящий из разрешений, предоставляемых группой кода, с дополнительными атрибутами, или <see langword="null" /> если группа кода не применяется (условие членства не соответствует указанному свидетельству).
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Более одной группы кода (включая родительские и дочерние группы кода) помечена <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.
    /// </exception>
    [SecuritySafeCritical]
    public override PolicyStatement Resolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      object usedEvidence = (object) null;
      if (!PolicyManager.CheckMembershipCondition(this.MembershipCondition, evidence, out usedEvidence))
        return (PolicyStatement) null;
      PolicyStatement assemblyPolicy = this.CalculateAssemblyPolicy(evidence);
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      if (dependentEvidence != null && !dependentEvidence.IsVerified)
        assemblyPolicy.AddDependentEvidence(dependentEvidence);
      bool flag = false;
      IEnumerator enumerator = this.Children.GetEnumerator();
      while (enumerator.MoveNext() && !flag)
      {
        PolicyStatement childPolicy = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
        if (childPolicy != null)
        {
          assemblyPolicy.InplaceUnion(childPolicy);
          if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
            flag = true;
        }
      }
      return assemblyPolicy;
    }

    PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      if (this.MembershipCondition.Check(evidence))
        return this.CalculateAssemblyPolicy(evidence);
      return (PolicyStatement) null;
    }

    /// <summary>Обрабатывает согласующиеся группы кода.</summary>
    /// <param name="evidence">Свидетельство для сборки.</param>
    /// <returns>
    ///   Полный набор групп кода, которые были сопоставлены свидетельство.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      if (!this.MembershipCondition.Check(evidence))
        return (CodeGroup) null;
      CodeGroup codeGroup = this.Copy();
      codeGroup.Children = (IList) new ArrayList();
      foreach (CodeGroup child in (IEnumerable) this.Children)
      {
        CodeGroup group = child.ResolveMatchingCodeGroups(evidence);
        if (group != null)
          codeGroup.AddChild(group);
      }
      return codeGroup;
    }

    private string EscapeStringForRegex(string str)
    {
      int startIndex = 0;
      StringBuilder stringBuilder = (StringBuilder) null;
      int index;
      for (; startIndex < str.Length && (index = str.IndexOfAny(NetCodeGroup.c_SomeRegexChars, startIndex)) != -1; startIndex = index + 1)
      {
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(str.Length * 2);
        stringBuilder.Append(str, startIndex, index - startIndex).Append('\\').Append(str[index]);
      }
      if (stringBuilder == null)
        return str;
      if (startIndex < str.Length)
        stringBuilder.Append(str, startIndex, str.Length - startIndex);
      return stringBuilder.ToString();
    }

    internal SecurityElement CreateWebPermission(string host, string scheme, string port, string assemblyOverride)
    {
      if (scheme == null)
        scheme = string.Empty;
      if (host == null || host.Length == 0)
        return (SecurityElement) null;
      host = host.ToLower(CultureInfo.InvariantCulture);
      scheme = scheme.ToLower(CultureInfo.InvariantCulture);
      int intPort = -1;
      if (port != null && port.Length != 0)
        intPort = int.Parse(port, (IFormatProvider) CultureInfo.InvariantCulture);
      else
        port = string.Empty;
      CodeConnectAccess[] accessRulesForScheme = this.FindAccessRulesForScheme(scheme);
      if (accessRulesForScheme == null || accessRulesForScheme.Length == 0)
        return (SecurityElement) null;
      SecurityElement securityElement = new SecurityElement("IPermission");
      string str1 = assemblyOverride == null ? "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" : assemblyOverride;
      securityElement.AddAttribute("class", "System.Net.WebPermission, " + str1);
      securityElement.AddAttribute("version", "1");
      SecurityElement child1 = new SecurityElement("ConnectAccess");
      host = this.EscapeStringForRegex(host);
      scheme = this.EscapeStringForRegex(scheme);
      string str2 = this.TryPermissionAsOneString(accessRulesForScheme, scheme, host, intPort);
      if (str2 != null)
      {
        SecurityElement child2 = new SecurityElement("URI");
        child2.AddAttribute("uri", str2);
        child1.AddChild(child2);
      }
      else
      {
        if (port.Length != 0)
          port = ":" + port;
        for (int index = 0; index < accessRulesForScheme.Length; ++index)
        {
          string accessElementString = this.GetPermissionAccessElementString(accessRulesForScheme[index], scheme, host, port);
          SecurityElement child2 = new SecurityElement("URI");
          child2.AddAttribute("uri", accessElementString);
          child1.AddChild(child2);
        }
      }
      securityElement.AddChild(child1);
      return securityElement;
    }

    private CodeConnectAccess[] FindAccessRulesForScheme(string lowerCaseScheme)
    {
      if (this.m_schemesList == null)
        return (CodeConnectAccess[]) null;
      int index = this.m_schemesList.IndexOf((object) lowerCaseScheme);
      if (index == -1 && (lowerCaseScheme == NetCodeGroup.AbsentOriginScheme || (index = this.m_schemesList.IndexOf((object) NetCodeGroup.AnyOtherOriginScheme)) == -1))
        return (CodeConnectAccess[]) null;
      return (CodeConnectAccess[]) ((ArrayList) this.m_accessList[index]).ToArray(typeof (CodeConnectAccess));
    }

    private string TryPermissionAsOneString(CodeConnectAccess[] access, string escapedScheme, string escapedHost, int intPort)
    {
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = false;
      int num = -2;
      for (int index = 0; index < access.Length; ++index)
      {
        flag1 = ((flag1 ? 1 : 0) & (access[index].IsDefaultPort ? 1 : (!access[index].IsOriginPort ? 0 : (intPort == -1 ? 1 : 0)))) != 0;
        flag2 = ((flag2 ? 1 : 0) & (access[index].IsOriginPort ? 1 : (access[index].Port == intPort ? 1 : 0))) != 0;
        if (access[index].Port >= 0)
        {
          if (num == -2)
            num = access[index].Port;
          else if (access[index].Port != num)
            num = -1;
        }
        else
          num = -1;
        if (access[index].IsAnyScheme)
          flag3 = true;
      }
      if (!flag1 && !flag2 && num == -1)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * access.Length + "".Length * 2 + escapedHost.Length);
      if (flag3)
      {
        stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
      }
      else
      {
        stringBuilder.Append('(');
        for (int index1 = 0; index1 < access.Length; ++index1)
        {
          int index2 = 0;
          while (index2 < index1 && !(access[index1].Scheme == access[index2].Scheme))
            ++index2;
          if (index2 == index1)
          {
            if (index1 != 0)
              stringBuilder.Append('|');
            stringBuilder.Append(access[index1].IsOriginScheme ? escapedScheme : this.EscapeStringForRegex(access[index1].Scheme));
          }
        }
        stringBuilder.Append(")://");
      }
      stringBuilder.Append("").Append(escapedHost);
      if (!flag1)
      {
        if (flag2)
          stringBuilder.Append(':').Append(intPort);
        else
          stringBuilder.Append(':').Append(num);
      }
      stringBuilder.Append("/.*");
      return stringBuilder.ToString();
    }

    private string GetPermissionAccessElementString(CodeConnectAccess access, string escapedScheme, string escapedHost, string strPort)
    {
      StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * 2 + "".Length + escapedHost.Length);
      if (access.IsAnyScheme)
        stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
      else if (access.IsOriginScheme)
        stringBuilder.Append(escapedScheme).Append("://");
      else
        stringBuilder.Append(this.EscapeStringForRegex(access.Scheme)).Append("://");
      stringBuilder.Append("").Append(escapedHost);
      if (!access.IsDefaultPort)
      {
        if (access.IsOriginPort)
          stringBuilder.Append(strPort);
        else
          stringBuilder.Append(':').Append(access.StrPort);
      }
      stringBuilder.Append("/.*");
      return stringBuilder.ToString();
    }

    internal PolicyStatement CalculatePolicy(string host, string scheme, string port)
    {
      SecurityElement webPermission = this.CreateWebPermission(host, scheme, port, (string) null);
      SecurityElement et = new SecurityElement("PolicyStatement");
      SecurityElement child = new SecurityElement("PermissionSet");
      child.AddAttribute("class", "System.Security.PermissionSet");
      child.AddAttribute("version", "1");
      if (webPermission != null)
        child.AddChild(webPermission);
      et.AddChild(child);
      PolicyStatement policyStatement = new PolicyStatement();
      policyStatement.FromXml(et);
      return policyStatement;
    }

    private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
    {
      PolicyStatement policyStatement = (PolicyStatement) null;
      Url hostEvidence1 = evidence.GetHostEvidence<Url>();
      if (hostEvidence1 != null)
        policyStatement = this.CalculatePolicy(hostEvidence1.GetURLString().Host, hostEvidence1.GetURLString().Scheme, hostEvidence1.GetURLString().Port);
      if (policyStatement == null)
      {
        Site hostEvidence2 = evidence.GetHostEvidence<Site>();
        if (hostEvidence2 != null)
          policyStatement = this.CalculatePolicy(hostEvidence2.Name, (string) null, (string) null);
      }
      if (policyStatement == null)
        policyStatement = new PolicyStatement(new PermissionSet(false), PolicyStatementAttribute.Nothing);
      return policyStatement;
    }

    /// <summary>Создает глубокую копию текущей группы кода.</summary>
    /// <returns>
    ///   Эквивалентная копия текущей группы кода, включающая условия членства и дочерние группы кода.
    /// </returns>
    public override CodeGroup Copy()
    {
      NetCodeGroup netCodeGroup = new NetCodeGroup(this.MembershipCondition);
      netCodeGroup.Name = this.Name;
      netCodeGroup.Description = this.Description;
      if (this.m_schemesList != null)
      {
        netCodeGroup.m_schemesList = (ArrayList) this.m_schemesList.Clone();
        netCodeGroup.m_accessList = new ArrayList(this.m_accessList.Count);
        for (int index = 0; index < this.m_accessList.Count; ++index)
          netCodeGroup.m_accessList.Add(((ArrayList) this.m_accessList[index]).Clone());
      }
      foreach (CodeGroup child in (IEnumerable) this.Children)
        netCodeGroup.AddChild(child);
      return (CodeGroup) netCodeGroup;
    }

    /// <summary>
    ///   Возвращает алгоритм, используемый для объединенных групп.
    /// </summary>
    /// <returns>Строка «Объединение».</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_Union");
      }
    }

    /// <summary>
    ///   Возвращает имя <see cref="T:System.Security.NamedPermissionSet" /> для группы кода.
    /// </summary>
    /// <returns>Всегда строка «Же веб-узел.»</returns>
    public override string PermissionSetName
    {
      get
      {
        return Environment.GetResourceString("NetCodeGroup_PermissionSet");
      }
    }

    /// <summary>
    ///   Возвращает строковое представление атрибутов инструкции политики для группы кода.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    public override string AttributeString
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>
    ///   Определяет, эквивалентен ли указанная группа кода текущей группы кода.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Policy.NetCodeGroup" /> Объект для сравнения с текущей группы кода.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанная группа кода эквивалентна текущей группы кода; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      if (this == o)
        return true;
      NetCodeGroup netCodeGroup = o as NetCodeGroup;
      if (netCodeGroup == null || !base.Equals((object) netCodeGroup) || this.m_schemesList == null != (netCodeGroup.m_schemesList == null))
        return false;
      if (this.m_schemesList == null)
        return true;
      if (this.m_schemesList.Count != netCodeGroup.m_schemesList.Count)
        return false;
      for (int index1 = 0; index1 < this.m_schemesList.Count; ++index1)
      {
        int index2 = netCodeGroup.m_schemesList.IndexOf(this.m_schemesList[index1]);
        if (index2 == -1)
          return false;
        ArrayList access1 = (ArrayList) this.m_accessList[index1];
        ArrayList access2 = (ArrayList) netCodeGroup.m_accessList[index2];
        if (access1.Count != access2.Count)
          return false;
        for (int index3 = 0; index3 < access1.Count; ++index3)
        {
          if (!access2.Contains(access1[index3]))
            return false;
        }
      }
      return true;
    }

    /// <summary>Получает хэш-код текущей группы кода.</summary>
    /// <returns>Хэш-код текущей группы кода.</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode() + this.GetRulesHashCode();
    }

    private int GetRulesHashCode()
    {
      if (this.m_schemesList == null)
        return 0;
      int num = 0;
      for (int index = 0; index < this.m_schemesList.Count; ++index)
        num += ((string) this.m_schemesList[index]).GetHashCode();
      foreach (ArrayList access in this.m_accessList)
      {
        for (int index = 0; index < access.Count; ++index)
          num += ((CodeConnectAccess) access[index]).GetHashCode();
      }
      return num;
    }

    protected override void CreateXml(SecurityElement element, PolicyLevel level)
    {
      DictionaryEntry[] connectAccessRules = this.GetConnectAccessRules();
      if (connectAccessRules == null)
        return;
      SecurityElement child1 = new SecurityElement("connectAccessRules");
      foreach (DictionaryEntry dictionaryEntry in connectAccessRules)
      {
        SecurityElement child2 = new SecurityElement("codeOrigin");
        child2.AddAttribute("scheme", (string) dictionaryEntry.Key);
        foreach (CodeConnectAccess codeConnectAccess in (CodeConnectAccess[]) dictionaryEntry.Value)
        {
          SecurityElement child3 = new SecurityElement("connectAccess");
          child3.AddAttribute("scheme", codeConnectAccess.Scheme);
          child3.AddAttribute("port", codeConnectAccess.StrPort);
          child2.AddChild(child3);
        }
        child1.AddChild(child2);
      }
      element.AddChild(child1);
    }

    protected override void ParseXml(SecurityElement e, PolicyLevel level)
    {
      this.ResetConnectAccess();
      SecurityElement securityElement = e.SearchForChildByTag("connectAccessRules");
      if (securityElement == null || securityElement.Children == null)
      {
        this.SetDefaults();
      }
      else
      {
        foreach (SecurityElement child1 in securityElement.Children)
        {
          if (child1.Tag.Equals("codeOrigin"))
          {
            string originScheme = child1.Attribute("scheme");
            bool flag = false;
            if (child1.Children != null)
            {
              foreach (SecurityElement child2 in child1.Children)
              {
                if (child2.Tag.Equals("connectAccess"))
                {
                  string allowScheme = child2.Attribute("scheme");
                  string allowPort = child2.Attribute("port");
                  this.AddConnectAccess(originScheme, new CodeConnectAccess(allowScheme, allowPort));
                  flag = true;
                }
              }
            }
            if (!flag)
              this.AddConnectAccess(originScheme, (CodeConnectAccess) null);
          }
        }
      }
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.NetCodeGroup";
    }

    private void SetDefaults()
    {
      this.AddConnectAccess("file", (CodeConnectAccess) null);
      this.AddConnectAccess("http", new CodeConnectAccess("http", CodeConnectAccess.OriginPort));
      this.AddConnectAccess("http", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
      this.AddConnectAccess("https", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
      this.AddConnectAccess(NetCodeGroup.AbsentOriginScheme, CodeConnectAccess.CreateAnySchemeAccess(CodeConnectAccess.OriginPort));
      this.AddConnectAccess(NetCodeGroup.AnyOtherOriginScheme, CodeConnectAccess.CreateOriginSchemeAccess(CodeConnectAccess.OriginPort));
    }
  }
}
