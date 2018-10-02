// Decompiled with JetBrains decompiler
// Type: System.TimeZoneInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System
{
  /// <summary>Представляет любой часовой пояс в мире.</summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
  {
    private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();
    private static DateTime s_maxDateOnly = new DateTime(9999, 12, 31);
    private static DateTime s_minDateOnly = new DateTime(1, 1, 2);
    private string m_id;
    private string m_displayName;
    private string m_standardDisplayName;
    private string m_daylightDisplayName;
    private TimeSpan m_baseUtcOffset;
    private bool m_supportsDaylightSavingTime;
    private TimeZoneInfo.AdjustmentRule[] m_adjustmentRules;
    private const string c_timeZonesRegistryHive = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";
    private const string c_timeZonesRegistryHivePermissionList = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";
    private const string c_displayValue = "Display";
    private const string c_daylightValue = "Dlt";
    private const string c_standardValue = "Std";
    private const string c_muiDisplayValue = "MUI_Display";
    private const string c_muiDaylightValue = "MUI_Dlt";
    private const string c_muiStandardValue = "MUI_Std";
    private const string c_timeZoneInfoValue = "TZI";
    private const string c_firstEntryValue = "FirstEntry";
    private const string c_lastEntryValue = "LastEntry";
    private const string c_utcId = "UTC";
    private const string c_localId = "Local";
    private const int c_maxKeyLength = 255;
    private const int c_regByteLength = 44;
    private const long c_ticksPerMillisecond = 10000;
    private const long c_ticksPerSecond = 10000000;
    private const long c_ticksPerMinute = 600000000;
    private const long c_ticksPerHour = 36000000000;
    private const long c_ticksPerDay = 864000000000;
    private const long c_ticksPerDayRange = 863999990000;

    /// <summary>Возвращает идентификатор часового пояса.</summary>
    /// <returns>Идентификатор часового пояса.</returns>
    [__DynamicallyInvokable]
    public string Id
    {
      [__DynamicallyInvokable] get
      {
        return this.m_id;
      }
    }

    /// <summary>
    ///   Возвращает универсальное отображаемое имя, представляющее часовой пояс.
    /// </summary>
    /// <returns>Общее отображаемое имя часового пояса.</returns>
    [__DynamicallyInvokable]
    public string DisplayName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_displayName != null)
          return this.m_displayName;
        return string.Empty;
      }
    }

    /// <summary>
    ///   Возвращает отображаемое имя для зимнего времени часового пояса.
    /// </summary>
    /// <returns>
    ///   Отображаемое имя для зимнего времени часового пояса.
    /// </returns>
    [__DynamicallyInvokable]
    public string StandardName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_standardDisplayName != null)
          return this.m_standardDisplayName;
        return string.Empty;
      }
    }

    /// <summary>
    ///   Возвращает отображаемое имя для летнего времени текущего часового пояса.
    /// </summary>
    /// <returns>
    ///   Отображаемое имя для летнего времени текущего часового пояса.
    /// </returns>
    [__DynamicallyInvokable]
    public string DaylightName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_daylightDisplayName != null)
          return this.m_daylightDisplayName;
        return string.Empty;
      }
    }

    /// <summary>
    ///   Возвращает разницу между зимним временем в текущем часовом поясе и временем в формате UTC.
    /// </summary>
    /// <returns>
    ///   Объект, указывающий разницу между зимним временем в текущем часовом поясе и временем в формате UTC.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan BaseUtcOffset
    {
      [__DynamicallyInvokable] get
      {
        return this.m_baseUtcOffset;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, заданы ли для часового пояса какие-либо правила перехода на летнее время.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если часовой пояс поддерживает летнее время; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool SupportsDaylightSavingTime
    {
      [__DynamicallyInvokable] get
      {
        return this.m_supportsDaylightSavingTime;
      }
    }

    /// <summary>
    ///   Извлекает массив объектов <see cref="T:System.TimeZoneInfo.AdjustmentRule" />, который применяется к текущему объекту <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <returns>Массив объектов для данного часового пояса.</returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   В системе недостаточно памяти для создания копии правил коррекции в памяти.
    /// </exception>
    public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
    {
      if (this.m_adjustmentRules == null)
        return new TimeZoneInfo.AdjustmentRule[0];
      return (TimeZoneInfo.AdjustmentRule[]) this.m_adjustmentRules.Clone();
    }

    /// <summary>
    ///   Возвращает сведения о возможных датах и времени, с которыми можно сопоставить неоднозначные значения этих величин.
    /// </summary>
    /// <param name="dateTimeOffset">Дата и время.</param>
    /// <returns>
    ///   Массив объектов, представляющий возможные смещения относительно времени UTC, которым может соответствовать определенная дата и время.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dateTimeOffset" /> не является неоднозначным временем.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
    {
      if (!this.SupportsDaylightSavingTime)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), nameof (dateTimeOffset));
      DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
      bool flag = false;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
      if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
      {
        DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
        flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForTime, daylightTime);
      }
      if (!flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), nameof (dateTimeOffset));
      TimeSpan[] timeSpanArray = new TimeSpan[2];
      TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
      if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
      {
        timeSpanArray[0] = timeSpan;
        timeSpanArray[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
      }
      else
      {
        timeSpanArray[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
        timeSpanArray[1] = timeSpan;
      }
      return timeSpanArray;
    }

    /// <summary>
    ///   Возвращает сведения о возможных датах и времени, с которыми можно сопоставить неоднозначные значения этих величин.
    /// </summary>
    /// <param name="dateTime">Дата и время.</param>
    /// <returns>
    ///   Массив объектов, представляющий возможные смещения относительно времени UTC, которым может соответствовать определенная дата и время.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dateTime" /> является неоднозначным временем.
    /// </exception>
    [__DynamicallyInvokable]
    public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
    {
      if (!this.SupportsDaylightSavingTime)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), nameof (dateTime));
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, this, TimeZoneInfoOptions.None, cachedData);
      }
      else
        dateTime1 = dateTime;
      bool flag = false;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
      {
        DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
        flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime);
      }
      if (!flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), nameof (dateTime));
      TimeSpan[] timeSpanArray = new TimeSpan[2];
      TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
      if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
      {
        timeSpanArray[0] = timeSpan;
        timeSpanArray[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
      }
      else
      {
        timeSpanArray[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
        timeSpanArray[1] = timeSpan;
      }
      return timeSpanArray;
    }

    /// <summary>
    ///   Вычисляет для определенной даты и времени смещение или разность между временем в данном часовом поясе и временем в формате UTC.
    /// </summary>
    /// <param name="dateTimeOffset">
    ///   Дата и время, для которых необходимо определить смещение.
    /// </param>
    /// <returns>
    ///   Объект, в котором указывается разность между временем в формате UTC и временем в текущем часовом поясе.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
    {
      return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
    }

    /// <summary>
    ///   Вычисляет для определенной даты и времени смещение или разность между временем в данном часовом поясе и временем в формате UTC.
    /// </summary>
    /// <param name="dateTime">
    ///   Дата и время, для которых необходимо определить смещение.
    /// </param>
    /// <returns>
    ///   Объект, в котором указывается разность во времени между двумя часовыми поясами.
    /// </returns>
    [__DynamicallyInvokable]
    public TimeSpan GetUtcOffset(DateTime dateTime)
    {
      return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
    }

    internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
    }

    internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
    }

    private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (dateTime.Kind == DateTimeKind.Local)
      {
        if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
          return TimeZoneInfo.GetUtcOffsetFromUtc(TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags), this);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
          return this.m_baseUtcOffset;
        return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
      }
      return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
    }

    /// <summary>
    ///   Определяет, являются ли заданная дата и время в заданном часовом поясе неоднозначными и можно ли им сопоставить два и более момента времени в формате UTC.
    /// </summary>
    /// <param name="dateTimeOffset">Дата и время.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="dateTimeOffset" /> в текущем часовом поясе является неоднозначным; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
    {
      if (!this.m_supportsDaylightSavingTime)
        return false;
      return this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
    }

    /// <summary>
    ///   Определяет, являются ли заданная дата и время в заданном часовом поясе неоднозначными и можно ли им сопоставить два и более момента времени в формате UTC.
    /// </summary>
    /// <param name="dateTime">Значение даты и времени.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="dateTime" /> неоднозначен; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.DateTime.Kind" /> значения <paramref name="dateTime" /> — <see cref="F:System.DateTimeKind.Local" />, а <paramref name="dateTime" /> — недопустимое время.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsAmbiguousTime(DateTime dateTime)
    {
      return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      if (!this.m_supportsDaylightSavingTime)
        return false;
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, this, flags, cachedData);
      }
      else
        dateTime1 = dateTime;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime == null || !adjustmentRuleForTime.HasDaylightSaving)
        return false;
      DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
      return TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime);
    }

    /// <summary>
    ///   Указывает, попадают ли заданные дата и время в диапазон летнего времени для часового пояса текущего объекта <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <param name="dateTimeOffset">Значение даты и времени.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="dateTimeOffset" /> относится к летнему времени; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
    {
      bool isDaylightSavings;
      TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out isDaylightSavings);
      return isDaylightSavings;
    }

    /// <summary>
    ///   Указывает, попадают ли заданные дата и время в диапазон летнего времени для часового пояса текущего объекта <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <param name="dateTime">Значение даты и времени.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="dateTime" /> относится к летнему времени; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.DateTime.Kind" /> значения <paramref name="dateTime" /> — <see cref="F:System.DateTimeKind.Local" />, а <paramref name="dateTime" /> — недопустимое время.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime(DateTime dateTime)
    {
      return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
    }

    internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
    }

    private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (!this.m_supportsDaylightSavingTime || this.m_adjustmentRules == null)
        return false;
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
      }
      else
      {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
          if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
            return false;
          bool isDaylightSavings;
          TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out isDaylightSavings);
          return isDaylightSavings;
        }
        dateTime1 = dateTime;
      }
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime == null || !adjustmentRuleForTime.HasDaylightSaving)
        return false;
      DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
      return TimeZoneInfo.GetIsDaylightSavings(dateTime1, adjustmentRuleForTime, daylightTime, flags);
    }

    /// <summary>
    ///   Указывает, являются ли определенная дата и время допустимыми.
    /// </summary>
    /// <param name="dateTime">Значение даты и времени.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="dateTime" /> недопустимо; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsInvalidTime(DateTime dateTime)
    {
      bool flag = false;
      if (dateTime.Kind == DateTimeKind.Unspecified || dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
        if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
          flag = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
        }
        else
          flag = false;
      }
      return flag;
    }

    /// <summary>Удаляет кэшированные данные о часовом поясе.</summary>
    public static void ClearCachedData()
    {
      TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
    }

    /// <summary>
    ///   Преобразует время во время в другом часовом поясе, исходя из идентификатора этого пояса.
    /// </summary>
    /// <param name="dateTimeOffset">
    ///   Преобразовываемые дата и время.
    /// </param>
    /// <param name="destinationTimeZoneId">
    ///   Идентификатор часового пояса назначения.
    /// </param>
    /// <returns>Дата и время в часовом поясе назначения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destinationTimeZoneId" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Идентификатор часового пояса найден, однако данные реестра повреждены.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Процесс не имеет разрешений, необходимых для чтения из раздела реестра, который содержит сведения о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    ///   Идентификатор <paramref name="destinationTimeZoneId" /> не найден в локальной системе.
    /// </exception>
    public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
    {
      return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
    }

    /// <summary>
    ///   Преобразует время во время в другом часовом поясе, исходя из идентификатора этого пояса.
    /// </summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <param name="destinationTimeZoneId">
    ///   Идентификатор часового пояса назначения.
    /// </param>
    /// <returns>Дата и время в часовом поясе назначения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destinationTimeZoneId" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Идентификатор часового пояса найден, однако данные реестра повреждены.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Процесс не имеет разрешений, необходимых для чтения из раздела реестра, который содержит сведения о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    ///   Идентификатор <paramref name="destinationTimeZoneId" /> не найден в локальной системе.
    /// </exception>
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
    {
      return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
    }

    /// <summary>
    ///   Преобразует время в одном часовом поясе во время в другом, исходя из идентификаторов этих поясов.
    /// </summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <param name="sourceTimeZoneId">
    ///   Идентификатор исходного часового пояса.
    /// </param>
    /// <param name="destinationTimeZoneId">
    ///   Идентификатор часового пояса назначения.
    /// </param>
    /// <returns>
    ///   Дата и время в часовом поясе назначения, которые соответствуют значению параметра <paramref name="dateTime" /> в исходном часовом поясе.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.DateTime.Kind" /> параметра <paramref name="dateTime" /> не соответствует исходному часовому поясу.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="dateTime" /> является недопустимым временем в исходном часовом поясе.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sourceTimeZoneId" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="destinationTimeZoneId" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Идентификаторы часового пояса найдены, однако данные реестра повреждены.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Процесс не имеет разрешений, необходимых для чтения из раздела реестра, который содержит сведения о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    ///   Идентификатор <paramref name="sourceTimeZoneId" /> не найден в локальной системе.
    /// 
    ///   -или-
    /// 
    ///   Идентификатор <paramref name="destinationTimeZoneId" /> не найден в локальной системе.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для чтения из разделов реестра, которые содержат сведения о часовом поясе.
    /// </exception>
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
    {
      if (dateTime.Kind == DateTimeKind.Local && string.Compare(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase) == 0)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
      }
      if (dateTime.Kind != DateTimeKind.Utc || string.Compare(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase) != 0)
        return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
      TimeZoneInfo.CachedData cachedData1 = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData1.Utc, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData1);
    }

    /// <summary>
    ///   Преобразует время во время в заданном часовом поясе.
    /// </summary>
    /// <param name="dateTimeOffset">
    ///   Преобразовываемые дата и время.
    /// </param>
    /// <param name="destinationTimeZone">
    ///   Часовой пояс, в который требуется преобразовать <paramref name="dateTime" />.
    /// </param>
    /// <returns>Дата и время в часовом поясе назначения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   параметр <paramref name="destinationTimeZone" /> имеет значение <see langword="null" />;
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
    {
      if (destinationTimeZone == null)
        throw new ArgumentNullException(nameof (destinationTimeZone));
      DateTime utcDateTime = dateTimeOffset.UtcDateTime;
      TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
      long ticks = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
      if (ticks > DateTimeOffset.MaxValue.Ticks)
        return DateTimeOffset.MaxValue;
      if (ticks < DateTimeOffset.MinValue.Ticks)
        return DateTimeOffset.MinValue;
      return new DateTimeOffset(ticks, utcOffsetFromUtc);
    }

    /// <summary>
    ///   Преобразует время во время в заданном часовом поясе.
    /// </summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <param name="destinationTimeZone">
    ///   Часовой пояс, в который требуется преобразовать <paramref name="dateTime" />.
    /// </param>
    /// <returns>Дата и время в часовом поясе назначения.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="dateTime" /> представляет недопустимое время.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   параметр <paramref name="destinationTimeZone" /> имеет значение <see langword="null" />;
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      if (destinationTimeZone == null)
        throw new ArgumentNullException(nameof (destinationTimeZone));
      if (dateTime.Ticks == 0L)
        TimeZoneInfo.ClearCachedData();
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      if (dateTime.Kind == DateTimeKind.Utc)
        return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>
    ///   Преобразует время в одном часовом поясе во время в другом.
    /// </summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <param name="sourceTimeZone">
    ///   Часовой пояс, соответствующий значению <paramref name="dateTime" />.
    /// </param>
    /// <param name="destinationTimeZone">
    ///   Часовой пояс, в который требуется преобразовать <paramref name="dateTime" />.
    /// </param>
    /// <returns>
    ///   Дата и время в часовом поясе назначения, которые соответствуют значению параметра <paramref name="dateTime" /> в исходном часовом поясе.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.DateTime.Kind" /> параметра <paramref name="dateTime" /> — <see cref="F:System.DateTimeKind.Local" />, но параметр <paramref name="sourceTimeZone" /> не равен <see cref="F:System.DateTimeKind.Local" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// 
    ///   -или-
    /// 
    ///   Свойство <see cref="P:System.DateTime.Kind" /> параметра <paramref name="dateTime" /> — <see cref="F:System.DateTimeKind.Utc" />, но параметр <paramref name="sourceTimeZone" /> не равен <see cref="P:System.TimeZoneInfo.Utc" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="dateTime" /> является недопустимым временем (то есть он представляет время, которое не существует из-за правил коррекции часового пояса).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sourceTimeZone" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="destinationTimeZone" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
    {
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
    }

    internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
    {
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
    }

    private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (sourceTimeZone == null)
        throw new ArgumentNullException(nameof (sourceTimeZone));
      if (destinationTimeZone == null)
        throw new ArgumentNullException(nameof (destinationTimeZone));
      DateTimeKind correspondingKind1 = cachedData.GetCorrespondingKind(sourceTimeZone);
      if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions) 0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind1)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConvertMismatch"), nameof (sourceTimeZone));
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime);
      TimeSpan baseUtcOffset = sourceTimeZone.BaseUtcOffset;
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
          if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions) 0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
            throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsInvalid"), nameof (dateTime));
          bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
      if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind1 != DateTimeKind.Unspecified && correspondingKind1 == correspondingKind2)
        return dateTime;
      long ticks = dateTime.Ticks - baseUtcOffset.Ticks;
      bool isAmbiguousLocalDst = false;
      DateTime timeZone = TimeZoneInfo.ConvertUtcToTimeZone(ticks, destinationTimeZone, out isAmbiguousLocalDst);
      if (correspondingKind2 == DateTimeKind.Local)
        return new DateTime(timeZone.Ticks, DateTimeKind.Local, isAmbiguousLocalDst);
      return new DateTime(timeZone.Ticks, correspondingKind2);
    }

    /// <summary>
    ///   Преобразует время в формате UTC во время в указанном часовом поясе.
    /// </summary>
    /// <param name="dateTime">Время в формате UTC.</param>
    /// <param name="destinationTimeZone">
    ///   Часовой пояс, в который требуется преобразовать <paramref name="dateTime" />.
    /// </param>
    /// <returns>
    ///   Дата и время в часовом поясе назначения.
    ///    Его свойство <see cref="P:System.DateTime.Kind" /> равно <see cref="F:System.DateTimeKind.Utc" />, если значение <paramref name="destinationTimeZone" /> равно <see cref="P:System.TimeZoneInfo.Utc" />; в противном случае — значение свойства <see cref="P:System.DateTime.Kind" /> равно <see cref="F:System.DateTimeKind.Unspecified" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.DateTime.Kind" /> параметра <paramref name="dateTime" /> равно <see cref="F:System.DateTimeKind.Local" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destinationTimeZone" /> имеет значение <see langword="null" />.
    /// </exception>
    public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>Преобразует указанные дату и время в формат UTC.</summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <returns>
    ///   Время в формате UTC, соответствующее значению параметра <paramref name="dateTime" />.
    ///    Свойству <see cref="P:System.DateTime.Kind" /> значения <see cref="T:System.DateTime" /> всегда присваивается значение <see cref="F:System.DateTimeKind.Utc" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see langword="TimeZoneInfo.Local.IsInvalidDateTime(" /><paramref name="dateTime" /><see langword=")" /> возвращает <see langword="true" />.
    /// </exception>
    public static DateTime ConvertTimeToUtc(DateTime dateTime)
    {
      if (dateTime.Kind == DateTimeKind.Utc)
        return dateTime;
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
    }

    internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      if (dateTime.Kind == DateTimeKind.Utc)
        return dateTime;
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags, cachedData);
    }

    /// <summary>
    ///   Преобразует время в указанном часовом поясе в формат UTC.
    /// </summary>
    /// <param name="dateTime">Преобразовываемые дата и время.</param>
    /// <param name="sourceTimeZone">
    ///   Часовой пояс, соответствующий значению <paramref name="dateTime" />.
    /// </param>
    /// <returns>
    ///   Время в формате UTC, соответствующее значению параметра <paramref name="dateTime" />.
    ///    Свойству <see cref="P:System.DateTime.Kind" /> объекта <see cref="T:System.DateTime" /> всегда присваивается значение <see cref="F:System.DateTimeKind.Utc" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dateTime" /><see langword=".Kind" /> равно <see cref="F:System.DateTimeKind.Utc" />, а <paramref name="sourceTimeZone" /> не равно <see cref="P:System.TimeZoneInfo.Utc" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="dateTime" /><see langword=".Kind" /> равно <see cref="F:System.DateTimeKind.Local" />, а <paramref name="sourceTimeZone" /> не равно <see cref="P:System.TimeZoneInfo.Local" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceTimeZone" /><see langword=".IsInvalidDateTime(" /><paramref name="dateTime" /><see langword=")" /> возвращает <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sourceTimeZone" /> имеет значение <see langword="null" />.
    /// </exception>
    public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>
    ///   Определяет, равен ли текущий объект <see cref="T:System.TimeZoneInfo" /> другому объекту <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <param name="other">
    ///   Второй объект, сравниваемый с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если два объекта <see cref="T:System.TimeZoneInfo" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(TimeZoneInfo other)
    {
      if (other != null && string.Compare(this.m_id, other.m_id, StringComparison.OrdinalIgnoreCase) == 0)
        return this.HasSameRules(other);
      return false;
    }

    /// <summary>
    ///   Определяет, равен ли текущий объект <see cref="T:System.TimeZoneInfo" /> другому объекту.
    /// </summary>
    /// <param name="obj">
    ///   Второй объект, сравниваемый с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является объектом <see cref="T:System.TimeZoneInfo" />, который равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      TimeZoneInfo other = obj as TimeZoneInfo;
      if (other == null)
        return false;
      return this.Equals(other);
    }

    /// <summary>
    ///   Десериализует строку для повторного создания исходного сериализованного объекта <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <param name="source">
    ///   Строковое представление сериализованного объекта <see cref="T:System.TimeZoneInfo" />.
    /// </param>
    /// <returns>Исходный сериализованный объект.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="source" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" /> является пустой строкой.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Исходный параметр нельзя десериализовать обратно в объект <see cref="T:System.TimeZoneInfo" />.
    /// </exception>
    public static TimeZoneInfo FromSerializedString(string source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (source.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSerializedString", (object) source), nameof (source));
      return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
    }

    /// <summary>
    ///   Служит хэш-функцией для алгоритмов хэширования и таких структур данных, как хэш-таблицы.
    /// </summary>
    /// <returns>
    ///   32-битовое целое число со знаком, выступающее в роли хэш-кода данного объекта <see cref="T:System.TimeZoneInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_id.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
    }

    /// <summary>
    ///   Возвращает отсортированную коллекцию всех часовых поясов, сведения о которых доступны в локальной системе.
    /// </summary>
    /// <returns>
    ///   Доступная только для чтения коллекция объектов <see cref="T:System.TimeZoneInfo" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для хранения всех сведений о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на чтение из разделов реестра, которые содержат сведения о часовом поясе.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      lock (cachedData)
      {
        if (cachedData.m_readOnlySystemTimeZones == null)
        {
          PermissionSet permissionSet = new PermissionSet(PermissionState.None);
          permissionSet.AddPermission((IPermission) new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
          permissionSet.Assert();
          using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
          {
            if (registryKey != null)
            {
              foreach (string subKeyName in registryKey.GetSubKeyNames())
              {
                TimeZoneInfo timeZoneInfo;
                Exception e;
                int timeZone = (int) TimeZoneInfo.TryGetTimeZone(subKeyName, false, out timeZoneInfo, out e, cachedData);
              }
            }
            cachedData.m_allSystemTimeZonesRead = true;
          }
          List<TimeZoneInfo> timeZoneInfoList = cachedData.m_systemTimeZones == null ? new List<TimeZoneInfo>() : new List<TimeZoneInfo>((IEnumerable<TimeZoneInfo>) cachedData.m_systemTimeZones.Values);
          timeZoneInfoList.Sort((IComparer<TimeZoneInfo>) new TimeZoneInfo.TimeZoneInfoComparer());
          cachedData.m_readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>((IList<TimeZoneInfo>) timeZoneInfoList);
        }
      }
      return cachedData.m_readOnlySystemTimeZones;
    }

    /// <summary>
    ///   Указывает, совпадают ли правила коррекции текущего объекта и другого объекта <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <param name="other">
    ///   Второй объект, сравниваемый с текущим объектом <see cref="T:System.TimeZoneInfo" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если правила коррекции и базовые смещения двух часовых поясов совпадают; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="other" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool HasSameRules(TimeZoneInfo other)
    {
      if (other == null)
        throw new ArgumentNullException(nameof (other));
      if (this.m_baseUtcOffset != other.m_baseUtcOffset || this.m_supportsDaylightSavingTime != other.m_supportsDaylightSavingTime)
        return false;
      TimeZoneInfo.AdjustmentRule[] adjustmentRules1 = this.m_adjustmentRules;
      TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other.m_adjustmentRules;
      bool flag = adjustmentRules1 == null && adjustmentRules2 == null || adjustmentRules1 != null && adjustmentRules2 != null;
      if (!flag)
        return false;
      if (adjustmentRules1 != null)
      {
        if (adjustmentRules1.Length != adjustmentRules2.Length)
          return false;
        for (int index = 0; index < adjustmentRules1.Length; ++index)
        {
          if (!adjustmentRules1[index].Equals(adjustmentRules2[index]))
            return false;
        }
      }
      return flag;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeZoneInfo" />, представляющий местный часовой пояс.
    /// </summary>
    /// <returns>Объект, представляющий местный часовой пояс.</returns>
    [__DynamicallyInvokable]
    public static TimeZoneInfo Local
    {
      [__DynamicallyInvokable] get
      {
        return TimeZoneInfo.s_cachedData.Local;
      }
    }

    /// <summary>
    ///   Преобразует текущий объект <see cref="T:System.TimeZoneInfo" /> в сериализованную строку.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.TimeZoneInfo" />.
    /// </returns>
    public string ToSerializedString()
    {
      return TimeZoneInfo.StringSerializer.GetSerializedString(this);
    }

    /// <summary>
    ///   Возвращает отображаемое имя текущего объекта <see cref="T:System.TimeZoneInfo" />.
    /// </summary>
    /// <returns>
    ///   Значение свойства <see cref="P:System.TimeZoneInfo.DisplayName" /> текущего объекта <see cref="T:System.TimeZoneInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.DisplayName;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.TimeZoneInfo" />, представляющий часовой пояс UTC.
    /// </summary>
    /// <returns>Объект, представляющий часовой пояс UTC.</returns>
    [__DynamicallyInvokable]
    public static TimeZoneInfo Utc
    {
      [__DynamicallyInvokable] get
      {
        return TimeZoneInfo.s_cachedData.Utc;
      }
    }

    [SecurityCritical]
    private TimeZoneInfo(Win32Native.TimeZoneInformation zone, bool dstDisabled)
    {
      this.m_id = !string.IsNullOrEmpty(zone.StandardName) ? zone.StandardName : nameof (Local);
      this.m_baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
      if (!dstDisabled)
      {
        TimeZoneInfo.AdjustmentRule timeZoneInformation = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(new Win32Native.RegistryTimeZoneInformation(zone), DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
        if (timeZoneInformation != null)
        {
          this.m_adjustmentRules = new TimeZoneInfo.AdjustmentRule[1];
          this.m_adjustmentRules[0] = timeZoneInformation;
        }
      }
      TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out this.m_supportsDaylightSavingTime);
      this.m_displayName = zone.StandardName;
      this.m_standardDisplayName = zone.StandardName;
      this.m_daylightDisplayName = zone.DaylightName;
    }

    private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      bool adjustmentRulesSupportDst;
      TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out adjustmentRulesSupportDst);
      if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
        this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[]) adjustmentRules.Clone();
      this.m_id = id;
      this.m_baseUtcOffset = baseUtcOffset;
      this.m_displayName = displayName;
      this.m_standardDisplayName = standardDisplayName;
      this.m_daylightDisplayName = disableDaylightSavingTime ? (string) null : daylightDisplayName;
      this.m_supportsDaylightSavingTime = adjustmentRulesSupportDst && !disableDaylightSavingTime;
    }

    /// <summary>
    ///   Создает пользовательский часовой пояс с указанным идентификатором, смещением от времени в формате UTC, отображаемым именем, а также отображаемым именем зимнего времени.
    /// </summary>
    /// <param name="id">Идентификатор часового пояса.</param>
    /// <param name="baseUtcOffset">
    ///   Объект, представляющий разность между значением времени в данном часовом поясе и временем в формате UTC.
    /// </param>
    /// <param name="displayName">
    ///   Отображаемое имя нового часового пояса.
    /// </param>
    /// <param name="standardDisplayName">
    ///   Имя зимнего времени нового часового пояса.
    /// </param>
    /// <returns>Новый часовой пояс.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="id" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="id" /> представляет собой пустую строку ("").
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="baseUtcOffset" /> не представляет целое число минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="baseUtcOffset" /> больше 14 часов или меньше –14 часов.
    /// </exception>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
    {
      return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, (TimeZoneInfo.AdjustmentRule[]) null, false);
    }

    /// <summary>
    ///   Создает пользовательский часовой пояс с указанным идентификатором, смещением от времени в формате UTC, отображаемым именем, именем зимнего времени, именем летнего времени и правилами коррекции для летнего времени.
    /// </summary>
    /// <param name="id">Идентификатор часового пояса.</param>
    /// <param name="baseUtcOffset">
    ///   Объект, представляющий разность между значением времени в данном часовом поясе и временем в формате UTC.
    /// </param>
    /// <param name="displayName">
    ///   Отображаемое имя нового часового пояса.
    /// </param>
    /// <param name="standardDisplayName">
    ///   Имя зимнего времени нового часового пояса.
    /// </param>
    /// <param name="daylightDisplayName">
    ///   Имя летнего времени нового часового пояса.
    /// </param>
    /// <param name="adjustmentRules">
    ///   Массив, который прибавляет базовое смещение относительно UTC для определенного периода.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.TimeZoneInfo" />, представляющий новый часовой пояс.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="id" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="id" /> представляет собой пустую строку ("").
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="baseUtcOffset" /> не представляет целое число минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="baseUtcOffset" /> больше 14 часов или меньше –14 часов.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Правила коррекции, заданные в параметре <paramref name="adjustmentRules" />, перекрываются.
    /// 
    ///   -или-
    /// 
    ///   Последовательность правил коррекции, заданных в параметре <paramref name="adjustmentRules" />, не является хронологической.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько элементов в <paramref name="adjustmentRules" /> имеют значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   К одной дате может быть применено несколько правил коррекции.
    /// 
    ///   -или-
    /// 
    ///   Сумма параметра <paramref name="baseUtcOffset" /> и значения свойства <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> одного или нескольких объектов в массиве <paramref name="adjustmentRules" /> больше 14 часов или меньше –14 часов.
    /// </exception>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
    {
      return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
    }

    /// <summary>
    ///   Создает пользовательский часовой пояс с указанным идентификатором, смещением от времени в формате UTC, отображаемым именем, именем зимнего времени, именем летнего времени, правилами коррекции для летнего времени и значением, позволяющим определить, отражает ли возвращаемый объект сведения о летнем времени.
    /// </summary>
    /// <param name="id">Идентификатор часового пояса.</param>
    /// <param name="baseUtcOffset">
    ///   Объект <see cref="T:System.TimeSpan" />, представляющий разность между значением времени в данном часовом поясе и временем в формате UTC.
    /// </param>
    /// <param name="displayName">
    ///   Отображаемое имя нового часового пояса.
    /// </param>
    /// <param name="standardDisplayName">
    ///   Имя зимнего времени нового часового пояса.
    /// </param>
    /// <param name="daylightDisplayName">
    ///   Имя летнего времени нового часового пояса.
    /// </param>
    /// <param name="adjustmentRules">
    ///   Массив объектов <see cref="T:System.TimeZoneInfo.AdjustmentRule" />, которые прибавляют базовое смещение относительно UTC для определенного периода.
    /// </param>
    /// <param name="disableDaylightSavingTime">
    ///   Значение <see langword="true" /> для сброса в новом объекте всех связанных с летним временем сведений, представленных в параметре <paramref name="adjustmentRules" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Новый часовой пояс.
    ///    Если значение параметра <paramref name="disableDaylightSavingTime" /> равно <see langword="true" />, в возвращаемом объекте отсутствуют данные о летнем времени.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="id" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="id" /> представляет собой пустую строку ("").
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="baseUtcOffset" /> не представляет целое число минут.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="baseUtcOffset" /> больше 14 часов или меньше –14 часов.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Правила коррекции, заданные в параметре <paramref name="adjustmentRules" />, перекрываются.
    /// 
    ///   -или-
    /// 
    ///   Последовательность правил коррекции, заданных в параметре <paramref name="adjustmentRules" />, не является хронологической.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько элементов в <paramref name="adjustmentRules" /> имеют значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   К одной дате может быть применено несколько правил коррекции.
    /// 
    ///   -или-
    /// 
    ///   Сумма параметра <paramref name="baseUtcOffset" /> и значения свойства <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> одного или нескольких объектов в массиве <paramref name="adjustmentRules" /> больше 14 часов или меньше –14 часов.
    /// </exception>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        bool adjustmentRulesSupportDst;
        TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out adjustmentRulesSupportDst);
        if (adjustmentRulesSupportDst != this.m_supportsDaylightSavingTime)
          throw new SerializationException(Environment.GetResourceString("Serialization_CorruptField", (object) "SupportsDaylightSavingTime"));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
      catch (InvalidTimeZoneException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("Id", (object) this.m_id);
      info.AddValue("DisplayName", (object) this.m_displayName);
      info.AddValue("StandardName", (object) this.m_standardDisplayName);
      info.AddValue("DaylightName", (object) this.m_daylightDisplayName);
      info.AddValue("BaseUtcOffset", (object) this.m_baseUtcOffset);
      info.AddValue("AdjustmentRules", (object) this.m_adjustmentRules);
      info.AddValue("SupportsDaylightSavingTime", this.m_supportsDaylightSavingTime);
    }

    private TimeZoneInfo(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_id = (string) info.GetValue(nameof (Id), typeof (string));
      this.m_displayName = (string) info.GetValue(nameof (DisplayName), typeof (string));
      this.m_standardDisplayName = (string) info.GetValue(nameof (StandardName), typeof (string));
      this.m_daylightDisplayName = (string) info.GetValue(nameof (DaylightName), typeof (string));
      this.m_baseUtcOffset = (TimeSpan) info.GetValue(nameof (BaseUtcOffset), typeof (TimeSpan));
      this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[]) info.GetValue("AdjustmentRules", typeof (TimeZoneInfo.AdjustmentRule[]));
      this.m_supportsDaylightSavingTime = (bool) info.GetValue(nameof (SupportsDaylightSavingTime), typeof (bool));
    }

    private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime)
    {
      if (this.m_adjustmentRules == null || this.m_adjustmentRules.Length == 0)
        return (TimeZoneInfo.AdjustmentRule) null;
      DateTime date = dateTime.Date;
      for (int index = 0; index < this.m_adjustmentRules.Length; ++index)
      {
        if (this.m_adjustmentRules[index].DateStart <= date && this.m_adjustmentRules[index].DateEnd >= date)
          return this.m_adjustmentRules[index];
      }
      return (TimeZoneInfo.AdjustmentRule) null;
    }

    [SecurityCritical]
    private static bool CheckDaylightSavingTimeNotSupported(Win32Native.TimeZoneInformation timeZone)
    {
      if ((int) timeZone.DaylightDate.Year == (int) timeZone.StandardDate.Year && (int) timeZone.DaylightDate.Month == (int) timeZone.StandardDate.Month && ((int) timeZone.DaylightDate.DayOfWeek == (int) timeZone.StandardDate.DayOfWeek && (int) timeZone.DaylightDate.Day == (int) timeZone.StandardDate.Day) && ((int) timeZone.DaylightDate.Hour == (int) timeZone.StandardDate.Hour && (int) timeZone.DaylightDate.Minute == (int) timeZone.StandardDate.Minute && (int) timeZone.DaylightDate.Second == (int) timeZone.StandardDate.Second))
        return (int) timeZone.DaylightDate.Milliseconds == (int) timeZone.StandardDate.Milliseconds;
      return false;
    }

    private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
    {
      TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(ticks <= DateTime.MaxValue.Ticks ? (ticks >= DateTime.MinValue.Ticks ? new DateTime(ticks) : DateTime.MinValue) : DateTime.MaxValue, destinationTimeZone, out isAmbiguousLocalDst);
      ticks += utcOffsetFromUtc.Ticks;
      return ticks <= DateTime.MaxValue.Ticks ? (ticks >= DateTime.MinValue.Ticks ? new DateTime(ticks) : DateTime.MinValue) : DateTime.MaxValue;
    }

    [SecurityCritical]
    private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
    {
      if ((uint) timeZoneInformation.StandardDate.Month <= 0U)
      {
        if (timeZoneInformation.Bias == defaultBaseUtcOffset)
          return (TimeZoneInfo.AdjustmentRule) null;
        return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
      }
      TimeZoneInfo.TransitionTime transitionTime1;
      if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime1, true))
        return (TimeZoneInfo.AdjustmentRule) null;
      TimeZoneInfo.TransitionTime transitionTime2;
      if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime2, false))
        return (TimeZoneInfo.AdjustmentRule) null;
      if (transitionTime1.Equals(transitionTime2))
        return (TimeZoneInfo.AdjustmentRule) null;
      return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), transitionTime1, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
    }

    [SecuritySafeCritical]
    private static string FindIdFromTimeZoneInformation(Win32Native.TimeZoneInformation timeZone, out bool dstDisabled)
    {
      dstDisabled = false;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        permissionSet.AddPermission((IPermission) new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
        permissionSet.Assert();
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
            return (string) null;
          foreach (string subKeyName in registryKey.GetSubKeyNames())
          {
            if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, subKeyName, out dstDisabled))
              return subKeyName;
          }
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
      return (string) null;
    }

    private static DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule)
    {
      TimeSpan daylightDelta = rule.DaylightDelta;
      return new DaylightTimeStruct(TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart), TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd), daylightDelta);
    }

    private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
    {
      if (rule == null)
        return false;
      DateTime startTime;
      DateTime endTime;
      if (time.Kind == DateTimeKind.Local)
      {
        DateTime dateTime1;
        DateTime dateTime2;
        if (!rule.IsStartDateMarkerForBeginningOfYear())
        {
          dateTime2 = daylightTime.Start + daylightTime.Delta;
        }
        else
        {
          dateTime1 = daylightTime.Start;
          dateTime2 = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0);
        }
        startTime = dateTime2;
        DateTime dateTime3;
        if (!rule.IsEndDateMarkerForEndOfYear())
        {
          dateTime3 = daylightTime.End;
        }
        else
        {
          dateTime1 = daylightTime.End;
          dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
          dateTime3 = dateTime1.AddTicks(-1L);
        }
        endTime = dateTime3;
      }
      else
      {
        bool flag = rule.DaylightDelta > TimeSpan.Zero;
        DateTime dateTime1;
        DateTime dateTime2;
        if (!rule.IsStartDateMarkerForBeginningOfYear())
        {
          dateTime2 = daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero);
        }
        else
        {
          dateTime1 = daylightTime.Start;
          dateTime2 = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0);
        }
        startTime = dateTime2;
        DateTime dateTime3;
        if (!rule.IsEndDateMarkerForEndOfYear())
        {
          dateTime3 = daylightTime.End + (flag ? -rule.DaylightDelta : TimeSpan.Zero);
        }
        else
        {
          dateTime1 = daylightTime.End;
          dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
          dateTime3 = dateTime1.AddTicks(-1L);
        }
        endTime = dateTime3;
      }
      bool flag1 = TimeZoneInfo.CheckIsDst(startTime, time, endTime, false);
      if (flag1 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
        flag1 = time.IsAmbiguousDaylightSavingTime();
      return flag1;
    }

    private static bool GetIsDaylightSavingsFromUtc(DateTime time, int Year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
    {
      isAmbiguousLocalDst = false;
      if (rule == null)
        return false;
      TimeSpan timeSpan = utc + rule.BaseUtcOffsetDelta;
      DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(Year, rule);
      bool ignoreYearAdjustment = false;
      DateTime dateTime1;
      DateTime startTime;
      if (rule.IsStartDateMarkerForBeginningOfYear())
      {
        int year1 = daylightTime.Start.Year;
        dateTime1 = DateTime.MinValue;
        int year2 = dateTime1.Year;
        if (year1 > year2)
        {
          TimeZoneInfo timeZoneInfo = zone;
          dateTime1 = daylightTime.Start;
          DateTime dateTime2 = new DateTime(dateTime1.Year - 1, 12, 31);
          TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = timeZoneInfo.GetAdjustmentRuleForTime(dateTime2);
          if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
          {
            dateTime1 = daylightTime.Start;
            startTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year - 1, adjustmentRuleForTime).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
            ignoreYearAdjustment = true;
            goto label_8;
          }
          else
          {
            dateTime1 = daylightTime.Start;
            startTime = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0) - timeSpan;
            goto label_8;
          }
        }
      }
      startTime = daylightTime.Start - timeSpan;
