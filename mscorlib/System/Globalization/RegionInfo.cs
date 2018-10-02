// Decompiled with JetBrains decompiler
// Type: System.Globalization.RegionInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>Содержит сведения о стране или регионе.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class RegionInfo
  {
    private static readonly int[] IdFromEverettRegionInfoDataItem = new int[110]
    {
      14337,
      1052,
      1067,
      11274,
      3079,
      3081,
      1068,
      2060,
      1026,
      15361,
      2110,
      16394,
      1046,
      1059,
      10249,
      3084,
      9225,
      2055,
      13322,
      2052,
      9226,
      5130,
      1029,
      1031,
      1030,
      7178,
      5121,
      12298,
      1061,
      3073,
      1027,
      1035,
      1080,
      1036,
      2057,
      1079,
      1032,
      4106,
      3076,
      18442,
      1050,
      1038,
      1057,
      6153,
      1037,
      1081,
      2049,
      1065,
      1039,
      1040,
      8201,
      11265,
      1041,
      1089,
      1088,
      1042,
      13313,
      1087,
      12289,
      5127,
      1063,
      4103,
      1062,
      4097,
      6145,
      6156,
      1071,
      1104,
      5124,
      1125,
      2058,
      1086,
      19466,
      1043,
      1044,
      5129,
      8193,
      6154,
      10250,
      13321,
      1056,
      1045,
      20490,
      2070,
      15370,
      16385,
      1048,
      1049,
      1025,
      1053,
      4100,
      1060,
      1051,
      2074,
      17418,
      1114,
      1054,
      7169,
      1055,
      11273,
      1028,
      1058,
      1033,
      14346,
      1091,
      8202,
      1066,
      9217,
      1078,
      12297
    };
    internal string m_name;
    [NonSerialized]
    internal CultureData m_cultureData;
    internal static volatile RegionInfo s_currentRegionInfo;
    [OptionalField(VersionAdded = 2)]
    private int m_cultureId;
    [OptionalField(VersionAdded = 2)]
    internal int m_dataItem;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Globalization.RegionInfo" /> класс на основе страны или региона или определенного языка и региональных параметров, заданных именем.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая код из двух букв, определенный в формате ISO 3166 для страны или региона.
    /// 
    ///   -или-
    /// 
    ///   Строка, содержащая имя языка и региональных параметров для определенного языка и региональных параметров, пользовательского языка или региональных параметров или языка и региональных параметров, свойственных только для ОС Windows.
    ///    Если имя языка и региональных параметров не указано в формате RFC 4646, в приложении должно быть указано имя языка и региональных параметров полностью, а не только название страны или региона.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> не является допустимым Страна или регион или имя определенного языка и региональных параметров.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public RegionInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
      this.m_cultureData = CultureData.GetCultureDataForRegion(name, true);
      if (this.m_cultureData == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), (object) name), nameof (name));
      if (this.m_cultureData.IsNeutralCulture)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNeutralRegionName", (object) name), nameof (name));
      this.SetName(name);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Globalization.RegionInfo" /> класс в зависимости от страны или региона, связанного с указанным идентификатором.
    /// </summary>
    /// <param name="culture">
    ///   Идентификатор языка и региональных параметров.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="culture" />Указывает инвариантный, пользовательский или нейтральный язык и региональные параметры.
    /// </exception>
    [SecuritySafeCritical]
    public RegionInfo(int culture)
    {
      if (culture == (int) sbyte.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
      if (culture == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", (object) culture), nameof (culture));
      if (culture == 3072)
        throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", (object) culture), nameof (culture));
      this.m_cultureData = CultureData.GetCultureData(culture, true);
      this.m_name = this.m_cultureData.SREGIONNAME;
      if (this.m_cultureData.IsNeutralCulture)
        throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", (object) culture), nameof (culture));
      this.m_cultureId = culture;
    }

    [SecuritySafeCritical]
    internal RegionInfo(CultureData cultureData)
    {
      this.m_cultureData = cultureData;
      this.m_name = this.m_cultureData.SREGIONNAME;
    }

    [SecurityCritical]
    private void SetName(string name)
    {
      this.m_name = name.Equals(this.m_cultureData.SREGIONNAME, StringComparison.OrdinalIgnoreCase) ? this.m_cultureData.SREGIONNAME : this.m_cultureData.CultureName;
    }

    [SecurityCritical]
    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name == null)
        this.m_cultureId = RegionInfo.IdFromEverettRegionInfoDataItem[this.m_dataItem];
      this.m_cultureData = this.m_cultureId != 0 ? CultureData.GetCultureData(this.m_cultureId, true) : CultureData.GetCultureDataForRegion(this.m_name, true);
      if (this.m_cultureData == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), (object) this.m_name), "m_name");
      if (this.m_cultureId == 0)
        this.SetName(this.m_name);
      else
        this.m_name = this.m_cultureData.SREGIONNAME;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Globalization.RegionInfo" /> представляющий страны или региона, используемые текущим потоком.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Globalization.RegionInfo" /> Представляющий страны или региона, используемые текущим потоком.
    /// </returns>
    [__DynamicallyInvokable]
    public static RegionInfo CurrentRegion
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        RegionInfo regionInfo = RegionInfo.s_currentRegionInfo;
        if (regionInfo == null)
        {
          regionInfo = new RegionInfo(CultureInfo.CurrentCulture.m_cultureData);
          regionInfo.m_name = regionInfo.m_cultureData.SREGIONNAME;
          RegionInfo.s_currentRegionInfo = regionInfo;
        }
        return regionInfo;
      }
    }

    /// <summary>
    ///   Возвращает имя или двухбуквенный страны или региона по стандарту ISO 3166 для текущего <see cref="T:System.Globalization.RegionInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Значение, заданное параметром <paramref name="name" /> параметр <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> конструктора.
    ///    Возвращается значение в верхнем регистре.
    /// 
    ///   -или-
    /// 
    ///   Двухбуквенный код, определенный в формате ISO 3166 для страны или региона, определяемое <paramref name="culture" /> параметр <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> конструктора.
    ///    Возвращается значение в верхнем регистре.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_name;
      }
    }

    /// <summary>
    ///   Возвращает полное имя страны или региона на английском языке.
    /// </summary>
    /// <returns>
    ///   Полное название страны или региона на английском языке.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string EnglishName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SENGCOUNTRY;
      }
    }

    /// <summary>
    ///   Возвращает полное имя страны или региона на языке локализованной версии .NET Framework.
    /// </summary>
    /// <returns>
    ///   Полное имя страны или региона на языке локализованной версии .NET Framework.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string DisplayName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SLOCALIZEDCOUNTRY;
      }
    }

    /// <summary>
    ///   Получает название страны или региона, отформатированное в соответствии с родным языком страны или региона.
    /// </summary>
    /// <returns>
    ///   Собственное имя страны или региона, отформатированное в язык, связанный с кодом страны или региона ISO 3166.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual string NativeName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SNATIVECOUNTRY;
      }
    }

    /// <summary>
    ///   Возвращает код из двух букв, определенный в формате ISO 3166 для страны или региона.
    /// </summary>
    /// <returns>
    ///   Код из двух букв, определенный в формате ISO 3166 для страны или региона.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string TwoLetterISORegionName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SISO3166CTRYNAME;
      }
    }

    /// <summary>
    ///   Возвращает код из трех букв, определенный в формате ISO 3166 для страны или региона.
    /// </summary>
    /// <returns>
    ///   Код из трех букв, определенный в формате ISO 3166 для страны или региона.
    /// </returns>
    public virtual string ThreeLetterISORegionName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SISO3166CTRYNAME2;
      }
    }

    /// <summary>
    ///   Возвращает трехбуквенный код, присвоенный операционной системой Windows стране или региону, представленный этим <see cref="T:System.Globalization.RegionInfo" />.
    /// </summary>
    /// <returns>
    ///   Трехбуквенный код, присвоенный операционной системой Windows стране или региону, представленный этим <see cref="T:System.Globalization.RegionInfo" />.
    /// </returns>
    public virtual string ThreeLetterWindowsRegionName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SABBREVCTRYNAME;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, использует ли страна или регион метрическую систему.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если страна или регион использует метрическую систему мер; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsMetric
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IMEASURE == 0;
      }
    }

    /// <summary>
    ///   Получает уникальный номер идентификации географического региона, страны, города или местности.
    /// </summary>
    /// <returns>
    ///   32-битное число со знаком, которое служит для уникального определения географического местоположения.
    /// </returns>
    [ComVisible(false)]
    public virtual int GeoId
    {
      get
      {
        return this.m_cultureData.IGEOID;
      }
    }

    /// <summary>
    ///   Возвращает имя денежной единицы, используемой в стране или регионе, на английском языке.
    /// </summary>
    /// <returns>
    ///   Имя денежной единицы, используемой в стране или регионе, на английском языке.
    /// </returns>
    [ComVisible(false)]
    public virtual string CurrencyEnglishName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SENGLISHCURRENCY;
      }
    }

    /// <summary>
    ///   Возвращает имя денежной единицы, используемой в стране или регионе, отформатированное в соответствии с родным языком страны или региона.
    /// </summary>
    /// <returns>
    ///   Собственное имя валюты, используемой в стране или регионе, отформатированное в язык, связанный с кодом страны или региона ISO 3166.
    /// </returns>
    [ComVisible(false)]
    public virtual string CurrencyNativeName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SNATIVECURRENCY;
      }
    }

    /// <summary>
    ///   Возвращает символ денежной единицы, связанной со страной или регионом.
    /// </summary>
    /// <returns>
    ///   Символ денежной единицы, связанной со страной или регионом.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string CurrencySymbol
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SCURRENCY;
      }
    }

    /// <summary>
    ///   Возвращает трехзначный символ денежной единицы в формате ISO 4217, связанный со страной или регионом.
    /// </summary>
    /// <returns>
    ///   Трехзначный символ денежной единицы в формате ISO 4217, связанный со страной или регионом.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string ISOCurrencySymbol
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SINTLSYMBOL;
      }
    }

    /// <summary>
    ///   Определяет, является ли указанный объект тем же экземпляром, как текущий <see cref="T:System.Globalization.RegionInfo" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Globalization.RegionInfo" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> параметр <see cref="T:System.Globalization.RegionInfo" /> объекта и его <see cref="P:System.Globalization.RegionInfo.Name" /> имеет то же <see cref="P:System.Globalization.RegionInfo.Name" /> свойство текущего <see cref="T:System.Globalization.RegionInfo" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      RegionInfo regionInfo = value as RegionInfo;
      if (regionInfo != null)
        return this.Name.Equals(regionInfo.Name);
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего класса <see cref="T:System.Globalization.RegionInfo" /> для использования в алгоритмах и структурах данных хеширования, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Globalization.RegionInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>
    ///   Возвращает строку, содержащую имя языка и региональных параметров или кодов двухбуквенный страны или региона ISO 3166 для текущего <see cref="T:System.Globalization.RegionInfo" />.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя языка и региональных параметров или кодов двухбуквенный страны или региона в формате ISO 3166, определенные для текущего <see cref="T:System.Globalization.RegionInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Name;
    }
  }
}
