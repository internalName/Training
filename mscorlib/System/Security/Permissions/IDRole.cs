// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IDRole
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.Permissions
{
  [Serializable]
  internal class IDRole
  {
    internal bool m_authenticated;
    internal string m_id;
    internal string m_role;
    [NonSerialized]
    private SecurityIdentifier m_sid;

    internal SecurityIdentifier Sid
    {
      [SecurityCritical] get
      {
        if (string.IsNullOrEmpty(this.m_role))
          return (SecurityIdentifier) null;
        if (this.m_sid == (SecurityIdentifier) null)
          this.m_sid = NTAccount.Translate(new IdentityReferenceCollection(1)
          {
            (IdentityReference) new NTAccount(this.m_role)
          }, typeof (SecurityIdentifier), false)[0] as SecurityIdentifier;
        return this.m_sid;
      }
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("Identity");
      if (this.m_authenticated)
        securityElement.AddAttribute("Authenticated", "true");
      if (this.m_id != null)
        securityElement.AddAttribute("ID", SecurityElement.Escape(this.m_id));
      if (this.m_role != null)
        securityElement.AddAttribute("Role", SecurityElement.Escape(this.m_role));
      return securityElement;
    }

    internal void FromXml(SecurityElement e)
    {
      string strA = e.Attribute("Authenticated");
      this.m_authenticated = strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0;
      string str1 = e.Attribute("ID");
      this.m_id = str1 == null ? (string) null : str1;
      string str2 = e.Attribute("Role");
      if (str2 != null)
        this.m_role = str2;
      else
        this.m_role = (string) null;
    }

    public override int GetHashCode()
    {
      return (this.m_authenticated ? 0 : 101) + (this.m_id == null ? 0 : this.m_id.GetHashCode()) + (this.m_role == null ? 0 : this.m_role.GetHashCode());
    }
  }
}
