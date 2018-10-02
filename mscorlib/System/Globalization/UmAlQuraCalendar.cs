// Decompiled with JetBrains decompiler
// Type: System.Globalization.UmAlQuraCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>Представляет саудовский календарь.</summary>
  [Serializable]
  public class UmAlQuraCalendar : Calendar
  {
    private static readonly UmAlQuraCalendar.DateMapping[] HijriYearInfo = UmAlQuraCalendar.InitDateMapping();
    internal static DateTime minDate = new DateTime(1900, 4, 30);
    internal static DateTime maxDate = new DateTime(new DateTime(2077, 11, 16, 23, 59, 59, 999).Ticks + 9999L);
    internal const int MinCalendarYear = 1318;
    internal const int MaxCalendarYear = 1500;
    /// <summary>
    ///   Представляет текущую эру.
    ///    Это поле является константой.
    /// </summary>
    public const int UmAlQuraEra = 1;
    internal const int DateCycle = 30;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;

    private static UmAlQuraCalendar.DateMapping[] InitDateMapping()
    {
      short[] numArray = new short[736]
      {
        (short) 746,
        (short) 1900,
        (short) 4,
        (short) 30,
        (short) 1769,
        (short) 1901,
        (short) 4,
        (short) 19,
        (short) 3794,
        (short) 1902,
        (short) 4,
        (short) 9,
        (short) 3748,
        (short) 1903,
        (short) 3,
        (short) 30,
        (short) 3402,
        (short) 1904,
        (short) 3,
        (short) 18,
        (short) 2710,
        (short) 1905,
        (short) 3,
        (short) 7,
        (short) 1334,
        (short) 1906,
        (short) 2,
        (short) 24,
        (short) 2741,
        (short) 1907,
        (short) 2,
        (short) 13,
        (short) 3498,
        (short) 1908,
        (short) 2,
        (short) 3,
        (short) 2980,
        (short) 1909,
        (short) 1,
        (short) 23,
        (short) 2889,
        (short) 1910,
        (short) 1,
        (short) 12,
        (short) 2707,
        (short) 1911,
        (short) 1,
        (short) 1,
        (short) 1323,
        (short) 1911,
        (short) 12,
        (short) 21,
        (short) 2647,
        (short) 1912,
        (short) 12,
        (short) 9,
        (short) 1206,
        (short) 1913,
        (short) 11,
        (short) 29,
        (short) 2741,
        (short) 1914,
        (short) 11,
        (short) 18,
        (short) 1450,
        (short) 1915,
        (short) 11,
        (short) 8,
        (short) 3413,
        (short) 1916,
        (short) 10,
        (short) 27,
        (short) 3370,
        (short) 1917,
        (short) 10,
        (short) 17,
        (short) 2646,
        (short) 1918,
        (short) 10,
        (short) 6,
        (short) 1198,
        (short) 1919,
        (short) 9,
        (short) 25,
        (short) 2397,
        (short) 1920,
        (short) 9,
        (short) 13,
        (short) 748,
        (short) 1921,
        (short) 9,
        (short) 3,
        (short) 1749,
        (short) 1922,
        (short) 8,
        (short) 23,
        (short) 1706,
        (short) 1923,
        (short) 8,
        (short) 13,
        (short) 1365,
        (short) 1924,
        (short) 8,
        (short) 1,
        (short) 1195,
        (short) 1925,
        (short) 7,
        (short) 21,
        (short) 2395,
        (short) 1926,
        (short) 7,
        (short) 10,
        (short) 698,
        (short) 1927,
        (short) 6,
        (short) 30,
        (short) 1397,
        (short) 1928,
        (short) 6,
        (short) 18,
        (short) 2994,
        (short) 1929,
        (short) 6,
        (short) 8,
        (short) 1892,
        (short) 1930,
        (short) 5,
        (short) 29,
        (short) 1865,
        (short) 1931,
        (short) 5,
        (short) 18,
        (short) 1621,
        (short) 1932,
        (short) 5,
        (short) 6,
        (short) 683,
        (short) 1933,
        (short) 4,
        (short) 25,
        (short) 1371,
        (short) 1934,
        (short) 4,
        (short) 14,
        (short) 2778,
        (short) 1935,
        (short) 4,
        (short) 4,
        (short) 1748,
        (short) 1936,
        (short) 3,
        (short) 24,
        (short) 3785,
        (short) 1937,
        (short) 3,
        (short) 13,
        (short) 3474,
        (short) 1938,
        (short) 3,
        (short) 3,
        (short) 3365,
        (short) 1939,
        (short) 2,
        (short) 20,
        (short) 2637,
        (short) 1940,
        (short) 2,
        (short) 9,
        (short) 685,
        (short) 1941,
        (short) 1,
        (short) 28,
        (short) 1389,
        (short) 1942,
        (short) 1,
        (short) 17,
        (short) 2922,
        (short) 1943,
        (short) 1,
        (short) 7,
        (short) 2898,
        (short) 1943,
        (short) 12,
        (short) 28,
        (short) 2725,
        (short) 1944,
        (short) 12,
        (short) 16,
        (short) 2635,
        (short) 1945,
        (short) 12,
        (short) 5,
        (short) 1175,
        (short) 1946,
        (short) 11,
        (short) 24,
        (short) 2359,
        (short) 1947,
        (short) 11,
        (short) 13,
        (short) 694,
        (short) 1948,
        (short) 11,
        (short) 2,
        (short) 1397,
        (short) 1949,
        (short) 10,
        (short) 22,
        (short) 3434,
        (short) 1950,
        (short) 10,
        (short) 12,
        (short) 3410,
        (short) 1951,
        (short) 10,
        (short) 2,
        (short) 2710,
        (short) 1952,
        (short) 9,
        (short) 20,
        (short) 2349,
        (short) 1953,
        (short) 9,
        (short) 9,
        (short) 605,
        (short) 1954,
        (short) 8,
        (short) 29,
        (short) 1245,
        (short) 1955,
        (short) 8,
        (short) 18,
        (short) 2778,
        (short) 1956,
        (short) 8,
        (short) 7,
        (short) 1492,
        (short) 1957,
        (short) 7,
        (short) 28,
        (short) 3497,
        (short) 1958,
        (short) 7,
        (short) 17,
        (short) 3410,
        (short) 1959,
        (short) 7,
        (short) 7,
        (short) 2730,
        (short) 1960,
        (short) 6,
        (short) 25,
        (short) 1238,
        (short) 1961,
        (short) 6,
        (short) 14,
        (short) 2486,
        (short) 1962,
        (short) 6,
        (short) 3,
        (short) 884,
        (short) 1963,
        (short) 5,
        (short) 24,
        (short) 1897,
        (short) 1964,
        (short) 5,
        (short) 12,
        (short) 1874,
        (short) 1965,
        (short) 5,
        (short) 2,
        (short) 1701,
        (short) 1966,
        (short) 4,
        (short) 21,
        (short) 1355,
        (short) 1967,
        (short) 4,
        (short) 10,
        (short) 2731,
        (short) 1968,
        (short) 3,
        (short) 29,
        (short) 1370,
        (short) 1969,
        (short) 3,
        (short) 19,
        (short) 2773,
        (short) 1970,
        (short) 3,
        (short) 8,
        (short) 3538,
        (short) 1971,
        (short) 2,
        (short) 26,
        (short) 3492,
        (short) 1972,
        (short) 2,
        (short) 16,
        (short) 3401,
        (short) 1973,
        (short) 2,
        (short) 4,
        (short) 2709,
        (short) 1974,
        (short) 1,
        (short) 24,
        (short) 1325,
        (short) 1975,
        (short) 1,
        (short) 13,
        (short) 2653,
        (short) 1976,
        (short) 1,
        (short) 2,
        (short) 1370,
        (short) 1976,
        (short) 12,
        (short) 22,
        (short) 2773,
        (short) 1977,
        (short) 12,
        (short) 11,
        (short) 1706,
        (short) 1978,
        (short) 12,
        (short) 1,
        (short) 1685,
        (short) 1979,
        (short) 11,
        (short) 20,
        (short) 1323,
        (short) 1980,
        (short) 11,
        (short) 8,
        (short) 2647,
        (short) 1981,
        (short) 10,
        (short) 28,
        (short) 1198,
        (short) 1982,
        (short) 10,
        (short) 18,
        (short) 2422,
        (short) 1983,
        (short) 10,
        (short) 7,
        (short) 1388,
        (short) 1984,
        (short) 9,
        (short) 26,
        (short) 2901,
        (short) 1985,
        (short) 9,
        (short) 15,
        (short) 2730,
        (short) 1986,
        (short) 9,
        (short) 5,
        (short) 2645,
        (short) 1987,
        (short) 8,
        (short) 25,
        (short) 1197,
        (short) 1988,
        (short) 8,
        (short) 13,
        (short) 2397,
        (short) 1989,
        (short) 8,
        (short) 2,
        (short) 730,
        (short) 1990,
        (short) 7,
        (short) 23,
        (short) 1497,
        (short) 1991,
        (short) 7,
        (short) 12,
        (short) 3506,
        (short) 1992,
        (short) 7,
        (short) 1,
        (short) 2980,
        (short) 1993,
        (short) 6,
        (short) 21,
        (short) 2890,
        (short) 1994,
        (short) 6,
        (short) 10,
        (short) 2645,
        (short) 1995,
        (short) 5,
        (short) 30,
        (short) 693,
        (short) 1996,
        (short) 5,
        (short) 18,
        (short) 1397,
        (short) 1997,
        (short) 5,
        (short) 7,
        (short) 2922,
        (short) 1998,
        (short) 4,
        (short) 27,
        (short) 3026,
        (short) 1999,
        (short) 4,
        (short) 17,
        (short) 3012,
        (short) 2000,
        (short) 4,
        (short) 6,
        (short) 2953,
        (short) 2001,
        (short) 3,
        (short) 26,
        (short) 2709,
        (short) 2002,
        (short) 3,
        (short) 15,
        (short) 1325,
        (short) 2003,
        (short) 3,
        (short) 4,
        (short) 1453,
        (short) 2004,
        (short) 2,
        (short) 21,
        (short) 2922,
        (short) 2005,
        (short) 2,
        (short) 10,
        (short) 1748,
        (short) 2006,
        (short) 1,
        (short) 31,
        (short) 3529,
        (short) 2007,
        (short) 1,
        (short) 20,
        (short) 3474,
        (short) 2008,
        (short) 1,
        (short) 10,
        (short) 2726,
        (short) 2008,
        (short) 12,
        (short) 29,
        (short) 2390,
        (short) 2009,
        (short) 12,
        (short) 18,
        (short) 686,
        (short) 2010,
        (short) 12,
        (short) 7,
        (short) 1389,
        (short) 2011,
        (short) 11,
        (short) 26,
        (short) 874,
        (short) 2012,
        (short) 11,
        (short) 15,
        (short) 2901,
        (short) 2013,
        (short) 11,
        (short) 4,
        (short) 2730,
        (short) 2014,
        (short) 10,
        (short) 25,
        (short) 2381,
        (short) 2015,
        (short) 10,
        (short) 14,
        (short) 1181,
        (short) 2016,
        (short) 10,
        (short) 2,
        (short) 2397,
        (short) 2017,
        (short) 9,
        (short) 21,
        (short) 698,
        (short) 2018,
        (short) 9,
        (short) 11,
        (short) 1461,
        (short) 2019,
        (short) 8,
        (short) 31,
        (short) 1450,
        (short) 2020,
        (short) 8,
        (short) 20,
        (short) 3413,
        (short) 2021,
        (short) 8,
        (short) 9,
        (short) 2714,
        (short) 2022,
        (short) 7,
        (short) 30,
        (short) 2350,
        (short) 2023,
        (short) 7,
        (short) 19,
        (short) 622,
        (short) 2024,
        (short) 7,
        (short) 7,
        (short) 1373,
        (short) 2025,
        (short) 6,
        (short) 26,
        (short) 2778,
        (short) 2026,
        (short) 6,
        (short) 16,
        (short) 1748,
        (short) 2027,
        (short) 6,
        (short) 6,
        (short) 1701,
        (short) 2028,
        (short) 5,
        (short) 25,
        (short) 1355,
        (short) 2029,
        (short) 5,
        (short) 14,
        (short) 2711,
        (short) 2030,
        (short) 5,
        (short) 3,
        (short) 1358,
        (short) 2031,
        (short) 4,
        (short) 23,
        (short) 2734,
        (short) 2032,
        (short) 4,
        (short) 11,
        (short) 1452,
        (short) 2033,
        (short) 4,
        (short) 1,
        (short) 2985,
        (short) 2034,
        (short) 3,
        (short) 21,
        (short) 3474,
        (short) 2035,
        (short) 3,
        (short) 11,
        (short) 2853,
        (short) 2036,
        (short) 2,
        (short) 28,
        (short) 1611,
        (short) 2037,
        (short) 2,
        (short) 16,
        (short) 3243,
        (short) 2038,
        (short) 2,
        (short) 5,
        (short) 1370,
        (short) 2039,
        (short) 1,
        (short) 26,
        (short) 2901,
        (short) 2040,
        (short) 1,
        (short) 15,
        (short) 1746,
        (short) 2041,
        (short) 1,
        (short) 4,
        (short) 3749,
        (short) 2041,
        (short) 12,
        (short) 24,
        (short) 3658,
        (short) 2042,
        (short) 12,
        (short) 14,
        (short) 2709,
        (short) 2043,
        (short) 12,
        (short) 3,
        (short) 1325,
        (short) 2044,
        (short) 11,
        (short) 21,
        (short) 2733,
        (short) 2045,
        (short) 11,
        (short) 10,
        (short) 876,
        (short) 2046,
        (short) 10,
        (short) 31,
        (short) 1881,
        (short) 2047,
        (short) 10,
        (short) 20,
        (short) 1746,
        (short) 2048,
        (short) 10,
        (short) 9,
        (short) 1685,
        (short) 2049,
        (short) 9,
        (short) 28,
        (short) 1325,
        (short) 2050,
        (short) 9,
        (short) 17,
        (short) 2651,
        (short) 2051,
        (short) 9,
        (short) 6,
        (short) 1210,
        (short) 2052,
        (short) 8,
        (short) 26,
        (short) 2490,
        (short) 2053,
        (short) 8,
        (short) 15,
        (short) 948,
        (short) 2054,
        (short) 8,
        (short) 5,
        (short) 2921,
        (short) 2055,
        (short) 7,
        (short) 25,
        (short) 2898,
        (short) 2056,
        (short) 7,
        (short) 14,
        (short) 2726,
        (short) 2057,
        (short) 7,
        (short) 3,
        (short) 1206,
        (short) 2058,
        (short) 6,
        (short) 22,
        (short) 2413,
        (short) 2059,
        (short) 6,
        (short) 11,
        (short) 748,
        (short) 2060,
        (short) 5,
        (short) 31,
        (short) 1753,
        (short) 2061,
        (short) 5,
        (short) 20,
        (short) 3762,
        (short) 2062,
        (short) 5,
        (short) 10,
        (short) 3412,
        (short) 2063,
        (short) 4,
        (short) 30,
        (short) 3370,
        (short) 2064,
        (short) 4,
        (short) 18,
        (short) 2646,
        (short) 2065,
        (short) 4,
        (short) 7,
        (short) 1198,
        (short) 2066,
        (short) 3,
        (short) 27,
        (short) 2413,
        (short) 2067,
        (short) 3,
        (short) 16,
        (short) 3434,
        (short) 2068,
        (short) 3,
        (short) 5,
        (short) 2900,
        (short) 2069,
        (short) 2,
        (short) 23,
        (short) 2857,
        (short) 2070,
        (short) 2,
        (short) 12,
        (short) 2707,
        (short) 2071,
        (short) 2,
        (short) 1,
        (short) 1323,
        (short) 2072,
        (short) 1,
        (short) 21,
        (short) 2647,
        (short) 2073,
        (short) 1,
        (short) 9,
        (short) 1334,
        (short) 2073,
        (short) 12,
        (short) 30,
        (short) 2741,
        (short) 2074,
        (short) 12,
        (short) 19,
        (short) 1706,
        (short) 2075,
        (short) 12,
        (short) 9,
        (short) 3731,
        (short) 2076,
        (short) 11,
        (short) 27,
        (short) 0,
        (short) 2077,
        (short) 11,
        (short) 17
      };
      UmAlQuraCalendar.DateMapping[] dateMappingArray = new UmAlQuraCalendar.DateMapping[numArray.Length / 4];
      for (int index = 0; index < dateMappingArray.Length; ++index)
        dateMappingArray[index] = new UmAlQuraCalendar.DateMapping((int) numArray[index * 4], (int) numArray[index * 4 + 1], (int) numArray[index * 4 + 2], (int) numArray[index * 4 + 3]);
      return dateMappingArray;
    }

    /// <summary>
    ///   Получает минимальное значение даты и времени, поддерживаемое этим календарем.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и времени, поддерживаемое классом <see cref="T:System.Globalization.UmAlQuraCalendar" />, которое эквивалентно первому мгновению 30 апреля 1900 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return UmAlQuraCalendar.minDate;
      }
    }

    /// <summary>
    ///   Возвращает последнюю дату и время, поддерживаемые этим календарем.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса, который эквивалентен самый последний момент 16 ноября 2077 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return UmAlQuraCalendar.maxDate;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />.
    /// </returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunarCalendar;
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 6;
      }
    }

    internal override int ID
    {
      get
      {
        return 23;
      }
    }

    /// <summary>
    ///   Возвращает число дней в году, предшествующий год, который задается параметром <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" /> свойство.
    /// </summary>
    /// <returns>
    ///   Число дней в году, который предшествует года в заданном по <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" />.
    /// </returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 355;
      }
    }

    private static void ConvertHijriToGregorian(int HijriYear, int HijriMonth, int HijriDay, ref int yg, ref int mg, ref int dg)
    {
      int num = HijriDay - 1;
      int index1 = HijriYear - 1318;
      DateTime gregorianDate = UmAlQuraCalendar.HijriYearInfo[index1].GregorianDate;
      int monthsLengthFlags = UmAlQuraCalendar.HijriYearInfo[index1].HijriMonthsLengthFlags;
      for (int index2 = 1; index2 < HijriMonth; ++index2)
      {
        num += 29 + (monthsLengthFlags & 1);
        monthsLengthFlags >>= 1;
      }
      gregorianDate.AddDays((double) num).GetDatePart(out yg, out mg, out dg);
    }

    private static long GetAbsoluteDateUmAlQura(int year, int month, int day)
    {
      int yg = 0;
      int mg = 0;
      int dg = 0;
      UmAlQuraCalendar.ConvertHijriToGregorian(year, month, day, ref yg, ref mg, ref dg);
      return GregorianCalendar.GetAbsoluteDate(yg, mg, dg);
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < UmAlQuraCalendar.minDate.Ticks || ticks > UmAlQuraCalendar.maxDate.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) UmAlQuraCalendar.minDate, (object) UmAlQuraCalendar.maxDate));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException(nameof (era), Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      UmAlQuraCalendar.CheckEraRange(era);
      if (year < 1318 || year > 1500)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    private static void ConvertGregorianToHijri(DateTime time, ref int HijriYear, ref int HijriMonth, ref int HijriDay)
    {
      int index = (int) ((time.Ticks - UmAlQuraCalendar.minDate.Ticks) / 864000000000L) / 355;
      do
        ;
      while (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[++index].GregorianDate) > 0);
      if (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[index].GregorianDate) != 0)
        --index;
      TimeSpan timeSpan = time.Subtract(UmAlQuraCalendar.HijriYearInfo[index].GregorianDate);
      int num1 = index + 1318;
      int num2 = 1;
      int num3 = 1;
      double totalDays = timeSpan.TotalDays;
      int monthsLengthFlags = UmAlQuraCalendar.HijriYearInfo[index].HijriMonthsLengthFlags;
      int num4 = 29 + (monthsLengthFlags & 1);
      while (totalDays >= (double) num4)
      {
        totalDays -= (double) num4;
        monthsLengthFlags >>= 1;
        num4 = 29 + (monthsLengthFlags & 1);
        ++num2;
      }
      int num5 = num3 + (int) totalDays;
      HijriDay = num5;
      HijriMonth = num2;
      HijriYear = num1;
    }

    internal virtual int GetDatePart(DateTime time, int part)
    {
      int HijriYear = 0;
      int HijriMonth = 0;
      int HijriDay = 0;
      UmAlQuraCalendar.CheckTicksRange(time.Ticks);
      UmAlQuraCalendar.ConvertGregorianToHijri(time, ref HijriYear, ref HijriMonth, ref HijriDay);
      switch (part)
      {
        case 0:
          return HijriYear;
        case 1:
          return (int) (UmAlQuraCalendar.GetAbsoluteDateUmAlQura(HijriYear, HijriMonth, HijriDay) - UmAlQuraCalendar.GetAbsoluteDateUmAlQura(HijriYear, 1, 1) + 1L);
        case 2:
          return HijriMonth;
        case 3:
          return HijriDay;
        default:
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
      }
    }

    /// <summary>
    ///   Вычисляет дату, определенное количество месяцев от указанной исходной даты.
    /// </summary>
    /// <param name="time">
    ///   Дата, к которому добавляются месяцы.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <param name="months">
    ///   Положительное или отрицательное число месяцев для добавления.
    /// </param>
    /// <returns>
    ///   Дату, полученную путем добавления числа месяцев, указанных <paramref name="months" /> параметр к указанной дате <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Полученный дата находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="months" /> имеет значение меньше –120 000 или больше 120 000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемого этим календарем.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException(nameof (months), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time, 0);
      int datePart2 = this.GetDatePart(time, 2);
      int day = this.GetDatePart(time, 3);
      int num = datePart2 - 1 + months;
      int month;
      int year;
      if (num >= 0)
      {
        month = num % 12 + 1;
        year = datePart1 + num / 12;
      }
      else
      {
        month = 12 + (num + 1) % 12;
        year = datePart1 + (num - 11) / 12;
      }
      if (day > 29)
      {
        int daysInMonth = this.GetDaysInMonth(year, month);
        if (day > daysInMonth)
          day = daysInMonth;
      }
      UmAlQuraCalendar.CheckYearRange(year, 1);
      DateTime dateTime = new DateTime(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day) * 864000000000L + time.Ticks % 864000000000L);
      Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return dateTime;
    }

    /// <summary>
    ///   Вычисляет дату, указанное число лет от указанной исходной даты.
    /// </summary>
    /// <param name="time">
    ///   Дата, к которому добавляются годы.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <param name="years">
    ///   Положительное или отрицательное число лет для добавления.
    /// </param>
    /// <returns>
    ///   Дату, полученную путем добавления указанное количество лет <paramref name="years" /> параметр к указанной дате <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Полученный дата находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="years" /> меньше параметр больше или равно 10 000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемого этим календарем.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>
    ///   Вычисляет число месяца, в который происходит указанной даты.
    /// </summary>
    /// <param name="time">
    ///   Значение даты для чтения.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 30, обозначающее день месяца в заданном по <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time, 3);
    }

    /// <summary>
    ///   Вычисляет день недели, в который происходит указанной даты.
    /// </summary>
    /// <param name="time">
    ///   Значение даты для чтения.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.DayOfWeek" /> значение, представляющее день недели, определяемое <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>
    ///   Рассчитывает день года, на который приходится указанная дата.
    /// </summary>
    /// <param name="time">
    ///   Значение даты для чтения.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 355, обозначающее день года, указанного параметром <paramref name="time" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time, 1);
    }

    /// <summary>
    ///   Вычисляет число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Число дней в указанном месяце указанных года и эры.
    ///    Возвращенное значение равно 29 для обычного года или 30 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      return (UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags & 1 << month - 1) == 0 ? 29 : 30;
    }

    internal static int RealGetDaysInYear(int year)
    {
      int num = 0;
      int monthsLengthFlags = UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags;
      for (int index = 1; index <= 12; ++index)
      {
        num += 29 + (monthsLengthFlags & 1);
        monthsLengthFlags >>= 1;
      }
      return num;
    }

    /// <summary>
    ///   Вычисляет количество дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Число дней в указанном году указанной эры.
    ///    Число дней равно 354 месяцам для обычного года или 355 для високосного.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return UmAlQuraCalendar.RealGetDaysInYear(year);
    }

    /// <summary>Вычисляет эры, в котором происходит указанной даты.</summary>
    /// <param name="time">Значение даты для чтения.</param>
    /// <returns>
    ///   Всегда возвращает <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" /> значение.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetEra(DateTime time)
    {
      UmAlQuraCalendar.CheckTicksRange(time.Ticks);
      return 1;
    }

    /// <summary>
    ///   Получает список эр, поддерживаемых текущим <see cref="T:System.Globalization.UmAlQuraCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив, состоящий из одного элемента, значение которого является <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ 1 };
      }
    }

    /// <summary>
    ///   Вычисляет месяца, в котором происходит указанной даты.
    /// </summary>
    /// <param name="time">
    ///   Значение даты для чтения.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 12, представляющее месяц в дате, указанной в <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time, 2);
    }

    /// <summary>
    ///   Вычисляет число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuaraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>Всегда 12.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="era" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return 12;
    }

    /// <summary>
    ///   Рассчитывает год даты, представленной указанным <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Значение даты для чтения.
    ///    Класс <see cref="T:System.Globalization.UmAlQuraCalendar" /> поддерживает только даты с 04/30/1900 00.00.00 (по григорианскому календарю) по 11/16/2077 23:59:59 (по григорианскому календарю).
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год, указанный по <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time, 0);
    }

    /// <summary>Определяет, является ли указанный день високосным.</summary>
    /// <param name="year">Год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 30, представляющее день.
    /// </param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный день — високосный; в противном случае — значение <see langword="false" />.
    ///    Возвращаемое значение всегда равно <see langword="false" /> поскольку <see cref="T:System.Globalization.UmAlQuraCalendar" /> класс не поддерживает дней високосным.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (day >= 1 && day <= 29)
      {
        UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
        return false;
      }
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      return false;
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Всегда равно 0 из-за <see cref="T:System.Globalization.UmAlQuraCalendar" /> класс не поддерживает месяцев високосным.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> — меньше 1318 или больше 1450.
    /// 
    ///   -или-
    /// 
    ///   Свойству <paramref name="era" /> задано значение, отличное от <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </exception>
    public override int GetLeapMonth(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Всегда <see langword="false" /> поскольку <see cref="T:System.Globalization.UmAlQuraCalendar" /> класс не поддерживает месяцев високосным.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      return false;
    }

    /// <summary>
    ///   Определяет, является ли указанный год указанной эры високосным годом.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный год — високосный; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return UmAlQuraCalendar.RealGetDaysInYear(year) == 355;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеющим значение указанной даты, времени и эры.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 29, представляющее день.
    /// </param>
    /// <param name="hour">
    ///   Целое число от 0 до 23, представляющее час.
    /// </param>
    /// <param name="minute">
    ///   Целое число от 0 до 59, представляющее минуты.
    /// </param>
    /// <param name="second">
    ///   Целое число от 0 до 59, представляющее секунды.
    /// </param>
    /// <param name="millisecond">
    ///   Целое число от 0 до 999, представляющее миллисекунду.
    /// </param>
    /// <param name="era">
    ///   Эра.
    ///    Следует задать <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> или <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Имеет значение указанной даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.UmAlQuraCalendar" /> класса.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="hour" /> меньше нуля или больше 23.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="minute" /> меньше нуля или больше 59.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="second" /> меньше нуля или больше 59.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="millisecond" /> меньше нуля или больше 999.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      if (day >= 1 && day <= 29)
      {
        UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      }
      else
      {
        int daysInMonth = this.GetDaysInMonth(year, month, era);
        if (day < 1 || day > daysInMonth)
          throw new ArgumentOutOfRangeException(nameof (day), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      }
      long absoluteDateUmAlQura = UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day);
      if (absoluteDateUmAlQura >= 0L)
        return new DateTime(absoluteDateUmAlQura * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот календарь доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   В операции задания, календарный год саудовского значение — меньше, но не 99 или больше 1450.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
        return this.twoDigitYearMax;
      }
      set
      {
        if (value != 99 && (value < 1318 || value > 1500))
          throw new ArgumentOutOfRangeException(nameof (value), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
        this.VerifyWritable();
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.UmAlQuraCalendar.TwoDigitYearMax" /> Свойства для определения века.
    /// </summary>
    /// <param name="year">
    ///   Год из 2 цифр от 0 до 99 или четырехзначный год саудовского календаря с 1318 по 1450.
    /// </param>
    /// <returns>
    ///   Если <paramref name="year" /> параметр представляет 2-значным годом, возвращается соответствующий четырехзначный год.
    ///    Если <paramref name="year" /> параметр представляет четырехзначный год, возвращаемое значение не изменяется <paramref name="year" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится за пределами диапазона, поддерживаемые этим календарем.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year < 1318 || year > 1500)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
      return year;
    }

    internal struct DateMapping
    {
      internal int HijriMonthsLengthFlags;
      internal DateTime GregorianDate;

      internal DateMapping(int MonthsLengthFlags, int GYear, int GMonth, int GDay)
      {
        this.HijriMonthsLengthFlags = MonthsLengthFlags;
        this.GregorianDate = new DateTime(GYear, GMonth, GDay);
      }
    }
  }
}
