// Decompiled with JetBrains decompiler
// Type: System.Security.Util.URLString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Util
{
  [Serializable]
  internal sealed class URLString : SiteString
  {
    private string m_protocol;
    [OptionalField(VersionAdded = 2)]
    private string m_userpass;
    private SiteString m_siteString;
    private int m_port;
    private LocalSiteString m_localSite;
    private DirectoryString m_directory;
    private const string m_defaultProtocol = "file";
    [OptionalField(VersionAdded = 2)]
    private bool m_parseDeferred;
    [OptionalField(VersionAdded = 2)]
    private string m_urlOriginal;
    [OptionalField(VersionAdded = 2)]
    private bool m_parsedOriginal;
    [OptionalField(VersionAdded = 3)]
    private bool m_isUncShare;
    private string m_fullurl;

    [OnDeserialized]
    public void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_urlOriginal != null)
        return;
      this.m_parseDeferred = false;
      this.m_parsedOriginal = false;
      this.m_userpass = "";
      this.m_urlOriginal = this.m_fullurl;
      this.m_fullurl = (string) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.DoDeferredParse();
      this.m_fullurl = this.m_urlOriginal;
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_fullurl = (string) null;
    }

    public URLString()
    {
      this.m_protocol = "";
      this.m_userpass = "";
      this.m_siteString = new SiteString();
      this.m_port = -1;
      this.m_localSite = (LocalSiteString) null;
      this.m_directory = new DirectoryString();
      this.m_parseDeferred = false;
    }

    private void DoDeferredParse()
    {
      if (!this.m_parseDeferred)
        return;
      this.ParseString(this.m_urlOriginal, this.m_parsedOriginal);
      this.m_parseDeferred = false;
    }

    public URLString(string url)
      : this(url, false, false)
    {
    }

    public URLString(string url, bool parsed)
      : this(url, parsed, false)
    {
    }

    internal URLString(string url, bool parsed, bool doDeferredParsing)
    {
      this.m_port = -1;
      this.m_userpass = "";
      this.DoFastChecks(url);
      this.m_urlOriginal = url;
      this.m_parsedOriginal = parsed;
      this.m_parseDeferred = true;
      if (!doDeferredParsing)
        return;
      this.DoDeferredParse();
    }

    private string UnescapeURL(string url)
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(url.Length);
      int startIndex1 = 0;
      int num1 = -1;
      int startIndex2 = url.IndexOf('[', startIndex1);
      if (startIndex2 != -1)
        num1 = url.IndexOf(']', startIndex2);
      while (true)
      {
        int num2 = url.IndexOf('%', startIndex1);
        if (num2 != -1)
        {
          if (num2 > startIndex2 && num2 < num1)
          {
            stringBuilder = stringBuilder.Append(url, startIndex1, num1 - startIndex1 + 1);
            startIndex1 = num1 + 1;
          }
          else if (url.Length - num2 >= 2)
          {
            if (url[num2 + 1] == 'u' || url[num2 + 1] == 'U')
            {
              if (url.Length - num2 >= 6)
              {
                try
                {
                  char ch = (char) (Hex.ConvertHexDigit(url[num2 + 2]) << 12 | Hex.ConvertHexDigit(url[num2 + 3]) << 8 | Hex.ConvertHexDigit(url[num2 + 4]) << 4 | Hex.ConvertHexDigit(url[num2 + 5]));
                  stringBuilder = stringBuilder.Append(url, startIndex1, num2 - startIndex1).Append(ch);
                }
                catch (ArgumentException ex)
                {
                  throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
                }
                startIndex1 = num2 + 6;
              }
              else
                goto label_10;
            }
            else if (url.Length - num2 >= 3)
            {
              try
              {
                char ch = (char) (Hex.ConvertHexDigit(url[num2 + 1]) << 4 | Hex.ConvertHexDigit(url[num2 + 2]));
                stringBuilder = stringBuilder.Append(url, startIndex1, num2 - startIndex1).Append(ch);
              }
              catch (ArgumentException ex)
              {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
              }
              startIndex1 = num2 + 3;
            }
            else
              goto label_15;
          }
          else
            goto label_7;
        }
        else
          break;
      }
      return StringBuilderCache.GetStringAndRelease(stringBuilder.Append(url, startIndex1, url.Length - startIndex1));
