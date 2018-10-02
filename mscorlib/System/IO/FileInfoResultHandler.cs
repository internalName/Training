// Decompiled with JetBrains decompiler
// Type: System.IO.FileInfoResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal class FileInfoResultHandler : SearchResultHandler<FileInfo>
  {
    [SecurityCritical]
    internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
    {
      return findData.IsFile;
    }

    [SecurityCritical]
    internal override FileInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      return FileInfoResultHandler.CreateFileInfo(searchData, ref findData);
    }

    [SecurityCritical]
    internal static FileInfo CreateFileInfo(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      string cFileName = findData.cFileName;
      string fullPath = Path.CombineNoChecks(searchData.fullPath, cFileName);
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          fullPath
        }, false, false).Demand();
      FileInfo fileInfo = new FileInfo(fullPath, cFileName);
      fileInfo.InitializeFrom(ref findData);
      return fileInfo;
    }
  }
}
