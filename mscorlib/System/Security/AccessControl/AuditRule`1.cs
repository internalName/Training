// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuditRule`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет сочетание идентификатора пользователя и маски доступа.
  /// </summary>
  /// <typeparam name="T">Тип правила аудита.</typeparam>
  public class AuditRule<T> : AuditRule where T : struct
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AuditRule`1" />, используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется данное правило аудита.
    /// </param>
    /// <param name="rights">Права правила аудита.</param>
    /// <param name="flags">
    ///   Условия, в которых применяется правило аудита.
    /// </param>
    public AuditRule(IdentityReference identity, T rights, AuditFlags flags)
      : this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AuditRule`1" />, используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    /// </param>
    /// <param name="rights">Права правила аудита.</param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил аудита.
    /// </param>
    /// <param name="flags">
    ///   Условия, в которых применяется правило аудита.
    /// </param>
    public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AuditRule`1" />, используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    /// </param>
    /// <param name="rights">Права правила аудита.</param>
    /// <param name="flags">Свойства правила аудита.</param>
    public AuditRule(string identity, T rights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), rights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AuditRule`1" />, используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    /// </param>
    /// <param name="rights">Права правила аудита.</param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил аудита.
    /// </param>
    /// <param name="flags">
    ///   Условия, в которых применяется правило аудита.
    /// </param>
    public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>Получает права правила аудита.</summary>
    /// <returns>Права правила аудита.</returns>
    public T Rights
    {
      get
      {
        return (T) (ValueType) this.AccessMask;
      }
    }
  }
}
