// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemInfoResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.IO
{
  internal class FileSystemInfoResultHandler : SearchResultHandler<FileSystemInfo>
  {
    [SecurityCritical]
    internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
    {
      if (!findData.IsFile)
        return findData.IsNormalDirectory;
      return true;
    }

    [SecurityCritical]
    internal override FileSystemInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
    {
      if (!findData.IsFile)
        return (FileSystemInfo) DirectoryInfoResultHandler.CreateDirectoryInfo(searchData, ref findData);
      return (FileSystemInfo) FileInfoResultHandler.CreateFileInfo(searchData, ref findData);
    }
  }
}
