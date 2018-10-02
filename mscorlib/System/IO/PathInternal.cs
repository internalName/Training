// Decompiled with JetBrains decompiler
// Type: System.IO.PathInternal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.IO
{
  internal static class PathInternal
  {
    internal static readonly int MaxComponentLength = (int) byte.MaxValue;
    internal static readonly char[] InvalidPathChars = new char[36]
    {
      '"',
      '<',
      '>',
      '|',
      char.MinValue,
      '\x0001',
      '\x0002',
      '\x0003',
      '\x0004',
      '\x0005',
      '\x0006',
      '\a',
      '\b',
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      '\x000E',
      '\x000F',
      '\x0010',
      '\x0011',
      '\x0012',
      '\x0013',
      '\x0014',
      '\x0015',
      '\x0016',
      '\x0017',
      '\x0018',
      '\x0019',
      '\x001A',
      '\x001B',
      '\x001C',
      '\x001D',
      '\x001E',
      '\x001F'
    };
    internal const string ExtendedPathPrefix = "\\\\?\\";
    internal const string UncPathPrefix = "\\\\";
    internal const string UncExtendedPrefixToInsert = "?\\UNC\\";
    internal const string UncExtendedPathPrefix = "\\\\?\\UNC\\";
    internal const string DevicePathPrefix = "\\\\.\\";
    internal const int DevicePrefixLength = 4;
    internal const int MaxShortPath = 260;
    internal const int MaxShortDirectoryPath = 248;
    internal const int MaxLongPath = 32767;

    internal static bool HasInvalidVolumeSeparator(string path)
    {
      int index = !AppContextSwitches.UseLegacyPathHandling && PathInternal.IsExtended(path) ? "\\\\?\\".Length : PathInternal.PathStartSkip(path);
      return path.Length > index && (int) path[index] == (int) Path.VolumeSeparatorChar || path.Length >= index + 2 && (int) path[index + 1] == (int) Path.VolumeSeparatorChar && !PathInternal.IsValidDriveChar(path[index]) || path.Length > index + 2 && path.IndexOf(Path.VolumeSeparatorChar, index + 2) != -1;
    }

    internal static bool StartsWithOrdinal(StringBuilder builder, string value, bool ignoreCase = false)
    {
      if (value == null || builder.Length < value.Length)
        return false;
      if (ignoreCase)
      {
        for (int index = 0; index < value.Length; ++index)
        {
          if ((int) char.ToUpperInvariant(builder[index]) != (int) char.ToUpperInvariant(value[index]))
            return false;
        }
      }
      else
      {
        for (int index = 0; index < value.Length; ++index)
        {
          if ((int) builder[index] != (int) value[index])
            return false;
        }
      }
      return true;
    }

    internal static bool IsValidDriveChar(char value)
    {
      if (value >= 'A' && value <= 'Z')
        return true;
      if (value >= 'a')
        return value <= 'z';
      return false;
    }

    internal static bool IsPathTooLong(string fullPath)
    {
      if (AppContextSwitches.BlockLongPaths && (AppContextSwitches.UseLegacyPathHandling || !PathInternal.IsExtended(fullPath)))
        return fullPath.Length >= 260;
      return fullPath.Length >= (int) short.MaxValue;
    }

    internal static bool AreSegmentsTooLong(string fullPath)
    {
      int length = fullPath.Length;
      int num = 0;
      for (int index = 0; index < length; ++index)
      {
        if (PathInternal.IsDirectorySeparator(fullPath[index]))
        {
          if (index - num > PathInternal.MaxComponentLength)
            return true;
          num = index;
        }
      }
      return length - 1 - num > PathInternal.MaxComponentLength;
    }

    internal static bool IsDirectoryTooLong(string fullPath)
    {
      if (AppContextSwitches.BlockLongPaths && (AppContextSwitches.UseLegacyPathHandling || !PathInternal.IsExtended(fullPath)))
        return fullPath.Length >= 248;
      return PathInternal.IsPathTooLong(fullPath);
    }

    internal static string EnsureExtendedPrefix(string path)
    {
      if (PathInternal.IsPartiallyQualified(path) || PathInternal.IsDevice(path))
        return path;
      if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
        return path.Insert(2, "?\\UNC\\");
      return "\\\\?\\" + path;
    }

    internal static string RemoveExtendedPrefix(string path)
    {
      if (!PathInternal.IsExtended(path))
        return path;
      if (PathInternal.IsExtendedUnc(path))
        return path.Remove(2, 6);
      return path.Substring(4);
    }

    internal static StringBuilder RemoveExtendedPrefix(StringBuilder path)
    {
      if (!PathInternal.IsExtended(path))
        return path;
      if (PathInternal.IsExtendedUnc(path))
        return path.Remove(2, 6);
      return path.Remove(0, 4);
    }

    internal static bool IsDevice(string path)
    {
      if (PathInternal.IsExtended(path))
        return true;
      if (path.Length >= 4 && PathInternal.IsDirectorySeparator(path[0]) && PathInternal.IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?'))
        return PathInternal.IsDirectorySeparator(path[3]);
      return false;
    }

    internal static bool IsDevice(StringBuffer path)
    {
      if (PathInternal.IsExtended(path))
        return true;
      if (path.Length >= 4U && PathInternal.IsDirectorySeparator(path[0U]) && PathInternal.IsDirectorySeparator(path[1U]) && (path[2U] == '.' || path[2U] == '?'))
        return PathInternal.IsDirectorySeparator(path[3U]);
      return false;
    }

    internal static bool IsExtended(string path)
    {
      if (path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?')
        return path[3] == '\\';
      return false;
    }

    internal static bool IsExtended(StringBuilder path)
    {
      if (path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?')
        return path[3] == '\\';
      return false;
    }

    internal static bool IsExtended(StringBuffer path)
    {
      if (path.Length >= 4U && path[0U] == '\\' && (path[1U] == '\\' || path[1U] == '?') && path[2U] == '?')
        return path[3U] == '\\';
      return false;
    }

    internal static bool IsExtendedUnc(string path)
    {
      if (path.Length >= "\\\\?\\UNC\\".Length && PathInternal.IsExtended(path) && (char.ToUpper(path[4]) == 'U' && char.ToUpper(path[5]) == 'N') && char.ToUpper(path[6]) == 'C')
        return path[7] == '\\';
      return false;
    }

    internal static bool IsExtendedUnc(StringBuilder path)
    {
      if (path.Length >= "\\\\?\\UNC\\".Length && PathInternal.IsExtended(path) && (char.ToUpper(path[4]) == 'U' && char.ToUpper(path[5]) == 'N') && char.ToUpper(path[6]) == 'C')
        return path[7] == '\\';
      return false;
    }

    internal static bool HasIllegalCharacters(string path, bool checkAdditional = false)
    {
      if (!AppContextSwitches.UseLegacyPathHandling && PathInternal.IsDevice(path))
        return false;
      return PathInternal.AnyPathHasIllegalCharacters(path, checkAdditional);
    }

    internal static bool AnyPathHasIllegalCharacters(string path, bool checkAdditional = false)
    {
      if (path.IndexOfAny(PathInternal.InvalidPathChars) >= 0)
        return true;
      if (checkAdditional)
        return PathInternal.AnyPathHasWildCardCharacters(path, 0);
      return false;
    }

    internal static bool HasWildCardCharacters(string path)
    {
      int startIndex = AppContextSwitches.UseLegacyPathHandling ? 0 : (PathInternal.IsDevice(path) ? "\\\\?\\".Length : 0);
      return PathInternal.AnyPathHasWildCardCharacters(path, startIndex);
    }

    internal static bool AnyPathHasWildCardCharacters(string path, int startIndex = 0)
    {
      for (int index = startIndex; index < path.Length; ++index)
      {
        switch (path[index])
        {
          case '*':
          case '?':
            return true;
          default:
            continue;
        }
      }
      return false;
    }

    [SecuritySafeCritical]
    internal static unsafe int GetRootLength(string path)
    {
      string str = path;
      char* path1 = (char*) str;
      if ((IntPtr) path1 != IntPtr.Zero)
        path1 += RuntimeHelpers.OffsetToStringData;
      return (int) PathInternal.GetRootLength(path1, (ulong) path.Length);
    }

    [SecuritySafeCritical]
    internal static unsafe uint GetRootLength(StringBuffer path)
    {
      if (path.Length == 0U)
        return 0;
      return PathInternal.GetRootLength(path.CharPointer, (ulong) path.Length);
    }

    [SecurityCritical]
    private static unsafe uint GetRootLength(char* path, ulong pathLength)
    {
      uint num1 = 0;
      uint num2 = 2;
      uint num3 = 2;
      bool flag1 = PathInternal.StartsWithOrdinal(path, pathLength, "\\\\?\\");
      bool flag2 = PathInternal.StartsWithOrdinal(path, pathLength, "\\\\?\\UNC\\");
      if (flag1)
      {
        if (flag2)
          num3 = (uint) "\\\\?\\UNC\\".Length;
        else
          num2 += (uint) "\\\\?\\".Length;
      }
      if (!flag1 | flag2 && pathLength > 0UL && PathInternal.IsDirectorySeparator(*path))
      {
        num1 = 1U;
        if (flag2 || pathLength > 1UL && PathInternal.IsDirectorySeparator((char) *(ushort*) ((IntPtr) path + 2)))
        {
          num1 = num3;
          int num4 = 2;
          while ((ulong) num1 < pathLength && (!PathInternal.IsDirectorySeparator((char) *(ushort*) ((IntPtr) path + (IntPtr) ((long) num1 * 2L))) || --num4 > 0))
            ++num1;
        }
      }
      else if (pathLength >= (ulong) num2 && (int) *(ushort*) ((IntPtr) path + (IntPtr) ((long) (num2 - 1U) * 2L)) == (int) Path.VolumeSeparatorChar)
      {
        num1 = num2;
        if (pathLength >= (ulong) (num2 + 1U) && PathInternal.IsDirectorySeparator((char) *(ushort*) ((IntPtr) path + (IntPtr) ((long) num2 * 2L))))
          ++num1;
      }
      return num1;
    }

    [SecurityCritical]
    private static unsafe bool StartsWithOrdinal(char* source, ulong sourceLength, string value)
    {
      if (sourceLength < (ulong) value.Length)
        return false;
      for (int index = 0; index < value.Length; ++index)
      {
        if ((int) value[index] != (int) source[index])
          return false;
      }
      return true;
    }

    internal static bool IsPartiallyQualified(string path)
    {
      if (path.Length < 2)
        return true;
      if (PathInternal.IsDirectorySeparator(path[0]))
      {
        if (path[1] != '?')
          return !PathInternal.IsDirectorySeparator(path[1]);
        return false;
      }
      if (path.Length >= 3 && (int) path[1] == (int) Path.VolumeSeparatorChar && PathInternal.IsDirectorySeparator(path[2]))
        return !PathInternal.IsValidDriveChar(path[0]);
      return true;
    }

    internal static bool IsPartiallyQualified(StringBuffer path)
    {
      if (path.Length < 2U)
        return true;
      if (PathInternal.IsDirectorySeparator(path[0U]))
      {
        if (path[1U] != '?')
          return !PathInternal.IsDirectorySeparator(path[1U]);
        return false;
      }
      if (path.Length >= 3U && (int) path[1U] == (int) Path.VolumeSeparatorChar && PathInternal.IsDirectorySeparator(path[2U]))
        return !PathInternal.IsValidDriveChar(path[0U]);
      return true;
    }

    internal static int PathStartSkip(string path)
    {
      int index = 0;
      while (index < path.Length && path[index] == ' ')
        ++index;
      if (index > 0 && index < path.Length && PathInternal.IsDirectorySeparator(path[index]) || index + 1 < path.Length && (int) path[index + 1] == (int) Path.VolumeSeparatorChar && PathInternal.IsValidDriveChar(path[index]))
        return index;
      return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsDirectorySeparator(char c)
    {
      if ((int) c != (int) Path.DirectorySeparatorChar)
        return (int) c == (int) Path.AltDirectorySeparatorChar;
      return true;
    }

    internal static string NormalizeDirectorySeparators(string path)
    {
      if (string.IsNullOrEmpty(path))
        return path;
      int index1 = PathInternal.PathStartSkip(path);
      if (index1 == 0)
      {
        bool flag = true;
        for (int index2 = 0; index2 < path.Length; ++index2)
        {
          char c = path[index2];
          if (PathInternal.IsDirectorySeparator(c) && ((int) c != (int) Path.DirectorySeparatorChar || index2 > 0 && index2 + 1 < path.Length && PathInternal.IsDirectorySeparator(path[index2 + 1])))
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return path;
      }
      StringBuilder sb = StringBuilderCache.Acquire(path.Length);
      if (PathInternal.IsDirectorySeparator(path[index1]))
      {
        ++index1;
        sb.Append(Path.DirectorySeparatorChar);
      }
      for (int index2 = index1; index2 < path.Length; ++index2)
      {
        char directorySeparatorChar = path[index2];
        if (PathInternal.IsDirectorySeparator(directorySeparatorChar))
        {
          if (index2 + 1 >= path.Length || !PathInternal.IsDirectorySeparator(path[index2 + 1]))
            directorySeparatorChar = Path.DirectorySeparatorChar;
          else
            continue;
        }
        sb.Append(directorySeparatorChar);
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }
  }
}
