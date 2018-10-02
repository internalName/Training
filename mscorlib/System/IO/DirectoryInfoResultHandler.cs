// Decompiled with JetBrains decompiler
// Type: System.IO.DirectoryInfoResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal class DirectoryInfoResultHandler : SearchResultHandler<DirectoryInfo>
  {
    [SecurityCritical]
    internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
    {
      return findData.IsNormalDirectory;
    }

    [SecurityCritical]
    internal override DirectoryInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      return DirectoryInfoResultHandler.CreateDirectoryInfo(searchData, ref findData);
    }

    [SecurityCritical]
    internal static DirectoryInfo CreateDirectoryInfo(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      string cFileName = findData.cFileName;
      string fullPath = Path.CombineNoChecks(searchData.fullPath, cFileName);
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          fullPath + "\\."
        }, false, false).Demand();
      DirectoryInfo directoryInfo = new DirectoryInfo(fullPath, cFileName);
      directoryInfo.InitializeFrom(ref findData);
      return directoryInfo;
    }
  }
}