label_7:
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
label_10:
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
label_15:
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
    }

    private string ParseProtocol(string url)
    {
      int length = url.IndexOf(':');
      string str;
      switch (length)
      {
        case -1:
          this.m_protocol = "file";
          str = url;
          break;
        case 0:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
        default:
          if (url.Length <= length + 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
          if (length == "file".Length && string.Compare(url, 0, "file", 0, length, StringComparison.OrdinalIgnoreCase) == 0)
          {
            this.m_protocol = "file";
            str = url.Substring(length + 1);
            this.m_isUncShare = true;
            break;
          }
          if (url[length + 1] != '\\')
          {
            if (url.Length <= length + 2 || url[length + 1] != '/' || url[length + 2] != '/')
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
            this.m_protocol = url.Substring(0, length);
            for (int index = 0; index < this.m_protocol.Length; ++index)
            {
              char ch = this.m_protocol[index];
              if ((ch < 'a' || ch > 'z') && (ch < 'A' || ch > 'Z') && ((ch < '0' || ch > '9') && (ch != '+' && ch != '.')) && ch != '-')
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
            }
            str = url.Substring(length + 3);
            break;
          }
          this.m_protocol = "file";
          str = url;
          break;
      }
      return str;
    }

    private string ParsePort(string url)
    {
      string str1 = url;
      char[] anyOf = new char[2]{ ':', '/' };
      int startIndex1 = 0;
      int num = str1.IndexOf('@');
      if (num != -1 && str1.IndexOf('/', 0, num) == -1)
      {
        this.m_userpass = str1.Substring(0, num);
        startIndex1 = num + 1;
      }
      int startIndex2 = -1;
      int startIndex3 = url.IndexOf('[', startIndex1);
      if (startIndex3 != -1)
        startIndex2 = url.IndexOf(']', startIndex3);
      int index = startIndex2 == -1 ? str1.IndexOfAny(anyOf, startIndex1) : str1.IndexOfAny(anyOf, startIndex2);
      string str2;
      if (index != -1 && str1[index] == ':')
      {
        if (str1[index + 1] < '0' || str1[index + 1] > '9')
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
        int startIndex4 = str1.IndexOf('/', startIndex1);
        if (startIndex4 == -1)
        {
          this.m_port = int.Parse(str1.Substring(index + 1), (IFormatProvider) CultureInfo.InvariantCulture);
          if (this.m_port < 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
          str2 = str1.Substring(startIndex1, index - startIndex1);
        }
        else
        {
          if (startIndex4 <= index)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
          this.m_port = int.Parse(str1.Substring(index + 1, startIndex4 - index - 1), (IFormatProvider) CultureInfo.InvariantCulture);
          str2 = str1.Substring(startIndex1, index - startIndex1) + str1.Substring(startIndex4);
        }
      }
      else
        str2 = str1.Substring(startIndex1);
      return str2;
    }

    internal static string PreProcessForExtendedPathRemoval(string url, bool isFileUrl)
    {
      return URLString.PreProcessForExtendedPathRemoval(true, url, isFileUrl);
    }

    internal static string PreProcessForExtendedPathRemoval(bool checkPathLength, string url, bool isFileUrl)
    {
      bool isUncShare = false;
      return URLString.PreProcessForExtendedPathRemoval(checkPathLength, url, isFileUrl, ref isUncShare);
    }

    private static string PreProcessForExtendedPathRemoval(string url, bool isFileUrl, ref bool isUncShare)
    {
      return URLString.PreProcessForExtendedPathRemoval(true, url, isFileUrl, ref isUncShare);
    }

    private static string PreProcessForExtendedPathRemoval(bool checkPathLength, string url, bool isFileUrl, ref bool isUncShare)
    {
      StringBuilder path = new StringBuilder(url);
      int indexA = 0;
      int startIndex = 0;
      int num;
      if (url.Length - indexA >= 4 && (string.Compare(url, indexA, "//?/", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, indexA, "//./", 0, 4, StringComparison.OrdinalIgnoreCase) == 0))
      {
        path.Remove(startIndex, 4);
        num = indexA + 4;
      }
      else
      {
        if (isFileUrl)
        {
          while (url[indexA] == '/')
          {
            ++indexA;
            ++startIndex;
          }
        }
        if (url.Length - indexA >= 4 && (string.Compare(url, indexA, "\\\\?\\", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, indexA, "\\\\?/", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || (string.Compare(url, indexA, "\\\\.\\", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, indexA, "\\\\./", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)))
        {
          path.Remove(startIndex, 4);
          num = indexA + 4;
        }
      }
      if (isFileUrl)
      {
        int length = 0;
        bool flag = false;
        for (; length < path.Length && (path[length] == '/' || path[length] == '\\'); ++length)
        {
          if (!flag && path[length] == '\\')
          {
            flag = true;
            if (length + 1 < path.Length && path[length + 1] == '\\')
              isUncShare = true;
          }
        }
        path.Remove(0, length);
        path.Replace('\\', '/');
      }
      if (checkPathLength)
        URLString.CheckPathTooLong(path);
      return path.ToString();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void CheckPathTooLong(StringBuilder path)
    {
      if (path.Length >= (AppContextSwitches.BlockLongPaths ? 260 : (int) short.MaxValue))
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
    }

    private string PreProcessURL(string url, bool isFileURL)
    {
      url = !isFileURL ? url.Replace('\\', '/') : URLString.PreProcessForExtendedPathRemoval(url, true, ref this.m_isUncShare);
      return url;
    }

    private void ParseFileURL(string url)
    {
      string str = url;
      int length = str.IndexOf('/');
      if (length != -1 && (length == 2 && str[length - 1] != ':' && str[length - 1] != '|' || length != 2) && length != str.Length - 1)
      {
        int num = str.IndexOf('/', length + 1);
        length = num == -1 ? -1 : num;
      }
      string strIn = length != -1 ? str.Substring(0, length) : str;
      if (strIn.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
      bool flag1;
      int index;
      if (strIn[0] == '\\' && strIn[1] == '\\')
      {
        flag1 = true;
        index = 2;
      }
      else
      {
        index = 0;
        flag1 = false;
      }
      bool flag2 = true;
      for (; index < strIn.Length; ++index)
      {
        char ch = strIn[index];
        if ((ch < 'A' || ch > 'Z') && (ch < 'a' || ch > 'z') && ((ch < '0' || ch > '9') && (ch != '-' && ch != '/')) && (ch != ':' && ch != '|' && (ch != '.' && ch != '*') && (ch != '$' && (!flag1 || ch != ' '))))
        {
          flag2 = false;
          break;
        }
      }
      string site = !flag2 ? strIn.ToUpper(CultureInfo.InvariantCulture) : string.SmallCharToUpper(strIn);
      this.m_localSite = new LocalSiteString(site);
      if (length == -1)
      {
        this.m_directory = site[site.Length - 1] != '*' ? new DirectoryString() : new DirectoryString("*", false);
      }
      else
      {
        string directory = str.Substring(length + 1);
        this.m_directory = directory.Length != 0 ? new DirectoryString(directory, true) : new DirectoryString();
      }
      this.m_siteString = (SiteString) null;
    }

    private void ParseNonFileURL(string url)
    {
      string site1 = url;
      int length = site1.IndexOf('/');
      if (length == -1)
      {
        this.m_localSite = (LocalSiteString) null;
        this.m_siteString = new SiteString(site1);
        this.m_directory = new DirectoryString();
      }
      else
      {
        string site2 = site1.Substring(0, length);
        this.m_localSite = (LocalSiteString) null;
        this.m_siteString = new SiteString(site2);
        string directory = site1.Substring(length + 1);
        if (directory.Length == 0)
          this.m_directory = new DirectoryString();
        else
          this.m_directory = new DirectoryString(directory, false);
      }
    }

    private void DoFastChecks(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url));
      if (url.Length == 0)
        throw new FormatException(Environment.GetResourceString("Format_StringZeroLength"));
    }

    private void ParseString(string url, bool parsed)
    {
      if (!parsed)
        url = this.UnescapeURL(url);
      string protocol = this.ParseProtocol(url);
      bool isFileURL = string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0;
      string url1 = this.PreProcessURL(protocol, isFileURL);
      if (isFileURL)
        this.ParseFileURL(url1);
      else
        this.ParseNonFileURL(this.ParsePort(url1));
    }

    public string Scheme
    {
      get
      {
        this.DoDeferredParse();
        return this.m_protocol;
      }
    }

    public string Host
    {
      get
      {
        this.DoDeferredParse();
        if (this.m_siteString != null)
          return this.m_siteString.ToString();
        return this.m_localSite.ToString();
      }
    }

    public string Port
    {
      get
      {
        this.DoDeferredParse();
        if (this.m_port == -1)
          return (string) null;
        return this.m_port.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    public string Directory
    {
      get
      {
        this.DoDeferredParse();
        return this.m_directory.ToString();
      }
    }

    public bool IsRelativeFileUrl
    {
      get
      {
        this.DoDeferredParse();
        if (!string.Equals(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) || this.m_isUncShare)
          return false;
        string str1 = this.m_localSite != null ? this.m_localSite.ToString() : (string) null;
        if (str1.EndsWith('*'))
          return false;
        string str2 = this.m_directory != null ? this.m_directory.ToString() : (string) null;
        if (str1 != null && str1.Length >= 2 && str1.EndsWith(':'))
          return string.IsNullOrEmpty(str2);
        return true;
      }
    }

    public string GetFileName()
    {
      this.DoDeferredParse();
      if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
        return (string) null;
      string str1 = this.Directory.Replace('/', '\\');
      string str2 = this.Host.Replace('/', '\\');
      int num = str2.IndexOf('\\');
      switch (num)
      {
        case -1:
          if (str2.Length != 2 || str2[1] != ':' && str2[1] != '|')
          {
            str2 = "\\\\" + str2;
            break;
          }
          break;
        case 2:
          if (num != 2 || str2[1] == ':' || str2[1] == '|')
            break;
          goto default;
        default:
          str2 = "\\\\" + str2;
          break;
      }
      return str2 + "\\" + str1;
    }

    public string GetDirectoryName()
    {
      this.DoDeferredParse();
      if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
        return (string) null;
      string str1 = this.Directory.Replace('/', '\\');
      int length1 = 0;
      for (int length2 = str1.Length; length2 > 0; --length2)
      {
        if (str1[length2 - 1] == '\\')
        {
          length1 = length2;
          break;
        }
      }
      string str2 = this.Host.Replace('/', '\\');
      int num = str2.IndexOf('\\');
      if (num == -1)
      {
        if (str2.Length != 2 || str2[1] != ':' && str2[1] != '|')
          str2 = "\\\\" + str2;
      }
      else if (num > 2 || num == 2 && str2[1] != ':' && str2[1] != '|')
        str2 = "\\\\" + str2;
      string str3 = str2 + "\\";
      if (length1 > 0)
        str3 += str1.Substring(0, length1);
      return str3;
    }

    public override SiteString Copy()
    {
      return (SiteString) new URLString(this.m_urlOriginal, this.m_parsedOriginal);
    }

    public override bool IsSubsetOf(SiteString site)
    {
      if (site == null)
        return false;
      URLString urlString1 = site as URLString;
      if (urlString1 == null)
        return false;
      this.DoDeferredParse();
      urlString1.DoDeferredParse();
      URLString urlString2 = this.SpecialNormalizeUrl();
      URLString urlString3 = urlString1.SpecialNormalizeUrl();
      if (string.Compare(urlString2.m_protocol, urlString3.m_protocol, StringComparison.OrdinalIgnoreCase) != 0 || !urlString2.m_directory.IsSubsetOf(urlString3.m_directory))
        return false;
      if (urlString2.m_localSite != null)
        return urlString2.m_localSite.IsSubsetOf(urlString3.m_localSite);
      if (urlString2.m_port != urlString3.m_port || urlString3.m_siteString == null)
        return false;
      return urlString2.m_siteString.IsSubsetOf(urlString3.m_siteString);
    }

    public override string ToString()
    {
      return this.m_urlOriginal;
    }

    public override bool Equals(object o)
    {
      this.DoDeferredParse();
      if (o == null || !(o is URLString))
        return false;
      return this.Equals((URLString) o);
    }

    public override int GetHashCode()
    {
      this.DoDeferredParse();
      TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
      int num = 0;
      if (this.m_protocol != null)
        num = textInfo.GetCaseInsensitiveHashCode(this.m_protocol);
      return (this.m_localSite == null ? num ^ this.m_siteString.GetHashCode() : num ^ this.m_localSite.GetHashCode()) ^ this.m_directory.GetHashCode();
    }

    public bool Equals(URLString url)
    {
      return URLString.CompareUrls(this, url);
    }

    public static bool CompareUrls(URLString url1, URLString url2)
    {
      if (url1 == null && url2 == null)
        return true;
      if (url1 == null || url2 == null)
        return false;
      url1.DoDeferredParse();
      url2.DoDeferredParse();
      URLString urlString1 = url1.SpecialNormalizeUrl();
      URLString urlString2 = url2.SpecialNormalizeUrl();
      if (string.Compare(urlString1.m_protocol, urlString2.m_protocol, StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      if (string.Compare(urlString1.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0)
      {
        if (!urlString1.m_localSite.IsSubsetOf(urlString2.m_localSite) || !urlString2.m_localSite.IsSubsetOf(urlString1.m_localSite))
          return false;
      }
      else if (string.Compare(urlString1.m_userpass, urlString2.m_userpass, StringComparison.Ordinal) != 0 || !urlString1.m_siteString.IsSubsetOf(urlString2.m_siteString) || (!urlString2.m_siteString.IsSubsetOf(urlString1.m_siteString) || url1.m_port != url2.m_port))
        return false;
      return urlString1.m_directory.IsSubsetOf(urlString2.m_directory) && urlString2.m_directory.IsSubsetOf(urlString1.m_directory);
    }

    internal string NormalizeUrl()
    {
      this.DoDeferredParse();
      StringBuilder stringBuilder1 = StringBuilderCache.Acquire(16);
      StringBuilder sb;
      if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0)
      {
        sb = stringBuilder1.AppendFormat("FILE:///{0}/{1}", (object) this.m_localSite.ToString(), (object) this.m_directory.ToString());
      }
      else
      {
        StringBuilder stringBuilder2 = stringBuilder1.AppendFormat("{0}://{1}{2}", (object) this.m_protocol, (object) this.m_userpass, (object) this.m_siteString.ToString());
        if (this.m_port != -1)
          stringBuilder2 = stringBuilder2.AppendFormat("{0}", (object) this.m_port);
        sb = stringBuilder2.AppendFormat("/{0}", (object) this.m_directory.ToString());
      }
      return StringBuilderCache.GetStringAndRelease(sb).ToUpper(CultureInfo.InvariantCulture);
    }

    [SecuritySafeCritical]
    internal URLString SpecialNormalizeUrl()
    {
      this.DoDeferredParse();
      if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
        return this;
      string driveLetter = this.m_localSite.ToString();
      if (driveLetter.Length != 2 || driveLetter[1] != '|' && driveLetter[1] != ':')
        return this;
      string s = (string) null;
      URLString.GetDeviceName(driveLetter, JitHelpers.GetStringHandleOnStack(ref s));
      if (s == null)
        return this;
      if (s.IndexOf("://", StringComparison.Ordinal) != -1)
      {
        URLString urlString = new URLString(s + "/" + this.m_directory.ToString());
        urlString.DoDeferredParse();
        return urlString;
      }
      URLString urlString1 = new URLString("file://" + s + "/" + this.m_directory.ToString());
      urlString1.DoDeferredParse();
      return urlString1;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetDeviceName(string driveLetter, StringHandleOnStack retDeviceName);
  }
}
