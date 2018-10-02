// Decompiled with JetBrains decompiler
// Type: System.DateTimeParse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
  internal static class DateTimeParse
  {
    internal static DateTimeParse.MatchNumberDelegate m_hebrewNumberParser = new DateTimeParse.MatchNumberDelegate(DateTimeParse.MatchHebrewDigits);
    internal static bool enableAmPmParseAdjustment = DateTimeParse.GetAmPmParseFlag();
    private static DateTimeParse.DS[][] dateParsingStates = new DateTimeParse.DS[20][]
    {
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.BEGIN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.TX_N,
        DateTimeParse.DS.N,
        DateTimeParse.DS.D_Nd,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_M,
        DateTimeParse.DS.D_M,
        DateTimeParse.DS.D_S,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.BEGIN,
        DateTimeParse.DS.D_Y,
        DateTimeParse.DS.D_Y,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.BEGIN,
        DateTimeParse.DS.BEGIN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.NN,
        DateTimeParse.DS.D_NNd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NM,
        DateTimeParse.DS.D_NM,
        DateTimeParse.DS.D_MNd,
        DateTimeParse.DS.D_NDS,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.N,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.D_YNd,
        DateTimeParse.DS.DX_YN,
        DateTimeParse.DS.N,
        DateTimeParse.DS.N,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_NN,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.TX_N,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.NN,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.NN,
        DateTimeParse.DS.NN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NN,
        DateTimeParse.DS.D_NNd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NM,
        DateTimeParse.DS.D_MN,
        DateTimeParse.DS.D_MNd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_Nd,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.D_YNd,
        DateTimeParse.DS.DX_YN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_Nd,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_NN,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.TX_N,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_DS,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.D_NN,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.DX_NNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_DS,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NNd,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_NNY,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NNd,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_MN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_MN,
        DateTimeParse.DS.D_MNd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_M,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.D_YMd,
        DateTimeParse.DS.DX_YM,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_M,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_MN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_DS,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.D_MN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_MN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_NM,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_DS,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.D_NM,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NM,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_MNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_MNd,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_MNd,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_NDS,
        DateTimeParse.DS.DX_NNDS,
        DateTimeParse.DS.DX_NNDS,
        DateTimeParse.DS.DX_NNDS,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NDS,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.D_NDS,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_NDS,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.D_YNd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YM,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.D_YMd,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_Y,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_Y,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_YN,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.DX_YNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YN,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_YM,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.DX_YMN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_YM,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.DX_DS,
        DateTimeParse.DS.DX_DSN,
        DateTimeParse.DS.TX_N,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_S,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.D_S,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_S,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.TX_TS,
        DateTimeParse.DS.TX_TS,
        DateTimeParse.DS.TX_TS,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.D_Nd,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.D_S,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.ERROR
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.TX_NN,
        DateTimeParse.DS.TX_NN,
        DateTimeParse.DS.TX_NN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_NNt,
        DateTimeParse.DS.DX_NM,
        DateTimeParse.DS.D_NM,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.T_Nt,
        DateTimeParse.DS.TX_NN
      },
      new DateTimeParse.DS[18]
      {
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.TX_NNN,
        DateTimeParse.DS.TX_NNN,
        DateTimeParse.DS.TX_NNN,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_S,
        DateTimeParse.DS.T_NNt,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.ERROR,
        DateTimeParse.DS.T_NNt,
        DateTimeParse.DS.T_NNt,
        DateTimeParse.DS.TX_NNN
      }
    };
    internal const int MaxDateTimeNumberDigits = 8;
    internal const string GMTName = "GMT";
    internal const string ZuluName = "Z";
    private const int ORDER_YMD = 0;
    private const int ORDER_MDY = 1;
    private const int ORDER_DMY = 2;
    private const int ORDER_YDM = 3;
    private const int ORDER_YM = 4;
    private const int ORDER_MY = 5;
    private const int ORDER_MD = 6;
    private const int ORDER_DM = 7;

    [SecuritySafeCritical]
    internal static bool GetAmPmParseFlag()
    {
      return DateTime.EnableAmPmParseAdjustment();
    }

    internal static DateTime ParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime ParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      offset = TimeSpan.Zero;
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, nameof (s));
        return false;
      }
      if (format == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, nameof (format));
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (format.Length != 0)
        return DateTimeParse.DoStrictParse(s, format, style, dtfi, ref result);
      result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
      return false;
    }

    internal static DateTime ParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime ParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      offset = TimeSpan.Zero;
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, nameof (s));
        return false;
      }
      if (formats == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, nameof (formats));
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (formats.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
        return false;
      }
      for (int index = 0; index < formats.Length; ++index)
      {
        if (formats[index] == null || formats[index].Length == 0)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
          return false;
        }
        DateTimeResult result1 = new DateTimeResult();
        result1.Init();
        result1.flags = result.flags;
        if (DateTimeParse.TryParseExact(s, formats[index], dtfi, style, ref result1))
        {
          result.parsedDate = result1.parsedDate;
          result.timeZoneOffset = result1.timeZoneOffset;
          return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool MatchWord(ref __DTString str, string target)
    {
      int length = target.Length;
      if (length > str.Value.Length - str.Index || str.CompareInfo.Compare(str.Value, str.Index, length, target, 0, length, CompareOptions.IgnoreCase) != 0)
        return false;
      int index = str.Index + target.Length;
      if (index < str.Value.Length && char.IsLetter(str.Value[index]))
        return false;
      str.Index = index;
      if (str.Index < str.len)
        str.m_current = str.Value[str.Index];
      return true;
    }

    private static bool GetTimeZoneName(ref __DTString str)
    {
      return DateTimeParse.MatchWord(ref str, "GMT") || DateTimeParse.MatchWord(ref str, "Z");
    }

    internal static bool IsDigit(char ch)
    {
      if (ch >= '0')
        return ch <= '9';
      return false;
    }

    private static bool ParseFraction(ref __DTString str, out double result)
    {
      result = 0.0;
      double num1 = 0.1;
      int num2 = 0;
      char current;
      while (str.GetNext() && DateTimeParse.IsDigit(current = str.m_current))
      {
        result += (double) ((int) current - 48) * num1;
        num1 *= 0.1;
        ++num2;
      }
      return num2 > 0;
    }

    private static bool ParseTimeZone(ref __DTString str, ref TimeSpan result)
    {
      int minutes = 0;
      DTSubString subString1 = str.GetSubString();
      if (subString1.length != 1)
        return false;
      char ch = subString1[0];
      switch (ch)
      {
        case '+':
        case '-':
          str.ConsumeSubString(subString1);
          DTSubString subString2 = str.GetSubString();
          if (subString2.type != DTSubStringType.Number)
            return false;
          int num = subString2.value;
          int hours;
          switch (subString2.length)
          {
            case 1:
            case 2:
              hours = num;
              str.ConsumeSubString(subString2);
              DTSubString subString3 = str.GetSubString();
              if (subString3.length == 1 && subString3[0] == ':')
              {
                str.ConsumeSubString(subString3);
                DTSubString subString4 = str.GetSubString();
                if (subString4.type != DTSubStringType.Number || subString4.length < 1 || subString4.length > 2)
                  return false;
                minutes = subString4.value;
                str.ConsumeSubString(subString4);
                break;
              }
              break;
            case 3:
            case 4:
              hours = num / 100;
              minutes = num % 100;
              str.ConsumeSubString(subString2);
              break;
            default:
              return false;
          }
          if (minutes < 0 || minutes >= 60)
            return false;
          result = new TimeSpan(hours, minutes, 0);
          if (ch == '-')
            result = result.Negate();
          return true;
        default:
          return false;
      }
    }

    private static bool HandleTimeZone(ref __DTString str, ref DateTimeResult result)
    {
      if (str.Index < str.len - 1)
      {
        char c = str.Value[str.Index];
        int num;
        for (num = 0; char.IsWhiteSpace(c) && str.Index + num < str.len - 1; c = str.Value[str.Index + num])
          ++num;
        if (c == '+' || c == '-')
        {
          str.Index += num;
          if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          result.flags |= ParseFlags.TimeZoneUsed;
          if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
        }
      }
      return true;
    }

    [SecuritySafeCritical]
    private static bool Lex(DateTimeParse.DS dps, ref __DTString str, ref DateTimeToken dtok, ref DateTimeRawInfo raw, ref DateTimeResult result, ref DateTimeFormatInfo dtfi, DateTimeStyles styles)
    {
      dtok.dtt = DateTimeParse.DTT.Unk;
      TokenType tokenType;
      int tokenValue;
      str.GetRegularToken(out tokenType, out tokenValue, dtfi);
      TokenType separatorToken1;
      switch (tokenType)
      {
        case TokenType.NumberToken:
        case TokenType.YearNumberToken:
          if (raw.numCount == 3 || tokenValue == -1)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (dps == DateTimeParse.DS.T_NNt && str.Index < str.len - 1 && str.Value[str.Index] == '.')
            DateTimeParse.ParseFraction(ref str, out raw.fraction);
          if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && (str.Index < str.len - 1 && !DateTimeParse.HandleTimeZone(ref str, ref result)))
            return false;
          dtok.num = tokenValue;
          if (tokenType == TokenType.YearNumberToken)
          {
            if (raw.year == -1)
            {
              raw.year = tokenValue;
              int indexBeforeSeparator;
              char charBeforeSeparator;
              TokenType separatorToken2;
              switch (separatorToken2 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator, out charBeforeSeparator))
              {
                case TokenType.SEP_End:
                  dtok.dtt = DateTimeParse.DTT.YearEnd;
                  break;
                case TokenType.SEP_Space:
                  dtok.dtt = DateTimeParse.DTT.YearSpace;
                  break;
                case TokenType.SEP_Am:
                case TokenType.SEP_Pm:
                  if (raw.timeMark == DateTimeParse.TM.NotSet)
                  {
                    raw.timeMark = separatorToken2 == TokenType.SEP_Am ? DateTimeParse.TM.AM : DateTimeParse.TM.PM;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    break;
                  }
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  break;
                case TokenType.SEP_Date:
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                case TokenType.SEP_Time:
                  if (!raw.hasSameDateAndTimeSeparators)
                  {
                    result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                    return false;
                  }
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                case TokenType.SEP_YearSuff:
                case TokenType.SEP_MonthSuff:
                case TokenType.SEP_DaySuff:
                  dtok.dtt = DateTimeParse.DTT.NumDatesuff;
                  dtok.suffix = separatorToken2;
                  break;
                case TokenType.SEP_HourSuff:
                case TokenType.SEP_MinuteSuff:
                case TokenType.SEP_SecondSuff:
                  dtok.dtt = DateTimeParse.DTT.NumTimesuff;
                  dtok.suffix = separatorToken2;
                  break;
                case TokenType.SEP_DateOrOffset:
                  if (DateTimeParse.dateParsingStates[(int) dps][13] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][12] > DateTimeParse.DS.ERROR)
                  {
                    str.Index = indexBeforeSeparator;
                    str.m_current = charBeforeSeparator;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    break;
                  }
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                default:
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
              }
              return true;
            }
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          int indexBeforeSeparator1;
          char charBeforeSeparator1;
          TokenType separatorToken3;
          switch (separatorToken3 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator1, out charBeforeSeparator1))
          {
            case TokenType.SEP_End:
              dtok.dtt = DateTimeParse.DTT.NumEnd;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_Space:
              dtok.dtt = DateTimeParse.DTT.NumSpace;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_Am:
            case TokenType.SEP_Pm:
              if (raw.timeMark == DateTimeParse.TM.NotSet)
              {
                raw.timeMark = separatorToken3 == TokenType.SEP_Am ? DateTimeParse.TM.AM : DateTimeParse.TM.PM;
                dtok.dtt = DateTimeParse.DTT.NumAmpm;
                if (dps == DateTimeParse.DS.D_NN && DateTimeParse.enableAmPmParseAdjustment && !DateTimeParse.ProcessTerminaltState(DateTimeParse.DS.DX_NN, ref result, ref styles, ref raw, dtfi))
                  return false;
                raw.AddNumber(dtok.num);
                if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && !DateTimeParse.HandleTimeZone(ref str, ref result))
                  return false;
                break;
              }
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              break;
            case TokenType.SEP_Date:
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_Time:
              if (raw.hasSameDateAndTimeSeparators && (dps == DateTimeParse.DS.D_Y || dps == DateTimeParse.DS.D_YN || (dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YM) || dps == DateTimeParse.DS.D_YMd))
              {
                dtok.dtt = DateTimeParse.DTT.NumDatesep;
                raw.AddNumber(dtok.num);
                break;
              }
              dtok.dtt = DateTimeParse.DTT.NumTimesep;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_YearSuff:
              try
              {
                dtok.num = dtfi.Calendar.ToFourDigitYear(tokenValue);
              }
              catch (ArgumentOutOfRangeException ex)
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) ex);
                return false;
              }
              dtok.dtt = DateTimeParse.DTT.NumDatesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_MonthSuff:
            case TokenType.SEP_DaySuff:
              dtok.dtt = DateTimeParse.DTT.NumDatesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_HourSuff:
            case TokenType.SEP_MinuteSuff:
            case TokenType.SEP_SecondSuff:
              dtok.dtt = DateTimeParse.DTT.NumTimesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_LocalTimeMark:
              dtok.dtt = DateTimeParse.DTT.NumLocalTimeMark;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_DateOrOffset:
              if (DateTimeParse.dateParsingStates[(int) dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][3] > DateTimeParse.DS.ERROR)
              {
                str.Index = indexBeforeSeparator1;
                str.m_current = charBeforeSeparator1;
                dtok.dtt = DateTimeParse.DTT.NumSpace;
              }
              else
                dtok.dtt = DateTimeParse.DTT.NumDatesep;
              raw.AddNumber(dtok.num);
              break;
            default:
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
          }
        case TokenType.Am:
        case TokenType.Pm:
          if (raw.timeMark == DateTimeParse.TM.NotSet)
          {
            raw.timeMark = (DateTimeParse.TM) tokenValue;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.MonthToken:
          if (raw.month == -1)
          {
            int indexBeforeSeparator2;
            char charBeforeSeparator2;
            switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator2, out charBeforeSeparator2))
            {
              case TokenType.SEP_End:
                dtok.dtt = DateTimeParse.DTT.MonthEnd;
                break;
              case TokenType.SEP_Space:
                dtok.dtt = DateTimeParse.DTT.MonthSpace;
                break;
              case TokenType.SEP_Date:
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              case TokenType.SEP_Time:
                if (!raw.hasSameDateAndTimeSeparators)
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              case TokenType.SEP_DateOrOffset:
                if (DateTimeParse.dateParsingStates[(int) dps][8] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][7] > DateTimeParse.DS.ERROR)
                {
                  str.Index = indexBeforeSeparator2;
                  str.m_current = charBeforeSeparator2;
                  dtok.dtt = DateTimeParse.DTT.MonthSpace;
                  break;
                }
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              default:
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
            }
            raw.month = tokenValue;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.EndOfString:
          dtok.dtt = DateTimeParse.DTT.End;
          break;
        case TokenType.DayOfWeekToken:
          if (raw.dayOfWeek == -1)
          {
            raw.dayOfWeek = tokenValue;
            dtok.dtt = DateTimeParse.DTT.DayOfWeek;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.TimeZoneToken:
          if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          dtok.dtt = DateTimeParse.DTT.TimeZone;
          result.flags |= ParseFlags.TimeZoneUsed;
          result.timeZoneOffset = new TimeSpan(0L);
          result.flags |= ParseFlags.TimeZoneUtc;
          break;
        case TokenType.EraToken:
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.UnknownToken:
          if (char.IsLetter(str.m_current))
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_UnknowDateTimeWord", (object) str.Index);
            return false;
          }
          if (Environment.GetCompatibilityFlag(CompatibilityFlag.DateTimeParseIgnorePunctuation) && (result.flags & ParseFlags.CaptureOffset) == (ParseFlags) 0)
          {
            str.GetNext();
            return true;
          }
          if ((str.m_current == '-' || str.m_current == '+') && (result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
          {
            int index = str.Index;
            if (DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              return true;
            }
            str.Index = index;
          }
          if (DateTimeParse.VerifyValidPunctuation(ref str))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.HebrewNumber:
          if (tokenValue >= 100)
          {
            if (raw.year == -1)
            {
              raw.year = tokenValue;
              int indexBeforeSeparator2;
              char charBeforeSeparator2;
              switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator2, out charBeforeSeparator2))
              {
                case TokenType.SEP_End:
                  dtok.dtt = DateTimeParse.DTT.YearEnd;
                  goto label_111;
                case TokenType.SEP_Space:
                  dtok.dtt = DateTimeParse.DTT.YearSpace;
                  goto label_111;
                case TokenType.SEP_DateOrOffset:
                  if (DateTimeParse.dateParsingStates[(int) dps][12] > DateTimeParse.DS.ERROR)
                  {
                    str.Index = indexBeforeSeparator2;
                    str.m_current = charBeforeSeparator2;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    goto label_111;
                  }
                  else
                    break;
              }
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          dtok.num = tokenValue;
          raw.AddNumber(dtok.num);
          int indexBeforeSeparator3;
          char charBeforeSeparator3;
          switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator3, out charBeforeSeparator3))
          {
            case TokenType.SEP_End:
              dtok.dtt = DateTimeParse.DTT.NumEnd;
              break;
            case TokenType.SEP_Space:
            case TokenType.SEP_Date:
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              break;
            case TokenType.SEP_DateOrOffset:
              if (DateTimeParse.dateParsingStates[(int) dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][3] > DateTimeParse.DS.ERROR)
              {
                str.Index = indexBeforeSeparator3;
                str.m_current = charBeforeSeparator3;
                dtok.dtt = DateTimeParse.DTT.NumSpace;
                break;
              }
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              break;
            default:
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
          }
        case TokenType.JapaneseEraToken:
          result.calendar = JapaneseCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.TEraToken:
          result.calendar = TaiwanCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
      }
