// Decompiled with JetBrains decompiler
// Type: System.Globalization.HebrewCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>Представляет еврейский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class HebrewCalendar : Calendar
  {
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public static readonly int HebrewEra = 1;
    private static readonly int[] HebrewTable = new int[1316]
    {
      7,
      3,
      17,
      3,
      0,
      4,
      11,
      2,
      21,
      6,
      1,
      3,
      13,
      2,
      25,
      4,
      5,
      3,
      16,
      2,
      27,
      6,
      9,
      1,
      20,
      2,
      0,
      6,
      11,
      3,
      23,
      4,
      4,
      2,
      14,
      3,
      27,
      4,
      8,
      2,
      18,
      3,
      28,
      6,
      11,
      1,
      22,
      5,
      2,
      3,
      12,
      3,
      25,
      4,
      6,
      2,
      16,
      3,
      26,
      6,
      8,
      2,
      20,
      1,
      0,
      6,
      11,
      2,
      24,
      4,
      4,
      3,
      15,
      2,
      25,
      6,
      8,
      1,
      19,
      2,
      29,
      6,
      9,
      3,
      22,
      4,
      3,
      2,
      13,
      3,
      25,
      4,
      6,
      3,
      17,
      2,
      27,
      6,
      7,
      3,
      19,
      2,
      31,
      4,
      11,
      3,
      23,
      4,
      5,
      2,
      15,
      3,
      25,
      6,
      6,
      2,
      19,
      1,
      29,
      6,
      10,
      2,
      22,
      4,
      3,
      3,
      14,
      2,
      24,
      6,
      6,
      1,
      17,
      3,
      28,
      5,
      8,
      3,
      20,
      1,
      32,
      5,
      12,
      3,
      22,
      6,
      4,
      1,
      16,
      2,
      26,
      6,
      6,
      3,
      17,
      2,
      0,
      4,
      10,
      3,
      22,
      4,
      3,
      2,
      14,
      3,
      24,
      6,
      5,
      2,
      17,
      1,
      28,
      6,
      9,
      2,
      19,
      3,
      31,
      4,
      13,
      2,
      23,
      6,
      3,
      3,
      15,
      1,
      27,
      5,
      7,
      3,
      17,
      3,
      29,
      4,
      11,
      2,
      21,
      6,
      3,
      1,
      14,
      2,
      25,
      6,
      5,
      3,
      16,
      2,
      28,
      4,
      9,
      3,
      20,
      2,
      0,
      6,
      12,
      1,
      23,
      6,
      4,
      2,
      14,
      3,
      26,
      4,
      8,
      2,
      18,
      3,
      0,
      4,
      10,
      3,
      21,
      5,
      1,
      3,
      13,
      1,
      24,
      5,
      5,
      3,
      15,
      3,
      27,
      4,
      8,
      2,
      19,
      3,
      29,
      6,
      10,
      2,
      22,
      4,
      3,
      3,
      14,
      2,
      26,
      4,
      6,
      3,
      18,
      2,
      28,
      6,
      10,
      1,
      20,
      6,
      2,
      2,
      12,
      3,
      24,
      4,
      5,
      2,
      16,
      3,
      28,
      4,
      8,
      3,
      19,
      2,
      0,
      6,
      12,
      1,
      23,
      5,
      3,
      3,
      14,
      3,
      26,
      4,
      7,
      2,
      17,
      3,
      28,
      6,
      9,
      2,
      21,
      4,
      1,
      3,
      13,
      2,
      25,
      4,
      5,
      3,
      16,
      2,
      27,
      6,
      9,
      1,
      19,
      3,
      0,
      5,
      11,
      3,
      23,
      4,
      4,
      2,
      14,
      3,
      25,
      6,
      7,
      1,
      18,
      2,
      28,
      6,
      9,
      3,
      21,
      4,
      2,
      2,
      12,
      3,
      25,
      4,
      6,
      2,
      16,
      3,
      26,
      6,
      8,
      2,
      20,
      1,
      0,
      6,
      11,
      2,
      22,
      6,
      4,
      1,
      15,
      2,
      25,
      6,
      6,
      3,
      18,
      1,
      29,
      5,
      9,
      3,
      22,
      4,
      2,
      3,
      13,
      2,
      23,
      6,
      4,
      3,
      15,
      2,
      27,
      4,
      7,
      3,
      19,
      2,
      31,
      4,
      11,
      3,
      21,
      6,
      3,
      2,
      15,
      1,
      25,
      6,
      6,
      2,
      17,
      3,
      29,
      4,
      10,
      2,
      20,
      6,
      3,
      1,
      13,
      3,
      24,
      5,
      4,
      3,
      16,
      1,
      27,
      5,
      7,
      3,
      17,
      3,
      0,
      4,
      11,
      2,
      21,
      6,
      1,
      3,
      13,
      2,
      25,
      4,
      5,
      3,
      16,
      2,
      29,
      4,
      9,
      3,
      19,
      6,
      30,
      2,
      13,
      1,
      23,
      6,
      4,
      2,
      14,
      3,
      27,
      4,
      8,
      2,
      18,
      3,
      0,
      4,
      11,
      3,
      22,
      5,
      2,
      3,
      14,
      1,
      26,
      5,
      6,
      3,
      16,
      3,
      28,
      4,
      10,
      2,
      20,
      6,
      30,
      3,
      11,
      2,
      24,
      4,
      4,
      3,
      15,
      2,
      25,
      6,
      8,
      1,
      19,
      2,
      29,
      6,
      9,
      3,
      22,
      4,
      3,
      2,
      13,
      3,
      25,
      4,
      7,
      2,
      17,
      3,
      27,
      6,
      9,
      1,
      21,
      5,
      1,
      3,
      11,
      3,
      23,
      4,
      5,
      2,
      15,
      3,
      25,
      6,
      6,
      2,
      19,
      1,
      29,
      6,
      10,
      2,
      22,
      4,
      3,
      3,
      14,
      2,
      24,
      6,
      6,
      1,
      18,
      2,
      28,
      6,
      8,
      3,
      20,
      4,
      2,
      2,
      12,
      3,
      24,
      4,
      4,
      3,
      16,
      2,
      26,
      6,
      6,
      3,
      17,
      2,
      0,
      4,
      10,
      3,
      22,
      4,
      3,
      2,
      14,
      3,
      24,
      6,
      5,
      2,
      17,
      1,
      28,
      6,
      9,
      2,
      21,
      4,
      1,
      3,
      13,
      2,
      23,
      6,
      5,
      1,
      15,
      3,
      27,
      5,
      7,
      3,
      19,
      1,
      0,
      5,
      10,
      3,
      22,
      4,
      2,
      3,
      13,
      2,
      24,
      6,
      4,
      3,
      15,
      2,
      27,
      4,
      8,
      3,
      20,
      4,
      1,
      2,
      11,
      3,
      22,
      6,
      3,
      2,
      15,
      1,
      25,
      6,
      7,
      2,
      17,
      3,
      29,
      4,
      10,
      2,
      21,
      6,
      1,
      3,
      13,
      1,
      24,
      5,
      5,
      3,
      15,
      3,
      27,
      4,
      8,
      2,
      19,
      6,
      1,
      1,
      12,
      2,
      22,
      6,
      3,
      3,
      14,
      2,
      26,
      4,
      6,
      3,
      18,
      2,
      28,
      6,
      10,
      1,
      20,
      6,
      2,
      2,
      12,
      3,
      24,
      4,
      5,
      2,
      16,
      3,
      28,
      4,
      9,
      2,
      19,
      6,
      30,
      3,
      12,
      1,
      23,
      5,
      3,
      3,
      14,
      3,
      26,
      4,
      7,
      2,
      17,
      3,
      28,
      6,
      9,
      2,
      21,
      4,
      1,
      3,
      13,
      2,
      25,
      4,
      5,
      3,
      16,
      2,
      27,
      6,
      9,
      1,
      19,
      6,
      30,
      2,
      11,
      3,
      23,
      4,
      4,
      2,
      14,
      3,
      27,
      4,
      7,
      3,
      18,
      2,
      28,
      6,
      11,
      1,
      22,
      5,
      2,
      3,
      12,
      3,
      25,
      4,
      6,
      2,
      16,
      3,
      26,
      6,
      8,
      2,
      20,
      4,
      30,
      3,
      11,
      2,
      24,
      4,
      4,
      3,
      15,
      2,
      25,
      6,
      8,
      1,
      18,
      3,
      29,
      5,
      9,
      3,
      22,
      4,
      3,
      2,
      13,
      3,
      23,
      6,
      6,
      1,
      17,
      2,
      27,
      6,
      7,
      3,
      20,
      4,
      1,
      2,
      11,
      3,
      23,
      4,
      5,
      2,
      15,
      3,
      25,
      6,
      6,
      2,
      19,
      1,
      29,
      6,
      10,
      2,
      20,
      6,
      3,
      1,
      14,
      2,
      24,
      6,
      4,
      3,
      17,
      1,
      28,
      5,
      8,
      3,
      20,
      4,
      1,
      3,
      12,
      2,
      22,
      6,
      2,
      3,
      14,
      2,
      26,
      4,
      6,
      3,
      17,
      2,
      0,
      4,
      10,
      3,
      20,
      6,
      1,
      2,
      14,
      1,
      24,
      6,
      5,
      2,
      15,
      3,
      28,
      4,
      9,
      2,
      19,
      6,
      1,
      1,
      12,
      3,
      23,
      5,
      3,
      3,
      15,
      1,
      27,
      5,
      7,
      3,
      17,
      3,
      29,
      4,
      11,
      2,
      21,
      6,
      1,
      3,
      12,
      2,
      25,
      4,
      5,
      3,
      16,
      2,
      28,
      4,
      9,
      3,
      19,
      6,
      30,
      2,
      12,
      1,
      23,
      6,
      4,
      2,
      14,
      3,
      26,
      4,
      8,
      2,
      18,
      3,
      0,
      4,
      10,
      3,
      22,
      5,
      2,
      3,
      14,
      1,
      25,
      5,
      6,
      3,
      16,
      3,
      28,
      4,
      9,
      2,
      20,
      6,
      30,
      3,
      11,
      2,
      23,
      4,
      4,
      3,
      15,
      2,
      27,
      4,
      7,
      3,
      19,
      2,
      29,
      6,
      11,
      1,
      21,
      6,
      3,
      2,
      13,
      3,
      25,
      4,
      6,
      2,
      17,
      3,
      27,
      6,
      9,
      1,
      20,
      5,
      30,
      3,
      10,
      3,
      22,
      4,
      3,
      2,
      14,
      3,
      24,
      6,
      5,
      2,
      17,
      1,
      28,
      6,
      9,
      2,
      21,
      4,
      1,
      3,
      13,
      2,
      23,
      6,
      5,
      1,
      16,
      2,
      27,
      6,
      7,
      3,
      19,
      4,
      30,
      2,
      11,
      3,
      23,
      4,
      3,
      3,
      14,
      2,
      25,
      6,
      5,
      3,
      16,
      2,
      28,
      4,
      9,
      3,
      21,
      4,
      2,
      2,
      12,
      3,
      23,
      6,
      4,
      2,
      16,
      1,
      26,
      6,
      8,
      2,
      20,
      4,
      30,
      3,
      11,
      2,
      22,
      6,
      4,
      1,
      14,
      3,
      25,
      5,
      6,
      3,
      18,
      1,
      29,
      5,
      9,
      3,
      22,
      4,
      2,
      3,
      13,
      2,
      23,
      6,
      4,
      3,
      15,
      2,
      27,
      4,
      7,
      3,
      20,
      4,
      1,
      2,
      11,
      3,
      21,
      6,
      3,
      2,
      15,
      1,
      25,
      6,
      6,
      2,
      17,
      3,
      29,
      4,
      10,
      2,
      20,
      6,
      3,
      1,
      13,
      3,
      24,
      5,
      4,
      3,
      17,
      1,
      28,
      5,
      8,
      3,
      18,
      6,
      1,
      1,
      12,
      2,
      22,
      6,
      2,
      3,
      14,
      2,
      26,
      4,
      6,
      3,
      17,
      2,
      28,
      6,
      10,
      1,
      20,
      6,
      1,
      2,
      12,
      3,
      24,
      4,
      5,
      2,
      15,
      3,
      28,
      4,
      9,
      2,
      19,
      6,
      33,
      3,
      12,
      1,
      23,
      5,
      3,
      3,
      13,
      3,
      25,
      4,
      6,
      2,
      16,
      3,
      26,
      6,
      8,
      2,
      20,
      4,
      30,
      3,
      11,
      2,
      24,
      4,
      4,
      3,
      15,
      2,
      25,
      6,
      8,
      1,
      18,
      6,
      33,
      2,
      9,
      3,
      22,
      4,
      3,
      2,
      13,
      3,
      25,
      4,
      6,
      3,
      17,
      2,
      27,
      6,
      9,
      1,
      21,
      5,
      1,
      3,
      11,
      3,
      23,
      4,
      5,
      2,
      15,
      3,
      25,
      6,
      6,
      2,
      19,
      4,
      33,
      3,
      10,
      2,
      22,
      4,
      3,
      3,
      14,
      2,
      24,
      6,
      6,
      1
    };
    private static readonly int[,] LunarMonthLen = new int[7, 14]
    {
      {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0
      },
      {
        0,
        30,
        29,
        29,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        0
      },
      {
        0,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        0
      },
      {
        0,
        30,
        30,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29,
        0
      },
      {
        0,
        30,
        29,
        29,
        29,
        30,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29
      },
      {
        0,
        30,
        29,
        30,
        29,
        30,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29
      },
      {
        0,
        30,
        30,
        30,
        29,
        30,
        30,
        29,
        30,
        29,
        30,
        29,
        30,
        29
      }
    };
    internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);
    internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int DatePartDayOfWeek = 4;
    private const int HebrewYearOf1AD = 3760;
    private const int FirstGregorianTableYear = 1583;
    private const int LastGregorianTableYear = 2239;
    private const int TABLESIZE = 656;
    private const int MinHebrewYear = 5343;
    private const int MaxHebrewYear = 5999;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

    /// <summary>
    ///   Возвращает минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.HebrewCalendar" /> тип.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.HebrewCalendar" /> тип, который эквивалентен первый момент 1 января 1583 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return HebrewCalendar.calendarMinValue;
      }
    }

    /// <summary>
    ///   Возвращает последнюю дату и время, поддерживаемые <see cref="T:System.Globalization.HebrewCalendar" /> тип.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.HebrewCalendar" /> тип, который эквивалентен самый последний момент сентября, 29, 2239 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return HebrewCalendar.calendarMaxValue;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.
    /// </returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunisolarCalendar;
      }
    }

    internal override int ID
    {
      get
      {
        return 8;
      }
    }

    private static void CheckHebrewYearValue(int y, int era, string varName)
    {
      HebrewCalendar.CheckEraRange(era);
      if (y > 5999 || y < 5343)
        throw new ArgumentOutOfRangeException(varName, string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 5343, (object) 5999));
    }

    private void CheckHebrewMonthValue(int year, int month, int era)
    {
      int monthsInYear = this.GetMonthsInYear(year, era);
      if (month < 1 || month > monthsInYear)
        throw new ArgumentOutOfRangeException(nameof (month), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) monthsInYear));
    }

    private void CheckHebrewDayValue(int year, int month, int day, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) daysInMonth));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != HebrewCalendar.HebrewEra)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    private static void CheckTicksRange(long ticks)
    {
      if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) HebrewCalendar.calendarMinValue, (object) HebrewCalendar.calendarMaxValue));
    }

    internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
    {
      switch (part)
      {
        case 0:
          return result.year;
        case 2:
          return result.month;
        case 3:
          return result.day;
        default:
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
      }
    }

    internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
    {
      int num1 = gregorianYear - 1583;
      if (num1 < 0 || num1 > 656)
        throw new ArgumentOutOfRangeException(nameof (gregorianYear));
      int index = num1 * 2;
      lunarDate.day = HebrewCalendar.HebrewTable[index];
      int num2 = HebrewCalendar.HebrewTable[index + 1];
      switch (lunarDate.day)
      {
        case 0:
          lunarDate.month = 5;
          lunarDate.day = 1;
          break;
        case 30:
          lunarDate.month = 3;
          break;
        case 31:
          lunarDate.month = 5;
          lunarDate.day = 2;
          break;
        case 32:
          lunarDate.month = 5;
          lunarDate.day = 3;
          break;
        case 33:
          lunarDate.month = 3;
          lunarDate.day = 29;
          break;
        default:
          lunarDate.month = 4;
          break;
      }
      return num2;
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      HebrewCalendar.CheckTicksRange(ticks);
      int year;
      int month;
      int day;
      new DateTime(ticks).GetDatePart(out year, out month, out day);
      HebrewCalendar.__DateBuffer lunarDate = new HebrewCalendar.__DateBuffer();
      lunarDate.year = year + 3760;
      int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(year, lunarDate);
      HebrewCalendar.__DateBuffer result = new HebrewCalendar.__DateBuffer();
      result.year = lunarDate.year;
      result.month = lunarDate.month;
      result.day = lunarDate.day;
      long absoluteDate = GregorianCalendar.GetAbsoluteDate(year, month, day);
      if (month == 1 && day == 1)
        return HebrewCalendar.GetResult(result, part);
      long num1 = absoluteDate - GregorianCalendar.GetAbsoluteDate(year, 1, 1);
      if (num1 + (long) lunarDate.day <= (long) HebrewCalendar.LunarMonthLen[lunarMonthDay, lunarDate.month])
      {
        result.day += (int) num1;
        return HebrewCalendar.GetResult(result, part);
      }
      ++result.month;
      result.day = 1;
      long num2 = num1 - (long) (HebrewCalendar.LunarMonthLen[lunarMonthDay, lunarDate.month] - lunarDate.day);
      if (num2 > 1L)
      {
        while (num2 > (long) HebrewCalendar.LunarMonthLen[lunarMonthDay, result.month])
        {
          num2 -= (long) HebrewCalendar.LunarMonthLen[lunarMonthDay, result.month++];
          if (result.month > 13 || HebrewCalendar.LunarMonthLen[lunarMonthDay, result.month] == 0)
          {
            ++result.year;
            lunarMonthDay = HebrewCalendar.HebrewTable[(year + 1 - 1583) * 2 + 1];
            result.month = 1;
          }
        }
        result.day += (int) (num2 - 1L);
      }
      return HebrewCalendar.GetResult(result, part);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число месяцев из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому необходимо добавить <paramref name="months" />.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанное число месяцев в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="months" /> имеет значение меньше –120 000 или больше 120 000.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      try
      {
        int datePart1 = this.GetDatePart(time.Ticks, 0);
        int datePart2 = this.GetDatePart(time.Ticks, 2);
        int day = this.GetDatePart(time.Ticks, 3);
        int month;
        if (months >= 0)
        {
          month = datePart2 + months;
          int monthsInYear;
          while (month > (monthsInYear = this.GetMonthsInYear(datePart1, 0)))
          {
            ++datePart1;
            month -= monthsInYear;
          }
        }
        else if ((month = datePart2 + months) <= 0)
        {
          months = -months;
          months -= datePart2;
          --datePart1;
          int monthsInYear;
          while (months > (monthsInYear = this.GetMonthsInYear(datePart1, 0)))
          {
            --datePart1;
            months -= monthsInYear;
          }
          month = this.GetMonthsInYear(datePart1, 0) - months;
        }
        int daysInMonth = this.GetDaysInMonth(datePart1, month);
        if (day > daysInMonth)
          day = daysInMonth;
        return new DateTime(this.ToDateTime(datePart1, month, day, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L);
      }
      catch (ArgumentException ex)
      {
        throw new ArgumentOutOfRangeException(nameof (months), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_AddValue"), Array.Empty<object>()));
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число лет из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" /> К которому необходимо добавить <paramref name="years" />.
    /// </param>
    /// <param name="years">Число лет для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число лет к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      int datePart = this.GetDatePart(time.Ticks, 0);
      int month = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
      int num = datePart + years;
      HebrewCalendar.CheckHebrewYearValue(num, 0, nameof (years));
      int monthsInYear = this.GetMonthsInYear(num, 0);
      if (month > monthsInYear)
        month = monthsInYear;
      int daysInMonth = this.GetDaysInMonth(num, month);
      if (day > daysInMonth)
        day = daysInMonth;
      long ticks = this.ToDateTime(num, month, day, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
      Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает день месяца в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 30, обозначающее день месяца в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    /// <summary>
    ///   Возвращает день недели в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DayOfWeek" /> значение, представляющее день недели в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    internal static int GetHebrewYearType(int year, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, nameof (year));
      return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
    }

    /// <summary>
    ///   Возвращает день года в заданном параметре <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 385, обозначающее день года в заданном параметре <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Время, указанное в параметре <paramref name="time" />, предшествует 17 сентября 1583 г. по григорианскому календарю или наступает позже значения, определяемого свойством <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetDayOfYear(DateTime time)
    {
      int year = this.GetYear(time);
      DateTime dateTime = year != 5343 ? this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0) : new DateTime(1582, 9, 27);
      return (int) ((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
    }

    /// <summary>
    ///   Возвращает число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="Calendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за диапазон, поддерживаемый текущим объектом <see cref="T:System.Globalization.HebrewCalendar" /> объекта.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      HebrewCalendar.CheckEraRange(era);
      int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
      this.CheckHebrewMonthValue(year, month, era);
      int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
      if (num == 0)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
      return num;
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>Число дней в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за диапазон, поддерживаемый текущим объектом <see cref="T:System.Globalization.HebrewCalendar" /> объекта.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      HebrewCalendar.CheckEraRange(era);
      int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
      if (hebrewYearType < 4)
        return 352 + hebrewYearType;
      return 382 + (hebrewYearType - 3);
    }

    /// <summary>
    ///   Возвращает значение эры в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в заданном <see cref="T:System.DateTime" />.
    ///    Возвращаемое значение всегда равно <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.
    /// </returns>
    public override int GetEra(DateTime time)
    {
      return HebrewCalendar.HebrewEra;
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.HebrewCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.HebrewCalendar" /> типа.
    ///    Возвращаемое значение всегда является массив, содержащий один элемент равен <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ HebrewCalendar.HebrewEra };
      }
    }

    /// <summary>
    ///   Возвращает месяц в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 13, представляющее месяц в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> является менее <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   Число месяцев в указанном году указанной эры.
    ///    Возвращенное значение равно 12 для обычного года или 13 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за диапазон, поддерживаемый текущим объектом <see cref="T:System.Globalization.HebrewCalendar" /> объекта.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      return !this.IsLeapYear(year, era) ? 12 : 13;
    }

    /// <summary>
    ///   Возвращает значение года в заданном <see cref="T:System.DateTime" /> значение.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в заданном <see cref="T:System.DateTime" /> значение.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> выходит за диапазон, поддерживаемый текущим объектом <see cref="T:System.Globalization.HebrewCalendar" /> объекта.
    /// </exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>
    ///   Определяет, является ли указанная дата указанной эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 30, представляющее день.
    /// </param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />...
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (this.IsLeapMonth(year, month, era))
      {
        this.CheckHebrewDayValue(year, month, day, era);
        return true;
      }
      if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
        return true;
      this.CheckHebrewDayValue(year, month, day, era);
      return false;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   Положительное целое число, указывающее високосный месяц в указанном году указанной эры.
    ///    Если возвращаемое значение равно 7 <paramref name="year" /> и <paramref name="era" /> Параметры указывают високосный год, или 0, если год не является високосным годом.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойству <paramref name="era" /> задано значение, отличное от <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> меньше года 5343 или больше года 5999.
    /// </exception>
    public override int GetLeapMonth(int year, int era)
    {
      return this.IsLeapYear(year, era) ? 7 : 0;
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный месяц — високосный; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      bool flag = this.IsLeapYear(year, era);
      this.CheckHebrewMonthValue(year, month, era);
      return flag && month == 7;
    }

    /// <summary>
    ///   Определяет, является ли указанный год указанной эры високосным годом.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный год — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> выходит за пределы диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, nameof (year));
      return (7L * (long) year + 1L) % 19L < 7L;
    }

    private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
    {
      if (month1 == month2)
        return day1 - day2;
      bool flag = month1 > month2;
      if (flag)
      {
        int num1 = month1;
        int num2 = day1;
        month1 = month2;
        day1 = day2;
        month2 = num1;
        day2 = num2;
      }
      int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
      ++month1;
      while (month1 < month2)
        num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
      int num4 = num3 + day2;
      if (!flag)
        return -num4;
      return num4;
    }

    private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
    {
      int num = hebrewYear - 3760;
      HebrewCalendar.__DateBuffer lunarDate = new HebrewCalendar.__DateBuffer();
      int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, lunarDate);
      if (hebrewMonth == lunarDate.month && hebrewDay == lunarDate.day)
        return new DateTime(num, 1, 1, hour, minute, second, millisecond);
      int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, lunarDate.month, lunarDate.day);
      return new DateTime(new DateTime(num, 1, 1).Ticks + (long) dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеет значение указанной даты и времени в заданной эре.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 13, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 30, представляющее день.
    /// </param>
    /// <param name="hour">
    ///   Целое число от 0 до 23, представляющее час.
    /// </param>
    /// <param name="minute">
    ///   Целое число от 0 до 59, представляющее минуту.
    /// </param>
    /// <param name="second">
    ///   Целое число от 0 до 59, представляющее секунду.
    /// </param>
    /// <param name="millisecond">
    ///   Целое число от 0 до 999, представляющее миллисекунду.
    /// </param>
    /// <param name="era">
    ///   Целое число, представляющее эру.
    ///    Укажите либо <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> или <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Имеет значение указанной даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> или <paramref name="era" /> выходит за диапазон, поддерживаемый текущим объектом <see cref="T:System.Globalization.HebrewCalendar" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше 0 или больше 23.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="minute" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="second" /> меньше 0 или больше 59.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="millisecond" /> имеет значение меньше 0 или больше 999.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, nameof (year));
      this.CheckHebrewMonthValue(year, month, era);
      this.CheckHebrewDayValue(year, month, day, era);
      DateTime gregorian = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
      HebrewCalendar.CheckTicksRange(gregorian.Ticks);
      return gregorian;
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Globalization.HebrewCalendar" /> объект доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   В наборе операций меньше 5343 значение года еврейского календаря, но не равно 99 или больше 5999 значение года.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value != 99)
          HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, nameof (value));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> Свойства для определения века.
    /// </summary>
    /// <param name="year">
    ///   Год из 2 цифр от 0 до 99 или четырехзначный год еврейского календаря с 5343 по 5999.
    /// </param>
    /// <returns>
    ///   Если <paramref name="year" /> параметр представляет 2-значным годом, возвращается соответствующий четырехзначный год.
    ///    Если <paramref name="year" /> параметр представляет четырехзначный год, возвращаемое значение не изменяется <paramref name="year" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="year" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="year" /> является менее <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> или больше, чем <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 5999 || year < 5343)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 5343, (object) 5999));
      return year;
    }

    internal class __DateBuffer
    {
      internal int year;
      internal int month;
      internal int day;
    }
  }
}
