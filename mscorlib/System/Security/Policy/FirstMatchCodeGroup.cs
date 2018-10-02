// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.FirstMatchCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Позволяет определить посредством объединения инструкции политики группы кода и первой дочерней группы кода, соответствующий политику безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Obsolete("This type is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
  [Serializable]
  public sealed class FirstMatchCodeGroup : CodeGroup
  {
    internal FirstMatchCodeGroup()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.FirstMatchCodeGroup" />.
    /// </summary>
    /// <param name="membershipCondition">
    ///   Условие членства, проверяющее свидетельство для определения, применяет ли эта группа кода политику.
    /// </param>
    /// <param name="policy">
    ///   Инструкция политики для группы кода в форме набора разрешений и атрибутов для доступа к коду, удовлетворяющему условию членства.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="membershipCondition" /> параметр не является допустимым.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="policy" /> параметр не является допустимым.
    /// </exception>
    public FirstMatchCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
      : base(membershipCondition, policy)
    {
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
      PolicyStatement childPolicy = (PolicyStatement) null;
      foreach (object child in (IEnumerable) this.Children)
      {
        childPolicy = PolicyManager.ResolveCodeGroup(child as CodeGroup, evidence);
        if (childPolicy != null)
          break;
      }
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      bool flag = dependentEvidence != null && !dependentEvidence.IsVerified;
      PolicyStatement policyStatement1 = this.PolicyStatement;
      if (policyStatement1 == null)
      {
        if (flag)
        {
          childPolicy = childPolicy.Copy();
          childPolicy.AddDependentEvidence(dependentEvidence);
        }
        return childPolicy;
      }
      if (childPolicy != null)
      {
        PolicyStatement policyStatement2 = policyStatement1.Copy();
        if (flag)
          policyStatement2.AddDependentEvidence(dependentEvidence);
        policyStatement2.InplaceUnion(childPolicy);
        return policyStatement2;
      }
      if (flag)
        policyStatement1.AddDependentEvidence(dependentEvidence);
      return policyStatement1;
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
        {
          codeGroup.AddChild(group);
          break;
        }
      }
      return codeGroup;
    }

    /// <summary>Производит глубокое копирование группы кода.</summary>
    /// <returns>
    ///   Эквивалентная копия группы кода, включающая условия членства и дочерние группы кода.
    /// </returns>
    public override CodeGroup Copy()
    {
      FirstMatchCodeGroup firstMatchCodeGroup = new FirstMatchCodeGroup();
      firstMatchCodeGroup.MembershipCondition = this.MembershipCondition;
      firstMatchCodeGroup.PolicyStatement = this.PolicyStatement;
      firstMatchCodeGroup.Name = this.Name;
      firstMatchCodeGroup.Description = this.Description;
      foreach (CodeGroup child in (IEnumerable) this.Children)
        firstMatchCodeGroup.AddChild(child);
      return (CodeGroup) firstMatchCodeGroup;
    }

    /// <summary>Получает объединенную логику.</summary>
    /// <returns>Строка «Первое совпадение».</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_FirstMatch");
      }
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.FirstMatchCodeGroup";
    }
  }
}
