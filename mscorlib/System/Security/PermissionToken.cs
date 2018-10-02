// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security
{
  [Serializable]
  internal sealed class PermissionToken : ISecurityEncodable
  {
    private static volatile ReflectionPermission s_reflectPerm = (ReflectionPermission) null;
    internal static TokenBasedSet s_tokenSet = new TokenBasedSet();
    private static readonly PermissionTokenFactory s_theTokenFactory = new PermissionTokenFactory(4);
    private const string c_mscorlibName = "mscorlib";
    internal int m_index;
    internal volatile PermissionTokenType m_type;
    internal string m_strTypeName;

    internal static bool IsMscorlibClassName(string className)
    {
      if (className.IndexOf(',') == -1)
        return true;
      int num = className.LastIndexOf(']');
      if (num == -1)
        num = 0;
      for (int indexA = num; indexA < className.Length; ++indexA)
      {
        if ((className[indexA] == 'm' || className[indexA] == 'M') && string.Compare(className, indexA, "mscorlib", 0, "mscorlib".Length, StringComparison.OrdinalIgnoreCase) == 0)
          return true;
      }
      return false;
    }

    internal PermissionToken()
    {
    }

    internal PermissionToken(int index, PermissionTokenType type, string strTypeName)
    {
      this.m_index = index;
      this.m_type = type;
      this.m_strTypeName = strTypeName;
    }

    [SecurityCritical]
    public static PermissionToken GetToken(Type cls)
    {
      if (cls == (Type) null)
        return (PermissionToken) null;
      if (!(cls.GetInterface("System.Security.Permissions.IBuiltInPermission") != (Type) null))
        return PermissionToken.s_theTokenFactory.GetToken(cls, (IPermission) null);
      if (PermissionToken.s_reflectPerm == null)
        PermissionToken.s_reflectPerm = new ReflectionPermission(PermissionState.Unrestricted);
      PermissionToken.s_reflectPerm.Assert();
      int index = (int) (cls.GetMethod("GetTokenIndex", BindingFlags.Static | BindingFlags.NonPublic) as RuntimeMethodInfo).UnsafeInvoke((object) null, BindingFlags.Default, (Binder) null, (object[]) null, (CultureInfo) null);
      return PermissionToken.s_theTokenFactory.BuiltInGetToken(index, (IPermission) null, cls);
    }

    public static PermissionToken GetToken(IPermission perm)
    {
      if (perm == null)
        return (PermissionToken) null;
      IBuiltInPermission builtInPermission = perm as IBuiltInPermission;
      if (builtInPermission != null)
        return PermissionToken.s_theTokenFactory.BuiltInGetToken(builtInPermission.GetTokenIndex(), perm, (Type) null);
      return PermissionToken.s_theTokenFactory.GetToken(perm.GetType(), perm);
    }

    public static PermissionToken GetToken(string typeStr)
    {
      return PermissionToken.GetToken(typeStr, false);
    }

    public static PermissionToken GetToken(string typeStr, bool bCreateMscorlib)
    {
      if (typeStr == null)
        return (PermissionToken) null;
      if (!PermissionToken.IsMscorlibClassName(typeStr))
        return PermissionToken.s_theTokenFactory.GetToken(typeStr);
      if (!bCreateMscorlib)
        return (PermissionToken) null;
      return PermissionToken.FindToken(Type.GetType(typeStr));
    }

    [SecuritySafeCritical]
    public static PermissionToken FindToken(Type cls)
    {
      if (cls == (Type) null)
        return (PermissionToken) null;
      if (!(cls.GetInterface("System.Security.Permissions.IBuiltInPermission") != (Type) null))
        return PermissionToken.s_theTokenFactory.FindToken(cls);
      if (PermissionToken.s_reflectPerm == null)
        PermissionToken.s_reflectPerm = new ReflectionPermission(PermissionState.Unrestricted);
      PermissionToken.s_reflectPerm.Assert();
      int index = (int) (cls.GetMethod("GetTokenIndex", BindingFlags.Static | BindingFlags.NonPublic) as RuntimeMethodInfo).UnsafeInvoke((object) null, BindingFlags.Default, (Binder) null, (object[]) null, (CultureInfo) null);
      return PermissionToken.s_theTokenFactory.BuiltInGetToken(index, (IPermission) null, cls);
    }

    public static PermissionToken FindTokenByIndex(int i)
    {
      return PermissionToken.s_theTokenFactory.FindTokenByIndex(i);
    }

    public static bool IsTokenProperlyAssigned(IPermission perm, PermissionToken token)
    {
      PermissionToken token1 = PermissionToken.GetToken(perm);
      return token1.m_index == token.m_index && token.m_type == token1.m_type && (!(perm.GetType().Module.Assembly == Assembly.GetExecutingAssembly()) || token1.m_index < 17);
    }

    public SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement(nameof (PermissionToken));
      if ((this.m_type & PermissionTokenType.BuiltIn) != (PermissionTokenType) 0)
        securityElement.AddAttribute("Index", string.Concat((object) this.m_index));
      else
        securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_strTypeName));
      securityElement.AddAttribute("Type", this.m_type.ToString("F"));
      return securityElement;
    }

    public void FromXml(SecurityElement elRoot)
    {
      elRoot.Tag.Equals(nameof (PermissionToken));
      string typeStr = elRoot.Attribute("Name");
      PermissionToken permissionToken = typeStr == null ? PermissionToken.FindTokenByIndex(int.Parse(elRoot.Attribute("Index"), (IFormatProvider) CultureInfo.InvariantCulture)) : PermissionToken.GetToken(typeStr, true);
      this.m_index = permissionToken.m_index;
      this.m_type = (PermissionTokenType) Enum.Parse(typeof (PermissionTokenType), elRoot.Attribute("Type"));
      this.m_strTypeName = permissionToken.m_strTypeName;
    }
  }
}
