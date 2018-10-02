// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, из которого создаются все реализации кода группы.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class CodeGroup
  {
    private IMembershipCondition m_membershipCondition;
    private IList m_children;
    private PolicyStatement m_policy;
    private SecurityElement m_element;
    private PolicyLevel m_parentLevel;
    private string m_name;
    private string m_description;

    internal CodeGroup()
    {
    }

    internal CodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
    {
      this.m_membershipCondition = membershipCondition;
      this.m_policy = new PolicyStatement();
      this.m_policy.SetPermissionSetNoCopy(permSet);
      this.m_children = (IList) ArrayList.Synchronized(new ArrayList());
      this.m_element = (SecurityElement) null;
      this.m_parentLevel = (PolicyLevel) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.CodeGroup" />.
    /// </summary>
    /// <param name="membershipCondition">
    ///   Условие членства, проверяющее свидетельство для определения, применяет ли эта группа кода политику.
    /// </param>
    /// <param name="policy">
    ///   Инструкция политики для группы кода в форме набора разрешений и атрибутов для доступа к коду, удовлетворяющему условию членства.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="membershipCondition" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="membershipCondition" /> параметр не является допустимым.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="policy" /> параметр не является допустимым.
    /// </exception>
    protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
    {
      if (membershipCondition == null)
        throw new ArgumentNullException(nameof (membershipCondition));
      this.m_policy = policy != null ? policy.Copy() : (PolicyStatement) null;
      this.m_membershipCondition = membershipCondition.Copy();
      this.m_children = (IList) ArrayList.Synchronized(new ArrayList());
      this.m_element = (SecurityElement) null;
      this.m_parentLevel = (PolicyLevel) null;
    }

    /// <summary>
    ///   Добавляет дочернюю группу кода для текущей группы кода.
    /// </summary>
    /// <param name="group">
    ///   Группы кода для добавления в качестве дочернего.
    ///    Эта новая группа кода дочерних добавляется в конец списка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="group" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="group" /> Параметр группа не является допустимым кодом.
    /// </exception>
    [SecuritySafeCritical]
    public void AddChild(CodeGroup group)
    {
      if (group == null)
        throw new ArgumentNullException(nameof (group));
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
        this.m_children.Add((object) group.Copy());
    }

    [SecurityCritical]
    internal void AddChildInternal(CodeGroup group)
    {
      if (group == null)
        throw new ArgumentNullException(nameof (group));
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
        this.m_children.Add((object) group);
    }

    /// <summary>Удаляет указанную дочернюю группу кода.</summary>
    /// <param name="group">
    ///   Группы кода должны быть удалены в дочерний.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="group" /> Параметр не является непосредственной дочерней группой кода текущей группы кода.
    /// </exception>
    [SecuritySafeCritical]
    public void RemoveChild(CodeGroup group)
    {
      if (group == null)
        return;
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
      {
        int index = this.m_children.IndexOf((object) group);
        if (index == -1)
          return;
        this.m_children.RemoveAt(index);
      }
    }

    /// <summary>
    ///   Возвращает или задает упорядоченный список дочерних групп кода группы кода.
    /// </summary>
    /// <returns>Список дочерних групп кода.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка присвоить этому свойству значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать это свойство в список дочерних элементов, которые не являются <see cref="T:System.Security.Policy.CodeGroup" /> объектов.
    /// </exception>
    public IList Children
    {
      [SecuritySafeCritical] get
      {
        if (this.m_children == null)
          this.ParseChildren();
        lock (this)
        {
          IList list = (IList) new ArrayList(this.m_children.Count);
          foreach (CodeGroup child in (IEnumerable) this.m_children)
            list.Add((object) child.Copy());
          return list;
        }
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (Children));
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(value.Count));
        foreach (object obj in (IEnumerable) value)
        {
          CodeGroup codeGroup = obj as CodeGroup;
          if (codeGroup == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_CodeGroupChildrenMustBeCodeGroups"));
          arrayList.Add((object) codeGroup.Copy());
        }
        this.m_children = (IList) arrayList;
      }
    }

    [SecurityCritical]
    internal IList GetChildrenInternal()
    {
      if (this.m_children == null)
        this.ParseChildren();
      return this.m_children;
    }

    /// <summary>
    ///   Возвращает или задает условие членства в группе кода.
    /// </summary>
    /// <returns>
    ///   Применяется условие членства, определяющее, к какому свидетельству группы кода.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка задать этому параметру <see langword="null" />.
    /// </exception>
    public IMembershipCondition MembershipCondition
    {
      [SecuritySafeCritical] get
      {
        if (this.m_membershipCondition == null && this.m_element != null)
          this.ParseMembershipCondition();
        return this.m_membershipCondition.Copy();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (MembershipCondition));
        this.m_membershipCondition = value.Copy();
      }
    }

    /// <summary>
    ///   Возвращает или задает инструкцию политики, связанные с группой кода.
    /// </summary>
    /// <returns>Инструкция политики для группы кода.</returns>
    public PolicyStatement PolicyStatement
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy != null)
          return this.m_policy.Copy();
        return (PolicyStatement) null;
      }
      set
      {
        if (value != null)
          this.m_policy = value.Copy();
        else
          this.m_policy = (PolicyStatement) null;
      }
    }

    /// <summary>Возвращает или задает имя группы кода.</summary>
    /// <returns>Имя группы кода.</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>Возвращает или задает описание группы кода.</summary>
    /// <returns>Описание группы кода.</returns>
    public string Description
    {
      get
      {
        return this.m_description;
      }
      set
      {
        this.m_description = value;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе, разрешает политики для группы кода и ее дочерних элементов, используя набор свидетельств.
    /// </summary>
    /// <param name="evidence">Свидетельство для сборки.</param>
    /// <returns>
    ///   Инструкция политики, состоящая из разрешений, предоставляемых группой кода, с дополнительными атрибутами, или <see langword="null" /> если группа кода не применяется (условие членства не соответствует указанному свидетельству).
    /// </returns>
    public abstract PolicyStatement Resolve(Evidence evidence);

    /// <summary>
    ///   При переопределении в производном классе обрабатывает соответствующие группы кода.
    /// </summary>
    /// <param name="evidence">Свидетельство для сборки.</param>
    /// <returns>
    ///   A <see cref="T:System.Security.Policy.CodeGroup" /> это корень дерева соответствующих групп кода.
    /// </returns>
    public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

    /// <summary>
    ///   При переопределении в производном классе создает глубокую копию текущей группы кода.
    /// </summary>
    /// <returns>
    ///   Эквивалентная копия текущей группы кода, включающая условия членства и дочерние группы кода.
    /// </returns>
    public abstract CodeGroup Copy();

    /// <summary>
    ///   Получает имя именованного набора разрешений для группы кода.
    /// </summary>
    /// <returns>Имя именованного набора разрешений уровня политики.</returns>
    public virtual string PermissionSetName
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy == null)
          return (string) null;
        return (this.m_policy.GetPermissionSetNoCopy() as NamedPermissionSet)?.Name;
      }
    }

    /// <summary>
    ///   Возвращает строковое представление атрибутов инструкции политики для группы кода.
    /// </summary>
    /// <returns>
    ///   Строковое представление атрибутов инструкции политики для группы кода.
    /// </returns>
    public virtual string AttributeString
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy != null)
          return this.m_policy.AttributeString;
        return (string) null;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает объединенную логику для группы кода.
    /// </summary>
    /// <returns>Описание объединенной логики для группы кода.</returns>
    public abstract string MergeLogic { get; }

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
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности, его текущего состояния и уровня политики, в которой существует код.
    /// </summary>
    /// <param name="level">
    ///   Уровень политики, в котором существует группа кода.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    [SecuritySafeCritical]
    public SecurityElement ToXml(PolicyLevel level)
    {
      return this.ToXml(level, this.GetTypeName());
    }

    internal virtual string GetTypeName()
    {
      return this.GetType().FullName;
    }

    [SecurityCritical]
    internal SecurityElement ToXml(PolicyLevel level, string policyClassName)
    {
      if (this.m_membershipCondition == null && this.m_element != null)
        this.ParseMembershipCondition();
      if (this.m_children == null)
        this.ParseChildren();
      if (this.m_policy == null && this.m_element != null)
        this.ParsePolicy();
      SecurityElement element = new SecurityElement(nameof (CodeGroup));
      XMLUtil.AddClassAttribute(element, this.GetType(), policyClassName);
      element.AddAttribute("version", "1");
      element.AddChild(this.m_membershipCondition.ToXml(level));
      if (this.m_policy != null)
      {
        PermissionSet permissionSetNoCopy = this.m_policy.GetPermissionSetNoCopy();
        NamedPermissionSet namedPermissionSet = permissionSetNoCopy as NamedPermissionSet;
        if (namedPermissionSet != null && level != null && level.GetNamedPermissionSetInternal(namedPermissionSet.Name) != null)
          element.AddAttribute("PermissionSetName", namedPermissionSet.Name);
        else if (!permissionSetNoCopy.IsEmpty())
          element.AddChild(permissionSetNoCopy.ToXml());
        if (this.m_policy.Attributes != PolicyStatementAttribute.Nothing)
          element.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof (PolicyStatementAttribute), (object) this.m_policy.Attributes));
      }
      if (this.m_children.Count > 0)
      {
        lock (this)
        {
          foreach (CodeGroup child in (IEnumerable) this.m_children)
            element.AddChild(child.ToXml(level));
        }
      }
      if (this.m_name != null)
        element.AddAttribute("Name", SecurityElement.Escape(this.m_name));
      if (this.m_description != null)
        element.AddAttribute("Description", SecurityElement.Escape(this.m_description));
      this.CreateXml(element, level);
      return element;
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет сериализацию свойств и внутреннего состояния, специфичные для группы кода и добавляет в указанный сериализации <see cref="T:System.Security.SecurityElement" />.
    /// </summary>
    /// <param name="element">
    ///   Кодировка XML, к которой добавляется сериализация.
    /// </param>
    /// <param name="level">
    ///   Уровень политики, в котором существует группа кода.
    /// </param>
    protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
    {
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с заданным состоянием и уровнем политики из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   Уровень политики, в котором существует группа кода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      lock (this)
      {
        this.m_element = e;
        this.m_parentLevel = level;
        this.m_children = (IList) null;
        this.m_membershipCondition = (IMembershipCondition) null;
        this.m_policy = (PolicyStatement) null;
        this.m_name = e.Attribute("Name");
        this.m_description = e.Attribute("Description");
        this.ParseXml(e, level);
      }
    }

    /// <summary>
    ///   При переопределении в производном классе восстанавливает свойства и внутреннее состояние определенного для производной группы кода из указанного <see cref="T:System.Security.SecurityElement" />.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   Уровень политики, в котором существует группа кода.
    /// </param>
    protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
    {
    }

    [SecurityCritical]
    private bool ParseMembershipCondition(bool safeLoad)
    {
      lock (this)
      {
        SecurityElement securityElement = this.m_element.SearchForChildByTag("IMembershipCondition");
        if (securityElement == null)
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "IMembershipCondition", (object) this.GetType().FullName));
        IMembershipCondition membershipCondition;
        try
        {
          membershipCondition = XMLUtil.CreateMembershipCondition(securityElement);
          if (membershipCondition == null)
            return false;
        }
        catch (Exception ex)
        {
          throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"), ex);
        }
        membershipCondition.FromXml(securityElement, this.m_parentLevel);
        this.m_membershipCondition = membershipCondition;
        return true;
      }
    }

    [SecurityCritical]
    private void ParseMembershipCondition()
    {
      this.ParseMembershipCondition(false);
    }

    [SecurityCritical]
    internal void ParseChildren()
    {
      lock (this)
      {
        ArrayList arrayList1 = ArrayList.Synchronized(new ArrayList());
        if (this.m_element != null && this.m_element.InternalChildren != null)
        {
          this.m_element.Children = (ArrayList) this.m_element.InternalChildren.Clone();
          ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList());
          Evidence evidence = new Evidence();
          int count = this.m_element.InternalChildren.Count;
          int index = 0;
          while (index < count)
          {
            SecurityElement child = (SecurityElement) this.m_element.Children[index];
            if (child.Tag.Equals(nameof (CodeGroup)))
            {
              CodeGroup codeGroup = XMLUtil.CreateCodeGroup(child);
              if (codeGroup != null)
              {
                codeGroup.FromXml(child, this.m_parentLevel);
                if (this.ParseMembershipCondition(true))
                {
                  codeGroup.Resolve(evidence);
                  codeGroup.MembershipCondition.Check(evidence);
                  arrayList1.Add((object) codeGroup);
                  ++index;
                }
                else
                {
                  this.m_element.InternalChildren.RemoveAt(index);
                  count = this.m_element.InternalChildren.Count;
                  arrayList2.Add((object) new CodeGroupPositionMarker(index, arrayList1.Count, child));
                }
              }
              else
              {
                this.m_element.InternalChildren.RemoveAt(index);
                count = this.m_element.InternalChildren.Count;
                arrayList2.Add((object) new CodeGroupPositionMarker(index, arrayList1.Count, child));
              }
            }
            else
              ++index;
          }
          foreach (CodeGroupPositionMarker groupPositionMarker in arrayList2)
          {
            CodeGroup codeGroup = XMLUtil.CreateCodeGroup(groupPositionMarker.element);
            if (codeGroup == null)
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FailedCodeGroup"), (object) groupPositionMarker.element.Attribute("class")));
            codeGroup.FromXml(groupPositionMarker.element, this.m_parentLevel);
            codeGroup.Resolve(evidence);
            codeGroup.MembershipCondition.Check(evidence);
            arrayList1.Insert(groupPositionMarker.groupIndex, (object) codeGroup);
            this.m_element.InternalChildren.Insert(groupPositionMarker.elementIndex, (object) groupPositionMarker.element);
          }
        }
        this.m_children = (IList) arrayList1;
      }
    }

    private void ParsePolicy()
    {
label_0:
      PolicyStatement policyStatement = new PolicyStatement();
      bool flag = false;
      SecurityElement et = new SecurityElement("PolicyStatement");
      et.AddAttribute("version", "1");
      SecurityElement element = this.m_element;
      lock (this)
      {
        if (this.m_element != null)
        {
          string str1 = this.m_element.Attribute("PermissionSetName");
          if (str1 != null)
          {
            et.AddAttribute("PermissionSetName", str1);
            flag = true;
          }
          else
          {
            SecurityElement child = this.m_element.SearchForChildByTag("PermissionSet");
            if (child != null)
            {
              et.AddChild(child);
              flag = true;
            }
            else
            {
              et.AddChild(new PermissionSet(false).ToXml());
              flag = true;
            }
          }
          string str2 = this.m_element.Attribute("Attributes");
          if (str2 != null)
          {
            et.AddAttribute("Attributes", str2);
            flag = true;
          }
        }
      }
      if (flag)
        policyStatement.FromXml(et, this.m_parentLevel);
      else
        policyStatement.PermissionSet = (PermissionSet) null;
      lock (this)
      {
        if (element == this.m_element && this.m_policy == null)
          this.m_policy = policyStatement;
        else if (this.m_policy == null)
          goto label_0;
      }
      if (this.m_policy == null || this.m_children == null)
        return;
      IMembershipCondition membershipCondition = this.m_membershipCondition;
    }

    /// <summary>
    ///   Определяет, эквивалентен ли указанная группа кода текущей группы кода.
    /// </summary>
    /// <param name="o">
    ///   Группы кода для сравнения с текущей группы кода.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанная группа кода эквивалентна текущей группы кода; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      CodeGroup codeGroup = o as CodeGroup;
      if (codeGroup != null && this.GetType().Equals(codeGroup.GetType()) && (object.Equals((object) this.m_name, (object) codeGroup.m_name) && object.Equals((object) this.m_description, (object) codeGroup.m_description)))
      {
        if (this.m_membershipCondition == null && this.m_element != null)
          this.ParseMembershipCondition();
        if (codeGroup.m_membershipCondition == null && codeGroup.m_element != null)
          codeGroup.ParseMembershipCondition();
        if (object.Equals((object) this.m_membershipCondition, (object) codeGroup.m_membershipCondition))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Определяет, эквивалентен ли указанная группа кода текущей группы кода, проверка дочерние группы кода, если указан.
    /// </summary>
    /// <param name="cg">
    ///   Группы кода для сравнения с текущей группы кода.
    /// </param>
    /// <param name="compareChildren">
    ///   <see langword="true" /> для сравнения дочерних групп кода, а также; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанная группа кода эквивалентна текущей группы кода; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public bool Equals(CodeGroup cg, bool compareChildren)
    {
      if (!this.Equals((object) cg))
        return false;
      if (compareChildren)
      {
        if (this.m_children == null)
          this.ParseChildren();
        if (cg.m_children == null)
          cg.ParseChildren();
        ArrayList arrayList1 = new ArrayList((ICollection) this.m_children);
        ArrayList arrayList2 = new ArrayList((ICollection) cg.m_children);
        if (arrayList1.Count != arrayList2.Count)
          return false;
        for (int index = 0; index < arrayList1.Count; ++index)
        {
          if (!((CodeGroup) arrayList1[index]).Equals((CodeGroup) arrayList2[index], true))
            return false;
        }
      }
      return true;
    }

    /// <summary>Получает хэш-код текущей группы кода.</summary>
    /// <returns>Хэш-код текущей группы кода.</returns>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      if (this.m_membershipCondition == null && this.m_element != null)
        this.ParseMembershipCondition();
      if (this.m_name != null || this.m_membershipCondition != null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_membershipCondition == null ? 0 : this.m_membershipCondition.GetHashCode());
      return this.GetType().GetHashCode();
    }
  }
}
