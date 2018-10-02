// Decompiled with JetBrains decompiler
// Type: System.IO.Path
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>
  ///   Выполняет операции для экземпляров класса <see cref="T:System.String" />, содержащих сведения о пути к файлу или каталогу.
  ///    Эти операции выполняются межплатформенным способом.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Path
  {
    /// <summary>
    ///   Предоставляет символ, задаваемый платформой, для разделения уровней папок в строке пути, в которой отражена иерархическая организация файловой системы.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly char DirectorySeparatorChar = '\\';
    /// <summary>
    ///   Предоставляет дополнительный символ, задаваемый платформой, для разделения уровней каталогов в строке пути, в которой отражена иерархическая организация файловой системы.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly char AltDirectorySeparatorChar = '/';
    /// <summary>
    ///   Предоставляет разделитель томов, задаваемый платформой.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly char VolumeSeparatorChar = ':';
    /// <summary>
    ///   Предоставляет массив символов, задаваемых платформой, которые не могут быть указаны в аргументах строки пути, передаваемых в элементы класса <see cref="T:System.IO.Path" />.
    /// </summary>
    /// <returns>
    ///   Массив недопустимых символов пути для текущей платформы.
    /// </returns>
    [Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
    public static readonly char[] InvalidPathChars = new char[36]
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
    internal static readonly char[] TrimEndChars = LongPathHelper.s_trimEndChars;
    private static readonly char[] RealInvalidPathChars = PathInternal.InvalidPathChars;
    private static readonly char[] InvalidPathCharsWithAdditionalChecks = new char[38]
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
      '\x001F',
      '*',
      '?'
    };
    private static readonly char[] InvalidFileNameChars = new char[41]
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
      '\x001F',
      ':',
      '*',
      '?',
      '\\',
      '/'
    };
    /// <summary>
    ///   Разделитель, задаваемый платформой, который используется в переменных среды для разделения строк пути.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly char PathSeparator = ';';
    internal static readonly int MaxPath = 260;
    private static readonly int MaxDirectoryLength = PathInternal.MaxComponentLength;
    private static readonly char[] s_Base32Char = new char[32]
    {
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z',
      '0',
      '1',
      '2',
      '3',
      '4',
      '5'
    };
    internal const string DirectorySeparatorCharAsString = "\\";
    internal const int MAX_PATH = 260;
    internal const int MAX_DIRECTORY_PATH = 248;
    internal const int MaxLongPath = 32767;
    private const string LongPathPrefix = "\\\\?\\";
    private const string UNCPathPrefix = "\\\\";
    private const string UNCLongPathPrefixToInsert = "?\\UNC\\";
    private const string UNCLongPathPrefix = "\\\\?\\UNC\\";

    /// <summary>Изменяет расширение строки пути.</summary>
    /// <param name="path">
    ///   Сведения о пути, которые нужно изменить.
    ///    Путь не может содержать символы, определенные в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </param>
    /// <param name="extension">
    ///   Новое расширение (начинающееся с точки или без нее).
    ///    Задает <see langword="null" /> для удаления существующего расширения из параметра <paramref name="path" />.
    /// </param>
    /// <returns>
    ///   Измененные сведения о пути.
    /// 
    ///   В настольных системах, работающих под управлением Windows, сведения о пути возвращаются без изменений, если значение параметра <paramref name="path" /> равно <see langword="null" /> или пустой строке ("").
    ///    Если значение параметра <paramref name="extension" /> равно <see langword="null" />, возвращаемая строка содержит указанный путь без расширения.
    ///    Если <paramref name="path" /> не имеет расширения и значение параметра <paramref name="extension" /> не равно <see langword="null" />, возвращаемая строка пути содержит <paramref name="extension" />, добавленное в конец <paramref name="path" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ChangeExtension(string path, string extension)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path, false);
      string str = path;
      int length = path.Length;
      while (--length >= 0)
      {
        char ch = path[length];
        if (ch == '.')
        {
          str = path.Substring(0, length);
          break;
        }
        if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      if (extension != null && path.Length != 0)
      {
        if (extension.Length == 0 || extension[0] != '.')
          str += ".";
        str += extension;
      }
      return str;
    }

    /// <summary>
    ///   Возвращает для указанной строки пути сведения о каталоге.
    /// </summary>
    /// <param name="path">Путь к файлу или каталогу.</param>
    /// <returns>
    ///   Сведения о каталоге для <paramref name="path" />, или значение <see langword="null" />, если путь <paramref name="path" /> указывает на корневой каталог или равен NULL.
    ///    Возвращает <see cref="F:System.String.Empty" />, если параметр <paramref name="path" /> не содержит сведения о каталоге.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="path" /> пустой или содержит недопустимые символы либо только пробелы.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Имя параметра <paramref name="path" /> превышает максимально допустимую в системе длину.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetDirectoryName(string path)
    {
      return Path.InternalGetDirectoryName(path);
    }

    [SecuritySafeCritical]
    private static string InternalGetDirectoryName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        string str1 = Path.NormalizePath(path, false, AppContextSwitches.UseLegacyPathHandling);
        if (path.Length > 0)
        {
          if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
          {
            try
            {
              string str2 = Path.RemoveLongPathPrefix(path);
              int length = 0;
              while (length < str2.Length && str2[length] != '?' && str2[length] != '*')
                ++length;
              if (length > 0)
                Path.GetFullPath(str2.Substring(0, length));
            }
            catch (SecurityException ex)
            {
              if (path.IndexOf("~", StringComparison.Ordinal) != -1)
                str1 = Path.NormalizePath(path, false, false);
            }
            catch (PathTooLongException ex)
            {
            }
            catch (NotSupportedException ex)
            {
            }
            catch (IOException ex)
            {
            }
            catch (ArgumentException ex)
            {
            }
          }
        }
        path = str1;
        int rootLength = Path.GetRootLength(path);
        if (path.Length > rootLength)
        {
          int length = path.Length;
          if (length == rootLength)
            return (string) null;
          do
            ;
          while (length > rootLength && (int) path[--length] != (int) Path.DirectorySeparatorChar && (int) path[length] != (int) Path.AltDirectorySeparatorChar);
          return path.Substring(0, length);
        }
      }
      return (string) null;
    }

    internal static int GetRootLength(string path)
    {
      Path.CheckInvalidPathChars(path, false);
      if (AppContextSwitches.UseLegacyPathHandling)
        return Path.LegacyGetRootLength(path);
      return PathInternal.GetRootLength(path);
    }

    private static int LegacyGetRootLength(string path)
    {
      int index = 0;
      int length = path.Length;
      if (length >= 1 && Path.IsDirectorySeparator(path[0]))
      {
        index = 1;
        if (length >= 2 && Path.IsDirectorySeparator(path[1]))
        {
          index = 2;
          int num = 2;
          while (index < length && ((int) path[index] != (int) Path.DirectorySeparatorChar && (int) path[index] != (int) Path.AltDirectorySeparatorChar || --num > 0))
            ++index;
        }
      }
      else if (length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
      {
        index = 2;
        if (length >= 3 && Path.IsDirectorySeparator(path[2]))
          ++index;
      }
      return index;
    }

    internal static bool IsDirectorySeparator(char c)
    {
      if ((int) c != (int) Path.DirectorySeparatorChar)
        return (int) c == (int) Path.AltDirectorySeparatorChar;
      return true;
    }

    /// <summary>
    ///   Возвращает массив, содержащий символы, которые не разрешены в именах путей.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий символы, которые не разрешены в именах путей.
    /// </returns>
    [__DynamicallyInvokable]
    public static char[] GetInvalidPathChars()
    {
      return (char[]) Path.RealInvalidPathChars.Clone();
    }

    /// <summary>
    ///   Возвращает массив, содержащий символы, которые не разрешены в именах файлов.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий символы, которые не разрешены в именах файлов.
    /// </returns>
    [__DynamicallyInvokable]
    public static char[] GetInvalidFileNameChars()
    {
      return (char[]) Path.InvalidFileNameChars.Clone();
    }

    /// <summary>Возвращает расширение указанной строки пути.</summary>
    /// <param name="path">
    ///   Строка пути, из которой нужно получить расширение.
    /// </param>
    /// <returns>
    ///   Расширение указанного пути (включая точку ".") или значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    ///    Если параметр <paramref name="path" /> имеет значение <see langword="null" />, <see cref="M:System.IO.Path.GetExtension(System.String)" /> возвращает <see langword="null" />.
    ///    Если параметр <paramref name="path" /> не содержит сведений о расширении, <see cref="M:System.IO.Path.GetExtension(System.String)" /> возвращает <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetExtension(string path)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path, false);
      int length = path.Length;
      int startIndex = length;
      while (--startIndex >= 0)
      {
        char ch = path[startIndex];
        if (ch == '.')
        {
          if (startIndex != length - 1)
            return path.Substring(startIndex, length - startIndex);
          return string.Empty;
        }
        if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      return string.Empty;
    }

    /// <summary>
    ///   Возвращает для указанной строки пути абсолютный путь.
    /// </summary>
    /// <param name="path">
    ///   Файл или каталог, для которых нужно получить сведения об абсолютном пути.
    /// </param>
    /// <returns>
    ///   Полное расположение <paramref name="path" />, например "C:\MyFile.txt".
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> представляет собой строку нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   Системе не удалось получить абсолютный путь.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствуют необходимые разрешения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="path" /> содержит двоеточие (»:»), не является частью идентификатора тома (например, «c:\»»).
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    ///    Например, для платформ на основе Windows длина пути должна составлять менее 248 знаков, а длина имен файлов — менее 260 знаков.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetFullPath(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
      return fullPathInternal;
    }

    [SecurityCritical]
    internal static string UnsafeGetFullPath(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
      return fullPathInternal;
    }

    internal static string GetFullPathInternal(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      return Path.NormalizePath(path, true);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck)
    {
      return Path.NormalizePath(path, fullCheck, AppContextSwitches.BlockLongPaths ? 260 : (int) short.MaxValue);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck, bool expandShortPaths)
    {
      return Path.NormalizePath(path, fullCheck, Path.MaxPath, expandShortPaths);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck, int maxPathLength)
    {
      return Path.NormalizePath(path, fullCheck, maxPathLength, true);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck, int maxPathLength, bool expandShortPaths)
    {
      if (AppContextSwitches.UseLegacyPathHandling)
        return Path.LegacyNormalizePath(path, fullCheck, maxPathLength, expandShortPaths);
      if (PathInternal.IsExtended(path))
        return path;
      string str = fullCheck ? Path.NewNormalizePath(path, maxPathLength, true) : Path.NewNormalizePathLimitedChecks(path, maxPathLength, expandShortPaths);
      if (string.IsNullOrWhiteSpace(str))
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
      return str;
    }

    [SecuritySafeCritical]
    private static string NewNormalizePathLimitedChecks(string path, int maxPathLength, bool expandShortPaths)
    {
      string str = PathInternal.NormalizeDirectorySeparators(path);
      if (PathInternal.IsPathTooLong(str) || PathInternal.AreSegmentsTooLong(str))
        throw new PathTooLongException();
      if (expandShortPaths)
      {
        if (str.IndexOf('~') != -1)
        {
          try
          {
            return LongPathHelper.GetLongPathName(str);
          }
          catch
          {
          }
        }
      }
      return str;
    }

    [SecuritySafeCritical]
    private static string NewNormalizePath(string path, int maxPathLength, bool expandShortPaths)
    {
      if (path.IndexOf(char.MinValue) != -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
      return LongPathHelper.Normalize(path, (uint) maxPathLength, !PathInternal.IsDevice(path), expandShortPaths);
    }

    [SecurityCritical]
    internal static unsafe string LegacyNormalizePath(string path, bool fullCheck, int maxPathLength, bool expandShortPaths)
    {
      if (fullCheck)
      {
        path = path.TrimEnd(Path.TrimEndChars);
        if (PathInternal.AnyPathHasIllegalCharacters(path, false))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
      }
      int index1 = 0;
      PathHelper pathHelper;
      if (path.Length + 1 <= Path.MaxPath)
      {
        char* charArrayPtr = stackalloc char[Path.MaxPath];
        pathHelper = new PathHelper(charArrayPtr, Path.MaxPath);
      }
      else
        pathHelper = new PathHelper(path.Length + Path.MaxPath, maxPathLength);
      uint num1 = 0;
      uint num2 = 0;
      bool flag1 = false;
      uint num3 = 0;
      int num4 = -1;
      bool flag2 = false;
      bool flag3 = true;
      int num5 = 0;
      bool flag4 = false;
      if (path.Length > 0 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar))
      {
        pathHelper.Append('\\');
        ++index1;
        num4 = 0;
      }
      for (; index1 < path.Length; ++index1)
      {
        char ch1 = path[index1];
        if ((int) ch1 == (int) Path.DirectorySeparatorChar || (int) ch1 == (int) Path.AltDirectorySeparatorChar)
        {
          if (num3 == 0U)
          {
            if (num2 > 0U)
            {
              int index2 = num4 + 1;
              if (path[index2] != '.')
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
              if (num2 >= 2U)
              {
                if (flag2 && num2 > 2U)
                  throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                if (path[index2 + 1] == '.')
                {
                  for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
                  {
                    if (path[index3] != '.')
                      throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  }
                  num2 = 2U;
                }
                else
                {
                  if (num2 > 1U)
                    throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  num2 = 1U;
                }
              }
              if (num2 == 2U)
                pathHelper.Append('.');
              pathHelper.Append('.');
              flag1 = false;
            }
            if (num1 > 0U & flag3 && index1 + 1 < path.Length && ((int) path[index1 + 1] == (int) Path.DirectorySeparatorChar || (int) path[index1 + 1] == (int) Path.AltDirectorySeparatorChar))
              pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num2 = 0U;
          num1 = 0U;
          if (!flag1)
          {
            flag1 = true;
            pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num3 = 0U;
          num4 = index1;
          flag2 = false;
          flag3 = false;
          if (flag4)
          {
            pathHelper.TryExpandShortFileName();
            flag4 = false;
          }
          int num6 = pathHelper.Length - 1;
          if (num6 - num5 > Path.MaxDirectoryLength)
            throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
          num5 = num6;
        }
        else
        {
          switch (ch1)
          {
            case ' ':
              ++num1;
              continue;
            case '.':
              ++num2;
              continue;
            default:
              if (ch1 == '~' & expandShortPaths)
                flag4 = true;
              flag1 = false;
              if (flag3 && (int) ch1 == (int) Path.VolumeSeparatorChar)
              {
                char ch2 = index1 > 0 ? path[index1 - 1] : ' ';
                if (num2 != 0U || num3 < 1U || ch2 == ' ')
                  throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                flag2 = true;
                if (num3 > 1U)
                {
                  int index2 = 0;
                  while (index2 < pathHelper.Length && pathHelper[index2] == ' ')
                    ++index2;
                  if ((long) num3 - (long) index2 == 1L)
                  {
                    pathHelper.Length = 0;
                    pathHelper.Append(ch2);
                  }
                }
                num3 = 0U;
              }
              else
                num3 += 1U + num2 + num1;
              if (num2 > 0U || num1 > 0U)
              {
                int num6 = num4 >= 0 ? index1 - num4 - 1 : index1;
                if (num6 > 0)
                {
                  for (int index2 = 0; index2 < num6; ++index2)
                    pathHelper.Append(path[num4 + 1 + index2]);
                }
                num2 = 0U;
                num1 = 0U;
              }
              pathHelper.Append(ch1);
              num4 = index1;
              continue;
          }
        }
      }
      if (pathHelper.Length - 1 - num5 > Path.MaxDirectoryLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (num3 == 0U && num2 > 0U)
      {
        int index2 = num4 + 1;
        if (path[index2] != '.')
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
        if (num2 >= 2U)
        {
          if (flag2 && num2 > 2U)
            throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
          if (path[index2 + 1] == '.')
          {
            for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
            {
              if (path[index3] != '.')
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            }
            num2 = 2U;
          }
          else
          {
            if (num2 > 1U)
              throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            num2 = 1U;
          }
        }
        if (num2 == 2U)
          pathHelper.Append('.');
        pathHelper.Append('.');
      }
      if (pathHelper.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
      if (fullCheck && (pathHelper.OrdinalStartsWith("http:", false) || pathHelper.OrdinalStartsWith("file:", false)))
        throw new ArgumentException(Environment.GetResourceString("Argument_PathUriFormatNotSupported"));
      if (flag4)
        pathHelper.TryExpandShortFileName();
      int num7 = 1;
      if (fullCheck)
      {
        num7 = pathHelper.GetFullPathName();
        bool flag5 = false;
        for (int index2 = 0; index2 < pathHelper.Length && !flag5; ++index2)
        {
          if (pathHelper[index2] == '~' & expandShortPaths)
            flag5 = true;
        }
        if (flag5 && !pathHelper.TryExpandShortFileName())
        {
          int lastSlash = -1;
          for (int index2 = pathHelper.Length - 1; index2 >= 0; --index2)
          {
            if ((int) pathHelper[index2] == (int) Path.DirectorySeparatorChar)
            {
              lastSlash = index2;
              break;
            }
          }
          if (lastSlash >= 0)
          {
            if (pathHelper.Length >= maxPathLength)
              throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
            int lenSavedName = pathHelper.Length - lastSlash - 1;
            pathHelper.Fixup(lenSavedName, lastSlash);
          }
        }
      }
      if (num7 != 0 && pathHelper.Length > 1 && (pathHelper[0] == '\\' && pathHelper[1] == '\\'))
      {
        int index2;
        for (index2 = 2; index2 < num7; ++index2)
        {
          if (pathHelper[index2] == '\\')
          {
            ++index2;
            break;
          }
        }
        if (index2 == num7)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
        if (pathHelper.OrdinalStartsWith("\\\\?\\globalroot", true))
          throw new ArgumentException(Environment.GetResourceString("Arg_PathGlobalRoot"));
      }
      if (pathHelper.Length >= maxPathLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (num7 == 0)
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 0)
          errorCode = 161;
        __Error.WinIOError(errorCode, path);
        return (string) null;
      }
      string a = pathHelper.ToString();
      if (string.Equals(a, path, StringComparison.Ordinal))
        a = path;
      return a;
    }

    internal static bool HasLongPathPrefix(string path)
    {
      if (AppContextSwitches.UseLegacyPathHandling)
        return path.StartsWith("\\\\?\\", StringComparison.Ordinal);
      return PathInternal.IsExtended(path);
    }

    internal static string AddLongPathPrefix(string path)
    {
      if (!AppContextSwitches.UseLegacyPathHandling)
        return PathInternal.EnsureExtendedPrefix(path);
      if (path.StartsWith("\\\\?\\", StringComparison.Ordinal))
        return path;
      if (path.StartsWith("\\\\", StringComparison.Ordinal))
        return path.Insert(2, "?\\UNC\\");
      return "\\\\?\\" + path;
    }

    internal static string RemoveLongPathPrefix(string path)
    {
      if (!AppContextSwitches.UseLegacyPathHandling)
        return PathInternal.RemoveExtendedPrefix(path);
      if (!path.StartsWith("\\\\?\\", StringComparison.Ordinal))
        return path;
      if (path.StartsWith("\\\\?\\UNC\\", StringComparison.OrdinalIgnoreCase))
        return path.Remove(2, 6);
      return path.Substring(4);
    }

    internal static StringBuilder RemoveLongPathPrefix(StringBuilder pathSB)
    {
      if (!AppContextSwitches.UseLegacyPathHandling)
        return PathInternal.RemoveExtendedPrefix(pathSB);
      if (!PathInternal.StartsWithOrdinal(pathSB, "\\\\?\\", false))
        return pathSB;
      if (PathInternal.StartsWithOrdinal(pathSB, "\\\\?\\UNC\\", true))
        return pathSB.Remove(2, 6);
      return pathSB.Remove(0, 4);
    }

    /// <summary>
    ///   Возвращает имя файла и расширение указанной строки пути.
    /// </summary>
    /// <param name="path">
    ///   Строка пути, из которой нужно получить имя файла и расширение.
    /// </param>
    /// <returns>
    ///   Символы, следующие за последним символом каталога в пути <paramref name="path" />.
    ///    Если последним символом параметра <paramref name="path" /> является символ разделения тома или каталога, этот метод возвращает <see cref="F:System.String.Empty" />.
    ///    Если значением параметра <paramref name="path" /> является <see langword="null" />, метод возвращает <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetFileName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        int index = length;
        while (--index >= 0)
        {
          char ch = path[index];
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            return path.Substring(index + 1, length - index - 1);
        }
      }
      return path;
    }

    /// <summary>
    ///   Возвращает имя файла указанной строки пути без расширения.
    /// </summary>
    /// <param name="path">Путь к файлу.</param>
    /// <returns>
    ///   Строка, возвращаемая функцией <see cref="M:System.IO.Path.GetFileName(System.String)" />, за вычетом последней точки (.)
    ///    и все символы.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetFileNameWithoutExtension(string path)
    {
      path = Path.GetFileName(path);
      if (path == null)
        return (string) null;
      int length;
      if ((length = path.LastIndexOf('.')) == -1)
        return path;
      return path.Substring(0, length);
    }

    /// <summary>
    ///   Возвращает сведения о корневом каталоге для указанного пути.
    /// </summary>
    /// <param name="path">
    ///   Путь, из которого нужно получить сведения о корневом каталоге.
    /// </param>
    /// <returns>
    ///   Корневой каталог для <paramref name="path" />, например "C:\", или <see langword="null" />, если параметр <paramref name="path" /> имеет значение <see langword="null" />, или пустая строка, если <paramref name="path" /> не содержит сведений о корневом каталоге.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.String.Empty" /> передан <paramref name="path" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetPathRoot(string path)
    {
      if (path == null)
        return (string) null;
      path = Path.NormalizePath(path, false, false);
      return path.Substring(0, Path.GetRootLength(path));
    }

    /// <summary>
    ///   Возвращает путь к временной папке текущего пользователя.
    /// </summary>
    /// <returns>
    ///   Путь к временной папке, заканчивающийся обратной косой чертой.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствуют необходимые разрешения.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetTempPath()
    {
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      StringBuilder buffer = new StringBuilder(260);
      uint tempPath = Win32Native.GetTempPath(260, buffer);
      string path = buffer.ToString();
      if (tempPath == 0U)
        __Error.WinIOError();
      return Path.GetFullPathInternal(path);
    }

    internal static bool IsRelative(string path)
    {
      return PathInternal.IsPartiallyQualified(path);
    }

    /// <summary>Возвращает произвольное имя каталога или файла.</summary>
    /// <returns>Произвольное имя каталога или файла.</returns>
    [__DynamicallyInvokable]
    public static string GetRandomFileName()
    {
      byte[] numArray = new byte[10];
      RNGCryptoServiceProvider cryptoServiceProvider = (RNGCryptoServiceProvider) null;
      try
      {
        cryptoServiceProvider = new RNGCryptoServiceProvider();
        cryptoServiceProvider.GetBytes(numArray);
        char[] charArray = Path.ToBase32StringSuitableForDirName(numArray).ToCharArray();
        charArray[8] = '.';
        return new string(charArray, 0, 12);
      }
      finally
      {
        cryptoServiceProvider?.Dispose();
      }
    }

    /// <summary>
    ///   Создает на диске временный пустой файл с уникальным именем и возвращает полный путь этого файла.
    /// </summary>
    /// <returns>Полный путь к временному файлу.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Возникает ошибка ввода-вывода, например, нет уникальное имя временного файла.
    /// 
    ///   -или-
    /// 
    ///   Этот метод не удалось создать временный файл.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetTempFileName()
    {
      return Path.InternalGetTempFileName(true);
    }

    [SecurityCritical]
    internal static string UnsafeGetTempFileName()
    {
      return Path.InternalGetTempFileName(false);
    }

    [SecurityCritical]
    private static string InternalGetTempFileName(bool checkHost)
    {
      string tempPath = Path.GetTempPath();
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, tempPath, false, true);
      StringBuilder tmpFileName = new StringBuilder(260);
      if (Win32Native.GetTempFileName(tempPath, "tmp", 0U, tmpFileName) == 0U)
        __Error.WinIOError();
      return tmpFileName.ToString();
    }

    /// <summary>
    ///   Определяет, включает ли путь расширение имени файла.
    /// </summary>
    /// <param name="path">Путь для поиска расширения.</param>
    /// <returns>
    ///   <see langword="true" /> Если символы, следующие за последним разделителем каталога (\\ или /) или разделителем тома (:) в пути, включают точку (.)
    ///    за которым следует один или несколько символов; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool HasExtension(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        while (--length >= 0)
        {
          char ch = path[length];
          if (ch == '.')
            return length != path.Length - 1;
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            break;
        }
      }
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, содержит ли заданный путь корневую папку.
    /// </summary>
    /// <param name="path">Проверяемый путь.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="path" /> содержит корневую папку; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsPathRooted(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        if (length >= 1 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar) || length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
          return true;
      }
      return false;
    }

    /// <summary>Объединяет две строки в путь.</summary>
    /// <param name="path1">Первый путь для объединения.</param>
    /// <param name="path2">Второй путь для объединения.</param>
    /// <returns>
    ///   Объединенные пути.
    ///    Если один из указанных путей является строкой нулевой длины, этот метод возвращает другой путь.
    ///    Если в качестве значения параметра <paramref name="path2" /> задан абсолютный путь, этот метод возвращает <paramref name="path2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path1" /> или <paramref name="path2" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path1" /> или <paramref name="path2" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Combine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? nameof (path1) : nameof (path2));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      return Path.CombineNoChecks(path1, path2);
    }

    /// <summary>Объединяет три строки в путь.</summary>
    /// <param name="path1">Первый путь для объединения.</param>
    /// <param name="path2">Второй путь для объединения.</param>
    /// <param name="path3">Третий путь для объединения.</param>
    /// <returns>Объединенные пути.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path1" />, <paramref name="path2" /> или <paramref name="path3" /> содержит по крайней мере один из недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path1" />, <paramref name="path2" /> или <paramref name="path3" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Combine(string path1, string path2, string path3)
    {
      if (path1 == null || path2 == null || path3 == null)
        throw new ArgumentNullException(path1 == null ? nameof (path1) : (path2 == null ? nameof (path2) : nameof (path3)));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      Path.CheckInvalidPathChars(path3, false);
      return Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3);
    }

    /// <summary>Объединяет четыре строки в путь.</summary>
    /// <param name="path1">Первый путь для объединения.</param>
    /// <param name="path2">Второй путь для объединения.</param>
    /// <param name="path3">Третий путь для объединения.</param>
    /// <param name="path4">Четвертый путь для объединения.</param>
    /// <returns>Объединенные пути.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" /> или <paramref name="path4" /> содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" /> или <paramref name="path4" /> имеет значение <see langword="null" />.
    /// </exception>
    public static string Combine(string path1, string path2, string path3, string path4)
    {
      if (path1 == null || path2 == null || (path3 == null || path4 == null))
        throw new ArgumentNullException(path1 == null ? nameof (path1) : (path2 == null ? nameof (path2) : (path3 == null ? nameof (path3) : nameof (path4))));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      Path.CheckInvalidPathChars(path3, false);
      Path.CheckInvalidPathChars(path4, false);
      return Path.CombineNoChecks(Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3), path4);
    }

    /// <summary>Объединяет массив строк в путь.</summary>
    /// <param name="paths">Массив частей пути.</param>
    /// <returns>Объединенные пути.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Одна из строк в массиве содержит один или несколько недопустимых символов, определенных в <see cref="M:System.IO.Path.GetInvalidPathChars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Одна из строк в массиве является <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Combine(params string[] paths)
    {
      if (paths == null)
        throw new ArgumentNullException(nameof (paths));
      int capacity = 0;
      int num = 0;
      for (int index = 0; index < paths.Length; ++index)
      {
        if (paths[index] == null)
          throw new ArgumentNullException(nameof (paths));
        if (paths[index].Length != 0)
        {
          Path.CheckInvalidPathChars(paths[index], false);
          if (Path.IsPathRooted(paths[index]))
          {
            num = index;
            capacity = paths[index].Length;
          }
          else
            capacity += paths[index].Length;
          char ch = paths[index][paths[index].Length - 1];
          if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
            ++capacity;
        }
      }
      StringBuilder sb = StringBuilderCache.Acquire(capacity);
      for (int index = num; index < paths.Length; ++index)
      {
        if (paths[index].Length != 0)
        {
          if (sb.Length == 0)
          {
            sb.Append(paths[index]);
          }
          else
          {
            char ch = sb[sb.Length - 1];
            if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
              sb.Append(Path.DirectorySeparatorChar);
            sb.Append(paths[index]);
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    internal static string CombineNoChecks(string path1, string path2)
    {
      if (path2.Length == 0)
        return path1;
      if (path1.Length == 0 || Path.IsPathRooted(path2))
        return path2;
      char ch = path1[path1.Length - 1];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + "\\" + path2;
      return path1 + path2;
    }

    internal static string ToBase32StringSuitableForDirName(byte[] buff)
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      int length = buff.Length;
      int num1 = 0;
      do
      {
        byte num2 = num1 < length ? buff[num1++] : (byte) 0;
        byte num3 = num1 < length ? buff[num1++] : (byte) 0;
        byte num4 = num1 < length ? buff[num1++] : (byte) 0;
        byte num5 = num1 < length ? buff[num1++] : (byte) 0;
        byte num6 = num1 < length ? buff[num1++] : (byte) 0;
        sb.Append(Path.s_Base32Char[(int) num2 & 31]);
        sb.Append(Path.s_Base32Char[(int) num3 & 31]);
        sb.Append(Path.s_Base32Char[(int) num4 & 31]);
        sb.Append(Path.s_Base32Char[(int) num5 & 31]);
        sb.Append(Path.s_Base32Char[(int) num6 & 31]);
        sb.Append(Path.s_Base32Char[((int) num2 & 224) >> 5 | ((int) num5 & 96) >> 2]);
        sb.Append(Path.s_Base32Char[((int) num3 & 224) >> 5 | ((int) num6 & 96) >> 2]);
        byte num7 = (byte) ((uint) num4 >> 5);
        if (((int) num5 & 128) != 0)
          num7 |= (byte) 8;
        if (((int) num6 & 128) != 0)
          num7 |= (byte) 16;
        sb.Append(Path.s_Base32Char[(int) num7]);
      }
      while (num1 < length);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    internal static void CheckSearchPattern(string searchPattern)
    {
      int num;
      for (; (num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1; searchPattern = searchPattern.Substring(num + 2))
      {
        if (num + 2 == searchPattern.Length)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
        if ((int) searchPattern[num + 2] == (int) Path.DirectorySeparatorChar || (int) searchPattern[num + 2] == (int) Path.AltDirectorySeparatorChar)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
      }
    }

    internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (PathInternal.HasIllegalCharacters(path, checkAdditional))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
    }

    internal static string InternalCombine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? nameof (path1) : nameof (path2));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      if (path2.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), nameof (path2));
      if (Path.IsPathRooted(path2))
        throw new ArgumentException(Environment.GetResourceString("Arg_Path2IsRooted"), nameof (path2));
      int length = path1.Length;
      if (length == 0)
        return path2;
      char ch = path1[length - 1];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + "\\" + path2;
      return path1 + path2;
    }
  }
}
