// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.GenericPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
  /// <summary>Представляет универсального участника.</summary>
  [ComVisible(true)]
  [Serializable]
  public class GenericPrincipal : ClaimsPrincipal
  {
    private IIdentity m_identity;
    private string[] m_roles;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.GenericPrincipal" /> из удостоверения пользователя и массива имен ролей, которые назначены представляемому этим удостоверением пользователю.
    /// </summary>
    /// <param name="identity">
    ///   Базовая реализация <see cref="T:System.Security.Principal.IIdentity" /> — представляет любого пользователя.
    /// </param>
    /// <param name="roles">
    ///   Массив имен ролей, назначенных пользователям, которые представлены параметром <paramref name="identity" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    public GenericPrincipal(IIdentity identity, string[] roles)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      this.m_identity = identity;
      if (roles != null)
      {
        this.m_roles = new string[roles.Length];
        for (int index = 0; index < roles.Length; ++index)
          this.m_roles[index] = roles[index];
      }
      else
        this.m_roles = (string[]) null;
      this.AddIdentityWithRoles(this.m_identity, this.m_roles);
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      ClaimsIdentity subject = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          subject = identity;
          break;
        }
      }
      if (this.m_roles != null && this.m_roles.Length != 0 && subject != null)
      {
        subject.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", this.m_roles, subject).Claims);
      }
      else
      {
        if (subject != null)
          return;
        this.AddIdentityWithRoles(this.m_identity, this.m_roles);
      }
    }

    [SecuritySafeCritical]
    private void AddIdentityWithRoles(IIdentity identity, string[] roles)
    {
      ClaimsIdentity claimsIdentity1 = identity as ClaimsIdentity;
      ClaimsIdentity claimsIdentity2 = claimsIdentity1 == null ? new ClaimsIdentity(identity) : claimsIdentity1.Clone();
      if (roles != null && roles.Length != 0)
        claimsIdentity2.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", roles, claimsIdentity2).Claims);
      this.AddIdentity(claimsIdentity2);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Principal.GenericIdentity" /> пользователя, представленного текущим <see cref="T:System.Security.Principal.GenericPrincipal" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Principal.GenericIdentity" /> Пользователя, представленного <see cref="T:System.Security.Principal.GenericPrincipal" />.
    /// </returns>
    public override IIdentity Identity
    {
      get
      {
        return this.m_identity;
      }
    }

    /// <summary>
    ///   Определяет, является ли текущий <see cref="T:System.Security.Principal.GenericPrincipal" /> принадлежит к указанной роли.
    /// </summary>
    /// <param name="role">
    ///   Имя роли, для которой требуется проверить членство.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Security.Principal.GenericPrincipal" /> является членом указанной роли; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsInRole(string role)
    {
      if (role == null || this.m_roles == null)
        return false;
      for (int index = 0; index < this.m_roles.Length; ++index)
      {
        if (this.m_roles[index] != null && string.Compare(this.m_roles[index], role, StringComparison.OrdinalIgnoreCase) == 0)
          return true;
      }
      return base.IsInRole(role);
    }
  }
}
