// Decompiled with JetBrains decompiler
// Type: System.IO.LongPathFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  [ComVisible(false)]
  internal static class LongPathFile
  {
    private const int ERROR_ACCESS_DENIED = 5;

    [SecurityCritical]
    internal static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
      string str1 = LongPath.NormalizePath(sourceFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, str1, false, false);
      string str2 = LongPath.NormalizePath(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, str2, false, false);
      LongPathFile.InternalCopy(str1, str2, sourceFileName, destFileName, overwrite);
    }

    [SecurityCritical]
    private static string InternalCopy(string fullSourceFileName, string fullDestFileName, string sourceFileName, string destFileName, bool overwrite)
    {
      fullSourceFileName = Path.AddLongPathPrefix(fullSourceFileName);
      fullDestFileName = Path.AddLongPathPrefix(fullDestFileName);
      if (!Win32Native.CopyFile(fullSourceFileName, fullDestFileName, !overwrite))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        string maybeFullPath = destFileName;
        if (lastWin32Error != 80)
        {
          using (SafeFileHandle file = Win32Native.UnsafeCreateFile(fullSourceFileName, int.MinValue, FileShare.Read, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 0, IntPtr.Zero))
          {
            if (file.IsInvalid)
              maybeFullPath = sourceFileName;
          }
          if (lastWin32Error == 5 && LongPathDirectory.InternalExists(fullDestFileName))
            throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", (object) destFileName), 5, fullDestFileName);
        }
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      return fullDestFileName;
    }

    [SecurityCritical]
    internal static void Delete(string path)
    {
      string str = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, str, false, false);
      if (Win32Native.DeleteFile(Path.AddLongPathPrefix(str)))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, str);
    }

    [SecurityCritical]
    internal static bool Exists(string path)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        path = LongPath.NormalizePath(path);
        if (path.Length > 0 && Path.IsDirectorySeparator(path[path.Length - 1]))
          return false;
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, path, false, false);
        return LongPathFile.InternalExists(path);
      }
      catch (ArgumentException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      catch (SecurityException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      return false;
    }

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      return File.InternalExists(Path.AddLongPathPrefix(path));
    }

    [SecurityCritical]
    internal static DateTimeOffset GetCreationTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, str, false, false);
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(path1, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc(data.ftCreationTime.ToTicks()).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static DateTimeOffset GetLastAccessTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, str, false, false);
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(path1, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc(data.ftLastAccessTime.ToTicks()).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static DateTimeOffset GetLastWriteTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, str, false, false);
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(path1, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc(data.ftLastWriteTime.ToTicks()).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static void Move(string sourceFileName, string destFileName)
    {
      string str1 = LongPath.NormalizePath(sourceFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, str1, false, false);
      string str2 = LongPath.NormalizePath(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, str2, false, false);
      if (!LongPathFile.InternalExists(str1))
        __Error.WinIOError(2, str1);
      if (Win32Native.MoveFile(Path.AddLongPathPrefix(str1), Path.AddLongPathPrefix(str2)))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    internal static long GetLength(string path)
    {
      string str = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, str, false, false);
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(path1, ref data, false, true);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, path);
      if ((data.fileAttributes & 16) != 0)
        __Error.WinIOError(2, path);
      return (long) data.fileSizeHigh << 32 | (long) data.fileSizeLow & (long) uint.MaxValue;
    }
  }
}
