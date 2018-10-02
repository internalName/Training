// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет набор прав доступа, подлежащие аудиту для пользователя или группы.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class EventWaitHandleAuditRule : AuditRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> с указанием пользователя или группы для аудита, прав для аудита и следует ли проводить аудит успехов и сбоев.
    /// </summary>
    /// <param name="identity">
    ///   Пользователь или группа, которым применяется правило.
    ///    Должен иметь тип <see cref="T:System.Security.Principal.SecurityIdentifier" /> или типа, например <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="eventRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> значения, указывающие типы доступа для аудита.
    /// </param>
    /// <param name="flags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.AuditFlags" /> значения, указывающие, следует ли проводить аудит успехов и сбоев.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="eventRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="flags" /> Задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="eventRights" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="flags" /> имеет значение <see cref="F:System.Security.AccessControl.AuditFlags.None" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" /> ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags)
      : this(identity, (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    internal EventWaitHandleAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>Получает права доступа, применяется правило аудита.</summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> значений, указывающих права, применяется правило аудита.
    /// </returns>
    public EventWaitHandleRights EventWaitHandleRights
    {
      get
      {
        return (EventWaitHandleRights) this.AccessMask;
      }
    }
  }
}
