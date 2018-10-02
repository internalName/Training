// Decompiled with JetBrains decompiler
// Type: System.IO.StringResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.IO
{
  internal class StringResultHandler : SearchResultHandler<string>
  {
    private bool _includeFiles;
    private bool _includeDirs;

    internal StringResultHandler(bool includeFiles, bool includeDirs)
    {
      this._includeFiles = includeFiles;
      this._includeDirs = includeDirs;
    }

    [SecurityCritical]
    internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
    {
      if (this._includeFiles && findData.IsFile)
        return true;
      if (this._includeDirs)
        return findData.IsNormalDirectory;
      return false;
    }

    [SecurityCritical]
    internal override string CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      return Path.CombineNoChecks(searchData.userPath, findData.cFileName);
    }
  }
}
