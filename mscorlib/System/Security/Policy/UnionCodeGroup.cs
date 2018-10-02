// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.UnionCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет группу кода, в которой инструкция политики является объединением инструкции политики текущей группы кода и инструкции политики все его соответствующих дочерних групп кода.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Obsolete("This type is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
  [Serializable]
  public sealed class UnionCodeGroup : CodeGroup, IUnionSemanticCodeGroup
  {
    internal UnionCodeGroup()
    {
    }

    internal UnionCodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
      : base(membershipCondition, permSet)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.UnionCodeGroup" />.
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
    public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
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
      PolicyStatement policyStatement = this.PolicyStatement;
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      if (dependentEvidence != null && !dependentEvidence.IsVerified)
        policyStatement.AddDependentEvidence(dependentEvidence);
      bool flag = false;
      IEnumerator enumerator = this.Children.GetEnumerator();
      while (enumerator.MoveNext() && !flag)
      {
        PolicyStatement childPolicy = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
        if (childPolicy != null)
        {
          policyStatement.InplaceUnion(childPolicy);
          if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
            flag = true;
        }
      }
      return policyStatement;
    }

    PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      if (this.MembershipCondition.Check(evidence))
        return this.PolicyStatement;
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

    /// <summary>Создает глубокую копию текущей группы кода.</summary>
    /// <returns>
    ///   Эквивалентная копия текущей группы кода, включающая условия членства и дочерние группы кода.
    /// </returns>
    public override CodeGroup Copy()
    {
      UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
      unionCodeGroup.MembershipCondition = this.MembershipCondition;
      unionCodeGroup.PolicyStatement = this.PolicyStatement;
      unionCodeGroup.Name = this.Name;
      unionCodeGroup.Description = this.Description;
      foreach (CodeGroup child in (IEnumerable) this.Children)
        unionCodeGroup.AddChild(child);
      return (CodeGroup) unionCodeGroup;
    }

    /// <summary>Получает объединенную логику.</summary>
    /// <returns>Всегда строка «Объединение».</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_Union");
      }
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.UnionCodeGroup";
    }
  }
}
