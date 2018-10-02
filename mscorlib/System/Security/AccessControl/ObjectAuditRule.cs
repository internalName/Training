// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет сочетание идентификатора пользователя, маски доступа и условий аудита.
  ///   <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> Объект также содержит сведения о типе объекта, к которому относится правило, типе дочернего объекта, который может наследовать правило, как правило наследуется дочерними объектами и как это наследование распространяется.
  /// </summary>
  public abstract class ObjectAuditRule : AuditRule
  {
    private readonly Guid _objectType;
    private readonly Guid _inheritedObjectType;
    private readonly ObjectAceFlags _objectFlags;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.ObjectAuditRule" />.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    ///     Это должен быть объект, который может быть приведен как <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа данного правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение каждого из которых определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение <see langword="true" />, если правило наследуется от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Указывает свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="objectType">
    ///   Тип объекта, к которому применяется правило.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Тип дочернего объекта, который может наследовать правило.
    /// </param>
    /// <param name="auditFlags">Условия аудита.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="identity" /> параметра не может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" />, или <paramref name="type" /> параметр содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="accessMask" /> равно 0, или <paramref name="inheritanceFlags" /> или <paramref name="propagationFlags" /> Параметры содержат Нераспознанный флаг значения.
    /// </exception>
    protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
    {
      if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
      {
        this._objectType = objectType;
        this._objectFlags |= ObjectAceFlags.ObjectAceTypePresent;
      }
      else
        this._objectType = Guid.Empty;
      if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
      {
        this._inheritedObjectType = inheritedObjectType;
        this._objectFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
      }
      else
        this._inheritedObjectType = Guid.Empty;
    }

    /// <summary>
    ///   Получает тип объекта, к которому применяется <see cref="T:System.Security.AccessControl.ObjectAuditRule" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта, к которому применяется <see cref="T:System.Security.AccessControl.ObjectAuditRule" />.
    /// </returns>
    public Guid ObjectType
    {
      get
      {
        return this._objectType;
      }
    }

    /// <summary>
    ///   Возвращает тип дочернего объекта, который может наследоваться от объекта <see cref="T:System.Security.AccessControl.ObjectAuditRule" />.
    /// </summary>
    /// <returns>
    ///   Тип дочернего объекта, который может наследоваться от объекта <see cref="T:System.Security.AccessControl.ObjectAuditRule" />.
    /// </returns>
    public Guid InheritedObjectType
    {
      get
      {
        return this._inheritedObjectType;
      }
    }

    /// <summary>
    ///   Свойства <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> и <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> объекта <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> содержат допустимые значения.
    /// </summary>
    /// <returns>
    ///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> указывает, что свойство <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> содержит допустимое значение.
    ///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> указывает, что свойство <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> содержит допустимое значение.
    ///    Эти значения могут объединяться с помощью логического ИЛИ.
    /// </returns>
    public ObjectAceFlags ObjectFlags
    {
      get
      {
        return this._objectFlags;
      }
    }
  }
}
