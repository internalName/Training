// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessRule`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет сочетание идентификатора пользователя, маски доступа и типа управления доступом ("Разрешить" или "Запретить").
  ///    Объект AccessRule 1 "также содержит сведения о том, как правило наследуется дочерними объектами и как это наследование распространяется.
  /// </summary>
  /// <typeparam name="T">
  ///   Тип прав доступа для правила доступа.
  /// </typeparam>
  public class AccessRule<T> : AccessRule where T : struct
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса AccessRule 1 "с помощью указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    /// </param>
    /// <param name="rights">Права, правила доступа.</param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public AccessRule(IdentityReference identity, T rights, AccessControlType type)
      : this(identity, (int) (ValueType) rights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса AccessRule 1 "с помощью указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    /// </param>
    /// <param name="rights">Права, правила доступа.</param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public AccessRule(string identity, T rights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса AccessRule 1 "с помощью указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    /// </param>
    /// <param name="rights">Права, правила доступа.</param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this(identity, (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса AccessRule 1 "с помощью указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    /// </param>
    /// <param name="rights">Права, правила доступа.</param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    internal AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>Возвращает права текущего экземпляра.</summary>
    /// <returns>
    ///   Права, привести как тип &lt; T &gt; текущего экземпляра.
    /// </returns>
    public T Rights
    {
      get
      {
        return (T) (ValueType) this.AccessMask;
      }
    }
  }
}
