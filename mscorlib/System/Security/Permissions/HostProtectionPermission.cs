// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.HostProtectionPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Util;

namespace System.Security.Permissions
{
  [Serializable]
  internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    internal static volatile HostProtectionResource protectedResources;
    private HostProtectionResource m_resources;

    public HostProtectionPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.Resources = HostProtectionResource.All;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.Resources = HostProtectionResource.None;
      }
    }

    public HostProtectionPermission(HostProtectionResource resources)
    {
      this.Resources = resources;
    }

    public bool IsUnrestricted()
    {
      return this.Resources == HostProtectionResource.All;
    }

    public HostProtectionResource Resources
    {
      set
      {
        if (value < HostProtectionResource.None || value > HostProtectionResource.All)
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) value));
        this.m_resources = value;
      }
      get
      {
        return this.m_resources;
      }
    }

    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.m_resources == HostProtectionResource.None;
      if (this.GetType() != target.GetType())
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return (this.m_resources & ((HostProtectionPermission) target).m_resources) == this.m_resources;
    }

    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (this.GetType() != target.GetType())
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return (IPermission) new HostProtectionPermission(this.m_resources | ((HostProtectionPermission) target).m_resources);
    }

    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (this.GetType() != target.GetType())
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      HostProtectionResource resources = this.m_resources & ((HostProtectionPermission) target).m_resources;
      if (resources == HostProtectionResource.None)
        return (IPermission) null;
      return (IPermission) new HostProtectionPermission(resources);
    }

    public override IPermission Copy()
    {
      return (IPermission) new HostProtectionPermission(this.m_resources);
    }

    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, this.GetType().FullName);
      if (this.IsUnrestricted())
        permissionElement.AddAttribute("Unrestricted", "true");
      else
        permissionElement.AddAttribute("Resources", XMLUtil.BitFieldEnumToString(typeof (HostProtectionResource), (object) this.Resources));
      return permissionElement;
    }

    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.Resources = HostProtectionResource.All;
      }
      else
      {
        string str = esd.Attribute("Resources");
        if (str == null)
          this.Resources = HostProtectionResource.None;
        else
          this.Resources = (HostProtectionResource) Enum.Parse(typeof (HostProtectionResource), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return HostProtectionPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 9;
    }
  }
}
