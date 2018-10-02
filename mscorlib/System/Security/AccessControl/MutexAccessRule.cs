// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.MutexAccessRule
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
  public sealed class MutexAccessRule : AccessRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.MutexAccessRule" /> класс, указав пользователя или группу, относится правило, права доступа и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Пользователь или группа, которым применяется правило.
    ///    Должен иметь тип <see cref="T:System.Security.Principal.SecurityIdentifier" /> или типа, например <see cref="T:System.Security.Principal.NTAccount" /> можно преобразовать в тип <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="eventRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.MutexRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="eventRights" /> Задает недопустимое значение.
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
    public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type)
      : this(identity, (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.MutexAccessRule" /> класс, указав имя пользователя или группы, относится правило, права доступа и ли указанные права доступа разрешен или запрещен.
    /// </summary>
    /// <param name="identity">
    ///   Имя пользователя или группы к применяется правило.
    /// </param>
    /// <param name="eventRights">
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.MutexRights" /> значения, указывающие права разрешен или запрещен.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Security.AccessControl.AccessControlType" /> значения, указывающие разрешен или запрещен права.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="eventRights" /> Задает недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> Задает недопустимое значение.
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
    public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    internal MutexAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Возвращает права разрешаются или запрещаются в правило доступа.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.AccessControl.MutexRights" /> значения, указывающие права разрешаются или запрещаются в правило доступа.
    /// </returns>
    public MutexRights MutexRights
    {
      get
      {
        return (MutexRights) this.AccessMask;
      }
    }
  }
}
