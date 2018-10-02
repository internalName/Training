// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.RoleClaimProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
  [ComVisible(false)]
  internal class RoleClaimProvider
  {
    private string m_issuer;
    private string[] m_roles;
    private ClaimsIdentity m_subject;

    public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
    {
      this.m_issuer = issuer;
      this.m_roles = roles;
      this.m_subject = subject;
    }

    public IEnumerable<Claim> Claims
    {
      get
      {
        for (int i = 0; i < this.m_roles.Length; ++i)
        {
          if (this.m_roles[i] != null)
            yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
        }
      }
    }
  }
}
