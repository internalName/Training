// Decompiled with JetBrains decompiler
// Type: System.DateTimeFormat
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
  internal static class DateTimeFormat
  {
    internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;
    internal static char[] allStandardFormats = new char[19]
    {
      'd',
      'D',
      'f',
      'F',
      'g',
      'G',
      'm',
      'M',
      'o',
      'O',
      'r',
      'R',
      's',
      't',
      'T',
      'u',
      'U',
      'y',
      'Y'
    };
    internal static readonly DateTimeFormatInfo InvariantFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;
    internal static readonly string[] InvariantAbbreviatedMonthNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedMonthNames;
    internal static readonly string[] InvariantAbbreviatedDayNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedDayNames;
    internal static string[] fixedNumberFormats = new string[7]
    {
      "0",
      "00",
      "000",
      "0000",
      "00000",
      "000000",
      "0000000"
    };
    internal const int MaxSecondsFractionDigits = 7;
    internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
    internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";
    private const int DEFAULT_ALL_DATETIMES_SIZE = 132;
    internal const string Gmt = "GMT";

    internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
    {
      DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
    }

    [SecuritySafeCritical]
    internal static unsafe void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
    {
      if (!overrideLengthLimit && len > 2)
        len = 2;
      char* chPtr1 = stackalloc char[16];
      char* chPtr2 = chPtr1 + 16;
      int num = value;
      do
      {
        *(chPtr2 -= 2) = (char) (num % 10 + 48);
        num /= 10;
      }
      while (num != 0 && chPtr2 > chPtr1);
      int valueCount;
      for (valueCount = (int) (chPtr1 + 16 - chPtr2); valueCount < len && chPtr2 > chPtr1; ++valueCount)
        *(chPtr2 -= 2) = '0';
      outputBuffer.Append(chPtr2, valueCount);
    }

    private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
    {
      outputBuffer.Append(HebrewNumber.ToString(digits));
    }

    internal static int ParseRepeatPattern(string format, int pos, char patternChar)
    {
      int length = format.Length;
      int index = pos + 1;
      while (index < length && (int) format[index] == (int) patternChar)
        ++index;
      return index - pos;
    }

    private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
    {
      if (repeat == 3)
        return dtfi.GetAbbreviatedDayName((DayOfWeek) dayOfWeek);
      return dtfi.GetDayName((DayOfWeek) dayOfWeek);
    }

    private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
    {
      if (repeatCount == 3)
        return dtfi.GetAbbreviatedMonthName(month);
      return dtfi.GetMonthName(month);
    }

    private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
    {
      if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
        return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
      if (month >= 7)
        ++month;
      if (repeatCount == 3)
        return dtfi.GetAbbreviatedMonthName(month);
      return dtfi.GetMonthName(month);
    }

    internal static int ParseQuoteString(string format, int pos, StringBuilder result)
    {
      int length = format.Length;
      int num = pos;
      char ch1 = format[pos++];
      bool flag = false;
      while (pos < length)
      {
        char ch2 = format[pos++];
        if ((int) ch2 == (int) ch1)
        {
          flag = true;
          break;
        }
        if (ch2 == '\\')
        {
          if (pos >= length)
            throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
          result.Append(format[pos++]);
        }
        else
          result.Append(ch2);
      }
      if (!flag)
        throw new FormatException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Format_BadQuote"), (object) ch1));
      return pos - num;
    }

    internal static int ParseNextChar(string format, int pos)
    {
      if (pos >= format.Length - 1)
        return -1;
      return (int) format[pos + 1];
    }

    private static bool IsUseGenitiveForm(string format, int index, int tokenLen, char patternToMatch)
    {
      int num1 = 0;
      int index1 = index - 1;
      while (index1 >= 0 && (int) format[index1] != (int) patternToMatch)
        --index1;
      if (index1 >= 0)
      {
        while (--index1 >= 0 && (int) format[index1] == (int) patternToMatch)
          ++num1;
        if (num1 <= 1)
          return true;
      }
      int index2 = index + tokenLen;
      while (index2 < format.Length && (int) format[index2] != (int) patternToMatch)
        ++index2;
      if (index2 < format.Length)
      {
        int num2 = 0;
        while (++index2 < format.Length && (int) format[index2] == (int) patternToMatch)
          ++num2;
        if (num2 <= 1)
          return true;
      }
      return false;
    }

    private static string FormatCustomized(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
    {
      Calendar calendar = dtfi.Calendar;
      StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
      bool flag = calendar.ID == 8;
      bool timeOnly = true;
      int index = 0;
      while (index < format.Length)
      {
        char patternChar = format[index];
        int num1;
        switch (patternChar)
        {
          case '"':
          case '\'':
            StringBuilder result = new StringBuilder();
            num1 = DateTimeFormat.ParseQuoteString(format, index, result);
            stringBuilder.Append((object) result);
            break;
          case '%':
            int nextChar1 = DateTimeFormat.ParseNextChar(format, index);
            if (nextChar1 < 0 || nextChar1 == 37)
              throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
            stringBuilder.Append(DateTimeFormat.FormatCustomized(dateTime, ((char) nextChar1).ToString(), dtfi, offset));
            num1 = 2;
            break;
          case '/':
            stringBuilder.Append(dtfi.DateSeparator);
            num1 = 1;
            break;
          case ':':
            stringBuilder.Append(dtfi.TimeSeparator);
            num1 = 1;
            break;
          case 'F':
          case 'f':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            if (num1 > 7)
              throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
            long num2 = dateTime.Ticks % 10000000L / (long) Math.Pow(10.0, (double) (7 - num1));
            if (patternChar == 'f')
            {
              stringBuilder.Append(((int) num2).ToString(DateTimeFormat.fixedNumberFormats[num1 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
              break;
            }
            int num3;
            for (num3 = num1; num3 > 0 && num2 % 10L == 0L; --num3)
              num2 /= 10L;
            if (num3 > 0)
            {
              stringBuilder.Append(((int) num2).ToString(DateTimeFormat.fixedNumberFormats[num3 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
              break;
            }
            if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '.')
            {
              stringBuilder.Remove(stringBuilder.Length - 1, 1);
              break;
            }
            break;
          case 'H':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            DateTimeFormat.FormatDigits(stringBuilder, dateTime.Hour, num1);
            break;
          case 'K':
            num1 = 1;
            DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder);
            break;
          case 'M':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            int month = calendar.GetMonth(dateTime);
            if (num1 <= 2)
            {
              if (flag)
                DateTimeFormat.HebrewFormatDigits(stringBuilder, month);
              else
                DateTimeFormat.FormatDigits(stringBuilder, month, num1);
            }
            else if (flag)
              stringBuilder.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num1, dtfi));
            else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num1 >= 4)
              stringBuilder.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, index, num1, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
            else
              stringBuilder.Append(DateTimeFormat.FormatMonth(month, num1, dtfi));
            timeOnly = false;
            break;
          case '\\':
            int nextChar2 = DateTimeFormat.ParseNextChar(format, index);
            if (nextChar2 < 0)
              throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
            stringBuilder.Append((char) nextChar2);
            num1 = 2;
            break;
          case 'd':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            if (num1 <= 2)
            {
              int dayOfMonth = calendar.GetDayOfMonth(dateTime);
              if (flag)
                DateTimeFormat.HebrewFormatDigits(stringBuilder, dayOfMonth);
              else
                DateTimeFormat.FormatDigits(stringBuilder, dayOfMonth, num1);
            }
            else
            {
              int dayOfWeek = (int) calendar.GetDayOfWeek(dateTime);
              stringBuilder.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num1, dtfi));
            }
            timeOnly = false;
            break;
          case 'g':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            stringBuilder.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
            break;
          case 'h':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            int num4 = dateTime.Hour % 12;
            if (num4 == 0)
              num4 = 12;
            DateTimeFormat.FormatDigits(stringBuilder, num4, num1);
            break;
          case 'm':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            DateTimeFormat.FormatDigits(stringBuilder, dateTime.Minute, num1);
            break;
          case 's':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            DateTimeFormat.FormatDigits(stringBuilder, dateTime.Second, num1);
            break;
          case 't':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            if (num1 == 1)
            {
              if (dateTime.Hour < 12)
              {
                if (dtfi.AMDesignator.Length >= 1)
                {
                  stringBuilder.Append(dtfi.AMDesignator[0]);
                  break;
                }
                break;
              }
              if (dtfi.PMDesignator.Length >= 1)
              {
                stringBuilder.Append(dtfi.PMDesignator[0]);
                break;
              }
              break;
            }
            stringBuilder.Append(dateTime.Hour < 12 ? dtfi.AMDesignator : dtfi.PMDesignator);
            break;
          case 'y':
            int year = calendar.GetYear(dateTime);
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            if (dtfi.HasForceTwoDigitYears)
              DateTimeFormat.FormatDigits(stringBuilder, year, num1 <= 2 ? num1 : 2);
            else if (calendar.ID == 8)
              DateTimeFormat.HebrewFormatDigits(stringBuilder, year);
            else if (num1 <= 2)
            {
              DateTimeFormat.FormatDigits(stringBuilder, year % 100, num1);
            }
            else
            {
              string format1 = "D" + (object) num1;
              stringBuilder.Append(year.ToString(format1, (IFormatProvider) CultureInfo.InvariantCulture));
            }
            timeOnly = false;
            break;
          case 'z':
            num1 = DateTimeFormat.ParseRepeatPattern(format, index, patternChar);
            DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num1, timeOnly, stringBuilder);
            break;
          default:
            stringBuilder.Append(patternChar);
            num1 = 1;
            break;
        }
        index += num1;
      }
      return StringBuilderCache.GetStringAndRelease(stringBuilder);
    }

    private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, string format, int tokenLen, bool timeOnly, StringBuilder result)
    {
      if (offset == DateTimeFormat.NullOffset)
      {
        if (timeOnly && dateTime.Ticks < 864000000000L)
          offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
        else if (dateTime.Kind == DateTimeKind.Utc)
        {
          DateTimeFormat.InvalidFormatForUtc(format, dateTime);
          dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
          offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
        }
        else
          offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
      }
      if (offset >= TimeSpan.Zero)
      {
        result.Append('+');
      }
      else
      {
        result.Append('-');
        offset = offset.Negate();
      }
      if (tokenLen <= 1)
      {
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:0}", (object) offset.Hours);
      }
      else
      {
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:00}", (object) offset.Hours);
        if (tokenLen < 3)
          return;
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, ":{0:00}", (object) offset.Minutes);
      }
    }

    private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
    {
      if (offset == DateTimeFormat.NullOffset)
      {
        switch (dateTime.Kind)
        {
          case DateTimeKind.Utc:
            result.Append("Z");
            return;
          case DateTimeKind.Local:
            offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
            break;
          default:
            return;
        }
      }
      if (offset >= TimeSpan.Zero)
      {
        result.Append('+');
      }
      else
      {
        result.Append('-');
        offset = offset.Negate();
      }
      result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:00}:{1:00}", (object) offset.Hours, (object) offset.Minutes);
    }

    internal static string GetRealFormat(string format, DateTimeFormatInfo dtfi)
    {
      switch (format[0])
      {
        case 'D':
          return dtfi.LongDatePattern;
        case 'F':
          return dtfi.FullDateTimePattern;
        case 'G':
          return dtfi.GeneralLongTimePattern;
        case 'M':
        case 'm':
          return dtfi.MonthDayPattern;
        case 'O':
        case 'o':
          return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
        case 'R':
        case 'r':
          return dtfi.RFC1123Pattern;
        case 'T':
          return dtfi.LongTimePattern;
        case 'U':
          return dtfi.FullDateTimePattern;
        case 'Y':
        case 'y':
          return dtfi.YearMonthPattern;
        case 'd':
          return dtfi.ShortDatePattern;
        case 'f':
          return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
        case 'g':
          return dtfi.GeneralShortTimePattern;
        case 's':
          return dtfi.SortableDateTimePattern;
        case 't':
          return dtfi.ShortTimePattern;
        case 'u':
          return dtfi.UniversalSortableDateTimePattern;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      }
    }

    private static string ExpandPredefinedFormat(string format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
    {
      switch (format[0])
      {
        case 'U':
          if (offset != DateTimeFormat.NullOffset)
            throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
          dtfi = (DateTimeFormatInfo) dtfi.Clone();
          if (dtfi.Calendar.GetType() != typeof (GregorianCalendar))
            dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
          dateTime = dateTime.ToUniversalTime();
          break;
        case 's':
          dtfi = DateTimeFormatInfo.InvariantInfo;
          break;
        case 'u':
          if (offset != DateTimeFormat.NullOffset)
            dateTime -= offset;
          else if (dateTime.Kind == DateTimeKind.Local)
            DateTimeFormat.InvalidFormatForLocal(format, dateTime);
          dtfi = DateTimeFormatInfo.InvariantInfo;
          break;
      }
      format = DateTimeFormat.GetRealFormat(format, dtfi);
      return format;
    }

    internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi)
    {
      return DateTimeFormat.Format(dateTime, format, dtfi, DateTimeFormat.NullOffset);
    }

    internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
    {
      if (format == null || format.Length == 0)
      {
        bool flag = false;
        if (dateTime.Ticks < 864000000000L)
        {
          switch (dtfi.Calendar.ID)
          {
            case 3:
            case 4:
            case 6:
            case 8:
            case 13:
            case 22:
            case 23:
              flag = true;
              dtfi = DateTimeFormatInfo.InvariantInfo;
              break;
          }
        }
        format = !(offset == DateTimeFormat.NullOffset) ? (!flag ? dtfi.DateTimeOffsetPattern : "yyyy'-'MM'-'ddTHH':'mm':'ss zzz") : (!flag ? "G" : "s");
      }
      if (format.Length == 1)
      {
        switch (format[0])
        {
          case 'O':
          case 'o':
            return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FastFormatRoundtrip(dateTime, offset));
          case 'R':
          case 'r':
            return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FastFormatRfc1123(dateTime, offset, dtfi));
          default:
            format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
            break;
        }
      }
      return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset);
    }

    internal static StringBuilder FastFormatRfc1123(DateTime dateTime, TimeSpan offset, DateTimeFormatInfo dtfi)
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(29);
      if (offset != DateTimeFormat.NullOffset)
        dateTime -= offset;
      int year;
      int month;
      int day;
      dateTime.GetDatePart(out year, out month, out day);
      stringBuilder.Append(DateTimeFormat.InvariantAbbreviatedDayNames[(int) dateTime.DayOfWeek]);
      stringBuilder.Append(',');
      stringBuilder.Append(' ');
      DateTimeFormat.AppendNumber(stringBuilder, (long) day, 2);
      stringBuilder.Append(' ');
      stringBuilder.Append(DateTimeFormat.InvariantAbbreviatedMonthNames[month - 1]);
      stringBuilder.Append(' ');
      DateTimeFormat.AppendNumber(stringBuilder, (long) year, 4);
      stringBuilder.Append(' ');
      DateTimeFormat.AppendHHmmssTimeOfDay(stringBuilder, dateTime);
      stringBuilder.Append(' ');
      stringBuilder.Append("GMT");
      return stringBuilder;
    }

    internal static StringBuilder FastFormatRoundtrip(DateTime dateTime, TimeSpan offset)
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(28);
      int year;
      int month;
      int day;
      dateTime.GetDatePart(out year, out month, out day);
      DateTimeFormat.AppendNumber(stringBuilder, (long) year, 4);
      stringBuilder.Append('-');
      DateTimeFormat.AppendNumber(stringBuilder, (long) month, 2);
      stringBuilder.Append('-');
      DateTimeFormat.AppendNumber(stringBuilder, (long) day, 2);
      stringBuilder.Append('T');
      DateTimeFormat.AppendHHmmssTimeOfDay(stringBuilder, dateTime);
      stringBuilder.Append('.');
      long val = dateTime.Ticks % 10000000L;
      DateTimeFormat.AppendNumber(stringBuilder, val, 7);
      DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder);
      return stringBuilder;
    }

    private static void AppendHHmmssTimeOfDay(StringBuilder result, DateTime dateTime)
    {
      DateTimeFormat.AppendNumber(result, (long) dateTime.Hour, 2);
      result.Append(':');
      DateTimeFormat.AppendNumber(result, (long) dateTime.Minute, 2);
      result.Append(':');
      DateTimeFormat.AppendNumber(result, (long) dateTime.Second, 2);
    }

    internal static void AppendNumber(StringBuilder builder, long val, int digits)
    {
      for (int index = 0; index < digits; ++index)
        builder.Append('0');
      for (int index = 1; val > 0L && index <= digits; ++index)
      {
        builder[builder.Length - index] = (char) (48UL + (ulong) (val % 10L));
        val /= 10L;
      }
    }

    internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
    {
      string[] strArray;
      switch (format)
      {
        case 'D':
        case 'F':
        case 'G':
        case 'M':
        case 'T':
        case 'Y':
        case 'd':
        case 'f':
        case 'g':
        case 'm':
        case 't':
        case 'y':
          string[] dateTimePatterns1 = dtfi.GetAllDateTimePatterns(format);
          strArray = new string[dateTimePatterns1.Length];
          for (int index = 0; index < dateTimePatterns1.Length; ++index)
            strArray[index] = DateTimeFormat.Format(dateTime, dateTimePatterns1[index], dtfi);
          break;
        case 'O':
        case 'R':
        case 'o':
        case 'r':
        case 's':
        case 'u':
          strArray = new string[1]
          {
            DateTimeFormat.Format(dateTime, new string(new char[1]
            {
              format
            }), dtfi)
          };
          break;
        case 'U':
          DateTime universalTime = dateTime.ToUniversalTime();
          string[] dateTimePatterns2 = dtfi.GetAllDateTimePatterns(format);
          strArray = new string[dateTimePatterns2.Length];
          for (int index = 0; index < dateTimePatterns2.Length; ++index)
            strArray[index] = DateTimeFormat.Format(universalTime, dateTimePatterns2[index], dtfi);
          break;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      }
      return strArray;
    }

    internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
    {
      List<string> stringList = new List<string>(132);
      for (int index = 0; index < DateTimeFormat.allStandardFormats.Length; ++index)
      {
        foreach (string allDateTime in DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[index], dtfi))
          stringList.Add(allDateTime);
      }
      string[] array = new string[stringList.Count];
      stringList.CopyTo(0, array, 0, stringList.Count);
      return array;
    }

    internal static void InvalidFormatForLocal(string format, DateTime dateTime)
    {
    }

    [SecuritySafeCritical]
    internal static void InvalidFormatForUtc(string format, DateTime dateTime)
    {
      Mda.DateTimeInvalidLocalFormat();
    }
  }
}
