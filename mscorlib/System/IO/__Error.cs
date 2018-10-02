// Decompiled with JetBrains decompiler
// Type: System.IO.__Error
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal static class __Error
  {
    internal const int ERROR_FILE_NOT_FOUND = 2;
    internal const int ERROR_PATH_NOT_FOUND = 3;
    internal const int ERROR_ACCESS_DENIED = 5;
    internal const int ERROR_INVALID_PARAMETER = 87;

    internal static void EndOfFile()
    {
      throw new EndOfStreamException(Environment.GetResourceString("IO.EOF_ReadBeyondEOF"));
    }

    internal static void FileNotOpen()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_FileClosed"));
    }

    internal static void StreamIsClosed()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
    }

    internal static void MemoryStreamNotExpandable()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_MemStreamNotExpandable"));
    }

    internal static void ReaderClosed()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ReaderClosed"));
    }

    internal static void ReadNotSupported()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
    }

    internal static void SeekNotSupported()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    internal static void WrongAsyncResult()
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_WrongAsyncResult"));
    }

    internal static void EndReadCalledTwice()
    {
      throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EndReadCalledMultiple"));
    }

    internal static void EndWriteCalledTwice()
    {
      throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EndWriteCalledMultiple"));
    }

    [SecurityCritical]
    internal static string GetDisplayablePath(string path, bool isInvalidPath)
    {
      if (string.IsNullOrEmpty(path))
        return string.Empty;
      if (path.Length < 2 || PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
        return path;
      bool flag = false;
      try
      {
        if (!isInvalidPath)
        {
          FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, path, false, false);
          flag = true;
        }
      }
      catch (SecurityException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      if (!flag)
        path = !Path.IsDirectorySeparator(path[path.Length - 1]) ? Path.GetFileName(path) : Environment.GetResourceString("IO.IO_NoPermissionToDirectoryName");
      return path;
    }

    [SecuritySafeCritical]
    internal static void WinIOError()
    {
      __Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
    }

    [SecurityCritical]
    internal static void WinIOError(int errorCode, string maybeFullPath)
    {
      bool isInvalidPath = errorCode == 123 || errorCode == 161;
      string displayablePath = __Error.GetDisplayablePath(maybeFullPath, isInvalidPath);
      switch (errorCode)
      {
        case 2:
          if (displayablePath.Length == 0)
            throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound"));
          throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) displayablePath), displayablePath);
        case 3:
          if (displayablePath.Length == 0)
            throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_NoPathName"));
          throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) displayablePath));
        case 5:
          if (displayablePath.Length == 0)
            throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_IODenied_NoPathName"));
          throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) displayablePath));
        case 15:
          throw new DriveNotFoundException(Environment.GetResourceString("IO.DriveNotFound_Drive", (object) displayablePath));
        case 32:
          if (displayablePath.Length == 0)
            throw new IOException(Environment.GetResourceString("IO.IO_SharingViolation_NoFileName"), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
          throw new IOException(Environment.GetResourceString("IO.IO_SharingViolation_File", (object) displayablePath), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
        case 80:
          if (displayablePath.Length != 0)
            throw new IOException(Environment.GetResourceString("IO.IO_FileExists_Name", (object) displayablePath), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
          break;
        case 87:
          throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
        case 183:
          if (displayablePath.Length != 0)
            throw new IOException(Environment.GetResourceString("IO.IO_AlreadyExists_Name", (object) displayablePath), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
          break;
        case 206:
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        case 995:
          throw new OperationCanceledException();
      }
      throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
    }

    [SecuritySafeCritical]
    internal static void WinIODriveError(string driveName)
    {
      int lastWin32Error = Marshal.GetLastWin32Error();
      __Error.WinIODriveError(driveName, lastWin32Error);
    }

    [SecurityCritical]
    internal static void WinIODriveError(string driveName, int errorCode)
    {
      if (errorCode == 3 || errorCode == 15)
        throw new DriveNotFoundException(Environment.GetResourceString("IO.DriveNotFound_Drive", (object) driveName));
      __Error.WinIOError(errorCode, driveName);
    }

    internal static void WriteNotSupported()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
    }

    internal static void WriterClosed()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_WriterClosed"));
    }
  }
}
