// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistryAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет набор прав доступа, разрешенных или запрещенных для пользователя или группы.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class RegistryAccessRule : AccessRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> класс, указав пользователя или группу, относится правило, права доступа и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Пользователь или группа, которым применяется правило.
    ///    Должен иметь тип <see cref="T:System.Security.Principal.SecurityIdentifier" /> или типа, например <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие, является ли права разрешен или запрещен.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="registryRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> Задает недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="eventRights" /> равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="identity" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" /> ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type)
      : this(identity, (int) registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> класс, указав имя пользователя или группы, относится правило, права доступа и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Имя пользователя или группы к применяется правило.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие, является ли права разрешен или запрещен.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="registryRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> Задает недопустимое значение.
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
    public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> класса, указав пользователя или группу, относится правило, права доступа, флаги наследования, флаги распространения и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Пользователь или группа, которым применяется правило.
    ///    Должен иметь тип <see cref="T:System.Security.Principal.SecurityIdentifier" /> или типа, например <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> флаги, определяющие наследование прав доступа от других объектов.
    /// </param>
    /// <param name="propagationFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> флаги, определяющие, как права доступа распространяются на другие объекты.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="registryRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> Задает недопустимое значение.
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
    ///   <paramref name="identity" /> не является ни типа <see cref="T:System.Security.Principal.SecurityIdentifier" />, ни типа, такие как <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </exception>
    public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this(identity, (int) registryRights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> класса, указав имя пользователя или группы, относится правило, права доступа, флаги наследования, флаги распространения и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Имя пользователя или группы к применяется правило.
    /// </param>
    /// <param name="registryRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="inheritanceFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.InheritanceFlags" /> флаги, определяющие наследование прав доступа от других объектов.
    /// </param>
    /// <param name="propagationFlags">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.PropagationFlags" /> флаги, определяющие, как права доступа распространяются на другие объекты.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="registryRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> Задает недопустимое значение.
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
    ///   <paramref name="eventRights" /> равно нулю.
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
    public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) registryRights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    internal RegistryAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Возвращает права разрешаются или запрещаются в правило доступа.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.RegistryRights" /> значения, указывающие права разрешаются или запрещаются в правило доступа.
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
