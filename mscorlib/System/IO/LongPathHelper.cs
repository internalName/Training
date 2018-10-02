// Decompiled with JetBrains decompiler
// Type: System.IO.LongPathHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
  internal class LongPathHelper
  {
    internal static readonly char[] s_trimEndChars = new char[8]
    {
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      ' ',
      '\x0085',
      ' '
    };
    private const int MaxShortName = 12;
    private const char LastAnsi = 'ÿ';
    private const char Delete = '\x007F';
    [ThreadStatic]
    private static StringBuffer t_fullPathBuffer;

    [SecurityCritical]
    internal static unsafe string Normalize(string path, uint maxPathLength, bool checkInvalidCharacters, bool expandShortPaths)
    {
      StringBuffer stringBuffer = LongPathHelper.t_fullPathBuffer ?? (LongPathHelper.t_fullPathBuffer = new StringBuffer(260U));
      try
      {
        LongPathHelper.GetFullPathName(path, stringBuffer);
        stringBuffer.TrimEnd(LongPathHelper.s_trimEndChars);
        if (stringBuffer.Length >= maxPathLength)
          throw new PathTooLongException();
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = stringBuffer.Length > 1U && stringBuffer[0U] == '\\' && stringBuffer[1U] == '\\';
        bool flag4 = PathInternal.IsDevice(stringBuffer);
        bool flag5 = flag3 && !flag4;
        uint num1 = flag3 ? 2U : 0U;
        uint num2 = flag3 ? 1U : 0U;
        char* charPointer = stringBuffer.CharPointer;
        for (; num1 < stringBuffer.Length; ++num1)
        {
          char ch = (char) *(ushort*) ((IntPtr) charPointer + (IntPtr) ((long) num1 * 2L));
          if (ch < '?' || ch == '\\' || (ch == '|' || ch == '~'))
          {
            if (ch <= '>')
            {
              if (ch != '"' && ch != '<' && ch != '>')
                goto label_24;
            }
            else if (ch != '\\')
            {
              if (ch != '|')
              {
                if (ch == '~')
                {
                  flag2 = true;
                  continue;
                }
                goto label_24;
              }
            }
            else
            {
              uint num3 = (uint) ((int) num1 - (int) num2 - 1);
              if (num3 > (uint) PathInternal.MaxComponentLength)
                throw new PathTooLongException();
              num2 = num1;
              if (flag2)
              {
                if (num3 <= 12U)
                  flag1 = true;
                flag2 = false;
              }
              if (flag5)
              {
                if ((int) num1 == (int) stringBuffer.Length - 1)
                  throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
                flag5 = false;
                continue;
              }
              continue;
            }
            if (checkInvalidCharacters)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
            flag2 = false;
            continue;
label_24:
            if (checkInvalidCharacters && ch < ' ')
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
          }
        }
        if (flag5)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
        uint num4 = (uint) ((int) stringBuffer.Length - (int) num2 - 1);
        if (num4 > (uint) PathInternal.MaxComponentLength)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        if (flag2 && num4 <= 12U)
          flag1 = true;
        if (expandShortPaths & flag1)
          return LongPathHelper.TryExpandShortFileName(stringBuffer, path);
        if ((int) stringBuffer.Length == path.Length && stringBuffer.StartsWith(path))
          return path;
        return stringBuffer.ToString();
      }
      finally
      {
        stringBuffer.Free();
      }
    }

    [SecurityCritical]
    private static unsafe void GetFullPathName(string path, StringBuffer fullPath)
    {
      int num = PathInternal.PathStartSkip(path);
      string str = path;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      uint fullPathNameW;
      while ((fullPathNameW = Win32Native.GetFullPathNameW(chPtr + num, fullPath.CharCapacity, fullPath.GetHandle(), IntPtr.Zero)) > fullPath.CharCapacity)
        fullPath.EnsureCharCapacity(fullPathNameW);
      if (fullPathNameW == 0U)
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 0)
          errorCode = 161;
        __Error.WinIOError(errorCode, path);
      }
      fullPath.Length = fullPathNameW;
      str = (string) null;
    }

    [SecurityCritical]
    internal static string GetLongPathName(StringBuffer path)
    {
      using (StringBuffer stringBuffer = new StringBuffer(path.Length))
      {
        uint longPathNameW;
        while ((longPathNameW = Win32Native.GetLongPathNameW(path.GetHandle(), stringBuffer.GetHandle(), stringBuffer.CharCapacity)) > stringBuffer.CharCapacity)
          stringBuffer.EnsureCharCapacity(longPathNameW);
        if (longPathNameW == 0U)
          LongPathHelper.GetErrorAndThrow(path.ToString());
        stringBuffer.Length = longPathNameW;
        return stringBuffer.ToString();
      }
    }

    [SecurityCritical]
    internal static string GetLongPathName(string path)
    {
      using (StringBuffer stringBuffer = new StringBuffer((uint) path.Length))
      {
        uint longPathNameW;
        while ((longPathNameW = Win32Native.GetLongPathNameW(path, stringBuffer.GetHandle(), stringBuffer.CharCapacity)) > stringBuffer.CharCapacity)
          stringBuffer.EnsureCharCapacity(longPathNameW);
        if (longPathNameW == 0U)
          LongPathHelper.GetErrorAndThrow(path);
        stringBuffer.Length = longPathNameW;
        return stringBuffer.ToString();
      }
    }

    [SecurityCritical]
    private static void GetErrorAndThrow(string path)
    {
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 0)
        errorCode = 161;
      __Error.WinIOError(errorCode, path);
    }

    [SecuritySafeCritical]
    private static string TryExpandShortFileName(StringBuffer outputBuffer, string originalPath)
    {
      using (StringBuffer stringBuffer1 = new StringBuffer(outputBuffer))
      {
        bool flag = false;
        uint num = outputBuffer.Length - 1U;
        uint startIndex = num;
        uint rootLength = PathInternal.GetRootLength(outputBuffer);
        while (!flag)
        {
          uint longPathNameW = Win32Native.GetLongPathNameW(stringBuffer1.GetHandle(), outputBuffer.GetHandle(), outputBuffer.CharCapacity);
          if (stringBuffer1[startIndex] == char.MinValue)
            stringBuffer1[startIndex] = '\\';
          if (longPathNameW == 0U)
          {
            switch (Marshal.GetLastWin32Error())
            {
              case 2:
              case 3:
                --startIndex;
                while (startIndex > rootLength && stringBuffer1[startIndex] != '\\')
                  --startIndex;
                if ((int) startIndex != (int) rootLength)
                {
                  stringBuffer1[startIndex] = char.MinValue;
                  continue;
                }
                goto label_16;
              default:
                goto label_16;
            }
          }
          else if (longPathNameW > outputBuffer.CharCapacity)
          {
            outputBuffer.EnsureCharCapacity(longPathNameW);
          }
          else
          {
            flag = true;
            outputBuffer.Length = longPathNameW;
            if (startIndex < num)
              outputBuffer.Append(stringBuffer1, startIndex, stringBuffer1.Length - startIndex);
          }
        }
label_16:
        StringBuffer stringBuffer2 = flag ? outputBuffer : stringBuffer1;
        if (stringBuffer2.SubstringEquals(originalPath, 0U, -1))
          return originalPath;
        return stringBuffer2.ToString();
      }
    }
  }
}
