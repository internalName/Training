// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.KnownAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Инкапсулирует все типы записи управления доступом (ACE), в настоящее время определены корпорацией Майкрософт.
  ///    Все <see cref="T:System.Security.AccessControl.KnownAce" /> объекты содержат маски доступа 32-разрядных и <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
  /// </summary>
  public abstract class KnownAce : GenericAce
  {
    private int _accessMask;
    private SecurityIdentifier _sid;
    internal const int AccessMaskLength = 4;

    internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier)
      : base(type, flags)
    {
      if (securityIdentifier == (SecurityIdentifier) null)
        throw new ArgumentNullException(nameof (securityIdentifier));
      this.AccessMask = accessMask;
      this.SecurityIdentifier = securityIdentifier;
    }

    /// <summary>
    ///   Возвращает или задает маску доступа для этого <see cref="T:System.Security.AccessControl.KnownAce" /> объекта.
    /// </summary>
    /// <returns>
    ///   Маска доступа для этого <see cref="T:System.Security.AccessControl.KnownAce" /> объекта.
    /// </returns>
    public int AccessMask
    {
      get
      {
        return this._accessMask;
      }
      set
      {
        this._accessMask = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект, связанный с этим <see cref="T:System.Security.AccessControl.KnownAce" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" /> Объект, связанный с этим <see cref="T:System.Security.AccessControl.KnownAce" /> объекта.
    /// </returns>
    public SecurityIdentifier SecurityIdentifier
    {
      get
      {
        return this._sid;
      }
      set
      {
        if (value == (SecurityIdentifier) null)
          throw new ArgumentNullException(nameof (value));
        this._sid = value;
      }
    }
  }
}
