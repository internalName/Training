// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuthorizationRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Определяет доступ к защищаемым объектам.
  ///    Производные классы <see cref="T:System.Security.AccessControl.AccessRule" /> и <see cref="T:System.Security.AccessControl.AuditRule" /> предоставляют специализации для функций доступа и аудита.
  /// </summary>
  public abstract class AuthorizationRule
  {
    private readonly IdentityReference _identity;
    private readonly int _accessMask;
    private readonly bool _isInherited;
    private readonly InheritanceFlags _inheritanceFlags;
    private readonly PropagationFlags _propagationFlags;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.AccessRule" />, используя указанные значения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    ///    Этот параметр должен быть объектом, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа этого правила.
    ///    Маска доступа является 32-разрядной коллекцией анонимных битов, значение которой определяется отдельными интеграторами.
    /// </param>
    /// <param name="isInherited">
    ///   Значение <see langword="true" />, если правило должно наследоваться от родительского контейнера.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Свойства наследования правила доступа.
    /// </param>
    /// <param name="propagationFlags">
    ///   Выполняется ли автоматическое распространение наследуемых правил доступа.
    ///    Флаги распространения не учитываются, если для <paramref name="inheritanceFlags" /> задано значение <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="identity" /> не может быть приведено к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="accessMask" /> равно нулю, либо параметр <paramref name="inheritanceFlags" /> или <paramref name="propagationFlags" /> содержит неопознанные значения флагов.
    /// </exception>
    protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), nameof (accessMask));
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
              if (!identity.IsValidTargetType(typeof (SecurityIdentifier)))
                throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), nameof (identity));
              this._identity = identity;
              this._accessMask = accessMask;
              this._isInherited = isInherited;
              this._inheritanceFlags = inheritanceFlags;
              if (inheritanceFlags != InheritanceFlags.None)
              {
                this._propagationFlags = propagationFlags;
                return;
              }
              this._propagationFlags = PropagationFlags.None;
              return;
            default:
              throw new ArgumentOutOfRangeException(nameof (propagationFlags), Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) nameof (PropagationFlags)));
          }
        default:
          throw new ArgumentOutOfRangeException(nameof (inheritanceFlags), Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) nameof (InheritanceFlags)));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Principal.IdentityReference" />, к которому применяется это правило.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Principal.IdentityReference" />, к которому применяется это правило.
    /// </returns>
    public IdentityReference IdentityReference
    {
      get
      {
        return this._identity;
      }
    }

    /// <summary>Возвращает маску доступа для этого правила.</summary>
    /// <returns>Маска доступа для этого правила.</returns>
    protected internal int AccessMask
    {
      get
      {
        return this._accessMask;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, задано ли это правило явно или унаследовано от родительского объекта контейнера.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если это правило не задано явно, но наследуется от родительского контейнера.
    /// </returns>
    public bool IsInherited
    {
      get
      {
        return this._isInherited;
      }
    }

    /// <summary>
    ///   Возвращает значение флагов, определяющих способ наследования этого правила дочерними объектами.
    /// </summary>
    /// <returns>Побитовое сочетание значений перечисления.</returns>
    public InheritanceFlags InheritanceFlags
    {
      get
      {
        return this._inheritanceFlags;
      }
    }

    /// <summary>
    ///   Получает значение флагов распространения, которые определяют, как наследование этого правила распространяется на дочерние объекты.
    ///    Это свойство является значимым, только когда значением перечисления <see cref="T:System.Security.AccessControl.InheritanceFlags" /> не является <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.
    /// </summary>
    /// <returns>Побитовое сочетание значений перечисления.</returns>
    public PropagationFlags PropagationFlags
    {
      get
      {
        return this._propagationFlags;
      }
    }
  }
}
