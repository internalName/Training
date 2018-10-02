// Decompiled with JetBrains decompiler
// Type: System.IO.LongPath
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
  [ComVisible(false)]
  internal static class LongPath
  {
    [SecurityCritical]
    internal static string NormalizePath(string path)
    {
      return LongPath.NormalizePath(path, true);
    }

    [SecurityCritical]
    internal static string NormalizePath(string path, bool fullCheck)
    {
      return Path.NormalizePath(path, fullCheck, (int) short.MaxValue);
    }

    internal static string InternalCombine(string path1, string path2)
    {
      bool removed;
      string path = Path.InternalCombine(LongPath.TryRemoveLongPathPrefix(path1, out removed), path2);
      if (removed)
        path = Path.AddLongPathPrefix(path);
      return path;
    }

    internal static int GetRootLength(string path)
    {
      bool removed;
      int rootLength = Path.GetRootLength(LongPath.TryRemoveLongPathPrefix(path, out removed));
      if (removed)
        rootLength += 4;
      return rootLength;
    }

    internal static bool IsPathRooted(string path)
    {
      return Path.IsPathRooted(Path.RemoveLongPathPrefix(path));
    }

    [SecurityCritical]
    internal static string GetPathRoot(string path)
    {
      if (path == null)
        return (string) null;
      bool removed;
      string path1 = LongPath.NormalizePath(LongPath.TryRemoveLongPathPrefix(path, out removed), false);
      string path2 = path.Substring(0, LongPath.GetRootLength(path1));
      if (removed)
        path2 = Path.AddLongPathPrefix(path2);
      return path2;
    }

    [SecurityCritical]
    internal static string GetDirectoryName(string path)
    {
      if (path != null)
      {
        bool removed;
        string path1 = LongPath.TryRemoveLongPathPrefix(path, out removed);
        Path.CheckInvalidPathChars(path1, false);
        path = LongPath.NormalizePath(path1, false);
        int rootLength = LongPath.GetRootLength(path1);
        if (path1.Length > rootLength)
        {
          int length = path1.Length;
          if (length == rootLength)
            return (string) null;
          do
            ;
          while (length > rootLength && (int) path1[--length] != (int) Path.DirectorySeparatorChar && (int) path1[length] != (int) Path.AltDirectorySeparatorChar);
          string path2 = path1.Substring(0, length);
          if (removed)
            path2 = Path.AddLongPathPrefix(path2);
          return path2;
        }
      }
      return (string) null;
    }

    internal static string TryRemoveLongPathPrefix(string path, out bool removed)
    {
      removed = Path.HasLongPathPrefix(path);
      if (!removed)
        return path;
      return Path.RemoveLongPathPrefix(path);
    }
  }
}
