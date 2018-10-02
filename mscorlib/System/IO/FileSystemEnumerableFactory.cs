// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemEnumerableFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.IO
{
  internal static class FileSystemEnumerableFactory
  {
    internal static IEnumerable<string> CreateFileNameIterator(string path, string originalUserPath, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
    {
      SearchResultHandler<string> resultHandler = (SearchResultHandler<string>) new StringResultHandler(includeFiles, includeDirs);
      return (IEnumerable<string>) new FileSystemEnumerableIterator<string>(path, originalUserPath, searchPattern, searchOption, resultHandler, checkHost);
    }

    internal static IEnumerable<FileInfo> CreateFileInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
    {
      SearchResultHandler<FileInfo> resultHandler = (SearchResultHandler<FileInfo>) new FileInfoResultHandler();
      return (IEnumerable<FileInfo>) new FileSystemEnumerableIterator<FileInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
    }

    internal static IEnumerable<DirectoryInfo> CreateDirectoryInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
    {
      SearchResultHandler<DirectoryInfo> resultHandler = (SearchResultHandler<DirectoryInfo>) new DirectoryInfoResultHandler();
      return (IEnumerable<DirectoryInfo>) new FileSystemEnumerableIterator<DirectoryInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
    }

    internal static IEnumerable<FileSystemInfo> CreateFileSystemInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
    {
      SearchResultHandler<FileSystemInfo> resultHandler = (SearchResultHandler<FileSystemInfo>) new FileSystemInfoResultHandler();
      return (IEnumerable<FileSystemInfo>) new FileSystemEnumerableIterator<FileSystemInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
    }
  }
}
