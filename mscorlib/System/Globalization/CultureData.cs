// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Globalization
{
  [FriendAccessAllowed]
  internal class CultureData
  {
    private static readonly Version s_win7Version = new Version(6, 1);
    private static string s_RegionKey = "System\\CurrentControlSet\\Control\\Nls\\RegionMapping";
    private int iGeoId = -1;
    private int iNegativePercent = -1;
    private int iPositivePercent = -1;
    private int iMeasure = -1;
    private int iFirstDayOfWeek = -1;
    private int iFirstWeekOfYear = -1;
    private int iReadingLayout = -1;
    private int iDefaultAnsiCodePage = -1;
    private int iDefaultOemCodePage = -1;
    private int iDefaultMacCodePage = -1;
    private int iDefaultEbcdicCodePage = -1;
    private int iInputLanguageHandle = -1;
    private const int undef = -1;
    private string sRealName;
    private string sWindowsName;
    private string sName;
    private string sParent;
    private string sLocalizedDisplayName;
    private string sEnglishDisplayName;
    private string sNativeDisplayName;
    private string sSpecificCulture;
    private string sISO639Language;
    private string sLocalizedLanguage;
    private string sEnglishLanguage;
    private string sNativeLanguage;
    private string sRegionName;
    private string sLocalizedCountry;
    private string sEnglishCountry;
    private string sNativeCountry;
    private string sISO3166CountryName;
    private string sPositiveSign;
    private string sNegativeSign;
    private string[] saNativeDigits;
    private int iDigitSubstitution;
    private int iLeadingZeros;
    private int iDigits;
    private int iNegativeNumber;
    private int[] waGrouping;
    private string sDecimalSeparator;
    private string sThousandSeparator;
    private string sNaN;
    private string sPositiveInfinity;
    private string sNegativeInfinity;
    private string sPercent;
    private string sPerMille;
    private string sCurrency;
    private string sIntlMonetarySymbol;
    private string sEnglishCurrency;
    private string sNativeCurrency;
    private int iCurrencyDigits;
    private int iCurrency;
    private int iNegativeCurrency;
    private int[] waMonetaryGrouping;
    private string sMonetaryDecimal;
    private string sMonetaryThousand;
    private string sListSeparator;
    private string sAM1159;
    private string sPM2359;
    private string sTimeSeparator;
    private volatile string[] saLongTimes;
    private volatile string[] saShortTimes;
    private volatile string[] saDurationFormats;
    private volatile int[] waCalendars;
    private CalendarData[] calendars;
    private string sTextInfo;
    private string sCompareInfo;
    private string sScripts;
    private int iLanguage;
    private string sAbbrevLang;
    private string sAbbrevCountry;
    private string sISO639Language2;
    private string sISO3166CountryName2;
    private string sConsoleFallbackName;
    private string sKeyboardsToInstall;
    private string fontSignature;
    private bool bUseOverrides;
    private bool bNeutral;
    private bool bWin32Installed;
    private bool bFramework;
    private static volatile Dictionary<string, string> s_RegionNames;
    private static volatile CultureData s_Invariant;
    internal static volatile ResourceSet MscorlibResourceSet;
    private static volatile Dictionary<string, CultureData> s_cachedCultures;
    private static volatile Dictionary<string, CultureData> s_cachedRegions;
    internal static volatile CultureInfo[] specificCultures;
    internal static volatile string[] s_replacementCultureNames;
    private const uint LOCALE_NOUSEROVERRIDE = 2147483648;
    private const uint LOCALE_RETURN_NUMBER = 536870912;
    private const uint LOCALE_RETURN_GENITIVE_NAMES = 268435456;
    private const uint LOCALE_SLOCALIZEDDISPLAYNAME = 2;
    private const uint LOCALE_SENGLISHDISPLAYNAME = 114;
    private const uint LOCALE_SNATIVEDISPLAYNAME = 115;
    private const uint LOCALE_SLOCALIZEDLANGUAGENAME = 111;
    private const uint LOCALE_SENGLISHLANGUAGENAME = 4097;
    private const uint LOCALE_SNATIVELANGUAGENAME = 4;
    private const uint LOCALE_SLOCALIZEDCOUNTRYNAME = 6;
    private const uint LOCALE_SENGLISHCOUNTRYNAME = 4098;
    private const uint LOCALE_SNATIVECOUNTRYNAME = 8;
    private const uint LOCALE_SABBREVLANGNAME = 3;
    private const uint LOCALE_ICOUNTRY = 5;
    private const uint LOCALE_SABBREVCTRYNAME = 7;
    private const uint LOCALE_IGEOID = 91;
    private const uint LOCALE_IDEFAULTLANGUAGE = 9;
    private const uint LOCALE_IDEFAULTCOUNTRY = 10;
    private const uint LOCALE_IDEFAULTCODEPAGE = 11;
    private const uint LOCALE_IDEFAULTANSICODEPAGE = 4100;
    private const uint LOCALE_IDEFAULTMACCODEPAGE = 4113;
    private const uint LOCALE_SLIST = 12;
    private const uint LOCALE_IMEASURE = 13;
    private const uint LOCALE_SDECIMAL = 14;
    private const uint LOCALE_STHOUSAND = 15;
    private const uint LOCALE_SGROUPING = 16;
    private const uint LOCALE_IDIGITS = 17;
    private const uint LOCALE_ILZERO = 18;
    private const uint LOCALE_INEGNUMBER = 4112;
    private const uint LOCALE_SNATIVEDIGITS = 19;
    private const uint LOCALE_SCURRENCY = 20;
    private const uint LOCALE_SINTLSYMBOL = 21;
    private const uint LOCALE_SMONDECIMALSEP = 22;
    private const uint LOCALE_SMONTHOUSANDSEP = 23;
    private const uint LOCALE_SMONGROUPING = 24;
    private const uint LOCALE_ICURRDIGITS = 25;
    private const uint LOCALE_IINTLCURRDIGITS = 26;
    private const uint LOCALE_ICURRENCY = 27;
    private const uint LOCALE_INEGCURR = 28;
    private const uint LOCALE_SDATE = 29;
    private const uint LOCALE_STIME = 30;
    private const uint LOCALE_SSHORTDATE = 31;
    private const uint LOCALE_SLONGDATE = 32;
    private const uint LOCALE_STIMEFORMAT = 4099;
    private const uint LOCALE_IDATE = 33;
    private const uint LOCALE_ILDATE = 34;
    private const uint LOCALE_ITIME = 35;
    private const uint LOCALE_ITIMEMARKPOSN = 4101;
    private const uint LOCALE_ICENTURY = 36;
    private const uint LOCALE_ITLZERO = 37;
    private const uint LOCALE_IDAYLZERO = 38;
    private const uint LOCALE_IMONLZERO = 39;
    private const uint LOCALE_S1159 = 40;
    private const uint LOCALE_S2359 = 41;
    private const uint LOCALE_ICALENDARTYPE = 4105;
    private const uint LOCALE_IOPTIONALCALENDAR = 4107;
    private const uint LOCALE_IFIRSTDAYOFWEEK = 4108;
    private const uint LOCALE_IFIRSTWEEKOFYEAR = 4109;
    private const uint LOCALE_SDAYNAME1 = 42;
    private const uint LOCALE_SDAYNAME2 = 43;
    private const uint LOCALE_SDAYNAME3 = 44;
    private const uint LOCALE_SDAYNAME4 = 45;
    private const uint LOCALE_SDAYNAME5 = 46;
    private const uint LOCALE_SDAYNAME6 = 47;
    private const uint LOCALE_SDAYNAME7 = 48;
    private const uint LOCALE_SABBREVDAYNAME1 = 49;
    private const uint LOCALE_SABBREVDAYNAME2 = 50;
    private const uint LOCALE_SABBREVDAYNAME3 = 51;
    private const uint LOCALE_SABBREVDAYNAME4 = 52;
    private const uint LOCALE_SABBREVDAYNAME5 = 53;
    private const uint LOCALE_SABBREVDAYNAME6 = 54;
    private const uint LOCALE_SABBREVDAYNAME7 = 55;
    private const uint LOCALE_SMONTHNAME1 = 56;
    private const uint LOCALE_SMONTHNAME2 = 57;
    private const uint LOCALE_SMONTHNAME3 = 58;
    private const uint LOCALE_SMONTHNAME4 = 59;
    private const uint LOCALE_SMONTHNAME5 = 60;
    private const uint LOCALE_SMONTHNAME6 = 61;
    private const uint LOCALE_SMONTHNAME7 = 62;
    private const uint LOCALE_SMONTHNAME8 = 63;
    private const uint LOCALE_SMONTHNAME9 = 64;
    private const uint LOCALE_SMONTHNAME10 = 65;
    private const uint LOCALE_SMONTHNAME11 = 66;
    private const uint LOCALE_SMONTHNAME12 = 67;
    private const uint LOCALE_SMONTHNAME13 = 4110;
    private const uint LOCALE_SABBREVMONTHNAME1 = 68;
    private const uint LOCALE_SABBREVMONTHNAME2 = 69;
    private const uint LOCALE_SABBREVMONTHNAME3 = 70;
    private const uint LOCALE_SABBREVMONTHNAME4 = 71;
    private const uint LOCALE_SABBREVMONTHNAME5 = 72;
    private const uint LOCALE_SABBREVMONTHNAME6 = 73;
    private const uint LOCALE_SABBREVMONTHNAME7 = 74;
    private const uint LOCALE_SABBREVMONTHNAME8 = 75;
    private const uint LOCALE_SABBREVMONTHNAME9 = 76;
    private const uint LOCALE_SABBREVMONTHNAME10 = 77;
    private const uint LOCALE_SABBREVMONTHNAME11 = 78;
    private const uint LOCALE_SABBREVMONTHNAME12 = 79;
    private const uint LOCALE_SABBREVMONTHNAME13 = 4111;
    private const uint LOCALE_SPOSITIVESIGN = 80;
    private const uint LOCALE_SNEGATIVESIGN = 81;
    private const uint LOCALE_IPOSSIGNPOSN = 82;
    private const uint LOCALE_INEGSIGNPOSN = 83;
    private const uint LOCALE_IPOSSYMPRECEDES = 84;
    private const uint LOCALE_IPOSSEPBYSPACE = 85;
    private const uint LOCALE_INEGSYMPRECEDES = 86;
    private const uint LOCALE_INEGSEPBYSPACE = 87;
    private const uint LOCALE_FONTSIGNATURE = 88;
    private const uint LOCALE_SISO639LANGNAME = 89;
    private const uint LOCALE_SISO3166CTRYNAME = 90;
    private const uint LOCALE_IDEFAULTEBCDICCODEPAGE = 4114;
    private const uint LOCALE_IPAPERSIZE = 4106;
    private const uint LOCALE_SENGCURRNAME = 4103;
    private const uint LOCALE_SNATIVECURRNAME = 4104;
    private const uint LOCALE_SYEARMONTH = 4102;
    private const uint LOCALE_SSORTNAME = 4115;
    private const uint LOCALE_IDIGITSUBSTITUTION = 4116;
    private const uint LOCALE_SNAME = 92;
    private const uint LOCALE_SDURATION = 93;
    private const uint LOCALE_SKEYBOARDSTOINSTALL = 94;
    private const uint LOCALE_SSHORTESTDAYNAME1 = 96;
    private const uint LOCALE_SSHORTESTDAYNAME2 = 97;
    private const uint LOCALE_SSHORTESTDAYNAME3 = 98;
    private const uint LOCALE_SSHORTESTDAYNAME4 = 99;
    private const uint LOCALE_SSHORTESTDAYNAME5 = 100;
    private const uint LOCALE_SSHORTESTDAYNAME6 = 101;
    private const uint LOCALE_SSHORTESTDAYNAME7 = 102;
    private const uint LOCALE_SISO639LANGNAME2 = 103;
    private const uint LOCALE_SISO3166CTRYNAME2 = 104;
    private const uint LOCALE_SNAN = 105;
    private const uint LOCALE_SPOSINFINITY = 106;
    private const uint LOCALE_SNEGINFINITY = 107;
    private const uint LOCALE_SSCRIPTS = 108;
    private const uint LOCALE_SPARENT = 109;
    private const uint LOCALE_SCONSOLEFALLBACKNAME = 110;
    private const uint LOCALE_IREADINGLAYOUT = 112;
    private const uint LOCALE_INEUTRAL = 113;
    private const uint LOCALE_INEGATIVEPERCENT = 116;
    private const uint LOCALE_IPOSITIVEPERCENT = 117;
    private const uint LOCALE_SPERCENT = 118;
    private const uint LOCALE_SPERMILLE = 119;
    private const uint LOCALE_SMONTHDAY = 120;
    private const uint LOCALE_SSHORTTIME = 121;
    private const uint LOCALE_SOPENTYPELANGUAGETAG = 122;
    private const uint LOCALE_SSORTLOCALE = 123;
    internal const uint TIME_NOSECONDS = 2;

    private static Dictionary<string, string> RegionNames
    {
      get
      {
        if (CultureData.s_RegionNames == null)
          CultureData.s_RegionNames = new Dictionary<string, string>()
          {
            {
              "029",
              "en-029"
            },
            {
              "AE",
              "ar-AE"
            },
            {
              "AF",
              "prs-AF"
            },
            {
              "AL",
              "sq-AL"
            },
            {
              "AM",
              "hy-AM"
            },
            {
              "AR",
              "es-AR"
            },
            {
              "AT",
              "de-AT"
            },
            {
              "AU",
              "en-AU"
            },
            {
              "AZ",
              "az-Cyrl-AZ"
            },
            {
              "BA",
              "bs-Latn-BA"
            },
            {
              "BD",
              "bn-BD"
            },
            {
              "BE",
              "nl-BE"
            },
            {
              "BG",
              "bg-BG"
            },
            {
              "BH",
              "ar-BH"
            },
            {
              "BN",
              "ms-BN"
            },
            {
              "BO",
              "es-BO"
            },
            {
              "BR",
              "pt-BR"
            },
            {
              "BY",
              "be-BY"
            },
            {
              "BZ",
              "en-BZ"
            },
            {
              "CA",
              "en-CA"
            },
            {
              "CH",
              "it-CH"
            },
            {
              "CL",
              "es-CL"
            },
            {
              "CN",
              "zh-CN"
            },
            {
              "CO",
              "es-CO"
            },
            {
              "CR",
              "es-CR"
            },
            {
              "CS",
              "sr-Cyrl-CS"
            },
            {
              "CZ",
              "cs-CZ"
            },
            {
              "DE",
              "de-DE"
            },
            {
              "DK",
              "da-DK"
            },
            {
              "DO",
              "es-DO"
            },
            {
              "DZ",
              "ar-DZ"
            },
            {
              "EC",
              "es-EC"
            },
            {
              "EE",
              "et-EE"
            },
            {
              "EG",
              "ar-EG"
            },
            {
              "ES",
              "es-ES"
            },
            {
              "ET",
              "am-ET"
            },
            {
              "FI",
              "fi-FI"
            },
            {
              "FO",
              "fo-FO"
            },
            {
              "FR",
              "fr-FR"
            },
            {
              "GB",
              "en-GB"
            },
            {
              "GE",
              "ka-GE"
            },
            {
              "GL",
              "kl-GL"
            },
            {
              "GR",
              "el-GR"
            },
            {
              "GT",
              "es-GT"
            },
            {
              "HK",
              "zh-HK"
            },
            {
              "HN",
              "es-HN"
            },
            {
              "HR",
              "hr-HR"
            },
            {
              "HU",
              "hu-HU"
            },
            {
              "ID",
              "id-ID"
            },
            {
              "IE",
              "en-IE"
            },
            {
              "IL",
              "he-IL"
            },
            {
              "IN",
              "hi-IN"
            },
            {
              "IQ",
              "ar-IQ"
            },
            {
              "IR",
              "fa-IR"
            },
            {
              "IS",
              "is-IS"
            },
            {
              "IT",
              "it-IT"
            },
            {
              "IV",
              ""
            },
            {
              "JM",
              "en-JM"
            },
            {
              "JO",
              "ar-JO"
            },
            {
              "JP",
              "ja-JP"
            },
            {
              "KE",
              "sw-KE"
            },
            {
              "KG",
              "ky-KG"
            },
            {
              "KH",
              "km-KH"
            },
            {
              "KR",
              "ko-KR"
            },
            {
              "KW",
              "ar-KW"
            },
            {
              "KZ",
              "kk-KZ"
            },
            {
              "LA",
              "lo-LA"
            },
            {
              "LB",
              "ar-LB"
            },
            {
              "LI",
              "de-LI"
            },
            {
              "LK",
              "si-LK"
            },
            {
              "LT",
              "lt-LT"
            },
            {
              "LU",
              "lb-LU"
            },
            {
              "LV",
              "lv-LV"
            },
            {
              "LY",
              "ar-LY"
            },
            {
              "MA",
              "ar-MA"
            },
            {
              "MC",
              "fr-MC"
            },
            {
              "ME",
              "sr-Latn-ME"
            },
            {
              "MK",
              "mk-MK"
            },
            {
              "MN",
              "mn-MN"
            },
            {
              "MO",
              "zh-MO"
            },
            {
              "MT",
              "mt-MT"
            },
            {
              "MV",
              "dv-MV"
            },
            {
              "MX",
              "es-MX"
            },
            {
              "MY",
              "ms-MY"
            },
            {
              "NG",
              "ig-NG"
            },
            {
              "NI",
              "es-NI"
            },
            {
              "NL",
              "nl-NL"
            },
            {
              "NO",
              "nn-NO"
            },
            {
              "NP",
              "ne-NP"
            },
            {
              "NZ",
              "en-NZ"
            },
            {
              "OM",
              "ar-OM"
            },
            {
              "PA",
              "es-PA"
            },
            {
              "PE",
              "es-PE"
            },
            {
              "PH",
              "en-PH"
            },
            {
              "PK",
              "ur-PK"
            },
            {
              "PL",
              "pl-PL"
            },
            {
              "PR",
              "es-PR"
            },
            {
              "PT",
              "pt-PT"
            },
            {
              "PY",
              "es-PY"
            },
            {
              "QA",
              "ar-QA"
            },
            {
              "RO",
              "ro-RO"
            },
            {
              "RS",
              "sr-Latn-RS"
            },
            {
              "RU",
              "ru-RU"
            },
            {
              "RW",
              "rw-RW"
            },
            {
              "SA",
              "ar-SA"
            },
            {
              "SE",
              "sv-SE"
            },
            {
              "SG",
              "zh-SG"
            },
            {
              "SI",
              "sl-SI"
            },
            {
              "SK",
              "sk-SK"
            },
            {
              "SN",
              "wo-SN"
            },
            {
              "SV",
              "es-SV"
            },
            {
              "SY",
              "ar-SY"
            },
            {
              "TH",
              "th-TH"
            },
            {
              "TJ",
              "tg-Cyrl-TJ"
            },
            {
              "TM",
              "tk-TM"
            },
            {
              "TN",
              "ar-TN"
            },
            {
              "TR",
              "tr-TR"
            },
            {
              "TT",
              "en-TT"
            },
            {
              "TW",
              "zh-TW"
            },
            {
              "UA",
              "uk-UA"
            },
            {
              "US",
              "en-US"
            },
            {
              "UY",
              "es-UY"
            },
            {
              "UZ",
              "uz-Cyrl-UZ"
            },
            {
              "VE",
              "es-VE"
            },
            {
              "VN",
              "vi-VN"
            },
            {
              "YE",
              "ar-YE"
            },
            {
              "ZA",
              "af-ZA"
            },
            {
              "ZW",
              "en-ZW"
            }
          };
        return CultureData.s_RegionNames;
      }
    }

    internal static CultureData Invariant
    {
      get
      {
        if (CultureData.s_Invariant == null)
        {
          CultureData cultureData = new CultureData();
          cultureData.bUseOverrides = false;
          cultureData.sRealName = "";
          CultureData.nativeInitCultureData(cultureData);
          cultureData.bUseOverrides = false;
          cultureData.sRealName = "";
          cultureData.sWindowsName = "";
          cultureData.sName = "";
          cultureData.sParent = "";
          cultureData.bNeutral = false;
          cultureData.bFramework = true;
          cultureData.sEnglishDisplayName = "Invariant Language (Invariant Country)";
          cultureData.sNativeDisplayName = "Invariant Language (Invariant Country)";
          cultureData.sSpecificCulture = "";
          cultureData.sISO639Language = "iv";
          cultureData.sLocalizedLanguage = "Invariant Language";
          cultureData.sEnglishLanguage = "Invariant Language";
          cultureData.sNativeLanguage = "Invariant Language";
          cultureData.sRegionName = "IV";
          cultureData.iGeoId = 244;
          cultureData.sEnglishCountry = "Invariant Country";
          cultureData.sNativeCountry = "Invariant Country";
          cultureData.sISO3166CountryName = "IV";
          cultureData.sPositiveSign = "+";
          cultureData.sNegativeSign = "-";
          cultureData.saNativeDigits = new string[10]
          {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"
          };
          cultureData.iDigitSubstitution = 1;
          cultureData.iLeadingZeros = 1;
          cultureData.iDigits = 2;
          cultureData.iNegativeNumber = 1;
          cultureData.waGrouping = new int[1]{ 3 };
          cultureData.sDecimalSeparator = ".";
          cultureData.sThousandSeparator = ",";
          cultureData.sNaN = "NaN";
          cultureData.sPositiveInfinity = "Infinity";
          cultureData.sNegativeInfinity = "-Infinity";
          cultureData.iNegativePercent = 0;
          cultureData.iPositivePercent = 0;
          cultureData.sPercent = "%";
          cultureData.sPerMille = "‰";
          cultureData.sCurrency = "¤";
          cultureData.sIntlMonetarySymbol = "XDR";
          cultureData.sEnglishCurrency = "International Monetary Fund";
          cultureData.sNativeCurrency = "International Monetary Fund";
          cultureData.iCurrencyDigits = 2;
          cultureData.iCurrency = 0;
          cultureData.iNegativeCurrency = 0;
          cultureData.waMonetaryGrouping = new int[1]{ 3 };
          cultureData.sMonetaryDecimal = ".";
          cultureData.sMonetaryThousand = ",";
          cultureData.iMeasure = 0;
          cultureData.sListSeparator = ",";
          cultureData.sAM1159 = "AM";
          cultureData.sPM2359 = "PM";
          cultureData.saLongTimes = new string[1]
          {
            "HH:mm:ss"
          };
          cultureData.saShortTimes = new string[4]
          {
            "HH:mm",
            "hh:mm tt",
            "H:mm",
            "h:mm tt"
          };
          cultureData.saDurationFormats = new string[1]
          {
            "HH:mm:ss"
          };
          cultureData.iFirstDayOfWeek = 0;
          cultureData.iFirstWeekOfYear = 0;
          cultureData.waCalendars = new int[1]{ 1 };
          cultureData.calendars = new CalendarData[23];
          cultureData.calendars[0] = CalendarData.Invariant;
          cultureData.iReadingLayout = 0;
          cultureData.sTextInfo = "";
          cultureData.sCompareInfo = "";
          cultureData.sScripts = "Latn;";
          cultureData.iLanguage = (int) sbyte.MaxValue;
          cultureData.iDefaultAnsiCodePage = 1252;
          cultureData.iDefaultOemCodePage = 437;
          cultureData.iDefaultMacCodePage = 10000;
          cultureData.iDefaultEbcdicCodePage = 37;
          cultureData.sAbbrevLang = "IVL";
          cultureData.sAbbrevCountry = "IVC";
          cultureData.sISO639Language2 = "ivl";
          cultureData.sISO3166CountryName2 = "ivc";
          cultureData.iInputLanguageHandle = (int) sbyte.MaxValue;
          cultureData.sConsoleFallbackName = "";
          cultureData.sKeyboardsToInstall = "0409:00000409";
          CultureData.s_Invariant = cultureData;
        }
        return CultureData.s_Invariant;
      }
    }

    [SecurityCritical]
    private static bool IsResourcePresent(string resourceKey)
    {
      if (CultureData.MscorlibResourceSet == null)
        CultureData.MscorlibResourceSet = new ResourceSet(typeof (Environment).Assembly.GetManifestResourceStream("mscorlib.resources"));
      return CultureData.MscorlibResourceSet.GetString(resourceKey) != null;
    }

    [FriendAccessAllowed]
    internal static CultureData GetCultureData(string cultureName, bool useUserOverride)
    {
      if (string.IsNullOrEmpty(cultureName))
        return CultureData.Invariant;
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        if (cultureName.Equals("iw", StringComparison.OrdinalIgnoreCase))
          cultureName = "he";
        else if (cultureName.Equals("tl", StringComparison.OrdinalIgnoreCase))
          cultureName = "fil";
        else if (cultureName.Equals("english", StringComparison.OrdinalIgnoreCase))
          cultureName = "en";
      }
      string lower = CultureData.AnsiToLower(useUserOverride ? cultureName : cultureName + "*");
      Dictionary<string, CultureData> dictionary = CultureData.s_cachedCultures;
      if (dictionary == null)
      {
        dictionary = new Dictionary<string, CultureData>();
      }
      else
      {
        CultureData cultureData;
        lock (((ICollection) dictionary).SyncRoot)
          dictionary.TryGetValue(lower, out cultureData);
        if (cultureData != null)
          return cultureData;
      }
      CultureData cultureData1 = CultureData.CreateCultureData(cultureName, useUserOverride);
      if (cultureData1 == null)
        return (CultureData) null;
      lock (((ICollection) dictionary).SyncRoot)
        dictionary[lower] = cultureData1;
      CultureData.s_cachedCultures = dictionary;
      return cultureData1;
    }

    private static CultureData CreateCultureData(string cultureName, bool useUserOverride)
    {
      CultureData cultureData = new CultureData();
      cultureData.bUseOverrides = useUserOverride;
      cultureData.sRealName = cultureName;
      if (!cultureData.InitCultureData() && !cultureData.InitCompatibilityCultureData() && !cultureData.InitLegacyAlternateSortData())
        return (CultureData) null;
      return cultureData;
    }

    private bool InitCultureData()
    {
      if (!CultureData.nativeInitCultureData(this))
        return false;
      if (CultureInfo.IsTaiwanSku)
        this.TreatTaiwanParentChainAsHavingTaiwanAsSpecific();
      return true;
    }

    [SecuritySafeCritical]
    private void TreatTaiwanParentChainAsHavingTaiwanAsSpecific()
    {
      if (!this.IsNeutralInParentChainOfTaiwan() || !CultureData.IsOsPriorToWin7() || this.IsReplacementCulture)
        return;
      string str = this.SNATIVELANGUAGE;
      str = this.SENGLISHLANGUAGE;
      str = this.SLOCALIZEDLANGUAGE;
      str = this.STEXTINFO;
      str = this.SCOMPAREINFO;
      str = this.FONTSIGNATURE;
      int num = this.IDEFAULTANSICODEPAGE;
      num = this.IDEFAULTOEMCODEPAGE;
      num = this.IDEFAULTMACCODEPAGE;
      this.sSpecificCulture = "zh-TW";
      this.sWindowsName = "zh-TW";
    }

    private bool IsNeutralInParentChainOfTaiwan()
    {
      if (!(this.sRealName == "zh"))
        return this.sRealName == "zh-Hant";
      return true;
    }

    private static bool IsOsPriorToWin7()
    {
      if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        return Environment.OSVersion.Version < CultureData.s_win7Version;
      return false;
    }

    private static bool IsOsWin7OrPrior()
    {
      if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        return Environment.OSVersion.Version < new Version(6, 2);
      return false;
    }

    private bool InitCompatibilityCultureData()
    {
      string lower = CultureData.AnsiToLower(this.sRealName);
      string str1;
      string str2;
      if (!(lower == "zh-chs"))
      {
        if (!(lower == "zh-cht"))
          return false;
        str1 = "zh-Hant";
        str2 = "zh-CHT";
      }
      else
      {
        str1 = "zh-Hans";
        str2 = "zh-CHS";
      }
      this.sRealName = str1;
      if (!this.InitCultureData())
        return false;
      this.sName = str2;
      this.sParent = str1;
      this.bFramework = true;
      return true;
    }

    private bool InitLegacyAlternateSortData()
    {
      if (!CompareInfo.IsLegacy20SortingBehaviorRequested)
        return false;
      string lower = CultureData.AnsiToLower(this.sRealName);
      string str;
      if (!(lower == "ko-kr_unicod"))
      {
        if (!(lower == "ja-jp_unicod"))
        {
          if (!(lower == "zh-hk_stroke"))
            return false;
          str = "zh-HK_stroke";
          this.sRealName = "zh-HK";
          this.iLanguage = 134148;
        }
        else
        {
          str = "ja-JP_unicod";
          this.sRealName = "ja-JP";
          this.iLanguage = 66577;
        }
      }
      else
      {
        str = "ko-KR_unicod";
        this.sRealName = "ko-KR";
        this.iLanguage = 66578;
      }
      if (!CultureData.nativeInitCultureData(this))
        return false;
      this.sRealName = str;
      this.sCompareInfo = str;
      this.bFramework = true;
      return true;
    }

    [SecurityCritical]
    internal static CultureData GetCultureDataForRegion(string cultureName, bool useUserOverride)
    {
      if (string.IsNullOrEmpty(cultureName))
        return CultureData.Invariant;
      CultureData cultureData1 = CultureData.GetCultureData(cultureName, useUserOverride);
      if (cultureData1 != null && !cultureData1.IsNeutralCulture)
        return cultureData1;
      CultureData cultureData2 = cultureData1;
      string lower = CultureData.AnsiToLower(useUserOverride ? cultureName : cultureName + "*");
      Dictionary<string, CultureData> dictionary = CultureData.s_cachedRegions;
      if (dictionary == null)
      {
        dictionary = new Dictionary<string, CultureData>();
      }
      else
      {
        lock (((ICollection) dictionary).SyncRoot)
          dictionary.TryGetValue(lower, out cultureData1);
        if (cultureData1 != null)
          return cultureData1;
      }
      try
      {
        RegistryKey registryKey = Registry.LocalMachine.InternalOpenSubKey(CultureData.s_RegionKey, false);
        if (registryKey != null)
        {
          try
          {
            object obj = registryKey.InternalGetValue(cultureName, (object) null, false, false);
            if (obj != null)
              cultureData1 = CultureData.GetCultureData(obj.ToString(), useUserOverride);
          }
          finally
          {
            registryKey.Close();
          }
        }
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
      if ((cultureData1 == null || cultureData1.IsNeutralCulture) && CultureData.RegionNames.ContainsKey(cultureName))
        cultureData1 = CultureData.GetCultureData(CultureData.RegionNames[cultureName], useUserOverride);
      if (cultureData1 == null || cultureData1.IsNeutralCulture)
      {
        CultureInfo[] specificCultures = CultureData.SpecificCultures;
        for (int index = 0; index < specificCultures.Length; ++index)
        {
          if (string.Compare(specificCultures[index].m_cultureData.SREGIONNAME, cultureName, StringComparison.OrdinalIgnoreCase) == 0)
          {
            cultureData1 = specificCultures[index].m_cultureData;
            break;
          }
        }
      }
      if (cultureData1 != null && !cultureData1.IsNeutralCulture)
      {
        lock (((ICollection) dictionary).SyncRoot)
          dictionary[lower] = cultureData1;
        CultureData.s_cachedRegions = dictionary;
      }
      else
        cultureData1 = cultureData2;
      return cultureData1;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string LCIDToLocaleName(int lcid);

    internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
    {
      string cultureName = (string) null;
      CultureData cultureData = (CultureData) null;
      if (CompareInfo.IsLegacy20SortingBehaviorRequested)
      {
        switch (culture)
        {
          case 66577:
            cultureName = "ja-JP_unicod";
            break;
          case 66578:
            cultureName = "ko-KR_unicod";
            break;
          case 134148:
            cultureName = "zh-HK_stroke";
            break;
        }
      }
      if (cultureName == null)
        cultureName = CultureData.LCIDToLocaleName(culture);
      if (string.IsNullOrEmpty(cultureName))
      {
        if (culture == (int) sbyte.MaxValue)
          return CultureData.Invariant;
      }
      else
      {
        if (!(cultureName == "zh-Hans"))
        {
          if (cultureName == "zh-Hant")
            cultureName = "zh-CHT";
        }
        else
          cultureName = "zh-CHS";
        cultureData = CultureData.GetCultureData(cultureName, bUseUserOverride);
      }
      if (cultureData == null)
        throw new CultureNotFoundException(nameof (culture), culture, Environment.GetResourceString("Argument_CultureNotSupported"));
      return cultureData;
    }

    internal static void ClearCachedData()
    {
      CultureData.s_cachedCultures = (Dictionary<string, CultureData>) null;
      CultureData.s_cachedRegions = (Dictionary<string, CultureData>) null;
      CultureData.s_replacementCultureNames = (string[]) null;
    }

    [SecuritySafeCritical]
    internal static CultureInfo[] GetCultures(CultureTypes types)
    {
      if (types <= (CultureTypes) 0 || (types & ~(CultureTypes.AllCultures | CultureTypes.UserCustomCulture | CultureTypes.ReplacementCultures | CultureTypes.WindowsOnlyCultures | CultureTypes.FrameworkCultures)) != (CultureTypes) 0)
        throw new ArgumentOutOfRangeException(nameof (types), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) CultureTypes.NeutralCultures, (object) CultureTypes.FrameworkCultures));
      if ((types & CultureTypes.WindowsOnlyCultures) != (CultureTypes) 0)
        types &= ~CultureTypes.WindowsOnlyCultures;
      string[] o = (string[]) null;
      if (CultureData.nativeEnumCultureNames((int) types, JitHelpers.GetObjectHandleOnStack<string[]>(ref o)) == 0)
        return new CultureInfo[0];
      int length = o.Length;
      if ((types & (CultureTypes.NeutralCultures | CultureTypes.FrameworkCultures)) != (CultureTypes) 0)
        length += 2;
      CultureInfo[] cultureInfoArray = new CultureInfo[length];
      for (int index = 0; index < o.Length; ++index)
        cultureInfoArray[index] = new CultureInfo(o[index]);
      if ((types & (CultureTypes.NeutralCultures | CultureTypes.FrameworkCultures)) != (CultureTypes) 0)
      {
        cultureInfoArray[o.Length] = new CultureInfo("zh-CHS");
        cultureInfoArray[o.Length + 1] = new CultureInfo("zh-CHT");
      }
      return cultureInfoArray;
    }

    private static CultureInfo[] SpecificCultures
    {
      get
      {
        if (CultureData.specificCultures == null)
          CultureData.specificCultures = CultureData.GetCultures(CultureTypes.SpecificCultures);
        return CultureData.specificCultures;
      }
    }

    internal bool IsReplacementCulture
    {
      get
      {
        return CultureData.IsReplacementCultureName(this.SNAME);
      }
    }

    [SecuritySafeCritical]
    private static bool IsReplacementCultureName(string name)
    {
      string[] replacementCultureNames = CultureData.s_replacementCultureNames;
      if (replacementCultureNames == null)
      {
        if (CultureData.nativeEnumCultureNames(16, JitHelpers.GetObjectHandleOnStack<string[]>(ref replacementCultureNames)) == 0)
          return false;
        Array.Sort<string>(replacementCultureNames);
        CultureData.s_replacementCultureNames = replacementCultureNames;
      }
      return Array.BinarySearch<string>(replacementCultureNames, name) >= 0;
    }

    internal string CultureName
    {
      get
      {
        string sName = this.sName;
        if (sName == "zh-CHS" || sName == "zh-CHT")
          return this.sName;
        return this.sRealName;
      }
    }

    internal bool UseUserOverride
    {
      get
      {
        return this.bUseOverrides;
      }
    }

    internal string SNAME
    {
      get
      {
        if (this.sName == null)
          this.sName = string.Empty;
        return this.sName;
      }
    }

    internal string SPARENT
    {
      [SecurityCritical] get
      {
        if (this.sParent == null)
        {
          this.sParent = this.DoGetLocaleInfo(this.sRealName, 109U);
          string sParent = this.sParent;
          if (!(sParent == "zh-Hans"))
          {
            if (sParent == "zh-Hant")
              this.sParent = "zh-CHT";
          }
          else
            this.sParent = "zh-CHS";
        }
        return this.sParent;
      }
    }

    internal string SLOCALIZEDDISPLAYNAME
    {
      [SecurityCritical] get
      {
        if (this.sLocalizedDisplayName == null)
        {
          string str = "Globalization.ci_" + this.sName;
          if (CultureData.IsResourcePresent(str))
            this.sLocalizedDisplayName = Environment.GetResourceString(str);
          if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
          {
            if (this.IsNeutralCulture)
            {
              this.sLocalizedDisplayName = this.SLOCALIZEDLANGUAGE;
            }
            else
            {
              if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
                this.sLocalizedDisplayName = this.DoGetLocaleInfo(2U);
              if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
                this.sLocalizedDisplayName = this.SNATIVEDISPLAYNAME;
            }
          }
        }
        return this.sLocalizedDisplayName;
      }
    }

    internal string SENGDISPLAYNAME
    {
      [SecurityCritical] get
      {
        if (this.sEnglishDisplayName == null)
        {
          if (this.IsNeutralCulture)
          {
            this.sEnglishDisplayName = this.SENGLISHLANGUAGE;
            string sName = this.sName;
            if (sName == "zh-CHS" || sName == "zh-CHT")
              this.sEnglishDisplayName += " Legacy";
          }
          else
          {
            this.sEnglishDisplayName = this.DoGetLocaleInfo(114U);
            if (string.IsNullOrEmpty(this.sEnglishDisplayName))
              this.sEnglishDisplayName = !this.SENGLISHLANGUAGE.EndsWith(')') ? this.SENGLISHLANGUAGE + " (" + this.SENGCOUNTRY + ")" : this.SENGLISHLANGUAGE.Substring(0, this.sEnglishLanguage.Length - 1) + ", " + this.SENGCOUNTRY + ")";
          }
        }
        return this.sEnglishDisplayName;
      }
    }

    internal string SNATIVEDISPLAYNAME
    {
      [SecurityCritical] get
      {
        if (this.sNativeDisplayName == null)
        {
          if (this.IsNeutralCulture)
          {
            this.sNativeDisplayName = this.SNATIVELANGUAGE;
            string sName = this.sName;
            if (!(sName == "zh-CHS"))
            {
              if (sName == "zh-CHT")
                this.sNativeDisplayName += " 舊版";
            }
            else
              this.sNativeDisplayName += " 旧版";
          }
          else
          {
            this.sNativeDisplayName = !this.IsIncorrectNativeLanguageForSinhala() ? this.DoGetLocaleInfo(115U) : "සිංහල (ශ්\x200Dරී ලංකා)";
            if (string.IsNullOrEmpty(this.sNativeDisplayName))
              this.sNativeDisplayName = this.SNATIVELANGUAGE + " (" + this.SNATIVECOUNTRY + ")";
          }
        }
        return this.sNativeDisplayName;
      }
    }

    internal string SSPECIFICCULTURE
    {
      get
      {
        return this.sSpecificCulture;
      }
    }

    internal string SISO639LANGNAME
    {
      [SecurityCritical] get
      {
        if (this.sISO639Language == null)
          this.sISO639Language = this.DoGetLocaleInfo(89U);
        return this.sISO639Language;
      }
    }

    internal string SISO639LANGNAME2
    {
      [SecurityCritical] get
      {
        if (this.sISO639Language2 == null)
          this.sISO639Language2 = this.DoGetLocaleInfo(103U);
        return this.sISO639Language2;
      }
    }

    internal string SABBREVLANGNAME
    {
      [SecurityCritical] get
      {
        if (this.sAbbrevLang == null)
          this.sAbbrevLang = this.DoGetLocaleInfo(3U);
        return this.sAbbrevLang;
      }
    }

    internal string SLOCALIZEDLANGUAGE
    {
      [SecurityCritical] get
      {
        if (this.sLocalizedLanguage == null)
        {
          if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
            this.sLocalizedLanguage = this.DoGetLocaleInfo(111U);
          if (string.IsNullOrEmpty(this.sLocalizedLanguage))
            this.sLocalizedLanguage = this.SNATIVELANGUAGE;
        }
        return this.sLocalizedLanguage;
      }
    }

    internal string SENGLISHLANGUAGE
    {
      [SecurityCritical] get
      {
        if (this.sEnglishLanguage == null)
          this.sEnglishLanguage = this.DoGetLocaleInfo(4097U);
        return this.sEnglishLanguage;
      }
    }

    internal string SNATIVELANGUAGE
    {
      [SecurityCritical] get
      {
        if (this.sNativeLanguage == null)
          this.sNativeLanguage = !this.IsIncorrectNativeLanguageForSinhala() ? this.DoGetLocaleInfo(4U) : "සිංහල";
        return this.sNativeLanguage;
      }
    }

    private bool IsIncorrectNativeLanguageForSinhala()
    {
      if (CultureData.IsOsWin7OrPrior() && (this.sName == "si-LK" || this.sName == "si"))
        return !this.IsReplacementCulture;
      return false;
    }

    internal string SREGIONNAME
    {
      [SecurityCritical] get
      {
        if (this.sRegionName == null)
          this.sRegionName = this.DoGetLocaleInfo(90U);
        return this.sRegionName;
      }
    }

    internal int ICOUNTRY
    {
      get
      {
        return this.DoGetLocaleInfoInt(5U);
      }
    }

    internal int IGEOID
    {
      get
      {
        if (this.iGeoId == -1)
          this.iGeoId = this.DoGetLocaleInfoInt(91U);
        return this.iGeoId;
      }
    }

    internal string SLOCALIZEDCOUNTRY
    {
      [SecurityCritical] get
      {
        if (this.sLocalizedCountry == null)
        {
          string str = "Globalization.ri_" + this.SREGIONNAME;
          if (CultureData.IsResourcePresent(str))
            this.sLocalizedCountry = Environment.GetResourceString(str);
          if (string.IsNullOrEmpty(this.sLocalizedCountry))
          {
            if (CultureInfo.UserDefaultUICulture.Name.Equals(Thread.CurrentThread.CurrentUICulture.Name))
              this.sLocalizedCountry = this.DoGetLocaleInfo(6U);
            if (string.IsNullOrEmpty(this.sLocalizedDisplayName))
              this.sLocalizedCountry = this.SNATIVECOUNTRY;
          }
        }
        return this.sLocalizedCountry;
      }
    }

    internal string SENGCOUNTRY
    {
      [SecurityCritical] get
      {
        if (this.sEnglishCountry == null)
          this.sEnglishCountry = this.DoGetLocaleInfo(4098U);
        return this.sEnglishCountry;
      }
    }

    internal string SNATIVECOUNTRY
    {
      [SecurityCritical] get
      {
        if (this.sNativeCountry == null)
          this.sNativeCountry = this.DoGetLocaleInfo(8U);
        return this.sNativeCountry;
      }
    }

    internal string SISO3166CTRYNAME
    {
      [SecurityCritical] get
      {
        if (this.sISO3166CountryName == null)
          this.sISO3166CountryName = this.DoGetLocaleInfo(90U);
        return this.sISO3166CountryName;
      }
    }

    internal string SISO3166CTRYNAME2
    {
      [SecurityCritical] get
      {
        if (this.sISO3166CountryName2 == null)
          this.sISO3166CountryName2 = this.DoGetLocaleInfo(104U);
        return this.sISO3166CountryName2;
      }
    }

    internal string SABBREVCTRYNAME
    {
      [SecurityCritical] get
      {
        if (this.sAbbrevCountry == null)
          this.sAbbrevCountry = this.DoGetLocaleInfo(7U);
        return this.sAbbrevCountry;
      }
    }

    private int IDEFAULTCOUNTRY
    {
      get
      {
        return this.DoGetLocaleInfoInt(10U);
      }
    }

    internal int IINPUTLANGUAGEHANDLE
    {
      get
      {
        if (this.iInputLanguageHandle == -1)
          this.iInputLanguageHandle = !this.IsSupplementalCustomCulture ? this.ILANGUAGE : 1033;
        return this.iInputLanguageHandle;
      }
    }

    internal string SCONSOLEFALLBACKNAME
    {
      [SecurityCritical] get
      {
        if (this.sConsoleFallbackName == null)
        {
          string str = this.DoGetLocaleInfo(110U);
          if (str == "es-ES_tradnl")
            str = "es-ES";
          this.sConsoleFallbackName = str;
        }
        return this.sConsoleFallbackName;
      }
    }

    private bool ILEADINGZEROS
    {
      get
      {
        return this.DoGetLocaleInfoInt(18U) == 1;
      }
    }

    internal int[] WAGROUPING
    {
      [SecurityCritical] get
      {
        if (this.waGrouping == null || this.UseUserOverride)
          this.waGrouping = CultureData.ConvertWin32GroupString(this.DoGetLocaleInfo(16U));
        return this.waGrouping;
      }
    }

    internal string SNAN
    {
      [SecurityCritical] get
      {
        if (this.sNaN == null)
          this.sNaN = this.DoGetLocaleInfo(105U);
        return this.sNaN;
      }
    }

    internal string SPOSINFINITY
    {
      [SecurityCritical] get
      {
        if (this.sPositiveInfinity == null)
          this.sPositiveInfinity = this.DoGetLocaleInfo(106U);
        return this.sPositiveInfinity;
      }
    }

    internal string SNEGINFINITY
    {
      [SecurityCritical] get
      {
        if (this.sNegativeInfinity == null)
          this.sNegativeInfinity = this.DoGetLocaleInfo(107U);
        return this.sNegativeInfinity;
      }
    }

    internal int INEGATIVEPERCENT
    {
      get
      {
        if (this.iNegativePercent == -1)
          this.iNegativePercent = this.DoGetLocaleInfoInt(116U);
        return this.iNegativePercent;
      }
    }

    internal int IPOSITIVEPERCENT
    {
      get
      {
        if (this.iPositivePercent == -1)
          this.iPositivePercent = this.DoGetLocaleInfoInt(117U);
        return this.iPositivePercent;
      }
    }

    internal string SPERCENT
    {
      [SecurityCritical] get
      {
        if (this.sPercent == null)
          this.sPercent = this.DoGetLocaleInfo(118U);
        return this.sPercent;
      }
    }

    internal string SPERMILLE
    {
      [SecurityCritical] get
      {
        if (this.sPerMille == null)
          this.sPerMille = this.DoGetLocaleInfo(119U);
        return this.sPerMille;
      }
    }

    internal string SCURRENCY
    {
      [SecurityCritical] get
      {
        if (this.sCurrency == null || this.UseUserOverride)
          this.sCurrency = this.DoGetLocaleInfo(20U);
        return this.sCurrency;
      }
    }

    internal string SINTLSYMBOL
    {
      [SecurityCritical] get
      {
        if (this.sIntlMonetarySymbol == null)
          this.sIntlMonetarySymbol = this.DoGetLocaleInfo(21U);
        return this.sIntlMonetarySymbol;
      }
    }

    internal string SENGLISHCURRENCY
    {
      [SecurityCritical] get
      {
        if (this.sEnglishCurrency == null)
          this.sEnglishCurrency = this.DoGetLocaleInfo(4103U);
        return this.sEnglishCurrency;
      }
    }

    internal string SNATIVECURRENCY
    {
      [SecurityCritical] get
      {
        if (this.sNativeCurrency == null)
          this.sNativeCurrency = this.DoGetLocaleInfo(4104U);
        return this.sNativeCurrency;
      }
    }

    internal int[] WAMONGROUPING
    {
      [SecurityCritical] get
      {
        if (this.waMonetaryGrouping == null || this.UseUserOverride)
          this.waMonetaryGrouping = CultureData.ConvertWin32GroupString(this.DoGetLocaleInfo(24U));
        return this.waMonetaryGrouping;
      }
    }

    internal int IMEASURE
    {
      get
      {
        if (this.iMeasure == -1 || this.UseUserOverride)
          this.iMeasure = this.DoGetLocaleInfoInt(13U);
        return this.iMeasure;
      }
    }

    internal string SLIST
    {
      [SecurityCritical] get
      {
        if (this.sListSeparator == null || this.UseUserOverride)
          this.sListSeparator = this.DoGetLocaleInfo(12U);
        return this.sListSeparator;
      }
    }

    private int IPAPERSIZE
    {
      get
      {
        return this.DoGetLocaleInfoInt(4106U);
      }
    }

    internal string SAM1159
    {
      [SecurityCritical] get
      {
        if (this.sAM1159 == null || this.UseUserOverride)
          this.sAM1159 = this.DoGetLocaleInfo(40U);
        return this.sAM1159;
      }
    }

    internal string SPM2359
    {
      [SecurityCritical] get
      {
        if (this.sPM2359 == null || this.UseUserOverride)
          this.sPM2359 = this.DoGetLocaleInfo(41U);
        return this.sPM2359;
      }
    }

    internal string[] LongTimes
    {
      get
      {
        if (this.saLongTimes == null || this.UseUserOverride)
        {
          string[] strArray = this.DoEnumTimeFormats();
          this.saLongTimes = strArray == null || strArray.Length == 0 ? CultureData.Invariant.saLongTimes : strArray;
        }
        return this.saLongTimes;
      }
    }

    internal string[] ShortTimes
    {
      get
      {
        if (this.saShortTimes == null || this.UseUserOverride)
        {
          string[] strArray = this.DoEnumShortTimeFormats();
          if (strArray == null || strArray.Length == 0)
            strArray = this.DeriveShortTimesFromLong();
          this.saShortTimes = strArray;
        }
        return this.saShortTimes;
      }
    }

    private string[] DeriveShortTimesFromLong()
    {
      string[] strArray = new string[this.LongTimes.Length];
      for (int index = 0; index < this.LongTimes.Length; ++index)
        strArray[index] = CultureData.StripSecondsFromPattern(this.LongTimes[index]);
      return strArray;
    }

    private static string StripSecondsFromPattern(string time)
    {
      bool flag = false;
      int num = -1;
      for (int index = 0; index < time.Length; ++index)
      {
        if (time[index] == '\'')
          flag = !flag;
        else if (time[index] == '\\')
          ++index;
        else if (!flag)
        {
          switch (time[index])
          {
            case 'H':
            case 'h':
            case 'm':
              num = index;
              continue;
            case 's':
              if (index - num <= 4 && index - num > 1 && (time[num + 1] != '\'' && time[index - 1] != '\'') && num >= 0)
                index = num + 1;
              bool containsSpace;
              int tokenAfterSeconds = CultureData.GetIndexOfNextTokenAfterSeconds(time, index, out containsSpace);
              StringBuilder stringBuilder = new StringBuilder(time.Substring(0, index));
              if (containsSpace)
                stringBuilder.Append(' ');
              stringBuilder.Append(time.Substring(tokenAfterSeconds));
              time = stringBuilder.ToString();
              continue;
            default:
              continue;
          }
        }
      }
      return time;
    }

    private static int GetIndexOfNextTokenAfterSeconds(string time, int index, out bool containsSpace)
    {
      bool flag = false;
      containsSpace = false;
      for (; index < time.Length; ++index)
      {
        switch (time[index])
        {
          case ' ':
            containsSpace = true;
            break;
          case '\'':
            flag = !flag;
            break;
          case 'H':
          case 'h':
          case 'm':
          case 't':
            if (!flag)
              return index;
            break;
          case '\\':
            ++index;
            if (time[index] == ' ')
            {
              containsSpace = true;
              break;
            }
            break;
        }
      }
      containsSpace = false;
      return index;
    }

    internal string[] SADURATION
    {
      [SecurityCritical] get
      {
        if (this.saDurationFormats == null)
          this.saDurationFormats = new string[1]
          {
            CultureData.ReescapeWin32String(this.DoGetLocaleInfo(93U))
          };
        return this.saDurationFormats;
      }
    }

    internal int IFIRSTDAYOFWEEK
    {
      get
      {
        if (this.iFirstDayOfWeek == -1 || this.UseUserOverride)
          this.iFirstDayOfWeek = CultureData.ConvertFirstDayOfWeekMonToSun(this.DoGetLocaleInfoInt(4108U));
        return this.iFirstDayOfWeek;
      }
    }

    internal int IFIRSTWEEKOFYEAR
    {
      get
      {
        if (this.iFirstWeekOfYear == -1 || this.UseUserOverride)
          this.iFirstWeekOfYear = this.DoGetLocaleInfoInt(4109U);
        return this.iFirstWeekOfYear;
      }
    }

    internal string[] ShortDates(int calendarId)
    {
      return this.GetCalendar(calendarId).saShortDates;
    }

    internal string[] LongDates(int calendarId)
    {
      return this.GetCalendar(calendarId).saLongDates;
    }

    internal string[] YearMonths(int calendarId)
    {
      return this.GetCalendar(calendarId).saYearMonths;
    }

    internal string[] DayNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saDayNames;
    }

    internal string[] AbbreviatedDayNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saAbbrevDayNames;
    }

    internal string[] SuperShortDayNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saSuperShortDayNames;
    }

    internal string[] MonthNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saMonthNames;
    }

    internal string[] GenitiveMonthNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saMonthGenitiveNames;
    }

    internal string[] AbbreviatedMonthNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saAbbrevMonthNames;
    }

    internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
    }

    internal string[] LeapYearMonthNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saLeapYearMonthNames;
    }

    internal string MonthDay(int calendarId)
    {
      return this.GetCalendar(calendarId).sMonthDay;
    }

    internal int[] CalendarIds
    {
      get
      {
        if (this.waCalendars == null)
        {
          int[] calendars1 = new int[23];
          int calendars2 = CalendarData.nativeGetCalendars(this.sWindowsName, this.bUseOverrides, calendars1);
          if (calendars2 == 0)
          {
            this.waCalendars = CultureData.Invariant.waCalendars;
          }
          else
          {
            if (this.sWindowsName == "zh-TW")
            {
              bool flag = false;
              for (int index = 0; index < calendars2; ++index)
              {
                if (calendars1[index] == 4)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                ++calendars2;
                Array.Copy((Array) calendars1, 1, (Array) calendars1, 2, 21);
                calendars1[1] = 4;
              }
            }
            int[] numArray = new int[calendars2];
            Array.Copy((Array) calendars1, (Array) numArray, calendars2);
            this.waCalendars = numArray;
          }
        }
        return this.waCalendars;
      }
    }

    internal string CalendarName(int calendarId)
    {
      return this.GetCalendar(calendarId).sNativeName;
    }

    internal CalendarData GetCalendar(int calendarId)
    {
      int index = calendarId - 1;
      if (this.calendars == null)
        this.calendars = new CalendarData[23];
      CalendarData calendarData = this.calendars[index];
      if (calendarData == null || this.UseUserOverride)
      {
        calendarData = new CalendarData(this.sWindowsName, calendarId, this.UseUserOverride);
        if (CultureData.IsOsWin7OrPrior() && !this.IsSupplementalCustomCulture && !this.IsReplacementCulture)
          calendarData.FixupWin7MonthDaySemicolonBug();
        this.calendars[index] = calendarData;
      }
      return calendarData;
    }

    internal int CurrentEra(int calendarId)
    {
      return this.GetCalendar(calendarId).iCurrentEra;
    }

    internal bool IsRightToLeft
    {
      get
      {
        return this.IREADINGLAYOUT == 1;
      }
    }

    private int IREADINGLAYOUT
    {
      get
      {
        if (this.iReadingLayout == -1)
          this.iReadingLayout = this.DoGetLocaleInfoInt(112U);
        return this.iReadingLayout;
      }
    }

    internal string STEXTINFO
    {
      [SecuritySafeCritical] get
      {
        if (this.sTextInfo == null)
        {
          if (this.IsNeutralCulture || this.IsSupplementalCustomCulture)
            this.sTextInfo = CultureData.GetCultureData(this.DoGetLocaleInfo(123U), this.bUseOverrides).SNAME;
          if (this.sTextInfo == null)
            this.sTextInfo = this.SNAME;
        }
        return this.sTextInfo;
      }
    }

    internal string SCOMPAREINFO
    {
      [SecuritySafeCritical] get
      {
        if (this.sCompareInfo == null)
        {
          if (this.IsSupplementalCustomCulture)
            this.sCompareInfo = this.DoGetLocaleInfo(123U);
          if (this.sCompareInfo == null)
            this.sCompareInfo = this.sWindowsName;
        }
        return this.sCompareInfo;
      }
    }

    internal bool IsSupplementalCustomCulture
    {
      get
      {
        return CultureData.IsCustomCultureId(this.ILANGUAGE);
      }
    }

    private string SSCRIPTS
    {
      [SecuritySafeCritical] get
      {
        if (this.sScripts == null)
          this.sScripts = this.DoGetLocaleInfo(108U);
        return this.sScripts;
      }
    }

    private string SOPENTYPELANGUAGETAG
    {
      [SecuritySafeCritical] get
      {
        return this.DoGetLocaleInfo(122U);
      }
    }

    private string FONTSIGNATURE
    {
      [SecuritySafeCritical] get
      {
        if (this.fontSignature == null)
          this.fontSignature = this.DoGetLocaleInfo(88U);
        return this.fontSignature;
      }
    }

    private string SKEYBOARDSTOINSTALL
    {
      [SecuritySafeCritical] get
      {
        return this.DoGetLocaleInfo(94U);
      }
    }

    internal int IDEFAULTANSICODEPAGE
    {
      get
      {
        if (this.iDefaultAnsiCodePage == -1)
          this.iDefaultAnsiCodePage = this.DoGetLocaleInfoInt(4100U);
        return this.iDefaultAnsiCodePage;
      }
    }

    internal int IDEFAULTOEMCODEPAGE
    {
      get
      {
        if (this.iDefaultOemCodePage == -1)
          this.iDefaultOemCodePage = this.DoGetLocaleInfoInt(11U);
        return this.iDefaultOemCodePage;
      }
    }

    internal int IDEFAULTMACCODEPAGE
    {
      get
      {
        if (this.iDefaultMacCodePage == -1)
          this.iDefaultMacCodePage = this.DoGetLocaleInfoInt(4113U);
        return this.iDefaultMacCodePage;
      }
    }

    internal int IDEFAULTEBCDICCODEPAGE
    {
      get
      {
        if (this.iDefaultEbcdicCodePage == -1)
          this.iDefaultEbcdicCodePage = this.DoGetLocaleInfoInt(4114U);
        return this.iDefaultEbcdicCodePage;
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int LocaleNameToLCID(string localeName);

    internal int ILANGUAGE
    {
      get
      {
        if (this.iLanguage == 0)
          this.iLanguage = CultureData.LocaleNameToLCID(this.sRealName);
        return this.iLanguage;
      }
    }

    internal bool IsWin32Installed
    {
      get
      {
        return this.bWin32Installed;
      }
    }

    internal bool IsFramework
    {
      get
      {
        return this.bFramework;
      }
    }

    internal bool IsNeutralCulture
    {
      get
      {
        return this.bNeutral;
      }
    }

    internal bool IsInvariantCulture
    {
      get
      {
        return string.IsNullOrEmpty(this.SNAME);
      }
    }

    internal Calendar DefaultCalendar
    {
      get
      {
        int calType = this.DoGetLocaleInfoInt(4105U);
        if (calType == 0)
          calType = this.CalendarIds[0];
        return CultureInfo.GetCalendarInstance(calType);
      }
    }

    internal string[] EraNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saEraNames;
    }

    internal string[] AbbrevEraNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saAbbrevEraNames;
    }

    internal string[] AbbreviatedEnglishEraNames(int calendarId)
    {
      return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
    }

    internal string TimeSeparator
    {
      [SecuritySafeCritical] get
      {
        if (this.sTimeSeparator == null || this.UseUserOverride)
        {
          string format = CultureData.ReescapeWin32String(this.DoGetLocaleInfo(4099U));
          if (string.IsNullOrEmpty(format))
            format = this.LongTimes[0];
          this.sTimeSeparator = CultureData.GetTimeSeparator(format);
        }
        return this.sTimeSeparator;
      }
    }

    internal string DateSeparator(int calendarId)
    {
      return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
    }

    private static string UnescapeNlsString(string str, int start, int end)
    {
      StringBuilder stringBuilder = (StringBuilder) null;
      for (int index = start; index < str.Length && index <= end; ++index)
      {
        switch (str[index])
        {
          case '\'':
            if (stringBuilder == null)
            {
              stringBuilder = new StringBuilder(str, start, index - start, str.Length);
              break;
            }
            break;
          case '\\':
            if (stringBuilder == null)
              stringBuilder = new StringBuilder(str, start, index - start, str.Length);
            ++index;
            if (index < str.Length)
            {
              stringBuilder.Append(str[index]);
              break;
            }
            break;
          default:
            if (stringBuilder != null)
            {
              stringBuilder.Append(str[index]);
              break;
            }
            break;
        }
      }
      if (stringBuilder == null)
        return str.Substring(start, end - start + 1);
      return stringBuilder.ToString();
    }

    internal static string ReescapeWin32String(string str)
    {
      if (str == null)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      bool flag = false;
      for (int length = 0; length < str.Length; ++length)
      {
        if (str[length] == '\'')
        {
          if (flag)
          {
            if (length + 1 < str.Length && str[length + 1] == '\'')
            {
              if (stringBuilder == null)
                stringBuilder = new StringBuilder(str, 0, length, str.Length * 2);
              stringBuilder.Append("\\'");
              ++length;
              continue;
            }
            flag = false;
          }
          else
            flag = true;
        }
        else if (str[length] == '\\')
        {
          if (stringBuilder == null)
            stringBuilder = new StringBuilder(str, 0, length, str.Length * 2);
          stringBuilder.Append("\\\\");
          continue;
        }
        stringBuilder?.Append(str[length]);
      }
      if (stringBuilder == null)
        return str;
      return stringBuilder.ToString();
    }

    internal static string[] ReescapeWin32Strings(string[] array)
    {
      if (array != null)
      {
        for (int index = 0; index < array.Length; ++index)
          array[index] = CultureData.ReescapeWin32String(array[index]);
      }
      return array;
    }

    private static string GetTimeSeparator(string format)
    {
      return CultureData.GetSeparator(format, "Hhms");
    }

    private static string GetDateSeparator(string format)
    {
      return CultureData.GetSeparator(format, "dyM");
    }

    private static string GetSeparator(string format, string timeParts)
    {
      int index = CultureData.IndexOfTimePart(format, 0, timeParts);
      if (index != -1)
      {
        char ch = format[index];
        do
        {
          ++index;
        }
        while (index < format.Length && (int) format[index] == (int) ch);
        int num1 = index;
        if (num1 < format.Length)
        {
          int num2 = CultureData.IndexOfTimePart(format, num1, timeParts);
          if (num2 != -1)
            return CultureData.UnescapeNlsString(format, num1, num2 - 1);
        }
      }
      return string.Empty;
    }

    private static int IndexOfTimePart(string format, int startIndex, string timeParts)
    {
      bool flag = false;
      for (int index = startIndex; index < format.Length; ++index)
      {
        if (!flag && timeParts.IndexOf(format[index]) != -1)
          return index;
        switch (format[index])
        {
          case '\'':
            flag = !flag;
            break;
          case '\\':
            if (index + 1 < format.Length)
            {
              ++index;
              switch (format[index])
              {
                case '\'':
                case '\\':
                  continue;
                default:
                  --index;
                  continue;
              }
            }
            else
              break;
        }
      }
      return -1;
    }

    [SecurityCritical]
    private string DoGetLocaleInfo(uint lctype)
    {
      return this.DoGetLocaleInfo(this.sWindowsName, lctype);
    }

    [SecurityCritical]
    private string DoGetLocaleInfo(string localeName, uint lctype)
    {
      if (!this.UseUserOverride)
        lctype |= 2147483648U;
      return CultureInfo.nativeGetLocaleInfoEx(localeName, lctype) ?? string.Empty;
    }

    private int DoGetLocaleInfoInt(uint lctype)
    {
      if (!this.UseUserOverride)
        lctype |= 2147483648U;
      return CultureInfo.nativeGetLocaleInfoExInt(this.sWindowsName, lctype);
    }

    private string[] DoEnumTimeFormats()
    {
      return CultureData.ReescapeWin32Strings(CultureData.nativeEnumTimeFormats(this.sWindowsName, 0U, this.UseUserOverride));
    }

    private string[] DoEnumShortTimeFormats()
    {
      return CultureData.ReescapeWin32Strings(CultureData.nativeEnumTimeFormats(this.sWindowsName, 2U, this.UseUserOverride));
    }

    internal static bool IsCustomCultureId(int cultureId)
    {
      return cultureId == 3072 || cultureId == 4096;
    }

    [SecurityCritical]
    internal void GetNFIValues(NumberFormatInfo nfi)
    {
      if (this.IsInvariantCulture)
      {
        nfi.positiveSign = this.sPositiveSign;
        nfi.negativeSign = this.sNegativeSign;
        nfi.nativeDigits = this.saNativeDigits;
        nfi.digitSubstitution = this.iDigitSubstitution;
        nfi.numberGroupSeparator = this.sThousandSeparator;
        nfi.numberDecimalSeparator = this.sDecimalSeparator;
        nfi.numberDecimalDigits = this.iDigits;
        nfi.numberNegativePattern = this.iNegativeNumber;
        nfi.currencySymbol = this.sCurrency;
        nfi.currencyGroupSeparator = this.sMonetaryThousand;
        nfi.currencyDecimalSeparator = this.sMonetaryDecimal;
        nfi.currencyDecimalDigits = this.iCurrencyDigits;
        nfi.currencyNegativePattern = this.iNegativeCurrency;
        nfi.currencyPositivePattern = this.iCurrency;
      }
      else
        CultureData.nativeGetNumberFormatInfoValues(this.sWindowsName, nfi, this.UseUserOverride);
      nfi.numberGroupSizes = this.WAGROUPING;
      nfi.currencyGroupSizes = this.WAMONGROUPING;
      nfi.percentNegativePattern = this.INEGATIVEPERCENT;
      nfi.percentPositivePattern = this.IPOSITIVEPERCENT;
      nfi.percentSymbol = this.SPERCENT;
      nfi.perMilleSymbol = this.SPERMILLE;
      nfi.negativeInfinitySymbol = this.SNEGINFINITY;
      nfi.positiveInfinitySymbol = this.SPOSINFINITY;
      nfi.nanSymbol = this.SNAN;
      nfi.percentDecimalDigits = nfi.numberDecimalDigits;
      nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
      nfi.percentGroupSizes = nfi.numberGroupSizes;
      nfi.percentGroupSeparator = nfi.numberGroupSeparator;
      if (nfi.positiveSign == null || nfi.positiveSign.Length == 0)
        nfi.positiveSign = "+";
      if (nfi.currencyDecimalSeparator == null || nfi.currencyDecimalSeparator.Length == 0)
        nfi.currencyDecimalSeparator = nfi.numberDecimalSeparator;
      if (932 != this.IDEFAULTANSICODEPAGE && 949 != this.IDEFAULTANSICODEPAGE)
        return;
      nfi.ansiCurrencySymbol = "\\";
    }

    private static int ConvertFirstDayOfWeekMonToSun(int iTemp)
    {
      ++iTemp;
      if (iTemp > 6)
        iTemp = 0;
      return iTemp;
    }

    internal static string AnsiToLower(string testString)
    {
      StringBuilder stringBuilder = new StringBuilder(testString.Length);
      for (int index = 0; index < testString.Length; ++index)
      {
        char ch = testString[index];
        stringBuilder.Append(ch > 'Z' || ch < 'A' ? ch : (char) ((int) ch - 65 + 97));
      }
      return stringBuilder.ToString();
    }

    private static int[] ConvertWin32GroupString(string win32Str)
    {
      if (win32Str == null || win32Str.Length == 0)
        return new int[1]{ 3 };
      if (win32Str[0] == '0')
        return new int[1];
      int[] numArray;
      if (win32Str[win32Str.Length - 1] == '0')
      {
        numArray = new int[win32Str.Length / 2];
      }
      else
      {
        numArray = new int[win32Str.Length / 2 + 2];
        numArray[numArray.Length - 1] = 0;
      }
      int index1 = 0;
      for (int index2 = 0; index1 < win32Str.Length && index2 < numArray.Length; ++index2)
      {
        if (win32Str[index1] < '1' || win32Str[index1] > '9')
          return new int[1]{ 3 };
        numArray[index2] = (int) win32Str[index1] - 48;
        index1 += 2;
      }
      return numArray;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool nativeInitCultureData(CultureData cultureData);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool nativeGetNumberFormatInfoValues(string localeName, NumberFormatInfo nfi, bool useUserOverride);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string[] nativeEnumTimeFormats(string localeName, uint dwFlags, bool useUserOverride);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int nativeEnumCultureNames(int cultureTypes, ObjectHandleOnStack retStringArray);
  }
}
