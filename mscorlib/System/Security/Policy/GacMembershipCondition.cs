// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.GacMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки членство в глобальной сборке кэша.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>
    ///   Указывает, является ли указанное свидетельство условию членства.
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
      return evidence.GetHostEvidence<GacInstalled>() != null;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.Security.Policy.GacMembershipCondition" />.
    /// </returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new GacMembershipCondition();
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.SecurityElement" /> содержащий кодировку XML для объекта безопасности, включая все сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>
    ///   Использует указанный XML-кодирование, чтобы восстановить объект безопасности.
    /// </summary>
    /// <param name="e">
    ///   <see cref="T:System.Security.SecurityElement" /> Содержащий код XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> не является недопустимый элемент условия членства.
    /// </exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния, используя заданный контекст уровня.
    /// </summary>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекста для разрешения <see cref="T:System.Security.NamedPermissionSet" /> ссылки.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.SecurityElement" /> содержащий кодировку XML для объекта безопасности, включая все сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), this.GetType().FullName);
      element.AddAttribute("version", "1");
      return element;
    }

    /// <summary>
    ///   Использует указанный XML-кодирование, чтобы восстановить объект безопасности с помощью заданного контекста уровня политики.
    /// </summary>
    /// <param name="e">
    ///   <see cref="T:System.Security.SecurityElement" /> Содержащий код XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   <see cref="T:System.Security.Policy.PolicyLevel" /> Контекста для разрешения <see cref="T:System.Security.NamedPermissionSet" /> ссылки.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> не является недопустимый элемент условия членства.
    /// </exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
    }

    /// <summary>
    ///   Указывает, эквивалентен ли текущий объект указанному объекту.
    /// </summary>
    /// <param name="o">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является <see cref="T:System.Security.Policy.GacMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      return o is GacMembershipCondition;
    }

    /// <summary>Возвращает хэш-код для текущего состояния членства.</summary>
    /// <returns>Ноль (0).</returns>
    public override int GetHashCode()
    {
      return 0;
    }

    /// <summary>
    ///   Возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление условия членства.</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("GAC_ToString");
    }
  }
}
