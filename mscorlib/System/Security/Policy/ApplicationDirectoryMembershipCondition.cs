// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationDirectoryMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки ее каталога приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationDirectoryMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
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
      ApplicationDirectory hostEvidence1 = evidence.GetHostEvidence<ApplicationDirectory>();
      Url hostEvidence2 = evidence.GetHostEvidence<Url>();
      if (hostEvidence1 != null && hostEvidence2 != null)
      {
        string directory = hostEvidence1.Directory;
        if (directory != null && directory.Length > 1)
        {
          URLString urlString = new URLString(directory[directory.Length - 1] != '/' ? directory + "/*" : directory + "*");
          if (hostEvidence2.GetURLString().IsSubsetOf((SiteString) urlString))
          {
            usedEvidence = (object) hostEvidence1;
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new ApplicationDirectoryMembershipCondition();
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
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> Параметр не элемент условия членства каталога допустимое приложение.
    /// </exception>
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
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.ApplicationDirectoryMembershipCondition");
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
    ///   <paramref name="e" /> Параметр не элемент условия членства каталога допустимое приложение.
    /// </exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
    }

    /// <summary>
    ///   Определяет, является ли заданному условию членства <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если заданному условию членства <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      return o is ApplicationDirectoryMembershipCondition;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    public override int GetHashCode()
    {
      return typeof (ApplicationDirectoryMembershipCondition).GetHashCode();
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление состояния условия членства.</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("ApplicationDirectory_ToString");
    }
  }
}
