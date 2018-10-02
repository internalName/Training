// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ZoneMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки зоны ее источника.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private static readonly string[] s_names = new string[5]
    {
      "MyComputer",
      "Intranet",
      "Trusted",
      "Internet",
      "Untrusted"
    };
    private SecurityZone m_zone;
    private SecurityElement m_element;

    internal ZoneMembershipCondition()
    {
      this.m_zone = SecurityZone.NoZone;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> класса с зоной, определяющей членство.
    /// </summary>
    /// <param name="zone">
    ///   <see cref="T:System.Security.SecurityZone" /> Для которой требуется проверить.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="zone" /> Параметр не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public ZoneMembershipCondition(SecurityZone zone)
    {
      ZoneMembershipCondition.VerifyZone(zone);
      this.SecurityZone = zone;
    }

    /// <summary>
    ///   Возвращает или задает зону, которая проверяет условие членства.
    /// </summary>
    /// <returns>Зона, который проверяет условие членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка установить <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> на недопустимый <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public SecurityZone SecurityZone
    {
      set
      {
        ZoneMembershipCondition.VerifyZone(value);
        this.m_zone = value;
      }
      get
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        return this.m_zone;
      }
    }

    private static void VerifyZone(SecurityZone zone)
    {
      switch (zone)
      {
        case SecurityZone.MyComputer:
        case SecurityZone.Intranet:
        case SecurityZone.Trusted:
        case SecurityZone.Internet:
        case SecurityZone.Untrusted:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
      }
    }

    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное свидетельство удовлетворяет условию членства; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
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
      Zone hostEvidence = evidence.GetHostEvidence<Zone>();
      if (hostEvidence != null)
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        if (hostEvidence.SecurityZone == this.m_zone)
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public IMembershipCondition Copy()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return (IMembershipCondition) new ZoneMembershipCondition(this.m_zone);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
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
    ///   <paramref name="e" /> Параметр не недопустимый элемент условия членства.
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
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.ZoneMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_zone != SecurityZone.NoZone)
        element.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) this.m_zone));
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
      lock (this)
      {
        this.m_zone = SecurityZone.NoZone;
        this.m_element = e;
      }
    }

    private void ParseZone()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string str = this.m_element.Attribute("Zone");
        this.m_zone = SecurityZone.NoZone;
        if (str == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_ZoneCannotBeNull"));
        this.m_zone = (SecurityZone) Enum.Parse(typeof (SecurityZone), str);
        ZoneMembershipCondition.VerifyZone(this.m_zone);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>
    ///   Определяет, является ли зоны из указанного объекта эквивалентна зоне, содержащейся в текущем <see cref="T:System.Security.Policy.ZoneMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.ZoneMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если зоны из указанного объекта эквивалентна зоне, содержащейся в текущем <see cref="T:System.Security.Policy.ZoneMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство для текущего объекта или указанный объект является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойства для текущего объекта или указанный объект не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public override bool Equals(object o)
    {
      ZoneMembershipCondition membershipCondition = o as ZoneMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        if (membershipCondition.m_zone == SecurityZone.NoZone && membershipCondition.m_element != null)
          membershipCondition.ParseZone();
        if (this.m_zone == membershipCondition.m_zone)
          return true;
      }
      return false;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public override int GetHashCode()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return (int) this.m_zone;
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление состояния условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> Свойство не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public override string ToString()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Zone_ToString"), (object) ZoneMembershipCondition.s_names[(int) this.m_zone]);
    }
  }
}
