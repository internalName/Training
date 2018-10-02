// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Globalization
{
  /// <summary>
  ///   Предоставляет сведения о конкретных языка и региональных параметров (называется языкового стандарта для разработки неуправляемого кода).
  ///    Эти сведения включают имена языков и региональных параметров, систему письма, используемый календарь, порядок сортировки строк и форматы дат и чисел.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CultureInfo : ICloneable, IFormatProvider
  {
    private static readonly bool init = CultureInfo.Init();
    [OptionalField(VersionAdded = 1)]
    internal int cultureID = (int) sbyte.MaxValue;
    internal bool m_isReadOnly;
    internal CompareInfo compareInfo;
    internal TextInfo textInfo;
    [NonSerialized]
    internal RegionInfo regionInfo;
    internal NumberFormatInfo numInfo;
    internal DateTimeFormatInfo dateTimeInfo;
    internal Calendar calendar;
    [OptionalField(VersionAdded = 1)]
    internal int m_dataItem;
    [NonSerialized]
    internal CultureData m_cultureData;
    [NonSerialized]
    internal bool m_isInherited;
    [NonSerialized]
    private bool m_isSafeCrossDomain;
    [NonSerialized]
    private int m_createdDomainID;
    [NonSerialized]
    private CultureInfo m_consoleFallbackCulture;
    internal string m_name;
    [NonSerialized]
    private string m_nonSortName;
    [NonSerialized]
    private string m_sortName;
    private static volatile CultureInfo s_userDefaultCulture;
    private static volatile CultureInfo s_InvariantCultureInfo;
    private static volatile CultureInfo s_userDefaultUICulture;
    private static volatile CultureInfo s_InstalledUICultureInfo;
    private static volatile CultureInfo s_DefaultThreadCurrentUICulture;
    private static volatile CultureInfo s_DefaultThreadCurrentCulture;
    private static volatile Hashtable s_LcidCachedCultures;
    private static volatile Hashtable s_NameCachedCultures;
    [SecurityCritical]
    private static volatile WindowsRuntimeResourceManagerBase s_WindowsRuntimeResourceManager;
    [ThreadStatic]
    private static bool ts_IsDoingAppXCultureInfoLookup;
    [NonSerialized]
    private CultureInfo m_parent;
    internal const int LOCALE_NEUTRAL = 0;
    private const int LOCALE_USER_DEFAULT = 1024;
    private const int LOCALE_SYSTEM_DEFAULT = 2048;
    internal const int LOCALE_CUSTOM_DEFAULT = 3072;
    internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;
    internal const int LOCALE_INVARIANT = 127;
    private const int LOCALE_TRADITIONAL_SPANISH = 1034;
    private bool m_useUserOverride;
    private const int LOCALE_SORTID_MASK = 983040;
    private static volatile bool s_isTaiwanSku;
    private static volatile bool s_haveIsTaiwanSku;

    private static bool Init()
    {
      if (CultureInfo.s_InvariantCultureInfo == null)
        CultureInfo.s_InvariantCultureInfo = new CultureInfo("", false)
        {
          m_isReadOnly = true
        };
      CultureInfo.s_userDefaultCulture = CultureInfo.s_userDefaultUICulture = CultureInfo.s_InvariantCultureInfo;
      CultureInfo.s_userDefaultCulture = CultureInfo.InitUserDefaultCulture();
      CultureInfo.s_userDefaultUICulture = CultureInfo.InitUserDefaultUICulture();
      return true;
    }

    [SecuritySafeCritical]
    private static CultureInfo InitUserDefaultCulture()
    {
      string defaultLocaleName = CultureInfo.GetDefaultLocaleName(1024);
      if (defaultLocaleName == null)
      {
        defaultLocaleName = CultureInfo.GetDefaultLocaleName(2048);
        if (defaultLocaleName == null)
          return CultureInfo.InvariantCulture;
      }
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultLocaleName, true);
      cultureByName.m_isReadOnly = true;
      return cultureByName;
    }

    private static CultureInfo InitUserDefaultUICulture()
    {
      string defaultUiLanguage = CultureInfo.GetUserDefaultUILanguage();
      if (defaultUiLanguage == CultureInfo.UserDefaultCulture.Name)
        return CultureInfo.UserDefaultCulture;
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultUiLanguage, true);
      if (cultureByName == null)
        return CultureInfo.InvariantCulture;
      cultureByName.m_isReadOnly = true;
      return cultureByName;
    }

    [SecuritySafeCritical]
    internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
    {
      if (CultureInfo.ts_IsDoingAppXCultureInfoLookup)
        return (CultureInfo) null;
      if (AppDomain.IsAppXNGen)
        return (CultureInfo) null;
      try
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = true;
        if (CultureInfo.s_WindowsRuntimeResourceManager == null)
          CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
        return CultureInfo.s_WindowsRuntimeResourceManager.GlobalResourceContextBestFitCultureInfo;
      }
      finally
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = false;
      }
    }

    [SecuritySafeCritical]
    internal static bool SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo ci)
    {
      if (AppDomain.IsAppXNGen)
        return false;
      if (CultureInfo.s_WindowsRuntimeResourceManager == null)
        CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
      return CultureInfo.s_WindowsRuntimeResourceManager.SetGlobalResourceContextDefaultCulture(ci);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureInfo" /> на основе языка и региональных параметров, заданных именем.
    /// </summary>
    /// <param name="name">
    ///   Предварительно определенное имя <see cref="T:System.Globalization.CultureInfo" />, свойство <see cref="P:System.Globalization.CultureInfo.Name" /> существующего объекта <see cref="T:System.Globalization.CultureInfo" /> или имя языка и региональных параметров, свойственных только Windows.
    ///   <paramref name="name" /> не учитывает регистр.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> не является допустимым именем культуры.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    [__DynamicallyInvokable]
    public CultureInfo(string name)
      : this(name, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureInfo" /> на основе языка и региональных параметров, заданных именем, и логического значения, указывающего, нужно ли использовать выбранные пользователем параметры языка и региональных параметров в операционной системе.
    /// </summary>
    /// <param name="name">
    ///   Предварительно определенное имя <see cref="T:System.Globalization.CultureInfo" />, свойство <see cref="P:System.Globalization.CultureInfo.Name" /> существующего объекта <see cref="T:System.Globalization.CultureInfo" /> или имя языка и региональных параметров, свойственных только Windows.
    ///   <paramref name="name" /> не учитывает регистр.
    /// </param>
    /// <param name="useUserOverride">
    ///   Логическое значение, определяющее применение параметров языка и региональных параметров, заданных пользователем (<see langword="true" />) или используемых по умолчанию (<see langword="false" />).
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> не является допустимым именем культуры.
    ///    Дополнительные сведения см. в разделе "Примечания для вызывающей стороны".
    /// </exception>
    public CultureInfo(string name, bool useUserOverride)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name), Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(name, useUserOverride);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException(nameof (name), name, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
    }

    private CultureInfo(CultureData cultureData)
    {
      this.m_cultureData = cultureData;
      this.m_name = cultureData.CultureName;
      this.m_isInherited = false;
    }

    private static CultureInfo CreateCultureInfoNoThrow(string name, bool useUserOverride)
    {
      CultureData cultureData = CultureData.GetCultureData(name, useUserOverride);
      if (cultureData == null)
        return (CultureInfo) null;
      return new CultureInfo(cultureData);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureInfo" /> на основе языка и региональных параметров, заданных идентификатором.
    /// </summary>
    /// <param name="culture">
    ///   Предварительно определенный идентификатор <see cref="T:System.Globalization.CultureInfo" />, свойство <see cref="P:System.Globalization.CultureInfo.LCID" /> существующего объекта <see cref="T:System.Globalization.CultureInfo" /> или идентификатор языка и региональных параметров, свойственных только Windows.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="culture" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="culture" /> не является допустимым идентификатором языка и региональных параметров.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    public CultureInfo(int culture)
      : this(culture, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureInfo" /> на основе языка и региональных параметров, заданных идентификатором, и логического значения, указывающего, нужно ли использовать выбранные пользователем параметры языка и региональных параметров в операционной системе.
    /// </summary>
    /// <param name="culture">
    ///   Предварительно определенный идентификатор <see cref="T:System.Globalization.CultureInfo" />, свойство <see cref="P:System.Globalization.CultureInfo.LCID" /> существующего объекта <see cref="T:System.Globalization.CultureInfo" /> или идентификатор языка и региональных параметров, свойственных только Windows.
    /// </param>
    /// <param name="useUserOverride">
    ///   Логическое значение, определяющее применение параметров языка и региональных параметров, заданных пользователем (<see langword="true" />) или используемых по умолчанию (<see langword="false" />).
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="culture" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="culture" /> не является допустимым идентификатором языка и региональных параметров.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    public CultureInfo(int culture, bool useUserOverride)
    {
      if (culture < 0)
        throw new ArgumentOutOfRangeException(nameof (culture), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.InitializeFromCultureId(culture, useUserOverride);
    }

    private void InitializeFromCultureId(int culture, bool useUserOverride)
    {
      if (culture <= 1024)
      {
        if (culture != 0 && culture != 1024)
          goto label_4;
      }
      else if (culture != 2048 && culture != 3072 && culture != 4096)
        goto label_4;
      throw new CultureNotFoundException(nameof (culture), culture, Environment.GetResourceString("Argument_CultureNotSupported"));
label_4:
      this.m_cultureData = CultureData.GetCultureData(culture, useUserOverride);
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
      this.m_name = this.m_cultureData.CultureName;
    }

    internal static void CheckDomainSafetyObject(object obj, object container)
    {
      if (obj.GetType().Assembly != typeof (CultureInfo).Assembly)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidOperation_SubclassedObject"), (object) obj.GetType(), (object) container.GetType()));
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name == null || CultureInfo.IsAlternateSortLcid(this.cultureID))
      {
        this.InitializeFromCultureId(this.cultureID, this.m_useUserOverride);
      }
      else
      {
        this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
        if (this.m_cultureData == null)
          throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
      }
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
      if (!(this.GetType().Assembly == typeof (CultureInfo).Assembly))
        return;
      if (this.textInfo != null)
        CultureInfo.CheckDomainSafetyObject((object) this.textInfo, (object) this);
      if (this.compareInfo == null)
        return;
      CultureInfo.CheckDomainSafetyObject((object) this.compareInfo, (object) this);
    }

    private static bool IsAlternateSortLcid(int lcid)
    {
      if (lcid == 1034)
        return true;
      return (uint) (lcid & 983040) > 0U;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.m_name = this.m_cultureData.CultureName;
      this.m_useUserOverride = this.m_cultureData.UseUserOverride;
      this.cultureID = this.m_cultureData.ILANGUAGE;
    }

    internal bool IsSafeCrossDomain
    {
      get
      {
        return this.m_isSafeCrossDomain;
      }
    }

    internal int CreatedDomainID
    {
      get
      {
        return this.m_createdDomainID;
      }
    }

    internal void StartCrossDomainTracking()
    {
      if (this.m_createdDomainID != 0)
        return;
      if (this.CanSendCrossDomain())
        this.m_isSafeCrossDomain = true;
      Thread.MemoryBarrier();
      this.m_createdDomainID = Thread.GetDomainID();
    }

    internal bool CanSendCrossDomain()
    {
      bool flag = false;
      if (this.GetType() == typeof (CultureInfo))
        flag = true;
      return flag;
    }

    internal CultureInfo(string cultureName, string textAndCompareCultureName)
    {
      if (cultureName == null)
        throw new ArgumentNullException(nameof (cultureName), Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(cultureName, false);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException(nameof (cultureName), cultureName, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
      this.compareInfo = cultureInfo.CompareInfo;
      this.textInfo = cultureInfo.TextInfo;
    }

    private static CultureInfo GetCultureByName(string name, bool userOverride)
    {
      try
      {
        return userOverride ? new CultureInfo(name) : CultureInfo.GetCultureInfo(name);
      }
      catch (ArgumentException ex)
      {
      }
      return (CultureInfo) null;
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Globalization.CultureInfo" />, который представляет определенный язык и региональные параметры, соответствующие заданному имени.
    /// </summary>
    /// <param name="name">
    ///   Предварительно определенное имя <see cref="T:System.Globalization.CultureInfo" /> или имя существующего объекта <see cref="T:System.Globalization.CultureInfo" />.
    ///   <paramref name="name" /> не учитывает регистр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, который может представлять перечисленные ниже параметры.
    /// 
    ///   Инвариантный язык и региональные параметры, если <paramref name="name" /> является пустой строкой ("");
    /// 
    ///   -или-
    /// 
    ///   определенный язык и региональные параметры, связанные с <paramref name="name" />, если <paramref name="name" /> относится к нейтральному языку и региональным параметрам;
    /// 
    ///   -или-
    /// 
    ///   язык и региональные параметры, указанные в параметре <paramref name="name" />, если <paramref name="name" /> уже относится к определенному языку и региональным параметрам.
    /// </returns>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> не является допустимым именем языка и региональных параметров.
    /// 
    ///   -или-
    /// 
    ///   Язык и региональные параметры, заданные <paramref name="name" />, не имеют определенных, связанных с ними языков и региональных параметров.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    public static CultureInfo CreateSpecificCulture(string name)
    {
      CultureInfo cultureInfo;
      try
      {
        cultureInfo = new CultureInfo(name);
      }
      catch (ArgumentException ex1)
      {
        cultureInfo = (CultureInfo) null;
        for (int length = 0; length < name.Length; ++length)
        {
          if ('-' == name[length])
          {
            try
            {
              cultureInfo = new CultureInfo(name.Substring(0, length));
              break;
            }
            catch (ArgumentException ex2)
            {
              throw;
            }
          }
        }
        if (cultureInfo == null)
          throw;
      }
      if (!cultureInfo.IsNeutralCulture)
        return cultureInfo;
      return new CultureInfo(cultureInfo.m_cultureData.SSPECIFICCULTURE);
    }

    internal static bool VerifyCultureName(string cultureName, bool throwException)
    {
      for (int index = 0; index < cultureName.Length; ++index)
      {
        char c = cultureName[index];
        if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
        {
          if (throwException)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", (object) cultureName));
          return false;
        }
      }
      return true;
    }

    internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
    {
      if (!culture.m_isInherited)
        return true;
      return CultureInfo.VerifyCultureName(culture.Name, throwException);
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Globalization.CultureInfo" />, представляющий язык и региональные параметры, используемые текущим потоком.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий язык и региональные параметры, используемые текущим потоком.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static CultureInfo CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentCulture;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
          return;
        Thread.CurrentThread.CurrentCulture = value;
      }
    }

    internal static CultureInfo UserDefaultCulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultCulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultCulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultCulture();
          CultureInfo.s_userDefaultCulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    internal static CultureInfo UserDefaultUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultUICulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultUICulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultUICulture();
          CultureInfo.s_userDefaultUICulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Globalization.CultureInfo" />, представляющий текущий язык и региональные параметры пользовательского интерфейса, используемые диспетчером ресурсов для поиска ресурсов, связанных с конкретным языком и региональными параметрами, во время выполнения.
    /// </summary>
    /// <returns>
    ///   Язык и региональные параметры, используемые диспетчером ресурсов для поиска ресурсов, связанных с языком и региональными параметрами, во время выполнения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойству присвоено имя языка и региональных параметров, которое не может использоваться для нахождения файла ресурсов.
    ///    Имена файлов ресурсов могут содержать только буквы, цифры, дефисы или символы подчеркивания.
    /// </exception>
    [__DynamicallyInvokable]
    public static CultureInfo CurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentUICulture;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
          return;
        Thread.CurrentThread.CurrentUICulture = value;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.CultureInfo" />, представляющий язык и региональные параметры, установленные с операционной системой.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, представляющий язык и региональные параметры, установленные с операционной системой.
    /// </returns>
    public static CultureInfo InstalledUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_InstalledUICultureInfo;
        if (cultureInfo == null)
        {
          cultureInfo = CultureInfo.GetCultureByName(CultureInfo.GetSystemDefaultUILanguage(), true) ?? CultureInfo.InvariantCulture;
          cultureInfo.m_isReadOnly = true;
          CultureInfo.s_InstalledUICultureInfo = cultureInfo;
        }
        return cultureInfo;
      }
    }

    /// <summary>
    ///   Возвращает или задает язык и региональные параметры, используемые по умолчанию для потоков в текущем домене приложения.
    /// </summary>
    /// <returns>
    ///   Язык и региональные параметры по умолчанию для потоков в текущем домене приложения или значение <see langword="null" />, если текущий язык и региональные параметры системы являются заданными по умолчанию для потока в домене приложения.
    /// </returns>
    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentCulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        CultureInfo.s_DefaultThreadCurrentCulture = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает язык и региональные параметры пользовательского интерфейса, используемые по умолчанию для потоков в текущем домене приложения.
    /// </summary>
    /// <returns>
    ///   Язык и региональные параметры по умолчанию пользовательского интерфейса для потоков в текущем домене приложения или значение <see langword="null" />, если текущий язык и региональные параметры пользовательского интерфейса системы являются заданными по умолчанию для потока пользовательского интерфейса в домене приложения.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   В операции задания значение свойства <see cref="P:System.Globalization.CultureInfo.Name" /> является недопустимым.
    /// </exception>
    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentUICulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        if (value != null)
          CultureInfo.VerifyCultureName(value, true);
        CultureInfo.s_DefaultThreadCurrentUICulture = value;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.CultureInfo" />, не зависящий от языка и региональных параметров (инвариантный).
    /// </summary>
    /// <returns>
    ///   Объект, не зависящий от языка и региональных параметров (инвариантный).
    /// </returns>
    [__DynamicallyInvokable]
    public static CultureInfo InvariantCulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_InvariantCultureInfo;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.CultureInfo" />, представляющий родительский язык и региональные параметры текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, представляющий родительский язык и региональные параметры текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual CultureInfo Parent
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.m_parent == null)
        {
          string sparent = this.m_cultureData.SPARENT;
          Interlocked.CompareExchange<CultureInfo>(ref this.m_parent, !string.IsNullOrEmpty(sparent) ? CultureInfo.CreateCultureInfoNoThrow(sparent, this.m_cultureData.UseUserOverride) ?? CultureInfo.InvariantCulture : CultureInfo.InvariantCulture, (CultureInfo) null);
        }
        return this.m_parent;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор языка и региональных параметров для текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор языка и региональных параметров для текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    public virtual int LCID
    {
      get
      {
        return this.m_cultureData.ILANGUAGE;
      }
    }

    /// <summary>Возвращает активный идентификатор языка ввода.</summary>
    /// <returns>
    ///   32-битный номер со знаком, указывающий идентификатор языка ввода.
    /// </returns>
    [ComVisible(false)]
    public virtual int KeyboardLayoutId
    {
      get
      {
        return this.m_cultureData.IINPUTLANGUAGEHANDLE;
      }
    }

    /// <summary>
    ///   Возвращает список поддерживаемых языков и региональных параметров, отфильтрованный по заданному значению параметра <see cref="T:System.Globalization.CultureTypes" />.
    /// </summary>
    /// <param name="types">
    ///   Побитовая комбинация значений перечисления, определяющих фильтрацию получаемых языков и региональных параметров.
    /// </param>
    /// <returns>
    ///   Массив, содержащий языки и региональные параметры, определенные параметром <paramref name="types" />.
    ///    Массив языков и региональных параметров не упорядочен.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="types" /> указывает недопустимое сочетание значений <see cref="T:System.Globalization.CultureTypes" />.
    /// </exception>
    public static CultureInfo[] GetCultures(CultureTypes types)
    {
      if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
        types |= CultureTypes.ReplacementCultures;
      return CultureData.GetCultures(types);
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров в формате languagecode2-country/regioncode2.
    /// </summary>
    /// <returns>
    ///   Имя языка и региональных параметров в формате languagecode2-country/regioncode2.
    ///   languagecode2 — двухбуквенный код в нижнем регистре, производный от ISO 639-1.
    ///   country/regioncode2 является производным от ISO 3166 и обычно состоит из 2 прописных букв или из тега языка BCP-47.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_nonSortName == null)
        {
          this.m_nonSortName = this.m_cultureData.SNAME;
          if (this.m_nonSortName == null)
            this.m_nonSortName = string.Empty;
        }
        return this.m_nonSortName;
      }
    }

    internal string SortName
    {
      get
      {
        if (this.m_sortName == null)
          this.m_sortName = this.m_cultureData.SCOMPAREINFO;
        return this.m_sortName;
      }
    }

    /// <summary>
    ///   Не рекомендуется.
    ///    Возвращает идентификацию языка по стандарту RFC 4646.
    /// </summary>
    /// <returns>
    ///   Строка, являющаяся идентификацией языка по стандарту RFC 4646.
    /// </returns>
    [ComVisible(false)]
    public string IetfLanguageTag
    {
      get
      {
        string name = this.Name;
        if (name == "zh-CHT")
          return "zh-Hant";
        if (name == "zh-CHS")
          return "zh-Hans";
        return this.Name;
      }
    }

    /// <summary>
    ///   Возвращает полное локализованное имя языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Полное локализованное имя языка и региональных параметров в формате languagefull [country/regionfull], где languagefull — полное имя языка, а country/regionfull — полное имя страны или региона.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string DisplayName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SLOCALIZEDDISPLAYNAME;
      }
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров, состоящих из языка, страны или региона и дополнительного набора символов, которые свойственны для этого языка.
    /// </summary>
    /// <returns>
    ///   Имя языка и региональных параметров.
    ///    состоит из полного имени языка, полного имени страны или региона и дополнительного набора символов.
    ///    Дополнительные сведения о формате см. в описании класса <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string NativeName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SNATIVEDISPLAYNAME;
      }
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров в формате languagefull [country/regionfull] на английском языке.
    /// </summary>
    /// <returns>
    ///   Имя языка и региональных параметров в формате languagefull [country/regionfull] на английском языке, где languagefull — полное имя языка, а country/regionfull — полное имя страны или региона.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string EnglishName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SENGDISPLAYNAME;
      }
    }

    /// <summary>
    ///   Возвращает двухбуквенный код ISO 639-1 для языка текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Двухбуквенный код ISO 639-1 для языка текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string TwoLetterISOLanguageName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SISO639LANGNAME;
      }
    }

    /// <summary>
    ///   Возвращает трехбуквенный код ISO 639-2 для языка текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Трехбуквенный код ISO 639-2 для языка текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    public virtual string ThreeLetterISOLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SISO639LANGNAME2;
      }
    }

    /// <summary>
    ///   Возвращает трехбуквенный код для языка, определенный в формате Windows API.
    /// </summary>
    /// <returns>
    ///   Трехбуквенный код для языка, определенный в формате Windows API.
    /// </returns>
    public virtual string ThreeLetterWindowsLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SABBREVLANGNAME;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.CompareInfo" />, который определяет способ сравнения строк в данном языке и региональных параметрах.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Globalization.CompareInfo" /> для определения способа сравнения строк в данном языке и региональных параметрах.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual CompareInfo CompareInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.compareInfo == null)
        {
          CompareInfo compareInfo = this.UseUserOverride ? CultureInfo.GetCultureInfo(this.m_name).CompareInfo : new CompareInfo(this);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return compareInfo;
          this.compareInfo = compareInfo;
        }
        return this.compareInfo;
      }
    }

    private RegionInfo Region
    {
      get
      {
        if (this.regionInfo == null)
          this.regionInfo = new RegionInfo(this.m_cultureData);
        return this.regionInfo;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.TextInfo" />, определяющий систему письма, связанную с данным языком и региональными параметрами.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.TextInfo" />, определяющий систему письма, связанную с данным языком и региональными параметрами.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual TextInfo TextInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.textInfo == null)
        {
          TextInfo textInfo = new TextInfo(this.m_cultureData);
          textInfo.SetReadOnlyState(this.m_isReadOnly);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return textInfo;
          this.textInfo = textInfo;
        }
        return this.textInfo;
      }
    }

    /// <summary>
    ///   Определяет, является ли заданный объект тем же языком и региональными параметрами, что и <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> относится к тому же языку и региональным параметрам, что и текущий объект <see cref="T:System.Globalization.CultureInfo" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (this == value)
        return true;
      CultureInfo cultureInfo = value as CultureInfo;
      if (cultureInfo != null && this.Name.Equals(cultureInfo.Name))
        return this.CompareInfo.Equals((object) cultureInfo.CompareInfo);
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего класса <see cref="T:System.Globalization.CultureInfo" /> для использования в алгоритмах и структурах данных хеширования, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
    }

    /// <summary>
    ///   Возвращает строку, содержащую имя текущего объекта <see cref="T:System.Globalization.CultureInfo" /> в формате languagecode2-country/regioncode2.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя текущего объекта<see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.m_name;
    }

    /// <summary>
    ///   Возвращает объект, определяющий способ форматирования заданного типа.
    /// </summary>
    /// <param name="formatType">
    ///   Значение <see cref="T:System.Type" />, для которого нужно получить объект форматирования.
    ///    Этот метод поддерживает только типы <see cref="T:System.Globalization.NumberFormatInfo" /> и <see cref="T:System.Globalization.DateTimeFormatInfo" />.
    /// </param>
    /// <returns>
    ///   Значение свойства <see cref="P:System.Globalization.CultureInfo.NumberFormat" />, являющееся объектом <see cref="T:System.Globalization.NumberFormatInfo" />, который содержит сведения о формате числа по умолчанию для текущего <see cref="T:System.Globalization.CultureInfo" />, если <paramref name="formatType" /> является объектом <see cref="T:System.Type" /> для класса <see cref="T:System.Globalization.NumberFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   Значение свойства <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" />, являющееся объектом <see cref="T:System.Globalization.DateTimeFormatInfo" />, который содержит сведения о формате даты и времени по умолчанию для текущего <see cref="T:System.Globalization.CultureInfo" />, если <paramref name="formatType" /> является объектом <see cref="T:System.Type" /> для класса <see cref="T:System.Globalization.DateTimeFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   Значение NULL, если <paramref name="formatType" /> — любой другой объект.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual object GetFormat(Type formatType)
    {
      if (formatType == typeof (NumberFormatInfo))
        return (object) this.NumberFormat;
      if (formatType == typeof (DateTimeFormatInfo))
        return (object) this.DateTimeFormat;
      return (object) null;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, представляет ли текущий объект <see cref="T:System.Globalization.CultureInfo" /> нейтральный язык и региональные параметры.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Globalization.CultureInfo" /> представляет нейтральный язык и региональные параметры, в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsNeutralCulture
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IsNeutralCulture;
      }
    }

    /// <summary>
    ///   Возвращает типы языков и региональных параметров, относящихся к текущему объекту <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Побитовая комбинация одного или нескольких значений <see cref="T:System.Globalization.CultureTypes" />.
    ///    Значение по умолчанию отсутствует.
    /// </returns>
    [ComVisible(false)]
    public CultureTypes CultureTypes
    {
      get
      {
        CultureTypes cultureTypes = (CultureTypes) 0;
        return (CultureTypes) ((!this.m_cultureData.IsNeutralCulture ? (int) (cultureTypes | CultureTypes.SpecificCultures) : (int) (cultureTypes | CultureTypes.NeutralCultures)) | (this.m_cultureData.IsWin32Installed ? 4 : 0) | (this.m_cultureData.IsFramework ? 64 : 0) | (this.m_cultureData.IsSupplementalCustomCulture ? 8 : 0) | (this.m_cultureData.IsReplacementCulture ? 24 : 0));
      }
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Globalization.NumberFormatInfo" />, определяющий формат отображения чисел, денежной единицы и процентов, соответствующий языку и региональным параметрам.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.NumberFormatInfo" />, определяющий формат отображения чисел, денежной единицы и процентов, соответствующий языку и региональным параметрам.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение NULL.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> или любого из свойств <see cref="T:System.Globalization.NumberFormatInfo" /> заданы значения, а <see cref="T:System.Globalization.CultureInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual NumberFormatInfo NumberFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.numInfo == null)
          this.numInfo = new NumberFormatInfo(this.m_cultureData)
          {
            isReadOnly = this.m_isReadOnly
          };
        return this.numInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value), Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.numInfo = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Globalization.DateTimeFormatInfo" />, определяющий формат отображения даты и времени, соответствующий языку и региональным параметрам.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.DateTimeFormatInfo" />, определяющий формат отображения даты и времени, соответствующий языку и региональным параметрам.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение NULL.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> или любого из свойств <see cref="T:System.Globalization.DateTimeFormatInfo" /> заданы значения, а <see cref="T:System.Globalization.CultureInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual DateTimeFormatInfo DateTimeFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.dateTimeInfo == null)
        {
          DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
          dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
          Thread.MemoryBarrier();
          this.dateTimeInfo = dateTimeFormatInfo;
        }
        return this.dateTimeInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value), Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.dateTimeInfo = value;
      }
    }

    /// <summary>
    ///   Обновляет кешированные данные, связанные с языком и региональными параметрами.
    /// </summary>
    public void ClearCachedData()
    {
      CultureInfo.s_userDefaultUICulture = (CultureInfo) null;
      CultureInfo.s_userDefaultCulture = (CultureInfo) null;
      RegionInfo.s_currentRegionInfo = (RegionInfo) null;
      TimeZone.ResetTimeZone();
      TimeZoneInfo.ClearCachedData();
      CultureInfo.s_LcidCachedCultures = (Hashtable) null;
      CultureInfo.s_NameCachedCultures = (Hashtable) null;
      CultureData.ClearCachedData();
    }

    internal static Calendar GetCalendarInstance(int calType)
    {
      if (calType == 1)
        return (Calendar) new GregorianCalendar();
      return CultureInfo.GetCalendarInstanceRare(calType);
    }

    internal static Calendar GetCalendarInstanceRare(int calType)
    {
      switch (calType)
      {
        case 2:
        case 9:
        case 10:
        case 11:
        case 12:
          return (Calendar) new GregorianCalendar((GregorianCalendarTypes) calType);
        case 3:
          return (Calendar) new JapaneseCalendar();
        case 4:
          return (Calendar) new TaiwanCalendar();
        case 5:
          return (Calendar) new KoreanCalendar();
        case 6:
          return (Calendar) new HijriCalendar();
        case 7:
          return (Calendar) new ThaiBuddhistCalendar();
        case 8:
          return (Calendar) new HebrewCalendar();
        case 14:
          return (Calendar) new JapaneseLunisolarCalendar();
        case 15:
          return (Calendar) new ChineseLunisolarCalendar();
        case 20:
          return (Calendar) new KoreanLunisolarCalendar();
        case 21:
          return (Calendar) new TaiwanLunisolarCalendar();
        case 22:
          return (Calendar) new PersianCalendar();
        case 23:
          return (Calendar) new UmAlQuraCalendar();
        default:
          return (Calendar) new GregorianCalendar();
      }
    }

    /// <summary>
    ///   Возвращает календарь, используемый по умолчанию для языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.Calendar" />, представляющий календарь, используемый по умолчанию в языке и региональных параметрах.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Calendar Calendar
    {
      [__DynamicallyInvokable] get
      {
        if (this.calendar == null)
        {
          Calendar defaultCalendar = this.m_cultureData.DefaultCalendar;
          Thread.MemoryBarrier();
          defaultCalendar.SetReadOnlyState(this.m_isReadOnly);
          this.calendar = defaultCalendar;
        }
        return this.calendar;
      }
    }

    /// <summary>
    ///   Возвращает список календарей, которые могут использоваться в данном языке и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Массив типа <see cref="T:System.Globalization.Calendar" />, представляющий календари, которые могут использоваться в языке и региональных параметрах, представленных текущим <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Calendar[] OptionalCalendars
    {
      [__DynamicallyInvokable] get
      {
        int[] calendarIds = this.m_cultureData.CalendarIds;
        Calendar[] calendarArray = new Calendar[calendarIds.Length];
        for (int index = 0; index < calendarArray.Length; ++index)
          calendarArray[index] = CultureInfo.GetCalendarInstance(calendarIds[index]);
        return calendarArray;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, использует ли текущий объект <see cref="T:System.Globalization.CultureInfo" /> параметры языка и региональных параметров, выбранные пользователем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Globalization.CultureInfo" /> использует параметры языка и региональных параметров, выбранные пользователем; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool UseUserOverride
    {
      get
      {
        return this.m_cultureData.UseUserOverride;
      }
    }

    /// <summary>
    ///   Возвращает язык и региональные стандарты интерфейса пользователя, подходящие для приложений консоли, если при этом неприменим язык и региональные стандарты графического пользовательского интерфейса по умолчанию.
    /// </summary>
    /// <returns>
    ///   Альтернативный язык и региональные параметры, используемые для чтения и отображения текста на консоли.
    /// </returns>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public CultureInfo GetConsoleFallbackUICulture()
    {
      CultureInfo cultureInfo = this.m_consoleFallbackCulture;
      if (cultureInfo == null)
      {
        cultureInfo = CultureInfo.CreateSpecificCulture(this.m_cultureData.SCONSOLEFALLBACKNAME);
        cultureInfo.m_isReadOnly = true;
        this.m_consoleFallbackCulture = cultureInfo;
      }
      return cultureInfo;
    }

    /// <summary>
    ///   Создает копию текущего поставщика <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <returns>
    ///   Копия текущего объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual object Clone()
    {
      CultureInfo cultureInfo = (CultureInfo) this.MemberwiseClone();
      cultureInfo.m_isReadOnly = false;
      if (!this.m_isInherited)
      {
        if (this.dateTimeInfo != null)
          cultureInfo.dateTimeInfo = (DateTimeFormatInfo) this.dateTimeInfo.Clone();
        if (this.numInfo != null)
          cultureInfo.numInfo = (NumberFormatInfo) this.numInfo.Clone();
      }
      else
      {
        cultureInfo.DateTimeFormat = (DateTimeFormatInfo) this.DateTimeFormat.Clone();
        cultureInfo.NumberFormat = (NumberFormatInfo) this.NumberFormat.Clone();
      }
      if (this.textInfo != null)
        cultureInfo.textInfo = (TextInfo) this.textInfo.Clone();
      if (this.calendar != null)
        cultureInfo.calendar = (Calendar) this.calendar.Clone();
      return (object) cultureInfo;
    }

    /// <summary>
    ///   Возвращает программу-оболочку, доступную только для чтения, для заданного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="ci">
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Доступная только для чтения программа-оболочка <see cref="T:System.Globalization.CultureInfo" /> для параметра <paramref name="ci" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="ci" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public static CultureInfo ReadOnly(CultureInfo ci)
    {
      if (ci == null)
        throw new ArgumentNullException(nameof (ci));
      if (ci.IsReadOnly)
        return ci;
      CultureInfo cultureInfo = (CultureInfo) ci.MemberwiseClone();
      if (!ci.IsNeutralCulture)
      {
        if (!ci.m_isInherited)
        {
          if (ci.dateTimeInfo != null)
            cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci.dateTimeInfo);
          if (ci.numInfo != null)
            cultureInfo.numInfo = NumberFormatInfo.ReadOnly(ci.numInfo);
        }
        else
        {
          cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
          cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
        }
      }
      if (ci.textInfo != null)
        cultureInfo.textInfo = TextInfo.ReadOnly(ci.textInfo);
      if (ci.calendar != null)
        cultureInfo.calendar = Calendar.ReadOnly(ci.calendar);
      cultureInfo.m_isReadOnly = true;
      return cultureInfo;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий объект <see cref="T:System.Globalization.CultureInfo" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Globalization.CultureInfo" /> доступен только для чтения, в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    private void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal bool HasInvariantCultureName
    {
      get
      {
        return this.Name == CultureInfo.InvariantCulture.Name;
      }
    }

    internal static CultureInfo GetCultureInfoHelper(int lcid, string name, string altName)
    {
      Hashtable hashtable1 = CultureInfo.s_NameCachedCultures;
      if (name != null)
        name = CultureData.AnsiToLower(name);
      if (altName != null)
        altName = CultureData.AnsiToLower(altName);
      if (hashtable1 == null)
      {
        hashtable1 = Hashtable.Synchronized(new Hashtable());
      }
      else
      {
        switch (lcid)
        {
          case -1:
            CultureInfo cultureInfo1 = (CultureInfo) hashtable1[(object) (name + "�" + altName)];
            if (cultureInfo1 != null)
              return cultureInfo1;
            break;
          case 0:
            CultureInfo cultureInfo2 = (CultureInfo) hashtable1[(object) name];
            if (cultureInfo2 != null)
              return cultureInfo2;
            break;
        }
      }
      Hashtable hashtable2 = CultureInfo.s_LcidCachedCultures;
      if (hashtable2 == null)
        hashtable2 = Hashtable.Synchronized(new Hashtable());
      else if (lcid > 0)
      {
        CultureInfo cultureInfo3 = (CultureInfo) hashtable2[(object) lcid];
        if (cultureInfo3 != null)
          return cultureInfo3;
      }
      CultureInfo cultureInfo4;
      try
      {
        switch (lcid)
        {
          case -1:
            cultureInfo4 = new CultureInfo(name, altName);
            break;
          case 0:
            cultureInfo4 = new CultureInfo(name, false);
            break;
          default:
            cultureInfo4 = new CultureInfo(lcid, false);
            break;
        }
      }
      catch (ArgumentException ex)
      {
        return (CultureInfo) null;
      }
      cultureInfo4.m_isReadOnly = true;
      if (lcid == -1)
      {
        hashtable1[(object) (name + "�" + altName)] = (object) cultureInfo4;
        cultureInfo4.TextInfo.SetReadOnlyState(true);
      }
      else
      {
        string lower = CultureData.AnsiToLower(cultureInfo4.m_name);
        hashtable1[(object) lower] = (object) cultureInfo4;
        if ((cultureInfo4.LCID != 4 || !(lower == "zh-hans")) && (cultureInfo4.LCID != 31748 || !(lower == "zh-hant")))
          hashtable2[(object) cultureInfo4.LCID] = (object) cultureInfo4;
      }
      if (-1 != lcid)
        CultureInfo.s_LcidCachedCultures = hashtable2;
      CultureInfo.s_NameCachedCultures = hashtable1;
      return cultureInfo4;
    }

    /// <summary>
    ///   Служит для получения кешированного доступного только для чтения экземпляра языка и региональных параметров с помощью указанного идентификатора языка и региональных параметров.
    /// </summary>
    /// <param name="culture">Идентификатор языка (LCID).</param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, доступный только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="culture" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="culture" /> указывает язык и региональные параметры, которые не поддерживаются.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    public static CultureInfo GetCultureInfo(int culture)
    {
      if (culture <= 0)
        throw new ArgumentOutOfRangeException(nameof (culture), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(culture, (string) null, (string) null);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException(nameof (culture), culture, Environment.GetResourceString("Argument_CultureNotSupported"));
      return cultureInfoHelper;
    }

    /// <summary>
    ///   Возвращает кешированный экземпляр языка и региональных параметров с помощью указанного имени.
    /// </summary>
    /// <param name="name">
    ///   Имя языка и региональных параметров.
    ///   <paramref name="name" /> не учитывает регистр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, доступный только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> указывает язык и региональные параметры, которые не поддерживаются.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    public static CultureInfo GetCultureInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(0, name, (string) null);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException(nameof (name), name, Environment.GetResourceString("Argument_CultureNotSupported"));
      return cultureInfoHelper;
    }

    /// <summary>
    ///   Служит для получения кешированного экземпляра языка и региональных параметров, доступного только для чтения.
    ///    В параметрах определяется язык и региональные параметры, которые инициализируются вместе с объектами <see cref="T:System.Globalization.TextInfo" /> и <see cref="T:System.Globalization.CompareInfo" />, заданными другим языком и региональными параметрами.
    /// </summary>
    /// <param name="name">
    ///   Имя языка и региональных параметров.
    ///   <paramref name="name" /> не учитывает регистр.
    /// </param>
    /// <param name="altName">
    ///   Имя языка и региональных параметров, предоставляющих объекты <see cref="T:System.Globalization.TextInfo" /> и <see cref="T:System.Globalization.CompareInfo" /> для инициализации параметра <paramref name="name" />.
    ///   <paramref name="altName" /> не учитывает регистр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, доступный только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> или <paramref name="altName" /> равно null.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> или <paramref name="altName" /> указывает язык и региональные параметры, которые не поддерживаются.
    ///    Дополнительные сведения см. в разделе "Примечания к вызывающим объектам".
    /// </exception>
    public static CultureInfo GetCultureInfo(string name, string altName)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (altName == null)
        throw new ArgumentNullException(nameof (altName));
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(-1, name, altName);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException("name or altName", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_OneOfCulturesNotSupported"), (object) name, (object) altName));
      return cultureInfoHelper;
    }

    /// <summary>
    ///   Не рекомендуется.
    ///    Служит для получения объекта <see cref="T:System.Globalization.CultureInfo" />, доступного только для чтения, который имеет языковые характеристики, указываемые определенным языковым тегом RFC 4646.
    /// </summary>
    /// <param name="name">Имя языка по стандарту RFC 4646.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, доступный только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    ///   <paramref name="name" /> не соответствует поддерживаемому языку и региональным параметрам.
    /// </exception>
    public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
    {
      if (name == "zh-CHT" || name == "zh-CHS")
        throw new CultureNotFoundException(nameof (name), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), (object) name));
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
      if (cultureInfo.LCID > (int) ushort.MaxValue || cultureInfo.LCID == 1034)
        throw new CultureNotFoundException(nameof (name), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), (object) name));
      return cultureInfo;
    }

    internal static bool IsTaiwanSku
    {
      get
      {
        if (!CultureInfo.s_haveIsTaiwanSku)
        {
          CultureInfo.s_isTaiwanSku = CultureInfo.GetSystemDefaultUILanguage() == "zh-TW";
          CultureInfo.s_haveIsTaiwanSku = true;
        }
        return CultureInfo.s_isTaiwanSku;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetLocaleInfoEx(string localeName, uint field);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeGetLocaleInfoExInt(string localeName, uint field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool nativeSetThreadLocale(string localeName);

    [SecurityCritical]
    private static string GetDefaultLocaleName(int localeType)
    {
      string s = (string) null;
      if (CultureInfo.InternalGetDefaultLocaleName(localeType, JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetDefaultLocaleName(int localetype, StringHandleOnStack localeString);

    [SecuritySafeCritical]
    private static string GetUserDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetUserDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetUserDefaultUILanguage(StringHandleOnStack userDefaultUiLanguage);

    [SecuritySafeCritical]
    private static string GetSystemDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetSystemDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetSystemDefaultUILanguage(StringHandleOnStack systemDefaultUiLanguage);
  }
}