label_8:
      DateTime endTime;
      if (rule.IsEndDateMarkerForEndOfYear())
      {
        dateTime1 = daylightTime.End;
        int year1 = dateTime1.Year;
        dateTime1 = DateTime.MaxValue;
        int year2 = dateTime1.Year;
        if (year1 < year2)
        {
          TimeZoneInfo timeZoneInfo = zone;
          dateTime1 = daylightTime.End;
          DateTime dateTime2 = new DateTime(dateTime1.Year + 1, 1, 1);
          TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = timeZoneInfo.GetAdjustmentRuleForTime(dateTime2);
          if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsStartDateMarkerForBeginningOfYear())
          {
            if (adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
            {
              dateTime1 = daylightTime.End;
              endTime = new DateTime(dateTime1.Year + 1, 12, 31) - utc - adjustmentRuleForTime.BaseUtcOffsetDelta - adjustmentRuleForTime.DaylightDelta;
            }
            else
            {
              dateTime1 = daylightTime.End;
              endTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year + 1, adjustmentRuleForTime).End - utc - adjustmentRuleForTime.BaseUtcOffsetDelta - adjustmentRuleForTime.DaylightDelta;
            }
            ignoreYearAdjustment = true;
            goto label_17;
          }
          else
          {
            dateTime1 = daylightTime.End;
            dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
            endTime = dateTime1.AddTicks(-1L) - timeSpan - rule.DaylightDelta;
            goto label_17;
          }
        }
      }
      endTime = daylightTime.End - timeSpan - rule.DaylightDelta;
