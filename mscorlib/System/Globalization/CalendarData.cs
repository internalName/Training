// Decompiled with JetBrains decompiler
// Type: System.Globalization.CalendarData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
  internal class CalendarData
  {
    internal int iTwoDigitYearMax = 2029;
    internal const int MAX_CALENDARS = 23;
    internal string sNativeName;
    internal string[] saShortDates;
    internal string[] saYearMonths;
    internal string[] saLongDates;
    internal string sMonthDay;
    internal string[] saEraNames;
    internal string[] saAbbrevEraNames;
    internal string[] saAbbrevEnglishEraNames;
    internal string[] saDayNames;
    internal string[] saAbbrevDayNames;
    internal string[] saSuperShortDayNames;
    internal string[] saMonthNames;
    internal string[] saAbbrevMonthNames;
    internal string[] saMonthGenitiveNames;
    internal string[] saAbbrevMonthGenitiveNames;
    internal string[] saLeapYearMonthNames;
    internal int iCurrentEra;
    internal bool bUseUserOverrides;
    internal static CalendarData Invariant;

    private CalendarData()
    {
    }

    static CalendarData()
    {
      CalendarData calendarData = new CalendarData();
      calendarData.sNativeName = "Gregorian Calendar";
      calendarData.iTwoDigitYearMax = 2029;
      calendarData.iCurrentEra = 1;
      calendarData.saShortDates = new string[2]
      {
        "MM/dd/yyyy",
        "yyyy-MM-dd"
      };
      calendarData.saLongDates = new string[1]
      {
        "dddd, dd MMMM yyyy"
      };
      calendarData.saYearMonths = new string[1]
      {
        "yyyy MMMM"
      };
      calendarData.sMonthDay = "MMMM dd";
      calendarData.saEraNames = new string[1]{ "A.D." };
      calendarData.saAbbrevEraNames = new string[1]{ "AD" };
      calendarData.saAbbrevEnglishEraNames = new string[1]
      {
        "AD"
      };
      calendarData.saDayNames = new string[7]
      {
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday"
      };
      calendarData.saAbbrevDayNames = new string[7]
      {
        "Sun",
        "Mon",
        "Tue",
        "Wed",
        "Thu",
        "Fri",
        "Sat"
      };
      calendarData.saSuperShortDayNames = new string[7]
      {
        "Su",
        "Mo",
        "Tu",
        "We",
        "Th",
        "Fr",
        "Sa"
      };
      calendarData.saMonthNames = new string[13]
      {
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December",
        string.Empty
      };
      calendarData.saAbbrevMonthNames = new string[13]
      {
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
        string.Empty
      };
      calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
      calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
      calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
      calendarData.bUseUserOverrides = false;
      CalendarData.Invariant = calendarData;
    }

    internal CalendarData(string localeName, int calendarId, bool bUseUserOverrides)
    {
      this.bUseUserOverrides = bUseUserOverrides;
      if (!CalendarData.nativeGetCalendarData(this, localeName, calendarId))
      {
        if (this.sNativeName == null)
          this.sNativeName = string.Empty;
        if (this.saShortDates == null)
          this.saShortDates = CalendarData.Invariant.saShortDates;
        if (this.saYearMonths == null)
          this.saYearMonths = CalendarData.Invariant.saYearMonths;
        if (this.saLongDates == null)
          this.saLongDates = CalendarData.Invariant.saLongDates;
        if (this.sMonthDay == null)
          this.sMonthDay = CalendarData.Invariant.sMonthDay;
        if (this.saEraNames == null)
          this.saEraNames = CalendarData.Invariant.saEraNames;
        if (this.saAbbrevEraNames == null)
          this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
        if (this.saAbbrevEnglishEraNames == null)
          this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
        if (this.saDayNames == null)
          this.saDayNames = CalendarData.Invariant.saDayNames;
        if (this.saAbbrevDayNames == null)
          this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
        if (this.saSuperShortDayNames == null)
          this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
        if (this.saMonthNames == null)
          this.saMonthNames = CalendarData.Invariant.saMonthNames;
        if (this.saAbbrevMonthNames == null)
          this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
      }
      this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
      this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
      this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
      this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
      if ((ushort) calendarId == (ushort) 4)
        this.sNativeName = !CultureInfo.IsTaiwanSku ? string.Empty : "中華民國曆";
      if (this.saMonthGenitiveNames == null || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
        this.saMonthGenitiveNames = this.saMonthNames;
      if (this.saAbbrevMonthGenitiveNames == null || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
        this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
      if (this.saLeapYearMonthNames == null || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
        this.saLeapYearMonthNames = this.saMonthNames;
      this.InitializeEraNames(localeName, calendarId);
      this.InitializeAbbreviatedEraNames(localeName, calendarId);
      if (calendarId == 3)
        this.saAbbrevEnglishEraNames = JapaneseCalendar.EnglishEraNames();
      else
        this.saAbbrevEnglishEraNames = new string[1]{ "" };
      this.iCurrentEra = this.saEraNames.Length;
    }

    private void InitializeEraNames(string localeName, int calendarId)
    {
      switch ((ushort) calendarId)
      {
        case 1:
          if (this.saEraNames != null && this.saEraNames.Length != 0 && !string.IsNullOrEmpty(this.saEraNames[0]))
            break;
          this.saEraNames = new string[1]{ "A.D." };
          break;
        case 2:
        case 13:
          this.saEraNames = new string[1]{ "A.D." };
          break;
        case 3:
        case 14:
          this.saEraNames = JapaneseCalendar.EraNames();
          break;
        case 4:
          if (CultureInfo.IsTaiwanSku)
          {
            this.saEraNames = new string[1]{ "中華民國" };
            break;
          }
          this.saEraNames = new string[1]{ string.Empty };
          break;
        case 5:
          this.saEraNames = new string[1]{ "단기" };
          break;
        case 6:
        case 23:
          if (localeName == "dv-MV")
          {
            this.saEraNames = new string[1]{ "ހިޖްރީ" };
            break;
          }
          this.saEraNames = new string[1]{ "بعد الهجرة" };
          break;
        case 7:
          this.saEraNames = new string[1]{ "พ.ศ." };
          break;
        case 8:
          this.saEraNames = new string[1]{ "C.E." };
          break;
        case 9:
          this.saEraNames = new string[1]{ "ap. J.-C." };
          break;
        case 10:
        case 11:
        case 12:
          this.saEraNames = new string[1]{ "م" };
          break;
        case 22:
          if (this.saEraNames != null && this.saEraNames.Length != 0 && !string.IsNullOrEmpty(this.saEraNames[0]))
            break;
          this.saEraNames = new string[1]{ "ه.ش" };
          break;
        default:
          this.saEraNames = CalendarData.Invariant.saEraNames;
          break;
      }
    }

    private void InitializeAbbreviatedEraNames(string localeName, int calendarId)
    {
      CalendarId calendarId1 = (CalendarId) calendarId;
      if ((uint) calendarId1 <= 13U)
      {
        switch (calendarId1)
        {
          case CalendarId.GREGORIAN:
            if (this.saAbbrevEraNames != null && this.saAbbrevEraNames.Length != 0 && !string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
              return;
            this.saAbbrevEraNames = new string[1]{ "AD" };
            return;
          case CalendarId.GREGORIAN_US:
          case CalendarId.JULIAN:
            this.saAbbrevEraNames = new string[1]{ "AD" };
            return;
          case CalendarId.JAPAN:
            break;
          case CalendarId.TAIWAN:
            this.saAbbrevEraNames = new string[1];
            if (this.saEraNames[0].Length == 4)
            {
              this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
              return;
            }
            this.saAbbrevEraNames[0] = this.saEraNames[0];
            return;
          case CalendarId.HIJRI:
            goto label_9;
          default:
            goto label_17;
        }
      }
      else
      {
        switch (calendarId1)
        {
          case CalendarId.JAPANESELUNISOLAR:
            break;
          case CalendarId.PERSIAN:
            if (this.saAbbrevEraNames != null && this.saAbbrevEraNames.Length != 0 && !string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
              return;
            this.saAbbrevEraNames = this.saEraNames;
            return;
          case CalendarId.UMALQURA:
            goto label_9;
          default:
            goto label_17;
        }
      }
      this.saAbbrevEraNames = JapaneseCalendar.AbbrevEraNames();
      return;
label_9:
      if (localeName == "dv-MV")
      {
        this.saAbbrevEraNames = new string[1]{ "ހ." };
        return;
      }
      this.saAbbrevEraNames = new string[1]{ "هـ" };
      return;
label_17:
      this.saAbbrevEraNames = this.saEraNames;
    }

    internal static CalendarData GetCalendarData(int calendarId)
    {
      return CultureInfo.GetCultureInfo(CalendarData.CalendarIdToCultureName(calendarId)).m_cultureData.GetCalendar(calendarId);
    }

    private static string CalendarIdToCultureName(int calendarId)
    {
      switch (calendarId)
      {
        case 2:
          return "fa-IR";
        case 3:
          return "ja-JP";
        case 4:
          return "zh-TW";
        case 5:
          return "ko-KR";
        case 6:
        case 10:
        case 23:
          return "ar-SA";
        case 7:
          return "th-TH";
        case 8:
          return "he-IL";
        case 9:
          return "ar-DZ";
        case 11:
        case 12:
          return "ar-IQ";
        default:
          return "en-US";
      }
    }

    internal void FixupWin7MonthDaySemicolonBug()
    {
      int unescapedCharacter = CalendarData.FindUnescapedCharacter(this.sMonthDay, ';');
      if (unescapedCharacter <= 0)
        return;
      this.sMonthDay = this.sMonthDay.Substring(0, unescapedCharacter);
    }

    private static int FindUnescapedCharacter(string s, char charToFind)
    {
      bool flag = false;
      int length = s.Length;
      for (int index = 0; index < length; ++index)
      {
        char ch = s[index];
        switch (ch)
        {
          case '\'':
            flag = !flag;
            break;
          case '\\':
            ++index;
            break;
          default:
            if (!flag && (int) charToFind == (int) ch)
              return index;
            break;
        }
      }
      return -1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeGetTwoDigitYearMax(int calID);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeGetCalendarData(CalendarData data, string localeName, int calendar);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeGetCalendars(string localeName, bool useUserOverride, [In, Out] int[] calendars);
  }
}
