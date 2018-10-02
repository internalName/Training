// Decompiled with JetBrains decompiler
// Type: System.Security.Util.SiteString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;

namespace System.Security.Util
{
  [Serializable]
  internal class SiteString
  {
    protected static char[] m_separators = new char[1]
    {
      '.'
    };
    protected string m_site;
    protected ArrayList m_separatedSite;

    protected internal SiteString()
    {
    }

    public SiteString(string site)
    {
      this.m_separatedSite = SiteString.CreateSeparatedSite(site);
      this.m_site = site;
    }

    private SiteString(string site, ArrayList separatedSite)
    {
      this.m_separatedSite = separatedSite;
      this.m_site = site;
    }

    private static ArrayList CreateSeparatedSite(string site)
    {
      if (site == null || site.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
      ArrayList arrayList = new ArrayList();
      int num1 = -1;
      int num2 = site.IndexOf('[');
      if (num2 == 0)
        num1 = site.IndexOf(']', num2 + 1);
      if (num1 != -1)
      {
        string str = site.Substring(num2 + 1, num1 - num2 - 1);
        arrayList.Add((object) str);
        return arrayList;
      }
      string[] strArray = site.Split(SiteString.m_separators);
      for (int index = strArray.Length - 1; index > -1; --index)
      {
        if (strArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
        if (strArray[index].Equals(""))
        {
          if (index != strArray.Length - 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
        }
        else if (strArray[index].Equals("*"))
        {
          if (index != 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
          arrayList.Add((object) strArray[index]);
        }
        else
        {
          if (!SiteString.AllLegalCharacters(strArray[index]))
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
          arrayList.Add((object) strArray[index]);
        }
      }
      return arrayList;
    }

    private static bool AllLegalCharacters(string str)
    {
      for (int index = 0; index < str.Length; ++index)
      {
        char c = str[index];
        if (!SiteString.IsLegalDNSChar(c) && !SiteString.IsNetbiosSplChar(c))
          return false;
      }
      return true;
    }

    private static bool IsLegalDNSChar(char c)
    {
      return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || (c >= '0' && c <= '9' || c == '-');
    }

    private static bool IsNetbiosSplChar(char c)
    {
      switch (c)
      {
        case '!':
        case '#':
        case '$':
        case '%':
        case '&':
        case '\'':
        case '(':
        case ')':
        case '-':
        case '.':
        case '@':
        case '^':
        case '_':
        case '{':
        case '}':
        case '~':
          return true;
        default:
          return false;
      }
    }

    public override string ToString()
    {
      return this.m_site;
    }

    public override bool Equals(object o)
    {
      if (o == null || !(o is SiteString))
        return false;
      return this.Equals((SiteString) o, true);
    }

    public override int GetHashCode()
    {
      return CultureInfo.InvariantCulture.TextInfo.GetCaseInsensitiveHashCode(this.m_site);
    }

    internal bool Equals(SiteString ss, bool ignoreCase)
    {
      if (this.m_site == null)
        return ss.m_site == null;
      if (ss.m_site == null || !this.IsSubsetOf(ss, ignoreCase))
        return false;
      return ss.IsSubsetOf(this, ignoreCase);
    }

    public virtual SiteString Copy()
    {
      return new SiteString(this.m_site, this.m_separatedSite);
    }

    public virtual bool IsSubsetOf(SiteString operand)
    {
      return this.IsSubsetOf(operand, true);
    }

    public virtual bool IsSubsetOf(SiteString operand, bool ignoreCase)
    {
      StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
      if (operand == null)
        return false;
      if (this.m_separatedSite.Count == operand.m_separatedSite.Count && this.m_separatedSite.Count == 0)
        return true;
      if (this.m_separatedSite.Count < operand.m_separatedSite.Count - 1 || this.m_separatedSite.Count > operand.m_separatedSite.Count && operand.m_separatedSite.Count > 0 && !operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals((object) "*"))
        return false;
      if (string.Compare(this.m_site, operand.m_site, comparisonType) == 0)
        return true;
      for (int index = 0; index < operand.m_separatedSite.Count - 1; ++index)
      {
        if (string.Compare((string) this.m_separatedSite[index], (string) operand.m_separatedSite[index], comparisonType) != 0)
          return false;
      }
      if (this.m_separatedSite.Count < operand.m_separatedSite.Count)
        return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals((object) "*");
      if (this.m_separatedSite.Count == operand.m_separatedSite.Count && string.Compare((string) this.m_separatedSite[this.m_separatedSite.Count - 1], (string) operand.m_separatedSite[this.m_separatedSite.Count - 1], comparisonType) != 0)
        return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals((object) "*");
      return true;
    }

    public virtual SiteString Intersect(SiteString operand)
    {
      if (operand == null)
        return (SiteString) null;
      if (this.IsSubsetOf(operand))
        return this.Copy();
      if (operand.IsSubsetOf(this))
        return operand.Copy();
      return (SiteString) null;
    }

    public virtual SiteString Union(SiteString operand)
    {
      if (operand == null)
        return this;
      if (this.IsSubsetOf(operand))
        return operand.Copy();
      if (operand.IsSubsetOf(this))
        return this.Copy();
      return (SiteString) null;
    }
  }
}
