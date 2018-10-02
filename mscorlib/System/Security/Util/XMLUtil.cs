// Decompiled with JetBrains decompiler
// Type: System.Security.Util.XMLUtil
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security.Util
{
  internal static class XMLUtil
  {
    private static readonly char[] sepChar = new char[2]
    {
      ',',
      ' '
    };
    private const string BuiltInPermission = "System.Security.Permissions.";
    private const string BuiltInMembershipCondition = "System.Security.Policy.";
    private const string BuiltInCodeGroup = "System.Security.Policy.";
    private const string BuiltInApplicationSecurityManager = "System.Security.Policy.";

    public static SecurityElement NewPermissionElement(IPermission ip)
    {
      return XMLUtil.NewPermissionElement(ip.GetType().FullName);
    }

    public static SecurityElement NewPermissionElement(string name)
    {
      SecurityElement securityElement = new SecurityElement("Permission");
      securityElement.AddAttribute("class", name);
      return securityElement;
    }

    public static void AddClassAttribute(SecurityElement element, Type type, string typename)
    {
      if (typename == null)
        typename = type.FullName;
      element.AddAttribute("class", typename + ", " + type.Module.Assembly.FullName.Replace('"', '\''));
    }

    internal static bool ParseElementForAssemblyIdentification(SecurityElement el, out string className, out string assemblyName, out string assemblyVersion)
    {
      className = (string) null;
      assemblyName = (string) null;
      assemblyVersion = (string) null;
      string str = el.Attribute("class");
      if (str == null)
        return false;
      if (str.IndexOf('\'') >= 0)
        str = str.Replace('\'', '"');
      int num = str.IndexOf(',');
      if (num == -1)
        return false;
      int length = num;
      className = str.Substring(0, length);
      AssemblyName assemblyName1 = new AssemblyName(str.Substring(num + 1));
      assemblyName = assemblyName1.Name;
      assemblyVersion = assemblyName1.Version.ToString();
      return true;
    }

    [SecurityCritical]
    private static bool ParseElementForObjectCreation(SecurityElement el, string requiredNamespace, out string className, out int classNameStart, out int classNameLength)
    {
      className = (string) null;
      classNameStart = 0;
      classNameLength = 0;
      int length = requiredNamespace.Length;
      string className1 = el.Attribute("class");
      if (className1 == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoClass"));
      if (className1.IndexOf('\'') >= 0)
        className1 = className1.Replace('\'', '"');
      if (!PermissionToken.IsMscorlibClassName(className1))
        return false;
      int num1 = className1.IndexOf(',');
      int num2 = num1 != -1 ? num1 : className1.Length;
      if (num2 <= length || !className1.StartsWith(requiredNamespace, StringComparison.Ordinal))
        return false;
      className = className1;
      classNameLength = num2 - length;
      classNameStart = length;
      return true;
    }

    public static string SecurityObjectToXmlString(object ob)
    {
      if (ob == null)
        return "";
      PermissionSet permissionSet = ob as PermissionSet;
      if (permissionSet != null)
        return permissionSet.ToXml().ToString();
      return ((ISecurityEncodable) ob).ToXml().ToString();
    }

    [SecurityCritical]
    public static object XmlStringToSecurityObject(string s)
    {
      if (s == null)
        return (object) null;
      if (s.Length < 1)
        return (object) null;
      return SecurityElement.FromString(s).ToSecurityObject();
    }

    [SecuritySafeCritical]
    public static IPermission CreatePermission(SecurityElement el, PermissionState permState, bool ignoreTypeLoadFailures)
    {
      if (el == null || !el.Tag.Equals("Permission") && !el.Tag.Equals("IPermission"))
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_WrongElementType"), (object) "<Permission>"));
      string className;
      int classNameStart;
      int classNameLength;
      if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Permissions.", out className, out classNameStart, out classNameLength))
      {
        switch (classNameLength)
        {
          case 12:
            if (string.Compare(className, classNameStart, "UIPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new UIPermission(permState);
            break;
          case 16:
            if (string.Compare(className, classNameStart, "FileIOPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new FileIOPermission(permState);
            break;
          case 18:
            if (className[classNameStart] == 'R')
            {
              if (string.Compare(className, classNameStart, "RegistryPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new RegistryPermission(permState);
              break;
            }
            if (string.Compare(className, classNameStart, "SecurityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new SecurityPermission(permState);
            break;
          case 19:
            if (string.Compare(className, classNameStart, "PrincipalPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new PrincipalPermission(permState);
            break;
          case 20:
            if (className[classNameStart] == 'R')
            {
              if (string.Compare(className, classNameStart, "ReflectionPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new ReflectionPermission(permState);
              break;
            }
            if (string.Compare(className, classNameStart, "FileDialogPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new FileDialogPermission(permState);
            break;
          case 21:
            if (className[classNameStart] == 'E')
            {
              if (string.Compare(className, classNameStart, "EnvironmentPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new EnvironmentPermission(permState);
              break;
            }
            if (className[classNameStart] == 'U')
            {
              if (string.Compare(className, classNameStart, "UrlIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new UrlIdentityPermission(permState);
              break;
            }
            if (string.Compare(className, classNameStart, "GacIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new GacIdentityPermission(permState);
            break;
          case 22:
            if (className[classNameStart] == 'S')
            {
              if (string.Compare(className, classNameStart, "SiteIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new SiteIdentityPermission(permState);
              break;
            }
            if (className[classNameStart] == 'Z')
            {
              if (string.Compare(className, classNameStart, "ZoneIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IPermission) new ZoneIdentityPermission(permState);
              break;
            }
            if (string.Compare(className, classNameStart, "KeyContainerPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new KeyContainerPermission(permState);
            break;
          case 24:
            if (string.Compare(className, classNameStart, "HostProtectionPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new HostProtectionPermission(permState);
            break;
          case 27:
            if (string.Compare(className, classNameStart, "PublisherIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new PublisherIdentityPermission(permState);
            break;
          case 28:
            if (string.Compare(className, classNameStart, "StrongNameIdentityPermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new StrongNameIdentityPermission(permState);
            break;
          case 29:
            if (string.Compare(className, classNameStart, "IsolatedStorageFilePermission", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IPermission) new IsolatedStorageFilePermission(permState);
            break;
        }
      }
      object[] args = new object[1]{ (object) permState };
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      Type classFromElement = XMLUtil.GetClassFromElement(el, ignoreTypeLoadFailures);
      if (classFromElement == (Type) null)
        return (IPermission) null;
      if (!typeof (IPermission).IsAssignableFrom(classFromElement))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionType"));
      return (IPermission) Activator.CreateInstance(classFromElement, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, args, (CultureInfo) null);
    }

    [SecuritySafeCritical]
    public static CodeGroup CreateCodeGroup(SecurityElement el)
    {
      if (el == null || !el.Tag.Equals("CodeGroup"))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongElementType"), (object) "<CodeGroup>"));
      string className;
      int classNameStart;
      int classNameLength;
      if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Policy.", out className, out classNameStart, out classNameLength))
      {
        switch (classNameLength)
        {
          case 12:
            if (string.Compare(className, classNameStart, "NetCodeGroup", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (CodeGroup) new NetCodeGroup();
            break;
          case 13:
            if (string.Compare(className, classNameStart, "FileCodeGroup", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (CodeGroup) new FileCodeGroup();
            break;
          case 14:
            if (string.Compare(className, classNameStart, "UnionCodeGroup", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (CodeGroup) new UnionCodeGroup();
            break;
          case 19:
            if (string.Compare(className, classNameStart, "FirstMatchCodeGroup", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (CodeGroup) new FirstMatchCodeGroup();
            break;
        }
      }
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      Type classFromElement = XMLUtil.GetClassFromElement(el, true);
      if (classFromElement == (Type) null)
        return (CodeGroup) null;
      if (!typeof (CodeGroup).IsAssignableFrom(classFromElement))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotACodeGroupType"));
      return (CodeGroup) Activator.CreateInstance(classFromElement, true);
    }

    [SecurityCritical]
    internal static IMembershipCondition CreateMembershipCondition(SecurityElement el)
    {
      if (el == null || !el.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongElementType"), (object) "<IMembershipCondition>"));
      string className;
      int classNameStart;
      int classNameLength;
      if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Policy.", out className, out classNameStart, out classNameLength))
      {
        switch (classNameLength)
        {
          case 22:
            if (className[classNameStart] == 'A')
            {
              if (string.Compare(className, classNameStart, "AllMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IMembershipCondition) new AllMembershipCondition();
              break;
            }
            if (string.Compare(className, classNameStart, "UrlMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IMembershipCondition) new UrlMembershipCondition();
            break;
          case 23:
            if (className[classNameStart] == 'H')
            {
              if (string.Compare(className, classNameStart, "HashMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IMembershipCondition) new HashMembershipCondition();
              break;
            }
            if (className[classNameStart] == 'S')
            {
              if (string.Compare(className, classNameStart, "SiteMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
                return (IMembershipCondition) new SiteMembershipCondition();
              break;
            }
            if (string.Compare(className, classNameStart, "ZoneMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IMembershipCondition) new ZoneMembershipCondition();
            break;
          case 28:
            if (string.Compare(className, classNameStart, "PublisherMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IMembershipCondition) new PublisherMembershipCondition();
            break;
          case 29:
            if (string.Compare(className, classNameStart, "StrongNameMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IMembershipCondition) new StrongNameMembershipCondition();
            break;
          case 39:
            if (string.Compare(className, classNameStart, "ApplicationDirectoryMembershipCondition", 0, classNameLength, StringComparison.Ordinal) == 0)
              return (IMembershipCondition) new ApplicationDirectoryMembershipCondition();
            break;
        }
      }
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      Type classFromElement = XMLUtil.GetClassFromElement(el, true);
      if (classFromElement == (Type) null)
        return (IMembershipCondition) null;
      if (!typeof (IMembershipCondition).IsAssignableFrom(classFromElement))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotAMembershipCondition"));
      return (IMembershipCondition) Activator.CreateInstance(classFromElement, true);
    }

    internal static Type GetClassFromElement(SecurityElement el, bool ignoreTypeLoadFailures)
    {
      string typeName = el.Attribute("class");
      if (typeName == null)
      {
        if (ignoreTypeLoadFailures)
          return (Type) null;
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_InvalidXMLMissingAttr"), (object) "class"));
      }
      if (!ignoreTypeLoadFailures)
        return Type.GetType(typeName, true, false);
      try
      {
        return Type.GetType(typeName, false, false);
      }
      catch (SecurityException ex)
      {
        return (Type) null;
      }
    }

    public static bool IsPermissionElement(IPermission ip, SecurityElement el)
    {
      return el.Tag.Equals("Permission") || el.Tag.Equals("IPermission");
    }

    public static bool IsUnrestricted(SecurityElement el)
    {
      string str = el.Attribute("Unrestricted");
      if (str == null)
        return false;
      if (!str.Equals("true") && !str.Equals("TRUE"))
        return str.Equals("True");
      return true;
    }

    public static string BitFieldEnumToString(Type type, object value)
    {
      int num1 = (int) value;
      if (num1 == 0)
        return Enum.GetName(type, (object) 0);
      StringBuilder sb = StringBuilderCache.Acquire(16);
      bool flag = true;
      int num2 = 1;
      for (int index = 1; index < 32; ++index)
      {
        if ((num2 & num1) != 0)
        {
          string name = Enum.GetName(type, (object) num2);
          if (name != null)
          {
            if (!flag)
              sb.Append(", ");
            sb.Append(name);
            flag = false;
          }
          else
            continue;
        }
        num2 <<= 1;
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }
  }
}
