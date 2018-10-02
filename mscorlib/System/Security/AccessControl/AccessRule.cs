// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет сочетание идентификатора пользователя, маски доступа и типа управления доступом ("Разрешить" или "Запретить").
  ///   <see cref="T:System.Security.AccessControl.AccessRule" /> Также содержит сведения о том, как правило наследуется дочерними объектами и как это наследование распространяется.
  /// </summary>
  public abstract class AccessRule : AuthorizationRule
  {
    private readonly AccessControlType _type;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.AccessRule" /> используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    ///    Этот параметр должен быть объектом, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа этого правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение каждого из которых определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение <see langword="true" />, если правило наследуется от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="identity" /> параметра не может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" />, или <paramref name="type" /> параметр содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="accessMask" /> равно нулю, либо параметр <paramref name="inheritanceFlags" /> или <paramref name="propagationFlags" /> содержит неопознанные значения флагов.
    /// </exception>
    protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
    {
      if (type != AccessControlType.Allow && type != AccessControlType.Deny)
        throw new ArgumentOutOfRangeException(nameof (type), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      switch (inheritanceFlags)
      {
        case InheritanceFlags.None:
        case InheritanceFlags.ContainerInherit:
        case InheritanceFlags.ObjectInherit:
        case InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit:
          switch (propagationFlags)
          {
            case PropagationFlags.None:
            case PropagationFlags.NoPropagateInherit:
            case PropagationFlags.InheritOnly:
            case PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly:
              this._type = type;
              return;
            default:
              throw new ArgumentOutOfRangeException(nameof (propagationFlags), Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "PropagationFlags"));
          }
        default:
          throw new ArgumentOutOfRangeException(nameof (inheritanceFlags), Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "InheritanceFlags"));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.AccessControl.AccessControlType" /> значение, связанное с этим <see cref="T:System.Security.AccessControl.AccessRule" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.AccessControlType" /> Значение, связанное с этим <see cref="T:System.Security.AccessControl.AccessRule" /> объекта.
    /// </returns>
    public AccessControlType AccessControlType
    {
      get
      {
        return this._type;
      }
    }
  }
}
