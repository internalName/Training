// Decompiled with JetBrains decompiler
// Type: System.Security.Util.DirectoryString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.Util
{
  [Serializable]
  internal class DirectoryString : SiteString
  {
    private new static char[] m_separators = new char[1]
    {
      '/'
    };
    protected static char[] m_illegalDirectoryCharacters = new char[8]
    {
      '\\',
      ':',
      '*',
      '?',
      '"',
      '<',
      '>',
      '|'
    };
    private bool m_checkForIllegalChars;

    public DirectoryString()
    {
      this.m_site = "";
      this.m_separatedSite = new ArrayList();
    }

    public DirectoryString(string directory, bool checkForIllegalChars)
    {
      this.m_site = directory;
      this.m_checkForIllegalChars = checkForIllegalChars;
      this.m_separatedSite = this.CreateSeparatedString(directory);
    }

    private ArrayList CreateSeparatedString(string directory)
    {
      if (directory == null || directory.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
      ArrayList arrayList = new ArrayList();
      string[] strArray = directory.Split(DirectoryString.m_separators);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != null && !strArray[index].Equals(""))
        {
          if (strArray[index].Equals("*"))
          {
            if (index != strArray.Length - 1)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
            arrayList.Add((object) strArray[index]);
          }
          else
          {
            if (this.m_checkForIllegalChars && strArray[index].IndexOfAny(DirectoryString.m_illegalDirectoryCharacters) != -1)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
            arrayList.Add((object) strArray[index]);
          }
        }
      }
      return arrayList;
    }

    public virtual bool IsSubsetOf(DirectoryString operand)
    {
      return this.IsSubsetOf(operand, true);
    }

    public virtual bool IsSubsetOf(DirectoryString operand, bool ignoreCase)
    {
      if (operand == null)
        return false;
      if (operand.m_separatedSite.Count == 0)
      {
        if (this.m_separatedSite.Count == 0)
          return true;
        if (this.m_separatedSite.Count > 0)
          return string.Compare((string) this.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
        return false;
      }
      if (this.m_separatedSite.Count == 0)
        return string.Compare((string) operand.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
      return this.IsSubsetOf((SiteString) operand, ignoreCase);
    }
  }
}
