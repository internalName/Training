// Decompiled with JetBrains decompiler
// Type: System.Globalization.JapaneseCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Globalization
{
  /// <summary>Представляет японский календарь.</summary>
  [ComVisible(true)]
  [Serializable]
  public class JapaneseCalendar : Calendar
  {
    internal static readonly DateTime calendarMinValue = new DateTime(1868, 9, 8);
    internal static volatile EraInfo[] japaneseEraInfo;
    private const string c_japaneseErasHive = "System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";
    private const string c_japaneseErasHivePermissionList = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";
    internal static volatile Calendar s_defaultInstance;
    internal GregorianCalendarHelper helper;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;

    /// <summary>
    ///   Возвращает минимальное значение даты и времени, поддерживаемый текущим объектом <see cref="T:System.Globalization.JapaneseCalendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Минимальное значение даты и время, поддерживаемые <see cref="T:System.Globalization.JapaneseCalendar" /> тип, который эквивалентен первый момент 8 сентября 1868 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return JapaneseCalendar.calendarMinValue;
      }
    }

    /// <summary>
    ///   Возвращает последнюю дату и время, поддерживаемые текущим <see cref="T:System.Globalization.JapaneseCalendar" /> объекта.
    /// </summary>
    /// <returns>
    ///   Самые последние дату и время, поддерживаемые <see cref="T:System.Globalization.JapaneseCalendar" /> тип, который эквивалентен последний момент 31 декабря 9999 года нашей эры в григорианском календаре.
    /// </returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return DateTime.MaxValue;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий календарь солнечным, лунным или их сочетание.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.
    /// </returns>
    [ComVisible(false)]
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.SolarCalendar;
      }
    }

    internal static EraInfo[] GetEraInfo()
    {
      if (JapaneseCalendar.japaneseEraInfo == null)
      {
        JapaneseCalendar.japaneseEraInfo = JapaneseCalendar.GetErasFromRegistry();
        if (JapaneseCalendar.japaneseEraInfo == null)
          JapaneseCalendar.japaneseEraInfo = new EraInfo[4]
          {
            new EraInfo(4, 1989, 1, 8, 1988, 1, 8011, "平成", "平", "H"),
            new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
            new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
            new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
          };
      }
      return JapaneseCalendar.japaneseEraInfo;
    }

    [SecuritySafeCritical]
    private static EraInfo[] GetErasFromRegistry()
    {
      int newSize = 0;
      EraInfo[] array = (EraInfo[]) null;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        permissionSet.AddPermission((IPermission) new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras"));
        permissionSet.Assert();
        RegistryKey registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE).OpenSubKey("System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras", false);
        if (registryKey == null)
          return (EraInfo[]) null;
        string[] valueNames = registryKey.GetValueNames();
        if (valueNames != null)
        {
          if (valueNames.Length != 0)
          {
            array = new EraInfo[valueNames.Length];
            for (int index = 0; index < valueNames.Length; ++index)
            {
              EraInfo eraFromValue = JapaneseCalendar.GetEraFromValue(valueNames[index], registryKey.GetValue(valueNames[index]).ToString());
              if (eraFromValue != null)
              {
                array[newSize] = eraFromValue;
                ++newSize;
              }
            }
          }
        }
      }
      catch (SecurityException ex)
      {
        return (EraInfo[]) null;
      }
      catch (IOException ex)
      {
        return (EraInfo[]) null;
      }
      catch (UnauthorizedAccessException ex)
      {
        return (EraInfo[]) null;
      }
      if (newSize < 4)
        return (EraInfo[]) null;
      Array.Resize<EraInfo>(ref array, newSize);
      Array.Sort<EraInfo>(array, new Comparison<EraInfo>(JapaneseCalendar.CompareEraRanges));
      for (int index = 0; index < array.Length; ++index)
      {
        array[index].era = array.Length - index;
        if (index == 0)
          array[0].maxEraYear = 9999 - array[0].yearOffset;
        else
          array[index].maxEraYear = array[index - 1].yearOffset + 1 - array[index].yearOffset;
      }
      return array;
    }

    private static int CompareEraRanges(EraInfo a, EraInfo b)
    {
      return b.ticks.CompareTo(a.ticks);
    }

    private static EraInfo GetEraFromValue(string value, string data)
    {
      if (value == null || data == null)
        return (EraInfo) null;
      if (value.Length != 10)
        return (EraInfo) null;
      int result1;
      int result2;
      int result3;
      if (!Number.TryParseInt32(value.Substring(0, 4), NumberStyles.None, NumberFormatInfo.InvariantInfo, out result1) || !Number.TryParseInt32(value.Substring(5, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out result2) || !Number.TryParseInt32(value.Substring(8, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out result3))
        return (EraInfo) null;
      string[] strArray = data.Split('_');
      if (strArray.Length != 4)
        return (EraInfo) null;
      if (strArray[0].Length == 0 || strArray[1].Length == 0 || (strArray[2].Length == 0 || strArray[3].Length == 0))
        return (EraInfo) null;
      return new EraInfo(0, result1, result2, result3, result1 - 1, 1, 0, strArray[0], strArray[1], strArray[3]);
    }

    internal static Calendar GetDefaultInstance()
    {
      if (JapaneseCalendar.s_defaultInstance == null)
        JapaneseCalendar.s_defaultInstance = (Calendar) new JapaneseCalendar();
      return JapaneseCalendar.s_defaultInstance;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.JapaneseCalendar" />.
    /// </summary>
    /// <exception cref="T:System.TypeInitializationException">
    ///   Не удалось инициализировать <see cref="T:System.Globalization.JapaneseCalendar" /> объект из-за отсутствия язык и региональные параметры.
    /// </exception>
    public JapaneseCalendar()
    {
      try
      {
        CultureInfo cultureInfo = new CultureInfo("ja-JP");
      }
      catch (ArgumentException ex)
      {
        throw new TypeInitializationException(this.GetType().FullName, (Exception) ex);
      }
      this.helper = new GregorianCalendarHelper((Calendar) this, JapaneseCalendar.GetEraInfo());
    }

    internal override int ID
    {
      get
      {
        return 3;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число месяцев из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются месяцы.
    /// </param>
    /// <param name="months">Число месяцев для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный в результате добавления указанное число месяцев в указанном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="months" /> — меньше -120000.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="months" /> больше 120000.
    /// </exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      return this.helper.AddMonths(time, months);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> это заданное число лет из заданного <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   Объект <see cref="T:System.DateTime" />, к которому добавляются годы.
    /// </param>
    /// <param name="years">Число лет для добавления.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Полученный добавлением указанное число лет к заданному <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> находится за пределами поддерживаемого диапазона <see cref="T:System.Globalization.JapaneseCalendar" /> типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="years" /> меньше параметр больше или равно 10 000.
    /// </exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.helper.AddYears(time, years);
    }

    /// <summary>
    ///   Возвращает число дней в указанном месяце указанных года и эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном месяце указанных года и эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      return this.helper.GetDaysInMonth(year, month, era);
    }

    /// <summary>
    ///   Возвращает число дней в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Число дней в указанном году указанной эры.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetDaysInYear(int year, int era)
    {
      return this.helper.GetDaysInYear(year, era);
    }

    /// <summary>
    ///   Возвращает день месяца в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 31, представляющее день месяца в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.helper.GetDayOfMonth(time);
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
      return this.helper.GetDayOfWeek(time);
    }

    /// <summary>
    ///   Возвращает день года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 366, обозначающее день года в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetDayOfYear(DateTime time)
    {
      return this.helper.GetDayOfYear(time);
    }

    /// <summary>
    ///   Возвращает число месяцев в указанном году указанной эры.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>Возвращаемое значение всегда равно 12.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int GetMonthsInYear(int year, int era)
    {
      return this.helper.GetMonthsInYear(year, era);
    }

    /// <summary>
    ///   Возвращает неделю года, к которой относится дата в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <param name="rule">
    ///   Один из <see cref="T:System.Globalization.CalendarWeekRule" /> значений, определяющее календарную неделю.
    /// </param>
    /// <param name="firstDayOfWeek">
    ///   Один из <see cref="T:System.DayOfWeek" /> значений, представляющее первый день недели.
    /// </param>
    /// <returns>
    ///   На основе 1 целое число, представляющее неделю года, к которой относится дата в <paramref name="time" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="time" /> или <paramref name="firstDayOfWeek" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="rule" /> не является допустимым значением <see cref="T:System.Globalization.CalendarWeekRule" />.
    /// </exception>
    [ComVisible(false)]
    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
    }

    /// <summary>
    ///   Возвращает значение эры в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее эру в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Итоговый <see cref="T:System.DateTime" /> находится за пределами допустимого диапазона.
    /// </exception>
    public override int GetEra(DateTime time)
    {
      return this.helper.GetEra(time);
    }

    /// <summary>
    ///   Возвращает месяц в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число от 1 до 12, представляющее месяц в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetMonth(DateTime time)
    {
      return this.helper.GetMonth(time);
    }

    /// <summary>
    ///   Возвращает значение года в заданном <see cref="T:System.DateTime" />.
    /// </summary>
    /// <param name="time">
    ///   <see cref="T:System.DateTime" />, который требуется прочитать.
    /// </param>
    /// <returns>
    ///   Целое число, представляющее год в заданном <see cref="T:System.DateTime" />.
    /// </returns>
    public override int GetYear(DateTime time)
    {
      return this.helper.GetYear(time);
    }

    /// <summary>
    ///   Определяет, является ли указанная дата указанной эры високосным днем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see langword="true" />, если указанный день — високосный; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      return this.helper.IsLeapDay(year, month, day, era);
    }

    /// <summary>
    ///   Определяет, является ли указанный год указанной эры високосным годом.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see langword="true" />, если указанный год — високосный; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapYear(int year, int era)
    {
      return this.helper.IsLeapYear(year, era);
    }

    /// <summary>Вычисляет високосный месяц для заданных года и эры.</summary>
    /// <param name="year">Год.</param>
    /// <param name="era">Эра.</param>
    /// <returns>
    ///   Возвращаемое значение всегда равно 0 из-за <see cref="T:System.Globalization.JapaneseCalendar" /> тип не поддерживает понятие високосного года.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> или <paramref name="era" /> находится вне диапазона, поддерживаемого <see cref="T:System.Globalization.JapaneseCalendar" /> типа.
    /// </exception>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      return this.helper.GetLeapMonth(year, era);
    }

    /// <summary>
    ///   Определяет, является ли указанный месяц указанных года и эры високосным месяцем.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   Этот метод всегда возвращает <see langword="false" />, если не переопределено в производном классе.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      return this.helper.IsLeapMonth(year, month, era);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.DateTime" /> имеет значение указанной даты и времени в заданной эре.
    /// </summary>
    /// <param name="year">Целое число, представляющее год.</param>
    /// <param name="month">
    ///   Целое число от 1 до 12, представляющее месяц.
    /// </param>
    /// <param name="day">
    ///   Целое число от 1 до 31, представляющее день.
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
    /// <param name="era">Целое число, представляющее эру.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Имеет значение указанной даты и времени в текущей эре.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="month" /> находится вне диапазона, поддерживаемого календарем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="day" /> находится вне диапазона, поддерживаемого календарем.
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
    /// 
    ///   -или-
    /// 
    ///   <paramref name="era" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
    }

    /// <summary>
    ///   Преобразует указанный год в четырехзначный год с помощью <see cref="P:System.Globalization.JapaneseCalendar.TwoDigitYearMax" /> Свойства для определения века.
    /// </summary>
    /// <param name="year">
    ///   Целое число (обычно две цифры), представляющее год для преобразования.
    /// </param>
    /// <returns>
    ///   Целое число, содержащее четырехразрядное представление <paramref name="year" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="year" /> находится вне диапазона, поддерживаемого календарем.
    /// </exception>
    public override int ToFourDigitYear(int year)
    {
      if (year <= 0)
        throw new ArgumentOutOfRangeException(nameof (year), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (year > this.helper.MaxYear)
        throw new ArgumentOutOfRangeException(nameof (year), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) this.helper.MaxYear));
      return year;
    }

    /// <summary>
    ///   Возвращает список эр в <see cref="T:System.Globalization.JapaneseCalendar" />.
    /// </summary>
    /// <returns>
    ///   Массив целых чисел, представляющий эры в <see cref="T:System.Globalization.JapaneseCalendar" />.
    /// </returns>
    public override int[] Eras
    {
      get
      {
        return this.helper.Eras;
      }
    }

    internal static string[] EraNames()
    {
      EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
      string[] strArray = new string[eraInfo.Length];
      for (int index = 0; index < eraInfo.Length; ++index)
        strArray[index] = eraInfo[eraInfo.Length - index - 1].eraName;
      return strArray;
    }

    internal static string[] AbbrevEraNames()
    {
      EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
      string[] strArray = new string[eraInfo.Length];
      for (int index = 0; index < eraInfo.Length; ++index)
        strArray[index] = eraInfo[eraInfo.Length - index - 1].abbrevEraName;
      return strArray;
    }

    internal static string[] EnglishEraNames()
    {
      EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
      string[] strArray = new string[eraInfo.Length];
      for (int index = 0; index < eraInfo.Length; ++index)
        strArray[index] = eraInfo[eraInfo.Length - index - 1].englishEraName;
      return strArray;
    }

    internal override bool IsValidYear(int year, int era)
    {
      return this.helper.IsValidYear(year, era);
    }

    /// <summary>
    ///   Возвращает или задает последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </summary>
    /// <returns>
    ///   Последний год в диапазоне ста лет, для которого существует двузначное представление года.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, указанное в наборе операций — до 99.
    /// 
    ///   -или-
    /// 
    ///   Значение, указанное в наборе операций больше, чем 8011 (или <see langword="MaxSupportedDateTime.Year" />).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В наборе операций текущим экземпляром доступно только для чтения.
    /// </exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.helper.MaxYear)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) this.helper.MaxYear));
        this.twoDigitYearMax = value;
      }
    }
  }
}
