// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.AllMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет условие членства, которому соответствует любой код.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
    /// </param>
    /// <returns>
    ///   Всегда <see langword="true" />.
    /// </returns>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      return true;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new AllMembershipCondition();
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Представление условия членства.</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("All_ToString");
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
    ///   Контекст уровня политики для разрешения именованного разрешение ссылок на наборы.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.AllMembershipCondition");
      element.AddAttribute("version", "1");
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
    }

    /// <summary>
    ///   Определяет, является ли заданному условию членства <see cref="T:System.Security.Policy.AllMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с <see cref="T:System.Security.Policy.AllMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если заданному условию членства <see cref="T:System.Security.Policy.AllMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      return o is AllMembershipCondition;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    public override int GetHashCode()
    {
      return typeof (AllMembershipCondition).GetHashCode();
    }
  }
}
