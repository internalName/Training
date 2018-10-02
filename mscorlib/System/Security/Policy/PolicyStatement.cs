// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyStatement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет инструкцию <see cref="T:System.Security.Policy.CodeGroup" /> описывающую разрешения и другую информацию, применяемую к коду с определенным набором свидетельств.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PolicyStatement : ISecurityPolicyEncodable, ISecurityEncodable
  {
    internal PermissionSet m_permSet;
    [NonSerialized]
    private List<IDelayEvaluatedEvidence> m_dependentEvidence;
    internal PolicyStatementAttribute m_attributes;

    internal PolicyStatement()
    {
      this.m_permSet = (PermissionSet) null;
      this.m_attributes = PolicyStatementAttribute.Nothing;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.PolicyStatement" /> указанным значением <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="permSet">
    ///   Объект <see cref="T:System.Security.PermissionSet" /> для инициализации нового экземпляра.
    /// </param>
    public PolicyStatement(PermissionSet permSet)
      : this(permSet, PolicyStatementAttribute.Nothing)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.PolicyStatement" /> с заданным <see cref="T:System.Security.PermissionSet" /> и атрибуты.
    /// </summary>
    /// <param name="permSet">
    ///   Объект <see cref="T:System.Security.PermissionSet" /> для инициализации нового экземпляра.
    /// </param>
    /// <param name="attributes">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Policy.PolicyStatementAttribute" />.
    /// </param>
    public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
    {
      this.m_permSet = permSet != null ? permSet.Copy() : new PermissionSet(false);
      if (!PolicyStatement.ValidProperties(attributes))
        return;
      this.m_attributes = attributes;
    }

    private PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes, bool copy)
    {
      this.m_permSet = permSet == null ? new PermissionSet(false) : (!copy ? permSet : permSet.Copy());
      this.m_attributes = attributes;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Security.PermissionSet" /> инструкции политики.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.PermissionSet" /> Инструкции политики.
    /// </returns>
    public PermissionSet PermissionSet
    {
      get
      {
        lock (this)
          return this.m_permSet.Copy();
      }
      set
      {
        lock (this)
        {
          if (value == null)
            this.m_permSet = new PermissionSet(false);
          else
            this.m_permSet = value.Copy();
        }
      }
    }

    internal void SetPermissionSetNoCopy(PermissionSet permSet)
    {
      this.m_permSet = permSet;
    }

    internal PermissionSet GetPermissionSetNoCopy()
    {
      lock (this)
        return this.m_permSet;
    }

    /// <summary>Возвращает или задает атрибуты инструкции политики.</summary>
    /// <returns>Атрибуты инструкции политики.</returns>
    public PolicyStatementAttribute Attributes
    {
      get
      {
        return this.m_attributes;
      }
      set
      {
        if (!PolicyStatement.ValidProperties(value))
          return;
        this.m_attributes = value;
      }
    }

    /// <summary>
    ///   Создает эквивалентную копию текущей инструкции политики.
    /// </summary>
    /// <returns>
    ///   Новая копия <see cref="T:System.Security.Policy.PolicyStatement" /> с <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> и <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> идентичны текущего <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </returns>
    public PolicyStatement Copy()
    {
      PolicyStatement policyStatement = new PolicyStatement(this.m_permSet, this.Attributes, true);
      if (this.HasDependentEvidence)
        policyStatement.m_dependentEvidence = new List<IDelayEvaluatedEvidence>((IEnumerable<IDelayEvaluatedEvidence>) this.m_dependentEvidence);
      return policyStatement;
    }

    /// <summary>
    ///   Получает строковое представление атрибутов инструкции политики.
    /// </summary>
    /// <returns>
    ///   Текстовая строка, представляющая атрибуты инструкции политики.
    /// </returns>
    public string AttributeString
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        bool flag = true;
        if (this.GetFlag(1))
        {
          stringBuilder.Append("Exclusive");
          flag = false;
        }
        if (this.GetFlag(2))
        {
          if (!flag)
            stringBuilder.Append(" ");
          stringBuilder.Append("LevelFinal");
        }
        return stringBuilder.ToString();
      }
    }

    private static bool ValidProperties(PolicyStatementAttribute attributes)
    {
      if ((attributes & ~PolicyStatementAttribute.All) == PolicyStatementAttribute.Nothing)
        return true;
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
    }

    private bool GetFlag(int flag)
    {
      return (uint) ((PolicyStatementAttribute) flag & this.m_attributes) > 0U;
    }

    internal IEnumerable<IDelayEvaluatedEvidence> DependentEvidence
    {
      get
      {
        return (IEnumerable<IDelayEvaluatedEvidence>) this.m_dependentEvidence.AsReadOnly();
      }
    }

    internal bool HasDependentEvidence
    {
      get
      {
        if (this.m_dependentEvidence != null)
          return this.m_dependentEvidence.Count > 0;
        return false;
      }
    }

    internal void AddDependentEvidence(IDelayEvaluatedEvidence dependentEvidence)
    {
      if (this.m_dependentEvidence == null)
        this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
      this.m_dependentEvidence.Add(dependentEvidence);
    }

    internal void InplaceUnion(PolicyStatement childPolicy)
    {
      if ((this.Attributes & childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
        throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
      if (childPolicy.HasDependentEvidence && this.HasDependentEvidence | (this.m_permSet.IsSubsetOf(childPolicy.GetPermissionSetNoCopy()) && !childPolicy.GetPermissionSetNoCopy().IsSubsetOf(this.m_permSet)))
      {
        if (this.m_dependentEvidence == null)
          this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
        this.m_dependentEvidence.AddRange(childPolicy.DependentEvidence);
      }
      if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
      {
        this.m_permSet = childPolicy.GetPermissionSetNoCopy();
        this.Attributes = childPolicy.Attributes;
      }
      else
      {
        this.m_permSet.InplaceUnion(childPolicy.GetPermissionSetNoCopy());
        this.Attributes |= childPolicy.Attributes;
      }
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
    ///   Восстанавливает объект безопасности с данным состоянием из кодировки XML.
    /// </summary>
    /// <param name="et">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="et" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="et" /> Параметр не является допустимым <see cref="T:System.Security.Policy.PolicyStatement" /> кодировку.
    /// </exception>
    public void FromXml(SecurityElement et)
    {
      this.FromXml(et, (PolicyLevel) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекст для поиска <see cref="T:System.Security.NamedPermissionSet" /> значения.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml(PolicyLevel level)
    {
      return this.ToXml(level, false);
    }

    internal SecurityElement ToXml(PolicyLevel level, bool useInternal)
    {
      SecurityElement securityElement = new SecurityElement(nameof (PolicyStatement));
      securityElement.AddAttribute("version", "1");
      if (this.m_attributes != PolicyStatementAttribute.Nothing)
        securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof (PolicyStatementAttribute), (object) this.m_attributes));
      lock (this)
      {
        if (this.m_permSet != null)
        {
          if (this.m_permSet is NamedPermissionSet)
          {
            NamedPermissionSet permSet = (NamedPermissionSet) this.m_permSet;
            if (level != null && level.GetNamedPermissionSet(permSet.Name) != null)
              securityElement.AddAttribute("PermissionSetName", permSet.Name);
            else if (useInternal)
              securityElement.AddChild(permSet.InternalToXml());
            else
              securityElement.AddChild(permSet.ToXml());
          }
          else if (useInternal)
            securityElement.AddChild(this.m_permSet.InternalToXml());
          else
            securityElement.AddChild(this.m_permSet.ToXml());
        }
      }
      return securityElement;
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с данным состоянием из кодировки XML.
    /// </summary>
    /// <param name="et">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекст для поиска <see cref="T:System.Security.NamedPermissionSet" /> значения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="et" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="et" /> Параметр не является допустимым <see cref="T:System.Security.Policy.PolicyStatement" /> кодировку.
    /// </exception>
    [SecuritySafeCritical]
    public void FromXml(SecurityElement et, PolicyLevel level)
    {
      this.FromXml(et, level, false);
    }

    [SecurityCritical]
    internal void FromXml(SecurityElement et, PolicyLevel level, bool allowInternalOnly)
    {
      if (et == null)
        throw new ArgumentNullException(nameof (et));
      if (!et.Tag.Equals(nameof (PolicyStatement)))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) nameof (PolicyStatement), (object) this.GetType().FullName));
      this.m_attributes = PolicyStatementAttribute.Nothing;
      string str1 = et.Attribute("Attributes");
      if (str1 != null)
        this.m_attributes = (PolicyStatementAttribute) Enum.Parse(typeof (PolicyStatementAttribute), str1);
      lock (this)
      {
        this.m_permSet = (PermissionSet) null;
        if (level != null)
        {
          string name = et.Attribute("PermissionSetName");
          if (name != null)
          {
            this.m_permSet = (PermissionSet) level.GetNamedPermissionSetInternal(name);
            if (this.m_permSet == null)
              this.m_permSet = new PermissionSet(PermissionState.None);
          }
        }
        if (this.m_permSet == null)
        {
          SecurityElement et1 = et.SearchForChildByTag("PermissionSet");
          if (et1 == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
          string str2 = et1.Attribute("class");
          this.m_permSet = str2 == null || !str2.Equals("NamedPermissionSet") && !str2.Equals("System.Security.NamedPermissionSet") ? new PermissionSet(PermissionState.None) : (PermissionSet) new NamedPermissionSet("DefaultName", PermissionState.None);
          try
          {
            this.m_permSet.FromXml(et1, allowInternalOnly, true);
          }
          catch
          {
          }
        }
        if (this.m_permSet != null)
          return;
        this.m_permSet = new PermissionSet(PermissionState.None);
      }
    }

    [SecurityCritical]
    internal void FromXml(SecurityDocument doc, int position, PolicyLevel level, bool allowInternalOnly)
    {
      if (doc == null)
        throw new ArgumentNullException(nameof (doc));
      if (!doc.GetTagForElement(position).Equals(nameof (PolicyStatement)))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) nameof (PolicyStatement), (object) this.GetType().FullName));
      this.m_attributes = PolicyStatementAttribute.Nothing;
      string attributeForElement1 = doc.GetAttributeForElement(position, "Attributes");
      if (attributeForElement1 != null)
        this.m_attributes = (PolicyStatementAttribute) Enum.Parse(typeof (PolicyStatementAttribute), attributeForElement1);
      lock (this)
      {
        this.m_permSet = (PermissionSet) null;
        if (level != null)
        {
          string attributeForElement2 = doc.GetAttributeForElement(position, "PermissionSetName");
          if (attributeForElement2 != null)
          {
            this.m_permSet = (PermissionSet) level.GetNamedPermissionSetInternal(attributeForElement2);
            if (this.m_permSet == null)
              this.m_permSet = new PermissionSet(PermissionState.None);
          }
        }
        if (this.m_permSet == null)
        {
          ArrayList positionForElement = doc.GetChildrenPositionForElement(position);
          int position1 = -1;
          for (int index = 0; index < positionForElement.Count; ++index)
          {
            if (doc.GetTagForElement((int) positionForElement[index]).Equals("PermissionSet"))
              position1 = (int) positionForElement[index];
          }
          if (position1 == -1)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
          string attributeForElement2 = doc.GetAttributeForElement(position1, "class");
          this.m_permSet = attributeForElement2 == null || !attributeForElement2.Equals("NamedPermissionSet") && !attributeForElement2.Equals("System.Security.NamedPermissionSet") ? new PermissionSet(PermissionState.None) : (PermissionSet) new NamedPermissionSet("DefaultName", PermissionState.None);
          this.m_permSet.FromXml(doc, position1, allowInternalOnly);
        }
        if (this.m_permSet != null)
          return;
        this.m_permSet = new PermissionSet(PermissionState.None);
      }
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.Policy.PolicyStatement" /> текущему объекту <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Security.Policy.PolicyStatement" />, который требуется сравнить с текущим объектом <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Security.Policy.PolicyStatement" /> равен текущему объекту <see cref="T:System.Security.Policy.PolicyStatement" />; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      PolicyStatement policyStatement = obj as PolicyStatement;
      return policyStatement != null && this.m_attributes == policyStatement.m_attributes && object.Equals((object) this.m_permSet, (object) policyStatement.m_permSet);
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.Policy.PolicyStatement" />, который можно использовать в алгоритмах хэширования и структурах данных, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int attributes = (int) this.m_attributes;
      if (this.m_permSet != null)
        attributes ^= this.m_permSet.GetHashCode();
      return attributes;
    }
  }
}
