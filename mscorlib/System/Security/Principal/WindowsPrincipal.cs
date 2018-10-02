// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>
  ///   Позволяет осуществить проверку членства в группе Windows для пользователя Windows.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, SecurityInfrastructure = true)]
  public class WindowsPrincipal : ClaimsPrincipal
  {
    private WindowsIdentity m_identity;
    private string[] m_roles;
    private Hashtable m_rolesTable;
    private bool m_rolesLoaded;

    private WindowsPrincipal()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsPrincipal" />, используя указанный объект <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </summary>
    /// <param name="ntIdentity">
    ///   Объект, из которого создается новый экземпляр <see cref="T:System.Security.Principal.WindowsPrincipal" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="ntIdentity" /> имеет значение <see langword="null" />.
    /// </exception>
    public WindowsPrincipal(WindowsIdentity ntIdentity)
      : base((IIdentity) ntIdentity)
    {
      if (ntIdentity == null)
        throw new ArgumentNullException(nameof (ntIdentity));
      this.m_identity = ntIdentity;
    }

    [OnDeserialized]
    [SecuritySafeCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          claimsIdentity = identity;
          break;
        }
      }
      if (claimsIdentity != null)
        return;
      this.AddIdentity((ClaimsIdentity) this.m_identity);
    }

    /// <summary>Возвращает удостоверение текущего участника.</summary>
    /// <returns>
    ///   <see cref="T:System.Security.Principal.WindowsIdentity" /> Объект текущего участника.
    /// </returns>
    public override IIdentity Identity
    {
      get
      {
        return (IIdentity) this.m_identity;
      }
    }

    /// <summary>
    ///   Определяет, относится ли текущий участник к группе пользователей Windows с заданным именем.
    /// </summary>
    /// <param name="role">
    ///   Имя группы пользователей Windows, для которой требуется проверить членство.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий участник является членом заданной группы пользователей Windows; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override bool IsInRole(string role)
    {
      if (role == null || role.Length == 0)
        return false;
      SecurityIdentifier sid = NTAccount.Translate(new IdentityReferenceCollection(1)
      {
        (IdentityReference) new NTAccount(role)
      }, typeof (SecurityIdentifier), false)[0] as SecurityIdentifier;
      if (sid != (SecurityIdentifier) null && this.IsInRole(sid))
        return true;
      return base.IsInRole(role);
    }

    /// <summary>
    ///   Возвращает все утверждения пользователя Windows с этим участником.
    /// </summary>
    /// <returns>
    ///   Коллекция всех утверждений пользователя Windows с этим участником.
    /// </returns>
    public virtual IEnumerable<Claim> UserClaims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          WindowsIdentity windowsIdentity = identity as WindowsIdentity;
          if (windowsIdentity != null)
          {
            foreach (Claim userClaim in windowsIdentity.UserClaims)
              yield return userClaim;
          }
        }
      }
    }

    /// <summary>
    ///   Возвращает все утверждения устройства Windows с этим участником.
    /// </summary>
    /// <returns>
    ///   Коллекция всех утверждений устройств Windows с этим участником.
    /// </returns>
    public virtual IEnumerable<Claim> DeviceClaims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          WindowsIdentity windowsIdentity = identity as WindowsIdentity;
          if (windowsIdentity != null)
          {
            foreach (Claim deviceClaim in windowsIdentity.DeviceClaims)
              yield return deviceClaim;
          }
        }
      }
    }

    /// <summary>
    ///   Определяет, относится ли текущий участник к группе пользователей Windows с заданным <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.
    /// </summary>
    /// <param name="role">
    ///   Одно из значений <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий участник является членом заданной группы пользователей Windows; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="role" /> не является допустимым значением <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.
    /// </exception>
    public virtual bool IsInRole(WindowsBuiltInRole role)
    {
      if (role < WindowsBuiltInRole.Administrator || role > WindowsBuiltInRole.Replicator)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) role), nameof (role));
      return this.IsInRole((int) role);
    }

    /// <summary>
    ///   Определяет, относится ли текущий участник к группе пользователей Windows с заданным относительным идентификатором (RID).
    /// </summary>
    /// <param name="rid">
    ///   RID группы пользователей Windows, в которой требуется проверить состояние членства участника.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий участник является членом заданной группы пользователей Windows, в определенной роли. в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsInRole(int rid)
    {
      return this.IsInRole(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[2]
      {
        32,
        rid
      }));
    }

    /// <summary>
    ///   Определяет, относится ли текущий участник к группе пользователей Windows с идентификатором безопасности (SID).
    /// </summary>
    /// <param name="sid">
    ///   Объект <see cref="T:System.Security.Principal.SecurityIdentifier" />  однозначно определяющий группу пользователей Windows.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий участник является членом заданной группы пользователей Windows; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sid" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Windows возвращается ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual bool IsInRole(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException(nameof (sid));
      if (this.m_identity.AccessToken.IsInvalid)
        return false;
      SafeAccessTokenHandle invalidHandle = SafeAccessTokenHandle.InvalidHandle;
      if (this.m_identity.ImpersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_identity.AccessToken, 8U, IntPtr.Zero, 2U, 2U, ref invalidHandle))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      bool IsMember = false;
      if (!Win32Native.CheckTokenMembership(this.m_identity.ImpersonationLevel != TokenImpersonationLevel.None ? this.m_identity.AccessToken : invalidHandle, sid.BinaryForm, ref IsMember))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      invalidHandle.Dispose();
      return IsMember;
    }
  }
}
