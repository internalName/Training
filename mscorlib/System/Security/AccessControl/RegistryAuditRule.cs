// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistryAuditRule
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
  public sealed class RegistryAuditRule : AuditRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> класс, указав пользователя или группы для аудита, прав для аудита, необходимости учитывать наследование и следует ли проводить аудит успехов и сбоев.
    /// </summary>
    /// <param name="identity">
    ///   Пользователь или группа, которым применяется правило.
    ///    Должен иметь тип <see cref="T:System.Security.Principal.SecurityIdentifier" /> или типа, например <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие типы доступа для аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> значения, указывающие, применяется ли это правило аудита для подразделов для текущего раздела.
    /// </param>
    /// <param name="propagationFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> значения, которые влияют на способ правила аудита, наследуемые распространяется подразделов для текущего раздела.
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
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inheritanceFlags" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="propagationFlags" /> Задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="registryRights" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" /> ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, (int) registryRights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> класс, указав имя пользователя или группы для аудита, прав для аудита, необходимости учитывать наследование и следует ли проводить аудит успехов и сбоев.
    /// </summary>
    /// <param name="identity">
    ///   Имя пользователя или группы к применяется правило.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие типы доступа для аудита.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> флагов, указывающее, применяется ли это правило аудита для подразделов для текущего раздела.
    /// </param>
    /// <param name="propagationFlags">
    ///   Сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> флаги, которые влияют на способ правила аудита, наследуемые распространяется подразделов для текущего раздела.
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
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inheritanceFlags" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="propagationFlags" /> Задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="registryRights" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="identity" /> представляет собой строку нулевой длины.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="identity" /> имеет длину более 512 символов.
    /// </exception>
    public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), (int) registryRights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal RegistryAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>Получает права доступа, применяется правило аудита.</summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значений, указывающих права, применяется правило аудита.
    /// </returns>
    public RegistryRights RegistryRights
    {
      get
      {
        return (RegistryRights) this.AccessMask;
      }
    }
  }
}
