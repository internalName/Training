// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongName2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Policy;

namespace System.Security.Permissions
{
  [Serializable]
  internal sealed class StrongName2
  {
    public StrongNamePublicKeyBlob m_publicKeyBlob;
    public string m_name;
    public Version m_version;

    public StrongName2(StrongNamePublicKeyBlob publicKeyBlob, string name, Version version)
    {
      this.m_publicKeyBlob = publicKeyBlob;
      this.m_name = name;
      this.m_version = version;
    }

    public StrongName2 Copy()
    {
      return new StrongName2(this.m_publicKeyBlob, this.m_name, this.m_version);
    }

    public bool IsSubsetOf(StrongName2 target)
    {
      return this.m_publicKeyBlob == null || this.m_publicKeyBlob.Equals(target.m_publicKeyBlob) && (this.m_name == null || target.m_name != null && StrongName.CompareNames(target.m_name, this.m_name)) && ((object) this.m_version == null || (object) target.m_version != null && target.m_version.CompareTo(this.m_version) == 0);
    }

    public StrongName2 Intersect(StrongName2 target)
    {
      if (target.IsSubsetOf(this))
        return target.Copy();
      if (this.IsSubsetOf(target))
        return this.Copy();
      return (StrongName2) null;
    }

    public bool Equals(StrongName2 target)
    {
      return target.IsSubsetOf(this) && this.IsSubsetOf(target);
    }
  }
}
