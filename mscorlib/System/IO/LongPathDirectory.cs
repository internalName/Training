// Decompiled with JetBrains decompiler
// Type: System.IO.LongPathDirectory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.IO
{
  [ComVisible(false)]
  internal static class LongPathDirectory
  {
    [SecurityCritical]
    internal static void CreateDirectory(string path)
    {
      string fullPath = LongPath.NormalizePath(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, LongPathDirectory.GetDemandDir(fullPath, true), false, false);
      LongPathDirectory.InternalCreateDirectory(fullPath, path, (object) null);
    }

    [SecurityCritical]
    private static unsafe void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj)
    {
      DirectorySecurity directorySecurity = (DirectorySecurity) dirSecurityObj;
      int length = fullPath.Length;
      if (length >= 2 && Path.IsDirectorySeparator(fullPath[length - 1]))
        --length;
      int rootLength = LongPath.GetRootLength(fullPath);
      if (length == 2 && Path.IsDirectorySeparator(fullPath[1]))
        throw new IOException(Environment.GetResourceString("IO.IO_CannotCreateDirectory", (object) path));
      List<string> stringList = new List<string>();
      bool flag1 = false;
      if (length > rootLength)
      {
        for (int index = length - 1; index >= rootLength && !flag1; --index)
        {
          string path1 = fullPath.Substring(0, index + 1);
          if (!LongPathDirectory.InternalExists(path1))
            stringList.Add(path1);
          else
            flag1 = true;
          while (index > rootLength && (int) fullPath[index] != (int) Path.DirectorySeparatorChar && (int) fullPath[index] != (int) Path.AltDirectorySeparatorChar)
            --index;
        }
      }
      int count = stringList.Count;
      if (stringList.Count != 0 && !CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        string[] strArray = new string[stringList.Count];
        stringList.CopyTo(strArray, 0);
        for (int index = 0; index < strArray.Length; ++index)
        {
          // ISSUE: explicit reference operation
          ^ref strArray[index] += "\\.";
        }
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, directorySecurity == null ? AccessControlActions.None : AccessControlActions.Change, strArray, false, false);
      }
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (directorySecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = directorySecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      bool flag2 = true;
      int errorCode = 0;
      string maybeFullPath = path;
      while (stringList.Count > 0)
      {
        string str = stringList[stringList.Count - 1];
        stringList.RemoveAt(stringList.Count - 1);
        if (str.Length >= (int) short.MaxValue)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        flag2 = Win32Native.CreateDirectory(PathInternal.EnsureExtendedPrefix(str), securityAttributes);
        if (!flag2 && errorCode == 0)
        {
          int lastError = Marshal.GetLastWin32Error();
          if (lastError != 183)
            errorCode = lastError;
          else if (LongPathFile.InternalExists(str) || !LongPathDirectory.InternalExists(str, out lastError) && lastError == 5)
          {
            errorCode = lastError;
            try
            {
              FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, LongPathDirectory.GetDemandDir(str, true), false, false);
              maybeFullPath = str;
            }
            catch (SecurityException ex)
            {
            }
          }
        }
      }
      if (count == 0 && !flag1)
      {
        if (LongPathDirectory.InternalExists(LongPathDirectory.InternalGetDirectoryRoot(fullPath)))
          return;
        __Error.WinIOError(3, LongPathDirectory.InternalGetDirectoryRoot(path));
      }
      else
      {
        if (flag2 || errorCode == 0)
          return;
        __Error.WinIOError(errorCode, maybeFullPath);
      }
    }

    [SecurityCritical]
    internal static void Move(string sourceDirName, string destDirName)
    {
      string str = LongPath.NormalizePath(sourceDirName);
      string demandDir1 = LongPathDirectory.GetDemandDir(str, false);
      if (demandDir1.Length >= (int) short.MaxValue)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      string demandDir2 = LongPathDirectory.GetDemandDir(LongPath.NormalizePath(destDirName), false);
      if (demandDir2.Length >= (int) short.MaxValue)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, demandDir1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir2, false, false);
      if (string.Compare(demandDir1, demandDir2, StringComparison.OrdinalIgnoreCase) == 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
      if (string.Compare(LongPath.GetPathRoot(demandDir1), LongPath.GetPathRoot(demandDir2), StringComparison.OrdinalIgnoreCase) != 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
      if (Win32Native.MoveFile(PathInternal.EnsureExtendedPrefix(sourceDirName), PathInternal.EnsureExtendedPrefix(destDirName)))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
      {
        errorCode = 3;
        __Error.WinIOError(errorCode, str);
      }
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) sourceDirName), Win32Native.MakeHRFromErrorCode(errorCode));
      __Error.WinIOError(errorCode, string.Empty);
    }

    [SecurityCritical]
    internal static void Delete(string path, bool recursive)
    {
      LongPathDirectory.InternalDelete(LongPath.NormalizePath(path), path, recursive);
    }

    [SecurityCritical]
    private static void InternalDelete(string fullPath, string userPath, bool recursive)
    {
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, LongPathDirectory.GetDemandDir(fullPath, !recursive), false, false);
      string str = Path.AddLongPathPrefix(fullPath);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(str, ref data, false, true);
      switch (errorCode)
      {
        case 0:
          if ((data.fileAttributes & 1024) != 0)
            recursive = false;
          LongPathDirectory.DeleteHelper(str, userPath, recursive, true);
          break;
        case 2:
          errorCode = 3;
          goto default;
        default:
          __Error.WinIOError(errorCode, fullPath);
          goto case 0;
      }
    }

    [SecurityCritical]
    private static void DeleteHelper(string fullPath, string userPath, bool recursive, bool throwOnTopLevelDirectoryNotFound)
    {
      Exception exception = (Exception) null;
      if (recursive)
      {
        Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
        string str1 = fullPath;
        char directorySeparatorChar = Path.DirectorySeparatorChar;
        string str2 = directorySeparatorChar.ToString();
        int num = 4;
        string fileName;
        if (str1.EndsWith(str2, (StringComparison) num))
        {
          fileName = fullPath + "*";
        }
        else
        {
          string str3 = fullPath;
          directorySeparatorChar = Path.DirectorySeparatorChar;
          string str4 = directorySeparatorChar.ToString();
          string str5 = "*";
          fileName = str3 + str4 + str5;
        }
        int lastWin32Error;
        using (SafeFindHandle firstFile = Win32Native.FindFirstFile(fileName, ref wiN32FindData))
        {
          if (firstFile.IsInvalid)
          {
            lastWin32Error = Marshal.GetLastWin32Error();
            __Error.WinIOError(lastWin32Error, userPath);
          }
          do
          {
            if ((uint) (wiN32FindData.dwFileAttributes & 16) > 0U)
            {
              if (!wiN32FindData.IsRelativeDirectory)
              {
                if ((wiN32FindData.dwFileAttributes & 1024) == 0)
                {
                  string fullPath1 = LongPath.InternalCombine(fullPath, wiN32FindData.cFileName);
                  string userPath1 = LongPath.InternalCombine(userPath, wiN32FindData.cFileName);
                  try
                  {
                    LongPathDirectory.DeleteHelper(fullPath1, userPath1, recursive, false);
                  }
                  catch (Exception ex)
                  {
                    if (exception == null)
                      exception = ex;
                  }
                }
                else
                {
                  if (wiN32FindData.dwReserved0 == -1610612733)
                  {
                    string path1 = fullPath;
                    string cFileName = wiN32FindData.cFileName;
                    directorySeparatorChar = Path.DirectorySeparatorChar;
                    string str3 = directorySeparatorChar.ToString();
                    string path2 = cFileName + str3;
                    if (!Win32Native.DeleteVolumeMountPoint(LongPath.InternalCombine(path1, path2)))
                    {
                      lastWin32Error = Marshal.GetLastWin32Error();
                      if (lastWin32Error != 3)
                      {
                        try
                        {
                          __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                        }
                        catch (Exception ex)
                        {
                          if (exception == null)
                            exception = ex;
                        }
                      }
                    }
                  }
                  if (!Win32Native.RemoveDirectory(LongPath.InternalCombine(fullPath, wiN32FindData.cFileName)))
                  {
                    lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 3)
                    {
                      try
                      {
                        __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                      }
                      catch (Exception ex)
                      {
                        if (exception == null)
                          exception = ex;
                      }
                    }
                  }
                }
              }
            }
            else if (!Win32Native.DeleteFile(LongPath.InternalCombine(fullPath, wiN32FindData.cFileName)))
            {
              lastWin32Error = Marshal.GetLastWin32Error();
              if (lastWin32Error != 2)
              {
                try
                {
                  __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                }
                catch (Exception ex)
                {
                  if (exception == null)
                    exception = ex;
                }
              }
            }
          }
          while (Win32Native.FindNextFile(firstFile, ref wiN32FindData));
          lastWin32Error = Marshal.GetLastWin32Error();
        }
        if (exception != null)
          throw exception;
        if (lastWin32Error != 0 && lastWin32Error != 18)
          __Error.WinIOError(lastWin32Error, userPath);
      }
      if (Win32Native.RemoveDirectory(fullPath))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
        errorCode = 3;
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) userPath));
      if (errorCode == 3 && !throwOnTopLevelDirectoryNotFound)
        return;
      __Error.WinIOError(errorCode, userPath);
    }

    [SecurityCritical]
    internal static bool Exists(string path)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        string str = LongPath.NormalizePath(path);
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, LongPathDirectory.GetDemandDir(str, true), false, false);
        return LongPathDirectory.InternalExists(str);
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
      int lastError = 0;
      return LongPathDirectory.InternalExists(path, out lastError);
    }

    [SecurityCritical]
    internal static bool InternalExists(string path, out int lastError)
    {
      return Directory.InternalExists(Path.AddLongPathPrefix(path), out lastError);
    }

    private static string GetDemandDir(string fullPath, bool thisDirOnly)
    {
      fullPath = Path.RemoveLongPathPrefix(fullPath);
      return !thisDirOnly ? (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) || fullPath.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal) ? fullPath : fullPath + Path.DirectorySeparatorChar.ToString()) : (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) || fullPath.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal) ? fullPath + "." : fullPath + Path.DirectorySeparatorChar.ToString() + ".");
    }

    private static string InternalGetDirectoryRoot(string path)
    {
      return path?.Substring(0, LongPath.GetRootLength(path));
    }
  }
}
