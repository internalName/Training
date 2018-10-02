// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет сочетание идентификатора пользователя и маски доступа.
  ///   <see cref="T:System.Security.AccessControl.AuditRule" /> Объект также содержит сведения о как правило наследуется дочерними объектами, как это наследование распространяется, а для условиями проводится аудит.
  /// </summary>
  public abstract class AuditRule : AuthorizationRule
  {
    private readonly AuditFlags _flags;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.AuditRule" /> используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    ///    Это должен быть объект, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа данного правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение которой определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение <see langword="true" />, если правило должно наследоваться от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила аудита.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил аудита.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="auditFlags">
    ///   Условия, в которых применяется правило аудита.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="identity" /> параметра не может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" />, или <paramref name="auditFlags" /> параметр содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="accessMask" /> равно нулю, либо параметр <paramref name="inheritanceFlags" /> или <paramref name="propagationFlags" /> содержит неопознанные значения флагов.
    /// </exception>
    protected AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
    {
      if (auditFlags == AuditFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), nameof (auditFlags));
      if ((auditFlags & ~(AuditFlags.Success | AuditFlags.Failure)) != AuditFlags.None)
        throw new ArgumentOutOfRangeException(nameof (auditFlags), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this._flags = auditFlags;
    }

    /// <summary>Получает флаги аудита для данного правила аудита.</summary>
    /// <returns>
    ///   Побитовое сочетание значений перечисления.
    ///    Это сочетание определяет условия аудита для данного правила аудита.
    /// </returns>
    public AuditFlags AuditFlags
    {
      get
      {
        return this._flags;
      }
    }
  }
}
