// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.FileCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет разрешение по управлению файлами сборки сборкам кода, удовлетворяющим условию членства.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileCodeGroup : CodeGroup, IUnionSemanticCodeGroup
  {
    private FileIOPermissionAccess m_access;

    internal FileCodeGroup()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.FileCodeGroup" />.
    /// </summary>
    /// <param name="membershipCondition">
    ///   Условие членства, проверяющее свидетельство для определения, применяет ли эта группа кода политику.
    /// </param>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    ///    Это значение используется для создания <see cref="T:System.Security.Permissions.FileIOPermission" /> которой предоставлено.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="membershipCondition" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="membershipCondition" /> параметр не является допустимым.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="access" /> параметр не является допустимым.
    /// </exception>
    public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access)
      : base(membershipCondition, (PolicyStatement) null)
    {
      this.m_access = access;
    }

    /// <summary>
    ///   Обрабатывает политику для группы кода и ее дочерних элементов, используя набор свидетельств.
    /// </summary>
    /// <param name="evidence">Свидетельство для сборки.</param>
    /// <returns>
    ///   Инструкция политики, состоящая из разрешений, предоставляемых группой кода, с дополнительными атрибутами, или <see langword="null" /> если группа кода не применяется (условие членства не соответствует указанному свидетельству).
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Текущая политика – <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Более одной группы кода (включая родительские и все дочерние группы кода) помечена <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.
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
    ///   A <see cref="T:System.Security.Policy.CodeGroup" /> это корень дерева соответствующих групп кода.
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

    internal PolicyStatement CalculatePolicy(Url url)
    {
      URLString urlString = url.GetURLString();
      if (string.Compare(urlString.Scheme, "file", StringComparison.OrdinalIgnoreCase) != 0)
        return (PolicyStatement) null;
      string directoryName = urlString.GetDirectoryName();
      PermissionSet permSet = new PermissionSet(PermissionState.None);
      permSet.SetPermission((IPermission) new FileIOPermission(this.m_access, Path.GetFullPath(directoryName)));
      return new PolicyStatement(permSet, PolicyStatementAttribute.Nothing);
    }

    private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
    {
      PolicyStatement policyStatement = (PolicyStatement) null;
      Url hostEvidence = evidence.GetHostEvidence<Url>();
      if (hostEvidence != null)
        policyStatement = this.CalculatePolicy(hostEvidence);
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
      FileCodeGroup fileCodeGroup = new FileCodeGroup(this.MembershipCondition, this.m_access);
      fileCodeGroup.Name = this.Name;
      fileCodeGroup.Description = this.Description;
      foreach (CodeGroup child in (IEnumerable) this.Children)
        fileCodeGroup.AddChild(child);
      return (CodeGroup) fileCodeGroup;
    }

    /// <summary>Получает объединенную логику.</summary>
    /// <returns>Строка «Объединение».</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_Union");
      }
    }

    /// <summary>
    ///   Получает имя именованного набора разрешений для группы кода.
    /// </summary>
    /// <returns>
    ///   Concatenatation строки «FileIO - же каталог» и тип доступа.
    /// </returns>
    public override string PermissionSetName
    {
      get
      {
        return Environment.GetResourceString("FileCodeGroup_PermissionSet", (object) XMLUtil.BitFieldEnumToString(typeof (FileIOPermissionAccess), (object) this.m_access));
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

    protected override void CreateXml(SecurityElement element, PolicyLevel level)
    {
      element.AddAttribute("Access", XMLUtil.BitFieldEnumToString(typeof (FileIOPermissionAccess), (object) this.m_access));
    }

    protected override void ParseXml(SecurityElement e, PolicyLevel level)
    {
      string str = e.Attribute("Access");
      if (str != null)
        this.m_access = (FileIOPermissionAccess) Enum.Parse(typeof (FileIOPermissionAccess), str);
      else
        this.m_access = FileIOPermissionAccess.NoAccess;
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
    public override bool Equals(object o)
    {
      FileCodeGroup fileCodeGroup = o as FileCodeGroup;
      return fileCodeGroup != null && base.Equals((object) fileCodeGroup) && this.m_access == fileCodeGroup.m_access;
    }

    /// <summary>Получает хэш-код текущей группы кода.</summary>
    /// <returns>Хэш-код текущей группы кода.</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode() + this.m_access.GetHashCode();
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.FileCodeGroup";
    }
  }
}
