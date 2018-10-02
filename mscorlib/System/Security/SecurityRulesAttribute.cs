// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityRulesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Указывает набор правил безопасности, которые следует применить среды CLR для сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  public sealed class SecurityRulesAttribute : Attribute
  {
    private SecurityRuleSet m_ruleSet;
    private bool m_skipVerificationInFullTrust;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.SecurityRulesAttribute" /> с использованием указанного правила заданное значение.
    /// </summary>
    /// <param name="ruleSet">
    ///   Задано одно из значений перечисления, определяющее правила прозрачности.
    /// </param>
    public SecurityRulesAttribute(SecurityRuleSet ruleSet)
    {
      this.m_ruleSet = ruleSet;
    }

    /// <summary>
    ///   Определяет ли полностью доверенного прозрачного кода следует пропустить проверку Microsoft промежуточного языка MSIL.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если проверка MSIL пропускается; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool SkipVerificationInFullTrust
    {
      get
      {
        return this.m_skipVerificationInFullTrust;
      }
      set
      {
        this.m_skipVerificationInFullTrust = value;
      }
    }

    /// <summary>Возвращает набор применяемых правил.</summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющее применяемые правила прозрачности.
    /// </returns>
    public SecurityRuleSet RuleSet
    {
      get
      {
        return this.m_ruleSet;
      }
    }
  }
}
