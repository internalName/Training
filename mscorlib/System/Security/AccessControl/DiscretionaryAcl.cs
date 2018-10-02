// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.DiscretionaryAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>Представляет список управления доступом (DACL).</summary>
  public sealed class DiscretionaryAcl : CommonAcl
  {
    private static SecurityIdentifier _sidEveryone = new SecurityIdentifier(WellKnownSidType.WorldSid, (SecurityIdentifier) null);
    private bool everyOneFullAccessForNullDacl;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> с использованием указанных значений.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="capacity">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
      : this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> с использованием указанных значений.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="revision">
    ///   Номер редакции нового <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </param>
    /// <param name="capacity">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
      : base(isContainer, isDS, revision, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> класса с использованием указанных значений из указанного <see cref="T:System.Security.AccessControl.RawAcl" /> объекта.
    /// </summary>
    /// <param name="isContainer">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объект-контейнер.
    /// </param>
    /// <param name="isDS">
    ///   <see langword="true" /> Если новый <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> является объектом каталога список управления доступом (ACL).
    /// </param>
    /// <param name="rawAcl">
    ///   Базовый <see cref="T:System.Security.AccessControl.RawAcl" /> объекта для нового <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    ///    Укажите <see langword="null" /> для создания пустой ACL.
    /// </param>
    public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
      : this(isContainer, isDS, rawAcl, false)
    {
    }

    internal DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
      : base(isContainer, isDS, rawAcl == null ? new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 0) : rawAcl, trusted, true)
    {
    }

    /// <summary>
    ///   Добавляет в текущую запись управления доступом (ACE) с указанными параметрами <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для добавления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить запись ACE.
    /// </param>
    /// <param name="accessMask">
    ///   Правила доступа для нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового элемента управления доступом.
    /// </param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.AddQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Задает указанный доступ для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для установки.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать запись ACE.
    /// </param>
    /// <param name="accessMask">
    ///   Правила доступа для нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового элемента управления доступом.
    /// </param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.SetQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Удаляет из текущего правила управления доступом указанного <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для удаляемого правила управления доступом.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод успешно удаляет указанное правило доступа; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      return this.RemoveQualifiedAces(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Удаляет указанный элемент управления доступом (ACE) из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить записи управления ДОСТУПОМ.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      this.RemoveQualifiedAcesSpecific(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>
    ///   Добавляет в текущую запись управления доступом (ACE) с указанными параметрами <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для добавления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить запись ACE.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> Для новых доступа.
    /// </param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Добавляет в текущую запись управления доступом (ACE) с указанными параметрами <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    ///    Используйте этот метод для объектов каталогов, списки управления доступом (ACL), при указании типа объекта или типа наследуемого объекта для нового элемента управления ДОСТУПОМ.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для добавления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которой необходимо добавить запись ACE.
    /// </param>
    /// <param name="accessMask">
    ///   Правила доступа для нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового элемента управления доступом.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новый элемент управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новый элемент управления ДОСТУПОМ.
    /// </param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.AddQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Задает указанный доступ для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для установки.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать запись ACE.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> Для которого требуется задать доступ.
    /// </param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Задает указанный доступ для указанного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для установки.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого требуется задать запись ACE.
    /// </param>
    /// <param name="accessMask">
    ///   Правила доступа для нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования нового элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования нового элемента управления доступом.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется новый элемент управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать новый элемент управления ДОСТУПОМ.
    /// </param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.SetQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Удаляет из текущего правила управления доступом указанного <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для удаляемого правила управления доступом.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> Для которой необходимо удалить доступ.
    /// </param>
    /// <returns>
    ///   Возвращает <see cref="T:System.Boolean" />.
    /// </returns>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Удаляет из текущего правила управления доступом указанного <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    ///    Используйте этот метод для объектов каталогов, списки управления доступом (ACL), при указании типа объекта или типа наследуемого объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для удаляемого правила управления доступом.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого правила управления доступом.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого правила управления доступом.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого правила управления доступом.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется удаляемое правило управления доступом.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать удаляемое правило управления доступом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод успешно удаляет указанное правило доступа; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      return this.RemoveQualifiedAces(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанный элемент управления доступом (ACE) из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить записи управления ДОСТУПОМ.
    /// </param>
    /// <param name="rule">
    ///   <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> Для которой необходимо удалить доступ.
    /// </param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>
    ///   Удаляет указанный элемент управления доступом (ACE) из текущего <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> объекта.
    ///    Используйте этот метод для объектов каталогов списки управления доступом (ACL) при указании типа объекта или типа наследуемого объекта для записи ACE для удаления.
    /// </summary>
    /// <param name="accessType">
    ///   Тип элемента управления доступом ("Разрешить" или "Запретить") для удаления.
    /// </param>
    /// <param name="sid">
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Для которого нужно удалить записи управления ДОСТУПОМ.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа для удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Флаги, определяющие свойства наследования удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="propagationFlags">
    ///   Флаги, определяющие свойства распространения наследования удаляемого элемента управления ДОСТУПОМ.
    /// </param>
    /// <param name="objectFlags">
    ///   Флаги, указывающие <paramref name="objectType" /> и <paramref name="inheritedObjectType" /> содержат параметры отличных<see langword="null" /> значения.
    /// </param>
    /// <param name="objectType">
    ///   Идентификатор класса объектов, к которым применяется удаляемый элемент управления ДОСТУПОМ.
    /// </param>
    /// <param name="inheritedObjectType">
    ///   Идентификатор класса дочерних объектов, которые могут наследовать удаляемый элемент управления ДОСТУПОМ.
    /// </param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      this.RemoveQualifiedAcesSpecific(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    internal bool EveryOneFullAccessForNullDacl
    {
      get
      {
        return this.everyOneFullAccessForNullDacl;
      }
      set
      {
        this.everyOneFullAccessForNullDacl = value;
      }
    }

    internal override void OnAclModificationTried()
    {
      this.everyOneFullAccessForNullDacl = false;
    }

    internal static DiscretionaryAcl CreateAllowEveryoneFullAccess(bool isDS, bool isContainer)
    {
      DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 1);
      discretionaryAcl.AddAccess(AccessControlType.Allow, DiscretionaryAcl._sidEveryone, -1, isContainer ? InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit : InheritanceFlags.None, PropagationFlags.None);
      discretionaryAcl.everyOneFullAccessForNullDacl = true;
      return discretionaryAcl;
    }
  }
}