label_17:
      DateTime dateTime3;
      DateTime dateTime4;
      if (daylightTime.Delta.Ticks > 0L)
      {
        dateTime3 = endTime - daylightTime.Delta;
        dateTime4 = endTime;
      }
      else
      {
        dateTime3 = startTime;
        dateTime4 = startTime - daylightTime.Delta;
      }
      bool flag = TimeZoneInfo.CheckIsDst(startTime, time, endTime, ignoreYearAdjustment);
      if (flag)
      {
        isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
        if (!isAmbiguousLocalDst)
        {
          if (dateTime3.Year != dateTime4.Year)
          {
            DateTime dateTime2;
            DateTime dateTime5;
            try
            {
              dateTime2 = dateTime3.AddYears(1);
              dateTime5 = dateTime4.AddYears(1);
              isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
            if (!isAmbiguousLocalDst)
            {
              try
              {
                dateTime2 = dateTime3.AddYears(-1);
                dateTime5 = dateTime4.AddYears(-1);
                isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
              }
              catch (ArgumentOutOfRangeException ex)
              {
              }
            }
          }
        }
      }
      return flag;
    }

    private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment)
    {
      if (!ignoreYearAdjustment)
      {
        int year1 = startTime.Year;
        int year2 = endTime.Year;
        if (year1 != year2)
          endTime = endTime.AddYears(year1 - year2);
        int year3 = time.Year;
        if (year1 != year3)
          time = time.AddYears(year1 - year3);
      }
      return !(startTime > endTime) ? time >= startTime && time < endTime : time < endTime || time >= startTime;
    }

    private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
    {
      bool flag1 = false;
      if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
        return flag1;
      DateTime dateTime1;
      DateTime dateTime2;
      if (rule.DaylightDelta > TimeSpan.Zero)
      {
        if (rule.IsEndDateMarkerForEndOfYear())
          return false;
        dateTime1 = daylightTime.End;
        dateTime2 = daylightTime.End - rule.DaylightDelta;
      }
      else
      {
        if (rule.IsStartDateMarkerForBeginningOfYear())
          return false;
        dateTime1 = daylightTime.Start;
        dateTime2 = daylightTime.Start + rule.DaylightDelta;
      }
      bool flag2 = time >= dateTime2 && time < dateTime1;
      if (!flag2)
      {
        if (dateTime1.Year != dateTime2.Year)
        {
          try
          {
            DateTime dateTime3 = dateTime1.AddYears(1);
            DateTime dateTime4 = dateTime2.AddYears(1);
            flag2 = time >= dateTime4 && time < dateTime3;
          }
          catch (ArgumentOutOfRangeException ex)
          {
          }
          if (!flag2)
          {
            try
            {
              DateTime dateTime3 = dateTime1.AddYears(-1);
              DateTime dateTime4 = dateTime2.AddYears(-1);
              flag2 = time >= dateTime4 && time < dateTime3;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      return flag2;
    }

    private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
    {
      bool flag1 = false;
      if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
        return flag1;
      DateTime dateTime1;
      DateTime dateTime2;
      if (rule.DaylightDelta < TimeSpan.Zero)
      {
        if (rule.IsEndDateMarkerForEndOfYear())
          return false;
        dateTime1 = daylightTime.End;
        dateTime2 = daylightTime.End - rule.DaylightDelta;
      }
      else
      {
        if (rule.IsStartDateMarkerForBeginningOfYear())
          return false;
        dateTime1 = daylightTime.Start;
        dateTime2 = daylightTime.Start + rule.DaylightDelta;
      }
      bool flag2 = time >= dateTime1 && time < dateTime2;
      if (!flag2)
      {
        if (dateTime1.Year != dateTime2.Year)
        {
          try
          {
            DateTime dateTime3 = dateTime1.AddYears(1);
            DateTime dateTime4 = dateTime2.AddYears(1);
            flag2 = time >= dateTime3 && time < dateTime4;
          }
          catch (ArgumentOutOfRangeException ex)
          {
          }
          if (!flag2)
          {
            try
            {
              DateTime dateTime3 = dateTime1.AddYears(-1);
              DateTime dateTime4 = dateTime2.AddYears(-1);
              flag2 = time >= dateTime3 && time < dateTime4;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      return flag2;
    }

    [SecuritySafeCritical]
    private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
    {
      Win32Native.DynamicTimeZoneInformation lpDynamicTimeZoneInformation = new Win32Native.DynamicTimeZoneInformation();
      if (UnsafeNativeMethods.GetDynamicTimeZoneInformation(out lpDynamicTimeZoneInformation) == -1)
        return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
      Win32Native.TimeZoneInformation timeZoneInformation1 = new Win32Native.TimeZoneInformation(lpDynamicTimeZoneInformation);
      bool dstDisabled = lpDynamicTimeZoneInformation.DynamicDaylightTimeDisabled;
      TimeZoneInfo timeZoneInfo1;
      Exception e1;
      if (!string.IsNullOrEmpty(lpDynamicTimeZoneInformation.TimeZoneKeyName) && TimeZoneInfo.TryGetTimeZone(lpDynamicTimeZoneInformation.TimeZoneKeyName, dstDisabled, out timeZoneInfo1, out e1, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
        return timeZoneInfo1;
      string timeZoneInformation2 = TimeZoneInfo.FindIdFromTimeZoneInformation(timeZoneInformation1, out dstDisabled);
      TimeZoneInfo timeZoneInfo2;
      Exception e2;
      if (timeZoneInformation2 != null && TimeZoneInfo.TryGetTimeZone(timeZoneInformation2, dstDisabled, out timeZoneInfo2, out e2, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
        return timeZoneInfo2;
      return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation1, dstDisabled);
    }

    [SecurityCritical]
    private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(Win32Native.TimeZoneInformation timeZoneInformation, bool dstDisabled)
    {
      try
      {
        return new TimeZoneInfo(timeZoneInformation, dstDisabled);
      }
      catch (ArgumentException ex)
      {
      }
      catch (InvalidTimeZoneException ex)
      {
      }
      if (!dstDisabled)
      {
        try
        {
          return new TimeZoneInfo(timeZoneInformation, true);
        }
        catch (ArgumentException ex)
        {
        }
        catch (InvalidTimeZoneException ex)
        {
        }
      }
      return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
    }

    /// <summary>
    ///   Извлекает объект <see cref="T:System.TimeZoneInfo" /> из реестра по его идентификатору.
    /// </summary>
    /// <param name="id">
    ///   Идентификатор часового пояса, соответствующий свойству <see cref="P:System.TimeZoneInfo.Id" />.
    /// </param>
    /// <returns>
    ///   Объект, идентификатор которого равен значению параметра <paramref name="id" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   В системе недостаточно памяти для хранения сведений о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="id" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    ///   Идентификатор часового пояса, указанный <paramref name="id" />, не найден.
    ///    Это означает, что раздел реестра, имя которого соответствует <paramref name="id" />, не существует, или что раздел существует, но не содержит данных о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Процесс не имеет разрешений, необходимых для чтения из раздела реестра, который содержит сведения о часовом поясе.
    /// </exception>
    /// <exception cref="T:System.InvalidTimeZoneException">
    ///   Идентификатор часового пояса найден, однако данные реестра повреждены.
    /// </exception>
    [__DynamicallyInvokable]
    public static TimeZoneInfo FindSystemTimeZoneById(string id)
    {
      if (string.Compare(id, "UTC", StringComparison.OrdinalIgnoreCase) == 0)
        return TimeZoneInfo.Utc;
      if (id == null)
        throw new ArgumentNullException(nameof (id));
      if (id.Length == 0 || id.Length > (int) byte.MaxValue || id.Contains("\0"))
        throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", (object) id));
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      TimeZoneInfo timeZoneInfo;
      Exception e;
      TimeZoneInfo.TimeZoneInfoResult timeZone;
      lock (cachedData)
        timeZone = TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out e, cachedData);
      switch (timeZone)
      {
        case TimeZoneInfo.TimeZoneInfoResult.Success:
          return timeZoneInfo;
        case TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException:
          throw new InvalidTimeZoneException(Environment.GetResourceString("InvalidTimeZone_InvalidRegistryData", (object) id), e);
        case TimeZoneInfo.TimeZoneInfoResult.SecurityException:
          throw new SecurityException(Environment.GetResourceString("Security_CannotReadRegistryData", (object) id), e);
        default:
          throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", (object) id), e);
      }
    }

    private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
    {
      TimeSpan baseUtcOffset = zone.BaseUtcOffset;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time);
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(time.Year, adjustmentRuleForTime);
          bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      return baseUtcOffset;
    }

    private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
    {
      bool isDaylightSavings;
      return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings);
    }

    private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
    {
      bool isAmbiguousLocalDst;
      return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out isAmbiguousLocalDst);
    }

    internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
    {
      isAmbiguousLocalDst = false;
      int year = time.Year;
      TimeZoneInfo.OffsetAndRule yearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
      TimeSpan offset = yearLocalFromUtc.offset;
      if (yearLocalFromUtc.rule != null)
      {
        offset += yearLocalFromUtc.rule.BaseUtcOffsetDelta;
        if (yearLocalFromUtc.rule.HasDaylightSaving)
        {
          bool daylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, yearLocalFromUtc.offset, yearLocalFromUtc.rule, out isAmbiguousLocalDst, TimeZoneInfo.Local);
          offset += daylightSavingsFromUtc ? yearLocalFromUtc.rule.DaylightDelta : TimeSpan.Zero;
        }
      }
      return offset;
    }

    internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
    {
      isDaylightSavings = false;
      isAmbiguousLocalDst = false;
      TimeSpan baseUtcOffset = zone.BaseUtcOffset;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime;
      int Year;
      if (time > TimeZoneInfo.s_maxDateOnly)
      {
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MaxValue);
        Year = 9999;
      }
      else if (time < TimeZoneInfo.s_minDateOnly)
      {
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MinValue);
        Year = 1;
      }
      else
      {
        DateTime dateTime = time + baseUtcOffset;
        Year = dateTime.Year;
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(dateTime);
      }
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, Year, zone.m_baseUtcOffset, adjustmentRuleForTime, out isAmbiguousLocalDst, zone);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      return baseUtcOffset;
    }

    [SecurityCritical]
    private static bool TransitionTimeFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
    {
      if ((uint) timeZoneInformation.StandardDate.Month <= 0U)
      {
        transitionTime = new TimeZoneInfo.TransitionTime();
        return false;
      }
      transitionTime = !readStartDate ? (timeZoneInformation.StandardDate.Year != (short) 0 ? TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.StandardDate.Hour, (int) timeZoneInformation.StandardDate.Minute, (int) timeZoneInformation.StandardDate.Second, (int) timeZoneInformation.StandardDate.Milliseconds), (int) timeZoneInformation.StandardDate.Month, (int) timeZoneInformation.StandardDate.Day) : TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.StandardDate.Hour, (int) timeZoneInformation.StandardDate.Minute, (int) timeZoneInformation.StandardDate.Second, (int) timeZoneInformation.StandardDate.Milliseconds), (int) timeZoneInformation.StandardDate.Month, (int) timeZoneInformation.StandardDate.Day, (DayOfWeek) timeZoneInformation.StandardDate.DayOfWeek)) : (timeZoneInformation.DaylightDate.Year != (short) 0 ? TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.DaylightDate.Hour, (int) timeZoneInformation.DaylightDate.Minute, (int) timeZoneInformation.DaylightDate.Second, (int) timeZoneInformation.DaylightDate.Milliseconds), (int) timeZoneInformation.DaylightDate.Month, (int) timeZoneInformation.DaylightDate.Day) : TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.DaylightDate.Hour, (int) timeZoneInformation.DaylightDate.Minute, (int) timeZoneInformation.DaylightDate.Second, (int) timeZoneInformation.DaylightDate.Milliseconds), (int) timeZoneInformation.DaylightDate.Month, (int) timeZoneInformation.DaylightDate.Day, (DayOfWeek) timeZoneInformation.DaylightDate.DayOfWeek));
      return true;
    }

    private static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
    {
      DateTime timeOfDay = transitionTime.TimeOfDay;
      DateTime dateTime;
      if (transitionTime.IsFixedDateRule)
      {
        int num = DateTime.DaysInMonth(year, transitionTime.Month);
        dateTime = new DateTime(year, transitionTime.Month, num < transitionTime.Day ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
      }
      else if (transitionTime.Week <= 4)
      {
        dateTime = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
        int dayOfWeek = (int) dateTime.DayOfWeek;
        int num1 = (int) (transitionTime.DayOfWeek - dayOfWeek);
        if (num1 < 0)
          num1 += 7;
        int num2 = num1 + 7 * (transitionTime.Week - 1);
        if (num2 > 0)
          dateTime = dateTime.AddDays((double) num2);
      }
      else
      {
        int day = DateTime.DaysInMonth(year, transitionTime.Month);
        dateTime = new DateTime(year, transitionTime.Month, day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
        int num = dateTime.DayOfWeek - transitionTime.DayOfWeek;
        if (num < 0)
          num += 7;
        if (num > 0)
          dateTime = dateTime.AddDays((double) -num);
      }
      return dateTime;
    }

    [SecurityCritical]
    private static bool TryCreateAdjustmentRules(string id, Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
    {
      e = (Exception) null;
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}\\Dynamic DST", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
          {
            Win32Native.RegistryTimeZoneInformation timeZoneInformation1 = defaultTimeZoneInformation;
            DateTime dateTime = DateTime.MinValue;
            DateTime date1 = dateTime.Date;
            dateTime = DateTime.MaxValue;
            DateTime date2 = dateTime.Date;
            int defaultBaseUtcOffset1 = defaultBaseUtcOffset;
            TimeZoneInfo.AdjustmentRule timeZoneInformation2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation1, date1, date2, defaultBaseUtcOffset1);
            if (timeZoneInformation2 == null)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
            }
            else
            {
              rules = new TimeZoneInfo.AdjustmentRule[1];
              rules[0] = timeZoneInformation2;
            }
            return true;
          }
          int year1 = (int) registryKey.GetValue("FirstEntry", (object) -1, RegistryValueOptions.None);
          int year2 = (int) registryKey.GetValue("LastEntry", (object) -1, RegistryValueOptions.None);
          if (year1 == -1 || year2 == -1 || year1 > year2)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          byte[] bytes1 = registryKey.GetValue(year1.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
          if (bytes1 == null || bytes1.Length != 44)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          Win32Native.RegistryTimeZoneInformation timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes1);
          if (year1 == year2)
          {
            Win32Native.RegistryTimeZoneInformation timeZoneInformation1 = timeZoneInformation3;
            DateTime dateTime = DateTime.MinValue;
            DateTime date1 = dateTime.Date;
            dateTime = DateTime.MaxValue;
            DateTime date2 = dateTime.Date;
            int defaultBaseUtcOffset1 = defaultBaseUtcOffset;
            TimeZoneInfo.AdjustmentRule timeZoneInformation2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation1, date1, date2, defaultBaseUtcOffset1);
            if (timeZoneInformation2 == null)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
            }
            else
            {
              rules = new TimeZoneInfo.AdjustmentRule[1];
              rules[0] = timeZoneInformation2;
            }
            return true;
          }
          List<TimeZoneInfo.AdjustmentRule> adjustmentRuleList = new List<TimeZoneInfo.AdjustmentRule>(1);
          TimeZoneInfo.AdjustmentRule timeZoneInformation4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, DateTime.MinValue.Date, new DateTime(year1, 12, 31), defaultBaseUtcOffset);
          if (timeZoneInformation4 != null)
            adjustmentRuleList.Add(timeZoneInformation4);
          for (int year3 = year1 + 1; year3 < year2; ++year3)
          {
            byte[] bytes2 = registryKey.GetValue(year3.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
            if (bytes2 == null || bytes2.Length != 44)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
              return false;
            }
            timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes2);
            TimeZoneInfo.AdjustmentRule timeZoneInformation1 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, new DateTime(year3, 1, 1), new DateTime(year3, 12, 31), defaultBaseUtcOffset);
            if (timeZoneInformation1 != null)
              adjustmentRuleList.Add(timeZoneInformation1);
          }
          byte[] bytes3 = registryKey.GetValue(year2.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
          timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes3);
          if (bytes3 == null || bytes3.Length != 44)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          TimeZoneInfo.AdjustmentRule timeZoneInformation5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, new DateTime(year2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
          if (timeZoneInformation5 != null)
            adjustmentRuleList.Add(timeZoneInformation5);
          rules = adjustmentRuleList.ToArray();
          if (rules != null)
          {
            if (rules.Length == 0)
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
          }
        }
      }
      catch (InvalidCastException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      catch (ArgumentException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      return true;
    }

    [SecurityCritical]
    private static bool TryCompareStandardDate(Win32Native.TimeZoneInformation timeZone, Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo)
    {
      if (timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias && ((int) timeZone.StandardDate.Year == (int) registryTimeZoneInfo.StandardDate.Year && (int) timeZone.StandardDate.Month == (int) registryTimeZoneInfo.StandardDate.Month) && ((int) timeZone.StandardDate.DayOfWeek == (int) registryTimeZoneInfo.StandardDate.DayOfWeek && (int) timeZone.StandardDate.Day == (int) registryTimeZoneInfo.StandardDate.Day && ((int) timeZone.StandardDate.Hour == (int) registryTimeZoneInfo.StandardDate.Hour && (int) timeZone.StandardDate.Minute == (int) registryTimeZoneInfo.StandardDate.Minute)) && (int) timeZone.StandardDate.Second == (int) registryTimeZoneInfo.StandardDate.Second)
        return (int) timeZone.StandardDate.Milliseconds == (int) registryTimeZoneInfo.StandardDate.Milliseconds;
      return false;
    }

    [SecuritySafeCritical]
    private static bool TryCompareTimeZoneInformationToRegistry(Win32Native.TimeZoneInformation timeZone, string id, out bool dstDisabled)
    {
      dstDisabled = false;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        permissionSet.AddPermission((IPermission) new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
        permissionSet.Assert();
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
            return false;
          byte[] bytes = (byte[]) registryKey.GetValue("TZI", (object) null, RegistryValueOptions.None);
          if (bytes == null || bytes.Length != 44)
            return false;
          Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo = new Win32Native.RegistryTimeZoneInformation(bytes);
          if (!TimeZoneInfo.TryCompareStandardDate(timeZone, registryTimeZoneInfo))
            return false;
          bool flag = dstDisabled || TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone) || timeZone.DaylightBias == registryTimeZoneInfo.DaylightBias && (int) timeZone.DaylightDate.Year == (int) registryTimeZoneInfo.DaylightDate.Year && ((int) timeZone.DaylightDate.Month == (int) registryTimeZoneInfo.DaylightDate.Month && (int) timeZone.DaylightDate.DayOfWeek == (int) registryTimeZoneInfo.DaylightDate.DayOfWeek) && ((int) timeZone.DaylightDate.Day == (int) registryTimeZoneInfo.DaylightDate.Day && (int) timeZone.DaylightDate.Hour == (int) registryTimeZoneInfo.DaylightDate.Hour && ((int) timeZone.DaylightDate.Minute == (int) registryTimeZoneInfo.DaylightDate.Minute && (int) timeZone.DaylightDate.Second == (int) registryTimeZoneInfo.DaylightDate.Second)) && (int) timeZone.DaylightDate.Milliseconds == (int) registryTimeZoneInfo.DaylightDate.Milliseconds;
          if (flag)
            flag = string.Compare(registryKey.GetValue("Std", (object) string.Empty, RegistryValueOptions.None) as string, timeZone.StandardName, StringComparison.Ordinal) == 0;
          return flag;
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
    }

    [SecuritySafeCritical]
    [FileIOPermission(SecurityAction.Assert, AllLocalFiles = FileIOPermissionAccess.PathDiscovery)]
    private static string TryGetLocalizedNameByMuiNativeResource(string resource)
    {
      if (string.IsNullOrEmpty(resource))
        return string.Empty;
      string[] strArray = resource.Split(new char[1]{ ',' }, StringSplitOptions.None);
      if (strArray.Length != 2)
        return string.Empty;
      string folderPath = Environment.UnsafeGetFolderPath(Environment.SpecialFolder.System);
      string path2 = strArray[0].TrimStart('@');
      string filePath;
      try
      {
        filePath = Path.Combine(folderPath, path2);
      }
      catch (ArgumentException ex)
      {
        return string.Empty;
      }
      int result;
      if (!int.TryParse(strArray[1], NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return string.Empty;
      result = -result;
      try
      {
        StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
        stringBuilder.Length = 260;
        int fileMuiPathLength = 260;
        int languageLength = 0;
        long enumerator = 0;
        if (UnsafeNativeMethods.GetFileMUIPath(16, filePath, (StringBuilder) null, ref languageLength, stringBuilder, ref fileMuiPathLength, ref enumerator))
          return TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), result);
        StringBuilderCache.Release(stringBuilder);
        return string.Empty;
      }
      catch (EntryPointNotFoundException ex)
      {
        return string.Empty;
      }
    }

    [SecurityCritical]
    private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
    {
      using (SafeLibraryHandle handle = UnsafeNativeMethods.LoadLibraryEx(filePath, IntPtr.Zero, 2))
      {
        if (!handle.IsInvalid)
        {
          StringBuilder stringBuilder = StringBuilderCache.Acquire(500);
          stringBuilder.Length = 500;
          if (UnsafeNativeMethods.LoadString(handle, resource, stringBuilder, stringBuilder.Length) != 0)
            return StringBuilderCache.GetStringAndRelease(stringBuilder);
        }
      }
      return string.Empty;
    }

    private static bool TryGetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
    {
      displayName = string.Empty;
      standardName = string.Empty;
      daylightName = string.Empty;
      string resource1 = key.GetValue("MUI_Display", (object) string.Empty, RegistryValueOptions.None) as string;
      string resource2 = key.GetValue("MUI_Std", (object) string.Empty, RegistryValueOptions.None) as string;
      string resource3 = key.GetValue("MUI_Dlt", (object) string.Empty, RegistryValueOptions.None) as string;
      if (!string.IsNullOrEmpty(resource1))
        displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource1);
      if (!string.IsNullOrEmpty(resource2))
        standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource2);
      if (!string.IsNullOrEmpty(resource3))
        daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource3);
      if (string.IsNullOrEmpty(displayName))
        displayName = key.GetValue("Display", (object) string.Empty, RegistryValueOptions.None) as string;
      if (string.IsNullOrEmpty(standardName))
        standardName = key.GetValue("Std", (object) string.Empty, RegistryValueOptions.None) as string;
      if (string.IsNullOrEmpty(daylightName))
        daylightName = key.GetValue("Dlt", (object) string.Empty, RegistryValueOptions.None) as string;
      return true;
    }

    [SecuritySafeCritical]
    private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneByRegistryKey(string id, out TimeZoneInfo value, out Exception e)
    {
      e = (Exception) null;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        permissionSet.AddPermission((IPermission) new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
        permissionSet.Assert();
        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (key == null)
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
          }
          byte[] bytes = key.GetValue("TZI", (object) null, RegistryValueOptions.None) as byte[];
          if (bytes == null || bytes.Length != 44)
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(bytes);
          TimeZoneInfo.AdjustmentRule[] rules;
          if (!TimeZoneInfo.TryCreateAdjustmentRules(id, defaultTimeZoneInformation, out rules, out e, defaultTimeZoneInformation.Bias))
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          string displayName;
          string standardName;
          string daylightName;
          if (!TimeZoneInfo.TryGetLocalizedNamesByRegistryKey(key, out displayName, out standardName, out daylightName))
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          try
          {
            value = new TimeZoneInfo(id, new TimeSpan(0, -defaultTimeZoneInformation.Bias, 0), displayName, standardName, daylightName, rules, false);
            return TimeZoneInfo.TimeZoneInfoResult.Success;
          }
          catch (ArgumentException ex)
          {
            value = (TimeZoneInfo) null;
            e = (Exception) ex;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          catch (InvalidTimeZoneException ex)
          {
            value = (TimeZoneInfo) null;
            e = (Exception) ex;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
    }

    private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
    {
      TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult1 = TimeZoneInfo.TimeZoneInfoResult.Success;
      e = (Exception) null;
      TimeZoneInfo timeZoneInfo = (TimeZoneInfo) null;
      if (cachedData.m_systemTimeZones != null && cachedData.m_systemTimeZones.TryGetValue(id, out timeZoneInfo))
      {
        value = !dstDisabled || !timeZoneInfo.m_supportsDaylightSavingTime ? new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false) : TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
        return timeZoneInfoResult1;
      }
      TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult2;
      if (!cachedData.m_allSystemTimeZonesRead)
      {
        timeZoneInfoResult2 = TimeZoneInfo.TryGetTimeZoneByRegistryKey(id, out timeZoneInfo, out e);
        if (timeZoneInfoResult2 == TimeZoneInfo.TimeZoneInfoResult.Success)
        {
          if (cachedData.m_systemTimeZones == null)
            cachedData.m_systemTimeZones = new Dictionary<string, TimeZoneInfo>();
          cachedData.m_systemTimeZones.Add(id, timeZoneInfo);
          value = !dstDisabled || !timeZoneInfo.m_supportsDaylightSavingTime ? new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false) : TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
        }
        else
          value = (TimeZoneInfo) null;
      }
      else
      {
        timeZoneInfoResult2 = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
        value = (TimeZoneInfo) null;
      }
      return timeZoneInfoResult2;
    }

    internal static bool UtcOffsetOutOfRange(TimeSpan offset)
    {
      if (offset.TotalHours >= -14.0)
        return offset.TotalHours > 14.0;
      return true;
    }

    private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
    {
      if (id == null)
        throw new ArgumentNullException(nameof (id));
      if (id.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidId", (object) id), nameof (id));
      if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
        throw new ArgumentOutOfRangeException(nameof (baseUtcOffset), Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
      if (baseUtcOffset.Ticks % 600000000L != 0L)
        throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), nameof (baseUtcOffset));
      adjustmentRulesSupportDst = false;
      if (adjustmentRules == null || adjustmentRules.Length == 0)
        return;
      adjustmentRulesSupportDst = true;
      TimeZoneInfo.AdjustmentRule adjustmentRule1 = (TimeZoneInfo.AdjustmentRule) null;
      for (int index = 0; index < adjustmentRules.Length; ++index)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule1;
        adjustmentRule1 = adjustmentRules[index];
        if (adjustmentRule1 == null)
          throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesNoNulls"));
        if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule1.DaylightDelta))
          throw new InvalidTimeZoneException(Environment.GetResourceString("ArgumentOutOfRange_UtcOffsetAndDaylightDelta"));
        if (adjustmentRule2 != null && adjustmentRule1.DateStart <= adjustmentRule2.DateEnd)
          throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesOutOfOrder"));
      }
    }

    private enum TimeZoneInfoResult
    {
      Success,
      TimeZoneNotFoundException,
      InvalidTimeZoneException,
      SecurityException,
    }

    private class CachedData
    {
      private volatile TimeZoneInfo m_localTimeZone;
      private volatile TimeZoneInfo m_utcTimeZone;
      public Dictionary<string, TimeZoneInfo> m_systemTimeZones;
      public ReadOnlyCollection<TimeZoneInfo> m_readOnlySystemTimeZones;
      public bool m_allSystemTimeZonesRead;
      private volatile TimeZoneInfo.OffsetAndRule m_oneYearLocalFromUtc;

      private TimeZoneInfo CreateLocal()
      {
        lock (this)
        {
          TimeZoneInfo timeZoneInfo = this.m_localTimeZone;
          if (timeZoneInfo == null)
          {
            TimeZoneInfo localTimeZone = TimeZoneInfo.GetLocalTimeZone(this);
            timeZoneInfo = new TimeZoneInfo(localTimeZone.m_id, localTimeZone.m_baseUtcOffset, localTimeZone.m_displayName, localTimeZone.m_standardDisplayName, localTimeZone.m_daylightDisplayName, localTimeZone.m_adjustmentRules, false);
            this.m_localTimeZone = timeZoneInfo;
          }
          return timeZoneInfo;
        }
      }

      public TimeZoneInfo Local
      {
        get
        {
          return this.m_localTimeZone ?? this.CreateLocal();
        }
      }

      private TimeZoneInfo CreateUtc()
      {
        lock (this)
        {
          TimeZoneInfo timeZoneInfo = this.m_utcTimeZone;
          if (timeZoneInfo == null)
          {
            timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");
            this.m_utcTimeZone = timeZoneInfo;
          }
          return timeZoneInfo;
        }
      }

      public TimeZoneInfo Utc
      {
        get
        {
          return this.m_utcTimeZone ?? this.CreateUtc();
        }
      }

      public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
      {
        return timeZone != this.m_utcTimeZone ? (timeZone != this.m_localTimeZone ? DateTimeKind.Unspecified : DateTimeKind.Local) : DateTimeKind.Utc;
      }

      [SecuritySafeCritical]
      private static TimeZoneInfo GetCurrentOneYearLocal()
      {
        Win32Native.TimeZoneInformation lpTimeZoneInformation = new Win32Native.TimeZoneInformation();
        return UnsafeNativeMethods.GetTimeZoneInformation(out lpTimeZoneInformation) != -1 ? TimeZoneInfo.GetLocalTimeZoneFromWin32Data(lpTimeZoneInformation, false) : TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
      }

      public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
      {
        TimeZoneInfo.OffsetAndRule offsetAndRule = this.m_oneYearLocalFromUtc;
        if (offsetAndRule == null || offsetAndRule.year != year)
        {
          TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
          TimeZoneInfo.AdjustmentRule rule = currentOneYearLocal.m_adjustmentRules == null ? (TimeZoneInfo.AdjustmentRule) null : currentOneYearLocal.m_adjustmentRules[0];
          offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, rule);
          this.m_oneYearLocalFromUtc = offsetAndRule;
        }
        return offsetAndRule;
      }
    }

    private class OffsetAndRule
    {
      public int year;
      public TimeSpan offset;
      public TimeZoneInfo.AdjustmentRule rule;

      public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
      {
        this.year = year;
        this.offset = offset;
        this.rule = rule;
      }
    }

    /// <summary>
    ///   Предоставляет сведения о корректировке часового пояса, например о переходе на летнее время и обратно.
    /// </summary>
    [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
    {
      private DateTime m_dateStart;
      private DateTime m_dateEnd;
      private TimeSpan m_daylightDelta;
      private TimeZoneInfo.TransitionTime m_daylightTransitionStart;
      private TimeZoneInfo.TransitionTime m_daylightTransitionEnd;
      private TimeSpan m_baseUtcOffsetDelta;

      /// <summary>
      ///   Возвращает дату вступления в силу правила коррекции.
      /// </summary>
      /// <returns>
      ///   Значение <see cref="T:System.DateTime" />, указывающее время вступления в силу правила коррекции.
      /// </returns>
      public DateTime DateStart
      {
        get
        {
          return this.m_dateStart;
        }
      }

      /// <summary>
      ///   Возвращает дату, когда правило коррекции перестает быть действительным.
      /// </summary>
      /// <returns>
      ///   Значение <see cref="T:System.DateTime" />, указывающее конечную дату правила коррекции.
      /// </returns>
      public DateTime DateEnd
      {
        get
        {
          return this.m_dateEnd;
        }
      }

      /// <summary>
      ///   Получает время, необходимое для формирования летнего времени часового пояса.
      ///    Это количество времени добавляется к смещению часового пояса от времени в формате UTC.
      /// </summary>
      /// <returns>
      ///   Объект <see cref="T:System.TimeSpan" />, показывающий количество времени, добавляемого к стандартному изменению времени в результате правила коррекции.
      /// </returns>
      public TimeSpan DaylightDelta
      {
        get
        {
          return this.m_daylightDelta;
        }
      }

      /// <summary>
      ///   Получает сведения о ежегодном переходе со стандартного времени на летнее время.
      /// </summary>
      /// <returns>
      ///   Объект <see cref="T:System.TimeZoneInfo.TransitionTime" />, определяющий ежегодный переход со стандартного времени часового пояса на летнее время.
      /// </returns>
      public TimeZoneInfo.TransitionTime DaylightTransitionStart
      {
        get
        {
          return this.m_daylightTransitionStart;
        }
      }

      /// <summary>
      ///   Получает сведения о ежегодном переходе с летнего времени обратно на стандартное время.
      /// </summary>
      /// <returns>
      ///   Объект <see cref="T:System.TimeZoneInfo.TransitionTime" />, определяющий ежегодный переход с летнего времени обратно на стандартное время часового пояса.
      /// </returns>
      public TimeZoneInfo.TransitionTime DaylightTransitionEnd
      {
        get
        {
          return this.m_daylightTransitionEnd;
        }
      }

      internal TimeSpan BaseUtcOffsetDelta
      {
        get
        {
          return this.m_baseUtcOffsetDelta;
        }
      }

      internal bool HasDaylightSaving
      {
        get
        {
          if (!(this.DaylightDelta != TimeSpan.Zero) && !(this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue))
            return this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0);
          return true;
        }
      }

      /// <summary>
      ///   Определяет, равен ли текущий объект <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> второму объекту <see cref="T:System.TimeZoneInfo.AdjustmentRule" />.
      /// </summary>
      /// <param name="other">
      ///   Объект, который требуется сравнить с текущим объектом.
      /// </param>
      /// <returns>
      ///   Значение <see langword="true" />, если оба объекта <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> имеют одинаковые значения; в противном случае — значение <see langword="false" />.
      /// </returns>
      public bool Equals(TimeZoneInfo.AdjustmentRule other)
      {
        return other != null && this.m_dateStart == other.m_dateStart && (this.m_dateEnd == other.m_dateEnd && this.m_daylightDelta == other.m_daylightDelta) && this.m_baseUtcOffsetDelta == other.m_baseUtcOffsetDelta && this.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd) && this.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart);
      }

      /// <summary>
      ///   Служит хэш-функцией для алгоритмов хэширования и таких структур данных, как хэш-таблицы.
      /// </summary>
      /// <returns>
      ///   32-разрядное целое число со знаком, выступающее в роли хэш-кода для текущего объекта <see cref="T:System.TimeZoneInfo.AdjustmentRule" />.
      /// </returns>
      public override int GetHashCode()
      {
        return this.m_dateStart.GetHashCode();
      }

      private AdjustmentRule()
      {
      }

      /// <summary>
      ///   Создает новое правило коррекции для определенного часового пояса.
      /// </summary>
      /// <param name="dateStart">
      ///   Дата начала действия правила коррекции.
      ///    Если параметр <paramref name="dateStart" /> имеет значение <see langword="DateTime.MinValue.Date" />, то это первое правило коррекции, действующее для часового пояса.
      /// </param>
      /// <param name="dateEnd">
      ///   Последняя дата действия правила коррекции.
      ///    Если параметр <paramref name="dateEnd" /> имеет значение <see langword="DateTime.MaxValue.Date" />, то данное правило коррекции не имеет даты окончания.
      /// </param>
      /// <param name="daylightDelta">
      ///   Изменение времени в результате коррекции.
      ///    Это значение добавляется к свойству <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> часового пояса для получения правильного смещения летнего времени от времени в формате UTC (UTC).
      ///    Это значение может быть в диапазоне от -14 до 14.
      /// </param>
      /// <param name="daylightTransitionStart">
      ///   Объект, определяющий начало летнего времени.
      /// </param>
      /// <param name="daylightTransitionEnd">
      ///   Объект, определяющий окончание летнего времени.
      /// </param>
      /// <returns>Объект, представляющий новое правило коррекции.</returns>
      /// <exception cref="T:System.ArgumentException">
      ///   Свойство <see cref="P:System.DateTime.Kind" /> параметра <paramref name="dateStart" /> или <paramref name="dateEnd" /> не равно <see cref="F:System.DateTimeKind.Unspecified" />.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="daylightTransitionStart" /> равен параметру <paramref name="daylightTransitionEnd" />.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="dateStart" /> или <paramref name="dateEnd" /> включает значение времени суток.
      /// </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///   Момент, указанный в параметре <paramref name="dateEnd" />, наступает раньше, чем момент, указанный в параметре <paramref name="dateStart" />.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="daylightDelta" /> меньше –14 или больше 14.
      /// 
      ///   -или-
      /// 
      ///   Свойство <see cref="P:System.TimeSpan.Milliseconds" /> параметра <paramref name="daylightDelta" /> не равно 0.
      /// 
      ///   -или-
      /// 
      ///   Свойство <see cref="P:System.TimeSpan.Ticks" /> параметра <paramref name="daylightDelta" /> не равно целому числу секунд.
      /// </exception>
      public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
      {
        TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
        return new TimeZoneInfo.AdjustmentRule()
        {
          m_dateStart = dateStart,
          m_dateEnd = dateEnd,
          m_daylightDelta = daylightDelta,
          m_daylightTransitionStart = daylightTransitionStart,
          m_daylightTransitionEnd = daylightTransitionEnd,
          m_baseUtcOffsetDelta = TimeSpan.Zero
        };
      }

      internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
        adjustmentRule.m_baseUtcOffsetDelta = baseUtcOffsetDelta;
        return adjustmentRule;
      }

      internal bool IsStartDateMarkerForBeginningOfYear()
      {
        if (this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1)
        {
          DateTime timeOfDay = this.DaylightTransitionStart.TimeOfDay;
          if (timeOfDay.Hour == 0)
          {
            timeOfDay = this.DaylightTransitionStart.TimeOfDay;
            if (timeOfDay.Minute == 0)
            {
              timeOfDay = this.DaylightTransitionStart.TimeOfDay;
              if (timeOfDay.Second == 0)
                return this.m_dateStart.Year == this.m_dateEnd.Year;
            }
          }
        }
        return false;
      }

      internal bool IsEndDateMarkerForEndOfYear()
      {
        if (this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1)
        {
          DateTime timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
          if (timeOfDay.Hour == 0)
          {
            timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
            if (timeOfDay.Minute == 0)
            {
              timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
              if (timeOfDay.Second == 0)
                return this.m_dateStart.Year == this.m_dateEnd.Year;
            }
          }
        }
        return false;
      }

      private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
      {
        if (dateStart.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), nameof (dateStart));
        if (dateEnd.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), nameof (dateEnd));
        if (daylightTransitionStart.Equals(daylightTransitionEnd))
          throw new ArgumentException(Environment.GetResourceString("Argument_TransitionTimesAreIdentical"), nameof (daylightTransitionEnd));
        if (dateStart > dateEnd)
          throw new ArgumentException(Environment.GetResourceString("Argument_OutOfOrderDateTimes"), nameof (dateStart));
        if (TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta))
          throw new ArgumentOutOfRangeException(nameof (daylightDelta), (object) daylightDelta, Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
        if (daylightDelta.Ticks % 600000000L != 0L)
          throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), nameof (daylightDelta));
        if (dateStart.TimeOfDay != TimeSpan.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), nameof (dateStart));
        if (dateEnd.TimeOfDay != TimeSpan.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), nameof (dateEnd));
      }

      void IDeserializationCallback.OnDeserialization(object sender)
      {
        try
        {
          TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this.m_dateStart, this.m_dateEnd, this.m_daylightDelta, this.m_daylightTransitionStart, this.m_daylightTransitionEnd);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("DateStart", this.m_dateStart);
        info.AddValue("DateEnd", this.m_dateEnd);
        info.AddValue("DaylightDelta", (object) this.m_daylightDelta);
        info.AddValue("DaylightTransitionStart", (object) this.m_daylightTransitionStart);
        info.AddValue("DaylightTransitionEnd", (object) this.m_daylightTransitionEnd);
        info.AddValue("BaseUtcOffsetDelta", (object) this.m_baseUtcOffsetDelta);
      }

      private AdjustmentRule(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_dateStart = (DateTime) info.GetValue(nameof (DateStart), typeof (DateTime));
        this.m_dateEnd = (DateTime) info.GetValue(nameof (DateEnd), typeof (DateTime));
        this.m_daylightDelta = (TimeSpan) info.GetValue(nameof (DaylightDelta), typeof (TimeSpan));
        this.m_daylightTransitionStart = (TimeZoneInfo.TransitionTime) info.GetValue(nameof (DaylightTransitionStart), typeof (TimeZoneInfo.TransitionTime));
        this.m_daylightTransitionEnd = (TimeZoneInfo.TransitionTime) info.GetValue(nameof (DaylightTransitionEnd), typeof (TimeZoneInfo.TransitionTime));
        object valueNoThrow = info.GetValueNoThrow(nameof (BaseUtcOffsetDelta), typeof (TimeSpan));
        if (valueNoThrow == null)
          return;
        this.m_baseUtcOffsetDelta = (TimeSpan) valueNoThrow;
      }
    }

    /// <summary>
    ///   Предоставляет данные о конкретном изменении времени, например переходе с летнего времени на зимнее или наоборот, в заданном часовом поясе.
    /// </summary>
    [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
    {
      private DateTime m_timeOfDay;
      private byte m_month;
      private byte m_week;
      private byte m_day;
      private DayOfWeek m_dayOfWeek;
      private bool m_isFixedDateRule;

      /// <summary>
      ///   Возвращает час, минуту и секунду, когда происходит изменение времени.
      /// </summary>
      /// <returns>Время суток, когда происходит изменение времени.</returns>
      public DateTime TimeOfDay
      {
        get
        {
          return this.m_timeOfDay;
        }
      }

      /// <summary>
      ///   Возвращает месяц, когда происходит изменение времени.
      /// </summary>
      /// <returns>Месяц, когда происходит изменение времени.</returns>
      public int Month
      {
        get
        {
          return (int) this.m_month;
        }
      }

      /// <summary>
      ///   Возвращает неделю месяца, в которую происходит изменение времени.
      /// </summary>
      /// <returns>
      ///   Неделя месяца, в которую происходит изменение времени.
      /// </returns>
      public int Week
      {
        get
        {
          return (int) this.m_week;
        }
      }

      /// <summary>
      ///   Получает день, в который происходит изменение времени.
      /// </summary>
      /// <returns>День, в который происходит изменение времени.</returns>
      public int Day
      {
        get
        {
          return (int) this.m_day;
        }
      }

      /// <summary>
      ///   Получает день недели, в который происходит изменение времени.
      /// </summary>
      /// <returns>
      ///   День недели, в который происходит изменение времени.
      /// </returns>
      public DayOfWeek DayOfWeek
      {
        get
        {
          return this.m_dayOfWeek;
        }
      }

      /// <summary>
      ///   Возвращает значение, указывающее, происходит ли изменение времени в фиксированную дату и время (например, 1 ноября) или нефиксированную (например, в последнее воскресенье октября).
      /// </summary>
      /// <returns>
      ///   Значение <see langword="true" />, если изменение времени происходит в фиксированный момент; значение <see langword="false" />, если изменение времени происходит в нефиксированный момент.
      /// </returns>
      public bool IsFixedDateRule
      {
        get
        {
          return this.m_isFixedDateRule;
        }
      }

      /// <summary>
      ///   Определяет, имеет ли объект одинаковые значения с текущим объектом <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </summary>
      /// <param name="obj">
      ///   Объект, сравниваемый с текущим объектом <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </param>
      /// <returns>
      ///   Значение <see langword="true" />, если эти два объекта равны; в противном случае — значение <see langword="false" />.
      /// </returns>
      public override bool Equals(object obj)
      {
        if (obj is TimeZoneInfo.TransitionTime)
          return this.Equals((TimeZoneInfo.TransitionTime) obj);
        return false;
      }

      /// <summary>
      ///   Определение равенства двух заданных объектов <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </summary>
      /// <param name="t1">Первый из сравниваемых объектов.</param>
      /// <param name="t2">Второй из сравниваемых объектов.</param>
      /// <returns>
      ///   Значение <see langword="true" />, если <paramref name="t1" /> и <paramref name="t2" /> имеют одинаковые значения; в противном случае значение <see langword="false" />.
      /// </returns>
      public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
      {
        return t1.Equals(t2);
      }

      /// <summary>
      ///   Определение неравенства двух заданных объектов <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </summary>
      /// <param name="t1">Первый из сравниваемых объектов.</param>
      /// <param name="t2">Второй из сравниваемых объектов.</param>
      /// <returns>
      ///   Значение <see langword="true" />, если <paramref name="t1" /> и <paramref name="t2" /> содержат разные значения членов; в противном случае — значение <see langword="false" />.
      /// </returns>
      public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
      {
        return !t1.Equals(t2);
      }

      /// <summary>
      ///   Определяет, имеет ли текущий объект <see cref="T:System.TimeZoneInfo.TransitionTime" /> одинаковые значения со вторым объектом <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </summary>
      /// <param name="other">
      ///   Объект, сравниваемый с текущим экземпляром.
      /// </param>
      /// <returns>
      ///   Значения <see langword="true" />, если оба объекта имеют одинаковые значения свойств; в противном случае значения <see langword="false" />.
      /// </returns>
      public bool Equals(TimeZoneInfo.TransitionTime other)
      {
        bool flag = this.m_isFixedDateRule == other.m_isFixedDateRule && this.m_timeOfDay == other.m_timeOfDay && (int) this.m_month == (int) other.m_month;
        if (flag)
          flag = !other.m_isFixedDateRule ? (int) this.m_week == (int) other.m_week && this.m_dayOfWeek == other.m_dayOfWeek : (int) this.m_day == (int) other.m_day;
        return flag;
      }

      /// <summary>
      ///   Служит хэш-функцией для алгоритмов хэширования и таких структур данных, как хэш-таблицы.
      /// </summary>
      /// <returns>
      ///   32-битовое целое число со знаком, выступающее в роли хэш-кода данного объекта <see cref="T:System.TimeZoneInfo.TransitionTime" />.
      /// </returns>
      public override int GetHashCode()
      {
        return (int) this.m_month ^ (int) this.m_week << 8;
      }

      /// <summary>
      ///   Определяет изменение времени, производимое по правилу с фиксированной датой (то есть в указанный день определенного месяца).
      /// </summary>
      /// <param name="timeOfDay">
      ///   Время, когда происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" />.
      ///    Дополнительные сведения см. в разделе "Заметки".
      /// </param>
      /// <param name="month">
      ///   Месяц, когда происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.Month" />.
      /// </param>
      /// <param name="day">
      ///   День месяца, в который происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.Day" />.
      /// </param>
      /// <returns>Сведения об изменении времени.</returns>
      /// <exception cref="T:System.ArgumentException">
      ///   Параметр <paramref name="timeOfDay" /> имеет компонент даты, не являющийся используемым по умолчанию.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="timeOfDay" /> имеет свойство <see cref="P:System.DateTime.Kind" />, не равное <see cref="F:System.DateTimeKind.Unspecified" />.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="timeOfDay" /> не представляет целое число миллисекунд.
      /// </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="day" /> имеет значение меньше 1 или больше 31.
      /// </exception>
      public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
      {
        return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
      }

      /// <summary>
      ///   Определяет изменение времени, производимое по правилу с плавающей датой (то есть изменение времени, происходящее в указанный день указанной недели определенного месяца).
      /// </summary>
      /// <param name="timeOfDay">
      ///   Время, когда происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" />.
      ///    Дополнительные сведения см. в разделе "Заметки".
      /// </param>
      /// <param name="month">
      ///   Месяц, когда происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.Month" />.
      /// </param>
      /// <param name="week">
      ///   Неделя месяца, в которую происходит изменение времени.
      ///    Это значение находится в диапазоне от 1 до 5, где 5 представляет последнюю неделю месяца.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.Week" />.
      /// </param>
      /// <param name="dayOfWeek">
      ///   День недели, в который происходит изменение времени.
      ///    Этот параметр соответствует свойству <see cref="P:System.TimeZoneInfo.TransitionTime.DayOfWeek" />.
      /// </param>
      /// <returns>Сведения об изменении времени.</returns>
      /// <exception cref="T:System.ArgumentException">
      ///   Параметр <paramref name="timeOfDay" /> имеет компонент даты, не являющийся используемым по умолчанию.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="timeOfDay" /> не представляет целое число миллисекунд.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="timeOfDay" /> имеет свойство <see cref="P:System.DateTime.Kind" />, не равное <see cref="F:System.DateTimeKind.Unspecified" />.
      /// </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///   Параметр <paramref name="month" /> имеет значение меньше 1 или больше 12.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="week" /> имеет значение меньше 1 или больше 5.
      /// 
      ///   -или-
      /// 
      ///   Параметр <paramref name="dayOfWeek" /> не является членом перечисления <see cref="T:System.DayOfWeek" />.
      /// </exception>
      public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
      {
        return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
      }

      private static TimeZoneInfo.TransitionTime CreateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
      {
        TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
        return new TimeZoneInfo.TransitionTime()
        {
          m_isFixedDateRule = isFixedDateRule,
          m_timeOfDay = timeOfDay,
          m_dayOfWeek = dayOfWeek,
          m_day = (byte) day,
          m_week = (byte) week,
          m_month = (byte) month
        };
      }

      private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
      {
        if (timeOfDay.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), nameof (timeOfDay));
        if (month < 1 || month > 12)
          throw new ArgumentOutOfRangeException(nameof (month), Environment.GetResourceString("ArgumentOutOfRange_MonthParam"));
        if (day < 1 || day > 31)
          throw new ArgumentOutOfRangeException(nameof (day), Environment.GetResourceString("ArgumentOutOfRange_DayParam"));
        if (week < 1 || week > 5)
          throw new ArgumentOutOfRangeException(nameof (week), Environment.GetResourceString("ArgumentOutOfRange_Week"));
        switch (dayOfWeek)
        {
          case DayOfWeek.Sunday:
          case DayOfWeek.Monday:
          case DayOfWeek.Tuesday:
          case DayOfWeek.Wednesday:
          case DayOfWeek.Thursday:
          case DayOfWeek.Friday:
          case DayOfWeek.Saturday:
            int year;
            int month1;
            int day1;
            timeOfDay.GetDatePart(out year, out month1, out day1);
            if (year == 1 && month1 == 1 && (day1 == 1 && timeOfDay.Ticks % 10000L == 0L))
              break;
            throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTicks"), nameof (timeOfDay));
          default:
            throw new ArgumentOutOfRangeException(nameof (dayOfWeek), Environment.GetResourceString("ArgumentOutOfRange_DayOfWeek"));
        }
      }

      void IDeserializationCallback.OnDeserialization(object sender)
      {
        try
        {
          TimeZoneInfo.TransitionTime.ValidateTransitionTime(this.m_timeOfDay, (int) this.m_month, (int) this.m_week, (int) this.m_day, this.m_dayOfWeek);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("TimeOfDay", this.m_timeOfDay);
        info.AddValue("Month", this.m_month);
        info.AddValue("Week", this.m_week);
        info.AddValue("Day", this.m_day);
        info.AddValue("DayOfWeek", (object) this.m_dayOfWeek);
        info.AddValue("IsFixedDateRule", this.m_isFixedDateRule);
      }

      private TransitionTime(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_timeOfDay = (DateTime) info.GetValue(nameof (TimeOfDay), typeof (DateTime));
        this.m_month = (byte) info.GetValue(nameof (Month), typeof (byte));
        this.m_week = (byte) info.GetValue(nameof (Week), typeof (byte));
        this.m_day = (byte) info.GetValue(nameof (Day), typeof (byte));
        this.m_dayOfWeek = (DayOfWeek) info.GetValue(nameof (DayOfWeek), typeof (DayOfWeek));
        this.m_isFixedDateRule = (bool) info.GetValue(nameof (IsFixedDateRule), typeof (bool));
      }
    }

    private sealed class StringSerializer
    {
      private string m_serializedText;
      private int m_currentTokenStartIndex;
      private TimeZoneInfo.StringSerializer.State m_state;
      private const int initialCapacityForString = 64;
      private const char esc = '\\';
      private const char sep = ';';
      private const char lhs = '[';
      private const char rhs = ']';
      private const string escString = "\\";
      private const string sepString = ";";
      private const string lhsString = "[";
      private const string rhsString = "]";
      private const string escapedEsc = "\\\\";
      private const string escapedSep = "\\;";
      private const string escapedLhs = "\\[";
      private const string escapedRhs = "\\]";
      private const string dateTimeFormat = "MM:dd:yyyy";
      private const string timeOfDayFormat = "HH:mm:ss.FFF";

      public static string GetSerializedString(TimeZoneInfo zone)
      {
        StringBuilder stringBuilder1 = StringBuilderCache.Acquire(16);
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.BaseUtcOffset.TotalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName));
        stringBuilder1.Append(';');
        TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
        if (adjustmentRules != null && adjustmentRules.Length != 0)
        {
          for (int index = 0; index < adjustmentRules.Length; ++index)
          {
            TimeZoneInfo.AdjustmentRule adjustmentRule = adjustmentRules[index];
            stringBuilder1.Append('[');
            StringBuilder stringBuilder2 = stringBuilder1;
            DateTime dateTime = adjustmentRule.DateStart;
            string str1 = TimeZoneInfo.StringSerializer.SerializeSubstitute(dateTime.ToString("MM:dd:yyyy", (IFormatProvider) DateTimeFormatInfo.InvariantInfo));
            stringBuilder2.Append(str1);
            stringBuilder1.Append(';');
            StringBuilder stringBuilder3 = stringBuilder1;
            dateTime = adjustmentRule.DateEnd;
            string str2 = TimeZoneInfo.StringSerializer.SerializeSubstitute(dateTime.ToString("MM:dd:yyyy", (IFormatProvider) DateTimeFormatInfo.InvariantInfo));
            stringBuilder3.Append(str2);
            stringBuilder1.Append(';');
            StringBuilder stringBuilder4 = stringBuilder1;
            double totalMinutes = adjustmentRule.DaylightDelta.TotalMinutes;
            string str3 = TimeZoneInfo.StringSerializer.SerializeSubstitute(totalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture));
            stringBuilder4.Append(str3);
            stringBuilder1.Append(';');
            TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder1);
            stringBuilder1.Append(';');
            TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder1);
            stringBuilder1.Append(';');
            if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
            {
              StringBuilder stringBuilder5 = stringBuilder1;
              totalMinutes = adjustmentRule.BaseUtcOffsetDelta.TotalMinutes;
              string str4 = TimeZoneInfo.StringSerializer.SerializeSubstitute(totalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture));
              stringBuilder5.Append(str4);
              stringBuilder1.Append(';');
            }
            stringBuilder1.Append(']');
          }
        }
        stringBuilder1.Append(';');
        return StringBuilderCache.GetStringAndRelease(stringBuilder1);
      }

      public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
      {
        TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
        string nextStringValue1 = stringSerializer.GetNextStringValue(false);
        TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue(false);
        string nextStringValue2 = stringSerializer.GetNextStringValue(false);
        string nextStringValue3 = stringSerializer.GetNextStringValue(false);
        string nextStringValue4 = stringSerializer.GetNextStringValue(false);
        TimeZoneInfo.AdjustmentRule[] adjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue(false);
        try
        {
          return TimeZoneInfo.CreateCustomTimeZone(nextStringValue1, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, adjustmentRuleArrayValue);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
        catch (InvalidTimeZoneException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      private StringSerializer(string str)
      {
        this.m_serializedText = str;
        this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
      }

      private static string SerializeSubstitute(string text)
      {
        text = text.Replace("\\", "\\\\");
        text = text.Replace("[", "\\[");
        text = text.Replace("]", "\\]");
        return text.Replace(";", "\\;");
      }

      private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
      {
        serializedText.Append('[');
        int num = time.IsFixedDateRule ? 1 : 0;
        serializedText.Append(num.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        serializedText.Append(';');
        if (time.IsFixedDateRule)
        {
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", (IFormatProvider) DateTimeFormatInfo.InvariantInfo)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Day.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
        }
        else
        {
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", (IFormatProvider) DateTimeFormatInfo.InvariantInfo)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Week.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(((int) time.DayOfWeek).ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
        }
        serializedText.Append(']');
      }

      private static void VerifyIsEscapableCharacter(char c)
      {
        if (c != '\\' && c != ';' && (c != '[' && c != ']'))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", (object) c));
      }

      private void SkipVersionNextDataFields(int depth)
      {
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
        for (int currentTokenStartIndex = this.m_currentTokenStartIndex; currentTokenStartIndex < this.m_serializedText.Length; ++currentTokenStartIndex)
        {
          switch (state)
          {
            case TimeZoneInfo.StringSerializer.State.Escaped:
              TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[currentTokenStartIndex]);
              state = TimeZoneInfo.StringSerializer.State.NotEscaped;
              break;
            case TimeZoneInfo.StringSerializer.State.NotEscaped:
              switch (this.m_serializedText[currentTokenStartIndex])
              {
                case char.MinValue:
                  throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
                case '[':
                  ++depth;
                  continue;
                case '\\':
                  state = TimeZoneInfo.StringSerializer.State.Escaped;
                  continue;
                case ']':
                  --depth;
                  if (depth == 0)
                  {
                    this.m_currentTokenStartIndex = currentTokenStartIndex + 1;
                    if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
                    {
                      this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
                      return;
                    }
                    this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
                    return;
                  }
                  continue;
                default:
                  continue;
              }
          }
        }
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
      }

      private string GetNextStringValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
        {
          if (canEndWithoutSeparator)
            return (string) null;
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
        StringBuilder sb = StringBuilderCache.Acquire(64);
        for (int currentTokenStartIndex = this.m_currentTokenStartIndex; currentTokenStartIndex < this.m_serializedText.Length; ++currentTokenStartIndex)
        {
          switch (state)
          {
            case TimeZoneInfo.StringSerializer.State.Escaped:
              TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[currentTokenStartIndex]);
              sb.Append(this.m_serializedText[currentTokenStartIndex]);
              state = TimeZoneInfo.StringSerializer.State.NotEscaped;
              break;
            case TimeZoneInfo.StringSerializer.State.NotEscaped:
              switch (this.m_serializedText[currentTokenStartIndex])
              {
                case char.MinValue:
                  throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
                case ';':
                  this.m_currentTokenStartIndex = currentTokenStartIndex + 1;
                  this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
                  return StringBuilderCache.GetStringAndRelease(sb);
                case '[':
                  throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
                case '\\':
                  state = TimeZoneInfo.StringSerializer.State.Escaped;
                  continue;
                case ']':
                  if (!canEndWithoutSeparator)
                    throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
                  this.m_currentTokenStartIndex = currentTokenStartIndex;
                  this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
                  return sb.ToString();
                default:
                  sb.Append(this.m_serializedText[currentTokenStartIndex]);
                  continue;
              }
          }
        }
        if (state == TimeZoneInfo.StringSerializer.State.Escaped)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", (object) string.Empty));
        if (!canEndWithoutSeparator)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        this.m_currentTokenStartIndex = this.m_serializedText.Length;
        this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
        return StringBuilderCache.GetStringAndRelease(sb);
      }

      private DateTime GetNextDateTimeValue(bool canEndWithoutSeparator, string format)
      {
        DateTime result;
        if (!DateTime.TryParseExact(this.GetNextStringValue(canEndWithoutSeparator), format, (IFormatProvider) DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        return result;
      }

      private TimeSpan GetNextTimeSpanValue(bool canEndWithoutSeparator)
      {
        int nextInt32Value = this.GetNextInt32Value(canEndWithoutSeparator);
        try
        {
          return new TimeSpan(0, nextInt32Value, 0);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      private int GetNextInt32Value(bool canEndWithoutSeparator)
      {
        int result;
        if (!int.TryParse(this.GetNextStringValue(canEndWithoutSeparator), NumberStyles.AllowLeadingSign, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        return result;
      }

      private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue(bool canEndWithoutSeparator)
      {
        List<TimeZoneInfo.AdjustmentRule> adjustmentRuleList = new List<TimeZoneInfo.AdjustmentRule>(1);
        int num = 0;
        for (TimeZoneInfo.AdjustmentRule adjustmentRuleValue = this.GetNextAdjustmentRuleValue(true); adjustmentRuleValue != null; adjustmentRuleValue = this.GetNextAdjustmentRuleValue(true))
        {
          adjustmentRuleList.Add(adjustmentRuleValue);
          ++num;
        }
        if (!canEndWithoutSeparator)
        {
          if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
          if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (num == 0)
          return (TimeZoneInfo.AdjustmentRule[]) null;
        return adjustmentRuleList.ToArray();
      }

      private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
        {
          if (canEndWithoutSeparator)
            return (TimeZoneInfo.AdjustmentRule) null;
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_serializedText[this.m_currentTokenStartIndex] == ';')
          return (TimeZoneInfo.AdjustmentRule) null;
        if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        ++this.m_currentTokenStartIndex;
        DateTime nextDateTimeValue1 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
        DateTime nextDateTimeValue2 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
        TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue(false);
        TimeZoneInfo.TransitionTime transitionTimeValue1 = this.GetNextTransitionTimeValue(false);
        TimeZoneInfo.TransitionTime transitionTimeValue2 = this.GetNextTransitionTimeValue(false);
        TimeSpan baseUtcOffsetDelta = TimeSpan.Zero;
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_serializedText[this.m_currentTokenStartIndex] >= '0' && this.m_serializedText[this.m_currentTokenStartIndex] <= '9' || (this.m_serializedText[this.m_currentTokenStartIndex] == '-' || this.m_serializedText[this.m_currentTokenStartIndex] == '+'))
          baseUtcOffsetDelta = this.GetNextTimeSpanValue(false);
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
          this.SkipVersionNextDataFields(1);
        else
          ++this.m_currentTokenStartIndex;
        TimeZoneInfo.AdjustmentRule adjustmentRule;
        try
        {
          adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue1, nextDateTimeValue2, nextTimeSpanValue, transitionTimeValue1, transitionTimeValue2, baseUtcOffsetDelta);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
        this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
        return adjustmentRule;
      }

      private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ']')
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        ++this.m_currentTokenStartIndex;
        int nextInt32Value1 = this.GetNextInt32Value(false);
        switch (nextInt32Value1)
        {
          case 0:
          case 1:
            DateTime timeOfDay = this.GetNextDateTimeValue(false, "HH:mm:ss.FFF");
            timeOfDay = new DateTime(1, 1, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
            int nextInt32Value2 = this.GetNextInt32Value(false);
            TimeZoneInfo.TransitionTime transitionTime;
            if (nextInt32Value1 == 1)
            {
              int nextInt32Value3 = this.GetNextInt32Value(false);
              try
              {
                transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(timeOfDay, nextInt32Value2, nextInt32Value3);
              }
              catch (ArgumentException ex)
              {
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
              }
            }
            else
            {
              int nextInt32Value3 = this.GetNextInt32Value(false);
              int nextInt32Value4 = this.GetNextInt32Value(false);
              try
              {
                transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(timeOfDay, nextInt32Value2, nextInt32Value3, (DayOfWeek) nextInt32Value4);
              }
              catch (ArgumentException ex)
              {
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
              }
            }
            if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
              throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
            if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
              this.SkipVersionNextDataFields(1);
            else
              ++this.m_currentTokenStartIndex;
            bool flag = false;
            if (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ';')
            {
              ++this.m_currentTokenStartIndex;
              flag = true;
            }
            if (!flag && !canEndWithoutSeparator)
              throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
            this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
            return transitionTime;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
      }

      private enum State
      {
        Escaped,
        NotEscaped,
        StartOfToken,
        EndOfLine,
      }
    }

    private class TimeZoneInfoComparer : IComparer<TimeZoneInfo>
    {
      int IComparer<TimeZoneInfo>.Compare(TimeZoneInfo x, TimeZoneInfo y)
      {
        int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
        if (num != 0)
          return num;
        return string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal);
      }
    }
  }
}
