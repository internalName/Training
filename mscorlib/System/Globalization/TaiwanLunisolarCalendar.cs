// Decompiled with JetBrains decompiler
// Type: System.Globalization.TaiwanLunisolarCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>
  ///   Представляет тайваньский лунно-солнечный календарь.
  ///    Что касается тайваньский календарь годы подсчитываются с помощью григорианского календаря, а дни и месяцы рассчитываются с использованием лунно-солнечный календарь.
  /// </summary>
  [Serializable]
  public class TaiwanLunisolarCalendar : EastAsianLunisolarCalendar
  {
    internal static EraInfo[] taiwanLunisolarEraInfo = new EraInfo[1]
    {
      new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
    };
    internal static DateTime minDate = new DateTime(1912, 2, 18);
    internal static DateTime maxDate = new DateTime(new DateTime(2051, 2, 10, 23, 59, 59, 999).Ticks + 9999L);
    private static readonly int[,] yinfo = new int[139, 4]
    {
      {
        0,
        2,
        18,
        42192
      },
      {
        0,
        2,
        6,
        53840
      },
      {
        5,
        1,
        26,
        54568
      },
      {
        0,
        2,
        14,
        46400
      },
      {
        0,
        2,
        3,
        54944
      },
      {
        2,
        1,
        23,
        38608
      },
      {
        0,
        2,
        11,
        38320
      },
      {
        7,
        2,
        1,
        18872
      },
      {
        0,
        2,
        20,
        18800
      },
      {
        0,
        2,
        8,
        42160
      },
      {
        5,
        1,
        28,
        45656
      },
      {
        0,
        2,
        16,
        27216
      },
      {
        0,
        2,
        5,
        27968
      },
      {
        4,
        1,
        24,
        44456
      },
      {
        0,
        2,
        13,
        11104
      },
      {
        0,
        2,
        2,
        38256
      },
      {
        2,
        1,
        23,
        18808
      },
      {
        0,
        2,
        10,
        18800
      },
      {
        6,
        1,
        30,
        25776
      },
      {
        0,
        2,
        17,
        54432
      },
      {
        0,
        2,
        6,
        59984
      },
      {
        5,
        1,
        26,
        27976
      },
      {
        0,
        2,
        14,
        23248
      },
      {
        0,
        2,
        4,
        11104
      },
      {
        3,
        1,
        24,
        37744
      },
      {
        0,
        2,
        11,
        37600
      },
      {
        7,
        1,
        31,
        51560
      },
      {
        0,
        2,
        19,
        51536
      },
      {
        0,
        2,
        8,
        54432
      },
      {
        6,
        1,
        27,
        55888
      },
      {
        0,
        2,
        15,
        46416
      },
      {
        0,
        2,
        5,
        22176
      },
      {
        4,
        1,
        25,
        43736
      },
      {
        0,
        2,
        13,
        9680
      },
      {
        0,
        2,
        2,
        37584
      },
      {
        2,
        1,
        22,
        51544
      },
      {
        0,
        2,
        10,
        43344
      },
      {
        7,
        1,
        29,
        46248
      },
      {
        0,
        2,
        17,
        27808
      },
      {
        0,
        2,
        6,
        46416
      },
      {
        5,
        1,
        27,
        21928
      },
      {
        0,
        2,
        14,
        19872
      },
      {
        0,
        2,
        3,
        42416
      },
      {
        3,
        1,
        24,
        21176
      },
      {
        0,
        2,
        12,
        21168
      },
      {
        8,
        1,
        31,
        43344
      },
      {
        0,
        2,
        18,
        59728
      },
      {
        0,
        2,
        8,
        27296
      },
      {
        6,
        1,
        28,
        44368
      },
      {
        0,
        2,
        15,
        43856
      },
      {
        0,
        2,
        5,
        19296
      },
      {
        4,
        1,
        25,
        42352
      },
      {
        0,
        2,
        13,
        42352
      },
      {
        0,
        2,
        2,
        21088
      },
      {
        3,
        1,
        21,
        59696
      },
      {
        0,
        2,
        9,
        55632
      },
      {
        7,
        1,
        30,
        23208
      },
      {
        0,
        2,
        17,
        22176
      },
      {
        0,
        2,
        6,
        38608
      },
      {
        5,
        1,
        27,
        19176
      },
      {
        0,
        2,
        15,
        19152
      },
      {
        0,
        2,
        3,
        42192
      },
      {
        4,
        1,
        23,
        53864
      },
      {
        0,
        2,
        11,
        53840
      },
      {
        8,
        1,
        31,
        54568
      },
      {
        0,
        2,
        18,
        46400
      },
      {
        0,
        2,
        7,
        46752
      },
      {
        6,
        1,
        28,
        38608
      },
      {
        0,
        2,
        16,
        38320
      },
      {
        0,
        2,
        5,
        18864
      },
      {
        4,
        1,
        25,
        42168
      },
      {
        0,
        2,
        13,
        42160
      },
      {
        10,
        2,
        2,
        45656
      },
      {
        0,
        2,
        20,
        27216
      },
      {
        0,
        2,
        9,
        27968
      },
      {
        6,
        1,
        29,
        44448
      },
      {
        0,
        2,
        17,
        43872
      },
      {
        0,
        2,
        6,
        38256
      },
      {
        5,
        1,
        27,
        18808
      },
      {
        0,
        2,
        15,
        18800
      },
      {
        0,
        2,
        4,
        25776
      },
      {
        3,
        1,
        23,
        27216
      },
      {
        0,
        2,
        10,
        59984
      },
      {
        8,
        1,
        31,
        27432
      },
      {
        0,
        2,
        19,
        23232
      },
      {
        0,
        2,
        7,
        43872
      },
      {
        5,
        1,
        28,
        37736
      },
      {
        0,
        2,
        16,
        37600
      },
      {
        0,
        2,
        5,
        51552
      },
      {
        4,
        1,
        24,
        54440
      },
      {
        0,
        2,
        12,
        54432
      },
      {
        0,
        2,
        1,
        55888
      },
      {
        2,
        1,
        22,
        23208
      },
      {
        0,
        2,
        9,
        22176
      },
      {
        7,
        1,
        29,
        43736
      },
      {
        0,
        2,
        18,
        9680
      },
      {
        0,
        2,
        7,
        37584
      },
      {
        5,
        1,
        26,
        51544
      },
      {
        0,
        2,
        14,
        43344
      },
      {
        0,
        2,
        3,
        46240
      },
      {
        4,
        1,
        23,
        46416
      },
      {
        0,
        2,
        10,
        44368
      },
      {
        9,
        1,
        31,
        21928
      },
      {
        0,
        2,
        19,
        19360
      },
      {
        0,
        2,
        8,
        42416
      },
      {
        6,
        1,
        28,
        21176
      },
      {
        0,
        2,
        16,
        21168
      },
      {
        0,
        2,
        5,
        43312
      },
      {
        4,
        1,
        25,
        29864
      },
      {
        0,
        2,
        12,
        27296
      },
      {
        0,
        2,
        1,
        44368
      },
      {
        2,
        1,
        22,
        19880
      },
      {
        0,
        2,
        10,
        19296
      },
      {
        6,
        1,
        29,
        42352
      },
      {
        0,
        2,
        17,
        42208
      },
      {
        0,
        2,
        6,
        53856
      },
      {
        5,
        1,
        26,
        59696
      },
      {
        0,
        2,
        13,
        54576
      },
      {
        0,
        2,
        3,
        23200
      },
      {
        3,
        1,
        23,
        27472
      },
      {
        0,
        2,
        11,
        38608
      },
      {
        11,
        1,
        31,
        19176
      },
      {
        0,
        2,
        19,
        19152
      },
      {
        0,
        2,
        8,
        42192
      },
      {
        6,
        1,
        28,
        53848
      },
      {
        0,
        2,
        15,
        53840
      },
      {
        0,
        2,
        4,
        54560
      },
      {
        5,
        1,
        24,
        55968
      },
      {
        0,
        2,
        12,
        46496
      },
      {
        0,
        2,
        1,
        22224
      },
      {
        2,
        1,
        22,
        19160
      },
      {
        0,
        2,
        10,
        18864
      },
      {
        7,
        1,
        30,
        42168
      },
      {
        0,
        2,
        17,
        42160
      },
      {
        0,
        2,
        6,
        43600
      },
      {
        5,
        1,
        26,
        46376
      },
      {
        0,
        2,
        14,
        27936
      },
      {
        0,
        2,
        2,
        44448
      },
      {
        3,
        1,
        23,
        21936
      }
    };
    internal GregorianCalendarHelper helper;
    internal const int MIN_LUNISOLAR_YEAR = 1912;
    internal const int MAX_LUNISOLAR_YEAR = 2050;
    internal const int MIN_GREGORIAN_YEAR = 1912;
    internal const int MIN_GREGORIAN_MONTH = 2;
    internal const int MIN_GREGORIAN_DAY = 18;
    internal const int MAX_GREGORIAN_YEAR = 2051;
    internal const int MAX_GREGORIAN_MONTH = 2;
    internal const int MAX_GREGORIAN_DAY = 10;

    /// <summary>
    ///   Возвращает минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> класса.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> класс, который эквивалентен первый момент 18 февраля 1912 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return TaiwanLunisolarCalendar.minDate;
      }
    }

    /// <summary>
    ///   Возвращает максимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> класса.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> класса, который эквивалентен самый последний момент 10 февраля 2051 года нашей эры в григорианском календаре.
    /// </returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return TaiwanLunisolarCalendar.maxDate;
      }
    }

    /// <summary>
    ///   Возвращает число дней в году, который предшествует года, указанного в <see cref="P:System.Globalization.TaiwanLunisolarCalendar.MinSupportedDateTime" /> свойство.
    /// </summary>
    /// <returns>
    ///   Число дней в году, который предшествует года в заданном по <see cref="P:System.Globalization.TaiwanLunisolarCalendar.MinSupportedDateTime" />.
    /// </returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 384;
      }
    }

    internal override int MinCalendarYear
    {
      get
      {
        return 1912;
      }
    }

    internal override int MaxCalendarYear
    {
      get
      {
        return 2050;
      }
    }

    internal override DateTime MinDate
    {
      get
      {
        return TaiwanLunisolarCalendar.minDate;
      }
    }

    internal override DateTime MaxDate
    {
      get
      {
        return TaiwanLunisolarCalendar.maxDate;
      }
    }

    internal override EraInfo[] CalEraInfo
    {
      get
      {
        return TaiwanLunisolarCalendar.taiwanLunisolarEraInfo;
      }
    }

    internal override int GetYearInfo(int LunarYear, int Index)
    {
      if (LunarYear < 1912 || LunarYear > 2050)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1912, (object) 2050));
      return TaiwanLunisolarCalendar.yinfo[LunarYear - 1912, Index];
    }

    internal override int GetYear(int year, DateTime time)
    {
      return this.helper.GetYear(year, time);
    }

    internal override int GetGregorianYear(int year, int era)
    {
      return this.helper.GetGregorianYear(year, era);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.TaiwanLunisolarCalendar" />.
    /// </summary>
    public TaiwanLunisolarCalendar()
    {
      this.helper = new GregorianCalendarHelper((Calendar) this, TaiwanLunisolarCalendar.taiwanLunisolarEraInfo);
    }

    /// <summary>
    ///   Извлекает эры, соответствующий указанному <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в заданном <paramref name="time" /> параметр.
    /// </returns>
    public override int GetEra(DateTime time)
    {
      return this.helper.GetEra(time);
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 4;
      }
    }

    internal override int ID
    {
      get
      {
        return 21;
      }
    }

    /// <summary>
    ///   Возвращает, относящиеся к текущей эры <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Массив, состоящий из одного элемента, значение, которое всегда равно текущей эре.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return this.helper.Eras;
      }
    }
  }
}
