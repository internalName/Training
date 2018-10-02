// Decompiled with JetBrains decompiler
// Type: System.Security.Util.StringExpressionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Util
{
  [Serializable]
  internal class StringExpressionSet
  {
    protected static readonly char[] m_separators = new char[1]
    {
      ';'
    };
    protected static readonly char[] m_trimChars = new char[1]
    {
      ' '
    };
    protected static readonly char m_directorySeparator = '\\';
    protected static readonly char m_alternateDirectorySeparator = '/';
    [SecurityCritical]
    protected ArrayList m_list;
    protected bool m_ignoreCase;
    [SecurityCritical]
    protected string m_expressions;
    [SecurityCritical]
    protected string[] m_expressionsArray;
    protected bool m_throwOnRelative;

    public StringExpressionSet()
      : this(true, (string) null, false)
    {
    }

    public StringExpressionSet(string str)
      : this(true, str, false)
    {
    }

    public StringExpressionSet(bool ignoreCase, bool throwOnRelative)
      : this(ignoreCase, (string) null, throwOnRelative)
    {
    }

    [SecuritySafeCritical]
    public StringExpressionSet(bool ignoreCase, string str, bool throwOnRelative)
    {
      this.m_list = (ArrayList) null;
      this.m_ignoreCase = ignoreCase;
      this.m_throwOnRelative = throwOnRelative;
      if (str == null)
        this.m_expressions = (string) null;
      else
        this.AddExpressions(str);
    }

    protected virtual StringExpressionSet CreateNewEmpty()
    {
      return new StringExpressionSet();
    }

    [SecuritySafeCritical]
    public virtual StringExpressionSet Copy()
    {
      StringExpressionSet newEmpty = this.CreateNewEmpty();
      if (this.m_list != null)
        newEmpty.m_list = new ArrayList((ICollection) this.m_list);
      newEmpty.m_expressions = this.m_expressions;
      newEmpty.m_ignoreCase = this.m_ignoreCase;
      newEmpty.m_throwOnRelative = this.m_throwOnRelative;
      return newEmpty;
    }

    public void SetThrowOnRelative(bool throwOnRelative)
    {
      this.m_throwOnRelative = throwOnRelative;
    }

    private static string StaticProcessWholeString(string str)
    {
      return str.Replace(StringExpressionSet.m_alternateDirectorySeparator, StringExpressionSet.m_directorySeparator);
    }

    private static string StaticProcessSingleString(string str)
    {
      return str.Trim(StringExpressionSet.m_trimChars);
    }

    protected virtual string ProcessWholeString(string str)
    {
      return StringExpressionSet.StaticProcessWholeString(str);
    }

    protected virtual string ProcessSingleString(string str)
    {
      return StringExpressionSet.StaticProcessSingleString(str);
    }

    [SecurityCritical]
    public void AddExpressions(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      if (str.Length == 0)
        return;
      str = this.ProcessWholeString(str);
      this.m_expressions = this.m_expressions != null ? this.m_expressions + StringExpressionSet.m_separators[0].ToString() + str : str;
      this.m_expressionsArray = (string[]) null;
      string[] strArray = this.Split(str);
      if (this.m_list == null)
        this.m_list = new ArrayList();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != null && !strArray[index].Equals(""))
        {
          string path = this.ProcessSingleString(strArray[index]);
          int length = path.IndexOf(char.MinValue);
          if (length != -1)
            path = path.Substring(0, length);
          if (path != null && !path.Equals(""))
          {
            if (this.m_throwOnRelative)
            {
              if (Path.IsRelative(path))
                throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
              path = StringExpressionSet.CanonicalizePath(path);
            }
            this.m_list.Add((object) path);
          }
        }
      }
      this.Reduce();
    }

    [SecurityCritical]
    public void AddExpressions(string[] str, bool checkForDuplicates, bool needFullPath)
    {
      this.AddExpressions(StringExpressionSet.CreateListFromExpressions(str, needFullPath), checkForDuplicates);
    }

    [SecurityCritical]
    public void AddExpressions(ArrayList exprArrayList, bool checkForDuplicates)
    {
      this.m_expressionsArray = (string[]) null;
      this.m_expressions = (string) null;
      if (this.m_list != null)
        this.m_list.AddRange((ICollection) exprArrayList);
      else
        this.m_list = new ArrayList((ICollection) exprArrayList);
      if (!checkForDuplicates)
        return;
      this.Reduce();
    }

    [SecurityCritical]
    internal static ArrayList CreateListFromExpressions(string[] str, bool needFullPath)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < str.Length; ++index)
      {
        if (str[index] == null)
          throw new ArgumentNullException(nameof (str));
        string str1 = StringExpressionSet.StaticProcessWholeString(str[index]);
        if (str1 != null && str1.Length != 0)
        {
          string path = StringExpressionSet.StaticProcessSingleString(str1);
          int length = path.IndexOf(char.MinValue);
          if (length != -1)
            path = path.Substring(0, length);
          if (path != null && path.Length != 0)
          {
            if (PathInternal.IsPartiallyQualified(path))
              throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
            string str2 = StringExpressionSet.CanonicalizePath(path, needFullPath);
            arrayList.Add((object) str2);
          }
        }
      }
      return arrayList;
    }

    [SecurityCritical]
    protected void CheckList()
    {
      if (this.m_list != null || this.m_expressions == null)
        return;
      this.CreateList();
    }

    protected string[] Split(string expressions)
    {
      if (!this.m_throwOnRelative)
        return expressions.Split(StringExpressionSet.m_separators);
      List<string> stringList = new List<string>();
      string[] strArray1 = expressions.Split('"');
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        if (index1 % 2 == 0)
        {
          string[] strArray2 = strArray1[index1].Split(';');
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            if (strArray2[index2] != null && !strArray2[index2].Equals(""))
              stringList.Add(strArray2[index2]);
          }
        }
        else
          stringList.Add(strArray1[index1]);
      }
      string[] strArray3 = new string[stringList.Count];
      IEnumerator enumerator = (IEnumerator) stringList.GetEnumerator();
      int num = 0;
      while (enumerator.MoveNext())
        strArray3[num++] = (string) enumerator.Current;
      return strArray3;
    }

    [SecurityCritical]
    protected void CreateList()
    {
      string[] strArray = this.Split(this.m_expressions);
      this.m_list = new ArrayList();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != null && !strArray[index].Equals(""))
        {
          string path = this.ProcessSingleString(strArray[index]);
          int length = path.IndexOf(char.MinValue);
          if (length != -1)
            path = path.Substring(0, length);
          if (path != null && !path.Equals(""))
          {
            if (this.m_throwOnRelative)
            {
              if (Path.IsRelative(path))
                throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
              path = StringExpressionSet.CanonicalizePath(path);
            }
            this.m_list.Add((object) path);
          }
        }
      }
    }

    [SecuritySafeCritical]
    public bool IsEmpty()
    {
      if (this.m_list == null)
        return this.m_expressions == null;
      return this.m_list.Count == 0;
    }

    [SecurityCritical]
    public bool IsSubsetOf(StringExpressionSet ses)
    {
      if (this.IsEmpty())
        return true;
      if (ses == null || ses.IsEmpty())
        return false;
      this.CheckList();
      ses.CheckList();
      for (int index = 0; index < this.m_list.Count; ++index)
      {
        if (!this.StringSubsetStringExpression((string) this.m_list[index], ses, this.m_ignoreCase))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    public bool IsSubsetOfPathDiscovery(StringExpressionSet ses)
    {
      if (this.IsEmpty())
        return true;
      if (ses == null || ses.IsEmpty())
        return false;
      this.CheckList();
      ses.CheckList();
      for (int index = 0; index < this.m_list.Count; ++index)
      {
        if (!StringExpressionSet.StringSubsetStringExpressionPathDiscovery((string) this.m_list[index], ses, this.m_ignoreCase))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    public StringExpressionSet Union(StringExpressionSet ses)
    {
      if (ses == null || ses.IsEmpty())
        return this.Copy();
      if (this.IsEmpty())
        return ses.Copy();
      this.CheckList();
      ses.CheckList();
      StringExpressionSet stringExpressionSet1 = ses.m_list.Count > this.m_list.Count ? ses : this;
      StringExpressionSet stringExpressionSet2 = ses.m_list.Count <= this.m_list.Count ? ses : this;
      StringExpressionSet stringExpressionSet3 = stringExpressionSet1.Copy();
      stringExpressionSet3.Reduce();
      for (int index = 0; index < stringExpressionSet2.m_list.Count; ++index)
        stringExpressionSet3.AddSingleExpressionNoDuplicates((string) stringExpressionSet2.m_list[index]);
      stringExpressionSet3.GenerateString();
      return stringExpressionSet3;
    }

    [SecurityCritical]
    public StringExpressionSet Intersect(StringExpressionSet ses)
    {
      if (this.IsEmpty() || ses == null || ses.IsEmpty())
        return this.CreateNewEmpty();
      this.CheckList();
      ses.CheckList();
      StringExpressionSet newEmpty = this.CreateNewEmpty();
      for (int index1 = 0; index1 < this.m_list.Count; ++index1)
      {
        for (int index2 = 0; index2 < ses.m_list.Count; ++index2)
        {
          if (this.StringSubsetString((string) this.m_list[index1], (string) ses.m_list[index2], this.m_ignoreCase))
          {
            if (newEmpty.m_list == null)
              newEmpty.m_list = new ArrayList();
            newEmpty.AddSingleExpressionNoDuplicates((string) this.m_list[index1]);
          }
          else if (this.StringSubsetString((string) ses.m_list[index2], (string) this.m_list[index1], this.m_ignoreCase))
          {
            if (newEmpty.m_list == null)
              newEmpty.m_list = new ArrayList();
            newEmpty.AddSingleExpressionNoDuplicates((string) ses.m_list[index2]);
          }
        }
      }
      newEmpty.GenerateString();
      return newEmpty;
    }

    [SecuritySafeCritical]
    protected void GenerateString()
    {
      if (this.m_list != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        IEnumerator enumerator = this.m_list.GetEnumerator();
        bool flag = true;
        while (enumerator.MoveNext())
        {
          if (!flag)
            stringBuilder.Append(StringExpressionSet.m_separators[0]);
          else
            flag = false;
          string current = (string) enumerator.Current;
          if (current != null)
          {
            int num = current.IndexOf(StringExpressionSet.m_separators[0]);
            if (num != -1)
              stringBuilder.Append('"');
            stringBuilder.Append(current);
            if (num != -1)
              stringBuilder.Append('"');
          }
        }
        this.m_expressions = stringBuilder.ToString();
      }
      else
        this.m_expressions = (string) null;
    }

    [SecurityCritical]
    public string UnsafeToString()
    {
      this.CheckList();
      this.Reduce();
      this.GenerateString();
      return this.m_expressions;
    }

    [SecurityCritical]
    public string[] UnsafeToStringArray()
    {
      if (this.m_expressionsArray == null && this.m_list != null)
        this.m_expressionsArray = (string[]) this.m_list.ToArray(typeof (string));
      return this.m_expressionsArray;
    }

    [SecurityCritical]
    private bool StringSubsetStringExpression(string left, StringExpressionSet right, bool ignoreCase)
    {
      for (int index = 0; index < right.m_list.Count; ++index)
      {
        if (this.StringSubsetString(left, (string) right.m_list[index], ignoreCase))
          return true;
      }
      return false;
    }

    [SecurityCritical]
    private static bool StringSubsetStringExpressionPathDiscovery(string left, StringExpressionSet right, bool ignoreCase)
    {
      for (int index = 0; index < right.m_list.Count; ++index)
      {
        if (StringExpressionSet.StringSubsetStringPathDiscovery(left, (string) right.m_list[index], ignoreCase))
          return true;
      }
      return false;
    }

    protected virtual bool StringSubsetString(string left, string right, bool ignoreCase)
    {
      StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
      if (right == null || left == null || (right.Length == 0 || left.Length == 0) || right.Length > left.Length)
        return false;
      if (right.Length == left.Length)
        return string.Compare(right, left, comparisonType) == 0;
      if (left.Length - right.Length == 1 && (int) left[left.Length - 1] == (int) StringExpressionSet.m_directorySeparator)
        return string.Compare(left, 0, right, 0, right.Length, comparisonType) == 0;
      if ((int) right[right.Length - 1] == (int) StringExpressionSet.m_directorySeparator || (int) left[right.Length] == (int) StringExpressionSet.m_directorySeparator)
        return string.Compare(right, 0, left, 0, right.Length, comparisonType) == 0;
      return false;
    }

    protected static bool StringSubsetStringPathDiscovery(string left, string right, bool ignoreCase)
    {
      StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
      if (right == null || left == null || (right.Length == 0 || left.Length == 0))
        return false;
      if (right.Length == left.Length)
        return string.Compare(right, left, comparisonType) == 0;
      string strA;
      string strB;
      if (right.Length < left.Length)
      {
        strA = right;
        strB = left;
      }
      else
      {
        strA = left;
        strB = right;
      }
      if (string.Compare(strA, 0, strB, 0, strA.Length, comparisonType) != 0)
        return false;
      if (strA.Length == 3 && strA.EndsWith(":\\", StringComparison.Ordinal) && (strA[0] >= 'A' && strA[0] <= 'Z' || strA[0] >= 'a' && strA[0] <= 'z'))
        return true;
      return (int) strB[strA.Length] == (int) StringExpressionSet.m_directorySeparator;
    }

    [SecuritySafeCritical]
    protected void AddSingleExpressionNoDuplicates(string expression)
    {
      int index = 0;
      this.m_expressionsArray = (string[]) null;
      this.m_expressions = (string) null;
      if (this.m_list == null)
        this.m_list = new ArrayList();
      while (index < this.m_list.Count)
      {
        if (this.StringSubsetString((string) this.m_list[index], expression, this.m_ignoreCase))
        {
          this.m_list.RemoveAt(index);
        }
        else
        {
          if (this.StringSubsetString(expression, (string) this.m_list[index], this.m_ignoreCase))
            return;
          ++index;
        }
      }
      this.m_list.Add((object) expression);
    }

    [SecurityCritical]
    protected void Reduce()
    {
      this.CheckList();
      if (this.m_list == null)
        return;
      for (int index1 = 0; index1 < this.m_list.Count - 1; ++index1)
      {
        int index2 = index1 + 1;
        while (index2 < this.m_list.Count)
        {
          if (this.StringSubsetString((string) this.m_list[index2], (string) this.m_list[index1], this.m_ignoreCase))
            this.m_list.RemoveAt(index2);
          else if (this.StringSubsetString((string) this.m_list[index1], (string) this.m_list[index2], this.m_ignoreCase))
          {
            this.m_list[index1] = this.m_list[index2];
            this.m_list.RemoveAt(index2);
            index2 = index1 + 1;
          }
          else
            ++index2;
        }
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetLongPathName(string path, StringHandleOnStack retLongPath);

    [SecurityCritical]
    internal static string CanonicalizePath(string path)
    {
      return StringExpressionSet.CanonicalizePath(path, true);
    }

    [SecurityCritical]
    internal static string CanonicalizePath(string path, bool needFullPath)
    {
      if (needFullPath)
      {
        string str = Path.GetFullPathInternal(path);
        if (path.EndsWith(StringExpressionSet.m_directorySeparator.ToString() + ".", StringComparison.Ordinal))
          str = !str.EndsWith(StringExpressionSet.m_directorySeparator) ? str + StringExpressionSet.m_directorySeparator.ToString() + "." : str + ".";
        path = str;
      }
      else if (path.IndexOf('~') != -1)
      {
        string s = (string) null;
        StringExpressionSet.GetLongPathName(path, JitHelpers.GetStringHandleOnStack(ref s));
        path = s ?? path;
      }
      if (path.IndexOf(':', 2) != -1)
        throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
      return path;
    }
  }
}
