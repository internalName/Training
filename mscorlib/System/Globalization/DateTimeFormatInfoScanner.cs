// Decompiled with JetBrains decompiler
// Type: System.Globalization.DateTimeFormatInfoScanner
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
  internal class DateTimeFormatInfoScanner
  {
    internal List<string> m_dateWords = new List<string>();
    internal const char MonthPostfixChar = '\xE000';
    internal const char IgnorableSymbolChar = '\xE001';
    internal const string CJKYearSuff = "年";
    internal const string CJKMonthSuff = "月";
    internal const string CJKDaySuff = "日";
    internal const string KoreanYearSuff = "년";
    internal const string KoreanMonthSuff = "월";
    internal const string KoreanDaySuff = "일";
    internal const string KoreanHourSuff = "시";
    internal const string KoreanMinuteSuff = "분";
    internal const string KoreanSecondSuff = "초";
    internal const string CJKHourSuff = "時";
    internal const string ChineseHourSuff = "时";
    internal const string CJKMinuteSuff = "分";
    internal const string CJKSecondSuff = "秒";
    private static volatile Dictionary<string, string> s_knownWords;
    private DateTimeFormatInfoScanner.FoundDatePattern m_ymdFlags;

    private static Dictionary<string, string> KnownWords
    {
      get
      {
        if (DateTimeFormatInfoScanner.s_knownWords == null)
          DateTimeFormatInfoScanner.s_knownWords = new Dictionary<string, string>()
          {
            {
              "/",
              string.Empty
            },
            {
              "-",
              string.Empty
            },
            {
              ".",
              string.Empty
            },
            {
              "年",
              string.Empty
            },
            {
              "月",
              string.Empty
            },
            {
              "日",
              string.Empty
            },
            {
              "년",
              string.Empty
            },
            {
              "월",
              string.Empty
            },
            {
              "일",
              string.Empty
            },
            {
              "시",
              string.Empty
            },
            {
              "분",
              string.Empty
            },
            {
              "초",
              string.Empty
            },
            {
              "時",
              string.Empty
            },
            {
              "时",
              string.Empty
            },
            {
              "分",
              string.Empty
            },
            {
              "秒",
              string.Empty
            }
          };
        return DateTimeFormatInfoScanner.s_knownWords;
      }
    }

    internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
    {
      while (currentIndex < pattern.Length)
      {
        char c = pattern[currentIndex];
        if (c == '\\')
        {
          ++currentIndex;
          if (currentIndex < pattern.Length)
          {
            c = pattern[currentIndex];
            if (c == '\'')
              continue;
          }
          else
            break;
        }
        if (!char.IsLetter(c) && c != '\'' && c != '.')
          ++currentIndex;
        else
          break;
      }
      return currentIndex;
    }

    internal void AddDateWordOrPostfix(string formatPostfix, string str)
    {
      if (str.Length <= 0)
        return;
      if (str.Equals("."))
      {
        this.AddIgnorableSymbols(".");
      }
      else
      {
        string str1;
        if (DateTimeFormatInfoScanner.KnownWords.TryGetValue(str, out str1))
          return;
        if (this.m_dateWords == null)
          this.m_dateWords = new List<string>();
        if (formatPostfix == "MMMM")
        {
          string str2 = "\xE000" + str;
          if (this.m_dateWords.Contains(str2))
            return;
          this.m_dateWords.Add(str2);
        }
        else
        {
          if (!this.m_dateWords.Contains(str))
            this.m_dateWords.Add(str);
          if (str[str.Length - 1] != '.')
            return;
          string str2 = str.Substring(0, str.Length - 1);
          if (this.m_dateWords.Contains(str2))
            return;
          this.m_dateWords.Add(str2);
        }
      }
    }

    internal int AddDateWords(string pattern, int index, string formatPostfix)
    {
      int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
      if (num != index && formatPostfix != null)
        formatPostfix = (string) null;
      index = num;
      StringBuilder stringBuilder = new StringBuilder();
      while (index < pattern.Length)
      {
        char c = pattern[index];
        switch (c)
        {
          case '\'':
            this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
            ++index;
            goto label_13;
          case '\\':
            ++index;
            if (index < pattern.Length)
            {
              stringBuilder.Append(pattern[index]);
              ++index;
              continue;
            }
            continue;
          default:
            if (char.IsWhiteSpace(c))
            {
              this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
              if (formatPostfix != null)
                formatPostfix = (string) null;
              stringBuilder.Length = 0;
              ++index;
              continue;
            }
            stringBuilder.Append(c);
            ++index;
            continue;
        }
      }
label_13:
      return index;
    }

    internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
    {
      count = 1;
      while (++index < pattern.Length && (int) pattern[index] == (int) ch)
        ++count;
      return index;
    }

    internal void AddIgnorableSymbols(string text)
    {
      if (this.m_dateWords == null)
        this.m_dateWords = new List<string>();
      string str = "\xE001" + text;
      if (this.m_dateWords.Contains(str))
        return;
      this.m_dateWords.Add(str);
    }

    internal void ScanDateWord(string pattern)
    {
      this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
      int index = 0;
      while (index < pattern.Length)
      {
        char c = pattern[index];
        int count;
        switch (c)
        {
          case '\'':
            index = this.AddDateWords(pattern, index + 1, (string) null);
            continue;
          case '.':
            if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
            {
              this.AddIgnorableSymbols(".");
              this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
            }
            ++index;
            continue;
          case 'M':
            index = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', index, out count);
            if (count >= 4 && index < pattern.Length && pattern[index] == '\'')
              index = this.AddDateWords(pattern, index + 1, "MMMM");
            this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
            continue;
          case '\\':
            index += 2;
            continue;
          case 'd':
            index = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', index, out count);
            if (count <= 2)
            {
              this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
              continue;
            }
            continue;
          case 'y':
            index = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', index, out count);
            this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
            continue;
          default:
            if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
              this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
            ++index;
            continue;
        }
      }
    }

    internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
    {
      foreach (string allDateTimePattern in dtfi.GetAllDateTimePatterns('D'))
        this.ScanDateWord(allDateTimePattern);
      foreach (string allDateTimePattern in dtfi.GetAllDateTimePatterns('d'))
        this.ScanDateWord(allDateTimePattern);
      foreach (string allDateTimePattern in dtfi.GetAllDateTimePatterns('y'))
        this.ScanDateWord(allDateTimePattern);
      this.ScanDateWord(dtfi.MonthDayPattern);
      foreach (string allDateTimePattern in dtfi.GetAllDateTimePatterns('T'))
        this.ScanDateWord(allDateTimePattern);
      foreach (string allDateTimePattern in dtfi.GetAllDateTimePatterns('t'))
        this.ScanDateWord(allDateTimePattern);
      string[] strArray = (string[]) null;
      if (this.m_dateWords != null && this.m_dateWords.Count > 0)
      {
        strArray = new string[this.m_dateWords.Count];
        for (int index = 0; index < this.m_dateWords.Count; ++index)
          strArray[index] = this.m_dateWords[index];
      }
      return strArray;
    }

    internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
    {
      return DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames) ? FORMATFLAGS.None : FORMATFLAGS.UseGenitiveMonth;
    }

    internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
    {
      return (FORMATFLAGS) (0 | (DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || (DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? 32 : 0) | (DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || (DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? 4 : 0));
    }

    internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
    {
      return !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames) ? FORMATFLAGS.None : FORMATFLAGS.UseSpacesInDayNames;
    }

    internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
    {
      return calID != 8 ? FORMATFLAGS.None : FORMATFLAGS.UseLeapYearMonth | FORMATFLAGS.UseHebrewParsing;
    }

    private static bool EqualStringArrays(string[] array1, string[] array2)
    {
      if (array1 == array2)
        return true;
      if (array1.Length != array2.Length)
        return false;
      for (int index = 0; index < array1.Length; ++index)
      {
        if (!array1[index].Equals(array2[index]))
          return false;
      }
      return true;
    }

    private static bool ArrayElementsHaveSpace(string[] array)
    {
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = 0; index2 < array[index1].Length; ++index2)
        {
          if (char.IsWhiteSpace(array[index1][index2]))
            return true;
        }
      }
      return false;
    }

    private static bool ArrayElementsBeginWithDigit(string[] array)
    {
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        if (array[index1].Length > 0 && array[index1][0] >= '0' && array[index1][0] <= '9')
        {
          int index2 = 1;
          while (index2 < array[index1].Length && array[index1][index2] >= '0' && array[index1][index2] <= '9')
            ++index2;
          if (index2 == array[index1].Length)
            return false;
          if (index2 == array[index1].Length - 1)
          {
            switch (array[index1][index2])
            {
              case '月':
              case '월':
                return false;
            }
          }
          return index2 != array[index1].Length - 4 || array[index1][index2] != '\'' || (array[index1][index2 + 1] != ' ' || array[index1][index2 + 2] != '月') || array[index1][index2 + 3] != '\'';
        }
      }
      return false;
    }

    private enum FoundDatePattern
    {
      None = 0,
      FoundYearPatternFlag = 1,
      FoundMonthPatternFlag = 2,
      FoundDayPatternFlag = 4,
      FoundYMDPatternFlag = 7,
    }
  }
}