label_111:
      return true;
    }

    private static bool VerifyValidPunctuation(ref __DTString str)
    {
      switch (str.Value[str.Index])
      {
        case char.MinValue:
          for (int index = str.Index; index < str.len; ++index)
          {
            if (str.Value[index] != char.MinValue)
              return false;
          }
          str.Index = str.len;
          return true;
        case '#':
          bool flag1 = false;
          bool flag2 = false;
          for (int index = 0; index < str.len; ++index)
          {
            char c = str.Value[index];
            switch (c)
            {
              case char.MinValue:
                if (!flag2)
                  return false;
                break;
              case '#':
                if (flag1)
                {
                  if (flag2)
                    return false;
                  flag2 = true;
                  break;
                }
                flag1 = true;
                break;
              default:
                if (!char.IsWhiteSpace(c) && !flag1 | flag2)
                  return false;
                break;
            }
          }
          if (!flag2)
            return false;
          str.GetNext();
          return true;
        default:
          return false;
      }
    }

    private static bool GetYearMonthDayOrder(string datePattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = -1;
      int num4 = 0;
      bool flag = false;
      for (int index = 0; index < datePattern.Length && num4 < 3; ++index)
      {
        char ch = datePattern[index];
        switch (ch)
        {
          case '"':
          case '\'':
            flag = !flag;
            goto default;
          case '%':
          case '\\':
            ++index;
            break;
          default:
            if (!flag)
            {
              switch (ch)
              {
                case 'M':
                  num2 = num4++;
                  while (index + 1 < datePattern.Length && datePattern[index + 1] == 'M')
                    ++index;
                  continue;
                case 'd':
                  int num5 = 1;
                  for (; index + 1 < datePattern.Length && datePattern[index + 1] == 'd'; ++index)
                    ++num5;
                  if (num5 <= 2)
                  {
                    num3 = num4++;
                    continue;
                  }
                  continue;
                case 'y':
                  num1 = num4++;
                  while (index + 1 < datePattern.Length && datePattern[index + 1] == 'y')
                    ++index;
                  continue;
                default:
                  continue;
              }
            }
            else
              break;
        }
      }
      if (num1 == 0 && num2 == 1 && num3 == 2)
      {
        order = 0;
        return true;
      }
      if (num2 == 0 && num3 == 1 && num1 == 2)
      {
        order = 1;
        return true;
      }
      if (num3 == 0 && num2 == 1 && num1 == 2)
      {
        order = 2;
        return true;
      }
      if (num1 == 0 && num3 == 1 && num2 == 2)
      {
        order = 3;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool GetYearMonthOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = 0;
      bool flag = false;
      for (int index = 0; index < pattern.Length && num3 < 2; ++index)
      {
        char ch = pattern[index];
        switch (ch)
        {
          case '"':
          case '\'':
            flag = !flag;
            goto default;
          case '%':
          case '\\':
            ++index;
            break;
          default:
            if (!flag)
            {
              switch (ch)
              {
                case 'M':
                  num2 = num3++;
                  while (index + 1 < pattern.Length && pattern[index + 1] == 'M')
                    ++index;
                  continue;
                case 'y':
                  num1 = num3++;
                  while (index + 1 < pattern.Length && pattern[index + 1] == 'y')
                    ++index;
                  continue;
                default:
                  continue;
              }
            }
            else
              break;
        }
      }
      if (num1 == 0 && num2 == 1)
      {
        order = 4;
        return true;
      }
      if (num2 == 0 && num1 == 1)
      {
        order = 5;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool GetMonthDayOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = 0;
      bool flag = false;
      for (int index = 0; index < pattern.Length && num3 < 2; ++index)
      {
        char ch = pattern[index];
        switch (ch)
        {
          case '"':
          case '\'':
            flag = !flag;
            goto default;
          case '%':
          case '\\':
            ++index;
            break;
          default:
            if (!flag)
            {
              switch (ch)
              {
                case 'M':
                  num1 = num3++;
                  while (index + 1 < pattern.Length && pattern[index + 1] == 'M')
                    ++index;
                  continue;
                case 'd':
                  int num4 = 1;
                  for (; index + 1 < pattern.Length && pattern[index + 1] == 'd'; ++index)
                    ++num4;
                  if (num4 <= 2)
                  {
                    num2 = num3++;
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
            else
              break;
        }
      }
      if (num1 == 0 && num2 == 1)
      {
        order = 6;
        return true;
      }
      if (num2 == 0 && num1 == 1)
      {
        order = 7;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool TryAdjustYear(ref DateTimeResult result, int year, out int adjustedYear)
    {
      if (year < 100)
      {
        try
        {
          year = result.calendar.ToFourDigitYear(year);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          adjustedYear = -1;
          return false;
        }
      }
      adjustedYear = year;
      return true;
    }

    private static bool SetDateYMD(ref DateTimeResult result, int year, int month, int day)
    {
      if (!result.calendar.IsValidDay(year, month, day, result.era))
        return false;
      result.SetDate(year, month, day);
      return true;
    }

    private static bool SetDateMDY(ref DateTimeResult result, int month, int day, int year)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static bool SetDateDMY(ref DateTimeResult result, int day, int month, int year)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static bool SetDateYDM(ref DateTimeResult result, int year, int day, int month)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static void GetDefaultYear(ref DateTimeResult result, ref DateTimeStyles styles)
    {
      result.Year = result.calendar.GetYear(DateTimeParse.GetDateTimeNow(ref result, ref styles));
      result.flags |= ParseFlags.YearDefault;
    }

    private static bool GetDayOfNN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      int order;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order == 6)
      {
        if (DateTimeParse.SetDateYMD(ref result, result.Year, number1, number2))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (DateTimeParse.SetDateYMD(ref result, result.Year, number2, number1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int number3 = raw.GetNumber(2);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      switch (order)
      {
        case 0:
          int adjustedYear1;
          if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear1) && DateTimeParse.SetDateYMD(ref result, adjustedYear1, number2, number3))
          {
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
        case 1:
          int adjustedYear2;
          if (DateTimeParse.TryAdjustYear(ref result, number3, out adjustedYear2) && DateTimeParse.SetDateMDY(ref result, number1, number2, adjustedYear2))
          {
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
        case 2:
          int adjustedYear3;
          if (DateTimeParse.TryAdjustYear(ref result, number3, out adjustedYear3) && DateTimeParse.SetDateDMY(ref result, number1, number2, adjustedYear3))
          {
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
        case 3:
          int adjustedYear4;
          if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear4) && DateTimeParse.SetDateYDM(ref result, adjustedYear4, number2, number3))
          {
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfMN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int order1;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order1))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order1 == 7)
      {
        int order2;
        if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out order2))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.YearMonthPattern);
          return false;
        }
        if (order2 == 5)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, raw.month, 1))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      if (DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
        return true;
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetHebrewDayOfNM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      int order;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      result.Month = raw.month;
      if ((order == 7 || order == 6) && result.calendar.IsValidDay(result.Year, result.Month, raw.GetNumber(0), result.era))
      {
        result.Day = raw.GetNumber(0);
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNM(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int order1;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order1))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order1 == 6)
      {
        int order2;
        if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out order2))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.YearMonthPattern);
          return false;
        }
        if (order2 == 4)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, raw.month, 1))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      if (DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
        return true;
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfMNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      switch (order)
      {
        case 0:
          int adjustedYear1;
          if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear1) && result.calendar.IsValidDay(adjustedYear1, raw.month, number2, result.era))
          {
            result.SetDate(adjustedYear1, raw.month, number2);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear1) && result.calendar.IsValidDay(adjustedYear1, raw.month, number1, result.era))
          {
            result.SetDate(adjustedYear1, raw.month, number1);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
        case 1:
          int adjustedYear2;
          if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear2) && result.calendar.IsValidDay(adjustedYear2, raw.month, number1, result.era))
          {
            result.SetDate(adjustedYear2, raw.month, number1);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear2) && result.calendar.IsValidDay(adjustedYear2, raw.month, number2, result.era))
          {
            result.SetDate(adjustedYear2, raw.month, number2);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
        case 2:
          int adjustedYear3;
          if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear3) && result.calendar.IsValidDay(adjustedYear3, raw.month, number1, result.era))
          {
            result.SetDate(adjustedYear3, raw.month, number1);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear3) && result.calendar.IsValidDay(adjustedYear3, raw.month, number2, result.era))
          {
            result.SetDate(adjustedYear3, raw.month, number2);
            result.flags |= ParseFlags.HaveDate;
            return true;
          }
          break;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order) && order == 3)
      {
        if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number1))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (DateTimeParse.SetDateYMD(ref result, raw.year, number1, number2))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNNY(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      if (order == 1 || order == 0)
      {
        if (DateTimeParse.SetDateYMD(ref result, raw.year, number1, number2))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYMN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, raw.GetNumber(0)))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.GetNumber(0), 1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, 1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static void AdjustTimeMark(DateTimeFormatInfo dtfi, ref DateTimeRawInfo raw)
    {
      if (raw.timeMark != DateTimeParse.TM.NotSet || dtfi.AMDesignator == null || dtfi.PMDesignator == null)
        return;
      if (dtfi.AMDesignator.Length == 0 && dtfi.PMDesignator.Length != 0)
        raw.timeMark = DateTimeParse.TM.AM;
      if (dtfi.PMDesignator.Length != 0 || dtfi.AMDesignator.Length == 0)
        return;
      raw.timeMark = DateTimeParse.TM.PM;
    }

    private static bool AdjustHour(ref int hour, DateTimeParse.TM timeMark)
    {
      switch (timeMark)
      {
        case DateTimeParse.TM.NotSet:
          return true;
        case DateTimeParse.TM.AM:
          if (hour < 0 || hour > 12)
            return false;
          hour = hour == 12 ? 0 : hour;
          goto case DateTimeParse.TM.NotSet;
        default:
          if (hour < 0 || hour > 23)
            return false;
          if (hour < 12)
          {
            hour += 12;
            goto case DateTimeParse.TM.NotSet;
          }
          else
            goto case DateTimeParse.TM.NotSet;
      }
    }

    private static bool GetTimeOfN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (raw.timeMark == DateTimeParse.TM.NotSet)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetTimeOfNN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.Minute = raw.GetNumber(1);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetTimeOfNNN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.Minute = raw.GetNumber(1);
      result.Second = raw.GetNumber(2);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetDateOfDSN(ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if (raw.numCount != 1 || result.Day != -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Day = raw.GetNumber(0);
      return true;
    }

    private static bool GetDateOfNDS(ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if (result.Month == -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (result.Year != -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out result.Year))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Day = 1;
      return true;
    }

    private static bool GetDateOfNNDS(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveYear) != (ParseFlags) 0)
      {
        if ((result.flags & ParseFlags.HaveMonth) == (ParseFlags) 0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags) 0 && (DateTimeParse.TryAdjustYear(ref result, raw.year, out result.Year) && DateTimeParse.SetDateYMD(ref result, result.Year, raw.GetNumber(0), raw.GetNumber(1))))
          return true;
      }
      else if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags) 0 && (result.flags & ParseFlags.HaveYear) == (ParseFlags) 0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags) 0)
      {
        int order;
        if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
          return false;
        }
        if (order == 0)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, result.Month, raw.GetNumber(1)))
            return true;
        }
        else
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(1), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, result.Month, raw.GetNumber(0)))
            return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool ProcessDateTimeSuffix(ref DateTimeResult result, ref DateTimeRawInfo raw, ref DateTimeToken dtok)
    {
      switch (dtok.suffix)
      {
        case TokenType.SEP_YearSuff:
          if ((result.flags & ParseFlags.HaveYear) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveYear;
          result.Year = raw.year = dtok.num;
          break;
        case TokenType.SEP_MonthSuff:
          if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveMonth;
          result.Month = raw.month = dtok.num;
          break;
        case TokenType.SEP_DaySuff:
          if ((result.flags & ParseFlags.HaveDay) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveDay;
          result.Day = dtok.num;
          break;
        case TokenType.SEP_HourSuff:
          if ((result.flags & ParseFlags.HaveHour) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveHour;
          result.Hour = dtok.num;
          break;
        case TokenType.SEP_MinuteSuff:
          if ((result.flags & ParseFlags.HaveMinute) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveMinute;
          result.Minute = dtok.num;
          break;
        case TokenType.SEP_SecondSuff:
          if ((result.flags & ParseFlags.HaveSecond) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveSecond;
          result.Second = dtok.num;
          break;
      }
      return true;
    }

    internal static bool ProcessHebrewTerminalState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      switch (dps)
      {
        case DateTimeParse.DS.DX_MN:
        case DateTimeParse.DS.DX_NM:
          DateTimeParse.GetDefaultYear(ref result, ref styles);
          if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetHebrewDayOfNM(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_MNN:
          raw.year = raw.GetNumber(1);
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_YMN:
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfYMN(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_YM:
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfYM(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.TX_N:
          if (!DateTimeParse.GetTimeOfN(dtfi, ref result, ref raw))
            return false;
          break;
        case DateTimeParse.DS.TX_NN:
          if (!DateTimeParse.GetTimeOfNN(dtfi, ref result, ref raw))
            return false;
          break;
        case DateTimeParse.DS.TX_NNN:
          if (!DateTimeParse.GetTimeOfNNN(dtfi, ref result, ref raw))
            return false;
          break;
        default:
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
      }
      if (dps > DateTimeParse.DS.ERROR)
        raw.numCount = 0;
      return true;
    }

    internal static bool ProcessTerminaltState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      bool flag = true;
      switch (dps)
      {
        case DateTimeParse.DS.DX_NN:
          flag = DateTimeParse.GetDayOfNN(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_NNN:
          flag = DateTimeParse.GetDayOfNNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_MN:
          flag = DateTimeParse.GetDayOfMN(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_NM:
          flag = DateTimeParse.GetDayOfNM(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_MNN:
          flag = DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_DS:
          flag = true;
          break;
        case DateTimeParse.DS.DX_DSN:
          flag = DateTimeParse.GetDateOfDSN(ref result, ref raw);
          break;
        case DateTimeParse.DS.DX_NDS:
          flag = DateTimeParse.GetDateOfNDS(ref result, ref raw);
          break;
        case DateTimeParse.DS.DX_NNDS:
          flag = DateTimeParse.GetDateOfNNDS(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YNN:
          flag = DateTimeParse.GetDayOfYNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YMN:
          flag = DateTimeParse.GetDayOfYMN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YN:
          flag = DateTimeParse.GetDayOfYN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YM:
          flag = DateTimeParse.GetDayOfYM(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.TX_N:
          flag = DateTimeParse.GetTimeOfN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_NN:
          flag = DateTimeParse.GetTimeOfNN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_NNN:
          flag = DateTimeParse.GetTimeOfNNN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_TS:
          flag = true;
          break;
        case DateTimeParse.DS.DX_NNY:
          flag = DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi);
          break;
      }
      if (!flag)
        return false;
      if (dps > DateTimeParse.DS.ERROR)
        raw.numCount = 0;
      return true;
    }

    internal static DateTime Parse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParse(s, dtfi, styles, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime Parse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, nameof (s));
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      DateTimeParse.DS dps = DateTimeParse.DS.BEGIN;
      bool flag1 = false;
      DateTimeToken dtok = new DateTimeToken();
      dtok.suffix = TokenType.SEP_Unk;
      DateTimeRawInfo raw = new DateTimeRawInfo();
      int* numberBuffer = stackalloc int[3];
      raw.Init(numberBuffer);
      raw.hasSameDateAndTimeSeparators = dtfi.DateSeparator.Equals(dtfi.TimeSeparator, StringComparison.Ordinal);
      result.calendar = dtfi.Calendar;
      result.era = 0;
      __DTString str = new __DTString(s, dtfi);
      str.GetNext();
      while (DateTimeParse.Lex(dps, ref str, ref dtok, ref raw, ref result, ref dtfi, styles))
      {
        if (dtok.dtt != DateTimeParse.DTT.Unk)
        {
          if (dtok.suffix != TokenType.SEP_Unk)
          {
            if (!DateTimeParse.ProcessDateTimeSuffix(ref result, ref raw, ref dtok))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            dtok.suffix = TokenType.SEP_Unk;
          }
          if (dtok.dtt == DateTimeParse.DTT.NumLocalTimeMark)
          {
            if (dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YN)
              return DateTimeParse.ParseISO8601(ref raw, ref str, styles, ref result);
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (raw.hasSameDateAndTimeSeparators)
          {
            if (dtok.dtt == DateTimeParse.DTT.YearEnd || dtok.dtt == DateTimeParse.DTT.YearSpace || dtok.dtt == DateTimeParse.DTT.YearDateSep)
            {
              if (dps == DateTimeParse.DS.T_Nt)
                dps = DateTimeParse.DS.D_Nd;
              if (dps == DateTimeParse.DS.T_NNt)
                dps = DateTimeParse.DS.D_NNd;
            }
            bool flag2 = str.AtEnd();
            if (DateTimeParse.dateParsingStates[(int) dps][(int) dtok.dtt] == DateTimeParse.DS.ERROR | flag2)
            {
              switch (dtok.dtt)
              {
                case DateTimeParse.DTT.NumDatesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace;
                  break;
                case DateTimeParse.DTT.NumTimesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace;
                  break;
                case DateTimeParse.DTT.MonthDatesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.MonthEnd : DateTimeParse.DTT.MonthSpace;
                  break;
                case DateTimeParse.DTT.YearDateSep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.YearEnd : DateTimeParse.DTT.YearSpace;
                  break;
              }
            }
          }
          dps = DateTimeParse.dateParsingStates[(int) dps][(int) dtok.dtt];
          if (dps == DateTimeParse.DS.ERROR)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (dps > DateTimeParse.DS.ERROR)
          {
            if ((dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
            {
              if (!DateTimeParse.ProcessHebrewTerminalState(dps, ref result, ref styles, ref raw, dtfi))
                return false;
            }
            else if (!DateTimeParse.ProcessTerminaltState(dps, ref result, ref styles, ref raw, dtfi))
              return false;
            flag1 = true;
            dps = DateTimeParse.DS.BEGIN;
          }
        }
        if (dtok.dtt == DateTimeParse.DTT.End || dtok.dtt == DateTimeParse.DTT.NumEnd || dtok.dtt == DateTimeParse.DTT.MonthEnd)
        {
          if (!flag1)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          DateTimeParse.AdjustTimeMark(dtfi, ref raw);
          if (!DateTimeParse.AdjustHour(ref result.Hour, raw.timeMark))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
          if (!DateTimeParse.CheckDefaultDateTime(ref result, ref result.calendar, styles))
            return false;
          DateTime result1;
          if (!result.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result1))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (raw.fraction > 0.0)
            result1 = result1.AddTicks((long) Math.Round(raw.fraction * 10000000.0));
          if (raw.dayOfWeek != -1 && (DayOfWeek) raw.dayOfWeek != result.calendar.GetDayOfWeek(result1))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDayOfWeek", (object) null);
            return false;
          }
          result.parsedDate = result1;
          return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, bTimeOnly);
        }
      }
      return false;
    }

    private static bool DetermineTimeZoneAdjustments(ref DateTimeResult result, DateTimeStyles styles, bool bTimeOnly)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
        return DateTimeParse.DateTimeOffsetTimeZonePostProcessing(ref result, styles);
      if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
      {
        if ((styles & DateTimeStyles.AssumeLocal) != DateTimeStyles.None)
        {
          if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
          {
            result.flags |= ParseFlags.TimeZoneUsed;
            result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
          }
          else
          {
            result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Local);
            return true;
          }
        }
        else
        {
          if ((styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
            return true;
          if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
          {
            result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
            return true;
          }
          result.flags |= ParseFlags.TimeZoneUsed;
          result.timeZoneOffset = TimeSpan.Zero;
        }
      }
      if ((styles & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (result.flags & ParseFlags.TimeZoneUtc) != (ParseFlags) 0)
      {
        result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
        return true;
      }
      if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
        return DateTimeParse.AdjustTimeZoneToUniversal(ref result);
      return DateTimeParse.AdjustTimeZoneToLocal(ref result, bTimeOnly);
    }

    private static bool DateTimeOffsetTimeZonePostProcessing(ref DateTimeResult result, DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
        result.timeZoneOffset = (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None ? TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime) : TimeSpan.Zero;
      long ticks1 = result.timeZoneOffset.Ticks;
      long ticks2 = result.parsedDate.Ticks - ticks1;
      if (ticks2 < 0L || ticks2 > 3155378975999999999L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_UTCOutOfRange", (object) null);
        return false;
      }
      if (ticks1 < -504000000000L || ticks1 > 504000000000L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_OffsetOutOfRange", (object) null);
        return false;
      }
      if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
      {
        if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0 && (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
        {
          bool universal = DateTimeParse.AdjustTimeZoneToUniversal(ref result);
          result.timeZoneOffset = TimeSpan.Zero;
          return universal;
        }
        result.parsedDate = new DateTime(ticks2, DateTimeKind.Utc);
        result.timeZoneOffset = TimeSpan.Zero;
      }
      return true;
    }

    private static bool AdjustTimeZoneToUniversal(ref DateTimeResult result)
    {
      long ticks = result.parsedDate.Ticks - result.timeZoneOffset.Ticks;
      if (ticks < 0L)
        ticks += 864000000000L;
      if (ticks < 0L || ticks > 3155378975999999999L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_DateOutOfRange", (object) null);
        return false;
      }
      result.parsedDate = new DateTime(ticks, DateTimeKind.Utc);
      return true;
    }

    private static bool AdjustTimeZoneToLocal(ref DateTimeResult result, bool bTimeOnly)
    {
      long ticks1 = result.parsedDate.Ticks;
      TimeZoneInfo local = TimeZoneInfo.Local;
      bool isAmbiguousLocalDst = false;
      long ticks2;
      if (ticks1 < 864000000000L)
      {
        ticks2 = ticks1 - result.timeZoneOffset.Ticks + local.GetUtcOffset(bTimeOnly ? DateTime.Now : result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
        if (ticks2 < 0L)
          ticks2 += 864000000000L;
      }
      else
      {
        long ticks3 = ticks1 - result.timeZoneOffset.Ticks;
        if (ticks3 < 0L || ticks3 > 3155378975999999999L)
        {
          ticks2 = ticks3 + local.GetUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
        }
        else
        {
          DateTime time = new DateTime(ticks3, DateTimeKind.Utc);
          bool isDaylightSavings = false;
          ticks2 = ticks3 + TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out isDaylightSavings, out isAmbiguousLocalDst).Ticks;
        }
      }
      if (ticks2 < 0L || ticks2 > 3155378975999999999L)
      {
        result.parsedDate = DateTime.MinValue;
        result.SetFailure(ParseFailureKind.Format, "Format_DateOutOfRange", (object) null);
        return false;
      }
      result.parsedDate = new DateTime(ticks2, DateTimeKind.Local, isAmbiguousLocalDst);
      return true;
    }

    private static bool ParseISO8601(ref DateTimeRawInfo raw, ref __DTString str, DateTimeStyles styles, ref DateTimeResult result)
    {
      if (raw.year >= 0 && raw.GetNumber(0) >= 0)
        raw.GetNumber(1);
      --str.Index;
      int result1 = 0;
      double result2 = 0.0;
      str.SkipWhiteSpaces();
      int result3;
      if (!DateTimeParse.ParseDigits(ref str, 2, out result3))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      if (!str.Match(':'))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      int result4;
      if (!DateTimeParse.ParseDigits(ref str, 2, out result4))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      if (str.Match(':'))
      {
        str.SkipWhiteSpaces();
        if (!DateTimeParse.ParseDigits(ref str, 2, out result1))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (str.Match('.'))
        {
          if (!DateTimeParse.ParseFraction(ref str, out result2))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          --str.Index;
        }
        str.SkipWhiteSpaces();
      }
      if (str.GetNext())
      {
        switch (str.GetChar())
        {
          case '+':
          case '-':
            result.flags |= ParseFlags.TimeZoneUsed;
            if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            break;
          case 'Z':
          case 'z':
            result.flags |= ParseFlags.TimeZoneUsed;
            result.timeZoneOffset = TimeSpan.Zero;
            result.flags |= ParseFlags.TimeZoneUtc;
            break;
          default:
            --str.Index;
            break;
        }
        str.SkipWhiteSpaces();
        if (str.Match('#'))
        {
          if (!DateTimeParse.VerifyValidPunctuation(ref str))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          str.SkipWhiteSpaces();
        }
        if (str.Match(char.MinValue) && !DateTimeParse.VerifyValidPunctuation(ref str))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (str.GetNext())
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTime result5;
      if (!GregorianCalendar.GetDefaultInstance().TryToDateTime(raw.year, raw.GetNumber(0), raw.GetNumber(1), result3, result4, result1, 0, result.era, out result5))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      result5 = result5.AddTicks((long) Math.Round(result2 * 10000000.0));
      result.parsedDate = result5;
      return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, false);
    }

    internal static bool MatchHebrewDigits(ref __DTString str, int digitLen, out int number)
    {
      number = 0;
      HebrewNumberParsingContext context = new HebrewNumberParsingContext(0);
      HebrewNumberParsingState numberParsingState = HebrewNumberParsingState.ContinueParsing;
      while (numberParsingState == HebrewNumberParsingState.ContinueParsing && str.GetNext())
        numberParsingState = HebrewNumber.ParseByChar(str.GetChar(), ref context);
      if (numberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
        return false;
      number = context.result;
      return true;
    }

    internal static bool ParseDigits(ref __DTString str, int digitLen, out int result)
    {
      if (digitLen == 1)
        return DateTimeParse.ParseDigits(ref str, 1, 2, out result);
      return DateTimeParse.ParseDigits(ref str, digitLen, digitLen, out result);
    }

    internal static bool ParseDigits(ref __DTString str, int minDigitLen, int maxDigitLen, out int result)
    {
      result = 0;
      int index = str.Index;
      int num;
      for (num = 0; num < maxDigitLen; ++num)
      {
        if (!str.GetNextDigit())
        {
          --str.Index;
          break;
        }
        result = result * 10 + str.GetDigit();
      }
      if (num >= minDigitLen)
        return true;
      str.Index = index;
      return false;
    }

    private static bool ParseFractionExact(ref __DTString str, int maxDigitLen, ref double result)
    {
      if (!str.GetNextDigit())
      {
        --str.Index;
        return false;
      }
      result = (double) str.GetDigit();
      int num;
      for (num = 1; num < maxDigitLen; ++num)
      {
        if (!str.GetNextDigit())
        {
          --str.Index;
          break;
        }
        result = result * 10.0 + (double) str.GetDigit();
      }
      result /= Math.Pow(10.0, (double) num);
      return num == maxDigitLen;
    }

    private static bool ParseSign(ref __DTString str, ref bool result)
    {
      if (!str.GetNext())
        return false;
      switch (str.GetChar())
      {
        case '+':
          result = true;
          return true;
        case '-':
          result = false;
          return true;
        default:
          return false;
      }
    }

    private static bool ParseTimeZoneOffset(ref __DTString str, int len, ref TimeSpan result)
    {
      bool result1 = true;
      int result2 = 0;
      int result3;
      if (len == 1 || len == 2)
      {
        if (!DateTimeParse.ParseSign(ref str, ref result1) || !DateTimeParse.ParseDigits(ref str, len, out result3))
          return false;
      }
      else
      {
        if (!DateTimeParse.ParseSign(ref str, ref result1) || !DateTimeParse.ParseDigits(ref str, 1, out result3))
          return false;
        if (str.Match(":"))
        {
          if (!DateTimeParse.ParseDigits(ref str, 2, out result2))
            return false;
        }
        else
        {
          --str.Index;
          if (!DateTimeParse.ParseDigits(ref str, 2, out result2))
            return false;
        }
      }
      if (result2 < 0 || result2 >= 60)
        return false;
      result = new TimeSpan(result3, result2, 0);
      if (!result1)
        result = result.Negate();
      return true;
    }

    private static bool MatchAbbreviatedMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int maxMatchStrLen = 0;
      result = -1;
      if (str.GetNext())
      {
        int num1 = dtfi.GetMonthName(13).Length == 0 ? 12 : 13;
        for (int month = 1; month <= num1; ++month)
        {
          string abbreviatedMonthName = dtfi.GetAbbreviatedMonthName(month);
          int length = abbreviatedMonthName.Length;
          if ((dtfi.HasSpacesInMonthNames ? (str.MatchSpecifiedWords(abbreviatedMonthName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(abbreviatedMonthName) ? 1 : 0)) != 0 && length > maxMatchStrLen)
          {
            maxMatchStrLen = length;
            result = month;
          }
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
      }
      if (result <= 0)
        return false;
      str.Index += maxMatchStrLen - 1;
      return true;
    }

    private static bool MatchMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int maxMatchStrLen = 0;
      result = -1;
      if (str.GetNext())
      {
        int num1 = dtfi.GetMonthName(13).Length == 0 ? 12 : 13;
        for (int month = 1; month <= num1; ++month)
        {
          string monthName = dtfi.GetMonthName(month);
          int length = monthName.Length;
          if ((dtfi.HasSpacesInMonthNames ? (str.MatchSpecifiedWords(monthName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(monthName) ? 1 : 0)) != 0 && length > maxMatchStrLen)
          {
            maxMatchStrLen = length;
            result = month;
          }
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.MonthGenitiveNames, ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
      }
      if (result <= 0)
        return false;
      str.Index += maxMatchStrLen - 1;
      return true;
    }

    private static bool MatchAbbreviatedDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int num = 0;
      result = -1;
      if (str.GetNext())
      {
        for (DayOfWeek dayofweek = DayOfWeek.Sunday; dayofweek <= DayOfWeek.Saturday; ++dayofweek)
        {
          string abbreviatedDayName = dtfi.GetAbbreviatedDayName(dayofweek);
          int length = abbreviatedDayName.Length;
          if ((dtfi.HasSpacesInDayNames ? (str.MatchSpecifiedWords(abbreviatedDayName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(abbreviatedDayName) ? 1 : 0)) != 0 && length > num)
          {
            num = length;
            result = (int) dayofweek;
          }
        }
      }
      if (result < 0)
        return false;
      str.Index += num - 1;
      return true;
    }

    private static bool MatchDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int num = 0;
      result = -1;
      if (str.GetNext())
      {
        for (DayOfWeek dayofweek = DayOfWeek.Sunday; dayofweek <= DayOfWeek.Saturday; ++dayofweek)
        {
          string dayName = dtfi.GetDayName(dayofweek);
          int length = dayName.Length;
          if ((dtfi.HasSpacesInDayNames ? (str.MatchSpecifiedWords(dayName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(dayName) ? 1 : 0)) != 0 && length > num)
          {
            num = length;
            result = (int) dayofweek;
          }
        }
      }
      if (result < 0)
        return false;
      str.Index += num - 1;
      return true;
    }

    private static bool MatchEraName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      if (str.GetNext())
      {
        int[] eras = dtfi.Calendar.Eras;
        if (eras != null)
        {
          for (int index = 0; index < eras.Length; ++index)
          {
            string eraName = dtfi.GetEraName(eras[index]);
            if (str.MatchSpecifiedWord(eraName))
            {
              str.Index += eraName.Length - 1;
              result = eras[index];
              return true;
            }
            string abbreviatedEraName = dtfi.GetAbbreviatedEraName(eras[index]);
            if (str.MatchSpecifiedWord(abbreviatedEraName))
            {
              str.Index += abbreviatedEraName.Length - 1;
              result = eras[index];
              return true;
            }
          }
        }
      }
      return false;
    }

    private static bool MatchTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
    {
      result = DateTimeParse.TM.NotSet;
      if (dtfi.AMDesignator.Length == 0)
        result = DateTimeParse.TM.AM;
      if (dtfi.PMDesignator.Length == 0)
        result = DateTimeParse.TM.PM;
      if (str.GetNext())
      {
        string amDesignator = dtfi.AMDesignator;
        if (amDesignator.Length > 0 && str.MatchSpecifiedWord(amDesignator))
        {
          str.Index += amDesignator.Length - 1;
          result = DateTimeParse.TM.AM;
          return true;
        }
        string pmDesignator = dtfi.PMDesignator;
        if (pmDesignator.Length > 0 && str.MatchSpecifiedWord(pmDesignator))
        {
          str.Index += pmDesignator.Length - 1;
          result = DateTimeParse.TM.PM;
          return true;
        }
        --str.Index;
      }
      return result != DateTimeParse.TM.NotSet;
    }

    private static bool MatchAbbreviatedTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
    {
      if (str.GetNext())
      {
        if ((int) str.GetChar() == (int) dtfi.AMDesignator[0])
        {
          result = DateTimeParse.TM.AM;
          return true;
        }
        if ((int) str.GetChar() == (int) dtfi.PMDesignator[0])
        {
          result = DateTimeParse.TM.PM;
          return true;
        }
      }
      return false;
    }

    private static bool CheckNewValue(ref int currentValue, int newValue, char patternChar, ref DateTimeResult result)
    {
      if (currentValue == -1)
      {
        currentValue = newValue;
        return true;
      }
      if (newValue == currentValue)
        return true;
      result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) patternChar);
      return false;
    }

    private static DateTime GetDateTimeNow(ref DateTimeResult result, ref DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
      {
        if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          return new DateTime(DateTime.UtcNow.Ticks + result.timeZoneOffset.Ticks, DateTimeKind.Unspecified);
        if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
          return DateTime.UtcNow;
      }
      return DateTime.Now;
    }

    private static bool CheckDefaultDateTime(ref DateTimeResult result, ref Calendar cal, DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0 && (result.Month != -1 || result.Day != -1) && ((result.Year == -1 || (result.flags & ParseFlags.YearDefault) != (ParseFlags) 0) && (result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_MissingIncompleteDate", (object) null);
        return false;
      }
      if (result.Year == -1 || result.Month == -1 || result.Day == -1)
      {
        DateTime dateTimeNow = DateTimeParse.GetDateTimeNow(ref result, ref styles);
        if (result.Month == -1 && result.Day == -1)
        {
          if (result.Year == -1)
          {
            if ((styles & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
            {
              cal = GregorianCalendar.GetDefaultInstance();
              result.Year = result.Month = result.Day = 1;
            }
            else
            {
              result.Year = cal.GetYear(dateTimeNow);
              result.Month = cal.GetMonth(dateTimeNow);
              result.Day = cal.GetDayOfMonth(dateTimeNow);
            }
          }
          else
          {
            result.Month = 1;
            result.Day = 1;
          }
        }
        else
        {
          if (result.Year == -1)
            result.Year = cal.GetYear(dateTimeNow);
          if (result.Month == -1)
            result.Month = 1;
          if (result.Day == -1)
            result.Day = 1;
        }
      }
      if (result.Hour == -1)
        result.Hour = 0;
      if (result.Minute == -1)
        result.Minute = 0;
      if (result.Second == -1)
        result.Second = 0;
      if (result.era == -1)
        result.era = 0;
      return true;
    }

    private static string ExpandPredefinedFormat(string format, ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo, ref DateTimeResult result)
    {
      switch (format[0])
      {
        case 'O':
        case 'o':
          parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.InvariantInfo;
          break;
        case 'R':
        case 'r':
          parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.InvariantInfo;
          if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
          {
            result.flags |= ParseFlags.Rfc1123Pattern;
            break;
          }
          break;
        case 'U':
          parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
          result.flags |= ParseFlags.TimeZoneUsed;
          result.timeZoneOffset = new TimeSpan(0L);
          result.flags |= ParseFlags.TimeZoneUtc;
          if (dtfi.Calendar.GetType() != typeof (GregorianCalendar))
          {
            dtfi = (DateTimeFormatInfo) dtfi.Clone();
            dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
            break;
          }
          break;
        case 's':
          dtfi = DateTimeFormatInfo.InvariantInfo;
          parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
          break;
        case 'u':
          parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.InvariantInfo;
          if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
          {
            result.flags |= ParseFlags.UtcSortPattern;
            break;
          }
          break;
      }
      return DateTimeFormat.GetRealFormat(format, dtfi);
    }

    private static bool ParseByFormat(ref __DTString str, ref __DTString format, ref ParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref DateTimeResult result)
    {
      int returnValue = 0;
      int result1 = 0;
      int result2 = 0;
      int result3 = 0;
      int result4 = 0;
      int result5 = 0;
      int result6 = 0;
      int result7 = 0;
      double result8 = 0.0;
      DateTimeParse.TM result9 = DateTimeParse.TM.AM;
      char ch = format.GetChar();
      if (ch <= 'K')
      {
        if (ch <= '.')
        {
          if (ch <= '%')
          {
            if (ch != '"')
            {
              if (ch == '%')
              {
                if (format.Index >= format.Value.Length - 1 || format.Value[format.Index + 1] == '%')
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
                  return false;
                }
                goto label_150;
              }
              else
                goto label_142;
            }
          }
          else if (ch != '\'')
          {
            if (ch == '.')
            {
              if (!str.Match(ch))
              {
                if (format.GetNext() && format.Match('F'))
                {
                  format.GetRepeatCount();
                  goto label_150;
                }
                else
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
              }
              else
                goto label_150;
            }
            else
              goto label_142;
          }
          StringBuilder result10 = new StringBuilder();
          if (!DateTimeParse.TryParseQuoteString(format.Value, format.Index, result10, out returnValue))
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadQuote", (object) ch);
            return false;
          }
          format.Index += returnValue - 1;
          string str1 = result10.ToString();
          for (int index = 0; index < str1.Length; ++index)
          {
            if (str1[index] == ' ' && parseInfo.fAllowInnerWhite)
              str.SkipWhiteSpaces();
            else if (!str.Match(str1[index]))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
          }
          if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
          {
            if ((result.flags & ParseFlags.Rfc1123Pattern) != (ParseFlags) 0 && str1 == "GMT")
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = TimeSpan.Zero;
              goto label_150;
            }
            else if ((result.flags & ParseFlags.UtcSortPattern) != (ParseFlags) 0 && str1 == "Z")
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = TimeSpan.Zero;
              goto label_150;
            }
            else
              goto label_150;
          }
          else
            goto label_150;
        }
        else if (ch <= ':')
        {
          if (ch != '/')
          {
            if (ch == ':')
            {
              if ((dtfi.TimeSeparator.Length > 1 && dtfi.TimeSeparator[0] == ':' || !str.Match(':')) && !str.Match(dtfi.TimeSeparator))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              goto label_150;
            }
            else
              goto label_142;
          }
          else
          {
            if ((dtfi.DateSeparator.Length > 1 && dtfi.DateSeparator[0] == '/' || !str.Match('/')) && !str.Match(dtfi.DateSeparator))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            goto label_150;
          }
        }
        else if (ch != 'F')
        {
          if (ch != 'H')
          {
            if (ch == 'K')
            {
              if (str.Match('Z'))
              {
                if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result.timeZoneOffset != TimeSpan.Zero)
                {
                  result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'K');
                  return false;
                }
                result.flags |= ParseFlags.TimeZoneUsed;
                result.timeZoneOffset = new TimeSpan(0L);
                result.flags |= ParseFlags.TimeZoneUtc;
                goto label_150;
              }
              else if (str.Match('+') || str.Match('-'))
              {
                --str.Index;
                TimeSpan result10 = new TimeSpan(0L);
                if (!DateTimeParse.ParseTimeZoneOffset(ref str, 3, ref result10))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result10 != result.timeZoneOffset)
                {
                  result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'K');
                  return false;
                }
                result.timeZoneOffset = result10;
                result.flags |= ParseFlags.TimeZoneUsed;
                goto label_150;
              }
              else
                goto label_150;
            }
            else
              goto label_142;
          }
          else
          {
            int repeatCount = format.GetRepeatCount();
            if (!DateTimeParse.ParseDigits(ref str, repeatCount < 2 ? 1 : 2, out result5))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Hour, result5, ch, ref result))
              return false;
            goto label_150;
          }
        }
      }
      else if (ch <= 'h')
      {
        if (ch <= 'Z')
        {
          if (ch != 'M')
          {
            if (ch == 'Z')
            {
              if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result.timeZoneOffset != TimeSpan.Zero)
              {
                result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'Z');
                return false;
              }
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = new TimeSpan(0L);
              result.flags |= ParseFlags.TimeZoneUtc;
              ++str.Index;
              if (!DateTimeParse.GetTimeZoneName(ref str))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              --str.Index;
              goto label_150;
            }
            else
              goto label_142;
          }
          else
          {
            int repeatCount = format.GetRepeatCount();
            if (repeatCount <= 2)
            {
              if (!DateTimeParse.ParseDigits(ref str, repeatCount, out result2) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount, out result2)))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
            }
            else
            {
              if (repeatCount == 3)
              {
                if (!DateTimeParse.MatchAbbreviatedMonthName(ref str, dtfi, ref result2))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
              }
              else if (!DateTimeParse.MatchMonthName(ref str, dtfi, ref result2))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              result.flags |= ParseFlags.ParsedMonthName;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Month, result2, ch, ref result))
              return false;
            goto label_150;
          }
        }
        else if (ch != '\\')
        {
          switch ((int) ch - 100)
          {
            case 0:
              int repeatCount1 = format.GetRepeatCount();
              if (repeatCount1 <= 2)
              {
                if (!DateTimeParse.ParseDigits(ref str, repeatCount1, out result3) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount1, out result3)))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if (!DateTimeParse.CheckNewValue(ref result.Day, result3, ch, ref result))
                  return false;
                goto label_150;
              }
              else
              {
                if (repeatCount1 == 3)
                {
                  if (!DateTimeParse.MatchAbbreviatedDayName(ref str, dtfi, ref result4))
                  {
                    result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                    return false;
                  }
                }
                else if (!DateTimeParse.MatchDayName(ref str, dtfi, ref result4))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if (!DateTimeParse.CheckNewValue(ref parseInfo.dayOfWeek, result4, ch, ref result))
                  return false;
                goto label_150;
              }
            case 2:
              break;
            case 3:
              format.GetRepeatCount();
              if (!DateTimeParse.MatchEraName(ref str, dtfi, ref result.era))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              goto label_150;
            case 4:
              parseInfo.fUseHour12 = true;
              int repeatCount2 = format.GetRepeatCount();
              if (!DateTimeParse.ParseDigits(ref str, repeatCount2 < 2 ? 1 : 2, out result5))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              if (!DateTimeParse.CheckNewValue(ref result.Hour, result5, ch, ref result))
                return false;
              goto label_150;
            default:
              goto label_142;
          }
        }
        else if (format.GetNext())
        {
          if (!str.Match(format.GetChar()))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          goto label_150;
        }
        else
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
          return false;
        }
      }
      else if (ch <= 's')
      {
        if (ch != 'm')
        {
          if (ch == 's')
          {
            int repeatCount3 = format.GetRepeatCount();
            if (!DateTimeParse.ParseDigits(ref str, repeatCount3 < 2 ? 1 : 2, out result7))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Second, result7, ch, ref result))
              return false;
            goto label_150;
          }
          else
            goto label_142;
        }
        else
        {
          int repeatCount3 = format.GetRepeatCount();
          if (!DateTimeParse.ParseDigits(ref str, repeatCount3 < 2 ? 1 : 2, out result6))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (!DateTimeParse.CheckNewValue(ref result.Minute, result6, ch, ref result))
            return false;
          goto label_150;
        }
      }
      else if (ch != 't')
      {
        if (ch != 'y')
        {
          if (ch == 'z')
          {
            int repeatCount3 = format.GetRepeatCount();
            TimeSpan result10 = new TimeSpan(0L);
            if (!DateTimeParse.ParseTimeZoneOffset(ref str, repeatCount3, ref result10))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result10 != result.timeZoneOffset)
            {
              result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'z');
              return false;
            }
            result.timeZoneOffset = result10;
            result.flags |= ParseFlags.TimeZoneUsed;
            goto label_150;
          }
          else
            goto label_142;
        }
        else
        {
          int repeatCount3 = format.GetRepeatCount();
          bool flag;
          if (dtfi.HasForceTwoDigitYears)
          {
            flag = DateTimeParse.ParseDigits(ref str, 1, 4, out result1);
          }
          else
          {
            if (repeatCount3 <= 2)
              parseInfo.fUseTwoDigitYear = true;
            flag = DateTimeParse.ParseDigits(ref str, repeatCount3, out result1);
          }
          if (!flag && parseInfo.fCustomNumberParser)
            flag = parseInfo.parseNumberDelegate(ref str, repeatCount3, out result1);
          if (!flag)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (!DateTimeParse.CheckNewValue(ref result.Year, result1, ch, ref result))
            return false;
          goto label_150;
        }
      }
      else
      {
        if (format.GetRepeatCount() == 1)
        {
          if (!DateTimeParse.MatchAbbreviatedTimeMark(ref str, dtfi, ref result9))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
        }
        else if (!DateTimeParse.MatchTimeMark(ref str, dtfi, ref result9))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
        {
          parseInfo.timeMark = result9;
          goto label_150;
        }
        else
        {
          if (parseInfo.timeMark != result9)
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) ch);
            return false;
          }
          goto label_150;
        }
      }
      int repeatCount4 = format.GetRepeatCount();
      if (repeatCount4 <= 7)
      {
        if (!DateTimeParse.ParseFractionExact(ref str, repeatCount4, ref result8) && ch == 'f')
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (result.fraction < 0.0)
        {
          result.fraction = result8;
          goto label_150;
        }
        else
        {
          if (result8 != result.fraction)
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) ch);
            return false;
          }
          goto label_150;
        }
      }
      else
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
label_142:
      if (ch == ' ')
      {
        if (!parseInfo.fAllowInnerWhite && !str.Match(ch) && (!parseInfo.fAllowTrailingWhite || !format.GetNext() || !DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result)))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      else if (format.MatchSpecifiedWord("GMT"))
      {
        format.Index += "GMT".Length - 1;
        result.flags |= ParseFlags.TimeZoneUsed;
        result.timeZoneOffset = TimeSpan.Zero;
        if (!str.Match("GMT"))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      else if (!str.Match(ch))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
label_150:
      return true;
    }

    internal static bool TryParseQuoteString(string format, int pos, StringBuilder result, out int returnValue)
    {
      returnValue = 0;
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
            return false;
          result.Append(format[pos++]);
        }
        else
          result.Append(ch2);
      }
      if (!flag)
        return false;
      returnValue = pos - num;
      return true;
    }

    private static bool DoStrictParse(string s, string formatParam, DateTimeStyles styles, DateTimeFormatInfo dtfi, ref DateTimeResult result)
    {
      ParsingInfo parseInfo = new ParsingInfo();
      parseInfo.Init();
      parseInfo.calendar = dtfi.Calendar;
      parseInfo.fAllowInnerWhite = (uint) (styles & DateTimeStyles.AllowInnerWhite) > 0U;
      parseInfo.fAllowTrailingWhite = (uint) (styles & DateTimeStyles.AllowTrailingWhite) > 0U;
      if (formatParam.Length == 1)
      {
        if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0 && formatParam[0] == 'U')
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
          return false;
        }
        formatParam = DateTimeParse.ExpandPredefinedFormat(formatParam, ref dtfi, ref parseInfo, ref result);
      }
      result.calendar = parseInfo.calendar;
      if (parseInfo.calendar.ID == 8)
      {
        parseInfo.parseNumberDelegate = DateTimeParse.m_hebrewNumberParser;
        parseInfo.fCustomNumberParser = true;
      }
      result.Hour = result.Minute = result.Second = -1;
      __DTString format = new __DTString(formatParam, dtfi, false);
      __DTString str = new __DTString(s, dtfi, false);
      if (parseInfo.fAllowTrailingWhite)
      {
        format.TrimTail();
        format.RemoveTrailingInQuoteSpaces();
        str.TrimTail();
      }
      if ((styles & DateTimeStyles.AllowLeadingWhite) != DateTimeStyles.None)
      {
        format.SkipWhiteSpaces();
        format.RemoveLeadingInQuoteSpaces();
        str.SkipWhiteSpaces();
      }
      while (format.GetNext())
      {
        if (parseInfo.fAllowInnerWhite)
          str.SkipWhiteSpaces();
        if (!DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result))
          return false;
      }
      if (str.Index < str.Value.Length - 1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (parseInfo.fUseTwoDigitYear && (dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) == DateTimeFormatFlags.None)
      {
        if (result.Year >= 100)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        try
        {
          result.Year = parseInfo.calendar.ToFourDigitYear(result.Year);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) ex);
          return false;
        }
      }
      if (parseInfo.fUseHour12)
      {
        if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
          parseInfo.timeMark = DateTimeParse.TM.AM;
        if (result.Hour > 12)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (parseInfo.timeMark == DateTimeParse.TM.AM)
        {
          if (result.Hour == 12)
            result.Hour = 0;
        }
        else
          result.Hour = result.Hour == 12 ? 12 : result.Hour + 12;
      }
      else if (parseInfo.timeMark == DateTimeParse.TM.AM && result.Hour >= 12 || parseInfo.timeMark == DateTimeParse.TM.PM && result.Hour < 12)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
      if (!DateTimeParse.CheckDefaultDateTime(ref result, ref parseInfo.calendar, styles))
        return false;
      if (!bTimeOnly && dtfi.HasYearMonthAdjustment && !dtfi.YearMonthAdjustment(ref result.Year, ref result.Month, (uint) (result.flags & ParseFlags.ParsedMonthName) > 0U))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      if (!parseInfo.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result.parsedDate))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      if (result.fraction > 0.0)
        result.parsedDate = result.parsedDate.AddTicks((long) Math.Round(result.fraction * 10000000.0));
      if (parseInfo.dayOfWeek != -1 && (DayOfWeek) parseInfo.dayOfWeek != parseInfo.calendar.GetDayOfWeek(result.parsedDate))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDayOfWeek", (object) null);
        return false;
      }
      return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, bTimeOnly);
    }

    private static Exception GetDateTimeParseException(ref DateTimeResult result)
    {
      switch (result.failure)
      {
        case ParseFailureKind.ArgumentNull:
          return (Exception) new ArgumentNullException(result.failureArgumentName, Environment.GetResourceString(result.failureMessageID));
        case ParseFailureKind.Format:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID));
        case ParseFailureKind.FormatWithParameter:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID, result.failureMessageFormatArgument));
        case ParseFailureKind.FormatBadDateTimeCalendar:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID, (object) result.calendar));
        default:
          return (Exception) null;
      }
    }

    [Conditional("_LOGGING")]
    internal static void LexTraceExit(string message, DateTimeParse.DS dps)
    {
    }

    [Conditional("_LOGGING")]
    internal static void PTSTraceExit(DateTimeParse.DS dps, bool passed)
    {
    }

    [Conditional("_LOGGING")]
    internal static void TPTraceExit(string message, DateTimeParse.DS dps)
    {
    }

    [Conditional("_LOGGING")]
    internal static void DTFITrace(DateTimeFormatInfo dtfi)
    {
    }

    internal delegate bool MatchNumberDelegate(ref __DTString str, int digitLen, out int result);

    internal enum DTT
    {
      End,
      NumEnd,
      NumAmpm,
      NumSpace,
      NumDatesep,
      NumTimesep,
      MonthEnd,
      MonthSpace,
      MonthDatesep,
      NumDatesuff,
      NumTimesuff,
      DayOfWeek,
      YearSpace,
      YearDateSep,
      YearEnd,
      TimeZone,
      Era,
      NumUTCTimeMark,
      Unk,
      NumLocalTimeMark,
      Max,
    }

    internal enum TM
    {
      NotSet = -1,
      AM = 0,
      PM = 1,
    }

    internal enum DS
    {
      BEGIN,
      N,
      NN,
      D_Nd,
      D_NN,
      D_NNd,
      D_M,
      D_MN,
      D_NM,
      D_MNd,
      D_NDS,
      D_Y,
      D_YN,
      D_YNd,
      D_YM,
      D_YMd,
      D_S,
      T_S,
      T_Nt,
      T_NNt,
      ERROR,
      DX_NN,
      DX_NNN,
      DX_MN,
      DX_NM,
      DX_MNN,
      DX_DS,
      DX_DSN,
      DX_NDS,
      DX_NNDS,
      DX_YNN,
      DX_YMN,
      DX_YN,
      DX_YM,
      TX_N,
      TX_NN,
      TX_NNN,
      TX_TS,
      DX_NNY,
    }
  }
}
